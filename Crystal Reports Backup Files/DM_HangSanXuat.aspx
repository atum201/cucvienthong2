<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="DM_HangSanXuat.aspx.cs" Inherits="WebUI_DM_HangSanXuat" Title="Danh mục hãng sản xuất" %>

<%@ Register Namespace="Fadrian.Web.Control" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin: 10px auto 10px 10px;">
        <strong>DANH MUC >> HÃNG SẢN XUẤT</strong>
    </div>
    <div align="center">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <div align="center">
                        <fieldset style="width: 97%">
                            <legend>Tìm kiếm</legend>
                            <table align="center" border="0" width="100%">
                                <tr>
                                    <td align="right">
                                        Tên hãng sản xuất</td>
                                    <td align="left" colspan="2">
                                        <asp:TextBox ID="txtTen" runat="server" Width="365px"></asp:TextBox>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td style="text-align: left">
                                        <asp:Button ID="btnTimKiem" runat="server" OnClick="btnTimKiem_Click" Text="Tìm kiếm" /></td>
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
                    <div align="center">
                        <fieldset style="width: 97%">
                            <legend>Danh mục hãng sản xuất</legend>
                            <table border="0" width="100%">
                                <tr>
                                    <td style="width: 60%">
                                        &nbsp;</td>
                                    <td align="right" colspan="3" style="text-align: right">
                                        <asp:ImageButton ID="imgBtnThemMoi" runat="server" AlternateText="Thêm mới" Height="16px"
                                            ImageUrl="~/htmls/image/new_f2.png" OnClientClick="popCenter('DM_HangSanXuat_ChiTiet.aspx','DM_HangSanXuat_ChiTiet',570,280);return false;"
                                            PostBackUrl="~/WebUI/DM_HangSanXuat_ChiTiet.aspx" Width="16px" />
                                        <asp:LinkButton ID="lnkThemMoi" runat="server" OnClientClick="popCenter('DM_HangSanXuat_ChiTiet.aspx','DM_HangSanXuat_ChiTiet',570,280);return false;">Thêm mới</asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="lnkXoa" runat="server" OnClick="lnkXoa_Click" OnClientClick="if(GridIsChecked('gvHangSanXuat')) { return confirm('Bạn có chắc chắn muốn xóa các hãng sản xuất đã chọn không?');} else {alert('Bạn chưa chọn hãng sản xuất cần xóa.'); return false;}"><img src="../htmls/image/cancel_f2.png" border="0" alt="" width="16px" />&nbsp;Xóa</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" valign="top">
                                        <asp:LinkButton ID="lnkRefresh" runat="server" OnClick="lnkRefresh_Click"></asp:LinkButton>
                                        <cc1:PagingGridView ID="gvHangSanXuat" runat="server" AllowPaging="true" AutoGenerateColumns="False"
                                            DataKeyNames="ID,TenHangSanXuat" EmptyDataText="Không có dữ liệu !" OnDataBound="gvHangSanXuat_DataBound"
                                            OnPageIndexChanging="gvHangSanXuat_PageIndexChanging" PageSize="15" VirtualItemCount="-1"
                                            Width="100%" OnSorting="gvHangSanXuat_Sorting" AllowMultiColumnSorting="true">
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="M&#227; " Visible="False" />
                                                <asp:TemplateField HeaderText="T&#234;n h&#227;ng sản xuất" SortExpression="TenHangSanXuat">
                                                    <itemstyle horizontalalign="Left" width="49%" />
                                                    <itemtemplate>
                                                    <a href="#" onclick="return popCenter('DM_HangSanXuat_ChiTiet.aspx?Ma=<%# Server.UrlEncode(Eval("ID").ToString()) %>','DM_HangSanXuat_ChiTiet',570,280);">
                                                        <%# Eval("TenHangSanXuat")%>
                                                    </a>
                                                
</itemtemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="TenTiengAnh" HeaderText="T&#234;n tiếng anh" SortExpression="TenTiengAnh">
                                                    <itemstyle horizontalalign="Left" width="48%" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <headertemplate>
                                                    <asp:CheckBox ID="chkCheckAll" runat="server" />
                                                
</headertemplate>
                                                    <itemstyle width="1px" horizontalalign="Center" />
                                                    <headerstyle width="1px" horizontalalign="Center" />
                                                    <itemtemplate>
                                                    <asp:CheckBox ID="chkCheck" runat="server" />
                                                
</itemtemplate>
                                                </asp:TemplateField>
                                            </Columns>
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
    </div>
</asp:Content>
