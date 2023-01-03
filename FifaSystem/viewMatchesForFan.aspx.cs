using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices.ComTypes;

namespace FifaSystem
{
    public partial class viewMatchesForFan : System.Web.UI.Page
    {
        protected String getFanNationalID()
        {
            String connStr = WebConfigurationManager.ConnectionStrings["FifaSystem"].ToString();
            SqlConnection conn = new SqlConnection(connStr);


            SqlCommand getFanNationalID = new SqlCommand("getFanNationalID", conn);
            getFanNationalID.CommandType = CommandType.StoredProcedure;
            getFanNationalID.Parameters.Add(new SqlParameter("@fanUsername", Session["user"]));

            SqlParameter fanNationalID = getFanNationalID.Parameters.Add("@fanNationalID", SqlDbType.VarChar, 20);
            fanNationalID.Direction = ParameterDirection.Output;

            conn.Open();
            getFanNationalID.ExecuteNonQuery();
            conn.Close();

            return fanNationalID.Value.ToString();
        }

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

                if (type.Value.ToString() != "2")
                {
                    Response.Redirect("login.aspx");
                }

            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }

        protected void homeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FanHome.aspx");
        }

        protected void viewButton_Click(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["FifaSystem"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String inpDate = dateT.Text.ToString();
            String inpTime = timeT.Text.ToString();

            if (inpDate != "" && inpTime != "")
            {
                String[] strList = inpDate.Split('-');
                String newInpDate = strList[0] + "/" + strList[1] + "/" + strList[2];

                inpTime = inpTime + ":00";
                String inpDateTime = newInpDate + " " + inpTime;

                SqlCommand availableMatchesToAttend = conn.CreateCommand();
                availableMatchesToAttend.CommandText = "Select * From dbo.availableMatchesToAttend(@start_time)";
                availableMatchesToAttend.Parameters.Add(new SqlParameter("@start_time", inpDateTime));


                conn.Open();
                SqlDataReader rdr = availableMatchesToAttend.ExecuteReader();
                while (rdr.Read())
                {
                    String hostClubName = rdr.GetString(rdr.GetOrdinal("Host Club Name"));
                    String guestClubName = rdr.GetString(rdr.GetOrdinal("Guest Club Name"));
                    DateTime matchTime = rdr.GetDateTime(rdr.GetOrdinal("Match Start Time"));
                    String stadiumName = rdr.GetString(rdr.GetOrdinal("Stadium Name"));
                    String stadiumLocation = rdr.GetString(rdr.GetOrdinal("Stadium Location"));


                    TableRow tempRow = new TableRow();
                    tempRow.CssClass = "tableRowC";

                    TableCell tempCell1 = new TableCell();
                    tempCell1.Text = hostClubName;
                    tempRow.Cells.Add(tempCell1);

                    TableCell tempCell2 = new TableCell();
                    tempCell2.Text = guestClubName;
                    tempRow.Cells.Add(tempCell2);

                    TableCell tempCell3 = new TableCell();
                    tempCell3.Text = stadiumName;
                    tempRow.Cells.Add(tempCell3);

                    TableCell tempCell4 = new TableCell();
                    tempCell4.Text = stadiumLocation;
                    tempRow.Cells.Add(tempCell4);

                    TableCell tempCell5 = new TableCell();
                    tempCell5.Text = matchTime.ToString();
                    tempRow.Cells.Add(tempCell5);

                    mainTable.Rows.Add(tempRow);

                }

                conn.Close();

            }
        }

        protected void purchaseButton_Click(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["FifaSystem"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String fanNationalID = getFanNationalID();
            String hostClubName = hostTextBox.Text;
            String guestClubName = guestTextBox.Text;

            if (hostClubName != "" && guestClubName != "")
            {
                SqlCommand getMatchTime = new SqlCommand("getMatchTime", conn);
                getMatchTime.CommandType = CommandType.StoredProcedure;
                getMatchTime.Parameters.Add(new SqlParameter("@hostName", hostClubName));
                getMatchTime.Parameters.Add(new SqlParameter("@guestName", guestClubName));
                SqlParameter matchTime = getMatchTime.Parameters.Add("@startTime", SqlDbType.DateTime);
                matchTime.Direction = ParameterDirection.Output;

                conn.Open();
                getMatchTime.ExecuteNonQuery();
                conn.Close();

                SqlCommand purchaseTicket = new SqlCommand("purchaseTicket", conn);
                purchaseTicket.CommandType = CommandType.StoredProcedure;
                purchaseTicket.Parameters.Add(new SqlParameter("@inp_national_id", fanNationalID));
                purchaseTicket.Parameters.Add(new SqlParameter("@inp_host_club", hostClubName));
                purchaseTicket.Parameters.Add(new SqlParameter("@inp_competing_club", guestClubName));
                purchaseTicket.Parameters.Add(new SqlParameter("@inp_match_date", matchTime.Value.ToString()));

                SqlParameter success = purchaseTicket.Parameters.Add("@success", SqlDbType.Bit);
                success.Direction = ParameterDirection.Output;

                conn.Open();
                purchaseTicket.ExecuteNonQuery();
                conn.Close();

                if (success.Value.ToString() == "False")
                {
                    successLabel.Text = "Purchase Unsuccessful!";
                }
                else
                {
                    successLabel.Text = "Purchase Successful!";
                }
            }
            
        }
    }
}