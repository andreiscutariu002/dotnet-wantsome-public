namespace WebApp.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;
    using _17MigrateToAsync;

    [Route("news")]
    public class NewsController : ApiController
    {
        private readonly NewsSearchServiceV1 searchServiceV1;

        public NewsController()
        {
            this.searchServiceV1 = new NewsSearchServiceV1();
        }

        [HttpGet]
        [Route("search")]
        public IHttpActionResult Get(string q)
        {
            if (string.IsNullOrEmpty(q))
            {
                return this.BadRequest("Missing query param!");
            }

            var response = this.searchServiceV1.GetHtml(q);
            return this.Ok(response);
        }
    }

    [Route("news")]
    public class NewsV2Controller : ApiController
    {
        private readonly NewsSearchServiceV2 searchServiceV2;

        public NewsV2Controller()
        {
            this.searchServiceV2 = new NewsSearchServiceV2();
        }

        [HttpGet]
        [Route("search")]
        public async Task<IHttpActionResult> Get(string q)
        {
            if (string.IsNullOrEmpty(q))
            {
                return this.BadRequest("Missing query param!");
            }

            string response = await this.searchServiceV2.GetHtmlAsync(q);
            return this.Ok(response);
        }
    }

    public class NewsV3Controller : ApiController
    {
        private readonly NewsSearchServiceV1 searchServiceV1;
        private readonly NewsSearchServiceV2 searchServiceV2;
        private readonly bool useAsync;

        public NewsV3Controller()
        {
            this.searchServiceV1 = new NewsSearchServiceV1();
            this.searchServiceV2 = new NewsSearchServiceV2();
            this.useAsync = true;
        }

        [HttpGet]
        [Route("search")]
        public async Task<IHttpActionResult> Get(string q)
        {
            if (string.IsNullOrEmpty(q)) return this.BadRequest("Missing query param!");

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
