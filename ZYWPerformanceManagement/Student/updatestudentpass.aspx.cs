using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

public partial class updatestudentpass : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 下载于www.51aspx.com
    }
    CommDB mydb = new CommDB();
    DataSet myds = new DataSet();

    protected void Button1_Click(object sender, EventArgs e)
    {

        string sql = "";
        string _orginfo = "";
        sql = string.Format(@"select count(1) from [dbo].[cb_User] a , [dbo].[cb_Org] b 
        where a.[orgid]=b.[orgid] and  orgcode='{0}' and pwd='{1}'", Session["uno"], TextBox1.Text.Trim());
      
        int SD = mydb.Rownum(sql, "student", ref _orginfo);
        if (SD > 0)
        {
            if (TextBox2.Text.Trim() == TextBox3.Text.Trim())
            {
                sql = string.Format(@"UPDATE [dbo].[cb_User]  SET pwd='{0}' FROM [dbo].[cb_Org] 
            where [cb_User].orgid=[cb_Org].orgid and   [cb_User].orgid='{1}'", TextBox2.Text.Trim(), Session["orgid"].ToString());
                mydb.ExecuteNonQuery(sql);
                Server.Transfer("~/dispinfo.aspx?info=密码修改成功!");
               
            }
            else
            {
                Response.Write("<script>alert('两次输入密码不一致，请查实!')</script>");
            }
        }
        else
        {
            Response.Write("<script>alert('原密码输入有误，请查实!')</script>");
        }


        //CommDB mydb = new CommDB();
        //string mysql,sn="";
        //int i;
        //mysql = "SELECT * FROM student WHERE sno='" + Session["uno"] + "' AND spass='" + TextBox1.Text.Trim() + "'";
        //i = mydb.Rownum(mysql,"student",ref sn);
        //if (i == 0)
        //    Server.Transfer("~/dispinfo.aspx?info=原密码输入错误!");
        //else
        //{
        //    mysql = "UPDATE student SET spass='" + TextBox2.Text.Trim() + "' WHERE sno='" + Session["uno"] + "'";
        //    mydb.ExecuteNonQuery(mysql);
        //    Server.Transfer("~/dispinfo.aspx?info=密码修改成功!");
        //}
    }
}
