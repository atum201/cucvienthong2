<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DM_KhachHang_ChiTiet.aspx.cs"
    Inherits="WebUI_DM_ToChuc_ChiTiet" Title="Cục Quản Lý Chất Lượng" Theme="Default" %>
<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Đánh giá hồ sơ</title>

</head>
<body style="background-color: #ffffff">
    <form id="form1" runat="server">
        <div align="center">
            <fieldset style="width: 97%; font-family: Verdana;">
                <legend ><div style="margin: 10px auto 10px 10px;">
        <strong>THÔNG TIN TỔ CHỨC</strong>
    </div></legend>
                <table align="center" border="0" width="100%" style="font-size: 13px; font-family: Verdana">
                    <tr>
                        <td align="right" style="height: 26px; text-align: right; width: 196px;">
                            Mã tổ chức (*)</td>
                        <td align="left" style="width: 204px; height: 26px">
                            <asp:TextBox ID="txtMaTC" runat="server" Width="239px"></asp:TextBox></td>
                        <td align="right" style="height: 26px; text-align: right; width: 134px;">
                            Tỉnh thành</td>
                        <td align="left" style="width: 280px; height: 26px">
                            <cc1:Combobox ID="cbTinhThanh" runat="server">
                                <asp:ListItem>Đ&#224; Nẵng</asp:ListItem>
                                <asp:ListItem>H&#224; Nội</asp:ListItem>
                                <asp:ListItem>Huế</asp:ListItem>
                            </cc1:ComboBox></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right; width: 196px;">
                            Tên tổ chức (*)</td>
                        <td style="width: 204px; height: 21px">
                            <asp:TextBox ID="txtTenKH" runat="server" Width="239px"></asp:TextBox>&nbsp;</td>
                        <td style="height: 21px; text-align: right; width: 134px;">
                            Địa chỉ</td>
                        <td style="width: 280px; height: 21px; text-align: left">
                            <span></span>
                            <asp:TextBox ID="txtDiaChi" runat="server" Width="239px"></asp:TextBox></td>
                    </tr>
                    <tr style="font-size: 13px">
                        <td style="height: 21px; text-align: right; width: 196px;">
                            Tên tiếng anh</td>
                        <td style="width: 204px; height: 21px; text-align: left">
                            <asp:TextBox ID="txtTenKHTA" runat="server" Width="239px"></asp:TextBox></td>
                        <td style="height: 21px; text-align: right; width: 134px;">
                            Mã Doanh nghiệp</td>
                        <td style="width: 280px; height: 21px; text-align: left">
                            <asp:TextBox ID="txtMaDN" runat="server" Width="239px"></asp:TextBox></td>
                    </tr>
                    <tr style="font-size: 13px">
                        <td style="height: 19px; text-align: right; width: 196px;">
                            Tên tắt</td>
                        <td style="width: 204px; height: 19px; text-align: left">
                            <asp:TextBox ID="txtTenTat" runat="server" Width="239px"></asp:TextBox></td>
                        <td style="height: 19px; text-align: right; width: 134px;">
                            Điện thoại</td>
                        <td style="width: 280px; height: 19px; text-align: left">
                            <asp:TextBox ID="txtPhone" runat="server" Width="239px"></asp:TextBox></td>
                    </tr>
                    <tr style="font-size: 13px">
                        <td style="height: 21px; text-align: right; width: 196px;">
                            Mail</td>
                        <td style="width: 204px; height: 21px; text-align: left">
                            <asp:TextBox ID="txtMail" runat="server" Width="239px"></asp:TextBox></td>
                        <td style="height: 21px; text-align: right; width: 134px;">
                            Fax&nbsp;</td>
                        <td style="width: 280px; height: 21px; text-align: left">
                            <asp:TextBox ID="txtFax" runat="server" Width="239px"></asp:TextBox></td>
                    </tr>
                    <tr style="font-size: 13px">
                        <td style="width: 196px">
                            &nbsp;</td>
                        <td style="width: 204px; text-align: left">
                            <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" OnClick="btnCapNhat_Click"
                                Width="80px" />
                            <asp:Button ID="btnCancel" runat="server" Text="Bỏ qua" Width="80px" OnClick="btnCancel_Click" /></td>
                        <td style="width: 134px">
                            &nbsp;</td>
                        <td style="width: 280px">
                            &nbsp;</td>
                    </tr>
                </table>
            </fieldset>
            <span style="font-family: Verdana"></span>
        </div>
    </form>
</body>
</html>
