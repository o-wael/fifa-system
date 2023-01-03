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
    public partial class viewStadiumInfo : System.Web.UI.Page
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
                else
                {
                    SqlCommand getStadiumOfManager = new SqlCommand("getStadiumOfManager", conn);
                    getStadiumOfManager.CommandType = CommandType.StoredProcedure;
                    getStadiumOfManager.Parameters.Add(new SqlParameter("@ManagerUname", Session["user"]));

                    SqlParameter stadiumName = getStadiumOfManager.Parameters.Add("@stadiumName", SqlDbType.VarChar, 20);
                    stadiumName.Direction = ParameterDirection.Output;

                    SqlParameter stadiumLocation = getStadiumOfManager.Parameters.Add("@stadiumLocation", SqlDbType.VarChar, 20);
                    stadiumLocation.Direction = ParameterDirection.Output;

                    SqlParameter stadiumCapacity = getStadiumOfManager.Parameters.Add("@stadiumCapacity", SqlDbType.Int);
                    stadiumCapacity.Direction = ParameterDirection.Output;

                    conn.Open();
                    getStadiumOfManager.ExecuteNonQuery();
                    conn.Close();

                    stadiumNameL.Text = "Stadium Name: " + stadiumName.Value.ToString();
                    stadiumLocationL.Text = "Stadium Location: " + stadiumLocation.Value.ToString();
                    stadiumCapacityL.Text = "Stadium Capacity: " + stadiumCapacity.Value.ToString();
                }

            }
            else
            {
                Response.Redirect("login.aspx");
            }

            
        }

        protected void homeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("StadiumManagerHome.aspx");
        }
    }
}