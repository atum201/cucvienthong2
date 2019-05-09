<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="CN_PhanCongXulyThamDinh.aspx.cs" Inherits="WebUI_CN_PhanCongXulyThamDinh"
    Theme="Default" %>
<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div >
        <span style="font-family: Arial; font-weight:bold;">CHỨNG NHẬN &gt;&gt;<a href="../WebUI/CN_HoSo_QuanLy.aspx?direct=CN_HoSoMoi"> HỒ SƠ MỚI NHẬN</a> &gt;&gt;
            <% string SoHoSo = Request.QueryString["SoHoSo"];%>
            PHÂN CÔNG</span>
    </div>
    
    <table id="Table2" style="width: 100%">
        <tr>
            <td style="width: 100%; height: auto; text-align: left">
                <fieldset style="width: 98%">
                    <legend>Thông tin hồ sơ</legend>
                    <div style="text-align: left">
                        <table align="center" border="0" width="100%">
                            <tr>
                                <td align="right" style="width: 40px;">
                                </td>
                                <td align="right" style="width: 113px; height: 21px; text-align: left">
                                    Đơn vị nộp:</td>
                                <td colspan="5" style="height: 21px; text-align: left">
                                    <asp:Label ID="lblDonVi" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40px;">
                                </td>
                                <td align="right" style="width: 113px; height: 21px; text-align: left">
                                    Số hồ sơ:</td>
                                <td colspan="1" style="width: 19%; height: 21px; text-align: left">
                                    <asp:Label ID="lblSoHoSo" runat="server" Font-Bold="True"></asp:Label></td>
                                <td colspan="1" style="width: 70px; height: 18px; text-align: left">
                                    Người nộp:</td>
                                <td colspan="1" style="width: 15%; height: 18px; text-align: left">
                                    
                                        <asp:Label ID="lblNguoiNopHoSo" runat="server" Font-Bold="True"></asp:Label></td>
                                <td colspan="1" style="width: 130px;">
                                    Loại hình chứng nhận:</td>
                                <td style="width: 25%; height: 21px; text-align: left">
                                    <asp:Label ID="lblLoaiHinhChungNhan" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40px;">
                                </td>
                                <td align="right" style="width: 113px; height: 18px; text-align: left">
                                    Người tiếp nhận</td>
                                <td colspan="1" style="width: 19%; height: 18px; text-align: left">
                                    <asp:Label ID="lblNguoiTiepNhan" runat="server" Font-Bold="True"></asp:Label></td>
                                <td colspan="1" style="width: 70px; height: 21px; text-align: left">
                                    Điện thoại:</td>
                                <td colspan="1" style="width: 15%; height: 21px; text-align: left">
                                    <asp:Label ID="lblDienThoai" runat="server" Font-Bold="True"></asp:Label></td><td colspan="1" style="width: 130px;">
                                        Hình thức tiếp nhận:</td>
                                <td style="width: 25%; height: 18px; text-align: left">
                                    <asp:Label ID="lblHinhThucTiepNhan" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40px;">
                                </td>
                                <td align="right" style="width: 113px; height: 18px; text-align: left">
                                    Ngày tiếp nhận:</td>
                                <td colspan="1" style="width: 19%; height: 18px; text-align: left">
                                    <asp:Label ID="lblNgayNhan" runat="server" Font-Bold="True"></asp:Label></td>
                                <td colspan="1" style="width: 70px; height: 18px; text-align: left">
                                    Email:</td>
                                <td colspan="1" style="width: 15%; height: 18px; text-align: left">
                                    <asp:Label ID="lblEmail" runat="server" Font-Bold="True"></asp:Label></td>
                                <td colspan="1" style="width: 130px;">
                                    Trạng thái:</td>
                                <td style="width: 25%; height: 18px; text-align: left">
                                    <asp:Label ID="lblTrangThai" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40px;">
                                </td>
                                <td colspan="1" style="width: 70px; height: 18px; text-align: left">
                                    Số công văn đến:</td>
                                <td colspan="1" style="width: 15%; height: 18px; text-align: left">
                                    <asp:Label ID="lblSoCVden" runat="server" Font-Bold="True"></asp:Label></td><td colspan="1" style="width: 70px; height: 18px; text-align: left">
                                    </td>
                                <td colspan="1" style="width: 15%; height: 18px; text-align: left">
                                </td>
                                <td colspan="1" style="width: 130px;">
                                    Nguồn gốc:</td>
                                <td style="width: 25%; height: 18px; text-align: left">
                                    <asp:Label ID="lblNguonGoc" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40px;">
                                </td>
                                <td align="right" style="width: 113px; height: auto; text-align: left">
                                    Danh sách sản phẩm</td>
                                <td colspan="5" style="height: auto; text-align: left">
                                    <asp:Label ID="lblSanPham" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40px; height: 21px">
                                </td>
                                <td align="right" style="width: 113px; height: 21px; text-align: left">
                                    Ký hiệu</td>
                                <td colspan="5" style="height: 21px; text-align: left">
                                    <asp:Label ID="lblKyHieu" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                </fieldset>
                <br />
            </td>
        </tr>
        <tr>
            <td id="TD_ThongTinXuLyHoSo" runat="server" align="right" colspan="4" style="height: auto"><table id="Table1" style="width: 100%">
                <tr>
                    <td style="width: 100%; height: auto; text-align: left">
                        <fieldset style="width: 98%">
                            <legend>Nội dung xử lý của chuyên viên tiếp nhận</legend>
                            <div style="text-align: left">
                                <table align="center" border="0" width="100%">
                                    <tr>
                                        <td align="right" style="width: 20%; height: auto; text-align: right; vertical-align: top; text-indent: 40pt;">
                                            Sản phẩm</td>
                                        <td colspan="1" style="width: 126px; height: 18px; text-align: left">
                                            <asp:TextBox ID="txtSanPham" runat="server" Height="100px" ReadOnly="True" Rows="2"
                                                TextMode="MultiLine" Width="600px">Nokia N71, Nokia N72, Nokia N73</asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 20%; height: auto; text-align: right; vertical-align: top; text-indent: 40pt;">
                                            Ký hiệu</td>
                                        <td colspan="1" style="width: 126px; height: 18px; text-align: left">
                                            <asp:TextBox ID="txtKyHieu" runat="server" Height="100px" ReadOnly="True" Rows="2"
                                                TextMode="MultiLine" Width="600px">Nokia N71, Nokia N72, Nokia N73</asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 20%; height: auto; text-align: right; vertical-align: top; text-indent: 40pt;">
                                            Nội dung xử lý</td>
                                        <td colspan="1" style="width: 126px; height: 18px; text-align: left">
                                            <asp:TextBox ID="txtXuLy" runat="server" Height="100px" ReadOnly="True" Rows="2"
                                                TextMode="MultiLine" Width="600px">Hồ sơ đ&#227; đầy đủ t&#224;i liệu</asp:TextBox></td>
                                    </tr>
                                </table>
                            </div>
                        </fieldset>
                        <br />
                        </td>
                </tr>
            </table>
            </td>
        </tr>
        <tr>
            <td runat="server" align="right" colspan="4" style="height: auto" id="Td2">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 100%; height: auto; text-align: left" valign="top">
                            <fieldset style="width: 98%">
                                <legend>Danh sách sản phẩm</legend>
                                <div style="text-align: left">
                                    &nbsp;</div>
                                <asp:GridView ID="gvSanPham" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    EmptyDataText="Không có dữ liệu !" PageSize="15" Width="100%" OnPageIndexChanging="gvSanPham_PageIndexChanging">
                                    <Columns>
                                           <asp:TemplateField HeaderText="T&#234;n sản phẩm">
                                            <ItemStyle Font-Underline="False" />
                                            <ItemTemplate>
                                                    <a href="#" onclick="return popCenter('CN_SanPhamChiTiet.aspx?SanPhamID=<%# Server.UrlEncode(Eval("SanPhamID").ToString()) %>','CN_SanPhamChiTiet',570,400);"><%# Eval("TenTiengViet")%></a>
                                                </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="KyHieu" HeaderText="K&#253; hiệu" />
                                        <asp:BoundField DataField="MaNhom" HeaderText="M&#227; nh&#243;m SP" />
                                        <asp:BoundField DataField="TenTieuChuan" HeaderText="T.Chuẩn &#225;p dụng" />
                                    </Columns>
                                </asp:GridView>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="TD1" runat="server" align="right" colspan="4" style="height: auto">
                <fieldset style="width: 98%">
                    <legend>Phân công xử lý, thẩm định</legend>
                    <table style="width: 100%">
                        <tr>
                            <td align="right" colspan="4" style="width: 20%; vertical-align: top; text-align: right;">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%; vertical-align: top; text-align: right;">
                                Chuyên viên xử lý</td>
                            <td colspan="3" style="text-align: left; width: 80%;">
                                <asp:DropDownList ID="cboChuyenVienXuLy" runat="server" Width="50%" IsSupportedBrowser="True">
                                   
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="cboChuyenVienThamDinh"
                                    ControlToValidate="cboChuyenVienXuLy" ErrorMessage="Chuyên viên xử lý phải khác chuyên viên thẩm định"
                                    Operator="NotEqual">*</asp:CompareValidator>
                                </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%; vertical-align: top; text-align: right;">
                                Chuyên viên thẩm định</td>
                            <td colspan="3" style="text-align: left; width: 80%;">
                                <asp:DropDownList ID="cboChuyenVienThamDinh" runat="server" Width="50%" IsSupportedBrowser="False">
                                    
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%; vertical-align: top; text-align: right;">
                                Ý kiến của lãnh đạo</td>
                            <td colspan="3" style="text-align: left; width: 80%;">
                                <asp:TextBox ID="txtYKien" runat="server" Height="100px" Rows="2" TextMode="MultiLine"
                                    Width="600px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%; vertical-align: top; text-align: right;">
                            </td>
                            <td colspan="3" style="text-align: left; width: 80%;">
                                <asp:Button ID="btnCapNhat" runat="server" OnClick="btnCapNhat_Click" Style="text-align: center"
                                    Text="Cập nhật" Width="85px" /><asp:Button ID="btnBoQua" runat="server" OnClick="btnBoQua_Click"
                                            Text="Bỏ qua" Width="85px" CausesValidation="False" /></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
