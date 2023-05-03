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
using System.Collections;

namespace AlchemyAccounting.CNF.report.vis_rep
{
    public partial class rpt_job_incom_bill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                string Companyid = Session["Companyid"].ToString();
                string company = Session["company"].ToString();
                string JOBYY = Session["JOBYY"].ToString();
                string JOBTP = Session["JOBTP"].ToString();
                string branch = Session["branch"].ToString();

                Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID='" + Companyid + "'", lblCompNM);
                Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID='" + Companyid + "'", lblAddress);

                DateTime PrintDate = DateTime.Now;
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblTime.Text = td;

                lblJobYear.Text = JOBYY;
                lblJobType.Text = JOBTP;

                showGrid();
            }
        }

        public void showGrid()
        {
            SqlConnection conn = new SqlConnection(Global.connection);

            string Companyid = Session["Companyid"].ToString();
            string company = Session["company"].ToString();
            string JOBYY = Session["JOBYY"].ToString();
            string JOBTP = Session["JOBTP"].ToString();
            string branch = Session["branch"].ToString();

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT  A.JOBTP, A.JOBYY,A.JOBNO, CNF_JOB.PERMITNO, CNF_JOB.DOCINVNO,GL_ACCHART.ACCOUNTNM  " +   
                           " FROM( SELECT  JOBTP, JOBYY,JOBNO   " +
                                " FROM CNF_JOB " +
                                " except " +
                                " SELECT  JOBTP, JOBYY,JOBNO   " +
                                " FROM CNF_JOBSTATUS) AS A" +
                                " INNER JOIN CNF_JOB ON A.JOBTP = CNF_JOB.JOBTP AND A.JOBYY =CNF_JOB.JOBYY AND A.JOBNO = CNF_JOB.JOBNO  " +
                                " INNER JOIN GL_ACCHART ON CNF_JOB.PARTYID=GL_ACCHART.ACCOUNTCD " +
                                " WHERE CNF_JOB.COMPID='" + Companyid + "' AND CNF_JOB.JOBTP='" + JOBTP + "' AND CNF_JOB.JOBYY=" + JOBYY + " ORDER BY A.JOBNO", conn);
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
                GridView1.Visible = true;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    string JOBNO = DataBinder.Eval(e.Row.DataItem, "JOBNO").ToString();
                    e.Row.Cells[0].Text = JOBNO;

                    string ACCOUNTNM = DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString();
                    e.Row.Cells[1].Text = "&nbsp;" + ACCOUNTNM;

                    string PERMITNO = DataBinder.Eval(e.Row.DataItem, "PERMITNO").ToString();
                    e.Row.Cells[2].Text = "&nbsp;" + PERMITNO;

                    string DOCINVNO = DataBinder.Eval(e.Row.DataItem, "DOCINVNO").ToString();
                    e.Row.Cells[3].Text = "&nbsp;" + DOCINVNO;
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
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