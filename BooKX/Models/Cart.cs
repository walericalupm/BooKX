using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooKX.Models
{
    public class Cart
    {
        public List<int> lstBooksId { get; set; }
        public string User_Id { get; set; }

    }
}