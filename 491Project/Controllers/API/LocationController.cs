
using _491Project.Models.DB;
using _491Project.Models.DTO.LocationDTO;
using DAL;
using DAL.Models;
using DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;

namespace _491Project.Controllers.API
{
    public class LocationController : ApiController 
    {
        public DatabaseContext database { get; set; } = new DatabaseContext();
        private EntityRepository<Location> locationDB;
        private EntityRepository<Item> itemDB;

        public LocationController()
        {
            DatabaseContext context = new DatabaseContext();
            locationDB = new EntityRepository<Location>(context);
            DatabaseContext context1 = new DatabaseContext();
            itemDB = new EntityRepository<Item>(context1);
        }

        [HttpGet]
        [ResponseType(typeof(List<IResponse>))]
        public async Task<IHttpActionResult> GetLocations(int page = 1, int take = 10)
        {
            PaginatedList<Location> existing = await locationDB.Paginate(
                page, //offest by (page-1)
                take, //number of results
                o => o.ID // order by ID
                );
            //Convert to DTO before returning
                return Ok(existing.ToPaginatedDto<LocationResponse, Location>());
        }

        [HttpGet]
        [ResponseType(typeof(LocationResponse))]
        public async Task<IHttpActionResult> GetLocation(int id)
        {
            Location existing = await locationDB.GetSingleOrDefaultWhere(o => o.ID == id);

            if (existing == null)
            {
                return NotFound();

            }else
            {

                LocationResponse resp = new LocationResponse();
                resp.ParseExisting(existing);
                return Ok(resp);

            }
        }

        [HttpPost]
        [ResponseType(typeof(LocationRequest))]
        public async Task<IHttpActionResult> CreateLocation(LocationRequest request)
        {
            //convert from DTO
            Location dbObject = request.ToDB();
            //add and save

            locationDB.Add(dbObject);
            await locationDB.Save();

            return CreatedAtRoute("DefaultApi",
                    new
                    {
                        ID = dbObject.ID
                    },
                    
                    request);
        }

        [HttpPatch]
        public async Task<IHttpActionResult> UpdateLocation(int id, LocationPatchRequest request)
        {
            Location dbObject = await locationDB.GetSingleOrDefaultWhere(o =>o.ID == id);

            if (dbObject==null)
            {
                return NotFound();
            }else
            {   //update dbObject from the request data
                dbObject = request.ToDB(dbObject);

                //mark as modified and save
                locationDB.Edit(dbObject);
                await locationDB.Save();
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
        }
        //TO DO
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteLocation(int id)
        {

            Location dbObject = await locationDB.GetSingleOrDefaultWhere(o => o.ID == id);

               
            if (dbObject == null)
            {
                return NotFound();
            }
            else
            {
                Task<List<Item>> itemlist = itemDB.GetAllWhere(o => o.LocationID == id);
                foreach (var Item in itemlist.Result)
                {
                    Item.LocationID = null; 
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}
