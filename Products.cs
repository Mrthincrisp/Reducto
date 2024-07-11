﻿using System;
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

            public Products(string name, decimal price, bool sold, ProductType productType) 
            {
            Name = name;
            Price = price;
            Sold = sold;
            ProductType = productType;
            }  
    }
}