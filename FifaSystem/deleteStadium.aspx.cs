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
    public partial class deleteStadium : System.Web.UI.Page
    {
        protected void doJob(object sender, EventArgs e, String stadiumName)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["FifaSystem"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand deleteStadium = new SqlCommand("deleteStadium", conn);
            deleteStadium.CommandType = CommandType.StoredProcedure;
            deleteStadium.Parameters.Add(new SqlParameter("@stadiumName", stadiumName));

            conn.Open();
            deleteStadium.ExecuteNonQuery();
            conn.Close();

            Response.Redirect("deleteStadium.aspx");
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

                if (type.Value.ToString() != "0")
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    SqlCommand trial = conn.CreateCommand();
                    trial.CommandText = "Select * From allStadiums";

                    conn.Open();
                    SqlDataReader rdr = trial.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        String stadiumName = rdr.GetString(rdr.GetOrdinal("Stadium Name"));
                        String stadiumLocation = rdr.GetString(rdr.GetOrdinal("Stadium Location"));
                        int stadiumCapacity = rdr.GetInt32(rdr.GetOrdinal("Capacity"));
                        Boolean stadiumStatus = rdr.GetBoolean(rdr.GetOrdinal("Status"));

                        TableRow tempRow = new TableRow();
                        tempRow.CssClass = "tableRowC";

                        TableCell tempCell1 = new TableCell();
                        tempCell1.Text = stadiumName;
                        tempRow.Cells.Add(tempCell1);

                        TableCell tempCell2 = new TableCell();
                        tempCell2.Text = stadiumLocation;
                        tempRow.Cells.Add(tempCell2);

                        TableCell tempCell3 = new TableCell();
                        tempCell3.Text = stadiumCapacity + "";
                        tempRow.Cells.Add(tempCell3);

                        TableCell tempCell4 = new TableCell();
                        tempCell4.Text = stadiumStatus ? "Available" : "Not Available";
                        tempRow.Cells.Add(tempCell4);

                        TableCell tempCell5 = new TableCell();
                        Button deleteB = new Button();
                        deleteB.Text = "Delete Stadium";
                        deleteB.CssClass = "buttonTable";
                        deleteB.Click += (sender2, e2) => doJob(sender2, e2, stadiumName);
                        tempRow.Cells.Add(tempCell5);

                        mainTable.Rows.Add(tempRow);
                        tempCell5.Controls.Add(deleteB);
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
            Response.Redirect("SystemAdminHome.aspx");
        }
    }
}