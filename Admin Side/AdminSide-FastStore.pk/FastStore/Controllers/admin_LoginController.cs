﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FastStore.Models;

namespace FastStore.Controllers
{
    public class admin_LoginController : Controller
    {
        FastStoreEntities db = new FastStoreEntities();
        // GET: admin_Login
        public ActionResult Index()
        {
            return View();
        }        

        [HttpPost]
        public ActionResult Login(admin_Login login)
        {
            if (ModelState.IsValid)
            {
                var model = (from m in db.admin_Login
                             where m.UserName == login.UserName && m.Password == login.Password
                            select m).Any();
                if (model)
                {                 
                    var loginInfo = db.admin_Login.Where(x=>x.UserName==login.UserName && x.Password==login.Password).FirstOrDefault();

                    Session["username"] = loginInfo.UserName;
                    TemData.EmpID = loginInfo.EmpID;
                   return RedirectToAction("Index", "Dashboard");
                }       
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "admin_Login");
        }
    }
}