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
    public partial class rpt_jobtypw_wise : System.Web.UI.Page
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
                Response.Redirect("~/Login/UI/Login.aspx");
            }

            else
            {
                if (!IsPostBack)
                {
                    Global.lblAdd("SELECT DISTINCT COMPNM FROM ASL_COMPANY", lblCompanyNM);
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

            string jobyr = Session["year"].ToString();
            string jobtype = Session["jobtype"].ToString();

            lbljobyy.Text = jobyr;
            lbljobtype.Text = jobtype;


            SqlCommand cmd = new SqlCommand("SELECT CNF_JOB.JOBNO, GL_ACCHART.ACCOUNTNM,CNF_JOB.REGID, CONVERT(NVARCHAR(20), CNF_JOB.JOBCDT,103) AS JOBCDT, CNF_JOB.GOODS, CNF_JOB.PKGS, BENO , CONVERT(NVARCHAR,BEDT,103) BEDT,  CNF_JOB.PERMITNO, CNF_JOB.DOCINVNO,CNF_JOB.CNFV_USD, " +
                     " ASL_COMPANY.COMPNM, (CASE WHEN CNF_JOB.DOCRCVDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20), CNF_JOB.DOCRCVDT, 103) END) AS DOCRCVDT, (CASE WHEN CNF_JOB.PERMITDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20), CNF_JOB.PERMITDT, 103) END) AS PERMITDT, ASL_COMPANY.BRANCHID " +
                        " FROM  CNF_JOB INNER JOIN " +
                     " ASL_COMPANY ON CNF_JOB.COMPID = ASL_COMPANY.COMPID INNER JOIN " +
                     " GL_ACCHART ON CNF_JOB.PARTYID = GL_ACCHART.ACCOUNTCD " +
                     " WHERE CNF_JOB.JOBYY='" + jobyr + "' AND CNF_JOB.JOBTP='" + jobtype + "' ORDER BY ASL_COMPANY.BRANCHID ", conn);

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
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "BRANCHID") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "BRANCHID").ToString())

                    IsSubTotalRowNeedToAdd = true;


            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "BRANCHID") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                //IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;

            }

            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "BRANCHID") != null))
            {
                GridView gvReport = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = DataBinder.Eval(e.Row.DataItem, "BRANCHID").ToString();
                cell.ColumnSpan = 12;
                cell.Visible = true;
                cell.Height = 2;
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
                //cell.Text = "Sub Total : ";
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.ColumnSpan = 2;
                //cell.Font.Bold = true;
                //cell.CssClass = "SubTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding Amount Column         
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", dblSubTotalAmountComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.Font.Bold = true;
                //cell.CssClass = "SubTotalRowStyle";
                //row.Cells.Add(cell);


                //Adding the Row at the RowIndex position in the Grid      
                gvReport.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                #endregion
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "BRANCHID") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = DataBinder.Eval(e.Row.DataItem, "BRANCHID").ToString();
                    cell.ColumnSpan = 12;
                    cell.Visible = true;
                    cell.Height = 2;
                    cell.CssClass = "GroupHeaderStyle";
                    cell.Font.Bold = true;
                    row.Cells.Add(cell);


                    //cell = new TableCell();
                    //cell.Text = DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString();
                    ////cell.ColumnSpan = 5;
                    //cell.CssClass = "GroupHeaderStyle";
                    //cell.Font.Bold = true;
                    //row.Cells.Add(cell);


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
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "BRANCHID").ToString();

                string JOBNO = DataBinder.Eval(e.Row.DataItem, "JOBNO").ToString();
                e.Row.Cells[0].Text = JOBNO;

                string JOBYY = DataBinder.Eval(e.Row.DataItem, "JOBCDT").ToString();
                e.Row.Cells[1].Text = JOBYY;

                string COMPNM = DataBinder.Eval(e.Row.DataItem, "COMPNM").ToString();
                e.Row.Cells[2].Text = COMPNM;

                string REGID = DataBinder.Eval(e.Row.DataItem, "REGID").ToString();
                e.Row.Cells[3].Text = REGID;



                string ACCOUNTNM = DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString();
                e.Row.Cells[4].Text = "&nbsp;" + "&nbsp;" + "&nbsp;" + "&nbsp;" + ACCOUNTNM;


                string GOODS = DataBinder.Eval(e.Row.DataItem, "GOODS").ToString();
                e.Row.Cells[5].Text = GOODS;

                string PKGS = DataBinder.Eval(e.Row.DataItem, "PKGS").ToString();
                e.Row.Cells[6].Text = PKGS;

                string PERMITNO = DataBinder.Eval(e.Row.DataItem, "PERMITNO").ToString();
                e.Row.Cells[7].Text = PERMITNO;

                string PERMITDT = DataBinder.Eval(e.Row.DataItem, "PERMITDT").ToString();
                e.Row.Cells[8].Text = PERMITDT;

                string DOCINVNO = DataBinder.Eval(e.Row.DataItem, "DOCINVNO").ToString();
                e.Row.Cells[9].Text = DOCINVNO;

                string DOCRCVDT = DataBinder.Eval(e.Row.DataItem, "DOCRCVDT").ToString();
                e.Row.Cells[10].Text = DOCRCVDT;

                ShowHeader(gvReport);
            }
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