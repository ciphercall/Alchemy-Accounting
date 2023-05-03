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

namespace AlchemyAccounting.Stock.Report.Report
{
    public partial class rptItemRegister : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        decimal tot_Buy = 0;
        decimal tot_Sale = 0;
        decimal tot_In = 0;
        decimal tot_Out = 0;

        string Tot_BuyComma = "";
        string Tot_SaleComma = "";
        string Tot_InComma = "";
        string Tot_OutComma = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY", lblCompNM);
            Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY", lblAddress);

            DateTime PrintDate = DateTime.Now;
            string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
            lblTime.Text = td;

            string StoreNM = Session["StoreNm"].ToString();
            string StoreID = Session["StoreID"].ToString();
            string ItemNM = Session["ItemNM"].ToString();
            string ItemID = Session["ItemID"].ToString();
            string txtFrom = Session["From"].ToString();
            string txtTo = Session["To"].ToString();

            DateTime FDate = DateTime.Parse(txtFrom, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            lblFDate.Text = FDate.ToString("dd-MMM-yyyy");
            string FdT = FDate.ToString("yyyy/MM/dd");

            DateTime TDate = DateTime.Parse(txtTo, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            lblTDate.Text = TDate.ToString("dd-MMM-yyyy");
            string TdT = TDate.ToString("yyyy/MM/dd");

            lblStNM.Text = StoreNM;
            lblItNM.Text = ItemNM;

            Global.lblAdd(@"SELECT B.CLCQTY FROM (SELECT CATID, ITEMID, (SUM(isnull(INCQTY,0)) + SUM(isnull(BCQTY,0))) - (SUM(isnull(OUTCQTY,0)) + SUM(isnull(SCQTY,0))) AS CLCQTY, SUM(isnull(BCQTY,0)) AS BCQTY, SUM(isnull(BAMT,0)) AS BAMT, SUM(isnull(SCQTY,0)) AS SCQTY, SUM(isnull(SAMT,0)) AS SAMT, " +
                    " (CASE WHEN SUM(isnull(BAMT,0)) = 0.00 THEN 0.00 ELSE CONVERT(decimal(18, 2), (SUM(isnull(BAMT,0))) / SUM(isnull(BCQTY,0))) END) AS AVGRATE FROM (SELECT CATID, ITEMID, SUM(isnull(CQTY,0)) AS BCQTY, SUM(isnull(AMOUNT,0)) AS BAMT, 0 AS SCQTY, 0 AS SAMT, 0 AS INCQTY, 0 AS OUTCQTY FROM STK_TRANS  " +
                    " WHERE (TRANSDT < '" + FdT + "') AND (TRANSTP = 'BUY') AND (STORETO = '" + StoreID + "')AND (ITEMID='" + ItemID + "') GROUP BY CATID, ITEMID UNION " +
                    " SELECT CATID, ITEMID, 0 AS BCQTY, 0 AS BAMT, SUM(isnull(CQTY,0)) AS SQTY, SUM(isnull(AMOUNT,0)) AS SAMT, 0 AS INCQTY, 0 AS OUTCQTY " +
                    " FROM STK_TRANS AS STK_TRANS_1  WHERE (TRANSDT < '" + FdT + "') AND (TRANSTP = 'SALE') AND (STOREFR = '" + StoreID + "')AND ITEMID='" + ItemID + "' GROUP BY CATID, ITEMID UNION " +
                    " SELECT CATID, ITEMID, 0 AS BCQTY, 0 AS BAMT, 0 AS SCQTY, 0 AS SAMT, SUM(isnull(CQTY,0)) AS INCQTY, 0 AS OUTCQTY FROM STK_TRANS AS STK_TRANS_2  " +
                    " WHERE (TRANSDT < '" + FdT + "') AND (TRANSTP = 'ITRF') AND (STORETO = '" + StoreID + "')AND (ITEMID='" + ItemID + "') GROUP BY CATID, ITEMID UNION  " +
                    " SELECT CATID, ITEMID, 0 AS BCQTY, 0 AS BAMT, 0 AS SCQTY, 0 AS SAMT, 0 AS INCQTY, SUM(isnull(CQTY,0)) AS OUTCQTY FROM STK_TRANS AS STK_TRANS_1  " +
                    " WHERE (TRANSDT < '" + FdT + "') AND (TRANSTP = 'ITRF') AND (STOREFR = '" + StoreID + "')AND (ITEMID='" + ItemID + "') GROUP BY CATID, ITEMID) AS A  " +
                    " GROUP BY CATID, ITEMID) AS B INNER JOIN STK_ITEM ON B.CATID = STK_ITEM.CATID AND B.ITEMID = STK_ITEM.ITEMID INNER JOIN STK_ITEMMST ON  STK_ITEM.CATID = STK_ITEMMST.CATID WHERE (B.CLCQTY <> 0)", lblOpenBalance);

            if (lblOpenBalance.Text == "")
            {
                lblOpenBalance.Text = "0.00";
            }
            else
                lblOpenBalance.Text = SpellAmount.comma(Convert.ToDecimal(lblOpenBalance.Text));
            
            showGrid();
        }

        public void showGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string StoreNM = Session["StoreNm"].ToString();
            string StoreID = Session["StoreID"].ToString();
            string ItemNM = Session["ItemNM"].ToString();
            string ItemID = Session["ItemID"].ToString();
            string txtFrom = Session["From"].ToString();
            string txtTo = Session["To"].ToString();

            DateTime FDate = DateTime.Parse(txtFrom, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string FdT = FDate.ToString("yyyy/MM/dd");

            DateTime TDate = DateTime.Parse(txtTo, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TdT = TDate.ToString("yyyy/MM/dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT CONVERT(nvarchar(20), TRANSDT, 103) AS TRANSDT, TRANSMY, TRANSNO, PSID, BUY, SALE, INCQTY, OUTCQTY, LCID, UNIT, ACCOUNTNM, STORENM " +
              " FROM (SELECT B.TRANSDT, B.TRANSMY, B.TRANSNO, B.PSID, B.BUY, B.SALE, B.INCQTY, B.OUTCQTY, B.LCID, B.UNIT, B.ACCOUNTNM, STK_STORE.STORENM " +
                       " FROM (SELECT A.TRANSDT, A.TRANSMY, A.TRANSNO, A.PSID, A.BUY, A.SALE, A.INCQTY, A.OUTCQTY, A.LCID, A.UNIT, GL_ACCHART.ACCOUNTNM " +
                                               " FROM (SELECT TRANSDT, TRANSMY, TRANSNO, PSID, 0 AS BUY, CQTY AS SALE, 0 AS INCQTY, 0 AS OUTCQTY, LCID, NULL AS UNIT " +
                                                                       " FROM          STK_TRANS AS STK_TRANS_4 " +
                                                                       " WHERE      (TRANSTP = 'SALE') AND (STOREFR = '" + StoreID + "') AND (TRANSDT BETWEEN '" + FdT + "' AND '" + TdT + "') AND (ITEMID = '" + ItemID + "') " +
                                                                       " UNION " +
                                                                       " SELECT     TRANSDT, TRANSMY, TRANSNO, PSID, CQTY AS BUY, 0 AS SALE, 0 AS INCQTY, 0 AS OUTCQTY, LCID, NULL AS UNIT " +
                                                                       " FROM         STK_TRANS AS STK_TRANS_3 " +
                                                                       " WHERE     (TRANSTP = 'BUY') AND (STORETO = '" + StoreID + "') AND (TRANSDT BETWEEN '" + FdT + "' AND '" + TdT + "') AND (ITEMID = '" + ItemID + "') " +
                                                                       " UNION " +
                                                                       " SELECT     TRANSDT, TRANSMY, TRANSNO, PSID, 0 AS BUY, 0 AS SALE, CQTY AS INCQTY, 0 AS OUTCQTY, LCID, STOREFR AS UNIT " +
                                                                       " FROM         STK_TRANS AS STK_TRANS_2 " +
                                                                       " WHERE     (TRANSTP = 'ITRF') AND (STORETO = '" + StoreID + "') AND (TRANSDT BETWEEN '" + FdT + "' AND '" + TdT + "') AND (ITEMID = '" + ItemID + "') " +
                                                                       " UNION " +
                                                                        " SELECT     TRANSDT, TRANSMY, TRANSNO, PSID, 0 AS BUY, 0 AS SALE, 0 AS INCQTY, CQTY AS OUTCQTY, LCID, STORETO AS UNIT " +
                                                                       " FROM         STK_TRANS AS STK_TRANS_1 " +
                                                                       " WHERE     (TRANSTP = 'ITRF') AND (STOREFR = '" + StoreID + "') AND (TRANSDT BETWEEN '" + FdT + "' AND '" + TdT + "') AND (ITEMID = '" + ItemID + "')) " +
                                                                      " AS A LEFT OUTER JOIN " +
                                                                      " GL_ACCHART ON A.PSID COLLATE Latin1_General_CI_AS = GL_ACCHART.ACCOUNTCD) AS B LEFT OUTER JOIN " +
                                              " STK_STORE ON B.UNIT COLLATE Latin1_General_CI_AS = STK_STORE.STOREID) AS C", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;

                Balance();
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;
            }
        }

        public void Balance()
        {
            try
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        string Buy = GridView1.Rows[i].Cells[2].Text;
                        decimal BuyQty = Convert.ToDecimal(Buy);
                        string Sale = GridView1.Rows[i].Cells[3].Text;
                        decimal SaleQty = Convert.ToDecimal(Sale);
                        string In = GridView1.Rows[i].Cells[5].Text;
                        decimal InQty = Convert.ToDecimal(In);
                        string Out = GridView1.Rows[i].Cells[6].Text;
                        decimal OutQty = Convert.ToDecimal(Out);
                        decimal OpenBal = Convert.ToDecimal(lblOpenBalance.Text);
                        decimal CumBalance = (OpenBal + BuyQty + InQty) - (SaleQty + OutQty); 
                        GridView1.Rows[i].Cells[7].Text = SpellAmount.comma(CumBalance);
                        GridView1.FooterRow.Cells[7].Text = GridView1.Rows[i].Cells[7].Text;
                    }
                    else
                    {
                        string BlnC = GridView1.Rows[i - 1].Cells[7].Text;
                        decimal CumulativeBalance = decimal.Parse(BlnC);

                        string Buy = GridView1.Rows[i].Cells[2].Text;
                        decimal BuyQty = Convert.ToDecimal(Buy);
                        string Sale = GridView1.Rows[i].Cells[3].Text;
                        decimal SaleQty = Convert.ToDecimal(Sale);
                        string In = GridView1.Rows[i].Cells[5].Text;
                        decimal InQty = Convert.ToDecimal(In);
                        string Out = GridView1.Rows[i].Cells[6].Text;
                        decimal OutQty = Convert.ToDecimal(Out);

                        decimal Balance = (CumulativeBalance + BuyQty + InQty) - (SaleQty+OutQty);
                        GridView1.Rows[i].Cells[7].Text = SpellAmount.comma(Balance);
                        GridView1.FooterRow.Cells[7].Text = GridView1.Rows[i].Cells[7].Text;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string TRANSDT = DataBinder.Eval(e.Row.DataItem, "TRANSDT").ToString();
                e.Row.Cells[0].Text = TRANSDT;
                //tex = DataBinder.Eval(e.Row.DataItem, "PSID").ToString();
                if (DataBinder.Eval(e.Row.DataItem, "PSID").ToString() != null)
                {
                    string PARTICULARS = DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString();
                    e.Row.Cells[1].Text = "&nbsp;" + PARTICULARS;
                }
                else if (DataBinder.Eval(e.Row.DataItem, "UNIT").ToString() != null)
                {
                    string PARTICULARS = DataBinder.Eval(e.Row.DataItem, "STORENM").ToString();
                    e.Row.Cells[1].Text = "&nbsp;" + PARTICULARS;
                }

                //else if (DataBinder.Eval(e.Row.DataItem, "LCID").ToString() != null)
                //{
                //    string PARTICULARS = DataBinder.Eval(e.Row.DataItem, "").ToString();
                //    e.Row.Cells[1].Text = "&nbsp;" + PARTICULARS;
                //}
           
                decimal BUY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BUY").ToString());
                string bQty = SpellAmount.comma(BUY);
                e.Row.Cells[2].Text = bQty;

                decimal SALE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SALE").ToString());
                string sQty = SpellAmount.comma(SALE);
                e.Row.Cells[3].Text = sQty;

                decimal INQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "INCQTY").ToString());
                string iQty = SpellAmount.comma(INQTY);
                e.Row.Cells[5].Text = iQty;

                decimal OUTQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OUTCQTY").ToString());
                string oQty = SpellAmount.comma(OUTQTY);
                e.Row.Cells[6].Text = oQty;

                tot_Buy += BUY;
                Tot_BuyComma = SpellAmount.comma(tot_Buy);
                
                tot_Sale += SALE;
                Tot_SaleComma = SpellAmount.comma(tot_Sale);

                tot_In += INQTY;
                Tot_InComma = SpellAmount.comma(tot_In);

                tot_Out += OUTQTY;
                Tot_OutComma = SpellAmount.comma(tot_Out);
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "TOTAL :";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].Text = Tot_BuyComma;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = Tot_SaleComma;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = Tot_InComma;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].Text = Tot_OutComma;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;
            }

            ShowHeader(GridView1);
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