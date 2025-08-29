using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SingleRQuestionnaire.Data;

public class SingleRQuestionnaireContext : IdentityDbContext<IdentityUser>
{
    public SingleRQuestionnaireContext(DbContextOptions<SingleRQuestionnaireContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
