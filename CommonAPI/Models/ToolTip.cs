using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonAPI.Models
{
    public class ToolTip
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}