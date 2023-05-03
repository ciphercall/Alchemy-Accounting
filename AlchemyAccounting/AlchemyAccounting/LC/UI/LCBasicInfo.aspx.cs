using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Configuration;
using AlchemyAccounting;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using System.Collections.Specialized;
using AlchemyAccounting.LC.DataAccess;
using AlchemyAccounting.LC.Interface;

namespace AlchemyAccounting.LC.UI
{
    public partial class LCBasicInfo : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlLCType.Focus();

                DateTime today = DateTime.Today.Date;
                string td = Global.Dayformat(today);
                txtLCDT.Text = td;

                if (ddlLCType.Text == "IMPORT")
                {
                    Global.lblAdd(@"SELECT MAX(LCID) FROM LC_BASIC WHERE LCTP ='IMPORT'", lblLcID);
                    string ItemCD;
                    string mxCD = "";
                    string mid = "";
                    string subItemCD = "";
                    int subCD, incrItCD;
                    if (lblLcID.Text == "")
                    {
                        ItemCD = "00001";
                    }
                    else
                    {
                        mxCD = lblLcID.Text;
                        //OItemCD = mxCD.Substring(4,4);
                        subCD = int.Parse(mxCD);
                        incrItCD = subCD + 1;
                        if (incrItCD < 10)
                        {
                            mid = incrItCD.ToString();
                            subItemCD = "0000" + mid;
                        }
                        else if (incrItCD < 100)
                        {
                            mid = incrItCD.ToString();
                            subItemCD = "000" + mid;
                        }
                        else if (incrItCD < 1000)
                        {
                            mid = incrItCD.ToString();
                            subItemCD = "00" + mid;
                        }
                        else if (incrItCD < 10000)
                        {
                            mid = incrItCD.ToString();
                            subItemCD = "0" + mid;
                        }
                        //else
                        //    subItemCD = incrItCD.ToString();

                        ItemCD = subItemCD;
                    }

                    txtLCID.Text = ItemCD;
                }
            }
        }

        protected void ddlLCType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLCType.Text == "IMPORT")
            {
                Global.lblAdd(@"SELECT MAX(LCID) FROM LC_BASIC WHERE LCTP ='IMPORT'", lblLcID);
                string ItemCD;
                string mxCD = "";
                string mid = "";
                string subItemCD = "";
                int subCD, incrItCD;
                if (lblLcID.Text == "")
                {
                    ItemCD = "00001";
                }
                else
                {
                    mxCD = lblLcID.Text;
                    //OItemCD = mxCD.Substring(4,4);
                    subCD = int.Parse(mxCD);
                    incrItCD = subCD + 1;
                    if (incrItCD < 10)
                    {
                        mid = incrItCD.ToString();
                        subItemCD = "0000" + mid;
                    }
                    else if (incrItCD < 100)
                    {
                        mid = incrItCD.ToString();
                        subItemCD = "000" + mid;
                    }
                    else if (incrItCD < 1000)
                    {
                        mid = incrItCD.ToString();
                        subItemCD = "00" + mid;
                    }
                    else if (incrItCD < 10000)
                    {
                        mid = incrItCD.ToString();
                        subItemCD = "0" + mid;
                    }
                    //else
                    //    subItemCD = incrItCD.ToString();

                    ItemCD = subItemCD;
                }

                txtLCID.Text = ItemCD;
            }
            else
            {
                Global.lblAdd(@"SELECT MAX(LCID) FROM LC_BASIC WHERE LCTP ='IMPORT'", lblLcID);
                string ItemCD;
                string mxCD = "";
                string mid = "";
                string subItemCD = "";
                int subCD, incrItCD;
                if (lblLcID.Text == "")
                {
                    ItemCD = "00001";
                }
                else
                {
                    mxCD = lblLcID.Text;
                    //OItemCD = mxCD.Substring(4,4);
                    subCD = int.Parse(mxCD);
                    incrItCD = subCD + 1;
                    if (incrItCD < 10)
                    {
                        mid = incrItCD.ToString();
                        subItemCD = "0000" + mid;
                    }
                    else if (incrItCD < 100)
                    {
                        mid = incrItCD.ToString();
                        subItemCD = "000" + mid;
                    }
                    else if (incrItCD < 1000)
                    {
                        mid = incrItCD.ToString();
                        subItemCD = "00" + mid;
                    }
                    else if (incrItCD < 10000)
                    {
                        mid = incrItCD.ToString();
                        subItemCD = "0" + mid;
                    }
                    //else
                    //    subItemCD = incrItCD.ToString();

                    ItemCD = subItemCD;
                }

                txtLCID.Text = ItemCD;
            }

            txtLCNo.Focus();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT * FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) IN ('1020102') AND ACCOUNTNM LIKE '" + prefixText + "%' AND STATUSCD = 'P'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtBankNM_TextChanged(object sender, EventArgs e)
        {
            Global.txtAdd(@"Select ACCOUNTCD from GL_ACCHART where ACCOUNTNM = '" + txtBankNM.Text + "' AND STATUSCD = 'P'", txtBankCD);
            txtImportrNM.Focus();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListImporter(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT IMPORTERNM FROM LC_BASIC WHERE IMPORTERNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["IMPORTERNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtImportrNM_TextChanged(object sender, EventArgs e)
        {
            txtBeneficiary.Focus();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListBeneficiary(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT BENEFICIARY FROM LC_BASIC WHERE BENEFICIARY LIKE '" + prefixText + "%' ", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["BENEFICIARY"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtBeneficiary_TextChanged(object sender, EventArgs e)
        {
            txtSCPINo.Focus();
        }

        protected void txtSCPIDT_TextChanged(object sender, EventArgs e)
        {
            txtMcNM.Focus();
        }

        protected void txtMcDT_TextChanged(object sender, EventArgs e)
        {
            txtMPINo.Focus();
        }

        protected void txtMPIDT_TextChanged(object sender, EventArgs e)
        {
            txtLcUSD.Focus();
        }

        protected void txtLcUSD_TextChanged(object sender, EventArgs e)
        {
            txtLCExRT.Focus();
        }

        protected void txtLCExRT_TextChanged(object sender, EventArgs e)
        {
            decimal lcUSd = Convert.ToDecimal(txtLcUSD.Text);
            decimal lcERT = Convert.ToDecimal(txtLCExRT.Text);
            decimal lcBDT = lcUSd * lcERT;
            string LcTK = SpellAmount.comma(lcBDT);
            txtLCBDT.Text = LcTK;

            txtRemarks.Focus();
        }

        protected void txtLCDT_TextChanged(object sender, EventArgs e)
        {
            txtBankNM.Focus();
            if (btnLCEdit.Text == "New")
            {
                DateTime LcDT = new DateTime();
                string lcDate = "";
                LcDT = DateTime.Parse(txtLCDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lcDate = LcDT.ToString("yyyy/MM/dd");

                Global.dropDownAddWithSelect(ddlLcID, "SELECT LCID FROM LC_BASIC WHERE LCDT = '" + lcDate + "'");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string query = "";
            SqlCommand comm = new SqlCommand(query, con);

            AlchemyAccounting.LC.DataAccess.LCDataAcces dob = new DataAccess.LCDataAcces();
            AlchemyAccounting.LC.Interface.LCInterface iob = new Interface.LCInterface();

            if (txtBankCD.Text == "")
            {
                Response.Write("<script>alert('Please Select Bank Name.');</script>");
                txtBankNM.Focus();
            }
            else if (txtLcUSD.Text == "")
            {
                Response.Write("<script>alert('Please Type LC USD.');</script>");
                txtLcUSD.Focus();
            }
            else if (txtLCExRT.Text == "")
            {
                Response.Write("<script>alert('Please Type LC Exchange Rate.');</script>");
                txtLCExRT.Focus();
            }
            else if (txtLCBDT.Text == "")
            {
                Response.Write("<script>alert('Please Type LC BDT.');</script>");
                txtLCBDT.Focus();
            }
            else
            {
                try
                {
                    if (btnSave.Text == "Save")
                    {
                        if (txtLCNo.Text == "")
                        {
                            Response.Write("<script>alert('Please Type LC No.');</script>");
                            txtLCNo.Focus();
                        }
                        else
                        {
                        iob.LcTp = ddlLCType.Text;
                        iob.LcID = txtLCID.Text;
                        iob.LcNo = txtLCNo.Text;
                        iob.LcDT = DateTime.Parse(txtLCDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        iob.BnkCD = txtBankCD.Text;
                        iob.ImporterNM = txtImportrNM.Text;
                        iob.Beneficiary = txtBeneficiary.Text;
                        iob.ScipNO = txtSCPINo.Text;
                        if (txtSCPIDT.Text == "")
                        {
                            iob.ScipDT = (DateTime.Parse("01/01/1900", dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
                        }
                        else
                        {
                            iob.ScipDT = (DateTime.Parse(txtSCPIDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
                        }
                        iob.McNM = txtMcNM.Text;
                        iob.McNO = txtMcNo.Text;
                        if (txtMcDT.Text == "")
                        {
                            iob.McDT = (DateTime.Parse("01/01/1900", dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
                        }
                        else
                        {
                            iob.McDT = (DateTime.Parse(txtMcDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
                        }
                        iob.MpiNO = txtMPINo.Text;
                        if (txtMPIDT.Text == "")
                        {
                            iob.MpiDT = (DateTime.Parse("01/01/1900", dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
                        }
                        else
                        {
                            iob.MpiDT = (DateTime.Parse(txtMPIDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
                        }
                        iob.LcUSD = Convert.ToDecimal(txtLcUSD.Text);
                        iob.LcERT = Convert.ToDecimal(txtLCExRT.Text);
                        iob.LcBDT = Convert.ToDecimal(txtLCBDT.Text);
                        iob.Remarks = txtRemarks.Text;
                        iob.Usernm = userName;

                        dob.insertLC(iob);

                        string LcAccCD = "";
                        string LcCntrCD = "";
                        string LcAccNM = "";
                         
                        if (ddlLCType.Text == "IMPORT")
                        {
                            LcAccCD = "4010103" + txtLCID.Text;
                            LcCntrCD = "4010103" + "00000";
                            LcAccNM = "L/C NO : " + txtLCNo.Text;

                            DateTime openDt= DateTime.Now;
                            string opnDate = openDt.ToString("yyyy/MM/dd");

                            con.Open();
                            SqlCommand cmd = new SqlCommand("SELECT CONTROLCD from GL_ACCHARTMST WHERE CONTROLCD = '" + LcCntrCD + "'", con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            con.Close();
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                query = (" INSERT INTO GL_ACCHART (ACCOUNTCD, ACCOUNTNM, OPENINGDT, LEVELCD, CONTROLCD, ACCOUNTTP, STATUSCD, ACTIVE, USERPC, USERID) " +
                                         " VALUES ('" + LcAccCD + "','" + LcAccNM + "','" + opnDate + "',5,'" + LcCntrCD + "','D','P','A','','" + userName + "')");

                                comm = new SqlCommand(query, con);

                                con.Open();
                                int result1 = comm.ExecuteNonQuery();
                                con.Close();
                            }
                            else
                            {
                            query = (" INSERT INTO GL_ACCHARTMST(CONTROLCD, USERID, USERPC) " +
                                     " VALUES ('" + LcCntrCD + "','" + userName + "','')");

                            comm = new SqlCommand(query, con);
                            con.Open();
                            int result = comm.ExecuteNonQuery();
                            con.Close();

                            query = (" INSERT INTO GL_ACCHART (ACCOUNTCD, ACCOUNTNM, OPENINGDT, LEVELCD, CONTROLCD, ACCOUNTTP, STATUSCD, ACTIVE, USERPC, USERID) " +
                                     " VALUES ('" + LcAccCD + "','" + LcAccNM + "','" + opnDate + "',5,'" + LcCntrCD + "','D','P','A','','" + userName + "')");

                            comm = new SqlCommand(query, con);

                            con.Open();
                            int result1 = comm.ExecuteNonQuery();
                            con.Close();
                            }

                            
                        }
                        else
                        {
                            LcAccCD = "4010104" + txtLCID.Text;
                            LcCntrCD = "4010104" + "00000";
                            LcAccNM = "L/C NO : " + txtLCNo.Text;
                        }

                        Refresh();
                        ddlLCType.Focus();
                        }
                    }
                    else
                    {
                        //if (ddlLcID.Text == "Select")
                        //{
                        //    Response.Write("<script>alert('Please Select LC No.');</script>");
                        //    ddlLcID.Focus();
                        //}
                        //else if (txtLcEdit.Text == "")
                        //{
                        //}
                        //else
                        //{
                            DateTime LcDT = new DateTime();
                            DateTime scpiDT = new DateTime();
                            DateTime mcDT = new DateTime();
                            DateTime mpDT = new DateTime();

                            string lcDate = "";
                            string scpiDate = "";
                            string mcDate = "";
                            string mpiDate = "";

                            LcDT = DateTime.Parse(txtLCDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            lcDate = LcDT.ToString("yyyy/MM/dd");

                            if (txtSCPIDT.Text == "")
                            {
                                scpiDT = (DateTime.Parse("01/01/1900", dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
                                scpiDate = scpiDT.ToString("yyyy/MM/dd");
                            }
                            else
                            {
                                scpiDT = DateTime.Parse(txtSCPIDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                scpiDate = scpiDT.ToString("yyyy/MM/dd");
                            }

                            if (txtMcDT.Text == "")
                            {
                                mcDT = (DateTime.Parse("01/01/1900", dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
                                mcDate = mcDT.ToString("yyyy/MM/dd");
                            }
                            else
                            {
                                mcDT = DateTime.Parse(txtMcDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                mcDate = mcDT.ToString("yyyy/MM/dd");
                            }

                            if (txtMPIDT.Text == "")
                            {
                                mpDT = (DateTime.Parse("01/01/1900", dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
                                mpiDate = mpDT.ToString("yyyy/MM/dd");
                            }
                            else
                            {
                                mpDT = DateTime.Parse(txtMPIDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                mpiDate = mpDT.ToString("yyyy/MM/dd");
                            }
                            decimal LCVUSD = Convert.ToDecimal(txtLcUSD.Text);
                            decimal LCVERT = Convert.ToDecimal(txtLCExRT.Text);
                            decimal LCVBDT = Convert.ToDecimal(txtLCBDT.Text);
                            con.Open();
                            SqlCommand cmd = new SqlCommand("update LC_BASIC set LCTP = '" + ddlLCType.Text + "', BANKCD='" + txtBankCD.Text + "', LCNO='" + txtLcEdit.Text + "', LCDT = '" + lcDate + "', IMPORTERNM= '" + txtImportrNM.Text + "', BENEFICIARY ='" + txtBeneficiary.Text + "', SCPINO = '" + txtSCPINo.Text + "', SCPIDT = '" + scpiDate + "', MCNM = '" + txtMcNM.Text + "', MCNO = '" + txtMcNo.Text + "', " +
                                  " MCDT = '" + mcDate + "', MPINO = '" + txtMPINo.Text + "', MPIDT = '" + mpiDate + "', LCVUSD = " + LCVUSD + ", LCVERT = " + LCVERT + ", LCVBDT = " + LCVBDT + ",REMARKS ='" + txtRemarks.Text + "', USERID = '" + userName + "' where LCID = '" + ddlLcID.Text + "'", con);
                            cmd.ExecuteNonQuery();
                            con.Close();

                            con.Open();
                            string AccCD = "4010103" + ddlLcID.Text;
                            string LcAccNM = "L/C NO : " + txtLcEdit.Text;

                            SqlCommand cmd1 = new SqlCommand(" UPDATE GL_ACCHART SET ACCOUNTNM = @LcAccNM WHERE ACCOUNTCD = '" + AccCD + "'", con);
                            cmd1.Parameters.AddWithValue("@LcAccNM", LcAccNM);
                            cmd1.ExecuteNonQuery();
                            con.Close();

                            Refresh();
                        //}
                        
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        public void Refresh()
        {
            if (btnLCEdit.Text == "Edit")
            {
                ddlLCType.SelectedIndex = -1;

                if (ddlLCType.Text == "IMPORT")
                {
                    Global.lblAdd(@"SELECT MAX(LCID) FROM LC_BASIC WHERE LCTP ='IMPORT'", lblLcID);
                    string ItemCD;
                    string mxCD = "";
                    string mid = "";
                    string subItemCD = "";
                    int subCD, incrItCD;
                    if (lblLcID.Text == "")
                    {
                        ItemCD = "00001";
                    }
                    else
                    {
                        mxCD = lblLcID.Text;
                        //OItemCD = mxCD.Substring(4,4);
                        subCD = int.Parse(mxCD);
                        incrItCD = subCD + 1;
                        if (incrItCD < 10)
                        {
                            mid = incrItCD.ToString();
                            subItemCD = "0000" + mid;
                        }
                        else if (incrItCD < 100)
                        {
                            mid = incrItCD.ToString();
                            subItemCD = "000" + mid;
                        }
                        else if (incrItCD < 1000)
                        {
                            mid = incrItCD.ToString();
                            subItemCD = "00" + mid;
                        }
                        else if (incrItCD < 10000)
                        {
                            mid = incrItCD.ToString();
                            subItemCD = "0" + mid;
                        }
                        //else
                        //    subItemCD = incrItCD.ToString();

                        ItemCD = subItemCD;
                    }

                    txtLCID.Text = ItemCD;
                }
                txtLCNo.Text = "";
                txtLcEdit.Text = "";
                txtBankNM.Text = "";
                txtBankCD.Text = "";
                txtImportrNM.Text = "";
                txtBeneficiary.Text = "";
                txtSCPINo.Text = "";
                txtSCPIDT.Text = "";
                txtMcNM.Text = "";
                txtMcNo.Text = "";
                txtMcDT.Text = "";
                txtMPINo.Text = "";
                txtMPIDT.Text = "";
                txtLcUSD.Text = "";
                txtLCExRT.Text = "";
                txtLCBDT.Text = "";
                txtRemarks.Text = "";
                ddlStatus.SelectedIndex = -1;
            }
            else
            {
                ddlLCType.SelectedIndex = -1;
                ddlLcID.SelectedIndex = -1;
                txtLcEdit.Text = "";
                txtLCNo.Text = "";
                DateTime today = DateTime.Today.Date;
                string td = Global.Dayformat(today);
                txtLCDT.Text = td;
                txtBankNM.Text = "";
                txtBankCD.Text = "";
                txtImportrNM.Text = "";
                txtBeneficiary.Text = "";
                txtSCPINo.Text = "";
                txtSCPIDT.Text = "";
                txtMcNM.Text = "";
                txtMcNo.Text = "";
                txtMcDT.Text = "";
                txtMPINo.Text = "";
                txtMPIDT.Text = "";
                txtLcUSD.Text = "";
                txtLCExRT.Text = "";
                txtLCBDT.Text = "";
                txtRemarks.Text = "";
                ddlStatus.SelectedIndex = -1;
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        protected void btnLCEdit_Click(object sender, EventArgs e)
        {
            if (btnLCEdit.Text == "Edit")
            {
                Refresh();
                btnLCEdit.Text = "New";
                
                txtLCID.Visible = false;
                ddlLcID.Visible = true;
                ddlLcID.Focus();
                txtLcEdit.Visible = true;
                txtLCNo.Visible = false;
                Global.dropDownAddWithSelect(ddlLcID, "SELECT LCID FROM LC_BASIC");
                btnSave.Text = "Update";
            }
            else
            {
                Refresh();
                ddlLCType.Focus();
                btnLCEdit.Text = "Edit";
                txtLCID.Visible = true;
                ddlLcID.Visible = false;
                txtLcEdit.Visible = false;
                txtLCNo.Visible = true;
                btnSave.Text = "Save";

                DateTime today = DateTime.Today.Date;
                string td = Global.Dayformat(today);
                txtLCDT.Text = td;
            }
        }

        public DataTable PassParam(String id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            DataTable table = new DataTable();
            try
            { 
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM LC_BASIC where LCID='" + ddlLcID.Text + "' ",con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch { }
            return table;
        }

        public DataTable PassbyName(String name)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            DataTable table = new DataTable();
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM LC_BASIC where LCNO=@LCNO ", con);
                cmd.Parameters.AddWithValue("@LCNO",txtLcEdit.Text);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch { }
            return table;
        }

        public void ShowData()
        {
            DataTable table = new DataTable();

            try
            {
                table = PassParam(ddlLcID.Text);
                DataTableReader reader = new DataTableReader(table);

                if (reader.Read())
                {
                    ddlLCType.Text = reader["LCTP"].ToString();
                    txtLcEdit.Text = reader["LCNO"].ToString();
                    string dt = reader["LCDT"].ToString();
                    DateTime LcDT = new DateTime();
                    LcDT = DateTime.Parse(dt);
                    txtLCDT.Text = Global.Dayformat(LcDT);
                    Global.txtAdd("SELECT GL_ACCHART.ACCOUNTNM FROM LC_BASIC INNER JOIN GL_ACCHART ON LC_BASIC.BANKCD = GL_ACCHART.ACCOUNTCD WHERE (LC_BASIC.LCID = '" + ddlLcID.Text + "') ", txtBankNM);
                    txtBankCD.Text = reader["BANKCD"].ToString();
                    txtImportrNM.Text = reader["IMPORTERNM"].ToString();
                    txtBeneficiary.Text = reader["BENEFICIARY"].ToString();
                    txtSCPINo.Text = reader["SCPINO"].ToString();
                    string bufferSCPI = reader["SCPIDT"].ToString();
                    DateTime SCPIDT = new DateTime();
                    SCPIDT= DateTime.Parse(bufferSCPI);
                    string sDT = Global.Dayformat(SCPIDT);
                    if (sDT == "01/01/1900")
                    {
                        txtSCPIDT.Text = "";
                    }
                    else
                        txtSCPIDT.Text = sDT;
                    txtMcNM.Text = reader["MCNM"].ToString();
                    txtMcNo.Text = reader["MCNO"].ToString();
                    string bufferMCDT = reader["MCDT"].ToString();
                    DateTime MCDT = new DateTime();
                    MCDT = DateTime.Parse(bufferMCDT);
                    string mDT = Global.Dayformat(MCDT);
                    if (mDT == "01/01/1900")
                    {
                        txtMcDT.Text = "";
                    }
                    else
                        txtMcDT.Text = mDT;
                    txtMPINo.Text = reader["MPINO"].ToString();
                    string bufferMPIDT = reader["MPIDT"].ToString();
                    DateTime MPIDT = new DateTime();
                    MPIDT = DateTime.Parse(bufferMPIDT);
                    string mpDT = Global.Dayformat(MPIDT);
                    if (mpDT == "01/01/1900")
                    {
                        txtMPIDT.Text = "";
                    }
                    else
                        txtMPIDT.Text = mpDT;
                    string bufferUSD = reader["LCVUSD"].ToString();
                    decimal LCVUSD = Convert.ToDecimal(bufferUSD);
                    txtLcUSD.Text = SpellAmount.comma(LCVUSD);
                    string bufferLCVERT = reader["LCVERT"].ToString();
                    decimal LCVERT = Convert.ToDecimal(bufferLCVERT);
                    txtLCExRT.Text = SpellAmount.comma(LCVERT);
                    string bufferLCVBDT = reader["LCVBDT"].ToString();
                    decimal LCVBDT = Convert.ToDecimal(bufferLCVBDT);
                    txtLCBDT.Text = SpellAmount.comma(LCVBDT);
                    txtRemarks.Text = reader["REMARKS"].ToString();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public void ShowDatabyName()
        {
            DataTable table = new DataTable();

            try
            {
                table = PassbyName(txtLcEdit.Text);
                DataTableReader reader = new DataTableReader(table);

                if (reader.Read())
                {
                    ddlLCType.Text = reader["LCTP"].ToString();
                    //txtLCNo.Text = reader["LCNO"].ToString();
                    ddlLcID.Text = reader["LCID"].ToString();
                    string dt = reader["LCDT"].ToString();
                    DateTime LcDT = new DateTime();
                    LcDT = DateTime.Parse(dt);
                    txtLCDT.Text = Global.Dayformat(LcDT);
                    Global.txtAdd("SELECT GL_ACCHART.ACCOUNTNM FROM LC_BASIC INNER JOIN GL_ACCHART ON LC_BASIC.BANKCD = GL_ACCHART.ACCOUNTCD WHERE (LC_BASIC.LCNO = '" + txtLcEdit.Text + "') ", txtBankNM);
                    txtBankCD.Text = reader["BANKCD"].ToString();
                    txtImportrNM.Text = reader["IMPORTERNM"].ToString();
                    txtBeneficiary.Text = reader["BENEFICIARY"].ToString();
                    txtSCPINo.Text = reader["SCPINO"].ToString();
                    string bufferSCPI = reader["SCPIDT"].ToString();
                    DateTime SCPIDT = new DateTime();
                    SCPIDT = DateTime.Parse(bufferSCPI);
                    string sDT = Global.Dayformat(SCPIDT);
                    if (sDT == "01/01/1900")
                    {
                        txtSCPIDT.Text = "";
                    }
                    else
                        txtSCPIDT.Text = sDT;
                    txtMcNM.Text = reader["MCNM"].ToString();
                    txtMcNo.Text = reader["MCNO"].ToString();
                    string bufferMCDT = reader["MCDT"].ToString();
                    DateTime MCDT = new DateTime();
                    MCDT = DateTime.Parse(bufferMCDT);
                    string mDT = Global.Dayformat(MCDT);
                    if (mDT == "01/01/1900")
                    {
                        txtMcDT.Text = "";
                    }
                    else
                        txtMcDT.Text = mDT;
                    txtMPINo.Text = reader["MPINO"].ToString();
                    string bufferMPIDT = reader["MPIDT"].ToString();
                    DateTime MPIDT = new DateTime();
                    MPIDT = DateTime.Parse(bufferMPIDT);
                    string mpDT = Global.Dayformat(MPIDT);
                    if (mpDT == "01/01/1900")
                    {
                        txtMPIDT.Text = "";
                    }
                    else
                        txtMPIDT.Text = mpDT;
                    string bufferUSD = reader["LCVUSD"].ToString();
                    decimal LCVUSD = Convert.ToDecimal(bufferUSD);
                    txtLcUSD.Text = SpellAmount.comma(LCVUSD);
                    string bufferLCVERT = reader["LCVERT"].ToString();
                    decimal LCVERT = Convert.ToDecimal(bufferLCVERT);
                    txtLCExRT.Text = SpellAmount.comma(LCVERT);
                    string bufferLCVBDT = reader["LCVBDT"].ToString();
                    decimal LCVBDT = Convert.ToDecimal(bufferLCVBDT);
                    txtLCBDT.Text = SpellAmount.comma(LCVBDT);
                    txtRemarks.Text = reader["REMARKS"].ToString();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void ddlLcID_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowData();
        }

        protected void txtLcEdit_TextChanged(object sender, EventArgs e)
        {
            ShowDatabyName();
            txtLcEdit.Focus();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListLcName(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT * FROM LC_BASIC WHERE LCNO LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["LCNO"].ToString());
            return CompletionSet.ToArray();
        }

    }
}