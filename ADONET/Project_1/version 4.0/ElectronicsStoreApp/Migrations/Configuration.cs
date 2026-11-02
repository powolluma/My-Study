// Миграция 
namespace ElectronicsStoreApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ElectronicsStoreApp.ShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ElectronicsStoreApp.ShopContext context)
        {
        }
    }
}
