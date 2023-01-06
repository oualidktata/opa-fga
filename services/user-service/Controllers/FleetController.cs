using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Reflection.Metadata.Ecma335;
using user_service.Controllers;

namespace users_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class FleetController : BaseController
    {
        private static readonly IEnumerable<string> Fleets = new[]
        {
        "Fleet1", "Fleet2", "Fleet3", "Fleet4", "Fleet5"
        };


        private readonly ILogger<FleetController> _logger;

        public FleetController(ILogger<FleetController> logger, IConnectionMultiplexer connectionMultiplexer) : base(connectionMultiplexer)
        {
            _logger = logger;
            //HttpClientHandler handler= new HttpClientHandler();
            //handler.Credentials
        }
        //        Task.FromResult(Enumerable.Range(1, 5).Select(index => new Fleet
        //            {
        //                Date = DateTime.Now.AddDays(index),
        //                ESN = { ESNNumber = $"ESN {Random.Shared.Next(Fleets.Length)}", Name = Fleets[Random.Shared.Next(Fleets.Length)]
        //    },
        //                Summary = Fleets[Random.Shared.Next(Fleets.Length)]
        //}))
        //[HttpGet(Name = "GetFleets")]
        ////[Authorize(Policy ="can-read-fleet")]
        //public async Task<IEnumerable<Fleet>> Get()
        //{
        //    return await CacheResponseAsync<IEnumerable<Fleet>>("GetFleets", () => Task.FromResult(Fleets.AsEnumerable<Fleet>)

        //    , TimeSpan.FromHours(1));
        //}
        [HttpGet(Name = "GetFleets")]
        //[Authorize(Policy ="can-read-fleet")]
        public async Task<IEnumerable<string>> Get()
        {
            return await CacheResponseAsync<IEnumerable<string>>("GetFleets",
                () => GetFleets(), TimeSpan.FromHours(1));
        }

        private Task<IEnumerable<string>> GetFleets()
        {
            return Task.FromResult<IEnumerable<string>>(Fleets);
        }

        //[HttpPost(Name = "AddFleet")]
        //[Authorize("can-add-fleet")]
        //public IEnumerable<Fleet> AddFleet()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new Fleet
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        ESN = { ESNNumber = "ESN1234", Name = "Something" },
        //        Summary = Fleets[Random.Shared.Next(Fleets.Length)]
        //    })
        //    .ToArray();
        //}

        //[HttpPost(Name = "UploadData")]
        //[Authorize("can-upload-data")]
        //public IEnumerable<Fleet> Upload()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new Fleet
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        ESN = { ESNNumber = "ESN1234", Name = "Something" },
        //        Summary = Fleets[Random.Shared.Next(Fleets.Length)]
        //    })
        //    .ToArray();
        //}

    }
}