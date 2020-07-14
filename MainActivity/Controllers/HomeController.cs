using Microsoft.AspNetCore.Mvc;
using MainActivity.Models;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
using System;

namespace MainActivity.Controllers {
    public class HomeController : Controller {
        //**> 
        private const string ARTICLE_SAVED_DATA_FILE = "ArticleContent.xml";
        private readonly string rootPath;
        private static ArticleContent content;
        //**> 

        public HomeController(IWebHostEnvironment hostEnvironment) {
            // Gets Project Root Path 
            this.rootPath = hostEnvironment.ContentRootPath;
        }



        //[Index]
        [HttpGet]
        public IActionResult Index() {
            try {
                // Gets File Path Of The XML Document s
                XDocument articleDocument = XDocument.Load(rootPath + "/" + ARTICLE_SAVED_DATA_FILE ?? null);
                content = new ArticleContent {
                    // *** Extra A Single Element From XML File ***
                    // Gets [Title] Value From XML Element 
                    Title = articleDocument.Root.Element("Title").Value,
                    // Gets [ParagraphTitle] Value From XML Element 
                    ParagraphTitle = articleDocument.Root.Element("ParagraphTitle").Value,
                    // Gets [ParagraphContent] Value From XML Element 
                    ParagraphContent = articleDocument.Root.Element("ParagraphContent").Value
                };
            } catch (Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            // Returns Article Content To The View
            return View(content);
        }

    }// CLASS ENDS

}

