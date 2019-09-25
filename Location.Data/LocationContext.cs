using Location.Data.Configurations;
using Location.Data.CustomConventions;
using Location.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationVoiture.Data
{
    public class LocationContext : DbContext
    {
        public LocationContext() : base("Name=RentDB")//nom de la chaine
        {

        }
        //les dbset
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CreationYear> CreationYears { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<TypeCar> TypeCars { get; set; }
        public DbSet<Claims> Claims { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //les config
            //les conventions
            modelBuilder.Conventions.Add(new DateTime2Convention1());
            modelBuilder.Conventions.Add(new CodeConvention());
        }
    }
}
