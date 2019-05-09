﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="CN_TraCuuGCN.aspx.cs" Inherits="WebUI_CN_TraCuuGCN" Title="Tra cứu giấy chứng nhận" %>

<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc2" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<%@ Register Namespace="Fadrian.Web.Control" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin: 10px auto 10px 10px">
        <strong>TRA CỨU>>TRA CỨU GIẤY CHỨNG NHẬN</strong>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="height: 157px">
                <div align="center">
                    <fieldset style="width: 97%">
                        <legend>Tra cứu Giấy chứng nhận</legend>
                        <table align="center" border="0" width="100%">
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" width="130" style="text-align: left;">
                                    Số Giấy chứng nhận</td>
                                <td align="left" style="height: 26px; width: 296px;">
                                    <asp:TextBox ID="txtSoGCN" runat="server" Width="262px" MaxLength="255"></asp:TextBox></td>
                                <td align="right" style="height: 26px; width: 167px;">
                                    Tiêu chuẩn áp dụng</td>
                                <td align="left" rowspan="8" valign="top">
                                    <asp:Panel ID="Panel1" runat="server" Height="150px" Width="300px">
                                        <asp:CheckBoxList ID="chklstTieuchuan" runat="server" Width="100%">
                                        </asp:CheckBoxList></asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" style="text-align: left" width="130">
                                    Loại hình chứng nhận</td>
                                <td align="left" style="width: 296px; height: 26px">
                                    <cc2:ComboBox ID="ddlLoaiHinhChungNhan" runat="server" Width="266px">
                                    </cc2:ComboBox></td>
                                <td align="right" style="width: 167px; height: 26px">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" width="130" style="text-align: left;">
                                    Đơn vị nộp hồ sơ</td>
                                <td align="left" style="height: 26px; width: 296px;">
                                    <cc2:ComboBox ID="ddlDonviNop" runat="server" Width="266px">
                                    </cc2:ComboBox></td>
                                <td align="right" style="height: 26px; width: 167px;">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" width="130" style="text-align: left">
                                    Ký hiệu sản phẩm</td>
                                <td align="left" style="width: 296px">
                                    <asp:TextBox ID="txtKyHieu" runat="server" MaxLength="255" Width="262px"></asp:TextBox></td>
                                <td align="right" style="width: 167px">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" width="130" style="text-align: left">
                                    Tên sản phẩm</td>
                                <td align="left" style="width: 296px">
                                    <cc2:ComboBox ID="ddlSanPham" runat="server" Width="266px">
                                    </cc2:ComboBox>
                                </td>
                                <td align="right" style="width: 167px">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" style="text-align: left;" width="130">
                                    Hãng sản xuất</td>
                                <td align="left" style="height: 21px; width: 296px;">
                                    <cc2:ComboBox ID="ddlHangSanXuat" runat="server" Width="266px">
                                    </cc2:ComboBox></td>
                                <td align="right" style="width: 167px; height: 21px">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" width="130" style="text-align: left">
                                    Ngày cấp</td>
                                <td align="left" style="width: 296px">
                                    <asp:TextBox ID="txtNgayCap" runat="server" Width="80px"></asp:TextBox>
                                    <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="txtNgayCap" Format="dd mm yyyy"
                                        InvalidDateMessage="Nhập sai định dạng ngày tháng" MessageAlignment="RightCalendarControl"
                                        Separator="/" ShowErrorMessage="False" />
                                    &nbsp; đến &nbsp;<asp:TextBox ID="txtNgayCapDen" runat="server" Width="80px"></asp:TextBox>
                                    <rjs:PopCalendar ID="PopCalendar3" runat="server" Control="txtNgayCapDen" Format="dd mm yyyy"
                                        InvalidDateMessage="Nhập sai định dạng ngày tháng" MessageAlignment="RightCalendarControl"
                                        Separator="/" ShowErrorMessage="False" />
                                </td>
                                <td align="right" style="width: 167px">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px" width="130">
                                </td>
                                <td align="right" width="130" style="text-align: left">
                                </td>
                                <td align="left" style="width: 296px">
                                    <asp:Button ID="btnTimKiem" runat="server" Text="Tra cứu" OnClick="btnTimKiem_Click" /></td>
                                <td align="right" style="width: 167px">
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
                        <legend>Danh sách Giấy chứng nhận</legend>
                        <table border="0" width="100%">
                            <tr>
                                <td colspan="4" valign="top">
                                    <cc1:PagingGridView ID="gvSanPham" runat="server" AllowMultiColumnSorting="True"
                                        AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="Không tìm thấy giấy chứng nhận nào thỏa mãn yêu cầu tìm kiếm !"
                                        OnPageIndexChanging="gvSanPham_PageIndexChanging" OnSorting="gvSanPham_Sorting"
                                        PageSize="15" VirtualItemCount="-1" Width="100%" AllowSorting="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Số giấy chứng nhận" SortExpression="o.SoGCN">
                                                <itemstyle font-underline="False" horizontalalign="Left" width="110px" />
                                                <headerstyle horizontalalign="Left" />
                                                <itemtemplate>
                                                    <a href="#"
                                                    onclick="return popCenter('../ReportForm/HienBaoCao.aspx?LoaiBaoCao=GiayChungNhan&SanphamID=<%# Eval("ID")%>','Tra_cuu_giay_chung_nhan',800,600);">
                                                    <%# Eval("SoGCN")%>
                                                    </a>                                                                                                
                                                
                                                </itemtemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Số hồ sơ" SortExpression="h.Sohoso">
                                                <itemstyle font-underline="False" horizontalalign="Left" width="110px" />
                                                <headerstyle horizontalalign="Left" />
                                                <itemtemplate>
                                                    <a href="CN_TraCuuThongTinHoSo.aspx?sohoso=<%# Eval("sohoso")%>">
                                                    <%# Eval("sohoso")%>
                                                    </a>                                                                                                
                                                
                                                </itemtemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Số TBLP">
                                                <itemstyle font-underline="False" horizontalalign="Left" width="110px" />
                                                <headerstyle horizontalalign="Left" />
                                                <itemtemplate>
                                                    <a href="CN_TraCuuThuPhi.aspx?sotblp=<%# Eval("sotblp")%>">
                                                    <%# Eval("sotblp")%>
                                                    </a>                                                                                                
                                                
                                                </itemtemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Trangthai" HeaderText="Trạng th&#225;i">
                                                <itemstyle horizontalalign="Left" width="60px" />
                                                <headerstyle horizontalalign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TenTiengViet" HeaderText="T&#234;n sản phẩm" SortExpression="TenTiengViet">
                                                <itemstyle horizontalalign="Left" width="90px" />
                                                <headerstyle horizontalalign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="KyHieu" HeaderText="KH sản phẩm" SortExpression="KyHieu">
                                                <itemstyle horizontalalign="Left" width="60px" />
                                                <headerstyle horizontalalign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Tenhangsanxuat" HeaderText="H&#227;ng sản xuất" SortExpression="TenHangSanXuat">
                                                <itemstyle horizontalalign="Left" width="60px" />
                                                <headerstyle horizontalalign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="manhomsanpham" HeaderText="M&#227; nh&#243;m sản phẩm"
                                                visible="False">
                                                <itemstyle horizontalalign="Left" width="60px" />
                                                <headerstyle horizontalalign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="donvi" HeaderText="Đơn vị nộp HS" >
                                                <itemstyle horizontalalign="Left" width="120px" />
                                                <headerstyle horizontalalign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TieuChuanApDung" HeaderText="Ti&#234;u chuẩn &#225;p dụng">
                                                <itemstyle horizontalalign="Left" width="80px" />
                                                <headerstyle horizontalalign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NgayKyDuyet" HeaderText="Ng&#224;y k&#253;" DataFormatString="{0:dd/MM/yyyy}"
                                                HtmlEncode="False">
                                                <itemstyle width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nguongoc" HeaderText="Nguồn gốc" visible="False">
                                                <itemstyle horizontalalign="Left" />
                                                <headerstyle horizontalalign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="sobantiepnhanCB" HeaderText="Số Bản tiếp nhận CB" visible="False" />
                                            <asp:BoundField DataField="ngayhethan" HeaderText="Ng&#224;y hết hạn" DataFormatString="{0:dd/MM/yyyy}"
                                                HtmlEncode="False" />
                                        </Columns>
                                        <PagerSettings Mode="NumericFirstLast" />
                                    </cc1:PagingGridView>
                                </td>
                            </tr>
                             <tr>
                                <td align="left" colspan="4" valign="top">
                                    <asp:Button ID="btnExcel" runat="server" Text="Xuất ra file Excel" OnClick="btnExcel_Click"
                                       />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
