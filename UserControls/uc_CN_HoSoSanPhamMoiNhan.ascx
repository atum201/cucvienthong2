<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_CN_HoSoSanPhamMoiNhan.ascx.cs"
    Inherits="UserControls_uc_CN_HoSoSanPhamMoiNhan" %>

<%--LongHH--%>
<script type="text/javascript">
    function PageRedirect(url) {
        window.location.href = url;
    }
</script>
<%--LongHH--%>

<div>
    <span style="font-family: Arial;">CHỨNG NHẬN &gt;&gt;
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
                            <td>
                            </td>
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
                            <td align="right" style="width: 10px; height: 21px;">
                            </td>
                            <td style="height: 21px">
                                Số hồ sơ:</td>
                            <td style="height: 21px">
                                <asp:Label ID="lblSoHoSo" runat="server" Font-Bold="True"></asp:Label></td>
                            <td style="height: 21px">
                                Người nộp:</td>
                            <td style="height: 21px">
                                <strong>
                                    <asp:Label ID="lblNguoiNop" runat="server" Font-Bold="True"></asp:Label></strong></td>
                            <td style="height: 21px">
                                Loại hình chứng nhận:</td>
                            <td style="height: 21px">
                                <asp:Label ID="lblLoaiHinhChungNhan" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                Người tiếp nhận:</td>
                            <td>
                                <asp:Label ID="lblNguoiTiepNhan" runat="server" Font-Bold="True"></asp:Label></td>
                            <td>
                                Điện thoại:</td>
                            <td>
                                <strong>
                                    <asp:Label ID="lblDienThoai" runat="server" Font-Bold="True"></asp:Label></strong></td>
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
                                Ngày tiếp nhận:</td>
                            <td>
                                <strong>
                                    <asp:Label ID="lblNgayTiepNhan" runat="server" Font-Bold="True"></asp:Label></strong></td>
                            <td>
                                Email:</td>
                            <td>
                                <strong>
                                    <asp:Label ID="lblEmail" runat="server" Font-Bold="True"></asp:Label></strong></td>
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
                                Số công văn đến:</td>
                            <td>
                                <strong>
                                    <asp:Label ID="lblSoCongVanDen" runat="server" Font-Bold="True"></asp:Label></strong></td>
                            <td style="height: 21px">
                                Hình thức tiếp nhận:</td>
                            <td style="height: 21px">
                                <strong>
                                    <asp:Label ID="lblLoaiHinhGui" runat="server" Font-Bold="True"></asp:Label></strong></td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Người Thẩm định"></asp:Label></td>
                            <td style="width: 25%; height: 18px; text-align: left">
                                <strong>
                                    <asp:Label ID="lblNguoiThamDinh" runat="server"></asp:Label></strong></td>
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
                <asp:GridView ID="gvSanPham" runat="server" AllowPaging="True" AutoGenerateColumns="False" Vi
                    EmptyDataText="Không có dữ liệu !" PageSize="15" Width="100%" OnPageIndexChanging="gvSanPham_PageIndexChanging"
                    DataKeyNames="ID,TrangThaiID" OnRowDataBound="gvSanPham_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="T&#234;n sản phẩm">
                            <ItemStyle Font-Underline="False" />
                            <ItemTemplate>
                                <a href="CN_HoSoSanPham_QuanLy.aspx?HoSoID=<%# Request["HoSoID"].ToString() %>&SanPhamID=<%# Server.UrlEncode(Eval("ID").ToString()) %>&TrangThaiID=<%# Server.UrlEncode(Eval("TrangThaiID").ToString())%>&UserControl=CN_HoSoDen&UserControlHS=<%# Request["UserControlHS"] %>">
                                    <%# Eval("TenTiengViet") %>
                                </a>
                                <div runat="server" id="View" style="float:right;" visible="false">
                                <a href="CN_TiepNhanHoSo_TaoMoi.aspx?Direct=CN_HoSoSanPham_QuanLy&Action=Edit&HoSoID=<%# Request["HoSoID"].ToString() %>&SanPhamID=<%# Server.UrlEncode(Eval("ID").ToString()) %>&TrangThaiID=<%# Server.UrlEncode(Eval("TrangThaiID").ToString())%>&UserControl=CN_HoSoDen&UserControlHS=<%# Request["UserControlHS"] %>">
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
                                <%--<a href="#" onclick="javascript:popCenter('../ReportForm/DieuKienInBaoCao.aspx?HoSoID=<%# Request["HoSoID"].ToString() %>&SanPhamId=<%# Server.UrlEncode(Eval("ID").ToString()) %>&LoaiBaoCao=PhieuDanhGia&format=Word','CBPhieuDanhGia',450,300);">--%>
                                <a href="../ReportForm/HienBaoCao.aspx?HoSoID=<%# Request["HoSoID"].ToString() %>&SanPhamId=<%# Server.UrlEncode(Eval("ID").ToString()) %>&LoaiBaoCao=PhieuDanhGia&format=Word">
                                    <%# Eval("KyHieu")%>
                                </a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <%--LongHH--%>
                        <asp:BoundField DataField="MaNhomSanPham" HeaderText="M&#227; nh&#243;m sản phẩm" />
                        <asp:BoundField DataField="TenHangSanXuat" HeaderText="H&#227;ng sản xuất" />
                        <%--LongHH--%>
                        <%--<asp:BoundField DataField="MoTa" HeaderText="Trạng th&#225;i" />--%>
                        <asp:TemplateField HeaderText="Trạng th&#225;i">
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--LongHH--%>
                        <asp:BoundField Visible="false" DataField="CheckDaDoc" />
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkCheckAll" runat="server" onclick="HeaderClick('gvSanPham')" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCheck" runat="server" onclick="ChildClick('gvSanPham')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
        </td>
    </tr>
    <tr runat="server" id="trThongBaoNopTien">
        <td style="width: 100%; text-align: left">
            <fieldset style="width: 98%">
                <legend>Thông báo nộp tiền lấy mẫu sản phẩm và đánh giá quy trình sản xuất</legend>
                <table style="width: 100%;" id="Table1">
                    <tr>
                        <td style="text-align: right; height: 33px;">
                            <asp:ImageButton ID="IBThemMoi" runat="server" Height="17px" ImageUrl="~/htmls/image/new_f2.png"
                                Width="17px" OnClick="IBThemMoi_Click" />
                            <asp:LinkButton ID="LBThemMoi" runat="server" Font-Bold="False" OnClick="LBThemMoi_Click">Thêm mới</asp:LinkButton>
                            <asp:ImageButton ID="IBGui" runat="server" Height="14px" ImageUrl="~/htmls/image/send.JPG"
                                OnClientClick="return confirm('Bạn có chắc chắn muốn gửi thông báo này không?');"
                                Width="23px" OnClick="IBGui_Click" />
                            <asp:LinkButton ID="LBGui" runat="server" Font-Bold="False" OnClick="LBGui_Click"
                                OnClientClick="return confirm('Bạn có chắc chắn muốn gửi thông báo này không?');">
                                 Gửi lãnh đạo</asp:LinkButton>
                            <asp:ImageButton ID="IBXoa" runat="server" Enabled="False" Height="16px" ImageUrl="~/htmls/image/cancel_f2.png"
                                Width="16px" OnClick="IBXoa_Click" />
                            <asp:LinkButton ID="LBXoa" runat="server" Font-Bold="False" Enabled="False" OnClick="LBXoa_Click">Xóa</asp:LinkButton>
                            <br />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvThongBaoNopTien" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    EmptyDataText="Không có dữ liệu !" PageSize="15" Width="100%" OnPageIndexChanging="gvThongBaoNopTien_PageIndexChanging"
                    DataKeyNames="ID,TrangThaiID,SoGiayThongBaoLePhi" OnRowDataBound="gvThongBaoNopTien_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Số th&#244;ng b&#225;o lệ ph&#237;">
                            <ItemStyle Font-Underline="False" />
                            <ItemTemplate>
                                <a href="#" onclick="if(<%# Eval("TrangThaiID") %> == 1) return popCenter('CN_ThongBaoNopTien_TaoMoi.aspx?TrangThaiID=<%# Eval("TrangThaiID") %>&action=edit&HoSoID=<%# Request["HoSoID"].ToString() %>&ThongBaoLePhiID=<%# Eval("ID")%>','CN_ThongBaoPhi_TaoMoi',800,600);
                                             else if (<%# Eval("TrangThaiID") %> == 9) return popCenter('CN_ThongBaoNopTien_TaoMoi.aspx?TrangThaiID=<%# Eval("TrangThaiID") %>&action=edit&HoSoID=<%# Request["HoSoID"].ToString() %>&ThongBaoLePhiID=<%# Eval("ID")%>','CN_ThongBaoPhi_TaoMoi',800,600);
                                             else if (<%# Eval("TrangThaiID") %> != 1) return popCenter('CN_ThongBaoNopTien_TaoMoi.aspx?TrangThaiID=<%# Eval("TrangThaiID") %>&action=view&HoSoID=<%# Request["HoSoID"].ToString() %>&ThongBaoLePhiID=<%# Eval("ID")%>','CN_ThongBaoPhi_TaoMoi',800,600);
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
                        <asp:TemplateField HeaderText="In th&#244;ng b&#225;o">
                            <ItemTemplate>
                                <a onclick="javascript:popCenter('../ReportForm/HienBaoCao.aspx?ThongBaoLePhiId=<%#DataBinder.Eval(Container.DataItem,"ID")%>&LoaiBaoCao=ThongBaoNopTien','_blank',790,590);"
                                    href="#">
                                    <img src="../Images/printer.png" style="border: none;" alt="In biên bản" /></a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkCheckAll" runat="server" onclick="HeaderClick('gvThongBaoNopTien')" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCheck" runat="server" onclick="ChildClick('gvThongBaoNopTien')" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
        </td>
    </tr>
    <tr runat="server" id="trThongBaoNopTienCNHQ">
        <td style="width: 100%; text-align: left">
            <fieldset style="width: 98%">
                <legend>Thông báo nộp tiền lấy mẫu sản phẩm và đánh giá quy trình sản xuất</legend>
                <table style="width: 100%;" id="TableCNHQ">
                    <tr>
                        <td style="text-align: right; height: 33px;">
                            <%--LongHH--%>
                            <asp:ImageButton ID="IBThemMoiCNHQ" runat="server" Height="17px" ImageUrl="~/htmls/image/new_f2.png"
                                Width="17px" OnClick="IBThemMoiCNHQ_Click" />
                            <asp:LinkButton ID="LBThemMoiCNHQ" runat="server" Font-Bold="False" OnClick="LBThemMoiCNHQ_Click">Thêm mới</asp:LinkButton>
                            <%--LongHH--%>
                            <asp:ImageButton ID="IBGuiCNHQ" runat="server" Height="14px" ImageUrl="~/htmls/image/send.JPG"
                                OnClientClick="return confirm('Bạn có chắc chắn muốn gửi thông báo này không?');"
                                Width="23px" OnClick="IBGuiCNHQ_Click" />
                            <asp:LinkButton ID="LBGuiCNHQ" runat="server" Font-Bold="False" OnClick="LBGuiCNHQ_Click"
                                OnClientClick="return confirm('Bạn có chắc chắn muốn gửi thông báo này không?');">
                                 Gửi lãnh đạo</asp:LinkButton>
                            <asp:ImageButton ID="IBXoaCNHQ" runat="server" Enabled="False" Height="16px" ImageUrl="~/htmls/image/cancel_f2.png"
                                Width="16px" OnClick="IBXoaCNHQ_Click" />
                            <asp:LinkButton ID="LBXoaCNHQ" runat="server" Font-Bold="False" Enabled="False" OnClick="LBXoaCNHQ_Click">Xóa</asp:LinkButton>
                            <br />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvThongBaoNopTienCNHQ" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    EmptyDataText="Không có dữ liệu !" PageSize="15" Width="100%" OnPageIndexChanging="gvThongBaoNopTienCNHQ_PageIndexChanging"
                    DataKeyNames="ID,TrangThaiID,SoGiayThongBaoLePhi" OnRowDataBound="gvThongBaoNopTienCNHQ_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Số th&#244;ng b&#225;o lệ ph&#237;">
                            <ItemStyle Font-Underline="False" />
                            <ItemTemplate>
                                <a href="#" onclick="if(<%# Eval("TrangThaiID") %> == 1) return popCenter('CN_ThongBaoPhi_QTSX_TaoMoi.aspx?TrangThaiID=<%# Eval("TrangThaiID") %>&action=edit&HoSoID=<%# Request["HoSoID"].ToString() %>&ThongBaoLePhiID=<%# Eval("ID")%>','CN_ThongBaoPhi_QTSX_TaoMoi',800,600);
                                             else if (<%# Eval("TrangThaiID") %> == 9) return popCenter('CN_ThongBaoPhi_QTSX_TaoMoi.aspx?TrangThaiID=<%# Eval("TrangThaiID") %>&action=edit&HoSoID=<%# Request["HoSoID"].ToString() %>&ThongBaoLePhiID=<%# Eval("ID")%>','CN_ThongBaoPhi_QTSX_TaoMoi',800,600);
                                             else if (<%# Eval("TrangThaiID") %> != 1) return popCenter('CN_ThongBaoPhi_QTSX_TaoMoi.aspx?TrangThaiID=<%# Eval("TrangThaiID") %>&action=view&HoSoID=<%# Request["HoSoID"].ToString() %>&ThongBaoLePhiID=<%# Eval("ID")%>','CN_ThongBaoPhi_QTSX_TaoMoi',800,600);
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
                        <asp:TemplateField HeaderText="In th&#244;ng b&#225;o">
                            <ItemTemplate>
                                <a onclick="javascript:popCenter('../ReportForm/HienBaoCao.aspx?ThongBaoLePhiId=<%#DataBinder.Eval(Container.DataItem,"ID")%>&LoaiBaoCao=ThuPhiDanhGiaQTSX','_blank',790,590);"
                                    href="#">
                                    <img src="../Images/printer.png" style="border: none;" alt="In biên bản" /></a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkCheckAll" runat="server" onclick="HeaderClick('gvThongBaoNopTienCNHQ')" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCheck" runat="server" onclick="ChildClick('gvThongBaoNopTienCNHQ')" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
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
                            <%--LongHH--%>
                            <%--<asp:ImageButton ID="imgBtnSuaPhiTiepNhan" runat="server" Height="16px" ImageUrl="~/htmls/image/sua.gif" OnClick="imgBtnSuaPhiTiepNhan_Click"
                                Width="16px" />--%>
                            <%--<asp:LinkButton ID="LBTaoTiepNhan" runat="server" Font-Bold="False" OnClick="LBTaoTiepNhan_Click">Phí tiếp nhận HS</asp:LinkButton>--%>
                            <asp:LinkButton ID="lnkThemMoiQTSX" runat="server">Thêm mới QTSX</asp:LinkButton>
                            <%--LongHH--%>
                            <asp:ImageButton ID="imgThemMoiTBP" runat="server" Height="16px" ImageUrl="~/htmls/image/new_f2.png"
                                Width="16px" />&nbsp;
                            <asp:LinkButton ID="lnkThemMoiTBP" runat="server">Thêm mới</asp:LinkButton>
                            <asp:ImageButton ID="imgGuiTBP" runat="server" Height="14px" ImageUrl="~/htmls/image/send.JPG"
                                Width="23px" OnClick="imgGuiTBP_Click" OnClientClick="if(GridIsChecked('gvPhi')) { return confirm('Bạn có chắc chắn muốn gửi các Thông báo lệ phí đã chọn không?');} else {alert('Bạn chưa chọn Thông báo lệ phí cần gửi.'); return false;}" />
                            <asp:LinkButton ID="lnkGuiTBP" runat="server" Font-Bold="False" OnClick="lnkGuiTBP_Click"
                                OnClientClick="if(GridIsChecked('gvPhi')) { return confirm('Bạn có chắc chắn muốn gửi các Thông báo lệ phí đã chọn không?');} else {alert('Bạn chưa chọn Thông báo lệ phí cần gửi.'); return false;}">Gửi lãnh đạo</asp:LinkButton>
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
                                <a href="#" onclick="if(<%# Eval("TrangThaiID") %> == 1) return popCenter('CN_ThongBaoPhi_TaoMoi.aspx?TrangThaiID=<%# Eval("TrangThaiID") %>&action=edit&HoSoID=<%# Request["HoSoID"].ToString() %>&ThongBaoLePhiID=<%# Eval("ID")%>','CN_ThongBaoPhi_TaoMoi',800,600);
                                                
                                             else if (<%# Eval("TrangThaiID") %> == 2) return popCenter('CN_ThongBaoPhi_TaoMoi.aspx?TrangThaiID=<%# Eval("TrangThaiID") %>&action=view&HoSoID=<%# Request["HoSoID"].ToString() %>&ThongBaoLePhiID=<%# Eval("ID")%>','CN_ThongBaoPhi_TaoMoi',800,600);
                                             else return popCenter('TestBaoCao.aspx?HoSoID=<%# Request["HoSoID"].ToString() %>&ThongBaoLePhiID=<%# Server.UrlEncode(Eval("ID").ToString()) %>&TenBaoCao=LePhi_CNHQ','CN_ThuPhi',800,600);" />
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
