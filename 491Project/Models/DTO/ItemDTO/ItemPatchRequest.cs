using _491Project.Models.DB;
using DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _491Project.Models.DTO.ItemDTO
{
    public class ItemPatchRequest : IPatchRequest<Item>
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public DateTime? Expires { get; set; }
        public DateTime? Updated { get; set; }
        public int? LocationID { get; set; }

        public Item ToDB(Item existing)
        {
            existing.ID = ID ?? existing.ID;
            existing.Name = Name ?? existing.Name;
            existing.Description = Description ?? existing.Description;
            existing.Quantity = Quantity ?? existing.Quantity;
            existing.Expires = Expires ?? existing.Expires;
            existing.Updated = DateTime.Now;
            existing.LocationID = LocationID ?? existing.LocationID;

            return existing;

        }
    }
}