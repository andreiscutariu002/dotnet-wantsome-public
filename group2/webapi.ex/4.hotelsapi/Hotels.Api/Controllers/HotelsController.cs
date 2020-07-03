namespace Hotels.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Resources;

    [Route("api/hotels")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly ApiDbContext context;

        public HotelsController(ApiDbContext context)
        {
            this.context = context;
        }

        // GET: api/Hotels
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Hotel>>> GetTodoItems()
        //{
        //    return await this.context.Hotels.ToListAsync();
        //}

        // GET: api/Hotel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelResource>> GetHotel(long id)
        {
            var hotel = await this.context.Hotels.FindAsync(id);
            
            if (hotel == null)
            {
                return this.NotFound();
            }

            return hotel.MapToResource();
        }

        // PUT: api/Hotel/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTodoItem(long id, Hotel hotel)
        //{
        //    if (id < 0)
        //    {
        //        throw new ArgumentException("negative id");
        //    }

        //    this.context.Entry(hotel).State = EntityState.Modified;
            
        //    try
        //    {
        //        await this.context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!this.HotelExists(id)) return this.NotFound();
        //        throw;
        //    }

        //    return this.NoContent();
        //}

        // POST: api/Hotel
        [HttpPost]
        public async Task<ActionResult<HotelResource>> PostHotel(HotelResource hotel)
        {
            this.context.Hotels.Add(hotel.MapToEntity());

            await this.context.SaveChangesAsync();
            
            return this.CreatedAtAction("GetHotel", new {id = hotel.Id}, hotel);
        }

        //// DELETE: api/Hotel/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Hotel>> DeleteTodoItem(long id)
        //{
        //    var hotel = await this.context.Hotels.FindAsync(id);
        //    if (hotel == null)
        //    {
        //        return this.NotFound();
        //    }

        //    this.context.Hotels.Remove(hotel);
        //    await this.context.SaveChangesAsync();
        //    return hotel;
        //}

        //private bool HotelExists(long id)
        //{
        //    return this.context.Hotels.Any(e => e.Id == id);
        //}
    }
}
