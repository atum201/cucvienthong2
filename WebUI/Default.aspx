<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="WebUI_Default" Title="Cục quản lý chất lượng" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="center" style="vertical-align: middle; margin-top: 20px; width: 100%">
        <fieldset style="width: 50%">
            <legend align="center" style="font-size: larger">Nhắc việc</legend>
            <table style="margin-top: 20px" border="0">
                <tbody>
                    <tr>
                        <td align="left" valign="middle" width="35">
                            <asp:Image ID="ImageSoHoSoCNMoi" runat="server" Height="32px" ImageUrl="~/Images/new.JPG"
                                Width="32px" /></td>
                        <td align="left" valign="middle">
                            <asp:LinkButton ID="lbSoHoSoCNMoi" runat="server" PostBackUrl="~/WebUI/CN_HoSo_QuanLy.aspx?UserControl=CN_HoSoDen"></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td align="left" valign="middle" width="35">
                            <asp:Image ID="ImageCB_SoBanGhi" runat="server" Height="32px" ImageUrl="~/Images/new.JPG"
                                Width="32px" /></td>
                        <td align="left" valign="middle">
                            <asp:LinkButton ID="lbCB_SoBanGhi" runat="server" PostBackUrl="~/WebUI/CN_HoSo_QuanLy.aspx?UserControl=CB_HoSoDen"></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td align="left" valign="middle">
                            <asp:Image ID="ImageSoHoSoSapHetHanCN" runat="server" Height="32px" ImageUrl="~/Images/new.JPG"
                                Width="32px" /></td>
                        <td align="left" valign="middle">
                            <asp:LinkButton ID="lbSoHoSoSapHetHanCN" runat="server" PostBackUrl="~/WebUI/CN_HoSo_QuanLy.aspx?UserControl=CN_HoSoDen"></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td align="left" valign="middle" width="35">
                            <asp:Image ID="ImageSoHoSoSapHetHanCB" runat="server" Height="32px" ImageUrl="~/Images/new.JPG"
                                Width="32px" /></td>
                        <td align="left" valign="middle">
                            <asp:LinkButton ID="lbSoHoSoSapHetHanCB" runat="server" PostBackUrl="~/WebUI/CN_HoSo_QuanLy.aspx?UserControl=CB_HoSoDen"></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td align="left" valign="middle">
                            <asp:Image ID="ImageSoHoSoHetHanCN" runat="server" Height="32px" ImageUrl="~/Images/new.JPG"
                                Width="32px" /></td>
                        <td align="left" valign="middle">
                            <asp:LinkButton ID="lbSoHoSoHetHanCN" runat="server" PostBackUrl="~/WebUI/CN_HoSo_QuanLy.aspx?UserControl=CN_HoSoDen"></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td align="left" valign="middle" width="35">
                            <asp:Image ID="ImageSoHoSoHetHanCB" runat="server" Height="32px" ImageUrl="~/Images/new.JPG"
                                Width="32px" /></td>
                        <td align="left" valign="middle">
                            <asp:LinkButton ID="lbSoHoSoHetHanCB" runat="server" PostBackUrl="~/WebUI/CN_HoSo_QuanLy.aspx?UserControl=CB_HoSoDen"></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td align="left" valign="middle" width="35">
                            <asp:Image ID="ImageThongBaoPhi" runat="server" Height="32px" ImageUrl="~/Images/new.JPG"
                                Width="32px" /></td>
                        <td align="left" valign="middle">
                            <asp:LinkButton ID="lbThongBaoPhi" runat="server"></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td align="left" valign="middle" width="35">
                            <asp:Image ID="ImageThongBaoNopTien" Visible="false" runat="server" Height="32px" ImageUrl="~/Images/new.JPG"
                                Width="32px" /></td>
                        <td align="left" valign="middle">
                            <asp:LinkButton ID="lbThongBaoNopTien" runat="server"></asp:LinkButton></td>
                    </tr>
                </tbody>
            </table>
        </fieldset>
    </div>
</asp:Content>
