﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LuckShopping.ProductAPI.DTOs
{
    public class ProductDTO
    {
       
        public int Id { get; set; }
     
        public string Name { get; set; }
    
        public decimal Price { get; set; }
      
        public string Description { get; set; }

        public string CategoryName { get; set; }

        public string ImageURL { get; set; }
    }
}
