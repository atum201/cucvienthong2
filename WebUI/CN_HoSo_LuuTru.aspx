<%@ Page AutoEventWireup="true" CodeFile="CN_HoSo_LuuTru.aspx.cs" Inherits="WebUI_CN_HoSo_LuuTru"
    Language="C#" MasterPageFile="~/MasterPage/Main.master" Title="Cục quản lý chất lượng - Lưu trữ hồ sơ" %>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table style="width: 100%">
            <tr>
                <td colspan="2" style="height: 18px">
                    LƯU TRỮ HỒ SƠ</td>
            </tr>
            <tr>
                <td style="width: 30%; text-align: right">
                </td>
                <td style="width: auto">
                </td>
            </tr>
            <tr>
                <td style="width: 30%; text-align: right">
                    Số hồ sơ</td>
                <td style="width: auto">
                    <asp:TextBox ID="txtSoHoSo" runat="server" BackColor="#FFFFC0" ReadOnly="True" Width="15%"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 30%; text-align: right">
                    Ngày lưu trữ</td>
                <td style="width: auto">
                    <asp:TextBox ID="txtTuNgay" runat="server" Width="100px"></asp:TextBox>
                    <rjs:PopCalendar ID="calendarFrom" runat="server" Control="txtTuNgay" Separator="/"
                        Format="dd mm yyyy" InvalidDateMessage="Nhập sai định dạng ngày tháng" ShowErrorMessage="False"
                        ControlFocusOnError="true" RequiredDate="true" RequiredDateMessage="Bạn phải nhập ngày lưu trữ hồ sơ">
                    </rjs:PopCalendar>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTuNgay"
                        Display="Dynamic" ErrorMessage="Ngày lưu trữ hồ sơ không đúng" ValidationExpression="^(((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}|\d))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}|\d))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}|\d))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00|[048])))$">*</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 30%; text-align: right">
                    Lưu trữ hồ sơ số</td>
                <td style="width: auto">
                    <asp:TextBox ID="txtLuuTruSo" runat="server" Width="50%" MaxLength="50" ReadOnly="True" />
                    <asp:CheckBox ID="chkbInsert" runat="server" OnCheckedChanged="chkbInsert_CheckedChanged"
                        Text="Tự nhập" AutoPostBack="True" /></td>
            </tr>
            <tr>
                <td style="width: 30%; text-align: right; vertical-align: top; height: 24px;">
                    Bản scan lưu trữ</td>
                <td style="width: auto; height: 24px;">
                    <div runat="server" id="divScanFile">
                        <asp:FileUpload ID="fileUploadScanFile" runat="server" />
                        <asp:LinkButton ID="lnkThemFile" runat="server" OnClick="LinkButton1_Click">Thêm</asp:LinkButton><br />
                    </div>
                    <asp:DataList ID="dtlFile" runat="server" OnItemDataBound="dtlFile_ItemDataBound">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkTenFile" runat="server"></asp:HyperLink> &nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lnkXoaFile"  CommandName="Delete" runat="server" OnClick="LinkButton2_Click" Font-Underline="True">Xóa</asp:LinkButton>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
            <tr>
                <td style="width: 30%; text-align: right; vertical-align: top;">
                    Nơi lưu trữ</td>
                <td style="width: auto">
                    <asp:TextBox ID="txtNoiLuuTru" runat="server" Width="70%" TextMode="MultiLine" Height="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 30%; text-align: right; vertical-align: top;">
                    Ghi chú</td>
                <td style="width: auto">
                    <asp:TextBox ID="txtGhiChu" runat="server" Width="70%" TextMode="MultiLine" Height="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 30%; height: auto">
                    &nbsp;
                </td>
                <td style="width: auto">
                    <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" Width="90px" OnClick="btnCapNhat_Click" /><asp:Button
                        ID="btnCancel" runat="server" Text="Bỏ qua" Width="90px" OnClick="btnCancel_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
