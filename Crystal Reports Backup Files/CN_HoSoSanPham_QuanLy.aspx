﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true" CodeFile="CN_HoSoSanPham_QuanLy.aspx.cs" Inherits="WebUI_CN_HoSoSanPham_QuanLy" Title="Quản lý hồ sơ" Theme="Default"%>

<%@ Register Src="../UserControls/uc_CN_HoSoSanPhamMoiNhan.ascx" TagName="uc_CN_HoSoSanPhamMoiNhan"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/uc_CN_HoSoSanPhamDaGui.ascx" TagName="uc_CN_HoSoSanPhamDaGui"
    TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="width: 100%">
        <tr>
            <td style="vertical-align: top; text-align: left; width:auto; height: auto;" >
                <div class="resizeMe" style="border-right: #cccccc 1px solid; overflow: auto; width:200px; height: 450px;">
                    <asp:TreeView ID="tvDanhMucHoSo" runat="server" ImageSet="Inbox" Width="99%" OnSelectedNodeChanged="tvDanhMucHoSo_SelectedNodeChanged">
                        <SelectedNodeStyle BackColor="Transparent" BorderColor="Transparent" BorderStyle="Solid" Font-Bold="True" Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" />
                        
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" />
                        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                </div>
            </td>
            <td style="width:99%; vertical-align: top; text-align: left; height: auto;">
                <asp:PlaceHolder ID="MyPlaceHolder" runat="server"></asp:PlaceHolder>               
            </td>
        </tr>
    </table>
</asp:Content>

