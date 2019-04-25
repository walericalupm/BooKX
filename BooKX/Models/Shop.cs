namespace BooKX.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Shop")]
    public partial class Shop
    {
        public int Id { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        public DateTime? Date { get; set; }

        [Required]
        [StringLength(128)]
        public string Id_AspNetUsers { get; set; }

        public int Id_Book { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Book Book { get; set; }
    }
}
