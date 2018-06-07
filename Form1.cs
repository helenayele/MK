using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
//using Excel = Microsoft.Office.Interop.Excel;
namespace QisiqesanaGebiAsebaseb
{
    public partial class Form1 : Form
    {
        #region memberVariables

        business.Project _project = new business.Project();
        business.Doner _doner = new business.Doner();

        #endregion

        public Form1()
        {
            InitializeComponent();
        }
       
        int donerId = 0;
        int projectId = 0;
        String projectName;
        DateTime paymentPeriod;
        private void Form1_Load(object sender, EventArgs e)
        {
                
            AssigenDataSourceToDGVW();
            AssignDataSourceToDonersGV();
            //tabPage2.Size = (Width,this.Height);
            paymentDate.Format = DateTimePickerFormat.Custom;
            paymentDate.CustomFormat = "MMMM-yyyy";
            DataTable t = getAllProjects();

            DonatedToCmbo.DataSource = t;// t.Select("true", "Name").CopyToDataTable(); 
            
            DonatedToCmbo.DisplayMember = "name";
            DonatedToCmbo.ValueMember = "name";
                     
        }

        private void AssignDataSourceToDonersGV()
        {
             donersGridView.DataSource = getAllDoners();
         
           /**  DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
             btnColumn.Name = "btnEdit";
             btnColumn.HeaderText = "Edit";
             btnColumn.Text = "Edit";
             btnColumn.UseColumnTextForButtonValue = true;
             donersGridView.Columns.Add(btnColumn); **/
            
            
            //projectDataGridView.Columns.Add("Column", "Test");

            //Image image = Image.FromFile(Application.StartupPath + "~\\bin\\Debug\\Edit.png");
            //Bitmap image;

            //Image image = Image.FromFile("Edit.jpg");
            //DataGridViewImageColumn img = new DataGridViewImageColumn();
           
            //img.HeaderText = "Edit";
            //img.Name = "editImg";
            //img.Image = image;
            //projectDataGridView.Columns.Add(img);           
        }

        private void AssigenDataSourceToDGVW()
        {
            projectDataGridView.DataSource = getAllProjects();

          /**  DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.Name = "btnEdit";
            btnColumn.HeaderText = "Edit";
            btnColumn.Text = "Edit";
            btnColumn.UseColumnTextForButtonValue = true;
            projectDataGridView.Columns.Add(btnColumn); **/


            //projectDataGridView.Columns.Add("Column", "Test");

            //Image image = Image.FromFile(Application.StartupPath + "~\\bin\\Debug\\Edit.png");
            //Bitmap image;

            //Image image = Image.FromFile("Edit.jpg");
            //DataGridViewImageColumn img = new DataGridViewImageColumn();

            //img.HeaderText = "Edit";
            //img.Name = "editImg";
            //img.Image = image;
            //projectDataGridView.Columns.Add(img);           
        }
        private void getProjectName()
        {
            DataTable tbl = getAllProjects();
            DataRowCollection r = tbl.Rows;
            List<String> projectNames = new List<String>();
            foreach (DataRow  i in r)
            {
               // projectNames.Add(i.ItemArray.Select "Name");
            }
           // sessionNo.DataSource = sessionNumbers;

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        public SqlConnection getConnection()
        {
            SqlConnection connection =
               new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);


            return connection;
        }
     
        public DataTable insertProject(string procedureName)
        {
        //SqlConnection connection =
        //        new SqlConnection(ConfigurationManager.ConnectionStrings["QisiqesanaGebiAsebaseb.Properties.Settings.dummyConnectionString"].ConnectionString);
            SqlConnection connection = getConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;
            DataTable tbl = new DataTable();
      
   
            try
            {
                command.Parameters.Add(new SqlParameter("@projectName", textProjectName.Text));
                command.Parameters.Add(new SqlParameter("@BudgetCost", textProjectCost.Text.Equals("") ? 0 : Convert.ToDouble(textProjectCost.Text)));
                command.Parameters.Add(new SqlParameter("@ProjectTypeID", textProjectType.Text));
                command.Parameters.Add(new SqlParameter("@projectLocation", textProjectLocation.Text));
                command.Parameters.Add(new SqlParameter("@ConsumedCost", ProjectConsumedCost.Text.Equals("") ? 0 : Convert.ToDouble(ProjectConsumedCost.Text)));

                command.Parameters.Add(new SqlParameter("@ProjectStatusID", "NEW"));
                command.Parameters.Add(new SqlParameter("@plannedEndDate", plannedEndDate.Value));
                command.Parameters.Add(new SqlParameter("@plannedStartDate", plannedStartDate.Value));
                command.Parameters.Add(new SqlParameter("@ActualStartDate", ActualStartDate.Value));
                command.Parameters.Add(new SqlParameter("@ActualEndDate", actualEndDate.Value));
                command.Parameters.Add(new SqlParameter("@FundStatusID", 1));
                command.Parameters.Add(new SqlParameter("@MonitorID",1));// Convert.ToInt16(sessionType.SelectedValue)));
                command.Parameters.Add(new SqlParameter("@ExecutorID", 1));
                command.Parameters.Add(new SqlParameter("@ownerId", 1));
                command.Parameters.Add(new SqlParameter("@ExpectedYearlyFund", texExpectedFund.Text.Equals("") ? 0 : Convert.ToDouble(texExpectedFund.Text)));
	
	            connection.Open();
                command.CommandText = procedureName;
                adapter.Fill(tbl);
                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
                projectDataGridView.DataSource = getAllProjects();
            }

           
        }

        public DataTable updateProject(int projectId)
        {
            SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;
            DataTable tbl = new DataTable();


            try
            {
                command.Parameters.Add(new SqlParameter("@ProjectID", projectId));// Convert.ToInt16(sessionType.SelectedValue)));
                command.Parameters.Add(new SqlParameter("@projectName", textProjectName.Text));
                command.Parameters.Add(new SqlParameter("@BudgetCost", textProjectCost.Text.Equals("") ? 0 : Convert.ToDouble(textProjectCost.Text)));
                command.Parameters.Add(new SqlParameter("@ProjectTypeID", textProjectType.Text));
                command.Parameters.Add(new SqlParameter("@projectLocation", textProjectLocation.Text));
                command.Parameters.Add(new SqlParameter("@ConsumedCost", ProjectConsumedCost.Text.Equals("") ? 0 : Convert.ToDouble(ProjectConsumedCost.Text)));
                command.Parameters.Add(new SqlParameter("@ProjectStatusID", projectStatus.Text));
                command.Parameters.Add(new SqlParameter("@plannedEndDate", plannedEndDate.Value));
                command.Parameters.Add(new SqlParameter("@plannedStartDate", plannedStartDate.Value));
                command.Parameters.Add(new SqlParameter("@ActualStartDate", ActualStartDate.Value));
                command.Parameters.Add(new SqlParameter("@ActualEndDate", actualEndDate.Value));
                command.Parameters.Add(new SqlParameter("@FundStatusID", 1));
                command.Parameters.Add(new SqlParameter("@MonitorID", 1));// Convert.ToInt16(sessionType.SelectedValue)));
                command.Parameters.Add(new SqlParameter("@ExecutorID", 1));
                command.Parameters.Add(new SqlParameter("@ownerId", 1));
                command.Parameters.Add(new SqlParameter("@ExpectedYearlyFund", texExpectedFund.Text.Equals("") ? 0 : Convert.ToDouble(texExpectedFund.Text)));

                connection.Open();
                command.CommandText = "ProjectUpdate";
                adapter.Fill(tbl);
                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
                projectDataGridView.DataSource = getAllProjects();
            }
        }
        public DataTable getAllProjects()
        {
            //SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlConnection connection = getConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;
            DataTable tbl = new DataTable();

            try
            {
                connection.Open();
                command.CommandText = "pGetAllProjects";
                command.CommandType = CommandType.StoredProcedure;
                adapter.Fill(tbl);
                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }
        }
        public DataTable getAllProjectNames()
        {
           // SqlConnection connection =
            //    new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlConnection connection = getConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.Text;
            command.Connection = connection;
            DataTable tbl = new DataTable();


            try
            {
                connection.Open();
                command.CommandText = "select [Name] from Project ";
                adapter.Fill(tbl);
                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }
        }

