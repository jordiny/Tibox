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

    [RoutePrefix("Product")]
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork unit) : base(unit)
        {
        }

        [Route("list")]
        [HttpGet]
        public IHttpActionResult Get()
        {

            return Ok(_unit.Products.GetAll());
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0) BadRequest();
            return Ok(_unit.Products.GetEntityById(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(Product product)
        {
            if (!ModelState.IsValid || product == null) return BadRequest(ModelState);

            var id = _unit.Products.Insert(product);

            return Ok(new { id=id});
        }

        [Route("")]
        [HttpPut]
        public IHttpActionResult Put(Product product)
        {
            if (!ModelState.IsValid || product == null) return BadRequest(ModelState);
            if (_unit.Products.Update(product)) return BadRequest("Error Update");
            return Ok(new { status=true});
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();

            return Ok(_unit.Products.Delete(new Product { Id=id }));
        }
    }
}
