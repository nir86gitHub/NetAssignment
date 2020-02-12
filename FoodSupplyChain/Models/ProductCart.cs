using System;
using System.Collections.Generic;

namespace FoodSupplyChain.Models
{
    public class ProductCart
    {
        public string SessionId { get; set; }
        public Int64 ProductId { get; set; }
        public Int64? ProductDetailId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ProductCartToppings> Toppings { get; set; }
    }
    public class ProductCartToppings
    {
        public Int64 ProductId { get; set; }
        public Int64 ToppingId { get; set; }
        public decimal Price { get; set; }
    }
}