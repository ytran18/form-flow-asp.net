using demo.Data;
using demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace demo.Controllers
{
    [ApiController]
    [Route("forms")]
    public class FormsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public FormsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Add form
        [HttpPost]
        public async Task<IActionResult> AddForm([FromBody] Forms form)
        {
            try
            {
                _db.Add(form);
                await _db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Add form successfully!" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { status = 500, message = e.Message });
            }
        }

        // Get all forms
        [HttpGet]
        public async Task<IActionResult> GetAllForms()
        {
            try
            {
                var forms = await _db.forms.ToListAsync();
                return Ok(new { status = 200, data = forms });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { status = 500, message = e.Message });
            }
        }

        // Get form by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFormById(string id)
        {
            try
            {
                var form = await _db.forms.FindAsync(id);
                var status = form == null ? 400 : 200;
                var success = form != null;
                return Ok(new { status, success, data = form });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { status = 500, message = e.Message });
            }
        }
        
        // Update form by id
        [HttpPut("{id}")]
        public async Task<IActionResult> updateForm([FromRoute] String id, [FromBody] Forms form)
        {
            try
            {
                var existingForm = await _db.forms.FindAsync(id);
                if (existingForm != null)
                {
                    _db.Entry(existingForm).CurrentValues.SetValues(form);    
                };
                
                await _db.SaveChangesAsync();
                
                var status = existingForm == null ? 400 : 200;
                var success = existingForm != null;
                var data = existingForm != null ? "Update form successfully" : "Form does not exists!";
                return Ok(new { status, success, data });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { status = 500, message = e.Message });
            }
        }
        
        // Delete form by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteForm([FromRoute] String id)
        {
            try
            {
                var existingForm = await _db.forms.FindAsync(id);
                if (existingForm != null)
                {
                    _db.forms.Remove(existingForm);
                    await _db.SaveChangesAsync();
                };
                
                var status = existingForm == null ? 400 : 200;
                var success = existingForm != null;
                var data = existingForm != null ? "Delete form successfully" : "Form does not exists!";
                return Ok(new { status, success, data });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { status = 500, message = e.Message });
            }
        }
        
        // Rename form by id
        [HttpPut("rename/{id}")]
        public async Task<IActionResult> renameForm([FromRoute] String id, [FromBody] String name)
        {
            try
            {
                var existingForm = await _db.forms.FindAsync(id);
                if (existingForm != null)
                {
                    existingForm.formTitle = name;
                    _db.forms.Update(existingForm);
                    await _db.SaveChangesAsync();
                };
                
                var status = existingForm == null ? 400 : 200;
                var success = existingForm != null;
                var data = existingForm != null ? "Rename form successfully" : "Form does not exists!";
                return Ok(new { status, success, data });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { status = 500, message = e.Message });
            }
        }
        
        // Clone form by id
        [HttpPost("clone/{id}")]
        public async Task<IActionResult> cloneForm([FromRoute] String id)
        {
            try
            {
                var existingForm = await _db.forms.FindAsync(id);
                if (existingForm != null)
                {
                    existingForm._id = Guid.NewGuid().ToString();
                    existingForm.modified_at = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
                    _db.forms.Add(existingForm);
                    await _db.SaveChangesAsync();
                };
                
                var status = existingForm == null ? 400 : 200;
                var success = existingForm != null;
                var data = existingForm != null ? "Clone form successfully" : "Form does not exists!";
                return Ok(new { status, success, data });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { status = 500, message = e.Message });
            }
        }
        
        
    }
}