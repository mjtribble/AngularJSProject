using DAL.Models.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _491Project.Models.DB
{
    public class Item : IEntity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public DateTime? Expires { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public int? LocationID { get; set; }
        public virtual Location Location { get; set; }
    }
}