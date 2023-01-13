//using MediatR;
//using Microsoft.IdentityModel.Tokens;
//using MongoDB.Bson;
//using MongoDB.Driver;
//using user_service_core;
//using users_api;

//namespace user_service.Handlers
//{
//    public class GetUsersRequestHandler : IRequestHandler<GetUsersRequestHandler.GetUsersRequest, GetUsersRequestHandler.GetUsersResponse>
//    {
//        private readonly IMongoClient _client;
//        private readonly IMongoDatabase _database;

//        public GetUsersRequestHandler(IMongoClient client)
//        {
//            _client=client;
//            _database=_client.GetDatabase("UsersDB");
//        }
     
//        Task<GetUsersResponse> IRequestHandler<GetUsersRequest, GetUsersResponse>.Handle(GetUsersRequest request, CancellationToken cancellationToken)
//        {
//            var collection = _database.GetCollection<UserDTO>("users");
//            var users = collection.Find(new BsonDocument()).ToList();


//            //var filter = Builders<User>.Filter.Eq("Name", "John");
//            //var users = collection.Find(filter).ToList();

//            return users.IsNullOrEmpty() ? Task.FromResult(new GetUsersResponse() { Users = users }) : Task.FromResult(new GetUsersResponse());


//            //return await CacheResponseAsync<IEnumerable<UserDTO>>("users", () => Task.FromResult(Fleets.AsEnumerable<Fleet>), TimeSpan.FromHours(1));
//        }

//        public class GetUsersRequest : IRequest<GetUsersResponse>
//        {
//        }
//        public record GetUsersResponse
//        {
//           public IEnumerable<UserDTO> Users { get; set; }
//        }
//    }
//}
