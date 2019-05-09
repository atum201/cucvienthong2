<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="BC_ThongKe.aspx.cs" Inherits="WebUI_BC_ThongKe" Title="Cục quản lý chất lượng" %>
    <%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <div style="margin: 10px auto 10px 10px;">
        <strong>BÁO CÁO >>  BÁO CÁO THỐNG KÊ</strong>
    </div>
    <table style="width: 100%">
        <tr>
            <td align="right" colspan="7" style="height: 30px; text-align: left">
                <fieldset style="width: 98%">
                    <legend>Lọc báo cáo</legend>
                    <table width="100%">
                        <tr>
                            <td align="right" rowspan="1" style="width: 20%; height: 29px; text-align: right">
                                Thời gian báo cáo từ ngày (*)&nbsp;</td>
                            <td colspan="1" rowspan="1" style="width: 30%; height: 29px; text-align: left">
                                <asp:TextBox ID="txtTuNgay" runat="server" MaxLength="15" Width="150px"></asp:TextBox>
                                <rjs:popcalendar id="PopCalendarTuNgay" runat="server" control="txtTuNgay" from-date="1975-01-01"
                                    invaliddatemessage="Nhập sai định dạng ngày tháng" messagealignment="RightCalendarControl"
                                    separator="/" showerrormessage="False"></rjs:popcalendar>
                                (dd/mm/yyyy)<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="txtTuNgay" ErrorMessage="Bạn phải nhập ngày bắt đầu!" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                            <td colspan="5" rowspan="1" style="width: 3%; height: 20px; text-align: right">
                                </td>
                            <td colspan="1" rowspan="1" style="width: 30%; height: 20px; text-align: left">
                                </td>
                        </tr>
                        <tr>
                            <td align="right" rowspan="1" style="width: 20%; height: 20px; text-align: right">
                                Đến ngày (*)&nbsp;</td>
                            <td colspan="1" rowspan="1" style="width: 30%; height: 20px; text-align: left">
                                <asp:TextBox ID="txtDenNgay" runat="server" MaxLength="15" Width="150px"></asp:TextBox>
                                <rjs:popcalendar id="PopCalendarDenNgay" runat="server" control="txtDenNgay" from-date="1975-01-01"
                                    invaliddatemessage="Nhập sai định dạng ngày tháng" messagealignment="RightCalendarControl"
                                    separator="/" showerrormessage="False"></rjs:popcalendar>
                                (dd/mm/yyyy)<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="txtDenNgay" ErrorMessage="Bạn phải nhập ngày kết thúc!" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtTuNgay"
                                    ControlToValidate="txtDenNgay" ErrorMessage="Ngày bắt đầu phải nhỏ hơn ngày kết thúc"
                                    Operator="GreaterThan" Type="Date">*</asp:CompareValidator></td>
                            <td colspan="5" rowspan="1" style="height: 23px; text-align: right">
                                </td>
                            <td colspan="1" rowspan="1" style="height: 23px; text-align: left">
                                </td>
                        </tr>
                        <tr>
                            <td align="right" rowspan="1" style="width: 20%; text-align: right">
                                Chuyên viên xử lý</td>
                            <td colspan="7" rowspan="1">
                                <cc1:ComboBox ID="ddlUser" runat="server" Width="252px">
                                    
                                </cc1:ComboBox></td>
                        </tr>
                        <tr>
                            <td align="right" rowspan="1" style="width: 20%; text-align: right">
                            </td>
                            <td colspan="6" rowspan="1" style="height: 23px; text-align: center">
                               <asp:Button ID="btnIn" runat="server" Text="Lọc" Width="100px" OnClick="btnIn_Click" />
                            </td>
                            <td colspan="1" rowspan="1" style="height: 23px; text-align: center">
                            </td>
                        </tr>
                    </table>
                    </fieldset>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    
</asp:Content>
