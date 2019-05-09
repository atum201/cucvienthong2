<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DM_TrungTam_ChiTiet.aspx.cs"
    Inherits="WebUI_DM_TrungTam_ChiTiet" Title="Cập nhật thông tin trung tâm" Theme="Default" %>

<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Trung tâm</title>

    <script type="text/javascript" src="../Js/jquery-1.3.2-vsdoc2.js"></script>

    <script type="text/javascript" src="../Js/Common.js"></script>

    <script type="text/javascript" src="../Js/dhtmlxcombo.js"></script>

    <script type="text/javascript" src="../Js/dhtmlxcommon.js"></script>

    <script type="text/javascript">
        window.dhx_globalImgPath="../Images/";
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset style="width: 97%px">
                <legend style="color: #000000">
                    <div style="margin: 10px 0px 0px 0px">
                        <strong>THÔNG TIN TRUNG TÂM</strong>
                    </div>
                </legend>
                <table align="center" border="0" style="width: 100%; color: #000000;">
                    <tr>
                        <td align="right" style="width: 200px; height: 24px;">
                            Tên trung tâm <span>(*)</span></td>
                        <td align="left" colspan="2" style="width: 245px; height: 24px; color: #000000;">
                            <asp:TextBox ID="txtTenTrungTam" runat="server" Width="200px" MaxLength="225"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqfTenTrungTam" runat="server" ErrorMessage="Nhập tên trung tâm(Tiếng Việt)"
                                ControlToValidate="txtTenTrungTam">*</asp:RequiredFieldValidator></td>
                        <td align="left" colspan="1" style="width: 122px; height: 24px; color: #000000;">
                            Tỉnh thành quản lý</td>
                        <td align="left" colspan="1" style="width: 167px; color: #000000;" rowspan="17" valign="top">
                            <asp:Panel ID="Panel1" runat="server" BorderStyle="Inset" Height="300px" Width="160px"
                                ToolTip="Danh mục các tỉnh thành">
                                <asp:CheckBoxList ID="chklTinhThanh" runat="server" BorderWidth="0px" Width="150px">
                                </asp:CheckBoxList></asp:Panel>
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td align="right" style="height: 24px; width: 200px;">
                            Tên tiếng Anh<span>(*)</span></td>
                        <td align="left" colspan="2" style="height: 24px; width: 245px; color: #000000;">
                            <asp:TextBox ID="txtTenTrungTamTA" runat="server" Width="200px" MaxLength="225"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqfTenTrungTamTA" ControlToValidate="txtTenTrungTamTA"
                                runat="server" ErrorMessage="Nhập tên trung tâm(Tiếng Anh)">*</asp:RequiredFieldValidator></td>
                        <td align="left" colspan="1" style="width: 122px; height: 24px; color: #000000;">
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td align="right" style="height: 24px; width: 200px;">
                            Mã trung tâm (Theo GCN)<span>(*)</span></td>
                        <td align="left" colspan="2" style="width: 245px; height: 24px; color: #000000;">
                            <asp:TextBox ID="txtMaTrungTam1" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqfMaTrungTam1" ControlToValidate="txtMaTrungTam1"
                                runat="server" ErrorMessage="Nhập mã trung tâm">*</asp:RequiredFieldValidator></td>
                        <td align="left" colspan="1" style="width: 122px; height: 24px; color: #000000;">
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td align="right" style="height: 18px; width: 200px;">
                            Mã trung tâm (Theo chuẩn)<span>(*)</span></td>
                        <td align="left" colspan="2" style="width: 245px; height: 18px">
                            <asp:TextBox ID="txtMaTrungTam2" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqfMaTrungTam2" ControlToValidate="txtMaTrungTam2"
                                runat="server" ErrorMessage="Nhập mã trung tâm(theo chuẩn)">*</asp:RequiredFieldValidator></td>
                        <td align="left" colspan="1" style="width: 122px; height: 18px">
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td align="right" style="width: 200px; height: 18px">
                            Tỉnh thành (*)</td>
                        <td align="left" colspan="2" style="width: 245px; height: 18px">
                            <cc1:ComboBox ID="drlTinhThanh" runat="server" Width="200px">
                            </cc1:ComboBox>
                            <asp:RequiredFieldValidator ID="rfqTinhThanh" runat="server" ControlToValidate="drlTinhThanh"
                                InitialValue="0" ErrorMessage="Chọn tỉnh thành">*</asp:RequiredFieldValidator></td>
                        <td align="left" colspan="1" style="width: 122px; height: 18px">
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td align="right" style="height: 18px; width: 200px;">
                            Địa chỉ</td>
                        <td align="left" colspan="2" style="width: 245px; height: 18px">
                            <asp:TextBox ID="txtDiaChi" runat="server" Width="200px" MaxLength="225"></asp:TextBox></td>
                        <td align="left" colspan="1" style="width: 122px; height: 18px">
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td align="right" style="height: 18px; width: 200px;">
                            Số điện thoại</td>
                        <td align="left" colspan="2" style="width: 245px; height: 18px">
                            <asp:TextBox ID="txtDienThoai" runat="server" Width="200px" MaxLength="20"></asp:TextBox></td>
                        <td align="left" colspan="1" style="width: 122px; height: 18px">
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td align="right" style="width: 200px; height: 18px">
                            Email</td>
                        <td align="left" colspan="2" style="width: 245px; height: 18px">
                            <asp:TextBox ID="txtEmail" runat="server" Width="200px" MaxLength="225"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="rqfEmail" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ErrorMessage="Địa chỉ Email khong hop le!" ControlToValidate="txtEmail">x</asp:RegularExpressionValidator></td>
                        <td align="left" colspan="1" style="width: 122px; height: 18px">
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td align="right" style="height: 18px; width: 200px;">
                            Fax</td>
                        <td align="left" colspan="2" style="width: 245px; height: 18px">
                            <asp:TextBox ID="txtFax" runat="server" Width="200px" MaxLength="20"></asp:TextBox></td>
                        <td align="left" colspan="1" style="width: 122px; height: 18px">
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td align="right" style="height: 18px; width: 200px;">
                            Số tài khoản HC</td>
                        <td align="left" colspan="2" style="width: 245px; height: 18px">
                            <asp:TextBox ID="txtSoTaiKhoan" runat="server" Width="200px" MaxLength="50"></asp:TextBox></td>
                        <td align="left" colspan="1" style="width: 122px; height: 18px">
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td align="right" style="height: 18px; width: 200px;">
                            Tên kho bạc HC</td>
                        <td align="left" colspan="2" style="width: 245px; height: 18px">
                            <asp:TextBox ID="txtTenKhoBac" runat="server" Width="200px" MaxLength="225"></asp:TextBox></td>
                        <td align="left" colspan="1" style="width: 122px; height: 18px">
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td align="right" style="height: 18px; width: 200px;">
                            Tên đơn vị thụ hưởng HC</td>
                        <td align="left" colspan="2" style="width: 245px; height: 18px">
                            <asp:TextBox ID="txtTenDonViThuHuong" runat="server" Width="200px" MaxLength="225"></asp:TextBox></td>
                        <td align="left" colspan="1" style="width: 122px; height: 18px">
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td align="right" style="width: 200px; height: 18px">
                            Địa chỉ đơn vị thụ hưởng HC</td>
                        <td align="left" colspan="2" style="width: 245px; height: 18px">
                            <asp:TextBox ID="txtDiaChiDonViThuHuong" runat="server" MaxLength="225" Width="200px"></asp:TextBox></td>
                        <td align="left" colspan="1" style="width: 122px; height: 18px">
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td align="right" style="height: 18px; width: 200px;">
                            Số tài khoản HQ</td>
                        <td align="left" colspan="2" style="width: 245px; height: 18px">
                            <asp:TextBox ID="txtSoTaiKhoanHQ" runat="server" MaxLength="50" Width="200px"></asp:TextBox></td>
                        <td align="left" colspan="1" style="width: 122px; height: 18px">
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td align="right" style="height: 18px; width: 200px;">
                            Tên kho bạc HQ</td>
                        <td align="left" colspan="2" style="width: 245px; height: 18px">
                            <asp:TextBox ID="txtTenKhoBacHQ" runat="server" MaxLength="225" Width="200px"></asp:TextBox></td>
                        <td align="left" colspan="1" style="width: 122px; height: 18px">
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td align="right" style="height: 18px; width: 200px;">
                            Tên đơn vị thụ hưởng HQ</td>
                        <td align="left" colspan="2" style="width: 245px; height: 18px">
                            <asp:TextBox ID="txtDonViThuHuongHQ" runat="server" MaxLength="225" Width="200px"></asp:TextBox></td>
                        <td align="left" colspan="1" style="width: 122px; height: 18px">
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td align="right" style="width: 200px; height: 18px">
                            Địa chỉ đơn vị thụ hưởng HQ</td>
                        <td align="left" colspan="2" style="width: 245px; height: 18px">
                            <asp:TextBox ID="txtDiaChiDonViThuHuongHQ" runat="server" MaxLength="225" Width="200px"></asp:TextBox></td>
                        <td align="left" colspan="1" style="width: 122px; height: 18px">
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td style="height: 20px; width: 200px;">
                            &nbsp;</td>
                        <td colspan="2" style="height: 20px; width: 245px;">
                            <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" OnClick="btnCapNhat_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Bỏ qua" CausesValidation="false"
                                OnClientClick="javascript:window.close();" />&nbsp;</td>
                        <td style="width: 122px; height: 20px;">
                        </td>
                        <td style="height: 20px; width: 167px;">
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" />
    </form>
</body>
</html>
