using Redis.OM;
using Redis.OM.Searching;
using user_service_core.Entities;

namespace user_service.DataHelpers
{
    public static class SeedHelpers
    {
        public static async Task SeedUsers(IRedisCollection<UserEntity> collection)
        {
            await collection.InsertAsync(new UserEntity() { Id=new Uuid4IdGenerationStrategy().GenerateId(), FirstName = "Oualid", LastName = "Ktata", Email = "oualid@ktata.net" });
            await collection.InsertAsync(new UserEntity() { Id = new Uuid4IdGenerationStrategy().GenerateId(), FirstName = "Walter", LastName = "White", Email = "walter@white.net" });
            await collection.InsertAsync(new UserEntity() { Id = new Uuid4IdGenerationStrategy().GenerateId(), FirstName = "Saul", LastName = "Goodman", Email = "Saul@Goodman.net" });
        }
    }
}
