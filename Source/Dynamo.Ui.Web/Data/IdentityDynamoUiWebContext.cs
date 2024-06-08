using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Dynamo.Ui.Web.Data
{
    public class IdentityDynamoUiWebContext(DbContextOptions<IdentityDynamoUiWebContext> options) : IdentityDbContext<IdentityUser>(options)
    {
    }
}
