<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="CB_ThamDinhHoSo.aspx.cs" Inherits="WebUI_CB_ThamDinhHoSo" Title="Thẩm định sản phẩm"
    Theme="default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <span style="font-family: Arial"><strong>CÔNG BỐ&gt;&gt;<asp:HyperLink ID="hlDanhSachHoSo"
            runat="server"></asp:HyperLink></strong><strong>&gt;&gt;
                <asp:HyperLink ID="hlDanhSachSanPham" runat="server"></asp:HyperLink></strong><strong>&gt;&gt;
                    THẨM ĐỊNH</strong></span></div>
    <table id="Table2" style="width: 100%">
        <tr>
            <td align="right" colspan="4" style="width: 20%; height: auto">
                <fieldset style="width: 98%">
                    <legend>Thông tin sản phẩm&nbsp;</legend>
                    <table align="center" border="0" width="100%">
                        <tr>
                            <td align="right" style="width: 49px;">
                            </td>
                            <td align="right" style="height: 10px; width: 119px;">
                            </td>
                            <td colspan="1" style="height: 10px">
                            </td>
                            <td colspan="1" style="height: 10px; width: 120px;">
                            </td>
                            <td colspan="1" style="height: 10px; width: 120px;">
                            </td>
                            <td colspan="1" style="height: 21px; width: 150px; text-indent: 20px; text-align: left;">
                            </td>
                            <td style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 49px; height: 21px;">
                            </td>
                            <td align="right" style="width: 119px; height: 21px; text-align: left">
                                Tên sản phẩm:</td>
                            <td colspan="5" style="height: 21px; text-align: left">
                                <asp:Label ID="lblTenSanPham" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 49px; height: 21px;">
                            </td>
                            <td align="right" style="width: 119px; height: 21px; text-align: left">
                                Tên sản phẩm TA:</td>
                            <td colspan="1" style="height: 21px; text-align: left">
                                <asp:Label ID="lblTenSanPhamTA" runat="server" Font-Bold="True"></asp:Label></td>
                            <td align="right" style="width: 120px; height: 21px; text-align: left">
                                Số GCN/Tự đánh giá:</td>
                            <td colspan="1" style="width: 120px; height: 21px; text-align: left">
                                <asp:Label ID="lblSoGCN_TuDanhGia" runat="server" Font-Bold="True"></asp:Label></td>
                            <td align="right" style="width: 150px; text-indent: 20px; height: 21px; text-align: left">
                                Tiêu chuẩn áp dụng:</td>
                            <td style="text-align: left; vertical-align: top;" rowspan="3">
                                <asp:Label ID="lblTCAdung" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>

                        <tr>
                            <td align="right" style="width: 49px;">
                            </td>
                            <td colspan="1" style="width: 119px; height: 18px; text-align: left">
                                Ký hiệu:</td>
                            <td style="height: 18px; text-align: left">
                                <asp:Label ID="lblKyHieu" runat="server" Font-Bold="True"></asp:Label></td>
                            <td colspan="1" style="width: 120px; height: 18px; text-align: left">
                                Ngày GCN/đánh giá:</td>
                            <td colspan="1" style="width: 120px; height: 18px; text-align: left">
                                <asp:Label ID="lblNgayDanhGia" runat="server" Font-Bold="True"></asp:Label></td>
                            <td colspan="1" style="width: 150px; text-indent: 20px; height: 21px; text-align: left">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 49px;">
                            </td>
                            <td colspan="1" style="width: 119px; height: 18px; text-align: left">
                                Ngày nhận:</td>
                            <td colspan="1" style="height: 18px; text-align: left">
                                <asp:Label ID="lblNgaynhan" runat="server" Font-Bold="True"></asp:Label></td>
                            <td colspan="1" style="width: 120px; height: 18px; text-align: left">
                                Số bản công bố:</td>
                            <td style="width: 120px; height: 18px; text-align: left">
                                <asp:Label ID="lblSoBanCongBo" runat="server" Font-Bold="True"></asp:Label></td>
                            <td colspan="1" style="width: 150px; text-indent: 20px; height: 21px; text-align: left">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 49px">
                            </td>
                            <td colspan="1" style="width: 119px; height: 21px; text-align: left">
                                Hãng sản xuất:</td>
                            <td style="height: 21px; text-align: left">
                                <asp:Label ID="lblhangsx" runat="server" Font-Bold="True"></asp:Label></td>
                            <td colspan="1" style="width: 120px; height: 21px; text-align: left">
                                Ngày công bố:</td>
                            <td style="width: 120px; height: 21px; text-align: left">
                                <asp:Label ID="lblNgayCongBo" runat="server" Font-Bold="True"></asp:Label></td>
                            <td colspan="1" style="width: 150px; text-indent: 20px; height: 21px; text-align: left">
                                Trạng thái:</td>
                            <td style="height: 18px; text-align: left">
                                <asp:Label ID="lblTrangThai" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 49px;" valign="top">
                            </td>
                            <td align="right" style="width: 119px; height: auto; text-align: left" valign="top">
                                Tài liệu:</td>
                            <td colspan="5" style="height: auto; text-align: left">
                                <asp:LinkButton ID="lbtnBAN_CONG_BO" runat="server"></asp:LinkButton>
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="lbtnGIAY_TO_TU_CACH_PHAP_NHAN" runat="server"></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="lbtnTAI_LIEU_KY_THUAT" runat="server"></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="lbtnKET_QUA_DO_KIEM" runat="server"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnQUY_TRINH_SAN_XUAT" runat="server"></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="lbtnBAN_TU_DANH_GIA" runat="server"></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="lbtnCONG_VAN" runat="server"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnTIEU_CHUAN_TU_NGUYEN_AP_DUNG" runat="server"></asp:LinkButton></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td id="TD_ThongTinXuLyHoSo" runat="server" align="right" colspan="4" style="height: auto">
                <fieldset style="width: 98%">
                    <legend>Thông tin xử lý hồ sơ</legend>
                    <table align="center" border="0" width="100%">
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; height: 65px; text-align: left">
                                Chuyên viên tiếp nhận</td>
                            <td colspan="5" style="height: 65px; text-align: left">
                                <asp:TextBox ID="txtChuyenVienTiepNhan" runat="server" Rows="7" TextMode="MultiLine"
                                    Width="85%" ReadOnly="True" Height="50px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: left; height: 60px;">
                                Ý kiến của lãnh đạo</td>
                            <td colspan="5" style="height: 60px; text-align: left">
                                <asp:TextBox ID="txtYKienLanhDao" runat="server" Height="50px" Rows="2" TextMode="MultiLine"
                                    Width="85%" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: left; height: 60px;">
                                Chuyên viên xử lý</td>
                            <td colspan="5" style="height: 60px; text-align: left">
                                <asp:TextBox ID="txtChuyenVienXuLy" runat="server" Height="50px" Rows="2" TextMode="MultiLine"
                                    Width="85%" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <%--LongHH--%>
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: left;">
                                Số đo kiểm</td>
                            <td colspan="5" style="text-align: left">
                                <asp:TextBox ID="txtSoDoKiem" runat="server" Width="85%" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: left;">
                                Ngày đo kiểm</td>
                            <td colspan="5" style="text-align: left">
                                <asp:TextBox ID="txtSoNgayDoKiem" runat="server" Width="85%" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: right">
                            </td>
                            <td colspan="5" style="text-align: left" valign="top">
                                
                            </td>
                        </tr>
                        <%--LongHH--%>
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: left;">
                                Cơ quan đo kiểm</td>
                            <td colspan="5" style="text-align: left">
                                <asp:TextBox ID="txtCoQuanDoKiem" runat="server" Width="85%" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: right">
                            </td>
                            <td colspan="5" style="text-align: left" valign="top">
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: left;">
                                Tên tiếng anh CQĐK</td>
                            <td colspan="5" style="text-align: left">
                                <asp:TextBox ID="txtTenTiengAnhCQDK" runat="server" Width="85%" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: right">
                            </td>
                            <td colspan="5" style="text-align: left" valign="top">
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: left;">
                                Địa chỉ CQĐK</td>
                            <td colspan="5" style="text-align: left">
                                <asp:TextBox ID="txtDiaChiCQDK" runat="server" Width="85%" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: right">
                            </td>
                            <td colspan="5" style="text-align: left" valign="top">
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: left;">
                                Số điện thoại CQĐK</td>
                            <td colspan="5" style="text-align: left">
                                <asp:TextBox ID="txtSoDienThoaiCQDK" runat="server" Width="85%" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr id="tr1" runat ="server">
                            <td align="left" style="width: 20%;" class="caption">
                                File đính kèm</td>
                            <td colspan="5" style="height: 13px; text-align: left;">
                                <asp:LinkButton ID="lbtnFileDinhKem" runat="server"></asp:LinkButton>&nbsp;
                            </td>
                        </tr>
                        <%--LongHH--%>
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: right">
                            </td>
                            <td colspan="5" style="text-align: left" valign="top">
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: left; height: 109px;">
                                Kết quả đo kiểm</td>
                            <td colspan="5" style="height: 109px; text-align: left">
                                <asp:TextBox ID="txtKetQuaDoKiem" runat="server" Height="99px" Rows="5" TextMode="MultiLine"
                                    Width="85%" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: right">
                            </td>
                            <td colspan="5" style="text-align: left" valign="top">
                                &nbsp; &nbsp;
                            </td>
                        </tr>
                        <%--LongHH--%>
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: right">
                            </td>
                            <td colspan="5" style="text-align: left" valign="top">
                                &nbsp; &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: left">
                                Kết luận</td>
                            <td colspan="5" style="text-align: left" valign="top">
                                <asp:Label ID="lblKetLuan" runat="server" Font-Bold="True" Width="568px"></asp:Label></td>
                        </tr>
                        <tr id="trCongVan" runat="server">
                            <td align="right" style="vertical-align: top; width: 20%; text-align: left">
                            </td>
                            <td colspan="5" style="text-align: left" valign="top">
                                <asp:HyperLink ID="lnkCongVan" runat="server">Công văn trả lời</asp:HyperLink></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td id="TD1" runat="server" align="right" colspan="4" style="height: auto">
                <fieldset style="width: 98%">
                    <legend>Thẩm định hồ sơ</legend>
                    <table style="width: 100%">
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: left;" valign="top">
                                Thẩm định</td>
                            <td colspan="3" style="height: auto; text-align: left" valign="top">
                                <asp:RadioButtonList ID="rdblThamDinh" runat="server" RepeatDirection="Horizontal" onclick="InsertYKien();">
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rdblThamDinh"
                                    Display="Dynamic" ErrorMessage="Phải chọn nhất trí hay không nhất trí hồ sơ !"
                                    SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%; vertical-align: top; text-align: left;" valign="top">
                                Ý kiến thẩm định</td>
                            <td colspan="3" style="height: 26px; text-align: left" valign="top">
                                <asp:TextBox ID="txtYKienThamDinh" runat="server" Height="50px" Rows="2" TextMode="MultiLine"
                                    Width="85%"></asp:TextBox>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%; vertical-align: top; text-align: right; height: 28px;">
                            </td>
                            <td colspan="3" style="height: 28px; text-align: left">
                                <asp:Button ID="btnCapNhat" runat="server" OnClick="btnCapNhat_Click" Style="text-align: center"
                                    Text="Cập nhật" Width="85px" />&nbsp;<asp:Button ID="btnInPhieuDanhGia" runat="server"
                                        OnClick="btnInPhieuDanhGia_Click" OnClientClick="CheckConfirm('Bạn có muốn cập nhật Nội dung xử lý và Nội dung đánh giá trước khi in phiếu đánh giá?');"
                                        Text="In phiếu đánh giá" Width="150px" CausesValidation="False" />
                                <asp:Button ID="btnBoQua" runat="server" OnClick="btnBoQua_Click" Text="Bỏ qua" Width="85px"
                                    CausesValidation="False" /></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
     // Đặt giá trị cho ô text ý kiến phê duyệt
    function InsertYKien()
    {
        controlYKienPheDuyet = document.getElementById("<%= rdblThamDinh.ClientID%>");
        txtYKien = document.getElementById("<%= txtYKienThamDinh.ClientID%>");
        var inputsYKien = controlYKienPheDuyet.getElementsByTagName("input");  
        
        // neu GĐ phe duyet
        if(inputsYKien[0].checked)
            txtYKien.value = "Nhất trí.";
        else
            txtYKien.value = "Không nhất trí.";
    }
    </script>

</asp:Content>
