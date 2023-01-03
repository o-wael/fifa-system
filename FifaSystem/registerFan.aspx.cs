using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Numerics;
using System.Runtime.InteropServices.ComTypes;

namespace FifaSystem
{
    public partial class registerFan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void registerButton_Click(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["FifaSystem"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String uname = username.Text;
            String pass = password.Text;
            String Name = name.Text;
            String natID = nationalID.Text;
            String birthDate = bDate.Text.ToString();
            String pnum = phoneNumber.Text;
            String add = address.Text;

            
            if (uname != "" && pass != "" && Name != "" && natID != "" && birthDate != "" && pnum != "" && add != "")
            {
                String[] strList = birthDate.Split('-');
                String newBirthDate = strList[0] + "/" + strList[1] + "/" + strList[2];

                SqlCommand registerFan = new SqlCommand("addFan", conn);
                registerFan.CommandType = CommandType.StoredProcedure;

                registerFan.Parameters.Add(new SqlParameter("@inp_name", Name));
                registerFan.Parameters.Add(new SqlParameter("@inp_username", uname));
                registerFan.Parameters.Add(new SqlParameter("@inp_password", pass));
                registerFan.Parameters.Add(new SqlParameter("@inp_national_id", natID));
                registerFan.Parameters.Add(new SqlParameter("@inp_birth_date", newBirthDate));
                registerFan.Parameters.Add(new SqlParameter("@inp_address", add));
                registerFan.Parameters.Add(new SqlParameter("@inp_phone_number", pnum));

                SqlParameter success = registerFan.Parameters.Add("@success", SqlDbType.Bit);
                success.Direction = ParameterDirection.Output;

                conn.Open();
                registerFan.ExecuteNonQuery();
                conn.Close();

                if (success.Value.ToString() == "False")
                {
                    successLabel.Text = "Registration Unsuccessful!";
                }
                else
                {
                    Response.Redirect("login.aspx");
                }
            }
        }
    }
}