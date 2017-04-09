using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tibox.Models;
using Tibox.UnitOfWork;

namespace Tibox.WebAPi.Controllers
{

     [RoutePrefix("Customer")]
    [Authorize]
    public class CustomerController : BaseController
    {
        public CustomerController(IUnitOfWork unit) : base(unit)
        {
        }

        
        [HttpGet]
        [Route("list")]
        public IHttpActionResult Get()
        { 

            return Ok(_unit.Customers.GetAll());
        }
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(_unit.Customers.GetEntityById(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(Customer customer)
        {
            if (!ModelState.IsValid || customer==null ) return BadRequest(ModelState);
            var id = _unit.Customers.Insert(customer);
            return Ok(new { id = id });

        }

        [Route("")]
        [HttpPut]
        public IHttpActionResult Put(Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if( _unit.Customers.Update(customer)) return BadRequest("Error Update");
            return Ok(new { status = true });
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0) return BadRequest();
            return Ok(_unit.Customers.Delete(new Customer { Id=id}));
        }
        
    }
}
