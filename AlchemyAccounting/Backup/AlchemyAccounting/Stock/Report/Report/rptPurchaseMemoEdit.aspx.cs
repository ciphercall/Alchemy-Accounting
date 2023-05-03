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
    public partial class rptPurchaseMemoEdit : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY", lblCompNM);
                Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY", lblAddress);
                Global.lblAdd(@"SELECT CONTACTNO FROM ASL_COMPANY", lblContact);

                DateTime PrintDate = DateTime.Now;
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblTime.Text = td;

                string InvDate_P = Session["InvDate_P"].ToString();
                string InvNoEdit_P = Session["InvNoEdit_P"].ToString();
                string Memo_P = Session["Memo_P"].ToString();
                string StoreNM_P = Session["StoreNM_P"].ToString();
                string StoreID_P = Session["StoreID_P"].ToString();
                string PartyNM_P = Session["PartyNM_P"].ToString();
                string PartyID_P = Session["PartyID_P"].ToString();

                DateTime InDT = DateTime.Parse(InvDate_P, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lblInVDT.Text = InDT.ToString("dd-MMM-yyyy");
                lblInVNo.Text = InvNoEdit_P;
                lblSalesMemoNo.Text = Memo_P;
                lblPurchaseFrom.Text = PartyNM_P;
                //lblPurchaseFor.Text = StoreNM_P;
                Global.lblAdd(@"SELECT ADDRESS FROM STK_PS WHERE PSID='" + PartyID_P + "' ", lblPurchaseAdd);
                showGrid();

                lblInWords.Text = "";
                decimal dec;
                decimal parseAmount = decimal.Parse(lblTotAmount.Text);
                string lblAmount = parseAmount.ToString();
                Boolean ValidInput = Decimal.TryParse(lblTotAmount.Text, out dec);
                if (!ValidInput)
                {
                    lblInWords.ForeColor = System.Drawing.Color.Red;
                    lblInWords.Text = "Enter the Proper Amount...";
                    return;
                }
                if (lblTotAmount.Text.ToString().Trim() == "")
                {
                    lblInWords.ForeColor = System.Drawing.Color.Red;
                    lblInWords.Text = "Amount Cannot Be Empty...";
                    return;
                }
                else
                {
                    if (Convert.ToDecimal(lblTotAmount.Text) == 0)
                    {
                        lblInWords.ForeColor = System.Drawing.Color.Red;
                        lblInWords.Text = "Amount Cannot Be Empty...";
                        return;
                    }
                }

                string x1 = "";
                string x2 = "";

                if (lblAmount.Contains("."))
                {
                    x1 = lblAmount.Trim().Substring(0, lblAmount.Trim().IndexOf("."));
                    x2 = lblAmount.Trim().Substring(lblAmount.Trim().IndexOf(".") + 1);
                }
                else
                {
                    x1 = lblAmount.Trim();
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

                lblAmount = x1 + "." + x2;

                if (x2.Length > 2)
                {
                    lblAmount = Math.Round(Convert.ToDouble(lblAmount), 2).ToString().Trim();
                }

                string AmtConv = SpellAmount.MoneyConvFn(lblAmount.Trim());

                lblInWords.Text = AmtConv.Trim();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public void showGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string InvDate_P = Session["InvDate_P"].ToString();
            string InvNoEdit_P = Session["InvNoEdit_P"].ToString();
            string Memo_P = Session["Memo_P"].ToString();
            string StoreNM_P = Session["StoreNM_P"].ToString();
            string StoreID_P = Session["StoreID_P"].ToString();
            string PartyNM_P = Session["PartyNM_P"].ToString();
            string PartyID_P = Session["PartyID_P"].ToString();

            DateTime InDT = DateTime.Parse(InvDate_P, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string inDate = InDT.ToString("yyyy/MM/dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT ROW_NUMBER() over (order by STK_TRANS.TRANSSL) as SL, dbo.STK_ITEMMST.CATNM, dbo.STK_ITEM.ITEMNM, " +
                             " dbo.GL_ACCHART.ACCOUNTNM, dbo.STK_STORE.STORENM, dbo.STK_TRANS.TRANSTP, dbo.STK_TRANS.TRANSDT, dbo.STK_TRANS.TRANSMY, " +
                             " dbo.STK_TRANS.TRANSNO, dbo.STK_TRANS.INVREFNO, dbo.STK_TRANS.STOREFR, dbo.STK_TRANS.STORETO, dbo.STK_TRANS.PSID, dbo.STK_TRANS.LCTP, " +
                             " dbo.STK_TRANS.LCID, dbo.STK_TRANS.LCDATE, dbo.STK_TRANS.REMARKS, dbo.STK_TRANS.TRANSSL, dbo.STK_TRANS.CATID, dbo.STK_TRANS.ITEMID, " +
                             " dbo.STK_TRANS.UNITTP, dbo.STK_TRANS.CPQTY, dbo.STK_TRANS.CQTY, dbo.STK_TRANS.PQTY, dbo.STK_TRANS.QTY, dbo.STK_TRANS.RATE, dbo.STK_TRANS.AMOUNT, " +
                             " dbo.STK_TRANS.USERPC, dbo.STK_TRANS.USERID, dbo.STK_TRANS.ACTDTI, dbo.STK_TRANS.INTIME, dbo.STK_TRANS.IPADDRESS FROM dbo.STK_TRANS INNER JOIN " +
                             " dbo.STK_ITEM ON dbo.STK_TRANS.CATID = dbo.STK_ITEM.CATID AND dbo.STK_TRANS.ITEMID = dbo.STK_ITEM.ITEMID INNER JOIN " +
                             " dbo.STK_ITEMMST ON dbo.STK_ITEM.CATID = dbo.STK_ITEMMST.CATID INNER JOIN " +
                             " dbo.STK_STORE ON dbo.STK_TRANS.STORETO COLLATE Latin1_General_CI_AS = dbo.STK_STORE.STOREID LEFT OUTER JOIN " +
                             " dbo.GL_ACCHART ON dbo.STK_TRANS.PSID = dbo.GL_ACCHART.ACCOUNTCD " +
                             " WHERE (dbo.STK_TRANS.TRANSTP = 'BUY') AND (dbo.STK_TRANS.TRANSDT = '" + inDate + "') AND (dbo.STK_TRANS.TRANSNO = '" + InvNoEdit_P + "') AND " +
                             " (dbo.STK_TRANS.PSID = '" + PartyID_P + "') AND (dbo.STK_TRANS.STORETO = '" + StoreID_P + "') ORDER BY dbo.STK_TRANS.TRANSSL", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;

                Decimal totQty = 0;
                Decimal a = 0;
                foreach (GridViewRow grid in GridView1.Rows)
                {
                    string Debit = grid.Cells[3].Text;
                    totQty = Convert.ToDecimal(Debit);
                    a += totQty;
                    decimal tot = a;
                    lblTotQTy.Text = tot.ToString();
                }

                Decimal totCqty = 0;
                Decimal c = 0;
                foreach (GridViewRow grid in GridView1.Rows)
                {
                    Label lblCarton = (Label)grid.FindControl("lblCarton");
                    totCqty = decimal.Parse(lblCarton.Text);
                    c += totCqty;
                    decimal tot = c;
                    lblTotCQTy.Text = SpellAmount.comma(tot);
                }

                Decimal totAmount = 0;
                Decimal b = 0;
                foreach (GridViewRow grid in GridView1.Rows)
                {
                    string Amnt = grid.Cells[5].Text;
                    totAmount = decimal.Parse(Amnt);
                    b += totAmount;
                    decimal tot = b;
                    lblTotAmount.Text = SpellAmount.comma(tot);
                }
            }
            else
            {

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal amNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());
                string AMOUNT = SpellAmount.comma(amNT);
                e.Row.Cells[5].Text = AMOUNT;
            }
        }

    }
}