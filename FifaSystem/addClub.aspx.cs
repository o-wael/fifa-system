using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace FifaSystem
{
    public partial class addClub : System.Web.UI.Page
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

        protected void addButton_Click(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["FifaSystem"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            String name = clubName.Text;
            String location = clubLocation.Text;

            if (name != "" && location != "")
            {

                SqlCommand checkIfClubExists = new SqlCommand("clubExists", conn);
                checkIfClubExists.CommandType = CommandType.StoredProcedure;
                checkIfClubExists.Parameters.Add(new SqlParameter("@clubName", name));

                SqlParameter successClub = checkIfClubExists.Parameters.Add("@success", SqlDbType.Bit);
                successClub.Direction = ParameterDirection.Output;

                conn.Open();
                checkIfClubExists.ExecuteNonQuery();
                conn.Close();

                if (successClub.Value.ToString() == "True")
                {
                    successLabel.Text = "Club Already Exists!";
                }
                else
                {
                    SqlCommand addClub = new SqlCommand("addClub", conn);
                    addClub.CommandType = CommandType.StoredProcedure;

                    addClub.Parameters.Add(new SqlParameter("@clubName", name));
                    addClub.Parameters.Add(new SqlParameter("@clubLocation", location));

                    conn.Open();
                    addClub.ExecuteNonQuery();
                    conn.Close();

                    successLabel.Text = "Success!";

                }
            }
        }

        protected void homeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("SystemAdminHome.aspx");
        }
    }
}