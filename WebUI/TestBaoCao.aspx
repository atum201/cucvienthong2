<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestBaoCao.aspx.cs" Inherits="WebUI_TestBaoCao"
    EnableTheming="false" %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
<script type="text/javascript">
    function sendMail() {
        var x = document.getElementById("trMail");
        if (x.style.display === "none") {
            x.style.display = "block";
        } else {
            x.style.display = "none";
        }
    }
</script>
    <form id="form1" runat="server">
        <div style="width: 100%; text-align: center">
            <table>
                <tr id="trPrint" runat="server">
                    <td colspan="2" style="text-align: left">
                        <asp:LinkButton runat="server" ID="lnkWord" OnClick="lnkWord_Click" ToolTip="Xuất ra file Word"
                            CssClass="lnkReport" CausesValidation="False"> <img src="../Images/doc.gif" border="0" height="20" width="20" alt=""/>&nbsp;Word </asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lnkExcel" OnClick="lnkExcel_Click" ToolTip="Xuất ra file Excel"
                            CssClass="lnkReport" CausesValidation="False"> <img src="../Images/xls.gif" border="0" height="20" width="20"   alt=""/>&nbsp;Excel </asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lnkPDF" OnClick="lnkPDF_Click" ToolTip="Xuất ra file PDF"
                            CssClass="lnkReport" CausesValidation="False"> <img src="../Images/pdf.gif" border="0" height="20" width="20"  alt="" />&nbsp;PDF </asp:LinkButton>&nbsp;
                        <a href="javascript:sendMail()" class="lnkReport">Gửi mail</a>
                    </td>
                </tr>
                <tr id="trMail" style="display:none;">
                    <td colspan="2" style="text-align: left">
                        <table>
                            <tr>
                                <td style="width:100px">Gửi tới: </td>
                                <td style="width:400px">
                                    <asp:TextBox runat="server" ID="txtEmail" Width="350px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:100px">Tiêu đề: </td>
                                <td style="width:400px">
                                    <asp:TextBox runat="server" ID="txtTieuDe" Width="350px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:100px">Nội dung: </td>
                                <td style="width:400px">
                                    <asp:TextBox runat="server" ID="txtNoiDung" TextMode="MultiLine" Rows="5" Width="350px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:100px"></td>
                                <td>
                                    <asp:LinkButton runat="server" ID="lnkSendMail" OnClick="SendEmail_Click" ToolTip="Gửi mail"
                            CssClass="lnkReport" CausesValidation="False">Gửi mail </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <CR:CrystalReportViewer ID="crView" runat="server" AutoDataBind="true" DisplayGroupTree="False"
                            EnableDatabaseLogonPrompt="False" EnableDrillDown="False" HasCrystalLogo="False"
                            HasDrillUpButton="False" HasSearchButton="False" HasToggleGroupTreeButton="False"
                            Height="50px" ReuseParameterValuesOnRefresh="True" Width="350px" HasViewList="False"
                            HasZoomFactorList="False" HasExportButton="False" HasPrintButton="False" />
                    </td>
                </tr>
                <tr id="trHuy" runat="server" style="height: auto">
                    <td align="center" bgcolor="#cccccc" colspan="2" style="height: 30px; text-align: left">
                        <table style="width: 100%">
                            <tr>
                                <td style="vertical-align: top; width: 150px; text-align: left">
                                    <asp:Label ID="lblLyDoHuy" runat="server" Text="Lý do xin huỷ"></asp:Label></td>
                                <td style="width: 496px">
                                    <asp:TextBox ID="txtLyDoHuy" runat="server" BackColor="#FFFFC0" Height="70px" ReadOnly="True"
                                        TextMode="MultiLine" Width="70%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; width: 150px; text-align: left; height: 47px;">
                                    Ý kiến phê duyệt</td>
                                <td style="height: 47px">
                                    <asp:RadioButtonList ID="rblKetLuan" runat="server" RepeatDirection="Horizontal"
                                        Width="492px">
                                        <asp:ListItem Value="1">Ph&#234; duyệt</asp:ListItem>
                                        <asp:ListItem Value="0">Kh&#244;ng ph&#234; duyệt</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server" id="trNgayThuTien">
                    <td bgcolor="#cccccc" style="width: 150px">
                        Ngày thu tiền</td>
                    <td bgcolor="#cccccc" style="width: 500px; text-align: left">
                        <asp:TextBox ID="txtNgayThuTien" runat="server" BorderColor="Transparent" TabIndex="3"
                            Width="30%"></asp:TextBox>
                        <rjs:PopCalendar ID="calendarFrom" runat="server" Control="txtNgayThuTien" ForeColor="#FFFFC0"
                            ScriptsValidators="No Validate" Separator="/" ShowErrorMessage="False"></rjs:PopCalendar>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNgayThuTien"
                            ErrorMessage="Bạn phải nhập ngày thu tiền" ToolTip="Bạn phải nhập ngày thu tiền">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr id="soHD" runat="server">
                    <td bgcolor="#cccccc" style="width: 150px;">
                        Số hoá đơn</td>
                    <td bgcolor="#cccccc" style="text-align: left; width: 500px;">
                        <asp:TextBox ID="txtSoHoaDon" runat="server" Width="71%" MaxLength="250"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSoHoaDon"
                            ErrorMessage="Bạn phải nhập số hoá đơn." ToolTip="Bạn phải nhập số hoá đơn.">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#cccccc" colspan="2" style="height: 30px; text-align: center">
                        <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" Width="120px" OnClick="btnCapNhat_Click"
                            CausesValidation="False" />
                        <asp:Button ID="btnThuPhi" runat="server" OnClick="btnThuPhi_Click" Text="Đã thu đủ phí"
                            Width="120px" /><asp:Button ID="btnHuyHieuLuc" runat="server" OnClick="btnHuyHieuLuc_Click"
                                Text="Hủy hiệu lực" Width="120px" CausesValidation="False" /><asp:Button ID="btnBoQua"
                                    runat="server" OnClick="btnBoQua_Click" Text="Bỏ qua" Width="120px" CausesValidation="False" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
