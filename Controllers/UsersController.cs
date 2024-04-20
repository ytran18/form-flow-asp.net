using demo.Data;
using demo.Dto;
using demo.Models;
using demo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace demo.Controllers;

public class UsersController : Controller
{
    
    private readonly ApplicationDbContext _db;
    
    public UsersController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    // POST
    [HttpPost("signup")]
    public async Task<IActionResult> addUsers([FromBody]Users user)
    {
        try
        {
            _db.Add(user);
            await _db.SaveChangesAsync();

            var res = new ResponseData<String>
            {
                Status = 200,
                Success = true,
                Data = "Add user successfully",
            };
            return Ok(res);
        }
        catch (Exception e)
        {
            var res = new ResponseData<String>
            {
                Status = 500,
                Success = false,
                Data = e.Message,
            };

            return Json(res);
        }
    }
    
    // check email before login
    [HttpGet("email/check")]
    public async Task<IActionResult> checkEmail([FromBody] String email)
    {
        try
        {
            bool emailExists = await _db.users.AnyAsync(u => u.email == email);
            var res = new ResponseData<String>
            {
                Status = emailExists ? 200 : 500,
                Success = emailExists ? true : false,
                Data = emailExists ? "Successful!" : "Email does not regiester!",
            };
            return Ok(res);
        }
        catch (Exception e)
        {
            var res = new ResponseData<String>
            {
                Status = 500,
                Success = false,
                Data = e.Message,
            };

            return Json(res);
        }
    }
    
    // Login
    [HttpGet("/login")]
    public async Task<IActionResult> login([FromBody] UsersDto user)
    {
        try
        {
            var existingUser = await _db.users.FirstOrDefaultAsync(u => u.email == user.email);
            var condition = existingUser != null && existingUser.password == user.password; 
            var res = new ResponseData<String>
            {
                Status = condition ? 200 : 400,
                Success = condition ? true : false,
                Data = condition ? "Login successfully!" : "Invalid password!",
            };
            return Ok(res);
        }
        catch (Exception e)
        {
            var res = new ResponseData<String>
            {
                Status = 500,
                Success = false,
                Data = e.Message,
            };

            return Json(res);
        }
    }
    
}