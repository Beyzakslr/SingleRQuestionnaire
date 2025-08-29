using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;


[CollectionName("Roller")]
public class ApplicationRole : MongoIdentityRole<Guid>
{
}
