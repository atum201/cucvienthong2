<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DM_NhomSanPham_ChiTiet.aspx.cs"
    Inherits="WebUI_DM_NhomSanPham_ChiTiet" Title="Cục Quản Lý Chất Lượng" Theme="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Nhóm sản phẩm</title>
</head>
<body>
    <form id="form1" runat="server">
        <div >
            <fieldset style="width: 97%">
                <legend style="color: #000000">
                    <div style="margin: 10px auto 10px 10px;;float:left">
                        <strong>THÔNG TIN NHÓM SẢN PHẨM</strong>
                    </div>
                </legend>
                <table align="center" border="0" width="100%" style="color: #000000">
                    <tr>
                        <td align="left"style="height: auto">
                        </td>
                        <td align="left" colspan="3" style="height: auto">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="False" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="height: 26px;" nowrap="nowrap">
                            Mã nhóm trong GCN<span>(*)</span></td>
                        <td align="left" colspan="3" style="height: 26px">
                            <asp:TextBox ID="txtMaNhomSP" runat="server" Width="250px" MaxLength="50"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMaNhomSP" ErrorMessage="Bạn phải nhập mã nhóm sản phẩm trong GCN">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="left" style="height: 26px" nowrap="nowrap">
                            Tên nhóm <span>(*)</span></td>
                        <td align="left" colspan="3" style="height: 26px">
                            <asp:TextBox ID="txtTenNhomSP" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTenNhomSP"
                                ErrorMessage="Bạn phải nhập tên nhóm sản phẩm">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="left" nowrap="nowrap" style="height: 26px">
                            Loại hình chứng nhận</td>
                        <td align="left" colspan="3" style="height: 26px">
                            <asp:DropDownList ID="ddlLoaiHinhChungNhan" runat="server" Width="253px">
                                <asp:ListItem Value="2">Hợp chuẩn</asp:ListItem>
                                <asp:ListItem Value="1">Hợp quy</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td align="left" style="height: 15px" nowrap="nowrap">
                            Mức lệ phí <span>(*)</span></td>
                        <td align="left" colspan="3" style="height: 15px">
                            <asp:TextBox ID="txtMucLePhi" runat="server" Width="250px" MaxLength="9"></asp:TextBox>
                            (Đơn vị:1000đ)
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMucLePhi"
                                ErrorMessage="Bạn phải nhập mức lệ phí cho nhóm sản phẩm">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Nhập sai định dạng.">*</asp:CustomValidator></td>
                    </tr>
                    <tr>
                        <td align="left" style="height: 15px" nowrap="nowrap">
                            Thời hạn</td>
                        <td align="left" colspan="3" style="height: 15px">
                            <asp:DropDownList ID="ddlThoiHan" runat="server" Width="253px">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td align="left" style="height: 15px" nowrap="nowrap">
                            Liên quan tần số</td>
                        <td align="left" colspan="3" style="height: 15px">
                            <asp:RadioButton ID="rdCo" runat="server" GroupName="LienQuanTanSo" Text="Có" />
                            <asp:RadioButton ID="rdKhong" runat="server" GroupName="LienQuanTanSo" Text="Không" /></td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: left" colspan="3">
                            <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" OnClick="CapNhat_Click"
                                Width="90px" />
                            <asp:Button ID="btnCancel" runat="server" Text="Bỏ qua" OnClick="btnCancel_Click"
                                Width="90px" CausesValidation="False" />
                            &nbsp;</td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </form>
</body>
</html>
