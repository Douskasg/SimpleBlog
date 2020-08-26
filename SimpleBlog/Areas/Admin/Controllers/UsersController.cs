using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Security;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTab("users")]
    public class UsersController : Controller
    {
        // GET: Admin/Users
        public ActionResult Index()
        {
            return View(new UsersIndex 
            { 
                Users = Database.Session.Query<User>().ToList()
            });
        }

        public ActionResult New()
        {
            return View(new Usersnew
            {
            });
        }
        [HttpPost]
        public ActionResult New(Usersnew form)
        {
            if (Database.Session.Query<User>().Any(User => User.Username == form.Username))
                ModelState.AddModelError("Username", "Username must be unique");

            if (!ModelState.IsValid)
                return View(form);

            var user = new User
            {
                Email = form.Email,
                Username = form.Username
            };

            user.SetPassword(form.Password);

            Database.Session.Save(user);

            return RedirectToAction("index");
        }

        public ActionResult Edit(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            return View(new UsersEdit
            {
                Username = user.Username,
                Email = user.Email
            });

        }

        [HttpPost]
        [Obsolete]
        public ActionResult Edit(int id, UsersEdit form)
        {
            Database.Session.Transaction.Begin();

            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            if (Database.Session.Query<User>().Any(u => u.Username == form.Username && u.Id != id))
                ModelState.AddModelError("Username", "Username must be unique");

            if (!ModelState.IsValid)
                return View(form);

            user.Username = form.Username;
            user.Email = form.Email;

            Database.Session.SaveOrUpdate(user);
            Database.Session.Transaction.Commit();
            return RedirectToAction("index");

        }

        public ActionResult ResetPassword(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            return View(new UsersRessetPassword
            {
                Username = user.Username,
              });
        }
        [HttpPost]
        [Obsolete]
        public ActionResult ResetPassword(int id, UsersRessetPassword form)
        {
            Database.Session.Transaction.Begin();

            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            form.Username = user.Username;
         
            if (!ModelState.IsValid)
                return View(form);

            user.SetPassword(form.Password);

            Database.Session.SaveOrUpdate(user);
            Database.Session.Transaction.Commit();
            return RedirectToAction("index");
        }
        [HttpPost]
        [Obsolete]
        public  ActionResult Delete(int id)
        {
            Database.Session.Transaction.Begin();

            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            Database.Session.Delete(user);
            Database.Session.Transaction.Commit();

            return RedirectToAction("index");
        }
    }

}