        public DataTable getAllMonthNames()
        {
            //SqlConnection connection =
              //  new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlConnection connection = getConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.Text;
            command.Connection = connection;
            DataTable tbl = new DataTable();


            try
            {
                connection.Open();
                command.CommandText = "SELECT [ETMonth], [ETMonthName] FROM vMonthNames";
                adapter.Fill(tbl);
                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }
        }
        public void deleteProject(int id)
        {
            //SqlConnection connection =
             //   new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlConnection connection = getConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;
         


            try
            {
                command.Parameters.Add(new SqlParameter("@ProjectId", id));
                
                connection.Open();
                command.CommandText = "ProjectDelete";
                command.ExecuteNonQuery();
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }
        }

        private DataTable getProject(int projectId)
        {
           // SqlConnection connection =
             //  new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlConnection connection = getConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.Text;
            command.Connection = connection;
            DataTable tbl = new DataTable();
            try
            {
                connection.Open();
                command.CommandText = "SELECT * FROM Project where ProjectID =" + projectId;
                adapter.Fill(tbl);
                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }
        }
        private void projectSave_Click(object sender, EventArgs e)
        {
           
            if (projectId == 0)
            {
                DataTable tbl = this.insertProject("ProjectSave");
            }
            else
            {
             DataTable tbl = this.updateProject(projectId);
                
            }
            projectDataGridView.DataSource = getAllProjects();
            MessageBox.Show("Successfully Saved");
            ClearAllProjectFields();
            
        }

        private void projectDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SaveDoner_Click(object sender, EventArgs e)
        {
            DataTable tbl = this.insertDoner();
            donersGridView.DataSource = getAllDoners();
            MessageBox.Show("Successfully Saved");
        }


        private Boolean CheckRepeatedDoner()
        {
            //SqlConnection connection =
            //   new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlConnection connection = getConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.Text;
            command.Connection = connection;
            DataTable tbl = new DataTable();
            try
            {
                connection.Open();
                command.CommandText = "SELECT * FROM DonerQuick2 where DonerPay.DonerFirstName =" + donerId.ToString();
                adapter.Fill(tbl);
                return tbl.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }
        }

       public DataTable insertDoner()
        {
            //SqlConnection connection =
            //        new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlConnection connection = getConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;
            DataTable tbl = new DataTable();
        
            try
            {
                command.Parameters.Add(new SqlParameter("@DonerFirstName", donerFirstName.Text));
                command.Parameters.Add(new SqlParameter("@DonerSecondName", donerFatherName.Text));
                command.Parameters.Add(new SqlParameter("@DonerLastName", grandFatherName.Text));                
                command.Parameters.Add(new SqlParameter("@Address", donerAddress.Text));
                command.Parameters.Add(new SqlParameter("@ItemsToDonate", donerItemsToDonate.Text));
                command.Parameters.Add(new SqlParameter("@AmountDonated", donerAmountDonated.Text.Equals("") ? 0 : Convert.ToDouble(donerAmountDonated.Text)));
                command.Parameters.Add(new SqlParameter("@DonatingInterval", donerDonatingInterval.Text));
                command.Parameters.Add(new SqlParameter("@FirstDonationStartDate", donerDonationStart.Value));
                command.Parameters.Add(new SqlParameter("@DonatedTo", DonatedToCmbo.SelectedValue));
                command.Parameters.Add(new SqlParameter("@EmailAddress", DonerEmailAddress.Text));
                command.Parameters.Add(new SqlParameter("@PromisedAmount", donerPromisedAmount.Text));
                connection.Open();
                command.CommandText = "DonerQuickSave";
                adapter.Fill(tbl);
                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }
        }

        public DataTable updateDoner(int donerId)
        {
            //SqlConnection connection =
                //    new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlConnection connection = getConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;
            DataTable tbl = new DataTable();


            try
            {
                command.Parameters.Add(new SqlParameter("@DonerID", donerId));// Convert.ToInt16(sessionType.SelectedValue)));
                command.Parameters.Add(new SqlParameter("@DonerFirstName", donerFirstName.Text));
                command.Parameters.Add(new SqlParameter("@DonerSecondName", donerFatherName.Text));
                command.Parameters.Add(new SqlParameter("@DonerLastName", grandFatherName.Text));
                command.Parameters.Add(new SqlParameter("@Address", donerAddress.Text));
                command.Parameters.Add(new SqlParameter("@ItemsToDonate", donerItemsToDonate.Text));
               
                command.Parameters.Add(new SqlParameter("@AmountDonated", donerAmountDonated.Text.Equals("") ? 0 : Convert.ToDouble(donerAmountDonated.Text)));
                command.Parameters.Add(new SqlParameter("@DonatingInterval", donerDonatingInterval.Text));
                command.Parameters.Add(new SqlParameter("@FirstDonationStartDate", donerDonationStart.Value));
                command.Parameters.Add(new SqlParameter("@DonatedTo", DonatedToCmbo.SelectedValue));
                command.Parameters.Add(new SqlParameter("@EmailAddress", DonerEmailAddress.Text));
                command.Parameters.Add(new SqlParameter("@PromisedAmount", donerPromisedAmount.Text.Equals("")? 0 : Convert.ToDouble(donerPromisedAmount.Text)));


                connection.Open();
                command.CommandText = "DonerQuickUpdate";
                adapter.Fill(tbl);
                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }
        }
                public DataTable getAllDoners()
        {
           // SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlConnection connection = getConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;
            DataTable tbl = new DataTable();

            try
            {
                connection.Open();
                command.CommandText = "pGetAllDoners";
                command.CommandType = CommandType.StoredProcedure;
                adapter.Fill(tbl);
                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }
        }

