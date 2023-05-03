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

namespace AlchemyAccounting.Stock.UI
{
    public partial class StoreTrans : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                //if (!this.IsPostBack)
                //{
                if (IsPostBack)
                {
                    //if (TabContainer1.ActiveTabIndex == 0)
                    //{
                    //    DateTime transdate = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    //    string TrDate = transdate.ToString("yyyy/MM/dd");

                    //    string month = transdate.ToString("MMM").ToUpper();
                    //    string years = transdate.ToString("yy");
                    //    lblSMY.Text = month + "-" + years;
                    //    Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSDT='" + TrDate + "' and TRANSMY='" + lblSMY.Text + "' and TRANSTP = 'SALE'", lblSMxNo);
                    //    if (lblSMxNo.Text == "")
                    //    {
                    //        txtInNo.Text = "1";
                    //    }
                    //    else
                    //    {
                    //        int iNo = int.Parse(lblSMxNo.Text);
                    //        int totIno = iNo + 1;
                    //        txtInNo.Text = totIno.ToString();
                    //    }
                    //    //GridShow();

                    //    ddlSalesEditInNo.AutoPostBack = true;
                    //    ddlSalesEditInNo.Focus();

                    //}
                }
                else
                {
                    //TabContainer1.AutoPostBack = false;
                    try
                    {
                        Start();
                        Purchase_Start();
                        Transfer_Start();
                        Return_Start();
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                }
                //}
                //else
                //    TabContainer1.AutoPostBack = false;
            }
        }


        public void Start()
        {
            //if (TabContainer1.ActiveTabIndex == 0)
            //{
            DateTime today = DateTime.Today.Date;
            string td = Global.Dayformat(today);
            txtInDT.Text = td;
            DateTime transdate = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDate = transdate.ToString("yyyy/MM/dd");

            string mon = DateTime.Today.Date.ToString("MMM").ToUpper();
            string varYear = today.ToString("yyyy");
            string year = today.ToString("yy");
            //lblSMY.Text = mon + "-" + year;
            lblSMY.Text = varYear;
            Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANSMST where TRANSMY='" + lblSMY.Text + "' and TRANSTP = 'SALE'", lblSMxNo);
            if (lblSMxNo.Text == "")
            {
                txtInNo.Text = "1";
            }
            else
            {
                int iNo = int.Parse(lblSMxNo.Text);
                int totIno = iNo + 1;
                txtInNo.Text = totIno.ToString();
            }

            txtSLMNo.Focus();

            GridShow();
            //}
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListStore(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT STORENM FROM STK_STORE WHERE STORENM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["STORENM"].ToString());
            return CompletionSet.ToArray();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListParty(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,5) IN ('10202') and ACCOUNTNM LIKE '" + prefixText + "%' AND STATUSCD = 'P'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void GridShow()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            DateTime transdate = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = transdate.ToString("yyyy/MM/dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT STK_ITEM.ITEMNM, STK_TRANS.TRANSTP, STK_TRANS.TRANSDT, STK_TRANS.TRANSMY, STK_TRANS.TRANSNO, STK_TRANS.INVREFNO, STK_TRANS.STOREFR, " +
                                            " STK_TRANS.STORETO, STK_TRANS.PSID, STK_TRANS.LCTP, STK_TRANS.LCID, STK_TRANS.LCDATE, STK_TRANS.REMARKS, STK_TRANS.TRANSSL, STK_TRANS.CATID, STK_TRANS.ITEMID, STK_TRANS.UNITTP, STK_TRANS.CPQTY, STK_TRANS.CQTY, STK_TRANS.PQTY, STK_TRANS.QTY, STK_TRANS.RATE, " +
                                            " STK_TRANS.AMOUNT, STK_TRANS.DISCRT, STK_TRANS.DISCAMT, STK_TRANS.NETAMT, STK_TRANS.USERPC, STK_TRANS.USERID, STK_TRANS.ACTDTI, STK_TRANS.INTIME, STK_TRANS.IPADDRESS, STK_STORE.STORENM " +
                                            " FROM STK_ITEM INNER JOIN STK_TRANS ON STK_ITEM.ITEMID = STK_TRANS.ITEMID INNER JOIN STK_STORE ON STK_TRANS.STOREFR = STK_STORE.STOREID " +
                                            " WHERE     (STK_TRANS.TRANSTP = 'SALE') and STK_TRANS.TRANSDT = '" + TrDt + "' and STK_TRANS.TRANSMY = '" + lblSMY.Text + "' and STK_TRANS.TRANSNO = '" + txtInNo.Text + "' order by TRANSSL", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetail.DataSource = ds;
                gvDetail.DataBind();

                if (gvDetail.EditIndex == -1)
                {
                    decimal totCar = 0;
                    decimal totQuan = 0;
                    Decimal totAmt = 0;
                    Decimal a = 0;
                    decimal b = 0;
                    decimal c = 0;
                    decimal dis = 0;
                    decimal disAmt = 0;
                    decimal net = 0;
                    decimal netAmt = 0;
                    foreach (GridViewRow grid in gvDetail.Rows)
                    {
                        Label lblCQty = (Label)grid.Cells[4].FindControl("lblCQty");
                        Label lblQty = (Label)grid.Cells[6].FindControl("lblQty");
                        Label Per = (Label)grid.Cells[8].FindControl("lblAmount");
                        Label lblDisAmt = (Label)grid.Cells[10].FindControl("lblDisAmt");
                        Label lblNetAmt = (Label)grid.Cells[11].FindControl("lblNetAmt");

                        if (lblCQty.Text == "")
                        {
                            lblCQty.Text = "0.00";
                        }
                        else
                        {
                            lblCQty.Text = lblCQty.Text;
                        }
                        String cQty = lblCQty.Text;
                        totCar = Convert.ToDecimal(cQty);
                        b += totCar;
                        string tCqty = SpellAmount.comma(b);
                        txtTCarton.Text = tCqty;

                        if (lblQty.Text == "")
                        {
                            lblQty.Text = "0.00";
                        }
                        else
                        {
                            lblQty.Text = lblQty.Text;
                        }
                        String Qty = lblQty.Text;
                        totQuan = Convert.ToDecimal(Qty);
                        c += totQuan;
                        string tQty = SpellAmount.comma(c);
                        txtTQuantity.Text = tQty;


                        if (Per.Text == "")
                        {
                            Per.Text = "0.00";
                        }
                        else
                        {
                            Per.Text = Per.Text;
                        }
                        String Perf = Per.Text;
                        totAmt = Convert.ToDecimal(Perf);
                        a += totAmt;
                        string tAmount = SpellAmount.comma(a);
                        txtTAmount.Text = tAmount;

                        if (lblDisAmt.Text == "")
                        {
                            lblDisAmt.Text = "0.00";
                        }
                        else
                            lblDisAmt.Text = lblDisAmt.Text;

                        dis = Convert.ToDecimal(lblDisAmt.Text);
                        disAmt += dis;
                        string disCount = SpellAmount.comma(disAmt);
                        txtTDisAmount.Text = disCount;

                        if (lblNetAmt.Text == "")
                        {
                            lblNetAmt.Text = "0.00";
                        }
                        else
                            lblNetAmt.Text = lblNetAmt.Text;

                        net = Convert.ToDecimal(lblNetAmt.Text);
                        netAmt += net;
                        string nAmount = SpellAmount.comma(netAmt);
                        txtTotal.Text = nAmount;

                        //txtTotAmt.Text = nAmount;
                        //txtGrossDisAmt.Text = "0.00";
                        //txtNetAmt.Text = nAmount;
                    }
                    a += totAmt;
                    disAmt += dis;
                    netAmt += net;
                }
                else
                {

                }

                //Global.txtAdd("SELECT MAX(TRANSNO) AS TRANSNO FROM STK_TRANSMST WHERE TRANSMY='" + lblSMY.Text + "' AND TRANSTP='SALE'",txtInNo);
                TextBox txtStoreNM = (TextBox)gvDetail.FooterRow.FindControl("txtStoreNM");
                txtStoreNM.Focus();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvDetail.DataSource = ds;
                gvDetail.DataBind();
                int columncount = gvDetail.Rows[0].Cells.Count;
                gvDetail.Rows[0].Cells.Clear();
                gvDetail.Rows[0].Cells.Add(new TableCell());
                gvDetail.Rows[0].Cells[0].ColumnSpan = columncount;
                gvDetail.Rows[0].Cells[0].Text = "No Records Found";
                gvDetail.Rows[0].Visible = false;
            }
        }

        protected void GridShow_Complete()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            DateTime transdate = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = transdate.ToString("yyyy/MM/dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT STK_ITEM.ITEMNM, STK_TRANS.TRANSTP, STK_TRANS.TRANSDT, STK_TRANS.TRANSMY, STK_TRANS.TRANSNO, STK_TRANS.INVREFNO, STK_TRANS.STOREFR, " +
                                            " STK_TRANS.STORETO, STK_TRANS.PSID, STK_TRANS.LCTP, STK_TRANS.LCID, STK_TRANS.LCDATE, STK_TRANS.REMARKS, STK_TRANS.TRANSSL, STK_TRANS.CATID, STK_TRANS.ITEMID, STK_TRANS.UNITTP, STK_TRANS.CPQTY, STK_TRANS.CQTY, STK_TRANS.PQTY, STK_TRANS.QTY, STK_TRANS.RATE, " +
                                            " STK_TRANS.AMOUNT, STK_TRANS.DISCRT, STK_TRANS.DISCAMT, STK_TRANS.NETAMT, STK_TRANS.USERPC, STK_TRANS.USERID, STK_TRANS.ACTDTI, STK_TRANS.INTIME, STK_TRANS.IPADDRESS, STK_STORE.STORENM " +
                                            " FROM STK_ITEM INNER JOIN STK_TRANS ON STK_ITEM.ITEMID = STK_TRANS.ITEMID INNER JOIN STK_STORE ON STK_TRANS.STOREFR = STK_STORE.STOREID " +
                                            " WHERE     (STK_TRANS.TRANSTP = 'SALE') and STK_TRANS.TRANSDT = '" + TrDt + "' and STK_TRANS.TRANSMY = '" + lblSMY.Text + "' and STK_TRANS.TRANSNO = '" + txtInNo.Text + "' order by TRANSSL", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetail.DataSource = ds;
                gvDetail.DataBind();
                gvDetail.FooterRow.Visible = false;

                if (gvDetail.EditIndex == -1)
                {
                    decimal totCar = 0;
                    decimal totQuan = 0;
                    Decimal totAmt = 0;
                    Decimal a = 0;
                    decimal b = 0;
                    decimal c = 0;
                    foreach (GridViewRow grid in gvDetail.Rows)
                    {
                        Label lblCQty = (Label)grid.Cells[4].FindControl("lblCQty");
                        Label lblQty = (Label)grid.Cells[6].FindControl("lblQty");
                        Label Per = (Label)grid.Cells[8].FindControl("lblAmount");

                        if (lblCQty.Text == "")
                        {
                            lblCQty.Text = "0.00";
                        }
                        else
                        {
                            lblCQty.Text = lblCQty.Text;
                        }
                        String cQty = lblCQty.Text;
                        totCar = Convert.ToDecimal(cQty);
                        b += totCar;
                        string tCqty = SpellAmount.comma(b);
                        txtTCarton.Text = tCqty;

                        if (lblQty.Text == "")
                        {
                            lblQty.Text = "0.00";
                        }
                        else
                        {
                            lblQty.Text = lblQty.Text;
                        }
                        String Qty = lblQty.Text;
                        totQuan = Convert.ToDecimal(Qty);
                        c += totQuan;
                        string tQty = SpellAmount.comma(c);
                        txtTQuantity.Text = tQty;


                        if (Per.Text == "")
                        {
                            Per.Text = "0.00";
                        }
                        else
                        {
                            Per.Text = Per.Text;
                        }
                        String Perf = Per.Text;
                        totAmt = Convert.ToDecimal(Perf);
                        a += totAmt;
                        string tAmount = SpellAmount.comma(a);
                        txtTAmount.Text = tAmount;

                        //txtTotAmt.Text = nAmount;
                        //txtGrossDisAmt.Text = "0.00";
                        //txtNetAmt.Text = nAmount;
                    }
                    a += totAmt;
                }
            }
            else
            {

            }
        }

        protected void GridShow_CompleteEdit()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            DateTime transdate = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = transdate.ToString("yyyy/MM/dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT STK_ITEM.ITEMNM, STK_TRANS.TRANSTP, STK_TRANS.TRANSDT, STK_TRANS.TRANSMY, STK_TRANS.TRANSNO, STK_TRANS.INVREFNO, STK_TRANS.STOREFR, " +
                                            " STK_TRANS.STORETO, STK_TRANS.PSID, STK_TRANS.LCTP, STK_TRANS.LCID, STK_TRANS.LCDATE, STK_TRANS.REMARKS, STK_TRANS.TRANSSL, STK_TRANS.CATID, STK_TRANS.ITEMID, STK_TRANS.UNITTP, STK_TRANS.CPQTY, STK_TRANS.CQTY, STK_TRANS.PQTY, STK_TRANS.QTY, STK_TRANS.RATE, " +
                                            " STK_TRANS.AMOUNT, STK_TRANS.DISCRT, STK_TRANS.DISCAMT, STK_TRANS.NETAMT, STK_TRANS.USERPC, STK_TRANS.USERID, STK_TRANS.ACTDTI, STK_TRANS.INTIME, STK_TRANS.IPADDRESS, STK_STORE.STORENM " +
                                            " FROM STK_ITEM INNER JOIN STK_TRANS ON STK_ITEM.ITEMID = STK_TRANS.ITEMID INNER JOIN STK_STORE ON STK_TRANS.STOREFR = STK_STORE.STOREID " +
                                            " WHERE     (STK_TRANS.TRANSTP = 'SALE') and STK_TRANS.TRANSDT = '" + TrDt + "' and STK_TRANS.TRANSMY = '" + lblSMY.Text + "' and STK_TRANS.TRANSNO = '" + ddlSalesEditInNo.Text + "' order by TRANSSL", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetail.DataSource = ds;
                gvDetail.DataBind();
                gvDetail.FooterRow.Visible = false;

                if (gvDetail.EditIndex == -1)
                {
                    decimal totCar = 0;
                    decimal totQuan = 0;
                    Decimal totAmt = 0;
                    Decimal a = 0;
                    decimal b = 0;
                    decimal c = 0;
                    foreach (GridViewRow grid in gvDetail.Rows)
                    {
                        Label lblCQty = (Label)grid.Cells[4].FindControl("lblCQty");
                        Label lblQty = (Label)grid.Cells[6].FindControl("lblQty");
                        Label Per = (Label)grid.Cells[8].FindControl("lblAmount");

                        if (lblCQty.Text == "")
                        {
                            lblCQty.Text = "0.00";
                        }
                        else
                        {
                            lblCQty.Text = lblCQty.Text;
                        }
                        String cQty = lblCQty.Text;
                        totCar = Convert.ToDecimal(cQty);
                        b += totCar;
                        string tCqty = SpellAmount.comma(b);
                        txtTCarton.Text = tCqty;

                        if (lblQty.Text == "")
                        {
                            lblQty.Text = "0.00";
                        }
                        else
                        {
                            lblQty.Text = lblQty.Text;
                        }
                        String Qty = lblQty.Text;
                        totQuan = Convert.ToDecimal(Qty);
                        c += totQuan;
                        string tQty = SpellAmount.comma(c);
                        txtTQuantity.Text = tQty;


                        if (Per.Text == "")
                        {
                            Per.Text = "0.00";
                        }
                        else
                        {
                            Per.Text = Per.Text;
                        }
                        String Perf = Per.Text;
                        totAmt = Convert.ToDecimal(Perf);
                        a += totAmt;
                        string tAmount = SpellAmount.comma(a);
                        txtTAmount.Text = tAmount;

                        //txtTotAmt.Text = nAmount;
                        //txtGrossDisAmt.Text = "0.00";
                        //txtNetAmt.Text = nAmount;
                    }
                    a += totAmt;
                }

            }
            else
            {

            }
        }

        protected void gvDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (btnSaleEdit.Text == "EDIT")
            {
                gvDetail.EditIndex = -1;
                GridShow();
            }
            else
            {
                gvDetail.EditIndex = -1;
                GridShowSale_Edit();
            }
        }

        protected void gvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string query = "";
            SqlCommand comm = new SqlCommand(query, conn);

            string Transtp = "SALE";


            DateTime TransDT = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = TransDT.ToString("yyyy/MM/dd");

            //DateTime LCDT = new DateTime();
            //string lcDate = "";
            //if (txtLCDT.Text == "")
            //{
            //    lcDate = "";
            //}
            //else
            //{
            //    LCDT = DateTime.Parse(txtLCDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            //    lcDate = LCDT.ToString("yyyy/MM/dd");
            //}

            if (e.CommandName.Equals("SaveCon"))
            {
                TextBox txtStoreNM = (TextBox)gvDetail.FooterRow.FindControl("txtStoreNM");
                TextBox txtStoreID = (TextBox)gvDetail.FooterRow.FindControl("txtStoreID");
                TextBox txtItID = (TextBox)gvDetail.FooterRow.FindControl("txtItID");
                TextBox txtItemNM = (TextBox)gvDetail.FooterRow.FindControl("txtItemNM");
                TextBox txtQty = (TextBox)gvDetail.FooterRow.FindControl("txtQty");
                TextBox txtPQty = (TextBox)gvDetail.FooterRow.FindControl("txtPQty");

                if (txtStoreID.Text == "")
                {
                    lblGridMsg.Visible = true;
                    lblGridMsg.Text = "Select Store.";
                    txtStoreNM.Focus();
                    lblPartyID.Visible = false;
                }
                else if (txtPID.Text == "")
                {
                    lblPartyID.Visible = true;
                    lblPartyID.Text = "Select Party";
                    txtPNM.Focus();
                    lblSaleFrom.Visible = false;
                    lblGridMsg.Visible = false;
                }
                else if (txtItID.Text == "")
                {
                    lblGridMsg.Visible = true;
                    lblGridMsg.Text = "Select Item.";
                    txtItemNM.Focus();
                    lblSaleFrom.Visible = false;
                    lblPartyID.Visible = false;
                }
                else if (txtQty.Text == "0")
                {
                    lblGridMsg.Visible = true;
                    lblGridMsg.Text = "Quantity is Wrong.";
                    txtPQty.Focus();
                }
                else
                {
                    lblSaleFrom.Visible = false;
                    lblPartyID.Visible = false;
                    lblGridMsg.Visible = false;

                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    if (btnSaleEdit.Text == "EDIT")
                    {
                        //Global.txtAdd("SELECT MAX(TRANSNO) AS TRANSNO FROM STK_TRANSMST WHERE TRANSMY='" + lblSMY.Text + "' AND TRANSTP='SALE'", txtInNo);
                        cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + txtInNo.Text + "' and TRANSTP='SALE' AND TRANSMY='" + lblSMY.Text + "'", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + ddlSalesEditInNo.Text + "' and TRANSTP='SALE' AND TRANSMY='" + lblSMY.Text + "'", conn);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    conn.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM.Text + "'", lblCatID);

                        DropDownList ddlType = (DropDownList)gvDetail.FooterRow.FindControl("ddlType");
                        TextBox txtCPQTY = (TextBox)gvDetail.FooterRow.FindControl("txtCPQTY");
                        if (txtCPQTY.Text == "")
                        {
                            txtCPQTY.Text = "0";
                        }
                        else
                            txtCPQTY.Text = txtCPQTY.Text;
                        decimal cpQty = 0;
                        cpQty = Convert.ToDecimal(txtCPQTY.Text);
                        TextBox txtCQty = (TextBox)gvDetail.FooterRow.FindControl("txtCQty");
                        if (txtCQty.Text == "")
                        {
                            txtCQty.Text = "0";
                        }
                        else
                            txtCQty.Text = txtCQty.Text;
                        decimal cQty = 0;
                        cQty = Convert.ToDecimal(txtCQty.Text);
                        decimal pQty = 0;
                        if (txtPQty.Text == "")
                        {
                            txtPQty.Text = "0";
                        }
                        else
                            txtPQty.Text = txtPQty.Text;
                        pQty = Convert.ToDecimal(txtPQty.Text);

                        decimal Qty = 0;
                        if (txtQty.Text == "")
                        {
                            txtQty.Text = "0";
                        }
                        else
                            txtQty.Text = txtQty.Text;
                        Qty = Convert.ToDecimal(txtQty.Text);
                        TextBox txtRate = (TextBox)gvDetail.FooterRow.FindControl("txtRate");
                        decimal Rate = 0;
                        Rate = Convert.ToDecimal(txtRate.Text);
                        TextBox txtAmount = (TextBox)gvDetail.FooterRow.FindControl("txtAmount");
                        decimal Amount = 0;
                        Amount = Convert.ToDecimal(txtAmount.Text);

                        TextBox txtDisRt = (TextBox)gvDetail.FooterRow.FindControl("txtDisRt");
                        if (txtDisRt.Text == "")
                        {
                            txtDisRt.Text = "0";
                        }
                        else
                        {
                            txtDisRt.Text = txtDisRt.Text;
                        }
                        decimal disRt = 0;
                        disRt = Convert.ToDecimal(txtDisRt.Text);

                        TextBox txtDisAmt = (TextBox)gvDetail.FooterRow.FindControl("txtDisAmt");
                        if (txtDisAmt.Text == "")
                        {
                            txtDisAmt.Text = "0";
                        }
                        else
                        {
                            txtDisAmt.Text = txtDisAmt.Text;
                        }
                        decimal disAmt = 0;
                        disAmt = Convert.ToDecimal(txtDisAmt.Text);

                        TextBox txtNetAmt = (TextBox)gvDetail.FooterRow.FindControl("txtNetAmt");
                        if (txtNetAmt.Text == "")
                        {
                            txtNetAmt.Text = "0";
                        }
                        else
                        {
                            txtNetAmt.Text = txtNetAmt.Text;
                        }
                        decimal NetAmt = 0;
                        NetAmt = Convert.ToDecimal(txtNetAmt.Text);


                        Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'SALE' and TRANSMY = '" + lblSMY.Text + "'", lblTransSL);
                        string ItemCD;
                        string mxCD = "";
                        string mid = "";
                        string subItemCD = "";
                        int subCD, incrItCD;
                        if (lblTransSL.Text == "")
                        {
                            ItemCD = "00000001";
                        }
                        else
                        {
                            mxCD = lblTransSL.Text;
                            //OItemCD = mxCD.Substring(4,4);
                            subCD = int.Parse(mxCD);
                            incrItCD = subCD + 1;
                            if (incrItCD < 10)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000000" + mid;
                            }
                            else if (incrItCD < 100)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000000" + mid;
                            }
                            else if (incrItCD < 1000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00000" + mid;
                            }
                            else if (incrItCD < 10000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000" + mid;
                            }
                            else if (incrItCD < 100000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000" + mid;
                            }
                            else if (incrItCD < 1000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00" + mid;
                            }
                            else if (incrItCD < 10000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0" + mid;
                            }
                            //else
                            //    subItemCD = incrItCD.ToString();

                            ItemCD = subItemCD;
                        }

                        if (btnSaleEdit.Text == "EDIT")
                        {
                            query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT,  DISCRT, DISCAMT, NETAMT, USERPC, USERID, IPADDRESS ) " +
                                 "values(@TRANSTP,@TRANSDT,'" + lblSMY.Text + "','" + txtInNo.Text + "','" + txtSLMNo.Text + "','" + txtStoreID.Text + "','','" + txtPID.Text + "','','','','" + txtRemarks.Text + "',@TRANSSL,'" + lblCatID.Text + "','" + txtItID.Text + "', " +
                                 " '" + ddlType.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + "," + disRt + "," + disAmt + "," + NetAmt + ",'',@USERID,'')");

                            comm = new SqlCommand(query, conn);
                            comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                            comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                            // comm.Parameters.AddWithValue("@LCDATE", lcDate);
                            comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                            comm.Parameters.AddWithValue("@USERID", userName);

                            conn.Open();
                            int result = comm.ExecuteNonQuery();
                            conn.Close();

                            GridShow();
                        }
                        else
                        {
                            Int64 TransSaleEdit = Convert.ToInt64(ddlSalesEditInNo.Text);
                            conn.Open();
                            SqlCommand cmd1 = new SqlCommand("select * from STK_TRANSMST where TRANSTP = 'SALE' and TRANSMY='" + lblSMY.Text + "' and TRANSNO =" + TransSaleEdit + "", conn);
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                            DataSet ds1 = new DataSet();
                            da.Fill(ds1);
                            conn.Close();
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, DISCRT, DISCAMT, NETAMT, USERPC, USERID, IPADDRESS ) " +
                                 "values(@TRANSTP,@TRANSDT,'" + lblSMY.Text + "','" + ddlSalesEditInNo.Text + "','" + txtSLMNo.Text + "','" + txtStoreID.Text + "','','" + txtPID.Text + "','','','','" + txtRemarks.Text + "',@TRANSSL,'" + lblCatID.Text + "','" + txtItID.Text + "', " +
                                 " '" + ddlType.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ", " + disRt + "," + disAmt + "," + NetAmt + ",'',@USERID,'')");

                                comm = new SqlCommand(query, conn);
                                comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                                comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                                // comm.Parameters.AddWithValue("@LCDATE", lcDate);
                                comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                                comm.Parameters.AddWithValue("@USERID", userName);

                                conn.Open();
                                int result = comm.ExecuteNonQuery();
                                conn.Close();

                                GridShowSale_Edit();
                            }
                            else
                            {
                                lblGridMsg.Visible = true;
                                lblGridMsg.Text = "Must be in New Mode.";
                            }

                        }
                    }
                    else
                    {
                        //conn.Open();
                        //SqlCommand cmdIN = new SqlCommand("SELECT MAX(TRANSNO) AS TRANSNO FROM STK_TRANSMST WHERE TRANSMY='" + lblSMY.Text + "' AND TRANSTP='SALE'", conn);
                        //SqlDataReader daIN = cmdIN.ExecuteReader();
                        //string InvoiceNo = "";
                        //if (daIN.Read())
                        //{
                        //    string inNo = daIN["TRANSNO"].ToString();
                        //    int inv, Finv;
                        //    if (Convert.ToInt16(inNo) >= Convert.ToInt16(txtInNo.Text))
                        //    {
                        //        inv = Convert.ToInt16(inNo);
                        //        Finv = inv + 1;
                        //        InvoiceNo = Finv.ToString(); ;
                        //    }
                        //    else
                        //    {
                        //        InvoiceNo = txtInNo.Text;
                        //    }
                        //}
                        //daIN.Close();
                        //conn.Close();

                        query = "insert into STK_TRANSMST ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS, USERPC, USERID, IPADDRESS) " +
                                    "values(@TRANSTP,@TRANSDT,'" + lblSMY.Text + "','" + txtInNo.Text + "','" + txtSLMNo.Text + "','" + txtStoreID.Text + "','','" + txtPID.Text + "','','','','" + txtRemarks.Text + "','',@USERID,'')";
                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                        comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                        //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                        comm.Parameters.AddWithValue("@USERID", userName);
                        conn.Open();
                        comm.ExecuteNonQuery();
                        conn.Close();


                        Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM.Text + "'", lblCatID);

                        DropDownList ddlType = (DropDownList)gvDetail.FooterRow.FindControl("ddlType");
                        TextBox txtCPQTY = (TextBox)gvDetail.FooterRow.FindControl("txtCPQTY");
                        if (txtCPQTY.Text == "")
                        {
                            txtCPQTY.Text = "0";
                        }
                        else
                            txtCPQTY.Text = txtCPQTY.Text;
                        decimal cpQty = 0;
                        cpQty = Convert.ToDecimal(txtCPQTY.Text);
                        TextBox txtCQty = (TextBox)gvDetail.FooterRow.FindControl("txtCQty");
                        if (txtCQty.Text == "")
                        {
                            txtCQty.Text = "0";
                        }
                        else
                            txtCQty.Text = txtCQty.Text;
                        decimal cQty = 0;
                        cQty = Convert.ToDecimal(txtCQty.Text);
                        decimal pQty = 0;
                        if (txtPQty.Text == "")
                        {
                            txtPQty.Text = "0";
                        }
                        else
                            txtPQty.Text = txtPQty.Text;
                        pQty = Convert.ToDecimal(txtPQty.Text);

                        decimal Qty = 0;
                        if (txtQty.Text == "")
                        {
                            txtQty.Text = "0";
                        }
                        else
                            txtQty.Text = txtQty.Text;
                        Qty = Convert.ToDecimal(txtQty.Text);
                        TextBox txtRate = (TextBox)gvDetail.FooterRow.FindControl("txtRate");
                        decimal Rate = 0;
                        Rate = Convert.ToDecimal(txtRate.Text);
                        TextBox txtAmount = (TextBox)gvDetail.FooterRow.FindControl("txtAmount");
                        decimal Amount = 0;
                        Amount = Convert.ToDecimal(txtAmount.Text);

                        TextBox txtDisRt = (TextBox)gvDetail.FooterRow.FindControl("txtDisRt");
                        if (txtDisRt.Text == "")
                        {
                            txtDisRt.Text = "0";
                        }
                        else
                        {
                            txtDisRt.Text = txtDisRt.Text;
                        }
                        decimal disRt = 0;
                        disRt = Convert.ToDecimal(txtDisRt.Text);

                        TextBox txtDisAmt = (TextBox)gvDetail.FooterRow.FindControl("txtDisAmt");
                        if (txtDisAmt.Text == "")
                        {
                            txtDisAmt.Text = "0";
                        }
                        else
                        {
                            txtDisAmt.Text = txtDisAmt.Text;
                        }
                        decimal disAmt = 0;
                        disAmt = Convert.ToDecimal(txtDisAmt.Text);

                        TextBox txtNetAmt = (TextBox)gvDetail.FooterRow.FindControl("txtNetAmt");
                        if (txtNetAmt.Text == "")
                        {
                            txtNetAmt.Text = "0";
                        }
                        else
                        {
                            txtNetAmt.Text = txtNetAmt.Text;
                        }
                        decimal NetAmt = 0;
                        NetAmt = Convert.ToDecimal(txtNetAmt.Text);

                        Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'SALE' and TRANSMY = '" + lblSMY.Text + "'", lblTransSL);
                        string ItemCD;
                        string mxCD = "";
                        string mid = "";
                        string subItemCD = "";
                        int subCD, incrItCD;
                        if (lblTransSL.Text == "")
                        {
                            ItemCD = "00000001";
                        }
                        else
                        {
                            mxCD = lblTransSL.Text;
                            //OItemCD = mxCD.Substring(4,4);
                            subCD = int.Parse(mxCD);
                            incrItCD = subCD + 1;
                            if (incrItCD < 10)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000000" + mid;
                            }
                            else if (incrItCD < 100)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000000" + mid;
                            }
                            else if (incrItCD < 1000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00000" + mid;
                            }
                            else if (incrItCD < 10000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000" + mid;
                            }
                            else if (incrItCD < 100000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000" + mid;
                            }
                            else if (incrItCD < 1000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00" + mid;
                            }
                            else if (incrItCD < 10000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0" + mid;
                            }
                            //else
                            //    subItemCD = incrItCD.ToString();

                            ItemCD = subItemCD;
                        }

                        query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT,  DISCRT, DISCAMT, NETAMT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblSMY.Text + "','" + txtInNo.Text + "','" + txtSLMNo.Text + "','" + txtStoreID.Text + "','','" + txtPID.Text + "','','','','" + txtRemarks.Text + "',@TRANSSL,'" + lblCatID.Text + "','" + txtItID.Text + "', " +
                                     " '" + ddlType.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + "," + disRt + "," + disAmt + "," + NetAmt + ",'',@USERID,'')");

                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                        comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                        //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                        comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                        comm.Parameters.AddWithValue("@USERID", userName);

                        conn.Open();
                        int result = comm.ExecuteNonQuery();
                        conn.Close();
                        //if (btnSaleEdit.Text == "EDIT")
                        //{

                        GridShow();
                        //}
                        //else
                        //{
                        //    GridShowSale_Edit();
                        //}

                    }
                }
            }


                ////////////////////////////////////////////////////////// For Complete   //////////////////////////////////////

            else if (e.CommandName.Equals("Complete"))
            {
                TextBox txtStoreNM = (TextBox)gvDetail.FooterRow.FindControl("txtStoreNM");
                TextBox txtStoreID = (TextBox)gvDetail.FooterRow.FindControl("txtStoreID");
                TextBox txtItID = (TextBox)gvDetail.FooterRow.FindControl("txtItID");
                TextBox txtItemNM = (TextBox)gvDetail.FooterRow.FindControl("txtItemNM");
                TextBox txtQty = (TextBox)gvDetail.FooterRow.FindControl("txtQty");
                TextBox txtPQty = (TextBox)gvDetail.FooterRow.FindControl("txtPQty");
                if (txtStoreID.Text == "")
                {
                    lblGridMsg.Visible = true;
                    lblGridMsg.Text = "Select Store.";
                    txtStoreNM.Focus();
                    lblPartyID.Visible = false;
                }
                else if (txtPID.Text == "")
                {
                    lblPartyID.Visible = true;
                    lblPartyID.Text = "Select Party";
                    txtPNM.Focus();
                    lblSaleFrom.Visible = false;
                    lblGridMsg.Visible = false;
                }
                else if (txtItID.Text == "")
                {
                    lblGridMsg.Visible = true;
                    lblGridMsg.Text = "Select Item.";
                    txtItemNM.Focus();
                    lblSaleFrom.Visible = false;
                    lblPartyID.Visible = false;
                }
                else if (txtQty.Text == "0")
                {
                    lblGridMsg.Visible = true;
                    lblGridMsg.Text = "Quantity is Wrong.";
                    txtPQty.Focus();
                }
                else
                {
                    lblSaleFrom.Visible = false;
                    lblPartyID.Visible = false;
                    lblGridMsg.Visible = false;

                    conn.Open();
                    SqlCommand cmd = new SqlCommand();

                    if (btnSaleEdit.Text == "EDIT")
                    {
                        cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + txtInNo.Text + "' and TRANSTP='SALE' AND TRANSMY='" + lblSMY.Text + "'", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + ddlSalesEditInNo.Text + "' and TRANSTP='SALE' AND TRANSMY='" + lblSMY.Text + "'", conn);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    conn.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM.Text + "'", lblCatID);

                        DropDownList ddlType = (DropDownList)gvDetail.FooterRow.FindControl("ddlType");
                        TextBox txtCPQTY = (TextBox)gvDetail.FooterRow.FindControl("txtCPQTY");
                        if (txtCPQTY.Text == "")
                        {
                            txtCPQTY.Text = "0";
                        }
                        else
                            txtCPQTY.Text = txtCPQTY.Text;
                        decimal cpQty = 0;
                        cpQty = Convert.ToDecimal(txtCPQTY.Text);
                        TextBox txtCQty = (TextBox)gvDetail.FooterRow.FindControl("txtCQty");
                        if (txtCQty.Text == "")
                        {
                            txtCQty.Text = "0";
                        }
                        else
                            txtCQty.Text = txtCQty.Text;
                        decimal cQty = 0;
                        cQty = Convert.ToDecimal(txtCQty.Text);
                        decimal pQty = 0;
                        if (txtPQty.Text == "")
                        {
                            txtPQty.Text = "0";
                        }
                        else
                            txtPQty.Text = txtPQty.Text;
                        pQty = Convert.ToDecimal(txtPQty.Text);

                        decimal Qty = 0;
                        if (txtQty.Text == "")
                        {
                            txtQty.Text = "0";
                        }
                        else
                            txtQty.Text = txtQty.Text;
                        Qty = Convert.ToDecimal(txtQty.Text);
                        TextBox txtRate = (TextBox)gvDetail.FooterRow.FindControl("txtRate");
                        decimal Rate = 0;
                        Rate = Convert.ToDecimal(txtRate.Text);
                        TextBox txtAmount = (TextBox)gvDetail.FooterRow.FindControl("txtAmount");
                        decimal Amount = 0;
                        Amount = Convert.ToDecimal(txtAmount.Text);

                        TextBox txtDisRt = (TextBox)gvDetail.FooterRow.FindControl("txtDisRt");
                        if (txtDisRt.Text == "")
                        {
                            txtDisRt.Text = "0";
                        }
                        else
                        {
                            txtDisRt.Text = txtDisRt.Text;
                        }
                        decimal disRt = 0;
                        disRt = Convert.ToDecimal(txtDisRt.Text);

                        TextBox txtDisAmt = (TextBox)gvDetail.FooterRow.FindControl("txtDisAmt");
                        if (txtDisAmt.Text == "")
                        {
                            txtDisAmt.Text = "0";
                        }
                        else
                        {
                            txtDisAmt.Text = txtDisAmt.Text;
                        }
                        decimal disAmt = 0;
                        disAmt = Convert.ToDecimal(txtDisAmt.Text);

                        TextBox txtNetAmt = (TextBox)gvDetail.FooterRow.FindControl("txtNetAmt");
                        if (txtNetAmt.Text == "")
                        {
                            txtNetAmt.Text = "0";
                        }
                        else
                        {
                            txtNetAmt.Text = txtNetAmt.Text;
                        }
                        decimal NetAmt = 0;
                        NetAmt = Convert.ToDecimal(txtNetAmt.Text);


                        Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'SALE' and TRANSMY = '" + lblSMY.Text + "'", lblTransSL);
                        string ItemCD;
                        string mxCD = "";
                        string mid = "";
                        string subItemCD = "";
                        int subCD, incrItCD;
                        if (lblTransSL.Text == "")
                        {
                            ItemCD = "00000001";
                        }
                        else
                        {
                            mxCD = lblTransSL.Text;
                            //OItemCD = mxCD.Substring(4,4);
                            subCD = int.Parse(mxCD);
                            incrItCD = subCD + 1;
                            if (incrItCD < 10)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000000" + mid;
                            }
                            else if (incrItCD < 100)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000000" + mid;
                            }
                            else if (incrItCD < 1000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00000" + mid;
                            }
                            else if (incrItCD < 10000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000" + mid;
                            }
                            else if (incrItCD < 100000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000" + mid;
                            }
                            else if (incrItCD < 1000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00" + mid;
                            }
                            else if (incrItCD < 10000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0" + mid;
                            }
                            //else
                            //    subItemCD = incrItCD.ToString();

                            ItemCD = subItemCD;
                        }

                        if (btnSaleEdit.Text == "EDIT")
                        {
                            query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, DISCRT, DISCAMT, NETAMT, USERPC, USERID, IPADDRESS ) " +
                                 "values(@TRANSTP,@TRANSDT,'" + lblSMY.Text + "','" + txtInNo.Text + "','" + txtSLMNo.Text + "','" + txtStoreID.Text + "','','" + txtPID.Text + "','','','','" + txtRemarks.Text + "',@TRANSSL,'" + lblCatID.Text + "','" + txtItID.Text + "', " +
                                 " '" + ddlType.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + "," + disRt + "," + disAmt + "," + NetAmt + ",'',@USERID,'')");

                            comm = new SqlCommand(query, conn);
                            comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                            comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                            // comm.Parameters.AddWithValue("@LCDATE", lcDate);
                            comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                            comm.Parameters.AddWithValue("@USERID", userName);

                            conn.Open();
                            int result = comm.ExecuteNonQuery();
                            conn.Close();

                            //////////    this is for  new method

                            /////////Refresh/////
                            //ddlSalesEditInNo.SelectedIndex = -1;
                            //txtSLMNo.Text = "";
                            //txtSaleFrom.Text = "";
                            //txtSlFr.Text = "";
                            ////ddlLC.SelectedIndex = -1;
                            ////txtLC.Text = "";
                            ////txtLCDT.Text = "";
                            //txtPNM.Text = "";
                            //txtPID.Text = "";
                            //txtRemarks.Text = "";
                            //txtItID.Text = "";
                            //txtItemNM.Text = "";
                            //ddlType.SelectedIndex = -1;
                            //txtCPQTY.Text = "";
                            //txtCQty.Text = "";
                            //txtPQty.Text = "";
                            //txtQty.Text = "";
                            //txtRate.Text = ".00";
                            //txtAmount.Text = ".00";
                            //txtTotal.Text = ".00";


                            //DateTime transdate = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            //string TrDate = transdate.ToString("yyyy/MM/dd");

                            //Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblSMY.Text + "' and TRANSTP = 'SALE'", lblSMxNo);
                            //if (lblSMxNo.Text == "")
                            //{
                            //    txtInNo.Text = "1";
                            //}
                            //else
                            //{
                            //    int iNo = int.Parse(lblSMxNo.Text);
                            //    int totIno = iNo + 1;
                            //    txtInNo.Text = totIno.ToString();
                            //}

                            //GridShow();
                            //txtSLMNo.Focus();

                            GridShow_Complete();



                        }
                        else
                        {
                            Int64 TransSaleEdit = Convert.ToInt64(ddlSalesEditInNo.Text);
                            conn.Open();
                            SqlCommand cmd1 = new SqlCommand("select * from STK_TRANSMST where TRANSTP = 'SALE' and TRANSMY='" + lblSMY.Text + "' and TRANSNO =" + TransSaleEdit + "", conn);
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                            DataSet ds1 = new DataSet();
                            da1.Fill(ds1);
                            conn.Close();
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, DISCRT, DISCAMT, NETAMT, USERPC, USERID, IPADDRESS ) " +
                                 "values(@TRANSTP,@TRANSDT,'" + lblSMY.Text + "','" + ddlSalesEditInNo.Text + "','" + txtSLMNo.Text + "','" + txtStoreID.Text + "','','" + txtPID.Text + "','','','','" + txtRemarks.Text + "',@TRANSSL,'" + lblCatID.Text + "','" + txtItID.Text + "', " +
                                 " '" + ddlType.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ", " + disRt + "," + disAmt + "," + NetAmt + ",'',@USERID,'')");

                                comm = new SqlCommand(query, conn);
                                comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                                comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                                // comm.Parameters.AddWithValue("@LCDATE", lcDate);
                                comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                                comm.Parameters.AddWithValue("@USERID", userName);

                                conn.Open();
                                int result = comm.ExecuteNonQuery();
                                conn.Close();

                                /////////Refresh/////
                                //ddlSalesEditInNo.SelectedIndex = -1;
                                //txtSLMNo.Text = "";
                                //txtSaleFrom.Text = "";
                                //txtSlFr.Text = "";
                                ////ddlLC.SelectedIndex = -1;
                                ////txtLC.Text = "";
                                ////txtLCDT.Text = "";
                                //txtPNM.Text = "";
                                //txtPID.Text = "";
                                //txtRemarks.Text = "";
                                //txtItID.Text = "";
                                //txtItemNM.Text = "";
                                //ddlType.SelectedIndex = -1;
                                //txtCPQTY.Text = "";
                                //txtCQty.Text = "";
                                //txtPQty.Text = "";
                                //txtQty.Text = "";
                                //txtRate.Text = ".00";
                                //txtAmount.Text = ".00";
                                //txtTotal.Text = ".00";

                                //ddlSalesEditInNo.Focus();
                                //GridShowSale_Edit();

                                GridShow_CompleteEdit();
                            }
                            else
                            {
                                lblGridMsg.Visible = true;
                                lblGridMsg.Text = "Must be in New Mode.";
                            }

                        }
                    }
                    else
                    {
                        //conn.Open();
                        //SqlCommand cmdIN = new SqlCommand("SELECT MAX(TRANSNO) AS TRANSNO FROM STK_TRANSMST WHERE TRANSMY='" + lblSMY.Text + "' AND TRANSTP='SALE'", conn);
                        //SqlDataReader daIN = cmdIN.ExecuteReader();

                        //if (daIN.Read())
                        //{
                        //    string inNo = daIN["TRANSNO"].ToString();
                        //    int inv, Finv;
                        //    if (inNo == txtInNo.Text)
                        //    {
                        //        inv = Convert.ToInt16(inNo);
                        //        Finv = inv + 1;
                        //        InvoiceNo = Finv.ToString(); ;
                        //    }
                        //    else
                        //    {
                        //        InvoiceNo = txtInNo.Text;
                        //    }
                        //}
                        //daIN.Close();
                        //conn.Close();

                        query = "insert into STK_TRANSMST ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS, USERPC, USERID, IPADDRESS) " +
                                    "values(@TRANSTP,@TRANSDT,'" + lblSMY.Text + "','" + txtInNo.Text + "','" + txtSLMNo.Text + "','" + txtStoreID.Text + "','','" + txtPID.Text + "','','','','" + txtRemarks.Text + "','',@USERID,'')";
                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                        comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                        //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                        comm.Parameters.AddWithValue("@USERID", userName);
                        conn.Open();
                        comm.ExecuteNonQuery();
                        conn.Close();


                        Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM.Text + "'", lblCatID);

                        DropDownList ddlType = (DropDownList)gvDetail.FooterRow.FindControl("ddlType");
                        TextBox txtCPQTY = (TextBox)gvDetail.FooterRow.FindControl("txtCPQTY");
                        if (txtCPQTY.Text == "")
                        {
                            txtCPQTY.Text = "0";
                        }
                        else
                            txtCPQTY.Text = txtCPQTY.Text;
                        decimal cpQty = 0;
                        cpQty = Convert.ToDecimal(txtCPQTY.Text);
                        TextBox txtCQty = (TextBox)gvDetail.FooterRow.FindControl("txtCQty");
                        if (txtCQty.Text == "")
                        {
                            txtCQty.Text = "0";
                        }
                        else
                            txtCQty.Text = txtCQty.Text;
                        decimal cQty = 0;
                        cQty = Convert.ToDecimal(txtCQty.Text);
                        decimal pQty = 0;
                        if (txtPQty.Text == "")
                        {
                            txtPQty.Text = "0";
                        }
                        else
                            txtPQty.Text = txtPQty.Text;
                        pQty = Convert.ToDecimal(txtPQty.Text);

                        decimal Qty = 0;
                        if (txtQty.Text == "")
                        {
                            txtQty.Text = "0";
                        }
                        else
                            txtQty.Text = txtQty.Text;
                        Qty = Convert.ToDecimal(txtQty.Text);
                        TextBox txtRate = (TextBox)gvDetail.FooterRow.FindControl("txtRate");
                        decimal Rate = 0;
                        Rate = Convert.ToDecimal(txtRate.Text);
                        TextBox txtAmount = (TextBox)gvDetail.FooterRow.FindControl("txtAmount");
                        decimal Amount = 0;
                        Amount = Convert.ToDecimal(txtAmount.Text);

                        TextBox txtDisRt = (TextBox)gvDetail.FooterRow.FindControl("txtDisRt");
                        if (txtDisRt.Text == "")
                        {
                            txtDisRt.Text = "0";
                        }
                        else
                        {
                            txtDisRt.Text = txtDisRt.Text;
                        }
                        decimal disRt = 0;
                        disRt = Convert.ToDecimal(txtDisRt.Text);

                        TextBox txtDisAmt = (TextBox)gvDetail.FooterRow.FindControl("txtDisAmt");
                        if (txtDisAmt.Text == "")
                        {
                            txtDisAmt.Text = "0";
                        }
                        else
                        {
                            txtDisAmt.Text = txtDisAmt.Text;
                        }
                        decimal disAmt = 0;
                        disAmt = Convert.ToDecimal(txtDisAmt.Text);

                        TextBox txtNetAmt = (TextBox)gvDetail.FooterRow.FindControl("txtNetAmt");
                        if (txtNetAmt.Text == "")
                        {
                            txtNetAmt.Text = "0";
                        }
                        else
                        {
                            txtNetAmt.Text = txtNetAmt.Text;
                        }
                        decimal NetAmt = 0;
                        NetAmt = Convert.ToDecimal(txtNetAmt.Text);

                        Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'SALE' and TRANSMY = '" + lblSMY.Text + "'", lblTransSL);
                        string ItemCD;
                        string mxCD = "";
                        string mid = "";
                        string subItemCD = "";
                        int subCD, incrItCD;
                        if (lblTransSL.Text == "")
                        {
                            ItemCD = "00000001";
                        }
                        else
                        {
                            mxCD = lblTransSL.Text;
                            //OItemCD = mxCD.Substring(4,4);
                            subCD = int.Parse(mxCD);
                            incrItCD = subCD + 1;
                            if (incrItCD < 10)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000000" + mid;
                            }
                            else if (incrItCD < 100)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000000" + mid;
                            }
                            else if (incrItCD < 1000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00000" + mid;
                            }
                            else if (incrItCD < 10000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000" + mid;
                            }
                            else if (incrItCD < 100000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000" + mid;
                            }
                            else if (incrItCD < 1000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00" + mid;
                            }
                            else if (incrItCD < 10000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0" + mid;
                            }
                            //else
                            //    subItemCD = incrItCD.ToString();

                            ItemCD = subItemCD;
                        }

                        query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, DISCRT, DISCAMT, NETAMT, USERPC, USERID, IPADDRESS ) " +
                                 "values(@TRANSTP,@TRANSDT,'" + lblSMY.Text + "','" + txtInNo.Text + "','" + txtSLMNo.Text + "','" + txtStoreID.Text + "','','" + txtPID.Text + "','','','','" + txtRemarks.Text + "',@TRANSSL,'" + lblCatID.Text + "','" + txtItID.Text + "', " +
                                 " '" + ddlType.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + "," + disRt + "," + disAmt + "," + NetAmt + ",'',@USERID,'')");

                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                        comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                        //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                        comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                        comm.Parameters.AddWithValue("@USERID", userName);

                        conn.Open();
                        int result = comm.ExecuteNonQuery();
                        conn.Close();

                        /////////Refresh/////
                        //txtSLMNo.Text = "";
                        //txtSaleFrom.Text = "";
                        //txtSlFr.Text = "";
                        ////ddlLC.SelectedIndex = -1;
                        ////txtLC.Text = "";
                        ////txtLCDT.Text = "";
                        //txtPNM.Text = "";
                        //txtPID.Text = "";
                        //txtRemarks.Text = "";
                        //txtItID.Text = "";
                        //txtItemNM.Text = "";
                        //ddlType.SelectedIndex = -1;
                        //txtCPQTY.Text = "";
                        //txtCQty.Text = "";
                        //txtPQty.Text = "";
                        //txtQty.Text = "";
                        //txtRate.Text = ".00";
                        //txtAmount.Text = ".00";
                        //txtTotal.Text = "";


                        //DateTime transdate = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        //string TrDate = transdate.ToString("yyyy/MM/dd");

                        //Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblSMY.Text + "' and TRANSTP = 'SALE'", lblSMxNo);
                        //if (lblSMxNo.Text == "")
                        //{
                        //    txtInNo.Text = "1";
                        //}
                        //else
                        //{
                        //    int iNo = int.Parse(lblSMxNo.Text);
                        //    int totIno = iNo + 1;
                        //    txtInNo.Text = totIno.ToString();
                        //}

                        //    GridShow();
                        //    txtSLMNo.Focus();

                        GridShow_Complete();
                    }
                }

                if (btnSaleEdit.Text == "EDIT") ////new mode        ///////////////// bottom code is block for fancy bd to bottom value and upper value be same
                {
                    txtTotAmt.Text = txtTAmount.Text;
                    txtNetAmt.Text = txtTAmount.Text;

                    //Decimal totAmt = 0;
                    //Decimal a = 0;
                    //decimal tAmt = 0;
                    //decimal dis = 0;
                    //decimal disAmt = 0;
                    //decimal amt = 0;
                    //decimal Amount = 0;
                    //if (gvDetail.EditIndex == -1)
                    //{
                    //    foreach (GridViewRow grid in gvDetail.Rows)
                    //    {
                    //        Label lblNetAmt = (Label)grid.Cells[11].FindControl("lblNetAmt");

                    //        if (lblNetAmt.Text == "")
                    //        {
                    //            lblNetAmt.Text = "0";
                    //        }
                    //        else
                    //        {
                    //            lblNetAmt.Text = lblNetAmt.Text;
                    //        }
                    //        String TotalAmount = lblNetAmt.Text;
                    //        totAmt = Convert.ToDecimal(TotalAmount);
                    //        a += totAmt;
                    //        string tAmount = SpellAmount.comma(a);
                    //        txtTotAmt.Text = tAmount;
                    //        txtNetAmt.Text = tAmount;
                    //        txtTotal.Text = a.ToString();
                    //        tAmt = a;

                    //        Label lblAmount = (Label)grid.Cells[8].FindControl("lblAmount");
                    //        Label lblDisAmt = (Label)grid.Cells[10].FindControl("lblDisAmt");

                    //        if (lblAmount.Text == "")
                    //        {
                    //            lblAmount.Text = "0.00";
                    //        }
                    //        else
                    //            lblAmount.Text = lblAmount.Text;

                    //        amt = Convert.ToDecimal(lblAmount.Text);
                    //        Amount += amt;
                    //        txtTTotalAmount.Text = Amount.ToString();

                    //        if (lblDisAmt.Text == "")
                    //        {
                    //            lblDisAmt.Text = "0.00";
                    //        }
                    //        else
                    //            lblDisAmt.Text = lblDisAmt.Text;

                    //        dis = Convert.ToDecimal(lblDisAmt.Text);
                    //        disAmt += dis;
                    //        txtTDisAmount.Text = disAmt.ToString();
                    //    }
                    //    a += totAmt;
                    //    Amount += amt;
                    //    disAmt += dis;
                    //    //}
                    //}
                    //else
                    //{

                    //}

                    txtGrossDisAmt.Focus();
                }
                else ////// edit mode
                {
                    txtTotAmt.Text = txtTAmount.Text;
                    txtNetAmt.Text = txtTAmount.Text;

                    //Decimal totAmt = 0;
                    //Decimal a = 0;
                    //decimal tAmt = 0;
                    //decimal dis = 0;
                    //decimal disAmt = 0;
                    //decimal amt = 0;
                    //decimal Amount = 0;
                    //if (gvDetail.EditIndex == -1)
                    //{
                    //    foreach (GridViewRow grid in gvDetail.Rows)
                    //    {
                    //        Label lblNetAmt = (Label)grid.Cells[11].FindControl("lblNetAmt");

                    //        if (lblNetAmt.Text == "")
                    //        {
                    //            lblNetAmt.Text = "0";
                    //        }
                    //        else
                    //        {
                    //            lblNetAmt.Text = lblNetAmt.Text;
                    //        }
                    //        String TotalAmount = lblNetAmt.Text;
                    //        totAmt = Convert.ToDecimal(TotalAmount);
                    //        a += totAmt;
                    //        string tAmount = SpellAmount.comma(a);
                    //        txtTotAmt.Text = tAmount;
                    //        txtNetAmt.Text = tAmount;
                    //        txtTotal.Text = a.ToString();
                    //        tAmt = a;

                    //        Label lblAmount = (Label)grid.Cells[8].FindControl("lblAmount");
                    //        Label lblDisAmt = (Label)grid.Cells[10].FindControl("lblDisAmt");

                    //        if (lblAmount.Text == "")
                    //        {
                    //            lblAmount.Text = "0.00";
                    //        }
                    //        else
                    //            lblAmount.Text = lblAmount.Text;

                    //        amt = Convert.ToDecimal(lblAmount.Text);
                    //        Amount += amt;
                    //        txtTTotalAmount.Text = Amount.ToString();

                    //        if (lblDisAmt.Text == "")
                    //        {
                    //            lblDisAmt.Text = "0.00";
                    //        }
                    //        else
                    //            lblDisAmt.Text = lblDisAmt.Text;

                    //        dis = Convert.ToDecimal(lblDisAmt.Text);
                    //        disAmt += dis;
                    //        txtTDisAmount.Text = disAmt.ToString();
                    //    }
                    //    a += totAmt;
                    //    Amount += amt;
                    //    disAmt += dis;
                    //    //}
                    //}
                    //else
                    //{

                    //}

                    txtGrossDisAmt.Focus();
                }
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["InvDate_S"] = "";
            Session["InvNo_S"] = "";
            //Session["InvNoEdit_S"] = "";
            Session["Memo_S"] = "";
            Session["StoreNM_S"] = "";
            Session["StoreID_S"] = "";
            Session["PartyNM_S"] = "";
            Session["PartyID_S"] = "";

            if (txtInDT.Text == "")
            {
                lblSaleFrom.Visible = true;
                lblSaleFrom.Text = "Select Date.";
            }
            else if (txtInNo.Text == "")
            {
                lblSaleFrom.Visible = true;
                lblSaleFrom.Text = "Select Invoice No.";
            }
            //else if (txtSlFr.Text == "")
            //{
            //    lblSaleFrom.Visible = true;
            //    lblSaleFrom.Text = "Select Store.";
            //    txtSaleFrom.Focus();
            //}
            else if (txtPID.Text == "")
            {
                lblSaleFrom.Visible = true;
                lblSaleFrom.Text = "Select Party.";
                txtPNM.Focus();
            }
            else
            {

                lblSaleFrom.Visible = false;

                Session["InvDate_S"] = txtInDT.Text;
                if (btnSaleEdit.Text == "EDIT")
                    Session["InvNo_S"] = txtInNo.Text;
                else
                    Session["InvNo_S"] = ddlSalesEditInNo.Text;

                Session["Memo_S"] = txtSLMNo.Text;
                //Session["StoreNM_S"] = txtSaleFrom.Text;
                //Session["StoreID_S"] = txtSlFr.Text;
                Session["PartyNM_S"] = txtPNM.Text;
                Session["PartyID_S"] = txtPID.Text;

                if (btnSaleEdit.Text == "NEW")
                {
                    if (ddlSalesEditInNo.Text == "Select")
                    {
                        lblSaleFrom.Visible = true;
                        lblSaleFrom.Text = "Select Invoice No";
                        ddlSalesEditInNo.Focus();
                    }
                    else
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                        SqlConnection conn = new SqlConnection(connectionString);
                        string userName = HttpContext.Current.Session["UserName"].ToString();

                        DateTime transdate = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        string TrDate = transdate.ToString("yyyy/MM/dd");

                        decimal totamt = Convert.ToDecimal(txtTotAmt.Text);
                        decimal grsDis = Convert.ToDecimal(txtGrossDisAmt.Text);
                        decimal NetAmt = Convert.ToDecimal(txtNetAmt.Text);
                        decimal ltCost = Convert.ToDecimal(txtLtCost.Text);

                        if ((totamt + ltCost) - grsDis == NetAmt)
                        {
                            Int64 TransNo = Convert.ToInt64(ddlSalesEditInNo.Text);
                            conn.Open();
                            SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", LTCOST = " + ltCost + ", TOTNET = " + NetAmt + ", " +
                                  " USERID = '" + userName + "' where TRANSTP = 'SALE'  and TRANSMY='" + lblSMY.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                            cmd1.ExecuteNonQuery();
                            conn.Close();
                        }
                        else
                        {
                            decimal p_NetAmt = (totamt + ltCost) - grsDis;
                            Int64 TransNo = Convert.ToInt64(ddlSalesEditInNo.Text);
                            conn.Open();
                            SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", LTCOST = " + ltCost + ", TOTNET = " + p_NetAmt + ", " +
                                  " USERID = '" + userName + "' where TRANSTP = 'SALE'  and TRANSMY='" + lblSMY.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                            cmd1.ExecuteNonQuery();
                            conn.Close();
                        }


                        ScriptManager.RegisterStartupScript(this,
                        this.GetType(), "OpenWindow", "window.open('../Report/Report/rptSaleMemo.aspx','_newtab');", true);
                    }
                }
                else
                {

                    string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                    SqlConnection conn = new SqlConnection(connectionString);
                    string userName = HttpContext.Current.Session["UserName"].ToString();

                    DateTime transdate = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string TrDate = transdate.ToString("yyyy/MM/dd");

                    decimal totamt = Convert.ToDecimal(txtTotAmt.Text);
                    decimal grsDis = Convert.ToDecimal(txtGrossDisAmt.Text);
                    decimal NetAmt = Convert.ToDecimal(txtNetAmt.Text);
                    decimal ltCost = Convert.ToDecimal(txtLtCost.Text);

                    if (btnSaleEdit.Text == "EDIT")
                    {
                        if ((totamt + ltCost) - grsDis == NetAmt)
                        {
                            Int64 TransNo = Convert.ToInt64(txtInNo.Text);
                            conn.Open();
                            SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", LTCOST = " + ltCost + ", TOTNET = " + NetAmt + ", " +
                                  " USERID = '" + userName + "' where TRANSTP = 'SALE'  and TRANSMY='" + lblSMY.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                            cmd1.ExecuteNonQuery();
                            conn.Close();
                        }
                        else
                        {
                            decimal p_NetAmt = (totamt + ltCost) - grsDis;

                            Int64 TransNo = Convert.ToInt64(txtInNo.Text);
                            conn.Open();
                            SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", LTCOST = " + ltCost + ", TOTNET = " + p_NetAmt + ", " +
                                  " USERID = '" + userName + "' where TRANSTP = 'SALE'  and TRANSMY='" + lblSMY.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                            cmd1.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    ScriptManager.RegisterStartupScript(this,
                    this.GetType(), "OpenWindow", "window.open('../Report/Report/rptSaleMemo.aspx','_newtab');", true);
                }
            }
        }

        protected void btnDoPrint_Click(object sender, EventArgs e)
        {
            Session["InvDate_S"] = "";
            Session["InvNo_S"] = "";
            //Session["InvNoEdit_S"] = "";
            Session["Memo_S"] = "";
            //Session["StoreNM_S"] = "";
            //Session["StoreID_S"] = "";
            Session["PartyNM_S"] = "";
            Session["PartyID_S"] = "";

            if (txtInDT.Text == "")
            {
                lblSaleFrom.Visible = true;
                lblSaleFrom.Text = "Select Date.";
            }
            else if (txtInNo.Text == "")
            {
                lblSaleFrom.Visible = true;
                lblSaleFrom.Text = "Select Invoice No.";
            }
            //else if (txtSlFr.Text == "")
            //{
            //    lblSaleFrom.Visible = true;
            //    lblSaleFrom.Text = "Select Store.";
            //    txtSaleFrom.Focus();
            //}
            else if (txtPID.Text == "")
            {
                lblSaleFrom.Visible = true;
                lblSaleFrom.Text = "Select Party.";
                txtPNM.Focus();
            }
            else
            {
                Session["InvDate_S"] = txtInDT.Text;
                if (btnSaleEdit.Text == "EDIT")
                    Session["InvNo_S"] = txtInNo.Text;
                else
                    Session["InvNo_S"] = ddlSalesEditInNo.Text;
                Session["Memo_S"] = txtSLMNo.Text;
                //Session["StoreNM_S"] = txtSaleFrom.Text;
                //Session["StoreID_S"] = txtSlFr.Text;
                Session["PartyNM_S"] = txtPNM.Text;
                Session["PartyID_S"] = txtPID.Text;

                if (btnSaleEdit.Text == "NEW")
                {
                    if (ddlSalesEditInNo.Text == "Select")
                    {
                        lblSaleFrom.Visible = true;
                        lblSaleFrom.Text = "Select Invoice No";
                        ddlSalesEditInNo.Focus();
                    }
                    else
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                        SqlConnection conn = new SqlConnection(connectionString);
                        string userName = HttpContext.Current.Session["UserName"].ToString();

                        DateTime transdate = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        string TrDate = transdate.ToString("yyyy/MM/dd");

                        decimal totamt = Convert.ToDecimal(txtTotAmt.Text);
                        decimal grsDis = Convert.ToDecimal(txtGrossDisAmt.Text);
                        decimal NetAmt = Convert.ToDecimal(txtNetAmt.Text);
                        decimal ltCost = Convert.ToDecimal(txtLtCost.Text);

                        if ((totamt + ltCost) - grsDis == NetAmt)
                        {
                            Int64 TransNo = Convert.ToInt64(ddlSalesEditInNo.Text);
                            conn.Open();
                            SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", LTCOST = " + ltCost + ", TOTNET = " + NetAmt + ", " +
                                  " USERID = '" + userName + "' where TRANSTP = 'SALE'  and TRANSMY='" + lblSMY.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                            cmd1.ExecuteNonQuery();
                            conn.Close();
                        }
                        else
                        {
                            decimal p_NetAmt = (totamt + ltCost) - grsDis;

                            Int64 TransNo = Convert.ToInt64(ddlSalesEditInNo.Text);
                            conn.Open();
                            SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", LTCOST = " + ltCost + ", TOTNET = " + p_NetAmt + ", " +
                                  " USERID = '" + userName + "' where TRANSTP = 'SALE'  and TRANSMY='" + lblSMY.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                            cmd1.ExecuteNonQuery();
                            conn.Close();
                        }

                        ScriptManager.RegisterStartupScript(this,
                        this.GetType(), "OpenWindow", "window.open('../Report/Report/rptDeliveryOrder.aspx','_newtab');", true);
                    }
                }
                else
                {

                    string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                    SqlConnection conn = new SqlConnection(connectionString);
                    string userName = HttpContext.Current.Session["UserName"].ToString();

                    DateTime transdate = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string TrDate = transdate.ToString("yyyy/MM/dd");

                    decimal totamt = Convert.ToDecimal(txtTotAmt.Text);
                    decimal grsDis = Convert.ToDecimal(txtGrossDisAmt.Text);
                    decimal NetAmt = Convert.ToDecimal(txtNetAmt.Text);
                    decimal ltCost = Convert.ToDecimal(txtLtCost.Text);

                    if (btnSaleEdit.Text == "EDIT")
                    {
                        if ((totamt + ltCost) - grsDis == NetAmt)
                        {
                            Int64 TransNo = Convert.ToInt64(txtInNo.Text);
                            conn.Open();
                            SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", LTCOST = " + ltCost + ", TOTNET = " + NetAmt + ", " +
                                  " USERID = '" + userName + "' where TRANSTP = 'SALE'  and TRANSMY='" + lblSMY.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                            cmd1.ExecuteNonQuery();
                            conn.Close();
                        }
                        else
                        {
                            decimal p_NetAmt = (totamt + ltCost) - grsDis;

                            Int64 TransNo = Convert.ToInt64(txtInNo.Text);
                            conn.Open();
                            SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", LTCOST = " + ltCost + ", TOTNET = " + p_NetAmt + ", " +
                                  " USERID = '" + userName + "' where TRANSTP = 'SALE'  and TRANSMY='" + lblSMY.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                            cmd1.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    ScriptManager.RegisterStartupScript(this,
                    this.GetType(), "OpenWindow", "window.open('../Report/Report/rptDeliveryOrder.aspx','_newtab');", true);
                }
            }
        }

        protected void gvDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string userName = HttpContext.Current.Session["UserName"].ToString();

            if (btnSaleEdit.Text == "EDIT")
            {
                DateTime transdate = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                Label lblTransSL = (Label)gvDetail.Rows[e.RowIndex].FindControl("lblTransSL");

                SqlCommand cmd1 = new SqlCommand("select * from STK_TRANS where TRANSTP = 'SALE' and TRANSMY='" + lblSMY.Text + "' and TRANSNO ='" + txtInNo.Text + "' and TRANSDT = '" + TrDate + "' ", conn);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                conn.Close();
                if (ds1.Tables[0].Rows.Count > 1)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from STK_TRANS where TRANSTP = 'SALE' and TRANSNO ='" + txtInNo.Text + "' and  TRANSDT = '" + TrDate + "' and TRANSSL = '" + lblTransSL.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from STK_TRANS where TRANSTP = 'SALE' and TRANSNO ='" + txtInNo.Text + "' and  TRANSDT = '" + TrDate + "' and TRANSSL = '" + lblTransSL.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand("delete from STK_TRANSMST where TRANSTP = 'SALE' and TRANSNO ='" + txtInNo.Text + "' and TRANSMY='" + lblSMY.Text + "' and  TRANSDT = '" + TrDate + "'", conn);
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                }


                gvDetail.EditIndex = -1;
                GridShow();

                txtTotAmt.Text = txtTotal.Text;
                decimal totamt = Convert.ToDecimal(txtTotAmt.Text);
                decimal grDisamt = Convert.ToDecimal(txtGrossDisAmt.Text);
                decimal n_amt = totamt - grDisamt;
                string n_Amt = SpellAmount.comma(n_amt);
                txtNetAmt.Text = n_Amt;
                txtGrossDisAmt.Focus();
                lblSmsgComTrans.Visible = true;
                lblSmsgComTrans.Text = "Complete Transaction By Changing Discount.";

            }
            else
            {
                DateTime transdate = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                Label lblTransSL = (Label)gvDetail.Rows[e.RowIndex].FindControl("lblTransSL");

                SqlCommand cmd1 = new SqlCommand("select * from STK_TRANS where TRANSTP = 'SALE' and TRANSMY='" + lblSMY.Text + "' and TRANSNO ='" + ddlSalesEditInNo.Text + "' and TRANSDT = '" + TrDate + "' ", conn);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                conn.Close();
                if (ds1.Tables[0].Rows.Count > 1)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from STK_TRANS where TRANSTP = 'SALE' and TRANSNO ='" + ddlSalesEditInNo.Text + "' and  TRANSDT = '" + TrDate + "' and TRANSSL = '" + lblTransSL.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from STK_TRANS where TRANSTP = 'SALE' and TRANSNO ='" + ddlSalesEditInNo.Text + "' and  TRANSDT = '" + TrDate + "' and TRANSSL = '" + lblTransSL.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand("delete from STK_TRANSMST where TRANSTP = 'SALE' and TRANSNO ='" + ddlSalesEditInNo.Text + "' and TRANSMY='" + lblSMY.Text + "' and  TRANSDT = '" + TrDate + "'", conn);
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                }

                gvDetail.EditIndex = -1;
                GridShowSale_Edit();

                txtTotAmt.Text = txtTotal.Text;
                decimal totamt = Convert.ToDecimal(txtTotAmt.Text);
                decimal grDisamt = Convert.ToDecimal(txtGrossDisAmt.Text);
                decimal n_amt = totamt - grDisamt;
                string n_Amt = SpellAmount.comma(n_amt);
                txtNetAmt.Text = n_Amt;
                txtGrossDisAmt.Focus();
                lblSmsgComTrans.Visible = true;
                lblSmsgComTrans.Text = "Complete Transaction By Changing Discount.";

            }
        }

        protected void gvDetail_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (btnSaleEdit.Text == "EDIT")
            {
                gvDetail.EditIndex = e.NewEditIndex;
                GridShow();
            }
            else
            {
                gvDetail.EditIndex = e.NewEditIndex;
                GridShowSale_Edit();
            }

            TextBox txtStoreNMEdit = (TextBox)gvDetail.Rows[e.NewEditIndex].FindControl("txtStoreNMEdit");
            txtStoreNMEdit.Focus();
        }

        protected void gvDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string userName = HttpContext.Current.Session["UserName"].ToString();

            TextBox txtStoreNMEdit = (TextBox)gvDetail.Rows[e.RowIndex].FindControl("txtStoreNMEdit");
            Label lblStoreIDEdit = (Label)gvDetail.Rows[e.RowIndex].FindControl("lblStoreIDEdit");
            Label lblItemIDEdit = (Label)gvDetail.Rows[e.RowIndex].FindControl("lblItemIDEdit");
            TextBox txtItemNMEdit = (TextBox)gvDetail.Rows[e.RowIndex].FindControl("txtItemNMEdit");
            //txtItemNMEdit.Focus();
            TextBox txtQtyEdit = (TextBox)gvDetail.Rows[e.RowIndex].FindControl("txtQtyEdit");
            TextBox txtPQtyEdit = (TextBox)gvDetail.Rows[e.RowIndex].FindControl("txtPQtyEdit");
            if (lblStoreIDEdit.Text == "")
            {
                lblSaleFrom.Visible = true;
                lblSaleFrom.Text = "Select Store.";
                txtStoreNMEdit.Focus();
                lblPartyID.Visible = false;
                lblGridMsg.Visible = false;
            }
            else if (txtPID.Text == "")
            {
                lblPartyID.Visible = true;
                lblPartyID.Text = "Select Party";
                txtPNM.Focus();
                lblSaleFrom.Visible = false;
                lblGridMsg.Visible = false;
            }
            else if (lblItemIDEdit.Text == "")
            {
                lblGridMsg.Visible = true;
                lblGridMsg.Text = "Select Item.";
                txtItemNMEdit.Focus();
                lblSaleFrom.Visible = false;
                lblPartyID.Visible = false;
            }
            else if (txtQtyEdit.Text == "0")
            {
                lblGridMsg.Visible = true;
                lblGridMsg.Text = "Quantity is Wrong.";
                txtPQtyEdit.Focus();
            }
            else
            {
                Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNMEdit.Text + "'", lblCatID);

                DropDownList ddltypeEdit = (DropDownList)gvDetail.Rows[e.RowIndex].FindControl("ddltypeEdit");
                TextBox txtCPQTYEdit = (TextBox)gvDetail.Rows[e.RowIndex].FindControl("txtCPQTYEdit");
                if (txtCPQTYEdit.Text == "")
                {
                    txtCPQTYEdit.Text = "0";
                }
                else
                    txtCPQTYEdit.Text = txtCPQTYEdit.Text;
                decimal cpQty = 0;
                cpQty = Convert.ToDecimal(txtCPQTYEdit.Text);
                TextBox txtCQtyEdit = (TextBox)gvDetail.Rows[e.RowIndex].FindControl("txtCQtyEdit");
                if (txtCQtyEdit.Text == "")
                {
                    txtCQtyEdit.Text = "0";
                }
                else
                    txtCQtyEdit.Text = txtCQtyEdit.Text;
                decimal cQty = 0;
                cQty = Convert.ToDecimal(txtCQtyEdit.Text);
                decimal pQty = 0;
                if (txtPQtyEdit.Text == "")
                {
                    txtPQtyEdit.Text = "0";
                }
                else
                    txtPQtyEdit.Text = txtPQtyEdit.Text;
                pQty = Convert.ToDecimal(txtPQtyEdit.Text);

                decimal Qty = 0;
                if (txtQtyEdit.Text == "")
                {
                    txtQtyEdit.Text = "0";
                }
                else
                    txtQtyEdit.Text = txtQtyEdit.Text;
                Qty = Convert.ToDecimal(txtQtyEdit.Text);
                TextBox txtRateEdit = (TextBox)gvDetail.Rows[e.RowIndex].FindControl("txtRateEdit");
                decimal Rate = 0;
                Rate = Convert.ToDecimal(txtRateEdit.Text);
                TextBox txtAmountEdit = (TextBox)gvDetail.Rows[e.RowIndex].FindControl("txtAmountEdit");
                decimal Amount = 0;
                Amount = Convert.ToDecimal(txtAmountEdit.Text);
                Label lblTransSLEdit = (Label)gvDetail.Rows[e.RowIndex].FindControl("lblTransSLEdit");

                DateTime transdate = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");


                TextBox txtDisRtEdit = (TextBox)gvDetail.Rows[e.RowIndex].FindControl("txtDisRtEdit");
                if (txtDisRtEdit.Text == "")
                {
                    txtDisRtEdit.Text = "0";
                }
                else
                {
                    txtDisRtEdit.Text = txtDisRtEdit.Text;
                }
                decimal disRt = 0;
                disRt = Convert.ToDecimal(txtDisRtEdit.Text);

                TextBox txtDisAmtEdit = (TextBox)gvDetail.Rows[e.RowIndex].FindControl("txtDisAmtEdit");
                if (txtDisAmtEdit.Text == "")
                {
                    txtDisAmtEdit.Text = "0";
                }
                else
                {
                    txtDisAmtEdit.Text = txtDisAmtEdit.Text;
                }
                decimal disAmt = 0;
                disAmt = Convert.ToDecimal(txtDisAmtEdit.Text);

                TextBox txtNetAmtEdit = (TextBox)gvDetail.Rows[e.RowIndex].FindControl("txtNetAmtEdit");
                if (txtNetAmtEdit.Text == "")
                {
                    txtNetAmtEdit.Text = "0";
                }
                else
                {
                    txtNetAmtEdit.Text = txtNetAmtEdit.Text;
                }
                decimal NetAmt = 0;
                NetAmt = Convert.ToDecimal(txtNetAmtEdit.Text);




                if (btnSaleEdit.Text == "EDIT")
                {
                    Int64 TransNo = Convert.ToInt64(txtInNo.Text);

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update STK_TRANS set INVREFNO = '" + txtSLMNo.Text + "', STOREFR='" + lblStoreIDEdit.Text + "', PSID='" + txtPID.Text + "', REMARKS = '" + txtRemarks.Text + "', CATID = '" + lblCatID.Text + "', ITEMID = '" + lblItemIDEdit.Text + "', UNITTP = '" + ddltypeEdit.Text + "', " +
                          " CPQTY =" + cpQty + ", CQTY = " + cQty + ", PQTY = " + pQty + ", QTY = " + Qty + ", RATE = " + Rate + ", AMOUNT = " + Amount + ",  DISCRT = " + disRt + ", DISCAMT = " + disAmt + ", NETAMT = " + NetAmt + ", USERID = '" + userName + "' where TRANSTP = 'SALE' and TRANSMY = '" + lblSMY.Text + "' and TRANSNO = " + TransNo + " and TRANSSL = '" + lblTransSLEdit.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();



                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set INVREFNO = '" + txtSLMNo.Text + "', STOREFR='" + lblStoreIDEdit.Text + "', PSID='" + txtPID.Text + "', REMARKS = '" + txtRemarks.Text + "', " +
                          " USERID = '" + userName + "' where TRANSTP = 'SALE'  and TRANSMY='" + lblSMY.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                    cmd1.ExecuteNonQuery();
                    conn.Close();

                    gvDetail.EditIndex = -1;
                    GridShow();

                    txtTotAmt.Text = txtTotal.Text;
                    decimal totamt = Convert.ToDecimal(txtTotAmt.Text);
                    decimal grDisamt = Convert.ToDecimal(txtGrossDisAmt.Text);
                    decimal n_amt = totamt - grDisamt;
                    string n_Amt = SpellAmount.comma(n_amt);
                    txtNetAmt.Text = n_Amt;
                    txtGrossDisAmt.Focus();
                    lblSmsgComTrans.Visible = true;
                    lblSmsgComTrans.Text = "Complete Transaction By Changing Discount.";
                }
                else
                {
                    Int64 TransNo = Convert.ToInt64(ddlSalesEditInNo.Text);

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update STK_TRANS set INVREFNO = '" + txtSLMNo.Text + "', STOREFR='" + lblStoreIDEdit.Text + "', PSID='" + txtPID.Text + "', REMARKS = '" + txtRemarks.Text + "', CATID = '" + lblCatID.Text + "', ITEMID = '" + lblItemIDEdit.Text + "', UNITTP = '" + ddltypeEdit.Text + "', " +
                          " CPQTY =" + cpQty + ", CQTY = " + cQty + ", PQTY = " + pQty + ", QTY = " + Qty + ", RATE = " + Rate + ", AMOUNT = " + Amount + ", DISCRT = " + disRt + ", DISCAMT = " + disAmt + ", NETAMT = " + NetAmt + ", USERID = '" + userName + "' where TRANSTP = 'SALE' and TRANSMY = '" + lblSMY.Text + "' and TRANSNO = " + TransNo + " and TRANSSL = '" + lblTransSLEdit.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();



                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set INVREFNO = '" + txtSLMNo.Text + "', STOREFR='" + lblStoreIDEdit.Text + "', PSID='" + txtPID.Text + "', REMARKS = '" + txtRemarks.Text + "', " +
                          " USERID = '" + userName + "' where TRANSTP = 'SALE'  and TRANSMY='" + lblSMY.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                    cmd1.ExecuteNonQuery();
                    conn.Close();

                    gvDetail.EditIndex = -1;
                    GridShowSale_Edit();

                    txtTotAmt.Text = txtTAmount.Text;        //////////////////// this a change only made for fancy bd bd otherwise it is txtTotal.text
                    decimal totamt = Convert.ToDecimal(txtTotAmt.Text);
                    decimal grDisamt = Convert.ToDecimal(txtGrossDisAmt.Text);
                    decimal n_amt = totamt - grDisamt;
                    string n_Amt = SpellAmount.comma(n_amt);
                    txtNetAmt.Text = n_Amt;
                    txtGrossDisAmt.Focus();
                    lblSmsgComTrans.Visible = true;
                    lblSmsgComTrans.Text = "Complete Transaction By Changing Discount.";
                }

            }
        }

        protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT ITEMNM FROM STK_ITEM WHERE ITEMNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ITEMNM"].ToString());
            return CompletionSet.ToArray();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListEdit(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT ITEMNM FROM STK_ITEM WHERE ITEMNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ITEMNM"].ToString());
            return CompletionSet.ToArray();
        }

        //protected void txtSaleFrom_TextChanged(object sender, EventArgs e)
        //{
        //    Global.txtAdd(@"Select STOREID from STK_STORE where STORENM = '" + txtSaleFrom.Text + "'", txtSlFr);
        //    txtPNM.Focus();
        //}

        protected void txtSlFr_TextChanged(object sender, EventArgs e)
        {
            txtPNM.Focus();
        }

        protected void txtPNM_TextChanged(object sender, EventArgs e)
        {
            Global.txtAdd(@"select ACCOUNTCD from GL_ACCHART where STATUSCD='P' and ACCOUNTNM='" + txtPNM.Text + "'", txtPID);
            //ScriptManager sm = ScriptManager.GetCurrent(this);
            //sm.SetFocus(txtRemarks);
            txtRemarks.Focus();
        }

        protected void txtLC_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtStoreNM_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtStoreNM = (TextBox)row.FindControl("txtStoreNM");
            TextBox txtStoreID = (TextBox)row.FindControl("txtStoreID");
            txtStoreID.Text = "";

            Global.txtAdd(@"Select STOREID from STK_STORE where STORENM = '" + txtStoreNM.Text + "'", txtStoreID);
            TextBox txtItemNM = (TextBox)row.FindControl("txtItemNM");
            txtItemNM.Focus();
        }

        protected void txtItemNM_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            //NamingContainer return the container that the control sits in
            TextBox txtItemNM = (TextBox)row.FindControl("txtItemNM");
            TextBox txtItID = (TextBox)row.FindControl("txtItID");
            txtItID.ReadOnly = true;
            Global.txtAdd(@"Select ITEMID from STK_ITEM where ITEMNM = '" + txtItemNM.Text + "'", txtItID);
            TextBox txtCPQTY = (TextBox)row.FindControl("txtCPQTY");
            Global.txtAdd(@"Select PQTY from STK_ITEM where ITEMNM = '" + txtItemNM.Text + "'", txtCPQTY);
            txtCPQTY.ReadOnly = true;
            TextBox txtCQty = (TextBox)row.FindControl("txtCQty");
            txtCQty.Text = "0";
            TextBox txtPQty = (TextBox)row.FindControl("txtPQty");
            TextBox txtQty = (TextBox)row.FindControl("txtQty");
            txtQty.Text = "0";
            txtPQty.Text = "0";
            TextBox txtRate = (TextBox)row.FindControl("txtRate");
            Global.txtAdd(@"Select SALERT from STK_ITEM where ITEMNM = '" + txtItemNM.Text + "'", txtRate);
            //txtRate.ReadOnly = true;
            TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
            txtAmount.Text = ".00";
            txtAmount.ReadOnly = true;
            DropDownList ddlType = (DropDownList)row.FindControl("ddlType");
            ddlType.Focus();
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddlType = (DropDownList)row.FindControl("ddlType");
            if (ddlType.Text == "CARTON")
            {
                TextBox txtCQty = (TextBox)row.FindControl("txtCQty");
                txtCQty.Focus();
            }
            else
            {
                TextBox txtPQty = (TextBox)row.FindControl("txtPQty");
                txtPQty.Focus();
                TextBox txtCQty = (TextBox)row.FindControl("txtCQty");
                txtCQty.Text = "0";
            }
        }

        protected void txtCQty_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCPQTY = (TextBox)row.FindControl("txtCPQTY");
            Int64 CPQty = Convert.ToInt64(txtCPQTY.Text);
            TextBox txtCQty = (TextBox)row.FindControl("txtCQty");
            Int64 CQty = Convert.ToInt64(txtCQty.Text);
            TextBox txtPQty = (TextBox)row.FindControl("txtPQty");
            Int64 PQty;
            if (txtPQty.Text == "")
            {
                PQty = 0;
            }
            else
                PQty = Convert.ToInt64(txtPQty.Text);

            Int64 Qty = CPQty * CQty + PQty;
            TextBox txtQty = (TextBox)row.FindControl("txtQty");
            txtQty.Text = Qty.ToString();
            txtQty.ReadOnly = true;

            TextBox txtRate = (TextBox)row.FindControl("txtRate");
            decimal Rate = Convert.ToDecimal(txtRate.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
            txtAmount.Text = Amount.ToString();
            txtAmount.ReadOnly = true;
            TextBox txtNetAmt = (TextBox)row.FindControl("txtNetAmt");
            txtNetAmt.Text = Amount.ToString();
            txtNetAmt.ReadOnly = true;
            //TextBox txtNetAmt = (TextBox)row.FindControl("txtNetAmt");
            //txtNetAmt.Text = Amount.ToString();
            //txtNetAmt.ReadOnly = true;
            txtPQty.Focus();
        }

        protected void txtPQty_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCPQTY = (TextBox)row.FindControl("txtCPQTY");
            Int64 CPQty = Convert.ToInt64(txtCPQTY.Text);
            TextBox txtCQty = (TextBox)row.FindControl("txtCQty");
            Int64 CQty = Convert.ToInt64(txtCQty.Text);
            TextBox txtPQty = (TextBox)row.FindControl("txtPQty");
            Int64 PQty = Convert.ToInt64(txtPQty.Text);
            Int64 Qty = CPQty * CQty + PQty;
            TextBox txtQty = (TextBox)row.FindControl("txtQty");
            txtQty.Text = Qty.ToString();
            txtQty.ReadOnly = true;

            TextBox txtRate = (TextBox)row.FindControl("txtRate");
            decimal Rate = Convert.ToDecimal(txtRate.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
            txtAmount.Text = Amount.ToString();
            txtAmount.ReadOnly = true;

            //TextBox txtNetAmt = (TextBox)row.FindControl("txtNetAmt");
            //txtNetAmt.Text = Amount.ToString();
            //txtNetAmt.ReadOnly = true;

            txtRate.Focus();

        }

        protected void txtRate_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

            TextBox txtQty = (TextBox)row.FindControl("txtQty");
            Int64 Qty = Convert.ToInt64(txtQty.Text);

            TextBox txtRate = (TextBox)row.FindControl("txtRate");
            decimal Rate = Convert.ToDecimal(txtRate.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
            txtAmount.Text = Amount.ToString();
            txtAmount.ReadOnly = true;

            //TextBox txtNetAmt = (TextBox)row.FindControl("txtNetAmt");
            //txtNetAmt.Text = Amount.ToString();
            //txtNetAmt.ReadOnly = true;

            ImageButton imgbtnAdd = (ImageButton)row.FindControl("imgbtnAdd");
            imgbtnAdd.Focus();
        }

        protected void txtInDT_TextChanged(object sender, EventArgs e)
        {
            if (btnSaleEdit.Text == "EDIT")
            {
                DateTime transdate = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                string varYear = transdate.ToString("yyyy");
                string month = transdate.ToString("MMM").ToUpper();
                string years = transdate.ToString("yy");
                //lblSMY.Text = month + "-" + years;
                lblSMY.Text = varYear;
                Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblSMY.Text + "' and TRANSTP = 'SALE'", lblSMxNo);
                if (lblSMxNo.Text == "")
                {
                    txtInNo.Text = "1";
                }
                else
                {
                    int iNo = int.Parse(lblSMxNo.Text);
                    int totIno = iNo + 1;
                    txtInNo.Text = totIno.ToString();
                }

                txtSLMNo.Focus();
            }

            else
            {
                DateTime transdate = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                string varYear = transdate.ToString("yyyy");
                string month = transdate.ToString("MMM").ToUpper();
                string years = transdate.ToString("yy");
                //lblSMY.Text = month + "-" + years;
                lblSMY.Text = varYear;

                Global.dropDownAddWithSelect(ddlSalesEditInNo, "SELECT DISTINCT TRANSNO FROM STK_TRANS WHERE TRANSDT ='" + TrDate + "' AND TRANSMY='" + lblSMY.Text + "' and TRANSTP='SALE'");
                ddlSalesEditInNo.Focus();
            }
        }

        protected void txtLCDT_TextChanged(object sender, EventArgs e)
        {
            txtPNM.Focus();
        }

        protected void txtRemarks_TextChanged(object sender, EventArgs e)
        {
            TextBox txtStoreNM = (TextBox)gvDetail.FooterRow.FindControl("txtStoreNM");
            txtStoreNM.Focus();
        }

        protected void txtDisRt_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

            TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
            decimal Amount = Convert.ToDecimal(txtAmount.Text);

            TextBox txtDisRt = (TextBox)row.FindControl("txtDisRt");
            if (txtDisRt.Text == "")
            {
                txtDisRt.Text = "0";
            }
            else
                txtDisRt.Text = txtDisRt.Text;
            decimal DisRt = Convert.ToDecimal(txtDisRt.Text);
            decimal disRate = 0;
            if (DisRt >= 100)
            {
                lblGridMsg.Visible = true;
                lblGridMsg.Text = "Rate bellow than 100";
                txtDisRt.Focus();
            }
            else
            {
                lblGridMsg.Visible = false;
                disRate = DisRt;

                decimal DisRT_F = disRate / 100;
                decimal Amt = Convert.ToDecimal(string.Format("{0:0.00}", Amount * DisRT_F));

                TextBox txtDisAmt = (TextBox)row.FindControl("txtDisAmt");
                txtDisAmt.Text = Amt.ToString();

                decimal NetAmount = Convert.ToDecimal(string.Format("{0:0.00}", Amount - Amt));

                TextBox txtNetAmt = (TextBox)row.FindControl("txtNetAmt");
                txtNetAmt.Text = NetAmount.ToString();
                txtNetAmt.ReadOnly = true;

                txtDisAmt.Focus();
            }

        }

        protected void txtDisRtEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

            TextBox txtAmountEdit = (TextBox)row.FindControl("txtAmountEdit");
            decimal Amount = Convert.ToDecimal(txtAmountEdit.Text);

            TextBox txtDisRtEdit = (TextBox)row.FindControl("txtDisRtEdit");
            if (txtDisRtEdit.Text == "")
            {
                txtDisRtEdit.Text = "0";
            }
            else
                txtDisRtEdit.Text = txtDisRtEdit.Text;
            decimal DisRt = Convert.ToDecimal(txtDisRtEdit.Text);
            decimal disRate = 0;
            if (DisRt >= 100)
            {
                lblGridMsg.Visible = true;
                lblGridMsg.Text = "Rate bellow than 100";
                txtDisRtEdit.Focus();
            }
            else
            {
                lblGridMsg.Visible = false;
                disRate = DisRt;

                decimal DisRT_F = disRate / 100;
                decimal Amt = Convert.ToDecimal(string.Format("{0:0.00}", Amount * DisRT_F));

                TextBox txtDisAmtEdit = (TextBox)row.FindControl("txtDisAmtEdit");
                txtDisAmtEdit.Text = Amt.ToString();

                decimal NetAmount = Convert.ToDecimal(string.Format("{0:0.00}", Amount - Amt));

                TextBox txtNetAmtEdit = (TextBox)row.FindControl("txtNetAmtEdit");
                txtNetAmtEdit.Text = NetAmount.ToString();
                txtNetAmtEdit.ReadOnly = true;

                txtDisAmtEdit.Focus();
            }
        }

        protected void txtDisAmt_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

            TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
            decimal Amount = Convert.ToDecimal(txtAmount.Text);

            TextBox txtDisAmt = (TextBox)row.FindControl("txtDisAmt");
            if (txtDisAmt.Text == "")
            {
                txtDisAmt.Text = "0.00";
            }
            else
                txtDisAmt.Text = txtDisAmt.Text;
            decimal DisAmt = Convert.ToDecimal(txtDisAmt.Text);

            decimal NetAmount = Convert.ToDecimal(string.Format("{0:0.00}", Amount - DisAmt));

            TextBox txtNetAmt = (TextBox)row.FindControl("txtNetAmt");
            txtNetAmt.Text = NetAmount.ToString();
            txtNetAmt.ReadOnly = true;

            ImageButton imgbtnAdd = (ImageButton)row.FindControl("imgbtnAdd");
            imgbtnAdd.Focus();
        }

        protected void txtDisAmtEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

            TextBox txtAmountEdit = (TextBox)row.FindControl("txtAmountEdit");
            decimal Amount = Convert.ToDecimal(txtAmountEdit.Text);

            TextBox txtDisAmtEdit = (TextBox)row.FindControl("txtDisAmtEdit");
            if (txtDisAmtEdit.Text == "")
            {
                txtDisAmtEdit.Text = "0.00";
            }
            else
                txtDisAmtEdit.Text = txtDisAmtEdit.Text;
            decimal DisAmt = Convert.ToDecimal(txtDisAmtEdit.Text);

            decimal NetAmount = Convert.ToDecimal(string.Format("{0:0.00}", Amount - DisAmt));

            TextBox txtNetAmtEdit = (TextBox)row.FindControl("txtNetAmtEdit");
            txtNetAmtEdit.Text = NetAmount.ToString();
            txtNetAmtEdit.ReadOnly = true;

            ImageButton imgbtnUpdate = (ImageButton)row.FindControl("imgbtnUpdate");
            imgbtnUpdate.Focus();
        }

        protected void txtStoreNMEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtStoreNMEdit = (TextBox)row.FindControl("txtStoreNMEdit");
            Label lblStoreIDEdit = (Label)row.FindControl("lblStoreIDEdit");
            lblStoreIDEdit.Text = "";

            Global.lblAdd("Select STOREID from STK_STORE where STORENM = '" + txtStoreNMEdit.Text + "'", lblStoreIDEdit);
            TextBox txtItemNMEdit = (TextBox)row.FindControl("txtItemNMEdit");
            txtItemNMEdit.Focus();
        }

        protected void txtItemNMEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            //NamingContainer return the container that the control sits in
            TextBox txtItemNMEdit = (TextBox)row.FindControl("txtItemNMEdit");
            Label lblItemIDEdit = (Label)row.FindControl("lblItemIDEdit");
            Global.lblAdd(@"Select ITEMID from STK_ITEM where ITEMNM = '" + txtItemNMEdit.Text + "'", lblItemIDEdit);
            TextBox txtCPQTYEdit = (TextBox)row.FindControl("txtCPQTYEdit");
            Global.txtAdd(@"Select PQTY from STK_ITEM where ITEMNM = '" + txtItemNMEdit.Text + "'", txtCPQTYEdit);
            txtCPQTYEdit.ReadOnly = true;
            TextBox txtCQtyEdit = (TextBox)row.FindControl("txtCQtyEdit");
            txtCQtyEdit.Text = "0";
            TextBox txtPQtyEdit = (TextBox)row.FindControl("txtPQtyEdit");
            TextBox txtQtyEdit = (TextBox)row.FindControl("txtQtyEdit");
            txtQtyEdit.Text = "0";
            txtPQtyEdit.Text = "0";
            TextBox txtRateEdit = (TextBox)row.FindControl("txtRateEdit");
            Global.txtAdd(@"Select SALERT from STK_ITEM where ITEMNM = '" + txtItemNMEdit.Text + "'", txtRateEdit);
            //txtRateEdit.ReadOnly = true;
            TextBox txtAmountEdit = (TextBox)row.FindControl("txtAmountEdit");
            txtAmountEdit.Text = ".00";
            txtAmountEdit.ReadOnly = true;
            DropDownList ddltypeEdit = (DropDownList)row.FindControl("ddltypeEdit");
            ddltypeEdit.Focus();
        }

        protected void ddltypeEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddltypeEdit = (DropDownList)row.FindControl("ddltypeEdit");
            if (ddltypeEdit.Text == "CARTON")
            {
                TextBox txtCQtyEdit = (TextBox)row.FindControl("txtCQtyEdit");
                txtCQtyEdit.Focus();
            }
            else
            {
                TextBox txtPQtyEdit = (TextBox)row.FindControl("txtPQtyEdit");
                txtPQtyEdit.Focus();
                TextBox txtCQtyEdit = (TextBox)row.FindControl("txtCQtyEdit");
                txtCQtyEdit.Text = "0";
            }
        }

        protected void txtCQtyEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCPQTYEdit = (TextBox)row.FindControl("txtCPQTYEdit");
            Int64 CPQty = Convert.ToInt64(txtCPQTYEdit.Text);
            TextBox txtCQtyEdit = (TextBox)row.FindControl("txtCQtyEdit");
            Int64 CQty = Convert.ToInt64(txtCQtyEdit.Text);
            TextBox txtPQtyEdit = (TextBox)row.FindControl("txtPQtyEdit");
            Int64 PQty;
            if (txtPQtyEdit.Text == "")
            {
                PQty = 0;
            }
            else
                PQty = Convert.ToInt64(txtPQtyEdit.Text);

            Int64 Qty = CPQty * CQty + PQty;
            TextBox txtQtyEdit = (TextBox)row.FindControl("txtQtyEdit");
            txtQtyEdit.Text = Qty.ToString();
            txtQtyEdit.ReadOnly = true;

            TextBox txtRateEdit = (TextBox)row.FindControl("txtRateEdit");
            decimal Rate = Convert.ToDecimal(txtRateEdit.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmountEdit = (TextBox)row.FindControl("txtAmountEdit");
            txtAmountEdit.Text = Amount.ToString();
            txtAmountEdit.ReadOnly = true;
            TextBox txtNetAmtEdit = (TextBox)row.FindControl("txtNetAmtEdit");
            txtNetAmtEdit.Text = Amount.ToString();
            txtNetAmtEdit.ReadOnly = true;
            txtPQtyEdit.Focus();
        }

        protected void txtPQtyEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCPQTYEdit = (TextBox)row.FindControl("txtCPQTYEdit");
            Int64 CPQty = Convert.ToInt64(txtCPQTYEdit.Text);
            TextBox txtCQtyEdit = (TextBox)row.FindControl("txtCQtyEdit");
            Int64 CQty = Convert.ToInt64(txtCQtyEdit.Text);
            TextBox txtPQtyEdit = (TextBox)row.FindControl("txtPQtyEdit");
            Int64 PQty = Convert.ToInt64(txtPQtyEdit.Text);
            Int64 Qty = CPQty * CQty + PQty;
            TextBox txtQtyEdit = (TextBox)row.FindControl("txtQtyEdit");
            txtQtyEdit.Text = Qty.ToString();
            txtQtyEdit.ReadOnly = true;

            TextBox txtRateEdit = (TextBox)row.FindControl("txtRateEdit");
            decimal Rate = Convert.ToDecimal(txtRateEdit.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmountEdit = (TextBox)row.FindControl("txtAmountEdit");
            txtAmountEdit.Text = Amount.ToString();
            txtAmountEdit.ReadOnly = true;

            txtRateEdit.Focus();
            //ImageButton imgbtnUpdate = (ImageButton)row.FindControl("imgbtnUpdate");
            //imgbtnUpdate.Focus();
        }

        protected void txtRateEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

            TextBox txtQtyEdit = (TextBox)row.FindControl("txtQtyEdit");
            Int64 Qty = Convert.ToInt64(txtQtyEdit.Text);

            TextBox txtRateEdit = (TextBox)row.FindControl("txtRateEdit");
            decimal Rate = Convert.ToDecimal(txtRateEdit.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmountEdit = (TextBox)row.FindControl("txtAmountEdit");
            txtAmountEdit.Text = Amount.ToString();
            txtAmountEdit.ReadOnly = true;
            TextBox txtNetAmtEdit = (TextBox)row.FindControl("txtNetAmtEdit");
            txtNetAmtEdit.Text = Amount.ToString();
            txtNetAmtEdit.ReadOnly = true;

            ImageButton imgbtnUpdate = (ImageButton)row.FindControl("imgbtnUpdate");
            imgbtnUpdate.Focus();
        }

        /// <summary>
        /// Editing of Store Transaction Sale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnSaleEdit_Click(object sender, EventArgs e)
        {
            if (btnSaleEdit.Text == "EDIT")
            {
                DateTime transdate = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                txtInNo.Visible = false;
                btnSaleEdit.Text = "NEW";
                //btnPrint.Visible = true;
                //btnDoPrint.Visible = true;
                ddlSalesEditInNo.Visible = true;
                Global.dropDownAddWithSelect(ddlSalesEditInNo, "SELECT DISTINCT TRANSNO FROM STK_TRANS WHERE TRANSDT ='" + TrDate + "' AND TRANSMY='" + lblSMY.Text + "' and TRANSTP='SALE'");
                txtSLMNo.Text = "";
                //txtSaleFrom.Text = "";
                //txtSlFr.Text = "";
                txtPNM.Text = "";
                txtPID.Text = "";
                txtRemarks.Text = "";
                lblSmsgComTrans.Visible = false;
                //lblSMY.Text = "";
                txtTotAmt.Text = "0.00";
                txtGrossDisAmt.Text = "0.00";
                txtNetAmt.Text = "0.00";
                txtTAmount.Text = "0.00";
                txtTDisAmount.Text = "0.00";
                txtTotal.Text = "0.00";
                txtLtCost.Text = "0.00";
                GridShowSale_Edit();
            }
            else
            {
                txtInNo.Visible = true;
                btnSaleEdit.Text = "EDIT";
                //btnPrint.Visible = false;
                //btnDoPrint.Visible = false;
                ddlSalesEditInNo.Visible = false;
                txtSLMNo.Text = "";
                //txtSaleFrom.Text = "";
                //txtSlFr.Text = "";
                txtPNM.Text = "";
                txtPID.Text = "";
                txtRemarks.Text = "";
                lblSmsgComTrans.Visible = false;
                txtTotAmt.Text = "0.00";
                txtGrossDisAmt.Text = "0.00";
                txtNetAmt.Text = "0.00";
                txtTAmount.Text = "0.00";
                txtTDisAmount.Text = "0.00";
                txtTotal.Text = "0.00";
                txtLtCost.Text = "0.00";
                Start();
            }
        }

        protected void ddlSalesEditInNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            DateTime transdate = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = transdate.ToString("yyyy/MM/dd");

            if (ddlSalesEditInNo.Text == "Select")
            {
                gvDetail.Visible = false;
                lblGridMsg.Visible = true;
                lblGridMsg.Text = "Type Invoice No.";
                txtTotal.Text = "";
            }
            else
            {
                gvDetail.Visible = true;
                lblGridMsg.Visible = false;
                Int64 TransNo = Convert.ToInt64(ddlSalesEditInNo.Text);

                Global.txtAdd(@"select INVREFNO from STK_TRANSMST where TRANSTP='SALE' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblSMY.Text + "' and TRANSNO =" + TransNo + "", txtSLMNo);
                //Global.txtAdd(@"select STOREFR from STK_TRANSMST where TRANSTP='SALE' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblSMY.Text + "' and TRANSNO =" + TransNo + "", txtSlFr);
                //Global.txtAdd(@"select STORENM from STK_STORE where STOREID ='" + txtSlFr.Text + "'", txtSaleFrom);
                Global.txtAdd(@"select PSID from STK_TRANSMST where TRANSTP='SALE' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblSMY.Text + "' and TRANSNO =" + TransNo + "", txtPID);
                Global.txtAdd(@"select ACCOUNTNM from GL_ACCHART where ACCOUNTCD ='" + txtPID.Text + "'", txtPNM);
                Global.txtAdd(@"select REMARKS from STK_TRANSMST where TRANSTP='SALE' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblSMY.Text + "' and TRANSNO =" + TransNo + "", txtRemarks);
                Global.txtAdd(@"select TOTAMT from STK_TRANSMST where TRANSTP='SALE' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblSMY.Text + "' and TRANSNO =" + TransNo + "", txtTotAmt);
                Global.txtAdd(@"select DISAMT from STK_TRANSMST where TRANSTP='SALE' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblSMY.Text + "' and TRANSNO =" + TransNo + "", txtGrossDisAmt);
                Global.txtAdd(@"select LTCOST from STK_TRANSMST where TRANSTP='SALE' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblSMY.Text + "' and TRANSNO =" + TransNo + "", txtLtCost);
                Global.txtAdd(@"select TOTNET from STK_TRANSMST where TRANSTP='SALE' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblSMY.Text + "' and TRANSNO =" + TransNo + "", txtNetAmt);
                GridShowSale_Edit();
            }
        }

        protected void GridShowSale_Edit()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            DateTime transdate = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = transdate.ToString("yyyy/MM/dd");

            Int64 ddlSalesEdit = 0;
            if (ddlSalesEditInNo.Text == "Select")
            {
                ddlSalesEdit = 0;
            }
            else
                ddlSalesEdit = Convert.ToInt64(ddlSalesEditInNo.Text);

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT STK_ITEM.ITEMNM, STK_TRANS.TRANSTP, STK_TRANS.TRANSDT, STK_TRANS.TRANSMY, STK_TRANS.TRANSNO, STK_TRANS.INVREFNO, STK_TRANS.STOREFR, " +
                                            " STK_TRANS.STORETO, STK_TRANS.PSID, STK_TRANS.LCTP, STK_TRANS.LCID, STK_TRANS.LCDATE, STK_TRANS.REMARKS, STK_TRANS.TRANSSL, STK_TRANS.CATID, STK_TRANS.ITEMID, STK_TRANS.UNITTP, STK_TRANS.CPQTY, STK_TRANS.CQTY, STK_TRANS.PQTY, STK_TRANS.QTY, STK_TRANS.RATE, " +
                                            " STK_TRANS.AMOUNT, STK_TRANS.DISCRT, STK_TRANS.DISCAMT, STK_TRANS.NETAMT, STK_TRANS.USERPC, STK_TRANS.USERID, STK_TRANS.ACTDTI, STK_TRANS.INTIME, STK_TRANS.IPADDRESS, STK_STORE.STORENM " +
                                            " FROM STK_ITEM INNER JOIN STK_TRANS ON STK_ITEM.ITEMID = STK_TRANS.ITEMID INNER JOIN STK_STORE ON STK_TRANS.STOREFR = STK_STORE.STOREID " +
                                            " WHERE     (STK_TRANS.TRANSTP = 'SALE') and STK_TRANS.TRANSDT = '" + TrDt + "' and STK_TRANS.TRANSMY = '" + lblSMY.Text + "' and STK_TRANS.TRANSNO = " + ddlSalesEdit + " order by TRANSSL", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetail.DataSource = ds;
                gvDetail.DataBind();

                if (gvDetail.EditIndex == -1)
                {
                    decimal totCar = 0;
                    decimal totQuan = 0;
                    Decimal totAmt = 0;
                    Decimal a = 0;
                    decimal b = 0;
                    decimal c = 0;
                    decimal dis = 0;
                    decimal disAmt = 0;
                    decimal net = 0;
                    decimal netAmt = 0;
                    foreach (GridViewRow grid in gvDetail.Rows)
                    {
                        Label lblCQty = (Label)grid.Cells[4].FindControl("lblCQty");
                        Label lblQty = (Label)grid.Cells[6].FindControl("lblQty");
                        Label Per = (Label)grid.Cells[8].FindControl("lblAmount");
                        Label lblDisAmt = (Label)grid.Cells[10].FindControl("lblDisAmt");
                        Label lblNetAmt = (Label)grid.Cells[11].FindControl("lblNetAmt");

                        if (lblCQty.Text == "")
                        {
                            lblCQty.Text = "0.00";
                        }
                        else
                        {
                            lblCQty.Text = lblCQty.Text;
                        }
                        String cQty = lblCQty.Text;
                        totCar = Convert.ToDecimal(cQty);
                        b += totCar;
                        string tCqty = SpellAmount.comma(b);
                        txtTCarton.Text = tCqty;

                        if (lblQty.Text == "")
                        {
                            lblQty.Text = "0.00";
                        }
                        else
                        {
                            lblQty.Text = lblQty.Text;
                        }
                        String Qty = lblQty.Text;
                        totQuan = Convert.ToDecimal(Qty);
                        c += totQuan;
                        string tQty = SpellAmount.comma(c);
                        txtTQuantity.Text = tQty;

                        if (Per.Text == "")
                        {
                            Per.Text = "0.00";
                        }
                        else
                        {
                            Per.Text = Per.Text;
                        }
                        String Perf = Per.Text;
                        totAmt = Convert.ToDecimal(Perf);
                        a += totAmt;
                        string tAmount = SpellAmount.comma(a);
                        txtTAmount.Text = tAmount;

                        if (lblDisAmt.Text == "")
                        {
                            lblDisAmt.Text = "0.00";
                        }
                        else
                            lblDisAmt.Text = lblDisAmt.Text;

                        dis = Convert.ToDecimal(lblDisAmt.Text);
                        disAmt += dis;
                        string tDamt = SpellAmount.comma(disAmt);
                        txtTDisAmount.Text = tDamt;

                        if (lblNetAmt.Text == "")
                        {
                            lblNetAmt.Text = "0.00";
                        }
                        else
                            lblNetAmt.Text = lblNetAmt.Text;

                        net = Convert.ToDecimal(lblNetAmt.Text);
                        netAmt += net;
                        string nAmount = SpellAmount.comma(netAmt);
                        txtTotal.Text = nAmount;

                        //txtTotAmt.Text = nAmount;
                        //txtGrossDisAmt.Text = "0.00";
                        //txtNetAmt.Text = nAmount;
                    }
                    a += totAmt;
                    disAmt += dis;
                    netAmt += net;
                }
                else
                {

                }

                TextBox txtStoreNM = (TextBox)gvDetail.FooterRow.FindControl("txtStoreNM");
                txtStoreNM.Focus();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvDetail.DataSource = ds;
                gvDetail.DataBind();
                int columncount = gvDetail.Rows[0].Cells.Count;
                gvDetail.Rows[0].Cells.Clear();
                gvDetail.Rows[0].Cells.Add(new TableCell());
                gvDetail.Rows[0].Cells[0].ColumnSpan = columncount;
                gvDetail.Rows[0].Cells[0].Text = "No Records Found";
                gvDetail.Rows[0].Visible = false;

                txtSLMNo.Text = "";
                //txtSaleFrom.Text = "";
                //txtSlFr.Text = "";
                txtPID.Text = "";
                txtPNM.Text = "";
                txtRemarks.Text = "";
                ddlSalesEditInNo.SelectedIndex = -1;
            }
        }

        protected void txtGrossDisAmt_TextChanged(object sender, EventArgs e)
        {
            decimal totAmt = 0;
            decimal grDisAmt = 0;
            decimal ntAmt = 0;

            totAmt = Convert.ToDecimal(txtTotAmt.Text);
            if (txtGrossDisAmt.Text == "")
            {
                txtGrossDisAmt.Text = "0";
            }
            else
                txtGrossDisAmt.Text = txtGrossDisAmt.Text;
            grDisAmt = Convert.ToDecimal(txtGrossDisAmt.Text);
            ntAmt = totAmt - grDisAmt;
            string NetAmount = SpellAmount.comma(ntAmt);
            txtNetAmt.Text = NetAmount;
            txtLtCost.Focus();
        }

        protected void txtLtCost_TextChanged(object sender, EventArgs e)
        {
            decimal totAmt = 0;
            decimal grDisAmt = 0;
            decimal ltCost = 0;
            decimal ntAmt = 0;

            totAmt = Convert.ToDecimal(txtTotAmt.Text);
            if (txtGrossDisAmt.Text == "")
            {
                txtGrossDisAmt.Text = "0";
            }
            else
                txtGrossDisAmt.Text = txtGrossDisAmt.Text;
            grDisAmt = Convert.ToDecimal(txtGrossDisAmt.Text);
            if (txtLtCost.Text == "")
            {
                txtLtCost.Text = "0";
            }
            else
                txtLtCost.Text = txtLtCost.Text;
            ltCost = Convert.ToDecimal(txtLtCost.Text);
            ntAmt = (totAmt + ltCost) - grDisAmt;
            string NetAmount = SpellAmount.comma(ntAmt);
            txtNetAmt.Text = NetAmount;
            btnComplete.Focus();
        }

        protected void btnComplete_Click(object sender, EventArgs e)
        {
            lblSmsgComTrans.Visible = false;

            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string userName = HttpContext.Current.Session["UserName"].ToString();

            DateTime transdate = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDate = transdate.ToString("yyyy/MM/dd");

            decimal totamt = Convert.ToDecimal(txtTotAmt.Text);
            decimal grsDis = Convert.ToDecimal(txtGrossDisAmt.Text);
            decimal ltCost = Convert.ToDecimal(txtLtCost.Text);
            decimal NetAmt = Convert.ToDecimal(txtNetAmt.Text);

            if ((totamt + ltCost) - grsDis == NetAmt)
            {
                if (btnSaleEdit.Text == "EDIT")
                {
                    Int64 TransNo = Convert.ToInt64(txtInNo.Text);
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", LTCOST = " + ltCost + ", TOTNET = " + NetAmt + ", " +
                          " USERID = '" + userName + "' where TRANSTP = 'SALE'  and TRANSMY='" + lblSMY.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                    cmd1.ExecuteNonQuery();
                    conn.Close();

                    ///////Refresh/////
                    ddlSalesEditInNo.SelectedIndex = -1;
                    txtSLMNo.Text = "";
                    //txtSaleFrom.Text = "";
                    //txtSlFr.Text = "";
                    txtPNM.Text = "";
                    txtPID.Text = "";
                    txtRemarks.Text = "";
                    txtTotAmt.Text = "0.00";
                    txtGrossDisAmt.Text = "0.00";
                    txtNetAmt.Text = "0.00";
                    txtTAmount.Text = "0.00";
                    txtTDisAmount.Text = "0.00";
                    txtTotal.Text = "0.00";
                    txtLtCost.Text = "0.00";

                    Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblSMY.Text + "' and TRANSTP = 'SALE'", lblSMxNo);
                    if (lblSMxNo.Text == "")
                    {
                        txtInNo.Text = "1";
                    }
                    else
                    {
                        int iNo = int.Parse(lblSMxNo.Text);
                        int totIno = iNo + 1;
                        txtInNo.Text = totIno.ToString();
                    }

                    GridShow();
                    //Up_Sales.Update();
                    txtSLMNo.Focus();
                }
                else
                {
                    Int64 TransNo = Convert.ToInt64(ddlSalesEditInNo.Text);
                    if (ddlSalesEditInNo.Text == "Select")
                    {
                        lblGridMsg.Visible = true;
                        lblGridMsg.Text = "Select Invoice No.";
                    }
                    else
                    {
                        lblGridMsg.Visible = false;

                        conn.Open();
                        SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", LTCOST = " + ltCost + ", TOTNET = " + NetAmt + ", " +
                              " USERID = '" + userName + "' where TRANSTP = 'SALE'  and TRANSMY='" + lblSMY.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                        cmd1.ExecuteNonQuery();
                        conn.Close();

                        conn.Open();
                        SqlCommand cmd2 = new SqlCommand("update STK_TRANSMST set INVREFNO='" + txtSLMNo.Text + "', PSID = '" + txtPID.Text + "', " +
                              " REMARKS = '" + txtRemarks.Text + "',USERID = '" + userName + "' where TRANSTP = 'SALE'  and TRANSMY='" + lblSMY.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                        cmd2.ExecuteNonQuery();
                        conn.Close();

                        conn.Open();
                        SqlCommand cmd3 = new SqlCommand("update  STK_TRANS set INVREFNO='" + txtSLMNo.Text + "', PSID = '" + txtPID.Text + "', " +
                              " REMARKS = '" + txtRemarks.Text + "',USERID = '" + userName + "' where TRANSTP = 'SALE'  and TRANSMY='" + lblSMY.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                        cmd3.ExecuteNonQuery();
                        conn.Close();

                        ///////Refresh/////
                        ddlSalesEditInNo.SelectedIndex = -1;
                        txtSLMNo.Text = "";
                        //txtSaleFrom.Text = "";
                        //txtSlFr.Text = "";
                        txtPNM.Text = "";
                        txtPID.Text = "";
                        txtRemarks.Text = "";
                        txtTotAmt.Text = "0.00";
                        txtGrossDisAmt.Text = "0.00";
                        txtNetAmt.Text = "0.00";
                        txtTAmount.Text = "0.00";
                        txtTDisAmount.Text = "0.00";
                        txtTotal.Text = "0.00";
                        txtLtCost.Text = "0.00";

                        GridShowSale_Edit();
                        //Up_Sales.Update();
                        ddlSalesEditInNo.Focus();
                    }
                }
            }
            else
            {
                decimal com_NetAmt = totamt - grsDis;

                if (btnSaleEdit.Text == "EDIT")
                {
                    Int64 TransNo = Convert.ToInt64(txtInNo.Text);
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", LTCOST = " + ltCost + ", TOTNET = " + com_NetAmt + ", " +
                          " USERID = '" + userName + "' where TRANSTP = 'SALE'  and TRANSMY='" + lblSMY.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                    cmd1.ExecuteNonQuery();
                    conn.Close();

                    ///////Refresh/////
                    ddlSalesEditInNo.SelectedIndex = -1;
                    txtSLMNo.Text = "";
                    //txtSaleFrom.Text = "";
                    //txtSlFr.Text = "";
                    txtPNM.Text = "";
                    txtPID.Text = "";
                    txtRemarks.Text = "";
                    txtTotAmt.Text = "0.00";
                    txtGrossDisAmt.Text = "0.00";
                    txtNetAmt.Text = "0.00";
                    txtTAmount.Text = "0.00";
                    txtTDisAmount.Text = "0.00";
                    txtTotal.Text = "0.00";
                    txtLtCost.Text = "0.00";

                    Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblSMY.Text + "' and TRANSTP = 'SALE'", lblSMxNo);
                    if (lblSMxNo.Text == "")
                    {
                        txtInNo.Text = "1";
                    }
                    else
                    {
                        int iNo = int.Parse(lblSMxNo.Text);
                        int totIno = iNo + 1;
                        txtInNo.Text = totIno.ToString();
                    }

                    GridShow();
                    //Up_Sales.Update();
                    txtSLMNo.Focus();
                }
                else
                {
                    Int64 TransNo = Convert.ToInt64(ddlSalesEditInNo.Text);
                    if (ddlSalesEditInNo.Text == "Select")
                    {
                        lblGridMsg.Visible = true;
                        lblGridMsg.Text = "Select Invoice No.";
                    }
                    else
                    {
                        lblGridMsg.Visible = false;

                        conn.Open();
                        SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", LTCOST = " + ltCost + ", TOTNET = " + com_NetAmt + ", " +
                              " USERID = '" + userName + "' where TRANSTP = 'SALE'  and TRANSMY='" + lblSMY.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                        cmd1.ExecuteNonQuery();
                        conn.Close();

                        ///////Refresh/////
                        ddlSalesEditInNo.SelectedIndex = -1;
                        txtSLMNo.Text = "";
                        //txtSaleFrom.Text = "";
                        //txtSlFr.Text = "";
                        txtPNM.Text = "";
                        txtPID.Text = "";
                        txtRemarks.Text = "";
                        txtTotAmt.Text = "0.00";
                        txtGrossDisAmt.Text = "0.00";
                        txtNetAmt.Text = "0.00";
                        txtTAmount.Text = "0.00";
                        txtTDisAmount.Text = "0.00";
                        txtTotal.Text = "0.00";
                        txtLtCost.Text = "0.00";

                        GridShowSale_Edit();
                        //Up_Sales.Update();
                        ddlSalesEditInNo.Focus();
                    }
                }

            }


        }

        //////////////////////////////////////////////////////////// END EDITING PORTION OF SALES ///////////////////////////////

        //////////////////////////////////////////////////////////// END SALE PORTION ////////////////////////////////////////////

        /////////////////////////////////////////////////////////// START PURCHASE //////////////////////////////////////////////

        public void Purchase_Start()
        {

            DateTime today = DateTime.Today.Date;
            string td = Global.Dayformat(today);
            txtPInDT.Text = td;
            DateTime transdate = DateTime.Parse(txtPInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDate = transdate.ToString("yyyy/MM/dd");

            string varYear = today.ToString("yyyy");
            string mon = DateTime.Today.Date.ToString("MMM").ToUpper();
            string year = today.ToString("yy");
            //lblPMy.Text = mon + "-" + year;
            lblPMy.Text = varYear;
            Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblPMy.Text + "' and TRANSTP = 'BUY'", lblPMxNo);
            if (lblPMxNo.Text == "")
            {
                txtPInNo.Text = "1";
            }
            else
            {
                int iNo = int.Parse(lblPMxNo.Text);
                int totIno = iNo + 1;
                txtPInNo.Text = totIno.ToString();
            }

            GridShow_Purchase();
        }

        protected void GridShow_Purchase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            DateTime transdate = DateTime.Parse(txtPInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = transdate.ToString("yyyy/MM/dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT STK_ITEM.ITEMNM, STK_TRANS.* FROM STK_ITEM INNER JOIN STK_TRANS ON STK_ITEM.ITEMID = STK_TRANS.ITEMID where STK_TRANS.TRANSTP='BUY' and STK_TRANS.TRANSDT = '" + TrDt + "' and STK_TRANS.TRANSMY = '" + lblPMy.Text + "' and STK_TRANS.TRANSNO = '" + txtPInNo.Text + "' order by TRANSSL", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPurchase.DataSource = ds;
                gvPurchase.DataBind();

                if (gvPurchase.EditIndex == -1)
                {
                    Decimal totAmt = 0;
                    Decimal a = 0;
                    foreach (GridViewRow grid in gvPurchase.Rows)
                    {
                        Label Per = (Label)grid.Cells[8].FindControl("lblAmount_P");
                        if (Per.Text == "")
                        {
                            Per.Text = "0";
                        }
                        else
                        {
                            Per.Text = Per.Text;
                        }
                        String Perf = Per.Text;
                        totAmt = Convert.ToDecimal(Perf);
                        a += totAmt;
                        txtPTotal.Text = a.ToString();
                    }
                    a += totAmt;
                }
                else
                {

                }

                TextBox txtItemNM_P = (TextBox)gvPurchase.FooterRow.FindControl("txtItemNM_P");
                txtItemNM_P.Focus();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvPurchase.DataSource = ds;
                gvPurchase.DataBind();
                int columncount = gvPurchase.Rows[0].Cells.Count;
                gvPurchase.Rows[0].Cells.Clear();
                gvPurchase.Rows[0].Cells.Add(new TableCell());
                gvPurchase.Rows[0].Cells[0].ColumnSpan = columncount;
                gvPurchase.Rows[0].Cells[0].Text = "No Records Found";
                gvPurchase.Rows[0].Visible = false;
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListPStore(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT STORENM FROM STK_STORE WHERE STORENM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["STORENM"].ToString());
            return CompletionSet.ToArray();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListSupplier(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,5) IN ('20202') and ACCOUNTNM LIKE '" + prefixText + "%' AND STATUSCD = 'P'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionPList(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT ITEMNM FROM STK_ITEM WHERE ITEMNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ITEMNM"].ToString());
            return CompletionSet.ToArray();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListPEdit(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT ITEMNM FROM STK_ITEM WHERE ITEMNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ITEMNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void gvPurchase_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (btnPurchaseEdit.Text == "EDIT")
            {
                gvPurchase.EditIndex = -1;
                GridShow_Purchase();
            }
            else
            {
                gvPurchase.EditIndex = -1;
                GridShowPurchase_Edit();
            }
        }

        protected void gvPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string query = "";
            SqlCommand comm = new SqlCommand(query, conn);

            string Transtp = "BUY";


            DateTime TransDT = DateTime.Parse(txtPInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = TransDT.ToString("yyyy/MM/dd");

            DateTime LCDT = new DateTime();
            string lcDate = "";
            if (txtPLCDT.Text == "")
            {
                lcDate = "";
            }
            else
            {
                LCDT = DateTime.Parse(txtPLCDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lcDate = LCDT.ToString("yyyy/MM/dd");
            }


            if (e.CommandName.Equals("SaveCon"))
            {
                TextBox txtItemID_P = (TextBox)gvPurchase.FooterRow.FindControl("txtItemID_P");
                TextBox txtItemNM_P = (TextBox)gvPurchase.FooterRow.FindControl("txtItemNM_P");
                TextBox txtQty_P = (TextBox)gvPurchase.FooterRow.FindControl("txtQty_P");
                TextBox txtPQty_P = (TextBox)gvPurchase.FooterRow.FindControl("txtPQty_P");
                if (txtPToID.Text == "")
                {
                    lblPValidMsg.Visible = true;
                    lblPValidMsg.Text = "Select Store.";
                    txtPTo.Focus();
                    lblValidMSG.Visible = false;
                    lblPGridMSG.Visible = false;
                }
                else if (ddlPLCType.Text == "LOCAL" && txtPSupID.Text == "")
                {
                    lblValidMSG.Visible = true;
                    lblValidMSG.Text = "Select Party";
                    txtPSupplier.Focus();
                    lblPValidMsg.Visible = false;
                    lblPGridMSG.Visible = false;
                }
                else if (txtItemID_P.Text == "")
                {
                    lblPGridMSG.Visible = true;
                    lblPGridMSG.Text = "Select Item.";
                    txtItemNM_P.Focus();
                    lblPValidMsg.Visible = false;
                    lblValidMSG.Visible = false;
                }
                else if (txtQty_P.Text == "0")
                {
                    lblPGridMSG.Visible = true;
                    lblPGridMSG.Text = "Quantity is Wrong.";
                    txtPQty_P.Focus();
                }
                else
                {
                    lblPValidMsg.Visible = false;
                    lblValidMSG.Visible = false;
                    lblPGridMSG.Visible = false;

                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    if (btnPurchaseEdit.Text == "EDIT")
                    {
                        cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + txtPInNo.Text + "' and TRANSTP='BUY' AND TRANSMY='" + lblPMy.Text + "'", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + ddlPurchaseEditInNo.Text + "' and TRANSTP='BUY' AND TRANSMY='" + lblPMy.Text + "'", conn);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    conn.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM_P.Text + "'", lblPCatID);

                        DropDownList ddlType_P = (DropDownList)gvPurchase.FooterRow.FindControl("ddlType_P");
                        TextBox txtCPQTY_P = (TextBox)gvPurchase.FooterRow.FindControl("txtCPQTY_P");
                        if (txtCPQTY_P.Text == "")
                        {
                            txtCPQTY_P.Text = "0";
                        }
                        else
                            txtCPQTY_P.Text = txtCPQTY_P.Text;
                        decimal cpQty = 0;
                        cpQty = Convert.ToDecimal(txtCPQTY_P.Text);
                        TextBox txtCQty_P = (TextBox)gvPurchase.FooterRow.FindControl("txtCQty_P");
                        if (txtCQty_P.Text == "")
                        {
                            txtCQty_P.Text = "0";
                        }
                        else
                            txtCQty_P.Text = txtCQty_P.Text;
                        decimal cQty = 0;
                        cQty = Convert.ToDecimal(txtCQty_P.Text);
                        decimal pQty = 0;
                        if (txtPQty_P.Text == "")
                        {
                            txtPQty_P.Text = "0";
                        }
                        else
                            txtPQty_P.Text = txtPQty_P.Text;
                        pQty = Convert.ToDecimal(txtPQty_P.Text);

                        decimal Qty = 0;
                        if (txtQty_P.Text == "")
                        {
                            txtQty_P.Text = "0";
                        }
                        else
                            txtQty_P.Text = txtQty_P.Text;
                        Qty = Convert.ToDecimal(txtQty_P.Text);
                        TextBox txtRate_P = (TextBox)gvPurchase.FooterRow.FindControl("txtRate_P");
                        decimal Rate = 0;
                        Rate = Convert.ToDecimal(txtRate_P.Text);
                        TextBox txtAmount_P = (TextBox)gvPurchase.FooterRow.FindControl("txtAmount_P");
                        decimal Amount = 0;
                        Amount = Convert.ToDecimal(txtAmount_P.Text);

                        Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'BUY' and TRANSMY = '" + lblPMy.Text + "'", lblPTransSL);
                        string ItemCD;
                        string mxCD = "";
                        string mid = "";
                        string subItemCD = "";
                        int subCD, incrItCD;
                        if (lblPTransSL.Text == "")
                        {
                            ItemCD = "00000001";
                        }
                        else
                        {
                            mxCD = lblPTransSL.Text;
                            //OItemCD = mxCD.Substring(4,4);
                            subCD = int.Parse(mxCD);
                            incrItCD = subCD + 1;
                            if (incrItCD < 10)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000000" + mid;
                            }
                            else if (incrItCD < 100)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000000" + mid;
                            }
                            else if (incrItCD < 1000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00000" + mid;
                            }
                            else if (incrItCD < 10000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000" + mid;
                            }
                            else if (incrItCD < 100000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000" + mid;
                            }
                            else if (incrItCD < 1000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00" + mid;
                            }
                            else if (incrItCD < 10000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0" + mid;
                            }
                            //else
                            //    subItemCD = incrItCD.ToString();

                            ItemCD = subItemCD;
                        }


                        if (btnPurchaseEdit.Text == "EDIT")
                        {
                            query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblPMy.Text + "','" + txtPInNo.Text + "','" + txtPMemoNo.Text + "','','" + txtPToID.Text + "','" + txtPSupID.Text + "','" + ddlPLCType.Text + "','" + lblPLcCD.Text + "',@LCDATE,'" + txtPRemarks.Text + "',@TRANSSL,'" + lblPCatID.Text + "','" + txtItemID_P.Text + "', " +
                                     " '" + ddlType_P.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ",'',@USERID,'')");

                            comm = new SqlCommand(query, conn);
                            comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                            comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                            comm.Parameters.AddWithValue("@LCDATE", lcDate);
                            comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                            comm.Parameters.AddWithValue("@USERID", userName);

                            conn.Open();
                            int result = comm.ExecuteNonQuery();
                            conn.Close();
                            GridShow_Purchase();
                        }

                        else
                        {
                            Int64 TransSaleEdit = Convert.ToInt64(ddlPurchaseEditInNo.Text);
                            conn.Open();
                            SqlCommand cmd1 = new SqlCommand("select * from STK_TRANSMST where TRANSTP = 'BUY' TRANSMY='" + lblPMy.Text + "' and TRANSNO =" + TransSaleEdit + "", conn);
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                            DataSet ds1 = new DataSet();
                            da.Fill(ds1);
                            conn.Close();
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblPMy.Text + "','" + ddlPurchaseEditInNo.Text + "','" + txtPMemoNo.Text + "','','" + txtPToID.Text + "','" + txtPSupID.Text + "','" + ddlPLCType.Text + "','" + lblPLcCD.Text + "',@LCDATE,'" + txtPRemarks.Text + "',@TRANSSL,'" + lblPCatID.Text + "','" + txtItemID_P.Text + "', " +
                                     " '" + ddlType_P.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ",'',@USERID,'')");

                                comm = new SqlCommand(query, conn);
                                comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                                comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                                comm.Parameters.AddWithValue("@LCDATE", lcDate);
                                comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                                comm.Parameters.AddWithValue("@USERID", userName);

                                conn.Open();
                                int result = comm.ExecuteNonQuery();
                                conn.Close();
                                GridShowPurchase_Edit();
                            }
                            else
                            {
                                lblPGridMSG.Visible = true;
                                lblPGridMSG.Text = "Must be in New Mode.";
                            }
                        }
                    }
                    else
                    {

                        query = "insert into STK_TRANSMST ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS, USERPC, USERID, IPADDRESS) " +
                                    "values(@TRANSTP,@TRANSDT,'" + lblPMy.Text + "','" + txtPInNo.Text + "','" + txtPMemoNo.Text + "','','" + txtPToID.Text + "','" + txtPSupID.Text + "','" + ddlPLCType.Text + "','" + lblPLcCD.Text + "',@LCDATE,'" + txtPRemarks.Text + "','',@USERID,'')";
                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                        comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                        comm.Parameters.AddWithValue("@LCDATE", lcDate);
                        comm.Parameters.AddWithValue("@USERID", userName);
                        conn.Open();
                        comm.ExecuteNonQuery();
                        conn.Close();


                        Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM_P.Text + "'", lblPCatID);

                        DropDownList ddlType_P = (DropDownList)gvPurchase.FooterRow.FindControl("ddlType_P");
                        TextBox txtCPQTY_P = (TextBox)gvPurchase.FooterRow.FindControl("txtCPQTY_P");
                        if (txtCPQTY_P.Text == "")
                        {
                            txtCPQTY_P.Text = "0";
                        }
                        else
                            txtCPQTY_P.Text = txtCPQTY_P.Text;
                        decimal cpQty = 0;
                        cpQty = Convert.ToDecimal(txtCPQTY_P.Text);
                        TextBox txtCQty_P = (TextBox)gvPurchase.FooterRow.FindControl("txtCQty_P");
                        if (txtCQty_P.Text == "")
                        {
                            txtCQty_P.Text = "0";
                        }
                        else
                            txtCQty_P.Text = txtCQty_P.Text;
                        decimal cQty = 0;
                        cQty = Convert.ToDecimal(txtCQty_P.Text);
                        decimal pQty = 0;
                        if (txtPQty_P.Text == "")
                        {
                            txtPQty_P.Text = "0";
                        }
                        else
                            txtPQty_P.Text = txtPQty_P.Text;
                        pQty = Convert.ToDecimal(txtPQty_P.Text);

                        decimal Qty = 0;
                        if (txtQty_P.Text == "")
                        {
                            txtQty_P.Text = "0";
                        }
                        else
                            txtQty_P.Text = txtQty_P.Text;
                        Qty = Convert.ToDecimal(txtQty_P.Text);
                        TextBox txtRate_P = (TextBox)gvPurchase.FooterRow.FindControl("txtRate_P");
                        decimal Rate = 0;
                        Rate = Convert.ToDecimal(txtRate_P.Text);
                        TextBox txtAmount_P = (TextBox)gvPurchase.FooterRow.FindControl("txtAmount_P");
                        decimal Amount = 0;
                        Amount = Convert.ToDecimal(txtAmount_P.Text);

                        Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'BUY' and TRANSMY = '" + lblPMy.Text + "'", lblPTransSL);
                        string ItemCD;
                        string mxCD = "";
                        string mid = "";
                        string subItemCD = "";
                        int subCD, incrItCD;
                        if (lblPTransSL.Text == "")
                        {
                            ItemCD = "00000001";
                        }
                        else
                        {
                            mxCD = lblPTransSL.Text;
                            //OItemCD = mxCD.Substring(4,4);
                            subCD = int.Parse(mxCD);
                            incrItCD = subCD + 1;
                            if (incrItCD < 10)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000000" + mid;
                            }
                            else if (incrItCD < 100)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000000" + mid;
                            }
                            else if (incrItCD < 1000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00000" + mid;
                            }
                            else if (incrItCD < 10000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000" + mid;
                            }
                            else if (incrItCD < 100000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000" + mid;
                            }
                            else if (incrItCD < 1000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00" + mid;
                            }
                            else if (incrItCD < 10000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0" + mid;
                            }
                            //else
                            //    subItemCD = incrItCD.ToString();

                            ItemCD = subItemCD;
                        }

                        query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, USERPC, USERID, IPADDRESS ) " +
                                 "values(@TRANSTP,@TRANSDT,'" + lblPMy.Text + "','" + txtPInNo.Text + "','" + txtPMemoNo.Text + "','','" + txtPToID.Text + "','" + txtPSupID.Text + "','" + ddlPLCType.Text + "','" + lblPLcCD.Text + "',@LCDATE,'" + txtPRemarks.Text + "',@TRANSSL,'" + lblPCatID.Text + "','" + txtItemID_P.Text + "', " +
                                 " '" + ddlType_P.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ",'',@USERID,'')");

                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                        comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                        comm.Parameters.AddWithValue("@LCDATE", lcDate);
                        comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                        comm.Parameters.AddWithValue("@USERID", userName);

                        conn.Open();
                        int result = comm.ExecuteNonQuery();
                        conn.Close();

                        if (btnSaleEdit.Text == "EDIT")
                        {
                            GridShow_Purchase();
                        }
                        else
                        {
                            GridShowPurchase_Edit();
                        }
                    }
                }
            }


                ////////////////////////////////////////////////////////// For Complete   //////////////////////////////////////

            else if (e.CommandName.Equals("Complete"))
            {
                TextBox txtItemID_P = (TextBox)gvPurchase.FooterRow.FindControl("txtItemID_P");
                TextBox txtItemNM_P = (TextBox)gvPurchase.FooterRow.FindControl("txtItemNM_P");
                TextBox txtQty_P = (TextBox)gvPurchase.FooterRow.FindControl("txtQty_P");
                TextBox txtPQty_P = (TextBox)gvPurchase.FooterRow.FindControl("txtPQty_P");
                if (txtPToID.Text == "")
                {
                    lblPValidMsg.Visible = true;
                    lblPValidMsg.Text = "Select Store.";
                    txtPTo.Focus();
                    lblValidMSG.Visible = false;
                    lblPGridMSG.Visible = false;
                }
                else if (ddlPLCType.Text == "LOCAL" && txtPSupID.Text == "")
                {
                    lblValidMSG.Visible = true;
                    lblValidMSG.Text = "Select Party";
                    txtPSupplier.Focus();
                    lblPValidMsg.Visible = false;
                    lblPGridMSG.Visible = false;
                }
                else if (txtItemID_P.Text == "")
                {
                    lblPGridMSG.Visible = true;
                    lblPGridMSG.Text = "Select Item.";
                    txtItemNM_P.Focus();
                    lblPValidMsg.Visible = false;
                    lblValidMSG.Visible = false;
                }
                else if (txtQty_P.Text == "0")
                {
                    lblPGridMSG.Visible = true;
                    lblPGridMSG.Text = "Quantity is Wrong.";
                    txtPQty_P.Focus();
                }
                else
                {
                    lblPValidMsg.Visible = false;
                    lblValidMSG.Visible = false;
                    lblPGridMSG.Visible = false;

                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    if (btnPurchaseEdit.Text == "EDIT")
                    {
                        cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + txtPInNo.Text + "' and TRANSTP='BUY' AND TRANSMY='" + lblPMy.Text + "'", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + ddlPurchaseEditInNo.Text + "' and TRANSTP='BUY' AND TRANSMY='" + lblPMy.Text + "'", conn);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    conn.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM_P.Text + "'", lblPCatID);

                        DropDownList ddlType_P = (DropDownList)gvPurchase.FooterRow.FindControl("ddlType_P");
                        TextBox txtCPQTY_P = (TextBox)gvPurchase.FooterRow.FindControl("txtCPQTY_P");
                        if (txtCPQTY_P.Text == "")
                        {
                            txtCPQTY_P.Text = "0";
                        }
                        else
                            txtCPQTY_P.Text = txtCPQTY_P.Text;
                        decimal cpQty = 0;
                        cpQty = Convert.ToDecimal(txtCPQTY_P.Text);
                        TextBox txtCQty_P = (TextBox)gvPurchase.FooterRow.FindControl("txtCQty_P");
                        if (txtCQty_P.Text == "")
                        {
                            txtCQty_P.Text = "0";
                        }
                        else
                            txtCQty_P.Text = txtCQty_P.Text;
                        decimal cQty = 0;
                        cQty = Convert.ToDecimal(txtCQty_P.Text);
                        decimal pQty = 0;
                        if (txtPQty_P.Text == "")
                        {
                            txtPQty_P.Text = "0";
                        }
                        else
                            txtPQty_P.Text = txtPQty_P.Text;
                        pQty = Convert.ToDecimal(txtPQty_P.Text);

                        decimal Qty = 0;
                        if (txtQty_P.Text == "")
                        {
                            txtQty_P.Text = "0";
                        }
                        else
                            txtQty_P.Text = txtQty_P.Text;
                        Qty = Convert.ToDecimal(txtQty_P.Text);
                        TextBox txtRate_P = (TextBox)gvPurchase.FooterRow.FindControl("txtRate_P");
                        decimal Rate = 0;
                        Rate = Convert.ToDecimal(txtRate_P.Text);
                        TextBox txtAmount_P = (TextBox)gvPurchase.FooterRow.FindControl("txtAmount_P");
                        decimal Amount = 0;
                        Amount = Convert.ToDecimal(txtAmount_P.Text);

                        Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'BUY' and TRANSMY = '" + lblPMy.Text + "'", lblPTransSL);
                        string ItemCD;
                        string mxCD = "";
                        string mid = "";
                        string subItemCD = "";
                        int subCD, incrItCD;
                        if (lblPTransSL.Text == "")
                        {
                            ItemCD = "00000001";
                        }
                        else
                        {
                            mxCD = lblPTransSL.Text;
                            //OItemCD = mxCD.Substring(4,4);
                            subCD = int.Parse(mxCD);
                            incrItCD = subCD + 1;
                            if (incrItCD < 10)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000000" + mid;
                            }
                            else if (incrItCD < 100)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000000" + mid;
                            }
                            else if (incrItCD < 1000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00000" + mid;
                            }
                            else if (incrItCD < 10000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000" + mid;
                            }
                            else if (incrItCD < 100000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000" + mid;
                            }
                            else if (incrItCD < 1000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00" + mid;
                            }
                            else if (incrItCD < 10000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0" + mid;
                            }
                            //else
                            //    subItemCD = incrItCD.ToString();

                            ItemCD = subItemCD;
                        }

                        if (btnPurchaseEdit.Text == "EDIT")
                        {
                            query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblPMy.Text + "','" + txtPInNo.Text + "','" + txtPMemoNo.Text + "','','" + txtPToID.Text + "','" + txtPSupID.Text + "','" + ddlPLCType.Text + "','" + lblPLcCD.Text + "',@LCDATE,'" + txtPRemarks.Text + "',@TRANSSL,'" + lblPCatID.Text + "','" + txtItemID_P.Text + "', " +
                                     " '" + ddlType_P.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ",'',@USERID,'')");

                            comm = new SqlCommand(query, conn);
                            comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                            comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                            comm.Parameters.AddWithValue("@LCDATE", lcDate);
                            comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                            comm.Parameters.AddWithValue("@USERID", userName);

                            conn.Open();
                            int result = comm.ExecuteNonQuery();
                            conn.Close();

                            ///////Refresh/////
                            txtPMemoNo.Text = "";
                            txtPTo.Text = "";
                            txtPToID.Text = "";
                            ddlPLCType.SelectedIndex = -1;
                            txtPLCID.Text = "";
                            lblPLcCD.Text = "";
                            txtPLCDT.Text = "";
                            txtPSupplier.Text = "";
                            txtPSupID.Text = "";
                            txtPRemarks.Text = "";
                            txtItemID_P.Text = "";
                            txtItemNM_P.Text = "";
                            ddlType_P.SelectedIndex = -1;
                            txtCPQTY_P.Text = "";
                            txtCQty_P.Text = "";
                            txtPQty_P.Text = "";
                            txtQty_P.Text = "";
                            txtRate_P.Text = ".00";
                            txtAmount_P.Text = ".00";
                            txtPTotal.Text = "";


                            DateTime transdate = DateTime.Parse(txtPInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            string TrDate = transdate.ToString("yyyy/MM/dd");

                            Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblPMy.Text + "' and TRANSTP = 'BUY'", lblPMxNo);
                            if (lblPMxNo.Text == "")
                            {
                                txtPInNo.Text = "1";
                            }
                            else
                            {
                                int iNo = int.Parse(lblPMxNo.Text);
                                int totIno = iNo + 1;
                                txtPInNo.Text = totIno.ToString();
                            }

                            GridShow_Purchase();
                            txtPMemoNo.Focus();
                        }

                        else
                        {
                            Int64 TransSaleEdit = Convert.ToInt64(ddlPurchaseEditInNo.Text);
                            conn.Open();
                            SqlCommand cmd1 = new SqlCommand("select * from STK_TRANSMST where TRANSTP = 'BUY' and TRANSMY='" + lblPMy.Text + "' and TRANSNO =" + TransSaleEdit + "", conn);
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                            DataSet ds1 = new DataSet();
                            da.Fill(ds1);
                            conn.Close();
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblPMy.Text + "','" + ddlPurchaseEditInNo.Text + "','" + txtPMemoNo.Text + "','','" + txtPToID.Text + "','" + txtPSupID.Text + "','" + ddlPLCType.Text + "','" + lblPLcCD.Text + "',@LCDATE,'" + txtPRemarks.Text + "',@TRANSSL,'" + lblPCatID.Text + "','" + txtItemID_P.Text + "', " +
                                     " '" + ddlType_P.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ",'',@USERID,'')");

                                comm = new SqlCommand(query, conn);
                                comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                                comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                                comm.Parameters.AddWithValue("@LCDATE", lcDate);
                                comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                                comm.Parameters.AddWithValue("@USERID", userName);

                                conn.Open();
                                int result = comm.ExecuteNonQuery();
                                conn.Close();

                                ///////Refresh/////
                                ddlPurchaseEditInNo.SelectedIndex = -1;
                                txtPMemoNo.Text = "";
                                txtPTo.Text = "";
                                txtPToID.Text = "";
                                ddlPLCType.SelectedIndex = -1;
                                txtPLCID.Text = "";
                                lblPLcCD.Text = "";
                                txtPLCDT.Text = "";
                                txtPSupplier.Text = "";
                                txtPSupID.Text = "";
                                txtPRemarks.Text = "";
                                txtItemID_P.Text = "";
                                txtItemNM_P.Text = "";
                                ddlType_P.SelectedIndex = -1;
                                txtCPQTY_P.Text = "";
                                txtCQty_P.Text = "";
                                txtPQty_P.Text = "";
                                txtQty_P.Text = "";
                                txtRate_P.Text = ".00";
                                txtAmount_P.Text = ".00";
                                txtPTotal.Text = "";

                                GridShowPurchase_Edit();
                                ddlPurchaseEditInNo.Focus();
                            }
                            else
                            {
                                lblPGridMSG.Visible = true;
                                lblPGridMSG.Text = "Must be in New Mode.";
                            }
                        }
                    }

                    else
                    {

                        query = "insert into STK_TRANSMST ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS, USERPC, USERID, IPADDRESS) " +
                                    "values(@TRANSTP,@TRANSDT,'" + lblPMy.Text + "','" + txtPInNo.Text + "','" + txtPMemoNo.Text + "','','" + txtPToID.Text + "','" + txtPSupID.Text + "','" + ddlPLCType.Text + "','" + lblPLcCD.Text + "',@LCDATE,'" + txtPRemarks.Text + "','',@USERID,'')";
                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                        comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                        comm.Parameters.AddWithValue("@LCDATE", lcDate);
                        comm.Parameters.AddWithValue("@USERID", userName);
                        conn.Open();
                        comm.ExecuteNonQuery();
                        conn.Close();


                        Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM_P.Text + "'", lblPCatID);

                        DropDownList ddlType_P = (DropDownList)gvPurchase.FooterRow.FindControl("ddlType_P");
                        TextBox txtCPQTY_P = (TextBox)gvPurchase.FooterRow.FindControl("txtCPQTY_P");
                        if (txtCPQTY_P.Text == "")
                        {
                            txtCPQTY_P.Text = "0";
                        }
                        else
                            txtCPQTY_P.Text = txtCPQTY_P.Text;
                        decimal cpQty = 0;
                        cpQty = Convert.ToDecimal(txtCPQTY_P.Text);
                        TextBox txtCQty_P = (TextBox)gvPurchase.FooterRow.FindControl("txtCQty_P");
                        if (txtCQty_P.Text == "")
                        {
                            txtCQty_P.Text = "0";
                        }
                        else
                            txtCQty_P.Text = txtCQty_P.Text;
                        decimal cQty = 0;
                        cQty = Convert.ToDecimal(txtCQty_P.Text);
                        decimal pQty = 0;
                        if (txtPQty_P.Text == "")
                        {
                            txtPQty_P.Text = "0";
                        }
                        else
                            txtPQty_P.Text = txtPQty_P.Text;
                        pQty = Convert.ToDecimal(txtPQty_P.Text);

                        decimal Qty = 0;
                        if (txtQty_P.Text == "")
                        {
                            txtQty_P.Text = "0";
                        }
                        else
                            txtQty_P.Text = txtQty_P.Text;
                        Qty = Convert.ToDecimal(txtQty_P.Text);
                        TextBox txtRate_P = (TextBox)gvPurchase.FooterRow.FindControl("txtRate_P");
                        decimal Rate = 0;
                        Rate = Convert.ToDecimal(txtRate_P.Text);
                        TextBox txtAmount_P = (TextBox)gvPurchase.FooterRow.FindControl("txtAmount_P");
                        decimal Amount = 0;
                        Amount = Convert.ToDecimal(txtAmount_P.Text);

                        Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'BUY' and TRANSMY = '" + lblPMy.Text + "'", lblPTransSL);
                        string ItemCD;
                        string mxCD = "";
                        string mid = "";
                        string subItemCD = "";
                        int subCD, incrItCD;
                        if (lblPTransSL.Text == "")
                        {
                            ItemCD = "00000001";
                        }
                        else
                        {
                            mxCD = lblPTransSL.Text;
                            //OItemCD = mxCD.Substring(4,4);
                            subCD = int.Parse(mxCD);
                            incrItCD = subCD + 1;
                            if (incrItCD < 10)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000000" + mid;
                            }
                            else if (incrItCD < 100)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000000" + mid;
                            }
                            else if (incrItCD < 1000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00000" + mid;
                            }
                            else if (incrItCD < 10000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000" + mid;
                            }
                            else if (incrItCD < 100000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000" + mid;
                            }
                            else if (incrItCD < 1000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00" + mid;
                            }
                            else if (incrItCD < 10000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0" + mid;
                            }
                            //else
                            //    subItemCD = incrItCD.ToString();

                            ItemCD = subItemCD;
                        }

                        query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, USERPC, USERID, IPADDRESS ) " +
                                 "values(@TRANSTP,@TRANSDT,'" + lblPMy.Text + "','" + txtPInNo.Text + "','" + txtPMemoNo.Text + "','','" + txtPToID.Text + "','" + txtPSupID.Text + "','" + ddlPLCType.Text + "','" + lblPLcCD.Text + "',@LCDATE,'" + txtPRemarks.Text + "',@TRANSSL,'" + lblPCatID.Text + "','" + txtItemID_P.Text + "', " +
                                 " '" + ddlType_P.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ",'',@USERID,'')");

                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                        comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                        comm.Parameters.AddWithValue("@LCDATE", lcDate);
                        comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                        comm.Parameters.AddWithValue("@USERID", userName);

                        conn.Open();
                        int result = comm.ExecuteNonQuery();
                        conn.Close();

                        ///////Refresh/////
                        txtPMemoNo.Text = "";
                        txtPTo.Text = "";
                        txtPToID.Text = "";
                        ddlPLCType.SelectedIndex = -1;
                        txtPLCID.Text = "";
                        lblPLcCD.Text = "";
                        txtPLCDT.Text = "";
                        txtPSupplier.Text = "";
                        txtPSupID.Text = "";
                        txtPRemarks.Text = "";
                        txtItemID_P.Text = "";
                        txtItemNM_P.Text = "";
                        ddlType_P.SelectedIndex = -1;
                        txtCPQTY_P.Text = "";
                        txtCQty_P.Text = "";
                        txtPQty_P.Text = "";
                        txtQty_P.Text = "";
                        txtRate_P.Text = ".00";
                        txtAmount_P.Text = ".00";
                        txtPTotal.Text = "";


                        DateTime transdate = DateTime.Parse(txtPInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        string TrDate = transdate.ToString("yyyy/MM/dd");

                        Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblPMy.Text + "' and TRANSTP = 'BUY'", lblPMxNo);
                        if (lblPMxNo.Text == "")
                        {
                            txtPInNo.Text = "1";
                        }
                        else
                        {
                            int iNo = int.Parse(lblPMxNo.Text);
                            int totIno = iNo + 1;
                            txtPInNo.Text = totIno.ToString();
                        }

                        GridShow_Purchase();
                        txtPMemoNo.Focus();

                    }
                }
            }

            /////////////////////////////////// For Print ///////////////////////////////////

            else if (e.CommandName.Equals("SavePrint"))
            {
                Session["InvDate_P"] = "";
                Session["InvNo_P"] = "";
                Session["Memo_P"] = "";
                Session["StoreNM_P"] = "";
                Session["StoreID_P"] = "";
                Session["PartyNM_P"] = "";
                Session["PartyID_P"] = "";

                Session["InvNoEdit_P"] = "";

                Session["InvDate_P"] = txtPInDT.Text;
                Session["InvNo_P"] = txtPInNo.Text;
                Session["Memo_P"] = txtPMemoNo.Text;
                Session["StoreNM_P"] = txtPTo.Text;
                Session["StoreID_P"] = txtPToID.Text;
                Session["PartyNM_P"] = txtPSupplier.Text;
                Session["PartyID_P"] = txtPSupID.Text;

                TextBox txtItemID_P = (TextBox)gvPurchase.FooterRow.FindControl("txtItemID_P");
                TextBox txtItemNM_P = (TextBox)gvPurchase.FooterRow.FindControl("txtItemNM_P");
                TextBox txtQty_P = (TextBox)gvPurchase.FooterRow.FindControl("txtQty_P");
                TextBox txtPQty_P = (TextBox)gvPurchase.FooterRow.FindControl("txtPQty_P");
                if (txtPToID.Text == "")
                {
                    lblPValidMsg.Visible = true;
                    lblPValidMsg.Text = "Select Store.";
                    txtPTo.Focus();
                    lblValidMSG.Visible = false;
                    lblPGridMSG.Visible = false;
                }
                else if (ddlPLCType.Text == "LOCAL" && txtPSupID.Text == "")
                {
                    lblValidMSG.Visible = true;
                    lblValidMSG.Text = "Select Party";
                    txtPSupplier.Focus();
                    lblPValidMsg.Visible = false;
                    lblPGridMSG.Visible = false;
                }
                else if (txtItemID_P.Text == "")
                {
                    lblPGridMSG.Visible = true;
                    lblPGridMSG.Text = "Select Item.";
                    txtItemNM_P.Focus();
                    lblPValidMsg.Visible = false;
                    lblValidMSG.Visible = false;
                }
                else if (txtQty_P.Text == "0")
                {
                    lblPGridMSG.Visible = true;
                    lblPGridMSG.Text = "Quantity is Wrong.";
                    txtPQty_P.Focus();
                }
                else
                {
                    lblPValidMsg.Visible = false;
                    lblValidMSG.Visible = false;
                    lblPGridMSG.Visible = false;

                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    if (btnPurchaseEdit.Text == "EDIT")
                    {
                        cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + txtPInNo.Text + "' and TRANSTP='BUY' AND TRANSMY='" + lblPMy.Text + "'", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + ddlPurchaseEditInNo.Text + "' and TRANSTP='BUY' AND TRANSMY='" + lblPMy.Text + "'", conn);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    conn.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM_P.Text + "'", lblPCatID);

                        DropDownList ddlType_P = (DropDownList)gvPurchase.FooterRow.FindControl("ddlType_P");
                        TextBox txtCPQTY_P = (TextBox)gvPurchase.FooterRow.FindControl("txtCPQTY_P");
                        if (txtCPQTY_P.Text == "")
                        {
                            txtCPQTY_P.Text = "0";
                        }
                        else
                            txtCPQTY_P.Text = txtCPQTY_P.Text;
                        decimal cpQty = 0;
                        cpQty = Convert.ToDecimal(txtCPQTY_P.Text);
                        TextBox txtCQty_P = (TextBox)gvPurchase.FooterRow.FindControl("txtCQty_P");
                        if (txtCQty_P.Text == "")
                        {
                            txtCQty_P.Text = "0";
                        }
                        else
                            txtCQty_P.Text = txtCQty_P.Text;
                        decimal cQty = 0;
                        cQty = Convert.ToDecimal(txtCQty_P.Text);
                        decimal pQty = 0;
                        if (txtPQty_P.Text == "")
                        {
                            txtPQty_P.Text = "0";
                        }
                        else
                            txtPQty_P.Text = txtPQty_P.Text;
                        pQty = Convert.ToDecimal(txtPQty_P.Text);

                        decimal Qty = 0;
                        if (txtQty_P.Text == "")
                        {
                            txtQty_P.Text = "0";
                        }
                        else
                            txtQty_P.Text = txtQty_P.Text;
                        Qty = Convert.ToDecimal(txtQty_P.Text);
                        TextBox txtRate_P = (TextBox)gvPurchase.FooterRow.FindControl("txtRate_P");
                        decimal Rate = 0;
                        Rate = Convert.ToDecimal(txtRate_P.Text);
                        TextBox txtAmount_P = (TextBox)gvPurchase.FooterRow.FindControl("txtAmount_P");
                        decimal Amount = 0;
                        Amount = Convert.ToDecimal(txtAmount_P.Text);

                        Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'BUY' and TRANSMY = '" + lblPMy.Text + "'", lblPTransSL);
                        string ItemCD;
                        string mxCD = "";
                        string mid = "";
                        string subItemCD = "";
                        int subCD, incrItCD;
                        if (lblPTransSL.Text == "")
                        {
                            ItemCD = "00000001";
                        }
                        else
                        {
                            mxCD = lblPTransSL.Text;
                            //OItemCD = mxCD.Substring(4,4);
                            subCD = int.Parse(mxCD);
                            incrItCD = subCD + 1;
                            if (incrItCD < 10)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000000" + mid;
                            }
                            else if (incrItCD < 100)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000000" + mid;
                            }
                            else if (incrItCD < 1000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00000" + mid;
                            }
                            else if (incrItCD < 10000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000" + mid;
                            }
                            else if (incrItCD < 100000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000" + mid;
                            }
                            else if (incrItCD < 1000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00" + mid;
                            }
                            else if (incrItCD < 10000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0" + mid;
                            }
                            //else
                            //    subItemCD = incrItCD.ToString();

                            ItemCD = subItemCD;
                        }

                        if (btnPurchaseEdit.Text == "EDIT")
                        {
                            query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblPMy.Text + "','" + txtPInNo.Text + "','" + txtPMemoNo.Text + "','','" + txtPToID.Text + "','" + txtPSupID.Text + "','" + ddlPLCType.Text + "','" + lblPLcCD.Text + "',@LCDATE,'" + txtPRemarks.Text + "',@TRANSSL,'" + lblPCatID.Text + "','" + txtItemID_P.Text + "', " +
                                     " '" + ddlType_P.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ",'',@USERID,'')");

                            comm = new SqlCommand(query, conn);
                            comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                            comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                            comm.Parameters.AddWithValue("@LCDATE", lcDate);
                            comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                            comm.Parameters.AddWithValue("@USERID", userName);

                            conn.Open();
                            int result = comm.ExecuteNonQuery();
                            conn.Close();

                            ///////Refresh/////
                            txtPMemoNo.Text = "";
                            txtPTo.Text = "";
                            txtPToID.Text = "";
                            ddlPLCType.SelectedIndex = -1;
                            txtPLCID.Text = "";
                            lblPLcCD.Text = "";
                            txtPLCDT.Text = "";
                            txtPSupplier.Text = "";
                            txtPSupID.Text = "";
                            txtPRemarks.Text = "";
                            txtItemID_P.Text = "";
                            txtItemNM_P.Text = "";
                            ddlType_P.SelectedIndex = -1;
                            txtCPQTY_P.Text = "";
                            txtCQty_P.Text = "";
                            txtPQty_P.Text = "";
                            txtQty_P.Text = "";
                            txtRate_P.Text = ".00";
                            txtAmount_P.Text = ".00";
                            txtPTotal.Text = "";


                            DateTime transdate = DateTime.Parse(txtPInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            string TrDate = transdate.ToString("yyyy/MM/dd");

                            Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblPMy.Text + "' and TRANSTP = 'BUY'", lblPMxNo);
                            if (lblPMxNo.Text == "")
                            {
                                txtPInNo.Text = "1";
                            }
                            else
                            {
                                int iNo = int.Parse(lblPMxNo.Text);
                                int totIno = iNo + 1;
                                txtPInNo.Text = totIno.ToString();
                            }

                            GridShow_Purchase();
                            txtPMemoNo.Focus();

                            ScriptManager.RegisterStartupScript(this,
                            this.GetType(), "OpenWindow", "window.open('../Report/Report/rptPurchaseMemo.aspx','_newtab');", true);

                        }

                        else
                        {
                            Session["InvNoEdit_P"] = ddlPurchaseEditInNo.Text;

                            Int64 TransSaleEdit = Convert.ToInt64(ddlPurchaseEditInNo.Text);
                            conn.Open();
                            SqlCommand cmd1 = new SqlCommand("select * from STK_TRANSMST where TRANSTP = 'BUY' and TRANSMY='" + lblPMy.Text + "' and TRANSNO =" + TransSaleEdit + "", conn);
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                            DataSet ds1 = new DataSet();
                            da.Fill(ds1);
                            conn.Close();
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblPMy.Text + "','" + ddlPurchaseEditInNo.Text + "','" + txtPMemoNo.Text + "','','" + txtPToID.Text + "','" + txtPSupID.Text + "','" + ddlPLCType.Text + "','" + lblPLcCD.Text + "',@LCDATE,'" + txtPRemarks.Text + "',@TRANSSL,'" + lblPCatID.Text + "','" + txtItemID_P.Text + "', " +
                                     " '" + ddlType_P.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ",'',@USERID,'')");

                                comm = new SqlCommand(query, conn);
                                comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                                comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                                comm.Parameters.AddWithValue("@LCDATE", lcDate);
                                comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                                comm.Parameters.AddWithValue("@USERID", userName);

                                conn.Open();
                                int result = comm.ExecuteNonQuery();
                                conn.Close();

                                ///////Refresh/////
                                ddlPurchaseEditInNo.SelectedIndex = -1;
                                txtPMemoNo.Text = "";
                                txtPTo.Text = "";
                                txtPToID.Text = "";
                                ddlPLCType.SelectedIndex = -1;
                                txtPLCID.Text = "";
                                lblPLcCD.Text = "";
                                txtPLCDT.Text = "";
                                txtPSupplier.Text = "";
                                txtPSupID.Text = "";
                                txtPRemarks.Text = "";
                                txtItemID_P.Text = "";
                                txtItemNM_P.Text = "";
                                ddlType_P.SelectedIndex = -1;
                                txtCPQTY_P.Text = "";
                                txtCQty_P.Text = "";
                                txtPQty_P.Text = "";
                                txtQty_P.Text = "";
                                txtRate_P.Text = ".00";
                                txtAmount_P.Text = ".00";
                                txtPTotal.Text = "";

                                GridShowPurchase_Edit();
                                ddlPurchaseEditInNo.Focus();

                                ScriptManager.RegisterStartupScript(this,
                                this.GetType(), "OpenWindow", "window.open('../Report/Report/rptPurchaseMemoEdit.aspx','_newtab');", true);

                            }
                            else
                            {
                                lblPGridMSG.Visible = true;
                                lblPGridMSG.Text = "Must be in New Mode.";
                            }
                        }
                    }

                    else
                    {

                        query = "insert into STK_TRANSMST ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS, USERPC, USERID, IPADDRESS) " +
                                    "values(@TRANSTP,@TRANSDT,'" + lblPMy.Text + "','" + txtPInNo.Text + "','" + txtPMemoNo.Text + "','','" + txtPToID.Text + "','" + txtPSupID.Text + "','" + ddlPLCType.Text + "','" + lblPLcCD.Text + "',@LCDATE,'" + txtPRemarks.Text + "','',@USERID,'')";
                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                        comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                        comm.Parameters.AddWithValue("@LCDATE", lcDate);
                        comm.Parameters.AddWithValue("@USERID", userName);
                        conn.Open();
                        comm.ExecuteNonQuery();
                        conn.Close();


                        Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM_P.Text + "'", lblPCatID);

                        DropDownList ddlType_P = (DropDownList)gvPurchase.FooterRow.FindControl("ddlType_P");
                        TextBox txtCPQTY_P = (TextBox)gvPurchase.FooterRow.FindControl("txtCPQTY_P");
                        if (txtCPQTY_P.Text == "")
                        {
                            txtCPQTY_P.Text = "0";
                        }
                        else
                            txtCPQTY_P.Text = txtCPQTY_P.Text;
                        decimal cpQty = 0;
                        cpQty = Convert.ToDecimal(txtCPQTY_P.Text);
                        TextBox txtCQty_P = (TextBox)gvPurchase.FooterRow.FindControl("txtCQty_P");
                        if (txtCQty_P.Text == "")
                        {
                            txtCQty_P.Text = "0";
                        }
                        else
                            txtCQty_P.Text = txtCQty_P.Text;
                        decimal cQty = 0;
                        cQty = Convert.ToDecimal(txtCQty_P.Text);
                        decimal pQty = 0;
                        if (txtPQty_P.Text == "")
                        {
                            txtPQty_P.Text = "0";
                        }
                        else
                            txtPQty_P.Text = txtPQty_P.Text;
                        pQty = Convert.ToDecimal(txtPQty_P.Text);

                        decimal Qty = 0;
                        if (txtQty_P.Text == "")
                        {
                            txtQty_P.Text = "0";
                        }
                        else
                            txtQty_P.Text = txtQty_P.Text;
                        Qty = Convert.ToDecimal(txtQty_P.Text);
                        TextBox txtRate_P = (TextBox)gvPurchase.FooterRow.FindControl("txtRate_P");
                        decimal Rate = 0;
                        Rate = Convert.ToDecimal(txtRate_P.Text);
                        TextBox txtAmount_P = (TextBox)gvPurchase.FooterRow.FindControl("txtAmount_P");
                        decimal Amount = 0;
                        Amount = Convert.ToDecimal(txtAmount_P.Text);

                        Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'BUY' and TRANSMY = '" + lblPMy.Text + "'", lblPTransSL);
                        string ItemCD;
                        string mxCD = "";
                        string mid = "";
                        string subItemCD = "";
                        int subCD, incrItCD;
                        if (lblPTransSL.Text == "")
                        {
                            ItemCD = "00000001";
                        }
                        else
                        {
                            mxCD = lblPTransSL.Text;
                            //OItemCD = mxCD.Substring(4,4);
                            subCD = int.Parse(mxCD);
                            incrItCD = subCD + 1;
                            if (incrItCD < 10)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000000" + mid;
                            }
                            else if (incrItCD < 100)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000000" + mid;
                            }
                            else if (incrItCD < 1000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00000" + mid;
                            }
                            else if (incrItCD < 10000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000" + mid;
                            }
                            else if (incrItCD < 100000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000" + mid;
                            }
                            else if (incrItCD < 1000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00" + mid;
                            }
                            else if (incrItCD < 10000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0" + mid;
                            }
                            //else
                            //    subItemCD = incrItCD.ToString();

                            ItemCD = subItemCD;
                        }

                        query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, USERPC, USERID, IPADDRESS ) " +
                                 "values(@TRANSTP,@TRANSDT,'" + lblPMy.Text + "','" + txtPInNo.Text + "','" + txtPMemoNo.Text + "','','" + txtPToID.Text + "','" + txtPSupID.Text + "','" + ddlPLCType.Text + "','" + lblPLcCD.Text + "',@LCDATE,'" + txtPRemarks.Text + "',@TRANSSL,'" + lblPCatID.Text + "','" + txtItemID_P.Text + "', " +
                                 " '" + ddlType_P.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ",'',@USERID,'')");

                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                        comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                        comm.Parameters.AddWithValue("@LCDATE", lcDate);
                        comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                        comm.Parameters.AddWithValue("@USERID", userName);

                        conn.Open();
                        int result = comm.ExecuteNonQuery();
                        conn.Close();

                        ///////Refresh/////
                        txtPMemoNo.Text = "";
                        txtPTo.Text = "";
                        txtPToID.Text = "";
                        ddlPLCType.SelectedIndex = -1;
                        txtPLCID.Text = "";
                        lblPLcCD.Text = "";
                        txtPLCDT.Text = "";
                        txtPSupplier.Text = "";
                        txtPSupID.Text = "";
                        txtPRemarks.Text = "";
                        txtItemID_P.Text = "";
                        txtItemNM_P.Text = "";
                        ddlType_P.SelectedIndex = -1;
                        txtCPQTY_P.Text = "";
                        txtCQty_P.Text = "";
                        txtPQty_P.Text = "";
                        txtQty_P.Text = "";
                        txtRate_P.Text = ".00";
                        txtAmount_P.Text = ".00";
                        txtPTotal.Text = "";


                        DateTime transdate = DateTime.Parse(txtPInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        string TrDate = transdate.ToString("yyyy/MM/dd");

                        Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblPMy.Text + "' and TRANSTP = 'BUY'", lblPMxNo);
                        if (lblPMxNo.Text == "")
                        {
                            txtPInNo.Text = "1";
                        }
                        else
                        {
                            int iNo = int.Parse(lblPMxNo.Text);
                            int totIno = iNo + 1;
                            txtPInNo.Text = totIno.ToString();
                        }

                        GridShow_Purchase();
                        txtPMemoNo.Focus();

                        ScriptManager.RegisterStartupScript(this,
                        this.GetType(), "OpenWindow", "window.open('../Report/Report/rptPurchaseMemo.aspx','_newtab');", true);

                    }
                }
            }
        }


        protected void btnPurchasePrint_Click(object sender, EventArgs e)
        {
            if (txtPInDT.Text == "" || ddlPurchaseEditInNo.Text == "Select")
            {
                lblPGridMSG.Visible = true;
                lblPGridMSG.Text = "Date & Invoice No.";
            }
            else
            {
                lblPGridMSG.Visible = false;

                Session["InvDate_P"] = txtPInDT.Text;
                Session["InvNoEdit_P"] = ddlPurchaseEditInNo.Text;
                Session["Memo_P"] = txtPMemoNo.Text;
                Session["StoreNM_P"] = txtPTo.Text;
                Session["StoreID_P"] = txtPToID.Text;
                Session["PartyNM_P"] = txtPSupplier.Text;
                Session["PartyID_P"] = txtPSupID.Text;

                ScriptManager.RegisterStartupScript(this,
                                    this.GetType(), "OpenWindow", "window.open('../Report/Report/rptPurchaseMemoEdit.aspx','_newtab');", true);
            }
        }

        protected void gvPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string userName = HttpContext.Current.Session["UserName"].ToString();

            if (btnPurchaseEdit.Text == "EDIT")
            {
                DateTime transdate = DateTime.Parse(txtPInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                Label lblTransSL_P = (Label)gvPurchase.Rows[e.RowIndex].FindControl("lblTransSL_P");

                SqlCommand cmd1 = new SqlCommand("select * from STK_TRANS where TRANSTP = 'BUY' and TRANSMY='" + lblPMy.Text + "' and TRANSNO ='" + txtPInNo.Text + "' and TRANSDT = '" + TrDate + "' ", conn);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                conn.Close();
                if (ds1.Tables[0].Rows.Count > 1)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from STK_TRANS where TRANSTP = 'BUY' and  TRANSDT = '" + TrDate + "' and TRANSSL = '" + lblTransSL_P.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from STK_TRANS where TRANSTP = 'BUY' and TRANSNO ='" + txtPInNo.Text + "' and  TRANSDT = '" + TrDate + "' and TRANSSL = '" + lblTransSL_P.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand("delete from STK_TRANSMST where TRANSTP = 'BUY' and TRANSNO ='" + txtPInNo.Text + "' and TRANSMY='" + lblPMy.Text + "' and  TRANSDT = '" + TrDate + "'", conn);
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                }

                gvPurchase.EditIndex = -1;
                GridShow_Purchase();
            }
            else
            {
                DateTime transdate = DateTime.Parse(txtPInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                Label lblTransSL_P = (Label)gvPurchase.Rows[e.RowIndex].FindControl("lblTransSL_P");

                SqlCommand cmd1 = new SqlCommand("select * from STK_TRANS where TRANSTP = 'BUY' and TRANSMY='" + lblPMy.Text + "' and TRANSNO ='" + ddlPurchaseEditInNo.Text + "' and TRANSDT = '" + TrDate + "' ", conn);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                conn.Close();
                if (ds1.Tables[0].Rows.Count > 1)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from STK_TRANS where TRANSTP = 'BUY' and  TRANSDT = '" + TrDate + "' and TRANSSL = '" + lblTransSL_P.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from STK_TRANS where TRANSTP = 'BUY' and TRANSNO ='" + ddlPurchaseEditInNo.Text + "' and  TRANSDT = '" + TrDate + "' and TRANSSL = '" + lblTransSL_P.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand("delete from STK_TRANSMST where TRANSTP = 'BUY' and TRANSNO ='" + ddlPurchaseEditInNo.Text + "' and TRANSMY='" + lblPMy.Text + "' and  TRANSDT = '" + TrDate + "'", conn);
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                }

                gvPurchase.EditIndex = -1;
                GridShowPurchase_Edit();
            }
        }

        protected void gvPurchase_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (btnPurchaseEdit.Text == "EDIT")
            {
                gvPurchase.EditIndex = e.NewEditIndex;
                GridShow_Purchase();
            }
            else
            {
                gvPurchase.EditIndex = e.NewEditIndex;
                GridShowPurchase_Edit();
            }

            TextBox txtItemNMEdit_P = (TextBox)gvPurchase.Rows[e.NewEditIndex].FindControl("txtItemNMEdit_P");
            txtItemNMEdit_P.Focus();
        }

        protected void gvPurchase_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string userName = HttpContext.Current.Session["UserName"].ToString();

            Label lblItemIDEdit_P = (Label)gvPurchase.Rows[e.RowIndex].FindControl("lblItemIDEdit_P");
            TextBox txtItemNMEdit_P = (TextBox)gvPurchase.Rows[e.RowIndex].FindControl("txtItemNMEdit_P");
            TextBox txtQtyEdit_P = (TextBox)gvPurchase.Rows[e.RowIndex].FindControl("txtQtyEdit_P");
            TextBox txtPQtyEdit_P = (TextBox)gvPurchase.Rows[e.RowIndex].FindControl("txtPQtyEdit_P");
            if (txtPToID.Text == "")
            {
                lblPValidMsg.Visible = true;
                lblPValidMsg.Text = "Select Store.";
                txtPTo.Focus();
                lblValidMSG.Visible = false;
                lblPGridMSG.Visible = false;
            }
            //else if (txtPSupID.Text == "")
            //{
            //    lblValidMSG.Visible = true;
            //    lblValidMSG.Text = "Select Party";
            //    txtPSupplier.Focus();
            //    lblPValidMsg.Visible = false;
            //    lblPGridMSG.Visible = false;
            //}
            else if (lblItemIDEdit_P.Text == "")
            {
                lblPGridMSG.Visible = true;
                lblPGridMSG.Text = "Select Item.";
                txtItemNMEdit_P.Focus();
                lblPValidMsg.Visible = false;
                lblValidMSG.Visible = false;
            }
            else if (txtQtyEdit_P.Text == "0")
            {
                lblPGridMSG.Visible = true;
                lblPGridMSG.Text = "Quantity is Wrong.";
                txtPQtyEdit_P.Focus();
                lblPValidMsg.Visible = false;
                lblValidMSG.Visible = false;
            }
            else
            {
                Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNMEdit_P.Text + "'", lblPCatID);

                DropDownList ddltypeEdit_P = (DropDownList)gvPurchase.Rows[e.RowIndex].FindControl("ddltypeEdit_P");
                TextBox txtCPQTYEdit_P = (TextBox)gvPurchase.Rows[e.RowIndex].FindControl("txtCPQTYEdit_P");
                if (txtCPQTYEdit_P.Text == "")
                {
                    txtCPQTYEdit_P.Text = "0";
                }
                else
                    txtCPQTYEdit_P.Text = txtCPQTYEdit_P.Text;
                decimal cpQty = 0;
                cpQty = Convert.ToDecimal(txtCPQTYEdit_P.Text);
                TextBox txtCQtyEdit_P = (TextBox)gvPurchase.Rows[e.RowIndex].FindControl("txtCQtyEdit_P");
                if (txtCQtyEdit_P.Text == "")
                {
                    txtCQtyEdit_P.Text = "0";
                }
                else
                    txtCQtyEdit_P.Text = txtCQtyEdit_P.Text;
                decimal cQty = 0;
                cQty = Convert.ToDecimal(txtCQtyEdit_P.Text);
                decimal pQty = 0;
                if (txtPQtyEdit_P.Text == "")
                {
                    txtPQtyEdit_P.Text = "0";
                }
                else
                    txtPQtyEdit_P.Text = txtPQtyEdit_P.Text;
                pQty = Convert.ToDecimal(txtPQtyEdit_P.Text);

                decimal Qty = 0;
                if (txtQtyEdit_P.Text == "")
                {
                    txtQtyEdit_P.Text = "0";
                }
                else
                    txtQtyEdit_P.Text = txtQtyEdit_P.Text;
                Qty = Convert.ToDecimal(txtQtyEdit_P.Text);
                TextBox txtRateEdit_P = (TextBox)gvPurchase.Rows[e.RowIndex].FindControl("txtRateEdit_P");
                decimal Rate = 0;
                Rate = Convert.ToDecimal(txtRateEdit_P.Text);
                TextBox txtAmountEdit_P = (TextBox)gvPurchase.Rows[e.RowIndex].FindControl("txtAmountEdit_P");
                decimal Amount = 0;
                Amount = Convert.ToDecimal(txtAmountEdit_P.Text);
                Label lblTransSLEdit_P = (Label)gvPurchase.Rows[e.RowIndex].FindControl("lblTransSLEdit_P");

                DateTime transdate = DateTime.Parse(txtPInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                DateTime LCDT = new DateTime();
                string lcDate = "";
                if (txtPLCDT.Text == "")
                {
                    lcDate = "";
                }
                else
                {
                    LCDT = DateTime.Parse(txtPLCDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    lcDate = LCDT.ToString("yyyy/MM/dd");
                }

                if (btnPurchaseEdit.Text == "EDIT")
                {
                    Int64 TransNo = Convert.ToInt64(txtPInNo.Text);

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update STK_TRANS set INVREFNO = '" + txtPMemoNo.Text + "', STORETO='" + txtPToID.Text + "', PSID='" + txtPSupID.Text + "', LCTP = '" + ddlPLCType.Text + "', LCID= '" + lblPLcCD.Text + "', LCDATE ='" + lcDate + "', REMARKS = '" + txtPRemarks.Text + "', CATID = '" + lblPCatID.Text + "', ITEMID = '" + lblItemIDEdit_P.Text + "', UNITTP = '" + ddltypeEdit_P.Text + "', " +
                          " CPQTY =" + cpQty + ", CQTY = " + cQty + ", PQTY = " + pQty + ", QTY = " + Qty + ", RATE = " + Rate + ", AMOUNT = " + Amount + ", USERID = '" + userName + "' where TRANSTP = 'BUY' and TRANSMY='" + lblPMy.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + " and TRANSSL = '" + lblTransSLEdit_P.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();


                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set INVREFNO = '" + txtPMemoNo.Text + "', STORETO='" + txtPToID.Text + "', PSID='" + txtPSupID.Text + "',LCTP = '" + ddlPLCType.Text + "', LCID= '" + lblPLcCD.Text + "', LCDATE ='" + lcDate + "', REMARKS = '" + txtPRemarks.Text + "', USERID = '" + userName + "'" +
                          " where TRANSTP = 'BUY' and TRANSMY='" + lblPMy.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                    cmd1.ExecuteNonQuery();
                    conn.Close();

                    gvPurchase.EditIndex = -1;
                    GridShow_Purchase();
                }
                else
                {
                    Int64 TransNo = Convert.ToInt64(ddlPurchaseEditInNo.Text);

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update STK_TRANS set INVREFNO = '" + txtPMemoNo.Text + "', STORETO='" + txtPToID.Text + "', PSID='" + txtPSupID.Text + "', LCTP = '" + ddlPLCType.Text + "', LCID= '" + lblPLcCD.Text + "', LCDATE ='" + lcDate + "', REMARKS = '" + txtPRemarks.Text + "', CATID = '" + lblPCatID.Text + "', ITEMID = '" + lblItemIDEdit_P.Text + "', UNITTP = '" + ddltypeEdit_P.Text + "', " +
                          " CPQTY =" + cpQty + ", CQTY = " + cQty + ", PQTY = " + pQty + ", QTY = " + Qty + ", RATE = " + Rate + ", AMOUNT = " + Amount + ", USERID = '" + userName + "' where TRANSTP = 'BUY' and TRANSMY='" + lblPMy.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + " and TRANSSL = '" + lblTransSLEdit_P.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();


                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set INVREFNO = '" + txtPMemoNo.Text + "', STORETO='" + txtPToID.Text + "', PSID='" + txtPSupID.Text + "',LCTP = '" + ddlPLCType.Text + "', LCID= '" + lblPLcCD.Text + "', LCDATE ='" + lcDate + "', REMARKS = '" + txtPRemarks.Text + "', USERID = '" + userName + "'" +
                          " where TRANSTP = 'BUY' and TRANSMY='" + lblPMy.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                    cmd1.ExecuteNonQuery();
                    conn.Close();

                    gvPurchase.EditIndex = -1;
                    GridShowPurchase_Edit();
                }
            }
        }

        protected void gvPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void txtPInDT_TextChanged(object sender, EventArgs e)
        {
            if (btnPurchaseEdit.Text == "EDIT")
            {
                DateTime transdate = DateTime.Parse(txtPInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                string varYear = transdate.ToString("yyyy");
                string month = transdate.ToString("MMM").ToUpper();
                string years = transdate.ToString("yy");
                //lblPMy.Text = month + "-" + years;
                lblPMy.Text = varYear;
                Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblPMy.Text + "' and TRANSTP = 'BUY'", lblPMxNo);
                if (lblPMxNo.Text == "")
                {
                    txtPInNo.Text = "1";
                }
                else
                {
                    int iNo = int.Parse(lblPMxNo.Text);
                    int totIno = iNo + 1;
                    txtPInNo.Text = totIno.ToString();
                }

                txtPMemoNo.Focus();
            }
            else
            {
                DateTime transdate = DateTime.Parse(txtPInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                string varYear = transdate.ToString("yyyy");
                string month = transdate.ToString("MMM").ToUpper();
                string years = transdate.ToString("yy");
                //lblPMy.Text = month + "-" + years;
                lblPMy.Text = varYear;

                Global.dropDownAddWithSelect(ddlPurchaseEditInNo, "SELECT DISTINCT TRANSNO FROM STK_TRANS WHERE TRANSDT ='" + TrDate + "' AND TRANSMY='" + lblPMy.Text + "' and TRANSTP='BUY'");
                ddlPurchaseEditInNo.Focus();
            }
        }

        protected void txtPTo_TextChanged(object sender, EventArgs e)
        {
            Global.txtAdd(@"Select STOREID from STK_STORE where STORENM = '" + txtPTo.Text + "'", txtPToID);
            ddlPLCType.Focus();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListPLcID(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020501','2020103') AND STATUSCD='P' AND ACCOUNTNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtPLCID_TextChanged(object sender, EventArgs e)
        {
            lblPLcCD.Text = "";
            Global.lblAdd("SELECT ACCOUNTCD FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020501','2020103') AND STATUSCD='P' AND ACCOUNTNM = '" + txtPLCID.Text + "'", lblPLcCD);
            txtPLCID.Focus();
        }

        protected void ddlPLCType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPLCType.Text == "IMPORT")
            {
                txtPLCID.Focus();
            }
            else
            {
                txtPSupplier.Focus();
            }
        }

        protected void txtPLCDT_TextChanged(object sender, EventArgs e)
        {
            txtPSupplier.Focus();
        }

        protected void txtPSupplier_TextChanged(object sender, EventArgs e)
        {
            Global.txtAdd(@"select ACCOUNTCD from GL_ACCHART where STATUSCD='P' and ACCOUNTNM='" + txtPSupplier.Text + "'", txtPSupID);
            txtPRemarks.Focus();
        }

        protected void txtPRemarks_TextChanged(object sender, EventArgs e)
        {
            TextBox txtItemNM_P = (TextBox)gvPurchase.FooterRow.FindControl("txtItemNM_P");
            txtItemNM_P.Focus();
        }

        protected void txtItemNM_P_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            //NamingContainer return the container that the control sits in
            TextBox txtItemNM_P = (TextBox)row.FindControl("txtItemNM_P");
            TextBox txtItemID_P = (TextBox)row.FindControl("txtItemID_P");
            txtItemID_P.ReadOnly = true;
            Global.txtAdd(@"Select ITEMID from STK_ITEM where ITEMNM = '" + txtItemNM_P.Text + "'", txtItemID_P);
            TextBox txtCPQTY_P = (TextBox)row.FindControl("txtCPQTY_P");
            Global.txtAdd(@"Select PQTY from STK_ITEM where ITEMNM = '" + txtItemNM_P.Text + "'", txtCPQTY_P);
            txtCPQTY_P.ReadOnly = true;
            TextBox txtCQty_P = (TextBox)row.FindControl("txtCQty_P");
            txtCQty_P.Text = "0";
            TextBox txtPQty_P = (TextBox)row.FindControl("txtPQty_P");
            TextBox txtQty_P = (TextBox)row.FindControl("txtQty_P");
            txtQty_P.Text = "0";
            txtPQty_P.Text = "0";
            TextBox txtRate_P = (TextBox)row.FindControl("txtRate_P");
            Global.txtAdd(@"Select BUYRT from STK_ITEM where ITEMNM = '" + txtItemNM_P.Text + "'", txtRate_P);
            TextBox txtAmount_P = (TextBox)row.FindControl("txtAmount_P");
            txtAmount_P.Text = ".00";
            txtAmount_P.ReadOnly = true;
            DropDownList ddlType_P = (DropDownList)row.FindControl("ddlType_P");
            ddlType_P.Focus();
        }

        protected void txtItemNMEdit_P_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            //NamingContainer return the container that the control sits in
            TextBox txtItemNMEdit_P = (TextBox)row.FindControl("txtItemNMEdit_P");
            Label lblItemIDEdit_P = (Label)row.FindControl("lblItemIDEdit_P");
            Global.lblAdd(@"Select ITEMID from STK_ITEM where ITEMNM = '" + txtItemNMEdit_P.Text + "'", lblItemIDEdit_P);
            TextBox txtCPQTYEdit_P = (TextBox)row.FindControl("txtCPQTYEdit_P");
            Global.txtAdd(@"Select PQTY from STK_ITEM where ITEMNM = '" + txtItemNMEdit_P.Text + "'", txtCPQTYEdit_P);
            txtCPQTYEdit_P.ReadOnly = true;
            TextBox txtCQtyEdit_P = (TextBox)row.FindControl("txtCQtyEdit_P");
            txtCQtyEdit_P.Text = "0";
            TextBox txtPQtyEdit_P = (TextBox)row.FindControl("txtPQtyEdit_P");
            TextBox txtQtyEdit_P = (TextBox)row.FindControl("txtQtyEdit_P");
            txtQtyEdit_P.Text = "0";
            txtPQtyEdit_P.Text = "0";
            TextBox txtRateEdit_P = (TextBox)row.FindControl("txtRateEdit_P");
            Global.txtAdd(@"Select BUYRT from STK_ITEM where ITEMNM = '" + txtItemNMEdit_P.Text + "'", txtRateEdit_P);
            TextBox txtAmountEdit_P = (TextBox)row.FindControl("txtAmountEdit_P");
            txtAmountEdit_P.Text = ".00";
            txtAmountEdit_P.ReadOnly = true;
            DropDownList ddltypeEdit_P = (DropDownList)row.FindControl("ddltypeEdit_P");
            ddltypeEdit_P.Focus();
        }

        protected void ddlType_P_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddlType_P = (DropDownList)row.FindControl("ddlType_P");
            if (ddlType_P.Text == "CARTON")
            {
                TextBox txtCQty_P = (TextBox)row.FindControl("txtCQty_P");
                txtCQty_P.Focus();
            }
            else
            {
                TextBox txtPQty_P = (TextBox)row.FindControl("txtPQty_P");
                txtPQty_P.Focus();
                TextBox txtCQty_P = (TextBox)row.FindControl("txtCQty_P");
                txtCQty_P.Text = "0";
            }
        }

        protected void ddltypeEdit_P_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddltypeEdit_P = (DropDownList)row.FindControl("ddltypeEdit_P");
            if (ddltypeEdit_P.Text == "CARTON")
            {
                TextBox txtCQtyEdit_P = (TextBox)row.FindControl("txtCQtyEdit_P");
                txtCQtyEdit_P.Focus();
            }
            else
            {
                TextBox txtPQtyEdit_P = (TextBox)row.FindControl("txtPQtyEdit_P");
                txtPQtyEdit_P.Focus();
                TextBox txtCQtyEdit_P = (TextBox)row.FindControl("txtCQtyEdit_P");
                txtCQtyEdit_P.Text = "0";
            }
        }

        protected void txtCQty_P_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCPQTY_P = (TextBox)row.FindControl("txtCPQTY_P");
            Int64 CPQty = Convert.ToInt64(txtCPQTY_P.Text);
            TextBox txtCQty_P = (TextBox)row.FindControl("txtCQty_P");
            Int64 CQty = Convert.ToInt64(txtCQty_P.Text);
            TextBox txtPQty_P = (TextBox)row.FindControl("txtPQty_P");
            Int64 PQty;
            if (txtPQty_P.Text == "")
            {
                PQty = 0;
            }
            else
                PQty = Convert.ToInt64(txtPQty_P.Text);

            Int64 Qty = CPQty * CQty + PQty;
            TextBox txtQty_P = (TextBox)row.FindControl("txtQty_P");
            txtQty_P.Text = Qty.ToString();
            txtQty_P.ReadOnly = true;

            TextBox txtRate_P = (TextBox)row.FindControl("txtRate_P");
            decimal Rate = Convert.ToDecimal(txtRate_P.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmount_P = (TextBox)row.FindControl("txtAmount_P");
            txtAmount_P.Text = Amount.ToString();
            txtAmount_P.ReadOnly = true;
            txtPQty_P.Focus();
        }

        protected void txtCQtyEdit_P_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCPQTYEdit_P = (TextBox)row.FindControl("txtCPQTYEdit_P");
            Int64 CPQty = Convert.ToInt64(txtCPQTYEdit_P.Text);
            TextBox txtCQtyEdit_P = (TextBox)row.FindControl("txtCQtyEdit_P");
            Int64 CQty = Convert.ToInt64(txtCQtyEdit_P.Text);
            TextBox txtPQtyEdit_P = (TextBox)row.FindControl("txtPQtyEdit_P");
            Int64 PQty;
            if (txtPQtyEdit_P.Text == "")
            {
                PQty = 0;
            }
            else
                PQty = Convert.ToInt64(txtPQtyEdit_P.Text);

            Int64 Qty = CPQty * CQty + PQty;
            TextBox txtQtyEdit_P = (TextBox)row.FindControl("txtQtyEdit_P");
            txtQtyEdit_P.Text = Qty.ToString();
            txtQtyEdit_P.ReadOnly = true;

            TextBox txtRateEdit_P = (TextBox)row.FindControl("txtRateEdit_P");
            decimal Rate = Convert.ToDecimal(txtRateEdit_P.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmountEdit_P = (TextBox)row.FindControl("txtAmountEdit_P");
            txtAmountEdit_P.Text = Amount.ToString();
            txtAmountEdit_P.ReadOnly = true;
            txtPQtyEdit_P.Focus();
        }

        protected void txtPQty_P_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCPQTY_P = (TextBox)row.FindControl("txtCPQTY_P");
            Int64 CPQty = Convert.ToInt64(txtCPQTY_P.Text);
            TextBox txtCQty_P = (TextBox)row.FindControl("txtCQty_P");
            Int64 CQty = Convert.ToInt64(txtCQty_P.Text);
            TextBox txtPQty_P = (TextBox)row.FindControl("txtPQty_P");
            Int64 PQty = Convert.ToInt64(txtPQty_P.Text);
            Int64 Qty = CPQty * CQty + PQty;
            TextBox txtQty_P = (TextBox)row.FindControl("txtQty_P");
            txtQty_P.Text = Qty.ToString();
            txtQty_P.ReadOnly = true;

            TextBox txtRate_P = (TextBox)row.FindControl("txtRate_P");
            decimal Rate = Convert.ToDecimal(txtRate_P.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmount_P = (TextBox)row.FindControl("txtAmount_P");
            txtAmount_P.Text = Amount.ToString();
            txtAmount_P.ReadOnly = true;

            txtRate_P.Focus();
            //ImageButton imgbtnPAdd = (ImageButton)row.FindControl("imgbtnPAdd");
            //imgbtnPAdd.Focus();
        }

        protected void txtPQtyEdit_P_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCPQTYEdit_P = (TextBox)row.FindControl("txtCPQTYEdit_P");
            Int64 CPQty = Convert.ToInt64(txtCPQTYEdit_P.Text);
            TextBox txtCQtyEdit_P = (TextBox)row.FindControl("txtCQtyEdit_P");
            Int64 CQty = Convert.ToInt64(txtCQtyEdit_P.Text);
            TextBox txtPQtyEdit_P = (TextBox)row.FindControl("txtPQtyEdit_P");
            Int64 PQty = Convert.ToInt64(txtPQtyEdit_P.Text);
            Int64 Qty = CPQty * CQty + PQty;
            TextBox txtQtyEdit_P = (TextBox)row.FindControl("txtQtyEdit_P");
            txtQtyEdit_P.Text = Qty.ToString();
            txtQtyEdit_P.ReadOnly = true;

            TextBox txtRateEdit_P = (TextBox)row.FindControl("txtRateEdit_P");
            decimal Rate = Convert.ToDecimal(txtRateEdit_P.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmountEdit_P = (TextBox)row.FindControl("txtAmountEdit_P");
            txtAmountEdit_P.Text = Amount.ToString();
            txtAmountEdit_P.ReadOnly = true;

            txtRateEdit_P.Focus();
            //ImageButton imgbtnPUpdate = (ImageButton)row.FindControl("imgbtnPUpdate");
            //imgbtnPUpdate.Focus();
        }

        protected void txtQty_P_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtQtyEdit_P_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtRate_P_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

            TextBox txtQty_P = (TextBox)row.FindControl("txtQty_P");
            Int64 Qty = Convert.ToInt64(txtQty_P.Text);

            TextBox txtRate_P = (TextBox)row.FindControl("txtRate_P");
            decimal Rate = Convert.ToDecimal(txtRate_P.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmount_P = (TextBox)row.FindControl("txtAmount_P");
            txtAmount_P.Text = Amount.ToString();
            txtAmount_P.ReadOnly = true;

            ImageButton imgbtnPAdd = (ImageButton)row.FindControl("imgbtnPAdd");
            imgbtnPAdd.Focus();
        }

        protected void txtRateEdit_P_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

            TextBox txtQtyEdit_P = (TextBox)row.FindControl("txtQtyEdit_P");
            Int64 Qty = Convert.ToInt64(txtQtyEdit_P.Text);

            TextBox txtRateEdit_P = (TextBox)row.FindControl("txtRateEdit_P");
            decimal Rate = Convert.ToDecimal(txtRateEdit_P.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmountEdit_P = (TextBox)row.FindControl("txtAmountEdit_P");
            txtAmountEdit_P.Text = Amount.ToString();
            txtAmountEdit_P.ReadOnly = true;

            ImageButton imgbtnPUpdate = (ImageButton)row.FindControl("imgbtnPUpdate");
            imgbtnPUpdate.Focus();
        }

        /// <summary>
        /// Editing of Store Transaction Purchase
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnPurchaseEdit_Click(object sender, EventArgs e)
        {
            if (btnPurchaseEdit.Text == "EDIT")
            {
                DateTime transdate = DateTime.Parse(txtPInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                txtPInNo.Visible = false;
                btnPurchaseEdit.Text = "NEW";
                btnPUpdate.Visible = true;
                btnPurchasePrint.Visible = true;
                ddlPurchaseEditInNo.Visible = true;
                Global.dropDownAddWithSelect(ddlPurchaseEditInNo, "SELECT DISTINCT TRANSNO FROM STK_TRANS WHERE TRANSDT ='" + TrDate + "' AND TRANSMY='" + lblPMy.Text + "' and TRANSTP='BUY'");
                ddlPurchaseEditInNo.SelectedIndex = -1;
                txtPMemoNo.Text = "";
                //ddlPLCType.SelectedIndex = -1;
                txtPLCID.Text = "";
                lblPLcCD.Text = "";
                txtPLCDT.Text = "";
                txtPTo.Text = "";
                txtPToID.Text = "";
                txtPSupplier.Text = "";
                txtPSupID.Text = "";
                txtPRemarks.Text = "";
                txtPTotal.Text = "0.00";
                //lblPMy.Text = "";
                GridShowPurchase_Edit();
            }
            else
            {
                txtPInNo.Visible = true;
                btnPurchaseEdit.Text = "EDIT";
                btnPUpdate.Visible = false;
                btnPurchasePrint.Visible = false;
                ddlPurchaseEditInNo.Visible = false;
                ddlPurchaseEditInNo.SelectedIndex = -1;
                txtPMemoNo.Text = "";
                lblPValidMsg.Visible = false;
                lblValidMSG.Visible = false;
                lblPGridMSG.Visible = false;
                //ddlPLCType.SelectedIndex = -1;
                txtPLCID.Text = "";
                lblPLcCD.Text = "";
                txtPLCDT.Text = "";
                txtPTo.Text = "";
                txtPToID.Text = "";
                txtPSupplier.Text = "";
                txtPSupID.Text = "";
                txtPRemarks.Text = "";
                txtPTotal.Text = "0.00";
                Purchase_Start();
            }
        }

        protected void ddlPurchaseEditInNo_TextChanged(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            DateTime transdate = DateTime.Parse(txtPInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = transdate.ToString("yyyy/MM/dd");

            if (ddlPurchaseEditInNo.Text == "Select")
            {
                gvPurchase.Visible = false;
                lblPGridMSG.Visible = true;
                lblPGridMSG.Text = "Select Invoice No.";
                txtPTotal.Text = "";
            }
            else
            {
                lblPLCTP.Text = "IMPORT";
                gvPurchase.Visible = true;
                lblPGridMSG.Visible = false;
                Int64 TransNo = Convert.ToInt64(ddlPurchaseEditInNo.Text);

                Global.txtAdd(@"select INVREFNO from STK_TRANSMST where TRANSTP='BUY' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblPMy.Text + "' and TRANSNO =" + TransNo + "", txtPMemoNo);
                Global.txtAdd(@"select STORETO from STK_TRANSMST where TRANSTP='BUY' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblPMy.Text + "' and TRANSNO =" + TransNo + "", txtPToID);
                Global.txtAdd(@"select STORENM from STK_STORE where STOREID ='" + txtPToID.Text + "'", txtPTo);
                Global.lblAdd(@"select LCTP from STK_TRANSMST where TRANSTP='BUY' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblPMy.Text + "' and TRANSNO =" + TransNo + "", lblPLCTP);

                //if (lblPLCTP.Text == "")
                //{
                //    lblPLCTP.Text = "IMPORT";
                //}
                //else lblPLCTP.Text = lblPLCTP.Text;

                ddlPLCType.Text = lblPLCTP.Text;

                //Global.dropDownAdd(ddlPLCType, "select LCTP from STK_TRANSMST where TRANSTP='BUY' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblPMy.Text + "' and TRANSNO =" + TransNo + "");
                Global.lblAdd("select LCID from STK_TRANSMST where TRANSTP='BUY' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblPMy.Text + "' and TRANSNO =" + TransNo + "", lblPLcCD);
                Global.txtAdd(@"SELECT ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD = '" + lblPLcCD.Text + "'", txtPLCID);
                Global.txtAdd(@"select convert(nvarchar(20),LCDATE,103)as LCDATE  from STK_TRANSMST where TRANSTP='BUY' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblPMy.Text + "' and TRANSNO =" + TransNo + "", txtPLCDT);
                if (txtPLCDT.Text == "01/01/1900")
                {
                    txtPLCDT.Text = "";
                }
                else
                {
                    txtPLCDT.Text = txtPLCDT.Text;
                }
                Global.txtAdd(@"select PSID from STK_TRANSMST where TRANSTP='BUY' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblPMy.Text + "' and TRANSNO =" + TransNo + "", txtPSupID);
                Global.txtAdd(@"select ACCOUNTNM from GL_ACCHART where ACCOUNTCD ='" + txtPSupID.Text + "'", txtPSupplier);
                Global.txtAdd(@"select REMARKS from STK_TRANSMST where TRANSTP='BUY' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblPMy.Text + "' and TRANSNO =" + TransNo + "", txtPRemarks);
                GridShowPurchase_Edit();
            }
        }

        protected void GridShowPurchase_Edit()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            DateTime transdate = DateTime.Parse(txtPInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = transdate.ToString("yyyy/MM/dd");

            Int64 EditTransNo = 0;
            if (ddlPurchaseEditInNo.Text == "Select")
            {
                EditTransNo = 0;
            }
            else
                EditTransNo = Convert.ToInt64(ddlPurchaseEditInNo.Text);

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT STK_ITEM.ITEMNM, STK_TRANS.* FROM STK_ITEM INNER JOIN STK_TRANS ON STK_ITEM.ITEMID = STK_TRANS.ITEMID where STK_TRANS.TRANSTP='BUY' and STK_TRANS.TRANSDT = '" + TrDt + "' and STK_TRANS.TRANSMY = '" + lblPMy.Text + "' and STK_TRANS.TRANSNO = " + EditTransNo + " order by TRANSSL", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPurchase.DataSource = ds;
                gvPurchase.DataBind();

                if (gvPurchase.EditIndex == -1)
                {
                    Decimal totAmt = 0;
                    Decimal a = 0;
                    foreach (GridViewRow grid in gvPurchase.Rows)
                    {
                        Label Per = (Label)grid.Cells[3].FindControl("lblAmount_P");
                        if (Per.Text == "")
                        {
                            Per.Text = "0";
                        }
                        else
                        {
                            Per.Text = Per.Text;
                        }
                        String Perf = Per.Text;
                        totAmt = Convert.ToDecimal(Perf);
                        a += totAmt;
                        txtPTotal.Text = a.ToString();
                    }
                    a += totAmt;
                }
                else
                {

                }

                TextBox txtItemNM_P = (TextBox)gvPurchase.FooterRow.FindControl("txtItemNM_P");
                txtItemNM_P.Focus();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvPurchase.DataSource = ds;
                gvPurchase.DataBind();
                int columncount = gvPurchase.Rows[0].Cells.Count;
                gvPurchase.Rows[0].Cells.Clear();
                gvPurchase.Rows[0].Cells.Add(new TableCell());
                gvPurchase.Rows[0].Cells[0].ColumnSpan = columncount;
                gvPurchase.Rows[0].Cells[0].Text = "No Records Found";
                gvPurchase.Rows[0].Visible = false;
                txtPMemoNo.Text = "";
                txtPToID.Text = "";
                txtPTo.Text = "";
                txtPSupID.Text = "";
                txtPSupplier.Text = "";
                txtPRemarks.Text = "";
                txtPLCID.Text = "";
                lblPLcCD.Text = "";
                txtPLCDT.Text = "";
                ddlPurchaseEditInNo.SelectedIndex = -1;
            }
        }


        protected void btnPUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string userName = HttpContext.Current.Session["UserName"].ToString();

            DateTime transdate = DateTime.Parse(txtPInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = transdate.ToString("yyyy/MM/dd");

            if (ddlPurchaseEditInNo.Text == "Select")
            {
                lblPGridMSG.Visible = true;
                lblPGridMSG.Text = "Select Transaction No.";
                ddlPurchaseEditInNo.Focus();
            }
            else if (txtPToID.Text == "")
            {
                lblPValidMsg.Visible = true;
                lblPValidMsg.Text = "Select Purchase to.";
                txtPTo.Focus();
            }
            //else if (txtPSupID.Text == "")
            //{
            //    lblValidMSG.Visible = true;
            //    lblValidMSG.Text = "Select Supplier ID.";
            //    txtPSupplier.Focus();
            //}
            else
            {
                lblPGridMSG.Visible = false;
                lblPValidMsg.Visible = false;
                lblValidMSG.Visible = false;

                Int64 TransNo = Convert.ToInt64(ddlPurchaseEditInNo.Text);

                conn.Open();
                SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set INVREFNO = '" + txtPMemoNo.Text + "', STORETO='" + txtPToID.Text + "', PSID='" + txtPSupID.Text + "', LCID = '" + lblPLcCD.Text + "', REMARKS = '" + txtPRemarks.Text + "', USERID = '" + userName + "'" +
                      " where TRANSTP = 'BUY' and TRANSMY='" + lblPMy.Text + "' and TRANSDT='" + TrDt + "' and TRANSNO = " + TransNo + "", conn);
                cmd1.ExecuteNonQuery();
                conn.Close();

                conn.Open();
                SqlCommand cmd = new SqlCommand("update STK_TRANS set INVREFNO = '" + txtPMemoNo.Text + "', STORETO='" + txtPToID.Text + "', PSID='" + txtPSupID.Text + "', LCID = '" + lblPLcCD.Text + "', REMARKS = '" + txtPRemarks.Text + "', " +
                      " USERID = '" + userName + "' where TRANSTP = 'BUY' and TRANSMY='" + lblPMy.Text + "' and TRANSDT='" + TrDt + "' and TRANSNO = " + TransNo + " ", conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                TextBox txtItemNM_P = (TextBox)gvPurchase.FooterRow.FindControl("txtItemNM_P");
                txtItemNM_P.Focus();
            }
        }

        ////////////////////////////////////////////////////////// END PURCHASE //////////////////////////////////////////////



        ///////////////////////////////////////////////////////// START TRANSFER /////////////////////////////////////////////

        public void Transfer_Start()
        {

            DateTime today = DateTime.Today.Date;
            string td = Global.Dayformat(today);
            txtInDT_Trans.Text = td;
            DateTime transdate = DateTime.Parse(txtInDT_Trans.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDate = transdate.ToString("yyyy/MM/dd");

            string varYear = today.ToString("yyyyy");
            string mon = DateTime.Today.Date.ToString("MMM").ToUpper();
            string year = today.ToString("yy");
            lblTMy.Text = varYear;
            //lblTMy.Text = mon + "-" + year;
            Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblTMy.Text + "' and TRANSTP = 'ITRF'", lblTMxNo);
            if (lblTMxNo.Text == "")
            {
                txtInNo_Trans.Text = "1";
            }
            else
            {
                int iNo = int.Parse(lblTMxNo.Text);
                int totIno = iNo + 1;
                txtInNo_Trans.Text = totIno.ToString();
            }

            GridShow_Transfer();
        }

        protected void GridShow_Transfer()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            DateTime transdate = DateTime.Parse(txtInDT_Trans.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = transdate.ToString("yyyy/MM/dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT STK_ITEM.ITEMNM, STK_TRANS.* FROM STK_ITEM INNER JOIN STK_TRANS ON STK_ITEM.ITEMID = STK_TRANS.ITEMID " +
                             " where STK_TRANS.TRANSTP='ITRF' and STK_TRANS.TRANSDT = '" + TrDt + "' and STK_TRANS.TRANSMY = '" + lblTMy.Text + "' and " +
                             " STK_TRANS.TRANSNO = '" + txtInNo_Trans.Text + "' order by TRANSSL", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_Transfer.DataSource = ds;
                gv_Transfer.DataBind();

                if (gv_Transfer.EditIndex == -1)
                {
                    Decimal totAmt = 0;
                    Decimal a = 0;
                    foreach (GridViewRow grid in gv_Transfer.Rows)
                    {
                        Label Per = (Label)grid.Cells[8].FindControl("lblAmount_T");
                        if (Per.Text == "")
                        {
                            Per.Text = "0";
                        }
                        else
                        {
                            Per.Text = Per.Text;
                        }
                        String Perf = Per.Text;
                        totAmt = Convert.ToDecimal(Perf);
                        a += totAmt;
                        txtTTotalAmount.Text = a.ToString();
                    }
                    a += totAmt;
                }
                else
                {

                }

                TextBox txtItemNM_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtItemNM_T");
                txtItemNM_T.Focus();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_Transfer.DataSource = ds;
                gv_Transfer.DataBind();
                int columncount = gv_Transfer.Rows[0].Cells.Count;
                gv_Transfer.Rows[0].Cells.Clear();
                gv_Transfer.Rows[0].Cells.Add(new TableCell());
                gv_Transfer.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_Transfer.Rows[0].Cells[0].Text = "No Records Found";
                gv_Transfer.Rows[0].Visible = false;
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListStore_TFrom(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT STORENM FROM STK_STORE WHERE STORENM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["STORENM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtTFr_Trans_TextChanged(object sender, EventArgs e)
        {
            Global.txtAdd(@"Select STOREID from STK_STORE where STORENM = '" + txtTFr_Trans.Text + "'", txtTFrID_Trans);
            txtTTo_Trans.Focus();
        }

        protected void txtTTo_Trans_TextChanged(object sender, EventArgs e)
        {
            Global.txtAdd(@"Select STOREID from STK_STORE where STORENM = '" + txtTTo_Trans.Text + "'", txtTToID_Trans);
            txtRemarks_Trans.Focus();
        }

        protected void txtRemarks_Trans_TextChanged(object sender, EventArgs e)
        {
            TextBox txtItemNM_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtItemNM_T");
            txtItemNM_T.Focus();
        }

        protected void gv_Transfer_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (btnTransferEdit.Text == "EDIT")
            {
                gv_Transfer.EditIndex = -1;
                GridShow_Transfer();
            }
            else
            {
                gv_Transfer.EditIndex = -1;
                GridShow_Transfer();
            }
        }

        protected void gv_Transfer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string query = "";
            SqlCommand comm = new SqlCommand(query, conn);

            string Transtp = "ITRF";


            DateTime TransDT = DateTime.Parse(txtInDT_Trans.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = TransDT.ToString("yyyy/MM/dd");


            if (e.CommandName.Equals("SaveCon"))
            {
                TextBox txtItemID_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtItemID_T");
                TextBox txtItemNM_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtItemNM_T");
                TextBox txtQty_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtQty_T");
                TextBox txtPQty_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtPQty_T");

                if (txtTFrID_Trans.Text == "")
                {
                    lblFromMsg_Trans.Visible = true;
                    lblFromMsg_Trans.Text = "Select Store.";
                    txtTFr_Trans.Focus();
                    lblMsgTo_Trans.Visible = false;
                    lblTGridMSG.Visible = false;
                }
                else if (txtTToID_Trans.Text == "")
                {
                    lblMsgTo_Trans.Visible = true;
                    lblMsgTo_Trans.Text = "Select Store";
                    txtTTo_Trans.Focus();
                    lblFromMsg_Trans.Visible = false;
                    lblTGridMSG.Visible = false;
                }
                else if (txtTFrID_Trans.Text == txtTToID_Trans.Text)
                {
                    lblMsgTo_Trans.Visible = true;
                    lblMsgTo_Trans.Text = "Same Store";
                    txtTTo_Trans.Focus();
                    lblFromMsg_Trans.Visible = false;
                    lblTGridMSG.Visible = false;
                }
                else if (txtItemID_T.Text == "")
                {
                    lblTGridMSG.Visible = true;
                    lblTGridMSG.Text = "Select Item.";
                    txtItemNM_T.Focus();
                    lblFromMsg_Trans.Visible = false;
                    lblMsgTo_Trans.Visible = false;
                }
                else if (txtQty_T.Text == "0")
                {
                    lblTGridMSG.Visible = true;
                    lblTGridMSG.Text = "Quantity is Wrong.";
                    txtQty_T.Focus();
                }
                else
                {
                    lblFromMsg_Trans.Visible = false;
                    lblMsgTo_Trans.Visible = false;
                    lblTGridMSG.Visible = false;

                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    if (btnTransferEdit.Text == "EDIT")
                    {
                        cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + txtInNo_Trans.Text + "' and TRANSTP='ITRF' AND TRANSMY='" + lblTMy.Text + "'", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + ddlInNoEdit_Trans.Text + "' and TRANSTP='ITRF' AND TRANSMY='" + lblTMy.Text + "'", conn);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    conn.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM_T.Text + "'", lblTCatID);

                        DropDownList ddlType_T = (DropDownList)gv_Transfer.FooterRow.FindControl("ddlType_T");
                        TextBox txtCPQTY_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtCPQTY_T");
                        if (txtCPQTY_T.Text == "")
                        {
                            txtCPQTY_T.Text = "0";
                        }
                        else
                            txtCPQTY_T.Text = txtCPQTY_T.Text;
                        decimal cpQty = 0;
                        cpQty = Convert.ToDecimal(txtCPQTY_T.Text);
                        TextBox txtCQty_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtCQty_T");
                        if (txtCQty_T.Text == "")
                        {
                            txtCQty_T.Text = "0";
                        }
                        else
                            txtCQty_T.Text = txtCQty_T.Text;
                        decimal cQty = 0;
                        cQty = Convert.ToDecimal(txtCQty_T.Text);
                        decimal pQty = 0;
                        if (txtPQty_T.Text == "")
                        {
                            txtPQty_T.Text = "0";
                        }
                        else
                            txtPQty_T.Text = txtPQty_T.Text;
                        pQty = Convert.ToDecimal(txtPQty_T.Text);

                        decimal Qty = 0;
                        if (txtQty_T.Text == "")
                        {
                            txtQty_T.Text = "0";
                        }
                        else
                            txtQty_T.Text = txtQty_T.Text;
                        Qty = Convert.ToDecimal(txtQty_T.Text);
                        TextBox txtRate_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtRate_T");
                        decimal Rate = 0;
                        Rate = Convert.ToDecimal(txtRate_T.Text);
                        TextBox txtAmount_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtAmount_T");
                        decimal Amount = 0;
                        Amount = Convert.ToDecimal(txtAmount_T.Text);

                        Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'ITRF' and TRANSMY = '" + lblTMy.Text + "'", lblTTransSl);
                        string ItemCD;
                        string mxCD = "";
                        string mid = "";
                        string subItemCD = "";
                        int subCD, incrItCD;
                        if (lblTTransSl.Text == "")
                        {
                            ItemCD = "00000001";
                        }
                        else
                        {
                            mxCD = lblTTransSl.Text;
                            //OItemCD = mxCD.Substring(4,4);
                            subCD = int.Parse(mxCD);
                            incrItCD = subCD + 1;
                            if (incrItCD < 10)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000000" + mid;
                            }
                            else if (incrItCD < 100)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000000" + mid;
                            }
                            else if (incrItCD < 1000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00000" + mid;
                            }
                            else if (incrItCD < 10000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000" + mid;
                            }
                            else if (incrItCD < 100000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000" + mid;
                            }
                            else if (incrItCD < 1000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00" + mid;
                            }
                            else if (incrItCD < 10000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0" + mid;
                            }
                            //else
                            //    subItemCD = incrItCD.ToString();

                            ItemCD = subItemCD;
                        }


                        if (btnTransferEdit.Text == "EDIT")
                        {
                            query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblTMy.Text + "','" + txtInNo_Trans.Text + "','" + txtMemo_Trans.Text + "','" + txtTFrID_Trans.Text + "','" + txtTToID_Trans.Text + "','','','','','" + txtRemarks_Trans.Text + "',@TRANSSL,'" + lblTCatID.Text + "','" + txtItemID_T.Text + "', " +
                                     " '" + ddlType_T.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ",'',@USERID,'')");

                            comm = new SqlCommand(query, conn);
                            comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                            comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                            //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                            comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                            comm.Parameters.AddWithValue("@USERID", userName);

                            conn.Open();
                            int result = comm.ExecuteNonQuery();
                            conn.Close();
                            GridShow_Transfer();
                        }

                        else
                        {
                            Int64 TransferEdit = Convert.ToInt64(ddlInNoEdit_Trans.Text);
                            conn.Open();
                            SqlCommand cmd1 = new SqlCommand("select * from STK_TRANSMST where TRANSTP = 'ITRF' TRANSMY='" + lblTMy.Text + "' and TRANSNO =" + TransferEdit + "", conn);
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                            DataSet ds1 = new DataSet();
                            da.Fill(ds1);
                            conn.Close();
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblTMy.Text + "','" + ddlInNoEdit_Trans.Text + "','" + txtMemo_Trans.Text + "','" + txtTFrID_Trans.Text + "','" + txtTToID_Trans.Text + "','','','','','" + txtRemarks_Trans.Text + "',@TRANSSL,'" + lblTCatID.Text + "','" + txtItemID_T.Text + "', " +
                                     " '" + ddlType_T.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ",'',@USERID,'')");

                                comm = new SqlCommand(query, conn);
                                comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                                comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                                //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                                comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                                comm.Parameters.AddWithValue("@USERID", userName);

                                conn.Open();
                                int result = comm.ExecuteNonQuery();
                                conn.Close();
                                GridShowTransfer_Edit();
                            }
                            else
                            {
                                lblTGridMSG.Visible = true;
                                lblTGridMSG.Text = "Must be in New Mode.";
                            }
                        }
                    }
                    else
                    {

                        query = "insert into STK_TRANSMST ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS, USERPC, USERID, IPADDRESS) " +
                                    "values(@TRANSTP,@TRANSDT,'" + lblTMy.Text + "','" + txtInNo_Trans.Text + "','" + txtMemo_Trans.Text + "','" + txtTFrID_Trans.Text + "','" + txtTToID_Trans.Text + "','','','','','" + txtRemarks_Trans.Text + "','',@USERID,'')";
                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                        comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                        //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                        comm.Parameters.AddWithValue("@USERID", userName);
                        conn.Open();
                        comm.ExecuteNonQuery();
                        conn.Close();


                        Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM_T.Text + "'", lblTCatID);

                        DropDownList ddlType_T = (DropDownList)gv_Transfer.FooterRow.FindControl("ddlType_T");
                        TextBox txtCPQTY_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtCPQTY_T");
                        if (txtCPQTY_T.Text == "")
                        {
                            txtCPQTY_T.Text = "0";
                        }
                        else
                            txtCPQTY_T.Text = txtCPQTY_T.Text;
                        decimal cpQty = 0;
                        cpQty = Convert.ToDecimal(txtCPQTY_T.Text);
                        TextBox txtCQty_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtCQty_T");
                        if (txtCQty_T.Text == "")
                        {
                            txtCQty_T.Text = "0";
                        }
                        else
                            txtCQty_T.Text = txtCQty_T.Text;
                        decimal cQty = 0;
                        cQty = Convert.ToDecimal(txtCQty_T.Text);
                        decimal pQty = 0;
                        if (txtPQty_T.Text == "")
                        {
                            txtPQty_T.Text = "0";
                        }
                        else
                            txtPQty_T.Text = txtPQty_T.Text;
                        pQty = Convert.ToDecimal(txtPQty_T.Text);

                        decimal Qty = 0;
                        if (txtQty_T.Text == "")
                        {
                            txtQty_T.Text = "0";
                        }
                        else
                            txtQty_T.Text = txtQty_T.Text;
                        Qty = Convert.ToDecimal(txtQty_T.Text);
                        TextBox txtRate_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtRate_T");
                        decimal Rate = 0;
                        Rate = Convert.ToDecimal(txtRate_T.Text);
                        TextBox txtAmount_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtAmount_T");
                        decimal Amount = 0;
                        Amount = Convert.ToDecimal(txtAmount_T.Text);

                        Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'ITRF' and TRANSMY = '" + lblTMy.Text + "'", lblTTransSl);
                        string ItemCD;
                        string mxCD = "";
                        string mid = "";
                        string subItemCD = "";
                        int subCD, incrItCD;
                        if (lblTTransSl.Text == "")
                        {
                            ItemCD = "00000001";
                        }
                        else
                        {
                            mxCD = lblTTransSl.Text;
                            //OItemCD = mxCD.Substring(4,4);
                            subCD = int.Parse(mxCD);
                            incrItCD = subCD + 1;
                            if (incrItCD < 10)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000000" + mid;
                            }
                            else if (incrItCD < 100)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000000" + mid;
                            }
                            else if (incrItCD < 1000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00000" + mid;
                            }
                            else if (incrItCD < 10000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000" + mid;
                            }
                            else if (incrItCD < 100000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000" + mid;
                            }
                            else if (incrItCD < 1000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00" + mid;
                            }
                            else if (incrItCD < 10000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0" + mid;
                            }
                            //else
                            //    subItemCD = incrItCD.ToString();

                            ItemCD = subItemCD;
                        }

                        query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, USERPC, USERID, IPADDRESS ) " +
                                 "values(@TRANSTP,@TRANSDT,'" + lblTMy.Text + "','" + txtInNo_Trans.Text + "','" + txtMemo_Trans.Text + "','" + txtTFrID_Trans.Text + "','" + txtTToID_Trans.Text + "','','','','','" + txtRemarks_Trans.Text + "',@TRANSSL,'" + lblTCatID.Text + "','" + txtItemID_T.Text + "', " +
                                 " '" + ddlType_T.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ",'',@USERID,'')");

                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                        comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                        //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                        comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                        comm.Parameters.AddWithValue("@USERID", userName);

                        conn.Open();
                        int result = comm.ExecuteNonQuery();
                        conn.Close();

                        if (btnSaleEdit.Text == "EDIT")
                        {
                            GridShow_Transfer();
                        }
                        else
                        {
                            GridShowTransfer_Edit();
                        }
                    }
                }
            }


                ////////////////////////////////////////////////////////// For Complete   //////////////////////////////////////

            else if (e.CommandName.Equals("Complete"))
            {
                TextBox txtItemID_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtItemID_T");
                TextBox txtItemNM_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtItemNM_T");
                TextBox txtQty_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtQty_T");
                TextBox txtPQty_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtPQty_T");
                if (txtTFrID_Trans.Text == "")
                {
                    lblFromMsg_Trans.Visible = true;
                    lblFromMsg_Trans.Text = "Select Store.";
                    txtTFr_Trans.Focus();
                    lblMsgTo_Trans.Visible = false;
                    lblTGridMSG.Visible = false;
                }
                else if (txtTToID_Trans.Text == "")
                {
                    lblMsgTo_Trans.Visible = true;
                    lblMsgTo_Trans.Text = "Select Party";
                    txtTTo_Trans.Focus();
                    lblTGridMSG.Visible = false;
                    lblFromMsg_Trans.Visible = false;
                }
                else if (txtItemID_T.Text == "")
                {
                    lblTGridMSG.Visible = true;
                    lblTGridMSG.Text = "Select Item.";
                    txtItemNM_T.Focus();
                    lblFromMsg_Trans.Visible = false;
                    lblMsgTo_Trans.Visible = false;
                }
                else if (txtQty_T.Text == "0")
                {
                    lblTGridMSG.Visible = true;
                    lblTGridMSG.Text = "Quantity is Wrong.";
                    txtPQty_T.Focus();
                }
                else
                {
                    lblFromMsg_Trans.Visible = false;
                    lblMsgTo_Trans.Visible = false;
                    lblTGridMSG.Visible = false;

                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    if (btnTransferEdit.Text == "EDIT")
                    {
                        cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + txtInNo_Trans.Text + "' and TRANSTP='ITRF' AND TRANSMY='" + lblTMy.Text + "'", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + ddlInNoEdit_Trans.Text + "' and TRANSTP='ITRF' AND TRANSMY='" + lblTMy.Text + "'", conn);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    conn.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM_T.Text + "'", lblTCatID);

                        DropDownList ddlType_T = (DropDownList)gv_Transfer.FooterRow.FindControl("ddlType_T");
                        TextBox txtCPQTY_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtCPQTY_T");
                        if (txtCPQTY_T.Text == "")
                        {
                            txtCPQTY_T.Text = "0";
                        }
                        else
                            txtCPQTY_T.Text = txtCPQTY_T.Text;
                        decimal cpQty = 0;
                        cpQty = Convert.ToDecimal(txtCPQTY_T.Text);
                        TextBox txtCQty_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtCQty_T");
                        if (txtCQty_T.Text == "")
                        {
                            txtCQty_T.Text = "0";
                        }
                        else
                            txtCQty_T.Text = txtCQty_T.Text;
                        decimal cQty = 0;
                        cQty = Convert.ToDecimal(txtCQty_T.Text);
                        decimal pQty = 0;
                        if (txtPQty_T.Text == "")
                        {
                            txtPQty_T.Text = "0";
                        }
                        else
                            txtPQty_T.Text = txtPQty_T.Text;
                        pQty = Convert.ToDecimal(txtPQty_T.Text);

                        decimal Qty = 0;
                        if (txtQty_T.Text == "")
                        {
                            txtQty_T.Text = "0";
                        }
                        else
                            txtQty_T.Text = txtQty_T.Text;
                        Qty = Convert.ToDecimal(txtQty_T.Text);
                        TextBox txtRate_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtRate_T");
                        decimal Rate = 0;
                        Rate = Convert.ToDecimal(txtRate_T.Text);
                        TextBox txtAmount_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtAmount_T");
                        decimal Amount = 0;
                        Amount = Convert.ToDecimal(txtAmount_T.Text);

                        Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'ITRF' and TRANSMY = '" + lblTMy.Text + "'", lblTTransSl);
                        string ItemCD;
                        string mxCD = "";
                        string mid = "";
                        string subItemCD = "";
                        int subCD, incrItCD;
                        if (lblTTransSl.Text == "")
                        {
                            ItemCD = "00000001";
                        }
                        else
                        {
                            mxCD = lblTTransSl.Text;
                            //OItemCD = mxCD.Substring(4,4);
                            subCD = int.Parse(mxCD);
                            incrItCD = subCD + 1;
                            if (incrItCD < 10)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000000" + mid;
                            }
                            else if (incrItCD < 100)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000000" + mid;
                            }
                            else if (incrItCD < 1000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00000" + mid;
                            }
                            else if (incrItCD < 10000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000" + mid;
                            }
                            else if (incrItCD < 100000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000" + mid;
                            }
                            else if (incrItCD < 1000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00" + mid;
                            }
                            else if (incrItCD < 10000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0" + mid;
                            }
                            //else
                            //    subItemCD = incrItCD.ToString();

                            ItemCD = subItemCD;
                        }

                        if (btnTransferEdit.Text == "EDIT")
                        {
                            query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblTMy.Text + "','" + txtInNo_Trans.Text + "','" + txtMemo_Trans.Text + "','" + txtTFrID_Trans.Text + "','" + txtTToID_Trans.Text + "','','','','','" + txtRemarks_Trans.Text + "',@TRANSSL,'" + lblTCatID.Text + "','" + txtItemID_T.Text + "', " +
                                     " '" + ddlType_T.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ",'',@USERID,'')");

                            comm = new SqlCommand(query, conn);
                            comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                            comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                            //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                            comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                            comm.Parameters.AddWithValue("@USERID", userName);

                            conn.Open();
                            int result = comm.ExecuteNonQuery();
                            conn.Close();

                            ///////Refresh/////
                            txtMemo_Trans.Text = "";
                            txtTFr_Trans.Text = "";
                            txtTFrID_Trans.Text = "";
                            //ddlPLCType.SelectedIndex = -1;
                            //txtPLCID.Text = "";
                            //txtPLCDT.Text = "";
                            txtTTo_Trans.Text = "";
                            txtTToID_Trans.Text = "";
                            txtRemarks_Trans.Text = "";
                            txtItemID_T.Text = "";
                            txtItemNM_T.Text = "";
                            //ddlType_P.SelectedIndex = -1;
                            txtCPQTY_T.Text = "";
                            txtCQty_T.Text = "";
                            txtPQty_T.Text = "";
                            txtQty_T.Text = "";
                            txtRate_T.Text = ".00";
                            txtAmount_T.Text = ".00";
                            txtTTotalAmount.Text = "";


                            DateTime transdate = DateTime.Parse(txtInDT_Trans.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            string TrDate = transdate.ToString("yyyy/MM/dd");

                            Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblTMy.Text + "' and TRANSTP = 'ITRF'", lblTMxNo);
                            if (lblTMxNo.Text == "")
                            {
                                txtInNo_Trans.Text = "1";
                            }
                            else
                            {
                                int iNo = int.Parse(lblTMxNo.Text);
                                int totIno = iNo + 1;
                                txtInNo_Trans.Text = totIno.ToString();
                            }

                            GridShow_Transfer();
                            txtMemo_Trans.Focus();
                        }

                        else
                        {
                            Int64 TransferEdit = Convert.ToInt64(ddlInNoEdit_Trans.Text);
                            conn.Open();
                            SqlCommand cmd1 = new SqlCommand("select * from STK_TRANSMST where TRANSTP = 'ITRF' and TRANSMY='" + lblTMy.Text + "' and TRANSNO =" + TransferEdit + "", conn);
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                            DataSet ds1 = new DataSet();
                            da.Fill(ds1);
                            conn.Close();
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblTMy.Text + "','" + ddlInNoEdit_Trans.Text + "','" + txtMemo_Trans.Text + "','" + txtTFrID_Trans.Text + "','" + txtTToID_Trans.Text + "','','','','','" + txtRemarks_Trans.Text + "',@TRANSSL,'" + lblTCatID.Text + "','" + txtItemID_T.Text + "', " +
                                     " '" + ddlType_T.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ",'',@USERID,'')");

                                comm = new SqlCommand(query, conn);
                                comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                                comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                                //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                                comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                                comm.Parameters.AddWithValue("@USERID", userName);

                                conn.Open();
                                int result = comm.ExecuteNonQuery();
                                conn.Close();

                                ///////Refresh/////
                                ddlInNoEdit_Trans.SelectedIndex = -1;
                                txtMemo_Trans.Text = "";
                                txtTFr_Trans.Text = "";
                                txtTFrID_Trans.Text = "";
                                //ddlPLCType.SelectedIndex = -1;
                                //txtPLCID.Text = "";
                                //txtPLCDT.Text = "";
                                txtTTo_Trans.Text = "";
                                txtTToID_Trans.Text = "";
                                txtRemarks_Trans.Text = "";
                                txtItemID_T.Text = "";
                                txtItemNM_T.Text = "";
                                //ddlType_P.SelectedIndex = -1;
                                txtCPQTY_T.Text = "";
                                txtCQty_T.Text = "";
                                txtPQty_T.Text = "";
                                txtQty_T.Text = "";
                                txtRate_T.Text = ".00";
                                txtAmount_T.Text = ".00";
                                txtTTotalAmount.Text = "";

                                GridShowTransfer_Edit();
                                ddlInNoEdit_Trans.Focus();
                            }
                            else
                            {
                                lblTGridMSG.Visible = true;
                                lblTGridMSG.Text = "Must be in New Mode.";
                            }
                        }
                    }

                    else
                    {

                        query = "insert into STK_TRANSMST ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS, USERPC, USERID, IPADDRESS) " +
                                    "values(@TRANSTP,@TRANSDT,'" + lblTMy.Text + "','" + txtInNo_Trans.Text + "','" + txtMemo_Trans.Text + "','" + txtTFrID_Trans.Text + "','" + txtTToID_Trans.Text + "','','','','','" + txtRemarks_Trans.Text + "','',@USERID,'')";
                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                        comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                        //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                        comm.Parameters.AddWithValue("@USERID", userName);
                        conn.Open();
                        comm.ExecuteNonQuery();
                        conn.Close();


                        Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM_T.Text + "'", lblTCatID);

                        DropDownList ddlType_T = (DropDownList)gv_Transfer.FooterRow.FindControl("ddlType_T");
                        TextBox txtCPQTY_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtCPQTY_T");
                        if (txtCPQTY_T.Text == "")
                        {
                            txtCPQTY_T.Text = "0";
                        }
                        else
                            txtCPQTY_T.Text = txtCPQTY_T.Text;
                        decimal cpQty = 0;
                        cpQty = Convert.ToDecimal(txtCPQTY_T.Text);
                        TextBox txtCQty_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtCQty_T");
                        if (txtCQty_T.Text == "")
                        {
                            txtCQty_T.Text = "0";
                        }
                        else
                            txtCQty_T.Text = txtCQty_T.Text;
                        decimal cQty = 0;
                        cQty = Convert.ToDecimal(txtCQty_T.Text);
                        decimal pQty = 0;
                        if (txtPQty_T.Text == "")
                        {
                            txtPQty_T.Text = "0";
                        }
                        else
                            txtPQty_T.Text = txtPQty_T.Text;
                        pQty = Convert.ToDecimal(txtPQty_T.Text);

                        decimal Qty = 0;
                        if (txtQty_T.Text == "")
                        {
                            txtQty_T.Text = "0";
                        }
                        else
                            txtQty_T.Text = txtQty_T.Text;
                        Qty = Convert.ToDecimal(txtQty_T.Text);
                        TextBox txtRate_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtRate_T");
                        decimal Rate = 0;
                        Rate = Convert.ToDecimal(txtRate_T.Text);
                        TextBox txtAmount_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtAmount_T");
                        decimal Amount = 0;
                        Amount = Convert.ToDecimal(txtAmount_T.Text);

                        Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'ITRF' and TRANSMY = '" + lblTMy.Text + "'", lblTTransSl);
                        string ItemCD;
                        string mxCD = "";
                        string mid = "";
                        string subItemCD = "";
                        int subCD, incrItCD;
                        if (lblTTransSl.Text == "")
                        {
                            ItemCD = "00000001";
                        }
                        else
                        {
                            mxCD = lblTTransSl.Text;
                            //OItemCD = mxCD.Substring(4,4);
                            subCD = int.Parse(mxCD);
                            incrItCD = subCD + 1;
                            if (incrItCD < 10)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000000" + mid;
                            }
                            else if (incrItCD < 100)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000000" + mid;
                            }
                            else if (incrItCD < 1000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00000" + mid;
                            }
                            else if (incrItCD < 10000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000" + mid;
                            }
                            else if (incrItCD < 100000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000" + mid;
                            }
                            else if (incrItCD < 1000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00" + mid;
                            }
                            else if (incrItCD < 10000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0" + mid;
                            }
                            //else
                            //    subItemCD = incrItCD.ToString();

                            ItemCD = subItemCD;
                        }

                        query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, USERPC, USERID, IPADDRESS ) " +
                                 "values(@TRANSTP,@TRANSDT,'" + lblTMy.Text + "','" + txtInNo_Trans.Text + "','" + txtMemo_Trans.Text + "','" + txtTFrID_Trans.Text + "','" + txtTToID_Trans.Text + "','','','','','" + txtRemarks_Trans.Text + "',@TRANSSL,'" + lblTCatID.Text + "','" + txtItemID_T.Text + "', " +
                                 " '" + ddlType_T.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ",'',@USERID,'')");

                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                        comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                        //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                        comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                        comm.Parameters.AddWithValue("@USERID", userName);

                        conn.Open();
                        int result = comm.ExecuteNonQuery();
                        conn.Close();

                        ///////Refresh/////
                        txtMemo_Trans.Text = "";
                        txtTFr_Trans.Text = "";
                        txtTFrID_Trans.Text = "";
                        //ddlPLCType.SelectedIndex = -1;
                        //txtPLCID.Text = "";
                        //txtPLCDT.Text = "";
                        txtTTo_Trans.Text = "";
                        txtTToID_Trans.Text = "";
                        txtRemarks_Trans.Text = "";
                        txtItemID_T.Text = "";
                        txtItemNM_T.Text = "";
                        //ddlType_P.SelectedIndex = -1;
                        txtCPQTY_T.Text = "";
                        txtCQty_T.Text = "";
                        txtPQty_T.Text = "";
                        txtQty_T.Text = "";
                        txtRate_T.Text = ".00";
                        txtAmount_T.Text = ".00";
                        txtTTotalAmount.Text = "";


                        DateTime transdate = DateTime.Parse(txtInDT_Trans.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        string TrDate = transdate.ToString("yyyy/MM/dd");

                        Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblTMy.Text + "' and TRANSTP = 'ITRF'", lblTMxNo);
                        if (lblTMxNo.Text == "")
                        {
                            txtInNo_Trans.Text = "1";
                        }
                        else
                        {
                            int iNo = int.Parse(lblTMxNo.Text);
                            int totIno = iNo + 1;
                            txtInNo_Trans.Text = totIno.ToString();
                        }

                        GridShow_Transfer();
                        txtMemo_Trans.Focus();

                    }
                }
            }

            /////////////////////////////////// For Print ///////////////////////////////////

            else if (e.CommandName.Equals("SavePrint"))
            {
                Session["InvDate_T"] = "";
                Session["InvNo_T"] = "";
                Session["Memo_T"] = "";
                Session["StoreNM_T"] = "";
                Session["StoreID_T"] = "";
                Session["PartyNM_T"] = "";
                Session["PartyID_T"] = "";

                Session["InvNoEdit_T"] = "";

                Session["InvDate_T"] = txtInDT_Trans.Text;
                Session["InvNo_T"] = txtInNo_Trans.Text;
                Session["Memo_T"] = txtMemo_Trans.Text;
                Session["StoreNM_T"] = txtTFr_Trans.Text;
                Session["StoreID_T"] = txtTFrID_Trans.Text;
                Session["PartyNM_T"] = txtTTo_Trans.Text;
                Session["PartyID_T"] = txtTToID_Trans.Text;

                TextBox txtItemID_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtItemID_T");
                TextBox txtItemNM_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtItemNM_T");
                TextBox txtQty_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtQty_T");
                TextBox txtPQty_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtPQty_T");
                if (txtTFrID_Trans.Text == "")
                {
                    lblFromMsg_Trans.Visible = true;
                    lblFromMsg_Trans.Text = "Select Store.";
                    txtTFr_Trans.Focus();
                    lblMsgTo_Trans.Visible = false;
                    lblTGridMSG.Visible = false;
                }
                else if (txtTToID_Trans.Text == "")
                {
                    lblMsgTo_Trans.Visible = true;
                    lblMsgTo_Trans.Text = "Select Party";
                    txtTTo_Trans.Focus();
                    lblFromMsg_Trans.Visible = false;
                    lblTGridMSG.Visible = false;
                }
                else if (txtItemID_T.Text == "")
                {
                    lblTGridMSG.Visible = true;
                    lblTGridMSG.Text = "Select Item.";
                    txtItemNM_T.Focus();
                    lblFromMsg_Trans.Visible = false;
                    lblMsgTo_Trans.Visible = false;
                }
                else if (txtQty_T.Text == "0")
                {
                    lblTGridMSG.Visible = true;
                    lblTGridMSG.Text = "Quantity is Wrong.";
                    txtPQty_T.Focus();
                }
                else
                {
                    lblFromMsg_Trans.Visible = false;
                    lblMsgTo_Trans.Visible = false;
                    lblTGridMSG.Visible = false;

                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    if (btnTransferEdit.Text == "EDIT")
                    {
                        cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + txtInNo_Trans.Text + "' and TRANSTP='ITRF' AND TRANSMY='" + lblTMy.Text + "'", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + ddlInNoEdit_Trans.Text + "' and TRANSTP='ITRF' AND TRANSMY='" + lblTMy.Text + "'", conn);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    conn.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM_T.Text + "'", lblTCatID);

                        DropDownList ddlType_T = (DropDownList)gv_Transfer.FooterRow.FindControl("ddlType_T");
                        TextBox txtCPQTY_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtCPQTY_T");
                        if (txtCPQTY_T.Text == "")
                        {
                            txtCPQTY_T.Text = "0";
                        }
                        else
                            txtCPQTY_T.Text = txtCPQTY_T.Text;
                        decimal cpQty = 0;
                        cpQty = Convert.ToDecimal(txtCPQTY_T.Text);
                        TextBox txtCQty_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtCQty_T");
                        if (txtCQty_T.Text == "")
                        {
                            txtCQty_T.Text = "0";
                        }
                        else
                            txtCQty_T.Text = txtCQty_T.Text;
                        decimal cQty = 0;
                        cQty = Convert.ToDecimal(txtCQty_T.Text);
                        decimal pQty = 0;
                        if (txtPQty_T.Text == "")
                        {
                            txtPQty_T.Text = "0";
                        }
                        else
                            txtPQty_T.Text = txtPQty_T.Text;
                        pQty = Convert.ToDecimal(txtPQty_T.Text);

                        decimal Qty = 0;
                        if (txtQty_T.Text == "")
                        {
                            txtQty_T.Text = "0";
                        }
                        else
                            txtQty_T.Text = txtQty_T.Text;
                        Qty = Convert.ToDecimal(txtQty_T.Text);
                        TextBox txtRate_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtRate_T");
                        decimal Rate = 0;
                        Rate = Convert.ToDecimal(txtRate_T.Text);
                        TextBox txtAmount_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtAmount_T");
                        decimal Amount = 0;
                        Amount = Convert.ToDecimal(txtAmount_T.Text);

                        Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'ITRF' and TRANSMY = '" + lblTMy.Text + "'", lblTTransSl);
                        string ItemCD;
                        string mxCD = "";
                        string mid = "";
                        string subItemCD = "";
                        int subCD, incrItCD;
                        if (lblTTransSl.Text == "")
                        {
                            ItemCD = "00000001";
                        }
                        else
                        {
                            mxCD = lblTTransSl.Text;
                            //OItemCD = mxCD.Substring(4,4);
                            subCD = int.Parse(mxCD);
                            incrItCD = subCD + 1;
                            if (incrItCD < 10)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000000" + mid;
                            }
                            else if (incrItCD < 100)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000000" + mid;
                            }
                            else if (incrItCD < 1000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00000" + mid;
                            }
                            else if (incrItCD < 10000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000" + mid;
                            }
                            else if (incrItCD < 100000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000" + mid;
                            }
                            else if (incrItCD < 1000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00" + mid;
                            }
                            else if (incrItCD < 10000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0" + mid;
                            }
                            //else
                            //    subItemCD = incrItCD.ToString();

                            ItemCD = subItemCD;
                        }

                        if (btnTransferEdit.Text == "EDIT")
                        {
                            query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblTMy.Text + "','" + txtInNo_Trans.Text + "','" + txtMemo_Trans.Text + "','" + txtTFrID_Trans.Text + "','" + txtTToID_Trans.Text + "','','','','','" + txtRemarks_Trans.Text + "',@TRANSSL,'" + lblTCatID.Text + "','" + txtItemID_T.Text + "', " +
                                     " '" + ddlType_T.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ",'',@USERID,'')");

                            comm = new SqlCommand(query, conn);
                            comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                            comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                            //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                            comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                            comm.Parameters.AddWithValue("@USERID", userName);

                            conn.Open();
                            int result = comm.ExecuteNonQuery();
                            conn.Close();

                            ///////Refresh/////
                            txtMemo_Trans.Text = "";
                            txtTFr_Trans.Text = "";
                            txtTFrID_Trans.Text = "";
                            //ddlPLCType.SelectedIndex = -1;
                            //txtPLCID.Text = "";
                            //txtPLCDT.Text = "";
                            txtTTo_Trans.Text = "";
                            txtTToID_Trans.Text = "";
                            txtRemarks_Trans.Text = "";
                            txtItemID_T.Text = "";
                            txtItemNM_T.Text = "";
                            //ddlType_P.SelectedIndex = -1;
                            txtCPQTY_T.Text = "";
                            txtCQty_T.Text = "";
                            txtPQty_T.Text = "";
                            txtQty_T.Text = "";
                            txtRate_T.Text = ".00";
                            txtAmount_T.Text = ".00";
                            txtTTotalAmount.Text = "";


                            DateTime transdate = DateTime.Parse(txtInDT_Trans.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            string TrDate = transdate.ToString("yyyy/MM/dd");

                            Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblTMy.Text + "' and TRANSTP = 'ITRF'", lblTMxNo);
                            if (lblTMxNo.Text == "")
                            {
                                txtInNo_Trans.Text = "1";
                            }
                            else
                            {
                                int iNo = int.Parse(lblTMxNo.Text);
                                int totIno = iNo + 1;
                                txtInNo_Trans.Text = totIno.ToString();
                            }

                            GridShow_Transfer();
                            txtMemo_Trans.Focus();

                            ScriptManager.RegisterStartupScript(this,
                            this.GetType(), "OpenWindow", "window.open('../Report/Report/rptTransferMemo.aspx','_newtab');", true);

                        }

                        else
                        {
                            Session["InvNoEdit_T"] = ddlInNoEdit_Trans.Text;

                            Int64 TransferEdit = Convert.ToInt64(ddlInNoEdit_Trans.Text);
                            conn.Open();
                            SqlCommand cmd1 = new SqlCommand("select * from STK_TRANSMST where TRANSTP = 'ITRF' and TRANSMY='" + lblTMy.Text + "' and TRANSNO =" + TransferEdit + "", conn);
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                            DataSet ds1 = new DataSet();
                            da.Fill(ds1);
                            conn.Close();
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblTMy.Text + "','" + ddlInNoEdit_Trans.Text + "','" + txtMemo_Trans.Text + "','" + txtTFrID_Trans.Text + "','" + txtTToID_Trans.Text + "','','','','','" + txtRemarks_Trans.Text + "',@TRANSSL,'" + lblTCatID.Text + "','" + txtItemID_T.Text + "', " +
                                     " '" + ddlType_T.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ",'',@USERID,'')");

                                comm = new SqlCommand(query, conn);
                                comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                                comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                                //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                                comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                                comm.Parameters.AddWithValue("@USERID", userName);

                                conn.Open();
                                int result = comm.ExecuteNonQuery();
                                conn.Close();

                                ///////Refresh/////
                                ddlInNoEdit_Trans.SelectedIndex = -1;
                                txtMemo_Trans.Text = "";
                                txtTFr_Trans.Text = "";
                                txtTFrID_Trans.Text = "";
                                //ddlPLCType.SelectedIndex = -1;
                                //txtPLCID.Text = "";
                                //txtPLCDT.Text = "";
                                txtTTo_Trans.Text = "";
                                txtTToID_Trans.Text = "";
                                txtRemarks_Trans.Text = "";
                                txtItemID_T.Text = "";
                                txtItemNM_T.Text = "";
                                //ddlType_P.SelectedIndex = -1;
                                txtCPQTY_T.Text = "";
                                txtCQty_T.Text = "";
                                txtPQty_T.Text = "";
                                txtQty_T.Text = "";
                                txtRate_T.Text = ".00";
                                txtAmount_T.Text = ".00";
                                txtTTotalAmount.Text = "";

                                GridShowTransfer_Edit();
                                ddlInNoEdit_Trans.Focus();

                                ScriptManager.RegisterStartupScript(this,
                                this.GetType(), "OpenWindow", "window.open('../Report/Report/rptTransferMemoEdit.aspx','_newtab');", true);

                            }
                            else
                            {
                                lblPGridMSG.Visible = true;
                                lblPGridMSG.Text = "Must be in New Mode.";
                            }
                        }
                    }

                    else
                    {

                        query = "insert into STK_TRANSMST ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS, USERPC, USERID, IPADDRESS) " +
                                    "values(@TRANSTP,@TRANSDT,'" + lblTMy.Text + "','" + txtInNo_Trans.Text + "','" + txtMemo_Trans.Text + "','" + txtTFrID_Trans.Text + "','" + txtTToID_Trans.Text + "','','','','','" + txtRemarks_Trans.Text + "','',@USERID,'')";
                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                        comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                        //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                        comm.Parameters.AddWithValue("@USERID", userName);
                        conn.Open();
                        comm.ExecuteNonQuery();
                        conn.Close();


                        Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM_T.Text + "'", lblTCatID);

                        DropDownList ddlType_T = (DropDownList)gv_Transfer.FooterRow.FindControl("ddlType_T");
                        TextBox txtCPQTY_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtCPQTY_T");
                        if (txtCPQTY_T.Text == "")
                        {
                            txtCPQTY_T.Text = "0";
                        }
                        else
                            txtCPQTY_T.Text = txtCPQTY_T.Text;
                        decimal cpQty = 0;
                        cpQty = Convert.ToDecimal(txtCPQTY_T.Text);
                        TextBox txtCQty_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtCQty_T");
                        if (txtCQty_T.Text == "")
                        {
                            txtCQty_T.Text = "0";
                        }
                        else
                            txtCQty_T.Text = txtCQty_T.Text;
                        decimal cQty = 0;
                        cQty = Convert.ToDecimal(txtCQty_T.Text);
                        decimal pQty = 0;
                        if (txtPQty_T.Text == "")
                        {
                            txtPQty_T.Text = "0";
                        }
                        else
                            txtPQty_T.Text = txtPQty_T.Text;
                        pQty = Convert.ToDecimal(txtPQty_T.Text);

                        decimal Qty = 0;
                        if (txtQty_T.Text == "")
                        {
                            txtQty_T.Text = "0";
                        }
                        else
                            txtQty_T.Text = txtQty_T.Text;
                        Qty = Convert.ToDecimal(txtQty_T.Text);
                        TextBox txtRate_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtRate_T");
                        decimal Rate = 0;
                        Rate = Convert.ToDecimal(txtRate_T.Text);
                        TextBox txtAmount_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtAmount_T");
                        decimal Amount = 0;
                        Amount = Convert.ToDecimal(txtAmount_T.Text);

                        Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'ITRF' and TRANSMY = '" + lblTMy.Text + "'", lblTTransSl);
                        string ItemCD;
                        string mxCD = "";
                        string mid = "";
                        string subItemCD = "";
                        int subCD, incrItCD;
                        if (lblTTransSl.Text == "")
                        {
                            ItemCD = "00000001";
                        }
                        else
                        {
                            mxCD = lblTTransSl.Text;
                            //OItemCD = mxCD.Substring(4,4);
                            subCD = int.Parse(mxCD);
                            incrItCD = subCD + 1;
                            if (incrItCD < 10)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000000" + mid;
                            }
                            else if (incrItCD < 100)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000000" + mid;
                            }
                            else if (incrItCD < 1000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00000" + mid;
                            }
                            else if (incrItCD < 10000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0000" + mid;
                            }
                            else if (incrItCD < 100000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "000" + mid;
                            }
                            else if (incrItCD < 1000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "00" + mid;
                            }
                            else if (incrItCD < 10000000)
                            {
                                mid = incrItCD.ToString();
                                subItemCD = "0" + mid;
                            }
                            //else
                            //    subItemCD = incrItCD.ToString();

                            ItemCD = subItemCD;
                        }

                        query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, USERPC, USERID, IPADDRESS ) " +
                                 "values(@TRANSTP,@TRANSDT,'" + lblTMy.Text + "','" + txtInNo_Trans.Text + "','" + txtMemo_Trans.Text + "','" + txtTFrID_Trans.Text + "','" + txtTToID_Trans.Text + "','','','','','" + txtRemarks_Trans.Text + "',@TRANSSL,'" + lblTCatID.Text + "','" + txtItemID_T.Text + "', " +
                                 " '" + ddlType_T.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ",'',@USERID,'')");

                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                        comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                        //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                        comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                        comm.Parameters.AddWithValue("@USERID", userName);

                        conn.Open();
                        int result = comm.ExecuteNonQuery();
                        conn.Close();

                        ///////Refresh/////
                        txtMemo_Trans.Text = "";
                        txtTFr_Trans.Text = "";
                        txtTFrID_Trans.Text = "";
                        //ddlPLCType.SelectedIndex = -1;
                        //txtPLCID.Text = "";
                        //txtPLCDT.Text = "";
                        txtTTo_Trans.Text = "";
                        txtTToID_Trans.Text = "";
                        txtRemarks_Trans.Text = "";
                        txtItemID_T.Text = "";
                        txtItemNM_T.Text = "";
                        //ddlType_P.SelectedIndex = -1;
                        txtCPQTY_T.Text = "";
                        txtCQty_T.Text = "";
                        txtPQty_T.Text = "";
                        txtQty_T.Text = "";
                        txtRate_T.Text = ".00";
                        txtAmount_T.Text = ".00";
                        txtTTotalAmount.Text = "";


                        DateTime transdate = DateTime.Parse(txtInDT_Trans.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        string TrDate = transdate.ToString("yyyy/MM/dd");

                        Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblTMy.Text + "' and TRANSTP = 'ITRF'", lblTMxNo);
                        if (lblTMxNo.Text == "")
                        {
                            txtInNo_Trans.Text = "1";
                        }
                        else
                        {
                            int iNo = int.Parse(lblTMxNo.Text);
                            int totIno = iNo + 1;
                            txtInNo_Trans.Text = totIno.ToString();
                        }

                        GridShow_Transfer();
                        txtMemo_Trans.Focus();

                        ScriptManager.RegisterStartupScript(this,
                        this.GetType(), "OpenWindow", "window.open('../Report/Report/rptTransferMemo.aspx','_newtab');", true);

                    }
                }
            }
        }

        protected void gv_Transfer_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string userName = HttpContext.Current.Session["UserName"].ToString();

            if (btnTransferEdit.Text == "EDIT")
            {
                DateTime transdate = DateTime.Parse(txtInDT_Trans.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                Label lblTransSL_T = (Label)gv_Transfer.Rows[e.RowIndex].FindControl("lblTransSL_T");

                SqlCommand cmd1 = new SqlCommand("select * from STK_TRANS where TRANSTP = 'ITRF' and TRANSMY='" + lblTMy.Text + "' and TRANSNO ='" + txtInNo_Trans.Text + "' and TRANSDT = '" + TrDate + "' ", conn);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                conn.Close();

                if (ds1.Tables[0].Rows.Count > 1)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from STK_TRANS where TRANSTP = 'ITRF' and  TRANSDT = '" + TrDate + "' and TRANSSL = '" + lblTransSL_T.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from STK_TRANS where TRANSTP = 'ITRF' and TRANSNO ='" + txtInNo_Trans.Text + "' and  TRANSDT = '" + TrDate + "' and TRANSSL = '" + lblTransSL_T.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand("delete from STK_TRANSMST where TRANSTP = 'ITRF' and TRANSNO ='" + txtInNo_Trans.Text + "' and TRANSMY='" + lblTMy.Text + "' and  TRANSDT = '" + TrDate + "'", conn);
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                }

                gv_Transfer.EditIndex = -1;
                GridShow_Transfer();
            }
            else
            {
                DateTime transdate = DateTime.Parse(txtInDT_Trans.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                Label lblTransSL_T = (Label)gv_Transfer.Rows[e.RowIndex].FindControl("lblTransSL_T");

                SqlCommand cmd1 = new SqlCommand("select * from STK_TRANS where TRANSTP = 'ITRF' and TRANSMY='" + lblTMy.Text + "' and TRANSNO ='" + ddlInNoEdit_Trans.Text + "' and TRANSDT = '" + TrDate + "' ", conn);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                conn.Close();

                if (ds1.Tables[0].Rows.Count > 1)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from STK_TRANS where TRANSTP = 'ITRF' and  TRANSDT = '" + TrDate + "' and TRANSSL = '" + lblTransSL_T.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from STK_TRANS where TRANSTP = 'ITRF' and TRANSNO ='" + ddlInNoEdit_Trans.Text + "' and  TRANSDT = '" + TrDate + "' and TRANSSL = '" + lblTransSL_T.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand("delete from STK_TRANSMST where TRANSTP = 'ITRF' and TRANSNO ='" + ddlInNoEdit_Trans.Text + "' and TRANSMY='" + lblTMy.Text + "' and  TRANSDT = '" + TrDate + "'", conn);
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                }

                gv_Transfer.EditIndex = -1;
                GridShowTransfer_Edit();
            }
        }

        protected void gv_Transfer_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (btnTransferEdit.Text == "EDIT")
            {
                gv_Transfer.EditIndex = e.NewEditIndex;
                GridShow_Transfer();
            }
            else
            {
                gv_Transfer.EditIndex = e.NewEditIndex;
                GridShowTransfer_Edit();
            }

            TextBox txtItemNMEdit_T = (TextBox)gv_Transfer.Rows[e.NewEditIndex].FindControl("txtItemNMEdit_T");
            txtItemNMEdit_T.Focus();
        }

        protected void gv_Transfer_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string userName = HttpContext.Current.Session["UserName"].ToString();

            Label lblItemIDEdit_T = (Label)gv_Transfer.Rows[e.RowIndex].FindControl("lblItemIDEdit_T");
            TextBox txtItemNMEdit_T = (TextBox)gv_Transfer.Rows[e.RowIndex].FindControl("txtItemNMEdit_T");
            TextBox txtQtyEdit_T = (TextBox)gv_Transfer.Rows[e.RowIndex].FindControl("txtQtyEdit_T");
            TextBox txtPQtyEdit_T = (TextBox)gv_Transfer.Rows[e.RowIndex].FindControl("txtPQtyEdit_T");

            if (txtTFrID_Trans.Text == "")
            {
                lblFromMsg_Trans.Visible = true;
                lblFromMsg_Trans.Text = "Select Store.";
                txtTFr_Trans.Focus();
                lblMsgTo_Trans.Visible = false;
                lblTGridMSG.Visible = false;
            }
            else if (txtTToID_Trans.Text == "")
            {
                lblMsgTo_Trans.Visible = true;
                lblMsgTo_Trans.Text = "Select Party";
                txtTTo_Trans.Focus();
                lblFromMsg_Trans.Visible = false;
                lblTGridMSG.Visible = false;
            }
            else if (lblItemIDEdit_T.Text == "")
            {
                lblTGridMSG.Visible = true;
                lblTGridMSG.Text = "Select Item.";
                txtItemNMEdit_T.Focus();
                lblFromMsg_Trans.Visible = false;
                lblMsgTo_Trans.Visible = false;
            }
            else if (txtQtyEdit_T.Text == "0")
            {
                lblTGridMSG.Visible = true;
                lblTGridMSG.Text = "Quantity is Wrong.";
                txtPQtyEdit_T.Focus();
                lblFromMsg_Trans.Visible = false;
                lblMsgTo_Trans.Visible = false;
            }
            else
            {
                lblFromMsg_Trans.Visible = false;
                lblMsgTo_Trans.Visible = false;
                lblTGridMSG.Visible = false;

                Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNMEdit_T.Text + "'", lblTCatID);

                DropDownList ddltypeEdit_T = (DropDownList)gv_Transfer.Rows[e.RowIndex].FindControl("ddltypeEdit_T");
                TextBox txtCPQTYEdit_T = (TextBox)gv_Transfer.Rows[e.RowIndex].FindControl("txtCPQTYEdit_T");
                if (txtCPQTYEdit_T.Text == "")
                {
                    txtCPQTYEdit_T.Text = "0";
                }
                else
                    txtCPQTYEdit_T.Text = txtCPQTYEdit_T.Text;
                decimal cpQty = 0;
                cpQty = Convert.ToDecimal(txtCPQTYEdit_T.Text);
                TextBox txtCQtyEdit_T = (TextBox)gv_Transfer.Rows[e.RowIndex].FindControl("txtCQtyEdit_T");
                if (txtCQtyEdit_T.Text == "")
                {
                    txtCQtyEdit_T.Text = "0";
                }
                else
                    txtCQtyEdit_T.Text = txtCQtyEdit_T.Text;
                decimal cQty = 0;
                cQty = Convert.ToDecimal(txtCQtyEdit_T.Text);
                decimal pQty = 0;
                if (txtPQtyEdit_T.Text == "")
                {
                    txtPQtyEdit_T.Text = "0";
                }
                else
                    txtPQtyEdit_T.Text = txtPQtyEdit_T.Text;
                pQty = Convert.ToDecimal(txtPQtyEdit_T.Text);

                decimal Qty = 0;
                if (txtQtyEdit_T.Text == "")
                {
                    txtQtyEdit_T.Text = "0";
                }
                else
                    txtQtyEdit_T.Text = txtQtyEdit_T.Text;
                Qty = Convert.ToDecimal(txtQtyEdit_T.Text);
                TextBox txtRateEdit_T = (TextBox)gv_Transfer.Rows[e.RowIndex].FindControl("txtRateEdit_T");
                decimal Rate = 0;
                Rate = Convert.ToDecimal(txtRateEdit_T.Text);
                TextBox txtAmountEdit_T = (TextBox)gv_Transfer.Rows[e.RowIndex].FindControl("txtAmountEdit_T");
                decimal Amount = 0;
                Amount = Convert.ToDecimal(txtAmountEdit_T.Text);
                Label lblTransSLEdit_T = (Label)gv_Transfer.Rows[e.RowIndex].FindControl("lblTransSLEdit_T");

                DateTime transdate = DateTime.Parse(txtInDT_Trans.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                //DateTime LCDT = new DateTime();
                //string lcDate = "";
                //if (txtPLCDT.Text == "")
                //{
                //    lcDate = "";
                //}
                //else
                //{
                //    LCDT = DateTime.Parse(txtPLCDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                //    lcDate = LCDT.ToString("yyyy/MM/dd");
                //}

                if (btnTransferEdit.Text == "EDIT")
                {
                    Int64 TransNo = Convert.ToInt64(txtInNo_Trans.Text);

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update STK_TRANS set INVREFNO = '" + txtMemo_Trans.Text + "', STOREFR = '" + txtTFrID_Trans.Text + "', STORETO='" + txtTToID_Trans.Text + "', REMARKS = '" + txtRemarks_Trans.Text + "', CATID = '" + lblTCatID.Text + "', ITEMID = '" + lblItemIDEdit_T.Text + "', UNITTP = '" + ddltypeEdit_T.Text + "', " +
                          " CPQTY =" + cpQty + ", CQTY = " + cQty + ", PQTY = " + pQty + ", QTY = " + Qty + ", RATE = " + Rate + ", AMOUNT = " + Amount + ", USERID = '" + userName + "' where TRANSTP = 'ITRF' and TRANSMY='" + lblTMy.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + " and TRANSSL = '" + lblTransSLEdit_T.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();


                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set INVREFNO = '" + txtMemo_Trans.Text + "', STOREFR = '" + txtTFrID_Trans.Text + "', STORETO='" + txtTToID_Trans.Text + "', REMARKS = '" + txtRemarks_Trans.Text + "', USERID = '" + userName + "'" +
                          " where TRANSTP = 'ITRF' and TRANSMY='" + lblTMy.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                    cmd1.ExecuteNonQuery();
                    conn.Close();

                    gv_Transfer.EditIndex = -1;
                    GridShow_Transfer();
                }
                else
                {
                    Int64 TransNo = Convert.ToInt64(ddlInNoEdit_Trans.Text);

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update STK_TRANS set INVREFNO = '" + txtMemo_Trans.Text + "', STOREFR = '" + txtTFrID_Trans.Text + "', STORETO='" + txtTToID_Trans.Text + "', REMARKS = '" + txtRemarks_Trans.Text + "', CATID = '" + lblTCatID.Text + "', ITEMID = '" + lblItemIDEdit_T.Text + "', UNITTP = '" + ddltypeEdit_T.Text + "', " +
                          " CPQTY =" + cpQty + ", CQTY = " + cQty + ", PQTY = " + pQty + ", QTY = " + Qty + ", RATE = " + Rate + ", AMOUNT = " + Amount + ", USERID = '" + userName + "' where TRANSTP = 'ITRF' and TRANSMY='" + lblTMy.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + " and TRANSSL = '" + lblTransSLEdit_T.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();


                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set INVREFNO = '" + txtMemo_Trans.Text + "', STOREFR = '" + txtTFrID_Trans.Text + "', STORETO='" + txtTToID_Trans.Text + "', REMARKS = '" + txtRemarks_Trans.Text + "', USERID = '" + userName + "'" +
                          " where TRANSTP = 'ITRF' and TRANSMY='" + lblTMy.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                    cmd1.ExecuteNonQuery();
                    conn.Close();

                    gv_Transfer.EditIndex = -1;
                    GridShowTransfer_Edit();
                }
            }
        }

        protected void gv_Transfer_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnTransferEdit_Click(object sender, EventArgs e)
        {
            if (btnTransferEdit.Text == "EDIT")
            {
                DateTime transdate = DateTime.Parse(txtPInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                txtInNo_Trans.Visible = false;
                btnTransferEdit.Text = "NEW";
                btnTransferPrint.Visible = true;
                ddlInNoEdit_Trans.Visible = true;
                Global.dropDownAddWithSelect(ddlInNoEdit_Trans, "SELECT DISTINCT TRANSNO FROM STK_TRANS WHERE TRANSDT ='" + TrDate + "' AND TRANSMY='" + lblTMy.Text + "' and TRANSTP='ITRF'");
                ddlInNoEdit_Trans.SelectedIndex = -1;
                txtMemo_Trans.Text = "";
                txtTFr_Trans.Text = "";
                txtTFrID_Trans.Text = "";
                txtTTo_Trans.Text = "";
                txtTToID_Trans.Text = "";
                txtRemarks_Trans.Text = "";
                txtTTotalAmount.Text = "0.00";
                GridShowTransfer_Edit();
            }
            else
            {
                txtInNo_Trans.Visible = true;
                btnTransferEdit.Text = "EDIT";
                btnTransferPrint.Visible = false;
                ddlInNoEdit_Trans.Visible = false;
                ddlInNoEdit_Trans.SelectedIndex = -1;
                txtMemo_Trans.Text = "";
                txtTFr_Trans.Text = "";
                txtTFrID_Trans.Text = "";
                txtTTo_Trans.Text = "";
                txtTToID_Trans.Text = "";
                txtRemarks_Trans.Text = "";
                txtTTotalAmount.Text = "0.00";
                Transfer_Start();
            }
        }

        protected void btnTransferPrint_Click(object sender, EventArgs e)
        {
            if (txtInDT_Trans.Text == "" || ddlInNoEdit_Trans.Text == "Select")
            {
                lblTGridMSG.Visible = true;
                lblTGridMSG.Text = "Date & Invoice No.";
            }
            else
            {
                lblTGridMSG.Visible = false;

                Session["InvDate_T"] = txtInDT_Trans.Text;
                Session["InvNoEdit_T"] = ddlInNoEdit_Trans.Text;
                Session["Memo_T"] = txtMemo_Trans.Text;
                Session["StoreNM_T"] = txtTFr_Trans.Text;
                Session["StoreID_T"] = txtTFrID_Trans.Text;
                Session["PartyNM_T"] = txtTTo_Trans.Text;
                Session["PartyID_T"] = txtTToID_Trans.Text;

                ScriptManager.RegisterStartupScript(this,
                                    this.GetType(), "OpenWindow", "window.open('../Report/Report/rptTransferMemoEdit.aspx','_newtab');", true);
            }
        }

        public void GridShowTransfer_Edit()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            DateTime transdate = DateTime.Parse(txtInDT_Trans.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = transdate.ToString("yyyy/MM/dd");

            Int64 EditTransferTransNo = 0;
            if (ddlInNoEdit_Trans.Text == "Select")
            {
                EditTransferTransNo = 0;
            }
            else
                EditTransferTransNo = Convert.ToInt64(ddlInNoEdit_Trans.Text);

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT STK_ITEM.ITEMNM, STK_TRANS.* FROM STK_ITEM INNER JOIN STK_TRANS ON STK_ITEM.ITEMID = STK_TRANS.ITEMID where STK_TRANS.TRANSTP='ITRF' and STK_TRANS.TRANSDT = '" + TrDt + "' and STK_TRANS.TRANSMY = '" + lblTMy.Text + "' and STK_TRANS.TRANSNO = " + EditTransferTransNo + " order by TRANSSL", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_Transfer.DataSource = ds;
                gv_Transfer.DataBind();

                if (gv_Transfer.EditIndex == -1)
                {
                    Decimal totAmt = 0;
                    Decimal a = 0;
                    foreach (GridViewRow grid in gv_Transfer.Rows)
                    {
                        Label Per = (Label)grid.Cells[8].FindControl("lblAmount_T");
                        if (Per.Text == "")
                        {
                            Per.Text = "0";
                        }
                        else
                        {
                            Per.Text = Per.Text;
                        }
                        String Perf = Per.Text;
                        totAmt = Convert.ToDecimal(Perf);
                        a += totAmt;
                        txtTTotalAmount.Text = a.ToString();
                    }
                    a += totAmt;
                }
                else
                {

                }

                TextBox txtItemNM_T = (TextBox)gv_Transfer.FooterRow.FindControl("txtItemNM_T");
                txtItemNM_T.Focus();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_Transfer.DataSource = ds;
                gv_Transfer.DataBind();
                int columncount = gv_Transfer.Rows[0].Cells.Count;
                gv_Transfer.Rows[0].Cells.Clear();
                gv_Transfer.Rows[0].Cells.Add(new TableCell());
                gv_Transfer.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_Transfer.Rows[0].Cells[0].Text = "No Records Found";
                gv_Transfer.Rows[0].Visible = false;

                txtMemo_Trans.Text = "";
                txtTFr_Trans.Text = "";
                txtTFrID_Trans.Text = "";
                txtTTo_Trans.Text = "";
                txtTToID_Trans.Text = "";
                txtRemarks_Trans.Text = "";
                ddlInNoEdit_Trans.SelectedIndex = -1;
            }
        }

        protected void txtInDT_Trans_TextChanged(object sender, EventArgs e)
        {
            if (btnTransferEdit.Text == "EDIT")
            {
                DateTime transdate = DateTime.Parse(txtInDT_Trans.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                string varYear = transdate.ToString("yyyy");
                string month = transdate.ToString("MMM").ToUpper();
                string years = transdate.ToString("yy");
                //lblTMy.Text = month + "-" + years;
                lblTMy.Text = varYear;
                Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblTMy.Text + "' and TRANSTP = 'ITRF'", lblTMxNo);
                if (lblTMxNo.Text == "")
                {
                    txtInNo_Trans.Text = "1";
                }
                else
                {
                    int iNo = int.Parse(lblTMxNo.Text);
                    int totIno = iNo + 1;
                    txtInNo_Trans.Text = totIno.ToString();
                }

                txtMemo_Trans.Focus();
            }
            else
            {
                DateTime transdate = DateTime.Parse(txtInDT_Trans.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                string varYear = transdate.ToString("yyyy");
                string month = transdate.ToString("MMM").ToUpper();
                string years = transdate.ToString("yy");
                //lblTMy.Text = month + "-" + years;
                lblTMy.Text = varYear;

                Global.dropDownAddWithSelect(ddlInNoEdit_Trans, "SELECT DISTINCT TRANSNO FROM STK_TRANS WHERE TRANSDT ='" + TrDate + "' AND TRANSMY='" + lblTMy.Text + "' and TRANSTP='ITRF'");
                ddlInNoEdit_Trans.Focus();
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionTList(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT ITEMNM FROM STK_ITEM WHERE ITEMNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ITEMNM"].ToString());
            return CompletionSet.ToArray();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListTEdit(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT ITEMNM FROM STK_ITEM WHERE ITEMNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ITEMNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtInNo_Trans_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlInNoEdit_Trans_TextChanged(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            DateTime transdate = DateTime.Parse(txtInDT_Trans.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = transdate.ToString("yyyy/MM/dd");

            if (ddlInNoEdit_Trans.Text == "Select")
            {
                gv_Transfer.Visible = false;
                lblTGridMSG.Visible = true;
                lblTGridMSG.Text = "Type Invoice No.";
                txtTTotalAmount.Text = "";
            }
            else
            {
                gv_Transfer.Visible = true;
                lblTGridMSG.Visible = false;
                Int64 TransNo = Convert.ToInt64(ddlInNoEdit_Trans.Text);

                Global.txtAdd(@"select INVREFNO from STK_TRANSMST where TRANSTP='ITRF' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblTMy.Text + "' and TRANSNO =" + TransNo + "", txtMemo_Trans);
                Global.txtAdd(@"select STOREFR from STK_TRANSMST where TRANSTP='ITRF' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblTMy.Text + "' and TRANSNO =" + TransNo + "", txtTFrID_Trans);
                Global.txtAdd(@"select STORENM from STK_STORE where STOREID ='" + txtTFrID_Trans.Text + "'", txtTFr_Trans);
                Global.txtAdd(@"select STORETO from STK_TRANSMST where TRANSTP='ITRF' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblTMy.Text + "' and TRANSNO =" + TransNo + "", txtTToID_Trans);
                Global.txtAdd(@"select STORENM from STK_STORE where STOREID ='" + txtTToID_Trans.Text + "'", txtTTo_Trans);

                Global.txtAdd(@"select REMARKS from STK_TRANSMST where TRANSTP='ITRF' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblTMy.Text + "' and TRANSNO =" + TransNo + "", txtRemarks_Trans);
                GridShowTransfer_Edit();
            }
        }

        protected void txtItemNM_T_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            //NamingContainer return the container that the control sits in
            TextBox txtItemNM_T = (TextBox)row.FindControl("txtItemNM_T");
            TextBox txtItemID_T = (TextBox)row.FindControl("txtItemID_T");
            txtItemID_T.ReadOnly = true;
            Global.txtAdd(@"Select ITEMID from STK_ITEM where ITEMNM = '" + txtItemNM_T.Text + "'", txtItemID_T);
            TextBox txtCPQTY_T = (TextBox)row.FindControl("txtCPQTY_T");
            Global.txtAdd(@"Select PQTY from STK_ITEM where ITEMNM = '" + txtItemNM_T.Text + "'", txtCPQTY_T);
            txtCPQTY_T.ReadOnly = true;
            TextBox txtCQty_T = (TextBox)row.FindControl("txtCQty_T");
            txtCQty_T.Text = "0";
            TextBox txtPQty_T = (TextBox)row.FindControl("txtPQty_T");
            TextBox txtQty_T = (TextBox)row.FindControl("txtQty_T");
            txtQty_T.Text = "0";
            txtPQty_T.Text = "0";
            TextBox txtRate_T = (TextBox)row.FindControl("txtRate_T");
            Global.txtAdd(@"Select BUYRT from STK_ITEM where ITEMNM = '" + txtItemNM_T.Text + "'", txtRate_T);
            TextBox txtAmount_T = (TextBox)row.FindControl("txtAmount_T");
            txtAmount_T.Text = ".00";
            txtAmount_T.ReadOnly = true;
            DropDownList ddlType_T = (DropDownList)row.FindControl("ddlType_T");
            ddlType_T.Focus();
        }

        protected void txtItemNMEdit_T_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            //NamingContainer return the container that the control sits in
            TextBox txtItemNMEdit_T = (TextBox)row.FindControl("txtItemNMEdit_T");
            Label lblItemIDEdit_T = (Label)row.FindControl("lblItemIDEdit_T");
            Global.lblAdd(@"Select ITEMID from STK_ITEM where ITEMNM = '" + txtItemNMEdit_T.Text + "'", lblItemIDEdit_T);
            TextBox txtCPQTYEdit_T = (TextBox)row.FindControl("txtCPQTYEdit_T");
            Global.txtAdd(@"Select PQTY from STK_ITEM where ITEMNM = '" + txtItemNMEdit_T.Text + "'", txtCPQTYEdit_T);
            txtCPQTYEdit_T.ReadOnly = true;
            TextBox txtCQtyEdit_T = (TextBox)row.FindControl("txtCQtyEdit_T");
            txtCQtyEdit_T.Text = "0";
            TextBox txtPQtyEdit_T = (TextBox)row.FindControl("txtPQtyEdit_T");
            TextBox txtQtyEdit_T = (TextBox)row.FindControl("txtQtyEdit_T");
            txtQtyEdit_T.Text = "0";
            txtPQtyEdit_T.Text = "0";
            TextBox txtRateEdit_T = (TextBox)row.FindControl("txtRateEdit_T");
            Global.txtAdd(@"Select BUYRT from STK_ITEM where ITEMNM = '" + txtItemNMEdit_T.Text + "'", txtRateEdit_T);
            TextBox txtAmountEdit_T = (TextBox)row.FindControl("txtAmountEdit_T");
            txtAmountEdit_T.Text = ".00";
            txtAmountEdit_T.ReadOnly = true;
            DropDownList ddltypeEdit_T = (DropDownList)row.FindControl("ddltypeEdit_T");
            ddltypeEdit_T.Focus();
        }

        protected void ddlType_T_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddlType_T = (DropDownList)row.FindControl("ddlType_T");
            if (ddlType_T.Text == "CARTON")
            {
                TextBox txtCQty_T = (TextBox)row.FindControl("txtCQty_T");
                txtCQty_T.Focus();
            }
            else
            {
                TextBox txtPQty_T = (TextBox)row.FindControl("txtPQty_T");
                txtPQty_T.Focus();
                TextBox txtCQty_T = (TextBox)row.FindControl("txtCQty_T");
                txtCQty_T.Text = "0";
            }
        }

        protected void ddltypeEdit_T_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddltypeEdit_T = (DropDownList)row.FindControl("ddltypeEdit_T");
            if (ddltypeEdit_T.Text == "CARTON")
            {
                TextBox txtCQtyEdit_T = (TextBox)row.FindControl("txtCQtyEdit_T");
                txtCQtyEdit_T.Focus();
            }
            else
            {
                TextBox txtPQtyEdit_T = (TextBox)row.FindControl("txtPQtyEdit_T");
                txtPQtyEdit_T.Focus();
                TextBox txtCQtyEdit_T = (TextBox)row.FindControl("txtCQtyEdit_T");
                txtCQtyEdit_T.Text = "0";
            }
        }

        protected void txtCQty_T_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCPQTY_T = (TextBox)row.FindControl("txtCPQTY_T");
            Int64 CPQty = Convert.ToInt64(txtCPQTY_T.Text);
            TextBox txtCQty_T = (TextBox)row.FindControl("txtCQty_T");
            Int64 CQty = Convert.ToInt64(txtCQty_T.Text);
            TextBox txtPQty_T = (TextBox)row.FindControl("txtPQty_T");
            Int64 PQty;
            if (txtPQty_T.Text == "")
            {
                PQty = 0;
            }
            else
                PQty = Convert.ToInt64(txtPQty_T.Text);

            Int64 Qty = CPQty * CQty + PQty;
            TextBox txtQty_T = (TextBox)row.FindControl("txtQty_T");
            txtQty_T.Text = Qty.ToString();
            txtQty_T.ReadOnly = true;

            TextBox txtRate_P = (TextBox)row.FindControl("txtRate_T");
            decimal Rate = Convert.ToDecimal(txtRate_P.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmount_T = (TextBox)row.FindControl("txtAmount_T");
            txtAmount_T.Text = Amount.ToString();
            txtAmount_T.ReadOnly = true;
            txtPQty_T.Focus();
        }

        protected void txtCQtyEdit_T_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCPQTYEdit_T = (TextBox)row.FindControl("txtCPQTYEdit_T");
            Int64 CPQty = Convert.ToInt64(txtCPQTYEdit_T.Text);
            TextBox txtCQtyEdit_T = (TextBox)row.FindControl("txtCQtyEdit_T");
            Int64 CQty = Convert.ToInt64(txtCQtyEdit_T.Text);
            TextBox txtPQtyEdit_T = (TextBox)row.FindControl("txtPQtyEdit_T");
            Int64 PQty;
            if (txtPQtyEdit_T.Text == "")
            {
                PQty = 0;
            }
            else
                PQty = Convert.ToInt64(txtPQtyEdit_T.Text);

            Int64 Qty = CPQty * CQty + PQty;
            TextBox txtQtyEdit_T = (TextBox)row.FindControl("txtQtyEdit_T");
            txtQtyEdit_T.Text = Qty.ToString();
            txtQtyEdit_T.ReadOnly = true;

            TextBox txtRateEdit_T = (TextBox)row.FindControl("txtRateEdit_T");
            decimal Rate = Convert.ToDecimal(txtRateEdit_T.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmountEdit_T = (TextBox)row.FindControl("txtAmountEdit_T");
            txtAmountEdit_T.Text = Amount.ToString();
            txtAmountEdit_T.ReadOnly = true;
            txtPQtyEdit_T.Focus();
        }

        protected void txtPQty_T_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCPQTY_T = (TextBox)row.FindControl("txtCPQTY_T");
            Int64 CPQty = Convert.ToInt64(txtCPQTY_T.Text);
            TextBox txtCQty_T = (TextBox)row.FindControl("txtCQty_T");
            Int64 CQty = Convert.ToInt64(txtCQty_T.Text);
            TextBox txtPQty_T = (TextBox)row.FindControl("txtPQty_T");
            Int64 PQty = Convert.ToInt64(txtPQty_T.Text);
            Int64 Qty = CPQty * CQty + PQty;
            TextBox txtQty_T = (TextBox)row.FindControl("txtQty_T");
            txtQty_T.Text = Qty.ToString();
            txtQty_T.ReadOnly = true;

            TextBox txtRate_T = (TextBox)row.FindControl("txtRate_T");
            decimal Rate = Convert.ToDecimal(txtRate_T.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmount_T = (TextBox)row.FindControl("txtAmount_T");
            txtAmount_T.Text = Amount.ToString();
            txtAmount_T.ReadOnly = true;

            txtRate_T.Focus();
            //ImageButton imgbtnPAdd = (ImageButton)row.FindControl("imgbtnPAdd");
            //imgbtnPAdd.Focus();
        }

        protected void txtPQtyEdit_T_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCPQTYEdit_T = (TextBox)row.FindControl("txtCPQTYEdit_T");
            Int64 CPQty = Convert.ToInt64(txtCPQTYEdit_T.Text);
            TextBox txtCQtyEdit_T = (TextBox)row.FindControl("txtCQtyEdit_T");
            Int64 CQty = Convert.ToInt64(txtCQtyEdit_T.Text);
            TextBox txtPQtyEdit_T = (TextBox)row.FindControl("txtPQtyEdit_T");
            Int64 PQty = Convert.ToInt64(txtPQtyEdit_T.Text);
            Int64 Qty = CPQty * CQty + PQty;
            TextBox txtQtyEdit_T = (TextBox)row.FindControl("txtQtyEdit_T");
            txtQtyEdit_T.Text = Qty.ToString();
            txtQtyEdit_T.ReadOnly = true;

            TextBox txtRateEdit_T = (TextBox)row.FindControl("txtRateEdit_T");
            decimal Rate = Convert.ToDecimal(txtRateEdit_T.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmountEdit_T = (TextBox)row.FindControl("txtAmountEdit_T");
            txtAmountEdit_T.Text = Amount.ToString();
            txtAmountEdit_T.ReadOnly = true;

            txtRateEdit_T.Focus();
            //ImageButton imgbtnPUpdate = (ImageButton)row.FindControl("imgbtnPUpdate");
            //imgbtnPUpdate.Focus();
        }

        protected void txtRate_T_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

            TextBox txtQty_T = (TextBox)row.FindControl("txtQty_T");
            Int64 Qty = Convert.ToInt64(txtQty_T.Text);

            TextBox txtRate_T = (TextBox)row.FindControl("txtRate_T");
            decimal Rate = Convert.ToDecimal(txtRate_T.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmount_T = (TextBox)row.FindControl("txtAmount_T");
            txtAmount_T.Text = Amount.ToString();
            txtAmount_T.ReadOnly = true;

            ImageButton imgbtnTAdd = (ImageButton)row.FindControl("imgbtnTAdd");
            imgbtnTAdd.Focus();
        }

        protected void txtRateEdit_T_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

            TextBox txtQtyEdit_T = (TextBox)row.FindControl("txtQtyEdit_T");
            Int64 Qty = Convert.ToInt64(txtQtyEdit_T.Text);

            TextBox txtRateEdit_T = (TextBox)row.FindControl("txtRateEdit_T");
            decimal Rate = Convert.ToDecimal(txtRateEdit_T.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmountEdit_T = (TextBox)row.FindControl("txtAmountEdit_T");
            txtAmountEdit_T.Text = Amount.ToString();
            txtAmountEdit_T.ReadOnly = true;

            ImageButton imgbtnTUpdate = (ImageButton)row.FindControl("imgbtnTUpdate");
            imgbtnTUpdate.Focus();
        }

        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {

        }

        //////////////////////////////////////////////////////// END TRANSFER ///////////////////////////////////////////////


        /////////////////////////////////////////////////////// START RETURN ///////////////////////////////////////////////


        public void Return_Start()
        {
            //if (TabContainer1.ActiveTabIndex == 0)
            //{
            if (ddlRetType.Text == "IRTS")
            {
                lblMode.Text = "Return To";
                lblPSID_Ret.Text = "Party ID";
                T_Ret.Text = "Sale";
                DateTime today = DateTime.Today.Date;
                string td = Global.Dayformat(today);
                txtTInDt_Ret.Text = td;
                DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                string mon = DateTime.Today.Date.ToString("MMM").ToUpper();
                string year = today.ToString("yy");
                lblMy_Ret.Text = mon + "-" + year;
                Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANSMST where TRANSMY='" + lblMy_Ret.Text + "' and TRANSTP = 'IRTS'", lblMxNo_Ret);
                if (lblMxNo_Ret.Text == "")
                {
                    txtInNo_Ret.Text = "1";
                }
                else
                {
                    int iNo = int.Parse(lblMxNo_Ret.Text);
                    int totIno = iNo + 1;
                    txtInNo_Ret.Text = totIno.ToString();
                }

                txtSLMNo_Ret.Focus();
            }
            else
            {
                lblMode.Text = "Return From";
                lblPSID_Ret.Text = "Supplier ID";
                T_Ret.Text = "Purchase";
                DateTime today = DateTime.Today.Date;
                string td = Global.Dayformat(today);
                txtTInDt_Ret.Text = td;
                DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                string mon = DateTime.Today.Date.ToString("MMM").ToUpper();
                string year = today.ToString("yy");
                lblMy_Ret.Text = mon + "-" + year;
                Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANSMST where TRANSMY='" + lblMy_Ret.Text + "' and TRANSTP = 'IRTB'", lblMxNo_Ret);
                if (lblMxNo_Ret.Text == "")
                {
                    txtInNo_Ret.Text = "1";
                }
                else
                {
                    int iNo = int.Parse(lblMxNo_Ret.Text);
                    int totIno = iNo + 1;
                    txtInNo_Ret.Text = totIno.ToString();
                }

                txtSLMNo_Ret.Focus();
            }

            GridShow_Ret();
            Session["RetType"] = ddlRetType.Text;
            //}
        }

        protected void ddlRetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["RetType"] = "";
            //if(ddlRetType.Text=="IRTS")
            //    Session["RetType"] = ddlRetType.Text;
            //else
            //    Session["RetType"] = ddlRetType.Text;
            Return_Start();
            txtSaleFrom_Ret.Text = "";
            txtSlFr_Ret.Text = "";
            txtPSNM_Ret.Text = "";
            txtPID_Ret.Text = "";
            txtSLMNo_Ret.Text = "";
            txtRemarks_Ret.Text = "";
            txtTotAmt_Ret.Text = "0.00";
            txtGrossDisAmt_Ret.Text = "0.00";
            txtNetAmt_Ret.Text = "0.00";
            txtTAmount_Ret.Text = ".00";
            txtTDisAmount_Ret.Text = ".00";
            txtTotal_Ret.Text = ".00";
            if (btnEdit_Ret.Text == "NEW")
            {
                ddlSalesEditInNo_Ret.SelectedIndex = -1;
            }
        }

        protected void txtTInDt_Ret_TextChanged(object sender, EventArgs e)
        {
            if (btnEdit_Ret.Text == "EDIT")
            {
                if (ddlRetType.Text == "IRTS")
                {
                    DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string TrDate = transdate.ToString("yyyy/MM/dd");

                    string varYear = transdate.ToString("yyyy");
                    string month = transdate.ToString("MMM").ToUpper();
                    string years = transdate.ToString("yy");
                    //lblMy_Ret.Text = month + "-" + years;
                    lblMy_Ret.Text = varYear;
                    Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblMy_Ret.Text + "' and TRANSTP = 'IRTS'", lblMxNo_Ret);
                    if (lblMxNo_Ret.Text == "")
                    {
                        txtInNo_Ret.Text = "1";
                    }
                    else
                    {
                        int iNo = int.Parse(lblMxNo_Ret.Text);
                        int totIno = iNo + 1;
                        txtInNo_Ret.Text = totIno.ToString();
                    }

                    txtSLMNo_Ret.Focus();
                }
                else
                {
                    DateTime transdate = DateTime.Parse(txtInDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string TrDate = transdate.ToString("yyyy/MM/dd");

                    string varYear = transdate.ToString("yyyy");
                    string month = transdate.ToString("MMM").ToUpper();
                    string years = transdate.ToString("yy");
                    //lblMy_Ret.Text = month + "-" + years;
                    lblMy_Ret.Text = varYear;
                    Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblMy_Ret.Text + "' and TRANSTP = 'IRTB'", lblMxNo_Ret);
                    if (lblMxNo_Ret.Text == "")
                    {
                        txtInNo_Ret.Text = "1";
                    }
                    else
                    {
                        int iNo = int.Parse(lblMxNo_Ret.Text);
                        int totIno = iNo + 1;
                        txtInNo_Ret.Text = totIno.ToString();
                    }

                    txtSLMNo_Ret.Focus();
                }
            }

            else
            {
                if (ddlRetType.Text == "IRTS")
                {
                    DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string TrDate = transdate.ToString("yyyy/MM/dd");

                    string varYear = transdate.ToString("yyyy");
                    string month = transdate.ToString("MMM").ToUpper();
                    string years = transdate.ToString("yy");
                    //lblMy_Ret.Text = month + "-" + years;
                    lblMy_Ret.Text = varYear;

                    Global.dropDownAddWithSelect(ddlSalesEditInNo_Ret, "SELECT DISTINCT TRANSNO FROM STK_TRANS WHERE TRANSDT ='" + TrDate + "' AND TRANSMY='" + lblMy_Ret.Text + "' and TRANSTP='IRTS'");
                    ddlSalesEditInNo_Ret.Focus();
                }
                else  //////////purchase
                {
                    DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string TrDate = transdate.ToString("yyyy/MM/dd");

                    string varYear = transdate.ToString("yyyy");
                    string month = transdate.ToString("MMM").ToUpper();
                    string years = transdate.ToString("yy");
                    //lblMy_Ret.Text = month + "-" + years;
                    lblMy_Ret.Text = varYear;

                    Global.dropDownAddWithSelect(ddlSalesEditInNo_Ret, "SELECT DISTINCT TRANSNO FROM STK_TRANS WHERE TRANSDT ='" + TrDate + "' AND TRANSMY='" + lblMy_Ret.Text + "' and TRANSTP='IRTB'");
                    ddlSalesEditInNo_Ret.Focus();
                }
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListStore_Ret(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT STORENM FROM STK_STORE WHERE STORENM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["STORENM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtSaleFrom_Ret_TextChanged(object sender, EventArgs e)
        {
            Global.txtAdd(@"Select STOREID from STK_STORE where STORENM = '" + txtSaleFrom_Ret.Text + "'", txtSlFr_Ret);
            txtPSNM_Ret.Focus();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListParty_Ret(string prefixText, int count, string contextKey)
        {
            string retType = HttpContext.Current.Session["RetType"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);
            if (retType == "IRTS")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,5) IN ('10202') and ACCOUNTNM LIKE '" + prefixText + "%' AND STATUSCD = 'P'");
            }
            else if (retType == "IRTB")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,5) IN ('20202') and ACCOUNTNM LIKE '" + prefixText + "%' AND STATUSCD = 'P'");
            }
            else
                retType = "";

            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtPSNM_Ret_TextChanged(object sender, EventArgs e)
        {
            Global.txtAdd(@"select ACCOUNTCD from GL_ACCHART where STATUSCD='P' and ACCOUNTNM='" + txtPSNM_Ret.Text + "'", txtPID_Ret);
            txtRemarks_Ret.Focus();
        }

        protected void txtRemarks_Ret_TextChanged(object sender, EventArgs e)
        {
            TextBox txtItemNM_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtItemNM_Ret");
            txtItemNM_Ret.Focus();
        }

        protected void GridShow_Ret()
        {
            if (ddlRetType.Text == "IRTS")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDt = transdate.ToString("yyyy/MM/dd");

                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT STK_ITEM.ITEMNM, STK_TRANS.* FROM STK_ITEM INNER JOIN STK_TRANS ON STK_ITEM.ITEMID = STK_TRANS.ITEMID where STK_TRANS.TRANSTP='IRTS' and STK_TRANS.TRANSDT = '" + TrDt + "' and STK_TRANS.TRANSMY = '" + lblMy_Ret.Text + "' and STK_TRANS.TRANSNO = '" + txtInNo_Ret.Text + "' order by TRANSSL", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDetail_Ret.DataSource = ds;
                    gvDetail_Ret.DataBind();

                    if (gvDetail_Ret.EditIndex == -1)
                    {
                        Decimal totAmt = 0;
                        Decimal a = 0;
                        decimal dis = 0;
                        decimal disAmt = 0;
                        decimal net = 0;
                        decimal netAmt = 0;
                        foreach (GridViewRow grid in gvDetail_Ret.Rows)
                        {
                            Label Per = (Label)grid.Cells[8].FindControl("lblAmount_Ret");
                            Label lblDisAmt_Ret = (Label)grid.Cells[10].FindControl("lblDisAmt_Ret");
                            Label lblNetAmt_Ret = (Label)grid.Cells[11].FindControl("lblNetAmt_Ret");
                            if (Per.Text == "")
                            {
                                Per.Text = "0.00";
                            }
                            else
                            {
                                Per.Text = Per.Text;
                            }
                            String Perf = Per.Text;
                            totAmt = Convert.ToDecimal(Perf);
                            a += totAmt;
                            string tAmount = SpellAmount.comma(a);
                            txtTAmount_Ret.Text = tAmount;

                            if (lblDisAmt_Ret.Text == "")
                            {
                                lblDisAmt_Ret.Text = "0.00";
                            }
                            else
                                lblDisAmt_Ret.Text = lblDisAmt_Ret.Text;

                            dis = Convert.ToDecimal(lblDisAmt_Ret.Text);
                            disAmt += dis;
                            string disCount = SpellAmount.comma(disAmt);
                            txtTDisAmount_Ret.Text = disCount;

                            if (lblNetAmt_Ret.Text == "")
                            {
                                lblNetAmt_Ret.Text = "0.00";
                            }
                            else
                                lblNetAmt_Ret.Text = lblNetAmt_Ret.Text;

                            net = Convert.ToDecimal(lblNetAmt_Ret.Text);
                            netAmt += net;
                            string nAmount = SpellAmount.comma(netAmt);
                            txtTotal_Ret.Text = nAmount;

                            //txtTotAmt.Text = nAmount;
                            //txtGrossDisAmt.Text = "0.00";
                            //txtNetAmt.Text = nAmount;
                        }
                        a += totAmt;
                        disAmt += dis;
                        netAmt += net;
                    }
                    else
                    {

                    }

                    //Global.txtAdd("SELECT MAX(TRANSNO) AS TRANSNO FROM STK_TRANSMST WHERE TRANSMY='" + lblSMY.Text + "' AND TRANSTP='SALE'",txtInNo);
                    TextBox txtItemNM_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtItemNM_Ret");
                    txtItemNM_Ret.Focus();
                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    gvDetail_Ret.DataSource = ds;
                    gvDetail_Ret.DataBind();
                    int columncount = gvDetail_Ret.Rows[0].Cells.Count;
                    gvDetail_Ret.Rows[0].Cells.Clear();
                    gvDetail_Ret.Rows[0].Cells.Add(new TableCell());
                    gvDetail_Ret.Rows[0].Cells[0].ColumnSpan = columncount;
                    gvDetail_Ret.Rows[0].Cells[0].Text = "No Records Found";
                    gvDetail_Ret.Rows[0].Visible = false;
                }
            }
            else //////////// for Purchase Return
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDt = transdate.ToString("yyyy/MM/dd");

                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT STK_ITEM.ITEMNM, STK_TRANS.* FROM STK_ITEM INNER JOIN STK_TRANS ON STK_ITEM.ITEMID = STK_TRANS.ITEMID where STK_TRANS.TRANSTP='IRTB' and STK_TRANS.TRANSDT = '" + TrDt + "' and STK_TRANS.TRANSMY = '" + lblMy_Ret.Text + "' and STK_TRANS.TRANSNO = '" + txtInNo_Ret.Text + "' order by TRANSSL", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDetail_Ret.DataSource = ds;
                    gvDetail_Ret.DataBind();

                    if (gvDetail_Ret.EditIndex == -1)
                    {
                        Decimal totAmt = 0;
                        Decimal a = 0;
                        decimal dis = 0;
                        decimal disAmt = 0;
                        decimal net = 0;
                        decimal netAmt = 0;
                        foreach (GridViewRow grid in gvDetail_Ret.Rows)
                        {
                            Label Per = (Label)grid.Cells[8].FindControl("lblAmount_Ret");
                            Label lblDisAmt_Ret = (Label)grid.Cells[10].FindControl("lblDisAmt_Ret");
                            Label lblNetAmt_Ret = (Label)grid.Cells[11].FindControl("lblNetAmt_Ret");
                            if (Per.Text == "")
                            {
                                Per.Text = "0.00";
                            }
                            else
                            {
                                Per.Text = Per.Text;
                            }
                            String Perf = Per.Text;
                            totAmt = Convert.ToDecimal(Perf);
                            a += totAmt;
                            string tAmount = SpellAmount.comma(a);
                            txtTAmount_Ret.Text = tAmount;

                            if (lblDisAmt_Ret.Text == "")
                            {
                                lblDisAmt_Ret.Text = "0.00";
                            }
                            else
                                lblDisAmt_Ret.Text = lblDisAmt_Ret.Text;

                            dis = Convert.ToDecimal(lblDisAmt_Ret.Text);
                            disAmt += dis;
                            string disCount = SpellAmount.comma(disAmt);
                            txtTDisAmount_Ret.Text = disCount;

                            if (lblNetAmt_Ret.Text == "")
                            {
                                lblNetAmt_Ret.Text = "0.00";
                            }
                            else
                                lblNetAmt_Ret.Text = lblNetAmt_Ret.Text;

                            net = Convert.ToDecimal(lblNetAmt_Ret.Text);
                            netAmt += net;
                            string nAmount = SpellAmount.comma(netAmt);
                            txtTotal_Ret.Text = nAmount;

                            //txtTotAmt.Text = nAmount;
                            //txtGrossDisAmt.Text = "0.00";
                            //txtNetAmt.Text = nAmount;
                        }
                        a += totAmt;
                        disAmt += dis;
                        netAmt += net;
                    }
                    else
                    {

                    }

                    //Global.txtAdd("SELECT MAX(TRANSNO) AS TRANSNO FROM STK_TRANSMST WHERE TRANSMY='" + lblSMY.Text + "' AND TRANSTP='SALE'",txtInNo);
                    TextBox txtItemNM_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtItemNM_Ret");
                    txtItemNM_Ret.Focus();
                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    gvDetail_Ret.DataSource = ds;
                    gvDetail_Ret.DataBind();
                    int columncount = gvDetail_Ret.Rows[0].Cells.Count;
                    gvDetail_Ret.Rows[0].Cells.Clear();
                    gvDetail_Ret.Rows[0].Cells.Add(new TableCell());
                    gvDetail_Ret.Rows[0].Cells[0].ColumnSpan = columncount;
                    gvDetail_Ret.Rows[0].Cells[0].Text = "No Records Found";
                    gvDetail_Ret.Rows[0].Visible = false;
                }
            }
        }

        protected void GridShow_Ret_Complete()
        {
            if (ddlRetType.Text == "IRTS")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDt = transdate.ToString("yyyy/MM/dd");

                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT STK_ITEM.ITEMNM, STK_TRANS.* FROM STK_ITEM INNER JOIN STK_TRANS ON STK_ITEM.ITEMID = STK_TRANS.ITEMID where STK_TRANS.TRANSTP='IRTS' and STK_TRANS.TRANSDT = '" + TrDt + "' and STK_TRANS.TRANSMY = '" + lblMy_Ret.Text + "' and STK_TRANS.TRANSNO = '" + txtInNo_Ret.Text + "' order by TRANSSL", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDetail_Ret.DataSource = ds;
                    gvDetail_Ret.DataBind();
                    gvDetail_Ret.FooterRow.Visible = false;
                }
                else
                {

                }
            }
            else       ////////////   for purchase
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDt = transdate.ToString("yyyy/MM/dd");

                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT STK_ITEM.ITEMNM, STK_TRANS.* FROM STK_ITEM INNER JOIN STK_TRANS ON STK_ITEM.ITEMID = STK_TRANS.ITEMID where STK_TRANS.TRANSTP='IRTB' and STK_TRANS.TRANSDT = '" + TrDt + "' and STK_TRANS.TRANSMY = '" + lblMy_Ret.Text + "' and STK_TRANS.TRANSNO = '" + txtInNo_Ret.Text + "' order by TRANSSL", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDetail_Ret.DataSource = ds;
                    gvDetail_Ret.DataBind();
                    gvDetail_Ret.FooterRow.Visible = false;
                }
                else
                {

                }
            }
        }

        protected void GridShow_Ret_Edit()
        {
            if (ddlRetType.Text == "IRTS")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDt = transdate.ToString("yyyy/MM/dd");

                Int64 ddlSalesEdit_Ret = 0;
                if (ddlSalesEditInNo_Ret.Text == "Select")
                {
                    ddlSalesEdit_Ret = 0;
                }
                else
                    ddlSalesEdit_Ret = Convert.ToInt64(ddlSalesEditInNo_Ret.Text);

                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT STK_ITEM.ITEMNM, STK_TRANS.* FROM STK_ITEM INNER JOIN STK_TRANS ON STK_ITEM.ITEMID = STK_TRANS.ITEMID where STK_TRANS.TRANSTP='IRTS' and STK_TRANS.TRANSDT = '" + TrDt + "' and STK_TRANS.TRANSMY = '" + lblMy_Ret.Text + "' and STK_TRANS.TRANSNO = " + ddlSalesEdit_Ret + " order by TRANSSL", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDetail_Ret.DataSource = ds;
                    gvDetail_Ret.DataBind();

                    if (gvDetail_Ret.EditIndex == -1)
                    {
                        Decimal totAmt = 0;
                        Decimal a = 0;
                        decimal dis = 0;
                        decimal disAmt = 0;
                        decimal net = 0;
                        decimal netAmt = 0;
                        foreach (GridViewRow grid in gvDetail_Ret.Rows)
                        {
                            Label Per = (Label)grid.Cells[8].FindControl("lblAmount_Ret");
                            Label lblDisAmt_Ret = (Label)grid.Cells[10].FindControl("lblDisAmt_Ret");
                            Label lblNetAmt_Ret = (Label)grid.Cells[11].FindControl("lblNetAmt_Ret");
                            if (Per.Text == "")
                            {
                                Per.Text = "0.00";
                            }
                            else
                            {
                                Per.Text = Per.Text;
                            }
                            String Perf = Per.Text;
                            totAmt = Convert.ToDecimal(Perf);
                            a += totAmt;
                            string tAmount = SpellAmount.comma(a);
                            txtTAmount_Ret.Text = tAmount;

                            if (lblDisAmt_Ret.Text == "")
                            {
                                lblDisAmt_Ret.Text = "0.00";
                            }
                            else
                                lblDisAmt_Ret.Text = lblDisAmt_Ret.Text;

                            dis = Convert.ToDecimal(lblDisAmt_Ret.Text);
                            disAmt += dis;
                            string tDamt = SpellAmount.comma(disAmt);
                            txtTDisAmount_Ret.Text = tDamt;

                            if (lblNetAmt_Ret.Text == "")
                            {
                                lblNetAmt_Ret.Text = "0.00";
                            }
                            else
                                lblNetAmt_Ret.Text = lblNetAmt_Ret.Text;

                            net = Convert.ToDecimal(lblNetAmt_Ret.Text);
                            netAmt += net;
                            string nAmount = SpellAmount.comma(netAmt);
                            txtTotal_Ret.Text = nAmount;

                            //txtTotAmt.Text = nAmount;
                            //txtGrossDisAmt.Text = "0.00";
                            //txtNetAmt.Text = nAmount;
                        }
                        a += totAmt;
                        disAmt += dis;
                        netAmt += net;
                    }
                    else
                    {

                    }

                    TextBox txtItemNM_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtItemNM_Ret");
                    txtItemNM_Ret.Focus();
                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    gvDetail_Ret.DataSource = ds;
                    gvDetail_Ret.DataBind();
                    int columncount = gvDetail_Ret.Rows[0].Cells.Count;
                    gvDetail_Ret.Rows[0].Cells.Clear();
                    gvDetail_Ret.Rows[0].Cells.Add(new TableCell());
                    gvDetail_Ret.Rows[0].Cells[0].ColumnSpan = columncount;
                    gvDetail_Ret.Rows[0].Cells[0].Text = "No Records Found";
                    gvDetail_Ret.Rows[0].Visible = false;

                    txtSLMNo_Ret.Text = "";
                    txtSaleFrom_Ret.Text = "";
                    txtSlFr_Ret.Text = "";
                    txtPID_Ret.Text = "";
                    txtPSNM_Ret.Text = "";
                    txtRemarks_Ret.Text = "";
                    ddlSalesEditInNo_Ret.SelectedIndex = -1;
                }
            }
            else  /// for purchase
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDt = transdate.ToString("yyyy/MM/dd");

                Int64 ddlSalesEdit_Ret = 0;
                if (ddlSalesEditInNo_Ret.Text == "Select")
                {
                    ddlSalesEdit_Ret = 0;
                }
                else
                    ddlSalesEdit_Ret = Convert.ToInt64(ddlSalesEditInNo_Ret.Text);

                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT STK_ITEM.ITEMNM, STK_TRANS.* FROM STK_ITEM INNER JOIN STK_TRANS ON STK_ITEM.ITEMID = STK_TRANS.ITEMID where STK_TRANS.TRANSTP='IRTB' and STK_TRANS.TRANSDT = '" + TrDt + "' and STK_TRANS.TRANSMY = '" + lblMy_Ret.Text + "' and STK_TRANS.TRANSNO = " + ddlSalesEdit_Ret + " order by TRANSSL", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDetail_Ret.DataSource = ds;
                    gvDetail_Ret.DataBind();

                    if (gvDetail_Ret.EditIndex == -1)
                    {
                        Decimal totAmt = 0;
                        Decimal a = 0;
                        decimal dis = 0;
                        decimal disAmt = 0;
                        decimal net = 0;
                        decimal netAmt = 0;
                        foreach (GridViewRow grid in gvDetail_Ret.Rows)
                        {
                            Label Per = (Label)grid.Cells[8].FindControl("lblAmount_Ret");
                            Label lblDisAmt_Ret = (Label)grid.Cells[10].FindControl("lblDisAmt_Ret");
                            Label lblNetAmt_Ret = (Label)grid.Cells[11].FindControl("lblNetAmt_Ret");
                            if (Per.Text == "")
                            {
                                Per.Text = "0.00";
                            }
                            else
                            {
                                Per.Text = Per.Text;
                            }
                            String Perf = Per.Text;
                            totAmt = Convert.ToDecimal(Perf);
                            a += totAmt;
                            string tAmount = SpellAmount.comma(a);
                            txtTAmount_Ret.Text = tAmount;

                            if (lblDisAmt_Ret.Text == "")
                            {
                                lblDisAmt_Ret.Text = "0.00";
                            }
                            else
                                lblDisAmt_Ret.Text = lblDisAmt_Ret.Text;

                            dis = Convert.ToDecimal(lblDisAmt_Ret.Text);
                            disAmt += dis;
                            string tDamt = SpellAmount.comma(disAmt);
                            txtTDisAmount_Ret.Text = tDamt;

                            if (lblNetAmt_Ret.Text == "")
                            {
                                lblNetAmt_Ret.Text = "0.00";
                            }
                            else
                                lblNetAmt_Ret.Text = lblNetAmt_Ret.Text;

                            net = Convert.ToDecimal(lblNetAmt_Ret.Text);
                            netAmt += net;
                            string nAmount = SpellAmount.comma(netAmt);
                            txtTotal_Ret.Text = nAmount;

                            //txtTotAmt.Text = nAmount;
                            //txtGrossDisAmt.Text = "0.00";
                            //txtNetAmt.Text = nAmount;
                        }
                        a += totAmt;
                        disAmt += dis;
                        netAmt += net;
                    }
                    else
                    {

                    }

                    TextBox txtItemNM_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtItemNM_Ret");
                    txtItemNM_Ret.Focus();
                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    gvDetail_Ret.DataSource = ds;
                    gvDetail_Ret.DataBind();
                    int columncount = gvDetail_Ret.Rows[0].Cells.Count;
                    gvDetail_Ret.Rows[0].Cells.Clear();
                    gvDetail_Ret.Rows[0].Cells.Add(new TableCell());
                    gvDetail_Ret.Rows[0].Cells[0].ColumnSpan = columncount;
                    gvDetail_Ret.Rows[0].Cells[0].Text = "No Records Found";
                    gvDetail_Ret.Rows[0].Visible = false;

                    txtSLMNo_Ret.Text = "";
                    txtSaleFrom_Ret.Text = "";
                    txtSlFr_Ret.Text = "";
                    txtPID_Ret.Text = "";
                    txtPSNM_Ret.Text = "";
                    txtRemarks_Ret.Text = "";
                    ddlSalesEditInNo_Ret.SelectedIndex = -1;
                }
            }
        }

        protected void GridShow_Ret_CompleteEdit()
        {
            if (ddlRetType.Text == "IRTS")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDt = transdate.ToString("yyyy/MM/dd");

                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT STK_ITEM.ITEMNM, STK_TRANS.* FROM STK_ITEM INNER JOIN STK_TRANS ON STK_ITEM.ITEMID = STK_TRANS.ITEMID where STK_TRANS.TRANSTP='IRTS' and STK_TRANS.TRANSDT = '" + TrDt + "' and STK_TRANS.TRANSMY = '" + lblMy_Ret.Text + "' and STK_TRANS.TRANSNO = '" + ddlSalesEditInNo_Ret.Text + "' order by TRANSSL", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDetail_Ret.DataSource = ds;
                    gvDetail_Ret.DataBind();
                    gvDetail_Ret.FooterRow.Visible = false;
                }
                else
                {

                }
            }
            else  ///////////// for purchase
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDt = transdate.ToString("yyyy/MM/dd");

                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT STK_ITEM.ITEMNM, STK_TRANS.* FROM STK_ITEM INNER JOIN STK_TRANS ON STK_ITEM.ITEMID = STK_TRANS.ITEMID where STK_TRANS.TRANSTP='IRTB' and STK_TRANS.TRANSDT = '" + TrDt + "' and STK_TRANS.TRANSMY = '" + lblMy_Ret.Text + "' and STK_TRANS.TRANSNO = '" + ddlSalesEditInNo_Ret.Text + "' order by TRANSSL", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDetail_Ret.DataSource = ds;
                    gvDetail_Ret.DataBind();
                    gvDetail_Ret.FooterRow.Visible = false;
                }
                else
                {

                }
            }
        }

        protected void txtItemNM_Ret_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            //NamingContainer return the container that the control sits in
            TextBox txtItemNM_Ret = (TextBox)row.FindControl("txtItemNM_Ret");
            TextBox txtItID_Ret = (TextBox)row.FindControl("txtItID_Ret");
            txtItID_Ret.ReadOnly = true;
            Global.txtAdd(@"Select ITEMID from STK_ITEM where ITEMNM = '" + txtItemNM_Ret.Text + "'", txtItID_Ret);
            TextBox txtCPQTY_Ret = (TextBox)row.FindControl("txtCPQTY_Ret");
            Global.txtAdd(@"Select PQTY from STK_ITEM where ITEMNM = '" + txtItemNM_Ret.Text + "'", txtCPQTY_Ret);
            txtCPQTY_Ret.ReadOnly = true;
            TextBox txtCQty_Ret = (TextBox)row.FindControl("txtCQty_Ret");
            txtCQty_Ret.Text = "0";
            TextBox txtPQty_Ret = (TextBox)row.FindControl("txtPQty_Ret");
            TextBox txtQty_Ret = (TextBox)row.FindControl("txtQty_Ret");
            txtQty_Ret.Text = "0";
            txtPQty_Ret.Text = "0";
            TextBox txtRate_Ret = (TextBox)row.FindControl("txtRate_Ret");
            Global.txtAdd(@"Select SALERT from STK_ITEM where ITEMNM = '" + txtItemNM_Ret.Text + "'", txtRate_Ret);
            //txtRate.ReadOnly = true;
            TextBox txtAmount_Ret = (TextBox)row.FindControl("txtAmount_Ret");
            txtAmount_Ret.Text = ".00";
            txtAmount_Ret.ReadOnly = true;
            DropDownList ddlType_Ret = (DropDownList)row.FindControl("ddlType_Ret");
            ddlType_Ret.Focus();
        }

        protected void ddlType_Ret_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddlType_Ret = (DropDownList)row.FindControl("ddlType_Ret");
            if (ddlType_Ret.Text == "CARTON")
            {
                TextBox txtCQty_Ret = (TextBox)row.FindControl("txtCQty_Ret");
                txtCQty_Ret.Focus();
            }
            else
            {
                TextBox txtPQty_Ret = (TextBox)row.FindControl("txtPQty_Ret");
                txtPQty_Ret.Focus();
                TextBox txtCQty_Ret = (TextBox)row.FindControl("txtCQty_Ret");
                txtCQty_Ret.Text = "0";
            }
        }

        protected void txtCQty_Ret_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCPQTY_Ret = (TextBox)row.FindControl("txtCPQTY_Ret");
            Int64 CPQty = Convert.ToInt64(txtCPQTY_Ret.Text);
            TextBox txtCQty_Ret = (TextBox)row.FindControl("txtCQty_Ret");
            Int64 CQty = Convert.ToInt64(txtCQty_Ret.Text);
            TextBox txtPQty_Ret = (TextBox)row.FindControl("txtPQty_Ret");
            Int64 PQty;
            if (txtPQty_Ret.Text == "")
            {
                PQty = 0;
            }
            else
                PQty = Convert.ToInt64(txtPQty_Ret.Text);

            Int64 Qty = CPQty * CQty + PQty;
            TextBox txtQty_Ret = (TextBox)row.FindControl("txtQty_Ret");
            txtQty_Ret.Text = Qty.ToString();
            txtQty_Ret.ReadOnly = true;

            TextBox txtRate_Ret = (TextBox)row.FindControl("txtRate_Ret");
            decimal Rate = Convert.ToDecimal(txtRate_Ret.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmount_Ret = (TextBox)row.FindControl("txtAmount_Ret");
            txtAmount_Ret.Text = Amount.ToString();
            txtAmount_Ret.ReadOnly = true;
            TextBox txtNetAmt_Ret = (TextBox)row.FindControl("txtNetAmt_Ret");
            txtNetAmt_Ret.Text = Amount.ToString();
            txtNetAmt_Ret.ReadOnly = true;
            //TextBox txtNetAmt = (TextBox)row.FindControl("txtNetAmt");
            //txtNetAmt.Text = Amount.ToString();
            //txtNetAmt.ReadOnly = true;
            txtPQty_Ret.Focus();
        }

        protected void txtPQty_Ret_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCPQTY_Ret = (TextBox)row.FindControl("txtCPQTY_Ret");
            Int64 CPQty = Convert.ToInt64(txtCPQTY_Ret.Text);
            TextBox txtCQty_Ret = (TextBox)row.FindControl("txtCQty_Ret");
            Int64 CQty = Convert.ToInt64(txtCQty_Ret.Text);
            TextBox txtPQty_Ret = (TextBox)row.FindControl("txtPQty_Ret");
            Int64 PQty = Convert.ToInt64(txtPQty_Ret.Text);
            Int64 Qty = CPQty * CQty + PQty;
            TextBox txtQty_Ret = (TextBox)row.FindControl("txtQty_Ret");
            txtQty_Ret.Text = Qty.ToString();
            txtQty_Ret.ReadOnly = true;

            TextBox txtRate_Ret = (TextBox)row.FindControl("txtRate_Ret");
            decimal Rate = Convert.ToDecimal(txtRate_Ret.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmount_Ret = (TextBox)row.FindControl("txtAmount_Ret");
            txtAmount_Ret.Text = Amount.ToString();
            txtAmount_Ret.ReadOnly = true;

            //TextBox txtNetAmt = (TextBox)row.FindControl("txtNetAmt");
            //txtNetAmt.Text = Amount.ToString();
            //txtNetAmt.ReadOnly = true;

            txtRate_Ret.Focus();
        }

        protected void txtRate_Ret_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

            TextBox txtQty_Ret = (TextBox)row.FindControl("txtQty_Ret");
            Int64 Qty = Convert.ToInt64(txtQty_Ret.Text);

            TextBox txtRate_Ret = (TextBox)row.FindControl("txtRate_Ret");
            decimal Rate = Convert.ToDecimal(txtRate_Ret.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmount_Ret = (TextBox)row.FindControl("txtAmount_Ret");
            txtAmount_Ret.Text = Amount.ToString();
            txtAmount_Ret.ReadOnly = true;

            //TextBox txtNetAmt = (TextBox)row.FindControl("txtNetAmt");
            //txtNetAmt.Text = Amount.ToString();
            //txtNetAmt.ReadOnly = true;

            TextBox txtDisRt_Ret = (TextBox)row.FindControl("txtDisRt_Ret");
            txtDisRt_Ret.Focus();
        }

        protected void txtDisRt_Ret_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

            TextBox txtAmount_Ret = (TextBox)row.FindControl("txtAmount_Ret");
            decimal Amount = Convert.ToDecimal(txtAmount_Ret.Text);

            TextBox txtDisRt_Ret = (TextBox)row.FindControl("txtDisRt_Ret");
            if (txtDisRt_Ret.Text == "")
            {
                txtDisRt_Ret.Text = "0";
            }
            else
                txtDisRt_Ret.Text = txtDisRt_Ret.Text;
            decimal DisRt = Convert.ToDecimal(txtDisRt_Ret.Text);
            decimal disRate = 0;
            if (DisRt >= 100)
            {
                lblGridMsg_Ret.Visible = true;
                lblGridMsg_Ret.Text = "Rate bellow than 100";
                txtDisRt_Ret.Focus();
            }
            else
            {
                lblGridMsg_Ret.Visible = false;
                disRate = DisRt;

                decimal DisRT_F = disRate / 100;
                decimal Amt = Convert.ToDecimal(string.Format("{0:0.00}", Amount * DisRT_F));

                TextBox txtDisAmt_Ret = (TextBox)row.FindControl("txtDisAmt_Ret");
                txtDisAmt_Ret.Text = Amt.ToString();

                decimal NetAmount = Convert.ToDecimal(string.Format("{0:0.00}", Amount - Amt));

                TextBox txtNetAmt_Ret = (TextBox)row.FindControl("txtNetAmt_Ret");
                txtNetAmt_Ret.Text = NetAmount.ToString();
                txtNetAmt_Ret.ReadOnly = true;

                txtDisAmt_Ret.Focus();
            }
        }

        protected void txtDisAmt_Ret_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

            TextBox txtAmount_Ret = (TextBox)row.FindControl("txtAmount_Ret");
            decimal Amount = Convert.ToDecimal(txtAmount_Ret.Text);

            TextBox txtDisAmt_Ret = (TextBox)row.FindControl("txtDisAmt_Ret");
            if (txtDisAmt_Ret.Text == "")
            {
                txtDisAmt_Ret.Text = "0.00";
            }
            else
                txtDisAmt_Ret.Text = txtDisAmt_Ret.Text;
            decimal DisAmt = Convert.ToDecimal(txtDisAmt_Ret.Text);

            decimal NetAmount = Convert.ToDecimal(string.Format("{0:0.00}", Amount - DisAmt));

            TextBox txtNetAmt_Ret = (TextBox)row.FindControl("txtNetAmt_Ret");
            txtNetAmt_Ret.Text = NetAmount.ToString();
            txtNetAmt_Ret.ReadOnly = true;

            ImageButton imgbtnAdd_Ret = (ImageButton)row.FindControl("imgbtnAdd_Ret");
            imgbtnAdd_Ret.Focus();
        }

        protected void txtItemNMEdit_Ret_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            //NamingContainer return the container that the control sits in
            TextBox txtItemNMEdit_Ret = (TextBox)row.FindControl("txtItemNMEdit_Ret");
            Label lblItemIDEdit_Ret = (Label)row.FindControl("lblItemIDEdit_Ret");
            Global.lblAdd(@"Select ITEMID from STK_ITEM where ITEMNM = '" + txtItemNMEdit_Ret.Text + "'", lblItemIDEdit_Ret);
            TextBox txtCPQTYEdit_Ret = (TextBox)row.FindControl("txtCPQTYEdit_Ret");
            Global.txtAdd(@"Select PQTY from STK_ITEM where ITEMNM = '" + txtItemNMEdit_Ret.Text + "'", txtCPQTYEdit_Ret);
            txtCPQTYEdit_Ret.ReadOnly = true;
            TextBox txtCQtyEdit_Ret = (TextBox)row.FindControl("txtCQtyEdit_Ret");
            txtCQtyEdit_Ret.Text = "0";
            TextBox txtPQtyEdit_Ret = (TextBox)row.FindControl("txtPQtyEdit_Ret");
            TextBox txtQtyEdit_Ret = (TextBox)row.FindControl("txtQtyEdit_Ret");
            txtQtyEdit_Ret.Text = "0";
            txtPQtyEdit_Ret.Text = "0";
            TextBox txtRateEdit_Ret = (TextBox)row.FindControl("txtRateEdit_Ret");
            Global.txtAdd(@"Select SALERT from STK_ITEM where ITEMNM = '" + txtItemNMEdit_Ret.Text + "'", txtRateEdit_Ret);
            //txtRateEdit.ReadOnly = true;
            TextBox txtAmountEdit_Ret = (TextBox)row.FindControl("txtAmountEdit_Ret");
            txtAmountEdit_Ret.Text = ".00";
            txtAmountEdit_Ret.ReadOnly = true;
            DropDownList ddltypeEdit_Ret = (DropDownList)row.FindControl("ddltypeEdit_Ret");
            ddltypeEdit_Ret.Focus();
        }

        protected void ddltypeEdit_Ret_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddltypeEdit_Ret = (DropDownList)row.FindControl("ddltypeEdit_Ret");
            if (ddltypeEdit_Ret.Text == "CARTON")
            {
                TextBox txtCQtyEdit_Ret = (TextBox)row.FindControl("txtCQtyEdit_Ret");
                txtCQtyEdit_Ret.Focus();
            }
            else
            {
                TextBox txtPQtyEdit_Ret = (TextBox)row.FindControl("txtPQtyEdit_Ret");
                txtPQtyEdit_Ret.Focus();
                TextBox txtCQtyEdit_Ret = (TextBox)row.FindControl("txtCQtyEdit_Ret");
                txtCQtyEdit_Ret.Text = "0";
            }
        }

        protected void txtCQtyEdit_Ret_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCPQTYEdit_Ret = (TextBox)row.FindControl("txtCPQTYEdit_Ret");
            Int64 CPQty = Convert.ToInt64(txtCPQTYEdit_Ret.Text);
            TextBox txtCQtyEdit_Ret = (TextBox)row.FindControl("txtCQtyEdit_Ret");
            Int64 CQty = Convert.ToInt64(txtCQtyEdit_Ret.Text);
            TextBox txtPQtyEdit_Ret = (TextBox)row.FindControl("txtPQtyEdit_Ret");
            Int64 PQty;
            if (txtPQtyEdit_Ret.Text == "")
            {
                PQty = 0;
            }
            else
                PQty = Convert.ToInt64(txtPQtyEdit_Ret.Text);

            Int64 Qty = CPQty * CQty + PQty;
            TextBox txtQtyEdit_Ret = (TextBox)row.FindControl("txtQtyEdit_Ret");
            txtQtyEdit_Ret.Text = Qty.ToString();
            txtQtyEdit_Ret.ReadOnly = true;

            TextBox txtRateEdit_Ret = (TextBox)row.FindControl("txtRateEdit_Ret");
            decimal Rate = Convert.ToDecimal(txtRateEdit_Ret.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmountEdit_Ret = (TextBox)row.FindControl("txtAmountEdit_Ret");
            txtAmountEdit_Ret.Text = Amount.ToString();
            txtAmountEdit_Ret.ReadOnly = true;
            TextBox txtNetAmtEdit_Ret = (TextBox)row.FindControl("txtNetAmtEdit_Ret");
            txtNetAmtEdit_Ret.Text = Amount.ToString();
            txtNetAmtEdit_Ret.ReadOnly = true;
            txtPQtyEdit_Ret.Focus();
        }

        protected void txtPQtyEdit_Ret_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCPQTYEdit_Ret = (TextBox)row.FindControl("txtCPQTYEdit_Ret");
            Int64 CPQty = Convert.ToInt64(txtCPQTYEdit_Ret.Text);
            TextBox txtCQtyEdit_Ret = (TextBox)row.FindControl("txtCQtyEdit_Ret");
            Int64 CQty = Convert.ToInt64(txtCQtyEdit_Ret.Text);
            TextBox txtPQtyEdit_Ret = (TextBox)row.FindControl("txtPQtyEdit_Ret");
            Int64 PQty = Convert.ToInt64(txtPQtyEdit_Ret.Text);
            Int64 Qty = CPQty * CQty + PQty;
            TextBox txtQtyEdit_Ret = (TextBox)row.FindControl("txtQtyEdit_Ret");
            txtQtyEdit_Ret.Text = Qty.ToString();
            txtQtyEdit_Ret.ReadOnly = true;

            TextBox txtRateEdit_Ret = (TextBox)row.FindControl("txtRateEdit_Ret");
            decimal Rate = Convert.ToDecimal(txtRateEdit_Ret.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmountEdit_Ret = (TextBox)row.FindControl("txtAmountEdit_Ret");
            txtAmountEdit_Ret.Text = Amount.ToString();
            txtAmountEdit_Ret.ReadOnly = true;

            txtRateEdit_Ret.Focus();
            //ImageButton imgbtnUpdate = (ImageButton)row.FindControl("imgbtnUpdate");
            //imgbtnUpdate.Focus();
        }

        protected void txtRateEdit_Ret_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

            TextBox txtQtyEdit_Ret = (TextBox)row.FindControl("txtQtyEdit_Ret");
            Int64 Qty = Convert.ToInt64(txtQtyEdit_Ret.Text);

            TextBox txtRateEdit_Ret = (TextBox)row.FindControl("txtRateEdit_Ret");
            decimal Rate = Convert.ToDecimal(txtRateEdit_Ret.Text);
            decimal Amount = Rate * Qty;
            TextBox txtAmountEdit_Ret = (TextBox)row.FindControl("txtAmountEdit_Ret");
            txtAmountEdit_Ret.Text = Amount.ToString();
            txtAmountEdit_Ret.ReadOnly = true;

            ImageButton imgbtnUpdate_Ret = (ImageButton)row.FindControl("imgbtnUpdate_Ret");
            imgbtnUpdate_Ret.Focus();
        }

        protected void txtDisRtEdit_Ret_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

            TextBox txtAmountEdit_Ret = (TextBox)row.FindControl("txtAmountEdit_Ret");
            decimal Amount = Convert.ToDecimal(txtAmountEdit_Ret.Text);

            TextBox txtDisRtEdit_Ret = (TextBox)row.FindControl("txtDisRtEdit_Ret");
            if (txtDisRtEdit_Ret.Text == "")
            {
                txtDisRtEdit_Ret.Text = "0";
            }
            else
                txtDisRtEdit_Ret.Text = txtDisRtEdit_Ret.Text;
            decimal DisRt = Convert.ToDecimal(txtDisRtEdit_Ret.Text);
            decimal disRate = 0;
            if (DisRt >= 100)
            {
                lblGridMsg_Ret.Visible = true;
                lblGridMsg.Text = "Rate bellow than 100";
                txtDisRtEdit_Ret.Focus();
            }
            else
            {
                lblGridMsg_Ret.Visible = false;
                disRate = DisRt;

                decimal DisRT_F = disRate / 100;
                decimal Amt = Convert.ToDecimal(string.Format("{0:0.00}", Amount * DisRT_F));

                TextBox txtDisAmtEdit_Ret = (TextBox)row.FindControl("txtDisAmtEdit_Ret");
                txtDisAmtEdit_Ret.Text = Amt.ToString();

                decimal NetAmount = Convert.ToDecimal(string.Format("{0:0.00}", Amount - Amt));

                TextBox txtNetAmtEdit_Ret = (TextBox)row.FindControl("txtNetAmtEdit_Ret");
                txtNetAmtEdit_Ret.Text = NetAmount.ToString();
                txtNetAmtEdit_Ret.ReadOnly = true;

                txtDisAmtEdit_Ret.Focus();
            }
        }

        protected void txtDisAmtEdit_Ret_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

            TextBox txtAmountEdit_Ret = (TextBox)row.FindControl("txtAmountEdit_Ret");
            decimal Amount = Convert.ToDecimal(txtAmountEdit_Ret.Text);

            TextBox txtDisAmtEdit_Ret = (TextBox)row.FindControl("txtDisAmtEdit_Ret");
            if (txtDisAmtEdit_Ret.Text == "")
            {
                txtDisAmtEdit_Ret.Text = "0.00";
            }
            else
                txtDisAmtEdit_Ret.Text = txtDisAmtEdit_Ret.Text;
            decimal DisAmt = Convert.ToDecimal(txtDisAmtEdit_Ret.Text);

            decimal NetAmount = Convert.ToDecimal(string.Format("{0:0.00}", Amount - DisAmt));

            TextBox txtNetAmtEdit_Ret = (TextBox)row.FindControl("txtNetAmtEdit_Ret");
            txtNetAmtEdit_Ret.Text = NetAmount.ToString();
            txtNetAmtEdit_Ret.ReadOnly = true;

            ImageButton imgbtnUpdate_Ret = (ImageButton)row.FindControl("imgbtnUpdate_Ret");
            imgbtnUpdate_Ret.Focus();
        }

        protected void gvDetail_Ret_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (ddlRetType.Text == "IRTS")
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();
                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);

                string query = "";
                SqlCommand comm = new SqlCommand(query, conn);

                string Transtp = "IRTS";


                DateTime TransDT = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDt = TransDT.ToString("yyyy/MM/dd");

                if (e.CommandName.Equals("SaveCon"))
                {
                    TextBox txtItID_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtItID_Ret");
                    TextBox txtItemNM_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtItemNM_Ret");
                    TextBox txtQty_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtQty_Ret");
                    TextBox txtPQty_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtPQty_Ret");

                    if (txtSlFr_Ret.Text == "")
                    {
                        lblSaleFrom_Ret.Visible = true;
                        lblSaleFrom_Ret.Text = "Select Store.";
                        txtSaleFrom_Ret.Focus();
                        lblPartyID_Ret.Visible = false;
                        lblGridMsg_Ret.Visible = false;
                    }
                    else if (txtPID_Ret.Text == "")
                    {
                        lblPartyID_Ret.Visible = true;
                        lblPartyID_Ret.Text = "Select Party";
                        txtPSNM_Ret.Focus();
                        lblSaleFrom.Visible = false;
                        lblGridMsg.Visible = false;
                    }
                    else if (txtItID_Ret.Text == "")
                    {
                        lblGridMsg_Ret.Visible = true;
                        lblGridMsg_Ret.Text = "Select Item.";
                        txtItemNM_Ret.Focus();
                        lblSaleFrom_Ret.Visible = false;
                        lblPartyID_Ret.Visible = false;
                    }
                    else if (txtQty_Ret.Text == "0")
                    {
                        lblGridMsg_Ret.Visible = true;
                        lblGridMsg_Ret.Text = "Quantity is Wrong.";
                        txtPQty_Ret.Focus();
                    }
                    else
                    {
                        lblSaleFrom_Ret.Visible = false;
                        lblPartyID_Ret.Visible = false;
                        lblGridMsg_Ret.Visible = false;

                        conn.Open();
                        SqlCommand cmd = new SqlCommand();
                        if (btnEdit_Ret.Text == "EDIT")
                        {
                            //Global.txtAdd("SELECT MAX(TRANSNO) AS TRANSNO FROM STK_TRANSMST WHERE TRANSMY='" + lblSMY.Text + "' AND TRANSTP='SALE'", txtInNo);
                            cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + txtInNo_Ret.Text + "' and TRANSTP='IRTS' AND TRANSMY='" + lblMy_Ret.Text + "'", conn);
                        }
                        else
                        {
                            cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + ddlSalesEditInNo_Ret.Text + "' and TRANSTP='IRTS' AND TRANSMY='" + lblMy_Ret.Text + "'", conn);
                        }
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        conn.Close();
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM_Ret.Text + "'", lblCatID_Ret);

                            DropDownList ddlType_Ret = (DropDownList)gvDetail_Ret.FooterRow.FindControl("ddlType_Ret");
                            TextBox txtCPQTY_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtCPQTY_Ret");
                            if (txtCPQTY_Ret.Text == "")
                            {
                                txtCPQTY_Ret.Text = "0";
                            }
                            else
                                txtCPQTY_Ret.Text = txtCPQTY_Ret.Text;
                            decimal cpQty = 0;
                            cpQty = Convert.ToDecimal(txtCPQTY_Ret.Text);
                            TextBox txtCQty_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtCQty_Ret");
                            if (txtCQty_Ret.Text == "")
                            {
                                txtCQty_Ret.Text = "0";
                            }
                            else
                                txtCQty_Ret.Text = txtCQty_Ret.Text;
                            decimal cQty = 0;
                            cQty = Convert.ToDecimal(txtCQty_Ret.Text);
                            decimal pQty = 0;
                            if (txtPQty_Ret.Text == "")
                            {
                                txtPQty_Ret.Text = "0";
                            }
                            else
                                txtPQty_Ret.Text = txtPQty_Ret.Text;
                            pQty = Convert.ToDecimal(txtPQty_Ret.Text);

                            decimal Qty = 0;
                            if (txtQty_Ret.Text == "")
                            {
                                txtQty_Ret.Text = "0";
                            }
                            else
                                txtQty_Ret.Text = txtQty_Ret.Text;
                            Qty = Convert.ToDecimal(txtQty_Ret.Text);
                            TextBox txtRate_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtRate_Ret");
                            decimal Rate = 0;
                            Rate = Convert.ToDecimal(txtRate_Ret.Text);
                            TextBox txtAmount_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtAmount_Ret");
                            decimal Amount = 0;
                            Amount = Convert.ToDecimal(txtAmount_Ret.Text);

                            TextBox txtDisRt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtDisRt_Ret");
                            if (txtDisRt_Ret.Text == "")
                            {
                                txtDisRt_Ret.Text = "0";
                            }
                            else
                            {
                                txtDisRt_Ret.Text = txtDisRt_Ret.Text;
                            }
                            decimal disRt = 0;
                            disRt = Convert.ToDecimal(txtDisRt_Ret.Text);

                            TextBox txtDisAmt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtDisAmt_Ret");
                            if (txtDisAmt_Ret.Text == "")
                            {
                                txtDisAmt_Ret.Text = "0";
                            }
                            else
                            {
                                txtDisAmt_Ret.Text = txtDisAmt_Ret.Text;
                            }
                            decimal disAmt = 0;
                            disAmt = Convert.ToDecimal(txtDisAmt_Ret.Text);

                            TextBox txtNetAmt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtNetAmt_Ret");
                            if (txtNetAmt_Ret.Text == "")
                            {
                                txtNetAmt_Ret.Text = "0";
                            }
                            else
                            {
                                txtNetAmt_Ret.Text = txtNetAmt_Ret.Text;
                            }
                            decimal NetAmt = 0;
                            NetAmt = Convert.ToDecimal(txtNetAmt_Ret.Text);


                            Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'IRTS' and TRANSMY = '" + lblMy_Ret.Text + "'", lblTransSL_Ret);
                            string ItemCD;
                            string mxCD = "";
                            string mid = "";
                            string subItemCD = "";
                            int subCD, incrItCD;
                            if (lblTransSL_Ret.Text == "")
                            {
                                ItemCD = "00000001";
                            }
                            else
                            {
                                mxCD = lblTransSL_Ret.Text;
                                //OItemCD = mxCD.Substring(4,4);
                                subCD = int.Parse(mxCD);
                                incrItCD = subCD + 1;
                                if (incrItCD < 10)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0000000" + mid;
                                }
                                else if (incrItCD < 100)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "000000" + mid;
                                }
                                else if (incrItCD < 1000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "00000" + mid;
                                }
                                else if (incrItCD < 10000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0000" + mid;
                                }
                                else if (incrItCD < 100000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "000" + mid;
                                }
                                else if (incrItCD < 1000000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "00" + mid;
                                }
                                else if (incrItCD < 10000000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0" + mid;
                                }
                                //else
                                //    subItemCD = incrItCD.ToString();

                                ItemCD = subItemCD;
                            }

                            if (btnEdit_Ret.Text == "EDIT")
                            {
                                query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT,  DISCRT, DISCAMT, NETAMT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblMy_Ret.Text + "','" + txtInNo_Ret.Text + "','" + txtSLMNo_Ret.Text + "','','" + txtSlFr_Ret.Text + "','" + txtPID_Ret.Text + "','','','','" + txtRemarks_Ret.Text + "',@TRANSSL,'" + lblCatID_Ret.Text + "','" + txtItID_Ret.Text + "', " +
                                     " '" + ddlType_Ret.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + "," + disRt + "," + disAmt + "," + NetAmt + ",'',@USERID,'')");

                                comm = new SqlCommand(query, conn);
                                comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                                comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                                // comm.Parameters.AddWithValue("@LCDATE", lcDate);
                                comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                                comm.Parameters.AddWithValue("@USERID", userName);

                                conn.Open();
                                int result = comm.ExecuteNonQuery();
                                conn.Close();

                                GridShow_Ret();
                            }
                            else
                            {
                                Int64 TransSaleEdit = Convert.ToInt64(ddlSalesEditInNo_Ret.Text);
                                conn.Open();
                                SqlCommand cmd1 = new SqlCommand("select * from STK_TRANSMST where TRANSTP = 'IRTS' and TRANSMY='" + lblMy_Ret.Text + "' and TRANSNO =" + TransSaleEdit + "", conn);
                                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                                DataSet ds1 = new DataSet();
                                da.Fill(ds1);
                                conn.Close();
                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, DISCRT, DISCAMT, NETAMT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblMy_Ret.Text + "','" + ddlSalesEditInNo_Ret.Text + "','" + txtSLMNo_Ret.Text + "','','" + txtSlFr_Ret.Text + "','" + txtPID_Ret.Text + "','','','','" + txtRemarks_Ret.Text + "',@TRANSSL,'" + lblCatID_Ret.Text + "','" + txtItID_Ret.Text + "', " +
                                     " '" + ddlType_Ret.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ", " + disRt + "," + disAmt + "," + NetAmt + ",'',@USERID,'')");

                                    comm = new SqlCommand(query, conn);
                                    comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                                    comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                                    // comm.Parameters.AddWithValue("@LCDATE", lcDate);
                                    comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                                    comm.Parameters.AddWithValue("@USERID", userName);

                                    conn.Open();
                                    int result = comm.ExecuteNonQuery();
                                    conn.Close();

                                    GridShow_Ret_Edit();
                                }
                                else
                                {
                                    lblGridMsg_Ret.Visible = true;
                                    lblGridMsg_Ret.Text = "Must be in New Mode.";
                                }

                            }
                        }
                        else
                        {
                            query = "insert into STK_TRANSMST ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS, USERPC, USERID, IPADDRESS) " +
                                        "values(@TRANSTP,@TRANSDT,'" + lblMy_Ret.Text + "','" + txtInNo_Ret.Text + "','" + txtSLMNo_Ret.Text + "','','" + txtSlFr_Ret.Text + "','" + txtPID_Ret.Text + "','','','','" + txtRemarks_Ret.Text + "','',@USERID,'')";
                            comm = new SqlCommand(query, conn);
                            comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                            comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                            //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                            comm.Parameters.AddWithValue("@USERID", userName);
                            conn.Open();
                            comm.ExecuteNonQuery();
                            conn.Close();


                            Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM_Ret.Text + "'", lblCatID_Ret);

                            DropDownList ddlType_Ret = (DropDownList)gvDetail_Ret.FooterRow.FindControl("ddlType_Ret");
                            TextBox txtCPQTY_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtCPQTY_Ret");
                            if (txtCPQTY_Ret.Text == "")
                            {
                                txtCPQTY_Ret.Text = "0";
                            }
                            else
                                txtCPQTY_Ret.Text = txtCPQTY_Ret.Text;
                            decimal cpQty = 0;
                            cpQty = Convert.ToDecimal(txtCPQTY_Ret.Text);
                            TextBox txtCQty_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtCQty_Ret");
                            if (txtCQty_Ret.Text == "")
                            {
                                txtCQty_Ret.Text = "0";
                            }
                            else
                                txtCQty_Ret.Text = txtCQty_Ret.Text;
                            decimal cQty = 0;
                            cQty = Convert.ToDecimal(txtCQty_Ret.Text);
                            decimal pQty = 0;
                            if (txtPQty_Ret.Text == "")
                            {
                                txtPQty_Ret.Text = "0";
                            }
                            else
                                txtPQty_Ret.Text = txtPQty_Ret.Text;
                            pQty = Convert.ToDecimal(txtPQty_Ret.Text);

                            decimal Qty = 0;
                            if (txtQty_Ret.Text == "")
                            {
                                txtQty_Ret.Text = "0";
                            }
                            else
                                txtQty_Ret.Text = txtQty_Ret.Text;
                            Qty = Convert.ToDecimal(txtQty_Ret.Text);
                            TextBox txtRate_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtRate_Ret");
                            decimal Rate = 0;
                            Rate = Convert.ToDecimal(txtRate_Ret.Text);
                            TextBox txtAmount_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtAmount_Ret");
                            decimal Amount = 0;
                            Amount = Convert.ToDecimal(txtAmount_Ret.Text);

                            TextBox txtDisRt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtDisRt_Ret");
                            if (txtDisRt_Ret.Text == "")
                            {
                                txtDisRt_Ret.Text = "0";
                            }
                            else
                            {
                                txtDisRt_Ret.Text = txtDisRt_Ret.Text;
                            }
                            decimal disRt = 0;
                            disRt = Convert.ToDecimal(txtDisRt_Ret.Text);

                            TextBox txtDisAmt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtDisAmt_Ret");
                            if (txtDisAmt_Ret.Text == "")
                            {
                                txtDisAmt_Ret.Text = "0";
                            }
                            else
                            {
                                txtDisAmt_Ret.Text = txtDisAmt_Ret.Text;
                            }
                            decimal disAmt = 0;
                            disAmt = Convert.ToDecimal(txtDisAmt_Ret.Text);

                            TextBox txtNetAmt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtNetAmt_Ret");
                            if (txtNetAmt_Ret.Text == "")
                            {
                                txtNetAmt_Ret.Text = "0";
                            }
                            else
                            {
                                txtNetAmt_Ret.Text = txtNetAmt_Ret.Text;
                            }
                            decimal NetAmt = 0;
                            NetAmt = Convert.ToDecimal(txtNetAmt_Ret.Text);

                            Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'IRTS' and TRANSMY = '" + lblMy_Ret.Text + "'", lblTransSL_Ret);
                            string ItemCD;
                            string mxCD = "";
                            string mid = "";
                            string subItemCD = "";
                            int subCD, incrItCD;
                            if (lblTransSL_Ret.Text == "")
                            {
                                ItemCD = "00000001";
                            }
                            else
                            {
                                mxCD = lblTransSL_Ret.Text;
                                //OItemCD = mxCD.Substring(4,4);
                                subCD = int.Parse(mxCD);
                                incrItCD = subCD + 1;
                                if (incrItCD < 10)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0000000" + mid;
                                }
                                else if (incrItCD < 100)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "000000" + mid;
                                }
                                else if (incrItCD < 1000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "00000" + mid;
                                }
                                else if (incrItCD < 10000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0000" + mid;
                                }
                                else if (incrItCD < 100000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "000" + mid;
                                }
                                else if (incrItCD < 1000000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "00" + mid;
                                }
                                else if (incrItCD < 10000000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0" + mid;
                                }
                                //else
                                //    subItemCD = incrItCD.ToString();

                                ItemCD = subItemCD;
                            }

                            query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT,  DISCRT, DISCAMT, NETAMT, USERPC, USERID, IPADDRESS ) " +
                                         "values(@TRANSTP,@TRANSDT,'" + lblMy_Ret.Text + "','" + txtInNo_Ret.Text + "','" + txtSLMNo_Ret.Text + "','','" + txtSlFr_Ret.Text + "','" + txtPID_Ret.Text + "','','','','" + txtRemarks_Ret.Text + "',@TRANSSL,'" + lblCatID_Ret.Text + "','" + txtItID_Ret.Text + "', " +
                                         " '" + ddlType_Ret.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + "," + disRt + "," + disAmt + "," + NetAmt + ",'',@USERID,'')");

                            comm = new SqlCommand(query, conn);
                            comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                            comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                            //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                            comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                            comm.Parameters.AddWithValue("@USERID", userName);

                            conn.Open();
                            int result = comm.ExecuteNonQuery();
                            conn.Close();

                            GridShow_Ret();

                        }
                    }
                }


                    ////////////////////////////////////////////////////////// For Complete   //////////////////////////////////////

                else if (e.CommandName.Equals("Complete"))
                {
                    TextBox txtItID_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtItID_Ret");
                    TextBox txtItemNM_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtItemNM_Ret");
                    TextBox txtQty_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtQty_Ret");
                    TextBox txtPQty_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtPQty_Ret");
                    if (txtSlFr_Ret.Text == "")
                    {
                        lblSaleFrom_Ret.Visible = true;
                        lblSaleFrom_Ret.Text = "Select Store.";
                        txtSaleFrom_Ret.Focus();
                        lblPartyID_Ret.Visible = false;
                        lblGridMsg_Ret.Visible = false;
                    }
                    else if (txtPID_Ret.Text == "")
                    {
                        lblPartyID_Ret.Visible = true;
                        lblPartyID_Ret.Text = "Select Party";
                        txtPSNM_Ret.Focus();
                        lblSaleFrom_Ret.Visible = false;
                        lblGridMsg_Ret.Visible = false;
                    }
                    else if (txtItID_Ret.Text == "")
                    {
                        lblGridMsg_Ret.Visible = true;
                        lblGridMsg_Ret.Text = "Select Item.";
                        txtItemNM_Ret.Focus();
                        lblSaleFrom_Ret.Visible = false;
                        lblPartyID_Ret.Visible = false;
                    }
                    else if (txtQty_Ret.Text == "0")
                    {
                        lblGridMsg_Ret.Visible = true;
                        lblGridMsg_Ret.Text = "Quantity is Wrong.";
                        txtPQty_Ret.Focus();
                    }
                    else
                    {
                        lblSaleFrom_Ret.Visible = false;
                        lblPartyID_Ret.Visible = false;
                        lblGridMsg_Ret.Visible = false;

                        conn.Open();
                        SqlCommand cmd = new SqlCommand();

                        if (btnEdit_Ret.Text == "EDIT")
                        {
                            cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + txtInNo_Ret.Text + "' and TRANSTP='IRTS' AND TRANSMY='" + lblMy_Ret.Text + "'", conn);
                        }
                        else
                        {
                            cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + ddlSalesEditInNo_Ret.Text + "' and TRANSTP='IRTS' AND TRANSMY='" + lblMy_Ret.Text + "'", conn);
                        }
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        conn.Close();
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM_Ret.Text + "'", lblCatID_Ret);

                            DropDownList ddlType_Ret = (DropDownList)gvDetail_Ret.FooterRow.FindControl("ddlType_Ret");
                            TextBox txtCPQTY_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtCPQTY_Ret");
                            if (txtCPQTY_Ret.Text == "")
                            {
                                txtCPQTY_Ret.Text = "0";
                            }
                            else
                                txtCPQTY_Ret.Text = txtCPQTY_Ret.Text;
                            decimal cpQty = 0;
                            cpQty = Convert.ToDecimal(txtCPQTY_Ret.Text);
                            TextBox txtCQty_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtCQty_Ret");
                            if (txtCQty_Ret.Text == "")
                            {
                                txtCQty_Ret.Text = "0";
                            }
                            else
                                txtCQty_Ret.Text = txtCQty_Ret.Text;
                            decimal cQty = 0;
                            cQty = Convert.ToDecimal(txtCQty_Ret.Text);
                            decimal pQty = 0;
                            if (txtPQty_Ret.Text == "")
                            {
                                txtPQty_Ret.Text = "0";
                            }
                            else
                                txtPQty_Ret.Text = txtPQty_Ret.Text;
                            pQty = Convert.ToDecimal(txtPQty_Ret.Text);

                            decimal Qty = 0;
                            if (txtQty_Ret.Text == "")
                            {
                                txtQty_Ret.Text = "0";
                            }
                            else
                                txtQty_Ret.Text = txtQty_Ret.Text;
                            Qty = Convert.ToDecimal(txtQty_Ret.Text);
                            TextBox txtRate_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtRate_Ret");
                            decimal Rate = 0;
                            Rate = Convert.ToDecimal(txtRate_Ret.Text);
                            TextBox txtAmount_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtAmount_Ret");
                            decimal Amount = 0;
                            Amount = Convert.ToDecimal(txtAmount_Ret.Text);

                            TextBox txtDisRt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtDisRt_Ret");
                            if (txtDisRt_Ret.Text == "")
                            {
                                txtDisRt_Ret.Text = "0";
                            }
                            else
                            {
                                txtDisRt_Ret.Text = txtDisRt_Ret.Text;
                            }
                            decimal disRt = 0;
                            disRt = Convert.ToDecimal(txtDisRt_Ret.Text);

                            TextBox txtDisAmt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtDisAmt_Ret");
                            if (txtDisAmt_Ret.Text == "")
                            {
                                txtDisAmt_Ret.Text = "0";
                            }
                            else
                            {
                                txtDisAmt_Ret.Text = txtDisAmt_Ret.Text;
                            }
                            decimal disAmt = 0;
                            disAmt = Convert.ToDecimal(txtDisAmt_Ret.Text);

                            TextBox txtNetAmt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtNetAmt_Ret");
                            if (txtNetAmt_Ret.Text == "")
                            {
                                txtNetAmt_Ret.Text = "0";
                            }
                            else
                            {
                                txtNetAmt_Ret.Text = txtNetAmt_Ret.Text;
                            }
                            decimal NetAmt = 0;
                            NetAmt = Convert.ToDecimal(txtNetAmt_Ret.Text);


                            Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'IRTS' and TRANSMY = '" + lblMy_Ret.Text + "'", lblTransSL_Ret);
                            string ItemCD;
                            string mxCD = "";
                            string mid = "";
                            string subItemCD = "";
                            int subCD, incrItCD;
                            if (lblTransSL_Ret.Text == "")
                            {
                                ItemCD = "00000001";
                            }
                            else
                            {
                                mxCD = lblTransSL_Ret.Text;
                                //OItemCD = mxCD.Substring(4,4);
                                subCD = int.Parse(mxCD);
                                incrItCD = subCD + 1;
                                if (incrItCD < 10)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0000000" + mid;
                                }
                                else if (incrItCD < 100)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "000000" + mid;
                                }
                                else if (incrItCD < 1000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "00000" + mid;
                                }
                                else if (incrItCD < 10000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0000" + mid;
                                }
                                else if (incrItCD < 100000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "000" + mid;
                                }
                                else if (incrItCD < 1000000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "00" + mid;
                                }
                                else if (incrItCD < 10000000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0" + mid;
                                }
                                //else
                                //    subItemCD = incrItCD.ToString();

                                ItemCD = subItemCD;
                            }

                            if (btnEdit_Ret.Text == "EDIT")
                            {
                                query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, DISCRT, DISCAMT, NETAMT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblMy_Ret.Text + "','" + txtInNo_Ret.Text + "','" + txtSLMNo_Ret.Text + "','','" + txtSlFr_Ret.Text + "','" + txtPID_Ret.Text + "','','','','" + txtRemarks_Ret.Text + "',@TRANSSL,'" + lblCatID_Ret.Text + "','" + txtItID_Ret.Text + "', " +
                                     " '" + ddlType_Ret.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + "," + disRt + "," + disAmt + "," + NetAmt + ",'',@USERID,'')");

                                comm = new SqlCommand(query, conn);
                                comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                                comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                                // comm.Parameters.AddWithValue("@LCDATE", lcDate);
                                comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                                comm.Parameters.AddWithValue("@USERID", userName);

                                conn.Open();
                                int result = comm.ExecuteNonQuery();
                                conn.Close();

                                GridShow_Ret_Complete();         /////for return complete

                            }
                            else
                            {
                                Int64 TransSaleEdit = Convert.ToInt64(ddlSalesEditInNo_Ret.Text);
                                conn.Open();
                                SqlCommand cmd1 = new SqlCommand("select * from STK_TRANSMST where TRANSTP = 'IRTS' and TRANSMY='" + lblMy_Ret.Text + "' and TRANSNO =" + TransSaleEdit + "", conn);
                                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                                DataSet ds1 = new DataSet();
                                da1.Fill(ds1);
                                conn.Close();
                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, DISCRT, DISCAMT, NETAMT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblMy_Ret.Text + "','" + ddlSalesEditInNo_Ret.Text + "','" + txtSLMNo_Ret.Text + "','','" + txtSlFr_Ret.Text + "','" + txtPID_Ret.Text + "','','','','" + txtRemarks_Ret.Text + "',@TRANSSL,'" + lblCatID_Ret.Text + "','" + txtItID_Ret.Text + "', " +
                                     " '" + ddlType_Ret.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ", " + disRt + "," + disAmt + "," + NetAmt + ",'',@USERID,'')");

                                    comm = new SqlCommand(query, conn);
                                    comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                                    comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                                    // comm.Parameters.AddWithValue("@LCDATE", lcDate);
                                    comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                                    comm.Parameters.AddWithValue("@USERID", userName);

                                    conn.Open();
                                    int result = comm.ExecuteNonQuery();
                                    conn.Close();

                                    GridShow_Ret_CompleteEdit();        ///////////// for return complete edit
                                }
                                else
                                {
                                    lblGridMsg_Ret.Visible = true;
                                    lblGridMsg_Ret.Text = "Must be in New Mode.";
                                }

                            }
                        }
                        else
                        {
                            query = "insert into STK_TRANSMST ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS, USERPC, USERID, IPADDRESS) " +
                                        "values(@TRANSTP,@TRANSDT,'" + lblMy_Ret.Text + "','" + txtInNo_Ret.Text + "','" + txtSLMNo_Ret.Text + "','','" + txtSlFr_Ret.Text + "','" + txtPID_Ret.Text + "','','','','" + txtRemarks_Ret.Text + "','',@USERID,'')";
                            comm = new SqlCommand(query, conn);
                            comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                            comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                            //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                            comm.Parameters.AddWithValue("@USERID", userName);
                            conn.Open();
                            comm.ExecuteNonQuery();
                            conn.Close();


                            Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM_Ret.Text + "'", lblCatID_Ret);

                            DropDownList ddlType_Ret = (DropDownList)gvDetail_Ret.FooterRow.FindControl("ddlType_Ret");
                            TextBox txtCPQTY_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtCPQTY_Ret");
                            if (txtCPQTY_Ret.Text == "")
                            {
                                txtCPQTY_Ret.Text = "0";
                            }
                            else
                                txtCPQTY_Ret.Text = txtCPQTY_Ret.Text;
                            decimal cpQty = 0;
                            cpQty = Convert.ToDecimal(txtCPQTY_Ret.Text);
                            TextBox txtCQty_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtCQty_Ret");
                            if (txtCQty_Ret.Text == "")
                            {
                                txtCQty_Ret.Text = "0";
                            }
                            else
                                txtCQty_Ret.Text = txtCQty_Ret.Text;
                            decimal cQty = 0;
                            cQty = Convert.ToDecimal(txtCQty_Ret.Text);
                            decimal pQty = 0;
                            if (txtPQty_Ret.Text == "")
                            {
                                txtPQty_Ret.Text = "0";
                            }
                            else
                                txtPQty_Ret.Text = txtPQty_Ret.Text;
                            pQty = Convert.ToDecimal(txtPQty_Ret.Text);

                            decimal Qty = 0;
                            if (txtQty_Ret.Text == "")
                            {
                                txtQty_Ret.Text = "0";
                            }
                            else
                                txtQty_Ret.Text = txtQty_Ret.Text;
                            Qty = Convert.ToDecimal(txtQty_Ret.Text);
                            TextBox txtRate_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtRate_Ret");
                            decimal Rate = 0;
                            Rate = Convert.ToDecimal(txtRate_Ret.Text);
                            TextBox txtAmount_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtAmount_Ret");
                            decimal Amount = 0;
                            Amount = Convert.ToDecimal(txtAmount_Ret.Text);

                            TextBox txtDisRt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtDisRt_Ret");
                            if (txtDisRt_Ret.Text == "")
                            {
                                txtDisRt_Ret.Text = "0";
                            }
                            else
                            {
                                txtDisRt_Ret.Text = txtDisRt_Ret.Text;
                            }
                            decimal disRt = 0;
                            disRt = Convert.ToDecimal(txtDisRt_Ret.Text);

                            TextBox txtDisAmt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtDisAmt_Ret");
                            if (txtDisAmt_Ret.Text == "")
                            {
                                txtDisAmt_Ret.Text = "0";
                            }
                            else
                            {
                                txtDisAmt_Ret.Text = txtDisAmt_Ret.Text;
                            }
                            decimal disAmt = 0;
                            disAmt = Convert.ToDecimal(txtDisAmt_Ret.Text);

                            TextBox txtNetAmt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtNetAmt_Ret");
                            if (txtNetAmt_Ret.Text == "")
                            {
                                txtNetAmt_Ret.Text = "0";
                            }
                            else
                            {
                                txtNetAmt_Ret.Text = txtNetAmt_Ret.Text;
                            }
                            decimal NetAmt = 0;
                            NetAmt = Convert.ToDecimal(txtNetAmt_Ret.Text);

                            Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'IRTS' and TRANSMY = '" + lblMy_Ret.Text + "'", lblTransSL_Ret);
                            string ItemCD;
                            string mxCD = "";
                            string mid = "";
                            string subItemCD = "";
                            int subCD, incrItCD;
                            if (lblTransSL_Ret.Text == "")
                            {
                                ItemCD = "00000001";
                            }
                            else
                            {
                                mxCD = lblTransSL_Ret.Text;
                                //OItemCD = mxCD.Substring(4,4);
                                subCD = int.Parse(mxCD);
                                incrItCD = subCD + 1;
                                if (incrItCD < 10)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0000000" + mid;
                                }
                                else if (incrItCD < 100)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "000000" + mid;
                                }
                                else if (incrItCD < 1000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "00000" + mid;
                                }
                                else if (incrItCD < 10000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0000" + mid;
                                }
                                else if (incrItCD < 100000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "000" + mid;
                                }
                                else if (incrItCD < 1000000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "00" + mid;
                                }
                                else if (incrItCD < 10000000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0" + mid;
                                }
                                //else
                                //    subItemCD = incrItCD.ToString();

                                ItemCD = subItemCD;
                            }

                            query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, DISCRT, DISCAMT, NETAMT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblMy_Ret.Text + "','" + txtInNo_Ret.Text + "','" + txtSLMNo_Ret.Text + "','','" + txtSlFr_Ret.Text + "','" + txtPID_Ret.Text + "','','','','" + txtRemarks_Ret.Text + "',@TRANSSL,'" + lblCatID_Ret.Text + "','" + txtItID_Ret.Text + "', " +
                                     " '" + ddlType_Ret.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + "," + disRt + "," + disAmt + "," + NetAmt + ",'',@USERID,'')");

                            comm = new SqlCommand(query, conn);
                            comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                            comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                            //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                            comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                            comm.Parameters.AddWithValue("@USERID", userName);

                            conn.Open();
                            int result = comm.ExecuteNonQuery();
                            conn.Close();

                            GridShow_Ret_Complete();
                        }
                    }

                    if (btnEdit_Ret.Text == "EDIT") ////new mode
                    {
                        Decimal totAmt = 0;
                        Decimal a = 0;
                        decimal tAmt = 0;
                        decimal dis = 0;
                        decimal disAmt = 0;
                        decimal amt = 0;
                        decimal Amount = 0;
                        if (gvDetail_Ret.EditIndex == -1)
                        {
                            foreach (GridViewRow grid in gvDetail_Ret.Rows)
                            {
                                Label lblNetAmt_Ret = (Label)grid.Cells[11].FindControl("lblNetAmt_Ret");

                                if (lblNetAmt_Ret.Text == "")
                                {
                                    lblNetAmt_Ret.Text = "0";
                                }
                                else
                                {
                                    lblNetAmt_Ret.Text = lblNetAmt_Ret.Text;
                                }
                                String TotalAmount = lblNetAmt_Ret.Text;
                                totAmt = Convert.ToDecimal(TotalAmount);
                                a += totAmt;
                                string tAmount = SpellAmount.comma(a);
                                txtTotAmt_Ret.Text = tAmount;
                                txtNetAmt_Ret.Text = tAmount;
                                txtTotal_Ret.Text = a.ToString();
                                tAmt = a;

                                Label lblAmount_Ret = (Label)grid.Cells[8].FindControl("lblAmount_Ret");
                                Label lblDisAmt_Ret = (Label)grid.Cells[10].FindControl("lblDisAmt_Ret");

                                if (lblAmount_Ret.Text == "")
                                {
                                    lblAmount_Ret.Text = "0.00";
                                }
                                else
                                    lblAmount_Ret.Text = lblAmount_Ret.Text;

                                amt = Convert.ToDecimal(lblAmount_Ret.Text);
                                Amount += amt;
                                txtTAmount_Ret.Text = Amount.ToString();

                                if (lblDisAmt_Ret.Text == "")
                                {
                                    lblDisAmt_Ret.Text = "0.00";
                                }
                                else
                                    lblDisAmt_Ret.Text = lblDisAmt_Ret.Text;

                                dis = Convert.ToDecimal(lblDisAmt_Ret.Text);
                                disAmt += dis;
                                txtTDisAmount_Ret.Text = disAmt.ToString();
                            }
                            a += totAmt;
                            Amount += amt;
                            disAmt += dis;
                            //}
                        }
                        else
                        {

                        }

                        txtGrossDisAmt_Ret.Focus();
                    }
                    else ////// edit mode
                    {
                        Decimal totAmt = 0;
                        Decimal a = 0;
                        decimal tAmt = 0;
                        decimal dis = 0;
                        decimal disAmt = 0;
                        decimal amt = 0;
                        decimal Amount = 0;
                        if (gvDetail_Ret.EditIndex == -1)
                        {
                            foreach (GridViewRow grid in gvDetail_Ret.Rows)
                            {
                                Label lblNetAmt_Ret = (Label)grid.Cells[11].FindControl("lblNetAmt_Ret");

                                if (lblNetAmt_Ret.Text == "")
                                {
                                    lblNetAmt_Ret.Text = "0";
                                }
                                else
                                {
                                    lblNetAmt_Ret.Text = lblNetAmt_Ret.Text;
                                }
                                String TotalAmount = lblNetAmt_Ret.Text;
                                totAmt = Convert.ToDecimal(TotalAmount);
                                a += totAmt;
                                string tAmount = SpellAmount.comma(a);
                                txtTotAmt_Ret.Text = tAmount;
                                txtNetAmt_Ret.Text = tAmount;
                                txtTotal_Ret.Text = a.ToString();
                                tAmt = a;

                                Label lblAmount_Ret = (Label)grid.Cells[8].FindControl("lblAmount_Ret");
                                Label lblDisAmt_Ret = (Label)grid.Cells[10].FindControl("lblDisAmt_Ret");

                                if (lblAmount_Ret.Text == "")
                                {
                                    lblAmount_Ret.Text = "0.00";
                                }
                                else
                                    lblAmount_Ret.Text = lblAmount_Ret.Text;

                                amt = Convert.ToDecimal(lblAmount_Ret.Text);
                                Amount += amt;
                                txtTAmount_Ret.Text = Amount.ToString();

                                if (lblDisAmt_Ret.Text == "")
                                {
                                    lblDisAmt_Ret.Text = "0.00";
                                }
                                else
                                    lblDisAmt_Ret.Text = lblDisAmt_Ret.Text;

                                dis = Convert.ToDecimal(lblDisAmt_Ret.Text);
                                disAmt += dis;
                                txtTDisAmount_Ret.Text = disAmt.ToString();
                            }
                            a += totAmt;
                            Amount += amt;
                            disAmt += dis;
                            //}
                        }
                        else
                        {

                        }

                        txtGrossDisAmt_Ret.Focus();
                    }
                }
            }
            else  //////////////////////////for purchase
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();
                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);

                string query = "";
                SqlCommand comm = new SqlCommand(query, conn);

                string Transtp = "IRTB";


                DateTime TransDT = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDt = TransDT.ToString("yyyy/MM/dd");

                if (e.CommandName.Equals("SaveCon"))
                {
                    TextBox txtItID_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtItID_Ret");
                    TextBox txtItemNM_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtItemNM_Ret");
                    TextBox txtQty_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtQty_Ret");
                    TextBox txtPQty_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtPQty_Ret");

                    if (txtSlFr_Ret.Text == "")
                    {
                        lblSaleFrom_Ret.Visible = true;
                        lblSaleFrom_Ret.Text = "Select Store.";
                        txtSaleFrom_Ret.Focus();
                        lblPartyID_Ret.Visible = false;
                        lblGridMsg_Ret.Visible = false;
                    }
                    else if (txtPID_Ret.Text == "")
                    {
                        lblPartyID_Ret.Visible = true;
                        lblPartyID_Ret.Text = "Select Party";
                        txtPSNM_Ret.Focus();
                        lblSaleFrom.Visible = false;
                        lblGridMsg.Visible = false;
                    }
                    else if (txtItID_Ret.Text == "")
                    {
                        lblGridMsg_Ret.Visible = true;
                        lblGridMsg_Ret.Text = "Select Item.";
                        txtItemNM_Ret.Focus();
                        lblSaleFrom_Ret.Visible = false;
                        lblPartyID_Ret.Visible = false;
                    }
                    else if (txtQty_Ret.Text == "0")
                    {
                        lblGridMsg_Ret.Visible = true;
                        lblGridMsg_Ret.Text = "Quantity is Wrong.";
                        txtPQty_Ret.Focus();
                    }
                    else
                    {
                        lblSaleFrom_Ret.Visible = false;
                        lblPartyID_Ret.Visible = false;
                        lblGridMsg_Ret.Visible = false;

                        conn.Open();
                        SqlCommand cmd = new SqlCommand();
                        if (btnEdit_Ret.Text == "EDIT")
                        {
                            //Global.txtAdd("SELECT MAX(TRANSNO) AS TRANSNO FROM STK_TRANSMST WHERE TRANSMY='" + lblSMY.Text + "' AND TRANSTP='SALE'", txtInNo);
                            cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + txtInNo_Ret.Text + "' and TRANSTP='IRTB' AND TRANSMY='" + lblMy_Ret.Text + "'", conn);
                        }
                        else
                        {
                            cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + ddlSalesEditInNo_Ret.Text + "' and TRANSTP='IRTB' AND TRANSMY='" + lblMy_Ret.Text + "'", conn);
                        }
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        conn.Close();
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM_Ret.Text + "'", lblCatID_Ret);

                            DropDownList ddlType_Ret = (DropDownList)gvDetail_Ret.FooterRow.FindControl("ddlType_Ret");
                            TextBox txtCPQTY_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtCPQTY_Ret");
                            if (txtCPQTY_Ret.Text == "")
                            {
                                txtCPQTY_Ret.Text = "0";
                            }
                            else
                                txtCPQTY_Ret.Text = txtCPQTY_Ret.Text;
                            decimal cpQty = 0;
                            cpQty = Convert.ToDecimal(txtCPQTY_Ret.Text);
                            TextBox txtCQty_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtCQty_Ret");
                            if (txtCQty_Ret.Text == "")
                            {
                                txtCQty_Ret.Text = "0";
                            }
                            else
                                txtCQty_Ret.Text = txtCQty_Ret.Text;
                            decimal cQty = 0;
                            cQty = Convert.ToDecimal(txtCQty_Ret.Text);
                            decimal pQty = 0;
                            if (txtPQty_Ret.Text == "")
                            {
                                txtPQty_Ret.Text = "0";
                            }
                            else
                                txtPQty_Ret.Text = txtPQty_Ret.Text;
                            pQty = Convert.ToDecimal(txtPQty_Ret.Text);

                            decimal Qty = 0;
                            if (txtQty_Ret.Text == "")
                            {
                                txtQty_Ret.Text = "0";
                            }
                            else
                                txtQty_Ret.Text = txtQty_Ret.Text;
                            Qty = Convert.ToDecimal(txtQty_Ret.Text);
                            TextBox txtRate_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtRate_Ret");
                            decimal Rate = 0;
                            Rate = Convert.ToDecimal(txtRate_Ret.Text);
                            TextBox txtAmount_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtAmount_Ret");
                            decimal Amount = 0;
                            Amount = Convert.ToDecimal(txtAmount_Ret.Text);

                            TextBox txtDisRt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtDisRt_Ret");
                            if (txtDisRt_Ret.Text == "")
                            {
                                txtDisRt_Ret.Text = "0";
                            }
                            else
                            {
                                txtDisRt_Ret.Text = txtDisRt_Ret.Text;
                            }
                            decimal disRt = 0;
                            disRt = Convert.ToDecimal(txtDisRt_Ret.Text);

                            TextBox txtDisAmt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtDisAmt_Ret");
                            if (txtDisAmt_Ret.Text == "")
                            {
                                txtDisAmt_Ret.Text = "0";
                            }
                            else
                            {
                                txtDisAmt_Ret.Text = txtDisAmt_Ret.Text;
                            }
                            decimal disAmt = 0;
                            disAmt = Convert.ToDecimal(txtDisAmt_Ret.Text);

                            TextBox txtNetAmt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtNetAmt_Ret");
                            if (txtNetAmt_Ret.Text == "")
                            {
                                txtNetAmt_Ret.Text = "0";
                            }
                            else
                            {
                                txtNetAmt_Ret.Text = txtNetAmt_Ret.Text;
                            }
                            decimal NetAmt = 0;
                            NetAmt = Convert.ToDecimal(txtNetAmt_Ret.Text);


                            Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'IRTB' and TRANSMY = '" + lblMy_Ret.Text + "'", lblTransSL_Ret);
                            string ItemCD;
                            string mxCD = "";
                            string mid = "";
                            string subItemCD = "";
                            int subCD, incrItCD;
                            if (lblTransSL_Ret.Text == "")
                            {
                                ItemCD = "00000001";
                            }
                            else
                            {
                                mxCD = lblTransSL_Ret.Text;
                                //OItemCD = mxCD.Substring(4,4);
                                subCD = int.Parse(mxCD);
                                incrItCD = subCD + 1;
                                if (incrItCD < 10)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0000000" + mid;
                                }
                                else if (incrItCD < 100)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "000000" + mid;
                                }
                                else if (incrItCD < 1000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "00000" + mid;
                                }
                                else if (incrItCD < 10000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0000" + mid;
                                }
                                else if (incrItCD < 100000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "000" + mid;
                                }
                                else if (incrItCD < 1000000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "00" + mid;
                                }
                                else if (incrItCD < 10000000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0" + mid;
                                }
                                //else
                                //    subItemCD = incrItCD.ToString();

                                ItemCD = subItemCD;
                            }

                            if (btnEdit_Ret.Text == "EDIT")
                            {
                                query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT,  DISCRT, DISCAMT, NETAMT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblMy_Ret.Text + "','" + txtInNo_Ret.Text + "','" + txtSLMNo_Ret.Text + "','" + txtSlFr_Ret.Text + "','','" + txtPID_Ret.Text + "','','','','" + txtRemarks_Ret.Text + "',@TRANSSL,'" + lblCatID_Ret.Text + "','" + txtItID_Ret.Text + "', " +
                                     " '" + ddlType_Ret.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + "," + disRt + "," + disAmt + "," + NetAmt + ",'',@USERID,'')");

                                comm = new SqlCommand(query, conn);
                                comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                                comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                                // comm.Parameters.AddWithValue("@LCDATE", lcDate);
                                comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                                comm.Parameters.AddWithValue("@USERID", userName);

                                conn.Open();
                                int result = comm.ExecuteNonQuery();
                                conn.Close();

                                GridShow_Ret();
                            }
                            else
                            {
                                Int64 TransSaleEdit = Convert.ToInt64(ddlSalesEditInNo_Ret.Text);
                                conn.Open();
                                SqlCommand cmd1 = new SqlCommand("select * from STK_TRANSMST where TRANSTP = 'IRTB' and TRANSMY='" + lblMy_Ret.Text + "' and TRANSNO =" + TransSaleEdit + "", conn);
                                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                                DataSet ds1 = new DataSet();
                                da.Fill(ds1);
                                conn.Close();
                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, DISCRT, DISCAMT, NETAMT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblMy_Ret.Text + "','" + ddlSalesEditInNo_Ret.Text + "','" + txtSLMNo_Ret.Text + "','" + txtSlFr_Ret.Text + "','','" + txtPID_Ret.Text + "','','','','" + txtRemarks_Ret.Text + "',@TRANSSL,'" + lblCatID_Ret.Text + "','" + txtItID_Ret.Text + "', " +
                                     " '" + ddlType_Ret.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ", " + disRt + "," + disAmt + "," + NetAmt + ",'',@USERID,'')");

                                    comm = new SqlCommand(query, conn);
                                    comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                                    comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                                    // comm.Parameters.AddWithValue("@LCDATE", lcDate);
                                    comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                                    comm.Parameters.AddWithValue("@USERID", userName);

                                    conn.Open();
                                    int result = comm.ExecuteNonQuery();
                                    conn.Close();

                                    GridShow_Ret_Edit();
                                }
                                else
                                {
                                    lblGridMsg_Ret.Visible = true;
                                    lblGridMsg_Ret.Text = "Must be in New Mode.";
                                }

                            }
                        }
                        else
                        {
                            query = "insert into STK_TRANSMST ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS, USERPC, USERID, IPADDRESS) " +
                                        "values(@TRANSTP,@TRANSDT,'" + lblMy_Ret.Text + "','" + txtInNo_Ret.Text + "','" + txtSLMNo_Ret.Text + "','" + txtSlFr_Ret.Text + "','','" + txtPID_Ret.Text + "','','','','" + txtRemarks_Ret.Text + "','',@USERID,'')";
                            comm = new SqlCommand(query, conn);
                            comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                            comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                            //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                            comm.Parameters.AddWithValue("@USERID", userName);
                            conn.Open();
                            comm.ExecuteNonQuery();
                            conn.Close();


                            Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM_Ret.Text + "'", lblCatID_Ret);

                            DropDownList ddlType_Ret = (DropDownList)gvDetail_Ret.FooterRow.FindControl("ddlType_Ret");
                            TextBox txtCPQTY_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtCPQTY_Ret");
                            if (txtCPQTY_Ret.Text == "")
                            {
                                txtCPQTY_Ret.Text = "0";
                            }
                            else
                                txtCPQTY_Ret.Text = txtCPQTY_Ret.Text;
                            decimal cpQty = 0;
                            cpQty = Convert.ToDecimal(txtCPQTY_Ret.Text);
                            TextBox txtCQty_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtCQty_Ret");
                            if (txtCQty_Ret.Text == "")
                            {
                                txtCQty_Ret.Text = "0";
                            }
                            else
                                txtCQty_Ret.Text = txtCQty_Ret.Text;
                            decimal cQty = 0;
                            cQty = Convert.ToDecimal(txtCQty_Ret.Text);
                            decimal pQty = 0;
                            if (txtPQty_Ret.Text == "")
                            {
                                txtPQty_Ret.Text = "0";
                            }
                            else
                                txtPQty_Ret.Text = txtPQty_Ret.Text;
                            pQty = Convert.ToDecimal(txtPQty_Ret.Text);

                            decimal Qty = 0;
                            if (txtQty_Ret.Text == "")
                            {
                                txtQty_Ret.Text = "0";
                            }
                            else
                                txtQty_Ret.Text = txtQty_Ret.Text;
                            Qty = Convert.ToDecimal(txtQty_Ret.Text);
                            TextBox txtRate_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtRate_Ret");
                            decimal Rate = 0;
                            Rate = Convert.ToDecimal(txtRate_Ret.Text);
                            TextBox txtAmount_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtAmount_Ret");
                            decimal Amount = 0;
                            Amount = Convert.ToDecimal(txtAmount_Ret.Text);

                            TextBox txtDisRt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtDisRt_Ret");
                            if (txtDisRt_Ret.Text == "")
                            {
                                txtDisRt_Ret.Text = "0";
                            }
                            else
                            {
                                txtDisRt_Ret.Text = txtDisRt_Ret.Text;
                            }
                            decimal disRt = 0;
                            disRt = Convert.ToDecimal(txtDisRt_Ret.Text);

                            TextBox txtDisAmt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtDisAmt_Ret");
                            if (txtDisAmt_Ret.Text == "")
                            {
                                txtDisAmt_Ret.Text = "0";
                            }
                            else
                            {
                                txtDisAmt_Ret.Text = txtDisAmt_Ret.Text;
                            }
                            decimal disAmt = 0;
                            disAmt = Convert.ToDecimal(txtDisAmt_Ret.Text);

                            TextBox txtNetAmt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtNetAmt_Ret");
                            if (txtNetAmt_Ret.Text == "")
                            {
                                txtNetAmt_Ret.Text = "0";
                            }
                            else
                            {
                                txtNetAmt_Ret.Text = txtNetAmt_Ret.Text;
                            }
                            decimal NetAmt = 0;
                            NetAmt = Convert.ToDecimal(txtNetAmt_Ret.Text);

                            Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'IRTB' and TRANSMY = '" + lblMy_Ret.Text + "'", lblTransSL_Ret);
                            string ItemCD;
                            string mxCD = "";
                            string mid = "";
                            string subItemCD = "";
                            int subCD, incrItCD;
                            if (lblTransSL_Ret.Text == "")
                            {
                                ItemCD = "00000001";
                            }
                            else
                            {
                                mxCD = lblTransSL_Ret.Text;
                                //OItemCD = mxCD.Substring(4,4);
                                subCD = int.Parse(mxCD);
                                incrItCD = subCD + 1;
                                if (incrItCD < 10)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0000000" + mid;
                                }
                                else if (incrItCD < 100)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "000000" + mid;
                                }
                                else if (incrItCD < 1000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "00000" + mid;
                                }
                                else if (incrItCD < 10000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0000" + mid;
                                }
                                else if (incrItCD < 100000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "000" + mid;
                                }
                                else if (incrItCD < 1000000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "00" + mid;
                                }
                                else if (incrItCD < 10000000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0" + mid;
                                }
                                //else
                                //    subItemCD = incrItCD.ToString();

                                ItemCD = subItemCD;
                            }

                            query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT,  DISCRT, DISCAMT, NETAMT, USERPC, USERID, IPADDRESS ) " +
                                         "values(@TRANSTP,@TRANSDT,'" + lblMy_Ret.Text + "','" + txtInNo_Ret.Text + "','" + txtSLMNo_Ret.Text + "','" + txtSlFr_Ret.Text + "','','" + txtPID_Ret.Text + "','','','','" + txtRemarks_Ret.Text + "',@TRANSSL,'" + lblCatID_Ret.Text + "','" + txtItID_Ret.Text + "', " +
                                         " '" + ddlType_Ret.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + "," + disRt + "," + disAmt + "," + NetAmt + ",'',@USERID,'')");

                            comm = new SqlCommand(query, conn);
                            comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                            comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                            //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                            comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                            comm.Parameters.AddWithValue("@USERID", userName);

                            conn.Open();
                            int result = comm.ExecuteNonQuery();
                            conn.Close();

                            GridShow_Ret();

                        }
                    }
                }


                    ////////////////////////////////////////////////////////// For Complete   //////////////////////////////////////

                else if (e.CommandName.Equals("Complete"))
                {
                    TextBox txtItID_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtItID_Ret");
                    TextBox txtItemNM_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtItemNM_Ret");
                    TextBox txtQty_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtQty_Ret");
                    TextBox txtPQty_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtPQty_Ret");
                    if (txtSlFr_Ret.Text == "")
                    {
                        lblSaleFrom_Ret.Visible = true;
                        lblSaleFrom_Ret.Text = "Select Store.";
                        txtSaleFrom_Ret.Focus();
                        lblPartyID_Ret.Visible = false;
                        lblGridMsg_Ret.Visible = false;
                    }
                    else if (txtPID_Ret.Text == "")
                    {
                        lblPartyID_Ret.Visible = true;
                        lblPartyID_Ret.Text = "Select Party";
                        txtPSNM_Ret.Focus();
                        lblSaleFrom_Ret.Visible = false;
                        lblGridMsg_Ret.Visible = false;
                    }
                    else if (txtItID_Ret.Text == "")
                    {
                        lblGridMsg_Ret.Visible = true;
                        lblGridMsg_Ret.Text = "Select Item.";
                        txtItemNM_Ret.Focus();
                        lblSaleFrom_Ret.Visible = false;
                        lblPartyID_Ret.Visible = false;
                    }
                    else if (txtQty_Ret.Text == "0")
                    {
                        lblGridMsg_Ret.Visible = true;
                        lblGridMsg_Ret.Text = "Quantity is Wrong.";
                        txtPQty_Ret.Focus();
                    }
                    else
                    {
                        lblSaleFrom_Ret.Visible = false;
                        lblPartyID_Ret.Visible = false;
                        lblGridMsg_Ret.Visible = false;

                        conn.Open();
                        SqlCommand cmd = new SqlCommand();

                        if (btnEdit_Ret.Text == "EDIT")
                        {
                            cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + txtInNo_Ret.Text + "' and TRANSTP='IRTB' AND TRANSMY='" + lblMy_Ret.Text + "'", conn);
                        }
                        else
                        {
                            cmd = new SqlCommand("Select TRANSNO from STK_TRANSMST where TRANSNO='" + ddlSalesEditInNo_Ret.Text + "' and TRANSTP='IRTB' AND TRANSMY='" + lblMy_Ret.Text + "'", conn);
                        }
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        conn.Close();
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM_Ret.Text + "'", lblCatID_Ret);

                            DropDownList ddlType_Ret = (DropDownList)gvDetail_Ret.FooterRow.FindControl("ddlType_Ret");
                            TextBox txtCPQTY_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtCPQTY_Ret");
                            if (txtCPQTY_Ret.Text == "")
                            {
                                txtCPQTY_Ret.Text = "0";
                            }
                            else
                                txtCPQTY_Ret.Text = txtCPQTY_Ret.Text;
                            decimal cpQty = 0;
                            cpQty = Convert.ToDecimal(txtCPQTY_Ret.Text);
                            TextBox txtCQty_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtCQty_Ret");
                            if (txtCQty_Ret.Text == "")
                            {
                                txtCQty_Ret.Text = "0";
                            }
                            else
                                txtCQty_Ret.Text = txtCQty_Ret.Text;
                            decimal cQty = 0;
                            cQty = Convert.ToDecimal(txtCQty_Ret.Text);
                            decimal pQty = 0;
                            if (txtPQty_Ret.Text == "")
                            {
                                txtPQty_Ret.Text = "0";
                            }
                            else
                                txtPQty_Ret.Text = txtPQty_Ret.Text;
                            pQty = Convert.ToDecimal(txtPQty_Ret.Text);

                            decimal Qty = 0;
                            if (txtQty_Ret.Text == "")
                            {
                                txtQty_Ret.Text = "0";
                            }
                            else
                                txtQty_Ret.Text = txtQty_Ret.Text;
                            Qty = Convert.ToDecimal(txtQty_Ret.Text);
                            TextBox txtRate_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtRate_Ret");
                            decimal Rate = 0;
                            Rate = Convert.ToDecimal(txtRate_Ret.Text);
                            TextBox txtAmount_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtAmount_Ret");
                            decimal Amount = 0;
                            Amount = Convert.ToDecimal(txtAmount_Ret.Text);

                            TextBox txtDisRt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtDisRt_Ret");
                            if (txtDisRt_Ret.Text == "")
                            {
                                txtDisRt_Ret.Text = "0";
                            }
                            else
                            {
                                txtDisRt_Ret.Text = txtDisRt_Ret.Text;
                            }
                            decimal disRt = 0;
                            disRt = Convert.ToDecimal(txtDisRt_Ret.Text);

                            TextBox txtDisAmt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtDisAmt_Ret");
                            if (txtDisAmt_Ret.Text == "")
                            {
                                txtDisAmt_Ret.Text = "0";
                            }
                            else
                            {
                                txtDisAmt_Ret.Text = txtDisAmt_Ret.Text;
                            }
                            decimal disAmt = 0;
                            disAmt = Convert.ToDecimal(txtDisAmt_Ret.Text);

                            TextBox txtNetAmt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtNetAmt_Ret");
                            if (txtNetAmt_Ret.Text == "")
                            {
                                txtNetAmt_Ret.Text = "0";
                            }
                            else
                            {
                                txtNetAmt_Ret.Text = txtNetAmt_Ret.Text;
                            }
                            decimal NetAmt = 0;
                            NetAmt = Convert.ToDecimal(txtNetAmt_Ret.Text);


                            Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'IRTB' and TRANSMY = '" + lblMy_Ret.Text + "'", lblTransSL_Ret);
                            string ItemCD;
                            string mxCD = "";
                            string mid = "";
                            string subItemCD = "";
                            int subCD, incrItCD;
                            if (lblTransSL_Ret.Text == "")
                            {
                                ItemCD = "00000001";
                            }
                            else
                            {
                                mxCD = lblTransSL_Ret.Text;
                                //OItemCD = mxCD.Substring(4,4);
                                subCD = int.Parse(mxCD);
                                incrItCD = subCD + 1;
                                if (incrItCD < 10)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0000000" + mid;
                                }
                                else if (incrItCD < 100)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "000000" + mid;
                                }
                                else if (incrItCD < 1000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "00000" + mid;
                                }
                                else if (incrItCD < 10000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0000" + mid;
                                }
                                else if (incrItCD < 100000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "000" + mid;
                                }
                                else if (incrItCD < 1000000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "00" + mid;
                                }
                                else if (incrItCD < 10000000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0" + mid;
                                }
                                //else
                                //    subItemCD = incrItCD.ToString();

                                ItemCD = subItemCD;
                            }

                            if (btnEdit_Ret.Text == "EDIT")
                            {
                                query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, DISCRT, DISCAMT, NETAMT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblMy_Ret.Text + "','" + txtInNo_Ret.Text + "','" + txtSLMNo_Ret.Text + "','" + txtSlFr_Ret.Text + "','','" + txtPID_Ret.Text + "','','','','" + txtRemarks_Ret.Text + "',@TRANSSL,'" + lblCatID_Ret.Text + "','" + txtItID_Ret.Text + "', " +
                                     " '" + ddlType_Ret.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + "," + disRt + "," + disAmt + "," + NetAmt + ",'',@USERID,'')");

                                comm = new SqlCommand(query, conn);
                                comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                                comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                                // comm.Parameters.AddWithValue("@LCDATE", lcDate);
                                comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                                comm.Parameters.AddWithValue("@USERID", userName);

                                conn.Open();
                                int result = comm.ExecuteNonQuery();
                                conn.Close();

                                GridShow_Ret_Complete();         /////for return complete

                            }
                            else
                            {
                                Int64 TransSaleEdit = Convert.ToInt64(ddlSalesEditInNo_Ret.Text);
                                conn.Open();
                                SqlCommand cmd1 = new SqlCommand("select * from STK_TRANSMST where TRANSTP = 'IRTB' and TRANSMY='" + lblMy_Ret.Text + "' and TRANSNO =" + TransSaleEdit + "", conn);
                                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                                DataSet ds1 = new DataSet();
                                da1.Fill(ds1);
                                conn.Close();
                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, DISCRT, DISCAMT, NETAMT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblMy_Ret.Text + "','" + ddlSalesEditInNo_Ret.Text + "','" + txtSLMNo_Ret.Text + "','" + txtSlFr_Ret.Text + "','','" + txtPID_Ret.Text + "','','','','" + txtRemarks_Ret.Text + "',@TRANSSL,'" + lblCatID_Ret.Text + "','" + txtItID_Ret.Text + "', " +
                                     " '" + ddlType_Ret.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + ", " + disRt + "," + disAmt + "," + NetAmt + ",'',@USERID,'')");

                                    comm = new SqlCommand(query, conn);
                                    comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                                    comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                                    // comm.Parameters.AddWithValue("@LCDATE", lcDate);
                                    comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                                    comm.Parameters.AddWithValue("@USERID", userName);

                                    conn.Open();
                                    int result = comm.ExecuteNonQuery();
                                    conn.Close();

                                    GridShow_Ret_CompleteEdit();        ///////////// for return complete edit
                                }
                                else
                                {
                                    lblGridMsg_Ret.Visible = true;
                                    lblGridMsg_Ret.Text = "Must be in New Mode.";
                                }

                            }
                        }
                        else
                        {
                            query = "insert into STK_TRANSMST ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS, USERPC, USERID, IPADDRESS) " +
                                        "values(@TRANSTP,@TRANSDT,'" + lblMy_Ret.Text + "','" + txtInNo_Ret.Text + "','" + txtSLMNo_Ret.Text + "','" + txtSlFr_Ret.Text + "','','" + txtPID_Ret.Text + "','','','','" + txtRemarks_Ret.Text + "','',@USERID,'')";
                            comm = new SqlCommand(query, conn);
                            comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                            comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                            //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                            comm.Parameters.AddWithValue("@USERID", userName);
                            conn.Open();
                            comm.ExecuteNonQuery();
                            conn.Close();


                            Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNM_Ret.Text + "'", lblCatID_Ret);

                            DropDownList ddlType_Ret = (DropDownList)gvDetail_Ret.FooterRow.FindControl("ddlType_Ret");
                            TextBox txtCPQTY_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtCPQTY_Ret");
                            if (txtCPQTY_Ret.Text == "")
                            {
                                txtCPQTY_Ret.Text = "0";
                            }
                            else
                                txtCPQTY_Ret.Text = txtCPQTY_Ret.Text;
                            decimal cpQty = 0;
                            cpQty = Convert.ToDecimal(txtCPQTY_Ret.Text);
                            TextBox txtCQty_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtCQty_Ret");
                            if (txtCQty_Ret.Text == "")
                            {
                                txtCQty_Ret.Text = "0";
                            }
                            else
                                txtCQty_Ret.Text = txtCQty_Ret.Text;
                            decimal cQty = 0;
                            cQty = Convert.ToDecimal(txtCQty_Ret.Text);
                            decimal pQty = 0;
                            if (txtPQty_Ret.Text == "")
                            {
                                txtPQty_Ret.Text = "0";
                            }
                            else
                                txtPQty_Ret.Text = txtPQty_Ret.Text;
                            pQty = Convert.ToDecimal(txtPQty_Ret.Text);

                            decimal Qty = 0;
                            if (txtQty_Ret.Text == "")
                            {
                                txtQty_Ret.Text = "0";
                            }
                            else
                                txtQty_Ret.Text = txtQty_Ret.Text;
                            Qty = Convert.ToDecimal(txtQty_Ret.Text);
                            TextBox txtRate_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtRate_Ret");
                            decimal Rate = 0;
                            Rate = Convert.ToDecimal(txtRate_Ret.Text);
                            TextBox txtAmount_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtAmount_Ret");
                            decimal Amount = 0;
                            Amount = Convert.ToDecimal(txtAmount_Ret.Text);

                            TextBox txtDisRt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtDisRt_Ret");
                            if (txtDisRt_Ret.Text == "")
                            {
                                txtDisRt_Ret.Text = "0";
                            }
                            else
                            {
                                txtDisRt_Ret.Text = txtDisRt_Ret.Text;
                            }
                            decimal disRt = 0;
                            disRt = Convert.ToDecimal(txtDisRt_Ret.Text);

                            TextBox txtDisAmt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtDisAmt_Ret");
                            if (txtDisAmt_Ret.Text == "")
                            {
                                txtDisAmt_Ret.Text = "0";
                            }
                            else
                            {
                                txtDisAmt_Ret.Text = txtDisAmt_Ret.Text;
                            }
                            decimal disAmt = 0;
                            disAmt = Convert.ToDecimal(txtDisAmt_Ret.Text);

                            TextBox txtNetAmt_Ret = (TextBox)gvDetail_Ret.FooterRow.FindControl("txtNetAmt_Ret");
                            if (txtNetAmt_Ret.Text == "")
                            {
                                txtNetAmt_Ret.Text = "0";
                            }
                            else
                            {
                                txtNetAmt_Ret.Text = txtNetAmt_Ret.Text;
                            }
                            decimal NetAmt = 0;
                            NetAmt = Convert.ToDecimal(txtNetAmt_Ret.Text);

                            Global.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'IRTB' and TRANSMY = '" + lblMy_Ret.Text + "'", lblTransSL_Ret);
                            string ItemCD;
                            string mxCD = "";
                            string mid = "";
                            string subItemCD = "";
                            int subCD, incrItCD;
                            if (lblTransSL_Ret.Text == "")
                            {
                                ItemCD = "00000001";
                            }
                            else
                            {
                                mxCD = lblTransSL_Ret.Text;
                                //OItemCD = mxCD.Substring(4,4);
                                subCD = int.Parse(mxCD);
                                incrItCD = subCD + 1;
                                if (incrItCD < 10)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0000000" + mid;
                                }
                                else if (incrItCD < 100)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "000000" + mid;
                                }
                                else if (incrItCD < 1000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "00000" + mid;
                                }
                                else if (incrItCD < 10000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0000" + mid;
                                }
                                else if (incrItCD < 100000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "000" + mid;
                                }
                                else if (incrItCD < 1000000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "00" + mid;
                                }
                                else if (incrItCD < 10000000)
                                {
                                    mid = incrItCD.ToString();
                                    subItemCD = "0" + mid;
                                }
                                //else
                                //    subItemCD = incrItCD.ToString();

                                ItemCD = subItemCD;
                            }

                            query = ("insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE, REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, DISCRT, DISCAMT, NETAMT, USERPC, USERID, IPADDRESS ) " +
                                     "values(@TRANSTP,@TRANSDT,'" + lblMy_Ret.Text + "','" + txtInNo_Ret.Text + "','" + txtSLMNo_Ret.Text + "','" + txtSlFr_Ret.Text + "','','" + txtPID_Ret.Text + "','','','','" + txtRemarks_Ret.Text + "',@TRANSSL,'" + lblCatID_Ret.Text + "','" + txtItID_Ret.Text + "', " +
                                     " '" + ddlType_Ret.Text + "'," + cpQty + "," + cQty + "," + pQty + "," + Qty + "," + Rate + "," + Amount + "," + disRt + "," + disAmt + "," + NetAmt + ",'',@USERID,'')");

                            comm = new SqlCommand(query, conn);
                            comm.Parameters.AddWithValue("@TRANSTP", Transtp);
                            comm.Parameters.AddWithValue("@TRANSDT", TrDt);
                            //comm.Parameters.AddWithValue("@LCDATE", lcDate);
                            comm.Parameters.AddWithValue("@TRANSSL", ItemCD);
                            comm.Parameters.AddWithValue("@USERID", userName);

                            conn.Open();
                            int result = comm.ExecuteNonQuery();
                            conn.Close();

                            GridShow_Ret_Complete();
                        }
                    }

                    if (btnEdit_Ret.Text == "EDIT") ////new mode
                    {
                        Decimal totAmt = 0;
                        Decimal a = 0;
                        decimal tAmt = 0;
                        decimal dis = 0;
                        decimal disAmt = 0;
                        decimal amt = 0;
                        decimal Amount = 0;
                        if (gvDetail_Ret.EditIndex == -1)
                        {
                            foreach (GridViewRow grid in gvDetail_Ret.Rows)
                            {
                                Label lblNetAmt_Ret = (Label)grid.Cells[11].FindControl("lblNetAmt_Ret");

                                if (lblNetAmt_Ret.Text == "")
                                {
                                    lblNetAmt_Ret.Text = "0";
                                }
                                else
                                {
                                    lblNetAmt_Ret.Text = lblNetAmt_Ret.Text;
                                }
                                String TotalAmount = lblNetAmt_Ret.Text;
                                totAmt = Convert.ToDecimal(TotalAmount);
                                a += totAmt;
                                string tAmount = SpellAmount.comma(a);
                                txtTotAmt_Ret.Text = tAmount;
                                txtNetAmt_Ret.Text = tAmount;
                                txtTotal_Ret.Text = a.ToString();
                                tAmt = a;

                                Label lblAmount_Ret = (Label)grid.Cells[8].FindControl("lblAmount_Ret");
                                Label lblDisAmt_Ret = (Label)grid.Cells[10].FindControl("lblDisAmt_Ret");

                                if (lblAmount_Ret.Text == "")
                                {
                                    lblAmount_Ret.Text = "0.00";
                                }
                                else
                                    lblAmount_Ret.Text = lblAmount_Ret.Text;

                                amt = Convert.ToDecimal(lblAmount_Ret.Text);
                                Amount += amt;
                                txtTAmount_Ret.Text = Amount.ToString();

                                if (lblDisAmt_Ret.Text == "")
                                {
                                    lblDisAmt_Ret.Text = "0.00";
                                }
                                else
                                    lblDisAmt_Ret.Text = lblDisAmt_Ret.Text;

                                dis = Convert.ToDecimal(lblDisAmt_Ret.Text);
                                disAmt += dis;
                                txtTDisAmount_Ret.Text = disAmt.ToString();
                            }
                            a += totAmt;
                            Amount += amt;
                            disAmt += dis;
                            //}
                        }
                        else
                        {

                        }

                        txtGrossDisAmt_Ret.Focus();
                    }
                    else ////// edit mode
                    {
                        Decimal totAmt = 0;
                        Decimal a = 0;
                        decimal tAmt = 0;
                        decimal dis = 0;
                        decimal disAmt = 0;
                        decimal amt = 0;
                        decimal Amount = 0;
                        if (gvDetail_Ret.EditIndex == -1)
                        {
                            foreach (GridViewRow grid in gvDetail_Ret.Rows)
                            {
                                Label lblNetAmt_Ret = (Label)grid.Cells[11].FindControl("lblNetAmt_Ret");

                                if (lblNetAmt_Ret.Text == "")
                                {
                                    lblNetAmt_Ret.Text = "0";
                                }
                                else
                                {
                                    lblNetAmt_Ret.Text = lblNetAmt_Ret.Text;
                                }
                                String TotalAmount = lblNetAmt_Ret.Text;
                                totAmt = Convert.ToDecimal(TotalAmount);
                                a += totAmt;
                                string tAmount = SpellAmount.comma(a);
                                txtTotAmt_Ret.Text = tAmount;
                                txtNetAmt_Ret.Text = tAmount;
                                txtTotal_Ret.Text = a.ToString();
                                tAmt = a;

                                Label lblAmount_Ret = (Label)grid.Cells[8].FindControl("lblAmount_Ret");
                                Label lblDisAmt_Ret = (Label)grid.Cells[10].FindControl("lblDisAmt_Ret");

                                if (lblAmount_Ret.Text == "")
                                {
                                    lblAmount_Ret.Text = "0.00";
                                }
                                else
                                    lblAmount_Ret.Text = lblAmount_Ret.Text;

                                amt = Convert.ToDecimal(lblAmount_Ret.Text);
                                Amount += amt;
                                txtTAmount_Ret.Text = Amount.ToString();

                                if (lblDisAmt_Ret.Text == "")
                                {
                                    lblDisAmt_Ret.Text = "0.00";
                                }
                                else
                                    lblDisAmt_Ret.Text = lblDisAmt_Ret.Text;

                                dis = Convert.ToDecimal(lblDisAmt_Ret.Text);
                                disAmt += dis;
                                txtTDisAmount_Ret.Text = disAmt.ToString();
                            }
                            a += totAmt;
                            Amount += amt;
                            disAmt += dis;
                            //}
                        }
                        else
                        {

                        }

                        txtGrossDisAmt_Ret.Focus();
                    }
                }
            }
        }

        protected void gvDetail_Ret_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (btnEdit_Ret.Text == "EDIT")
            {
                gvDetail_Ret.EditIndex = -1;
                GridShow_Ret();
            }
            else
            {
                gvDetail_Ret.EditIndex = -1;
                GridShow_Ret_Edit();
            }
        }

        protected void gvDetail_Ret_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ddlRetType.Text == "IRTS")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                string userName = HttpContext.Current.Session["UserName"].ToString();

                if (btnEdit_Ret.Text == "EDIT")
                {
                    DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string TrDate = transdate.ToString("yyyy/MM/dd");

                    Label lblTransSL_Ret = (Label)gvDetail_Ret.Rows[e.RowIndex].FindControl("lblTransSL_Ret");

                    SqlCommand cmd1 = new SqlCommand("select * from STK_TRANS where TRANSTP = 'IRTS' and TRANSMY='" + lblMy_Ret.Text + "' and TRANSNO ='" + txtInNo_Ret.Text + "' and TRANSDT = '" + TrDate + "' ", conn);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);
                    conn.Close();
                    if (ds1.Tables[0].Rows.Count > 1)
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("delete from STK_TRANS where TRANSTP = 'IRTS' and TRANSNO ='" + txtInNo_Ret.Text + "' and  TRANSDT = '" + TrDate + "' and TRANSSL = '" + lblTransSL_Ret.Text + "'", conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("delete from STK_TRANS where TRANSTP = 'IRTS' and TRANSNO ='" + txtInNo_Ret.Text + "' and  TRANSDT = '" + TrDate + "' and TRANSSL = '" + lblTransSL_Ret.Text + "'", conn);
                        cmd.ExecuteNonQuery();
                        SqlCommand cmd2 = new SqlCommand("delete from STK_TRANSMST where TRANSTP = 'IRTS' and TRANSNO ='" + txtInNo_Ret.Text + "' and TRANSMY='" + lblMy_Ret.Text + "' and  TRANSDT = '" + TrDate + "'", conn);
                        cmd2.ExecuteNonQuery();
                        conn.Close();
                    }


                    gvDetail_Ret.EditIndex = -1;
                    GridShow_Ret();

                    txtTotAmt_Ret.Text = txtTotal_Ret.Text;
                    decimal totamt = Convert.ToDecimal(txtTotAmt_Ret.Text);
                    decimal grDisamt = Convert.ToDecimal(txtGrossDisAmt_Ret.Text);
                    decimal n_amt = totamt - grDisamt;
                    string n_Amt = SpellAmount.comma(n_amt);
                    txtNetAmt_Ret.Text = n_Amt;
                    txtGrossDisAmt_Ret.Focus();
                    lblSmsgComTrans_Ret.Visible = true;
                    lblSmsgComTrans_Ret.Text = "Complete Transaction By Changing Discount.";

                }
                else
                {
                    DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string TrDate = transdate.ToString("yyyy/MM/dd");

                    Label lblTransSL_Ret = (Label)gvDetail_Ret.Rows[e.RowIndex].FindControl("lblTransSL_Ret");

                    SqlCommand cmd1 = new SqlCommand("select * from STK_TRANS where TRANSTP = 'IRTS' and TRANSMY='" + lblMy_Ret.Text + "' and TRANSNO ='" + ddlSalesEditInNo_Ret.Text + "' and TRANSDT = '" + TrDate + "' ", conn);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);
                    conn.Close();
                    if (ds1.Tables[0].Rows.Count > 1)
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("delete from STK_TRANS where TRANSTP = 'IRTS' and TRANSNO ='" + ddlSalesEditInNo_Ret.Text + "' and  TRANSDT = '" + TrDate + "' and TRANSSL = '" + lblTransSL_Ret.Text + "'", conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("delete from STK_TRANS where TRANSTP = 'IRTS' and TRANSNO ='" + ddlSalesEditInNo_Ret.Text + "' and  TRANSDT = '" + TrDate + "' and TRANSSL = '" + lblTransSL_Ret.Text + "'", conn);
                        cmd.ExecuteNonQuery();
                        SqlCommand cmd2 = new SqlCommand("delete from STK_TRANSMST where TRANSTP = 'IRTS' and TRANSNO ='" + ddlSalesEditInNo_Ret.Text + "' and TRANSMY='" + lblMy_Ret.Text + "' and  TRANSDT = '" + TrDate + "'", conn);
                        cmd2.ExecuteNonQuery();
                        conn.Close();
                    }

                    gvDetail_Ret.EditIndex = -1;
                    GridShow_Ret_Edit();

                    txtTotAmt_Ret.Text = txtTotal_Ret.Text;
                    decimal totamt = Convert.ToDecimal(txtTotAmt_Ret.Text);
                    decimal grDisamt = Convert.ToDecimal(txtGrossDisAmt_Ret.Text);
                    decimal n_amt = totamt - grDisamt;
                    string n_Amt = SpellAmount.comma(n_amt);
                    txtNetAmt_Ret.Text = n_Amt;
                    txtGrossDisAmt_Ret.Focus();
                    lblSmsgComTrans_Ret.Visible = true;
                    lblSmsgComTrans_Ret.Text = "Complete Transaction By Changing Discount.";

                }
            }
            else ///////////////   Purchase return 
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                string userName = HttpContext.Current.Session["UserName"].ToString();

                if (btnEdit_Ret.Text == "EDIT")
                {
                    DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string TrDate = transdate.ToString("yyyy/MM/dd");

                    Label lblTransSL_Ret = (Label)gvDetail_Ret.Rows[e.RowIndex].FindControl("lblTransSL_Ret");

                    SqlCommand cmd1 = new SqlCommand("select * from STK_TRANS where TRANSTP = 'IRTB' and TRANSMY='" + lblMy_Ret.Text + "' and TRANSNO ='" + txtInNo_Ret.Text + "' and TRANSDT = '" + TrDate + "' ", conn);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);
                    conn.Close();
                    if (ds1.Tables[0].Rows.Count > 1)
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("delete from STK_TRANS where TRANSTP = 'IRTB' and TRANSNO ='" + txtInNo_Ret.Text + "' and  TRANSDT = '" + TrDate + "' and TRANSSL = '" + lblTransSL_Ret.Text + "'", conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("delete from STK_TRANS where TRANSTP = 'IRTB' and TRANSNO ='" + txtInNo_Ret.Text + "' and  TRANSDT = '" + TrDate + "' and TRANSSL = '" + lblTransSL_Ret.Text + "'", conn);
                        cmd.ExecuteNonQuery();
                        SqlCommand cmd2 = new SqlCommand("delete from STK_TRANSMST where TRANSTP = 'IRTB' and TRANSNO ='" + txtInNo_Ret.Text + "' and TRANSMY='" + lblMy_Ret.Text + "' and  TRANSDT = '" + TrDate + "'", conn);
                        cmd2.ExecuteNonQuery();
                        conn.Close();
                    }


                    gvDetail_Ret.EditIndex = -1;
                    GridShow_Ret();

                    txtTotAmt_Ret.Text = txtTotal_Ret.Text;
                    decimal totamt = Convert.ToDecimal(txtTotAmt_Ret.Text);
                    decimal grDisamt = Convert.ToDecimal(txtGrossDisAmt_Ret.Text);
                    decimal n_amt = totamt - grDisamt;
                    string n_Amt = SpellAmount.comma(n_amt);
                    txtNetAmt_Ret.Text = n_Amt;
                    txtGrossDisAmt_Ret.Focus();
                    lblSmsgComTrans_Ret.Visible = true;
                    lblSmsgComTrans_Ret.Text = "Complete Transaction By Changing Discount.";

                }
                else
                {
                    DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string TrDate = transdate.ToString("yyyy/MM/dd");

                    Label lblTransSL_Ret = (Label)gvDetail_Ret.Rows[e.RowIndex].FindControl("lblTransSL_Ret");

                    SqlCommand cmd1 = new SqlCommand("select * from STK_TRANS where TRANSTP = 'IRTB' and TRANSMY='" + lblMy_Ret.Text + "' and TRANSNO ='" + ddlSalesEditInNo_Ret.Text + "' and TRANSDT = '" + TrDate + "' ", conn);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);
                    conn.Close();
                    if (ds1.Tables[0].Rows.Count > 1)
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("delete from STK_TRANS where TRANSTP = 'IRTB' and TRANSNO ='" + ddlSalesEditInNo_Ret.Text + "' and  TRANSDT = '" + TrDate + "' and TRANSSL = '" + lblTransSL_Ret.Text + "'", conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("delete from STK_TRANS where TRANSTP = 'IRTB' and TRANSNO ='" + ddlSalesEditInNo_Ret.Text + "' and  TRANSDT = '" + TrDate + "' and TRANSSL = '" + lblTransSL_Ret.Text + "'", conn);
                        cmd.ExecuteNonQuery();
                        SqlCommand cmd2 = new SqlCommand("delete from STK_TRANSMST where TRANSTP = 'IRTB' and TRANSNO ='" + ddlSalesEditInNo_Ret.Text + "' and TRANSMY='" + lblMy_Ret.Text + "' and  TRANSDT = '" + TrDate + "'", conn);
                        cmd2.ExecuteNonQuery();
                        conn.Close();
                    }

                    gvDetail_Ret.EditIndex = -1;
                    GridShow_Ret_Edit();

                    txtTotAmt_Ret.Text = txtTotal_Ret.Text;
                    decimal totamt = Convert.ToDecimal(txtTotAmt_Ret.Text);
                    decimal grDisamt = Convert.ToDecimal(txtGrossDisAmt_Ret.Text);
                    decimal n_amt = totamt - grDisamt;
                    string n_Amt = SpellAmount.comma(n_amt);
                    txtNetAmt_Ret.Text = n_Amt;
                    txtGrossDisAmt_Ret.Focus();
                    lblSmsgComTrans_Ret.Visible = true;
                    lblSmsgComTrans_Ret.Text = "Complete Transaction By Changing Discount.";

                }
            }
        }

        protected void gvDetail_Ret_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (btnEdit_Ret.Text == "EDIT")
            {
                gvDetail_Ret.EditIndex = e.NewEditIndex;
                GridShow_Ret();
            }
            else
            {
                gvDetail_Ret.EditIndex = e.NewEditIndex;
                GridShow_Ret_Edit();
            }

            TextBox txtItemNMEdit_Ret = (TextBox)gvDetail_Ret.Rows[e.NewEditIndex].FindControl("txtItemNMEdit_Ret");
            txtItemNMEdit_Ret.Focus();
        }

        protected void gvDetail_Ret_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (ddlRetType.Text == "IRTS")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                string userName = HttpContext.Current.Session["UserName"].ToString();

                Label lblItemIDEdit_Ret = (Label)gvDetail_Ret.Rows[e.RowIndex].FindControl("lblItemIDEdit_Ret");
                TextBox txtItemNMEdit_Ret = (TextBox)gvDetail_Ret.Rows[e.RowIndex].FindControl("txtItemNMEdit_Ret");
                //txtItemNMEdit.Focus();
                TextBox txtQtyEdit_Ret = (TextBox)gvDetail_Ret.Rows[e.RowIndex].FindControl("txtQtyEdit_Ret");
                TextBox txtPQtyEdit_Ret = (TextBox)gvDetail_Ret.Rows[e.RowIndex].FindControl("txtPQtyEdit_Ret");
                if (txtSlFr_Ret.Text == "")
                {
                    lblSaleFrom_Ret.Visible = true;
                    lblSaleFrom_Ret.Text = "Select Store.";
                    txtSaleFrom_Ret.Focus();
                    lblPartyID_Ret.Visible = false;
                    lblGridMsg_Ret.Visible = false;
                }
                else if (txtPID_Ret.Text == "")
                {
                    lblPartyID_Ret.Visible = true;
                    lblPartyID_Ret.Text = "Select Party";
                    txtPSNM_Ret.Focus();
                    lblSaleFrom_Ret.Visible = false;
                    lblGridMsg_Ret.Visible = false;
                }
                else if (lblItemIDEdit_Ret.Text == "")
                {
                    lblGridMsg_Ret.Visible = true;
                    lblGridMsg_Ret.Text = "Select Item.";
                    txtItemNMEdit_Ret.Focus();
                    lblSaleFrom_Ret.Visible = false;
                    lblPartyID_Ret.Visible = false;
                }
                else if (txtQtyEdit_Ret.Text == "0")
                {
                    lblGridMsg_Ret.Visible = true;
                    lblGridMsg_Ret.Text = "Quantity is Wrong.";
                    txtPQtyEdit_Ret.Focus();
                }
                else
                {
                    Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNMEdit_Ret.Text + "'", lblCatID_Ret);

                    DropDownList ddltypeEdit_Ret = (DropDownList)gvDetail_Ret.Rows[e.RowIndex].FindControl("ddltypeEdit_Ret");
                    TextBox txtCPQTYEdit_Ret = (TextBox)gvDetail_Ret.Rows[e.RowIndex].FindControl("txtCPQTYEdit_Ret");
                    if (txtCPQTYEdit_Ret.Text == "")
                    {
                        txtCPQTYEdit_Ret.Text = "0";
                    }
                    else
                        txtCPQTYEdit_Ret.Text = txtCPQTYEdit_Ret.Text;
                    decimal cpQty = 0;
                    cpQty = Convert.ToDecimal(txtCPQTYEdit_Ret.Text);
                    TextBox txtCQtyEdit_Ret = (TextBox)gvDetail_Ret.Rows[e.RowIndex].FindControl("txtCQtyEdit_Ret");
                    if (txtCQtyEdit_Ret.Text == "")
                    {
                        txtCQtyEdit_Ret.Text = "0";
                    }
                    else
                        txtCQtyEdit_Ret.Text = txtCQtyEdit_Ret.Text;
                    decimal cQty = 0;
                    cQty = Convert.ToDecimal(txtCQtyEdit_Ret.Text);
                    decimal pQty = 0;
                    if (txtPQtyEdit_Ret.Text == "")
                    {
                        txtPQtyEdit_Ret.Text = "0";
                    }
                    else
                        txtPQtyEdit_Ret.Text = txtPQtyEdit_Ret.Text;
                    pQty = Convert.ToDecimal(txtPQtyEdit_Ret.Text);

                    decimal Qty = 0;
                    if (txtQtyEdit_Ret.Text == "")
                    {
                        txtQtyEdit_Ret.Text = "0";
                    }
                    else
                        txtQtyEdit_Ret.Text = txtQtyEdit_Ret.Text;
                    Qty = Convert.ToDecimal(txtQtyEdit_Ret.Text);
                    TextBox txtRateEdit_Ret = (TextBox)gvDetail_Ret.Rows[e.RowIndex].FindControl("txtRateEdit_Ret");
                    decimal Rate = 0;
                    Rate = Convert.ToDecimal(txtRateEdit_Ret.Text);
                    TextBox txtAmountEdit_Ret = (TextBox)gvDetail_Ret.Rows[e.RowIndex].FindControl("txtAmountEdit_Ret");
                    decimal Amount = 0;
                    Amount = Convert.ToDecimal(txtAmountEdit_Ret.Text);
                    Label lblTransSLEdit_Ret = (Label)gvDetail_Ret.Rows[e.RowIndex].FindControl("lblTransSLEdit_Ret");

                    DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string TrDate = transdate.ToString("yyyy/MM/dd");


                    TextBox txtDisRtEdit_Ret = (TextBox)gvDetail_Ret.Rows[e.RowIndex].FindControl("txtDisRtEdit_Ret");
                    if (txtDisRtEdit_Ret.Text == "")
                    {
                        txtDisRtEdit_Ret.Text = "0";
                    }
                    else
                    {
                        txtDisRtEdit_Ret.Text = txtDisRtEdit_Ret.Text;
                    }
                    decimal disRt = 0;
                    disRt = Convert.ToDecimal(txtDisRtEdit_Ret.Text);

                    TextBox txtDisAmtEdit_Ret = (TextBox)gvDetail_Ret.Rows[e.RowIndex].FindControl("txtDisAmtEdit_Ret");
                    if (txtDisAmtEdit_Ret.Text == "")
                    {
                        txtDisAmtEdit_Ret.Text = "0";
                    }
                    else
                    {
                        txtDisAmtEdit_Ret.Text = txtDisAmtEdit_Ret.Text;
                    }
                    decimal disAmt = 0;
                    disAmt = Convert.ToDecimal(txtDisAmtEdit_Ret.Text);

                    TextBox txtNetAmtEdit_Ret = (TextBox)gvDetail_Ret.Rows[e.RowIndex].FindControl("txtNetAmtEdit_Ret");
                    if (txtNetAmtEdit_Ret.Text == "")
                    {
                        txtNetAmtEdit_Ret.Text = "0";
                    }
                    else
                    {
                        txtNetAmtEdit_Ret.Text = txtNetAmtEdit_Ret.Text;
                    }
                    decimal NetAmt = 0;
                    NetAmt = Convert.ToDecimal(txtNetAmtEdit_Ret.Text);




                    if (btnEdit_Ret.Text == "EDIT")
                    {
                        Int64 TransNo = Convert.ToInt64(txtInNo_Ret.Text);

                        conn.Open();
                        SqlCommand cmd = new SqlCommand("update STK_TRANS set INVREFNO = '" + txtSLMNo_Ret.Text + "', STORETO='" + txtSlFr_Ret.Text + "', PSID='" + txtPID_Ret.Text + "', REMARKS = '" + txtRemarks_Ret.Text + "', CATID = '" + lblCatID_Ret.Text + "', ITEMID = '" + lblItemIDEdit_Ret.Text + "', UNITTP = '" + ddltypeEdit_Ret.Text + "', " +
                              " CPQTY =" + cpQty + ", CQTY = " + cQty + ", PQTY = " + pQty + ", QTY = " + Qty + ", RATE = " + Rate + ", AMOUNT = " + Amount + ",  DISCRT = " + disRt + ", DISCAMT = " + disAmt + ", NETAMT = " + NetAmt + ", USERID = '" + userName + "' where TRANSTP = 'IRTS' and TRANSMY = '" + lblMy_Ret.Text + "' and TRANSNO = " + TransNo + " and TRANSSL = '" + lblTransSLEdit_Ret.Text + "'", conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();



                        conn.Open();
                        SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set INVREFNO = '" + txtSLMNo_Ret.Text + "', STORETO='" + txtSlFr_Ret.Text + "', PSID='" + txtPID_Ret.Text + "', REMARKS = '" + txtRemarks_Ret.Text + "', " +
                              " USERID = '" + userName + "' where TRANSTP = 'IRTS'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                        cmd1.ExecuteNonQuery();
                        conn.Close();

                        gvDetail_Ret.EditIndex = -1;
                        GridShow_Ret();

                        txtTotAmt_Ret.Text = txtTotal_Ret.Text;
                        decimal totamt = Convert.ToDecimal(txtTotAmt_Ret.Text);
                        decimal grDisamt = Convert.ToDecimal(txtGrossDisAmt_Ret.Text);
                        decimal n_amt = totamt - grDisamt;
                        string n_Amt = SpellAmount.comma(n_amt);
                        txtNetAmt_Ret.Text = n_Amt;
                        txtGrossDisAmt_Ret.Focus();
                        lblSmsgComTrans_Ret.Visible = true;
                        lblSmsgComTrans_Ret.Text = "Complete Transaction By Changing Discount.";
                    }
                    else
                    {
                        Int64 TransNo = Convert.ToInt64(ddlSalesEditInNo_Ret.Text);

                        conn.Open();
                        SqlCommand cmd = new SqlCommand("update STK_TRANS set INVREFNO = '" + txtSLMNo_Ret.Text + "', STORETO='" + txtSlFr_Ret.Text + "', PSID='" + txtPID_Ret.Text + "', REMARKS = '" + txtRemarks_Ret.Text + "', CATID = '" + lblCatID_Ret.Text + "', ITEMID = '" + lblItemIDEdit_Ret.Text + "', UNITTP = '" + ddltypeEdit_Ret.Text + "', " +
                              " CPQTY =" + cpQty + ", CQTY = " + cQty + ", PQTY = " + pQty + ", QTY = " + Qty + ", RATE = " + Rate + ", AMOUNT = " + Amount + ", DISCRT = " + disRt + ", DISCAMT = " + disAmt + ", NETAMT = " + NetAmt + ", USERID = '" + userName + "' where TRANSTP = 'IRTS' and TRANSMY = '" + lblMy_Ret.Text + "' and TRANSNO = " + TransNo + " and TRANSSL = '" + lblTransSLEdit_Ret.Text + "'", conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();



                        conn.Open();
                        SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set INVREFNO = '" + txtSLMNo_Ret.Text + "', STORETO='" + txtSlFr_Ret.Text + "', PSID='" + txtPID_Ret.Text + "', REMARKS = '" + txtRemarks_Ret.Text + "', " +
                              " USERID = '" + userName + "' where TRANSTP = 'IRTS'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                        cmd1.ExecuteNonQuery();
                        conn.Close();

                        gvDetail_Ret.EditIndex = -1;
                        GridShow_Ret_Edit();

                        txtTotAmt_Ret.Text = txtTotal_Ret.Text;
                        decimal totamt = Convert.ToDecimal(txtTotAmt_Ret.Text);
                        decimal grDisamt = Convert.ToDecimal(txtGrossDisAmt_Ret.Text);
                        decimal n_amt = totamt - grDisamt;
                        string n_Amt = SpellAmount.comma(n_amt);
                        txtNetAmt_Ret.Text = n_Amt;
                        txtGrossDisAmt_Ret.Focus();
                        lblSmsgComTrans_Ret.Visible = true;
                        lblSmsgComTrans_Ret.Text = "Complete Transaction By Changing Discount.";
                    }

                }
            }
            else   /////////////purchase return
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                string userName = HttpContext.Current.Session["UserName"].ToString();

                Label lblItemIDEdit_Ret = (Label)gvDetail_Ret.Rows[e.RowIndex].FindControl("lblItemIDEdit_Ret");
                TextBox txtItemNMEdit_Ret = (TextBox)gvDetail_Ret.Rows[e.RowIndex].FindControl("txtItemNMEdit_Ret");
                //txtItemNMEdit.Focus();
                TextBox txtQtyEdit_Ret = (TextBox)gvDetail_Ret.Rows[e.RowIndex].FindControl("txtQtyEdit_Ret");
                TextBox txtPQtyEdit_Ret = (TextBox)gvDetail_Ret.Rows[e.RowIndex].FindControl("txtPQtyEdit_Ret");
                if (txtSlFr_Ret.Text == "")
                {
                    lblSaleFrom_Ret.Visible = true;
                    lblSaleFrom_Ret.Text = "Select Store.";
                    txtSaleFrom_Ret.Focus();
                    lblPartyID_Ret.Visible = false;
                    lblGridMsg_Ret.Visible = false;
                }
                else if (txtPID_Ret.Text == "")
                {
                    lblPartyID_Ret.Visible = true;
                    lblPartyID_Ret.Text = "Select Party";
                    txtPSNM_Ret.Focus();
                    lblSaleFrom_Ret.Visible = false;
                    lblGridMsg_Ret.Visible = false;
                }
                else if (lblItemIDEdit_Ret.Text == "")
                {
                    lblGridMsg_Ret.Visible = true;
                    lblGridMsg_Ret.Text = "Select Item.";
                    txtItemNMEdit_Ret.Focus();
                    lblSaleFrom_Ret.Visible = false;
                    lblPartyID_Ret.Visible = false;
                }
                else if (txtQtyEdit_Ret.Text == "0")
                {
                    lblGridMsg_Ret.Visible = true;
                    lblGridMsg_Ret.Text = "Quantity is Wrong.";
                    txtPQtyEdit_Ret.Focus();
                }
                else
                {
                    Global.lblAdd(@"select CATID from STK_ITEM where ITEMNM = '" + txtItemNMEdit_Ret.Text + "'", lblCatID_Ret);

                    DropDownList ddltypeEdit_Ret = (DropDownList)gvDetail_Ret.Rows[e.RowIndex].FindControl("ddltypeEdit_Ret");
                    TextBox txtCPQTYEdit_Ret = (TextBox)gvDetail_Ret.Rows[e.RowIndex].FindControl("txtCPQTYEdit_Ret");
                    if (txtCPQTYEdit_Ret.Text == "")
                    {
                        txtCPQTYEdit_Ret.Text = "0";
                    }
                    else
                        txtCPQTYEdit_Ret.Text = txtCPQTYEdit_Ret.Text;
                    decimal cpQty = 0;
                    cpQty = Convert.ToDecimal(txtCPQTYEdit_Ret.Text);
                    TextBox txtCQtyEdit_Ret = (TextBox)gvDetail_Ret.Rows[e.RowIndex].FindControl("txtCQtyEdit_Ret");
                    if (txtCQtyEdit_Ret.Text == "")
                    {
                        txtCQtyEdit_Ret.Text = "0";
                    }
                    else
                        txtCQtyEdit_Ret.Text = txtCQtyEdit_Ret.Text;
                    decimal cQty = 0;
                    cQty = Convert.ToDecimal(txtCQtyEdit_Ret.Text);
                    decimal pQty = 0;
                    if (txtPQtyEdit_Ret.Text == "")
                    {
                        txtPQtyEdit_Ret.Text = "0";
                    }
                    else
                        txtPQtyEdit_Ret.Text = txtPQtyEdit_Ret.Text;
                    pQty = Convert.ToDecimal(txtPQtyEdit_Ret.Text);

                    decimal Qty = 0;
                    if (txtQtyEdit_Ret.Text == "")
                    {
                        txtQtyEdit_Ret.Text = "0";
                    }
                    else
                        txtQtyEdit_Ret.Text = txtQtyEdit_Ret.Text;
                    Qty = Convert.ToDecimal(txtQtyEdit_Ret.Text);
                    TextBox txtRateEdit_Ret = (TextBox)gvDetail_Ret.Rows[e.RowIndex].FindControl("txtRateEdit_Ret");
                    decimal Rate = 0;
                    Rate = Convert.ToDecimal(txtRateEdit_Ret.Text);
                    TextBox txtAmountEdit_Ret = (TextBox)gvDetail_Ret.Rows[e.RowIndex].FindControl("txtAmountEdit_Ret");
                    decimal Amount = 0;
                    Amount = Convert.ToDecimal(txtAmountEdit_Ret.Text);
                    Label lblTransSLEdit_Ret = (Label)gvDetail_Ret.Rows[e.RowIndex].FindControl("lblTransSLEdit_Ret");

                    DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string TrDate = transdate.ToString("yyyy/MM/dd");


                    TextBox txtDisRtEdit_Ret = (TextBox)gvDetail_Ret.Rows[e.RowIndex].FindControl("txtDisRtEdit_Ret");
                    if (txtDisRtEdit_Ret.Text == "")
                    {
                        txtDisRtEdit_Ret.Text = "0";
                    }
                    else
                    {
                        txtDisRtEdit_Ret.Text = txtDisRtEdit_Ret.Text;
                    }
                    decimal disRt = 0;
                    disRt = Convert.ToDecimal(txtDisRtEdit_Ret.Text);

                    TextBox txtDisAmtEdit_Ret = (TextBox)gvDetail_Ret.Rows[e.RowIndex].FindControl("txtDisAmtEdit_Ret");
                    if (txtDisAmtEdit_Ret.Text == "")
                    {
                        txtDisAmtEdit_Ret.Text = "0";
                    }
                    else
                    {
                        txtDisAmtEdit_Ret.Text = txtDisAmtEdit_Ret.Text;
                    }
                    decimal disAmt = 0;
                    disAmt = Convert.ToDecimal(txtDisAmtEdit_Ret.Text);

                    TextBox txtNetAmtEdit_Ret = (TextBox)gvDetail_Ret.Rows[e.RowIndex].FindControl("txtNetAmtEdit_Ret");
                    if (txtNetAmtEdit_Ret.Text == "")
                    {
                        txtNetAmtEdit_Ret.Text = "0";
                    }
                    else
                    {
                        txtNetAmtEdit_Ret.Text = txtNetAmtEdit_Ret.Text;
                    }
                    decimal NetAmt = 0;
                    NetAmt = Convert.ToDecimal(txtNetAmtEdit_Ret.Text);




                    if (btnEdit_Ret.Text == "EDIT")
                    {
                        Int64 TransNo = Convert.ToInt64(txtInNo_Ret.Text);

                        conn.Open();
                        SqlCommand cmd = new SqlCommand("update STK_TRANS set INVREFNO = '" + txtSLMNo_Ret.Text + "', STOREFR='" + txtSlFr_Ret.Text + "', PSID='" + txtPID_Ret.Text + "', REMARKS = '" + txtRemarks_Ret.Text + "', CATID = '" + lblCatID_Ret.Text + "', ITEMID = '" + lblItemIDEdit_Ret.Text + "', UNITTP = '" + ddltypeEdit_Ret.Text + "', " +
                              " CPQTY =" + cpQty + ", CQTY = " + cQty + ", PQTY = " + pQty + ", QTY = " + Qty + ", RATE = " + Rate + ", AMOUNT = " + Amount + ",  DISCRT = " + disRt + ", DISCAMT = " + disAmt + ", NETAMT = " + NetAmt + ", USERID = '" + userName + "' where TRANSTP = 'IRTB' and TRANSMY = '" + lblMy_Ret.Text + "' and TRANSNO = " + TransNo + " and TRANSSL = '" + lblTransSLEdit_Ret.Text + "'", conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();



                        conn.Open();
                        SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set INVREFNO = '" + txtSLMNo_Ret.Text + "', STOREFR='" + txtSlFr_Ret.Text + "', PSID='" + txtPID_Ret.Text + "', REMARKS = '" + txtRemarks_Ret.Text + "', " +
                              " USERID = '" + userName + "' where TRANSTP = 'IRTB'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                        cmd1.ExecuteNonQuery();
                        conn.Close();

                        gvDetail_Ret.EditIndex = -1;
                        GridShow_Ret();

                        txtTotAmt_Ret.Text = txtTotal_Ret.Text;
                        decimal totamt = Convert.ToDecimal(txtTotAmt_Ret.Text);
                        decimal grDisamt = Convert.ToDecimal(txtGrossDisAmt_Ret.Text);
                        decimal n_amt = totamt - grDisamt;
                        string n_Amt = SpellAmount.comma(n_amt);
                        txtNetAmt_Ret.Text = n_Amt;
                        txtGrossDisAmt_Ret.Focus();
                        lblSmsgComTrans_Ret.Visible = true;
                        lblSmsgComTrans_Ret.Text = "Complete Transaction By Changing Discount.";
                    }
                    else
                    {
                        Int64 TransNo = Convert.ToInt64(ddlSalesEditInNo_Ret.Text);

                        conn.Open();
                        SqlCommand cmd = new SqlCommand("update STK_TRANS set INVREFNO = '" + txtSLMNo_Ret.Text + "', STOREFR='" + txtSlFr_Ret.Text + "', PSID='" + txtPID_Ret.Text + "', REMARKS = '" + txtRemarks_Ret.Text + "', CATID = '" + lblCatID_Ret.Text + "', ITEMID = '" + lblItemIDEdit_Ret.Text + "', UNITTP = '" + ddltypeEdit_Ret.Text + "', " +
                              " CPQTY =" + cpQty + ", CQTY = " + cQty + ", PQTY = " + pQty + ", QTY = " + Qty + ", RATE = " + Rate + ", AMOUNT = " + Amount + ", DISCRT = " + disRt + ", DISCAMT = " + disAmt + ", NETAMT = " + NetAmt + ", USERID = '" + userName + "' where TRANSTP = 'IRTB' and TRANSMY = '" + lblMy_Ret.Text + "' and TRANSNO = " + TransNo + " and TRANSSL = '" + lblTransSLEdit_Ret.Text + "'", conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();



                        conn.Open();
                        SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set INVREFNO = '" + txtSLMNo_Ret.Text + "', STOREFR='" + txtSlFr_Ret.Text + "', PSID='" + txtPID_Ret.Text + "', REMARKS = '" + txtRemarks_Ret.Text + "', " +
                              " USERID = '" + userName + "' where TRANSTP = 'IRTB'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                        cmd1.ExecuteNonQuery();
                        conn.Close();

                        gvDetail_Ret.EditIndex = -1;
                        GridShow_Ret_Edit();

                        txtTotAmt_Ret.Text = txtTotal_Ret.Text;
                        decimal totamt = Convert.ToDecimal(txtTotAmt_Ret.Text);
                        decimal grDisamt = Convert.ToDecimal(txtGrossDisAmt_Ret.Text);
                        decimal n_amt = totamt - grDisamt;
                        string n_Amt = SpellAmount.comma(n_amt);
                        txtNetAmt_Ret.Text = n_Amt;
                        txtGrossDisAmt_Ret.Focus();
                        lblSmsgComTrans_Ret.Visible = true;
                        lblSmsgComTrans_Ret.Text = "Complete Transaction By Changing Discount.";
                    }

                }
            }
        }

        protected void gvDetail_Ret_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnEdit_Ret_Click(object sender, EventArgs e)
        {
            if (ddlRetType.Text == "IRTS")
            {
                if (btnEdit_Ret.Text == "EDIT")
                {
                    DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string TrDate = transdate.ToString("yyyy/MM/dd");

                    txtInNo_Ret.Visible = false;
                    btnEdit_Ret.Text = "NEW";
                    ddlSalesEditInNo_Ret.Visible = true;
                    Global.dropDownAddWithSelect(ddlSalesEditInNo_Ret, "SELECT DISTINCT TRANSNO FROM STK_TRANS WHERE TRANSDT ='" + TrDate + "' AND TRANSMY='" + lblMy_Ret.Text + "' and TRANSTP='IRTS'");
                    txtSLMNo_Ret.Text = "";
                    txtSaleFrom_Ret.Text = "";
                    txtSlFr_Ret.Text = "";
                    txtPSNM_Ret.Text = "";
                    txtPID_Ret.Text = "";
                    txtRemarks_Ret.Text = "";
                    lblSmsgComTrans_Ret.Visible = false;
                    //lblSMY.Text = "";
                    txtTotAmt_Ret.Text = "0.00";
                    txtGrossDisAmt_Ret.Text = "0.00";
                    txtNetAmt_Ret.Text = "0.00";
                    txtTAmount_Ret.Text = "0.00";
                    txtTDisAmount_Ret.Text = "0.00";
                    txtTotal_Ret.Text = "0.00";
                    GridShow_Ret_Edit();
                }
                else
                {
                    txtInNo_Ret.Visible = true;
                    btnEdit_Ret.Text = "EDIT";
                    ddlSalesEditInNo_Ret.Visible = false;
                    txtSLMNo_Ret.Text = "";
                    txtSaleFrom_Ret.Text = "";
                    txtSlFr_Ret.Text = "";
                    txtPSNM_Ret.Text = "";
                    txtPID_Ret.Text = "";
                    txtRemarks_Ret.Text = "";
                    lblSmsgComTrans_Ret.Visible = false;
                    txtTotAmt_Ret.Text = "0.00";
                    txtGrossDisAmt_Ret.Text = "0.00";
                    txtNetAmt_Ret.Text = "0.00";
                    txtTAmount_Ret.Text = "0.00";
                    txtTDisAmount_Ret.Text = "0.00";
                    txtTotal_Ret.Text = "0.00";
                    Return_Start();
                }
            }
            else ////purchase
            {
                if (btnEdit_Ret.Text == "EDIT")
                {
                    DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string TrDate = transdate.ToString("yyyy/MM/dd");

                    txtInNo_Ret.Visible = false;
                    btnEdit_Ret.Text = "NEW";
                    ddlSalesEditInNo_Ret.Visible = true;
                    Global.dropDownAddWithSelect(ddlSalesEditInNo_Ret, "SELECT DISTINCT TRANSNO FROM STK_TRANS WHERE TRANSDT ='" + TrDate + "' AND TRANSMY='" + lblMy_Ret.Text + "' and TRANSTP='IRTB'");
                    txtSLMNo_Ret.Text = "";
                    txtSaleFrom_Ret.Text = "";
                    txtSlFr_Ret.Text = "";
                    txtPSNM_Ret.Text = "";
                    txtPID_Ret.Text = "";
                    txtRemarks_Ret.Text = "";
                    lblSmsgComTrans_Ret.Visible = false;
                    //lblSMY.Text = "";
                    txtTotAmt_Ret.Text = "0.00";
                    txtGrossDisAmt_Ret.Text = "0.00";
                    txtNetAmt_Ret.Text = "0.00";
                    txtTAmount_Ret.Text = "0.00";
                    txtTDisAmount_Ret.Text = "0.00";
                    txtTotal_Ret.Text = "0.00";
                    GridShow_Ret_Edit();
                }
                else
                {
                    txtInNo_Ret.Visible = true;
                    btnEdit_Ret.Text = "EDIT";
                    ddlSalesEditInNo_Ret.Visible = false;
                    txtSLMNo_Ret.Text = "";
                    txtSaleFrom_Ret.Text = "";
                    txtSlFr_Ret.Text = "";
                    txtPSNM_Ret.Text = "";
                    txtPID_Ret.Text = "";
                    txtRemarks_Ret.Text = "";
                    lblSmsgComTrans_Ret.Visible = false;
                    txtTotAmt_Ret.Text = "0.00";
                    txtGrossDisAmt_Ret.Text = "0.00";
                    txtNetAmt_Ret.Text = "0.00";
                    txtTAmount_Ret.Text = "0.00";
                    txtTDisAmount_Ret.Text = "0.00";
                    txtTotal_Ret.Text = "0.00";
                    Return_Start();
                }
            }
        }

        protected void btnComplete_Ret_Click(object sender, EventArgs e)
        {
            if (ddlRetType.Text == "IRTS")
            {
                lblSmsgComTrans_Ret.Visible = false;

                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                string userName = HttpContext.Current.Session["UserName"].ToString();

                DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                decimal totamt = Convert.ToDecimal(txtTotAmt_Ret.Text);
                decimal grsDis = Convert.ToDecimal(txtGrossDisAmt_Ret.Text);
                decimal NetAmt = Convert.ToDecimal(txtNetAmt_Ret.Text);

                if (totamt - grsDis == NetAmt)
                {
                    if (btnSaleEdit.Text == "EDIT")
                    {
                        Int64 TransNo = Convert.ToInt64(txtInNo_Ret.Text);
                        conn.Open();
                        SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", TOTNET = " + NetAmt + ", " +
                              " USERID = '" + userName + "' where TRANSTP = 'IRTS'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                        cmd1.ExecuteNonQuery();
                        conn.Close();

                        ///////Refresh/////
                        ddlSalesEditInNo_Ret.SelectedIndex = -1;
                        txtSLMNo_Ret.Text = "";
                        txtSaleFrom_Ret.Text = "";
                        txtSlFr_Ret.Text = "";
                        txtPSNM_Ret.Text = "";
                        txtPID_Ret.Text = "";
                        txtRemarks_Ret.Text = "";
                        txtTotAmt_Ret.Text = "0.00";
                        txtGrossDisAmt_Ret.Text = "0.00";
                        txtNetAmt_Ret.Text = "0.00";
                        txtTAmount_Ret.Text = "0.00";
                        txtTDisAmount_Ret.Text = "0.00";
                        txtTotal_Ret.Text = "0.00";

                        Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblMy_Ret.Text + "' and TRANSTP = 'IRTS'", lblMxNo_Ret);
                        if (lblMxNo_Ret.Text == "")
                        {
                            txtInNo_Ret.Text = "1";
                        }
                        else
                        {
                            int iNo = int.Parse(lblMxNo_Ret.Text);
                            int totIno = iNo + 1;
                            txtInNo_Ret.Text = totIno.ToString();
                        }

                        GridShow_Ret();
                        //Up_Sales.Update();
                        txtSLMNo_Ret.Focus();
                    }
                    else
                    {
                        Int64 TransNo = Convert.ToInt64(ddlSalesEditInNo_Ret.Text);
                        if (ddlSalesEditInNo_Ret.Text == "Select")
                        {
                            lblGridMsg_Ret.Visible = true;
                            lblGridMsg_Ret.Text = "Select Invoice No.";
                        }
                        else
                        {
                            lblGridMsg_Ret.Visible = false;

                            conn.Open();
                            SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", TOTNET = " + NetAmt + ", " +
                                  " USERID = '" + userName + "' where TRANSTP = 'IRTS'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                            cmd1.ExecuteNonQuery();
                            conn.Close();

                            conn.Open();
                            SqlCommand cmd2 = new SqlCommand("update STK_TRANSMST set INVREFNO='" + txtSLMNo_Ret.Text + "', STORETO='" + txtSlFr_Ret.Text + "', PSID = '" + txtPID_Ret.Text + "', " +
                                  " REMARKS = '" + txtRemarks_Ret.Text + "',USERID = '" + userName + "' where TRANSTP = 'IRTS'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                            cmd2.ExecuteNonQuery();
                            conn.Close();

                            conn.Open();
                            SqlCommand cmd3 = new SqlCommand("update  STK_TRANS set INVREFNO='" + txtSLMNo_Ret.Text + "', STORETO='" + txtSlFr_Ret.Text + "', PSID = '" + txtPID_Ret.Text + "', " +
                                  " REMARKS = '" + txtRemarks_Ret.Text + "',USERID = '" + userName + "' where TRANSTP = 'IRTS'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                            cmd3.ExecuteNonQuery();
                            conn.Close();

                            ///////Refresh/////
                            ddlSalesEditInNo_Ret.SelectedIndex = -1;
                            txtSLMNo_Ret.Text = "";
                            txtSaleFrom_Ret.Text = "";
                            txtSlFr_Ret.Text = "";
                            txtPSNM_Ret.Text = "";
                            txtPID_Ret.Text = "";
                            txtRemarks_Ret.Text = "";
                            txtTotAmt_Ret.Text = "0.00";
                            txtGrossDisAmt_Ret.Text = "0.00";
                            txtNetAmt_Ret.Text = "0.00";
                            txtTAmount_Ret.Text = "0.00";
                            txtTDisAmount_Ret.Text = "0.00";
                            txtTotal_Ret.Text = "0.00";

                            GridShow_Ret_Edit();
                            //Up_Sales.Update();
                            ddlSalesEditInNo_Ret.Focus();
                        }
                    }
                }
                else
                {
                    decimal com_NetAmt = totamt - grsDis;

                    if (btnEdit_Ret.Text == "EDIT")
                    {
                        Int64 TransNo = Convert.ToInt64(txtInNo_Ret.Text);
                        conn.Open();
                        SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", TOTNET = " + com_NetAmt + ", " +
                              " USERID = '" + userName + "' where TRANSTP = 'IRTS'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                        cmd1.ExecuteNonQuery();
                        conn.Close();

                        ///////Refresh/////
                        ddlSalesEditInNo_Ret.SelectedIndex = -1;
                        txtSLMNo_Ret.Text = "";
                        txtSaleFrom_Ret.Text = "";
                        txtSlFr_Ret.Text = "";
                        txtPSNM_Ret.Text = "";
                        txtPID_Ret.Text = "";
                        txtRemarks_Ret.Text = "";
                        txtTotAmt_Ret.Text = "0.00";
                        txtGrossDisAmt_Ret.Text = "0.00";
                        txtNetAmt_Ret.Text = "0.00";
                        txtTAmount_Ret.Text = "0.00";
                        txtTDisAmount_Ret.Text = "0.00";
                        txtTotal_Ret.Text = "0.00";

                        Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblMy_Ret.Text + "' and TRANSTP = 'IRTS'", lblMxNo_Ret);
                        if (lblMxNo_Ret.Text == "")
                        {
                            txtInNo_Ret.Text = "1";
                        }
                        else
                        {
                            int iNo = int.Parse(lblMxNo_Ret.Text);
                            int totIno = iNo + 1;
                            txtInNo_Ret.Text = totIno.ToString();
                        }

                        GridShow_Ret();
                        //Up_Sales.Update();
                        txtSLMNo_Ret.Focus();
                    }
                    else
                    {
                        Int64 TransNo = Convert.ToInt64(ddlSalesEditInNo_Ret.Text);
                        if (ddlSalesEditInNo_Ret.Text == "Select")
                        {
                            lblGridMsg_Ret.Visible = true;
                            lblGridMsg_Ret.Text = "Select Invoice No.";
                        }
                        else
                        {
                            lblGridMsg_Ret.Visible = false;

                            conn.Open();
                            SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", TOTNET = " + com_NetAmt + ", " +
                                  " USERID = '" + userName + "' where TRANSTP = 'IRTS'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                            cmd1.ExecuteNonQuery();
                            conn.Close();

                            ///////Refresh/////
                            ddlSalesEditInNo_Ret.SelectedIndex = -1;
                            txtSLMNo_Ret.Text = "";
                            txtSaleFrom_Ret.Text = "";
                            txtSlFr_Ret.Text = "";
                            txtPSNM_Ret.Text = "";
                            txtPID_Ret.Text = "";
                            txtRemarks_Ret.Text = "";
                            txtTotAmt_Ret.Text = "0.00";
                            txtGrossDisAmt_Ret.Text = "0.00";
                            txtNetAmt_Ret.Text = "0.00";
                            txtTAmount_Ret.Text = "0.00";
                            txtTDisAmount_Ret.Text = "0.00";
                            txtTotal_Ret.Text = "0.00";

                            GridShow_Ret_Edit();
                            //Up_Sales.Update();
                            ddlSalesEditInNo_Ret.Focus();
                        }
                    }

                }
            }
            else   ////// PURCHASE
            {
                lblSmsgComTrans_Ret.Visible = false;

                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                string userName = HttpContext.Current.Session["UserName"].ToString();

                DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                decimal totamt = Convert.ToDecimal(txtTotAmt_Ret.Text);
                decimal grsDis = Convert.ToDecimal(txtGrossDisAmt_Ret.Text);
                decimal NetAmt = Convert.ToDecimal(txtNetAmt_Ret.Text);

                if (totamt - grsDis == NetAmt)
                {
                    if (btnSaleEdit.Text == "EDIT")
                    {
                        Int64 TransNo = Convert.ToInt64(txtInNo_Ret.Text);
                        conn.Open();
                        SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", TOTNET = " + NetAmt + ", " +
                              " USERID = '" + userName + "' where TRANSTP = 'IRTB'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                        cmd1.ExecuteNonQuery();
                        conn.Close();

                        ///////Refresh/////
                        ddlSalesEditInNo_Ret.SelectedIndex = -1;
                        txtSLMNo_Ret.Text = "";
                        txtSaleFrom_Ret.Text = "";
                        txtSlFr_Ret.Text = "";
                        txtPSNM_Ret.Text = "";
                        txtPID_Ret.Text = "";
                        txtRemarks_Ret.Text = "";
                        txtTotAmt_Ret.Text = "0.00";
                        txtGrossDisAmt_Ret.Text = "0.00";
                        txtNetAmt_Ret.Text = "0.00";
                        txtTAmount_Ret.Text = "0.00";
                        txtTDisAmount_Ret.Text = "0.00";
                        txtTotal_Ret.Text = "0.00";

                        Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblMy_Ret.Text + "' and TRANSTP = 'IRTB'", lblMxNo_Ret);
                        if (lblMxNo_Ret.Text == "")
                        {
                            txtInNo_Ret.Text = "1";
                        }
                        else
                        {
                            int iNo = int.Parse(lblMxNo_Ret.Text);
                            int totIno = iNo + 1;
                            txtInNo_Ret.Text = totIno.ToString();
                        }

                        GridShow_Ret();
                        //Up_Sales.Update();
                        txtSLMNo_Ret.Focus();
                    }
                    else
                    {
                        Int64 TransNo = Convert.ToInt64(ddlSalesEditInNo_Ret.Text);
                        if (ddlSalesEditInNo_Ret.Text == "Select")
                        {
                            lblGridMsg_Ret.Visible = true;
                            lblGridMsg_Ret.Text = "Select Invoice No.";
                        }
                        else
                        {
                            lblGridMsg_Ret.Visible = false;

                            conn.Open();
                            SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", TOTNET = " + NetAmt + ", " +
                                  " USERID = '" + userName + "' where TRANSTP = 'IRTB'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                            cmd1.ExecuteNonQuery();
                            conn.Close();

                            conn.Open();
                            SqlCommand cmd2 = new SqlCommand("update STK_TRANSMST set INVREFNO='" + txtSLMNo_Ret.Text + "', STOREFR='" + txtSlFr_Ret.Text + "', PSID = '" + txtPID_Ret.Text + "', " +
                                  " REMARKS = '" + txtRemarks_Ret.Text + "',USERID = '" + userName + "' where TRANSTP = 'IRTB'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                            cmd2.ExecuteNonQuery();
                            conn.Close();

                            conn.Open();
                            SqlCommand cmd3 = new SqlCommand("update  STK_TRANS set INVREFNO='" + txtSLMNo_Ret.Text + "', STOREFR='" + txtSlFr_Ret.Text + "', PSID = '" + txtPID_Ret.Text + "', " +
                                  " REMARKS = '" + txtRemarks_Ret.Text + "',USERID = '" + userName + "' where TRANSTP = 'IRTB'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                            cmd3.ExecuteNonQuery();
                            conn.Close();

                            ///////Refresh/////
                            ddlSalesEditInNo_Ret.SelectedIndex = -1;
                            txtSLMNo_Ret.Text = "";
                            txtSaleFrom_Ret.Text = "";
                            txtSlFr_Ret.Text = "";
                            txtPSNM_Ret.Text = "";
                            txtPID_Ret.Text = "";
                            txtRemarks_Ret.Text = "";
                            txtTotAmt_Ret.Text = "0.00";
                            txtGrossDisAmt_Ret.Text = "0.00";
                            txtNetAmt_Ret.Text = "0.00";
                            txtTAmount_Ret.Text = "0.00";
                            txtTDisAmount_Ret.Text = "0.00";
                            txtTotal_Ret.Text = "0.00";

                            GridShow_Ret_Edit();
                            //Up_Sales.Update();
                            ddlSalesEditInNo_Ret.Focus();
                        }
                    }
                }
                else
                {
                    decimal com_NetAmt = totamt - grsDis;

                    if (btnEdit_Ret.Text == "EDIT")
                    {
                        Int64 TransNo = Convert.ToInt64(txtInNo_Ret.Text);
                        conn.Open();
                        SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", TOTNET = " + com_NetAmt + ", " +
                              " USERID = '" + userName + "' where TRANSTP = 'IRTB'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                        cmd1.ExecuteNonQuery();
                        conn.Close();

                        ///////Refresh/////
                        ddlSalesEditInNo_Ret.SelectedIndex = -1;
                        txtSLMNo_Ret.Text = "";
                        txtSaleFrom_Ret.Text = "";
                        txtSlFr_Ret.Text = "";
                        txtPSNM_Ret.Text = "";
                        txtPID_Ret.Text = "";
                        txtRemarks_Ret.Text = "";
                        txtTotAmt_Ret.Text = "0.00";
                        txtGrossDisAmt_Ret.Text = "0.00";
                        txtNetAmt_Ret.Text = "0.00";
                        txtTAmount_Ret.Text = "0.00";
                        txtTDisAmount_Ret.Text = "0.00";
                        txtTotal_Ret.Text = "0.00";

                        Global.lblAdd(@"Select max(TRANSNO) FROM STK_TRANS where TRANSMY='" + lblMy_Ret.Text + "' and TRANSTP = 'IRTB'", lblMxNo_Ret);
                        if (lblMxNo_Ret.Text == "")
                        {
                            txtInNo_Ret.Text = "1";
                        }
                        else
                        {
                            int iNo = int.Parse(lblMxNo_Ret.Text);
                            int totIno = iNo + 1;
                            txtInNo_Ret.Text = totIno.ToString();
                        }

                        GridShow_Ret();
                        //Up_Sales.Update();
                        txtSLMNo_Ret.Focus();
                    }
                    else
                    {
                        Int64 TransNo = Convert.ToInt64(ddlSalesEditInNo_Ret.Text);
                        if (ddlSalesEditInNo_Ret.Text == "Select")
                        {
                            lblGridMsg_Ret.Visible = true;
                            lblGridMsg_Ret.Text = "Select Invoice No.";
                        }
                        else
                        {
                            lblGridMsg_Ret.Visible = false;

                            conn.Open();
                            SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", TOTNET = " + com_NetAmt + ", " +
                                  " USERID = '" + userName + "' where TRANSTP = 'IRTB'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                            cmd1.ExecuteNonQuery();
                            conn.Close();

                            ///////Refresh/////
                            ddlSalesEditInNo_Ret.SelectedIndex = -1;
                            txtSLMNo_Ret.Text = "";
                            txtSaleFrom_Ret.Text = "";
                            txtSlFr_Ret.Text = "";
                            txtPSNM_Ret.Text = "";
                            txtPID_Ret.Text = "";
                            txtRemarks_Ret.Text = "";
                            txtTotAmt_Ret.Text = "0.00";
                            txtGrossDisAmt_Ret.Text = "0.00";
                            txtNetAmt_Ret.Text = "0.00";
                            txtTAmount_Ret.Text = "0.00";
                            txtTDisAmount_Ret.Text = "0.00";
                            txtTotal_Ret.Text = "0.00";

                            GridShow_Ret_Edit();
                            //Up_Sales.Update();
                            ddlSalesEditInNo_Ret.Focus();
                        }
                    }

                }
            }
        }

        protected void ddlSalesEditInNo_Ret_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRetType.Text == "IRTS")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);

                DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDt = transdate.ToString("yyyy/MM/dd");

                if (ddlSalesEditInNo_Ret.Text == "Select")
                {
                    gvDetail_Ret.Visible = false;
                    lblGridMsg_Ret.Visible = true;
                    lblGridMsg_Ret.Text = "Type Invoice No.";
                    txtTotal_Ret.Text = "";
                }
                else
                {
                    gvDetail_Ret.Visible = true;
                    lblGridMsg_Ret.Visible = false;
                    Int64 TransNo = Convert.ToInt64(ddlSalesEditInNo_Ret.Text);

                    Global.txtAdd(@"select INVREFNO from STK_TRANSMST where TRANSTP='IRTS' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblMy_Ret.Text + "' and TRANSNO =" + TransNo + "", txtSLMNo_Ret);
                    Global.txtAdd(@"select STORETO from STK_TRANSMST where TRANSTP='IRTS' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblMy_Ret.Text + "' and TRANSNO =" + TransNo + "", txtSlFr_Ret);
                    Global.txtAdd(@"select STORENM from STK_STORE where STOREID ='" + txtSlFr_Ret.Text + "'", txtSaleFrom_Ret);
                    Global.txtAdd(@"select PSID from STK_TRANSMST where TRANSTP='IRTS' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblMy_Ret.Text + "' and TRANSNO =" + TransNo + "", txtPID_Ret);
                    Global.txtAdd(@"select ACCOUNTNM from GL_ACCHART where ACCOUNTCD ='" + txtPID_Ret.Text + "'", txtPSNM_Ret);
                    Global.txtAdd(@"select REMARKS from STK_TRANSMST where TRANSTP='IRTS' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblMy_Ret.Text + "' and TRANSNO =" + TransNo + "", txtRemarks_Ret);
                    Global.txtAdd(@"select TOTAMT from STK_TRANSMST where TRANSTP='IRTS' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblMy_Ret.Text + "' and TRANSNO =" + TransNo + "", txtTotAmt_Ret);
                    Global.txtAdd(@"select DISAMT from STK_TRANSMST where TRANSTP='IRTS' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblMy_Ret.Text + "' and TRANSNO =" + TransNo + "", txtGrossDisAmt_Ret);
                    Global.txtAdd(@"select TOTNET from STK_TRANSMST where TRANSTP='IRTS' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblMy_Ret.Text + "' and TRANSNO =" + TransNo + "", txtNetAmt_Ret);
                    GridShow_Ret_Edit();
                }
            }
            else  ////////////////Purchase
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);

                DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDt = transdate.ToString("yyyy/MM/dd");

                if (ddlSalesEditInNo_Ret.Text == "Select")
                {
                    gvDetail_Ret.Visible = false;
                    lblGridMsg_Ret.Visible = true;
                    lblGridMsg_Ret.Text = "Type Invoice No.";
                    txtTotal_Ret.Text = "";
                }
                else
                {
                    gvDetail_Ret.Visible = true;
                    lblGridMsg_Ret.Visible = false;
                    Int64 TransNo = Convert.ToInt64(ddlSalesEditInNo_Ret.Text);

                    Global.txtAdd(@"select INVREFNO from STK_TRANSMST where TRANSTP='IRTB' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblMy_Ret.Text + "' and TRANSNO =" + TransNo + "", txtSLMNo_Ret);
                    Global.txtAdd(@"select STOREFR from STK_TRANSMST where TRANSTP='IRTB' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblMy_Ret.Text + "' and TRANSNO =" + TransNo + "", txtSlFr_Ret);
                    Global.txtAdd(@"select STORENM from STK_STORE where STOREID ='" + txtSlFr_Ret.Text + "'", txtSaleFrom_Ret);
                    Global.txtAdd(@"select PSID from STK_TRANSMST where TRANSTP='IRTB' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblMy_Ret.Text + "' and TRANSNO =" + TransNo + "", txtPID_Ret);
                    Global.txtAdd(@"select ACCOUNTNM from GL_ACCHART where ACCOUNTCD ='" + txtPID_Ret.Text + "'", txtPSNM_Ret);
                    Global.txtAdd(@"select REMARKS from STK_TRANSMST where TRANSTP='IRTB' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblMy_Ret.Text + "' and TRANSNO =" + TransNo + "", txtRemarks_Ret);
                    Global.txtAdd(@"select TOTAMT from STK_TRANSMST where TRANSTP='IRTB' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblMy_Ret.Text + "' and TRANSNO =" + TransNo + "", txtTotAmt_Ret);
                    Global.txtAdd(@"select DISAMT from STK_TRANSMST where TRANSTP='IRTB' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblMy_Ret.Text + "' and TRANSNO =" + TransNo + "", txtGrossDisAmt_Ret);
                    Global.txtAdd(@"select TOTNET from STK_TRANSMST where TRANSTP='IRTB' and TRANSDT = '" + TrDt + "' and TRANSMY = '" + lblMy_Ret.Text + "' and TRANSNO =" + TransNo + "", txtNetAmt_Ret);
                    GridShow_Ret_Edit();
                }
            }
        }

        protected void btnPrint_Ret_Click(object sender, EventArgs e)
        {
            //if (btnEdit_Ret.Text == "Edit")
            //{
            if (ddlRetType.Text == "IRTS")
            {
                Session["Ret_Type"] = "SALE";
                Session["InvDate_S_Ret"] = "";
                Session["InvNo_S_Ret"] = "";
                Session["InvNoEdit_S_Ret"] = "";
                Session["Memo_S_Ret"] = "";
                Session["StoreNM_S_Ret"] = "";
                Session["StoreID_S_Ret"] = "";
                Session["PartyNM_S_Ret"] = "";
                Session["PartyID_S_Ret"] = "";

                if (txtTInDt_Ret.Text == "")
                {
                    lblSaleFrom_Ret.Visible = true;
                    lblSaleFrom_Ret.Text = "Select Date.";
                }
                else if (txtInNo_Ret.Text == "")
                {
                    lblSaleFrom_Ret.Visible = true;
                    lblSaleFrom_Ret.Text = "Select Invoice No.";
                }
                else if (txtSlFr_Ret.Text == "")
                {
                    lblSaleFrom_Ret.Visible = true;
                    lblSaleFrom_Ret.Text = "Select Store.";
                    txtSaleFrom_Ret.Focus();
                }
                else if (txtPID_Ret.Text == "")
                {
                    lblSaleFrom_Ret.Visible = true;
                    lblSaleFrom_Ret.Text = "Select Party.";
                    txtPSNM_Ret.Focus();
                }
                else
                {

                    lblSaleFrom_Ret.Visible = false;

                    Session["InvDate_S_Ret"] = txtTInDt_Ret.Text;
                    Session["InvNo_S_Ret"] = txtInNo_Ret.Text;
                    Session["InvNoEdit_S_Ret"] = ddlSalesEditInNo_Ret.Text;
                    Session["Memo_S_Ret"] = txtSLMNo_Ret.Text;
                    Session["StoreNM_S_Ret"] = txtSaleFrom_Ret.Text;
                    Session["StoreID_S_Ret"] = txtSlFr_Ret.Text;
                    Session["PartyNM_S_Ret"] = txtPSNM_Ret.Text;
                    Session["PartyID_S_Ret"] = txtPID_Ret.Text;

                    if (btnEdit_Ret.Text == "NEW")
                    {
                        if (ddlSalesEditInNo_Ret.Text == "Select")
                        {
                            lblSaleFrom_Ret.Visible = true;
                            lblSaleFrom_Ret.Text = "Select Invoice No";
                            ddlSalesEditInNo_Ret.Focus();
                        }
                        else
                        {
                            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                            SqlConnection conn = new SqlConnection(connectionString);
                            string userName = HttpContext.Current.Session["UserName"].ToString();

                            DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            string TrDate = transdate.ToString("yyyy/MM/dd");

                            decimal totamt = Convert.ToDecimal(txtTotAmt_Ret.Text);
                            decimal grsDis = Convert.ToDecimal(txtGrossDisAmt_Ret.Text);
                            decimal NetAmt = Convert.ToDecimal(txtNetAmt_Ret.Text);

                            if (totamt - grsDis == NetAmt)
                            {
                                Int64 TransNo = Convert.ToInt64(ddlSalesEditInNo_Ret.Text);
                                conn.Open();
                                SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", TOTNET = " + NetAmt + ", " +
                                      " USERID = '" + userName + "' where TRANSTP = 'IRTS'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                                cmd1.ExecuteNonQuery();
                                conn.Close();
                            }
                            else
                            {
                                decimal p_NetAmt = totamt - grsDis;
                                Int64 TransNo = Convert.ToInt64(ddlSalesEditInNo_Ret.Text);
                                conn.Open();
                                SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", TOTNET = " + p_NetAmt + ", " +
                                      " USERID = '" + userName + "' where TRANSTP = 'IRTS'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                                cmd1.ExecuteNonQuery();
                                conn.Close();
                            }


                            ScriptManager.RegisterStartupScript(this,
                            this.GetType(), "OpenWindow", "window.open('../Report/Report/rptSalePurchaseReturnMemoEdit.aspx','_newtab');", true);
                        }
                    }
                    else
                    {

                        string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                        SqlConnection conn = new SqlConnection(connectionString);
                        string userName = HttpContext.Current.Session["UserName"].ToString();

                        DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        string TrDate = transdate.ToString("yyyy/MM/dd");

                        decimal totamt = Convert.ToDecimal(txtTotAmt_Ret.Text);
                        decimal grsDis = Convert.ToDecimal(txtGrossDisAmt_Ret.Text);
                        decimal NetAmt = Convert.ToDecimal(txtNetAmt_Ret.Text);

                        if (btnEdit_Ret.Text == "EDIT")
                        {
                            if (totamt - grsDis == NetAmt)
                            {
                                Int64 TransNo = Convert.ToInt64(txtInNo_Ret.Text);
                                conn.Open();
                                SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", TOTNET = " + NetAmt + ", " +
                                      " USERID = '" + userName + "' where TRANSTP = 'IRTS'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                                cmd1.ExecuteNonQuery();
                                conn.Close();
                            }
                            else
                            {
                                decimal p_NetAmt = totamt - grsDis;

                                Int64 TransNo = Convert.ToInt64(txtInNo.Text);
                                conn.Open();
                                SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", TOTNET = " + p_NetAmt + ", " +
                                      " USERID = '" + userName + "' where TRANSTP = 'IRTS'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                                cmd1.ExecuteNonQuery();
                                conn.Close();
                            }
                        }

                        ScriptManager.RegisterStartupScript(this,
                        this.GetType(), "OpenWindow", "window.open('../Report/Report/rptSalePurchaseReturnMemo.aspx','_newtab');", true);
                    }
                }
            }
            else ////// purchase
            {
                Session["Ret_Type"] = "PURCHASE";
                Session["InvDate_S_Ret"] = "";
                Session["InvNo_S_Ret"] = "";
                Session["InvNoEdit_S_Ret"] = "";
                Session["Memo_S_Ret"] = "";
                Session["StoreNM_S_Ret"] = "";
                Session["StoreID_S_Ret"] = "";
                Session["PartyNM_S_Ret"] = "";
                Session["PartyID_S_Ret"] = "";

                if (txtTInDt_Ret.Text == "")
                {
                    lblSaleFrom_Ret.Visible = true;
                    lblSaleFrom_Ret.Text = "Select Date.";
                }
                else if (txtInNo_Ret.Text == "")
                {
                    lblSaleFrom_Ret.Visible = true;
                    lblSaleFrom_Ret.Text = "Select Invoice No.";
                }
                else if (txtSlFr_Ret.Text == "")
                {
                    lblSaleFrom_Ret.Visible = true;
                    lblSaleFrom_Ret.Text = "Select Store.";
                    txtSaleFrom_Ret.Focus();
                }
                else if (txtPID_Ret.Text == "")
                {
                    lblSaleFrom_Ret.Visible = true;
                    lblSaleFrom_Ret.Text = "Select Party.";
                    txtPSNM_Ret.Focus();
                }
                else
                {

                    lblSaleFrom_Ret.Visible = false;

                    Session["InvDate_S_Ret"] = txtTInDt_Ret.Text;
                    Session["InvNo_S_Ret"] = txtInNo_Ret.Text;
                    Session["InvNoEdit_S_Ret"] = ddlSalesEditInNo_Ret.Text;
                    Session["Memo_S_Ret"] = txtSLMNo_Ret.Text;
                    Session["StoreNM_S_Ret"] = txtSaleFrom_Ret.Text;
                    Session["StoreID_S_Ret"] = txtSlFr_Ret.Text;
                    Session["PartyNM_S_Ret"] = txtPSNM_Ret.Text;
                    Session["PartyID_S_Ret"] = txtPID_Ret.Text;

                    if (btnEdit_Ret.Text == "NEW")
                    {
                        if (ddlSalesEditInNo_Ret.Text == "Select")
                        {
                            lblSaleFrom_Ret.Visible = true;
                            lblSaleFrom_Ret.Text = "Select Invoice No";
                            ddlSalesEditInNo_Ret.Focus();
                        }
                        else
                        {
                            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                            SqlConnection conn = new SqlConnection(connectionString);
                            string userName = HttpContext.Current.Session["UserName"].ToString();

                            DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            string TrDate = transdate.ToString("yyyy/MM/dd");

                            decimal totamt = Convert.ToDecimal(txtTotAmt_Ret.Text);
                            decimal grsDis = Convert.ToDecimal(txtGrossDisAmt_Ret.Text);
                            decimal NetAmt = Convert.ToDecimal(txtNetAmt_Ret.Text);

                            if (totamt - grsDis == NetAmt)
                            {
                                Int64 TransNo = Convert.ToInt64(ddlSalesEditInNo_Ret.Text);
                                conn.Open();
                                SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", TOTNET = " + NetAmt + ", " +
                                      " USERID = '" + userName + "' where TRANSTP = 'IRTB'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                                cmd1.ExecuteNonQuery();
                                conn.Close();
                            }
                            else
                            {
                                decimal p_NetAmt = totamt - grsDis;
                                Int64 TransNo = Convert.ToInt64(ddlSalesEditInNo_Ret.Text);
                                conn.Open();
                                SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", TOTNET = " + p_NetAmt + ", " +
                                      " USERID = '" + userName + "' where TRANSTP = 'IRTB'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                                cmd1.ExecuteNonQuery();
                                conn.Close();
                            }


                            ScriptManager.RegisterStartupScript(this,
                            this.GetType(), "OpenWindow", "window.open('../Report/Report/rptSalePurchaseReturnMemoEdit.aspx','_newtab');", true);
                        }
                    }
                    else
                    {

                        string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                        SqlConnection conn = new SqlConnection(connectionString);
                        string userName = HttpContext.Current.Session["UserName"].ToString();

                        DateTime transdate = DateTime.Parse(txtTInDt_Ret.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        string TrDate = transdate.ToString("yyyy/MM/dd");

                        decimal totamt = Convert.ToDecimal(txtTotAmt_Ret.Text);
                        decimal grsDis = Convert.ToDecimal(txtGrossDisAmt_Ret.Text);
                        decimal NetAmt = Convert.ToDecimal(txtNetAmt_Ret.Text);

                        if (btnEdit_Ret.Text == "EDIT")
                        {
                            if (totamt - grsDis == NetAmt)
                            {
                                Int64 TransNo = Convert.ToInt64(txtInNo_Ret.Text);
                                conn.Open();
                                SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", TOTNET = " + NetAmt + ", " +
                                      " USERID = '" + userName + "' where TRANSTP = 'IRTB'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                                cmd1.ExecuteNonQuery();
                                conn.Close();
                            }
                            else
                            {
                                decimal p_NetAmt = totamt - grsDis;

                                Int64 TransNo = Convert.ToInt64(txtInNo.Text);
                                conn.Open();
                                SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", TOTNET = " + p_NetAmt + ", " +
                                      " USERID = '" + userName + "' where TRANSTP = 'IRTB'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                                cmd1.ExecuteNonQuery();
                                conn.Close();
                            }
                        }

                        ScriptManager.RegisterStartupScript(this,
                        this.GetType(), "OpenWindow", "window.open('../Report/Report/rptSalePurchaseReturnMemo.aspx','_newtab');", true);
                    }
                }
            }
            //}
            //else ///// on edit
            //{
            //}
        }

        protected void txtGrossDisAmt_Ret_TextChanged(object sender, EventArgs e)
        {
            decimal totAmt = 0;
            decimal grDisAmt = 0;
            decimal ntAmt = 0;

            totAmt = Convert.ToDecimal(txtTotAmt_Ret.Text);
            if (txtGrossDisAmt_Ret.Text == "")
            {
                txtGrossDisAmt_Ret.Text = "0";
            }
            else
                txtGrossDisAmt_Ret.Text = txtGrossDisAmt_Ret.Text;
            grDisAmt = Convert.ToDecimal(txtGrossDisAmt_Ret.Text);
            ntAmt = totAmt - grDisAmt;
            string NetAmount = SpellAmount.comma(ntAmt);
            txtNetAmt_Ret.Text = NetAmount;
            btnComplete_Ret.Focus();
        }
    }
}