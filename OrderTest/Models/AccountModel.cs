using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace OrderTest.Models
{
    public class AccountModel
    {
        #region Order List
        public string OrderList(string userid)
        {
            List<AccountViewModel> list = new List<AccountViewModel>();
            try
            {
                string sql = @"
                select o.OrderId, o.OrderItem, p.Price, p.Cost, o.Status 
                from OrderList o
                left join Product p on o.OrderItem = p.Item
                where userid=@userid
                order by OrderItem
                ";

                using (DataTable dt = new DataTable())
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionModel.ConnStr()))
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("userid", userid);
                        da.Fill(dt);
                    }
                    var query = dt.AsEnumerable().Select(i => new AccountViewModel
                    {
                        OrderId = i.Field<string>("OrderId") ?? "",
                        OrderItem = i.Field<string>("OrderItem") ?? "",
                        Price = i.Field<int>("Price").ToString(),
                        Cost = i.Field<int>("Cost").ToString(),
                        Status = i.Field<string>("Status") ?? "",
                    });
                    list.AddRange(query);
                }

            }
            catch { }
            return JsonConvert.SerializeObject(list);
        }
        #endregion

        #region Product Detail
        public AccountViewModel ProductDetail(string OrderItem)
        {
            AccountViewModel m = new AccountViewModel();
            try
            {
                string sql = @"select item, price, cost from Product where item=@orderitem ";
                using (SqlConnection conn = new SqlConnection(ConnectionModel.ConnStr()))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("orderitem", OrderItem);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        m.OrderItem = dr["item"].ToString();
                        m.Price = dr["price"].ToString();
                        m.Cost = dr["cost"].ToString();

                    }
                }

            }
            catch { }
            return m;
        }
        #endregion

        #region 更改狀態
        public bool ChangeStatus(string orderid)
        {
            bool mRet = false;
            try
            {
                string sql = @"
                declare
	                @Sid varchar(20);
	                set @Sid = (select ConCat('S0', CONVERT(VARCHAR(8), GETDATE(), 112), Right('000' + Cast(count(1)+1 as varchar),3)) from Shipping);
                begin 
	                update OrderList set Status='1' where OrderId=@OrderId;
	                insert into Shipping values(@Sid, @OrderId, '3', GETDATE());
                end;
                ";

                using (SqlConnection conn = new SqlConnection(ConnectionModel.ConnStr()))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("OrderId", orderid);
                    cmd.ExecuteNonQuery();
                    mRet = true;
                }
            }
            catch { }
            return mRet;
        }
        #endregion
    }
}