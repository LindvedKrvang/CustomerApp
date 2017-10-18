using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAppBLL;
using CustomerAppBLL.BusinessObjects;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CustomerRestAPI.Controllers
{
    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("api/Customers")]
    public class CustomersController : Controller
    {
        private readonly BLLFacade _facade = new BLLFacade();

        // GET: api/Customers
        
        [HttpGet]
        public IEnumerable<CustomerBO> Get()
        {
            return _facade.CustomerService.GetAllCustomers();
        }

        // GET: api/Customers/5
        [HttpGet("{id}", Name = "Get")]
        public CustomerBO Get(int id)
        {
            return _facade.CustomerService.GetCustomer(id);
        }
        
        // POST: api/Customers
        [HttpPost]
        public IActionResult Post([FromBody]CustomerBO customer)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(_facade.CustomerService.CreateCustomer(customer));
        }
        
        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CustomerBO customer)
        {
            if(id != customer.Id) return StatusCode(405, "Path ID does not match Customer ID");

            try
            {
                return Ok(_facade.CustomerService.UpdateCustomer(customer));
            }
            catch (Exception e)
            {
                return StatusCode(404, e.Message);
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(_facade.CustomerService.DeleteCustomer(id));
            }
            catch (Exception e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}
