using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BooKX.Models
{
    public class ShoppingData
    {
        //Only to Create the View with Scalaffolding 
        public int Id { get; set; }
        [Required]
        [DisplayName("Nombre")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Pais")]
        public string Country { get; set; }
        [Required]
        [DisplayName("Ciudad")]
        public string City { get; set; }
        [Required]
        [DisplayName("Direccion del Envio")]
        public string Address { get; set; }
        [Required]
        [DisplayName("Codigo Postal")]
        public string PostalCode { get; set; }
        [Required]
        [DisplayName("Total a Pagar")]
        public decimal Total { get; set; }
        [Required]
        [DisplayName("Titular de la Tarjeta")]
        public string CardName { get; set; }
        [Required]
        [DataType(DataType.CreditCard)]
        [DisplayName("Numero de Tarjeta")]
        public int CardNumber { get; set; }
        [Required]
        [DisplayName("Mes")]
        public int ExpirationMonth { get; set; }
        [Required]
        [DisplayName("Año")]
        public int ExpirationYear { get; set; }
        [Required]
        [DisplayName("CCV")]
        public int CCVNumber { get; set; }
    }
}