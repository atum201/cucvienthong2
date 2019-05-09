<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true" CodeFile="DB_BaoCaoDongBo.aspx.cs" Inherits="WebUI_DB_BaoCaoDongBo1" Title="Báo cáo đồng bộ" Theme="Default"%>
<%@ Register Namespace="Fadrian.Web.Control" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div style="margin: 10px auto 10px 10px;">
        <strong>ĐỒNG BỘ >> BÁO CÁO ĐỒNG BỘ</strong>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td >         
                <div align="center">
                <fieldset style="width: 97%">
                        <legend>Kết quả đồng bộ</legend>
                        <table border="0" width="100%">
                            <tr>
                                <td colspan="4" valign="top">
                                    &nbsp;<cc1:PagingGridView ID="gvDongBo" runat="server" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="Không có dữ liệu !"
                                        OnPageIndexChanging="gvDongBo_PageIndexChanging" PageSize="15" VirtualItemCount="-1"
                                        Width="100%" AllowMultiColumnSorting="True" OnSorting="gvDongBo_Sorting" AllowSorting="True">
                                        <Columns>
                                            <asp:BoundField DataField="NgayCapNhatSauCung" HeaderText="Thời gian" SortExpression="NgayCapNhatSauCung"/>                                            
                                            <asp:BoundField DataField="TenTrungTam" HeaderText="Đơn vị" SortExpression="TenTrungTam"/> 
                                            <asp:BoundField DataField="TrangThai" HeaderText="Kết quả" />
                                        </Columns>
                                        <PagerSettings Mode="NumericFirstLast" />
                                    </cc1:PagingGridView>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    
                    <fieldset style="width: 98%; text-align: left">
                                    <asp:Button ID="btnBoQua" runat="server" Text="Bỏ qua" Width="77px" /></fieldset>
                </div>
            </td>
        </tr>
        <tr>
            <td style="height: 19px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 272px">
                <div align="center">
                    &nbsp;</div>
            </td>
        </tr>
    </table>
</asp:Content>

