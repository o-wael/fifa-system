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
    public partial class registerClubRepresentative : System.Web.UI.Page
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
            String club = clubName.Text;

            if (uname != "" && pass != "" && Name != "" && club != "")
            {
                SqlCommand checkIfClubExists = new SqlCommand("clubExists", conn);
                checkIfClubExists.CommandType = CommandType.StoredProcedure;
                checkIfClubExists.Parameters.Add(new SqlParameter("@clubName", club));

                SqlParameter successClub = checkIfClubExists.Parameters.Add("@success", SqlDbType.Bit);
                successClub.Direction = ParameterDirection.Output;

                conn.Open();
                checkIfClubExists.ExecuteNonQuery();
                conn.Close();

                if (successClub.Value.ToString() == "False")
                {
                    successLabel.Text = "Registration Unsuccessful!";
                }
                else
                {
                    SqlCommand registerClubRepresentative = new SqlCommand("addRepresentative", conn);
                    registerClubRepresentative.CommandType = CommandType.StoredProcedure;

                    registerClubRepresentative.Parameters.Add(new SqlParameter("@representativeName", Name));
                    registerClubRepresentative.Parameters.Add(new SqlParameter("@representativeClubname", club));
                    registerClubRepresentative.Parameters.Add(new SqlParameter("@representativeUsername", uname));
                    registerClubRepresentative.Parameters.Add(new SqlParameter("@representativePassword", pass));

                    SqlParameter success = registerClubRepresentative.Parameters.Add("@success", SqlDbType.Bit);
                    success.Direction = ParameterDirection.Output;

                    conn.Open();
                    registerClubRepresentative.ExecuteNonQuery();
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