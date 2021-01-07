using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderTest.Models
{
    public class LoginModel
    {
        #region 登入
        public bool Login(string ID, string PW)
        {
            bool mRet = false;
            try
            {
                string sql = @"select count(1) as cnt from Account where id = @ID and pw = @PW ";
                using (SqlConnection conn = new SqlConnection(ConnectionModel.ConnStr()))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("ID", ID);
                    cmd.Parameters.AddWithValue("PW", PW);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (Convert.ToInt32(dr["cnt"]) != 0)
                        {
                            mRet = true;
                        }
                    }

                }
            }
            catch { }
            return mRet;
        }
        #endregion

    }
}