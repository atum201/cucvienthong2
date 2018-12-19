<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DM_PhongBan_ChiTiet.aspx.cs"
    Inherits="WebUI_DM_PhongBan_ChiTiet" Title="Danh mục phòng ban" Theme="Default" %>

<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Phòng ban</title>

    <script type="text/javascript" src="../Js/Common.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset style="width: 97%">
                <legend>
                    <div style="margin: 10px 0px 0px 0px;">
                        <strong>THÔNG TIN PHÒNG BAN</strong>
                    </div>
                </legend>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" />
                <table border="0" width="100%" style="text-align: left;margin: 10px 0px 0px 0px ;">
                    <tr>
                        <td align="left">
                            Tên phòng ban(*)</td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="txtTenPB" runat="server" Width="365px" MaxLength="255"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqfTenPB" runat="server" ControlToValidate="txtTenPB"
                                ErrorMessage="Bạn phải nhập tên Phòng ban" Display="Dynamic">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="left" style="text-align: left; height: 24px;">
                            Trưởng Phòng<span style="color: #ff0033"></span></td>
                        <td align="left" colspan="2" style="height: 24px">
                            <cc1:ComboBox ID="ddlTruongPhong" runat="server" Width="150px">
                            </cc1:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="text-align: left; vertical-align: top; height: 88px">
                            Mô tả</td>
                        <td align="left" colspan="2" style="height: 88px">
                            <asp:TextBox ID="txtMoTa" runat="server" Height="77px" TextMode="MultiLine" Width="365px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: left" colspan="2">
                            <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" OnClick="btnCapNhat_Click"
                                Width="90px" />
                            <asp:Button ID="btnCancel" runat="server" Text="Bỏ qua" OnClick="btnCancel_Click"
                                CausesValidation="False" Width="90px" />&nbsp;</td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </form>
</body>
</html>
