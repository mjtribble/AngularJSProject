using _491Project.Models.DB;
using DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _491Project.Models.DTO.LocationDTO
{
    public class LocationResponse : IResponse<Location>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public void ParseExisting(Location existing)
        {
            ID = existing.ID;
            Name = existing.Name;
            Description = existing.Description;
        }
    }
}