        public void deleteDoner(int id)
        {
           // SqlConnection connection =
              //  new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlConnection connection = getConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;



            try
            {
                command.Parameters.Add(new SqlParameter("@DonerId", id));

                connection.Open();
                command.CommandText = "DonerQuickDelete";
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }
        }
        //payment 
        public DataTable insertPayment()
        {
            //SqlConnection connection =
                //    new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlConnection connection = getConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;
            DataTable tbl = new DataTable();
           



            try
            {
                command.Parameters.Add(new SqlParameter("@DonerId", donerId));
                command.Parameters.Add(new SqlParameter("@PaymentAmount", Convert.ToDouble(payiedAmount.Text)));
                command.Parameters.Add(new SqlParameter("@paymentCRV",CRVNumber.Text));
                command.Parameters.Add(new SqlParameter("@PaymentDate", donationDate.Value));
                command.Parameters.Add(new SqlParameter("@ProjectName", ProjectNameCmb.SelectedValue));
                command.Parameters.Add(new SqlParameter("@PaymentPeriod", paymentDate.Value));
                command.Parameters.Add(new SqlParameter("@ItemsToDonate", ItemsDonated.Text));
                command.Parameters.Add(new SqlParameter("@Remark", remark.Text));

                connection.Open();
                command.CommandText = "DonerPaySave";
              //  MessageBox.Show(tbl.Rows.Count.ToString());
                adapter.Fill(tbl);
                return tbl;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }
        }

        public DataTable updateDonerPayment(int donerId,String projectName,DateTime paymentPeriod)
        {
            //SqlConnection connection =
                 //   new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlConnection connection = getConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;
            DataTable tbl = new DataTable();


            try
            {
                command.Parameters.Add(new SqlParameter("@DonerId", donerId));
                command.Parameters.Add(new SqlParameter("@PaymentAmount", Convert.ToDouble(payiedAmount.Text)));
                command.Parameters.Add(new SqlParameter("@paymentCRV", CRVNumber.Text));
                command.Parameters.Add(new SqlParameter("@PaymentDate", donationDate.Value));
                command.Parameters.Add(new SqlParameter("@ProjectName", projectName));
                command.Parameters.Add(new SqlParameter("@PaymentPeriod", paymentPeriod));
                command.Parameters.Add(new SqlParameter("@ItemsToDonate", ItemsDonated.Text));
                command.Parameters.Add(new SqlParameter("@Remark", remark.Text));

                connection.Open();
                command.CommandText = "DonerPayUpdate";
                adapter.Fill(tbl);
                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }
        }

        private DataTable getDonerPayment()
        {
            //SqlConnection connection =
                //    new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlConnection connection = getConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;
            DataTable tbl = new DataTable();


            try
            {
                command.Parameters.Add(new SqlParameter("@DonerId", donerId));
                command.Parameters.Add(new SqlParameter("@ProjectName", projectName));
                command.Parameters.Add(new SqlParameter("@PaymentPeriod", paymentPeriod));
               
                connection.Open();
                command.CommandText = "getDonerPayment";
                adapter.Fill(tbl);
              
                //return tbl.Rows.Count > 0 ? true : false;

                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }
       
        }

        private DataTable getDonerPromise(int dId, int prjId)
        {
           // SqlConnection connection =
                  //  new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlConnection connection = getConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.Text;
            command.Connection = connection;
            DataTable tbl = new DataTable();


            try
            {

                connection.Open();
                command.CommandText = "SELECT * FROM DonerPromise where donerId =" + dId.ToString() + " And ProjectId =" + prjId.ToString();

                adapter.Fill(tbl);

                //return tbl.Rows.Count > 0 ? true : false;

                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }

        }
        public DataTable getAllDonerPromises(int donerID)
        {
            //SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlConnection connection = getConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.Text;
            command.Connection = connection;
            DataTable tbl = new DataTable();


            try
            {
                connection.Open();
                command.CommandText = "SELECT dp.DonerId,dp.projectId, "
                + "  dp.PromisedAmount, dp.donatingInterval, dp.ItemToDonate, "
                   + "dp.Remark, dp.DonationStartDate, dp.DonationEndDate "
                   + "FROM DonerPromise as dp where dp.DonerId =" + donerID.ToString();
                adapter.Fill(tbl);
                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }

        }
        public DataTable getAllDonerPayments(int donerID)
        {
            //SqlConnection connection =
             //   new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlConnection connection = getConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.Text;
            command.Connection = connection;
            DataTable tbl = new DataTable();


            try
            {
                connection.Open();
                command.CommandText = "SELECT DonerQuick.DonerFirstName, DonerQuick.DonerSecondName,"
                + " DonerQuick.Address, DonerPay.DonerId, DonerPay.PaymentAmount, DonerPay.PaymentDate, "
                   + "DonerPay.Remark, DonerPay.ProjectName, DonerPay.PaymentPeriod, DonerPay.PaymentCRV, DonerPay.ItemsToDonate "
                   + "FROM DonerPay1 as DonerPay INNER JOIN DonerQuick2 as DonerQuick ON DonerPay.DonerId = DonerQuick.DonerID where DonerPay.DonerId =" + donerID.ToString();
                adapter.Fill(tbl);
                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }
        }

        public void deleteDonerPayement(int id,DateTime paymentPeriod,String projectName)
        {
            //SqlConnection connection =
              //  new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlConnection connection = getConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;



            try
            {
                command.Parameters.Add(new SqlParameter("@DonerId", id));
                command.Parameters.Add(new SqlParameter("@PaymentPeriod", paymentPeriod));
                command.Parameters.Add(new SqlParameter("@ProjectName", projectName));

                connection.Open();
                command.CommandText = "DonerPayDelete";
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }
        }

        //payment end

        private void donersGridView_DoubleClick(object sender, EventArgs e)
        {
            
           
            if (donersGridView.SelectedRows.Count < 1) return;
           //dRw = donerQuickDSMK.DonerQuick2.FindByDonerID((int)donersGridView.SelectedRows[0].Cells[0].Value);
         
           int selectedRowIndex = donersGridView.SelectedCells[0].RowIndex;
           //object item = donersGridView.Rows[e.RowIndex].Cells[0].Value;

           DataGridViewRow selectedRow = donersGridView.Rows[selectedRowIndex];

           donerId = Convert.ToInt32(selectedRow.Cells["DonerID"].Value);

           DataTable dtProjectDetail = _doner.GetDonerDetailByID(donerId);

           //MessageBox.Show(donerId.ToString());

          AssigenValueToDonerTextBox(dtProjectDetail);
            
      }

