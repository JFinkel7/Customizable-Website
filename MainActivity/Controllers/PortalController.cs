using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MainActivity.Models;
using MainActivity.Services;
using Newtonsoft.Json;
using System;
using System.Xml;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MainActivity.Controllers {
    public class PortalController : Controller {
        // Class Fields
        private readonly ICompany<ArticleContent> company;
        private const string ARTICLE_FILE = "ArticleContent.xml";
        private const string TAG = "PortalController";
        private const string MASTER = "Master";
        private const string ADMIN = "Admin";
        private readonly string rootPath;

        // Constructor
        public PortalController(IWebHostEnvironment hostEnvironment, ICompany<ArticleContent> company) {
            // Gets Project Root Path 
            this.rootPath = hostEnvironment.ContentRootPath;
            this.company = company;
        }



        //============================== Dashboard ============================== 

        // - GET
        [HttpGet]
        [Authorize(Roles = MASTER)]
        public IActionResult Dashboard() {
            try {
                // Request The Saved Cookie Data
                string loggedInAdmin = HttpContext.Request.Cookies[AccountController.COOKIE_KEY];
                // Deserialize Cookie Data 
                Administrator registeredAdmin = JsonConvert.DeserializeObject<Administrator>(loggedInAdmin);
                // Store The Cookie Data In ViewBag
                ViewBag.Administrator = new Administrator {
                    ID = registeredAdmin.ID,
                    Name = registeredAdmin.Name,
                    Email = registeredAdmin.Email,
                    Password = registeredAdmin.Password,
                    Role = registeredAdmin.Role
                };
            } catch (Exception exception) {
                // Show Error Message
                System.Diagnostics.Debug.WriteLine(TAG + exception.Message);
                return RedirectToAction("Index", "Home");
            }
            return View();

        }

        //============================== CREATE ============================== 


        // - GET
        [HttpGet]
        public IActionResult Create() => View();


        // - POST
        [HttpPost]
        public IActionResult Create(ArticleContent article) {
            if (ModelState.IsValid) {
                // Add The New Article Contents To SQLite Database
                company.Add(article);
                return RedirectToActionPermanent("Dashboard");
            }
            return View();
        }



        //============================== [EDIT] ============================== 

        // - GET
        [HttpGet]
        public IActionResult Edit() {
            if (company != null) {
                // Get A List Of All Saved Articles
                return View(company.GetAllReferences);
            }
            return View();

        }


        //============================== DELETE ============================== 
        public IActionResult Delete(int id) {
            if (ModelState.IsValid) {
                // Delete Article Based On The ID
                company.Delete(id);
            }
            return View();
        }


        //============================== SAVE ============================== 
        public IActionResult Save(int? id) {
            ArticleContent savedContent = company.GetReference(id);
            if (ModelState.IsValid) {
                // Save Article To The XML File
                SaveToXML(savedContent);
                return RedirectToAction("Index", "Home");
            }
            return View("Edit");

        }



        //============================== Controller Methods ============================== 
        private void SaveToXML(ArticleContent article) {
            // Creating Xml Settings For Our XML File
            XmlWriterSettings settings = new XmlWriterSettings() {
                Async = true,
                Indent = true,
                IndentChars = ("    "),
                CloseOutput = true,
                OmitXmlDeclaration = true

            };

            // Writes Contents To The New XML File 
            try {
                new System.Threading.Thread(() => {
                    using (var stream = System.IO.File.Open(ARTICLE_FILE, FileMode.Create, FileAccess.Write, FileShare.Read)) {
                        XmlWriter writer = XmlWriter.Create(stream, settings);
                        writer.WriteStartElement("ArticleContent");
                        writer.WriteElementString("Title", article.Title);
                        writer.WriteElementString("ParagraphTitle", article.ParagraphTitle);
                        writer.WriteElementString("ParagraphContent", article.ParagraphContent);
                        writer.WriteEndElement();
                        writer.Flush();
                    }

                }).Start();

            } catch (Exception exception) {
                System.Diagnostics.Debug.WriteLine(exception.Message);
            }
        }
    }// CLASS ENDS 
}
