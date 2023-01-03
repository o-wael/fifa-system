using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace FifaSystem
{
    public partial class viewRequests : System.Web.UI.Page
    {
        protected void doJobAccept(object sender, EventArgs e, String managerUsername, String hostName, String guestName, DateTime startTime)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["FifaSystem"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand acceptRequest = new SqlCommand("acceptRequest", conn);
            acceptRequest.CommandType = CommandType.StoredProcedure;
            acceptRequest.Parameters.Add(new SqlParameter("@inp_stadium_manager_username", managerUsername));
            acceptRequest.Parameters.Add(new SqlParameter("@inp_hosting_club_name", hostName));
            acceptRequest.Parameters.Add(new SqlParameter("@inp_competing_club_name", guestName));
            acceptRequest.Parameters.Add(new SqlParameter("@inp_start_time_of_match", startTime));

            conn.Open();
            acceptRequest.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("viewRequests.aspx");
        }

        protected void doJobReject(object sender, EventArgs e, String managerUsername, String hostName, String guestName, DateTime startTime)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["FifaSystem"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand rejectRequest = new SqlCommand("rejectRequest", conn);
            rejectRequest.CommandType = CommandType.StoredProcedure;
            rejectRequest.Parameters.Add(new SqlParameter("@inp_stadium_manager_username", managerUsername));
            rejectRequest.Parameters.Add(new SqlParameter("@inp_hosting_club_name", hostName));
            rejectRequest.Parameters.Add(new SqlParameter("@inp_competing_club_name", guestName));
            rejectRequest.Parameters.Add(new SqlParameter("@inp_start_time_of_match", startTime));

            conn.Open();
            rejectRequest.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("viewRequests.aspx");
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

                if (type.Value.ToString() != "3")
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    SqlCommand getRequestOfManager = conn.CreateCommand();
                    getRequestOfManager.CommandText = "Select * From dbo.viewRequestsOfManager(@managerUsername)";
                    getRequestOfManager.Parameters.Add(new SqlParameter("@managerUsername", Session["user"]));

                    conn.Open();
                    SqlDataReader rdr = getRequestOfManager.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        String repName = rdr.GetString(rdr.GetOrdinal("Representative Name"));
                        String hostName = rdr.GetString(rdr.GetOrdinal("Host Name"));
                        String guestName = rdr.GetString(rdr.GetOrdinal("Guest Name"));
                        DateTime startTime = rdr.GetDateTime(rdr.GetOrdinal("Start Time"));
                        DateTime endTime = rdr.GetDateTime(rdr.GetOrdinal("End Time"));
                        String requestStatus = rdr.GetString(rdr.GetOrdinal("Request Status"));

                        TableRow tempRow = new TableRow();
                        tempRow.CssClass = "tableRowC";

                        TableCell tempCell1 = new TableCell();
                        tempCell1.Text = repName;
                        tempRow.Cells.Add(tempCell1);

                        TableCell tempCell2 = new TableCell();
                        tempCell2.Text = hostName;
                        tempRow.Cells.Add(tempCell2);

                        TableCell tempCell3 = new TableCell();
                        tempCell3.Text = guestName;
                        tempRow.Cells.Add(tempCell3);

                        TableCell tempCell4 = new TableCell();
                        tempCell4.Text = startTime.ToString();
                        tempRow.Cells.Add(tempCell4);

                        TableCell tempCell5 = new TableCell();
                        tempCell5.Text = endTime.ToString();
                        tempRow.Cells.Add(tempCell5);

                        if (requestStatus == "Unhandled")
                        {
                            TableCell tempCell6 = new TableCell();
                            Button acceptB = new Button();
                            acceptB.Text = "Accept Request";
                            acceptB.CssClass = "buttonTable";
                            acceptB.Click += (sender2, e2) => doJobAccept(sender2, e2, Session["user"].ToString(), hostName, guestName, startTime);
                            tempRow.Cells.Add(tempCell6);
                    
                            TableCell tempCell7 = new TableCell();
                            Button rejectB = new Button();
                            rejectB.Text = "Reject Request";
                            rejectB.CssClass = "buttonTable";
                            rejectB.Click += (sender2, e2) => doJobReject(sender2, e2, Session["user"].ToString(), hostName, guestName, startTime);
                            tempRow.Cells.Add(tempCell7);

                            mainTable.Rows.Add(tempRow);
                            tempCell6.Controls.Add(acceptB);
                            tempCell7.Controls.Add(rejectB);
                        }
                        else if (requestStatus == "Accepted")
                        {
                            TableCell tempCell6 = new TableCell();
                            tempCell6.Text = "Accepted Request";
                            tempRow.Cells.Add(tempCell6);

                            TableCell tempCell7 = new TableCell();
                            tempCell7.Text = "";
                            tempRow.Cells.Add(tempCell7);

                            mainTable.Rows.Add(tempRow);
                        }
                        else
                        {
                            TableCell tempCell6 = new TableCell();
                            tempCell6.Text = "";
                            tempRow.Cells.Add(tempCell6);

                            TableCell tempCell7 = new TableCell();
                            tempCell7.Text = "Rejected Request";
                            tempRow.Cells.Add(tempCell7);

                            mainTable.Rows.Add(tempRow);
                        }


                    }

                    conn.Close();
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