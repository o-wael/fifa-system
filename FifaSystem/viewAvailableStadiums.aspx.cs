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
    public partial class viewAvailableStadiums : System.Web.UI.Page
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

        protected void viewStadiumsButton_Click(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["FifaSystem"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String StartDate = startDate.Text.ToString();
            String StartTime = startTime.Text.ToString();

            if (StartDate != "" && StartTime != "")
            {
                StartTime = StartTime + ":00";
                String[] strList = StartDate.Split('-');
                String newStartDate = strList[0] + "/" + strList[1] + "/" + strList[2];
                String start = newStartDate + " " + StartTime;

                SqlCommand viewAvailableStadiumsOn = conn.CreateCommand();
                viewAvailableStadiumsOn.CommandText = "Select * From dbo.viewAvailableStadiumsOn(@date)";
                viewAvailableStadiumsOn.Parameters.Add(new SqlParameter("@date", start));

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

                conn.Open();
                SqlDataReader rdr = viewAvailableStadiumsOn.ExecuteReader(CommandBehavior.CloseConnection);
                while (rdr.Read())
                {
                    String stadiumName = rdr.GetString(rdr.GetOrdinal("Stadium Name"));
                    String stadiumLocation = rdr.GetString(rdr.GetOrdinal("Stadium Location"));
                    int stadiumCapacity = rdr.GetInt32(rdr.GetOrdinal("Stadium Capacity"));

                    TableRow tempRow = new TableRow();

                    TableCell tempCell1 = new TableCell();
                    tempCell1.Text = stadiumName;
                    tempRow.Cells.Add(tempCell1);

                    TableCell tempCell2 = new TableCell();
                    tempCell2.Text = stadiumLocation;
                    tempRow.Cells.Add(tempCell2);

                    TableCell tempCell3 = new TableCell();
                    tempCell3.Text = stadiumCapacity + "";
                    tempRow.Cells.Add(tempCell3);

                    mainTable.Rows.Add(tempRow);


                }

                conn.Close();

                successLabel.Text = "";
            }
            else
            {
                successLabel.Text = "You need to enter the date and time!";
            }
        }

        protected void sendRequestButton_Click(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["FifaSystem"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            successLabel.Text = "";

            String StartDate = startDate.Text.ToString();
            String StartTime = startTime.Text.ToString();

            if (StartDate != "" && StartTime != "")
            {
                StartTime = StartTime + ":00";
                String[] strList = StartDate.Split('-');
                String newStartDate = strList[0] + "/" + strList[1] + "/" + strList[2];
                String start = newStartDate + " " + StartTime;

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

                SqlCommand matchExists = new SqlCommand("matchExists", conn);
                matchExists.CommandType = CommandType.StoredProcedure;
                matchExists.Parameters.Add(new SqlParameter("@clubRepUsername", Session["user"].ToString()));
                matchExists.Parameters.Add(new SqlParameter("@startTime", start));

                SqlParameter successMatch = matchExists.Parameters.Add("@success", SqlDbType.Bit);
                successMatch.Direction = ParameterDirection.Output;

                conn.Open();
                matchExists.ExecuteNonQuery();
                conn.Close();

                String stadiumName = stadiumTextBox.Text;

                if (successMatch.Value.ToString() == "True")
                {
                    SqlCommand hostRequestExists = new SqlCommand("hostRequestExists", conn);
                    hostRequestExists.CommandType = CommandType.StoredProcedure;
                    hostRequestExists.Parameters.Add(new SqlParameter("@clubName", clubName.Value.ToString()));
                    hostRequestExists.Parameters.Add(new SqlParameter("@stadiumName", stadiumName));
                    hostRequestExists.Parameters.Add(new SqlParameter("@datetime", start));

                    SqlParameter successRequest = hostRequestExists.Parameters.Add("@success", SqlDbType.Bit);
                    successRequest.Direction = ParameterDirection.Output;

                    conn.Open();
                    hostRequestExists.ExecuteNonQuery();
                    conn.Close();

                    if (successRequest.Value.ToString() == "True")
                    {
                        successLabel.Text = "Host Request Already Exists!";
                    }
                    else
                    {
                        SqlCommand addHostRequest = new SqlCommand("addHostRequest", conn);
                        addHostRequest.CommandType = CommandType.StoredProcedure;
                        addHostRequest.Parameters.Add(new SqlParameter("@clubName", clubName.Value.ToString()));
                        addHostRequest.Parameters.Add(new SqlParameter("@stadiumName", stadiumName));
                        addHostRequest.Parameters.Add(new SqlParameter("@datetime", start));

                        conn.Open();
                        addHostRequest.ExecuteNonQuery();
                        conn.Close();
                        successLabel.Text = "Success!";
                    }
                }
                else
                {
                    successLabel.Text = "Match Does Not Exist!";
                }
            }
            else
            {
                successLabel.Text = "You need to enter the date and time!";
            }
        }
    }
}