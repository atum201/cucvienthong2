<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="DM_PhongBan.aspx.cs" Inherits="WebUI_DM_PhongBan" Title="Danh mục phòng ban"
    Theme="Default" %>
<%@ Register Namespace="Fadrian.Web.Control" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin: 10px auto 10px 10px;">
        <strong>DANH MUC >> PHÒNG BAN</strong>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <div align="center">
                    <fieldset style="width: 97%">
                        <legend>Danh mục phòng ban</legend>
                        <table border="0" width="100%">
                            <tr>
                                <td colspan="4" style="text-align: right;">
                                    &nbsp;<asp:ImageButton ID="imgbtnThemMoi" runat="server" Height="16px" ImageUrl="~/htmls/image/new_f2.png"
                                        Width="16px" OnClientClick="popCenter('DM_PhongBan_ChiTiet.aspx','DanhMucPhongBan', 600, 300); return false;" />&nbsp;<asp:LinkButton
                                            ID="lnkThemMoi" runat="server" OnClientClick="popCenter('DM_PhongBan_ChiTiet.aspx','DanhMucPhongBan', 600, 300); return false;">Thêm mới</asp:LinkButton>
                                    &nbsp;<asp:ImageButton ID="imgbtnXoa" runat="server" Height="16px" ImageUrl="~/htmls/image/cancel_f2.png"
                                        Width="16px" OnClick="imgbtnXoa_Click" OnClientClick="if(GridIsChecked('gvPhongBan')) { return confirm('Bạn có chắc chắn muốn xóa các phòng ban đã chọn không?');} else {alert('Bạn chưa chọn phòng ban cần xóa.'); return false;}" />
                                    <asp:LinkButton ID="lnkXoa" runat="server" OnClick="btnXoa_Click" OnClientClick="if(GridIsChecked('gvPhongBan')) { return confirm('Bạn có chắc chắn muốn xóa các phòng ban đã chọn không?');} else {alert('Bạn chưa chọn phòng ban cần xóa.'); return false;}">Xóa</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" valign="top">
                                    <cc1:PagingGridView id="gvPhongBan" runat="server" autogeneratecolumns="False" emptydatatext="Không có dữ liệu !"
                                        width="100%" ondatabound="gvPhongBan_DataBound" datakeynames="ID,TenPhongBan" AllowMultiColumnSorting="true" OnSorting="gvPhongBan_Sorting" AllowSorting="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="T&#234;n Ph&#242;ng Ban" SortExpression="TenPhongBan">
                                                <ItemTemplate>
                                                    <a href="#" onclick="return popCenter('DM_PhongBan_ChiTiet.aspx?id=<%# Server.UrlEncode(Eval("ID").ToString()) %>','DM_PhongBan_ChiTiet',800,250);"><%# Eval("TenPhongBan")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TruongPhong" HeaderText="Trưởng ph&#242;ng"  SortExpression="TruongPhong"/>                                        
                                            <asp:TemplateField>
                                                <HeaderStyle HorizontalAlign="Center" Width="10px" />
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkCheckAll" runat="server" onclick="HeaderClick('gvPhongBan')" />
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCheck" runat="server" onclick="ChildClick('gvPhongBan')" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                    </Columns>
                                    </cc1:PagingGridView>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