       private void donersGridView_DoubleClick_1(object sender, EventArgs e)
        {

            if (donersGridView.SelectedRows.Count < 1) return;
            //dRw = donerQuickDSMK.DonerQuick2.FindByDonerID((int)donersGridView.SelectedRows[0].Cells[0].Value);

            int selectedRowIndex = donersGridView.SelectedCells[0].RowIndex;
            //object item = donersGridView.Rows[e.RowIndex].Cells[0].Value;

            DataGridViewRow selectedRow = donersGridView.Rows[selectedRowIndex];

            donerId = Convert.ToInt32(selectedRow.Cells["DonerID"].Value);

            DataTable dtProjectDetail = _doner.GetDonerDetailByID(donerId);

          //  MessageBox.Show(donerId.ToString());

            AssigenValueToDonerTextBox(dtProjectDetail);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void updatedonerbutton_Click(object sender, EventArgs e)
        {                
            this.updateDoner(donerId);
            donersGridView.DataSource = getAllDoners();
            MessageBox.Show("Successfully Updated");

           ClearAllDonerFields();
        }

        private void ClearAllDonerFields()
        {
            donerFirstName.Text = "";
            donerFatherName.Text ="";
            donerDonatingInterval.Text = "";
            donerAddress.Text = "";
            donerAmountDonated.Text = "";
            donerDonationStart.Value = DateTime.Now;
            DonatedToCmbo.SelectedIndex = -1;
            donerItemsToDonate.Text ="";
            DonerEmailAddress.Text = "";
            donerPromisedAmount.Text = "";
           // DonatedToCmbo.Selected = "";
            donerId = 0;

        }

        private void projectDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check that the button column was clicked
       /*     if (projectDataGridView.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                int selectedRowIndex = projectDataGridView.SelectedCells[0].RowIndex;
                object item = projectDataGridView.Rows[e.RowIndex].Cells[0].Value;

                DataGridViewRow selectedRow = projectDataGridView.Rows[selectedRowIndex];

                projectId = Convert.ToInt32(selectedRow.Cells[0].Value);

                DataTable dtProjectDetail = _project.GetProjectDetailByID(projectId);

                MessageBox.Show(projectId.ToString());

                AssigenValueToTextBox(dtProjectDetail);
            } **/
        }

        private void AssigenValueToDonerTextBox(DataTable dt)
        {
            ClearTextBoxContent();
            donerFirstName.Text = Convert.ToString(dt.Rows[0]["DonerFirstName"]);
            donerFatherName.Text = Convert.ToString(dt.Rows[0]["DonerSecondName"]);
          //  grandFatherName.Text = Convert.ToString(dt.Rows[0]["BudgetCost"]);
            donerAddress.Text = Convert.ToString(dt.Rows[0]["Address"]);
            DonerEmailAddress.Text = Convert.ToString(dt.Rows[0]["EmailAddress"]);
            donerItemsToDonate.Text = Convert.ToString(dt.Rows[0]["ItemsToDonate"]);
            if (!(dt.Rows[0]["AmountDonated"] == null))
                 donerAmountDonated.Text = Convert.ToString(dt.Rows[0]["AmountDonated"]);
            if (!(dt.Rows[0]["FirstDonationStartDate"] == null))
                if (!(dt.Rows[0]["FirstDonationStartDate"].ToString().Equals("")))
                donerDonationStart.Value = (DateTime)dt.Rows[0]["FirstDonationStartDate"];

            donerDonatingInterval.Text = Convert.ToString(dt.Rows[0]["DonatingInterval"]);
            //DonatedToCmbo.SelectedItem = Convert.ToString(dt.Rows[0]["DonatedTo"]);
            DonatedToCmbo.SelectedValue = Convert.ToString(dt.Rows[0]["DonatedTo"]);
            //DonatedToCmbo.SelectedItem = Convert.ToString(dt.Rows[0]["DonatedTo"]);
            //DonatedToCmbo.SelectedText = Convert.ToString(dt.Rows[0]["DonatedTo"]);
             donerPromisedAmount.Text = Convert.ToString(dt.Rows[0]["PromisedAmount"]);  
        }

        private void AssigenValueToTextBox(DataTable dt)
        {
            ClearTextBoxContent();
            textProjectName.Text = Convert.ToString(dt.Rows[0]["Name"]);
            textProjectLocation.Text = Convert.ToString(dt.Rows[0]["projectLocation"]);
            textProjectCost.Text = Convert.ToString(dt.Rows[0]["BudgetCost"]);
            texExpectedFund.Text = Convert.ToString(dt.Rows[0]["ExpectedYearlyFund"]);
            ProjectConsumedCost.Text = Convert.ToString(dt.Rows[0]["ConsumedCost"]);
            plannedStartDate.Value = Convert.ToDateTime(dt.Rows[0]["plannedStartDate"]);
            plannedEndDate.Value = Convert.ToDateTime(dt.Rows[0]["plannedEndDate"]);
            actualEndDate.Value = Convert.ToDateTime(dt.Rows[0]["ActualEndDate"]);
            ActualStartDate.Value = Convert.ToDateTime(dt.Rows[0]["ActualStartDate"]);
            projectStatus.SelectedItem = Convert.ToString(dt.Rows[0]["ProjectStatusID"]);
            textProjectType.SelectedItem = Convert.ToString(dt.Rows[0]["ProjectTypeID"]); 
        }

        private void ClearTextBoxContent()
        {
            textProjectName.Text = "";
            textProjectCost.Text = "";
            texExpectedFund.Text = "";
            ProjectConsumedCost.Text = "";
            plannedStartDate.Text = "";
            plannedEndDate.Text = "";
            actualEndDate.Text = "";
            ActualStartDate.Text = "";
            textProjectType.Text = "";
            projectStatus.Text = "";
        }

        //private void projectDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}

        private void projectDataGridView_DoubleClick(object sender, EventArgs e)
        {
           

       if (projectDataGridView.SelectedRows.Count < 1) return;
       int selectedRowIndex = projectDataGridView.SelectedCells[0].RowIndex;
       //object item = projectDataGridView.Rows[e.RowIndex].Cells[0].Value;

       DataGridViewRow selectedRow = projectDataGridView.Rows[selectedRowIndex];

       projectId = Convert.ToInt32(selectedRow.Cells[0].Value);

       DataTable dtProjectDetail = _project.GetProjectDetailByID(projectId);

       MessageBox.Show(projectId.ToString());

       AssigenValueToTextBox(dtProjectDetail);
          

           
   
       }

        private void pClear_Click(object sender, EventArgs e)
        {
            this.ClearAllProjectFields();
        }

        private void ClearAllProjectFields() {
            textProjectName.Text = "";
            textProjectCost.Text = "";
            textProjectType.SelectedIndex = -1;
            projectStatus.SelectedIndex = -1;
            textProjectLocation.Text = "";
            ProjectConsumedCost.Text ="";
            texExpectedFund.Text = "";
            plannedEndDate.Value = DateTime.Now;
            plannedStartDate.Value = DateTime.Now;
            ActualStartDate.Value = DateTime.Now;
            actualEndDate.Value = DateTime.Now;
     
            projectId = 0;

    }

        private void Clear_Click(object sender, EventArgs e)
        {
            ClearAllDonerFields();
        }

        private void searchDoners_Click(object sender, EventArgs e)
        {
            DataTable tbl = this.findAllDoners();
            donersGridView.DataSource = tbl;
          
            ClearAllDonerFields();


        }
        
