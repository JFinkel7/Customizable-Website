using System.Collections.Generic;

namespace MainActivity.Services {
    public interface ICompany<T> {

        // Gets Company Reference By [ID]
        public abstract T GetReference(int? id);

        // Gets Company Reference By [Enumerable]
        public abstract IEnumerable<T> GetAllReferences { get; }

        // [Adds] A Company Reference
        public abstract T Add(T reference);

        // [Update] A Company Reference
        public abstract T Update(T reference);

        // [Deletes] A Company Reference By (ID)
        public abstract T Delete(int? id);

    }// Class ENDS 
}
