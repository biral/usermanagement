using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserManagement
{ 
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {            
            if(radioAdmin.Checked==true)
            {
                if(txtUser.Text == "Admin" && txtPass.Text == "root")
                {
                    Session["username"] = txtUser.Text;
                    Response.Redirect("Admin.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Username or password incorrect');</script>");
                    txtUser.Text = "";
                    txtPass.Text = "";
                    txtUser.Focus();
                }
            }
            else if (radioUser.Checked == true)
            {
                SqlCommand cmd = new SqlCommand(@"Select * from [User] where Username=@username and Password=@password", con);
                cmd.Parameters.AddWithValue("@username", txtUser.Text);
                cmd.Parameters.AddWithValue("@password", txtPass.Text);
                con.Open();
                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                DataSet dsa = new DataSet();
                ada.Fill(dsa);
                if (dsa.Tables[0].Rows.Count == 1)
                {
                    Session["username"] = txtUser.Text;
                    Response.Redirect("User.aspx?Username="+txtUser.Text);
                }
                else
                {
                    Response.Write("<script>alert('Username or password incorrect');</script>");
                    txtUser.Text = "";
                    txtPass.Text = "";
                    txtUser.Focus();
                }
            }
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddUser.aspx");
        }

        protected void radioUser_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}