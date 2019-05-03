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
            List<BookCart> lstBooks = new List<BookCart>();
            List<BookCart> lstBookView = new List<BookCart>();
            if (cart.lstBooksId!=null && cart.lstBooksId.Count > 0)
            {
                foreach (int book_id in cart.lstBooksId){
                    Book book = db.Books.Find(book_id);
                    lstBooks.Add(new BookCart { Id = book_id, Author = book.Author, Name = book.Name, Price = book.Price, Quantity = 1 });
                }
            }


            return View(lstBooks);
        }

        public ActionResult Remove(int? id)
        {
            return View();
        }
    }
}
