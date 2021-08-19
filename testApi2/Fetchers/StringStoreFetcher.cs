using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using testApi2.models;
using testApi2.Fetchers.CoreContext;

namespace testApi2.Fetchers
{
    // inteface used for dependency injection
    public interface IStringStoreFetcher
    {
        Task CreateAsync(StringStoreModel obj);
        Task<StringStoreModel> Get(int id);
        bool Update(StringStoreModel obj);
    }
    public class StringStoreFetcher : IStringStoreFetcher
    {
        private DbSet<StringStoreModel> _entitySet;
        private ApplicationDbCoreContext _dbContext;

        // the constructor used for getting required db context values
        public StringStoreFetcher(ApplicationDbCoreContext _DbContext)
        {
            // a db conext for a specific model (StringStoreModel)
            _entitySet = _DbContext.Set<StringStoreModel>();

            // a db context for the entire database
            _dbContext = _DbContext;
        }

        public async Task CreateAsync(StringStoreModel obj)
        {
            // uses the specific context to create a new row in dbcontext (the database is not updated with this method)
            await _entitySet.AddAsync(obj);
            // saving the previous changes to the database
            // as of my testing this will only update changes that occured in this function (not sure why)
            await _dbContext.SaveChangesAsync();
            return;
        }

        public async Task<StringStoreModel> Get(int id)
        {
            // grabbing a specific entity (StringStoreModel) from the database where the id equals the id given
            return await _entitySet
                .Where(e => e.id.Equals(id))
                .SingleOrDefaultAsync();
        }

        public bool Update(StringStoreModel obj)
        {
           // taking object , finding a matching object in the database and updating it 
           // not sure exactly how this works but from my testing the id will match and it used that to target a specific row
           bool result = _entitySet.Update(obj) != null;
           if (result)
                // saving the changes that was made in a non async fasion
                _dbContext.SaveChanges();
           return result;
        }
    }
}
