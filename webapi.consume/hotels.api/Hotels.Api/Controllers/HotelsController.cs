namespace Hotels.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Extensions.Map;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Models.Hotels;
    using Services;

    [Route("api/hotels")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly ApiDbContext context;
        private readonly ISimpleLogger logger;
        private readonly IMemoryCache memoryCache;

        public HotelsController(ApiDbContext context, ISimpleLogger logger, IMemoryCache memoryCache)
        {
            this.context = context;
            this.logger = logger;
            this.memoryCache = memoryCache;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HotelResource>> Get(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("Negative id exception");
            }

            var entity = await this.context.Hotels.FindAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            this.logger.LogInfo("HotelsController-Get(id) hit");

            return entity.MapAsResource();
        }

        [HttpPost]
        public async Task<ActionResult<HotelResource>> Post(CreateHotelResource model)
        {
            var entity = model.MapAsNewEntity();
            this.context.Hotels.Add(entity);

            await this.context.SaveChangesAsync();

            var cts = new CancellationTokenSource();
            this.memoryCache.Set($"_CTS{entity.Id}", cts);

            return this.CreatedAtAction("Get", new {id = entity.Id}, entity.MapAsResource());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, JsonPatchDocument<UpdateHotelResource> pathPatchDocument)
        {
            //https://docs.microsoft.com/en-us/aspnet/core/web-api/jsonpatch?view=aspnetcore-3.1

            var entity = await this.context.Hotels.FindAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            var existing = new UpdateHotelResource
            {
                City = entity.City
                // imagine that we have a lot of props/info here
            };

            pathPatchDocument.ApplyTo(existing, this.ModelState);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            entity.UpdateWith(existing);
            this.context.Hotels.Update(entity);
            await this.context.SaveChangesAsync();

            return this.NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateHotelResource model)
        {
            var entity = await this.context.Hotels.FindAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            entity.UpdateWith(model);
            this.context.Hotels.Update(entity);
            await this.context.SaveChangesAsync();

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<HotelResource>> Delete(int id)
        {
            var hotel = await this.context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return this.NotFound();
            }

            var rooms = await this.context.Rooms.Include(x=>x.Hotel).Where(x => x.Hotel.Id == id).ToListAsync();
            foreach (var room in rooms)
            {
                this.context.Rooms.Remove(room);
            }

            this.context.Hotels.Remove(hotel);
            await this.context.SaveChangesAsync();

            var cts = this.memoryCache.Get<CancellationTokenSource>($"_CTS{id}");
            cts?.Cancel();

            return hotel.MapAsResource();
        }

        [HttpPost("long-running")]
        public async Task<ActionResult> LongRunningCall(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                await Task.Delay(TimeSpan.FromSeconds(1));

                this.logger.LogInfo("Operation running ...");
            }

            this.logger.LogInfo("Cancellation Requested");

            return this.Ok();
        }
    }
}
