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

namespace AlchemyAccounting.LC.Report.Report
{
    public partial class rptLCWiseExpense : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        // To keep track of the previous row Group Identifier    
        string strPreviousRowID = string.Empty;

        // To keep track the Index of Group Total    
        int intSubTotalIndex = 1;

        // To temporarily store Sub Total    
        decimal dblSubTotalAMT = 0;

        // To temporarily store Grand Total    
        decimal dblGrandTotalAMT = 0;

        string dblSubTotalAMTComma = "";

        string dblGrandTotalAMTComma = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY", lblCompNM);
                Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY", lblAddress);

                DateTime PrintDate = DateTime.Now;
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblTime.Text = td;

                string lcCode =  Session["LCCD"].ToString();
                string lcID =  Session["LCID"].ToString();

                lblLCID.Text = lcID;

                showData();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public void showData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string lcCode = Session["LCCD"].ToString();

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT LC_CHARGE.CHARGETP, LC_CHARGE.CHARGENM, CONVERT(NVARCHAR(20),LC_EXPENSE.TRANSDT,103)AS TRANSDT, LC_EXPENSE.TRANSMY, LC_EXPENSE.TRANSNO, LC_EXPENSE.LCCD, LC_EXPENSE.LCINVNO, " +
                                            " LC_EXPENSE.CHARGEID, LC_EXPENSE.AMOUNT, LC_EXPENSE.CNBCD, GL_ACCHART.ACCOUNTNM FROM LC_EXPENSE INNER JOIN LC_CHARGE ON LC_EXPENSE.CHARGEID = LC_CHARGE.CHARGEID INNER JOIN " +
                                            " GL_ACCHART ON LC_EXPENSE.CNBCD = GL_ACCHART.ACCOUNTCD WHERE LCCD = '" + lcCode + "' GROUP BY LC_CHARGE.CHARGETP, LC_CHARGE.CHARGENM, CONVERT(NVARCHAR(20),LC_EXPENSE.TRANSDT,103), LC_EXPENSE.TRANSMY, LC_EXPENSE.TRANSNO, LC_EXPENSE.LCCD, LC_EXPENSE.LCINVNO, " +
                                            " LC_EXPENSE.CHARGEID, LC_EXPENSE.AMOUNT, LC_EXPENSE.CNBCD, GL_ACCHART.ACCOUNTNM", conn);

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
                GridView1.Visible = false;
            }
        }

        /// <summary>   
        /// /// Event fires for every row creation   
        /// /// Used for creating SubTotal row when next group starts by adding Group Total at previous row manually    
        /// </summary>    /// <param name="sender"></param>    /// <param name="e"></param>   
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsSubTotalRowNeedToAdd = false;
            bool IsGrandTotalRowNeedtoAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "CHARGEID") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "CHARGEID").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "CHARGEID") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;
            }
            //#region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "CHARGEID") != null))
            {
                GridView GridView1 = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "&nbsp;" + DataBinder.Eval(e.Row.DataItem, "CHARGETP").ToString(); //////// Sub Header Name
                cell.ColumnSpan = 4;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);
                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
            }
            //#endregion
            if (IsSubTotalRowNeedToAdd)
            {
                //#region Adding Sub Total Row
                GridView GridView1 = (GridView)sender;
                // Creating a Row          
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell          
                TableCell cell = new TableCell();
                cell.Text = "Sub Total : ";
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.ColumnSpan = 3;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding AMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalAMTComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);
                //Adding the Row at the RowIndex position in the Grid      
                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                //#endregion
                //#region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "CHARGEID") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = "&nbsp;" + DataBinder.Eval(e.Row.DataItem, "CHARGETP").ToString(); //////// Sub Header Name
                    cell.ColumnSpan = 3;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                //#endregion
                //#region Reseting the Sub Total Variables
                dblSubTotalAMT = 0;
                //#endregion
            }
            if (IsGrandTotalRowNeedtoAdd)
            {
                //#region Grand Total Row
                GridView GridView1 = (GridView)sender;
                // Creating a Row      
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell           
                TableCell cell = new TableCell();
                cell.Text = "Grand Total : ";
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.ColumnSpan = 3;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding AMT Column          
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblGrandTotalAMTComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);
                //Adding the Row at the RowIndex position in the Grid     
                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
                //#endregion
            }
        }

        /// <summary>    
        /// Event fires when data binds to each row   
        /// Used for calculating Group Total     
        /// </summary>   
        /// /// <param name="sender"></param>    
        /// <param name="e"></param>    
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // This is for cumulating the values       
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "CHARGEID").ToString();


                string TransDate = DataBinder.Eval(e.Row.DataItem, "TRANSDT").ToString();
                DateTime trDT = DateTime.Parse(TransDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TransDT = trDT.ToString("dd-MMM-yyyy");
                e.Row.Cells[0].Text = TransDT;

                string chrgNM = DataBinder.Eval(e.Row.DataItem, "CHARGENM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + chrgNM;

                string ACCOUNTNM = DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + ACCOUNTNM;

                decimal Amount = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());
                string AMT = SpellAmount.comma(Amount);
                e.Row.Cells[3].Text = AMT;

                decimal dblAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());

                // Cumulating Sub Total            
                dblSubTotalAMT += dblAMT;
                dblSubTotalAMTComma = SpellAmount.comma(dblSubTotalAMT);


                // Cumulating Grand Total           
                dblGrandTotalAMT += dblAMT;
                dblGrandTotalAMTComma = SpellAmount.comma(dblGrandTotalAMT);
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