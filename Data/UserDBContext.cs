using Microsoft.EntityFrameworkCore;
using Porfolio_Satendra.Models;

namespace Porfolio_Satendra.Data
{
    public class UserDBContext : DbContext
    {
     

        public UserDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<UserInfo> Users { get; set; }
    }
}
