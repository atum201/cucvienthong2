<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="CN_PheDuyetHoSo.aspx.cs" Inherits="WebUI_CN_PheDuyetHoSo" Theme="Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <span style="font-family: Arial"><strong>CHỨNG NHẬN &gt;&gt;</strong><a href="../WebUI/CN_HoSo_QuanLy.aspx?&UserControl=<%=Request.QueryString["UserControl"].ToString()%>"><strong>DANH
            SÁCH HỒ SƠ
            <% if (Request.QueryString["UserControl"].ToString() == "CN_HoSoDen") { Response.Write("MỚI NHẬN"); } else { Response.Write("ĐÃ GỬI"); }%>
        </strong></a><strong>&gt;&gt;
            <% string SoHoSo = Request.QueryString["HoSoID"];%>
        </strong><a href="../WebUI/CN_HoSoSanPham_QuanLy.aspx?HoSoID=<%=Request.QueryString["HoSoID"]%>&UserControl=<%=Request.QueryString["UserControl"].ToString()%>">
            <strong>DANH SÁCH HỒ SƠ SẢN PHẨM
                <% if (Request.QueryString["UserControl"].ToString() == "CN_HoSoDen") { Response.Write("MỚI NHẬN"); } else { Response.Write("ĐÃ GỬI"); }%>
            </strong></a><strong>>> PHÊ DUYỆT</strong></span><strong> </strong>
    </div>
    <table id="Table2" style="width: 100%">
        <tr>
            <td align="right" colspan="4" style="width: 20%; height: auto">
                <table id="Table1" style="width: 100%">
                    <tr>
                        <td align="right" colspan="4" style="width: 20%; height: auto">
                            <fieldset style="width: 98%">
                                <legend>Thông tin sản phẩm</legend>
                                <table align="center" border="0" width="100%">
                                    <tr>
                                        <td align="right" style="width: 50px;">
                                        </td>
                                        <td align="right" style="height: 10px; width: 140px;">
                                        </td>
                                        <td colspan="1" style="height: 10px">
                                        </td>
                                        <td colspan="1" style="height: 18px; width: 10%; text-indent: 20px; text-align: left;">
                                        </td>
                                        <td colspan="1" style="height: 10px">
                                        </td>
                                        <td colspan="1" style="height: 10px">
                                        </td>
                                        <td style="height: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 50px;">
                                        </td>
                                        <td align="right" style="width: 140px; height: 18px; text-align: left">
                                            Tên sản phẩm:</td>
                                        <td colspan="1" style="height: 18px; text-align: left">
                                            <asp:Label ID="lblTenSanPham" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td colspan="1" style="width: 10%; height: 18px; text-align: left; text-indent: 20px;">
                                            Trạng thái:</td>
                                        <td colspan="1" style="width: 20%; height: 18px; text-align: left">
                                            <asp:Label ID="lblTrangThai" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td colspan="1" style="width: 10%; height: 18px; text-align: left">
                                            Ký hiệu:</td>
                                        <td style="width: 25%; height: 18px; text-align: left">
                                            <asp:Label ID="lblKyHieu" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 50px;">
                                        </td>
                                        <td align="right" style="width: 140px; height: 18px; text-align: left">
                                            Mã nhóm sản phẩm:</td>
                                        <td colspan="1" style="height: 18px; text-align: left">
                                            <asp:Label ID="lblManhomSP" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td colspan="1" style="width: 10%; height: 18px; text-align: left; text-indent: 20px;">
                                            Ngày nhận:</td>
                                        <td colspan="1" style="width: 20%; height: 18px; text-align: left">
                                            <asp:Label ID="lblNgaynhan" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td colspan="1" style="width: 10%; height: 18px; text-align: left">
                                            Hãng sản xuất:</td>
                                        <td style="width: 25%; height: 18px; text-align: left">
                                            <asp:Label ID="lblhangsx" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 50px;">
                                        </td>
                                        <td align="right" style="width: 140px; height: 18px; text-align: left">
                                            Tiêu chuẩn áp dụng:</td>
                                        <td colspan="1" style="height: 18px; text-align: left">
                                            <asp:Label ID="lblTCAdung" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td colspan="1" style="width: 10%; height: 18px; text-align: left; text-indent: 20px;">
                                            Lệ phí</td>
                                        <td colspan="1" style="width: 20%; height: 18px; text-align: left">
                                            <asp:Label ID="lblLePhi" runat="server" Font-Bold="True"></asp:Label>
                                            VNĐ</td>
                                        <td colspan="1" style="width: 10%; height: 18px; text-align: left">
                                            Thời hạn GCN</td>
                                        <td style="width: 25%; height: 18px; text-align: left">
                                            <asp:Label ID="lblThoiHanGCN" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 50px;" valign="top">
                                        </td>
                                        <td align="right" style="width: 140px; height: auto; text-align: left" valign="top">
                                            Tài liệu:</td>
                                        <td colspan="5" style="height: auto; text-align: left">
                                            <asp:LinkButton ID="lbtnDON_DE_NGHI_CN" runat="server"></asp:LinkButton><asp:LinkButton
                                                ID="lbtnGIAY_TO_TU_CACH_PHAP_NHAN" runat="server"></asp:LinkButton>
                                            <asp:LinkButton ID="lbtnTAI_LIEU_KY_THUAT" runat="server"></asp:LinkButton><asp:LinkButton
                                                ID="lbtnKET_QUA_DO_KIEM" runat="server"></asp:LinkButton>
                                            <asp:LinkButton ID="lbtnQUY_TRINH_SAN_XUAT" runat="server"></asp:LinkButton>
                                            <asp:LinkButton ID="lbtnQUY_TRINH_CHAT_LUONG" runat="server"></asp:LinkButton><asp:LinkButton
                                                ID="lbtnCHUNG_CHI_HE_THONG_QLCL" runat="server"></asp:LinkButton>
                                            <asp:LinkButton ID="lbtnTIEU_CHUAN_TU_NGUYEN_AP_DUNG" runat="server"></asp:LinkButton></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 50px;">
                                        </td>
                                        <td align="right" style="width: 140px; height: 18px; text-align: right">
                                        </td>
                                        <td colspan="5" style="width: 20%; height: 18px; text-align: left">
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right" style="height: auto;" colspan="4">
                <fieldset style="width: 98%;">
                    <legend>Thông tin xử lý hồ sơ</legend>
                    <table width="100%" border="0" align="center">
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: right">
                                Chuyên viên tiếp nhận</td>
                            <td colspan="5" style="height: 50px; text-align: left">
                                <asp:TextBox ID="txtNoiDungXuLyCV1" runat="server" Rows="7" TextMode="MultiLine"
                                    Width="85%" Height="77px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%; vertical-align: top; text-align: right; height: 60px;">
                                Chuyên viên thẩm định</td>
                            <td colspan="5" style="height: 60px; text-align: left">
                                <asp:TextBox ID="txtNoiDungXuLyCV2" runat="server" Rows="7" TextMode="MultiLine"
                                    Width="85%" Height="70px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%; vertical-align: top; text-align: right;">
                                Ý kiến của lãnh đạo</td>
                            <td colspan="5" style="height: 50px; text-align: left">
                                <asp:TextBox ID="txtYKienChiDao" runat="server" Height="86px" Rows="2" TextMode="MultiLine"
                                    Width="85%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%; vertical-align: top; text-align: right;">
                                Chuyên viên xử lý</td>
                            <td colspan="5" style="height: 50px; text-align: left">
                                <asp:TextBox ID="txtGhiChuCV1" runat="server" Height="74px" Rows="2" TextMode="MultiLine"
                                    Width="85%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: right">
                            </td>
                            <td colspan="5" style="text-align: left" valign="top">
                                &nbsp; &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: right">
                                Kết luận</td>
                            <td colspan="5" style="text-align: left" valign="top">
                                <asp:Label ID="lblKetLuan" runat="server" Font-Bold="True" Width="96px"></asp:Label></td>
                        </tr>
                        <tr id="trCongVan" runat="server">
                            <td align="right" style="vertical-align: top; width: 20%; text-align: left">
                            </td>
                            <td colspan="5" style="text-align: left" valign="top">
                                <asp:HyperLink ID="lnkCongVan" runat="server">Công văn trả lời</asp:HyperLink></td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top; width: 20%; text-align: right">
                            </td>
                            <td colspan="5" style="height: auto; text-align: left">
                                <asp:LinkButton ID="lnkPhieuDanhGia" runat="server" Visible="False">Phiếu đánh giá hồ sơ</asp:LinkButton></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td align="right" style="height: auto;" colspan="4">
                <fieldset style="width: 98%;">
                    <legend>Phê duyệt hồ sơ</legend>
                    <table width="100%" border="0" align="center">
                        <tr>
                            <td align="right" style="height: auto; width: 20%;">
                            </td>
                            <td colspan="5" style="height: 21px; text-align: left">
                                <asp:RadioButtonList ID="grdlPheDuyet" runat="server" RepeatDirection="Horizontal"  onclick="InsertYKien();">
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="grdlPheDuyet"
                                    Display="Dynamic" ErrorMessage="Phải chọn phê duyệt hay không phê duyệt hồ sơ !"
                                    SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%; vertical-align: top; text-align: right; height: auto;">
                                Ý kiến phê duyệt</td>
                            <td colspan="5" style="height: 50px; text-align: left">
                                <asp:TextBox ID="txtYKienPheDuyet" runat="server" Height="132px" Rows="2" TextMode="MultiLine"
                                    Width="85%"></asp:TextBox>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td align="right" style="height: auto; text-align: right; width: 15%;">
            </td>
            <td align="left" style="height: auto; text-align: left;" colspan="3">
                <asp:Button ID="btnCapNhat" runat="server" Style="text-align: center" Text="Cập nhật"
                    OnClick="btnCapNhat_Click" />&nbsp;<asp:Button ID="btnInPhieuDanhGia" runat="server"
                        OnClick="btnInPhieuDanhGia_Click" OnClientClick="CheckConfirm('Bạn có muốn cập nhật Nội dung xử lý và Nội dung đánh giá trước khi in phiếu đánh giá?');"
                        Text="In phiếu đánh giá" Width="150px" CausesValidation="False" />
                <asp:Button ID="btnInGiayChungNhan" runat="server" Style="text-align: center" Text="In giấy chứng nhận"
                    OnClick="btnInGiayChungNhan_Click" Width="132px" CausesValidation="False" />
                <asp:Button ID="btnBoQua" runat="server" OnClick="btnBoQua_Click" Text="Bỏ qua" Width="82px"
                    CausesValidation="False" />
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 15%; height: auto; text-align: right">
            </td>
            <td align="left" colspan="3" style="height: auto; text-align: left">
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 20%; height: auto">
            </td>
            <td style="height: auto; width: 30%;">
            </td>
            <td style="height: auto; vertical-align: top; width: 15%; text-align: right;">
            </td>
        </tr>
    </table>

    <script type="text/javascript">
    // Đặt giá trị cho ô text ý kiến phê duyệt
    function InsertYKien()
    {
        controlYKienPheDuyet = document.getElementById("<%= grdlPheDuyet.ClientID%>");
        txtYKien = document.getElementById("<%= txtYKienPheDuyet.ClientID%>");
        var inputsYKien = controlYKienPheDuyet.getElementsByTagName("input");  
        
        // neu GĐ phe duyet
        if(inputsYKien[0].checked)
            txtYKien.value = "Nhất trí.";
        else
            txtYKien.value = "Không nhất trí.";
    }
    </script>

</asp:Content>
