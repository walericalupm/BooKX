using BooKX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooKX.Controllers
{
    public class CartController : Controller
    {
        private BooKXDBModel db = new BooKXDBModel();
        // GET: Cart
        public ActionResult Index()
        {
            Cart cart = (Session["cart"] == null) ? new Cart() : (Cart)Session["cart"];
            List<Book> lstBooks = new List<Book>();
            if (cart.lstBooksId!=null && cart.lstBooksId.Count > 0)
            {
                foreach (int book_id in cart.lstBooksId){
                    lstBooks.Add(db.Books.Find(book_id));
                }
            }
            return View(lstBooks);
        }
    }
}
