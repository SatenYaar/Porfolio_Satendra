using Microsoft.EntityFrameworkCore;
using Porfolio_Satendra.Models;

namespace Porfolio_Satendra.Data
{

    public class UserDBContext : DbContext
    {
        // Define a DbSet for the UserInfo entity
        public DbSet<UserInfo> Users { get; set; }

        // Constructor that takes DbContextOptions as a parameter
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options)
        {
        }

        // Optional: Configure additional entities, relationships, and database settings here

        // Define other DbSet properties for additional entities if needed
    }

}
