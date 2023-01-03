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
    public partial class addStadium : System.Web.UI.Page
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
            String name = stadiumName.Text;
            String location = stadiumLocation.Text;
            String capacity = stadiumCapacity.Text;

            if (name != "" && location != "" && capacity != "")
            {
                SqlCommand checkIfStadiumExists = new SqlCommand("stadiumExists", conn);
                checkIfStadiumExists.CommandType = CommandType.StoredProcedure;
                checkIfStadiumExists.Parameters.Add(new SqlParameter("@stadiumName", name));

                SqlParameter successStadium = checkIfStadiumExists.Parameters.Add("@success", SqlDbType.Bit);
                successStadium.Direction = ParameterDirection.Output;

                conn.Open();
                checkIfStadiumExists.ExecuteNonQuery();
                conn.Close();

                if (successStadium.Value.ToString() == "True")
                {
                    successLabel.Text = "Stadium Already Exists!";
                }
                else
                {
                    SqlCommand addStadium = new SqlCommand("addStadium", conn);
                    addStadium.CommandType = CommandType.StoredProcedure;

                    addStadium.Parameters.Add(new SqlParameter("@stadiumName", name));
                    addStadium.Parameters.Add(new SqlParameter("@stadiumLocation", location));
                    addStadium.Parameters.Add(new SqlParameter("@stadiumCapacity", capacity));

                    conn.Open();
                    addStadium.ExecuteNonQuery();
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