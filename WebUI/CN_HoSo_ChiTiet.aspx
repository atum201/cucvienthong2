<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="CN_HoSo_ChiTiet.aspx.cs" Inherits="WebUI_CN_HoSo_ChiTiet" Theme="Default" %>

<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>

<%--<script type="text/javascript">
    $().ready(function () {
        $("#<%=ddlDonVi.ClientID %>").change(function () { 
            alert($(this).val())
        })
    })
</script>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--LongHH--%>
    <style type="text/css">
        .dhx_combo_box{
            width:100% !important;
        }
        .dhx_combo_list{
            height:550px;
        }
    </style>
    <%--LongHH--%>
    <table id="Table2" style="width: 100%; padding-left: 8px;">
        <tr>
            <td align="left" colspan="5" style="text-align: left; padding-top: 10px">
                <strong>HỒ SƠ CHỨNG NHẬN</strong></td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" />
                <asp:HiddenField ID="hidHoSoID" runat="server" />
            </td>
        </tr>
        <tr id="trLoaiHoSo" runat="server">
            <td align="left" style="height: auto;" width="150">
                Loại hồ sơ</td>
            <td style="height: auto;">
                <asp:CheckBox ID="chkHoSoMoi" runat="server" Text="Hồ sơ mới nhận" onclick="ChangeSoHoSo()"
                    Checked="True" /></td>
            <td>
            </td>
            <td nowrap="nowrap">
            </td>
        </tr>
        <tr>
            <td style="height: 26px" width="210px">
                Trạng thái</td>
            <td style="height: 26px">
                <asp:TextBox ID="txtTrangThai" runat="server" ReadOnly="True" Width="90%" BackColor="#FFFFC0"></asp:TextBox></td>
            <td style="height: 26px" width="210px">
            </td>
            <td style="height: 26px">
            </td>
        </tr>
        <%--LongHH--%>
        <tr>
            <td align="left" style="text-align: left; height: 26px;" width="150px">
                Số hồ sơ</td>
            <td style="width: 465px; height: 26px;">
                <asp:TextBox ID="txtSoHoSo" runat="server" Width="90%" BackColor="#FFFFC0"></asp:TextBox></td>
            <td align="left" style="text-align: left;">
                Đơn vị nộp hồ sơ</td>
            <td colspan="" style="height: auto; text-align: left; width: 465px;">
                <div style="float: left; width:80%">
                    <cc1:ComboBox ID="ddlDonVi" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlDonVi_SelectedIndexChanged"
                        TabIndex="5">
                    </cc1:ComboBox>
                </div>
                <asp:LinkButton ID="lbtTaoMoiDV" runat="server" OnClientClick="popCenter('DM_DonVi_ChiTiet.aspx','DM_DonVi_ChiTiet',800,400);return false;"
                    TabIndex="6">Thêm đơn vị</asp:LinkButton></td>
        </tr>
        <tr>
            <td>
                Số công văn đơn vị</td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtSoCongVanDonVi" runat="server" Width="90%" MaxLength="50" TabIndex="2"></asp:TextBox></td>
            <td align="left" style="text-align: left">
                Tên tiếng anh</td>
            <td colspan="" style="text-align: left; width: 465px;">
                <asp:TextBox ID="txtTenTiengAnh" runat="server" Width="90%" MaxLength="255" TabIndex="6"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td>
                Người nộp hồ sơ (*)</td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtNguoiNopHoSo" runat="server" Width="90%" MaxLength="255" TabIndex="6"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNguoiNopHoSo"
                    Display="Dynamic" ErrorMessage="Bạn phải nhập Nguời nộp hồ sơ">*</asp:RequiredFieldValidator></td>
            <td align="left" style="text-align: left">
                Địa chỉ</td>
            <td colspan="" style="text-align: left; width: 465px;">
                <asp:TextBox ID="txtDiaChi" runat="server" Width="90%" MaxLength="255" TabIndex="6"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td align="left" style="text-align: left;">
                Người tiếp nhận</td>
            <td colspan="" style="text-align: left; width: 465px;">
                <asp:TextBox ID="txtNguoiTiepNhan" runat="server" Width="90%" ReadOnly="True" BackColor="#FFFFC0"></asp:TextBox></td>
            <td>
                Điện thoại liên hệ (*)</td>
            <td>
                <asp:TextBox ID="txtDienThoai" runat="server" Width="90%" MaxLength="20" TabIndex="7"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDienThoai"
                    ErrorMessage="Bạn phải nhập điện thoại liên hệ">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="left" style="text-align: left;">
                Ngày tiếp nhận (*)</td>
            <td style="text-align: left; width: 465px;">
                <asp:TextBox ID="txtNgayNhan" runat="server" Width="70%" TabIndex="3"></asp:TextBox>&nbsp;
                <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtNgayNhan" Separator="/"
                    Format="dd mm yyyy" InvalidDateMessage="Nhập sai định dạng ngày tháng" ShowErrorMessage="False"
                    ControlFocusOnError="true" RequiredDate="true" RequiredDateMessage="Bạn phải nhập ngày nhận hồ sơ" />
                (dd/mm/yyyy)
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNgayNhan"
                    Display="Dynamic" ErrorMessage="Ngày tiếp nhận hồ sơ không đúng" ValidationExpression="^(((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}|\d))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}|\d))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}|\d))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00|[048])))$">*</asp:RegularExpressionValidator></td>
            <td>
                Email liên hệ</td>
            <td style="text-align: left">
                <asp:TextBox ID="txtEmail" runat="server" Width="90%" MaxLength="255" TabIndex="8"></asp:TextBox><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Địa chỉ hòm thư không đúng định dạng" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td>
                Hình thức tiếp nhận</td>
            <td nowrap="nowrap">
                <asp:RadioButtonList ID="rdgNhanTu" runat="server" RepeatDirection="Horizontal" TabIndex="4" />
            </td>
            <td>
                Mã số thuế</td>
            <td>
                <asp:TextBox ID="txtMaSoThue" runat="server" Width="90%" MaxLength="20" TabIndex="7"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>
                Loại hình chứng nhận (*)</td>
            <td>
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rdgLoaiHinhChungNhan" runat="server" RepeatDirection="Horizontal"
                                TabIndex="9">
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rdgLoaiHinhChungNhan"
                                Display="Dynamic" ErrorMessage="Bạn phải chọn Loại hình chứng nhận">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <%-- Code Cũ
        <tr>
            <td align="left" style="text-align: left; height: 26px;" width="150px">
                Số hồ sơ</td>
            <td style="width: 465px; height: 26px;">
                <asp:TextBox ID="txtSoHoSo" runat="server" Width="90%" BackColor="#FFFFC0"></asp:TextBox></td>
            <td align="left" style="text-align: left;">
                Đơn vị nộp hồ sơ</td>
            <td colspan="" style="height: auto; text-align: left; width: 465px;">
                <div style="float: left">
                    <cc1:ComboBox ID="ddlDonVi" runat="server" Width="95%" AutoPostBack="True" OnSelectedIndexChanged="ddlDonVi_SelectedIndexChanged"
                        TabIndex="5">
                    </cc1:ComboBox>
                </div>
                <asp:LinkButton ID="lbtTaoMoiDV" runat="server" OnClientClick="popCenter('DM_DonVi_ChiTiet.aspx','DM_DonVi_ChiTiet',800,400);return false;"
                    TabIndex="6">Thêm đơn vị</asp:LinkButton></td>
        </tr>
        <tr>
            <td>
                Số công văn đơn vị</td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtSoCongVanDonVi" runat="server" Width="90%" MaxLength="50" TabIndex="2"></asp:TextBox></td>
            <td align="left" style="text-align: left">
                Người nộp hồ sơ (*)</td>
            <td colspan="" style="text-align: left; width: 465px;">
                <asp:TextBox ID="txtNguoiNopHoSo" runat="server" Width="90%" MaxLength="255" TabIndex="6"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNguoiNopHoSo"
                    Display="Dynamic" ErrorMessage="Bạn phải nhập Nguời nộp hồ sơ">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td align="left" style="text-align: left;">
                Người tiếp nhận</td>
            <td colspan="" style="text-align: left; width: 465px;">
                <asp:TextBox ID="txtNguoiTiepNhan" runat="server" Width="90%" ReadOnly="True" BackColor="#FFFFC0"></asp:TextBox></td>
            <td>
                Điện thoại liên hệ (*)</td>
            <td>
                <asp:TextBox ID="txtDienThoai" runat="server" Width="90%" MaxLength="20" TabIndex="7"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDienThoai"
                    ErrorMessage="Bạn phải nhập điện thoại liên hệ">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="left" style="text-align: left;">
                Ngày tiếp nhận (*)</td>
            <td style="text-align: left; width: 465px;">
                <asp:TextBox ID="txtNgayNhan" runat="server" Width="30%" TabIndex="3"></asp:TextBox>&nbsp;
                <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtNgayNhan" Separator="/"
                    Format="dd mm yyyy" InvalidDateMessage="Nhập sai định dạng ngày tháng" ShowErrorMessage="False"
                    ControlFocusOnError="true" RequiredDate="true" RequiredDateMessage="Bạn phải nhập ngày nhận hồ sơ" />
                (dd/mm/yyyy)
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNgayNhan"
                    Display="Dynamic" ErrorMessage="Ngày tiếp nhận hồ sơ không đúng" ValidationExpression="^(((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}|\d))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}|\d))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}|\d))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00|[048])))$">*</asp:RegularExpressionValidator></td>
            <td>
                Email liên hệ</td>
            <td style="text-align: left">
                <asp:TextBox ID="txtEmail" runat="server" Width="90%" MaxLength="255" TabIndex="8"></asp:TextBox><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Địa chỉ hòm thư không đúng định dạng" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td>
                Hình thức tiếp nhận</td>
            <td nowrap="nowrap">
                <asp:RadioButtonList ID="rdgNhanTu" runat="server" RepeatDirection="Horizontal" TabIndex="4" />
            </td>
            <td>
                Loại hình chứng nhận (*)</td>
            <td>
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rdgLoaiHinhChungNhan" runat="server" RepeatDirection="Horizontal"
                                TabIndex="9">
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rdgLoaiHinhChungNhan"
                                Display="Dynamic" ErrorMessage="Bạn phải chọn Loại hình chứng nhận">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
        <%--LongHH--%>
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
                        <td style="width: 225px; height: 20px;">
                            <asp:CheckBox ID="chkDon" runat="server" Text=" Đơn đề nghị chứng nhận  " TabIndex="13">
                            </asp:CheckBox></td>
                        <td style="height: 20px">
                            <asp:CheckBox ID="chkQuyTrinhSX" runat="server" Text="Quy trình sản xuất" TabIndex="17" /></td>
                    </tr>
                    <tr>
                        <td style="width: 225px; height: 20px;">
                            <asp:CheckBox ID="chkGiayTo" runat="server" Text="Giấy tờ về tư cách pháp nhân" TabIndex="14" /></td>
                        <td style="height: 20px">
                            <asp:CheckBox ID="chkQuyTrinhCL" runat="server" Text="Quy trình đảm bảo chất lượng"
                                TabIndex="18" /></td>
                    </tr>
                    <tr>
                        <td style="width: 225px; height: 19px;">
                            <asp:CheckBox ID="chkTaiLieu" runat="server" Text="Tài liệu kỹ thuật " TabIndex="15" /></td>
                        <td style="height: 19px">
                            <asp:CheckBox ID="chkChungChi" runat="server" Text="Chứng chỉ hệ thống quản lý chất lượng"
                                TabIndex="19" /></td>
                    </tr>
                    <tr>
                        <td style="width: 225px; height: 20px;">
                            <asp:CheckBox ID="chkKetQua" runat="server" Text="Kết quả đo kiểm" TabIndex="16" /></td>
                        <td style="height: 20px">
                            <asp:CheckBox ID="chkTieuChuan" runat="server" Text="Tiêu chuẩn tự nguyện áp dụng"
                                TabIndex="20" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="color: #000000">
            <td align="left" style="text-align: left;" valign="top">
                Tài liệu khác</td>
            <td style="text-align: left; width: 465px;">
                <asp:TextBox ID="txtTaiLieuKhac" runat="server" Rows="3" TextMode="MultiLine" Width="90%"
                    TabIndex="21"></asp:TextBox></td>
            <td style="vertical-align: top;">
                Ý kiến CVTN</td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtLuuY" runat="server" Width="90%" TextMode="MultiLine" Rows="3"
                    TabIndex="22"></asp:TextBox></td>
        </tr>
        <tr id="Nhanxet" runat="server">
            <td style="width: 14%; text-align: left" align="left">
                Hồ sơ đã đầy đủ</td>
            <td style="width: 32%; text-align: left">
                &nbsp;<asp:RadioButtonList ID="grlHoSoDayDu" runat="server" RepeatDirection="Horizontal"
                    TabIndex="23">
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
        <%--LongHH--%>
        <tr style="color: #000000" id="RowNguoiKy" runat="server">
            <td align="left" style="text-align: left;" valign="top">
                Người ký giấy báo phí</td>
            <td style="text-align: left; width: 465px;">
                <asp:DropDownList ID="ddlNguoiKy" runat="server" Width="90%" TabIndex="24"></asp:DropDownList></td>
            <td style="vertical-align: top;">
                Thẩm quyền</td>
            <td style="text-align: left;">
                <asp:DropDownList ID="ddlThamQuyen" runat="server" Width="90%" TabIndex="25"></asp:DropDownList></td>
        </tr>
        <tr style="color: #000000" id="RowSoLuong" runat="server">
            <td align="left" style="text-align: left;" valign="top">
                Số lượng tiếp nhận</td>
            <td style="text-align: left; width: 465px;">
                <asp:TextBox ID="txtSLTiepNhan" runat="server" Width="90%"  class="sltnchange" 
                    TabIndex="26"></asp:TextBox></td>
            <td style="vertical-align: top;">
                Tổng phí</td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtTongPhi" runat="server" Width="90%" TabIndex="27">
                </asp:TextBox></td>
        </tr>
        <%--LongHH--%>
        <tr>
            <td style="text-align: left" align="left">
                &nbsp;</td>
            <td style="text-align: left;" colspan="3">
                <asp:Button ID="btnCapNhat" runat="server" OnClick="btnCapNhat_Click" Style="text-align: center"
                    Text="Thêm mới" Width="85px" TabIndex="28" />
                <asp:Button ID="btnInPhieuNhan" runat="server" Text="In phiếu tiếp nhận" Width="133px"
                    OnClick="btnInPhieuNhan_Click" TabIndex="29" />
                <asp:Button ID="btnInGiayBaoPhi" runat="server" Text="In giấy báo phí" Width="133px"
                    OnClick="btnInGiayBaoPhi_Click" TabIndex="30" />
                <asp:Button ID="btnHoSoChiTiet" runat="server" TabIndex="31" Text="Hồ Sơ Chi Tiết"
                    OnClick="btnHoSoChiTiet_Click" Visible="false" />
                <asp:Button ID="btnCopy" runat="server" Text="Thêm hồ sơ mới" Width="145px" CausesValidation="False"
                    TabIndex="32" OnClick="btnCopy_Click" />
                <asp:Button ID="btnBoQua" runat="server" OnClick="btnBoQua_Click" Text="Bỏ qua" Width="85px"
                    CausesValidation="False" TabIndex="33" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfDonGiaTiepNhan" runat="server" />
    <asp:HiddenField ID="hfDonGiaXemXet" runat="server" />
    <script type="text/javascript" src="../Js/jquery-1.3.2-vsdoc2.js"></script>

    <script type="text/javascript" src="../Js/jquery.keypad.js"></script>

    <script type="text/javascript" src="../Js/Common.js"></script>

    <script type="text/javascript" src="../Js/DataGridCheckBoxAll.js"></script>
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
            }
            else
            {
                txtSoHoSo.readOnly = false;
                txtSoHoSo.value = '';
                txtSoHoSo.style.backgroundColor = '#ffffff';                
            }
        }
        $(document).ready(function () {
            $("#<%=ddlNguoiKy.ClientID%>").change(function () {
                $("#<%=ddlThamQuyen.ClientID%>").val($(this).val())
            })
            TinhTongPhi();
            $(".sltnchange").change(TinhTongPhi);
        })

        
        function TinhTongPhi() {
            var txtTongPhi = GetControlByName("txtTongPhi");
            var sl = GetControlByName("txtSLTiepNhan").val();
            var tn = parseInt(GetControlByName("hfDonGiaTiepNhan").val());
            var xx = parseInt(GetControlByName("hfDonGiaXemXet").val());
            var tongphi = parseInt(sl) * (tn + xx)*1000;
            txtTongPhi.val(formatCurrency(tongphi));
        }
         
   
    </script>

</asp:Content>
