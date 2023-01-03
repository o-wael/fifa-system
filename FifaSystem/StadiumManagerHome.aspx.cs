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
    public partial class StadiumManagerHome : System.Web.UI.Page
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

                if (type.Value.ToString() != "3")
                {
                    Response.Redirect("login.aspx");
                }

            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }

        protected void viewStadiumInfoButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewStadiumInfo.aspx");
        }

        protected void viewRequestsButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewRequests.aspx");
        }
    }
}