using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using testApi2.models;

namespace testApi2.Fetchers.CoreContext
{
    public class ApplicationDbCoreContext : DbContext
    {
        // this is used to build a dbcontext specifically for our application 
        // hence the name applicationDbCoreConetext
        public ApplicationDbCoreContext(DbContextOptions<ApplicationDbCoreContext> options) : base(options) { }

        // this creates a new dbset method that will be used specifically for our stringStoreModel table
        // using our predefined model in Models/StringStoreModel
        // Dbset is defined the microsoft entity framework
        public DbSet<StringStoreModel> StringStore { get; set; }
    }
}
