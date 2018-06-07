using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace QisiqesanaGebiAsebaseb.business
{
    class BusinessEntiy
    {
         public static SqlConnection getConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
        }

        public DataTable getMontlyPayments(String projectName,int year)
        {

            SqlConnection connection = getConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            DataTable tbl = new DataTable();
            try
            {
                command.Parameters.Add(new SqlParameter("@ProjectName", projectName));
                command.Parameters.Add(new SqlParameter("@Year", year));

                connection.Open();
                command.CommandText = "getYearlyPaymentByMonthDetail";

                adapter.Fill(tbl);
                return tbl;
               
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }
        }
    
    }
}
