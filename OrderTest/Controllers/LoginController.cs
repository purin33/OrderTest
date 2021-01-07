using OrderTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderTest.Controllers
{
    public class LoginController : Controller
    {
        LoginModel loginModel = new LoginModel();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Check(LoginViewModel m)
        {
            bool mRet = loginModel.Login(m.ID, m.PW);
            string text = "帳號或密碼錯誤";
            if (mRet)
            {
                Session["UserID"] = m.ID;
                text = "成功";
            }

            return Content(text);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}