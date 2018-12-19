<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="HT_NguoiDung_TaoMoi.aspx.cs" Inherits="WebUI_HT_NguoiDung_TaoMoi" Title="Thêm mới người dùng"
    Theme="Default" %>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin: 10px auto 10px 10px;">
        <strong>HỆ THỐNG >> <a href="HT_NguoiDung_QuanLy.aspx" style="color:Blue">QUẢN LÝ NGƯỜI DÙNG</a> >>
            <asp:Label ID="lblTieuDe" runat="server" Font-Bold="True" Text="THÊM MỚI NGƯỜI DÙNG HỆ THỐNG"></asp:Label></strong>
    </div>
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td style="width: 15%;" class="caption">
                        </td>
                        <td style="width: 75%; text-align: left;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 15%;" class="caption">
                        </td>
                        <td style="width: 75%; text-align: left;">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="False" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%;" class="caption">
                            Tên trung tâm(*)</td>
                        <td style="width: 75%; text-align: left;">
                            <asp:DropDownList ID="ddlTenDonVi" runat="server" Width="41%">
                                <asp:ListItem>Trung t&#226;m Chứng nhận</asp:ListItem>
                                <asp:ListItem>Trung t&#226;m Kiểm định v&#224; Chứng nhận 2</asp:ListItem>
                                <asp:ListItem>Trung t&#226;m Kiểm định v&#224; Chứng nhận 3</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width: 15%;" class="caption">
                            Tên phòng ban(*)</td>
                        <td style="width: 75%; text-align: left;">
                            <asp:DropDownList ID="ddlTenPhongBan" runat="server" Width="41%">
                                <asp:ListItem>Chứng nhận</asp:ListItem>
                                <asp:ListItem>C&#244;ng bố</asp:ListItem>
                                <asp:ListItem>Kế to&#225;n</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width: 15%;" class="caption">
                            Chức vụ(*)</td>
                        <td style="width: 75%; text-align: left">
                            <asp:DropDownList ID="ddlChucVu" runat="server" Width="41%">
                                <asp:ListItem>Gi&#225;m đốc trung t&#226;m</asp:ListItem>
                                <asp:ListItem>Trưởng ph&#242;ng</asp:ListItem>
                                <asp:ListItem>Chuy&#234;n vi&#234;n</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="caption" style="width: 15%">
                        </td>
                        <td style="width: 75%; text-align: left">
                            <asp:CheckBox ID="chkTiepNhanHoSoQuaMang" runat="server" Text="Có trách nhiệm tiếp nhận hồ sơ qua mạng" OnCheckedChanged="chkThayDoiMatKhau_CheckedChanged" /></td>
                    </tr>
                    <tr>
                        <td style="width: 15%;" class="caption">
                            Tên truy cập (*)</td>
                        <td style="width: 75%; text-align: left;">
                            <asp:TextBox ID="txtTenDangNhap" runat="server" Width="40%" MaxLength="50"></asp:TextBox>&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldUserName" runat="server" ControlToValidate="txtTenDangNhap"
                                ErrorMessage="Bạn phải nhập tên đăng nhập">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr id="rMatKhau" runat="server">
                        <td class="caption">
                            Mật khẩu(*)</td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtMatKhau" runat="server" MaxLength="255" Width="40%" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqfieldPassWord" runat="server" ControlToValidate="txtMatKhau"
                                ErrorMessage="Bạn phải nhập mật khẩu">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr id="rThayDoiMatKhau" runat="server">
                        <td class="caption">
                        </td>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="chkThayDoiMatKhau" runat="server" Text="Thay đổi mật khẩu" OnCheckedChanged="chkThayDoiMatKhau_CheckedChanged"
                                AutoPostBack="True" /></td>
                    </tr>
                    <tr id="rMatKhauMoi1" runat="server">
                        <td class="caption" style="height: 26px">
                            Mật khẩu mới</td>
                        <td style="text-align: left; height: 26px;">
                            <asp:TextBox ID="txtMatKhauMoi" runat="server" MaxLength="255" Width="40%" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldPassword1" runat="server" ControlToValidate="txtMatKhauMoi"
                                ErrorMessage="Bạn phải nhập mật khẩu mới">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr id="rMatKhauMoi2" runat="server">
                        <td class="caption">
                            Nhập lại mật khẩu mới</td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtNhapLaiMatKhau" runat="server" MaxLength="255" TextMode="Password"
                                Width="40%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldPassword2" runat="server" ControlToValidate="txtNhapLaiMatKhau"
                                ErrorMessage="Bạn phải nhập lại mật khẩu mới">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 15%;" class="caption">
                            Họ và tên(*)</td>
                        <td style="width: 75%; text-align: left;">
                            <asp:TextBox ID="txtHoTen" runat="server" Width="40%" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldFullName" runat="server" ControlToValidate="txtHoTen"
                                ErrorMessage="Bạn phải nhập họ tên">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 15%;" class="caption">
                            Ngày sinh&nbsp;</td>
                        <td style="width: 75%; text-align: left;">
                            <asp:TextBox ID="txtNgaySinh" runat="server" Width="150px" MaxLength="15"></asp:TextBox>
                            <rjs:PopCalendar ID="PopCalendar" runat="server" Control="txtNgaySinh" Format="dd mm yyyy"
                                InvalidDateMessage="Nhập sai định dạng ngày tháng" MessageAlignment="RightCalendarControl"
                                Separator="/" ShowErrorMessage="False"></rjs:PopCalendar>
                            (dd/mm/yyyy)<asp:RangeValidator ID="rvdate" runat="server" ControlToValidate="txtNgaySinh"
                                ErrorMessage="Ngày sinh không được lớn hơn ngày hiện tại" Type="Date" MinimumValue="01/01/1900">*</asp:RangeValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 15%; height: 22px;" class="caption">
                            Giới tính&nbsp;</td>
                        <td style="width: 75%; text-align: left; height: 22px;">
                            <asp:RadioButton ID="rbNam" runat="server" Checked="true" GroupName="GioiTinh" Text="Nam" />&nbsp;
                            <asp:RadioButton ID="rbNu" runat="server" GroupName="GioiTinh" Text="Nữ" /></td>
                    </tr>
                    <tr>
                        <td style="width: 15%;" class="caption">
                            Quê quán</td>
                        <td style="width: 75%; text-align: left;">
                            <asp:TextBox ID="txtQueQuan" runat="server" Width="40%" MaxLength="255"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 15%;" class="caption">
                            Địa chỉ</td>
                        <td style="width: 75%; text-align: left;">
                            <asp:TextBox ID="txtDiaChi" runat="server" Width="40%" MaxLength="255"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 15%;" class="caption">
                            Số điện thoại(*)</td>
                        <td style="width: 75%; text-align: left;">
                            <asp:TextBox ID="txtDienThoai" runat="server" MaxLength="15" Width="40%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requirePhoneNumber" runat="server" ControlToValidate="txtDienThoai"
                                ErrorMessage="Bạn phải nhập số điện thoại liên hệ">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 15%;" class="caption">
                            Email</td>
                        <td style="width: 75%; text-align: left;">
                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" Width="40%"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionEmail" runat="server" ErrorMessage="Địa chỉ Email không hợp lệ"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail">*</asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 15%;" class="caption">
                        </td>
                        <td align="left" valign="top" style="width: 75%; text-align: left;">
                            <asp:Button ID="btnCapNhat" runat="server" OnClick="btnCapNhat_Click" Text="Cập nhật"
                                Width="80px" />
                            <asp:Button ID="btnBoQua" runat="server" CausesValidation="False" Text="Bỏ qua" Width="80px"
                                OnClick="btnBoQua_Click" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
