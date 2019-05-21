<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HienBaoCao.aspx.cs" Inherits="WebUI_HienBaoCao" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hiển thị báo cáo</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%">
            <tr id="trPrint" runat="server">
                <td>
                    <asp:LinkButton runat="server" ID="lnkWord" OnClick="lnkWord_Click" ToolTip="Xuất ra file Word"
                        CssClass="lnkReport"> <img src="../Images/doc.gif" border="0" height="20" width="20" alt=""/>&nbsp;Word </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lnkExcel" OnClick="lnkExcel_Click" ToolTip="Xuất ra file Excel"
                        CssClass="lnkReport"> <img src="../Images/xls.gif" border="0" height="20" width="20"   alt=""/>&nbsp;Excel </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lnkPDF" OnClick="lnkPDF_Click" ToolTip="Xuất ra file PDF"
                        CssClass="lnkReport"> <img src="../Images/pdf.gif" border="0" height="20" width="20"  alt="" />&nbsp;PDF </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lnkHTML" OnClick="lnkHTML_Click" ToolTip="Xuất ra file HTML"
                        CssClass="lnkReport" Visible="false"> <img src="../Images/htm.gif" border="0" height="20" width="20"  alt="" />&nbsp;HTML </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <CR:CrystalReportViewer ID="hienBC" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False"
                        Width="350px" HasCrystalLogo="False" HasSearchButton="False" HasToggleGroupTreeButton="False"
                        DisplayGroupTree="False" EnableDrillDown="False" HasDrillUpButton="False" HasViewList="False"
                        HasZoomFactorList="False" HasExportButton="False" HasPrintButton="False" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="hidFormat" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
