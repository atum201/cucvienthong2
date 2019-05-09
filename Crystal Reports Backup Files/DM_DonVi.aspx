<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="DM_DonVi.aspx.cs" Inherits="WebUI_DM_DonVi" Title="Danh mục đơn vị"
    Theme="Default" %>

<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc2" %>
<%@ Register Namespace="Fadrian.Web.Control" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin: 10px auto 10px 10px;">
        <strong>DANH MUC >> ĐƠN VỊ</strong>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                <fieldset style="width: 97%">
                    <legend>Tìm kiếm</legend>
                    <table align="center" border="0" width="100%">
                        <tr>
                            <td style="width: 120px; text-align: left">
                                Mã đơn vị</td>
                            <td align="right" style="text-align: left">
                                <asp:TextBox ID="txtMaDonVi" runat="server" Width="400px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 120px; text-align: left">
                                Tên đơn vị</td>
                            <td align="right" style="text-align: left">
                                <asp:TextBox ID="txtTenDonVi" runat="server" Width="400px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 120px; text-align: left">
                                Tỉnh thành phố</td>
                            <td align="right" style="text-align: left">
                                <cc2:ComboBox ID="ddlTinhThanh" runat="server" Width="300px">
                                </cc2:ComboBox></td>
                        </tr>
                        <tr>
                            <td style="width: 120px; text-align: left; height: 26px;">
                                &nbsp;</td>
                            <td style="text-align: left; height: 26px;">
                                <asp:Button ID="btnTimKiem" runat="server" OnClick="btnTimKiem_Click" Text="Tìm kiếm" /></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td align="center">
                <fieldset style="width: 97%">
                    <legend>Danh mục</legend>
                    <table border="0" width="100%">
                        <tr>
                            <td colspan="4" style="text-align: right">
                                &nbsp;<asp:ImageButton ID="imgAdd" runat="server" AlternateText="Thêm mới" Height="16px"
                                    ImageUrl="~/htmls/image/new_f2.png" OnClientClick="popCenter('DM_DonVi_ChiTiet.aspx','DM_HangSanXuat_ChiTiet',800,250);return false;"
                                    Width="16px" />
                                <asp:LinkButton ID="lnkThemMoi" runat="server" OnClientClick="popCenter('DM_DonVi_ChiTiet.aspx','DM_HangSanXuat_ChiTiet',800,250);return false;">Thêm mới</asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="lnkXoa" runat="server" OnClick="lnkXoa_Click" OnClientClick="if(GridIsChecked('gvDonVi')) { return confirm('Bạn có chắc chắn muốn xóa các đơn vị đã chọn không?');} else {alert('Bạn chưa chọn đơn vị cần xóa.'); return false;}"><img src="../htmls/image/cancel_f2.png" border="0" alt="" width="16px" />&nbsp;Xóa</asp:LinkButton></td>
                        </tr>
                        <tr>
                            <td colspan="4" valign="top">
                                <cc1:PagingGridView ID="gvDonVi" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    DataKeyNames="ID,TenTiengViet" EmptyDataText="Không có dữ liệu !" OnDataBound="gvDonVi_DataBound"
                                    OnPageIndexChanging="gvDonVi_PageIndexChanging" PageSize="15" VirtualItemCount="-1"
                                    Width="100%" AllowPaging="True" AllowMultiColumnSorting="True" OnSorting="gvDonVi_Sorting">
                                    <Columns>
                                        <asp:BoundField DataField="OSTT" HeaderText="STT" Visible="False" />
                                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                                        <asp:TemplateField HeaderText="T&#234;n tiếng Việt" SortExpression="TenTiengViet">
                                            <itemtemplate>
                                                    <a href="#" onclick="popCenter('DM_DonVi_ChiTiet.aspx?ID=<%# Server.UrlEncode(Eval("ID").ToString()) %>','DM_DonVi_ChiTiet',800,250); return false;"><%# Eval("TenTiengViet")%></a>
                                            </itemtemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TenTiengAnh" HeaderText="T&#234;n tiếng Anh" SortExpression="TenTiengAnh" />
                                        <%--LongHH--%>
                                        <asp:BoundField DataField="MaDonVi" HeaderText="M&#227; đơn vị" SortExpression="MaDonVi" />
                                        <%--<asp:TemplateField HeaderText="M&#227; đơn vị" SortExpression="MaDonVi">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%--LongHH--%>
                                        <asp:BoundField DataField="TenVietTat" HeaderText="T&#234;n viết tắt" Visible="False" />
                                        <asp:BoundField DataField="MaSoThue" HeaderText="M&#227; số thuế" SortExpression="MaSoThue" />
                                        <asp:BoundField DataField="DiaChi" HeaderText="Địa chỉ" SortExpression="DiaChi" />
                                        <asp:BoundField DataField="TinhThanhID" HeaderText="TinhThanhID" Visible="False" />
                                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                        <asp:BoundField DataField="MatKhau" HeaderText="Mật khẩu" Visible="False" />
                                        <asp:BoundField DataField="DienThoai" HeaderText="Điện thoại" SortExpression="DienThoai" />
                                        <asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="Fax" />
                                        <asp:BoundField DataField="NgayCapNhatSauCung" HeaderText="NgayCapNhatSauCung" Visible="False" />
                                        <asp:BoundField DataField="DaDongBo" HeaderText="DaDongBo" Visible="False" />
                                        <asp:TemplateField>
                                            <headertemplate>
                                                    <asp:CheckBox ID="chkCheckAll" runat="server" />                                                
                                            </headertemplate>
                                            <itemtemplate>
                                                    <asp:CheckBox ID="chkCheck" runat="server" />
                                            </itemtemplate>
                                            <headerstyle width="1px" />
                                            <itemstyle width="1px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings Mode="NumericFirstLast" />
                                </cc1:PagingGridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align:left">
                                <asp:Button ID="btnExcel" runat="server" Text="Xuất ra file Excel" OnClick="btnExcel_Click" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
