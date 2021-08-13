<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sale_Querys.aspx.cs" Inherits="LiuXiang_Sale_Querys" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script> 
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1
        {
            height: 24px;
        }
        .auto-style4
        {
            height: 24px;
            width: 175px;
        }
        .auto-style5
        {
            width: 175px;
        }
        .auto-style6
        {
            height: 24px;
            width: 93px;
        }
        .auto-style7
        {
            width: 93px;
        }
        .auto-style9 {
            width: 175px;
            height: 23px;
        }
        .auto-style10 {
            width: 93px;
            height: 23px;
        }
        .auto-style11 {
            height: 23px;
        }
        .auto-style12
        {
            height: 24px;
            text-align: center;
        }
        .auto-style15
        {
            height: 24px;
            width: 66px;
            text-align: center;
        }
        .auto-style16
        {
            width: 66px;
            height: 23px;
        }
        .auto-style17
        {
            width: 66px;
        }
    </style>
     <link rel="stylesheet" type="text/css" href="../Styles/BaseStyle.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <table style="width: 100%;">
            <tr>
                <td class="auto-style12" colspan="4">
                    <asp:Label ID="Label1" runat="server" Text="商品购进查询" ForeColor="#FF3300" style="font-size: x-large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style15">
                    <asp:Label ID="lblspbh" runat="server" Text="商品编号"></asp:Label>
                </td>
                <td class="auto-style4">
                    <asp:TextBox ID="txt_spbh1" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style6">
                    <asp:Label ID="lblspbh0" runat="server" Text="商品名称"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txt_spmc1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style16">起始日期</td>
                <td class="auto-style9">
                    <asp:TextBox ID="txt2" runat="server" OnTextChanged="txt_spbh2_TextChanged" onClick="WdatePicker()"></asp:TextBox>
                </td>
                <td class="auto-style10">截止日期</td>
                <td class="auto-style11">
                    <asp:TextBox ID="txt3" runat="server" onClick="WdatePicker({minDate:'#F{$dp.$D(\'txt2\')}',dateFmt:'yyyy-MM-dd',isShowClear:false});"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style17">&nbsp;</td>
                <td class="auto-style5">
                    &nbsp;</td>
                <td class="auto-style7">&nbsp;</td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询"   CssClass="ButtonCss"/>
                    <asp:Button ID="btnExport" runat="server" Text="导出" OnClick="btnExport_Click"  CssClass="ButtonCss"/>
                </td>
            </tr>
        </table>
        <br />

            <asp:GridView ID="GridView1" runat="server"  Width="1200px" AutoGenerateColumns="False"
        BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"
        CellPadding="3" GridLines="Horizontal" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20">

            <FooterStyle BackColor="Tan" />
            <Columns>
                <asp:BoundField DataField="rowid" HeaderText="行号" />
                <asp:BoundField DataField="danwbh" HeaderText="单位编号" />
                <asp:BoundField DataField="dwmch" HeaderText="单位名称" />
                <asp:BoundField DataField="djbh" HeaderText="单据编号" />
                <asp:BoundField DataField="rq" HeaderText="日期" />
                <asp:BoundField DataField="spbh" HeaderText="商品编号" />
                <asp:BoundField DataField="spmch" HeaderText="商品名称" />
                <asp:BoundField DataField="dw" HeaderText="单位" />
                <asp:BoundField DataField="shpgg" HeaderText="商品规格" />
                 <asp:BoundField DataField="pihao" HeaderText="批号" />
                 <asp:BoundField DataField="rkshl" HeaderText="入库数量" />
                 <asp:BoundField DataField="shengccj" HeaderText="生产厂家" />
                 <asp:BoundField DataField="zhy" HeaderText="摘要" />
            </Columns>
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="Blue" HorizontalAlign="Right" Font-Names="方正粗黑宋简体" Font-Size="Medium" Font-Underline="False" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
        <AlternatingRowStyle BackColor="#F7F7F7" />
        </asp:GridView>
        <br />
    
    </div>
    </form>
</body>
</html>
