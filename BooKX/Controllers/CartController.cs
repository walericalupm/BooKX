using BooKX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            if (cart.lstBooksId != null && cart.lstBooksId.Count > 0)
            {
                foreach (int book_id in cart.lstBooksId)
                {
                    Book book = db.Books.Find(book_id);
                    lstBooks.Add(new BookCart { Id = book_id, Author = book.Author, Name = book.Name, Price = book.Price, Quantity = 1 });
                }
                lstBookView = RemoveBooksDuplicated(lstBooks);
            }
            ViewBag.Total = GetTotal(lstBookView);
            Session["total_shop"] = GetTotal(lstBookView);
            return View(lstBookView);
        }

        public ActionResult Remove(int? id)
        {
            Cart cart = (Session["cart"] == null) ? new Cart() : (Cart)Session["cart"];
            if (cart.lstBooksId != null && cart.lstBooksId.Count > 0)
            {
                cart.lstBooksId.Remove(id.Value);
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ValidateCart()
        {
            Cart cart = (Session["cart"] == null) ? new Cart() : (Cart)Session["cart"];
            if (cart.lstBooksId != null && cart.lstBooksId.Count > 0)
                return Json(true);
            else
                return Json(false);
        }
        public ActionResult Buy()
        {
            Cart cart = (Session["cart"] == null) ? new Cart() : (Cart)Session["cart"];
            decimal total = (Session["total_shop"] == null) ? 0.0M : (decimal)Session["total_shop"];
            ShoppingData shoppingData = new ShoppingData();
            if (User.Identity.IsAuthenticated)
            {
                AspNetUser user = db.AspNetUsers.Where(x => x.UserName == User.Identity.Name).First();
                cart.User_Id = user.Id;
                shoppingData.Address = user.Address;
                shoppingData.City = user.City;
                shoppingData.Country = user.Country;
                shoppingData.PostalCode = user.PostalCode;
                shoppingData.Name = user.UserName;
                shoppingData.Total = total;
                return View(shoppingData);
            }
            else
            {
                string returnUrl = Url.Action("Buy", "Cart");
                return RedirectToAction("Login","Account",new { returnUrl = returnUrl });
            }            
        }
        [HttpPost]
        public ActionResult Buy(ShoppingData shoppingData)
        {
            Cart cart = (Session["cart"] == null) ? new Cart() : (Cart)Session["cart"];
            String randomCode = RandomString(10, false);
            if (cart.lstBooksId != null && cart.lstBooksId.Count > 0)
            {
                foreach (int book_id in cart.lstBooksId)
                {
                    db.Shops.Add(new Shop
                    {
                        Id_AspNetUsers = cart.User_Id,
                        Id_Book = book_id,
                        Code = randomCode
                    });
                }
            }
            var a = db.SaveChanges();
            Session["cart"] = null;
            Session["shopping_data"] = shoppingData;
            Session["random_code"] = randomCode; 
            return RedirectToAction("ShopDetail");
        }

        public ActionResult ShopDetail()
        {
            ShoppingData shoppingData = (ShoppingData)Session["shopping_data"];
            string code = (string)Session["random_code"];
            ViewBag.code = code;
            if (shoppingData == null)
                return RedirectToAction("Index", "Home");
            else
                return View(shoppingData);
        }
        #region Additional Functions
        private List<BookCart> RemoveBooksDuplicated(List<BookCart> lstBooksToRemove)
        {
            List<BookCart> lstBooksAux = new List<BookCart>();
            var q = lstBooksToRemove.GroupBy(x => x.Name)
                .Select(x => new
                {
                    Id = x.First().Id,
                    Name = x.Key,
                    Author = x.First().Author,
                    Price = x.First().Price,
                    Quantity = x.Count()
                }).OrderByDescending(x => x.Quantity).ToList();

            foreach (var x in q)
            {
                lstBooksAux.Add(new BookCart
                {
                    Id = x.Id,
                    Name = x.Name,
                    Author = x.Author,
                    Price = x.Price,
                    Quantity = x.Quantity
                });
            }

            return lstBooksAux;
        }
        private decimal GetTotal(List<BookCart> lstBooks)
        {
            decimal total = 0.0M;
            if(lstBooks!=null && lstBooks.Count > 0)
            {
                foreach(BookCart x in lstBooks)
                {
                    total = total + x.TotalPrice;
                }
            }
            return total;
        }
        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        #endregion
    }
}
