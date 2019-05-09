<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_CN_HoSoSanPhamDaGui.ascx.cs" Inherits="UserControls_uc_CN_HoSoSanPhamDaGui" %>
<div><span style="font-family: Arial"><strong>CHỨNG NHẬN &gt;&gt; </strong>
    <asp:HyperLink ID="linkCum" runat="server" /><strong> &gt;&gt; DANH SÁCH SẢN PHẨM ĐÃ GỬI </strong></span>
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
                                &nbsp;<asp:Label ID="lblDonViNopHoSo" runat="server" Font-Bold="True"></asp:Label></td>
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
                            <td style="width: 10px;">
                            </td>
                            <td>
                                Số hồ sơ:</td>
                            <td>
                                <asp:Label ID="lblSoHoSo" runat="server" Font-Bold="True"></asp:Label></td>
                            <td>
                                Người nộp:</td>
                            <td>
                                <strong>
                                    <asp:Label ID="lblNguoiNop" runat="server" Font-Bold="True"></asp:Label></strong></td><td>
                                Loại hình chứng nhận:</td>
                            <td>
                                <strong>
                                    <asp:Label ID="lblLoaiHinhChungNhan" runat="server" Font-Bold="True"></asp:Label></strong></td>
                        </tr>
                        <tr>
                            <td style="height: 21px">
                            </td>
                            <td style="height: 21px">
                                Người tiếp nhận</td>
                            <td style="height: 21px">
                                <asp:Label ID="lblNguoiTiepNhan" runat="server" Font-Bold="True"></asp:Label></td>
                            <td style="height: 21px">
                                Điện thoại:</td>
                            <td style="height: 21px">
                                <strong>
                                    <asp:Label ID="lblDienThoai" runat="server" Font-Bold="True"></asp:Label></strong></td>
                            <td style="height: 21px">
                                Nguồn gốc:</td>
                            <td style="height: 21px">
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
                            <td>
                                Hình thức tiếp nhận:</td>
                            <td>
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
                
                <asp:GridView ID="gvSanPham" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    EmptyDataText="Không có dữ liệu !" PageSize="15" Width="100%" OnPageIndexChanging="gvSanPham_PageIndexChanging" OnRowDataBound="gvSanPham_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="SanPhamID" Visible="False" />
                        <asp:TemplateField HeaderText="T&#234;n sản phẩm">
                            <ItemStyle Font-Underline="False" />
                            <ItemTemplate>
                                <%# Eval("TenTiengViet") %>
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
                        <asp:TemplateField HeaderText="Chức năng">
                            <ItemStyle Width="105px" VerticalAlign="Top" />
                            <ItemTemplate>
                                <asp:Panel ID="Panel1" runat="server" Visible="false" ScrollBars="None" SkinID="FU" >
                                    <asp:HyperLink ID="HyperLink1" runat="server"/>
                                </asp:Panel>
                                <asp:Panel ID="Panel2" runat="server" Visible="false" ScrollBars="None" SkinID="FU" >
                                    <asp:HyperLink ID="HyperLink2" runat="server"/>
                                </asp:Panel>
                                <asp:Panel ID="Panel3" runat="server" Visible="false" ScrollBars="None" SkinID="FU" >
                                    <asp:HyperLink ID="HyperLink3" runat="server"/>
                                </asp:Panel>
                                <asp:Panel ID="Panel4" runat="server" Visible="false" ScrollBars="None" SkinID="FU" >
                                    <asp:HyperLink ID="HyperLink4" runat="server"/>
                                </asp:Panel>
                                <asp:Panel ID="Panel5" runat="server" Visible="false" ScrollBars="None" SkinID="FU" >
                                    <asp:HyperLink ID="HyperLink5" runat="server"/>
                                </asp:Panel>
                            </ItemTemplate>
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
                <br />
                <asp:GridView ID="gvPhi" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    EmptyDataText="Không có dữ liệu !" PageSize="20" Width="100%"
                    DataKeyNames="ID,TrangThaiID,SoGiayThongBaoLePhi">
                    <Columns>
                        <asp:TemplateField HeaderText="Số th&#244;ng b&#225;o lệ ph&#237;">
                            <ItemStyle Font-Underline="False" />
                            <ItemTemplate>
                                <a href="#" onclick="if(<%# Eval("TrangThaiID") %> == 1) return popCenter('CN_ThongBaoPhi_TaoMoi.aspx?action=edit&HoSoID=<%# Request["HoSoID"].ToString() %>&ThongBaoLePhiID=<%# Eval("ID")%>','CN_ThongBaoPhi_TaoMoi',800,600);                                             
                                             else if (<%# Eval("TrangThaiID") %> != 1) return popCenter('CN_ThongBaoPhi_TaoMoi.aspx?action=view&HoSoID=<%# Request["HoSoID"].ToString() %>&ThongBaoLePhiID=<%# Eval("ID")%>','CN_ThongBaoPhi_TaoMoi',800,600);
                                             else return popCenter('TestBaoCao.aspx?HoSoID=<%# Request["HoSoID"].ToString() %>&ThongBaoLePhiID=<%# Server.UrlEncode(Eval("ID").ToString()) %>','CN_ThuPhi',800,600);" />
                                <%# Eval("SoGiayThongBaoLePhi")%>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TenTiengViet" HeaderText="Đơn vị nộp hồ sơ, c&#225; nh&#226;n" />
                        <asp:BoundField DataField="DiaChi" HeaderText="Địa chỉ" />
                        <asp:BoundField DataField="DienThoai" HeaderText="Điện thoại" />
                        <asp:BoundField DataField="Fax" HeaderText="Fax" />
                        <asp:BoundField DataField="TongPhi" HeaderText="Tổng ph&#237;" DataFormatString="{0:#,#}.000" />
                        <asp:BoundField DataField="MoTa" HeaderText="Trạng th&#225;i" />
                    </Columns>
                </asp:GridView>
            </fieldset>
        </td>
    </tr>
</table>
