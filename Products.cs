using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reducto
{
    public class Products
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public bool Sold { get; set; }
        public ProductType? ProductType { get; set; }

        public DateTime DateStocked {get; set;}

            public Products(string name, decimal price, bool sold, ProductType productType, DateTime dateStocked) 
            {
            Name = name;
            Price = price;
            Sold = sold;
            ProductType = productType;
            DateStocked = dateStocked;
            }  
    public int DaysOnShelf
    {
        get
        {
            TimeSpan timeOnShelf = DateTime.Now - DateStocked;
            return timeOnShelf.Days;
        }
    }
    }
}