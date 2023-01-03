using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FifaSystem
{
    public partial class SystemAdminHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["FifaSystem"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            if (Session["user"] != null)
            {
                SqlCommand getType = new SqlCommand("typeOfUser", conn);
                getType.CommandType = CommandType.StoredProcedure;
                getType.Parameters.Add(new SqlParameter("@username", Session["user"]));

                SqlParameter type = getType.Parameters.Add("@type", SqlDbType.Int);
                type.Direction = ParameterDirection.Output;

                conn.Open();
                getType.ExecuteNonQuery();
                conn.Close();

                if (type.Value.ToString() != "0")
                {
                    Response.Redirect("login.aspx");
                }

            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }

        protected void addClubButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("addClub.aspx");
        }

        protected void deleteClubButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("deleteClub.aspx");
        }

        protected void addStadiumButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("addStadium.aspx");
        }

        protected void deleteStadiumButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("deleteStadium.aspx");
        }

        protected void blockFanButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("blockFan.aspx");
        }
    }
}