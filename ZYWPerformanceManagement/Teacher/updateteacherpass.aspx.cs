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

public partial class updateteacherpass : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        CommDB mydb = new CommDB();
        string mysql, sn = "";
        int i;
        mysql = "SELECT * FROM teacher WHERE tno='" + Session["uno"] + "' AND tpass='" + TextBox1.Text.Trim() + "'";
        i = mydb.Rownum(mysql, "teacher", ref sn);
        if (i == 0)
            Server.Transfer("~/dispinfo.aspx?info=原密码输入错误!");
        else
        {
            mysql = "UPDATE teacher SET tpass='" + TextBox2.Text.Trim() + "' WHERE tno='" + Session["uno"] + "'";
            mydb.ExecuteNonQuery(mysql);
            Server.Transfer("~/dispinfo.aspx?info=密码修改成功!");
        }
    }
}
