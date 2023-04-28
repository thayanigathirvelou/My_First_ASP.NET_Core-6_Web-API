using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.ContactsAPI;
using WebApplication4.model;

namespace WebApplication4.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private ContactsAPIDbContext dbContext;
        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.contacts.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(Contact addContactRequest)
        {
            var contact = new Contact()
            {
                id = Guid.NewGuid(),
                firstName = addContactRequest.firstName,
                lastName = addContactRequest.lastName,
                email = addContactRequest.email,
                phoneno = addContactRequest.phoneno,
            };
            await dbContext.contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await dbContext.contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, Contact updateContactRequest)
        {
            var contact = await dbContext.contacts.FindAsync(id);
            if (contact != null)
            {
                contact.firstName = updateContactRequest.firstName;
                contact.lastName = updateContactRequest.lastName;
                contact.email = updateContactRequest.email;
                contact.phoneno = updateContactRequest.phoneno;
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var contact = await dbContext.contacts.FindAsync(id);
            if (contact != null)
            {
                dbContext.Remove(contact);
                dbContext.SaveChanges();
                return Ok(contact);
            }
            return NotFound();
        }




    }
}
