using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.CNF.report.vis_rep
{
    public partial class RptReceive_VR : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                try
                {
                    Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID='" + Session["CompID"] + "'", lblCompNM);
                    Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID='" + Session["CompID"] + "'", lblAddress);
                    Global.lblAdd(@"SELECT CONTACTNO FROM ASL_COMPANY WHERE COMPID='" + Session["CompID"] + "'", lblContact);

                    DateTime PrintDate = DateTime.Today.Date;
                    string td = PrintDate.ToString("dd-MMM-yyyy");
                    lblTime.Text = td;

                    lblVtype.Text = "Money Receipt For Job";

                    string VouchNo = Session["voucherNo"].ToString();

                    string Remarks = Session["Remarks"].ToString();
                    string Amount = Session["Amount"].ToString();
                    string Inwords = Session["Inwords"].ToString();

                    string DebitCD = Session["DebitCD"].ToString();
                    string PartyID = Session["PartyID"].ToString();

                    string Jobtype = Session["JobTp"].ToString();
                    string Jobyear = Session["JobYear"].ToString();
                    string JobNo = Session["Jobno"].ToString();
                    string CompanyID = Session["CompID"].ToString();
                    string ReceiveType = Session["ReceiveTp"].ToString();

                    Global.lblAdd(@"SELECT ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD='" + DebitCD + "'", lblReceivedBy);
                    Global.lblAdd(@"SELECT ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD='" + PartyID + "'", lblReceivedFrom);


                    lblVNo.Text = VouchNo;
                    lblParticulars.Text = Remarks;
                    lblAmount.Text = Amount;
                    lblInWords.Text = Inwords;

                    lblTotAmount.Text = Amount;

                    lblRMode.Text = Jobtype;
                    lblReceivedFor.Text = ReceiveType;

                    lblJobNo.Text = JobNo + "/" + Jobyear;


                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }

        }
    }
}