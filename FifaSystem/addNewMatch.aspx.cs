using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Xml.Linq;

namespace FifaSystem
{
    public partial class addNewMatch : System.Web.UI.Page
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

        protected void addButton_Click(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["FifaSystem"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            String StartDate = startDate.Text.ToString();
            String StartTime = startTime.Text.ToString();
            
            String EndDate = endDate.Text.ToString();
            String EndTime = endTime.Text.ToString();
            
            String host = hostName.Text;
            String guest = guestName.Text;

            if (host != "" && guest != "" && StartDate != "" && StartTime != "" && EndDate != "" && EndTime != "")
            {

                String[] strList = StartDate.Split('-');
                String[] strList1 = EndDate.Split('-');

                String newStartDate = strList[0] + "/" + strList[1] + "/" + strList[2];
                String newEndDate = strList1[0] + "/" + strList1[1] + "/" + strList1[2];

                StartTime = StartTime + ":00";
                EndTime = EndTime + ":00";
                String start = newStartDate + " " + StartTime;
                String end = newEndDate + " " + EndTime;

                SqlCommand addNewMatch = new SqlCommand("addNewMatch", conn);
                addNewMatch.CommandType = CommandType.StoredProcedure;
                addNewMatch.Parameters.Add(new SqlParameter("@hostClubName", host));
                addNewMatch.Parameters.Add(new SqlParameter("@guestClubName", guest));
                addNewMatch.Parameters.Add(new SqlParameter("@matchTime", start));
                addNewMatch.Parameters.Add(new SqlParameter("@endTime", end));


                SqlParameter successMatch = addNewMatch.Parameters.Add("@success", SqlDbType.Bit);
                successMatch.Direction = ParameterDirection.Output;

                conn.Open();
                addNewMatch.ExecuteNonQuery();
                conn.Close();


                if (successMatch.Value.ToString() == "False")
                {
                    successLabel.Text = "Operation Unsuccessful!";
                }
                else
                {
                    successLabel.Text = "Success!";

                }
            }
        }

        protected void homeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("SportsAssociationManagerHome.aspx");
        }
    }
}