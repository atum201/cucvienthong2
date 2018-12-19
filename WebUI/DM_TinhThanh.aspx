<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true" CodeFile="DM_TinhThanh.aspx.cs" Inherits="WebUI_DM_TinhThanh" Title="Danh mục tỉnh thành"  Theme="Default"%>
<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="margin: 10px auto 10px 10px;">
        <strong>DANH MUC >> TỈNH THÀNH</strong>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <div align="center">
                  <div class="tieude"> &nbsp;</div>
                    <fieldset style="width: 97%">
                       
                        <legend>Tìm kiếm</legend>
                        <table align="center" border="0" width="100%">
                            <tr>
                                <td align="right" style="width: 200px; height: 26px">
                                    Đơn vị quản lý</td>
                                <td align="left" colspan="3" style="height: 26px">
                                    <cc1:ComboBox ID="DropDownList1" runat="server" Width="272px">                                        
                                    </cc1:ComboBox></td>
                            </tr>
                            <tr>
                                <td align="right" style="height: 26px; width: 200px;">
                                    Mã tỉnh thành</td>
                                <td align="left" colspan="3" style="height: 26px">
                                    <asp:TextBox ID="txtMaTT" runat="server" Width="270px"></asp:TextBox>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right" style="height: 26px">
                                    Tên tỉnh thành</td>
                                <td align="left" colspan="3" style="height: 26px">
                                    <asp:TextBox ID="txtTenTT" runat="server" Width="270px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td style="width: 162px; text-align: left">
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
                                <td align="right" colspan="4">
                                    &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" Height="16px" ImageUrl="~/htmls/image/new_f2.png"
                                        Width="16px" PostBackUrl="~/WebUI/DM_TinhThanh_ChiTiet.aspx" />&nbsp;<asp:LinkButton
                                            ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Thêm mới</asp:LinkButton>
                                    &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" Height="16px" ImageUrl="~/htmls/image/cancel_f2.png"
                                        Width="16px" />
                                    <asp:LinkButton ID="btnXoa" runat="server" >Xóa</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td colspan="4" valign="top">
                                    <asp:GridView ID="gvTinhThanh" runat="server" AutoGenerateColumns="False" EmptyDataText="Không có dữ liệu !"
                                        Width="100%" AllowPaging="True" PageSize="15">
                                        <Columns>
                                            <asp:TemplateField>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </EditItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkCheckAll" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="MaTT" HeaderText="M&#227; tỉnh th&#224;nh" />
                                            <asp:BoundField DataField="TenTinhThanh" HeaderText="T&#234;n tỉnh th&#224;nh" />
                                            <asp:BoundField DataField="TenTrungTam" HeaderText="Đơn vị quản l&#253;" />
                                            <asp:BoundField DataField="GhiChu" HeaderText="Ghi ch&#250;" />
               					 <asp:TemplateField HeaderText="Chi tiết">
                                                <ItemTemplate>
                                                    <a href="#" onclick="return popCenter('DM_TinhThanh_ChiTiet.aspx?Ma=<%# Server.UrlEncode(Eval("MaTT").ToString()) %>','DM_TinhThanh_ChiTiet',600,150);">Xem/Sửa</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>   
                                            <asp:BoundField DataField="MaTrungTam" Visible="False" />
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

