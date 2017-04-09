using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tibox.Models;
using Tibox.UnitOfWork;
using Tibox.WebAPi.Models;

namespace Tibox.WebAPi.Controllers
{

    [RoutePrefix("Order")]
    public class OrderController : BaseController
    {
        public OrderController(IUnitOfWork unit) : base(unit)
        {

       

        }

        [Route("list")]
        [HttpGet]
        public IHttpActionResult Get() {

            return Ok(_unit.Orders.GetAll());
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id) {
            if (id <= 0) return BadRequest();

            return Ok(_unit.Orders.GetEntityById(id));
        }
        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(OrderViewModel model ) {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var id = _unit.Orders.SaveOrderAndOrderItems(model.Order, model.OrderItems);

            return Ok(new { id = id });
        }

    }
}
