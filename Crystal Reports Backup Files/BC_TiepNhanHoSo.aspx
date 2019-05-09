<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true" CodeFile="BC_TiepNhanHoSo.aspx.cs" Inherits="WebUI_BC_TiepNhanHoSo" Title="Tổng hợp tiếp nhận hồ sơ" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin: 10px auto 10px 10px">
        <strong>BÁO CÁO &gt;&gt; TỔNG HỢP TIẾP NHẬN HỒ SƠ</strong></div>
    <table width="100%">
        <tr>
            <td align="right" rowspan="1" style="width: 20%; height: 29px; text-align: right">
            </td>
            <td colspan="6" rowspan="1" style="height: 29px; text-align: left">
            </td>
            <td colspan="1" rowspan="1" style="width: 30%; height: 20px; text-align: left">
            </td>
        </tr>
        <tr>
            <td align="right" rowspan="1" style="width: 20%; height: 29px; text-align: right">
                Ngày tiếp nhận</td>
            <td colspan="6" rowspan="1" style="height: 29px; text-align: left">
                <asp:TextBox ID="txtNgayTiepNhan" runat="server" MaxLength="15" Width="150px"></asp:TextBox>
                <rjs:popcalendar id="PopCalendarTuNgay" runat="server" control="txtNgayTiepNhan" from-date="1975-01-01"
                    invaliddatemessage="Nhập sai định dạng ngày tháng" messagealignment="RightCalendarControl"
                    separator="/" showerrormessage="False"></rjs:popcalendar>
                (dd/mm/yyyy)<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="txtNgayTiepNhan" ErrorMessage="Bạn phải nhập ngày bắt đầu!" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
            <td colspan="1" rowspan="1" style="width: 30%; height: 20px; text-align: left">
            </td>
        </tr>
        <tr>
            <td align="right" rowspan="1" style="width: 20%; height: 29px; text-align: right">
                Loại hồ sơ</td>
            <td colspan="6" rowspan="1" style="height: 29px; text-align: left">
                <asp:DropDownList ID="ddlLoaiHoSo" runat="server" Width="275px">
                    <asp:ListItem Value="1">Chứng nhận hợp quy</asp:ListItem>
                    <asp:ListItem Value="3">Chứng nhận hợp chuẩn</asp:ListItem>
                    <asp:ListItem Value="2">C&#244;ng bố hợp quy</asp:ListItem>
                </asp:DropDownList></td>
            <td colspan="1" rowspan="1" style="width: 30%; height: 20px; text-align: left">
            </td>
        </tr>
        <tr>
            <td align="right" rowspan="1" style="width: 20%; height: 20px; text-align: right">
            </td>
            <td colspan="6" rowspan="1" style="height: 20px; text-align: left">
            </td>
            <td colspan="1" rowspan="1" style="height: 23px; text-align: left">
            </td>
        </tr>
        <tr>
            <td align="right" rowspan="1" style="width: 252px; height: 23px">
            </td>
            <td colspan="6" rowspan="1" style="height: 23px; text-align: left">
                <asp:Button ID="btnIn" runat="server" OnClick="btnIn_Click" Text="In báo cáo" Width="100px" /></td>
            <td colspan="1" rowspan="1" style="height: 23px; text-align: center">
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
</asp:Content>

