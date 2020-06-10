namespace WebApi.Controllers
{
    using System.Threading.Tasks;
    using _17MigrateToAsync;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("news")]
    public class NewsController : ControllerBase
    {
        private readonly NewsSearchServiceV1 searchServiceV1;

        private readonly NewsSearchServiceV2 searchServiceV2;

        private readonly bool useAsync;

        public NewsController()
        {
            this.searchServiceV1 = new NewsSearchServiceV1();
            this.searchServiceV2 = new NewsSearchServiceV2();

            this.useAsync = true;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Get([FromQuery] string q)
        {
            if (string.IsNullOrEmpty(q)) return this.NoContent();

            if (this.useAsync)
            {
                var response = await this.searchServiceV2.GetHtmlAsync(q);
                return this.Ok(response);
            }
            else
            {
                var response = this.searchServiceV1.GetHtml(q);
                return this.Ok(response);
            }
        }
    }
}
