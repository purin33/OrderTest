using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using OrderTest.Models;

namespace OrderTest.Controllers
{
    public class AccountController : Controller
    {
        AccountModel accountModel = new AccountModel();
        public ActionResult Order()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Index", "Login");

            string userid = Session["UserID"].ToString();
            var list = JsonConvert.DeserializeObject<List<AccountViewModel>>(accountModel.OrderList(userid));
            return View(list);
        }

        public ActionResult ProductDetail(string OrderItem)
        {
            return View(accountModel.ProductDetail(OrderItem));
        }

        public ActionResult ChangeStatus(string OrderID)
        {
            string mRet = string.Empty;
            int count = 0;//更新數量
            if (string.IsNullOrEmpty(OrderID))
            {
                mRet = "請選擇項目";
            }
            else
            {
                string[] tmp = OrderID.Split(',');
                foreach(var i in tmp)
                {
                    count += accountModel.ChangeStatus(i) ? 1 : 0;
                }

                mRet = $"已更新{count}筆資料";
            }


            return Content(mRet);
        }
    }
}