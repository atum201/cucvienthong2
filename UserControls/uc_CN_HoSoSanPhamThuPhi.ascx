<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_CN_HoSoSanPhamThuPhi.ascx.cs"
    Inherits="UserControls_uc_CN_HoSoSanPhamThuPhi" %>
<div>
    <span style="font-family: Arial"><strong>CHỨNG NHẬN &gt;&gt;</strong><a href="../WebUI/CN_HoSo_QuanLy.aspx?direct=CN_HoSoMoi"><strong>DANH
        SÁCH HỒ SƠ MỚI NHẬN</strong></a><strong> &gt;&gt; DANH SÁCH THÔNG BÁO LỆ PHÍ</strong></span><strong>&nbsp;</strong></div>
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
                    <table align="center" border="0" width="100%">
                        <tr>
                            <td align="right" style="width: 10%; height: auto; text-align: left">
                                Số hồ sơ:</td>
                            <td colspan="1" style="width: 19%; height: auto; text-align: left">
                                <asp:Label ID="lblSoHoSo" runat="server" Font-Bold="True" Text="Label"></asp:Label></td>
                            <td colspan="1" style="width: 10%; height: auto; text-align: left">
                                Nhận HS từ:</td>
                            <td colspan="1" style="width: 20%; height: auto; text-align: left">
                                <asp:Label ID="lblNhanhstu" runat="server" Font-Bold="True" Text="Label"></asp:Label></td>
                            <td colspan="1" style="width: 10%; height: auto; text-align: left">
                                Email:</td>
                            <td style="width: 25%; height: auto; text-align: left">
                                <asp:Label ID="lblEmail" runat="server" Font-Bold="True" Text="Label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%; height: auto; text-align: left">
                                Ngày nhận:</td>
                            <td colspan="1" style="width: 19%; height: auto; text-align: left">
                                <asp:Label ID="lblNgayNhan" runat="server" Font-Bold="True" Text="Label"></asp:Label></td>
                            <td colspan="1" style="width: 10%; height: auto; text-align: left">
                                Người nộp HS:</td>
                            <td colspan="1" style="width: 20%; height: auto; text-align: left">
                                <asp:Label ID="lblNguoiNopHoSo" runat="server" Font-Bold="True" Text="Label"></asp:Label></td>
                            <td colspan="1" style="width: 10%; height: auto; text-align: left">
                                Nguồn gốc:</td>
                            <td style="width: 25%; height: auto; text-align: left">
                                <asp:Label ID="lblNguonGoc" runat="server" Font-Bold="True" Text="Label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%; height: auto; text-align: left">
                                Đơn vị nộp HS:</td>
                            <td colspan="1" style="width: 19%; height: auto; text-align: left">
                                <asp:Label ID="lblDonVi" runat="server" Font-Bold="True" Text="Label"></asp:Label></td>
                            <td colspan="1" style="width: 10%; height: auto; text-align: left">
                                Điện thoại:</td>
                            <td colspan="1" style="width: 20%; height: auto; text-align: left">
                                <asp:Label ID="lblDienThoai" runat="server" Font-Bold="True" Text="Label"></asp:Label></td>
                            <td colspan="1" style="width: 10%; height: auto; text-align: left">
                                Hình thức:</td>
                            <td style="width: 25%; height: auto; text-align: left">
                                <asp:Label ID="lblHinhThuc" runat="server" Font-Bold="True" Text="Label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%; height: auto; text-align: left">
                                Số công văn đến:</td>
                            <td colspan="1" style="width: 19%; height: auto; text-align: left">
                                <asp:Label ID="lblSoCVden" runat="server" Font-Bold="True" Text="Label"></asp:Label></td>
                            <td style="width: 10%; height: auto; text-align: left">
                                Fax:</td>
                            <td style="width: 20%; height: auto; text-align: left">
                                <asp:Label ID="lblFax" runat="server" Font-Bold="True"></asp:Label></td>
                            <td style="width: 10%; height: auto; text-align: left">
                            </td>
                            <td style="width: 25%; height: auto; text-align: left">
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
            <br />
        </td>
    </tr>
    <tr id="trDanhSachSP" runat="server">
        <td style="width: 100%; text-align: left;">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 100%; text-align: left">
            <fieldset style="width: 98%">
                <legend>Danh sách thông báo lệ phí</legend>
                <asp:GridView ID="gvPhi" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    EmptyDataText="Không có dữ liệu !" PageSize="15" Width="100%" OnDataBound="gvSanPham_DataBound">
                    <Columns>
                        <asp:HyperLinkField DataTextField="TenSanPham" HeaderText="Số th&#244;ng b&#225;o lệ ph&#237;"
                            NavigateUrl="~/WebUI/CN_ThuPhi.aspx" />
                        <asp:BoundField DataField="SoGiayCN" HeaderText="Số giấy chứng nhận" />
                        <asp:BoundField DataField="MaSanPham" HeaderText="Tổ chức, c&#225; nh&#226;n" />
                        <asp:BoundField DataField="NguonGoc" HeaderText="Địa chỉ" />
                        <asp:BoundField DataField="HinhThuc" HeaderText="Điện thoại" />
                        <asp:BoundField DataField="TrangThai" HeaderText="Fax" />
                        <asp:BoundField DataField="MaTrangThai" HeaderText="Danh s&#225;ch sản phẩm cần thu ph&#237;"
                            Visible="False" />
                        <asp:BoundField DataField="TongPhi" HeaderText="Tổng lệ ph&#237;" />
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkCheckAll" runat="server" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                            <ItemTemplate>
                                &nbsp;<asp:CheckBox ID="chkCheck" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
        </td>
    </tr>
</table>
