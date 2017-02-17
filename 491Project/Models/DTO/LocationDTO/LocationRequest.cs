using _491Project.Models.DB;
using DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _491Project.Models.DTO.LocationDTO
{
    public class LocationRequest : IRequest<Location>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Location ToDB()
        {
            return new Location
            {
                Name = this.Name,
                Description = this.Description
            };
        }
    }
}