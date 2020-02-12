using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodSupplyChainAdminPanel.Models
{
    public class FSCFoodAttributes
    {
        public string AttributeGroupCode { get; set; }
        public string AttributeGroupDesc { get; set; }
        public List<FSCFoodAttributeValues> FoodAttributes { get; set; }
    }
    public class FSCFoodAttributeValues
    {
        public int AttributeId { get; set; }
        public string AttributeGroupCode { get; set; }
        public string AttributeGroupDesc { get; set; }
        public string AttributeValue { get; set; }
    }
}