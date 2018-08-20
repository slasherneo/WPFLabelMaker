using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WPFLabelMaker.Class;

namespace WPFLabelMaker
{
    public class DBLoadUtility
    {
        private SqlConnection connection;
        public DBLoadUtility()
        {            
        }

        private void GetDatabaseConnection()
        {            
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString);
            connection.Open();
        }

        public List<ResultItem> GetSearchData(String number)
        {
            List<ResultItem> data = new List<ResultItem>();
            //A.[貨單單號],A.[品名規格],A.[數量],B.[公司簡稱],A.[型號]
            GetDatabaseConnection();
            string selectedStudentQuery = @"SELECT A.[貨單單號],A.[品名規格],A.[數量],B.[公司簡稱],A.[型號],B.[公司編號] FROM  [泉裕ERP].[dbo].[銷貨細項] A, [泉裕ERP].[dbo].[客戶] B WHERE 
[貨單單號] ='"+number+"' and A.[公司編號] =B.[公司編號]";
            SqlCommand command = new SqlCommand(selectedStudentQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ResultItem tempdata = new ResultItem { Name = reader[1].ToString(), Number = reader[2].ToString(), Company = reader[3].ToString(), ItemID = reader[4].ToString(),CompanyId =reader[5].ToString() };
                data.Add(tempdata);
            }
            connection.Close();
            return data;
        }
    }
}
