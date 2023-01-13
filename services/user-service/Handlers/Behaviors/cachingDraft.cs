
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


//return await CacheResponseAsync<IEnumerable<string>>("GetFleets",
//    () => GetFleets(), TimeSpan.FromHours(1));