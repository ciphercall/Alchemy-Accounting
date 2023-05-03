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
    public partial class rptMultipleVoucherEdit : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        decimal dblGrandTotalAmount = 0;

        string dblGrandTotalAmountComma = "0";

        string grandtotalamt = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string branch = Session["BrCD"].ToString();
                Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='" + branch + "'", lblCompNM);
                Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='" + branch + "'", lblAddress);
                Global.lblAdd(@"SELECT CONTACTNO FROM ASL_COMPANY WHERE COMPID ='" + branch + "'", lblContact);

                string TransDate = Session["TransDate"].ToString();
                DateTime TransDT = DateTime.Parse(TransDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TDT = TransDT.ToString("yyyy/MM/dd");

                DateTime PrintDate = Global.Timezone(DateTime.Now);
                string td = PrintDate.ToString("dd-MMM-yyyy");
                lblTime.Text = TransDate; 
                string TransType = Session["TransType"].ToString(); 
                string VouchNo = Session["VouchNo"].ToString();
                lblVNo.Text = VouchNo;
                ShowGrid_MREC();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public void ShowGrid_MREC()
        {
            string TransType = Session["TransType"].ToString();
            string TransDate = Session["TransDate"].ToString();
            DateTime TransDT = DateTime.Parse(TransDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TDT = TransDT.ToString("yyyy-MM-dd");
            string MonYear = Session["MonthYear"].ToString();
            string VouchNo = Session["VouchNo"].ToString();


            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlDataAdapter da = new SqlDataAdapter();

            conn.Open();
            if (TransType == "MREC")
            {
                SqlCommand cmd = new SqlCommand(" SELECT GL_MTRANS.SERIALNO, GL_ACCHART.ACCOUNTNM, GL_ACCHART_1.ACCOUNTNM AS CREDIT,  GL_MTRANS.REMARKS , GL_MTRANS.AMOUNT FROM GL_MTRANS INNER JOIN " +
                                                " GL_ACCHART ON GL_MTRANS.DEBITCD = GL_ACCHART.ACCOUNTCD INNER JOIN GL_ACCHART AS GL_ACCHART_1 ON GL_MTRANS.CREDITCD = GL_ACCHART_1.ACCOUNTCD " +
                                                " WHERE (GL_MTRANS.TRANSTP = 'MREC') AND (GL_MTRANS.TRANSDT = '" + TDT + "') AND (GL_MTRANS.TRANSMY = '" + MonYear + "') AND (GL_MTRANS.TRANSNO = " + VouchNo + ") " +
                                                " ORDER BY GL_MTRANS.SERIALNO", conn);
                da = new SqlDataAdapter(cmd);
            }
            else if (TransType == "MPAY")
            {
                SqlCommand cmd = new SqlCommand(" SELECT GL_MTRANS.SERIALNO, GL_ACCHART.ACCOUNTNM, GL_ACCHART_1.ACCOUNTNM AS CREDIT, GL_MTRANS.REMARKS , GL_MTRANS.AMOUNT FROM GL_MTRANS INNER JOIN " +
                                                " GL_ACCHART ON GL_MTRANS.DEBITCD = GL_ACCHART.ACCOUNTCD INNER JOIN GL_ACCHART AS GL_ACCHART_1 ON GL_MTRANS.CREDITCD = GL_ACCHART_1.ACCOUNTCD " +
                                                " WHERE (GL_MTRANS.TRANSTP = 'MPAY') AND (GL_MTRANS.TRANSDT = '" + TDT + "') AND (GL_MTRANS.TRANSMY = '" + MonYear + "') AND (GL_MTRANS.TRANSNO = " + VouchNo + ") " +
                                                " ORDER BY GL_MTRANS.SERIALNO", conn);
                da = new SqlDataAdapter(cmd);
            }
            else if (TransType == "JOUR")
            {
                SqlCommand cmd = new SqlCommand(" SELECT GL_MTRANS.SERIALNO, GL_ACCHART.ACCOUNTNM, GL_ACCHART_1.ACCOUNTNM AS CREDIT, GL_MTRANS.REMARKS , GL_MTRANS.AMOUNT FROM GL_MTRANS INNER JOIN " +
                                                " GL_ACCHART ON GL_MTRANS.DEBITCD = GL_ACCHART.ACCOUNTCD INNER JOIN GL_ACCHART AS GL_ACCHART_1 ON GL_MTRANS.CREDITCD = GL_ACCHART_1.ACCOUNTCD " +
                                                " WHERE (GL_MTRANS.TRANSTP = 'JOUR') AND (GL_MTRANS.TRANSDT = '" + TDT + "') AND (GL_MTRANS.TRANSMY = '" + MonYear + "') AND (GL_MTRANS.TRANSNO = " + VouchNo + ") " +
                                                " ORDER BY GL_MTRANS.SERIALNO", conn);
                da = new SqlDataAdapter(cmd);
            }
            else if (TransType == "CONT")
            {
                SqlCommand cmd = new SqlCommand(" SELECT GL_MTRANS.SERIALNO, GL_ACCHART.ACCOUNTNM, GL_ACCHART_1.ACCOUNTNM AS CREDIT,  GL_MTRANS.REMARKS , GL_MTRANS.AMOUNT FROM GL_MTRANS INNER JOIN " +
                                                " GL_ACCHART ON GL_MTRANS.DEBITCD = GL_ACCHART.ACCOUNTCD INNER JOIN GL_ACCHART AS GL_ACCHART_1 ON GL_MTRANS.CREDITCD = GL_ACCHART_1.ACCOUNTCD " +
                                                " WHERE (GL_MTRANS.TRANSTP = 'CONT') AND (GL_MTRANS.TRANSDT = '" + TDT + "') AND (GL_MTRANS.TRANSMY = '" + MonYear + "') AND (GL_MTRANS.TRANSNO = " + VouchNo + ") " +
                                                " ORDER BY GL_MTRANS.SERIALNO", conn);
                da = new SqlDataAdapter(cmd);
            }
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvdetails.DataSource = ds;
                gvdetails.DataBind();
                gvdetails.Visible = true;
            }
            else
            {

            }
        }

        protected void gvdetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string SERIALNO = DataBinder.Eval(e.Row.DataItem, "SERIALNO").ToString();
                e.Row.Cells[0].Text = SERIALNO;

                string DEBITED = DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + DEBITED;

                string CREDIT = DataBinder.Eval(e.Row.DataItem, "CREDIT").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + CREDIT;

                string REMARKS = DataBinder.Eval(e.Row.DataItem, "REMARKS").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + REMARKS;

                decimal AMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());
                string Amnt = SpellAmount.comma(AMOUNT);
                e.Row.Cells[4].Text = Amnt + "&nbsp;";

                dblGrandTotalAmount += AMOUNT;
                grandtotalamt = dblGrandTotalAmount.ToString();
                dblGrandTotalAmountComma = SpellAmount.comma(dblGrandTotalAmount);

                decimal dec;
                Boolean ValidInput = Decimal.TryParse(dblGrandTotalAmountComma, out dec);
                //if (!ValidInput)
                //{
                //    //txtTotInWords.ForeColor = System.Drawing.Color.Red;
                //    //txtTotInWords.Text = "Enter the Proper Amount...";
                //    //return;
                //}
                //if (dblGrandTotalAmountComma.ToString().Trim() == "")
                //{
                //    //txtTotInWords.ForeColor = System.Drawing.Color.Red;
                //    //txtTotInWords.Text = "Amount Cannot Be Empty...";
                //    //return;
                //}
                //else
                //{
                //    if (Convert.ToDecimal(dblGrandTotalAmountComma) == 0)
                //    {
                //        txtTotInWords.ForeColor = System.Drawing.Color.Red;
                //        txtTotInWords.Text = "Amount Cannot Be Empty...";
                //        return;
                //    }
                //}

                

            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = "TOTAL :";
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Text = dblGrandTotalAmountComma;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;

                string x1 = "";
                string x2 = "";

                if (grandtotalamt.Contains("."))
                {
                    x1 = grandtotalamt.ToString().Trim().Substring(0, grandtotalamt.ToString().Trim().IndexOf("."));
                    x2 = grandtotalamt.ToString().Trim().Substring(grandtotalamt.ToString().Trim().IndexOf(".") + 1);
                }
                else
                {
                    x1 = grandtotalamt.ToString().Trim();
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

                dblGrandTotalAmountComma = x1 + "." + x2;

                if (x2.Length > 2)
                {
                    grandtotalamt = Math.Round(Convert.ToDouble(grandtotalamt), 2).ToString().Trim();
                }

                string AmtConv = SpellAmount.MoneyConvFn(grandtotalamt.ToString().Trim());
                //string amntComma = SpellAmount.comma(Convert.ToDecimal(txtAmount.Text));
                //Label3.Text = amntComma;

                lblInWords.Text = AmtConv.Trim();
                //txtTotInWords.ForeColor = System.Drawing.Color.Green;
            }

            ShowHeader(gvdetails);
        }

        private void ShowHeader(GridView grid)
        {
            if (grid.Rows.Count > 0)
            {
                grid.UseAccessibleHeader = true;
                grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                //gridView.HeaderRow.Style["display"] = "table-header-group";
            }
        }
    }
}