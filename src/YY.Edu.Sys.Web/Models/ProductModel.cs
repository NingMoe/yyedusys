﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace YY.Edu.Sys.Web.Models
{

    /// <summary>
    /// 商品实体类
    /// </summary>
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ProductModel()
        {
        }

        public ProductModel(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }


        private static List<ProductModel> ProductList { get; set; }

    }
}