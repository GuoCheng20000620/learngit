using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LiuXiang_Store_Query : System.Web.UI.Page
{
    CommDB mydb = new CommDB();
    DataSet myds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session.Timeout = 5;
    }
    private string GetSql()
    {
        string spbh = txt_spbh.Text.Trim();
        string spmch = txt_spmc.Text.Trim();
        string dwbh = Session["orgid"].ToString();
        string is_sc = Session["scflag"].ToString();
        string nolimit = Session["nolimit"].ToString(); //账号是否不受到期日限制

        DateTime today = DateTime.Now;
        DateTime zzhrq = DateTime.Parse(Session["zzhrq"].ToString());
        string sql = "";
        string sqlsub = "";

        if (zzhrq < today && nolimit == "N")
        {

        }
        else
        {

            if (is_sc == "否" && dwbh != "CBO00000016")
            {
                sql = " select  ROW_NUMBER() Over (ORDER BY a.spid,a.pihao) as rowid,rtrim(cast(a.spid as char(10))) spid,a.spmch,a.shpgg,a.dw,a.jlgg,a.shpchd,"
                  + " sum(a.shl) as shl,a.pihao,a.sxrq,(case when a.beidian='天士力民生' then '重药民生' else a.beidian end) as beizhu,(case when a.dj=0.01 then '赠品' else '本品' end) as memo "
                    + " from v_inca_spkclx a(nolock),xjsp_dwgx c (nolock) where a.spid=c.goodsid  and c.orgid = '" + dwbh + "'  ";
                if (today > zzhrq)
                { sql += " and 1=0 "; }
                sql += " and a.spid like  '" + spbh + "%' and ( a.spmch like '%" + spmch + "%') "
                  + " group by a.spid,a.spmch,a.shpgg,a.dw,a.jlgg,a.shpchd,a.pihao,a.sxrq,a.beidian,a.dj having sum(a.shl)<>0  ";
            }
            if (is_sc == "否" && dwbh == "CBO00000016")
            {
                sqlsub = " select  ROW_NUMBER() Over (ORDER BY a.spid,a.pihao) as rowid,rtrim(cast(a.spid as char(10))) spid,a.spmch,a.shpgg,a.dw,a.jlgg,a.shpchd,"
                    + "a.zpflag,sum(a.shl) as shl,a.pihao,a.sxrq ,(case when a.beidian='天士力民生' then '重药民生' else a.beidian end) as beizhu,(case when a.dj=0.01 then '赠品' else '本品' end) as memo"
                    + " from v_inca_spkclx a(nolock),xjsp_dwgx c (nolock) where a.spid=c.goodsid  and c.orgid = '" + dwbh + "'  ";
                if (today > zzhrq)
                { sqlsub += " and 1=0 "; }
                sqlsub += " and a.spid like  '" + spbh + "%' and ( a.spmch like '%" + spmch + "%') "
                  + " group by a.spid,a.spmch,a.shpgg,a.dw,a.jlgg,a.shpchd,a.pihao,a.sxrq,a.zpflag,a.beidian,a.dj having sum(a.shl)<>0  ";
                sql = "select spid,spmch,shpgg,dw,jlgg,shpchd,shl,pihao,sxrq,beizhu,memo from ("
               + sqlsub + ") aa";

            }
            if (is_sc == "是")
            {
                sql = " select  ROW_NUMBER() Over (ORDER BY a.spid,a.pihao) as rowid,rtrim(cast(a.spid as char(10))) spid,a.spmch,a.shpgg,a.dw,a.jlgg,a.shpchd,sum(a.shl) as shl,a.pihao,a.sxrq,(case when a.beidian='天士力民生' then '重药民生' else a.beidian end) as beizhu,"
                    + " (case when a.dj=0.01 then '赠品' else '本品' end) as memo from v_inca_spkclx_all a(nolock),xjsp_dwgx c (nolock) where a.spid=c.goodsid  and c.orgid = '" + dwbh + "'  ";
                if (today > zzhrq)
                { sql += " and 1=0 "; }
                sql += " and a.spid like  '" + spbh + "%' and ( a.spmch like '%" + spmch + "%') "
                    + " group by a.spid,a.spmch,a.shpgg,a.dw,a.jlgg,a.shpchd,a.pihao,a.sxrq,a.beidian,a.dj having sum(a.shl)<>0  ";
            }
        }
        return sql;
    
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string sql = GetSql();
        if (sql == "")
        {
            Response.Write("<script>alert('对不起，您的账号已停用,请与管理员联系')</script>");

        }
        else
        {
            myds = mydb.ExecuteQuery(sql, "store");
            //  if (myds.Tables["store"].Rows.Count > 0)
            DataTable dt = myds.Tables["store"];
            dt.DefaultView.Sort = "spid,pihao";
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Session["CurStore"] = myds.Tables["store"];

        }
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        string mysql = this.GetSql();
        myds = mydb.ExecuteQuery(mysql, "score");
        DataTable dt = myds.Tables["store"];
        dt.DefaultView.Sort = "spid,pihao";
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        /*
        GridView1.BottomPagerRow.Visible = false;//导出到Excel表后，隐藏分页部分

        GridView1.Columns[9].Visible = false;//隐藏“编辑”列
        GridView1.Columns[10].Visible = false;//隐藏“删除”列

        GridView1.AllowPaging = false;//取消分页，便于导出所有数据，不然只能导出当前页面的几条数据

        GridView1.DataSource = ds;//取消分页后重新绑定数据集,ds为数据集dataset
        GridView1.DataBind();


        DateTime dt = DateTime.Now;//给导出后的Excel表命名，结合表的用途以及系统时间来命名
        string filename = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();
         * */

        /*如导出的表中有某些列为编号、身份证号之类的纯数字字符串，如不进行处理，则导出的数据会默认为数字，例如原字符串"0010"则会变为数字10，字符串"1245787888"则会变为科学计数法1.236+E9，这样便达不到我们想要的结果，所以需要在导出前对相应列添加格式化的数据类型，以下为格式化为字符串型*/

        /*
        foreach (GridViewRow dg in this.GridView1.Rows)
        {
            dg.Cells[0].Attributes.Add("style", "vnd.ms-excel.numberformat: @;");
            dg.Cells[5].Attributes.Add("style", "vnd.ms-excel.numberformat: @;");
            dg.Cells[6].Attributes.Add("style", "vnd.ms-excel.numberformat: @;");
            dg.Cells[8].Attributes.Add("style", "vnd.ms-excel.numberformat: @;");
        }

        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("学生表" + filename, System.Text.Encoding.UTF8) + ".xls");//导出文件命名
        Response.ContentEncoding = System.Text.Encoding.UTF7;//如果设置为"GB2312"则中文字符可能会乱码
        Response.ContentType = "applicationshlnd.xls";
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        Panel1.RenderControl(oHtmlTextWriter);//Add the Panel into the output Stream.
        Response.Write(oStringWriter.ToString());//Output the stream.
        Response.Flush();
        Response.End(); */
        string[] ss = new string[] { "行号","药品编码","名称","规格","单位","件装数","产地","数量","批号","有效期","备注","备注二"};
        if (Session["CurStore"] != null)
        {
            DataTable DT = Session["CurStore"] as DataTable;
            CommDB.DataTableToExcel(DT, ss,"Store");
        }

    }

    protected void lbtnexcel_Click(object sender, EventArgs e)
    {
      
    }
}