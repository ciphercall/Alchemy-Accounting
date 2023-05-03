using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace AlchemyAccounting.CNF.report.vis_rep
{
    public partial class RptExpense_VR : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        decimal totAmount = 0;

        string totAmountComma = "0";
        string ttAmt = "0";

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
                    DateTime td = DateTime.Now;
                    lblPrintDate.Text = td.ToString("dd/MM/yyyy hh:mm tt");

                    //string dt = Session["Jobdate"].ToString();

                    string dt = Session["Jobdate"].ToString();

                    DateTime d = DateTime.Parse(dt, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string date = d.ToString("yyyy-MM-dd");

                    lbldt.Text = d.ToString("dd/MM/yyyy");

                    string tranmy = Session["transmy"].ToString();
                    string invoiceNo = Session["InvoiceNo"].ToString();
                    string jobno = Session["JobNo"].ToString();
                    string jobyy = Session["Jobyear"].ToString();
                    string jobtp = Session["JobType"].ToString();
                    string compid = Session["Compid"].ToString();
                    string expensecd = Session["expenseCD"].ToString();
                    string remarkstop = Session["remarksT"].ToString();

                    Global.lblAdd("select COMPNM from ASL_COMPANY where COMPID='" + compid + "'", lblCompNM);
                    Global.lblAdd("select ADDRESS from ASL_COMPANY where COMPID='" + compid + "'", lblAddress);

                    string jobtype = jobtp.Substring(0,3);

                    lblJobNo.Text = jobtype + "/" + jobno + "/" + jobyy;
                    lblInvoiceNo.Text = invoiceNo;
                    lblRemarks.Text = remarkstop;

                    Global.lblAdd("SELECT GOODS FROM CNF_JOB WHERE JOBNO =" + jobno + " AND JOBYY =" + jobyy + " AND JOBTP ='" + jobtp + "' ", lblGoods);
                    Global.lblAdd("SELECT PKGS FROM CNF_JOB WHERE JOBNO =" + jobno + " AND JOBYY =" + jobyy + " AND JOBTP ='" + jobtp + "' ", lblPackages);

                    Global.lblAdd("SELECT GL_ACCHART.ACCOUNTNM FROM CNF_JOB INNER JOIN GL_ACCHART ON CNF_JOB.PARTYID = GL_ACCHART.ACCOUNTCD WHERE JOBNO =" + jobno + " AND JOBYY =" + jobyy + " AND JOBTP ='" + jobtp + "' ", lblparty);
                    Global.lblAdd("select ACCOUNTNM from GL_ACCHART where ACCOUNTCD='" + expensecd + "'", lblExpenseBy);
                    ShowGrid();

                }
                catch (Exception ex)
                {
                    Response.Write(ex);
                }
            }
        }
        public void ShowGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string dt = Session["Jobdate"].ToString();

            DateTime d = DateTime.Parse(dt, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string date = d.ToString("yyyy-MM-dd");

            string tranmy = Session["transmy"].ToString();
            string invoiceNo = Session["InvoiceNo"].ToString();
            string jobno = Session["JobNo"].ToString();
            string jobyy = Session["Jobyear"].ToString();
            string jobtp = Session["JobType"].ToString();
            string compid = Session["Compid"].ToString();
            string expensecd = Session["expenseCD"].ToString();
            string remarkstop = Session["remarksT"].ToString();

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT CNF_EXPENSE.EXPNM, CNF_JOBEXP.SLNO, CNF_JOBEXP.EXPAMT, CNF_JOBEXP.REMARKS FROM CNF_JOBEXP INNER JOIN CNF_EXPENSE ON CNF_JOBEXP.EXPID = CNF_EXPENSE.EXPID WHERE CNF_JOBEXP.TRANSDT='" + date + "' AND CNF_JOBEXP.TRANSMY='" + tranmy + "' AND TRANSNO='" + invoiceNo + "' ORDER BY CNF_JOBEXP.SLNO", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvReport.DataSource = ds;
                gvReport.DataBind();
                gvReport.Visible = true;
            }
            else
            {
                gvReport.Visible = true;
            }
        }
        protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string SLNO = DataBinder.Eval(e.Row.DataItem, "SLNO").ToString();
                e.Row.Cells[0].Text = SLNO;

                string EXPNM = DataBinder.Eval(e.Row.DataItem, "EXPNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + EXPNM;

                string REMARKS = DataBinder.Eval(e.Row.DataItem, "REMARKS").ToString();
                e.Row.Cells[3].Text = REMARKS;

                decimal EXPAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "EXPAMT").ToString());
                string Amnt = SpellAmount.comma(EXPAMT);
                e.Row.Cells[2].Text = Amnt + "&nbsp;";

                totAmount += EXPAMT;
                ttAmt = totAmount.ToString();
                totAmountComma = SpellAmount.comma(totAmount);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Total : ";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].Text = totAmountComma;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;

                lblInWords.Text = "";
                decimal dec;
                Boolean ValidInput = Decimal.TryParse(ttAmt, out dec);
                if (!ValidInput)
                {
                    lblInWords.ForeColor = System.Drawing.Color.Red;
                    lblInWords.Text = "Enter the Proper Amount...";
                    return;
                }
                if (ttAmt.ToString().Trim() == "")
                {
                    lblInWords.ForeColor = System.Drawing.Color.Red;
                    lblInWords.Text = "Amount Cannot Be Empty...";
                    return;
                }
                else
                {
                    if (Convert.ToDecimal(ttAmt) == 0)
                    {
                        lblInWords.ForeColor = System.Drawing.Color.Red;
                        lblInWords.Text = "Amount Cannot Be Empty...";
                        return;
                    }
                }

                string x1 = "";
                string x2 = "";

                if (ttAmt.Contains("."))
                {
                    x1 = ttAmt.ToString().Trim().Substring(0, ttAmt.ToString().Trim().IndexOf("."));
                    x2 = ttAmt.ToString().Trim().Substring(ttAmt.ToString().Trim().IndexOf(".") + 1);
                }
                else
                {
                    x1 = ttAmt.ToString().Trim();
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

                ttAmt = x1 + "." + x2;

                if (x2.Length > 2)
                {
                    ttAmt = Math.Round(Convert.ToDouble(ttAmt), 2).ToString().Trim();
                }

                string AmtConv = SpellAmount.MoneyConvFn(ttAmt.ToString().Trim());

                lblInWords.Text = AmtConv.Trim();

            }
            ShowHeader(gvReport);
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