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
    public partial class registerStadiumManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void registerButton_Click(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["FifaSystem"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            String uname = username.Text;
            String pass = password.Text;
            String Name = name.Text;
            String stadium = stadiumName.Text;

            if (uname != "" && pass != "" && Name != "" && stadium != "")
            {

                SqlCommand checkIfStadiumExists = new SqlCommand("stadiumExists", conn);
                checkIfStadiumExists.CommandType = CommandType.StoredProcedure;
                checkIfStadiumExists.Parameters.Add(new SqlParameter("@stadiumName", stadium));

                SqlParameter successStadium = checkIfStadiumExists.Parameters.Add("@success", SqlDbType.Bit);
                successStadium.Direction = ParameterDirection.Output;

                conn.Open();
                checkIfStadiumExists.ExecuteNonQuery();
                conn.Close();

                if (successStadium.Value.ToString() == "False")
                {
                    successLabel.Text = "Registration Unsuccessful!";
                }
                else
                {
                    SqlCommand registerStadiumManager = new SqlCommand("addStadiumManager", conn);
                    registerStadiumManager.CommandType = CommandType.StoredProcedure;

                    registerStadiumManager.Parameters.Add(new SqlParameter("@managerName", Name));
                    registerStadiumManager.Parameters.Add(new SqlParameter("@stadiumName", stadium));
                    registerStadiumManager.Parameters.Add(new SqlParameter("@managerUsername", uname));
                    registerStadiumManager.Parameters.Add(new SqlParameter("@managerPassword", pass));

                    SqlParameter success = registerStadiumManager.Parameters.Add("@success", SqlDbType.Bit);
                    success.Direction = ParameterDirection.Output;

                    conn.Open();
                    registerStadiumManager.ExecuteNonQuery();
                    conn.Close();

                    if (success.Value.ToString() == "False")
                    {
                        successLabel.Text = "Registration Unsuccessful!";
                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }
                }
            }
        }
    }
}