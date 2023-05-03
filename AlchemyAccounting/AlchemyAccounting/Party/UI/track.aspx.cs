using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace AlchemyAccounting.Party.UI
{
    public partial class track : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(Global.connection);
        SqlConnection conn;
        SqlCommand cmdd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["email"] == null)
            {
                Response.Redirect("~/Login/UI/PartyLogin.aspx");
            }
            else
            {
                string email = Session["email"].ToString();
                
                string query = "Select PARTYID From CNF_PARTY where LOGINID=@LoginId";
                string party = "";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@LoginId", email);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    party = dr["PARTYID"].ToString();
                }
                dr.Close();

                lblpartyid.Text = party;
                Session["PartyID"] = null;
                Session["PartyID"] = lblpartyid.Text;
                Session["dropselect"] = ddltrackselect.Text;
                Session["email"] = email;

                string queryname = @"SELECT    distinct GL_ACCHART.ACCOUNTNM AS PARTY
                                FROM         CNF_JOB INNER JOIN
                                      GL_ACCHART ON CNF_JOB.PARTYID = GL_ACCHART.ACCOUNTCD
                                    WHERE     (CNF_JOB.PARTYID = @partyid)";
                SqlCommand cmdname = new SqlCommand(queryname, con);
                cmd.Parameters.Clear();
                cmdname.Parameters.AddWithValue("@partyid", party);
                SqlDataReader drparty = cmdname.ExecuteReader();
                while (drparty.Read())
                {
                    lblPartyNM.Text = drparty["PARTY"].ToString();
                }
                con.Close();
                GridShow();
            }
        }


        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListJob_No_Year_Type(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            
            string partyid = HttpContext.Current.Session["PartyID"].ToString().Trim();
            string ddltext = HttpContext.Current.Session["dropselect"].ToString().Trim();

            string query = "";

            if (ddltext == "Invoice Number")
            {
                query = "Select DOCINVNO As AUTOCOM From CNF_JOB where DOCINVNO like '" + prefixText + "%' and PARTYID='" + partyid + "'";
            }
            else if (ddltext == "Permit Number")
            {
                query = "Select PERMITNO As AUTOCOM From CNF_JOB where PERMITNO like '" + prefixText + "%' and PARTYID='" + partyid + "'";
            }
            else if (ddltext == "House B/L")
            {
                query = "Select HBLNO As AUTOCOM From CNF_JOB where HBLNO like '" + prefixText + "%' and PARTYID='" + partyid + "'";
            }
            else if (ddltext == "House AWB Number")
            {
                query = "Select HAWBNO As AUTOCOM From CNF_JOB where HAWBNO like '" + prefixText + "%' and PARTYID='" + partyid + "'";
            }
            
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (oReader.Read())
            {
                CompletionSet.Add(oReader["AUTOCOM"].ToString());
            }
            return CompletionSet.ToArray();

        }

        protected void txtJobID_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddltrackselect_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            Session["dropselect"] = ddltrackselect.Text;
            txtjob.Text = "";
        }

        protected void txtjob_TextChanged(object sender, EventArgs e)
        {
            string ddldata = "";
            string textdata = txtjob.Text.Trim();
            string ddlselecttext = ddltrackselect.Text.Trim();
            string partyid = HttpContext.Current.Session["PartyID"].ToString().Trim();

            if (ddlselecttext == "Invoice Number")
            {
                ddldata = "DOCINVNO";
            }
            else if (ddlselecttext == "Permit Number")
            {
                ddldata = "PERMITNO";
            }
            else if (ddlselecttext == "House B/L")
            {
                ddldata = "HBLNO";
            }
            else if (ddlselecttext == "House AWB Number")
            {
                ddldata = "HAWBNO";
            }


            Global.txtAdd("Select JOBTP From CNF_JOB where PARTYID='" + partyid + "' and " + ddldata + "='" + textdata + "'", txtjobtp);
            Global.txtAdd("Select JOBYY From CNF_JOB where PARTYID='" + partyid + "' and " + ddldata + "='" + textdata + "'", txtjobyear);
            Global.txtAdd("Select JOBNO From CNF_JOB where PARTYID='" + partyid + "' and " + ddldata + "='" + textdata + "'", txtjobno);

            Global.txtAdd("Select JOBCDT From CNF_JOB where PARTYID='" + partyid + "' and " + ddldata + "='" + textdata + "'", txtDate);
            Global.txtAdd("Select BENO From CNF_JOB where PARTYID='" + partyid + "' and " + ddldata + "='" + textdata + "'", txtbill);
            Global.txtAdd("Select HBLNO From CNF_JOB where PARTYID='" + partyid + "' and " + ddldata + "='" + textdata + "'", txthousebill);
            Global.txtAdd("Select PKGS From CNF_JOB where PARTYID='" + partyid + "' and " + ddldata + "='" + textdata + "'", txtCount);
            Global.txtAdd("Select LCNO From CNF_JOB where PARTYID='" + partyid + "' and " + ddldata + "='" + textdata + "'", txtLcNo);
            Global.txtAdd("Select DOCINVNO From CNF_JOB where PARTYID='" + partyid + "' and " + ddldata + "='" + textdata + "'", txtInvNo);
            Global.txtAdd("Select HAWBNO From CNF_JOB where PARTYID='" + partyid + "' and " + ddldata + "='" + textdata + "'", txtAWBHbill);
            Global.txtAdd("Select PERMITNO From CNF_JOB where PARTYID='" + partyid + "' and " + ddldata + "='" + textdata + "'", txtPermit);
            
            GridShow();
        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        private void GridShow()
        {
            string jobno = txtjobno.Text;
            string jootp = txtjobtp.Text;
            string jobyear = txtjobyear.Text;

            cmdd = new SqlCommand(@"Select CONVERT(varchar(10),STATSDT,105) AS STATSDT, STATUS, REMARKS From CNF_DOCSTATS 
                        where JOBNO=@JOBNO and JOBTP=@JOBTP and JOBYY=@JOBYY", con);
            cmdd.Parameters.Clear();
            cmdd.Parameters.AddWithValue("@JOBNO", jobno);
            cmdd.Parameters.AddWithValue("@JOBTP", jootp);
            cmdd.Parameters.AddWithValue("@JOBYY", jobyear);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();


            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                gvDetails.Visible = true;

            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                int columncount = gvDetails.Rows[0].Cells.Count;
                gvDetails.Rows[0].Cells.Clear();
                gvDetails.Rows[0].Cells.Add(new TableCell());
                gvDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                gvDetails.Rows[0].Cells[0].Text = "Do not found any status";
                
            }
        }
        

    }
}