namespace FirstApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //route: api/users/1
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return new User
            {
                UserId = id,
                Email = "email",
                UserName = "user name"
            };
        }
    }
}
