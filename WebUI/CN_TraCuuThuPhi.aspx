<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="CN_TraCuuThuPhi.aspx.cs" Inherits="WebUI_CN_TraCuuThuPhi" Title="Tra cứu thu phí" %>

<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc2" %>
<%@ Register Namespace="Fadrian.Web.Control" TagPrefix="cc1" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin: 10px auto 10px 10px">
        <strong>TRA CỨU >> TRA CỨU GIẤY BÁO LỆ PHÍ</strong>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="height: 157px">
                <div align="center">
                    <fieldset style="width: 97%">
                        <legend>Tìm kiếm</legend>
                        <table align="center" border="0" width="100%">
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" width="130" style="text-align: left;">
                                    Số thông báo lệ phí</td>
                                <td align="left" width="250" style="height: 26px">
                                    <asp:TextBox ID="txtSoHS" runat="server" Width="250px" MaxLength="255"></asp:TextBox></td>
                                <td align="right" width="130" style="text-align: left;">
                                    Đơn vị nộp hồ sơ</td>
                                <td align="left" style="height: 26px">
                                    <cc2:ComboBox ID="ddlDonVi" runat="server" OnSelectedIndexChanged="ddlDonVi_SelectedIndexChanged"
                                        Width="368px">
                                    </cc2:ComboBox></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" width="130" style="text-align: left;">
                                    Ngày duyệt từ</td>
                                <td align="left" style="height: 26px">
                                    <asp:TextBox ID="txtNgayDuyetTu" runat="server" Width="80px"></asp:TextBox>
                                    <rjs:PopCalendar ID="PopCalendar" runat="server" Control="txtNgayDuyetTu" Format="dd mm yyyy"
                                        InvalidDateMessage="Nhập sai định dạng ngày tháng" MessageAlignment="RightCalendarControl"
                                        Separator="/" ShowErrorMessage="False" />
                                    đến
                                    <asp:TextBox ID="txtNgayDuyetDen" runat="server" Width="80px"></asp:TextBox>
                                    <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtNgayDuyetDen" Format="dd mm yyyy"
                                        InvalidDateMessage="Nhập sai định dạng ngày tháng" MessageAlignment="RightCalendarControl"
                                        Separator="/" ShowErrorMessage="False" />
                                </td>
                                <td align="right" width="130" style="text-align: left;">
                                    Loại phí</td>
                                <td align="left" style="height: 26px">
                                    <cc2:ComboBox ID="ddlLoaiPhi" runat="server" OnSelectedIndexChanged="ddlDonVi_SelectedIndexChanged"
                                        Width="368px">
                                    </cc2:ComboBox></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" style="text-align: left" width="130">
                                    Số hoá đơn</td>
                                <td align="left" style="height: 26px">
                                    <asp:TextBox ID="txtSoHoaDon" runat="server" Width="250px"></asp:TextBox></td>
                                <td align="right" style="text-align: left" width="130">
                                </td>
                                <td align="left" style="height: 26px">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" width="130" style="text-align: left">
                                    <asp:Button ID="btnTimKiem" runat="server" OnClick="btnTimKiem_Click" Text="Tìm kiếm"
                                        Width="137px" /></td>
                                <td align="left" width="250">
                                </td>
                                <td align="right" width="130" style="text-align: left">
                                </td>
                                <td align="left">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div align="center">
                    <fieldset style="width: 97%">
                        <legend>Danh sách thông báo lệ phí</legend>
                        <table border="0" width="100%">
                            <tr>
                                <td colspan="4" valign="top">
                                    <cc1:PagingGridView ID="gvPhi" runat="server" AllowMultiColumnSorting="True" AllowPaging="True"
                                        AutoGenerateColumns="False" EmptyDataText="Không tìm thấy thông báo phí nào thỏa mãn yêu cầu tìm kiếm !"
                                        OnPageIndexChanging="gvPhi_PageIndexChanging" OnSorting="gvPhi_Sorting" PageSize="15"
                                        VirtualItemCount="-1" Width="100%" AllowSorting="True" OnRowDataBound="gvPhi_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Số th&#244;ng b&#225;o lệ ph&#237;" SortExpression="SoGiayThongBaoLePhi">
                                                <itemtemplate>
                                                 <a href="#" runat="server" id="lnkSoTBLP" >
                                                    <%# Eval("SoGiayThongBaoLePhi")%> </a>
                                                </itemtemplate>
                                                <itemstyle font-underline="False" width="100px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SoHoaDon" HeaderText="Số ho&#225; đơn" />
                                            <asp:BoundField DataField="NgayThuTien" HeaderText="Ngày thu tiền" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="TenTiengViet" HeaderText="Đơn vị nộp hồ sơ" />
                                            <asp:BoundField DataField="DiaChi" HeaderText="Địa chỉ" />
                                            <asp:BoundField DataField="DienThoai" HeaderText="Điện thoại" />
                                            <asp:BoundField DataField="Fax" HeaderText="Fax" />
                                            <asp:BoundField DataField="ngaypheduyet" HeaderText="Ng&#224;y duyệt" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="TongPhi" HeaderText="Tổng lệ ph&#237; (VNĐ)" DataFormatString="{0:#,#}.000"
                                                HtmlEncode="False" SortExpression="TongPhi" />
                                            <asp:BoundField DataField="mota" HeaderText="Trạng th&#225;i" />
                                            <asp:TemplateField>
                                                <itemtemplate>
                                   <a id="lnkIn" href="#" onclick="return loadBaoCao('<%# Eval("Id") %>');">
                                        <img runat="server" id="lnkInLePhi" title="In biên lai thu tiền" style="border: none" src="../images/printer.png"
                                            alt="" /></a>
                                </itemtemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings Mode="NumericFirstLast" />
                                    </cc1:PagingGridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    <asp:Button ID="btnExcel" runat="server" Text="Xuất ra file Excel" OnClick="btnExcel_Click"
                                        Enabled="false" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">    
        function loadBaoCao(gID) {
            popCenter("../ReportForm/HienBaoCao.aspx?LoaiBaoCao=InBienLai&LePhiID=" + gID.toString(), "rptInBienLai", 1024, 600); return false;
        }
    </script>

</asp:Content>
