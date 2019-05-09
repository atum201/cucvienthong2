<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="HT_ThayDoiThongTinCaNhan.aspx.cs" Inherits="WebUI_HT_ThayDoiThongTinCaNhan"
    Title="Thay đổi thông tin cá nhân" Theme="Default" %>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin: 10px auto 10px 10px;">
        <strong>HỆ THỐNG >> THÔNG TIN CÁ NHÂN</strong>
    </div>
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td style="width: 15%;" class="caption">
                        </td>
                        <td style="text-align: left;">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="False" />
                        </td>
                    </tr>
                    <tr>
                        <td class="caption">
                            Tên đơn vị</td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlTenDonVi" runat="server" Width="35%" Enabled="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="caption">
                            Tên phòng ban</td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlTenPhongBan" runat="server" Width="35%" Enabled="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="caption">
                            Tên truy cập (*)</td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtTenDangNhap" runat="server" Width="35%" MaxLength="50" BackColor="#FFFFC0"
                                ReadOnly="True"></asp:TextBox>&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTenDangNhap"
                                ErrorMessage="Bạn phải nhập tên truy cập">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="caption">
                            Họ và tên(*)</td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox ID="txtHoTen" runat="server" Width="35%" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldFullName" runat="server" ControlToValidate="txtHoTen"
                                ErrorMessage="Bạn phải nhập họ tên">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="caption">
                            Ngày sinh
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtNgaySinh" runat="server" Width="87px" MaxLength="10"></asp:TextBox>
                            <rjs:PopCalendar ID="pcalNgaySinh" runat="server" Control="txtNgaySinh" Format="dd mm yyyy"
                                InvalidDateMessage="Nhập sai định dạng ngày tháng" MessageAlignment="RightCalendarControl"
                                Separator="/" ShowErrorMessage="False"></rjs:PopCalendar>
                            (dd/mm/yyyy)<asp:RangeValidator ID="rvdate" runat="server" ControlToValidate="txtNgaySinh"
                                ErrorMessage="Ngày sinh không được lớn hơn ngày hiện tại" MinimumValue="01/01/1900"
                                Type="Date">*</asp:RangeValidator></td>
                    </tr>
                    <tr>
                        <td class="caption">
                            Giới tính</td>
                        <td style="text-align: left;">
                            <asp:RadioButton ID="rbNam" runat="server" Checked="true" GroupName="GioiTinh" Text="Nam" />&nbsp;
                            <asp:RadioButton ID="rbNu" runat="server" GroupName="GioiTinh" Text="Nữ" /></td>
                    </tr>
                    <tr>
                        <td class="caption">
                            Quê quán</td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtQueQuan" runat="server" Width="35%" MaxLength="255"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="caption">
                            Địa chỉ</td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtDiaChi" runat="server" Width="35%" MaxLength="255"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="caption">
                            Số điện thoại(*)</td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtDienThoai" runat="server" MaxLength="15" Width="35%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requirePhoneNumber" runat="server" ControlToValidate="txtDienThoai"
                                ErrorMessage="Bạn phải nhập số điện thoại liên hệ">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="caption">
                            Email</td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="35%"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionEmail" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="Địa chỉ Email không hợp lệ" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
                    </tr>
                    <tr id="rThayDoiMatKhau">
                        <td class="caption">
                        </td>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="chkThayDoiMatKhau" runat="server" Text="Thay đổi mật khẩu" AutoPostBack="True"
                                OnCheckedChanged="chkThayDoiMatKhau_CheckedChanged" /></td>
                    </tr>
                    <tr id="rMatKhauCu" runat="server">
                        <td class="caption">
                            Mật khẩu cũ</td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtMatKhauCu" runat="server" MaxLength="255" Width="35%" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldPassWord" runat="server" ControlToValidate="txtMatKhauCu"
                                ErrorMessage="Bạn phải nhập mật khẩu cũ">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr id="rMatKhauMoi1" runat="server">
                        <td class="caption">
                            Mật khẩu mới</td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtMatKhauMoi" runat="server" MaxLength="255" Width="35%" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldPassword1" runat="server" ControlToValidate="txtMatKhauMoi"
                                ErrorMessage="Bạn phải nhập mật khẩu mới">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr id="rMatKhauMoi2" runat="server">
                        <td class="caption">
                            Nhập lại mật khẩu mới</td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtNhapLaiMatKhau" runat="server" MaxLength="255" TextMode="Password"
                                Width="35%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldPassword2" runat="server" ControlToValidate="txtNhapLaiMatKhau"
                                ErrorMessage="Bạn phải nhập lại mật khẩu mới">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="caption">
                        </td>
                        <td align="left" valign="top" style="text-align: left;">
                            <asp:Button ID="btnCapNhat" runat="server" OnClick="btnCapNhat_Click" Text="Cập nhật"
                                Width="80px" />
                            <asp:Button ID="btnBoQua" runat="server" CausesValidation="False" OnClick="btnBoQua_Click"
                                Text="Bỏ qua" Width="80px" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
