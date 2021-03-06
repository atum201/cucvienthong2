<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="HT_VaiTro_QuanLy.aspx.cs" Inherits="WebUI_HT_VaiTro_QuanLy" Title="Quản lý vai trò"
    Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    function CheckDelete(edit){
        var lst=  document.getElementById("<%= lstDanhSachVaiTro.ClientID %>");
        if(lst.selectedIndex>=0){
            if(edit==1)
                return true;
            else
                return confirm('Bạn có muốn xóa vai trò đã chọn?');
        }else{
           alert('Bạn Chưa chọn vai trò'); 
           return false;           
        }
    }

    </script>

    <div style="margin: 10px auto 10px 10px;">
        <strong>HỆ THỐNG >> QUẢN LÝ VAI TRÒ</strong>
    </div>
    <table style="text-align: center" width="100%">
        <tr>
            <td class="title" colspan="4" style="width: 30%;">
            </td>
        </tr>
        <tr>
            <td align="right" valign="top" style="width: 30%">
            </td>
            <td colspan="2" valign="top" style="width: 30%">
            </td>
            <td align="right" style="width: 30%" valign="top">
                <asp:ImageButton ID="imgbtnVaiTro" runat="server" Enabled="False" ImageUrl="../htmls/image/nguoidung.gif" /><asp:LinkButton
                    ID="lnkQuanLyNguoiDung" runat="server" Font-Bold="True" PostBackUrl="~/WebUI/HT_NguoiDung_QuanLy.aspx">Quản lý người dùng</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="width: 30%;" valign="top">
                <strong>Danh sách vai trò</strong></td>
            <td align="left" colspan="2" style="width: 30%;" valign="top">
                <strong>Danh sách quyền không được cấp </strong>
            </td>
            <td align="left" style="width: 30%;" valign="top">
                <strong>Danh sách quyền được cấp</strong>
            </td>
        </tr>
        <tr>
            <td style="width: 30%; height: 372px;" valign="top">
                <asp:ListBox ID="lstDanhSachVaiTro" runat="server" AutoPostBack="True" Height="380px"
                    Width="100%" BackColor="#ffffff" OnSelectedIndexChanged="lstDanhSachVaiTro_SelectedIndexChanged">
                </asp:ListBox>
            </td>
            <td style="width: 30%; vertical-align: top; text-align: left; height: 372px;" valign="top">
                <div style="width: 97%; overflow: auto; border: #cccccc 1px solid; height: 372px;">
                    <asp:TreeView ID="tvQuyenChuaCap" runat="server" ForeColor="Black" ShowLines="True"
                        Width="99%" ImageSet="Arrows" ShowCheckBoxes="All">
                        <SelectedNodeStyle Font-Bold="True" ForeColor="#5555DD" Font-Underline="True" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                        <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                </div>
            </td>
            <td align="center" style="width: 100px; height: 372px;" valign="top">
                <div style="width: 100%; line-height: 200%; padding-top: 40%">
                    <span style="padding-top: 10px">
                        <asp:ImageButton ID="btnThemQuyen" runat="server" ImageUrl="~/htmls/image/right_1.gif"
                            OnClick="btnThemQuyen_Click" /></span> <span style="padding-top: 10px">
                                <asp:ImageButton ID="btnThemHetQuyen" runat="server" ImageUrl="~/htmls/image/right_2.gif"
                                    OnClick="btnThemHetQuyen_Click" /></span> <span style="padding-top: 10px">
                                        <asp:ImageButton ID="btnXoaQuyen" runat="server" ImageUrl="~/htmls/image/left_1.gif"
                                            OnClick="btnXoaQuyen_Click" /></span> <span style="padding-top: 10px">
                                                <asp:ImageButton ID="btnXoaHetQuyen" runat="server" ImageUrl="~/htmls/image/left_2.gif"
                                                    OnClick="btnXoaHetQuyen_Click" /></span>
                </div>
            </td>
            <td align="right" style="width: 30%; text-align: left; vertical-align: top; height: 372px;"
                valign="top">
                <div style="width: 100%; overflow: auto; border: #cccccc 1px solid; height: 372px;">
                    <asp:TreeView ID="tvQuyenDaCap" runat="server" ForeColor="Black" ShowLines="True"
                        Width="100%" ShowCheckBoxes="All">
                        <SelectedNodeStyle BackColor="Transparent" BorderColor="DodgerBlue" Font-Bold="True"
                            ForeColor="Blue" />
                    </asp:TreeView>
                </div>
            </td>
        </tr>
        <tr>
            <td align="center" style="width: 30%; text-align: left">
                <asp:Button ID="btnThem" runat="server" Text="Thêm" Width="61px" OnClick="btnThem_Click" />
                <span>
                    <asp:Button ID="btnSua" runat="server" Text="Sửa" Width="61px" OnClick="btnSua_Click"
                        OnClientClick="return CheckDelete(1);" /></span> <span>
                            <asp:Button ID="btnXoa" runat="server" OnClientClick="return CheckDelete(0);" Text="Xoá"
                                Width="61px" OnClick="btnXoa_Click" /></span>
            </td>
            <td align="center" style="width: 30%">
            </td>
            <td align="center">
            </td>
            <td align="right" style="width: 30%">
                <span></span>
            </td>
        </tr>
        <tr>
            <td align="center" style="width: 30%; height: 21px;">
            </td>
            <td align="center" style="width: 30%; height: 21px;">
            </td>
            <td align="center" style="height: 21px">
            </td>
            <td align="right" style="width: 30%; height: 21px;">
            </td>
        </tr>
    </table>
</asp:Content>
