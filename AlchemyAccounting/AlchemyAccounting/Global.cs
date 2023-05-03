using System;
using System.Data;
using System.Collections.Generic;
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


namespace AlchemyAccounting
{
    public class Global
    {
        public static String connection = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ToString();

        public static void  dropDownAdd(DropDownList ob, String sql)
        {
            List<String> List = new List<string>();
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader(); List.Clear();
                //List.Add("Select");
                while (rd.Read())
                {
                    List.Add(rd[0].ToString());
                }
                rd.Close();
                ob.Items.Clear();
                ob.Text = "";
                for (int i = 0; i < List.Count; i++)
                {
                    ob.Items.Add(List[i].ToString());
                }
            }
            catch { }
        }
        public static void DropDownAddTextWithValue(DropDownList ob, String sql)
        {
            List<String> listName = new List<string>();
            List<String> listValue = new List<string>();
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                listName.Clear();
                listValue.Clear();
                listName.Add("Select");
                listValue.Add("Select");
                while (rd.Read())
                {
                    listName.Add(rd[0].ToString());
                    listValue.Add(rd[1].ToString());
                }
                rd.Close();
                ob.Items.Clear();

                ob.Text = "";
                for (int i = 0; i < listName.Count; i++)
                {
                    ob.Items.Add(new ListItem(listName[i].ToUpper(), listValue[i]));
                }
            }
            catch
            {
                //ignore
            }
        }
        public static void dropDownAddWithSelect(DropDownList ob, String sql)
        {
            List<String> List = new List<string>();
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader(); List.Clear();
                List.Add("Select");
                while (rd.Read())
                {
                    List.Add(rd[0].ToString());
                }
                rd.Close();
                ob.Items.Clear();
                ob.Text = "";
                for (int i = 0; i < List.Count; i++)
                {
                    ob.Items.Add(List[i].ToString());
                }
            }
            catch { }
        }

        public static void editableDropDownAdd(DropDownList ob, String sql)
        {
            List<String> List = new List<string>();
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader(); List.Clear();
                List.Add("Select");
                while (rd.Read())
                {
                    List.Add(rd[0].ToString());
                }
                rd.Close();
                ob.Items.Clear();
                ob.Text = "";
                for (int i = 0; i < List.Count; i++)
                {
                    ob.Items.Add(List[i].ToString());
                }
            }
            catch { }
        }

        public static void listAdd(ListBox ob, String sql)
        {
            List<String> List = new List<string>();
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader(); 
                List.Clear();
                while (rd.Read())
                {
                    List.Add(rd[0].ToString());
                }
                rd.Close();
                ob.Items.Clear();
                ob.Text = "";
                for (int i = 0; i < List.Count; i++)
                {
                    ob.Items.Add(List[i].ToString());
                }
            }
            catch { }
        }
        public static void txtAdd(String sql, TextBox txtadd)
        {
            //String mystring = "";
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    txtadd.Text = reader[0].ToString();
                }
                con.Close();
                reader.Close();
            }
            catch { }
            //return List;
        }

        public static void lblAdd(String sql, Label lblAdd)
        {
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblAdd.Text = reader[0].ToString();
                }
                con.Close();
                reader.Close();
            }
            catch { }
        }

        public static void gridViewAdd(GridView ob, String sql)
        {
            DataTable table = new DataTable();
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(table);
                ob.DataSource = table;
                ob.DataBind();
            }
            catch { }
            //return List;
        }

        public static string Dayformat(DateTime dt)
        {
            string mydate = dt.ToString("dd/MM/yyyy");
            return mydate;
        }
        public static string DayformatHifen(DateTime dt)
        {
            string mydate = dt.ToString("dd-MMM-yyyy");
            return mydate;
        }
        public static string TimeFormat(DateTime Tt)
        {
            string myTime = Tt.ToString("HH:mm:ss");
            return myTime;
        }
        public string monformat(DateTime mm)
        {
            string mymonth = mm.ToString("MMM");
            return mymonth;
        }
        public static DateTime Timezone(DateTime datetime)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(datetime, timeZoneInfo);
            return PrintDate;
        }
        public static string StringData(String sql)
        {
            string data = "";
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    data = reader[0].ToString();
                }
                con.Close();
                reader.Close();
            }
            catch
            {
                //ignore
            }
            return data;
        }

        public static string ExecuteCommand(String sql)
        {
            string data = "";
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteReader();
                con.Close();
            }
            catch(Exception ex)
            {
                data = ex.ToString();
            }
            return data;
        }
    }
}
