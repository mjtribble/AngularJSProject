using _491Project.Models.DB;
using DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _491Project.Models.DTO.ItemDTO
{
    public class ItemResponse : IResponse<Item>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public DateTime? Expires { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public int? LocationID { get; set; }

        public void ParseExisting(Item existing)
        {
            ID = existing.ID;
            Name = existing.Name;
            Description = existing.Description;
            Quantity = existing.Quantity;
            Expires = existing.Expires;
            Created = existing.Created;
            Updated = existing.Updated;
            LocationID = existing.LocationID;

        }
    }
}