using InvtryMgtSystemAPI.Authentication;
using InvtryMgtSystemAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Data
{
    public class DataInvntryContext: IdentityDbContext<ApplicationUser>
    {
        
        public DataInvntryContext(DbContextOptions<DataInvntryContext>options):base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PaymentMode> PaymentModes { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);   
         
        }
    }
}
