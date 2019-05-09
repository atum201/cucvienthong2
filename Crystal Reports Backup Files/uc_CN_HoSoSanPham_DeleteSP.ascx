<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_CN_HoSoSanPham_DeleteSP.ascx.cs" Inherits="UserControls_uc_CN_HoSoSanPham_DeleteSP" %>

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
                                Số công văn đến:<%=countsanpham %></td>
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
                        <asp:BoundField DataField="TenTiengViet" HeaderText="T&#234;n sản phẩm" />
                        <asp:BoundField DataField="TenTiengAnh" HeaderText="T&#234;n tiếng anh" />
                        <asp:BoundField DataField="KyHieu" HeaderText="K&#253; hiệu" />
                        <%--LongHH--%>
                        <%--<asp:BoundField DataField="KyHieu" HeaderText="K&#253; hiệu" />--%>
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
</table>
