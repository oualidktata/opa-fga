using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Redis.OM.Modeling;

namespace user_service_core.Entities
{
    //[Document(StorageType=StorageType.Json,Prefixes = new[] {"User"})]
    [Document(StorageType=StorageType.Json,Prefixes = new[] {"User"})]
    public class UserEntity
    {
       
        [RedisIdField][Indexed] public string Id { get; set; }
        [Indexed] public string FirstName { get; set; }
        [Indexed] public string LastName { get; set; }
        [Indexed] public string Email { get; set; }
        //[Searchable] public string Description { get; set; }

        //[Indexed(CascadeDepth =1)]
        //public Address HomeAddress {get;set;}
    }
}
