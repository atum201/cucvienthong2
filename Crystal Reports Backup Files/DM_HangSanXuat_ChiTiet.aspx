<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DM_HangSanXuat_ChiTiet.aspx.cs" Inherits="WebUI_DM_HangSanXuat_ChiTiet" Title="Hãng sản xuất" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript">
        function CloseForm(){
        self.opener.window.document.forms[0].submit();
        window.close();
    }    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <fieldset style="width: 95%">
            <legend style="color: #000000"><div style="margin: 10px auto 10px 10px;">
        <strong>THÔNG TIN HÃNG SẢN XUẤT</strong>
    </div></legend>
            <table align="center" border="0" width="100%" style="color: #000000">
                <tr>
                    <td align="right">
                    </td>
                    <td align="left" colspan="2">
                        <asp:ValidationSummary ID="ValidationSummary" runat="server" ShowMessageBox="True"
                            ShowSummary="False" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Tên hãng sản xuất <span>(*)</span></td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtId" runat="server" Width="365px" Visible="false" />
                        <asp:TextBox ID="txtTen" runat="server" Width="365px" MaxLength ="255"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqfTenPB" runat="server" ControlToValidate="txtTen"
                            ErrorMessage="Nhập vào tên hãng sản xuất">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td align="right">
                        Tên tiếng anh</td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtTenTA" runat="server" MaxLength="255" Width="365px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td style="text-align: left">
                        <asp:Button ID="btnCapnhat" runat="server" Text="Cập nhật" OnClick="btnCapnhat_Click" /> 
                        <asp:Button ID="btnBoQua" runat="server" CausesValidation="False" OnClientClick="javascript:window.close();"
                            Text="Bỏ qua" />
                        </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </fieldset>
    
    </div>
    </form>
</body>
</html>
