using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAppBLL;
using CustomerAppBLL.BusinessObjects;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerRestAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private BLLFacade _facade = new BLLFacade();

        // GET: api/values
        [HttpGet]
        public IEnumerable<OrderBO> Get()
        {
            return _facade.OrderService.GetAllOrders();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public OrderBO Get(int id)
        {
            return _facade.OrderService.GetOrder(id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]OrderBO order)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(_facade.OrderService.CreateOrder(order));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]OrderBO order)
        {
            if (id != order.Id) return BadRequest("Path ID does not match the Orders id");

            try
            {
                return Ok(_facade.OrderService.UpdateOrder(order));
            }
            catch (Exception e)
            {
                return StatusCode(404, e.Message);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _facade.OrderService.DeleteOrder(id);
        }
    }
}
