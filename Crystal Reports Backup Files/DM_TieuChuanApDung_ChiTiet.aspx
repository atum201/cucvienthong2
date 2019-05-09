<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DM_TieuChuanApDung_ChiTiet.aspx.cs"
    Inherits="WebUI_DM_TieuChuanApDung_ChiTiet" Title="Tiêu chuẩn áp dụng" Theme="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>

    <script type="text/javascript" src="../Js/Common.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset style="width: 97%">
                <legend>
                    <div style="margin: 10px 0px 0px 0px;">
                        <strong>THÔNG TIN TIÊU CHUẨN ÁP DỤNG</strong>
                    </div>
                </legend>
                <table align="center" border="0" width="100%">
                    <tr>
                        <td align="left" style="height: 26px; width: 200px;">
                            Mã tiêu chuẩn<span style="color: #000000">(*)</span></td>
                        <td align="left" colspan="2" style="height: 26px; text-align: left">
                            <asp:TextBox ID="txtMaTC" runat="server" Width="365px" MaxLength="255"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMaTC" Display="Dynamic"
                                ErrorMessage="Nhập mã tiêu chuẩn">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="left" style="height: 24px">
                            Tên tiêu chuẩn<span style="color: #000000">(*)</span></td>
                        <td align="left" colspan="2" style="height: 24px">
                            <asp:TextBox ID="txtTenTieuChuan" runat="server" Width="365px" MaxLength="255"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator
                                ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTenTieuChuan"
                                Display="Dynamic" ErrorMessage="Nhập tên tiêu chuẩn">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="left" style="height: 24px">
                            Tên tiếng anh</td>
                        <td align="left" colspan="2" style="height: 24px">
                            <asp:TextBox ID="txtTenTiengAnh" runat="server" MaxLength="255" Width="365px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: left; vertical-align: top;">
                            &nbsp;Ghi chú</td>
                        <td colspan="2" style="height: 21px; text-align: left">
                            <asp:TextBox ID="txtGhiChu" runat="server" Height="100px" TextMode="MultiLine" Width="361px"></asp:TextBox>&nbsp;<asp:ValidationSummary
                                ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: left">
                            <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" OnClick="btnCapNhat_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Bỏ qua" OnClick="btnCancel_Click"
                                CausesValidation="False" /></td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </form>
</body>
</html>
