using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TodoDataAccess;
using APM.WebAPI.Models;
using System.Web.Http.Cors;
using System.Web.Http.OData;

namespace APM.WebAPI.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class TodoController : ApiController
    {
        [HttpGet]
        [EnableQuery]
        public IHttpActionResult Get()
        {
            using (TodoEntities1 entities=new TodoEntities1())
            {
                List<TodoModel> retList = new List<TodoModel>();
                List<TodoList> a1 = entities.TodoLists.ToList();
                foreach(var a in a1)
                {
                    TodoModel b = new TodoModel();
                    b.Description = a.Description;
                    b.Id = a.Id;
                    b.isDeleted = a.isDeleted;
                    b.Name = a.Name;
                    b.StatusId = a.StatusId;
                    if(b.isDeleted == false)
                        retList.Add(b);
                }
                return Ok(retList.AsQueryable());
            }
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            using (TodoEntities1 entities = new TodoEntities1())
            {
                TodoDataAccess.TodoList a = entities.TodoLists.Where(item => item.Id == id).FirstOrDefault();
                if (a == null)
                {
                    return NotFound();
                }
                TodoModel b = new TodoModel();
                b.Description = a.Description;
                b.Id = a.Id;
                b.isDeleted = a.isDeleted;
                b.Name = a.Name;
                b.StatusId = a.StatusId;
                if (b.isDeleted == true)
                    return NotFound();
                return Ok(b);
            }
        }

        [HttpPost]
        public IHttpActionResult Post(TodoModel value)
        {
            if (value == null)
            {
                return BadRequest("Product cannot be null");
            }
            using (TodoEntities1 entities = new TodoEntities1())
            {
                TodoList a = new TodoList();
                a.Description = value.Description;
                a.isDeleted = false;
                a.Name = value.Name;
                a.StatusId = 1;
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                entities.TodoLists.Add(a);
                
                int i=entities.SaveChanges();
                if (i == 0)
                {
                    return Conflict();
                }
                return Created<TodoList>(Request.RequestUri + a.Id.ToString(), a);
            }
        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]TodoModel value)
        {
            if (value == null)
                return BadRequest("Product cannot be null");

            using (TodoEntities1 entities = new TodoEntities1())
            {
                TodoList a = entities.TodoLists.Where(item => item.Id == id).FirstOrDefault();
                if (a == null)
                    return BadRequest("Product not Found");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                a.Description = value.Description;
                a.isDeleted = value.isDeleted;
                a.Name = value.Name;
                a.StatusId = value.StatusId;
                int i = entities.SaveChanges();
                return Ok();
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest("Product not found");
            using (TodoEntities1 entities = new TodoEntities1())
            {
                var entity = entities.TodoLists.Where(a => a.Id == id).FirstOrDefault();
                if (entity == null)
                    return BadRequest("Product not Found");
                entity.isDeleted = true;
                int i = entities.SaveChanges();
                if (i == 0)
                    return Conflict();
                return Ok();
            }
        }

    }
}
