﻿<%@ Page AutoEventWireup="true" CodeFile="BC_BaoCaoQuyIn.aspx.cs" Inherits="WebUI_BC_BaoCaoQuyIn"
    Language="C#" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Báo cáo theo quý</title>
</head>
<body style="background-color:#ffffff;font-family:Times New Roman;">
    <form id="form1" runat="server">
        <div style="width:100%;text-align:right;margin-top:10px;">
            <asp:LinkButton runat="server" ID="lnkDong" OnClientClick="window.close();return false;" Style="padding-right:10px;">Đóng</asp:LinkButton>
        </div>
        <div style="width:100%;text-align:center;">
            <CR:CrystalReportViewer ID="HienBaoCao" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False" Width="350px" HasCrystalLogo="False" HasSearchButton="False" HasToggleGroupTreeButton="False" DisplayGroupTree="False" EnableDrillDown="False" HasDrillUpButton="False" OnUnload="HienBaoCao_Unload"/>
        </div>
        <div style="width:100%;text-align:right;margin-top:10px;">
            <asp:LinkButton runat="server" ID="LinkButton1" OnClientClick="window.close();return false;" Style="padding-right:10px;">Đóng</asp:LinkButton>
        </div>
    </form>
</body>
</html>
