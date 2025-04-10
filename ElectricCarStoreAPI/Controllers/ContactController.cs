using ElectricCarStore_BLL.IService;
using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.PostModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectricCarStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetAllContacts(int page = 1, int perPage = 10)
        {
            var contacts = await _contactService.GetAllContactsAsync(page, perPage);
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            try
            {
                var contact = await _contactService.GetContactByIdAsync(id);
                return Ok(contact);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("car/{carId}")]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContactsByCar(int carId)
        {
            var contacts = await _contactService.GetContactsByCarIdAsync(carId);
            return Ok(contacts);
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> CreateContact(ContactPostModel contactModel)
        {
            try
            {
                var createdContact = await _contactService.CreateContactAsync(contactModel);
                return CreatedAtAction(nameof(GetContact), new { id = createdContact.Id }, createdContact);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, ContactPostModel contactModel)
        {
            try
            {
                var updatedContact = await _contactService.UpdateContactAsync(id, contactModel);
                return Ok(updatedContact);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            try
            {
                var result = await _contactService.DeleteContactAsync(id);
                return Ok(new { success = result });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }

}
