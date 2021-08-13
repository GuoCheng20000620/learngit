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

public partial class studentmenu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = "欢迎您:" + Session["uname"] + "(" + Session["uno"] + ")";
        string _acctype = Session["accounttype"].ToString();
        if (_acctype == "1") //供应商权限
        {
            this.TreeView1.Nodes[0].ChildNodes.RemoveAt(3);
        }
        else //采购员权限
        {
            this.TreeView1.Nodes[0].ChildNodes.RemoveAt(0);
            this.TreeView1.Nodes[0].ChildNodes.RemoveAt(0);
            this.TreeView1.Nodes[0].ChildNodes.RemoveAt(0);
        }
    }
}
