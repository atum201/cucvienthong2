<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="GiamSat_View.aspx.cs" Inherits="WebUI_GiamSat_View" Title="Chi tiết hồ sơ chứng nhận hợp chuẩn" %>

<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
        <span style="font-family: Arial; font-weight: bold;">GIÁM SÁT HỒ SƠ CHỨNG NHẬN HỢP CHUẨN
            &gt;&gt<%--<a
            href="../WebUI/CN_HoSo_QuanLy.aspx?UserControl=CN_HoSoDen"> DANH SÁCH HỒ SƠ MỚI</a>--%>
            DANH SÁCH SẢN PHẨM</span>
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
                                        <asp:Label ID="lblNguoiNop" runat="server"></asp:Label></strong></td>
                                <td colspan="1" style="width: 130px;">
                                    Loại hình chứng nhận:</td>
                                <td style="width: auto;">
                                    <asp:Label ID="lblLoaiHinhChungNhan" runat="server" Font-Bold="True"></asp:Label></td>
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
                                        <asp:Label ID="lblDienthoai" runat="server"></asp:Label></strong></td>
                                <td colspan="1" style="width: 120px;">
                                    Hình thức tiếp nhận:</td>
                                <td style="width: auto;">
                                    <strong>
                                        <asp:Label ID="lblLoaiHinhGui" runat="server"></asp:Label></strong></td>
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
                                    Nguồn gốc:</td>
                                <td style="width: auto;">
                                    <strong>
                                        <asp:Label ID="lblNguonGoc" runat="server"></asp:Label></strong></td>
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
                    <asp:GridView ID="gvSanPham" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        EmptyDataText="Không có dữ liệu !" PageSize="15" Width="100%" OnPageIndexChanging="gvSanPham_PageIndexChanging"
                        DataKeyNames="Id,TenSanPham">
                        <Columns>
                            <asp:TemplateField HeaderText="T&#234;n sản phẩm">
                                <ItemTemplate>
                                    <a href="GiamSat_SanPhamView.aspx?HoSoId=<%# Request["HoSoID"].ToString() %>&SanPhamID=<%# Server.UrlEncode(Eval("ID").ToString()) %>&TrangThaiID=13&UserControl=CN_HoSoDen&UserControlHS=<%# Request["UserControlHS"] %>">
                                        <%# Eval("TenSanPham")%>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="KyHieu" HeaderText="K&#253; hiệu" />
                            <asp:BoundField DataField="MaNhom" HeaderText="M&#227; nh&#243;m SP" />
                            <asp:BoundField DataField="TieuChuan" HeaderText="T.Chuẩn &#225;p dụng" />
                            <asp:BoundField DataField="NoiDungXuLy" HeaderText="Nội dung xử l&#253;" />
                            <asp:BoundField DataField="GhiChu" HeaderText="Ghi ch&#250;" />
                        </Columns>
                    </asp:GridView>
                </fieldset>
            </td>
        </tr>
        <tr runat="server" id="trThongBaoNopTien">
            <td style="width: 100%; text-align: left">
                <fieldset style="width: 98%">
                    <legend>Danh sách thông báo nộp tiền giám sát</legend>
                    <table style="width: 100%;" id="Table1">
                        <tr>
                            <td style="text-align: right; height: 33px;">
                                <asp:ImageButton ID="IBThemMoi" runat="server" Height="17px" ImageUrl="~/htmls/image/new_f2.png"
                                    Width="17px" />
                                <asp:LinkButton ID="LBThemMoi" runat="server" Font-Bold="False">Thêm mới</asp:LinkButton>
                                &nbsp;
                                <asp:ImageButton ID="IBXoa" runat="server" Enabled="False" Height="16px" ImageUrl="~/htmls/image/cancel_f2.png"
                                    Width="16px" OnClick="IBXoa_Click" />
                                <asp:LinkButton ID="LBXoa" runat="server" Font-Bold="False" Enabled="False" OnClick="LBXoa_Click">Xóa</asp:LinkButton>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gvThongBaoNopTien" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        EmptyDataText="Không có dữ liệu !" PageSize="15" Width="100%" OnPageIndexChanging="gvThongBaoNopTien_PageIndexChanging"
                        DataKeyNames="ID,TrangThaiID,SoGiayThongBaoLePhi">
                        <Columns>
                            <asp:TemplateField HeaderText="Số th&#244;ng b&#225;o lệ ph&#237;">
                                <ItemStyle Font-Underline="False" />
                                <ItemTemplate>
                                    <a href="#" onclick=" return popCenter('GiamSat_ThongBaoNopTien.aspx?TrangThaiID=<%# Eval("TrangThaiID") %>&action=edit&HoSoID=<%# Request["HoSoID"].ToString() %>&ThongBaoLePhiID=<%# Eval("ID")%>','CN_ThongBaoPhi_TaoMoi',800,600);" />
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
                                    <asp:CheckBox ID="chkCheckAll" runat="server" onclick="HeaderClick('gvThongBaoNopTien')" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkCheck" runat="server" onclick="ChildClick('gvThongBaoNopTien')" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <a id="lnkIn" href="#" onclick="return loadBaoCao('<%# Eval("Id") %>');">
                                        <img runat="server" id="lnkInLePhi" title="In thông báo lệ phí giám sát" style="border: none"
                                            src="../images/printer.png" alt="" /></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </fieldset>
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">    
        function loadBaoCao(gID) {
            popCenter("../ReportForm/HienBaoCao.aspx?LoaiBaoCao=ThongBaoNopTienGS&LePhiID=" + gID.toString(), "rptInLePhiGS", 1024, 600); return false;
        }
    </script>

</asp:Content>
