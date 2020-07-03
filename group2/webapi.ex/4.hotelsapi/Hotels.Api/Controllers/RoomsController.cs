namespace Hotels.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Resources;

    [Route("api/hotels/{id}/rooms")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ApiDbContext context;

        public RoomsController(ApiDbContext context)
        {
            this.context = context;
        }

        [HttpGet("{roomId}")]
        public async Task<ActionResult<RoomResource>> GetRoom(int id, long roomId)
        {
            var hotel = await this.context.Hotels.FindAsync(id);
            var room = hotel.Rooms.FirstOrDefault(x => x.Id == roomId);

            if (room == null)
            {
                return this.NotFound();
            }

            return this.Ok(new RoomResource
            {
                Name = room.Name,
                Number = room.Number,
                Id = room.Id
            });
        }

        [HttpPost("")]
        public async Task<ActionResult<Room>> CreateRoom(int id, RoomResource room)
        {
            var hotel = await this.context.Hotels.FindAsync(id);

            if (hotel.Rooms == null)
            {
                hotel.Rooms = new List<Room>();
            }

            hotel.Rooms.Add(new Room
            {
                Id = room.Id,
                Name = room.Name,
                Number = room.Number,
                HotelId = id
            });

            this.context.Entry(hotel).State = EntityState.Modified;
            
            await this.context.SaveChangesAsync();

            return CreatedAtAction("GetRoom", new { id = hotel.Id, roomId = room.Id }, room);
        }
    }
}