        private DataTable findAllDoners()
        {
            SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;
            DataTable tbl = new DataTable();


            try
            {
                command.Parameters.Add(new SqlParameter("@DonerFirstName", donerFirstName.Text.Equals("") ? null : donerFirstName.Text));
                command.Parameters.Add(new SqlParameter("@DonerSecondName", donerFatherName.Text.Equals("") ? null : donerFatherName.Text));
                command.Parameters.Add(new SqlParameter("@Address", donerAddress.Text.Equals("") ? null : donerAddress.Text));
                command.Parameters.Add(new SqlParameter("@ItemsToDonate", donerItemsToDonate.Text.Equals("") ? null : donerItemsToDonate.Text));
                command.Parameters.Add(new SqlParameter("@AmountDonated", donerAmountDonated.Text.Equals("") ? 0 : Convert.ToDouble(donerAmountDonated.Text)));
                command.Parameters.Add(new SqlParameter("@DonatingInterval", donerDonatingInterval.Text.Equals("") ? null : donerDonatingInterval.Text));
                command.Parameters.Add(new SqlParameter("@FirstDonationStartDate", donerDonationStart.Value));
                command.Parameters.Add(new SqlParameter("@DonatedTo", DonatedToCmbo.SelectedValue == null ? null : DonatedToCmbo.SelectedValue));
                command.Parameters.Add(new SqlParameter("@EmailAddress", DonerEmailAddress.Text.Equals("") ? null : DonerEmailAddress.Text));
                command.Parameters.Add(new SqlParameter("@PromisedAmount",  donerPromisedAmount.Text.Equals("") ? 0 : Convert.ToDouble(donerPromisedAmount.Text)));


                connection.Open();
                command.CommandText = "findAllDonerQuick1";
                adapter.Fill(tbl);
                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }

        }

        private DataTable searchProjectsByAllParameters()
        {
            SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;
            DataTable tbl = new DataTable();


            try
            {
                command.Parameters.Add(new SqlParameter("@projectName", textProjectName.Text.Equals("") ? null : textProjectName.Text));
                command.Parameters.Add(new SqlParameter("@projectLocation", textProjectLocation.Text.Equals("") ? null : textProjectLocation.Text));
                command.Parameters.Add(new SqlParameter("@projectType", textProjectType.SelectedIndex == -1 ? null : textProjectType.Text));
                command.Parameters.Add(new SqlParameter("@projectStatus", projectStatus.SelectedIndex == -1 ? null : projectStatus.Text));
              

                connection.Open();
                command.CommandText = "findAllProject";
                adapter.Fill(tbl);
                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }

        }

        private DataTable searchDonerPayements()
        {
            SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;
            DataTable tbl = new DataTable();

            try
            {
              //  command.Parameters.Add(new SqlParameter("@DonerId", donerId));
                command.Parameters.Add(new SqlParameter("@PaymentPeriod", paymentDate.Value));
               // command.Parameters.Add(new SqlParameter("@Address", donerAddress.Text.Equals("") ? null : donerAddress.Text));
                command.Parameters.Add(new SqlParameter("@ItemsToDonate", ItemsDonated.SelectedValue == null ? null : ItemsDonated.SelectedValue));
                command.Parameters.Add(new SqlParameter("@ProjectName", ProjectNameCmb.SelectedValue == null ? null : ProjectNameCmb.SelectedValue));
                

                connection.Open();
                command.CommandText = "findAllDonerPay";
                adapter.Fill(tbl);
                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                adapter.Dispose();
                command.Dispose();
            }

        }

        public DataTable insertDonerPromise()
          {
              SqlConnection connection = getConnection();
              SqlCommand command = new SqlCommand();
              SqlDataAdapter adapter = new SqlDataAdapter(command);
              command.CommandType = CommandType.StoredProcedure;
              command.Connection = connection;
              DataTable tbl1 = new DataTable();
      
   
              try
              {
                  command.Parameters.Add(new SqlParameter("@projectId", pDonatingTo.SelectedValue == null ? null : pDonatingTo.SelectedValue));
                  command.Parameters.Add(new SqlParameter("@donerId", donerId));
                  command.Parameters.Add(new SqlParameter("@promisedAmount", promise.Text.Equals("") ? 0 : Convert.ToDouble(promise.Text)));
                  command.Parameters.Add(new SqlParameter("@PaymentStartDate", donationStart.Value));
                  command.Parameters.Add(new SqlParameter("@PaymentEndDate", DonationEnd.Value));
                  command.Parameters.Add(new SqlParameter("@DonationInterval", pDonatingInterval.Text));
                  command.Parameters.Add(new SqlParameter("@Remark", pRemark.Text.Equals("") ? "":pRemark.Text));
                  command.Parameters.Add(new SqlParameter("@itemToDonate", pItemToDonate.SelectedItem == null ? "Money" : pItemToDonate.SelectedItem));
                  command.Parameters.Add(new SqlParameter("@eventName", eventName.Text));
                  command.Parameters.Add(new SqlParameter("@eventDate", eventDate.Value));
                  connection.Open();
                  command.CommandText = "DonerPromiseSave";
                  adapter.Fill(tbl1);
                  return tbl1;
              }
              catch (Exception ex)
              {
                  throw ex;
              }
              finally
              {
                  connection.Close();
                  adapter.Dispose();
                  command.Dispose();
              }
          }

          public DataTable updateDonerPromise(int donerId,int projectId)
          {
              SqlConnection connection =
                      new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
              SqlCommand command = new SqlCommand();
              SqlDataAdapter adapter = new SqlDataAdapter(command);
              command.CommandType = CommandType.StoredProcedure;
              command.Connection = connection;
              DataTable tbl = new DataTable();


              try
              {

                  command.Parameters.Add(new SqlParameter("@donerId", donerId));
                  command.Parameters.Add(new SqlParameter("@projectId", projectId));
                //  command.Parameters.Add(new SqlParameter("@projectId", pDonatingTo.SelectedValue == null ? null : pDonatingTo.SelectedValue));
                  command.Parameters.Add(new SqlParameter("@promisedAmount", promise.Text.Equals("") ? 0 : Convert.ToDouble(promise.Text)));
                  command.Parameters.Add(new SqlParameter("@PaymentStartDate", donationStart.Value));
                  command.Parameters.Add(new SqlParameter("@PaymentEndDate", DonationEnd.Value));
                  command.Parameters.Add(new SqlParameter("@DonationInterval", pDonatingInterval.Text));
                  command.Parameters.Add(new SqlParameter("@Remark", pRemark.Text.Equals("") ? "" : pRemark.Text));
                  command.Parameters.Add(new SqlParameter("@itemToDonate", pItemToDonate.SelectedItem == null ? "Money" : pItemToDonate.SelectedItem));
                  command.Parameters.Add(new SqlParameter("@eventName", eventName.Text));
                  command.Parameters.Add(new SqlParameter("@eventDate", eventDate.Value));
                  connection.Open();
                  command.CommandText = "DonerPromiseSave";
                  adapter.Fill(tbl);
                  return tbl;
              }
              catch (Exception ex)
              {
                  throw ex;
              }
              finally
              {
                  connection.Close();
                  adapter.Dispose();
                  command.Dispose();
              }
          }
          /*  
              *        public DataTable getAllDonerPromise()
               {
                   SqlConnection connection =
                       new SqlConnection(ConfigurationManager.ConnectionStrings["QisiqesanaGebiAsebaseb.Properties.Settings.dummyConnectionString"].ConnectionString);
                   SqlCommand command = new SqlCommand();
                   SqlDataAdapter adapter = new SqlDataAdapter(command);
                   command.CommandType = CommandType.Text;
                   command.Connection = connection;
                   DataTable tbl = new DataTable();


                   try
                   {
                       connection.Open();
                       command.CommandText = "SELECT ProjectID, Name, plannedStartDate, plannedEndDate, ProjectTypeID, ActualStartDate, ActualEndDate, BudgetCost, ProjectStatusID, projectLocation, ConsumedCost, ExpectedYearlyFund FROM Project";
                       adapter.Fill(tbl);
                       return tbl;
                   }
                   catch (Exception ex)
                   {
                       throw ex;
                   }
                   finally
                   {
                       connection.Close();
                       adapter.Dispose();
                       command.Dispose();
                   }
               }
        */      
           public void deleteDonerPromise(int donerId, int projectId)
               {
                   SqlConnection connection =
                       new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                   SqlCommand command = new SqlCommand();
                   SqlDataAdapter adapter = new SqlDataAdapter(command);
                   command.CommandType = CommandType.StoredProcedure;
                   command.Connection = connection;
         


                   try
                   {
                       command.Parameters.Add(new SqlParameter("@donerId", donerId));
                       command.Parameters.Add(new SqlParameter("@ProjectId", projectId));

                       connection.Open();
                       command.CommandText = "DonerPromiseDelete";
                       command.ExecuteNonQuery();
               
                   }
                   catch (Exception ex)
                   {
                       throw ex;
                   }
                   finally
                   {
                       connection.Close();
                       adapter.Dispose();
                       command.Dispose();
                   }
               }

