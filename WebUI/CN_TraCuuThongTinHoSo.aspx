<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="CN_TraCuuNguoiNop.aspx.cs" Inherits="WebUI_CN_TraCuuNguoiNop"
    Title="Tra cứu thông tin hồ sơ" Theme="Default" %>

<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc2" %>
<%@ Register Namespace="Fadrian.Web.Control" TagPrefix="cc1" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
    
    </script>

    <div style="margin: 10px auto 10px 10px;">
        <strong>TRA CỨU >> TRA CỨU HỒ SƠ CHỨNG NHẬN</strong>
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
                                <td align="right" style="text-align: left" width="130">
                                    Số hồ sơ</td>
                                <td align="left" width="320">
                                    <asp:TextBox ID="txtSoHS" runat="server" Width="270px" MaxLength="255"></asp:TextBox></td>
                                <td align="right" width="130">
                                </td>
                                <td align="left" rowspan="8" valign="top">
                                    <asp:CheckBox ID="chkAllTrangThai" runat="server" Text="Trạng thái hồ sơ" AutoPostBack="true"
                                        Width="122px" OnCheckedChanged="chkAllTrangThai_CheckedChanged" />
                                    <div style="margin-left: 15px;">
                                        <asp:CheckBoxList ID="chklstTrangThai" runat="server" Enabled="false">
                                            <asp:ListItem Value="1">Hồ sơ mới</asp:ListItem>
                                            <asp:ListItem Value="2">Chờ ph&#226;n c&#244;ng</asp:ListItem>
                                            <asp:ListItem Value="4">Đang xử l&#253;</asp:ListItem>
                                            <asp:ListItem Value="11">Chờ lưu trữ</asp:ListItem>
                                            <asp:ListItem Value="13">Đ&#227; lưu trữ</asp:ListItem>
                                        </asp:CheckBoxList></div>
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
                                <td align="right" width="130">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" style="text-align: left" width="130">
                                    Ký hiệu sản phẩm</td>
                                <td align="left" width="320">
                                    <asp:TextBox ID="txtKyHieuSanPham" runat="server" MaxLength="255" Width="270px"></asp:TextBox></td>
                                <td align="right" width="130">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" width="130" style="text-align: left">
                                    Loại hình chứng nhận</td>
                                <td align="left" width="320">
                                    <cc2:ComboBox ID="ddlLoaiHinhChungNhan" runat="server" Width="270px">
                                    </cc2:ComboBox></td>
                                <td align="right" width="130">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" width="130" style="text-align: left">
                                    Đơn vị nộp hồ sơ</td>
                                <td align="left" width="320">
                                    <cc2:ComboBox ID="ddlDonViNop" runat="server" Width="270px">
                                    </cc2:ComboBox></td>
                                <td align="right" width="130">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" width="130" style="text-align: left">
                                    Người tiếp nhận</td>
                                <td align="left" width="320">
                                    <cc2:ComboBox ID="ddlNguoiTiepNhan" runat="server" Width="270px">
                                    </cc2:ComboBox></td>
                                <td align="right" width="130">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" width="130" style="text-align: left">
                                    Người xử lý</td>
                                <td align="left" width="320">
                                    <cc2:ComboBox ID="ddlNguoiXuLy" runat="server" Width="270px">
                                    </cc2:ComboBox></td>
                                <td align="right" width="130">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" width="130" style="text-align: left">
                                    Ngày nhận từ</td>
                                <td align="left" width="320">
                                    <asp:TextBox ID="txtNgayNhanTu" runat="server" Width="100px"></asp:TextBox>
                                    <rjs:PopCalendar ID="PopCalendar" runat="server" Control="txtNgayNhanTu" Format="dd mm yyyy"
                                        InvalidDateMessage="Nhập sai định dạng ngày tháng" MessageAlignment="RightCalendarControl"
                                        Separator="/" ShowErrorMessage="False" />
                                    đến
                                    <asp:TextBox ID="txtNgayNhanDen" runat="server" Width="100px"></asp:TextBox>
                                    <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtNgayNhanDen" Format="dd mm yyyy"
                                        InvalidDateMessage="Nhập sai định dạng ngày tháng" MessageAlignment="RightCalendarControl"
                                        Separator="/" ShowErrorMessage="False" />
                                </td>
                                <td align="right" width="130">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" width="130" style="text-align: left">
                                    &nbsp;</td>
                                <td align="left" width="320">
                                    <asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" OnClick="btnTimKiem_Click" /></td>
                                <td align="right" width="130">
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
                        <legend>Danh sách hồ sơ</legend>
                        <table border="0" width="100%">
                            <tr>
                                <td colspan="4" valign="top">
                                    <cc1:PagingGridView ID="gvHoSo" AllowSorting="True" runat="server" AutoGenerateColumns="False"
                                        EmptyDataText="Không tìm thấy Hồ sơ nào thỏa mãn yêu cầu tìm kiếm !" Width="100%"
                                        AllowPaging="True" OnPageIndexChanging="gvHoSo_PageIndexChanging" OnSorting="gvHoSo_Sorting"
                                        AllowMultiColumnSorting="True" VirtualItemCount="-1" OnRowDataBound="gvHoSo_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Số hồ sơ" SortExpression="hs.SoHoSo">
                                                <itemtemplate>
                                                   
                                                    <a href="CN_HoSoSanPham.aspx?action=view&direct=CN_TraHoSo&HoSoID=<%# Server.UrlEncode(Eval("ID").ToString()) %>">
                                                        <%#Eval("SoHoSo") %>
                                                    </a>
                                                
</itemtemplate>
                                                <itemstyle font-underline="False" width="100px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DonVi" HeaderText="Đơn vị nộp HS" SortExpression="dv.TenTiengViet">
                                                <headerstyle horizontalalign="Left" />
                                                <itemstyle horizontalalign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NgayTiepNhan" HeaderText="Ng&#224;y nhận" DataFormatString="{0:dd/MM/yyyy}"
                                                HtmlEncode="False" SortExpression="NgayTiepNhan">
                                                <itemstyle width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nguoitiepnhan" HeaderText="Người nhận" SortExpression="hs.NguoiTiepNhanId" />
                                            <asp:BoundField DataField="NguoiXuLy" HeaderText="Người xử l&#253;" SortExpression="NguoiXuLy" />
                                            <asp:BoundField DataField="NguonGoc" HeaderText="Nguồn gốc" SortExpression="NguonGocID">
                                                <headerstyle horizontalalign="Left" />
                                                <itemstyle horizontalalign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="DS Thông báo lệ phí">
                                                <ItemTemplate>                                
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trạng th&#225;i" SortExpression="TrangThaiID">
                                                <itemtemplate>
                                                                                        
                                                <asp:Label runat="server" Text='<%# Bind("TrangThai") %>' id="Label1" visible= '<%# Eval("TrangThaiID").ToString()!="13" %>' ></asp:Label>
                                                <div style='display:<%# Eval("TrangThaiID").ToString()=="13"?"":"none" %>'>
                                                <a href="CN_HoSo_Luutru.aspx?postback=cn_tracuu&HoSoID=<%#Eval("ID") %>" style="color:Blue" target="_blank" ><%#DataBinder.Eval(Container.DataItem,"TrangThai")%></a>
                                                </div>
                                                </itemtemplate>
                                                <headerstyle horizontalalign="Left" />
                                                <itemstyle horizontalalign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings Mode="NumericFirstLast" />
                                    </cc1:PagingGridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4" valign="top">
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
</asp:Content>
