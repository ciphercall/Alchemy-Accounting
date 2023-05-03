using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AlchemyAccounting.Login.Interface;

namespace AlchemyAccounting.Login.DataAccess
{
    public class LoginDataAccess
    {
        SqlCommand cmd;
        SqlConnection con;
        //LoginInterface iob = new LoginInterface();
        public LoginDataAccess()
        {
            con = new SqlConnection(Global.connection);
            cmd = new SqlCommand("", con);
        }
        public DataSet showdata(String empid)
        {
            DataSet ds = new DataSet();
            DataTable tableInfo = new DataTable();
            DataTable tblDependent = new DataTable();
            DataTable tableAcademic = new DataTable();
            DataTable tableTraining = new DataTable();
            DataTable tableProfessional = new DataTable();
            DataTable tableExp = new DataTable();
            DataTable tableLanguage = new DataTable();
            DataTable tableSpecial = new DataTable();
            DataTable tableDoc = new DataTable();
            ds.Tables.Add(tableInfo);
            ds.Tables.Add(tblDependent);
            ds.Tables.Add(tableAcademic);
            ds.Tables.Add(tableTraining);
            ds.Tables.Add(tableProfessional);
            ds.Tables.Add(tableExp);
            ds.Tables.Add(tableLanguage);
            ds.Tables.Add(tableSpecial);
            ds.Tables.Add(tableDoc);             

            tableInfo.Clear();
            tblDependent.Clear();
            tableAcademic.Clear();
            tableTraining.Clear();
            tableProfessional.Clear();
            tableExp.Clear();
            tableLanguage.Clear();
            tableSpecial.Clear();
            tableDoc.Clear();
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * From Employee_Info Where EmpID = @EmpID";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = empid;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds.Tables[0]);
                cmd.CommandText = "Select DepNo,DepName,DepRelation,DepGender,DepDoB,DepBloodGroup,DepImage From Employee_Dependent Where EmpID = @EmpID";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = empid;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds.Tables[1]);
                cmd.CommandText = "SELECT ExamTitle, [Group/Major], Institute, Result, YearOfPassing, Duration, Achievement FROM Emp_AcademicInfo Where EmpID =@EmpId";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@EmpId", SqlDbType.VarChar).Value = empid;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds.Tables[2]);
                cmd.CommandText = "SELECT TrainingTitle,Topic,Institute,Country,Location,Year,Duration FROM Employee_Training Where EmpID = @EmpId";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@EmpId", SqlDbType.VarChar).Value = empid;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds.Tables[3]);
                cmd.CommandText = "SELECT Certification,Institute,Location,FromDate,ToDate FROM Employee_Professionalinfo Where EmpID = @EmpId";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@EmpId", SqlDbType.VarChar).Value = empid;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds.Tables[4]);
                cmd.CommandText = "SELECT CompanyName,CompanyBusiness,CompLocation,Position,Dept,Responsibilities,ExpDuration,AreaofExp FROM Employee_Experience Where EmpID = @EmpId";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@EmpId", SqlDbType.VarChar).Value = empid;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds.Tables[5]);
                cmd.CommandText = "SELECT LanguageName,Reading,Writing,Speaking FROM Employee_LanguageProficiency Where EmpID = @EmpId";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@EmpId", SqlDbType.VarChar).Value = empid;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds.Tables[6]);
                cmd.CommandText = "SELECT FieldOfSpecialization,Description FROM Employee_Specialization Where EmpID = @EmpId";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@EmpId", SqlDbType.VarChar).Value = empid;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds.Tables[7]);
                cmd.CommandText = "SELECT DocType,DocFor,DocName From Document Where EmpID = @EmpID";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = empid;
                adapter.Fill(ds.Tables[8]);
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch { }
            return ds;
        }
        public DataTable showEmpLoginInfo(string id,string pass )
        {
            DataTable table = new DataTable();
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select UserID,Password  From User_Registration Where UserID = '"+id+"' and Password ='"+pass+"'" ;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch { }
            return table;            
        }
        public DataTable birthDayInfo(String id)
        {
            DataTable table = new DataTable();
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select EmpName, DOB From Employee_Info Where EmpID<>'"+id+"'";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch { }
            return table;
        }
        public String InsertEmpLoginInfo(LoginInterface iob)
        {
            string str = "";
            SqlConnection conn = new SqlConnection(Global.connection);
            SqlCommand cmmd = new SqlCommand("", conn);
            //DataTable table = new DataTable();
            //SqlTransaction tran = null;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
               // tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmmd.CommandText = "Insert into User_Registration (UserID,Name, Password, Email) " +
                                   "Values(@UserId,@Name,@Password,@Email)";
                cmmd.Parameters.Clear();
                cmmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = iob.UserID;
                cmmd.Parameters.Add("@Name",SqlDbType.NVarChar).Value =iob.Name;
                cmmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = iob.Password;
                cmmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = iob.Email;
                //cmmd.Parameters.Add("@SecurityQ", SqlDbType.VarChar).Value = iob.SecurityQ;
                //cmmd.Parameters.Add("@SecurityA", SqlDbType.VarChar).Value = iob.SecurityA;
                //cmd.Transaction = tran;
                cmmd.ExecuteNonQuery();
               // tran.Commit();
                str = "Registration Successfully......";

                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            catch (Exception ex)
            {
                //tran.Rollback();
                str = ex.Message;
            }
            return str;
        }
        public String InsertLogonHistory(String Name, DateTime Intime, String comName, DateTime date, String domain, String ip)
        {
            String str = "";
            try
            {
                SqlConnection connection = new SqlConnection(Global.connection);//.ConnectionInfo.GenerateString());
                connection.Open();
                SqlCommand cmd = new SqlCommand(@"exec sp_InsertLogOnHistory @userName
                                              ,@loginTime
                                              ,@computerName
                                              ,@LoginDate                                              
                                              ,@domainName
                                              ,@ipAddress ", connection);
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = Name;
                cmd.Parameters.Add("@loginTime", SqlDbType.DateTime).Value = Intime;
                cmd.Parameters.Add("@computerName", SqlDbType.VarChar).Value = comName;
                cmd.Parameters.Add("@LoginDate", SqlDbType.DateTime).Value = date;
                //cmd.Parameters.Add("@LoginOutTime", SqlDbType.DateTime).Value = Outtime;
                cmd.Parameters.Add("@domainName", SqlDbType.VarChar).Value = domain;
                cmd.Parameters.Add("@ipAddress", SqlDbType.VarChar).Value = ip;
                cmd.ExecuteNonQuery();
                str = "Success";
                connection.Close();
            }
            catch (Exception er)
            {
                str = er.Message;
            }
            return str;
        }
    }
}
