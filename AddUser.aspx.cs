using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserManagement
{
    public partial class AddUser : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * from [User] where Username=@username", con);
            cmd.Parameters.AddWithValue("@username", txtUser.Text);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                con.Close();
                Response.Write("<script>alert('Username already exist ');</script>");
                txtUser.Text = "";
                txtUser.Focus();
            }
            
            else
            {
                con.Close();
                FileUpload img = (FileUpload)FileUpload1;
                Byte[] imgByte = null;
                if (img.HasFile && img.PostedFile != null)
                {
                    //To create a PostedFile
                    HttpPostedFile File = FileUpload1.PostedFile;
                    //Create byte Array with file len
                    imgByte = new Byte[File.ContentLength];
                    //force the control to load data in array
                    File.InputStream.Read(imgByte, 0, File.ContentLength);
                }
                string query = "insert into [User] (Username, Password, Firstname, Lastname, DOB, Gender, Mobileno, Email, Userimage) values(@username, @password, @firstname, @lastname, @dob, @gender, @mobileno, @email, @userimage)";
                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandText = query;
                cmd1.Connection = con;                
                cmd1.Parameters.AddWithValue("@username", txtUser.Text);
                cmd1.Parameters.AddWithValue("@password", txtPass.Text);
                cmd1.Parameters.AddWithValue("@firstname", txtFname.Text);
                cmd1.Parameters.AddWithValue("@lastname", txtLname.Text);
                cmd1.Parameters.AddWithValue("@dob", txtDOB.Text);
                cmd1.Parameters.AddWithValue("@gender", txtGender.Text);
                cmd1.Parameters.AddWithValue("@mobileno", txtMobile.Text);
                cmd1.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd1.Parameters.AddWithValue("@userimage", imgByte);
                con.Open();
                cmd1.ExecuteNonQuery();
                Response.Write("<script>alert('User inserted');</script>");
                con.Close();
                Response.Redirect("Login.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}