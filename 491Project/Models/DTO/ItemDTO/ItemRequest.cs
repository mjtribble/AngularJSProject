using _491Project.Models.DB;
using DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _491Project.Models.DTO.ItemDTO
{
    public class ItemRequest : IRequest<Item>
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public DateTime? Expires { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public int? LocationID { get; set; }
        public virtual Location Location { get; set; }

        public Item ToDB()
        {
            return new Item
            {
                ID = this.ID,
                Name = this.Name,
                Description = this.Description,
                Quantity = this.Quantity,
                Expires = this.Expires,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                LocationID = this.LocationID,
                Location = this.Location
            };
            
        }
    }
}