using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dynamo.Ui.Web;

namespace Dynamo.Ui.Web.Data
{
    public class DynamoUiWebContext : DbContext
    {
        public DynamoUiWebContext (DbContextOptions<DynamoUiWebContext> options)
            : base(options)
        {
        }

        public DbSet<Dynamo.Ui.Web.Movie> Movie { get; set; } = default!;
    }
}
