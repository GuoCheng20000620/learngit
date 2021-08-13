<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Store_Query.aspx.cs" Inherits="LiuXiang_Store_Query" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        .auto-style8
        {
            height: 24px;
            text-align: center;
        }
        .auto-style9
        {
            width: 101px;
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
                <td class="auto-style8" colspan="4">
                    <asp:Label ID="Label1" runat="server" Text="商品库存查询" ForeColor="#FF3300" style="font-size: x-large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style8">
                    <asp:Label ID="lblspbh" runat="server" Text="商品编号"></asp:Label>
                </td>
                <td class="auto-style4">
                    <asp:TextBox ID="txt_spbh" runat="server" style="margin-left: 0px" CssClass="InputCss" AutoPostBack="True"></asp:TextBox>
                </td>
                <td class="auto-style6">
                    <asp:Label ID="lblspbh0" runat="server" Text="商品名称"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txt_spmc" runat="server" Width="429px" CssClass="InputCss" AutoPostBack="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style7">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style7">&nbsp;</td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询"   CssClass="ButtonCss"/>
                    <asp:Button ID="btnExport" runat="server" Text="导出" OnClick="btnExport_Click"  CssClass="ButtonCss"/>
                </td>
            </tr>
        </table>
        <br />

            <asp:GridView ID="GridView1" runat="server"  Width="1192px" AutoGenerateColumns="False"
        BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"
        CellPadding="3" GridLines="Horizontal" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20">

            <FooterStyle BackColor="Tan" />
            <Columns>
                <asp:BoundField DataField="rowid" HeaderText="行号" />
                <asp:BoundField DataField="spid" HeaderText="药品编码" />
                <asp:BoundField DataField="spmch" HeaderText="名称" />
                <asp:BoundField DataField="shpgg" HeaderText="规格" />
                <asp:BoundField DataField="dw" HeaderText="单位" />
    <asp:BoundField DataField="jlgg" HeaderText="件装数" />
                <asp:BoundField DataField="shpchd" HeaderText="产地" />

                <asp:BoundField DataField="pihao" HeaderText="批号" />
                <asp:BoundField DataField="sxrq" HeaderText="效期至" />
                <asp:BoundField DataField="shl" HeaderText="数量" />
                 <asp:BoundField DataField="beizhu" HeaderText="备注" />
                <asp:BoundField DataField="memo" HeaderText="备注二" />
            </Columns>
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="Blue" HorizontalAlign="Right" Font-Size="Medium" Font-Bold="False" Font-Names="方正粗黑宋简体" Font-Underline="False" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
        <AlternatingRowStyle BackColor="#F7F7F7" />
        </asp:GridView>
        <br />
    
    </div>
    </form>
</body>
</html>
