<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="CN_HoSo_QuanLy.aspx.cs" Inherits="WebUI_CN_HoSo_QuanLy" Title="Quản lý hồ sơ"
    EnableEventValidation="false" Theme="Default" %>

<%@ Register Src="../UserControls/uc_CN_HoSoMoiNhan.ascx" TagName="uc_CN_HoSoMoiNhan"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/uc_CN_HoSoDaGui.ascx" TagName="uc_CN_HoSoDaGui"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControls/uc_CB_HoSoMoiNhan.ascx" TagName="uc_CB_HoSoMoiNhan"
    TagPrefix="uc3" %>
<%@ Register Src="../UserControls/uc_CB_HoSoDaGui.ascx" TagName="uc_CB_HoSoDaGui"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="vertical-align: top; text-align: left; width: auto;">
                <div class="resizeMe" style="border-right: #cccccc 1px solid; overflow: auto; width: 200px;
                    height: 450px;">
                    <asp:TreeView ID="tvDanhMucHoSo" runat="server" ImageSet="Inbox" Width="99%" OnSelectedNodeChanged="tvDanhMucHoSo_SelectedNodeChanged">
                        <SelectedNodeStyle Font-Bold="True" Font-Size="Smaller" Font-Underline="True" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" />
                        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                </div>
            </td>
            <td style="width: 99%; height: auto; vertical-align: top; text-align: left;">
                <asp:PlaceHolder ID="MyPlaceHolder" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
    </table>
</asp:Content>
