﻿using Project03.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project03.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryContext _db;

        public BookController(LibraryContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Book()
        {
            var books = _db.Books.ToList();
            ViewData["books"] = books;
            return View();
        }

        public IActionResult Details(int id)
        {
            var book = _db.Books.ToList().Find(P => P.Id == id);

            if (book == null)
            {
                return Content("ii");
            }

            ViewData["books"] = book;
            return View(book);
        }

        //Get - path: Book/Create
        public IActionResult Create()
        {
            return View();
        }

        //Post - path: Book/Create
        [HttpPost]
        public IActionResult Create([Bind("Title", "Author", "Price", "Availabe")] Book bookItem) // Bind with product model
        {
            if (ModelState.IsValid)// check the state of the model
            {
                _db.Books.Add(bookItem);
                _db.SaveChanges();
                return RedirectToAction("Book");
            }
            return View(bookItem);
        }

        public IActionResult Edit(int id)
        {
            var book = _db.Books.ToList().Find(p => p.Id == id);
            if (id == null || book == null)
            {
                return RedirectToAction("Book");
            }
            ViewData["Books"] = book;
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("Title", "Author", "Price", "Availabe")] Book bookItem)
        {
            var Book = _db.Books.ToList().Find(p => p.Id == id);
            Book.Title = bookItem.Title;
            Book.Author = bookItem.Author;
            Book.Price = bookItem.Price;
            Book.Availabe = bookItem.Availabe;
            _db.Books.Update(Book);
            _db.SaveChanges();
            return RedirectToAction("Book");
        }


        public IActionResult Delete(int id)
        {
            var book = _db.Books.FirstOrDefault(p => p.Id == id);
            _db.Books.Remove(book);
            _db.SaveChanges();
            return RedirectToAction("Book");
        }

    }
}

