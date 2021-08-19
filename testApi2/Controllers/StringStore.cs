using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using testApi2.Fetchers;
using testApi2.models;

namespace testApi2.Controllers
{
    [ApiController]
    // this is defining the base route for this controller
    [Route("/")]
    public class stringStore : ControllerBase
    {
        private readonly IStringStoreFetcher _fetcher;

        // this constructor uses the Inteface for the fetcher class
        // dotnet will automatically inject the required class as defined in the startup.cs
        // the ijected class will be automaticallyu instanciated
        public stringStore(IStringStoreFetcher fetcher) {

            // since we use the fetcher class to access the databse 
            // if you wish to see how the db is accessed i reccomend you checkout the fetcher class
            _fetcher = fetcher;
        }

        [HttpGet] // this allows only get methods to be sent to this method
        
        [Route("{id}")] // this defines the route for this specific method
        // e.g "localhost/<id>"
        public async Task<StringStoreModel> Get(int id)
        {
            StringStoreModel data = await _fetcher.Get(id);
            return data;
        }
        
        [HttpPost] // this allows only post methods to be sent to this method

        // since we did not define a route this method will take the route specified at the start of this class ("/")
        public async Task<StringStoreModel> Post([FromBody] StringStoreModel data)
        {
            await _fetcher.CreateAsync(data);
            return data;
        }

        [HttpPatch] // this allows only post methods to be sent to this method

        [Route("{id}")] // this defines the route for this specific method
        // e.g "localhost/<id>"
        public async Task<StringStoreModel> Patch(int id, [FromBody] StringStoreModel data)
        {
            StringStoreModel OldData = await Get(id);
            OldData.data = data.data;
            _fetcher.Update(OldData);
            return OldData;
        }
    }
}
