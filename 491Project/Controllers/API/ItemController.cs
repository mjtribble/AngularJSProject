
using _491Project.Models.DB;
using _491Project.Models.DTO.ItemDTO;
using DAL;
using DAL.Models;
using DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;


namespace _491Project.Controllers.API
{
    public class ItemController : ApiController 
    {
        private EntityRepository<Item> itemDB;

        public ItemController()
        {
            DatabaseContext context = new DatabaseContext();
            itemDB = new EntityRepository<Item>(context);
        }

        [HttpGet]
        [ResponseType(typeof(List<IResponse>))]
        public async Task<IHttpActionResult> GetItems(int page = 1, int take = 100)
        {
            PaginatedList<Item> existing = await itemDB.Paginate(
                page, //offest by (page-1)
                take, //number of results
                o => o.ID // order by ID
                );
            //Convert to DTO before returning
            return Ok(existing.ToPaginatedDto<ItemResponse, Item>());
        }
  
        [HttpGet]
        [ResponseType(typeof(List<IResponse>))]
        [Route("api/location/{id}/item")]
        public async Task<IHttpActionResult> GetItemsByLocation(int id, int page = 1, int take = 100)
        {
            PaginatedList<Item> existing = await itemDB.Paginate(
                page, //offest by (page-1)
                take, //number of results
                o => o.LocationID == id // order by ID
                );
            //Convert to DTO before returning
            return Ok(existing.ToPaginatedDto<ItemResponse, Item>());
        }



        [HttpGet]
        [ResponseType(typeof(IResponse))]
        public async Task<IHttpActionResult> GetItem(int id)
        {
            Item existing = await itemDB.GetSingleOrDefaultWhere(o => o.ID == id);

            if (existing == null)
            {
                return NotFound();

            }
            else
            {

                ItemResponse resp = new ItemResponse();
                resp.ParseExisting(existing);
                return Ok(resp);
            }
        }

        [HttpPost]
        [ResponseType(typeof(Item))]
        public async Task<IHttpActionResult> CreateItem(ItemRequest request)
        {
            //convert from DTO
            Item dbObject = request.ToDB();
            //add and save

            itemDB.Add(dbObject);
            await itemDB.Save();

            return CreatedAtRoute("DefaultApi",
                    new
                    {
                        ID = dbObject.ID

                    },
                    dbObject);
        }

        [HttpPatch]
        public async Task<IHttpActionResult> UpdateItem(int id, ItemPatchRequest request)
        {
            Item dbObject = await itemDB.GetSingleOrDefaultWhere(o => o.ID == id);

            if (dbObject == null)
            {
                return NotFound();
            }
            else
            {   //update dbObject from the request data
                dbObject = request.ToDB(dbObject);

                //mark as modified and save
                itemDB.Edit(dbObject);
                await itemDB.Save();
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
        }
        //TO DO
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteItem(int id)
        {
            Item dbObject = await itemDB.GetSingleOrDefaultWhere(o => o.ID == id);

            if (dbObject == null)
            {
                return NotFound();
            }
            itemDB.Delete(dbObject);
            await itemDB.Save();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}