using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderTest.Models
{
    public class AccountViewModel
    {
        public string OrderId { get; set; } = "";
        public string OrderItem { get; set; } = "";
        public string Status { get; set; } = "";
        public string UserID { get; set; } = "";
        public string Price { get; set; } = "";
        public string Cost { get; set; } = "";
        public string StatusText => StatusToText(Status);

        public string StatusToText(string status)
        {
            string mRet = string.Empty;
            switch (status)
            {
                case "0":
                    mRet = "Payment completed";
                    break;
                case "1":
                    mRet = "To be shipped";
                    break;
                case "3":
                    mRet = "New";
                    break;
            }
            return mRet;
        }
    }
}