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

namespace DBConnection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conn = @"Data Source=localhost;Initial Catalog=master;Integrated Security=True";//User ID=sa;Password=admin@123";
            SqlConnection sqlConnection = new SqlConnection(conn);
            sqlConnection.Open();
            string fname= textBox1.Text;
            string Lname= textBox2.Text;
            string query = "Insert Into DBConn(fname,Lname) Values('"+fname+"','"+Lname+"')";
            SqlCommand cmd = new SqlCommand(query,sqlConnection);
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Database as Saved");
     

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var conn = @"Data Source=localhost;Initial Catalog=master;Integrated Security=True";//User ID=sa;Password=admin@123";
            //SqlConnection sqlConnection = new SqlConnection(conn);
            //sqlConnection.Open();
            
            //SqlCommand cmd = new SqlCommand(query, sqlConnection);
            var con = new SqlConnection(conn);
            con.Open();

            var cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = @"CREATE TABLE BankEnt (
                            Accountid int IDENTITY(101, 1) PRIMARY KEY,
                            LastName varchar(255) NOT NULL,
                            FirstName varchar(255),
                            Age int,
                            Amount int,
                            Gender varchar(255))";
            cmd.ExecuteNonQuery();
            //cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Database Created");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string conn = @"Data Source=localhost;Initial Catalog=master;Integrated Security=True";//User ID=sa;Password=admin@123";
            SqlConnection sqlConnection = new SqlConnection(conn);
            sqlConnection.Open();
            string fname = textBox1.Text;
            string Lname = textBox2.Text;
            int Age = Convert.ToInt32(textBox3.Text);
            int Amount = Convert.ToInt32(textBox4.Text);
            string Gender = "NA";
            if (radioButton1.Checked)
            {
                Gender = "Male";
            }
            else
            {
                Gender = "Female";
            }
            string query = "Insert Into BankEnt(FirstName,LastName,Age,Amount,Gender) Values('" + fname + "','" + Lname + "','" + Age + "','" + Amount + "','" + Gender + "')";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Inserted Value in Database");

        }
    }
}
