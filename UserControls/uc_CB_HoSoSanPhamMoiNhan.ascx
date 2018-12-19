<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_CB_HoSoSanPhamMoiNhan.ascx.cs"
    Inherits="UserControls_uc_CB_HoSoSanPhamMoiNhan" %>
<div>
    <span style="font-family: Arial;">CÔNG BỐ &gt;&gt;
        <asp:HyperLink ID="linkCum" runat="server" />
        &gt;&gt; DANH SÁCH SẢN PHẨM ĐẾN </span>
</div>
<table style="width: 100%">
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td style="width: 100%; height: 21px; text-align: left">
            <fieldset style="width: 98%">
                <legend>Thông tin hồ sơ</legend>
                <div style="text-align: left">
                    &nbsp;<table align="center" border="0" width="100%">
                        <tr>
                            <td></td>
                            <td>
                                Đơn vị nộp:</td>
                            <td colspan="5">
                                    <asp:Label ID="lblDonViNopHoSo" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <%--LongHH Start--%>
                        <tr>
                            <td style="width: 10px; height: 21px;">
                            </td>
                            <td style="height: 21px">
                                Tên tiếng anh:</td>
                            <td style="height: 21px">
                                <asp:Label ID="lblTenTiengAnh" runat="server" Font-Bold="True"></asp:Label></td>
                            <td style="height: 21px; width: 129px;">
                                Địa chỉ:</td>
                            <td style="height: 21px">
                                <strong>
                                    <asp:Label ID="lblDiaChi" runat="server" Font-Bold="True"></asp:Label></strong></td>
                            <td>
                                Mã số thuế:</td>
                            <td>
                                <strong>
                                    <asp:Label ID="lblMaSoThue" runat="server" Font-Bold="True"></asp:Label></strong></td>
                        </tr>
                        <%--LongHH End--%>
                        <tr>
                            <td style="width: 10px; height: 21px;">
                            </td>
                            <td style="height: 21px">
                                Số hồ sơ:</td>
                            <td style="height: 21px">
                                <asp:Label ID="lblSoHoSo" runat="server" Font-Bold="True"></asp:Label></td>
                            <td style="height: 21px; width: 129px;">
                                Người nộp:</td>
                            <td style="height: 21px">
                                <strong>
                                    <asp:Label ID="lblNguoiNop" runat="server" Font-Bold="True"></asp:Label></strong></td>
                            <td>
                                Nguồn gốc:</td>
                            <td>
                                <strong>
                                    <asp:Label ID="lblNguonGoc" runat="server" Font-Bold="True"></asp:Label></strong></td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                Người tiếp nhận:</td>
                            <td>
                                <asp:Label ID="lblNguoiTiepNhan" runat="server" Font-Bold="True"></asp:Label></td>
                            <td style="width: 129px">
                                Điện thoại:</td>
                            <td>
                                <strong>
                                    <asp:Label ID="lblDienThoai" runat="server" Font-Bold="True"></asp:Label></strong></td>
                            <td>
                                Trạng thái:</td>
                            <td>
                                <strong>
                                    <asp:Label ID="lblTrangThai" runat="server" Font-Bold="True"></asp:Label></strong></td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                Ngày tiếp nhận:</td>
                            <td>
                                <strong>
                                    <asp:Label ID="lblNgayTiepNhan" runat="server" Font-Bold="True"></asp:Label></strong></td>
                            <td style="width: 129px">
                                Email:</td>
                            <td>
                                <strong>
                                    <asp:Label ID="lblEmail" runat="server" Font-Bold="True"></asp:Label></strong></td><td>
                                <asp:Label ID="Label1" runat="server" Text="Người Thẩm định"></asp:Label></td>
                            <td style="width: 25%; height: 18px; text-align: left">
                                <strong>
                                    <asp:Label ID="lblNguoiThamDinh" runat="server"></asp:Label></strong></td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                Số công văn đến:</td>
                            <td>
                                <strong>
                                    <asp:Label ID="lblSoCongVanDen" runat="server" Font-Bold="True"></asp:Label></strong></td>
                            <td style="height: 21px; width: 129px;">
                                Hình thức tiếp nhận:</td>
                            <td style="height: 21px">
                                <strong>
                                    <asp:Label ID="lblLoaiHinhGui" runat="server" Font-Bold="True"></asp:Label></strong></td>
                            <td>
                            </td>
                            <td style="width: 25%; height: 18px; text-align: left">
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                Ý kiến tiếp nhận:</td>
                            <td colspan="5">
                                <asp:Label ID="lblYKienTiepNhan" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                    </table>
                </div>
            </fieldset>
            <br />
        </td>
    </tr>
    <tr id="trDanhSachSP" runat="server">
        <td style="width: 100%; text-align: left;">
            <fieldset style="width: 98%">
                <legend>Danh sách sản phẩm đến</legend>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: right; height: 40px;">
                            <asp:ImageButton ID="ImgBtnThemmoiSanPham" runat="server" Height="17px" ImageUrl="~/htmls/image/new_f2.png"
                                Width="17px" OnClick="ImgBtnThemmoiSanPham_Click" /><asp:LinkButton ID="lnkThemMoiSanPham"
                                    runat="server" Font-Bold="False" OnClick="lnkThemMoiSanPham_Click">Thêm mới </asp:LinkButton>&nbsp;
                            <asp:ImageButton ID="ImgBtnCompleteHS" runat="server" Height="14px" ImageUrl="~/htmls/image/send.JPG"
                                Width="23px" OnClick="ImgBtnCompleteHS_Click" Visible="False" OnClientClick="return confirm('Bạn có chắc chắn muốn hoàn thành hồ sơ không?');" />
                            <asp:LinkButton ID="LinkBtnCompleteHS" runat="server" Font-Bold="False" OnClick="LinkBtnCompleteHS_Click"
                                Visible="False" OnClientClick="return confirm('Bạn có chắc chắn muốn hoàn thành hồ sơ không?');">Gửi lưu trữ</asp:LinkButton>
                            <asp:ImageButton ID="imgXoaSanPham" runat="server" Height="16px" ImageUrl="~/htmls/image/cancel_f2.png"
                                Width="16px" OnClick="imgXoaSanPham_Click" OnClientClick="if(GridIsChecked('gvSanPham')) { return confirm('Bạn có chắc chắn muốn xóa các Sản phẩm đã chọn không?');} else {alert('Bạn chưa chọn Sản phẩm cần xóa.'); return false;}" />
                            <asp:LinkButton ID="lnkXoaSanPham" runat="server" Font-Bold="False" OnClick="lnkXoaSanPham_Click"
                                OnClientClick="if(GridIsChecked('gvSanPham')) { return confirm('Bạn có chắc chắn muốn xóa các Sản phẩm đã chọn không?');} else {alert('Bạn chưa chọn Sản phẩm cần xóa.'); return false;}">Xóa</asp:LinkButton>
                            &nbsp;
                            <br />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvSanPham" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    EmptyDataText="Không có dữ liệu !" PageSize="15" Width="100%" OnPageIndexChanging="gvSanPham_PageIndexChanging"
                    DataKeyNames="ID,TrangThaiID" OnRowDataBound="gvSanPham_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="T&#234;n sản phẩm">
                            <ItemStyle Font-Underline="False" />
                            <ItemTemplate>
                                <a href="CB_HoSoSanPham_QuanLy.aspx?HoSoID=<%# Request["HoSoID"].ToString() %>&SanPhamID=<%# Server.UrlEncode(Eval("ID").ToString()) %>&TrangThaiID=<%# Server.UrlEncode(Eval("TrangThaiID").ToString())%>&UserControl=CB_HoSoDen&UserControlHS=<%# Request["UserControlHS"] %>">
                                    <%# Eval("TenTiengViet")%>
                                </a>
                                 <div runat="server" id="View" style="float:right;" visible="false">
                                <a href="CB_TiepNhanHoSo_TaoMoi.aspx?Direct=CB_HoSoSanPham_QuanLy&Action=Edit&HoSoID=<%# Request["HoSoID"].ToString() %>&SanPhamID=<%# Server.UrlEncode(Eval("ID").ToString()) %>&TrangThaiID=<%# Server.UrlEncode(Eval("TrangThaiID").ToString())%>&UserControl=CB_HoSoDen&UserControlHS=<%# Request["UserControlHS"] %>">
                                    <img src="../htmls/image/sua.gif" alt="sửa" title="Sửa thông tin sản phẩm" border="0">
                                </a>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TenTiengAnh" HeaderText="T&#234;n tiếng anh" />
                        <%--LongHH--%>
                        <%--<asp:BoundField DataField="KyHieu" HeaderText="K&#253; hiệu" />--%>
                        <asp:TemplateField HeaderText="K&#253; hiệu">
                            <ItemTemplate>
                                <a href="../ReportForm/DieuKienInBaoCao.aspx?HoSoID=<%# Request["HoSoID"].ToString() %>&SanPhamId=<%# Server.UrlEncode(Eval("ID").ToString()) %>&LoaiBaoCao=CBPhieuDanhGia&format=Word">
                                    <%# Eval("KyHieu")%>
                                </a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <%--LongHH--%>
                        <asp:BoundField DataField="TenHangSanXuat" HeaderText="H&#227;ng sản xuất" />
                        <%--LongHH--%>
                        <%--<asp:BoundField DataField="MoTa" HeaderText="Trạng th&#225;i" />--%>
                        <asp:TemplateField HeaderText="Trạng th&#225;i">
                            <ItemTemplate>
                                <%--<a href="<%= (Eval("MoTa").ToString() == "Giám đốc phê duyệt" ? "'../ReportForm/HienBaoCao.aspx?HoSoID="+Request["HoSoID"].ToString() +"&SanPhamId="+ Server.UrlEncode(Eval("ID").ToString()) +"&LoaiBaoCao=BanTiepNhan'" :"#")%>">
                                    <%# Eval("MoTa")%>
                                </a>--%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <%--LongHH--%>
                        <asp:BoundField Visible="False" DataField="CheckDaDoc" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkCheckAll" runat="server" onclick="HeaderClick('gvSanPham')" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCheck" runat="server" onclick="ChildClick('gvSanPham')" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
        </td>
    </tr>
    <tr>
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
                            <asp:ImageButton ID="imgGuiTBP" runat="server" Height="14px" ImageUrl="~/htmls/image/send.JPG"
                                Width="23px" OnClick="imgGuiTBP_Click" OnClientClick="if(GridIsChecked('gvPhi')) { return confirm('Bạn có chắc chắn muốn gửi các Thông báo lệ phí đã chọn không?');} else {alert('Bạn chưa chọn Thông báo lệ phí cần gửi.'); return false;}" />
                            <asp:LinkButton ID="lnkGuiTBP" runat="server" Font-Bold="False" OnClick="lnkGuiTBP_Click"
                                OnClientClick="if(GridIsChecked('gvPhi')) { return confirm('Bạn có chắc chắn muốn gửi các Thông báo lệ phí đã chọn không?');} else {alert('Bạn chưa chọn Thông báo lệ phí cần gửi.'); return false;}">Gửi</asp:LinkButton>
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
                    DataKeyNames="ID,TrangThaiID,SoGiayThongBaoLePhi" OnRowDataBound="gvPhi_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Số th&#244;ng b&#225;o lệ ph&#237;">
                            <ItemStyle Font-Underline="False" />
                            <ItemTemplate>
                                <a href="#" onclick="if(<%# Eval("TrangThaiID") %> == 1) return popCenter('CB_ThongBaoPhi_TaoMoi.aspx?TrangThaiID=<%# Eval("TrangThaiID") %>&action=edit&HoSoID=<%# Request["HoSoID"].ToString() %>&ThongBaoLePhiID=<%# Eval("ID")%>','CN_ThongBaoPhi_TaoMoi',800,600);
                                                
                                             else if (<%# Eval("TrangThaiID") %> == 2) return popCenter('CB_ThongBaoPhi_TaoMoi.aspx?TrangThaiID=<%# Eval("TrangThaiID") %>&action=view&HoSoID=<%# Request["HoSoID"].ToString() %>&ThongBaoLePhiID=<%# Eval("ID")%>','CN_ThongBaoPhi_TaoMoi',800,600);
                                             else return popCenter('TestBaoCao.aspx?HoSoID=<%# Request["HoSoID"].ToString() %>&ThongBaoLePhiID=<%# Server.UrlEncode(Eval("ID").ToString()) %>','CN_ThuPhi',800,600);" />
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
