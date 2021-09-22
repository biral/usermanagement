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
    public partial class User : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["username"]==null)
            {
                Response.Redirect("Login.aspx");
            }
            
            if(!Page.IsPostBack)
            {
                string Username = Request.QueryString["Username"];
                Label1.Text = "WELCOME " + Username;
                SqlCommand cmd = new SqlCommand(@"Select * from [User] where Username=@username", con);
                cmd.Parameters.AddWithValue("@username", Username);
                con.Open();
                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                DataSet dsa = new DataSet();
                ada.Fill(dsa);
                txtUser.Text = dsa.Tables[0].Rows[0][0].ToString();
                txtUser.ReadOnly = true;
                txtPass.Text = dsa.Tables[0].Rows[0][1].ToString();
                txtFname.Text = dsa.Tables[0].Rows[0][2].ToString();
                txtLname.Text = dsa.Tables[0].Rows[0][3].ToString();
                txtDOB.Text = dsa.Tables[0].Rows[0][4].ToString();
                txtGender.Text = dsa.Tables[0].Rows[0][5].ToString();
                txtMobile.Text = dsa.Tables[0].Rows[0][6].ToString();
                txtEmail.Text = dsa.Tables[0].Rows[0][7].ToString();
                byte[] bytes = (byte[])dsa.Tables[0].Rows[0]["Userimage"];
                string basestring = Convert.ToBase64String(bytes, 0, bytes.Length);
                Image1.ImageUrl = "data:image/png;base64,"+basestring;
                con.Close();
            }            
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            FileUpload1.Visible = true;
            txtPass.ReadOnly = false;
            txtPass.TextMode = TextBoxMode.Password;
            ValidatorPass.Enabled = true;
            txtFname.ReadOnly = false;
            txtLname.ReadOnly = false;
            txtDOB.ReadOnly = false;
            txtDOB.TextMode = TextBoxMode.Date;
            txtGender.ReadOnly = false;
            txtMobile.ReadOnly = false;        
            txtEmail.ReadOnly = false;
            ValidatorEmail.Enabled = true;
            btnUpdate1.Visible = true;
            btnUpdate.Visible = false;
        }

        protected void btnUpdate1_Click(object sender, EventArgs e)
        {
            if(FileUpload1.FileName == null)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "update [User] set Password=@password, Firstname=@firstname, Lastname=@lastname, DOB=@dob, Gender=@gender, Mobileno=@mobileno, Email=@email Where Username = @username";
                cmd.Parameters.AddWithValue("@username", txtUser.Text);
                cmd.Parameters.AddWithValue("@password", txtPass.Text);
                cmd.Parameters.AddWithValue("@firstname", txtFname.Text);
                cmd.Parameters.AddWithValue("@lastname", txtLname.Text);
                cmd.Parameters.AddWithValue("@dob", txtDOB.Text);
                cmd.Parameters.AddWithValue("@gender", txtGender.Text);
                cmd.Parameters.AddWithValue("@mobileno", txtMobile.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Details Updated');</script>");
                txtPass.TextMode = TextBoxMode.SingleLine;
                txtDOB.TextMode = TextBoxMode.SingleLine;
            }
            else
            {
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
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "update [User] set Password=@password, Firstname=@firstname, Lastname=@lastname, DOB=@dob, Gender=@gender, Mobileno=@mobileno, Email=@email, Userimage=@userimage Where Username = @username";
                cmd.Parameters.AddWithValue("@username", txtUser.Text);
                cmd.Parameters.AddWithValue("@password", txtPass.Text);
                cmd.Parameters.AddWithValue("@firstname", txtFname.Text);
                cmd.Parameters.AddWithValue("@lastname", txtLname.Text);
                cmd.Parameters.AddWithValue("@dob", txtDOB.Text);
                cmd.Parameters.AddWithValue("@gender", txtGender.Text);
                cmd.Parameters.AddWithValue("@mobileno", txtMobile.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@userimage", imgByte);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Details Updated');</script>");
                txtPass.TextMode = TextBoxMode.SingleLine;
                txtDOB.TextMode = TextBoxMode.SingleLine;
            }
            
            
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("Login.aspx");
        }
    }
}