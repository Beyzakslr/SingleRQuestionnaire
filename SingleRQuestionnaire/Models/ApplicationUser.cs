using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace SingleRQuestionnaire.Models
{
    [CollectionName("Kullanicilar")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
    }
}
