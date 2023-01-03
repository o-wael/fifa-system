using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FifaSystem
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["FifaSystem"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            String uname = username.Text;
            String pass = password.Text;

            SqlCommand checkCredentials = new SqlCommand("checkCredentials", conn);
            checkCredentials.CommandType = CommandType.StoredProcedure;
            checkCredentials.Parameters.Add(new SqlParameter("@username", uname));
            checkCredentials.Parameters.Add(new SqlParameter("@password", pass));

            SqlParameter success = checkCredentials.Parameters.Add("@success", SqlDbType.Bit);
            success.Direction = ParameterDirection.Output;

            conn.Open();
            checkCredentials.ExecuteNonQuery();
            conn.Close();

            if (success.Value.ToString() == "False")
            {
                successLabel.Text = "Login Unsuccessful!";
            }
            else
            {
                SqlCommand getType = new SqlCommand("typeOfUser", conn);
                getType.CommandType = CommandType.StoredProcedure;
                getType.Parameters.Add(new SqlParameter("@username", uname));

                SqlParameter type = getType.Parameters.Add("@type", SqlDbType.Int);
                type.Direction = ParameterDirection.Output;

                conn.Open();
                getType.ExecuteNonQuery();
                conn.Close();

                switch (type.Value.ToString())
                {
                    case "0": Session["user"] = uname; Response.Redirect("SystemAdminHome.aspx"); break;
                    case "1": Session["user"] = uname; Response.Redirect("SportsAssociationManagerHome.aspx"); break;
                    case "2": Session["user"] = uname; Response.Redirect("FanHome.aspx"); break;
                    case "3": Session["user"] = uname; Response.Redirect("StadiumManagerHome.aspx"); break;
                    case "4": Session["user"] = uname; Response.Redirect("ClubRepresentativeHome.aspx"); break;
                    default: successLabel.Text = "Login Unsuccessful!"; Session["user"] = uname; break;
                }
            }
        }
        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("register.aspx");
        }
    }
}