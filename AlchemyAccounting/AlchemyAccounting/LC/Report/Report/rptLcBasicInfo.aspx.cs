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
    public partial class rptLcBasicInfo : System.Web.UI.Page
    {
        decimal dblQTY=0;
        decimal dblGrandTotalAMT = 0;
        
        string dblGrandTotalQtyComma = "0";
        string dblGrandTotalAMTComma="0";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY", lblCompNM);
                Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY", lblAddress);

                DateTime PrintDate = DateTime.Now;
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblTime.Text = td;

                string lcCode = Session["LCCD"].ToString();
                string lcID = Session["LCID"].ToString();

                lblLCNM.Text = lcID;

                Global.lblAdd(@"SELECT CONVERT(NVARCHAR(20),LCDT,103)AS LCDT FROM LC_BASIC where LCNO= '" + lcID + "'", lblLCDt);
                Global.lblAdd(@"SELECT IMPORTERNM FROM LC_BASIC where LCNO= '" + lcID + "'", lblImporNM);
                Global.lblAdd(@"SELECT BENEFICIARY FROM LC_BASIC where LCNO= '" + lcID + "'", lblBeneficiary);
                Global.lblAdd(@"SELECT MCNM FROM LC_BASIC where LCNO= '" + lcID + "'", lblMCNM);
                Global.lblAdd(@"SELECT MCNO FROM LC_BASIC where LCNO= '" + lcID + "'", lblMCNO);
                Global.lblAdd(@"SELECT (CASE WHEN MCDT='1900-01-01 00:00:00' THEN '' ELSE CONVERT(NVARCHAR(20),MCDT,103) END) AS MCDT FROM LC_BASIC where LCNO= '" + lcID + "'", lblMcDT);
                Global.lblAdd(@"SELECT SCPINO FROM LC_BASIC where LCNO= '" + lcID + "'", lblScpiNo);
                Global.lblAdd(@"SELECT (CASE WHEN SCPIDT='1900-01-01 00:00:00' THEN '' ELSE CONVERT(NVARCHAR(20),SCPIDT,103) END) AS SCPIDT FROM LC_BASIC where LCNO= '" + lcID + "'", lblScpiDT);
                Global.lblAdd(@"SELECT MPINO FROM LC_BASIC where LCNO= '" + lcID + "'", lblMpiNo);
                Global.lblAdd(@"SELECT (CASE WHEN MPIDT='1900-01-01 00:00:00' THEN '' ELSE CONVERT(NVARCHAR(20),MPIDT,103) END) AS MPIDT FROM LC_BASIC where LCNO= '" + lcID + "'", lblMpiDT);
                Global.lblAdd(@"SELECT LCVUSD FROM LC_BASIC where LCNO= '" + lcID + "'", lblLcUSD);
                Global.lblAdd(@"SELECT LCVERT FROM LC_BASIC where LCNO= '" + lcID + "'", lblLcRT);
                Global.lblAdd(@"SELECT LCVBDT FROM LC_BASIC where LCNO= '" + lcID + "'", lblBDT);
                Global.lblAdd(@"SELECT REMARKS FROM LC_BASIC where LCNO= '" + lcID + "'", lblRemarks);
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
            SqlCommand cmd = new SqlCommand(" SELECT ROW_NUMBER() over (order by LC_ITEM.ITEMID) as SL,LC_ITEM.ITEMID, SUM(LC_ITEM.QTY) AS QTY, SUM(LC_ITEM.AMOUNT) AS AMOUNT, STK_ITEM.ITEMNM " +
                                            " FROM LC_ITEM INNER JOIN STK_ITEM ON LC_ITEM.ITEMID = STK_ITEM.ITEMID WHERE (LC_ITEM.LCID = '" + lcCode + "') " +
                                            " GROUP BY LC_ITEM.ITEMID, STK_ITEM.ITEMNM ORDER BY LC_ITEM.ITEMID", conn);

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
 
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {     
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string SL = DataBinder.Eval(e.Row.DataItem, "SL").ToString();
                e.Row.Cells[0].Text = SL;

                string ITEMNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + ITEMNM;

                decimal QTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTY").ToString());
                string qt = SpellAmount.comma(QTY);
                e.Row.Cells[2].Text = qt;

                decimal AMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());
                string AMT = SpellAmount.comma(AMOUNT);
                e.Row.Cells[3].Text = AMT;

                decimal dblAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());

                // Cumulating Grand Total  
                dblQTY += QTY;
                dblGrandTotalQtyComma = SpellAmount.comma(dblQTY);

                dblGrandTotalAMT += dblAMT;
                dblGrandTotalAMTComma = SpellAmount.comma(dblGrandTotalAMT);
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Grand Total :   ";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[2].Text = dblGrandTotalQtyComma;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[3].Text = dblGrandTotalAMTComma;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
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