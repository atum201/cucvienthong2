<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DM_CoQuanDoKiem_ChiTiet.aspx.cs"
    Inherits="WebUI_DM_CoQuanDoKiem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Danh mục cơ quan đo kiểm</title>

    <script type="text/javascript">
    function CloseForm(){
        self.opener.window.document.forms[0].submit();
        window.close();
    }
    
    </script>

</head>
<body class="body_popup">
    <form id="form1" runat="server">
        <div>
            <fieldset style="width: 95%">
                <legend>
                    <div style="margin: 10px auto 10px 10px;">
                        <strong>THÔNG TIN CƠ QUAN ĐO KIỂM</strong>&nbsp;</div>
                </legend>
                <table align="center" border="0" width="98%">
                    <tr>
                        <td align="right" style="width: 200px; text-align: left;">
                            Tên cơ quan đo kiểm (*)</td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="txtTenCoQuanTV" runat="server" Width="325px" MaxLength="255"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfqTenTV" runat="server" ErrorMessage="Nhập vào tên cơ quan (tiếng việt)"
                                ControlToValidate="txtTenCoQuanTV">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 200px; height: 26px; text-align: left;">
                            Tên tiếng anh</td>
                        <td align="left" colspan="2" style="height: 26px">
                            <asp:TextBox ID="txtTenCoQuanTA" runat="server" Width="325px" MaxLength="255"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 200px; text-align: left;">
                            Địa chỉ</td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="txtDiaChi" runat="server" Width="325px" MaxLength="255"></asp:TextBox>&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 200px; text-align: left;">
                            Điện thoại</td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="txtDienThoai" runat="server" Width="325px" MaxLength="50"></asp:TextBox></td>
                    </tr>
                    <%--LongHH Them filedinhkem--%>
                    <tr>
                        <td align="right" style="width: 200px; text-align: left;">
                            File đính kèm</td>
                        <td align="left" colspan="2">
                            <asp:LinkButton ID="lbtnFileDinhKem" runat="server"></asp:LinkButton>
                            <asp:FileUpload ID="fileupFileDinhKem" runat="server" Width="325px" />
                        </td>
                    </tr>
                    <%--LongHH--%>
                    <tr>
                        <td style="width: 200px; text-align: left;">
                            &nbsp;</td>
                        <td style="text-align: left;" colspan="2">
                            <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" OnClick="btnCapNhat_Click" />&nbsp;<asp:Button
                                ID="btnBoQua" runat="server" CausesValidation="False" OnClientClick="CloseForm();"
                                Text="Bỏ qua" /></td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="ValidationSummary" runat="server" ShowMessageBox="True"
                    ShowSummary="False" />
                <asp:TextBox ID="txtId" runat="server" Visible="false" Width="65px"></asp:TextBox></fieldset>
        </div>
    </form>
</body>
</html>
