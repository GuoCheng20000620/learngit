using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LiuXiang_Sale_Querys : System.Web.UI.Page
{
    CommDB mydb = new CommDB();
    DataSet myds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            int monthDay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); //当月天数
            DateTime d1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime d2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, monthDay);

            txt2.Text = d1.ToString("yyyy-MM-dd");
            txt3.Text = d2.ToString("yyyy-MM-dd");
        }
        //Session.Timeout = 5;
        //GridView1.PagerSettings.
    }

    private string GetSql()
    {
        string shi1 = txt2.Text;
        string shi2 = txt3.Text;
        string spbh =txt_spbh1.Text.Trim();
        string spmch =txt_spmc1.Text.Trim();
        string dwbh = Session["orgid"].ToString();
        string is_sc = Session["scflag"].ToString();
        string nolimit = Session["nolimit"].ToString(); //账号是否不受到期日限制

        DateTime today = DateTime.Now;
        DateTime zzhrq = DateTime.Parse(Session["zzhrq"].ToString());
        string sql = "";
        string fromsql = "";
        if (zzhrq < today && nolimit == "N")
        {
          
        }
        else
        {
            sql = string.Format(@"select distinct ROW_NUMBER() Over (ORDER BY rq,spbh,djbh) as rowid,
             danwbh,dwmch,djbh,rq,spbh,spmch,dw,shpgg,pihao,rkshl,shengccj,
			 case when (orgid='CBO00000037' or orgid='CBO00000225' or orgid='CBO00000177')  then CAST(rkdj as varchar(10)) 
              when orgid='CBO00000112' and CONVERT(NUMERIC(10,2),rkdj)<=1 then '赠品'
              when orgid='CBO00000112' and CONVERT(NUMERIC(10,2),rkdj)>1 then '本品'
             else zhy end as zhy
             from spcglx where rq between '{0}' and '{1}'      
             and orgid= '{2}' and hw<>'HWI00000087' 
             and (spbh like '%{3}%' and spmch like '%{4}%') order by spbh,rq", shi1, shi2, dwbh, spbh, spmch);
        }
      
        return sql;

    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string sql = GetSql();
        if (sql == "")
        {
            Response.Write("<script>alert('对不起，您的账号已停用,请与管理员联系')</script>");   

        }else
        {
          myds = mydb.ExecuteQuery(sql, "store");
   //     if (myds.Tables["store"].Rows.Count > 0)
          DataTable dt = myds.Tables["store"];
          dt.DefaultView.Sort = "rq,spbh,djbh";
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Session["CurPurch"] = myds.Tables["store"];
            
    }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string[] ss=new string[]{"行号","单位编号","单位名称","单据编号","日期","商品编号","商品名称","单位","商品规格","批号","入库数量","生产厂家","摘要"};
        if (Session["CurPurch"] != null)
        {
            DataTable DT = Session["CurPurch"] as DataTable;
            CommDB.DataTableToExcel(DT,ss,"Purch");
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        string mysql = this.GetSql();
        myds = mydb.ExecuteQuery(mysql, "score");
        DataTable dt = myds.Tables["score"];
        dt.DefaultView.Sort = "rq,spbh,djbh";
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void txt_spbh2_TextChanged(object sender, EventArgs e)
    {
     
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        //txt_spbh2.Text = null;
        //txt_spbh2.Text = Calendar1.SelectedDate.ToShortDateString();
        
    }
    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
        //txt_spbh3.Text = null;
        //txt_spbh3.Text = Calendar2.SelectedDate.ToShortDateString();
    }
}