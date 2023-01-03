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
    public partial class blockFan : System.Web.UI.Page
    {
        protected void doJobBlock(object sender, EventArgs e, String fanNationalID)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["FifaSystem"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand blockFan = new SqlCommand("blockFan", conn);
            blockFan.CommandType = CommandType.StoredProcedure;
            blockFan.Parameters.Add(new SqlParameter("@fan_national_id", fanNationalID));

            conn.Open();
            blockFan.ExecuteNonQuery();
            conn.Close();

            Response.Redirect("blockFan.aspx");
        }

        protected void doJobUnblock(object sender, EventArgs e, String fanNationalID)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["FifaSystem"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand unblockFan = new SqlCommand("unblockFan", conn);
            unblockFan.CommandType = CommandType.StoredProcedure;
            unblockFan.Parameters.Add(new SqlParameter("@fan_national_id", fanNationalID));

            conn.Open();
            unblockFan.ExecuteNonQuery();
            conn.Close();

            Response.Redirect("blockFan.aspx");
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
                    trial.CommandText = "Select * From allFans";

                    conn.Open();
                    SqlDataReader rdr = trial.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        String fanName = rdr.GetString(rdr.GetOrdinal("Fan Name"));
                        String fanNationalID = rdr.GetString(rdr.GetOrdinal("Fan National ID"));
                        Boolean fanStatus = rdr.GetBoolean(rdr.GetOrdinal("Status"));

                        TableRow tempRow = new TableRow();
                        tempRow.CssClass = "tableRowC";

                        TableCell tempCell1 = new TableCell();
                        tempCell1.Text = fanName;
                        tempRow.Cells.Add(tempCell1);

                        TableCell tempCell2 = new TableCell();
                        tempCell2.Text = fanNationalID;
                        tempRow.Cells.Add(tempCell2);

                        if (fanStatus)
                        {
                            TableCell tempCell3 = new TableCell();
                            Button blockB = new Button();
                            blockB.Text = "Block Fan";
                            blockB.CssClass = "buttonTable";
                            blockB.Click += (sender2, e2) => doJobBlock(sender2, e2, fanNationalID);
                            tempRow.Cells.Add(tempCell3);

                            TableCell tempCell4 = new TableCell();
                            tempCell4.Text = "Unblocked";
                            tempRow.Cells.Add(tempCell4);

                            mainTable.Rows.Add(tempRow);
                            tempCell3.Controls.Add(blockB);
                        }
                        else
                        {
                            TableCell tempCell3 = new TableCell();
                            tempCell3.Text = "Blocked";
                            tempRow.Cells.Add(tempCell3);

                            TableCell tempCell4 = new TableCell();
                            Button unblockB = new Button();
                            unblockB.Text = "Unblock Fan";
                            unblockB.CssClass = "buttonTable";
                            unblockB.Click += (sender2, e2) => doJobUnblock(sender2, e2, fanNationalID);
                            tempRow.Cells.Add(tempCell4);

                            mainTable.Rows.Add(tempRow);
                            tempCell4.Controls.Add(unblockB);
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
            Response.Redirect("SystemAdminHome.aspx");
        }
    }
}