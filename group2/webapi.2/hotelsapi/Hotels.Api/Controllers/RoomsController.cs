namespace Hotels.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Extensions.Map;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Distributed;
    using Models.Rooms;
    using Newtonsoft.Json;
    using Services;

    [Route("api/hotels/{hotelId}/rooms")]
    [ApiController]
    //[Authorize]
    public class RoomsController : ControllerBase
    {
        private readonly ApiDbContext context;
        private readonly ISimpleLogger logger;
        private readonly IDistributedCache distributedCache;

        public RoomsController(ApiDbContext context, ISimpleLogger logger, IDistributedCache distributedCache)
        {
            this.context = context;
            this.logger = logger;
            this.distributedCache = distributedCache;
        }

        [HttpGet("")]
        public async Task<IEnumerable<RoomResource>> Get(int hotelId, CancellationToken token)
        {
            var key = $"_rooms_{hotelId}";

            var existingCache = await this.distributedCache.GetStringAsync(key, token: token);

            if (existingCache != null)
            {
                var list = JsonConvert.DeserializeObject<List<RoomResource>>(existingCache);

                return list;
            }
            else
            {
                List<Room> list = await this.context.Rooms
                    .Include(h => h.Hotel)
                    .Where(h => h.Hotel.Id == hotelId)
                    .ToListAsync(token);

                IList<RoomResource> roomResources = list.Select(r => r.MapAsResource()).ToList();

                this.distributedCache.SetString(key, JsonConvert.SerializeObject(roomResources), new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(10) });

                this.logger.LogInfo("RoomsController-Get(hotelId) hit");

                return roomResources;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomResource>> Get(int hotelId, int id)
        {
            if (id < 0) throw new ArgumentException("Negative id exception");

            var entity = await this.context.Rooms.FindAsync(id);
            if (entity == null) return this.NotFound();

            this.logger.LogInfo("RoomsController-Get(hotelId, roomId) hit");

            return entity.MapAsResource();
        }

        [HttpPost]
        public async Task<ActionResult<RoomResource>> Post(int hotelId, CreateRoomResource model)
        {
            var hotel = await this.context.Hotels.FindAsync(hotelId);

            if (hotel == null) return this.NotFound();

            var entity = model.MapAsNewEntity(hotel);
            this.context.Rooms.Add(entity);
            await this.context.SaveChangesAsync();

            return this.CreatedAtAction("Get", new {hotelId, id = entity.Id}, entity.MapAsResource());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int hotelId, int id, UpdateRoomResource model)
        {
            var room = await this.context.Rooms.FindAsync(id);

            if (room == null) return this.NotFound();

            room.UpdateWith(model);
            this.context.Rooms.Update(room);
            await this.context.SaveChangesAsync();

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RoomResource>> Delete(int hotelId, int id)
        {
            var room = await this.context.Rooms.FindAsync(id);

            if (room == null) return this.NotFound();

            this.context.Rooms.Remove(room);
            await this.context.SaveChangesAsync();

            return room.MapAsResource();
        }
    }
}
