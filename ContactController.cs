using Contactly_WebAPICore.Data;
using Contactly_WebAPICore.Model.DTO;
using Contactly_WebAPICore.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contactly_WebAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactlyDbContext DbContext;
        public ContactController( ContactlyDbContext dbContext)
        {
            DbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllContacts()
        {
            var contact = DbContext.Contacts.ToList();
            return Ok(contact); 
        }
        [HttpPost]
        public IActionResult AddContacts(AddContactsRequestDto addContactsRequestDto)

        {
            var ContactEntity = new Contact
            {
                Id=Guid.NewGuid(),
                Name=addContactsRequestDto.Name,
                Email=addContactsRequestDto.Email,
                PhoneNumber=addContactsRequestDto.PhoneNumber,
                Favorite=addContactsRequestDto.Favorite,
            };
           
            DbContext.Contacts.Add(ContactEntity);
            DbContext.SaveChanges();
            return Ok(ContactEntity);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteContact(Guid id)
        {
            var contact = DbContext.Contacts.Find(id);
            if(contact is not null)
            {
                DbContext.Contacts.Remove(contact);
                DbContext.SaveChanges();
            }
            return Ok();

        }
        
    }
}
