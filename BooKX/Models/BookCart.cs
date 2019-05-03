using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BooKX.Models
{
    public class BookCart
    {
        public int Id { get; set; }
        [DisplayName("Autor")]
        public string Author { get; set; }
        [DisplayName("Libro")]
        public string Name { get; set; }
        [DisplayName("Cantidad")]
        public int Quantity { get; set; }
        [DisplayName("Precio Unitario")]
        public decimal Price { get; set; }
        [DisplayName("Precio Total")]
        public decimal TotalPrice {
            get
            {
                return Quantity * Price;
            }
        }
    }
}