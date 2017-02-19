namespace Sample.Domain.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Sample.Domain.SampleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Sample.Domain.SampleContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

            IEnumerable<Flavor> flavors = from Sample.Domain.FlavorEnum flavor in Enum.GetValues(typeof(Sample.Domain.FlavorEnum))
                          select new Flavor() { Name = flavor.ToString(), Id = (int)flavor };

            flavors.ToList().ForEach(x => {
                context.Flavors.AddOrUpdate(
                  p => p.Id,
                    x
                );

            });
        }
    }
}
