using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FifaSystem
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SportsAssociationManagerButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("registerSportsAssociationManager.aspx");
        }

        protected void StadiumManagerButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("registerStadiumManager.aspx");
        }

        protected void ClubRepresentativeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("registerClubRepresentative.aspx");
        }

        protected void FanButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("registerFan.aspx");
        }
    }
}