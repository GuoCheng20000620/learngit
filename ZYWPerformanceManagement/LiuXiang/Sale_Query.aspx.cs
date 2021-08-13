using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LiuXiang_Sale_Query : System.Web.UI.Page
{
    CommDB mydb = new CommDB();
    DataSet myds = new DataSet();
    string mode = "2";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.GridView1.Columns[13].Visible = Session["showprice"].ToString() == "Y" ? true : false;
            int monthDay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            DateTime d1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime d2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, monthDay);
          

            txt_spbh2.Text = d1.ToString("yyyy-MM-dd");

            txt_spbh3.Text = d2.ToString("yyyy-MM-dd");
            
            //if (mode == "0")

            mode = Session["mode"].ToString();
        }
        //Session.Timeout = 5;
        mode = Session["mode"].ToString();
        ckbMode.Visible = mode == "0";


        //txt_spbh2.Text = DateTime.Now.ToString().Substring(0, 7) + "/1";
        //txt_spbh3.Text = DateTime.Now.ToString().Substring(0, 9); //获取当前日期
    }

    private string GetSqlResult()
    {
        string sql = "";
        string shi1 = txt_spbh2.Text;
        string shi2 = txt_spbh3.Text;
        string spbh = txt_spbh1.Text.Trim();
        string spmch = txt_spmc1.Text.Trim();
        string dwbh = Session["orgid"].ToString();
        string nolimit = Session["nolimit"].ToString();
        
        DateTime today = DateTime.Now;
        DateTime zzhrq = DateTime.Parse(Session["zzhrq"].ToString());
        string fromsql = "";
        if (zzhrq < today&&nolimit=="N")
        {

        }
        else
        {
            if (dwbh == "CBO00000241") //阿斯利康
            {
                //   aslk_sql = " and quyufl<>'岳阳瑞致' ";
                fromsql = "  spxslx_aslk ";
            }
            else
            {
                fromsql = " v_inca_splx";
            }
            sql = "";
            if (mode == "0")
            {
                //  when (d.orgid='CBO000000037' or d.orgid='CBO00000225' or d.orgid='CBO00000177' or d.orgid='CBO00000176') then CAST(a.hshj as varchar(10))
                sql = string.Format(@" select ROW_NUMBER() Over (ORDER BY a.rq,a.spbh,a.djbh)  as rowid,a.djbh,a.rq,a.spbh ,a.spmch,a.dw,a.shpgg, a.pihao,a.shengccj,a.shl ,a.dwmch,a.beidianbs,
                case when a.beidianbs=7 then '重药民生' when a.beidianbs=8 then '长沙' when a.beidianbs=9 then '浏阳' when a.beidianbs=16 then '岳阳' end  as quyufl,a.danwbh,         
                case when (d.orgid='CBO00000112' or d.orgid='CBO00000016') and CONVERT(NUMERIC(10,2),a.hshj)<=1 then '赠品' 
                when (d.orgid='CBO00000112' or d.orgid='CBO00000016') and CONVERT(NUMERIC(10,2),a.hshj)>1 then '本品'           
                when d.orgid<>'CBO00000016' and left(a.ywbs,3)='XSA' then '销售出库' 
                when d.orgid<>'CBO00000016' and left(a.ywbs,3)='XSC' then '销售退回' 		   
                else '' END as zhy,convert(varchar(20),convert(decimal(10,2),a.hshj)) as hshj
                from v_inca_splx a ,xjsp_dwgx d(nolock)  where  a.spbh=d.goodsid and a.ywbs like 'XS[A,C]%' 
                and a.djbh<>'5463237'
                and a.rq between '{0}' and '{1}'      
                and d.orgid= '{2}' 
		        and (a.spbh like '%{3}%' and ( a.spmch like '%{4}%'))", shi1, shi2, dwbh, spbh, spmch);
		       // order by a.rq,a.spbh  ", shi1, shi2, dwbh, spbh, spmch);

            }
            else if (mode == "2") // 总部流向+分公司终端流向(不含总部到分公司调拨,从spxslx中取)
            {
//                if (dwbh == "CBO00000240")
//                {
//                    sql = string.Format(@"  select djbh,rq,spbh,spmch,dw,shpgg,pihao,shengccj,shl,dwmch,quyufl,zhy,convert(decimal(10,2),dj) as hshj from spxslx
//                     where orgid='CBO00000240' and rq between '{0}' and '{1}' 
//                     and spid<>'74434'
//                     and (spbh like '%{2}%' and ( spmch like '%{3}%'))
//                     union select djbh,rq,spbh,spmch,dw,shpgg,shengccj,pihao,shl,dwmch,quyufl,zhy,convert(decimal(10,2),dj) as hshj from spxslx where spid='74434'
//                     and rq between '{4}' and '2020-08-31'
//                     and (spbh like '%{5}%' and ( spmch like '%{6}%')) ", shi1, shi2, spbh, spmch, shi1, spbh, spmch);
//                }
//                else
//                {
                sql = string.Format(@" select ROW_NUMBER() Over (ORDER BY a.rq,a.spbh,a.djbh) as rowid,a.djbh,a.rq,a.spbh ,a.spmch,a.dw,a.shpgg, a.pihao,a.shengccj,a.shl ,a.dwmch,a.beidianbs,
                case when a.beidianbs=7 then '重药民生' when a.beidianbs=8 then '长沙' when a.beidianbs=9 then '浏阳' when a.beidianbs=16 then '岳阳' end as quyufl,a.danwbh,         
                case when (d.orgid='CBO00000112' or d.orgid='CBO00000016') and CONVERT(NUMERIC(10,2),a.hshj)<=1 then '赠品' 
                when (d.orgid='CBO00000112' or d.orgid='CBO00000016') and CONVERT(NUMERIC(10,2),a.hshj)>1 then '本品'           
                when d.orgid<>'CBO00000016' and left(a.ywbs,3)='XSA' then '销售出库' 
                when d.orgid<>'CBO00000016' and left(a.ywbs,3)='XSC' then '销售退回' 		   
                else '' END as zhy,convert(decimal(10,2),a.hshj) as hshj
                from {0} a ,xjsp_dwgx d(nolock)  where  a.spbh=d.goodsid and a.ywbs like 'XS[A,C]%' 
                and a.djbh<>'5463237'and a.danwbh not in ('32798','40494','44499','42347','42348')
                and a.rq between '{1}' and '{2}'      
                and d.orgid= '{3}' 
		        and (a.spbh like '%{4}%' and ( a.spmch like '%{5}%'))", fromsql, shi1, shi2, dwbh, spbh, spmch);
               
            }
            else if (mode == "1") //总部流向+总部调拨流向(不含分公司终端流向从v_inca_splx中取)
            {
//                sql = string.Format(@"select djbh,rq,spbh,spmch,dw,shpgg,pihao,shengccj,shl,danwbh,dwmch,
//                    case when beidianbs=7 then '重药民生' when beidianbs=8 then '长沙' when beidianbs=9 then '浏阳' end as quyufl,
//                     zhy,beidianbs,convert(decimal(10,2),dj) as hshj
//                     from {0} a,xjsp_dwgx d(nolock)  where  a.spbh=d.goodsid and a.rq between '{1}' and '{2}' and a.djbh<>'5463237'            
//                     and d.orgid= '{3}' and hw<>'HWI00000087' 
//                     and (spbh like '%{4}%' and ( spmch like '%{5}%'))          
//                     order by spbh,rq", fromsql, shi1, shi2, dwbh, spbh,spmch);  
                sql = string.Format(@" select ROW_NUMBER() Over (ORDER BY a.rq,a.spbh,a.djbh) as rowid,a.djbh,a.rq,a.spbh ,a.spmch,a.dw,a.shpgg, a.pihao,a.shengccj,a.shl ,a.dwmch,a.beidianbs,a.danwbh,
                    '重药民生' as quyufl,convert(decimal(10,2),a.hshj) as hshj,
                case when (d.orgid='CBO00000112' or d.orgid='CBO00000016') and CONVERT(NUMERIC(10,2),a.hshj)<=1 then '赠品' 
                when (d.orgid='CBO00000112' or d.orgid='CBO00000016') and CONVERT(NUMERIC(10,2),a.hshj)>1 then '本品'           
                when d.orgid<>'CBO00000016' and left(a.ywbs,3)='XSA' then '销售出库' 
                when d.orgid<>'CBO00000016' and left(a.ywbs,3)='XSC' then '销售退回' 		   
                else '' END as zhy
                from {0} a ,xjsp_dwgx d(nolock)  where  a.spbh=d.goodsid and a.beidianbs='7' and a.ywbs like 'XS[A,C]%' 
                and a.djbh<>'5463237' and a.rq between '{1}' and '{2}' and d.orgid= '{3}'
		        and (a.spbh like '%{4}%' and ( a.spmch like '%{5}%'))", fromsql, shi1, shi2, dwbh, spbh, spmch);              

            }
        }

        return sql;
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        //string sql = GetSql();
        string sql = this.GetSqlResult();
       
        if (sql == "")
        {
            Response.Write("<script>alert('对不起，您的账号已停用,请与管理员联系')</script>");
        }
        else
        {
            myds = mydb.ExecuteQuery(sql, "store");
         //   if (myds.Tables["store"].Rows.Count > 0)
            DataTable dtNew = new DataTable();
            DataRow[] drows = new DataRow[] { };
            DataTable dt = myds.Tables["store"];
            if (dt.Rows.Count == 0)
                return;

            Session["CurDs"] = dt;
           
            if (mode == "0" ) //mode=0 默认状态取不包含调拨到分公司的数据
              drows = dt.Select("danwbh not in ('32798','40494','44499','42347','42348')");
            else             
              drows = dt.Select("1=1");
             
                
            if (null != drows)            
                dtNew = drows.CopyToDataTable();

            ckbMode.Checked = false;
             dtNew.DefaultView.Sort = "rq,spbh,djbh";
             GridView1.DataSource = dtNew;
             GridView1.DataBind();
             Session["CurSale"] = dtNew;
            
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {   
        if (Session["CurSale"] != null)
        {
            DataTable DT = Session["CurSale"] as DataTable;

            //导出时移除beidianbs列
            if (DT.Columns.Contains("beidianbs"))
               DT.Columns.Remove("beidianbs");//
            if (DT.Columns.Contains("danwbh"))
             DT.Columns.Remove("danwbh");

            if (Session["showprice"].ToString() == "N")
            {
                DT.Columns.Remove("hshj");
                string[] ss = new string[] { "行号","单据编号", "日期", "商品编号", "名称", "单位", "商品规格", "批号", "生产厂家", "出库数量","客户名称","区域分类", "摘要" };
                CommDB.DataTableToExcel(DT, ss,"Sale");
            }
            else
            {
                string[] s1 = new string[] { "行号","单据编号", "日期", "商品编号", "名称", "单位", "商品规格", "批号", "生产厂家", "出库数量","客户名称", "区域分类", "摘要", "单价" };
                CommDB.DataTableToExcel(DT, s1,"Sale");
            }

        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
       // string mysql = this.GetSql();
        //myds = mydb.ExecuteQuery(mysql, "score");
        DataTable dt = Session["CurSale"] as DataTable;
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
    protected void ckbMode_CheckedChanged(object sender, EventArgs e)
    {
        if (Session["CurDs"] == null)
            return;
        DataTable dtNew = new DataTable();
        DataTable dt = Session["CurDs"] as DataTable;
        DataRow[] drows = new DataRow[] { };

        if (ckbMode.Checked)
        {            
             drows = dt.Select("beidianbs = '7'");
            if (null != drows)
            {
               dtNew = drows.CopyToDataTable();
             //  DataRow[] _rows = dtNew.Select("djbh='6778947'");
            }
        }
        else
        {
            //不勾选状态:过滤分公司销售流向
           drows = dt.Select("danwbh not in ('32798','40494','44499','42347','42348')");
            if (null != drows)
            {
                dtNew = drows.CopyToDataTable();
            }
           
        }
        
        GridView1.DataSource = dtNew;
        dtNew.DefaultView.Sort = "rq,spbh";
        GridView1.DataBind();
        Session["CurSale"] = dtNew;


    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //列宽自适应设置
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Text = "<nobr> " + e.Row.Cells[i].Text + " </nobr>";
            }
        }

    }
}