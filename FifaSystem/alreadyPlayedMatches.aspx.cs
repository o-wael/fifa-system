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
    public partial class alreadyPlayedMatches : System.Web.UI.Page
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
                else
                {
                    SqlCommand trial = conn.CreateCommand();
                    trial.CommandText = "Select * From alreadyPlayedMatches";

                    conn.Open();
                    SqlDataReader rdr = trial.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        String hostName = rdr.GetString(rdr.GetOrdinal("Host Name"));
                        String guestName = rdr.GetString(rdr.GetOrdinal("Guest Name"));
                        DateTime startTime = rdr.GetDateTime(rdr.GetOrdinal("Start Time"));
                        DateTime endTime = rdr.GetDateTime(rdr.GetOrdinal("End Time"));

                        TableRow tempRow = new TableRow();
                        tempRow.CssClass = "tableRowC";

                        TableCell tempCell1 = new TableCell();
                        tempCell1.Text = hostName;
                        tempRow.Cells.Add(tempCell1);

                        TableCell tempCell2 = new TableCell();
                        tempCell2.Text = guestName;
                        tempRow.Cells.Add(tempCell2);

                        TableCell tempCell3 = new TableCell();
                        tempCell3.Text = startTime + "";
                        tempRow.Cells.Add(tempCell3);

                        TableCell tempCell4 = new TableCell();
                        tempCell4.Text = endTime + "";
                        tempRow.Cells.Add(tempCell4);

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
            Response.Redirect("SportsAssociationManagerHome.aspx");
        }
    }
}