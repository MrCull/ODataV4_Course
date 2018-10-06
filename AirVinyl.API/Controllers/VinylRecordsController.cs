using AirVinyl.DataAccessLayer;
using AirVinyl.Model;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AirVinyl.API.Controllers
{
    public class VinylRecordsController : ODataController
    {
        private AirVinylDbContext _ctx = new AirVinylDbContext();

        [HttpGet]
        [ODataRoute("VinylRecords")]
        public IHttpActionResult GetAllVinylRecords()
        {
            return Ok(_ctx.VinylRecords);
        }


        [HttpGet]
        [ODataRoute("VinylRecords({key})")]
        public IHttpActionResult GetOneVinylRecord([FromODataUri] int key)
        {
            var vinylRecord = _ctx.VinylRecords.FirstOrDefault(p => p.VinylRecordId == key);

            if (vinylRecord == null)
            {
                return NotFound();
            }
            return Ok(vinylRecord);
        }

        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
            base.Dispose(disposing);
        }
    }
}