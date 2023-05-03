using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                string unm = Session["UserName"].ToString();
                Global.lblAdd("SELECT Name FROM User_Registration WHERE UserID ='" + unm + "'", lblUserNM);
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void lnkLOgout_Click(object sender, EventArgs e)
        {
            Session["UserName"] = null;
            Session["PCName"] = null;
            Session["IpAddress"] = null;
            Session["BrCD"] = null;
            Session["UserTp"] = null;
            Response.Redirect("~/Login/UI/Login.aspx");
        }
    }
}
