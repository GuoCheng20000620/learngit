using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddGoods : System.Web.UI.Page
{
    CommDB mydb = new CommDB();
    DataSet dbsupp = new DataSet();
    DataSet ds_goods = new DataSet(); //供应商己有货品数据
    DataSet ds_query = new DataSet(); //查询货品结果数据
    DataTable db_save = new DataTable(); //保存到zlkhgl_mx的数据
    SqlCommand sqlcmd;
    SqlDataAdapter sda_zlgoods;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetSupplier();
            IniDataTable();            
        }
        
    }


    private void IniDataTable()
    {
        string _sqlstr = "SELECT top 0 spbh,spmch,shpgg,dw,shpchd,oldspid from t_inca_spzl ";       
        ds_query = mydb.ExecuteQuery(_sqlstr, "goods");
        Session["goods"] = ds_query.Tables["goods"];             
    }

    private void GenerateSaveTable(string dwbh)
    {
        string sqlmx = "select  * from zlkhgl_mx where dwbh='"+dwbh+"'";
        //db_save = mydb.ExecuteQuery(sqlmx, "mx").Tables[0];
        sqlcmd = new SqlCommand(sqlmx,new SqlConnection(ConfigurationManager.AppSettings["myconnstring"]));
        sda_zlgoods = new SqlDataAdapter(sqlcmd);
        sda_zlgoods.Fill(db_save);
        Session["Save"] = db_save;
        Session["Adapter"] = sda_zlgoods;
        SqlCommandBuilder scb = new SqlCommandBuilder(sda_zlgoods);
    }
    private void GetSupplier()
    {
       // string spbh ="0000"; //txt_spbh.Text.Trim();
      //  string spmch = "111"; //txt_spmc.Text.Trim();
       // string dwbh = Session["orgid"].ToString();
        string is_sc = Session["scflag"].ToString();
        DateTime today = DateTime.Now;
        DateTime zzhrq = DateTime.Parse(Session["zzhrq"].ToString());
        string sqlsupp = "SELECT dwbh,dwmch,qishrq,zzhrq from zlkhgl";
        
        dbsupp = mydb.ExecuteQuery(sqlsupp, "supp");

        this.cbbSupplier.DataSource = dbsupp.Tables[0];
        cbbSupplier.ValueField= "dwbh";
        cbbSupplier.TextField = "dwmch";
        
        dbsupp = mydb.ExecuteQuery(sqlsupp, "supp");
        Session["supplist"] = dbsupp.Tables[0];
        cbbSupplier.DataSource = dbsupp.Tables[0];
        cbbSupplier.DataBind();

        cbbSupplier.SelectedIndex = 0;
        string _dwbh = dbsupp.Tables[0].Rows[0]["dwbh"].ToString();   
        DataRow[] _rows = dbsupp.Tables[0].Select("dwbh='" + _dwbh+"'");
        if (_rows.Length > 0)
        {
            deStart.Date =DateTime.Parse(_rows[0]["qishrq"].ToString());
            deEnd.Date =DateTime.Parse(_rows[0]["zzhrq"].ToString());
        }
        this.GetSupplyGoodsList(_dwbh);
        dgvAddList.Caption = "己注册货品列表<" + _rows[0]["dwmch"].ToString() + ">";
        GenerateSaveTable(_dwbh); //保存原始数据至datatable,修改后对比此表数据进行保存
        Session["dwbh"] = _dwbh;
    
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string _goods = tb_goods.Text;

        if (_goods == "")
        {
            Response.Write("<script>alert('货品ID不可为空!')</script>");
            return;
        }

        string[] _arrygoods = _goods.Split(new char[] { ','});
        string _goodlist = "";
        string _sqlstr = "SELECT spbh,spmch,shpgg,dw,shpchd,oldspid from t_inca_spzl where spbh in ";
        for (int i = 0; i <= _arrygoods.Length-1; i++)
        {
            _goodlist =_goodlist+"'"+ _arrygoods[i] + "',";
        }
        _goodlist = _goodlist.Remove(_goodlist.Length - 1, 1); //删除最后一个逗号
        _sqlstr = _sqlstr + "(" + _goodlist + ")";
        ds_query = mydb.ExecuteQuery(_sqlstr, "goods");

        GridView1.DataSource = ds_query.Tables["goods"];
        GridView1.DataBind();
        Session["goods"] = ds_query.Tables["goods"];

        //DataColumn[] _keys= ds_query.Tables["goods"].PrimaryKey;

        }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //GridView1.PageIndex = e.NewPageIndex;
        //string mysql = this.GetSql();
        //myds = mydb.ExecuteQuery(mysql, "score");
        //GridView1.DataSource = myds.Tables["score"];
        //GridView1.DataBind();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
         dgvAddList.DataSource = (Session["suppliergoods"] as DataSet).Tables[0];
        dgvAddList.DataBind();
        ASPxGridViewExporter1.WriteXlsxToResponse();

    }

    protected void lbtnexcel_Click(object sender, EventArgs e)
    {
      
    }
    protected void cbbSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = Session["supplist"] as DataTable;
        string _dwbh = cbbSupplier.Value.ToString();
        string _dwmc="";
        DataRow[] _rows =dt.Select("dwbh='" + _dwbh+"'");
        if (_rows.Length>0)
        {
          //  deStart.Value = _rows[0]["qishrq"].ToString();
          //  deEnd.Value   = _rows[0]["zzhrq"].ToString();
            deStart.Date = DateTime.Parse(_rows[0]["qishrq"].ToString());
            deEnd.Date = DateTime.Parse(_rows[0]["zzhrq"].ToString());
            _dwmc = _rows[0]["dwmch"].ToString();
        }
        GetSupplyGoodsList(_dwbh);
        dgvAddList.Caption = "己注册货品列表<" + _dwmc + ">";
        GenerateSaveTable(_dwbh); //保存原始数据至datatable,修改后对比此表数据进行保存
        Session["dwbh"] = _dwbh;
       
    }
    private void GetSupplyGoodsList(string dwbh)  //查找供应商己有货品并绑定
    {
        string _sqlstr = "SELECT spbh,spmch,shpgg,dw,shpchd,oldspid from t_inca_spzl where spbh in (select goodsid from zlkhgl_mx where dwbh='"+dwbh+"')";
        ds_goods = mydb.ExecuteQuery(_sqlstr, "suppliergoods");
        this.dgvAddList.DataSource = ds_goods.Tables["suppliergoods"];
        dgvAddList.DataBind();
        Session["suppliergoods"] = ds_goods;
    }

    protected void callback_addlist_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        
    }
    
    protected void btnAddtolist_Click(object sender, EventArgs e)
    {
        /*
        string _key = GridView1.KeyFieldName;
        string[,] _selectlist = this.GetAddItem();
        if (_selectlist.GetLength(0) == 0)
        {
            Response.Write("<script>alert('请至少选择一个货品ID!')</script>");
            return;
        }
        else
        {           
            //Response.Write("<script>alert('选择货品ID如下:"+_selectlist+"')</script>");

            DataSet _ds = Session["suppliergoods"] as DataSet;
            DataTable _dt = _ds.Tables[0];
           
            DataTable _db_goods = Session["goods"] as DataTable;
            for (int j = 0; j < _selectlist.GetLength(0); j++)
            {
                string _bh=_selectlist[j,0];

                DataRow[] _dr = _dt.Select("spbh='" + _bh + "'");
                if (_dr.Length == 0) //若不存在
                {
                    DataRow _drow = _dt.NewRow();
                    _drow["spbh"] = _selectlist[j, 0];
                    _drow["oldspid"] = _selectlist[j, 1];
                    _drow["spmch"] = _selectlist[j, 2];
                    _drow["dw"] = _selectlist[j, 3];
                    _drow["shpgg"] = _selectlist[j, 4];
                    _drow["shpchd"] = _selectlist[j, 5];                    
                    _ds.Tables[0].Rows.Add(_drow);                  
                }
                //移除左边列表中选中的数据行
                DataRow[] _removes = _db_goods.Select("spbh='" + _bh + "'");
                _db_goods.Rows.Remove(_removes[0]);
                

            }

            Session["suppliergoods"] = _ds;
            dgvAddList.DataSource = _ds.Tables[0];
            dgvAddList.DataBind();

            
            Session["goods"] = _db_goods;
            GridView1.DataSource = _db_goods;
            GridView1.DataBind();
     
            
        }*/
    }

    protected string[,] GetAddItem(string spbhlist)
    {
        string[] _bhlist = spbhlist.Split(new char[] { ',' });
        string _goodlist = "";
        string _sqlstr = "SELECT spbh,spmch,shpgg,dw,shpchd,oldspid from t_inca_spzl where spbh in ";
        for (int i = 0; i <= _bhlist.Length - 1; i++)
        {
            _goodlist = _goodlist + "'" + _bhlist[i] + "',";
        }
        _goodlist = _goodlist.Remove(_goodlist.Length - 1, 1); //删除最后一个逗号
        _sqlstr = _sqlstr + "(" + _goodlist + ")";
        DataTable db_list = mydb.ExecuteQuery(_sqlstr, "addlist").Tables[0]; 
        string[,] addinfo = new string[db_list.Rows.Count,6];
        for (int i = 0; i < db_list.Rows.Count; i++)
        {
            DataRow _dr = db_list.Rows[i];
            addinfo[i, 0] = _dr["spbh"].ToString();
            addinfo[i, 1] = _dr["oldspid"].ToString();
            addinfo[i, 2] = _dr["spmch"].ToString().ToString(); //名称
            addinfo[i, 3] = _dr["dw"].ToString().ToString(); //单位
            addinfo[i, 4] = _dr["shpgg"].ToString().ToString();  //规格 
            addinfo[i, 5] = _dr["shpchd"].ToString().ToString(); //产地        
        }
        return addinfo;
    
    }
    protected string[,] GetAddItem()
    {        
       // string[,] retvalues = new string[,] { };
        //获取选中的记录Id
        //spmch,shpgg,dw,shpchd
        string _key = GridView1.KeyFieldName;
        List<object> lSelectValues = GridView1.GetSelectedFieldValues(new string[]{"spbh","oldspid","spmch","dw","shpgg","shpchd" });
        //string[,] retvalues = new string[lSelectValues.Count,6];
        string[,] addinfo = new string[lSelectValues.Count,6];
        for (int i = 0; i < lSelectValues.Count; i++)
        {
            object[] ss = lSelectValues[i] as object[];
            string _spbh = ss[0].ToString();
            string _oldspid = ss[1].ToString();
            addinfo[i, 0] = _spbh; 
            addinfo[i, 1] = _oldspid;
            addinfo[i, 2] = ss[2].ToString(); //名称
            addinfo[i, 3] = ss[3].ToString(); //单位
            addinfo[i, 4] = ss[4].ToString();  //规格 
            addinfo[i, 5] = ss[5].ToString(); //产地
           // delId += "'"+spbh+ "',";
        }

      //  delId = delId.Substring(0, delId.LastIndexOf(','));
        return addinfo;
    }

    //获取右边列表中选中的需要删除的货品ID
    protected string[,] GetDelItem()
    {
        // string[,] retvalues = new string[,] { };
        //获取选中的记录Id
        //spmch,shpgg,dw,shpchd
        List<object> lSelectValues = this.dgvAddList.GetSelectedFieldValues(new string[] {"spbh","oldspid","spmch","dw","shpgg","shpchd"});        
        string[,] delinfo = new string[lSelectValues.Count, 6];
        for (int i = 0; i < lSelectValues.Count; i++)
        {
            object[] ss = lSelectValues[i] as object[];
            string _spbh = ss[0].ToString();
            string _oldspid = ss[1].ToString();
            delinfo[i, 0] = _spbh;  //新货品ID
            delinfo[i, 1] = _oldspid; //旧编码
            delinfo[i, 2] = ss[2].ToString(); //名称
            delinfo[i, 3] = ss[3].ToString(); //单位
            delinfo[i, 4] = ss[4].ToString();  //规格 
            delinfo[i, 5] = ss[5].ToString(); //产地            
        }        
        return delinfo;
    }

    //保存右边列表中的货品数据
    protected void btnSaveGoods_Click(object sender, EventArgs e)
    {
        DataTable _dbgoods =(Session["suppliergoods"] as DataSet).Tables[0];  //右边列表中数据
        DataTable _dborign = Session["Save"] as DataTable; //原有数据

        //先删除_dborign中在右边列表中不存在的货品
        for (int i = 0; i < _dborign.Rows.Count; i++)
        {             
            DataRow _drorign = _dborign.Rows[i];
            string _bh = _drorign["goodsid"].ToString();
            DataRow[] _rows = _dbgoods.Select("spbh='" + _bh + "'");
            if (_rows.Length == 0)
                _drorign.Delete();
        }

        //把右边列表中的不存在于_dborign中的货品新增至_dborign中
        for (int i = 0; i < _dbgoods.Rows.Count; i++)
        {
            DataRow _dr = _dbgoods.Rows[i];
            string _bh = _dr["spbh"].ToString();
            string _spid =_dr["oldspid"].ToString();
            DataRow[] _rows = _dborign.Select("goodsid='" + _bh + "'");
            if (_rows.Length == 0) //没找到新增
            {
                DataRow _newrow = _dborign.NewRow();
                _newrow["dwbh"] = Session["dwbh"].ToString();
                _newrow["goodsid"] = _bh;
                _newrow["spid"] = _spid;
                _dborign.Rows.Add(_newrow);
            }
        }
        try
        {
            //将_dborign中数据批量提交到数据库
            SqlDataAdapter _adapter = Session["Adapter"] as SqlDataAdapter;

            _adapter.Update(_dborign.GetChanges());
            _dborign.AcceptChanges();
            Response.Write("<script>alert('更新货品数据成功!')</script>");

        }
        catch
        {
            Response.Write("<script>alert('更新货品数据失败!')</script>");
        }


    }
    protected void btnDelexists_Click(object sender, EventArgs e)
    {
        string[,] _selectlist = this.GetDelItem();
        if (_selectlist.GetLength(0) == 0)
        {
            Response.Write("<script>alert('请至少选择一个货品ID!')</script>");
            return;
        }
        else
        {
            DataSet _ds = Session["suppliergoods"] as DataSet;
            DataTable _dt = _ds.Tables[0];
            DataTable _dbgoods = Session["goods"] as DataTable;
            for (int j = 0; j < _selectlist.GetLength(0); j++)
            {
                string _bh = _selectlist[j, 0];
                DataRow[] _dr = _dbgoods.Select("spbh='" + _bh + "'");
                if (_dr.Length == 0) //若不存在
                {
                    DataRow _drow = _dbgoods.NewRow();
                    _drow["spbh"] = _selectlist[j, 0];
                    _drow["oldspid"] = _selectlist[j, 1];
                    _drow["spmch"] = _selectlist[j, 2];
                    _drow["dw"] = _selectlist[j, 3];
                    _drow["shpgg"] = _selectlist[j, 4];
                    _drow["shpchd"] = _selectlist[j, 5];
                    _dbgoods.Rows.Add(_drow);

                }
                //移除右边列表中选中的数据行
                DataRow[] _removes = _dt.Select("spbh='" + _bh + "'");
                if (_removes.Length>0)                
                  _dt.Rows.Remove(_removes[0]);
            }
            //重新绑定Gridview
            dgvAddList.DataSource = _dt; dgvAddList.DataBind();
            GridView1.DataSource = _dbgoods; GridView1.DataBind();
            Session["suppliergoods"] = _ds; Session["goods"] = _dbgoods;


        }

    }
   
   protected void GridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
   {
     
       string _paralist = e.Parameters;

       string[] _list = _paralist.Split(new char[] { ':'});

       if (_list[0] == "add") 
       {
           string _bhlst = _list[1];
            // List<object> lSelectValues = this.dgvAddList.GetSelectedFieldValues(new string[] { "spbh", "oldspid", "spmch", "dw", "shpgg", "shpchd" });  
          // List<object> lSelectValues = this.GridView1.GetSelectedFieldValues(new string[] { "spbh", "oldspid", "spmch", "dw", "shpgg", "shpchd" });  

           string[,] _selectlist = this.GetAddItem(_bhlst);
           if (_selectlist.GetLength(0) == 0)
           {
               Response.Write("<script>alert('请至少选择一个货品ID!')</script>");
               return;
           }
           else
           {

               DataSet _ds = Session["suppliergoods"] as DataSet;
               DataTable _dt = _ds.Tables[0];

               DataTable _db_goods = Session["goods"] as DataTable;
               for (int j = 0; j < _selectlist.GetLength(0); j++)
               {
                   string _bh = _selectlist[j, 0];
                   DataRow[] _dr = _dt.Select("spbh='" + _bh + "'");
                   if (_dr.Length == 0) //不存在则新增
                   {
                       DataRow _drow = _dt.NewRow();
                       _drow["spbh"] = _selectlist[j, 0];
                       _drow["oldspid"] = _selectlist[j, 1];
                       _drow["spmch"] = _selectlist[j, 2];
                       _drow["dw"] = _selectlist[j, 3];
                       _drow["shpgg"] = _selectlist[j, 4];
                       _drow["shpchd"] = _selectlist[j, 5];
                       _dt.Rows.Add(_drow);
                   }
                   //移除左边列表中选中的数据行
                   DataRow[] _removes = _db_goods.Select("spbh='" + _bh + "'");    
                   if (_removes.Length>0)
                     _db_goods.Rows.Remove(_removes[0]);
               }
              // GridView1.UpdateEdit();
               Session["suppliergoods"] = _ds;
               // dgvAddList.DataSource = _ds.Tables["suppliergoods"]; //直接在此绑定不起作用
               // dgvAddList.DataBind();

               Session["goods"] = _db_goods;

               GridView1.DataSource = null;
               GridView1.DataSource = _db_goods;
               GridView1.DataBind();

           }
       }
   }
   
   protected void dgvAddList_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
   {
       if (e.Parameters == "refrush")
       {
           DataSet _ds = Session["suppliergoods"] as DataSet;
           dgvAddList.DataSource = _ds.Tables["suppliergoods"];
           dgvAddList.DataBind();
       }

   }
   
   protected void btnSave_Click(object sender, EventArgs e)
   {
       string _dwbh = Session["dwbh"].ToString();
       string _sqlstr = "update zlkhgl set qishrq='" + deStart.Date.ToString("yyyy-MM-dd") + "',zzhrq='" + deEnd.Date.ToString("yyyy-MM-dd") + "' where dwbh='" + _dwbh + "'";
       try
       {
           bool _succ = mydb.ExecuteNonQuery(_sqlstr);
           Response.Write("<script>alert('更新日期成功!')</script>");
             
       }
       catch(Exception ex)
       {
           Response.Write("<script>alert('更新日期失败!')</script>");                  
       }
       return;
       
   }
   protected void btnAddtolist_Click1(object sender, EventArgs e)
   {

   }
}