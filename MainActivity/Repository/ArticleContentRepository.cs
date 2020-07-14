using MainActivity.Models;
using MainActivity.Services;
using System.Collections.Generic;
using System;

namespace MainActivity.Repository {
    public sealed class ArticleContentRepository : ICompany<ArticleContent> {
        private readonly PrimeDbContext dbContext;


        // [Constructor] 
        public ArticleContentRepository(PrimeDbContext dbContext) {
            this.dbContext = dbContext;
        }



        // [Finds All] References 
        public IEnumerable<ArticleContent> GetAllReferences => dbContext.ArticleContents;


        // [Adds] Reference By Reference Type 
        public ArticleContent Add(ArticleContent reference) {
            try {
                dbContext.ArticleContents.Add(reference);
                dbContext.SaveChanges();
            } catch (NullReferenceException exception) {
                System.Diagnostics.Debug.WriteLine(exception.Message);
            }
            return (reference);
        }

        // [Deletes] Reference By Reference Type 
        public ArticleContent Delete(int? id) {
            ArticleContent content = dbContext.ArticleContents.Find(id);
            dbContext.ArticleContents.Remove(content);
            dbContext.SaveChanges();
            return (content);

        }

        // [Finds] Reference By ID 
        public ArticleContent GetReference(int? id) {
            return (this.dbContext.ArticleContents.Find(id));
        }

        // [Updates] Reference By Reference Type 
        public ArticleContent Update(ArticleContent reference) {
            ArticleContent content = dbContext.ArticleContents.Find(reference);
            dbContext.ArticleContents.Update(content);
            dbContext.SaveChanges();
            return (content);
        }
    }// Class Ends 
}
