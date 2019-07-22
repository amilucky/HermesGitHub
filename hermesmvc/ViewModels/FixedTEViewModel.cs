using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hermesmvc.ViewModels
{
    public class FixedTEViewModel
    {
        public int customer_id { get; set; }
        public string customer_name { get; set; }
        public int year { get; set; }
        public int segment_id { get; set; }
        public string segment_name { get; set; }
        [System.ComponentModel.DataAnnotations.DisplayFormat(DataFormatString = "{0:P2}")]
        public double On_invoice { get; set; }
        [System.ComponentModel.DataAnnotations.DisplayFormat(DataFormatString = "{0:P2}")]
        public double Off_invoice { get; set; }

        public double ToNIV { get { return (1-On_invoice); } }
        public double ToNSV { get { return (1 - On_invoice)*(1-Off_invoice); } }
    }
}