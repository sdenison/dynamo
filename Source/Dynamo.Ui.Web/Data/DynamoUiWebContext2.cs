using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Dynamo.Ui.Web.Data;

namespace Dynamo.Ui.Web.Data
{
    public class DynamoUiWebContext2(DbContextOptions<DynamoUiWebContext2> options) : IdentityDbContext<DynamoUiWebUser>(options)
    {
    }
}
