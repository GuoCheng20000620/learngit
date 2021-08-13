<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="studentmenu.aspx.cs" Inherits="studentmenu" Title="欢迎使用重药民生流向查询系统" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table  style="width: 100%; height: 55px">
        <tr>
            <td colspan="2" style="height: 21px">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#CC9966" Width="337px" Font-Names="隶书" Font-Size="14pt"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 14%; height: 400px; background-color: aliceblue">
                <asp:TreeView ID="TreeView1" runat="server" Font-Bold="True" Font-Names="仿宋_GB2312"
                    Font-Size="11pt">
                    <Nodes>
                        <asp:TreeNode Text="功能列表" Value="功能管理" NavigateUrl="dispinfo.aspx?info=欢迎使用本系统" Target="Iframe1">
                            <asp:TreeNode Text="库存查询" Value="库存查询" NavigateUrl="~/LiuXiang/Store_Query.aspx" Target="Iframe1"></asp:TreeNode>
                            <asp:TreeNode Text="购进查询" Value="购进查询" NavigateUrl="~/LiuXiang/Sale_Querys.aspx" Target="Iframe1"></asp:TreeNode>
                            <asp:TreeNode Text="销售查询" Value="销售查询" NavigateUrl="~/LiuXiang/Sale_Query.aspx" Target="Iframe1"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/LiuXiang/AddGoods_ZL11.aspx" Text="直连品种维护" Value="直连品种维护" Target="Iframe1"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/LiuXiang/AddGoods.aspx" Text="流向品种维护" Value="流向品种维护" Target="Iframe1"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="密码管理" Value="密码管理" NavigateUrl="dispinfo.aspx?info=欢迎使用本系统" Target="Iframe1">
                            <asp:TreeNode Text="更改我的密码" Value="更改我的密码" NavigateUrl="~/Student/updatestudentpass.aspx" Target="Iframe1"></asp:TreeNode>
                        </asp:TreeNode>
                    </Nodes>
                </asp:TreeView>
                <br />
                <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" Font-Size="11pt" ForeColor="#009900"
                    NavigateUrl="~/Default1.aspx" Target="_self">退出本系统</asp:HyperLink></td>
            <td style="width: 99%; height: 400px">
               <iframe name = "Iframe1" style="height:99%; width:99%" id = "Iframe1" src="dispinfo.aspx?info=欢迎使用本系统"></iframe>

            </td>
        </tr>
    </table>
</asp:Content>

