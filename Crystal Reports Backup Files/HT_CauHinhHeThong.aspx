<%@ Page AutoEventWireup="true" CodeFile="HT_CauHinhHeThong.aspx.cs" Inherits="WebUI_HT_CauHinhHeThong"
    Language="C#" MasterPageFile="~/MasterPage/Main.master" Title="Cấu hình hệ thống" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin: 10px auto 10px 10px;">
        <strong>HỆ THỐNG >> CẤU HÌNH HỆ THÔNG</strong>
    </div>
    <div>
        <fieldset style="width: 97%">
            <table style="width: 600px">
                <tr>
                    <td style="text-align: left; width: 200px;">
                        Mã trung tâm:</td>
                    <td colspan="2" style="text-align: left">
                        <asp:TextBox ID="txtTenTrungTam" runat="server" MaxLength="4000" Width="400px"></asp:TextBox>
                        <asp:TextBox ID="txtTenTrungTamHide" runat="server" Width="400px" Visible="false"></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td style="height: 17px; text-align: left">
                        Đường dẫn sao lưu:</td>
                    <td colspan="2" style="height: 17px; text-align: left">
                        <asp:TextBox ID="txtDuongdansaoluu" runat="server" MaxLength="4000" Width="400px"></asp:TextBox>
                        <asp:TextBox ID="txtDuongdansaoluuHide" runat="server" Visible="false" Width="400px">
                        </asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        Địa chỉ webservice:</td>
                    <td colspan="2" style="text-align: left">
                        <asp:TextBox ID="txtDiachiwebservice" runat="server" MaxLength="4000" Width="400px"></asp:TextBox>
                        <asp:TextBox ID="txtDiachiwebserviceHide" runat="server" Visible="false" Width="400px">
                        </asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left">
                        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Cập nhật"
                            Width="94px" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
</asp:Content>