  /*             private DataTable getDonerPromise(int donerPromiseId)
               {
                   SqlConnection connection =
                      new SqlConnection(ConfigurationManager.ConnectionStrings["QisiqesanaGebiAsebaseb.Properties.Settings.dummyConnectionString"].ConnectionString);
                   SqlCommand command = new SqlCommand();
                   SqlDataAdapter adapter = new SqlDataAdapter(command);
                   command.CommandType = CommandType.Text;
                   command.Connection = connection;
                   DataTable tbl = new DataTable();
                   try
                   {
                       connection.Open();
                       command.CommandText = "SELECT * FROM Project where ProjectID =" + projectId;
                       adapter.Fill(tbl);
                       return tbl;
                   }
                   catch (Exception ex)
                   {
                       throw ex;
                   }
                   finally
                   {
                       connection.Close();
                       adapter.Dispose();
                       command.Dispose();
                   }
               }
       
              */

          private void ShowPayment_Click(object sender, EventArgs e)
        {
            //PaymentTab.Show();
             if (donersGridView.SelectedRows.Count < 1) return;
                tabControl1.SelectTab("PaymentTab");
             //  dRw = dummyDataSet7.DonerQuick.FindByDonerID((int)donersGridView.SelectedRows[0].Cells[0].Value);
                donerId = (int)donersGridView.SelectedRows[0].Cells[0].Value;
               ProjectNameCmb.DataSource = getAllProjectNames();
               ProjectNameCmb.DisplayMember = "name";
               ProjectNameCmb.ValueMember = "name";
             
               donerPaymentGridView1.DataSource = getAllDonerPayments(donerId);
            
        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void donerPaymentInsert_Click(object sender, EventArgs e)
        {
            projectName = (String)ProjectNameCmb.SelectedValue;
            paymentPeriod = paymentDate.Value.Date;
          //  donerId = (int)donerPaymentGridView1.SelectedRows[0].Cells[0].Value;
            if (getDonerPayment().Rows.Count > 0)
            {
                MessageBox.Show("Unable to Insert: You have entered this payment for the doner in the same period and for the same project");
                return;
            }

            this.insertPayment();
            donerPaymentGridView1.DataSource = getAllDonerPayments(donerId);
            MessageBox.Show("Successfully Inserted");
        }

       
        private void donerPaymentSearch_Click(object sender, EventArgs e)
        {
            DataTable tbl = this.searchDonerPayements();
            donerPaymentGridView1.DataSource = tbl;

            ClearAllDonerFields();
        }
        //update doner payment
        private void button2_Click(object sender, EventArgs e)
        {
            DataTable tbl = this.updateDonerPayment(donerId,projectName,paymentPeriod);
            donerPaymentGridView1.DataSource = getAllDonerPayments(donerId);
            MessageBox.Show("Successfully Updated");
            ClearAllProjectFields();
           
           // ClearAllFields();
        }

        private void remark_TextChanged(object sender, EventArgs e)
       {

        }

        private void donerPaymentGridView1_DoubleClick(object sender, EventArgs e)
        {
           
            if (donerPaymentGridView1.SelectedRows.Count < 1) return;

            projectName = (String)donerPaymentGridView1.SelectedRows[0].Cells["ProjectName"].Value;
            paymentPeriod = (DateTime)donerPaymentGridView1.SelectedRows[0].Cells["PaymentPeriod"].Value;
            donerId = (int)donerPaymentGridView1.SelectedRows[0].Cells["DonerID"].Value;

            Object[] paymentKeys = { projectName, paymentPeriod, donerId };
       
          

          
           
          
            paymentDate.Enabled = false;
            ProjectNameCmb.Enabled = false;

        }

        //clear fields after update
        private void ClearDonerPaymentFields()
        {
            ItemsDonated.SelectedIndex = -1;
            payiedAmount.Text = "";
            remark.Text = "";
            paymentDate.Value = DateTime.Now;
            if (ProjectNameCmb.Items.Count > 0)
            ProjectNameCmb.SelectedIndex = 0;
            donationDate.Value = DateTime.Now;
            paymentDate.Enabled = true;
            ProjectNameCmb.Enabled = true;
        }

        private void ClearDonerPromiseFields()
        {
            pItemToDonate.SelectedIndex = -1;
            pDonatingInterval.SelectedIndex = -1;
            promise.Text = "";
            pRemark.Text = "";
            donationStart.Value = DateTime.Now;
            //if (pDonatingTo.Items.Count > 0)
            //    pDonatingTo.SelectedIndex = -1;
            //DonationEnd.Value = DateTime.Now;
            pDonatingTo.SelectedIndex = -1;
            donerId = 0;
            eventName.Text = "";
            eventDate.Value = DateTime.Now;
            
        }


        private void deleteDonerPay_Click(object sender, EventArgs e)
        {
            DialogResult myResult;
            myResult = MessageBox.Show("Are you really deleting this payment?", "Delete Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (myResult == DialogResult.OK)
            {
                this.deleteDonerPayement(donerId, paymentPeriod, projectName);
                donerPaymentGridView1.DataSource = getAllDonerPayments(donerId);
                MessageBox.Show("Successfully Deleted");
                ClearDonerPaymentFields();
            }
            else
            {
                return;
            }

             return;
           

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearDonerPaymentFields();
        }

        //private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        //{
        //    if (tabControl1.SelectedIndex == 2)
        //    {
        //        ProjectNameCmb.DataSource = getAllProjectNames();
        //    }
        //}

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                if (ProjectNameCmb.SelectedIndex == -1)
                {
                    ProjectNameCmb.DataSource = getAllProjectNames();//.Select("true","Name").CopyToDataTable();
                    ProjectNameCmb.DisplayMember = "name";
                    ProjectNameCmb.ValueMember = "name";

                }
            }
           
        }
        //project delete
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult myResult;
            myResult = MessageBox.Show("Are you really deleting this Project?", "Delete Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (myResult == DialogResult.OK)
            {
                this.deleteProject(projectId);
                projectDataGridView.DataSource = getAllProjects();
                MessageBox.Show("Successfully Deleted");
                ClearAllProjectFields();
            }
            else
            {
                return;
            }
            return;
        }

