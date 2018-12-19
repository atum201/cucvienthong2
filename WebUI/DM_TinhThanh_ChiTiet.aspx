<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DM_TinhThanh_ChiTiet.aspx.cs"
    Inherits="WebUI_DM_TinhThanh_ChiTiet" Title="Untitled Page" Theme="Default" %>
<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tỉnh thành</title>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <fieldset style="width: 97%">
                <legend><div style="margin: 10px auto 10px 10px;">
        <strong>THÔNG TIN TỈNH THÀNH</strong>
    </div></legend>
                <table align="center" border="0" width="100%">
                    <tr>
                        <td align="right" style="width: 200px; height: 26px">
                            Đơn vị quản lý</td>
                        <td align="left" colspan="3" style="height: 26px">
                            <cc1:ComboBox  ID="DropDownList1" runat="server" Width="270px">                                
                           </cc1:ComboBox ></td>
                    </tr>
                    <tr>
                        <td align="right" style="height: 26px; width: 200px;">
                            Mã tỉnh thành&nbsp;</td>
                        <td align="left" colspan="3" style="height: 26px">
                            <asp:TextBox ID="txtMaTT" runat="server" Width="270px"></asp:TextBox>&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right" style="height: 26px">
                            Tên tỉnh thành&nbsp;</td>
                        <td align="left" colspan="3" style="height: 26px">
                            <asp:TextBox ID="txtTenTT" runat="server" Width="270px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: left" colspan="3">
                            <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" OnClick="btnCapNhat_Click" />
                            <asp:Button ID="btnCancel" runat="server"
                                Text="Bỏ qua" OnClick="btnCancel_Click" /></td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </form>
</body>
</html>
