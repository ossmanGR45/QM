using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using QM.DataAccess.Managers;
using QM.DataAccess.Repo.IRepo;
using QM.Models.DataModels;
using QM.Models.DTO;
using QM.Utility;

namespace QM.Controller
{

    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public AuthController(UserManager<User> userManager, IConfiguration configuration,IUnitOfWork uow): base(uow)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        
        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] Login model)
        //{
        //    var user = await _userManager.FindByNameAsync(model.Email);
            

        //    if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        //    {
        //        // 1. Fetch the roles for this user
        //        var roles = await _userManager.GetRolesAsync(user);
                
                
        //        var token = TokenGenerator.GenerateToken(user, roles,_configuration);

        //        return Ok(new
        //        {
        //            Token = token,
        //            Username = user.UserName,
        //            Roles = roles, 
        //            ExpiresAt = 3600 
        //        });
        //    }

        //    return Unauthorized(new { Message = "Invalid username or password" });
        //}

        [HttpPost("refresh")]
        public IActionResult RefreshToken()
        {
            
            return Ok(new { Message = "Token refreshed" });
        }


    }
}
