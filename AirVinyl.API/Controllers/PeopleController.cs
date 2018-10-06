using Microsoft.AspNet.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AirVinyl.DataAccessLayer;
using System.Web.Http;
using Microsoft.AspNet.OData.Routing;
using AirVinyl.API.Helpers;

namespace AirVinyl.API.Controllers
{
    public class PeopleController : ODataController
    {
        private AirVinylDbContext _ctx = new AirVinylDbContext();

        public IHttpActionResult Get()
        {
            return Ok(_ctx.People);
        }

        public IHttpActionResult Get([FromODataUri] int key)
        {
            var person = _ctx.People.FirstOrDefault(p => p.PersonId == key);

            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpGet]
        [ODataRoute("People({key})/Email")]
        [ODataRoute("People({key})/FirstName")]
        [ODataRoute("People({key})/LastName")]
        [ODataRoute("People({key})/DataOfBirth")]
        [ODataRoute("People({key})/Gender")]
        protected IHttpActionResult GetPersonPreperty([FromODataUri] int key)
        {

            var person = _ctx.People.FirstOrDefault(p => p.PersonId == key);
            if (person == null)
            {
                return NotFound();
            }

            var propertyToGet = Url.Request.RequestUri.Segments.Last();

            if (!person.HasProperty(propertyToGet))
            {
                return NotFound();
            }

            var perpertyValue = person.GetValue(propertyToGet);

            if (perpertyValue == null)
            {
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }

            return this.CreateOKHttpActionResult(perpertyValue);
        }

        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
            base.Dispose(disposing);
        }
    }
}