<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="CB_HoSo_ChiTiet.aspx.cs" Inherits="WebUI_CB_HoSo_ChiTiet" Theme="Default" %>

<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--LongHH--%>
    <style type="text/css">
        .dhx_combo_box{
            width:100% !important;
        }
        .mr10{
            margin-right:10px;
        }
        .dhx_combo_list{
            height:550px;
        }
    </style>
    <%--LongHH--%>
    <table id="Table2" style="width: 100%">
        <tr>
            <td align="left" colspan="5" style="width: 14%; text-align: left">
                <strong>HỒ SƠ CÔNG BỐ </strong>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="5" style="width: 14%; text-align: left;">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" />
                <asp:HiddenField ID="hidHoSoID" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left" style="height: auto;">
            </td>
            <td style="height: auto;">
                <asp:CheckBox ID="chkHoSoMoi" runat="server" Checked="True" onclick="ChangeSoHoSo()"
                    Text="Hồ sơ mới nhận" /></td>
            <td style="height: auto;">
            </td>
            <td style="height: auto;">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 14%; text-align: left;">
                Số hồ sơ</td>
            <td style="text-align: left; width: 32%;">
                <asp:TextBox ID="txtSoHoSo" runat="server" Width="70%" BackColor="#FFFFC0" TabIndex="1">ABDQ3H/TCN</asp:TextBox></td>
            <td style="width: 22%; text-align: left">
                Trạng thái</td>
            <td style="width: 32%">
                <asp:TextBox ID="txtTrangThai" runat="server" ReadOnly="True" Width="70%" BackColor="#FFFFC0"
                    TabIndex="7">Hồ sơ mới</asp:TextBox></td>
        </tr>
        <tr>
            <td align="left" style="width: 14%; text-align: left">
                Người tiếp nhận</td>
            <td style="text-align: left; width: 32%;">
                <asp:TextBox ID="txtNguoiTiepNhan" runat="server" Width="70%" BackColor="#FFFFC0"
                    ReadOnly="True" TabIndex="2">Nguyễn Văn A</asp:TextBox></td>
            <td style="width: 22%; text-align: left">
                Hình thức tiếp nhận</td>
            <td style="width: 32%">
                <asp:RadioButtonList ID="rdgNhanTu" runat="server" RepeatDirection="Horizontal" TabIndex="4">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td align="left" style="width: 14%; text-align: left">
                Ngày tiếp nhận (*)</td>
            <td style="text-align: left; width: 32%;">
                <asp:TextBox ID="txtNgayNhan" runat="server" Width="70%" BorderColor="Transparent"
                    TabIndex="3"></asp:TextBox>
                <rjs:PopCalendar ID="calendarFrom" runat="server" Control="txtNgayNhan" ForeColor="#FFFFC0"
                    ScriptsValidators="No Validate" Separator="/" ShowErrorMessage="False"></rjs:PopCalendar>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNgayNhan"
                    ErrorMessage="Ngày tiếp nhận không đúng định dạng DD/MM/YYYY" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                    Display="Dynamic">*</asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNgayNhan"
                    Display="Dynamic" ErrorMessage="Nhập ngày nộp hồ sơ !">*</asp:RequiredFieldValidator>(dd/mm/yyyy)</td>
            <td style="width: 22%; text-align: left">
                Số công văn đơn vị</td>
            <td style="width: 32%">
                <asp:TextBox ID="txtSoCongVanDonVi" runat="server" Width="70%" BackColor="White"
                    TabIndex="9"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="left" style="width: 14%; text-align: left;">
                Đơn vị nộp hồ sơ</td>
            <td style="text-align: left; width: 32%;">
                <div style="float: left;width:70%">
                    <cc1:ComboBox ID="ddlDonVi" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlDonVi_SelectedIndexChanged"
                        TabIndex="4">
                    </cc1:ComboBox>
                </div>
                <%-- LongHH Comment
                    <asp:LinkButton ID="lnkbtnThemDonVi" runat="server" OnClientClick="popCenter('DM_DonVi_ChiTiet.aspx','DM_DonVi_ChiTiet',800,400); return false;"
                    CausesValidation="False" TabIndex="5">Thêm Đơn Vị</asp:LinkButton>--%></td>
            <td style="width: 22%; text-align: left">
                Điện thoại liên hệ (*)</td>
            <td style="width: 32%">
                <asp:TextBox ID="txtDienThoai" runat="server" Width="70%" TabIndex="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDienThoai"
                    ErrorMessage="Bạn phải nhập điện thoại liên hệ">*</asp:RequiredFieldValidator></td>
        </tr>
        <%--LongHH start--%>
        <tr>
            <td align="left" style="width: 14%; text-align: left;">
                </td>
            <td style="text-align: left; width: 32%;">
                <asp:LinkButton ID="lnkbtnThemDonVi" runat="server" OnClientClick="popCenter('DM_DonVi_ChiTiet.aspx','DM_DonVi_ChiTiet',800,400); return false;"
                    CausesValidation="False" TabIndex="5">Thêm Đơn Vị</asp:LinkButton></td>
            <td style="width: 22%; text-align: left">
                Địa chỉ</td>
            <td style="width: 32%">
                <asp:TextBox ID="txtDiaChi" runat="server" Width="70%" TabIndex="10"></asp:TextBox></td>
        </tr>

        <tr>
            <td align="left" style="width: 14%; text-align: left;">
                Tên tiếng anh</td>
            <td style="text-align: left; width: 32%;">
                <asp:TextBox ID="txtTenTiengAnh" runat="server" Width="70%" TabIndex="10"></asp:TextBox></td>
            <td style="width: 22%; text-align: left">
                Mã số thuế</td>
            <td style="width: 32%">
                <asp:TextBox ID="txtMaSoThue" runat="server" Width="70%" TabIndex="10"></asp:TextBox></td>
        </tr>
        <%--LongHH end--%>
        <tr>
            <td align="left" style="width: 14%; text-align: left">
                Người nộp hồ sơ (*)</td>
            <td style="text-align: left; width: 32%;">
                <asp:TextBox ID="txtNguoiNopHoSo" runat="server" Width="70%" TabIndex="6"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNguoiNopHoSo"
                    Display="Dynamic" ErrorMessage="Bạn phải nhập Nguời nộp hồ sơ">*</asp:RequiredFieldValidator></td>
            <td style="width: 22%; text-align: left">
                Email liên hệ</td>
            <td style="width: 32%">
                <asp:TextBox ID="txtEmail" runat="server" Width="70%" TabIndex="11"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Địa chỉ hòm thư không đúng định dạng" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="left" style="width: 14%; text-align: left; vertical-align: top;">
                Sản phẩm</td>
            <td style="text-align: left; width: 32%;">
                <asp:TextBox ID="txtSanPham" runat="server" Width="95%" Rows="3" TextMode="MultiLine"
                    TabIndex="12"></asp:TextBox></td>
            <td align="left" style="width: 14%; text-align: left; vertical-align: top;">
                Ký hiệu</td>
            <td style="text-align: left; width: 32%;">
                <asp:TextBox ID="txtKyHieu" runat="server" Rows="3" TextMode="MultiLine" Width="92%"
                    CssClass="textKeyPad" TabIndex="13"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="vertical-align: top; text-align: left; height: 27px;">
                Nguồn gốc (*)</td>
            <td  style="text-align: left; height: 27px;">
                <asp:RadioButtonList ID="rdgNguonGoc" runat="server" RepeatDirection="Horizontal"
                    TabIndex="13" RepeatColumns="2" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rdgNguonGoc"
                    Display="Dynamic" ErrorMessage="Bạn phải chọn Nguồn gốc">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 14%; text-align: left;" valign="top">
                Hồ sơ gồm</td>
            <td style="text-align: left; width: 32%;">
                <asp:CheckBox ID="chkTL_BanCongBo" runat="server" Text="Bản công bố hợp quy" TabIndex="14" /></td>
            <td style="width: 22%; text-align: left">
                <asp:CheckBox ID="chkTl_BanSaoGiayChungNhanHQ" runat="server" Text="Bản sao Giấy chứng nhận hợp quy"
                    TabIndex="18" /></td>
            <td style="width: 32%">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 14%; text-align: left" valign="top">
            </td>
            <td style="width: 32%; text-align: left">
                <asp:CheckBox ID="chkTl_BanTuDanhGia" runat="server" Text="Bản Tự đánh giá hợp quy"
                    TabIndex="17" /></td>
            <td style="width: 22%; text-align: left">
                <asp:CheckBox ID="chkTl_KetQuaDoKiem" runat="server" Text="Kết quả đo kiểm " TabIndex="18" /></td>
            <td style="width: 32%">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 14%; text-align: left;" valign="top">
            </td>
            <td style="text-align: left; width: 32%;">
                <asp:CheckBox ID="chkTL_TuCachPN" runat="server" Text="Giấy tờ về tư cách pháp nhân"
                    TabIndex="15" /></td>
            <td style="width: 22%; text-align: left">
                <asp:CheckBox ID="chkTL_TaiLieuKT" runat="server" Text="Tài liệu kỹ thuật " TabIndex="16" /></td>
            <td style="width: 32%">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 14%; text-align: left" valign="top">
            </td>
            <td style="width: 32%; text-align: left">
                <asp:CheckBox ID="chkTl_MauDau" runat="server" Text="Mẫu dấu hợp quy" TabIndex="14" /></td>
            <td style="width: 22%; text-align: left">
            </td>
            <td style="width: 32%">
            </td>
        </tr>
        <tr>
            <td style="width: 14%; text-align: left; vertical-align: top;" align="left">
                Tài liệu khác</td>
            <td style="width: 32%; text-align: left">
                <asp:TextBox ID="txtTaiLieuKhac" runat="server" Rows="3" TextMode="MultiLine" Width="95%"
                    TabIndex="19">Trả kết quả sau 7 ngày làm việc</asp:TextBox></td>
            <td style="width: 14%; text-align: left; vertical-align: top;">
                Lưu ý</td>
            <td style="width: 32%">
                <asp:TextBox ID="txtLuuY" runat="server" Width="95%" Rows="3" TextMode="MultiLine"
                    TabIndex="20"></asp:TextBox></td>
        </tr>
        <tr id="Nhanxet" runat="server">
            <td style="width: 14%; text-align: left; vertical-align: middle;" align="left">
                Hồ sơ đã đầy đủ</td>
            <td style="width: 32%; text-align: left">
                &nbsp;<asp:RadioButtonList ID="grlHoSoDayDu" runat="server" RepeatDirection="Horizontal"
                    TabIndex="21">
                    <asp:ListItem Value="true">Đầy đủ</asp:ListItem>
                    <asp:ListItem Value="false">Chưa đầy đủ</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="RequireDanhGia" runat="server" ControlToValidate="grlHoSoDayDu"
                    Display="Dynamic" ErrorMessage="Chưa đánh giá hồ sơ đầy đủ hay không đầy đủ !">*</asp:RequiredFieldValidator></td>
            <td style="width: 22%; text-align: left">
            </td>
            <td style="width: 32%">
            </td>
        </tr>
        <tr>
            <td style="width: 14%; text-align: left;" align="left">
                &nbsp;</td>
            <td style="width: 32%; text-align: left;">
                &nbsp;</td>
            <td style="width: 22%; text-align: left">
            </td>
            <td style="width: 32%;">
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" style="width: 14%; text-align: left">
            </td>
            <td colspan="4" style="width: 32%; text-align: left">
                <asp:Button ID="btnCapNhat" runat="server" OnClick="btnCapNhat_Click" Style="text-align: center"
                    Text="Cập nhật" Width="85px" TabIndex="22" />
                <asp:Button ID="btnInPhieuNhan" runat="server" Text="In phiếu tiếp nhận hồ sơ" Width="173px"
                    OnClick="btnInPhieuNhan_Click" Visible="False" TabIndex="23" />&nbsp;<asp:Button
                        ID="btnCopy" runat="server" CausesValidation="False" OnClick="btnCopy_Click"
                        TabIndex="26" Text="Thêm hồ sơ mới" Width="145px" />
                <asp:Button ID="btnBoQua" runat="server" OnClick="btnBoQua_Click" Text="Bỏ qua" Width="85px"
                    CausesValidation="False" TabIndex="24" /></td>
        </tr>
    </table>

    <script type="text/javascript">
        function ChangeSoHoSo()
        {
            var chkHoSoMoi = document.getElementById("<%= chkHoSoMoi.ClientID %>");
            var txtSoHoSo = document.getElementById("<%= txtSoHoSo.ClientID %>");
            
            if(chkHoSoMoi.checked == true)
            {
                txtSoHoSo.style.backgroundColor = '#FFFFC0';
                txtSoHoSo.readOnly = true;
                txtSoHoSo.value = 'Số sinh tự động';
                
//                txtSoCongVanDen.style.backgroundColor = '#FFFFC0';
//                txtSoCongVanDen.readOnly = true;
//                txtSoCongVanDen.value = 'Số sinh tự động';
            }
            else
            {
                txtSoHoSo.readOnly = false;
                txtSoHoSo.value = '';
                txtSoHoSo.style.backgroundColor = '#ffffff';
                
//                txtSoCongVanDen.readOnly = false;
//                txtSoCongVanDen.value = '';
//                txtSoCongVanDen.style.backgroundColor = '#ffffff';
            }
        }
    
    </script>

</asp:Content>
