using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{


    [Route("api/TodoItems")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {

        List<TodoItem> TodoItems = new List<TodoItem>();
        public TodoItemsController()
        {
            TodoItems.Add(new TodoItem
            {
                Id = 1,
                Description = "Test 1",
                isCompeleted = true,
            });
            TodoItems.Add(new TodoItem
            {
                Id = 2,
                Description = "Test 2",
                isCompeleted = true,
            });TodoItems.Add(new TodoItem
            {
                Id = 3,
                Description = "Test 3",
                isCompeleted = false,
            });
        }
        // GET: api/TodoItems
        [HttpGet]
        public List<TodoItem> Get()
        {
            return TodoItems;
        }

        // GET api/TodoItems/5
        [HttpGet("{id}")]
        public ActionResult<TodoItem> Get(int id)
        {
            var item = TodoItems.SingleOrDefault(a => a.Id == id);

            if (item == null) { 
            
                return NotFound();
            }
            return item;
        }

        // POST api/TodoItems
        [HttpPost]
        public ActionResult<TodoItem> Post([FromBody]TodoItem item)
        {
            TodoItems.Add (item);
            return CreatedAtAction("Get", new { id = item.Id }, TodoItems);

        }

        // PUT api/TodoItems/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TodoItem item)
        {
            foreach(var i in TodoItems)
            {
                if (i.Id == id)
                {
                    i.Description = item.Description;
                    i.isCompeleted = item.isCompeleted;
                }
            }
            return Ok(TodoItems);
        }

        // DELETE api/TodoItems/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var item = TodoItems.FirstOrDefault(a => a.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            TodoItems.Remove(item);
            return NoContent();
        }
    }
}
