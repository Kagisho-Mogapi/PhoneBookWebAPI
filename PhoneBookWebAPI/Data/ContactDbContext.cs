using Microsoft.EntityFrameworkCore;
using PhoneBookWebAPI.Models;

namespace PhoneBookWebAPI.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options):base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
