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
using System.Threading;
using System.Collections;

namespace AlchemyAccounting.CNF.report.vis_rep
{
    public partial class rpt_job_bill_status : System.Web.UI.Page
    {
        decimal expAmount = 0;
        string ExpAmountcomma = "0";

        decimal billAmount = 0;
        string BillAmountcomma = "0";

        decimal recAmount = 0;
        string RecAmountcomma = "0";

        decimal dueAmount = 0;
        string DueAmountcomma = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                string Companyid = Session["Companyid"].ToString();
                string company = Session["company"].ToString();
                string JOBYY = Session["JOBYY"].ToString();
                string JOBTP = Session["JOBTP"].ToString();
                string branch = Session["branch"].ToString();

                Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID='" + Companyid + "'", lblCompNM);
                Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID='" + Companyid + "'", lblAddress);
                
                DateTime PrintDate = DateTime.Now;
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblTime.Text = td;

                lblJobYear.Text = JOBYY;
                lblJobType.Text = JOBTP;

                showGrid();
            }
        }

        public void showGrid()
        {
            SqlConnection conn = new SqlConnection(Global.connection);

            string Companyid = Session["Companyid"].ToString();
            string company = Session["company"].ToString();
            string JOBYY = Session["JOBYY"].ToString();
            string JOBTP = Session["JOBTP"].ToString();
            string branch = Session["branch"].ToString();

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT A.JOBNO, CONVERT(NVARCHAR(20),A.BILLDT,103) AS BILLDT, A.BILLNO, A.EXPAMT, A.BILLAMT, A.JOBYY, A.JOBTP, SUM(ISNULL(CNF_JOBRCV.AMOUNT,0)) AS RCVAMT, (A.BILLAMT - SUM(ISNULL(CNF_JOBRCV.AMOUNT,0))) AS DUEAMT, A.ACCOUNTNM " +
                   " FROM (SELECT CNF_JOBBILL.JOBNO, CNF_JOBBILL.BILLDT, CNF_JOBBILL.BILLNO, SUM(ISNULL(CNF_JOBBILL.EXPAMT, 0)) AS EXPAMT, SUM(ISNULL(CNF_JOBBILL.BILLAMT, 0)) + CNF_JOB.COMM_AMT AS BILLAMT, CNF_JOBBILL.JOBYY, CNF_JOBBILL.JOBTP, GL_ACCHART.ACCOUNTNM " +
                   " FROM CNF_JOBBILL INNER JOIN GL_ACCHART ON CNF_JOBBILL.PARTYID = GL_ACCHART.ACCOUNTCD LEFT OUTER JOIN CNF_JOB ON CNF_JOBBILL.JOBTP = CNF_JOB.JOBTP AND CNF_JOBBILL.JOBYY = CNF_JOB.JOBYY AND CNF_JOBBILL.JOBNO = CNF_JOB.JOBNO " +
                   " WHERE (CNF_JOBBILL.JOBYY = " + JOBYY + ") AND (CNF_JOBBILL.JOBTP = '" + JOBTP + "') AND (CNF_JOBBILL.COMPID='" + Companyid + "') GROUP BY CNF_JOBBILL.JOBNO, CNF_JOBBILL.BILLDT, CNF_JOBBILL.BILLNO, CNF_JOB.COMM_AMT, CNF_JOBBILL.JOBYY, CNF_JOBBILL.JOBTP, GL_ACCHART.ACCOUNTNM) AS A LEFT OUTER JOIN " +
                   " CNF_JOBRCV ON A.JOBNO = CNF_JOBRCV.JOBNO AND A.JOBTP = CNF_JOBRCV.JOBTP AND A.JOBYY = CNF_JOBRCV.JOBYY GROUP BY A.JOBNO, A.BILLDT, A.BILLNO, A.EXPAMT, A.BILLAMT, A.JOBYY, A.JOBTP, A.ACCOUNTNM", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;
            }
            else
            {
                GridView1.Visible = true;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    string JOBNO = DataBinder.Eval(e.Row.DataItem, "JOBNO").ToString();
                    e.Row.Cells[0].Text = JOBNO;

                    string BILLDT = DataBinder.Eval(e.Row.DataItem, "BILLDT").ToString();
                    e.Row.Cells[1].Text = BILLDT;

                    string BILLNO = DataBinder.Eval(e.Row.DataItem, "BILLNO").ToString();
                    e.Row.Cells[2].Text = BILLNO;

                    string ACCOUNTNM = DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString();
                    e.Row.Cells[3].Text = "&nbsp;" + ACCOUNTNM;

                    decimal EXPAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "EXPAMT").ToString());
                    string EXAMT = SpellAmount.comma(EXPAMT);
                    e.Row.Cells[4].Text = EXAMT;

                    decimal BILLAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BILLAMT").ToString());
                    string BAMT = SpellAmount.comma(BILLAMT);
                    e.Row.Cells[5].Text = BAMT;

                    decimal RCVAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RCVAMT").ToString());
                    string RAMT = SpellAmount.comma(RCVAMT);
                    e.Row.Cells[6].Text = RAMT;

                    decimal DUEAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DUEAMT").ToString());
                    string DAMT = SpellAmount.comma(DUEAMT);
                    e.Row.Cells[7].Text = DAMT;

                    if (DUEAMT > 0)
                        e.Row.Cells[7].CssClass = "cellred";
                    else
                        e.Row.Cells[7].CssClass = "cellgreen";

                    expAmount += EXPAMT;
                    ExpAmountcomma = SpellAmount.comma(expAmount);

                    billAmount += BILLAMT;
                    BillAmountcomma = SpellAmount.comma(billAmount);

                    recAmount += RCVAMT;
                    RecAmountcomma = SpellAmount.comma(recAmount);

                    dueAmount += DUEAMT;
                    DueAmountcomma = SpellAmount.comma(dueAmount);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = "Grand Total : ";
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Text = ExpAmountcomma;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = BillAmountcomma;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].Text = RecAmountcomma;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].Text = DueAmountcomma;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;
            }
            MakeGridViewPrinterFriendly(GridView1);
        }

        private void MakeGridViewPrinterFriendly(GridView grid)
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