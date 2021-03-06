<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="HT_VaiTro_TaoMoi.aspx.cs" Inherits="WebUI_HT_VaiTro_TaoMoi" Title="Tạo mới vai trò"
    Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin: 10px auto 10px 10px;">
        <strong>HỆ THỐNG >> <a href="HT_VaiTro_QuanLy.aspx" style="color: Blue">QUẢN LÝ VAI
            TRÒ</a> >>
            <asp:Label ID="lblHanhDong" runat="server" Font-Bold="True" Text="THÊM MỚI VAI TRÒ"></asp:Label></strong>
    </div>
    <table width="777">
        <tr>
            <td align="left" style="width: 30%; text-align: right" valign="middle">
            </td>
            <td align="left" style="text-align: left; width: 70%;">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" />
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 30%; text-align: right" valign="middle">
                Tên vai trò(*)&nbsp;</td>
            <td align="left" style="text-align: left; width: 70%;">
                <asp:TextBox ID="txtTenVaiTro" runat="server" MaxLength="50" Text="" Width="389px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rqfieldTenVaiTro" runat="server" ControlToValidate="txtTenVaiTro"
                    ErrorMessage="Bạn chưa nhập tên vai trò">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td align="left" style="width: 30%; text-align: right" valign="top">
                Mô tả&nbsp;
            </td>
            <td align="left" style="text-align: left; width: 70%;">
                <asp:TextBox ID="txtMota" runat="server" Height="70px" MaxLength="1024" Text="" TextMode="MultiLine"
                    Width="550px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 30%; text-align: right;">
                &nbsp;</td>
            <td style="text-align: left; width: 70%;">
                <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" Width="80px" OnClick="btnCapNhat_Click" />
                <asp:Button ID="btnBoQua" runat="server" CausesValidation="False" Text="Bỏ qua" Width="80px"
                    OnClick="btnBoQua_Click" /></td>
        </tr>
    </table>
</asp:Content>
