<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="GiamSatCNHC.aspx.cs" Inherits="WebUI_GiamSatCNHC" Title="Giám sát chứng nhận hợp chuẩn" %>

<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc2" %>
<%@ Register Namespace="Fadrian.Web.Control" TagPrefix="cc1" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <strong>GIÁM SÁT HỒ SƠ CHỨNG NHẬN HỢP CHUẨN<br />
    </strong>
    <table style="width: 100%;">
        <tr>
            <td style="width: 100%; height: 21px; text-align: right">
            </td>
        </tr>
        <tr>
            <td style="width: 100%; height: auto; text-align: left;">
                <fieldset style="width: 98%">
                    <legend>Tìm kiếm</legend>
                    <table align="center" border="0" width="100%">
                        <tr>
                            <td align="right" style="width: 50px" width="130">
                            </td>
                            <td align="right" style="text-align: left" width="130">
                                Số hồ sơ</td>
                            <td align="left" width="320">
                                <asp:TextBox ID="txtSoHS" runat="server" Width="270px" MaxLength="255"></asp:TextBox></td>
                            <td align="right" width="130" style="text-align: left">
                                Đơn vị nộp hồ sơ</td>
                            <td align="left" width="320">
                                <cc2:ComboBox ID="ddlDonViNop" runat="server" Width="270px">
                                </cc2:ComboBox></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 50px; height: 24px;" width="130">
                            </td>
                            <td align="right" width="130" style="text-align: left; height: 24px;">
                                Người tiếp nhận</td>
                            <td align="left" width="320" style="height: 24px">
                                <cc2:ComboBox ID="ddlNguoiTiepNhan" runat="server" Width="270px">
                                </cc2:ComboBox></td>
                            <td align="right" width="130" style="text-align: left">
                                Người xử lý</td>
                            <td align="left" width="320">
                                <cc2:ComboBox ID="ddlNguoiXuLy" runat="server" Width="270px">
                                </cc2:ComboBox></td>
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
                            <td align="right" width="130" style="text-align: left; height: 24px;">
                            </td>
                            <td align="left" width="320" style="height: 24px">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 50px" width="130">
                            </td>
                            <td align="right" width="130" style="text-align: left">
                                &nbsp;</td>
                            <td align="left" width="320">
                                <asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" OnClick="btnTimKiem_Click" /></td>
                            <td align="left" width="320">
                            </td>
                            <td align="right" width="130">
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td style="width: 100%; height: auto; text-align: left;">
                <fieldset style="width: 98%">
                    <legend>Danh sách hồ sơ hợp chuẩn đã lưu trữ.</legend>
                    <cc1:PagingGridView ID="gvHoSo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        EmptyDataText="Không có dữ liệu !" PageSize="15" Width="100%" DataKeyNames="ID,TrangThaiID,SoHoSo"
                        OnPageIndexChanging="gvHoSo_PageIndexChanging" AllowSorting="True" AllowMultiColumnSorting="False"
                        VirtualItemCount="-1">
                        <Columns>
                            <asp:TemplateField HeaderText="Số hồ sơ">
                                <itemtemplate>
                                    <a href="GiamSat_View.aspx?HoSoId=<%# Server.UrlEncode(Eval("ID").ToString()) %>&TrangThaiId=<%# Server.UrlEncode(Eval("TrangThaiID").ToString()) %>&UserControl=CN_HoSoDen"
                                        style="float: left">
                                        <%# Eval("SoHoSo") %>
                                    </a>
                                   
                                
</itemtemplate>
                                <itemstyle font-underline="False" width="160px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="DonVi" HeaderText="Đơn vị nộp HS" />
                            <asp:BoundField DataField="DiaChi" HeaderText="Địa chỉ" Visible="False" />
                            <asp:BoundField DataField="DienThoai" HeaderText="Điện thoại">
                                <itemstyle width="100px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Ng&#224;y nhận HS">
                                <itemtemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "NgayTiepNhan") == DBNull.Value? "" : ((DateTime)DataBinder.Eval(Container.DataItem, "NgayTiepNhan")).ToShortDateString()%>
                                
</itemtemplate>
                                <itemstyle width="130px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="NguoiTiepNhan" HeaderText="Người tiếp nhận">
                                <itemstyle width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NguoiXuLy" HeaderText="Người xử l&#253;">
                                <itemstyle width="100px" />
                            </asp:BoundField>
                            <asp:BoundField Visible="False" DataField="CheckDaDoc" />                            
                        </Columns>
                        <PagerSettings Mode="NumericFirstLast" />
                    </cc1:PagingGridView>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
