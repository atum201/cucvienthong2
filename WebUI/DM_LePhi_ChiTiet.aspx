<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DM_LePhi_ChiTiet.aspx.cs"
    Inherits="WebUI_DM_LePhi_ChiTiet" Title="Cục Quản Lý Chất Lượng" Theme="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Lệ phí</title>
</head>
<body>
    <form id="form1" runat="server">
        <fieldset style="width: 97%">
            <legend>
                <div style="margin: 10px 0px 0px 0px;">
                    <strong>THÔNG TIN LỆ PHÍ</strong>
                </div>
            </legend>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" />
            <table align="center" border="0" width="100%">
                <tr>
                    <td style="height: 28px; text-align: left; width: 100px;">
                        Giá trị lô hàng(*)</td>
                    <td align="left" colspan="3" style="height: 28px">
                        <asp:TextBox ID="txtGiaTriLoHang" runat="server" Width="337px" MaxLength="255"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGiaTriLoHang"
                            ErrorMessage="Bạn phải nhập Giá trị lô hàng" Display="Dynamic">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 100px;">
                        Mức lệ phí(*)</td>
                    <td align="left" colspan="3" style="height: 15px">
                        <asp:TextBox ID="txtMucLePhi" runat="server" Width="155px" MaxLength="8"></asp:TextBox>
                        (Đơn vị:1000đ)<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ControlToValidate="txtMucLePhi" ErrorMessage="Bạn phải nhập Mức lệ phí">*</asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Nhập sai định dạng.">*</asp:CustomValidator></td>
                </tr>
                <tr>
                    <td style="width: 100px; text-align: left">
                        &nbsp;</td>
                    <td style="text-align: left" colspan="3">
                        <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" OnClick="btnCapNhat_Click"
                            Width="90px" />
                        <asp:Button ID="btnCancel" runat="server" Text="Bỏ qua" OnClick="btnCancel_Click"
                            CausesValidation="False" Width="90px" />
                        &nbsp;</td>
                </tr>
            </table>
        </fieldset>
    </form>
</body>
</html>
