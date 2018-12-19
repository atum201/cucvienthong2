<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="CB_PhanCongXuLyThamDinh.aspx.cs" Inherits="WebUI_CB_PhanCongXulyThamDinh"
    Theme="Default" %>
<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style ="padding:10px;">
        <span style="font-family: Arial;font-weight:bold; padding:5px;"><strong>CÔNG BỐ &gt;&gt;</strong><a href="../WebUI/CN_HoSo_QuanLy.aspx?direct=CB_HoSoDaGui"><strong>
            DANH SÁCH HỒ SƠ </strong></a><strong>&gt;&gt; PHÂN CÔNG</strong></span><strong> </strong>
    </div>
    <table id="Table2" style="width: 100%">
        <tr>
            <td align="left" style="width: 96%;" valign="top">
                <fieldset style="width: 98%">
                    <legend>Thông tin hồ sơ</legend>
                    <div style="text-align: left">
                        <table align="center" border="0" width="100%">
                            <tr>
                                <td class="caption" style="width: 15%" valign="top">
                                    Đơn vị nộp HS:</td>
                                <td colspan="5" style="height: 21px; text-align: left" valign="top">
                                    <asp:Label ID="lblDonVi" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 15%;" class="caption" valign="top">
                                    Số hồ sơ:</td>
                                <td colspan="1" style="width: 19%; height: 21px; text-align: left" valign="top">
                                    <asp:Label ID="lblSoHoSo" runat="server" Font-Bold="True"></asp:Label></td>
                                <td colspan="1" style="width: 10%;" class="caption" valign="top">
                                    Người nộp HS:</td>
                                <td colspan="1" style="width: 20%; height: 21px; text-align: left" valign="top">
                                    &nbsp;<asp:Label ID="lblNguoiNopHoSo" runat="server" Font-Bold="True"></asp:Label></td>
                                <td colspan="1" style="width: 14%;" class="caption" valign="top">
                                    Số công văn đơn vị
                                </td>
                                <td style="width: 25%; height: 21px; text-align: left" valign="top">
                                    <asp:Label ID="lblSoCVDonVi" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 15%;" class="caption" valign="top">
                                    Người tiếp nhận</td>
                                <td colspan="1" style="width: 19%; height: auto; text-align: left; " valign="top">
                                    &nbsp;<asp:Label ID="lblNguoiTiepNhan" runat="server" Font-Bold="True"></asp:Label></td>
                                <td colspan="1" style="width: 10%;" class="caption" valign="top">
                                    Điện thoại:</td>
                                <td colspan="1" style="width: 20%; height: auto; text-align: left" valign="top">
                                    <asp:Label ID="lblDienThoai" runat="server" Font-Bold="True"></asp:Label></td>
                                <td colspan="1" style="width: 14%;" class="caption" valign="top">
                                    Trạng thái:</td>
                                <td style="width: 25%; height: auto; text-align: left" valign="top">
                                    <asp:Label ID="lblTrangThai" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 15%; height: 21px;" class="caption" valign="top">
                                    Ngày nhận:</td>
                                <td colspan="1" style="width: 19%; height: 21px; text-align: left" valign="top">
                                    <asp:Label ID="lblNgayNhan" runat="server" Font-Bold="True"></asp:Label></td>
                                <td colspan="1" style="width: 10%; height: 21px;" class="caption" valign="top">
                                    Email:</td>
                                <td colspan="1" style="width: 20%; height: 21px; text-align: left" valign="top">
                                    <asp:Label ID="lblEmail" runat="server" Font-Bold="True"></asp:Label></td>
                                <td colspan="1" style="width: 14%; height: 21px;" class="caption" valign="top">
                                    </td>
                                <td style="width: 25%; height: 21px; text-align: left" valign="top">
                                  
                            </tr>
                            <tr>
                                <td style="width: 15%; height: 21px;" class="caption" valign="top">
                                    Danh sách sản phẩm</td>
                                <td colspan="5" style="height: 21px; text-align: left" valign="top">
                                    <asp:Label ID="lblSanPham" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="caption" style="width: 15%; height: 21px" valign="top">
                                    Ký hiệu</td>
                                <td colspan="5" style="height: 21px; text-align: left" valign="top">
                                    <asp:Label ID="lblKyHieu" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td id="TD_ThongTinXuLyHoSo" runat="server" colspan="4" style="height: auto">
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
            <td style="width: 100%; height: auto; text-align: left">
                <fieldset style="width: 98%">
                    <legend>Nội dung xử lý của chuyên viên tiếp nhận</legend>
                    <div style="text-align: left">
                        <table align="center" border="0" width="100%">
                            <tr>
                                <td style="width: 15%; vertical-align: top; text-align: right;" class="caption">
                                    Sản phẩm</td>
                                <td colspan="1" style="width: 126px; height: 18px; text-align: left">
                                    <asp:TextBox ID="txtSanPham" runat="server" Height="100px" ReadOnly="True" Rows="2"
                                        TextMode="MultiLine" Width="600px">Nokia N71, Nokia N72, Nokia N73</asp:TextBox></td>
                            </tr>
                             <tr>
                                <td style="width: 15%; vertical-align: top; text-align: right;" class="caption">
                                    Ký hiệu</td>
                                <td colspan="1" style="width: 126px; height: 18px; text-align: left">
                                    <asp:TextBox ID="txtKyHieu" runat="server" Height="100px" ReadOnly="True" Rows="2"
                                        TextMode="MultiLine" Width="600px">Nokia N71, Nokia N72, Nokia N73</asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: top; text-align: right;" class="caption">
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
        <tr>
            <td>
            </td>
        </tr>
        
        <tr>
            <td id="TD1" runat="server" colspan="4" style="height: auto">
                <fieldset style="width: 98%">
                    <legend><strong>Phân công xử lý, thẩm định</strong></legend>
                    <table width="95%" border="0" style="left: 8px">
                        <tr>
                            <td align="right" colspan="4" style="width: 15%" class="caption">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 15%" class="caption">
                                Chuyên viên xử lý</td>
                            <td colspan="3" style="width: 80%; text-align: left">
                                <asp:DropDownList ID="cboChuyenVienXuLy" runat="server" Width="50%">
                                  
                                  
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="cboChuyenVienThamDinh"
                                    ControlToValidate="cboChuyenVienXuLy" ErrorMessage="Chuyên viên xử lý phải khác chuyên viên thẩm định"
                                    Operator="NotEqual">*</asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 15%" class="caption">
                                Chuyên viên thẩm định</td>
                            <td colspan="3" style="width: 80%; text-align: left">
                                <asp:DropDownList ID="cboChuyenVienThamDinh" runat="server" Width="50%">
                                    
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 15%;" class="caption">
                                Ý kiến chỉ đạo</td>
                            <td colspan="3" style="width: 80%; text-align: left">
                                <asp:TextBox ID="txtYKien" runat="server" Height="100px" Rows="2" TextMode="MultiLine"
                                    Width="80%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 15%" class="caption">
                            </td>
                            <td colspan="3" style="width: 80%; text-align: left">
                                <asp:Button ID="btnCapNhat" runat="server" OnClick="btnCapNhat_Click" Style="text-align: center"
                                    Text="Cập nhật" Width="85px" />
                                <input type="button" ID="btnBoQua" runat="server" Style="text-align: center" value="Bỏ qua" onclick="javascript:history.go(-1);"
                                    Width="80px" /></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
