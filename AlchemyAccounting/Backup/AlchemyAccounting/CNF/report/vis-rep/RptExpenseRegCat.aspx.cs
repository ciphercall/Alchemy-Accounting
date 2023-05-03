using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace AlchemyAccounting.CNF.report.vis_rep
{
    public partial class RptExpenseRegCat : System.Web.UI.Page
    {
        string strPreviousRowID = string.Empty;
        string strPreviousRowID2 = string.Empty;
        string strPreviousRowID3 = string.Empty;
        string strPreviousRowID4 = string.Empty;
        string strPreviousRowID5 = string.Empty;

        int intSubTotalIndex = 1;
        int intSubTotalIndex2 = 1;
        decimal dblSubTotalAmount = 0;
        decimal dblGrandTotalAmount = 0;
        string dblSubTotalAmountComma = "0";
        string dblGrandTotalAmountComma = "0";
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/user/Login.aspx");
            }

            else
            {
                if (!IsPostBack)
                {
                    ShowGrid();

                    DateTime t = DateTime.Now;
                    lblPrintDate.Text = t.ToString("dd/MM/yyy hh:mm:ss:tt");
                }
            }
        }

        public void ShowGrid()
        {


            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);




            string jobno = Session["jobno"].ToString();
            string jobty = Session["jobtp"].ToString();
            string jobyy = Session["jobyy"].ToString();

            Global.lblAdd("SELECT GL_ACCHART.ACCOUNTNM FROM CNF_JOB INNER JOIN CNF_JOBEXP ON CNF_JOB.JOBYY = CNF_JOBEXP.JOBYY AND CNF_JOB.JOBTP = CNF_JOBEXP.JOBTP AND CNF_JOB.JOBNO = CNF_JOBEXP.JOBNO INNER JOIN GL_ACCHART ON CNF_JOB.PARTYID = GL_ACCHART.ACCOUNTCD WHERE CNF_JOBEXP.JOBNO='" + jobno + "' AND CNF_JOBEXP.JOBTP='" + jobty + "' AND CNF_JOBEXP.JOBYY ='" + jobyy + "' ", lblPartyNM);

            Global.lblAdd("SELECT CONVERT(NVARCHAR(20),TRANSDT,103) AS TRANSDT FROM CNF_JOBEXPMST WHERE JOBNO=" + jobno + " AND JOBTP='" + jobty + "' AND JOBYY=" + jobyy + "", lblJobDate);
            Global.lblAdd("SELECT TRANSNO FROM CNF_JOBEXPMST WHERE JOBNO=" + jobno + " AND JOBTP='" + jobty + "' AND JOBYY=" + jobyy + "", lblInvoice);
            Global.lblAdd("SELECT REMARKS FROM CNF_JOBEXPMST WHERE JOBNO=" + jobno + " AND JOBTP='" + jobty + "' AND JOBYY=" + jobyy + "", lblRemarks);

            
            lblJobNo.Text = jobno;
            lblJobType.Text = jobty;
            lblJobyr.Text = jobyy;

            Global.lblAdd("SELECT ASL_COMPANY.BRANCHID FROM CNF_JOBEXP INNER JOIN ASL_COMPANY ON CNF_JOBEXP.COMPID = ASL_COMPANY.COMPID WHERE CNF_JOBEXP.JOBNO='" + jobno + "' AND CNF_JOBEXP.JOBTP='" + jobty + "' AND CNF_JOBEXP.JOBYY ='" + jobyy + "' ", lblBranch);
            Global.lblAdd("SELECT ASL_COMPANY.COMPNM FROM CNF_JOBEXP INNER JOIN ASL_COMPANY ON CNF_JOBEXP.COMPID = ASL_COMPANY.COMPID WHERE CNF_JOBEXP.JOBNO='" + jobno + "' AND CNF_JOBEXP.JOBTP='" + jobty + "' AND CNF_JOBEXP.JOBYY ='" + jobyy + "' ", lblCompanyNM);

            SqlCommand cmd = new SqlCommand(" SELECT CNF_JOBEXP.EXPCD,COUNT(*) AS Qty, SUM (CNF_JOBEXP.EXPAMT) AS EXPAMT, GL_ACCHART.ACCOUNTNM, CNF_EXPENSE.EXPNM, CNF_JOBEXP.EXPID " +
                            " FROM CNF_JOBEXP INNER JOIN " +
                      " CNF_EXPENSE ON CNF_JOBEXP.EXPID = CNF_EXPENSE.EXPID INNER JOIN " +
                      " GL_ACCHART ON CNF_JOBEXP.EXPCD = GL_ACCHART.ACCOUNTCD " +
                     " WHERE CNF_JOBEXP.JOBNO='" + jobno + "' AND CNF_JOBEXP.JOBTP='" + jobty + "' AND CNF_JOBEXP.JOBYY ='" + jobyy + "' GROUP BY CNF_JOBEXP.EXPCD, GL_ACCHART.ACCOUNTNM, CNF_EXPENSE.EXPNM, CNF_JOBEXP.EXPID", conn);
            cmd.Parameters.Clear();

            conn.Open();
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
                gvReport.DataSource = ds;
                gvReport.DataBind();
                gvReport.Visible = true;
            }
        }


        protected void gvReport_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsSubTotalRowNeedToAdd = false;
            //bool IsGrandTotalRowNeedtoAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "EXPCD") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "EXPCD").ToString())

                    IsSubTotalRowNeedToAdd = true;


            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "EXPCD") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                //IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;

            }

            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "EXPCD") != null))
            {
                GridView gvReport = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = DataBinder.Eval(e.Row.DataItem, "EXPCD").ToString();
                //cell.ColumnSpan = 5;
                cell.Visible = false;
                //cell.CssClass = "GroupHeaderStyle";
                cell.Font.Bold = true;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString();
                //cell.ColumnSpan = 5;
                cell.CssClass = "GroupHeaderStyle";
                cell.Font.Bold = true;
                row.Cells.Add(cell);

                //cell = new TableCell();
                //cell.Text = "Party :  " + DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString();
                ////cell.ColumnSpan = 5;
                //cell.CssClass = "GroupHeaderStyle";
                //cell.Font.Bold = true;
                //row.Cells.Add(cell);

                gvReport.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;



            }
            #endregion
            if (IsSubTotalRowNeedToAdd)
            {
                #region Adding Sub Total Row
                GridView gvReport = (GridView)sender;
                // Creating a Row          
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell          
                TableCell cell = new TableCell();
                cell.Text = "Sub Total : ";
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 2;
                cell.Font.Bold = true;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Amount Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalAmountComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Font.Bold = true;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

               
                //Adding the Row at the RowIndex position in the Grid      
                gvReport.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                #endregion
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "EXPCD") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = DataBinder.Eval(e.Row.DataItem, "EXPCD").ToString();
                    //cell.ColumnSpan = 5;
                    cell.Visible = false;
                    cell.CssClass = "GroupHeaderStyle";
                    cell.Font.Bold = true;
                    row.Cells.Add(cell);


                    cell = new TableCell();
                    cell.Text =  DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString();
                    //cell.ColumnSpan = 5;
                    cell.CssClass = "GroupHeaderStyle";
                    cell.Font.Bold = true;
                    row.Cells.Add(cell);


                    gvReport.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion
                #region Reseting the Sub Total Variables
                dblSubTotalAmount = 0;
                #endregion
            }

        }

        protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "EXPCD").ToString();



                string EXPNM = DataBinder.Eval(e.Row.DataItem, "EXPNM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + "&nbsp;" + "&nbsp;" + "&nbsp;" + EXPNM;

                string Qty = DataBinder.Eval(e.Row.DataItem, "Qty").ToString();
                e.Row.Cells[1].Text = Qty;
                e.Row.Cells[1].Visible = true;
                e.Row.Cells[1].Enabled = true;              

                decimal EXPAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "EXPAMT").ToString());
                string INWORDS = SpellAmount.comma(EXPAMT);
                //e.Row.Cells[3].Text = styleQty;
                e.Row.Cells[2].Text = INWORDS;


                dblSubTotalAmount += EXPAMT;
                dblSubTotalAmountComma = SpellAmount.comma(dblSubTotalAmount);

                dblGrandTotalAmount += EXPAMT;
                dblGrandTotalAmountComma = SpellAmount.comma(dblGrandTotalAmount);


            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Grand Total : ";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].Text = dblGrandTotalAmountComma;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;
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