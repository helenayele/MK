﻿using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QisiqesanaGebiAsebaseb.business
{
    class Doner
    {
        #region memberVariables


        #endregion
        #region memberMethods

        public DataTable GetDonerDetailByID(int id)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "pGetDonerDetailByID";
            command.Parameters.Add(new SqlParameter("@Id", id));
            DataTable tbl = new DataTable();
            try
            {
                connection.Open();
                adapter.Fill(tbl);
            }
            catch (Exception ex)
            {
                throw new Exception("GetDonerDetailByID: " + ex.Message);
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }
            return tbl;
        }

        #endregion
    }

    
}
