using CommandLine;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DddExample.Infrastructure.Data
{
    public class DesignTimeDatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(options =>
                {
                    if (options.Connection != null)
                    {
                        optionsBuilder.UseSqlServer(options.Connection);
                    }
                    else
                    {
                        optionsBuilder.UseSqlServer();
                    }
                })
                .WithNotParsed(_ => optionsBuilder.UseSqlServer());

            var context = new DatabaseContext(optionsBuilder.Options);
            
            return context;
        }

        private class Options
        {
            [Option('c', "connection", Required = false, HelpText = "Set database connection string.")]
            public string Connection { get; set; }
        }
    }
}