        private void projectDataGridView_Click(object sender, EventArgs e)
        {
           

        //    if (projectDataGridView.SelectedRows.Count < 1) return;
        //    projectId = (int)projectDataGridView.SelectedRows[0].Cells[0].Value;
        //  // Object[] paymentKeys = { projectName, paymentPeriod, donerId };
         
        //   // dRw = dummyDataSet1.DonerQuick.FindByDonerID((int)donersGridView.SelectedRows[0].Cells[0].Value);

           
        }

        private void SearchProject_Click(object sender, EventArgs e)
        {
            DataTable tbl =  this.searchProjectsByAllParameters();


            projectDataGridView.DataSource = tbl;

            this.ClearAllProjectFields();
        }

        private void DeleteDon_Click(object sender, EventArgs e)
        {
            DialogResult myResult;
            myResult = MessageBox.Show("Are you really deleting this Doner?", "Delete Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (myResult == DialogResult.OK)
            {
                this.deleteDoner(donerId);
                donersGridView.DataSource = getAllDoners();
                MessageBox.Show("Successfully Deleted");
                ClearAllDonerFields();
            }
            else
            {
                return;
            }
            return;
        }

        private void donersGridView_Click(object sender, EventArgs e)
        {
           

            //if (donersGridView.SelectedRows.Count < 1) return;
            //donerId = (int)donersGridView.SelectedRows[0].Cells[0].Value;
            // Object[] paymentKeys = { projectName, paymentPeriod, donerId };
           
           

        }

        private void donerPaymentGridView1_Click(object sender, EventArgs e)
        {
            if (donerPaymentGridView1.SelectedRows.Count < 1) return;
            projectName = (String)donerPaymentGridView1.SelectedRows[0].Cells["ProjectName"].Value;
            paymentPeriod = (DateTime)donerPaymentGridView1.SelectedRows[0].Cells["PaymentPeriod"].Value;
            donerId = (int)donerPaymentGridView1.SelectedRows[0].Cells["DonerID"].Value;

        }

        private void paymentReportDateTime_ValueChanged(object sender, EventArgs e)
        {
            //getMonthlyPaymentBindingSource.DataSource = this.getMonthlyPaymentTableAdapter.GetData(paymentReportDateTime.Value.Month, null, paymentReportDateTime.Value.Year);
            //this.reportViewer3.RefreshReport();
        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void PromiseInsert_Click(object sender, EventArgs e)
        {
            if (pDonatingTo.Enabled == false)
                pDonatingTo.Enabled = true;
            int projectId = (Convert.ToInt16(pDonatingTo.SelectedValue));
            //paymentPeriod = paymentDate.Value.Date;
            //  donerId = (int)donerPaymentGridView1.SelectedRows[0].Cells[0].Value;
            if (getDonerPromise(donerId, projectId).Rows.Count > 0)
            {
                MessageBox.Show("Unable to Insert: You have entered this promise for the doner ");
                return;
            }
            this.insertDonerPromise();
            promiseGridView.DataSource = getAllDonerPromises(donerId);
            MessageBox.Show("Successfully Inserted");

        }

      
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (donersGridView.SelectedRows.Count < 1) return;
            tabControl1.SelectTab("donerPromise");
            //  dRw = dummyDataSet7.DonerQuick.FindByDonerID((int)donersGridView.SelectedRows[0].Cells[0].Value);
            donerId = (int)donersGridView.SelectedRows[0].Cells[0].Value;

            pDonatingTo.DataSource =  getAllProjects();//();//.Select("true", "Name").CopyToDataTable(); 
    
            pDonatingTo.DisplayMember = "name";
            pDonatingTo.ValueMember = "ProjectID";
            

            //if (dRw != null)
            //    pDonatingTo.SelectedValue = dRw.;
             promiseGridView.DataSource = getAllDonerPromises(donerId);
        }

        private void promiseUpdate_Click(object sender, EventArgs e)
        {
            int projectId = (Convert.ToInt16(pDonatingTo.SelectedValue));
            if (projectId < 1)
            {
                MessageBox.Show("Please select project");
                return;
            }

            if (donerId < 1)
            {
                MessageBox.Show("Please select Doner");
            }

            //if(pItemToDonate.SelectedIndex < 0 || promise.Text.Equals("") || pDonatingInterval.SelectedIndex < 0)
            //{
            //    MessageBox.Show("Please fill all the fields");
            //    return;
            //}

            //paymentPeriod = paymentDate.Value.Date;
            //  donerId = (int)donerPaymentGridView1.SelectedRows[0].Cells[0].Value;
            if (getDonerPromise(donerId, projectId).Rows.Count > 0)
            {
                this.updateDonerPromise(donerId, projectId);
                promiseGridView.DataSource = getAllDonerPromises(donerId);
                MessageBox.Show("Successfully Updated");

               
                if (pDonatingTo.Enabled == false)
                    pDonatingTo.Enabled = true;
                   ClearDonerPromiseFields();
            }
           
        }

        private void promiseGridView_DoubleClick(object sender, EventArgs e)
        {
          
        }

        private void promiseDelete_Click(object sender, EventArgs e)
        {
            DialogResult myResult;
            myResult = MessageBox.Show("Are you really deleting this promise?", "Delete Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (myResult == DialogResult.OK)
            {
                this.deleteDonerPromise(donerId, projectId);
                promiseGridView.DataSource = getAllDonerPromises(donerId);
                MessageBox.Show("Successfully Deleted");
                ClearDonerPromiseFields();
            }
            else
            {
                return;
            }

            return;

        }

        private void promiseGridView_Click(object sender, EventArgs e)
        {
          
           
        }

        private void PromisePaymentProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
         //   String pName = ((System.Data.DataRowView)PromisePaymentProjectName.Items[PromisePaymentProjectName.SelectedIndex]).DataView.Table.Rows[PromisePaymentProjectName.SelectedIndex].ItemArray[0].ToString();
          //  String pName = "St.Yared";//.ToString();

           
        }

        private void PromisePaymentProjectName_SelectedValueChanged(object sender, EventArgs e)
        {
            //String pName = PromisePaymentProjectName.Items[PromisePaymentProjectName.SelectedIndex].ToString();
            ////  String pName = "St.Yared";//.ToString();

            //this.getPromisePaymentByProjectBindingSource.DataSource = this.getPromisePaymentByProjectTableAdapter.GetData(pName);
            //this.PromisePaymentReport.RefreshReport();
        }

        private void PromisePaymentProjectName_Click(object sender, EventArgs e)
        {
            //String pName = PromisePaymentProjectName.Items[PromisePaymentProjectName.SelectedIndex].ToString();
            ////  String pName = "St.Yared";//.ToString();

            //this.getPromisePaymentByProjectBindingSource.DataSource = this.getPromisePaymentByProjectTableAdapter.GetData(pName);
            //this.PromisePaymentReport.RefreshReport();
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
             //   this.donerPromiseTableAdapter.FillBy(this.donerPromisDS.DonerPromise1);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillBy1ToolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
             //   this.donerPromiseTableAdapter.FillBy1(this.donerPromisDS.DonerPromise);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void promiseCancel_Click(object sender, EventArgs e)
        {
            ClearDonerPromiseFields();
        }

      
        private void button4_Click(object sender, EventArgs e)
        {
            //Excel.Application myExcelApp;
            //Excel.Workbooks myExcelWorkbooks;
            //Excel.Workbook myExcelWorkbook;
            //int row = 4;

            //myExcelApp = new Excel.Application();
            //myExcelApp.Visible = true;
            //myExcelWorkbooks = myExcelApp.Workbooks;
            //object misValue = System.Reflection.Missing.Value;
           
            //  string reportPath = System.Configuration.ConfigurationManager.AppSettings["ReportTemplatePath"];
            //    String fileName = reportPath;
            //    myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
            //    Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["MontlyPayment"];
            //  //  myExcelWorksheet.get_Range("F3", misValue).FormulaR1C1 = "Date:" + String.Format("{0:dd-MMM-yyyy}", dateTimePicker1.Value);
            //    myExcelWorksheet.get_Range("E1", misValue).FormulaR1C1 = ProjectPaMontCmb.SelectedValue.ToString();
            //    myExcelWorksheet.get_Range("E2", misValue).FormulaR1C1 = yearCombo.SelectedItem.ToString();
            //    myExcelWorksheet.get_Range("E1", misValue).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            //    myExcelWorksheet.get_Range("E2", misValue).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignJustify;

            //    ///report's data source
            //     Dictionary<int, business.MontlyPayment> payments = getPaymentDictionary();

            //     if (payments.Count > 0)
            //    {

            //        foreach (int r in payments.Keys)
            //        {
            //            business.MontlyPayment donerPay = payments[r];
            //            myExcelWorksheet.get_Range("A" + row.ToString(), misValue).FormulaR1C1 = donerPay.FullName;
            //            myExcelWorksheet.get_Range("B" + row.ToString(), misValue).FormulaR1C1 = donerPay.Address;
            //            myExcelWorksheet.get_Range("C" + row.ToString(), misValue).FormulaR1C1 = donerPay.PromisedAmount;
            //            myExcelWorksheet.get_Range("D" + row.ToString(), misValue).FormulaR1C1 = donerPay.DonatingInterval;
            //            myExcelWorksheet.get_Range("E" + row.ToString(), misValue).FormulaR1C1 = donerPay.TotalPrmisedAmount;

            //            if(donerPay.Monthly_payment_Dic.ContainsKey(9)){
            //                myExcelWorksheet.get_Range("F" + row.ToString(), misValue).FormulaR1C1 = donerPay.Monthly_payment_Dic[9];
            //            }
            //            if (donerPay.Monthly_payment_Dic.ContainsKey(10))
            //            {
            //                myExcelWorksheet.get_Range("G" + row.ToString(), misValue).FormulaR1C1 = donerPay.Monthly_payment_Dic[10];
            //            }
            //            if (donerPay.Monthly_payment_Dic.ContainsKey(11))
            //            {
            //                myExcelWorksheet.get_Range("H" + row.ToString(), misValue).FormulaR1C1 = donerPay.Monthly_payment_Dic[11];
            //            }
            //            if (donerPay.Monthly_payment_Dic.ContainsKey(12))
            //            {
            //                myExcelWorksheet.get_Range("I" + row.ToString(), misValue).FormulaR1C1 = donerPay.Monthly_payment_Dic[12];
            //            }
            //            if (donerPay.Monthly_payment_Dic.ContainsKey(1))
            //            {
            //                myExcelWorksheet.get_Range("J" + row.ToString(), misValue).FormulaR1C1 = donerPay.Monthly_payment_Dic[1];
            //            }
            //            if (donerPay.Monthly_payment_Dic.ContainsKey(2))
            //            {
            //                myExcelWorksheet.get_Range("K" + row.ToString(), misValue).FormulaR1C1 = donerPay.Monthly_payment_Dic[2];
            //            }
            //            if (donerPay.Monthly_payment_Dic.ContainsKey(3))
            //            {
            //                myExcelWorksheet.get_Range("L" + row.ToString(), misValue).FormulaR1C1 = donerPay.Monthly_payment_Dic[3];
            //            }
            //            if (donerPay.Monthly_payment_Dic.ContainsKey(4))
            //            {
            //                myExcelWorksheet.get_Range("M" + row.ToString(), misValue).FormulaR1C1 = donerPay.Monthly_payment_Dic[4];
            //            }
            //            if (donerPay.Monthly_payment_Dic.ContainsKey(5))
            //            {
            //                myExcelWorksheet.get_Range("N" + row.ToString(), misValue).FormulaR1C1 = donerPay.Monthly_payment_Dic[5];
            //            }
            //            if (donerPay.Monthly_payment_Dic.ContainsKey(6))
            //            {
            //                myExcelWorksheet.get_Range("O" + row.ToString(), misValue).FormulaR1C1 = donerPay.Monthly_payment_Dic[6];
            //            }
            //            if (donerPay.Monthly_payment_Dic.ContainsKey(7))
            //            {
            //                myExcelWorksheet.get_Range("P" + row.ToString(), misValue).FormulaR1C1 = donerPay.Monthly_payment_Dic[7];
            //            }
            //            if (donerPay.Monthly_payment_Dic.ContainsKey(8))
            //            {
            //                myExcelWorksheet.get_Range("Q" + row.ToString(), misValue).FormulaR1C1 = donerPay.Monthly_payment_Dic[8];
            //            }
                       
            //            row++;
            //        }
            //    }
        }

        private void PaymentTab_Click(object sender, EventArgs e)
        {

        }

        private void generateIndiReport_Click(object sender, EventArgs e)
        {

        }

        private void indPayMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void fillByToolStripButton_Click_1(object sender, EventArgs e)
        {
            try
            {
               // this.projectTableAdapter1.FillBy(this.projectDSMK.Project1);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillBy1ToolStripButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
             //   this.projectTableAdapter1.FillBy1(this.projectDSMK.Project);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        //donersGridView click event on the editor
        private void nn(object sender, DataGridViewCellEventArgs e)
        {
           /** if (donersGridView.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                int selectedRowIndex = donersGridView.SelectedCells[0].RowIndex;
                object item = donersGridView.Rows[e.RowIndex].Cells[0].Value;

                DataGridViewRow selectedRow = donersGridView.Rows[selectedRowIndex];

                donerId = Convert.ToInt32(selectedRow.Cells["DonerID"].Value);

                DataTable dtProjectDetail = _doner.GetDonerDetailByID(donerId);

                MessageBox.Show(projectId.ToString());

                AssigenValueToTextBox(dtProjectDetail);
            }
            * **/
        }

        private void projectDataGridView_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
        }

        private void projectDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //
                      
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

      


        }
    
}
