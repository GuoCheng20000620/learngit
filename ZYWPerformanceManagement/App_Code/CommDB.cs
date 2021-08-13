using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// CommDB 的摘要说明
/// </summary>
public class CommDB
{

    static string _filename = "_"+DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss");
	public CommDB()   //默认构造函数
	{}
    //******************************************************************
    //返回SELECT语句执行后记录集中的行数
    //******************************************************************
    public int Rownum(string sql,string tname,ref string sname)
    {
        int i=0;
        string mystr = ConfigurationManager.AppSettings["myconnstring"];
        SqlConnection myconn = new SqlConnection();
        myconn.ConnectionString = mystr;
        myconn.Open();
        SqlCommand mycmd = new SqlCommand(sql, myconn);
        SqlDataReader myreader = mycmd.ExecuteReader();
        while (myreader.Read())　　//循环读取信息
        {
            sname = myreader[0].ToString();
            i++;
        }
        myconn.Close();
        return i;
        // 下载于www.51aspx.com
    }
    //******************************************************************
    //执行SQL语句，返回是否成功执行。SQL语句最好是如下：
    //UPDATE 表名 SET 字段名=value,字段名=value WHERE 字段名=value
    //DELETE FROM 表名 WHERE 字段名=value
    //INSERT INTO 表名 (字段名,字段名) values (value,value)
    //******************************************************************
    public Boolean ExecuteNonQuery(string sql)
    {
        string mystr = ConfigurationManager.AppSettings["myconnstring"];
        SqlConnection myconn = new SqlConnection();
        myconn.ConnectionString = mystr;
        myconn.Open();
        SqlCommand mycmd = new SqlCommand(sql,myconn);
        try
        {
            mycmd.ExecuteNonQuery();
            myconn.Close();
        }
        catch
        {
            myconn.Close();
            return false;
        }
        return true;
    }
    //*******************************************************************
    //执行SELECT语句，返回DataSet对象
    //*******************************************************************
    public DataSet ExecuteQuery(string sql,string tname)
    {
        string mystr = ConfigurationManager.AppSettings["myconnstring"];
        SqlConnection myconn = new SqlConnection();
        myconn.ConnectionString = mystr;
        myconn.Open();
        SqlDataAdapter myda = new SqlDataAdapter(sql,myconn);
        DataSet myds = new DataSet();
        myda.Fill(myds,tname);
        myconn.Close();
        return myds;
    }
    //*******************************************************************
    /// 实现随机验证码:返回生成的随机数
    //*******************************************************************
    public string RandomNum(int n)      //n为验证码的位数
    {
        //定义一个包括数字、大写英文字母和小写英文字母的字符串
        //string strchar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H," + 
        //    "I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z," +
        //    "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
        string strchar = "0,1,2,3,4,5,6,7,8,9";

        //将strchar字符串转化为数组
        //String.Split方法返回包含此实例中的子字符串的String数组。
        string[] arry = strchar.Split(',');
        string num = "";
        //记录上次随机数值，尽量避免产生几个一样的随机数           
        int temp = -1;
        //采用一个简单的算法以保证生成随机数的不同
        Random rand = new Random();
        for (int i = 1; i < n + 1; i++)
        {
            if (temp != -1)
            {
                //unchecked 关键字用于取消整型算术运算和转换的溢出检查。
                //DateTime.Ticks 属性获取表示此实例的日期和时间的刻度数。
                rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));
            }
            //Random.Next 方法返回一个小于所指定最大值的非负随机数。
           // int t = rand.Next(61);
            int t = rand.Next(9);
            if (temp != -1 && temp == t)
            {
                return RandomNum(n);
            }
            temp = t;
            num += arry[t];
        }
        return num;         //返回生成的随机数
    }

    // datatable导出为excel
    public static void DataTableToExcel(System.Data.DataTable dtData ,string[] columinfo,string prefix)
    {
       
        System.Web.UI.WebControls.GridView gvExport = null;
        // 当前对话 
        System.Web.HttpContext curContext = System.Web.HttpContext.Current;
        // IO用于导出并返回excel文件 
        System.IO.StringWriter strWriter = null;
        System.Web.UI.HtmlTextWriter htmlWriter = null;

        if (dtData != null)
        {
            // 设置编码和附件格式 
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.AddHeader("Content-Disposition", "attachment;   filename=" + System.Web.HttpUtility.UrlEncode(prefix+_filename, System.Text.Encoding.UTF8) + ".xls");
           // curContext.Response.ContentType = "application/msexcel *.xls *.xlsx";
            curContext.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            curContext.Response.Charset = "utf-8";

            // 导出excel文件 
            strWriter = new System.IO.StringWriter();
            htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);
            // 为了解决gvData中可能进行了分页的情况，需要重新定义一个无分页的GridView 
            gvExport = new System.Web.UI.WebControls.GridView();
            gvExport.AutoGenerateColumns = false;
            
            for (int i = 0; i < columinfo.Length; i++)
            {
                BoundField bc = new BoundField();
                  bc.DataField = dtData.Columns[i].ColumnName.ToString();
                bc.HeaderText = columinfo[i];
                gvExport.Columns.Add(bc);

            }
            gvExport.DataSource = dtData.DefaultView;
            gvExport.AllowPaging = false;                                   
            gvExport.DataBind();
            // 返回客户端 
            gvExport.RenderControl(htmlWriter);
            curContext.Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\" />" + strWriter.ToString());
            curContext.Response.End();
        }
    }

    /// <summary> 
    /// 直接输出Excel 
    /// </summary> 
    /// <param name="dtData"></param> 
    public static void ToExcel(System.Data.DataTable dtData,string prefix)
    {
        System.Web.UI.WebControls.DataGrid dgExport = null;
        // 当前对话 
        System.Web.HttpContext curContext = System.Web.HttpContext.Current;
        // IO用于导出并返回excel文件 
        System.IO.StringWriter strWriter = null;
        System.Web.UI.HtmlTextWriter htmlWriter = null;

        if (dtData != null)
        {
            // 设置编码和附件格式 
            curContext.Response.ContentType = "application/vnd.ms-excel";
            
            curContext.Response.AddHeader("Content-Disposition", "attachment;   filename=" + System.Web.HttpUtility.UrlEncode(prefix+_filename,System.Text.Encoding.UTF8) + ".xls");
            //curContext.Response.ContentType = "application/msexcel *.xls *.xlsx";
            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            curContext.Response.Charset = "";

            // 导出excel文件 
            strWriter = new System.IO.StringWriter();
            htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);

            // 为了解决dgData中可能进行了分页的情况，需要重新定义一个无分页的DataGrid 
            dgExport = new System.Web.UI.WebControls.DataGrid();
            dgExport.DataSource = dtData.DefaultView;
            dgExport.AllowPaging = false;
            dgExport.DataBind();

            // 返回客户端 
            dgExport.RenderControl(htmlWriter);
            curContext.Response.Write(strWriter.ToString());
            curContext.Response.End();            
        }
    }
}
