using Microsoft.EntityFrameworkCore;
using WebApplication4.model;

namespace WebApplication4.ContactsAPI
{
    public class ContactsAPIDbContext : DbContext
    {
        public ContactsAPIDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Contact> contacts
        {
            get;
            set;
        }
    }
}
