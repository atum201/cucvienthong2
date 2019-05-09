<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MauDauHopQuy_ChiTiet.aspx.cs"
    Inherits="WebUI_MauDauHopQuy_ChiTiet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Thêm mới mẫu dấu</title>

    <script type="text/javascript" src="../Js/jquery-1.3.2-vsdoc2.js"></script>

    <script type="text/javascript">          
        function SelectItem(ImgID){
            var url_image;
            var imgCtrl="../Handler.ashx?MauDauId=" + ImgID;
            url_image = imgCtrl.attr("src");     
            window.opener.SetSignImage(url_image);           
            window.close();
            return false;
        }        
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 10px 10px 10px 10px">
            <fieldset>
                <legend>Thông tin mẫu dấu</legend>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                    ShowMessageBox="true" ShowSummary="false" />
                <table>
                    <tr>
                        <td style="height: 26px; width: 120px;">
                            Đơn vị</td>
                        <td style="height: 26px; width: 400px;">
                            <asp:TextBox ID="txtDonvi" runat="server" BackColor="#FFFF80" Width="95%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 26px">
                            Tên mẫu dấu
                        </td>
                        <td style="height: 26px">
                            <asp:TextBox ID="txtTenMauDau" runat="server" Width="194px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfqTenDau" runat="server" ControlToValidate="txtTenMauDau"
                                ErrorMessage="Nhập tên mẫu dấu">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="height: 21px">
                            Mã mẫu dấu</td>
                        <td style="height: 21px">
                            <asp:TextBox ID="txtMaMauDau" runat="server" Width="194px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Mẫu dấu
                        </td>
                        <td>
                            <asp:Image ID="imgMauDau" runat="server" Height="90px" Width="90px" AlternateText="Mẫu dấu" />&nbsp;
                            &nbsp;<asp:FileUpload
                                ID="fileupMauDau" runat="server" Width="250px" /></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" OnClick="btnCapNhat_Click" />
                            <asp:Button ID="btnBoQua" runat="server" Text="Bỏ qua" CausesValidation="false" /></td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </form>
</body>
</html>
