using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QisiqesanaGebiAsebaseb.DataAccess
{
    public class DonerQuick
    {
        public DataTable GetAllDonerQuick()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "pGetAllDonnerQuick";
            DataTable tbl = new DataTable();
            try
            {
                connection.Open();               
                adapter.Fill(tbl);
            }
            catch (Exception ex)
            {
                throw new Exception("GetAllDonerQuick: " + ex.Message);
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }
            return tbl;
        }

        public bool Insert(DonerQuickEntity _entity)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "pInsertDonerQuick";
            command.Parameters.Add(new SqlParameter("@DonerFirstName", _entity.DonatingInterval));
            command.Parameters.Add(new SqlParameter("@DonerSecondName", _entity.DonerSecondName));
            command.Parameters.Add(new SqlParameter("@Address", _entity.Address));
            command.Parameters.Add(new SqlParameter("@ItemsToDonate", _entity.ItemsToDonate));
            command.Parameters.Add(new SqlParameter("@AmountDonated", _entity.AmountDonated));
            command.Parameters.Add(new SqlParameter("@DonatingInterval", _entity.DonatingInterval));
            command.Parameters.Add(new SqlParameter("@FirstDonationStartDate", _entity.FirstDonationStartDate));
            command.Parameters.Add(new SqlParameter("@DonatedTo", _entity.DonatedTo));
            command.Parameters.Add(new SqlParameter("@EmailAddress", _entity.EmailAddress));
            command.Parameters.Add(new SqlParameter("@PromisedAmount", _entity.PromisedAmount));
            command.Parameters.Add(new SqlParameter("@DonerLastName", _entity.DonerLastName));
            command.Parameters.Add(new SqlParameter("@Contribution", _entity.Contribution));
            command.Parameters.Add(new SqlParameter("@FullName", _entity.FullName));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }
        }

        public bool Update(DonerQuickEntity _entity)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "pUpdateDonnerQuick";
            command.Parameters.Add(new SqlParameter("@DonerID", _entity.DonerID));
            command.Parameters.Add(new SqlParameter("@DonerFirstName", _entity.DonatingInterval));
            command.Parameters.Add(new SqlParameter("@DonerSecondName", _entity.DonerSecondName));
            command.Parameters.Add(new SqlParameter("@Address", _entity.Address));
            command.Parameters.Add(new SqlParameter("@ItemsToDonate", _entity.ItemsToDonate));
            command.Parameters.Add(new SqlParameter("@AmountDonated", _entity.AmountDonated));
            command.Parameters.Add(new SqlParameter("@DonatingInterval", _entity.DonatingInterval));
            command.Parameters.Add(new SqlParameter("@FirstDonationStartDate", _entity.FirstDonationStartDate));
            command.Parameters.Add(new SqlParameter("@DonatedTo", _entity.DonatedTo));
            command.Parameters.Add(new SqlParameter("@EmailAddress", _entity.EmailAddress));
            command.Parameters.Add(new SqlParameter("@PromisedAmount", _entity.PromisedAmount));
            command.Parameters.Add(new SqlParameter("@DonerLastName", _entity.DonerLastName));
            command.Parameters.Add(new SqlParameter("@Contribution", _entity.Contribution));
            command.Parameters.Add(new SqlParameter("@FullName", _entity.FullName));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }
        }
    }

    public class DonerQuickEntity
    {
        public int DonerID
        {
            set;
            get;
        }
        public string DonerFirstName
        {
            set;
            get;
        }
        public string DonerSecondName
        {
            set;
            get;
        }
        public string Address
        {
            set;
            get;
        }
        public string ItemsToDonate
        {
            set;
            get;
        }
        public decimal AmountDonated
        {
            set;
            get;
        }
        public string DonatingInterval
        {
            set;
            get;
        }
        public DateTime FirstDonationStartDate
        {
            set;
            get;
        }
        public string DonatedTo
        {
            set;
            get;
        }
        public string EmailAddress
        {
            set;
            get;
        }
        public decimal PromisedAmount
        {
            set;
            get;
        }
        public string DonerLastName
        {
            set;
            get;
        }
        public string Contribution
        {
            set;
            get;
        }
        public string FullName
        {
            set;
            get;
        }
    }
}
