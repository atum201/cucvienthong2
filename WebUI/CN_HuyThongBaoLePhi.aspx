<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CN_HuyThongBaoLePhi.aspx.cs" Inherits="WebUI_CN_HuyThongBaoLePhi" Theme="default"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Huỷ thông báo lệ phí</title>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%">
            <tr>
                <td colspan="2">
                    <strong>ĐỀ XUẤT HUỶ THÔNG BÁO LỆ PHÍ</strong></td>
            </tr>
            <tr>
                <td style="width: 319px">
                </td>
                <td style="width: 906px">
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; width: 20%; text-align: left">
                    Lý do huỷ</td>
                <td style="width: 80%; vertical-align: top;">
                    <asp:TextBox ID="txtLyDoHuy" runat="server" Height="150px" TextMode="MultiLine" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLyDoHuy"
                        ErrorMessage="Bạn phải nhập lý do xin huỷ thông báo lệ phí">*</asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="width: 319px; height: 26px;">
                </td>
                <td style="width: 906px; height: 26px;">
                    <asp:Button ID="btnGui" runat="server" Text="Gửi lãnh đạo phê duyệt" OnClick="btnGui_Click" />
                    <asp:Button ID="btnBoQua" runat="server" Text="Bỏ qua" CausesValidation="False" OnClick="btnBoQua_Click" Width="77px" /></td>
            </tr>
        </table>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" />
    </form>
</body>
</html>
