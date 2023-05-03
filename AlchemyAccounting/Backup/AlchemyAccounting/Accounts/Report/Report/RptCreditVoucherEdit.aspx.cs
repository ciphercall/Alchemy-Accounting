using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Accounts.Report.Report
{
    public partial class RptCreditVoucherEdit : System.Web.UI.Page
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

                string Mode = "";
                string TransType = Session["TransType"].ToString();


                if (TransType == "MREC")
                {
                    string TransDate = Session["TransDate"].ToString();
                    DateTime TransDT = DateTime.Parse(TransDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string TDT = TransDT.ToString("yyyy/MM/dd");
                    string VouchNo = Session["VouchNo"].ToString();
                    Int64 transNo = Convert.ToInt64(VouchNo);
                    string DebitCD = Session["DebitCD"].ToString();
                    string CreditCD = Session["CreditCD"].ToString();
                    
                    string SubDebitCd = DebitCD.Substring(0, 7);

                    if (SubDebitCd == "1020101")
                        Mode = "CREDIT VOUCHER - CASH";
                    else
                        Mode = "CREDIT VOUCHER - BANK";

                    lblVtype.Text = Mode;
                    lblVNo.Text = VouchNo;
                    Global.lblAdd(@"select REMARKS from GL_STRANS where TRANSDT ='" + TDT + "' and TRANSNO=" + transNo + " and DEBITCD='" + DebitCD + "' and CREDITCD='" + CreditCD + "'", lblParticulars);
                    Global.lblAdd(@"select AMOUNT from GL_STRANS where TRANSDT ='" + TDT + "' and TRANSNO=" + transNo + " and DEBITCD='" + DebitCD + "' and CREDITCD='" + CreditCD + "'", lblAmount);
                    string AmountComma = SpellAmount.comma(Convert.ToDecimal(lblAmount.Text));
                    lblAmountComma.Text = AmountComma;
                    string TotAmountComma = SpellAmount.comma(Convert.ToDecimal(lblAmount.Text));
                    lblTotAmount.Text = TotAmountComma;
                    //decimal amount = Convert.ToDecimal(lblAmount.Text);
                    //lblAmountComma.Text = amount.ToString("N0");
                    //decimal Totamount = Convert.ToDecimal(lblAmount.Text);
                    //lblTotAmount.Text = Totamount.ToString("N0");
                    Global.lblAdd(@"select TRANSMODE from GL_STRANS where TRANSDT ='" + TDT + "' and TRANSNO=" + transNo + " and DEBITCD='" + DebitCD + "' and CREDITCD='" + CreditCD + "'", lblRMode);
                    Global.lblAdd(@"select CHEQUENO from GL_STRANS where TRANSDT ='" + TDT + "' and TRANSNO=" + transNo + " and DEBITCD='" + DebitCD + "' and CREDITCD='" + CreditCD + "'", lblChequeNo);
                    if (lblChequeNo.Text == "")
                    {
                        lblChequeDT.Text = "";
                    }
                    else
                    {
                        Global.lblAdd(@"select convert(nvarchar(10),CHEQUEDT,103) as CHEQUEDT from GL_STRANS where TRANSDT ='" + TDT + "' and TRANSNO=" + transNo + " and DEBITCD='" + DebitCD + "' and CREDITCD='" + CreditCD + "'", lblMidDate);
                        DateTime cqDt= DateTime.Parse(lblMidDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        lblChequeDT.Text = cqDt.ToString("dd-MMM-yyyy");
                    }



                    lblInWords.Text = "";
                    decimal dec;
                    Boolean ValidInput = Decimal.TryParse(lblAmount.Text, out dec);
                    if (!ValidInput)
                    {
                        lblInWords.ForeColor = System.Drawing.Color.Red;
                        lblInWords.Text = "Enter the Proper Amount...";
                        return;
                    }
                    if (lblAmount.Text.ToString().Trim() == "")
                    {
                        lblInWords.ForeColor = System.Drawing.Color.Red;
                        lblInWords.Text = "Amount Cannot Be Empty...";
                        return;
                    }
                    else
                    {
                        if (Convert.ToDecimal(lblAmount.Text) == 0)
                        {
                            lblInWords.ForeColor = System.Drawing.Color.Red;
                            lblInWords.Text = "Amount Cannot Be Empty...";
                            return;
                        }
                    }

                    string x1 = "";
                    string x2 = "";

                    if (lblAmount.Text.Contains("."))
                    {
                        x1 = lblAmount.Text.ToString().Trim().Substring(0, lblAmount.Text.ToString().Trim().IndexOf("."));
                        x2 = lblAmount.Text.ToString().Trim().Substring(lblAmount.Text.ToString().Trim().IndexOf(".") + 1);
                    }
                    else
                    {
                        x1 = lblAmount.Text.ToString().Trim();
                        x2 = "00";
                    }

                    if (x1.ToString().Trim() != "")
                    {
                        x1 = Convert.ToInt64(x1.Trim()).ToString().Trim();
                    }
                    else
                    {
                        x1 = "0";
                    }

                    lblAmount.Text = x1 + "." + x2;

                    if (x2.Length > 2)
                    {
                        lblAmount.Text = Math.Round(Convert.ToDouble(lblAmount.Text), 2).ToString().Trim();
                    }

                    string AmtConv = SpellAmount.MoneyConvFn(lblAmount.Text.ToString().Trim());

                    lblInWords.Text = AmtConv.Trim();

                    Global.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + DebitCD + "'", lblReceivedBy);
                    Global.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + CreditCD + "'", lblReceivedFrom);

                    

                }
                else if (TransType == "MPAY")
                {
                    string TransDate = Session["TransDate"].ToString();
                    DateTime TransDT = DateTime.Parse(TransDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string TDT = TransDT.ToString("yyyy/MM/dd");
                    string VouchNo = Session["VouchNo"].ToString();
                    Int64 transNo = Convert.ToInt64(VouchNo);
                    string DebitCD = Session["DebitCD"].ToString();
                    string CreditCD = Session["CreditCD"].ToString();

                    string SubCreditCd = CreditCD.Substring(0, 7);
                    if (SubCreditCd == "1020102")
                        Mode = "DEBIT VOUCHER - BANK";
                    else
                        Mode = "DEBIT VOUCHER - CASH";

                    lblVtype.Text = Mode;
                    lblVNo.Text = VouchNo;
                    Global.lblAdd(@"select REMARKS from GL_STRANS where TRANSDT ='" + TDT + "' and TRANSNO=" + transNo + " and DEBITCD='" + DebitCD + "' and CREDITCD='" + CreditCD + "'", lblParticulars);
                    Global.lblAdd(@"select AMOUNT from GL_STRANS where TRANSDT ='" + TDT + "' and TRANSNO=" + transNo + " and DEBITCD='" + DebitCD + "' and CREDITCD='" + CreditCD + "'", lblAmount);
                    string AmountComma = SpellAmount.comma(Convert.ToDecimal(lblAmount.Text));
                    lblAmountComma.Text = AmountComma;
                    string TotAmountComma = SpellAmount.comma(Convert.ToDecimal(lblAmount.Text));
                    lblTotAmount.Text = TotAmountComma;
                    //decimal amount = Convert.ToDecimal(lblAmount.Text);
                    //lblAmountComma.Text = amount.ToString("##,0.00");
                    //decimal Totamount = Convert.ToDecimal(lblAmount.Text);
                    //lblTotAmount.Text = Totamount.ToString("##,0.00");
                    Global.lblAdd(@"select TRANSMODE from GL_STRANS where TRANSDT ='" + TDT + "' and TRANSNO=" + transNo + " and DEBITCD='" + DebitCD + "' and CREDITCD='" + CreditCD + "'", lblRMode);
                    Global.lblAdd(@"select CHEQUENO from GL_STRANS where TRANSDT ='" + TDT + "' and TRANSNO=" + transNo + " and DEBITCD='" + DebitCD + "' and CREDITCD='" + CreditCD + "'", lblChequeNo);
                    if (lblChequeNo.Text == "")
                    {
                        lblChequeDT.Text = "";
                    }
                    else
                    {
                        Global.lblAdd(@"select convert(nvarchar(10),CHEQUEDT,103) as CHEQUEDT from GL_STRANS where TRANSDT ='" + TDT + "' and TRANSNO=" + transNo + " and DEBITCD='" + DebitCD + "' and CREDITCD='" + CreditCD + "'", lblMidDate);
                        DateTime cqDt = DateTime.Parse(lblMidDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        lblChequeDT.Text = cqDt.ToString("dd-MMM-yyyy");
                    }



                    lblInWords.Text = "";
                    decimal dec;
                    Boolean ValidInput = Decimal.TryParse(lblAmount.Text, out dec);
                    if (!ValidInput)
                    {
                        lblInWords.ForeColor = System.Drawing.Color.Red;
                        lblInWords.Text = "Enter the Proper Amount...";
                        return;
                    }
                    if (lblAmount.Text.ToString().Trim() == "")
                    {
                        lblInWords.ForeColor = System.Drawing.Color.Red;
                        lblInWords.Text = "Amount Cannot Be Empty...";
                        return;
                    }
                    else
                    {
                        if (Convert.ToDecimal(lblAmount.Text) == 0)
                        {
                            lblInWords.ForeColor = System.Drawing.Color.Red;
                            lblInWords.Text = "Amount Cannot Be Empty...";
                            return;
                        }
                    }

                    string x1 = "";
                    string x2 = "";

                    if (lblAmount.Text.Contains("."))
                    {
                        x1 = lblAmount.Text.ToString().Trim().Substring(0, lblAmount.Text.ToString().Trim().IndexOf("."));
                        x2 = lblAmount.Text.ToString().Trim().Substring(lblAmount.Text.ToString().Trim().IndexOf(".") + 1);
                    }
                    else
                    {
                        x1 = lblAmount.Text.ToString().Trim();
                        x2 = "00";
                    }

                    if (x1.ToString().Trim() != "")
                    {
                        x1 = Convert.ToInt64(x1.Trim()).ToString().Trim();
                    }
                    else
                    {
                        x1 = "0";
                    }

                    lblAmount.Text = x1 + "." + x2;

                    if (x2.Length > 2)
                    {
                        lblAmount.Text = Math.Round(Convert.ToDouble(lblAmount.Text), 2).ToString().Trim();
                    }

                    string AmtConv = SpellAmount.MoneyConvFn(lblAmount.Text.ToString().Trim());

                    lblInWords.Text = AmtConv.Trim();

                    Global.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + DebitCD + "'", lblReceivedBy);
                    Global.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + CreditCD + "'", lblReceivedFrom);

                }
                else if (TransType == "JOUR")
                {

                    Mode = "JOURNAL VOUCHER";
                    string TransDate = Session["TransDate"].ToString();
                    
                    string DebitCD = Session["DebitCD"].ToString();
                    string CreditCD = Session["CreditCD"].ToString();

                    lblVtype.Text = Mode;
                    DateTime TransDT = DateTime.Parse(TransDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string TDT = TransDT.ToString("yyyy/MM/dd");
                    string VouchNo = Session["VouchNo"].ToString();
                    Int64 transNo = Convert.ToInt64(VouchNo);
                    Global.lblAdd(@"select REMARKS from GL_STRANS where TRANSDT ='" + TDT + "' and TRANSNO=" + transNo + " and DEBITCD='" + DebitCD + "' and CREDITCD='" + CreditCD + "'", lblParticulars);
                    Global.lblAdd(@"select AMOUNT from GL_STRANS where TRANSDT ='" + TDT + "' and TRANSNO=" + transNo + " and DEBITCD='" + DebitCD + "' and CREDITCD='" + CreditCD + "'", lblAmount);
                    string AmountComma = SpellAmount.comma(Convert.ToDecimal(lblAmount.Text));
                    lblAmountComma.Text = AmountComma;
                    string TotAmountComma = SpellAmount.comma(Convert.ToDecimal(lblAmount.Text));
                    lblTotAmount.Text = TotAmountComma;
                    //decimal amount = Convert.ToDecimal(lblAmount.Text);
                    //lblAmountComma.Text = amount.ToString("##,0.00");
                    //decimal Totamount = Convert.ToDecimal(lblAmount.Text);
                    //lblTotAmount.Text = Totamount.ToString("##,0.00");
                    Global.lblAdd(@"select TRANSMODE from GL_STRANS where TRANSDT ='" + TDT + "' and TRANSNO=" + transNo + " and DEBITCD='" + DebitCD + "' and CREDITCD='" + CreditCD + "'", lblRMode);
                    Global.lblAdd(@"select CHEQUENO from GL_STRANS where TRANSDT ='" + TDT + "' and TRANSNO=" + transNo + " and DEBITCD='" + DebitCD + "' and CREDITCD='" + CreditCD + "'", lblChequeNo);
                    if (lblChequeNo.Text == "")
                    {
                        lblChequeDT.Text = "";
                    }
                    else
                    {
                        Global.lblAdd(@"select convert(nvarchar(10),CHEQUEDT,103) as CHEQUEDT from GL_STRANS where TRANSDT ='" + TDT + "' and TRANSNO=" + transNo + " and DEBITCD='" + DebitCD + "' and CREDITCD='" + CreditCD + "'", lblMidDate);
                        DateTime cqDt = DateTime.Parse(lblMidDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        lblChequeDT.Text = cqDt.ToString("dd-MMM-yyyy");
                    }



                    lblInWords.Text = "";
                    decimal dec;
                    Boolean ValidInput = Decimal.TryParse(lblAmount.Text, out dec);
                    if (!ValidInput)
                    {
                        lblInWords.ForeColor = System.Drawing.Color.Red;
                        lblInWords.Text = "Enter the Proper Amount...";
                        return;
                    }
                    if (lblAmount.Text.ToString().Trim() == "")
                    {
                        lblInWords.ForeColor = System.Drawing.Color.Red;
                        lblInWords.Text = "Amount Cannot Be Empty...";
                        return;
                    }
                    else
                    {
                        if (Convert.ToDecimal(lblAmount.Text) == 0)
                        {
                            lblInWords.ForeColor = System.Drawing.Color.Red;
                            lblInWords.Text = "Amount Cannot Be Empty...";
                            return;
                        }
                    }

                    string x1 = "";
                    string x2 = "";

                    if (lblAmount.Text.Contains("."))
                    {
                        x1 = lblAmount.Text.ToString().Trim().Substring(0, lblAmount.Text.ToString().Trim().IndexOf("."));
                        x2 = lblAmount.Text.ToString().Trim().Substring(lblAmount.Text.ToString().Trim().IndexOf(".") + 1);
                    }
                    else
                    {
                        x1 = lblAmount.Text.ToString().Trim();
                        x2 = "00";
                    }

                    if (x1.ToString().Trim() != "")
                    {
                        x1 = Convert.ToInt64(x1.Trim()).ToString().Trim();
                    }
                    else
                    {
                        x1 = "0";
                    }

                    lblAmount.Text = x1 + "." + x2;

                    if (x2.Length > 2)
                    {
                        lblAmount.Text = Math.Round(Convert.ToDouble(lblAmount.Text), 2).ToString().Trim();
                    }

                    string AmtConv = SpellAmount.MoneyConvFn(lblAmount.Text.ToString().Trim());

                    lblInWords.Text = AmtConv.Trim();

                    Global.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + DebitCD + "'", lblReceivedBy);
                    Global.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + CreditCD + "'", lblReceivedFrom);

                }
                else if (TransType == "CONT")
                {
                    Mode = "CONTRA VOUCHER";
                    string TransDate = Session["TransDate"].ToString();
                    string DebitCD = Session["DebitCD"].ToString();
                    string CreditCD = Session["CreditCD"].ToString();

                    lblVtype.Text = Mode;
                    DateTime TransDT = DateTime.Parse(TransDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string TDT = TransDT.ToString("yyyy/MM/dd");
                    string VouchNo = Session["VouchNo"].ToString();
                    Int64 transNo = Convert.ToInt64(VouchNo);
                    Global.lblAdd(@"select REMARKS from GL_STRANS where TRANSDT ='" + TDT + "' and TRANSNO=" + transNo + " and DEBITCD='" + DebitCD + "' and CREDITCD='" + CreditCD + "'", lblParticulars);
                    Global.lblAdd(@"select AMOUNT from GL_STRANS where TRANSDT ='" + TDT + "' and TRANSNO=" + transNo + " and DEBITCD='" + DebitCD + "' and CREDITCD='" + CreditCD + "'", lblAmount);
                    string AmountComma = SpellAmount.comma(Convert.ToDecimal(lblAmount.Text));
                    lblAmountComma.Text = AmountComma;
                    string TotAmountComma = SpellAmount.comma(Convert.ToDecimal(lblAmount.Text));
                    lblTotAmount.Text = TotAmountComma;
                    //decimal amount = Convert.ToDecimal(lblAmount.Text);
                    //lblAmountComma.Text = amount.ToString("##,0.00");
                    //decimal Totamount = Convert.ToDecimal(lblAmount.Text);
                    //lblTotAmount.Text = Totamount.ToString("##,0.00");

                    Global.lblAdd(@"select TRANSMODE from GL_STRANS where TRANSDT ='" + TDT + "' and TRANSNO=" + transNo + " and DEBITCD='" + DebitCD + "' and CREDITCD='" + CreditCD + "'", lblRMode);
                    Global.lblAdd(@"select CHEQUENO from GL_STRANS where TRANSDT ='" + TDT + "' and TRANSNO=" + transNo + " and DEBITCD='" + DebitCD + "' and CREDITCD='" + CreditCD + "'", lblChequeNo);
                    if (lblChequeNo.Text == "")
                    {
                        lblChequeDT.Text = "";
                    }
                    else
                    {
                        Global.lblAdd(@"select convert(nvarchar(10),CHEQUEDT,103) as CHEQUEDT from GL_STRANS where TRANSDT ='" + TDT + "' and TRANSNO=" + transNo + " and DEBITCD='" + DebitCD + "' and CREDITCD='" + CreditCD + "'", lblMidDate);
                        DateTime cqDt = DateTime.Parse(lblMidDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        lblChequeDT.Text = cqDt.ToString("dd-MMM-yyyy");
                    }



                    lblInWords.Text = "";
                    decimal dec;
                    Boolean ValidInput = Decimal.TryParse(lblAmount.Text, out dec);
                    if (!ValidInput)
                    {
                        lblInWords.ForeColor = System.Drawing.Color.Red;
                        lblInWords.Text = "Enter the Proper Amount...";
                        return;
                    }
                    if (lblAmount.Text.ToString().Trim() == "")
                    {
                        lblInWords.ForeColor = System.Drawing.Color.Red;
                        lblInWords.Text = "Amount Cannot Be Empty...";
                        return;
                    }
                    else
                    {
                        if (Convert.ToDecimal(lblAmount.Text) == 0)
                        {
                            lblInWords.ForeColor = System.Drawing.Color.Red;
                            lblInWords.Text = "Amount Cannot Be Empty...";
                            return;
                        }
                    }

                    string x1 = "";
                    string x2 = "";

                    if (lblAmount.Text.Contains("."))
                    {
                        x1 = lblAmount.Text.ToString().Trim().Substring(0, lblAmount.Text.ToString().Trim().IndexOf("."));
                        x2 = lblAmount.Text.ToString().Trim().Substring(lblAmount.Text.ToString().Trim().IndexOf(".") + 1);
                    }
                    else
                    {
                        x1 = lblAmount.Text.ToString().Trim();
                        x2 = "00";
                    }

                    if (x1.ToString().Trim() != "")
                    {
                        x1 = Convert.ToInt64(x1.Trim()).ToString().Trim();
                    }
                    else
                    {
                        x1 = "0";
                    }

                    lblAmount.Text = x1 + "." + x2;

                    if (x2.Length > 2)
                    {
                        lblAmount.Text = Math.Round(Convert.ToDouble(lblAmount.Text), 2).ToString().Trim();
                    }

                    string AmtConv = SpellAmount.MoneyConvFn(lblAmount.Text.ToString().Trim());

                    lblInWords.Text = AmtConv.Trim();

                    Global.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + DebitCD + "'", lblReceivedBy);
                    Global.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + CreditCD + "'", lblReceivedFrom);
                }
                else
                {
                    Response.Write("<script>alert('Please Select Transaction Type');</script>");
                    Response.Redirect("~/Accounts/UI/SingleTransaction.aspx");
                }
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}