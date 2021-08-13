<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="欢迎使用学生成绩管理系统" StylesheetTheme="Blue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%; background-color: aliceblue" cellspacing="1">
        <tr>
            <td colspan="2" style="text-align: center; height: 84px;">
                <strong><span style="font-size: 24pt; color: #ff0033; font-family: 华文新魏">用户登陆</span></strong></td>
        </tr>
        <tr>
            <td style="width: 40%; text-align: right; height: 20px;">
                <span style="color: #0000ff; font-family: 楷体_GB2312;"><strong>用户编号</strong></span></td>
            <td style="width: 60%; height: 20px;">
                <asp:TextBox ID="TextBox1" runat="server" Width="139px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                    ErrorMessage="用户编号不能为空" Font-Bold="True" Font-Size="10pt"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="width: 40%; text-align: right; height: 23px;">
                <strong><span style="color: #0000ff; font-family: 楷体_GB2312;">密&nbsp; 码</span></strong></td>
            <td style="width: 60%; height: 23px;">
                <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2"
                    ErrorMessage="密码不能为空" Font-Bold="True" Font-Size="10pt"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="width: 40%; text-align: right; height: 22px;">
                <strong><span style="color: #0000ff; font-family: 楷体_GB2312;">用户类型</span></strong></td>
            <td style="width: 60%; height: 22px;">
                &nbsp;<asp:RadioButton ID="RadioButton1" runat="server" Font-Bold="True" Font-Size="11pt"
                    Text="学生" GroupName="sel" />
                &nbsp; &nbsp;
                <asp:RadioButton ID="RadioButton2" runat="server" Font-Bold="True" Font-Size="11pt"
                    Text="教师" GroupName="sel" />
                &nbsp; &nbsp;&nbsp;
                <asp:RadioButton ID="RadioButton3" runat="server" Font-Bold="True" Font-Size="11pt"
                    Text="管理员" GroupName="sel" /></td>
        </tr>
        <tr>
            <td style="width: 40%; height: 22px; text-align: right">
                <span style="color: #0000ff; font-family: 楷体_GB2312"><strong>输入验证码</strong></span></td>
            <td style="width: 60%; height: 22px">
                <asp:TextBox ID="TextBox3" runat="server" Width="52px"></asp:TextBox>
                &nbsp; &nbsp; &nbsp; <strong><span style="color: #339966; font-family: 仿宋_GB2312">验证码：</span></strong><asp:Label
                    ID="Label1" runat="server" Font-Size="12pt" Width="60px"></asp:Label>&nbsp;
                <span style="font-size: 10pt"><strong>区分大小写
                    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="看不清" Font-Size="10pt" /></strong></span></td>
        </tr>
        <tr>
            <td colspan="2" style="height: 56px">
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="#FF0033"
                    Text="登陆" OnClick="Button1_Click" />
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                <input type ="reset" ID="Button2"  Font-Bold="True" Font-Size="10pt" ForeColor="#FF0033"
                    Text="重置" style="font-weight: bold; color: red" /></td>
        </tr>
    </table>
</asp:Content>

