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
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Primitives;
    using Models.Rooms;
    using Newtonsoft.Json;
    using Services;

    [Route("api/hotels/{hotelId}/dis-cached-rooms")]
    [ApiController]
    public class DistributedCachedRoomsController
    {
        private readonly ApiDbContext context;
        private readonly IDistributedCache cache;
        private readonly ISimpleLogger logger;

        public DistributedCachedRoomsController(ApiDbContext context, IDistributedCache cache, ISimpleLogger logger)
        {
            this.context = context;
            this.cache = cache;
            this.logger = logger;
        }

        [HttpGet("")]
        public async Task<IEnumerable<RoomResource>> Get(int hotelId, CancellationToken token)
        {
            var key = $"_rooms_for_hotel_{hotelId}";

            var rooms = this.cache.GetString(key);

            if (!string.IsNullOrEmpty(rooms))
            {
                this.logger.LogInfo("DistributedCachedRoomsController-Get(hotelId) cache hit");

                var roomsList = this.Deserialize<List<RoomResource>>(rooms);

                return roomsList;
            }
            else
            {
                this.logger.LogInfo("DistributedCachedRoomsController-Get(hotelId) db hit");

                var roomsEntities = await this.context.Rooms
                    .Include(h => h.Hotel)
                    .Where(h => h.Hotel.Id == hotelId)
                    .ToListAsync(token);

                var options = new DistributedCacheEntryOptions();
                options.SetAbsoluteExpiration(TimeSpan.FromSeconds(3));
                
                var resources = roomsEntities.Select(e => e.MapAsResource());
                this.cache.SetString(key, this.Serialize(resources), options);

                return resources;
            }
        }

        private string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private T Deserialize<T>(string serialized)
        {
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }

    [Route("api/hotels/{hotelId}/rooms")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ApiDbContext context;
        private readonly ISimpleLogger logger;
        private readonly IMemoryCache memoryCache;

        public RoomsController(ApiDbContext context, ISimpleLogger logger, IMemoryCache memoryCache)
        {
            this.context = context;
            this.logger = logger;
            this.memoryCache = memoryCache;
        }

        [HttpGet("")]
        public async Task<IEnumerable<RoomResource>> Get(int hotelId, CancellationToken token)
        {
            var key = $"_rooms_for_hotel_{hotelId}";

            var list = await this.memoryCache.GetOrCreateAsync(key, entry =>
            {
                var cacheTokenSource = this.memoryCache.GetOrCreate($"_CTS{hotelId}", cacheEntry => new CancellationTokenSource());

                entry.AddExpirationToken(new CancellationChangeToken(cacheTokenSource.Token));
                entry.RegisterPostEvictionCallback(this.Callback, this);

                this.logger.LogInfo("RoomsController-Get(hotelId) db hit");

                return this.context.Rooms
                    .Include(h => h.Hotel)
                    .Where(h => h.Hotel.Id == hotelId)
                    .ToListAsync(token);
            });

            return list.Select(e => e.MapAsResource());
        }

        private void Callback(object key, object value, EvictionReason reason, object state)
        {
            this.logger.LogInfo($"RoomsController-Get(hotelId) cache reset: {reason} on key: {key}");
        }

        [HttpGet("{id}")]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
        public async Task<ActionResult<RoomResource>> Get(int hotelId, int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("Negative id exception");
            }

            var entity = await this.context.Rooms.FindAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            this.logger.LogInfo("RoomsController-Get(hotelId, roomId) hit");

            return entity.MapAsResource();
        }

        [HttpPost]
        public async Task<ActionResult<RoomResource>> Post(int hotelId, CreateRoomResource model)
        {
            var hotel = await this.context.Hotels.FindAsync(hotelId);

            if (hotel == null)
            {
                return this.NotFound();
            }

            var entity = model.MapAsNewEntity(hotel);
            this.context.Rooms.Add(entity);
            await this.context.SaveChangesAsync();

            return this.CreatedAtAction("Get", new {hotelId, id = entity.Id}, entity.MapAsResource());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int hotelId, int id, UpdateRoomResource model)
        {
            var room = await this.context.Rooms.FindAsync(id);

            if (room == null)
            {
                return this.NotFound();
            }

            room.UpdateWith(model);
            this.context.Rooms.Update(room);
            await this.context.SaveChangesAsync();

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RoomResource>> Delete(int hotelId, int id)
        {
            var room = await this.context.Rooms.FindAsync(id);

            if (room == null)
            {
                return this.NotFound();
            }

            this.context.Rooms.Remove(room);
            await this.context.SaveChangesAsync();

            return room.MapAsResource();
        }
    }
}
