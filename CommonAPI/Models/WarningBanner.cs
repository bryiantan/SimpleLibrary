using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonAPI.Models
{
    public class WarningBanner
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}