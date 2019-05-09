<%@ Page AutoEventWireup="true" CodeFile="HT_CauHinhKetNoiData.aspx.cs" Inherits="WebUI_HT_CauHinhKetNoiData"
    Language="C#" MasterPageFile="~/MasterPage/Main.master" Title="Cấu hình hệ thống" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    <br />
        <span class="title">CẤU HÌNH HỆ THỐNG</span>
    </div>
    <div>
        <fieldset style="width: 97%">
        <legend>Thông tin kết nối CSDL hiện tại</legend>
            <table style="width: 600px">
                <tr>
                    <td style="text-align: right; width: 200px;">
                        Máy chủ:</td>
                    <td colspan="2" style="text-align: left">
                        <asp:TextBox ID="txtServerName" runat="server" ReadOnly="true" MaxLength="255" Width="400px"></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 200px;">
                        Tên Database:</td>
                    <td colspan="2" style="text-align: left">
                        <asp:TextBox ID="txtDatabaseName" runat="server" MaxLength="255" ReadOnly="true"
                            Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height: 17px; text-align: right">
                        Tên truy cập:</td>
                    <td colspan="2" style="height: 17px; text-align: left">
                        <asp:TextBox ID="txtLogin" runat="server" MaxLength="255" ReadOnly="true" Width="400px"></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        Mật khẩu:</td>
                    <td colspan="2" style="text-align: left">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="255" ReadOnly="true" Width="400px"></asp:TextBox>
                        </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <fieldset style="width: 97%">
            <legend>Thiết lập lại</legend>
            <table style="width: 600px">
                <tr>
                    <td style="text-align: right; width: 200px;">
                        Máy chủ:</td>
                    <td colspan="2" style="text-align: left">
                        <asp:TextBox ID="txtServerNameR" runat="server" MaxLength="255" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 200px;">
                        Tên Database:</td>
                    <td colspan="2" style="text-align: left">
                        <asp:TextBox ID="txtDatabaseNameR" runat="server" MaxLength="255" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height: 17px; text-align: right">
                        Tên truy cập:</td>
                    <td colspan="2" style="height: 17px; text-align: left">
                        <asp:TextBox ID="txtLoginR" runat="server" MaxLength="255" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        Mật khẩu:</td>
                    <td colspan="2" style="text-align: left">
                        <asp:TextBox ID="txtPasswordR" runat="server" MaxLength="255" TextMode="Password"
                            Width="400px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
        <div>
            <table style="width: 600px">
            <tr>
                <td style="width:186px">
                    </td>
                <td colspan="2" style="text-align: left">
                    <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Cập nhật"
                        Width="94px" />
                </td>
            </tr>
            </table>
        </div>
    </div>
</asp:Content>

