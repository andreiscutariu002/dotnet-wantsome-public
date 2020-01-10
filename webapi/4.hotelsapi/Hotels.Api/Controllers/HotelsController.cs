using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hotels.Api.Data.Entities;
using Hotels.Api.Services;
using Microsoft.Extensions.Configuration;

namespace Hotels.Api.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/hotels")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly ApiDbContext context;
        private readonly INotificationService notificationService;

        public HotelsController(ApiDbContext context, INotificationService notificationService)
        {
            this.context = context;
            this.notificationService = notificationService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> Get(int id, CancellationToken token)
        {
            if (id < 0)
            {
                throw new AccessViolationException("Negative id exception");
            }

            var entity = await this.context.Hotels.FindAsync(id, token);

            if (entity == null)
            {
                return this.NotFound();
            }

            return entity;
        }

        [HttpPost]
        public async Task<ActionResult<Hotel>> Post([FromBody] Hotel model, CancellationToken token)
        {
            this.context.Hotels.Add(model);
                
            await this.context.SaveChangesAsync(token);

            this.notificationService.Notify($"hotel with id {model.Id} created!");

            return this.CreatedAtAction("Get", new { id = model.Id }, model);
        }
    }
}
