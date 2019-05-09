<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DM_DonVi_ChiTiet.aspx.cs"
    Inherits="WebUI_DM_DonVi_ChiTiet" Title="Cục Quản Lý Chất Lượng" Theme="Default" %>

<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Đánh giá hồ sơ</title>
    
    <script type="text/javascript" src="../Js/jquery-1.3.2-vsdoc2.js"></script>

    <script type="text/javascript" src="../Js/dhtmlxcombo.js"></script>

    <script type="text/javascript" src="../Js/dhtmlxcommon.js"></script>
    <script type="text/javascript">
        window.dhx_globalImgPath="../Images/";
    </script>
</head>
<body style="background-color: #ffffff">
    <form id="form1" runat="server">
        <div>
            <fieldset style="width: 97%; font-family: Verdana; padding-top: 20px">
                <legend>
                    <div style="margin: 10px 0px 0px 0px;">
                        <strong>THÔNG TIN ĐƠN VỊ</strong>
                    </div>
                </legend>
                <table align="center" border="0" width="100%" style="font-size: 13px; font-family: Verdana;
                    color: #000000;">
                    <tr>
                        <td align="right" style="width: 120px; text-align: left; height: 24px;">
                            <span style="font-family: Arial">Mã đơn vị <span>(*)</span></span></td>
                        <td align="left" style="width: 220px; text-align: left;">
                            <asp:TextBox ID="txtMaDonVi" runat="server" Width="200px" Font-Names="Arial" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqfTenPB" runat="server" ControlToValidate="txtMaDonVi"
                                ErrorMessage="Bạn phải nhập mã đơn vị">*</asp:RequiredFieldValidator></td>
                        <td align="right" style="width: 120px; text-align: left; height: 24px;">
                            <span style="font-family: Arial">Mật khẩu <span style="color: #000000">(*)</span></span></td>
                        <td align="left" style="text-align: left;">
                            <asp:CheckBox ID="chkPassChange" runat="server" AutoPostBack="True" OnCheckedChanged="chkPassChange_CheckedChanged"
                                Text="Thay đổi mật khẩu" Visible="False" />
                            <asp:TextBox ID="txtMatKhau" runat="server" Width="200px" Font-Names="Arial" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMatKhau"
                                ErrorMessage="Bạn phải nhập mật khẩu">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 120px; text-align: left;" align="right">
                            <span style="font-family: Arial">Tỉnh thành <span style="color: #000000">(*)</span></span></td>
                        <td style="width: 220px; text-align: left;">
                            <cc1:ComboBox ID="cbTinhThanh" runat="server" Width="202px">
                            </cc1:ComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="cbTinhThanh"
                                Display="Dynamic" ErrorMessage="Chọn tỉnh thành">*</asp:RequiredFieldValidator></td>
                        <td style="width: 120px; text-align: left;">
                            <span style="font-family: Arial">Địa chỉ <span style="color: #000000">(*)</span></span></td>
                        <td style="text-align: left">
                            <span></span>
                            <asp:TextBox ID="txtDiaChi" runat="server" Width="200px" Font-Names="Arial" MaxLength="255"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDiaChi"
                                ErrorMessage="Bạn phải nhập địa chỉ">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr style="font-size: 13px">
                        <td style="width: 120px; text-align: left;" align="right">
                            <span style="font-family: Arial">Tên đơn vị (*)<span style="color: #ff0000"></span></span></td>
                        <td style="width: 220px; text-align: left;">
                            <asp:TextBox ID="txtTenTiengViet" runat="server" Width="200px" Font-Names="Arial"
                                MaxLength="255"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server" ControlToValidate="txtTenTiengViet" ErrorMessage="Bạn phải nhập tên đơn vị">*</asp:RequiredFieldValidator></td>
                        <td style="width: 120px; text-align: left;">
                            <span style="font-family: Arial">Email </span></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMail" runat="server" Width="200px" Font-Names="Arial" MaxLength="255"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionEmail" runat="server" ControlToValidate="txtMail"
                                ErrorMessage="Địa chỉ Email không hợp lệ" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
                    </tr>
                    <tr style="font-size: 13px">
                        <td style="width: 120px; text-align: left">
                            <span style="font-family: Arial">Tên tiếng anh</span></td>
                        <td style="width: 220px; text-align: left;">
                            <asp:TextBox ID="txtTenTiengAnh" runat="server" Width="200px" Font-Names="Arial"
                                MaxLength="255"></asp:TextBox></td>
                        <td style="width: 120px; text-align: left">
                            <span style="font-family: Arial">Điện thoại</span></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPhone" runat="server" Width="200px" Font-Names="Arial" MaxLength="50"></asp:TextBox>
                            </td>
                    </tr>
                    <tr style="font-size: 13px">
                        <td style="width: 120px; text-align: left;" align="right">
                            <span style="font-family: Arial">Tên tắt</span></td>
                        <td style="width: 220px; text-align: left;">
                            <asp:TextBox ID="txtTenTat" runat="server" Width="200px" Font-Names="Arial" MaxLength="255"></asp:TextBox></td>
                        <td style="width: 120px; text-align: left;">
                            <span style="font-family: Arial">Fax&nbsp;</span></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFax" runat="server" Width="200px" Font-Names="Arial" MaxLength="50"></asp:TextBox></td>
                    </tr>
                    <tr style="font-size: 13px">
                        <td align="right" style="width: 120px; text-align: left">
                            Mã số thuế</td>
                        <td style="width: 220px; text-align: left">
                            <asp:TextBox ID="txtMaSoThue" runat="server" Font-Names="Arial" MaxLength="255" Width="200px"></asp:TextBox></td>
                        <td style="width: 120px; text-align: left">
                            Giấy phép kinh doanh
                        </td>
                        <td style="text-align: left">
                            <%--LongHH--%>
                            <asp:LinkButton ID="lbtnGIAY_PHEP_KINH_DOANH" runat="server"></asp:LinkButton>
                            <asp:FileUpload ID="fileupGiayPhepKinhDoanh" runat="server" Width="200px" />
                            <%--LongHH--%>
                        </td>
                    </tr>
                    <tr style="font-size: 13px">
                        <td style="width: 120px; text-align: left;" align="right">
                            &nbsp;</td>
                        <td style="width: 220px; text-align: left;">
                            <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" OnClick="btnCapNhat_Click"
                                Width="80px" />
                            <asp:Button ID="btnCancel" runat="server" Text="Bỏ qua" Width="80px" OnClick="btnCancel_Click"
                                CausesValidation="False" /></td>
                        <td style="width: 120px; text-align: left;" align="right">
                            <span style="font-family: Arial"></span>
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                </table>
            </fieldset>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" />
            <span style="font-family: Verdana"></span>
        </div>
    </form>
</body>
</html>
