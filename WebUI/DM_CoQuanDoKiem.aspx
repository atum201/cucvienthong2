<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true" CodeFile="DM_CoQuanDoKiem.aspx.cs" Inherits="WebUI_DM_CoQuanDoKiem" Title="Danh mục cơ quan đo kiểm" %>


<%@ Register Namespace="Fadrian.Web.Control" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="margin: 10px auto 10px 10px;">
        <strong>DANH MUC >> CƠ QUAN ĐO KIỂM</strong>
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
                                    Tên cơ quan đo kiểm</td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtTenCQDK" runat="server" Width="365px" MaxLength="255"></asp:TextBox>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Tên tiếng anh</td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtTenTiengAnh" runat="server" MaxLength="255" Width="365px"></asp:TextBox></td>
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
            <td >
                <div align="center">
                    <fieldset style="width: 97%">
                        <legend>Danh mục cơ quan đo kiểm</legend>
                        <table border="0" width="100%">
                            <tr>
                                <td style="width: 80%; height: 21px;">
                                    &nbsp;</td>
                                <td colspan="3" align="right" style="height: 21px; text-align: right;">
                                    <asp:ImageButton ID="imgThemMoi" runat="server" AlternateText="Thêm mới" Height="16px"
                                        ImageUrl="~/htmls/image/new_f2.png" OnClientClick="popCenter('DM_CoQuanDoKiem_ChiTiet.aspx','DM_CoQuanDoKiem_ChiTiet',600,280);return false;"
                                        PostBackUrl="~/WebUI/DM_CoQuanDoKiem_ChiTiet.aspx" Width="16px" />
                                    <asp:LinkButton ID="lnkThemMoi" runat="server" OnClientClick="popCenter('DM_CoQuanDoKiem_ChiTiet.aspx','DM_CoQuanDoKiem_ChiTiet',600,280);return false;">Thêm mới</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="lnkXoa" runat="server" OnClick="lnkXoa_Click" OnClientClick="if(GridIsChecked('gvCoQuanDK')) { return confirm('Bạn có chắc chắn muốn xóa cơ quan đo kiểm đã chọn không?');} else {alert('Bạn chưa chọn cơ quan đo kiểm cần xóa.'); return false;}"><img src="../htmls/image/cancel_f2.png" border="0" alt="" width="16px" />&nbsp;Xóa</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" valign="top">
                                    <asp:LinkButton ID="lnkRefresh" runat="server"></asp:LinkButton>
                                    <cc1:PagingGridView ID="gvCoQuanDK" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="ID,TenCoQuanDoKiem" EmptyDataText="Không có dữ liệu !" OnDataBound="gvCoQuanDK_DataBound"
                                        OnPageIndexChanging="gvCoQuanDK_PageIndexChanging" PageSize="15" VirtualItemCount="-1"
                                        Width="100%" AllowMultiColumnSorting="True" OnSorting="gvCoQuanDK_Sorting" AllowSorting="True">
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="M&#227; " Visible="False" />
                                            <asp:TemplateField HeaderText="T&#234;n cơ quan đo kiểm" SortExpression="TenCoQuanDoKiem">
                                                <ItemTemplate>
                                                    <a href="#" onclick="return popCenter('DM_CoQuanDoKiem_ChiTiet.aspx?Ma=<%# Server.UrlEncode(Eval("ID").ToString()) %>','DM_CoQuanDoKiem_ChiTiet',600,250);">
                                                        <%# Eval("TenCoQuanDoKiem")%>
                                                    </a>
                                                
</ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TenTiengAnh" HeaderText="T&#234;n tiếng anh" SortExpression="TenTiengAnh" />
                                            <asp:BoundField DataField="DiaChi" HeaderText="Địa chỉ" SortExpression="DiaChi"/>                                            
                                            <asp:BoundField DataField="dienthoai" HeaderText="Số điện thoại" SortExpression="dienthoai"/> 
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkCheckAll" runat="server" />
                                                
</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCheck" runat="server" />
                                                
</ItemTemplate>
                                                <HeaderStyle Width="1px" />
                                                <ItemStyle Width="1px" />
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

