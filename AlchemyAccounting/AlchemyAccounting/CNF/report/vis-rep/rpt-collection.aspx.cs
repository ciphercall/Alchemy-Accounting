using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AlchemyAccounting.CNF.report.vis_rep
{
    public partial class rpt_collection : System.Web.UI.Page
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
            else if (Session["UserName"].ToString() == "")
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY", lblCompNM);
                Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY", lblAddress);

                DateTime PrintDate = DateTime.Now;
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblPrintDate.Text = td;

                string printSchem = Session["schNM"].ToString();
                string printBranch = Session["brNM"].ToString();
                string schID = Session["schTp"].ToString();
                string branchID = Session["brId"].ToString();
                string printArea = Session["areaNm"].ToString();
                string areaID = Session["areaID"].ToString();
                string monYear = Session["mnYr"].ToString();
                string date = Session["dt"].ToString();
                string docNo = Session["docNo"].ToString();
                string printCollector = Session["collector"].ToString();
                string collID = Session["collecId"].ToString();
                string remarks = Session["remarks"].ToString();

                lblSchTp.Text = printSchem;
                lblBranch.Text = printBranch;
                lblArea.Text = printArea;
                lblDocNo.Text = docNo;
                lblCollector.Text = printCollector;
                lblRemarks.Text = remarks;

                DateTime asOn = DateTime.Parse(date, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lblDate.Text = asOn.ToString("dd-MMM-yyyy");

                showGrid();
            }
        }

        public void showGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string schID = Session["schTp"].ToString();
            string branchID = Session["brId"].ToString();
            string areaID = Session["areaID"].ToString();
            string monYear = Session["mnYr"].ToString();
            string date = Session["dt"].ToString();
            DateTime asOn = DateTime.Parse(date, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string clDT = asOn.ToString("yyyy-MM-dd");
            string docNo = Session["docNo"].ToString();
            string printCollector = Session["collector"].ToString();
            string collID = Session["collecId"].ToString();


            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT MC_COLLECT_MST.SCHEME_ID, MC_COLLECT_MST.BRANCH_ID, MC_COLLECT_MST.AREA_ID, MC_COLLECT_MST.TRANSDT, MC_COLLECT_MST.TRANSMY, " +
                      " MC_COLLECT_MST.TRANSYY, MC_COLLECT_MST.TRANSNO, MC_COLLECT_MST.FWID, MC_COLLECT_MST.REMARKS, MC_COLLECT.SERIALNO, MC_COLLECT.MEMBERID, MC_COLLECT.INTERNALID, MC_COLLECT.COLLECTAMT, MC_MEMBER.MEMBER_NM " +
                      " FROM MC_COLLECT_MST INNER JOIN MC_COLLECT ON MC_COLLECT_MST.TRANSMY = MC_COLLECT.TRANSMY AND MC_COLLECT_MST.TRANSNO = MC_COLLECT.TRANSNO AND " +
                      " MC_COLLECT_MST.FWID = MC_COLLECT.FWID AND MC_COLLECT_MST.TRANSDT = MC_COLLECT.TRANSDT AND MC_COLLECT_MST.AREA_ID = MC_COLLECT.AREA_ID AND MC_COLLECT_MST.BRANCH_ID = MC_COLLECT.BRANCH_ID AND " +
                      " MC_COLLECT_MST.SCHEME_ID = MC_COLLECT.SCHEME_ID INNER JOIN MC_MEMBER ON MC_COLLECT.MEMBERID = MC_MEMBER.MEMBER_ID WHERE MC_COLLECT_MST.SCHEME_ID='" + schID + "' " +
                      " AND MC_COLLECT_MST.BRANCH_ID ='" + branchID + "' AND MC_COLLECT_MST.AREA_ID = '" + areaID + "' AND MC_COLLECT_MST.TRANSDT ='" + clDT + "' AND " +
                      " MC_COLLECT_MST.TRANSMY ='" + monYear + "' AND MC_COLLECT_MST.TRANSNO =" + docNo + " AND MC_COLLECT_MST.FWID ='" + collID + "'", conn);

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
                string SERIALNO = DataBinder.Eval(e.Row.DataItem, "SERIALNO").ToString();
                e.Row.Cells[0].Text = SERIALNO;

                string MEMBER_NM = DataBinder.Eval(e.Row.DataItem, "MEMBER_NM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + MEMBER_NM;

                string INTERNALID = DataBinder.Eval(e.Row.DataItem, "INTERNALID").ToString();
                e.Row.Cells[2].Text = INTERNALID;

                decimal COLLECTAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "COLLECTAMT").ToString());
                string Amnt = SpellAmount.comma(COLLECTAMT);
                e.Row.Cells[3].Text = Amnt + "&nbsp;";

                totAmount += COLLECTAMT;
                ttAmt = totAmount.ToString();
                totAmountComma = SpellAmount.comma(totAmount);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total : ";
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = totAmountComma;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
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