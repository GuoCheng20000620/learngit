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

public partial class _Default1 : System.Web.UI.Page
{
    CommDB mydb = new CommDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)            
            Label1.Text = mydb.RandomNum(4);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string mysql;
        int i;
        string _orginfo = "";
        if (TextBox3.Text.Trim() != Label1.Text.Trim()) //验证码输入错误
            Response.Write("<script>alert('你的验证码输入错误，请重输入!')</script>");
        else
        {
                      
                //mysql = "SELECT sname FROM student WHERE sno = '" + TextBox1.Text + "' AND spass = '" + TextBox2.Text + "'";
            //mysql = "SELECT sname FROM student WHERE sno = '" + TextBox1.Text + "' AND spass = '" + TextBox2.Text + "'";
            mysql = " select b.orgid+'&'+ltrim(rtrim(b.dwmch))+'&'+b.is_sc+'&'+b.zzhrq+'&'+isnull(a.jg,'N')+'&'+isnull(a.zpflg,'N')+'&'+isnull(cast(b.mode as varchar),'2')+'&'+b.nolimit+'&'+cast(b.accounttype as varchar) as accounttype from [dbo].[cb_User] a,[dbo].[cb_Org] b where a.[orgid]=b.[orgid] and  orgcode='" 
                 + TextBox1.Text + "' and pwd='" + TextBox2.Text + "'";
               i = mydb.Rownum(mysql, "student", ref _orginfo);
                if (i > 0)              //合法用户
                {
                    string[] _arr = _orginfo.Split(new char[]{'&'});
                    Session["uno"] = TextBox1.Text.Trim();      //保存用户名

                    //保存供应商orgid&dwmch&is_sc信息
                    Session["orgid"] = _arr[0]; 
                    Session["uname"] = _arr[1];	//单位名称                
                    Session["scflag"] = _arr[2]; //上传标记
                    Session["zzhrq"] = _arr[3];
                    Session["showprice"] = _arr[4];	 //是否显示销售价格
                    Session["zpflg"] = _arr[5];	 //是否显示赠品
                    Session["mode"] = _arr[6]; //数据显示模式 0:所有数据(可选择1,2两种方式之一显示);1:含总部销售及调拨;2.含总部销售不含调拨
                    Session["nolimit"] = _arr[7]; //是否不受设定的日期限制(到期仍可查询)
                    Session["accounttype"] = _arr[8];  // 1:供应商账号;0:采购员账号(可维护直连或网站流向品种)
                    Server.Transfer("~/masterform.aspx");
                }
                else    //非法用户
                    Response.Write("<script>alert('对不起，你输入的用户名或者密码错误，请查实!')</script>");           
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Label1.Text = mydb.RandomNum(4);
    }
}
