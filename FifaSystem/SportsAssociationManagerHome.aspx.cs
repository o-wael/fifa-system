using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FifaSystem
{
    public partial class SportsAssociationManagerHome : System.Web.UI.Page
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

                if (type.Value.ToString() != "1")
                {
                    Response.Redirect("login.aspx");
                }

            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }

        protected void addMatchButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("addNewMatch.aspx");
        }

        protected void deleteMatchButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("deleteMatch.aspx");
        }

        protected void viewMatchesButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewUpcomingMatches.aspx");
        }

        protected void clubsNeverMatchedButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("clubsNeverMatched.aspx");
        }

        protected void alreadyPlayedMatchesButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("alreadyPlayedMatches.aspx");
        }
    }
}