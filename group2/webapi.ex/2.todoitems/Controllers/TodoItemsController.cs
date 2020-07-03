namespace FirstApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Services;

    [Route("api/todoitems")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ApiDbContext context;
        private readonly MailService mailService;
        private readonly ILoggingService service;

        public TodoItemsController(ApiDbContext context, MailService mailService, ILoggingService service)
        {
            this.context = context;
            this.mailService = mailService;
            this.service = service;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await this.context.TodoItems.ToListAsync();
        }

        [HttpGet("search/{value}")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems([FromRoute] string value, CancellationToken token)
        {
            return this.Ok(await this.context.TodoItems.Where(t=>t.Name.Contains(value)).ToListAsync(cancellationToken: token));
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem([FromRoute] long id, [FromQuery] bool value, CancellationToken token)
        {
            var todoItem = await this.context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return this.NotFound();
            }

            return this.Ok(todoItem);
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem([FromRoute] long id, [FromBody] TodoItem todoItem)
        {
            if (id < 0)
            {
                throw new ArgumentException("negative id");
            }

            this.context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.TodoItemExists(id))
                {
                    return this.NotFound();
                }

                throw;
            }

            return this.NoContent();
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem([FromBody] TodoItem todoItem)
        {
            this.context.TodoItems.Add(todoItem);
            await this.context.SaveChangesAsync();

            this.service.Log($"TODO ITEM created with id {todoItem.Id}");

            this.mailService.SendMail("TODO ITEM created");

            return this.CreatedAtAction("GetTodoItem", new {id = todoItem.Id}, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            var todoItem = await this.context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return this.NotFound();
            }

            this.context.TodoItems.Remove(todoItem);
            await this.context.SaveChangesAsync();

            return this.Ok(todoItem);
        }

        private bool TodoItemExists(long id)
        {
            return this.context.TodoItems.Any(e => e.Id == id);
        }
    }

    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment(
            [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message);
        }


        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}
