using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAppBLL.BusinessObjects;
using Microsoft.AspNetCore.Mvc;

namespace CustomerRestAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        //R in CRUD
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value -- " + id;
        }

        // POST api/values
        //C in CRUD
        [HttpPost]
        public void Post([FromBody]CustomerBO customer)
        {
        }

        // PUT api/values/5
        //U in CRUD
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
