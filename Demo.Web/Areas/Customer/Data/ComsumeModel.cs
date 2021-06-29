using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Entity.Models.Consume;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Web.Areas.Customer.Data
{
    public struct ComsumeModel
    {
        public List<ConsumeEntity> Consumes { get; set; }

        public IEnumerable<SelectListItem> SelectListItems { get; set; }
        public string Title { get; set; }
        public DateTime?  StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
