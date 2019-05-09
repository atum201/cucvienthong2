<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true" CodeFile="NhapLieu_CB_HoSoChitiet.aspx.cs" Inherits="WebUI_NhapLieu_CB_HoSoChitiet" Title="Nhập hồ sơ công bố" %>
<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="Table2" style="width: 100%; padding-left: 8px;">
        <tr>
            <td align="left" colspan="5" style="text-align: left; padding-top: 10px">
                <strong>HỒ SƠ CÔNG BỐ</strong></td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" />
                <asp:HiddenField ID="hidHoSoID" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left" style="text-align: left; height: 26px;" width="150px">
                Số hồ sơ</td>
            <td style="width: 465px; height: 26px;">
                <asp:TextBox ID="txtSoHoSo" runat="server" Width="90%" BackColor="Transparent"></asp:TextBox></td>
            <td>
                Hình thức tiếp nhận</td>
            <td nowrap="nowrap">
                <asp:RadioButtonList ID="rdgNhanTu" runat="server" RepeatDirection="Horizontal" TabIndex="4">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr><td align="left" style="text-align: left;">
                Đơn vị nộp hồ sơ</td>
            <td colspan="" style="height: auto; text-align: left; width: 465px;">
                <div style="float: left">
                    <cc1:ComboBox ID="ddlDonVi" runat="server" Width="274px" AutoPostBack="True" OnSelectedIndexChanged="ddlDonVi_SelectedIndexChanged"
                        TabIndex="5">
                    </cc1:ComboBox>
                </div>
                <asp:LinkButton ID="lbtTaoMoiDV" runat="server" OnClientClick="popCenter('DM_DonVi_ChiTiet.aspx','DM_DonVi_ChiTiet',800,400);return false;"
                    TabIndex="6">Thêm đơn vị</asp:LinkButton></td>
            <td align="left" style="text-align: left;">
                Người tiếp nhận</td>
            <td colspan="" style="height: auto; text-align: left; width: 465px;">
                <cc1:ComboBox ID="cboNguoiTiepNhan" runat="server" Width="286px">
                </cc1:ComboBox></td>
        </tr>
        <tr>
            <td align="left" style="text-align: left">
                Người nộp hồ sơ (*)</td>
            <td colspan="" style="text-align: left; width: 465px;">
                <asp:TextBox ID="txtNguoiNopHoSo" runat="server" Width="90%" MaxLength="255" TabIndex="6"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNguoiNopHoSo"
                    Display="Dynamic" ErrorMessage="Bạn phải nhập Nguời nộp hồ sơ">*</asp:RequiredFieldValidator></td>
            <td>
                        Người xử lý</td>
            <td colspan="" style="text-align: left; width: 465px;">
                <cc1:ComboBox ID="cboNguoiXuLy" runat="server" Width="286px">
                </cc1:ComboBox></td>
        </tr>
        <tr>
            <td align="left" style="text-align: left;">
                Ngày nộp hồ sơ (*)</td>
            <td style="text-align: left; width: 465px;">
                <asp:TextBox ID="txtNgayNhan" runat="server" Width="30%" TabIndex="3"></asp:TextBox>&nbsp;
                <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtNgayNhan" Separator="/"
                    Format="dd mm yyyy" InvalidDateMessage="Nhập sai định dạng ngày tháng" ShowErrorMessage="False"
                    ControlFocusOnError="true" RequiredDate="true" RequiredDateMessage="Bạn phải nhập ngày nhận hồ sơ" />
                (dd/mm/yyyy)
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNgayNhan"
                    Display="Dynamic" ErrorMessage="Ngày tiếp nhận hồ sơ không đúng" ValidationExpression="^(((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}|\d))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}|\d))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}|\d))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00|[048])))$">*</asp:RegularExpressionValidator></td>
            <td>
                Người thẩm định</td>
            <td>
                <cc1:ComboBox ID="cboNguoiThamDinh" runat="server" Width="286px">
                </cc1:ComboBox></td>
        </tr>
        <tr>
            <td>
                Điện thoại liên hệ (*)</td>
            <td>
                <asp:TextBox ID="txtDienThoai" runat="server" Width="90%" MaxLength="20" TabIndex="7"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDienThoai"
                    ErrorMessage="Bạn phải nhập điện thoại liên hệ">*</asp:RequiredFieldValidator>
            </td>
            <td>
                </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Email liên hệ</td>
            <td style="text-align: left">
                <asp:TextBox ID="txtEmail" runat="server" Width="90%" MaxLength="255" TabIndex="8"></asp:TextBox><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Địa chỉ hòm thư không đúng định dạng" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td><td>
                    </td>
            <td style="text-align: left">
            </td>
        </tr>
        <tr>
            <td align="left" style="text-align: left; vertical-align: top; height: 64px;">
                Sản phẩm</td>
            <td colspan="" style="text-align: left; vertical-align: top; width: 465px; height: 64px;">
                <asp:TextBox ID="txtSanPham" runat="server" Width="90%" TextMode="MultiLine" Rows="4"
                    TabIndex="10" /></td>
            <td align="left" style="text-align: left; vertical-align: top; height: 64px;">
                Ký hiệu</td>
            <td colspan="" style="text-align: left; vertical-align: top; width: 465px; height: 64px;
                vertical-align: top" valign="top">
                <asp:TextBox ID="txtKyHieu" runat="server" Width="87%" TextMode="MultiLine" Rows="4"
                    CssClass="textKeyPad" TabIndex="11" /></td>
        </tr>
        <tr>
            <td style="vertical-align: top; text-align: left">
                Nguồn gốc (*)</td>
            <td colspan="3" style="text-align: left">
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rdgNguonGoc" runat="server" RepeatDirection="Horizontal"
                                TabIndex="13" RepeatColumns="2" />
                        </td>
                        <td style="width: 20px; text-align: right;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rdgNguonGoc"
                                Display="Dynamic" ErrorMessage="Bạn phải chọn Nguồn gốc">*</asp:RequiredFieldValidator></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                Hồ sơ gồm</td>
            <td colspan="3">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 7%; text-align: left">
                            <asp:CheckBox ID="chkTL_BanCongBo" runat="server" TabIndex="14" Text=" Bản công bố hợp quy" /></td>
                        <td style="width: 22%; text-align: left">
                            <asp:CheckBox ID="chkTl_BanSaoGiayChungNhanHQ" runat="server" TabIndex="18" Text="Bản sao Giấy chứng nhận hợp quy" /></td>
                    </tr>
                    <tr>
                        <td style="width: 7%; text-align: left">
                            <asp:CheckBox ID="chkTl_BanTuDanhGia" runat="server" TabIndex="17" Text="Bản Tự đánh giá hợp quy" /></td>
                        <td style="width: 22%; text-align: left">
                            <asp:CheckBox ID="chkTl_KetQuaDoKiem" runat="server" TabIndex="18" Text="Kết quả đo kiểm " /></td>
                    </tr>
                    <tr>
                        <td style="width: 7%; height: 20px; text-align: left">
                            <asp:CheckBox ID="chkTL_TuCachPN" runat="server" TabIndex="15" Text="Giấy tờ về tư cách pháp nhân" /></td>
                        <td style="width: 22%; height: 20px; text-align: left">
                            <asp:CheckBox ID="chkTL_TaiLieuKT" runat="server" TabIndex="16" Text="Tài liệu kỹ thuật " /></td>
                    </tr>
                    <tr>
                        <td style="width: 7%; text-align: left">
                            <asp:CheckBox ID="chkTl_MauDau" runat="server" TabIndex="14" Text="Mẫu dấu hợp quy" /></td>
                        <td style="width: 22%; text-align: left">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="color: #000000">
            <td align="left" style="text-align: left;" valign="top">
                Tài liệu khác</td>
            <td style="text-align: left; width: 465px;">
                <asp:TextBox ID="txtTaiLieuKhac" runat="server" Rows="5" TextMode="MultiLine" Width="90%"
                    TabIndex="21"></asp:TextBox></td>
            <td style="vertical-align: top;">
                Ý kiến CVTN</td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtLuuY" runat="server" Width="90%" TextMode="MultiLine" Rows="5"
                    TabIndex="22"></asp:TextBox></td>
        </tr>
        <tr id="Nhanxet" runat="server">
            <td style="width: 14%; text-align: left" align="left">
                </td>
            <td style="width: 32%; text-align: left">
                </td>
            <td style="width: 22%; text-align: left">
            </td>
            <td style="width: 32%">
            </td>
        </tr>
        <tr>
            <td style="text-align: left" align="left">
                &nbsp;</td>
            <td style="text-align: left;" colspan="3">
                <asp:Button ID="btnCapNhat" runat="server" OnClick="btnCapNhat_Click" Style="text-align: center"
                    Text="Thêm mới" Width="85px" TabIndex="24" />&nbsp;
                <asp:Button ID="btnHoSoChiTiet" runat="server" TabIndex="27" Text="Hồ Sơ Chi Tiết"
                    OnClick="btnHoSoChiTiet_Click" Visible="false" />
                <asp:Button ID="btnBoQua" runat="server" OnClick="btnBoQua_Click" Text="Bỏ qua" Width="85px"
                    CausesValidation="False" TabIndex="26" />
            </td>
        </tr>
    </table>   

</asp:Content>


