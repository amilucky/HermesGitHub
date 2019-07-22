using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using hermesmvc.Models;

namespace hermesmvc.ViewModels
{
    public class PromotionsProducts
    {
        public Promotion Promotion { get; set; }
        public Product Product { get; set; }
    }
}