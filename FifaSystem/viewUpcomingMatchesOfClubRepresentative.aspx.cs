using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace FifaSystem
{
    public partial class viewUpcomingMatchesOfClubRepresentative : System.Web.UI.Page
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
                else
                {
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

                    SqlCommand upcomingMatchesOfClub = conn.CreateCommand();
                    upcomingMatchesOfClub.CommandText = "Select * From dbo.upcomingMatchesOfClub(@inp_club_name)";
                    String clubNameStr = clubName.Value.ToString();
                    upcomingMatchesOfClub.Parameters.Add(new SqlParameter("@inp_club_name", clubNameStr));



                    conn.Open();
                    SqlDataReader rdr = upcomingMatchesOfClub.ExecuteReader();
                    while (rdr.Read())
                    {
                        String hostClubName = rdr.GetString(rdr.GetOrdinal("Given Club Name"));
                        String guestClubName = rdr.GetString(rdr.GetOrdinal("The Competing Club Name"));
                        DateTime startTime = rdr.GetDateTime(rdr.GetOrdinal("Start Time of Match"));
                        DateTime endTime = rdr.GetDateTime(rdr.GetOrdinal("End Time of Match"));
                        int ordinalPos = rdr.GetOrdinal("Stadium Name");
                        String stadiumName = rdr.IsDBNull(ordinalPos) ?
                                    "Not Assigned Yet" : rdr.GetString(ordinalPos);


                        TableRow tempRow = new TableRow();
                        tempRow.CssClass = "tableRowC";

                        TableCell tempCell1 = new TableCell();
                        tempCell1.Text = hostClubName;
                        tempRow.Cells.Add(tempCell1);

                        TableCell tempCell2 = new TableCell();
                        tempCell2.Text = guestClubName;
                        tempRow.Cells.Add(tempCell2);

                        TableCell tempCell3 = new TableCell();
                        tempCell3.Text = startTime + "";
                        tempRow.Cells.Add(tempCell3);

                        TableCell tempCell4 = new TableCell();
                        tempCell4.Text = endTime + "";
                        tempRow.Cells.Add(tempCell4);

                        TableCell tempCell5 = new TableCell();
                        tempCell5.Text = stadiumName;
                        tempRow.Cells.Add(tempCell5);

                        mainTable.Rows.Add(tempRow);

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
            Response.Redirect("ClubRepresentativeHome.aspx");
        }
    }
}