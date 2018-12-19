<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="HT_NguoiDung_QuanLy.aspx.cs" Inherits="WebUI_HT_NguoiDung_QuanLy" Title="Quản lý người dùng"
    Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    function CheckDelete(edit){
        var lst=  document.getElementById("<%= lstDanhSachNguoiDung.ClientID %>");
        if(lst.selectedIndex>=0){
            if(edit==1)
                return true;
            else            
                return confirm('Bạn có muốn xóa người dùng đã chọn?');
        }else{
           alert('Bạn Chưa chọn người dùng'); 
           return false;           
        }
    }

    </script>

    <div style="margin: 10px auto 10px 10px;">
        <strong>HỆ THỐNG >> QUẢN LÝ NGƯỜI DÙNG</strong>
    </div>
    <table width="100%" style="text-align: left">
        <tr>
            <td class="title" colspan="4" style="text-align: left; width: 30%;">
            </td>
        </tr>
        <tr>
            <td align="right" valign="top" style="width: 30%">
            </td>
            <td colspan="2" valign="top" style="width: 30%; text-align: left">
            </td>
            <td align="right" valign="top" style="width: 30%">
                <asp:ImageButton ID="imgbtnVaiTro" runat="server" Enabled="False" ImageUrl="../htmls/image/nguoidung.gif" /><asp:LinkButton
                    ID="lnkQuanLyVaiTro" runat="server" Font-Bold="True" PostBackUrl="~/WebUI/HT_VaiTro_QuanLy.aspx">Quản lý vai trò</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="width: 30%;" valign="top">
                <strong>Danh sách người dùng </strong>
            </td>
            <td align="left" colspan="2" style="width: 30%; text-align: left;" valign="top">
                <strong>Danh sách vai trò không được cấp </strong>
            </td>
            <td align="left" style="width: 30%;" valign="top">
                <strong>Danh sách vai trò được cấp</strong>
            </td>
        </tr>
        <tr>
            <td style="width: 30%" valign="top">
                <asp:ListBox ID="lstDanhSachNguoiDung" runat="server" Height="400px" Width="100%"
                    OnSelectedIndexChanged="lstDanhSachNguoiDung_SelectedIndexChanged" AutoPostBack="True">
                </asp:ListBox></td>
            <td style="width: 30%; text-align: left;" valign="top">
                <asp:ListBox ID="lstDanhSachVaiTroChuaCap" runat="server" Height="400px" SelectionMode="Multiple"
                    Width="100%"></asp:ListBox></td>
            <td align="center" style="width: 100px; text-align: center;" valign="top">
                <div style="width: 100%; line-height: 200%; padding-top: 40%">
                    <span style="padding-top: 10px">
                        <asp:ImageButton ID="btnThemVaiTro" runat="server" ImageUrl="~/htmls/image/right_1.gif"
                            OnClick="btnThemVaiTro_Click" /></span> <span style="padding-top: 10px">
                                <asp:ImageButton ID="btnThemHetVaiTro" runat="server" ImageUrl="~/htmls/image/right_2.gif"
                                    OnClick="btnThemHetVaiTro_Click" /></span> <span style="padding-top: 10px">
                                        <asp:ImageButton ID="btnXoaVaiTro" runat="server" ImageUrl="~/htmls/image/left_1.gif"
                                            OnClick="btnXoaVaiTro_Click" /></span> <span style="padding-top: 10px">
                                                <asp:ImageButton ID="btnXoaHetVaiTro" runat="server" ImageUrl="~/htmls/image/left_2.gif"
                                                    OnClick="btnXoaHetVaiTro_Click" /></span>
                </div>
            </td>
            <td align="right" style="width: 30%" valign="top">
                <asp:ListBox ID="lstDanhSachVaiTroDaCap" runat="server" Height="400px" SelectionMode="Multiple"
                    Width="100%"></asp:ListBox></td>
        </tr>
        <tr>
            <td align="left" style="width: 30%; text-align: left; height: 26px;">
                <asp:Button ID="btnThem" runat="server" Text="Thêm" Width="61px" OnClick="btnThem_Click" />
                <span>
                    <asp:Button ID="btnSua" runat="server" Text="Sửa" Width="61px" OnClick="btnSua_Click"
                        OnClientClick="return CheckDelete(1);" /></span> <span>
                            <asp:Button ID="btnXoa" runat="server" OnClientClick="return CheckDelete(0);" Text="Xoá"
                                Width="61px" OnClick="btnXoa_Click" /></span>
            </td>
            <td align="center" style="width: 30%; text-align: left;">
            </td>
            <td align="center" style="height: 26px">
            </td>
            <td align="right" style="width: 30%; height: 26px">
                <span></span>
            </td>
        </tr>
        <tr>
            <td align="center" style="width: 30%;">
            </td>
            <td align="center" style="width: 30%; text-align: left;">
            </td>
            <td align="center">
            </td>
            <td align="right" style="width: 30%">
            </td>
        </tr>
    </table>
</asp:Content>
