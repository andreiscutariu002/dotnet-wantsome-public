namespace Hotels.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Extensions.Map;
    using Microsoft.AspNetCore.Mvc;
    using Models.Hotels;

    [Route("api/hotels")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly ApiDbContext context;

        public HotelsController(ApiDbContext context)
        {
            this.context = context;
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

            return entity.MapAsModel();
        }

        [HttpPost]
        public async Task<ActionResult<Hotel>> Post(CreateHotelResource model)
        {
            var entity = model.MapAsNewEntity();
            this.context.Hotels.Add(entity);

            await this.context.SaveChangesAsync();

            return this.CreatedAtAction("Get", new {id = entity.Id}, entity.MapAsModel());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, UpdateHotelResource model)
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
        public async Task<ActionResult<Hotel>> Delete(int id)
        {
            var hotel = await this.context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return this.NotFound();
            }

            this.context.Hotels.Remove(hotel);
            await this.context.SaveChangesAsync();

            return hotel;
        }
    }
}
