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
    public partial class viewClubInfo : System.Web.UI.Page
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

                if (type.Value.ToString() != "4")
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    SqlCommand getClubOfRepresentative = new SqlCommand("getClubOfRepresentative", conn);
                    getClubOfRepresentative.CommandType = CommandType.StoredProcedure;
                    getClubOfRepresentative.Parameters.Add(new SqlParameter("@RepresentativeUname", Session["user"]));

                    SqlParameter clubName = getClubOfRepresentative.Parameters.Add("@clubName", SqlDbType.VarChar, 20);
                    clubName.Direction = ParameterDirection.Output;

                    SqlParameter clubLocation = getClubOfRepresentative.Parameters.Add("@clubLocation", SqlDbType.VarChar, 20);
                    clubLocation.Direction = ParameterDirection.Output;

                    conn.Open();
                    getClubOfRepresentative.ExecuteNonQuery();
                    conn.Close();

                    clubNameL.Text = "Club Name: " + clubName.Value.ToString();
                    clubLocationL.Text = "Club Location: " + clubLocation.Value.ToString();
                }

            }
            else
            {
                Response.Redirect("login.aspx");
            }

            
        }

        protected void homeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClubRepresentativeHome.aspx");
        }
    }
}