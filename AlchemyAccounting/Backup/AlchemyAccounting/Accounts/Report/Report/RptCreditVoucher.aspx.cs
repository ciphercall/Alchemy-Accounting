using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using AlchemyAccounting;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using System.Collections.Specialized;

namespace AlchemyAccounting.Accounts.Report.Report
{
    public partial class RptCreditVoucher : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string branch = Session["BrCD"].ToString();
                Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='" + branch + "'", lblCompNM);
                Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='" + branch + "'", lblAddress);
                Global.lblAdd(@"SELECT CONTACTNO FROM ASL_COMPANY WHERE COMPID ='" + branch + "'", lblContact);

                DateTime PrintDate = DateTime.Today.Date;
                string td = PrintDate.ToString("dd-MMM-yyyy");
                lblTime.Text = td;

                string Mode =""; 
                string TransType = Session["TransType"].ToString();
                

                if (TransType == "MREC")
                {
                    string TransDate = Session["TransDate"].ToString();
                    string VouchNo = Session["VouchNo"].ToString();
                    string TransMode = Session["TransMode"].ToString();
                    string DebitCD = Session["DebitCD"].ToString();
                    string CreditCD = Session["CreditCD"].ToString();
                    string ChequeNo = Session["ChequeNo"].ToString();
                    string ChequeDT = Session["ChequeDT"].ToString();
                    string Remarks = Session["Remarks"].ToString();
                    string Amount = Session["Amount"].ToString();
                    string Inword = Session["Inword"].ToString();
                    string SubDebitCd = DebitCD.Substring(0, 7);

                    if (SubDebitCd == "1020101")
                        Mode = "CREDIT VOUCHER - CASH";
                    else
                        Mode = "CREDIT VOUCHER - BANK";

                    lblVtype.Text = Mode;
                    lblVNo.Text = VouchNo;
                    lblParticulars.Text = Remarks;
                    string AmountComma = SpellAmount.comma(Convert.ToDecimal(Amount));
                    lblAmount.Text = AmountComma;
                    string TotAmountComma = SpellAmount.comma(Convert.ToDecimal(Amount));
                    lblTotAmount.Text = TotAmountComma;
                    lblRMode.Text = TransMode;
                    lblChequeNo.Text = ChequeNo;
                    lblChequeDT.Text = ChequeDT;
                    lblInWords.Text = Inword;
                    Global.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + DebitCD + "'", lblReceivedBy);
                    Global.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + CreditCD + "'", lblReceivedFrom);

                    DateTime TransDT = DateTime.Parse(TransDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

                }
                else if (TransType == "MPAY")
                {
                    string TransDate = Session["TransDate"].ToString();
                    string VouchNo = Session["VouchNo"].ToString();
                    string TransMode = Session["TransMode"].ToString();
                    string DebitCD = Session["DebitCD"].ToString();
                    string CreditCD = Session["CreditCD"].ToString();
                    string ChequeNo = Session["ChequeNo"].ToString();
                    string ChequeDT = Session["ChequeDT"].ToString();
                    string Remarks = Session["Remarks"].ToString();
                    string Amount = Session["Amount"].ToString();
                    string Inword = Session["Inword"].ToString();
                    string Credited = Session["CreditCD"].ToString();
                    string SubCreditCd = Credited.Substring(0, 7);
                    if (SubCreditCd == "1020102")
                        Mode = "DEBIT VOUCHER - BANK";
                    else
                        Mode = "DEBIT VOUCHER - CASH";

                    lblVtype.Text = Mode;
                    lblVNo.Text = VouchNo;
                    lblParticulars.Text = Remarks;
                    string AmountComma = SpellAmount.comma(Convert.ToDecimal(Amount));
                    lblAmount.Text = AmountComma;
                    string TotAmountComma = SpellAmount.comma(Convert.ToDecimal(Amount));
                    lblTotAmount.Text = TotAmountComma;
                    lblRMode.Text = TransMode;
                    lblChequeNo.Text = ChequeNo;
                    lblChequeDT.Text = ChequeDT;
                    lblInWords.Text = Inword;
                    Global.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + DebitCD + "'", lblReceivedBy);
                    Global.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + CreditCD + "'", lblReceivedFrom);

                    DateTime TransDT = DateTime.Parse(TransDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

                }
                else if (TransType == "JOUR")
                {

                    Mode = "JOURNAL VOUCHER";
                    string TransDate = Session["TransDate"].ToString();
                    string VouchNo = Session["VouchNo"].ToString();
                    string TransMode = Session["TransMode"].ToString();
                    string DebitCD = Session["DebitCD"].ToString();
                    string CreditCD = Session["CreditCD"].ToString();
                    string ChequeNo = Session["ChequeNo"].ToString();
                    string ChequeDT = Session["ChequeDT"].ToString();
                    string Remarks = Session["Remarks"].ToString();
                    string Amount = Session["Amount"].ToString();
                    string Inword = Session["Inword"].ToString();
                    lblVtype.Text = Mode;
                    lblVNo.Text = VouchNo;
                    lblParticulars.Text = Remarks;
                    string AmountComma = SpellAmount.comma(Convert.ToDecimal(Amount));
                    lblAmount.Text = AmountComma;
                    string TotAmountComma = SpellAmount.comma(Convert.ToDecimal(Amount));
                    lblTotAmount.Text = TotAmountComma;
                    lblRMode.Text = TransMode;
                    lblChequeNo.Text = ChequeNo;
                    lblChequeDT.Text = ChequeDT;
                    lblInWords.Text = Inword;
                    Global.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + DebitCD + "'", lblReceivedBy);
                    Global.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + CreditCD + "'", lblReceivedFrom);

                    DateTime TransDT = DateTime.Parse(TransDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

                }
                else if (TransType == "CONT")
                {
                    Mode = "CONTRA VOUCHER";
                    string TransDate = Session["TransDate"].ToString();
                    string VouchNo = Session["VouchNo"].ToString();
                    string TransMode = Session["TransMode"].ToString();
                    string DebitCD = Session["DebitCD"].ToString();
                    string CreditCD = Session["CreditCD"].ToString();
                    string ChequeNo = Session["ChequeNo"].ToString();
                    string ChequeDT = Session["ChequeDT"].ToString();
                    string Remarks = Session["Remarks"].ToString();
                    string Amount = Session["Amount"].ToString();
                    string Inword = Session["Inword"].ToString();

                    lblVtype.Text = Mode;
                    lblVNo.Text = VouchNo;
                    lblParticulars.Text = Remarks;
                    string AmountComma = SpellAmount.comma(Convert.ToDecimal(Amount));
                    lblAmount.Text = AmountComma;
                    string TotAmountComma = SpellAmount.comma(Convert.ToDecimal(Amount));
                    lblTotAmount.Text = TotAmountComma;
                    lblRMode.Text = TransMode;
                    lblChequeNo.Text = ChequeNo;
                    lblChequeDT.Text = ChequeDT;
                    lblInWords.Text = Inword;
                    Global.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + DebitCD + "'", lblReceivedBy);
                    Global.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + CreditCD + "'", lblReceivedFrom);

                    DateTime TransDT = DateTime.Parse(TransDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                }
                else
                {
                    Response.Write("<script>alert('Please Select Transaction Type');</script>");
                    Response.Redirect("~/Accounts/UI/SingleTransaction.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}