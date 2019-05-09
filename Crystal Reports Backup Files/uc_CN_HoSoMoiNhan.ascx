<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_CN_HoSoMoiNhan.ascx.cs"
    Inherits="UserControls_uc_CN_HoSoMoiNhan" %>
<strong>CHỨNG NHẬN >> DANH SÁCH HỒ SƠ ĐẾN
    <br />
</strong>
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
                                Width="17px" OnClick="imgButtonAdd_Click" />
                            <asp:LinkButton ID="lnkThemMoi" runat="server" Font-Bold="False" OnClick="lnkThemMoi_Click">Thêm mới</asp:LinkButton>
                            &nbsp;
                            <asp:ImageButton ID="ImageButton1" runat="server" Height="17px" ImageUrl="~/htmls/image/new_f2.png"
                                Width="17px" OnClick="imgButtonAddVB_Click" />
                            <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="False" OnClick="lnkThemMoiVB_Click">Thêm mới VB</asp:LinkButton>
                            &nbsp;
                            <asp:ImageButton ID="ImgBtnXoa" runat="server" Height="16px" ImageUrl="~/htmls/image/cancel_f2.png"
                                Width="16px" OnClick="ImgBtnXoa_Click" />
                            <asp:LinkButton ID="lnkXoa" runat="server" OnClick="lnkXoa_Click" OnClientClick="if(GridIsChecked('gvHoSo')) { return confirm('Bạn có chắc chắn muốn xóa các hồ sơ đã chọn không?');} else {alert('Bạn chưa chọn Hồ sơ cần xóa.'); return false;}"
                                Font-Bold="False">Xóa</asp:LinkButton>
                            &nbsp;&nbsp;
                            <br />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvHoSo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    EmptyDataText="Không có dữ liệu !" PageSize="30" Width="100%" DataKeyNames="ID,TrangThaiID,SoHoSo"
                    OnRowDataBound="gvHoSo_RowDataBound" OnPageIndexChanging="gvHoSo_PageIndexChanging"
                    AllowSorting="True">
                    <Columns>
                        <asp:TemplateField HeaderText="Số hồ sơ">
                            <ItemStyle Font-Underline="False" Width="160px" />
                            <ItemTemplate>
                                <a href="CN_HoSo_QuanLy.aspx?HoSoId=<%# Server.UrlEncode(Eval("ID").ToString()) %>&TrangThaiId=<%# Server.UrlEncode(Eval("TrangThaiID").ToString()) %>&UserControl=CN_HoSoDen"
                                    style="float: left">
                                    <%# Eval("SoHoSo") %>
                                </a>
                                <div runat="server" id="View" style="float: right;" visible="false">
                                    <a href="CN_HoSo_ChiTiet.aspx?id=<%# Server.UrlEncode(Eval("ID").ToString())%>&TrangThaiId=<%# Server.UrlEncode(Eval("TrangThaiID").ToString())%>&UserControl=CN_HoSoDen">
                                        <img src="../htmls/image/sua.gif" alt="sửa" title="Sửa hồ sơ" border="0">
                                    </a>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TenTiengViet" HeaderText="Đơn vị nộp HS" />
                        <asp:BoundField DataField="DiaChi" HeaderText="Địa chỉ" Visible="false" />
                        <asp:BoundField DataField="DienThoai" HeaderText="Điện thoại">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Ngày nhận HS">
                            <ItemStyle Width="130px" />
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "NgayTiepNhan") == DBNull.Value? "" : ((DateTime)DataBinder.Eval(Container.DataItem, "NgayTiepNhan")).ToShortDateString()%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nguồn gốc" Visible="false">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "NguonGocID") == DBNull.Value? "" : Cuc_QLCL.Entities.EntityHelper.GetEnumTextValue((Cuc_QLCL.Entities.EnNguonGocList)Convert.ToInt32(DataBinder.Eval(Container.DataItem, "NguonGocID"))) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trạng thái" ItemStyle-Width="100px">
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NguoiXuLy" HeaderText="Người xử lý" ItemStyle-Width="100px" />
                        <asp:BoundField Visible="false" DataField="CheckDaDoc" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkCheckAll" runat="server" onclick="HeaderClick('gvHoSo')" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCheck" runat="server" onclick="ChildClick('gvHoSo')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
        </td>
    </tr>
</table>
