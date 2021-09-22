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
    public partial class Admin : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (Session["username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            HttpCookie cookie = Request.Cookies["Admin"];
            if(cookie == null)
            {
                cookie = new HttpCookie("Admin");
            }
            else
            {
                lblcookie.Text = "Last Login: "+cookie["Last Login"];
            }
            cookie["Last Login"] = Convert.ToString(DateTime.Now);
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie);
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {            
            Response.Redirect("AddUser.aspx");
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            txtUser.Visible = true;
            btnSearch.Visible = true;
            Label2.Visible = true;
            btnUpdate1.Visible = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand(@"Select * from [User] where Username=@username", con);
            cmd.Parameters.AddWithValue("@username", txtUser.Text);
            con.Open();
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet dsa = new DataSet();
            ada.Fill(dsa);
            if (dsa.Tables[0].Rows.Count == 1)
            {
                lblDOB.Visible = true;
                lblEmail.Visible = true;
                lblFname.Visible = true;
                lblGen.Visible = true;
                lblLname.Visible = true;
                lblMobile.Visible = true;
                lblPass.Visible = true;
                txtDOB.Visible = true;
                txtEmail.Visible = true;
                txtFname.Visible = true;
                txtGender.Visible = true;
                txtLname.Visible = true;
                txtMobile.Visible = true;
                txtPass.Visible = true;
                txtPass.Text = dsa.Tables[0].Rows[0][1].ToString();
                txtFname.Text = dsa.Tables[0].Rows[0][2].ToString();
                txtLname.Text = dsa.Tables[0].Rows[0][3].ToString();
                txtDOB.Text = dsa.Tables[0].Rows[0][4].ToString();
                txtGender.Text = dsa.Tables[0].Rows[0][5].ToString();
                txtMobile.Text = dsa.Tables[0].Rows[0][6].ToString();
                txtEmail.Text = dsa.Tables[0].Rows[0][7].ToString();
                con.Close();

            }
            else
            {
                Response.Write("<script>alert('No such user exist');</script>");
                txtUser.Text = "";
                txtUser.Focus();
            }
        }

        protected void btnDelete0_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand(@"Select * from [User] where Username=@username", con);
            cmd.Parameters.AddWithValue("@username", txtUser.Text);
            con.Open();
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet dsa = new DataSet();
            ada.Fill(dsa);
            if (dsa.Tables[0].Rows.Count == 1)
            {
                SqlCommand cmd1 = new SqlCommand(@"delete from [User] where Username=@username", con);
                cmd1.Parameters.AddWithValue("@username", txtUser.Text);
                int i = cmd1.ExecuteNonQuery();
                Response.Write("<script>alert('User Deleted');</script>");
                con.Close();
            }
            else
            {
                Response.Write("<script>alert('No such user exist);</script>");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lblDOB.Visible = false;
            lblEmail.Visible = false;
            lblFname.Visible = false;
            lblGen.Visible = false;
            lblLname.Visible = false;
            lblMobile.Visible = false;
            lblPass.Visible = false;
            txtDOB.Visible = false;
            txtEmail.Visible = false;
            txtFname.Visible = false;
            txtGender.Visible = false;
            txtLname.Visible = false;
            txtMobile.Visible = false;
            txtPass.Visible = false;
            txtUser.Visible = true;
            btnSearch.Visible = false;
            btnDelete0.Visible = true;
            Label2.Visible = true;
            btnUpdate1.Visible = false;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("Login.aspx");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lblDOB.Visible = false;
            lblEmail.Visible = false;
            lblFname.Visible = false;
            lblGen.Visible = false;
            lblLname.Visible = false;
            lblMobile.Visible = false;
            lblPass.Visible = false;
            txtDOB.Visible = false;
            txtEmail.Visible = false;
            txtFname.Visible = false;
            txtGender.Visible = false;
            txtLname.Visible = false;
            txtMobile.Visible = false;
            txtPass.Visible = false;
            txtUser.Visible = true;
            btnSearch.Visible = false;
            btnDelete0.Visible = false;
            Label2.Visible = true;
            btnUpdate1.Visible = true;
        }

        protected void btnUpdate1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand(@"Select * from [User] where Username=@username", con);
            cmd.Parameters.AddWithValue("@username", txtUser.Text);
            con.Open();
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet dsa = new DataSet();
            ada.Fill(dsa);
            if (dsa.Tables[0].Rows.Count == 1)
            {
                Response.Redirect("User.aspx?Username=" + txtUser.Text);
            }
            else
            {
                Response.Write("<script>alert('No such user exist');</script>");
                txtUser.Text = "";
                txtUser.Focus();
            }
        }
    }
}