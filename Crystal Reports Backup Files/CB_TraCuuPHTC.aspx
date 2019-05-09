<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="CB_TraCuuPHTC.aspx.cs" Inherits="WebUI_CB_TraCuuPHTC" Title="Tra cứu Công bố"
    Theme="Default" %>

<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc2" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<%@ Register Namespace="Fadrian.Web.Control" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin: 10px auto 10px 10px">
        <strong>TRA CỨU >> TRA CỨU HỒ SƠ CÔNG BỐ HỢP QUY</strong></div>
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
                                <td align="right" width="130" style="text-align: left">
                                    Số hồ sơ</td>
                                <td align="left" width="320">
                                    <asp:TextBox ID="txtSoHS" runat="server" Width="200px"></asp:TextBox></td>
                                <td align="left" valign="top" style="width: 103px">
                                </td>
                                <td align="left" colspan="1" rowspan="8" valign="top">
                                    <asp:CheckBox ID="chkAllTrangThai" runat="server" Width="146px" OnCheckedChanged="chkAllTrangThai_CheckedChanged"
                                        AutoPostBack="true" Text="Trạng thái hồ sơ"></asp:CheckBox>
                                    <div style="margin-left: 15px;">
                                        <asp:CheckBoxList ID="chklstTrangThai" runat="server" Enabled="false">
                                            <asp:ListItem Value="1">Hồ sơ mới</asp:ListItem>
                                            <asp:ListItem Value="2">Chờ ph&#226;n c&#244;ng</asp:ListItem>
                                            <asp:ListItem Value="4">Đang xử l&#253;</asp:ListItem>
                                            <asp:ListItem Value="11">Chờ lưu trữ</asp:ListItem>
                                            <asp:ListItem Value="13">Đ&#227; lưu trữ</asp:ListItem>
                                        </asp:CheckBoxList>&nbsp;</div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" style="text-align: left" width="130">
                                    Tên sản phẩm</td>
                                <td align="left" width="320">
                                    <cc2:ComboBox ID="cboTenSanPham" runat="server" Width="270px">
                                    </cc2:ComboBox></td>
                                <td align="left" style="width: 103px" valign="top">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px; height: 21px" width="130">
                                </td>
                                <td align="right" style="text-align: left" width="130">
                                    Ký hiệu sản phẩm</td>
                                <td align="left" width="320">
                                    <asp:TextBox ID="txtKyHieuSanPham" runat="server" MaxLength="255" Width="270px"></asp:TextBox></td>
                                <td align="left" style="width: 103px; height: 21px" valign="top">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" width="130" style="text-align: left">
                                    Đơn vị nộp hồ sơ</td>
                                <td align="left" width="320">
                                    <cc2:ComboBox ID="ddlDonVi" runat="server" Width="204px">
                                    </cc2:ComboBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" width="130" style="text-align: left">
                                    Người tiếp nhận</td>
                                <td align="left" width="320">
                                    <cc2:ComboBox ID="ddlNguoiTiepNhan" runat="server" Width="204px">
                                    </cc2:ComboBox></td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" width="130" style="text-align: left;">
                                    Người xử lý</td>
                                <td align="left" width="320" style="height: 27px">
                                    <cc2:ComboBox ID="ddlNguoiXuly" runat="server" Width="204px">
                                    </cc2:ComboBox></td>
                                <td style="height: 27px">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" width="130" style="text-align: left">
                                    Ngày nhận từ</td>
                                <td align="left" width="320">
                                    <asp:TextBox ID="txtNgayNhanTu" runat="server" Width="75px"></asp:TextBox>
                                    <rjs:PopCalendar ID="PopCalendar" runat="server" Control="txtNgayNhanTu" Format="dd mm yyyy"
                                        InvalidDateMessage="Nhập sai định dạng ngày tháng" MessageAlignment="RightCalendarControl"
                                        Separator="/" ShowErrorMessage="False" />
                                    đến
                                    <asp:TextBox ID="txtNgayNhanDen" runat="server" Width="75px"></asp:TextBox>
                                    <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtNgayNhanDen" Format="dd mm yyyy"
                                        InvalidDateMessage="Nhập sai định dạng ngày tháng" MessageAlignment="RightCalendarControl"
                                        Separator="/" ShowErrorMessage="False" />
                                    (dd/mm/yyyy)</td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" width="130" style="text-align: left">
                                    &nbsp;</td>
                                <td align="left" width="320">
                                    <asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" OnClick="btnTimKiem_Click" CausesValidation="False" /></td>
                                <td>
                                </td>
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
                        <legend>Danh sách hồ sơ</legend>
                        <table border="0" width="100%">
                            <tr>
                                <td colspan="4" valign="top">
                                    <cc1:PagingGridView ID="gvHoSo" runat="server" AllowPaging="true" AutoGenerateColumns="False"
                                        AllowMultiColumnSorting="true" EmptyDataText="Không tìm thấy Hồ sơ nào thỏa mãn yêu cầu tìm kiếm !"
                                        OnPageIndexChanging="gvHoSo_PageIndexChanging" OnRowDataBound="gvHoSo_RowDataBound"
                                        OnSorting="gvHoSo_Sorting" PageSize="15" VirtualItemCount="-1" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Số hồ sơ" SortExpression="SoHoSo">
                                                <itemstyle font-underline="False" width="100px" />
                                                <itemtemplate>
                                                    <a href="CB_HoSoSanPham.aspx?action=view&direct=CB_TraHoSo&HoSoID=<%# Server.UrlEncode(Eval("ID").ToString()) %>">
                                                        <%# Eval("SoHoSo") %>
                                                    </a>
                                                </itemtemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Donvi" HeaderText="Đơn vị nộp HS" SortExpression="dv.TenTiengViet">
                                                <itemstyle horizontalalign="Left" />
                                                <headerstyle horizontalalign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NgayTiepNhan" HeaderText="Ng&#224;y nhận" SortExpression="NgayTiepNhan">
                                                <itemstyle width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nguoitiepnhan" HeaderText="Người nhận" />
                                            <asp:BoundField DataField="NguoiXuLy" HeaderText="Người xử l&#253;" />
                                            <%--<asp:BoundField DataField="NguonGoc" HeaderText="Nguồn gốc">
                                                <itemstyle horizontalalign="Left" />
                                                <headerstyle horizontalalign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="HinhThuc" HeaderText="H&#236;nh thức">
                                                <itemstyle horizontalalign="Left" />
                                                <headerstyle horizontalalign="Left" />
                                            </asp:BoundField>--%>
                                             <asp:TemplateField HeaderText="DS Thông báo lệ phí">
                                                <ItemTemplate>                                
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="trangthaiid" HeaderText="MaTrangThai" Visible="False">
                                                <itemstyle horizontalalign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Trạng th&#225;i" SortExpression="TrangThaiID">
                                                <itemtemplate>
                                                                                        
                                                <asp:Label runat="server" Text='<%# Bind("TrangThai") %>' id="Label1" visible= '<%# Eval("TrangThaiID").ToString()!="13" %>' ></asp:Label>
                                                <div style='display:<%# Eval("TrangThaiID").ToString()=="13"?"":"none" %>'>
                                                <a href="CN_HoSo_Luutru.aspx?postback=cb_tracuu&HoSoID=<%#Eval("ID") %>" style="color:Blue" target="_blank" ><%#DataBinder.Eval(Container.DataItem,"TrangThai")%></a>
                                                </div>
                                                </itemtemplate>
                                                <headerstyle horizontalalign="Left" />
                                                <itemstyle horizontalalign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc1:PagingGridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4" valign="top">
                                    <asp:Button ID="btnExcel" runat="server" OnClick="btnExcel_Click" Text="Xuất ra file Excel" /></td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
