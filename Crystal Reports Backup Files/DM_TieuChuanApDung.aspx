<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="DM_TieuChuanApDung.aspx.cs" Inherits="WebUI_DM_TieuChuanApDung" Title="Danh mục tiêu chuẩn"
    Theme="Default" %>

<%@ Register Namespace="Fadrian.Web.Control" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin: 10px auto 10px 10px;">
        <strong>DANH MUC >> TIÊU CHUẨN ÁP DỤNG</strong>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <div align="center">               
                    <fieldset style="width: 97%">
                        <legend>Tìm kiếm</legend>
                        <table align="center" border="0" width="100%">
                            <tr>
                                <td align="right" style="height: 26px; width: 200px;">
                                    Mã tiêu chuẩn</td>
                                <td align="left" colspan="2" style="height: 26px; text-align: left">
                                    <asp:TextBox ID="txtMaTC" runat="server" Width="365px" MaxLength="50"></asp:TextBox>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Tên tiêu chuẩn</td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtTenTieuChuan" runat="server" Width="365px" MaxLength="255"></asp:TextBox>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: left">
                                    <asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" OnClick="btnTimKiem_Click" /></td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <div align="center">
                    <fieldset style="width: 97%">
                        <legend>Danh mục</legend>
                        <table border="0" width="100%">
                            <tr>
                                <td align="right" colspan="4" style="text-align: right">
                                    &nbsp;<asp:ImageButton ID="imgThemMoi" runat="server" Height="16px" ImageUrl="~/htmls/image/new_f2.png"
                                        Width="16px" OnClientClick="popCenter('DM_TieuChuanApDung_ChiTiet.aspx','DM_TieuChuanApDung_ChiTiet',800,250); return false;" />&nbsp;<asp:LinkButton
                                            ID="lnkThemMoi" runat="server" OnClientClick="popCenter('DM_TieuChuanApDung_ChiTiet.aspx','DM_TieuChuanApDung_ChiTiet',800,250); return false;">Thêm mới</asp:LinkButton>
                                    &nbsp;<asp:LinkButton ID="lnkXoa" runat="server" OnClick="btnXoa_Click" OnClientClick="if(GridIsChecked('gvTieuChuan')) { return confirm('Bạn có chắc chắn muốn xóa các tiêu chuẩn đã chọn không?');} else {alert('Bạn chưa chọn tiêu chuẩn cần xóa.'); return false;}"><img src="../htmls/image/cancel_f2.png" border="0" alt="" width="16px" />&nbsp;Xóa</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td colspan="4" valign="top">
                                    <cc1:PagingGridView ID="gvTieuChuan" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        DataKeyNames="ID,TenTieuChuan" EmptyDataText="Không có dữ liệu !" OnDataBound="gvTieuChuan_DataBound"
                                        OnPageIndexChanging="gvTieuChuan_PageIndexChanging" PageSize="15" VirtualItemCount="-1"
                                        AllowPaging="True" Width="100%" AllowMultiColumnSorting="True" OnSorting="gvTieuChuan_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="T&#234;n Ti&#234;u Chuẩn" SortExpression="TenTieuChuan">
                                                <itemtemplate>
                                                    <a href="#" onclick="return popCenter('DM_TieuChuanApDung_ChiTiet.aspx?Ma=<%# Server.UrlEncode(Eval("ID").ToString()) %>','DM_TieuChuanApDung_ChiTiet',800,250);"><%# Eval("TenTieuChuan").ToString()%></a>
                                                
                                                
</itemtemplate>
                                                <controlstyle width="40%" />
                                            </asp:TemplateField>
                                             <asp:BoundField DataField="TenTiengAnh" HeaderText="T&#234;n tiếng anh" SortExpression="TenTiengAnh">
                                                <controlstyle width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MaTieuChuan" HeaderText="M&#227; TC" SortExpression="MaTieuChuan">
                                                <controlstyle width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="GhiChu" HeaderText="Ghi ch&#250;" SortExpression="GhiChu">
                                                <controlstyle width="30%" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <headertemplate>
                                                    <asp:CheckBox ID="chkCheckAll" runat="server" />                                                
                                                
</headertemplate>
                                                <itemtemplate>
                                                    <asp:CheckBox ID="chkCheck" runat="server" />                                                
                                                
</itemtemplate>
                                                <headerstyle cssclass="unsortable" width="1px" />
                                                <itemstyle horizontalalign="Justify" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="sortbottom" />
                                        <PagerStyle CssClass="sortbottom" />
                                        <PagerSettings Mode="NumericFirstLast" />
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
