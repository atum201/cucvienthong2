<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="NhapLieu_CB_SanPhamQuanLy.aspx.cs" Inherits="WebUI_NhapLieu_CB_SanPhamQuanLy" Theme="default" Title="Danh sách sản phẩm"%>

<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="Server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript">
    function ShowAction(isDisplay)
    {       
        var tdaction=$('#action');                
        if (!isDisplay)
            tdaction.hide();
        else
            tdaction.show();        
    }        
    </script>

    <div style="margin: 10px auto 10px 10px">
        <span style="font-family: Arial; font-weight: bold;">NHẬP HỒ SƠ CÔNG BỐ &gt;&gt<%--<a
            href="../WebUI/CB_HoSo_QuanLy.aspx?UserControl=CB_HoSoDen"> DANH SÁCH HỒ SƠ MỚI</a>--%>
            DANH SÁCH SẢN
            PHẨM</span>
    </div>
    <table cellpadding="5" cellspacing="0" width="100%" border="0">
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
                <fieldset style="width: 98%">
                    <legend>Thông tin hồ sơ</legend>
                    <div style="text-align: left">
                        <table align="center" border="0" width="100%" cellspacing="5">
                            <tr>
                                <td align="right" style="width: 120px; text-align: left" valign="top">
                                    Đơn vị nộp:</td>
                                <td colspan="5">
                                    <asp:Label ID="lblDonVi" runat="server" Width="529px" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" style="text-align: left; width: 120px;" valign="top">
                                    Số hồ sơ:</td>
                                <td colspan="1" style="width: auto;">
                                    <strong>
                                        <asp:Label ID="lblSoHoSo" runat="server"></asp:Label></strong></td>
                                <td colspan="1" style="width: 75px;">
                                    Người nộp:</td>
                                <td colspan="1" style="width: auto;">
                                    <strong>
                                        <asp:Label ID="lblNguoiNop" runat="server"></asp:Label></strong></td><td colspan="1" style="width: 88px;">
                                    Nguồn gốc:</td>
                                <td style="width: auto;">
                                    <strong>
                                        <asp:Label ID="lblNguonGoc" runat="server"></asp:Label></strong></td>
                            </tr>
                            <tr>
                                <td align="right" style="text-align: left; width: 120px;" valign="top">
                                    Người tiếp nhận</td>
                                <td colspan="1" style="width: auto;">
                                    <strong>
                                        <asp:Label ID="lblNguoiNhan" runat="server"></asp:Label></strong></td>
                                <td colspan="1" style="width: 75px;">
                                    Điện thoại:</td>
                                <td colspan="1" style="width: auto;">
                                    <strong>
                                        <asp:Label ID="lblDienthoai" runat="server"></asp:Label></strong></td><td colspan="1" style="width: 88px;">
                                        </td>
                                <td style="width: auto;">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="text-align: left; width: 120px;" valign="top">
                                    Ngày tiếp nhận:</td>
                                <td colspan="1" style="width: auto;">
                                    <strong>
                                        <asp:Label ID="lblNgayTiepNhan" runat="server"></asp:Label></strong></td>
                                <td colspan="1" style="width: 75px;">
                                    Email:</td>
                                <td colspan="1" style="width: auto;">
                                    <strong>
                                        <asp:Label ID="lblEmail" runat="server"></asp:Label></strong></td>
                                <td colspan="1" style="width: 88px;">
                                </td>
                                <td style="width: auto;">
                                </td>
                            </tr>
                        </table>
                    </div>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 96%;" valign="top">
                <fieldset style="width: 98%">
                    <legend>Danh sách sản phẩm</legend>
                    <table style="width: 100%;" id="action">
                        <tr>
                            <td style="text-align: right; height: 33px;">
                                <asp:ImageButton ID="ImgBtnThemmoi" runat="server" Height="17px" ImageUrl="~/htmls/image/new_f2.png"
                                    Width="17px" OnClick="ImgBtnThemmoi_Click" />
                                <asp:LinkButton ID="lnkThemMoi" runat="server" Font-Bold="False" OnClick="lnkThemMoi_Click">Thêm mới</asp:LinkButton>
                                &nbsp;
                                <asp:ImageButton ID="ImgBtnXoa" runat="server" Enabled="False" Height="16px" ImageUrl="~/htmls/image/cancel_f2.png"
                                    Width="16px" OnClick="ImgBtnXoa_Click" />
                                <asp:LinkButton ID="lnkXoa" runat="server" Font-Bold="False" OnClick="lnkXoa_Click"
                                    Enabled="False">Xóa</asp:LinkButton>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gvSanPham" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        EmptyDataText="Không có dữ liệu !"  PageSize="15"
                        Width="100%"  OnPageIndexChanging="gvSanPham_PageIndexChanging"
                        DataKeyNames="Id,TenSanPham" >
                        <Columns>
                            <asp:TemplateField HeaderText="T&#234;n sản phẩm">
                                <ItemTemplate>
                                <a href="NhapLieu_CB_SanphamChiTiet.aspx?direct=NhapLieu_CN_SanPhamQuanLy.aspx&Action=Edit&HoSoId=<%# Request["HoSoID"].ToString() %>&SanPhamID=<%# Server.UrlEncode(Eval("ID").ToString()) %>&TrangThaiID=13&UserControl=CN_HoSoDen&UserControlHS=<%# Request["UserControlHS"] %>">
                                    <%# Eval("TenSanPham")%>
                                </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="KyHieu" HeaderText="K&#253; hiệu" />
                            <asp:BoundField DataField="MaNhom" HeaderText="M&#227; nh&#243;m SP" />
                            <asp:BoundField DataField="TieuChuan" HeaderText="T.Chuẩn &#225;p dụng" />
                            <asp:BoundField DataField="NoiDungXuLy" HeaderText="Nội dung xử l&#253;" />
                            <asp:BoundField DataField="GhiChu" HeaderText="Ghi ch&#250;" />
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkCheckAll" runat="server" />
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkCheck" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </fieldset>
            </td>
        </tr>
    <tr runat="server" id="trThongBaoPhi">
        <td style="width: 100%; text-align: left">
            <fieldset style="width: 98%">
                <legend>Danh sách thông báo lệ phí</legend>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%; text-align: left">
                        </td>
                        <td style="text-align: right">
                            <asp:ImageButton ID="imgThemMoiTBP" runat="server" Height="16px" ImageUrl="~/htmls/image/new_f2.png"
                                Width="16px" />&nbsp;
                            <asp:LinkButton ID="lnkThemMoiTBP" runat="server">Thêm mới</asp:LinkButton>
                            &nbsp;
                            <asp:ImageButton ID="imgXoaTBP" runat="server" Height="16px" ImageUrl="~/htmls/image/cancel_f2.png"
                                Width="16px" OnClick="imgXoaTBP_Click" OnClientClick="if(GridIsChecked('gvPhi')) { return confirm('Bạn có chắc chắn muốn xóa các Thông báo lệ phí đã chọn không?');} else {alert('Bạn chưa chọn Thông báo lệ phí cần xóa.'); return false;}" />
                            <asp:LinkButton ID="lnkXoaTBP" runat="server" Font-Bold="False" OnClick="lnkXoaTBP_Click"
                                OnClientClick="if(GridIsChecked('gvPhi')) { return confirm('Bạn có chắc chắn muốn xóa các Thông báo lệ phí đã chọn không?');} else {alert('Bạn chưa chọn Thông báo lệ phí cần xóa.'); return false;}">Xóa</asp:LinkButton>
                            &nbsp;
                            <br />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvPhi" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    EmptyDataText="Không có dữ liệu !" PageSize="15" Width="100%" OnPageIndexChanging="gvPhi_PageIndexChanging"
                    DataKeyNames="ID,TrangThaiID,SoGiayThongBaoLePhi" >
                    <Columns>
                        <asp:TemplateField HeaderText="Số th&#244;ng b&#225;o lệ ph&#237;">
                            <ItemStyle Font-Underline="False" />
                            <ItemTemplate>
                                <a href="#" onclick=" return popCenter('NhapLieu_CB_ThongBaoPhi.aspx?TrangThaiID=<%# Eval("TrangThaiID") %>&action=edit&HoSoID=<%# Request["HoSoID"].ToString() %>&ThongBaoLePhiID=<%# Eval("ID")%>','CB_ThongBaoPhi_TaoMoi',800,600);" />
                                <%# Eval("SoGiayThongBaoLePhi")%>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TenTiengViet" HeaderText="Đơn vị nộp hồ sơ, c&#225; nh&#226;n" />
                        <asp:BoundField DataField="DiaChi" HeaderText="Địa chỉ" />
                        <asp:BoundField DataField="DienThoai" HeaderText="Điện thoại" />
                        <asp:BoundField DataField="Fax" HeaderText="Fax" />
                        <asp:BoundField DataField="TongPhi" HeaderText="Tổng ph&#237; (VNĐ)" DataFormatString="{0:#,#}.000" />
                        <asp:BoundField DataField="MoTa" HeaderText="Trạng th&#225;i" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkCheckAll" runat="server" onclick="HeaderClick('gvPhi')" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCheck" runat="server" onclick="ChildClick('gvPhi')" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
        </td>
    </tr>
    </table>
</asp:Content>
