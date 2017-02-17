using _491Project.Models.DB;
using DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _491Project.Models.DTO.LocationDTO
{
    public class LocationPatchRequest : IPatchRequest<Location>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Location ToDB(Location existing)
        {
            existing.Name = Name ?? existing.Name;
            existing.Description = Description ?? existing.Description;

            return existing;
        }
    }
}