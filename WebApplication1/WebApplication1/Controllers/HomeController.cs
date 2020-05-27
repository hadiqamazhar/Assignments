using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        DataClasses2DataContext dc = new DataClasses2DataContext();

        public ActionResult index()
        {
            return View();
        }

        public ActionResult AddBooks()
        {
            return View();
        }

        public ActionResult ViewBooks(string searchBy,string search)
        {
            if(searchBy=="Name")
            {
                return View(dc.books.Where(x=>x.name==search || search==null).ToList());
            }
            else
            {
                return View(dc.books.Where(x => x.author == search || search==null).ToList());
            }
        }

        public ActionResult Add()
        {
            string bookName = Request["bookname"];
            string author = Request["author"];
            string genre = Request["genre"];

            book b = new book();
            b.name = bookName;
            b.author = author;
            b.genre = genre;

            dc.books.InsertOnSubmit(b);
            dc.SubmitChanges();

            return RedirectToAction("ViewBooks");
        }

        public ActionResult Delete(int id)
        {
            var selection = dc.books.First(c => c.Id == id);
            dc.books.DeleteOnSubmit(selection);
            dc.SubmitChanges();
            
            return RedirectToAction("ViewBooks");
        }

        public ActionResult EditBook(int id)
        {
            return View(dc.books.First(c=>c.Id==id));
        }

        public ActionResult EditDone(int id)
        {
            var selection = dc.books.First(c => c.Id == id);
            selection.name = Request["bookname"];
            selection.author = Request["author"];
            selection.genre = Request["genre"];
            dc.SubmitChanges();

            return RedirectToAction("ViewBooks");
        }
    }
}