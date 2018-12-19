<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true" CodeFile="HT_QuanLySaoLuuPhucHoi.aspx.cs" Inherits="WebUI_HT_QuanLySaoLuuPhucHoi" Title="Quản lý sao lưu, phục hồi" Theme="Default"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div style="margin: 10px auto 10px 10px;">
        <strong>HỆ THỐNG >> SAO LƯU PHỤC HỒI</strong>
    </div>
    
    <table width="100%">
        <tr>
            <td colspan="3" style="height: 11px">
                <span class="title"><strong>NHẬT KÝ SAO LƯU HỆ THỐNG</strong></span></td>
        </tr>
        <tr>
            <td colspan="3" style="height: 11px">
                <asp:GridView ID="gvNhatKySaoLuu" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="None" OnDataBound="gvNhatKySaoLuu_DataBound"
                    OnPageIndexChanging="gvNhatKySaoLuu_PageIndexChanging" OnRowCommand="gvNhatKySaoLuu_RowCommand"
                    PageSize="15" Width="100%">
                    <FooterStyle BackColor="#507CD1" BorderColor="#507CD1" BorderStyle="Solid" Font-Bold="True"
                        ForeColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAll" runat="server" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="30px"/>
                            <HeaderStyle HorizontalAlign="Left"  Width="30px"/>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCheck" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="BackupDate" HeaderText="Ng&#224;y sao lưu"
                            HtmlEncode="False">
                            <ItemStyle HorizontalAlign="Left" Width="170px" />
                            <HeaderStyle HorizontalAlign="Left" Width="170px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BackupFile" HeaderText="T&#234;n file">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:ButtonField CommandName="Restore" Text="Phục hồi">
                            <ControlStyle ForeColor="Navy" />
                            <ItemStyle Width="70px" />
                            <HeaderStyle Width="70px" />
                        </asp:ButtonField>
                    </Columns>
                    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnXoa" runat="server" Text="Xoá" Width="70px" OnClick="btnXoa_Click" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa các file backup đã chọn không?');"/>&nbsp;
    <asp:Button ID="btnLapLichDongBo" runat="server" PostBackUrl="~/WebUI/HT_SaoLuu_CauHinh.aspx"
        Text="Lập lịch sao lưu" Width="115px" />&nbsp;
    <asp:Button ID="btnSaoLuuBangTay" runat="server" Text="Sao lưu bằng tay" Width="115px" OnClick="btnSaoLuuBangTay_Click" />
</asp:Content>

