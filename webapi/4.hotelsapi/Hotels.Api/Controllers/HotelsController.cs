namespace Hotels.Api.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/hotels")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly ApiDbContext context;

        public HotelsController(ApiDbContext context)
        {
            this.context = context;
        }
    }
}
