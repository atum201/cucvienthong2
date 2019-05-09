<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true" CodeFile="DM_KhachHang.aspx.cs" Inherits="WebUI_DM_ToChuc" Title="Danh mục khách hàng"  Theme ="Default"%>
<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td >
                <div align="center"><div class="tieude"> &nbsp;</div>
                    <fieldset style="width: 98%" >
                       
                        <legend>Tìm kiếm</legend>
                        <table align="center" border="0" width="100%">
                            <tr>
                                <td align="right" style="height: 26px; text-align: right; width: 200px;">
                                    Mã tổ chức</td>
                                <td align="left" style="width: 204px; height: 26px">
                                    <asp:TextBox ID="txtMaTC" runat="server" Width="237px" BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana, helvetica, sans-serif" Font-Size="13px"></asp:TextBox></td>
                                <td align="right" style="height: 26px; text-align: right;">
                                    Tỉnh thành</td>
                                <td align="left" style="height: 26px;">
                                    <cc1:ComboBox ID="cbTinhThanh" runat="server">
                                        <asp:ListItem>H&#224; Nội</asp:ListItem>
                                        <asp:ListItem>Hồ CH&#237; Minh</asp:ListItem>
                                        <asp:ListItem>Đ&#224; Nẵng</asp:ListItem>
                                    </cc1:ComboBox></td>
                            </tr>
                            <tr>
                                <td style="height: 26px; text-align: right">
                                    Tên tổ chức</td>
                                <td style="width: 204px;">
                                    <asp:TextBox ID="txtTenKH" runat="server" Width="237px"></asp:TextBox></td>
                                <td style="text-align: right">
                                    Địa chỉ</td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtDiaChi" runat="server" Width="213px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    </td>
                                <td style="width: 204px; text-align: left;">
                                    <asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" /></td>
                                <td>
                                    &nbsp;</td>
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
               </td>
        </tr>
        <tr>
            <td >
                <div align="center">
                    <fieldset style="width: 97%">
                        <legend>Danh mục</legend>
                        <table border="0" width="100%">
                            <tr>
                                <td style="width: 80%">
                                    &nbsp;</td>
                                <td colspan="3" align="right">
                                    <asp:ImageButton ID="ImageButton2" runat="server" Height="16px" ImageUrl="~/htmls/image/new_f2.png"
                                        Width="16px" PostBackUrl="~/WebUI/DM_KhachHang_ChiTiet.aspx" AlternateText="Thêm mới" />
                                    <asp:LinkButton
                                            ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Thêm mới</asp:LinkButton>&nbsp;
                                   <asp:ImageButton ID="ImageButton1" runat="server" Height="16px" ImageUrl="~/htmls/image/cancel_f2.png"
                                        Width="16px" />
                                    <asp:LinkButton ID="btnXoa" runat="server">Xóa</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td colspan="4" valign="top">
                                    <asp:GridView ID="gvKhachHang" runat="server" AutoGenerateColumns="False" EmptyDataText="Không có dữ liệu !"
                                        Width="100%" AllowPaging="True" PageSize="15">
                                        <Columns>
                                            <asp:TemplateField>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkCheckAll" runat="server" />
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="MaKH" HeaderText="M&#227; tổ chức" />
                                            <asp:BoundField DataField="TenKH" HeaderText="T&#234;n tổ chức" />
                                            <asp:BoundField DataField="Mail" HeaderText="Mail" />
                                            <asp:BoundField DataField="TinhThanh" HeaderText="Tỉnh th&#224;nh" />
                                            <asp:TemplateField HeaderText="Chi tiết">
                                                <ItemTemplate>
                                                    <a href="#" onclick="return popCenter('DM_KhachHang_ChiTiet.aspx?Ma=<%# Server.UrlEncode(Eval("MaKH").ToString()) %>','DM_HangSanXuat_ChiTiet',800,250);">Xem/Sửa</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>   
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                       
                    </fieldset>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

