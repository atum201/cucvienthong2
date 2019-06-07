<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_CB_HoSoMoiNhan.ascx.cs"
    Inherits="UserControls_uc_CB_HoSoMoiNhan" %>
<span style="font-family: Arial"><strong>CÔNG BỐ >> DANH SÁCH HỒ SƠ MỚI NHẬN</strong></span><br />
<table style="width: 100%;">
    <tr>
        <td style="width: 100%; height: 21px; text-align: right">
        </td>
    </tr>
    <tr>
        <td style="width: 100%; height: auto; text-align: left;">
            <fieldset style="width: 98%">
                <legend>Danh sách hồ sơ đến</legend>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%; text-align: left">
                            <asp:Label ID="lblTrangThaiHoSo" runat="server" Text="Lọc theo trạng thái hồ sơ" />
                            <asp:DropDownList ID="ddlFilterTrangThai" runat="server" OnSelectedIndexChanged="ddlFilterTrangThai_SelectedIndexChanged"
                                AutoPostBack="true" />
                        </td>
                        <td style="text-align: right">
                            <asp:ImageButton ID="imgButtonAdd" runat="server" Height="17px" ImageUrl="~/htmls/image/new_f2.png"
                                Width="17px" OnClick="imgButtonAdd_Click" Visible="false" />
                            <asp:LinkButton ID="lnkThemMoi" runat="server" Font-Bold="False" OnClick="lnkThemMoi_Click" Visible="false">Thêm mới</asp:LinkButton>	&nbsp;
                            <asp:LinkButton ID="lnkMienGiam" runat="server" Font-Bold="False" OnClick="lnkMiemGiam_Click">Miễm giảm</asp:LinkButton>	&nbsp;
                            <asp:LinkButton ID="lnkNhapKhau" runat="server" Font-Bold="False" OnClick="lnkNhapKhau_Click">Nhập khẩu</asp:LinkButton>	&nbsp;
                            <asp:LinkButton ID="lnkSanXuatTN" runat="server" Font-Bold="False" OnClick="lnkSanXuatTN_Click">Sản xuất TN</asp:LinkButton>	&nbsp;
                            &nbsp;
                            <asp:ImageButton ID="imgBtnXoa" runat="server" Height="16px" ImageUrl="~/htmls/image/cancel_f2.png"
                                Width="16px" OnClick="imgBtnXoa_Click" />
                            <asp:LinkButton ID="lnkXoa" runat="server" OnClick="lnkXoa_Click" OnClientClick="if(GridIsChecked('gvHoSoCB')) { return confirm('Bạn có chắc chắn muốn xóa các hồ sơ đã chọn không?');} else {alert('Bạn chưa chọn Hồ sơ cần xóa.'); return false;}"
                                Font-Bold="False">Xóa</asp:LinkButton>
                            &nbsp;&nbsp;
                            <br />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvHoSoCB" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    EmptyDataText="Không có dữ liệu !" PageSize="30" Width="100%" DataKeyNames="ID,TrangThaiID,SoHoSo"
                    OnRowDataBound="gvHoSo_RowDataBound" OnPageIndexChanging="gvHoSo_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Số hồ sơ">
                            <ItemStyle Font-Underline="False" Width="150px" />
                            <ItemTemplate>
                                <a href="CN_HoSo_QuanLy.aspx?HoSoId=<%# Server.UrlEncode(Eval("ID").ToString()) %>&TrangThaiId=<%# Server.UrlEncode(Eval("TrangThaiID").ToString()) %>&UserControl=CB_HoSoDen" style="float:left"><%# Eval("SoHoSo") %></a>
                                <div runat="server" id="View" style="float: right;" visible="false">
                                    <a href="CB_HoSo_ChiTiet.aspx?id=<%# Eval("ID").ToString()%>">
                                        <img src="../htmls/image/sua.gif" alt="sửa" title="Sửa hồ sơ" border="0">
                                    </a>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TenTiengViet" HeaderText="Đơn vị nộp HS" />
                        <asp:BoundField DataField="DiaChi" HeaderText="Địa chỉ" Visible="False" />
                        <asp:BoundField DataField="DienThoai" HeaderText="Điện thoại">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Ng&#224;y nhận HS">
                            <ItemStyle Width="130px" />
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "NgayTiepNhan") == DBNull.Value? "" : ((DateTime)DataBinder.Eval(Container.DataItem, "NgayTiepNhan")).ToShortDateString()%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trạng th&#225;i">
                            <ItemTemplate>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="NguoiXuLy" HeaderText="Người xử l&#253;">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField Visible="False" DataField="CheckDaDoc" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkCheckAll" runat="server" onclick="HeaderClick('gvHoSoCB')" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCheck" runat="server" onclick="ChildClick('gvHoSoCB')" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
        </td>
    </tr>
</table>
