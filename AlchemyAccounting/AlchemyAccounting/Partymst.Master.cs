using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting
{
    public partial class Partymst : System.Web.UI.MasterPage
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["email"] = null;
            Response.Redirect("/index.aspx");
        }

        protected void btnchangpass_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Party/UI/ChangePassword.aspx");
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Party/UI/track.aspx");
        }

    }
}