<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddGoods_.aspx.cs" Inherits="AddGoods_ZL11" %>

<%@ Register assembly="DevExpress.Web.v12.1, Version=12.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSplitter" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v12.1, Version=12.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxDataView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v12.1, Version=12.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v12.1.Export, Version=12.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style27 {
            height: 24px;
            text-align: center;
            width: 14%;
            font-size: x-small;
        }
        .auto-style37 {
            height: 47px;
        }
        .auto-style41 {
            width: 10%;
            font-size: x-small;
            text-align: center;
        }
        .auto-style44 {
            width: 10%;
            font-size: x-small;
            text-align: center;
        }
        .auto-style45 {
            width: 13%;
        }
        .ButtonCss {}
        .auto-style50 {
            width: 213px;
        }
        .auto-style52 {
            font-size: x-small;
        }
        .auto-style53 {
            width: 50%;
        }
        .auto-style54 {
            width: 46px;
        }
        </style>

    <!--
     <link rel="stylesheet" type="text/css" href="../Styles/BaseStyle.css" />

    
     <script src="VwdCmsSplitterBar.js" type="text/javascript"></script>

        
        -->
    <script type="text/javascript">

        function onGridView1_EndCallback(s, e) {

            //alert(' the end callback has been executed!');
            cl_registedlist.PerformCallback('refrush');
            //UnCheckAll('GridView1');
           // alert(cl_gvlist.GetSelectedFieldValues('spbh'));
        
        }
        function UnCheckAll(gvID) {
            var gv = document.getElementById(gvID);
            for (i = 1; i < gv.rows.length; i++) {
                if (gv.rows[i].style.display != 'none') {
                    gv.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = false;
                }
            };
        };
        function OnSelectAddGoods(result)
        {
            if (result == '')
                alert('请至少选择一个货品！');
            else {
                //alert(result);                       
                cl_gvlist.PerformCallback('add:' + result);

            }
        }
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <table style="width: 100%;">
            <tr>
                <td  colspan="4" style="text-align: center" class="auto-style37">
                    <asp:Label ID="Label1" runat="server" Text="流向品种及用户信息维护" ForeColor="#FF3300" style="font-size: x-large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td  class="auto-style44">选择供应商</td>
                <td class="auto-style45">
                    <dx:ASPxComboBox ID="cbbSupplier" runat="server" ClientInstanceName="c_supplist" OnSelectedIndexChanged="cbbSupplier_SelectedIndexChanged" AutoPostBack="True" DropDownWidth="2px" Height="29px" style="margin-top: 0px; margin-bottom: 7px; margin-left: 3px; margin-right: 0px;" Width="200px">
                    </dx:ASPxComboBox>
                </td>
                <td class="auto-style41">新增货品ID</td>
                <td class="auto-style53">
                    <asp:TextBox ID="tb_goods" runat="server" Width="325px"></asp:TextBox>
                    <span class="auto-style52">(如有多个ID请用逗号分隔)</span></td>
            </tr>
            <tr>
                <td class="auto-style44">开始日期</td>
                <td class="auto-style45">
                    <dx:ASPxDateEdit ID="deStart" runat="server" DisplayFormatString="yyyy-MM-dd" Height="29px" Width="200px" EditFormat="Custom" EditFormatString="yyyy-MM-dd">
                    </dx:ASPxDateEdit>
                </td>
                <td class="auto-style54">&nbsp;</td>
                <td class="auto-style53"></td>
            </tr>
            <tr>
                <td class="auto-style27">结束日期</td>
                <td class="auto-style45">
                    <dx:ASPxDateEdit ID="deEnd" runat="server" DisplayFormatString="yyyy-MM-dd" Height="26px" Width="200px" EditFormat="Custom" EditFormatString="yyyy-MM-dd">
                    </dx:ASPxDateEdit>
                </td>
                <td class="auto-style54">&nbsp;</td>

                <td class="auto-style53">
                    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询"   Width="60px" Font-Size="X-Small"/>
                    <asp:Button ID="btnSave" runat="server" Text="保存日期" OnClick="btnSave_Click"  Width="71px" Font-Size="X-Small"/>
                    </td>
            </tr>
            <tr>
             <td class="auto-style52" colspan="4">操作说明:1.输入货品ID(用逗号分隔)，点击查询按钮,货品信息将显示在待添加列表中,选择需要添加的货品,点击&quot;添加至己有货品列表&quot;,再点击&quot;保存己有货品列表&quot;即可完成添加操作;2.从己有货品列表中选择需要删除的货品,点击&quot;删除己有货品列表&quot;,再点击&quot;保存己有货品列表&quot;即可完成删除货品操作</td>
            </tr>
    </table>

       <br />

	    
    </div>

        <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Width="30%" Height="500px">
            <panes>
                <dx:SplitterPane AllowResize="True" AutoWidth="True" Size="40%">
                    <ContentCollection>
<dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
    <dx:ASPxGridView ID="GridView1" runat="server" AutoGenerateColumns="False" Caption="待添加货品信息列表" Font-Size="X-Small" Width="400px"  KeyFieldName="spbh" ClientInstanceName="cl_gvlist" OnCustomCallback="GridView1_CustomCallback">
        <ClientSideEvents EndCallback="function(s, e) {
	onGridView1_EndCallback(s,e)
}" />
        <Columns>
            <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True" VisibleIndex="0">
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
                <HeaderTemplate>
                    <dx:ASPxCheckBox ID="chkAll" runat="server" Text="全选" ClientSideEvents-CheckedChanged="function(s,e){cl_gvlist.SelectAllRowsOnPage(s.GetChecked());}">
                    </dx:ASPxCheckBox>
                </HeaderTemplate>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="货品ID" Name="colGoodsId" ShowInCustomizationForm="True" VisibleIndex="1" FieldName="spbh">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="货品名称" Name="colGoodName" ShowInCustomizationForm="True" VisibleIndex="2" FieldName="spmch">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="货品规格" Name="colGuige" ShowInCustomizationForm="True" VisibleIndex="3" FieldName="shpgg">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="计量单位" Name="colDw" ShowInCustomizationForm="True" VisibleIndex="4" FieldName="dw">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="生产厂家" Name="colProducter" ShowInCustomizationForm="True" VisibleIndex="5" FieldName="shpchd">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="oldspid" ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
            </dx:GridViewDataTextColumn>
        </Columns>      
        <SettingsPager Mode="ShowAllRecords">
        </SettingsPager>
        <Settings ShowVerticalScrollBar="True" UseFixedTableLayout="True" />
        <Templates>
            <FooterRow>
                <dx:ASPxButton ID="btnDelete" runat="server" Text="删除">
                </dx:ASPxButton>
            </FooterRow>
        </Templates>
    </dx:ASPxGridView>
                        </dx:SplitterContentControl>
</ContentCollection>
                </dx:SplitterPane>
                <dx:SplitterPane AllowResize="True" AutoWidth="True" Size="20%">
                    <ContentCollection>
<dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
    <br />
    <br />
    <table align="center" style="width: 126px">
        <tr>
            <td class="auto-style50">
                <dx:ASPxButton ID="btnAddtolist" runat="server" AutoPostBack="False" Text="添加至己有货品列表" Width="136px"  ClientInstanceName="cin_addlist" OnClick="btnAddtolist_Click1">
                    <ClientSideEvents Click="function(s, e) {  
                      //  alert('click the add to list button'); OnGetSelectedFieldValues 
                       cl_gvlist.GetSelectedFieldValues('spbh',OnSelectAddGoods); 
}"  />
                </dx:ASPxButton>
            </td>
        </tr>
        <tr>
            <td class="auto-style50">
                <dx:ASPxButton ID="btnDelexists" runat="server"  Text="删除己有货品列表" Width="136px" OnClick="btnDelexists_Click">
                </dx:ASPxButton>
            </td>
        </tr>
        <tr>
            <td class="auto-style50">
                <dx:ASPxButton ID="btnExport" runat="server" Text="导出己有货品列表" Width="136px" OnClick="btnExport_Click" AutoPostBack="False" EnableViewState="False">
                </dx:ASPxButton>
            </td>
        </tr>
        <tr>
            <td class="auto-style50">
                <dx:ASPxButton ID="btnSaveGoods" runat="server" Text="保存己有货品列表" Width="136px" OnClick="btnSaveGoods_Click">
                </dx:ASPxButton>
            </td>
        </tr>

                <tr>
            <td class="auto-style50">
                &nbsp;</td>
        </tr>

    </table>
                        </dx:SplitterContentControl>
</ContentCollection>
                </dx:SplitterPane>
                <dx:SplitterPane AllowResize="True" AutoWidth="True" Size="40%">
                    <ContentCollection>
                        <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxGridView ID="dgvAddList" runat="server" AutoGenerateColumns="False" Caption="己有货品列表" Font-Size="X-Small" style="margin-left: 0px" Width="400px" KeyFieldName="spbh" ClientInstanceName="cl_registedlist" OnCustomCallback="dgvAddList_CustomCallback">
                                <Columns>
                                    <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True" VisibleIndex="0">
                                        <HeaderTemplate>
                                            <dx:ASPxCheckBox ID="chkAll1" runat="server" Text="全选" ClientSideEvents-CheckedChanged="function(s,e){cl_registedlist.SelectAllRowsOnPage(s.GetChecked());}">
                                            </dx:ASPxCheckBox>
                                        </HeaderTemplate>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn Caption="货品ID" Name="colGoodsId" ShowInCustomizationForm="True" VisibleIndex="1" FieldName="spbh">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="货品名称" Name="colGoodsName" ShowInCustomizationForm="True" VisibleIndex="2" FieldName="spmch">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="货品规格" Name="colSpecial" ShowInCustomizationForm="True" VisibleIndex="3" FieldName="shpgg">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="生产厂家" Name="colProducter" ShowInCustomizationForm="True" VisibleIndex="5" FieldName="shpchd">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="计量单位" Name="colDw" ShowInCustomizationForm="True" VisibleIndex="4" FieldName="dw">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataDateColumn FieldName="oldspid" ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                    </dx:GridViewDataDateColumn>
                                </Columns>
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <Settings ShowStatusBar="Visible" ShowVerticalScrollBar="True" />
                            </dx:ASPxGridView>
                        </dx:SplitterContentControl>
                    </ContentCollection>
                </dx:SplitterPane>
            </panes>
        </dx:ASPxSplitter>



        <dx:ASPxCallback ID="cab_addlist" runat="server" OnCallback="callback_addlist_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	if (s.cpUpdate &amp;&amp; !s.cpHasErrors)  
            window.location.reload();  
}" />
        </dx:ASPxCallback>



        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="dgvAddList">
        </dx:ASPxGridViewExporter>



    </form>
</body>
</html>
