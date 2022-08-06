using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace hms_new
{
    public partial class dashboard : Form
    {
        int index;

        public dashboard()
        {
            InitializeComponent();
        }
        private void addpatient_Click(object sender, EventArgs e)
        {
            addpanel.BackColor = System.Drawing.Color.Navy;
            diagnopanel.BackColor = System.Drawing.Color.White;
            appointpanel.BackColor = System.Drawing.Color.White;
            historypanel.BackColor = System.Drawing.Color.White;
            doctorpanel.BackColor = System.Drawing.Color.White;
            paneladd.Show();
            panelpdiagno.Hide();
            appointmentpanel.Hide();
            panelhistory.Hide();
            panel2add_d.Hide();
        }

        private void diagnosis_Click(object sender, EventArgs e)
        {
            addpanel.BackColor = System.Drawing.Color.White;
            diagnopanel.BackColor = System.Drawing.Color.Navy;
            appointpanel.BackColor = System.Drawing.Color.White;
            historypanel.BackColor = System.Drawing.Color.White;
            doctorpanel.BackColor = System.Drawing.Color.White;
            paneladd.Hide();
            panelpdiagno.Show();
            appointmentpanel.Hide();
            panelhistory.Hide();
            panel2add_d.Hide();
        }

        private void appointment_Click(object sender, EventArgs e)
        {
            addpanel.BackColor = System.Drawing.Color.White;
            diagnopanel.BackColor = System.Drawing.Color.White;
            appointpanel.BackColor = System.Drawing.Color.Navy;
            historypanel.BackColor = System.Drawing.Color.White;
            doctorpanel.BackColor = System.Drawing.Color.White;
            paneladd.Hide();
            panelpdiagno.Hide();
            appointmentpanel.Show();
            panelhistory.Hide();
            panel2add_d.Hide();

            refresh_Click(null,null);
        }

        private void history_Click(object sender, EventArgs e)
        {
            addpanel.BackColor = System.Drawing.Color.White;
            diagnopanel.BackColor = System.Drawing.Color.White;
            appointpanel.BackColor = System.Drawing.Color.White;
            historypanel.BackColor = System.Drawing.Color.Navy;
            doctorpanel.BackColor = System.Drawing.Color.White;
            paneladd.Hide();
            panelpdiagno.Hide();
            appointmentpanel.Hide();
            panelhistory.Show();
            panel2add_d.Hide();

            string mainconn = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            if (sqlconn.State == ConnectionState.Closed)
                sqlconn.Open();
            string sqlquery = "select * from AddPatient inner join PatientDiagno on AddPatient.Pid=PatientDiagno.pid";
            SqlDataAdapter sdr = new SqlDataAdapter(sqlquery, sqlconn);
            DataTable dt = new DataTable("[dbo].[AddPatient],[dbo].[PatientDiagno]");
            sdr.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void adddoctor_Click(object sender, EventArgs e)
        {
            addpanel.BackColor = System.Drawing.Color.White;
            diagnopanel.BackColor = System.Drawing.Color.White;
            appointpanel.BackColor = System.Drawing.Color.White;
            historypanel.BackColor = System.Drawing.Color.White;
            doctorpanel.BackColor = System.Drawing.Color.Navy;
            paneladd.Show();
            panelpdiagno.Hide();
            appointmentpanel.Hide();
            panelhistory.Hide();
            panel2add_d.Show();

            show_data();
        }

        public void show_data()
            {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-QBB83C20\\SQLEXPRESS;Initial Catalog=finalProject;Integrated Security=True");
            string query = @"SELECT [Name]
      ,[Address]
      ,[Phone]
      ,[Email]
      ,[Speciality]
  FROM [dbo].[AddDoctor]";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable("[dbo].[AddDoctor]");
            sda.Fill(dt);
            dataGridViewDoctor.DataSource = dt;
        }

        private void dashboard_Load(object sender, EventArgs e)
        {
            paneladd.Hide();
            panelpdiagno.Hide();
            appointmentpanel.Hide();
            panelhistory.Hide();
            panel2add_d.Hide();
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=LAPTOP-QBB83C20\\SQLEXPRESS;Initial Catalog=finalProject;Integrated Security=True");
                SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[AddPatient]
           ([Name]
           ,[Full_Address]
           ,[Contact]
           ,[Age]
           ,[Gender]
           ,[Blood_Group]
           ,[Major_Disease]
           ,[Pid])
     VALUES
           ('"+name.Text+"','"+address.Text+"','"+contact.Text+"','"+age.Text+"','"+combogender.Text+"','"+bloodgrp.Text+"','"+majord.Text+"','"+pid.Text+"')", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Data Saved!"); 
            }

            catch(Exception)
            {
                MessageBox.Show("Invalid data format or invalid pid!");
            }

            name.Clear();
            address.Clear();
            contact.Clear();
            age.Clear();
            bloodgrp.Clear();
            majord.Clear();
            pid.Clear();
            combogender.ResetText();
        }

        private void pdID_TextChanged(object sender, EventArgs e)
        {
            if (pdID.Text != "")
            {
                int pid = Convert.ToInt32(pdID.Text);

                string mainconn = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);
                if (sqlconn.State == ConnectionState.Closed)
                    sqlconn.Open();
                
                string sqlquery = "select * from [dbo].[AddPatient] where Pid='"+pid+"'";
                
                
                SqlDataAdapter sdr = new SqlDataAdapter(sqlquery, sqlconn);
                DataTable dt = new DataTable("[dbo].[AddPatient]");
                sdr.Fill(dt);
                dataGridView1.DataSource = dt;
            
            }
        }
            

        private void button1_save_Click_1(object sender, EventArgs e)
        {
                try
                {
                    int pid = Convert.ToInt32(pdID.Text);
                    string sympt = symp.Text;
                    string diag = textBox3_diagno.Text;
                    string medi = medicine.Text;
                    string ward = comboward.Text;
                    string type = combotype.Text;

                    SqlConnection con = new SqlConnection("Data Source=LAPTOP-QBB83C20\\SQLEXPRESS;Initial Catalog=finalProject;Integrated Security=True");
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO[dbo].[PatientDiagno]
           ([pid]
           ,[Symptoms]
           ,[Diagnosis]
           ,[Medicines]
           ,[Ward]
           ,[Ward_Type])
     VALUES
                 (" + pid + ",'" + sympt + "','" + diag + "','" + medi + "','" + ward + "','" + type + "')", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Data Saved!");
            }

            catch (Exception)
                {
                    MessageBox.Show("Any field is empty 'OR' Data is in wrong format");
                }

            pdID.Clear();
            symp.Clear();
            textBox3_diagno.Clear();
            medicine.Clear();
            comboward.ResetText();
            combotype.ResetText();
        }

        private void btnbook_Click(object sender, EventArgs e)
        {
            try
            {
                int Pid = Convert.ToInt32(bpid.Text);
                string name = bpname.Text;
                string age = bage.Text;
                string gender = bgender.Text;
                string dname = bdname.Text;
                string date = dateTimePicker1.Text;

                SqlConnection con = new SqlConnection("Data Source=LAPTOP-QBB83C20\\SQLEXPRESS;Initial Catalog=finalProject;Integrated Security=True");
                SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[appointment]
           ([Pid]
           ,[Patient_Name]
           ,[Age]
           ,[Gender]
           ,[Doctor_Name]
           ,[Date])
     VALUES
           (" + Pid + ",'" + name + "'," + age + ",'" + gender + "','" + dname + "','" + date + "')", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Appointment Booked Successfully!");

                refresh_Click(null, null);

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            bpid.Clear();
            bpname.Clear();
            bage.Clear();
            bgender.Clear();
            bdname.Clear();
            dateTimePicker1.ResetText();
        }

        private void dataGridViewAppoint_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(MessageBox.Show("Do you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                int id = Convert.ToInt32(dataGridViewAppoint.Rows[e.RowIndex].Cells["Pid"].FormattedValue.ToString());
                SqlConnection con = new SqlConnection("Data Source=LAPTOP-QBB83C20\\SQLEXPRESS;Initial Catalog=finalProject;Integrated Security=True");
                SqlCommand cmd = new SqlCommand(@"DELETE FROM [dbo].[appointment]
      WHERE Pid = '" + id + "'",con);
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record deleted successfully!");
                con.Close();

                refresh_Click(null,null);
             }
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            if (sqlconn.State == ConnectionState.Closed)
                sqlconn.Open();

            string sqlquery = "select * from appointment";

            SqlDataAdapter sdr = new SqlDataAdapter(sqlquery, sqlconn);
            DataTable dt = new DataTable("[dbo].[appointment]");
            sdr.Fill(dt);
            dataGridViewAppoint.DataSource = dt;
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string add = textBox2.Text;
            Int64 ph = Convert.ToInt64(textBox3.Text);
            string mail = textBox4.Text;
            string speciality = textBox5.Text;

            SqlConnection constring =new SqlConnection( "Data Source=LAPTOP-QBB83C20\\SQLEXPRESS;Initial Catalog=finalProject;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(@"INSERT INTO[dbo].[AddDoctor]
            ([Name]
                      ,[Address]
                      ,[Phone]
                      ,[Email]
                      ,[Speciality])
     VALUES
           ('"+name+"','"+add+"','"+ph+"','"+mail+"','"+speciality+"')",constring);
            constring.Open();
            cmd.ExecuteNonQuery();
            constring.Close();

            show_data();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

        }

        private void dataGridViewDoctor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            DataGridViewRow row = dataGridViewDoctor.Rows[index];
            textBox1.Text = row.Cells[0].Value.ToString();
            textBox2.Text = row.Cells[1].Value.ToString();
            textBox3.Text = row.Cells[2].Value.ToString();
            textBox4.Text = row.Cells[3].Value.ToString();
            textBox5.Text = row.Cells[4].Value.ToString();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            DataGridViewRow newdata = dataGridViewDoctor.Rows[index];
            newdata.Cells[0].Value = textBox1.Text;
            newdata.Cells[1].Value = textBox2.Text;
            newdata.Cells[2].Value = textBox3.Text;
            newdata.Cells[3].Value = textBox4.Text;
            newdata.Cells[4].Value = textBox5.Text;
            MessageBox.Show("Data Updated Successfully!!!");
        }

        private void btn_dlt_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                index = dataGridViewDoctor.CurrentCell.RowIndex;
                dataGridViewDoctor.Rows.RemoveAt(index);
                MessageBox.Show("Data Deleted!!");
                    }
        }

        private void logout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
