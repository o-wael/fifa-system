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
    public partial class registerSportsAssociationManager : System.Web.UI.Page
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

            if (uname != "" && pass != "" && Name != "")
            {

                SqlCommand registerSportsAssociationManager = new SqlCommand("addAssociationManager", conn);
                registerSportsAssociationManager.CommandType = CommandType.StoredProcedure;

                registerSportsAssociationManager.Parameters.Add(new SqlParameter("@assocManagerName", Name));
                registerSportsAssociationManager.Parameters.Add(new SqlParameter("@assocManagerUsername", uname));
                registerSportsAssociationManager.Parameters.Add(new SqlParameter("@assocManagerPassword", pass));

                SqlParameter success = registerSportsAssociationManager.Parameters.Add("@success", SqlDbType.Bit);
                success.Direction = ParameterDirection.Output;

                conn.Open();
                registerSportsAssociationManager.ExecuteNonQuery();
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