<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true" CodeFile="GiamSat_SanPhamView.aspx.cs" Inherits="WebUI_GiamSat_SanPhamView" Title="Chi tiết sản phẩm" %>

<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  

    <div style="margin: 10px 10px 10px 20px">
        <span style="font-family: Arial">GIÁM SÁT HỒ SƠ CHỨNG NHẬN HỢP CHUẨN<strong> &gt;&gt;
        </strong>
            <asp:Label ID="lblDsSanPham" runat="server" Text=""/><strong>
            SẢN PHẨM CHI TIẾT </strong>
        </span>
        <table id="Table2" style="width: 100%; color: #000000;border:1px solid blue">
            <tr>
                <td colspan="6" width="250" style="height: 21px">
                </td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; width: 199px; height: 23px;">
                    Số hồ sơ</td>
                <td colspan="2" style="width: 215px; height: 23px;">
                    <asp:Label ID="txtSoHoSo" runat="server" Font-Bold="True"/></td>
                <td colspan="1" align="left" width="150" style="height: 23px">
                    Tiêu chuẩn áp dụng</td>
                <td rowspan="5" valign="top" style="width: auto">
                    <asp:Panel ID="Panel1" runat="server" Height="120px" ScrollBars="Both" Width="350px"
                        BorderWidth="1px" >
                        <asp:CheckBoxList ID="chklstTieuChuan" runat="server" Width="100%" Enabled="False" Font-Bold="True">
                        </asp:CheckBoxList></asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; height: 27px; width: 199px;">
                    Tên sản phẩm <span>(*)</span></td>
                <td colspan="2" style="width: 215px; height: 27px;">
                    <asp:Label ID="lblTenSanPham" runat="server" Font-Bold="True"></asp:Label></td>
                <td style="height: 27px" width="150">
                </td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; width: 199px;">
                    Ký hiệu sản phẩm</td>
                <td colspan="2" style="width: 215px">
                    <div style="float: left">
                        <asp:Label ID="txtKyHieuSanPham" runat="server" Font-Bold="True" />
                    </div>
                </td>
                <td width="150">
                </td>
            </tr>
            <tr style="color: #000000">
                <td align="right" style="text-align: left; height: 28px; width: 199px;">
                    Nhóm sản phẩm</td>
                <td colspan="2" style="width: 215px; height: 28px">
                    <asp:Label ID="txtNhomSanPham" runat="server" Font-Bold="True" /></td>
                <td style="height: 28px" width="150">
                </td>
            </tr>
            <tr style="color: #000000">
                <td align="right" style="text-align: left; width: 199px;">
                    Hãng sản xuất <span>(*)</span></td>
                <td colspan="2" style="width: 215px">
                    <asp:Label ID="lblHangSanXuat" runat="server" Font-Bold="True"></asp:Label></td>
                <td width="150">
                </td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; height: 29px; width: 199px;">
                    Giấy tờ về tư cách pháp nhân</td>
                <td colspan="4" style="height: 29px">
                    &nbsp;
                    <%--<input type="button" id="btnrefreshTuCachPhapNha" style="height:22px; width:35px;" value="Xóa" onclick="ClearText('FileUploadGiayToTuCachPhapNhan');return false"  />--%>
                    <asp:Label ID="lblTuCachPhapNhan" runat="server" Visible="False" Font-Bold="True"/></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; height: 27px; width: 199px;">
                    Tài liệu kỹ thuật</td>
                <td colspan="4" style="height: 27px">
                    &nbsp;
                    <%--<input type="button" id="btnrefreshTaiLieuKyThuat"  style="height:22px; width:35px;" value="Xóa"onclick="ClearText('FileUploadTaiLieuKyThuat');return false"  />--%>
                    <asp:Label ID="lblTaiLieuKyThuat" runat="server" Visible="False" Font-Bold="True"/></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; height: 24px; width: 199px;">
                    Tài liệu quy trình sản xuất</td>
                <td colspan="4" style="height: 24px">
                    &nbsp;
                    <%--<input type="button" id="btnRefreshTaiLieuQuyTrinhSanXuat"  style="height:22px; width:35px;" value="Xóa" onclick="ClearText('FileUploadTaiLieuQuyTrinhSanXuat');return false"  />--%>
                    <asp:Label ID="lblTaiLieuQuyTrinhSanXuat" runat="server" Visible="False" Font-Bold="True"/></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; height: 24px; width: 199px;">
                    Đơn đề nghị chứng nhận PH</td>
                <td colspan="4" style="height: 24px">
                    &nbsp;
                    <%--<input type="button" id="Button1"  style="height:22px; width:35px;" value="Xóa"onclick="ClearText('FileUploadTaiLieuDeNghiCN');return false"  />--%>
                    <asp:Label ID="lblTaiLieuDeNghi" runat="server" Visible="False" Font-Bold="True"/></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; width: 199px;">
                    Kết quả đo kiểm</td>
                <td colspan="4" style="height: 24px">
                    &nbsp;
                    <%--<input type="button" id="Button2"  style="height:22px; width:35px;" value="Xóa" onclick="ClearText('FileUploadKetQuaDoKiem');return false"  />--%>
                    <asp:Label ID="lblKetQuaDoKiem" runat="server" Visible="False" Font-Bold="True"/></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; height: 24px; width: 199px;">
                    Quy trình đảm bảo chất lượng</td>
                <td colspan="4">
                    &nbsp;
                    <%--<input type="button" id="Button3"  style="height:22px; width:35px;" value="Xóa" onclick="ClearText('FileUploadQuyTrinhDamBao');return false"  />--%>
                    <asp:Label ID="lblQuyTrinhDamBao" runat="server" Visible="False" Font-Bold="True"/></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; width: 199px;">
                    Tiêu chuẩn tự nguyện áp dụng</td>
                <td colspan="4">
                    &nbsp;
                    <%--<input type="button" id="Button4"  style="height:22px; width:35px;" value="Xóa" onclick="ClearText('FileUploadTieuChuanTuNguyen');return false"  />--%>
                    <asp:Label ID="lblTieuChuanTuNguyen" runat="server" Visible="False" Font-Bold="True"/></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; width: 199px;">
                    Chứng chỉ hệ thống QLCL</td>
                <td colspan="4">
                    &nbsp;
                    <%--<input type="button" id="Button5"  style="height:22px; width:35px;" value="Xóa" onclick="ClearText('FileUploadChungChiHeThong');return false"  />--%>
                    <asp:Label ID="lblChungChiHeThong" runat="server" Visible="False" Font-Bold="True"/></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; width: 199px;">
                    Chỉ tiêu kỹ thuật kèm theo&nbsp;</td>
                <td colspan="4">
                    &nbsp;
                    <asp:Label ID="lblChiTieuKyThuatKemTheo" runat="server" Visible="False" Font-Bold="True"/></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; width: 199px;">
                    Hồ sơ</td>
                <td colspan="4">
                    &nbsp;<asp:Label ID="lblHopLe" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; width: 199px;">
                </td>
                <td colspan="4">
                    &nbsp;<asp:Label ID="lblDayDu" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; width: 199px;">
                    Số/Ngày đo kiểm</td>
                <td colspan="4">
                    <asp:Label ID="txtSoDoKiem" runat="server" Font-Bold="True" />-Ngày:
                    <asp:Label
                        ID="txtNgayDoKiem" runat="server" Font-Bold="True" />
                    </td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; width: 199px;">
                    Cơ quan đo kiểm</td>
                <td colspan="4">
                    <div style="float: left">
                        &nbsp;</div>
                    <asp:Label ID="lblCoQuanDoKiem" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; vertical-align: top; width: 199px;">
                    Nội dung đo kiểm</td>
                <td colspan="4">
                    <asp:Label ID="txtNoiDungDoKiem" runat="server" Font-Bold="True" /></td>
            </tr>
            <tr id="rKTCSSX" runat="server">
                <td align="left" style="width: 199px;" class="caption">
                    Kết quả kiểm tra CSSX</td>
                <td colspan="5" style="height: auto; text-align: left">
                    &nbsp;<asp:Label ID="lbDat" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr id="rTanSo" runat="server">
                <td align="left" style="width: 199px;" class="caption">
                    Quy hoạch tần số</td>
                <td colspan="5" style="text-align: left">
                    &nbsp;<asp:Label ID="lblPhuHop" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr runat="server" id="Tr1">
                <td align="left" class="caption" style="width: 199px; vertical-align: top; height: 72px;">
                    Nội dung xử lý</td>
                <td colspan="5" style="text-align: left;" valign="top">
                    <asp:Label ID="txtNoiDungXuLy" runat="server" Font-Bold="True" /></td>
            </tr>
            <tr>
                <td align="left" style="width: 199px; vertical-align: top;" class="caption">
                    Nhận xét khác</td>
                <td colspan="5" style="text-align: left">
                    <asp:Label ID="txtNhanXetKhac" runat="server" Font-Bold="True"/></td>
            </tr>
            <tr id="rKetLuan">
                <td align="left" style="width: 199px;" class="caption">
                    Kết luận</td>
                <td colspan="5" style="text-align: left">
                    &nbsp;<asp:Label ID="lblCapGCN" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr id="rThoiHan">
                <td style="width: 199px;" align="left" class="caption">
                    Thời hạn</td>
                <td colspan="5" style="text-align: left">
                    <asp:Label ID="txtThoiHan" runat="server" Font-Bold="True"
                         /></td>
            </tr>
            <tr id="rLephi">
                <td style="width: 199px; height: 26px;" align="left" class="caption">
                    Lệ phí chứng nhận</td>
                <td colspan="5" style="text-align: left; height: 26px;">
                    <asp:Label ID="txtLePhi" runat="server" Font-Bold="True" 
                        /><strong>(VNĐ)</strong></td>
            </tr>
            <tr id="rSogiayCN" runat="server">
                <td style="width: 199px; height: 21px;" align="left" class="caption">
                    Số GCN/CV</td>
                <td colspan="5" style="text-align: left; height: 21px;">
                    <asp:Label ID="txtSoGCNCV" runat="server" Font-Bold="True"/>
                    &nbsp;&nbsp;
                    <asp:HyperLink ID="hlCongVan" runat="server" Visible="False" Font-Bold="True">Công văn</asp:HyperLink></td>
            </tr>
            <tr id="rNgayCap" runat="server">
                <td align="left" class="caption" style="width: 199px; height: 29px;">
                    <asp:Label ID="lblNgayCap" runat="server" Text="Ngày ký duyệt"/></td>
                <td colspan="5" style="text-align: left; height: 29px;">
                    <asp:Label ID="txtNgayCap" runat="server" Font-Bold="True"
                         />&nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; width: 199px;">
                </td>
                <td colspan="5">
                    &nbsp;<asp:Button ID="btnBoQua" runat="server" Text="Bỏ qua" Width="80px" CausesValidation="false"
                        OnClick="btnBoQua_Click" /></td>
            </tr>
        </table>
    </div>
    
</asp:Content>

