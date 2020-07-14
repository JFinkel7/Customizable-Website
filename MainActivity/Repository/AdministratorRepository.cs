using MainActivity.Models;
using MainActivity.Services;
using System;
using System.Collections.Generic;

namespace MainActivity.Repository {
    public sealed class AdministratorRepository : ICompany<Administrator> {
        private readonly PrimeDbContext dbContext;

        // [Constructor] 
        public AdministratorRepository(PrimeDbContext dbContext) {
            this.dbContext = dbContext;
        }



        // [Finds All] References 
        public IEnumerable<Administrator> GetAllReferences => dbContext.Administrators;


        // [Adds] Reference By Reference Type 
        public Administrator Add(Administrator reference) {
            try {
                dbContext.Administrators.Add(reference);
                dbContext.SaveChanges();
            } catch (NullReferenceException exception) {
                System.Diagnostics.Debug.WriteLine(exception.Message);
            }
            return (reference);
        }

        // [Deletes] Reference By Reference Type 
        public Administrator Delete(int? id) {
            Administrator adminID = dbContext.Administrators.Find(id);
            dbContext.Administrators.Remove(adminID);
            dbContext.SaveChanges();
            return (adminID);

        }

        // [Finds] Reference By ID 
        public Administrator GetReference(int? id) {
            return (this.dbContext.Administrators.Find(id));
        }

        // [Updates] Reference By Reference Type 
        public Administrator Update(Administrator reference) {
            Administrator admin = dbContext.Administrators.Find(reference);
            dbContext.Administrators.Update(admin);
            return (admin);
        }


    }// CLASS ENDS 
}
