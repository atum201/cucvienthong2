<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CB_SanPhamChiTiet.aspx.cs"
    Inherits="WebUI_CB_SanPhamChiTiet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <strong>
                <br />
                THÔNG TIN SẢN PHẨM</strong>
            <table align="center" border="0" width="100%">
                <tr>
                    <td align="right" style="height: 10px">
                    </td>
                    <td align="right" style="height: 10px">
                    </td>
                    <td colspan="1" style="height: 10px">
                    </td>
                    <td colspan="1" style="height: 10px">
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 15%; height: 18px; text-align: right">
                    </td>
                    <td align="right" style="width: 15%; height: 18px; text-align: left">
                        Tên sản phẩm:</td>
                    <td colspan="1" style="width: 20%; height: 18px; text-align: left">
                        <asp:Label ID="lblTenSanPham" runat="server" Font-Bold="True"></asp:Label></td>
                    <td colspan="1" style="width: 10%; height: 18px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 15%; height: 18px; text-align: right">
                    </td>
                    <td align="right" style="width: 15%; height: 18px; text-align: left">
                        Tiêu chuẩn áp dụng:</td>
                    <td colspan="1" style="width: 20%; height: 18px; text-align: left">
                        <asp:Label ID="lblTCAdung" runat="server" Font-Bold="True"></asp:Label></td>
                    <td colspan="1" style="width: 10%; height: 18px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 15%; height: 18px; text-align: right">
                    </td>
                    <td align="right" style="width: 15%; height: 18px; text-align: left">
                        Trạng thái:</td>
                    <td colspan="1" style="width: 20%; height: 18px; text-align: left">
                        <asp:Label ID="lblTrangThai" runat="server" Font-Bold="True"></asp:Label></td>
                    <td colspan="1" style="width: 10%; height: 18px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 15%; height: 18px; text-align: right">
                    </td>
                    <td align="right" style="width: 15%; height: 18px; text-align: left">
                        Ngày nhận:</td>
                    <td colspan="1" style="width: 20%; height: 18px; text-align: left">
                        <asp:Label ID="lblNgaynhan" runat="server" Font-Bold="True"></asp:Label></td>
                    <td colspan="1" style="width: 10%; height: 18px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 15%; height: 18px; text-align: right">
                    </td>
                    <td align="right" style="width: 15%; height: 18px; text-align: left">
                        Ký hiệu:</td>
                    <td colspan="1" style="width: 20%; height: 18px; text-align: left">
                        <asp:Label ID="lblKyHieu" runat="server" Font-Bold="True"></asp:Label></td>
                    <td colspan="1" style="width: 10%; height: 18px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 15%; height: 18px; text-align: right">
                    </td>
                    <td align="right" style="width: 15%; height: 18px; text-align: left">
                        Hãng sản xuất:</td>
                    <td colspan="1" style="width: 20%; height: 18px; text-align: left">
                        <asp:Label ID="lblhangsx" runat="server" Font-Bold="True"></asp:Label></td>
                    <td colspan="1" style="width: 10%; height: 18px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 15%; height: auto; text-align: right" valign="top">
                    </td>
                    <td align="right" style="width: 15%; height: auto; text-align: left" valign="top">
                        Tài liệu:</td>
                    <td colspan="2" style="height: auto; text-align: left">
                        <asp:LinkButton ID="lbtnBAN_CONG_BO" runat="server"></asp:LinkButton>
                        <asp:LinkButton ID="lbtnGIAY_TO_TU_CACH_PHAP_NHAN" runat="server"></asp:LinkButton>
                        <asp:LinkButton ID="lbtnTAI_LIEU_KY_THUAT" runat="server"></asp:LinkButton>
                        <asp:LinkButton ID="lbtnKET_QUA_DO_KIEM" runat="server"></asp:LinkButton>
                        <asp:LinkButton ID="lbtnQUY_TRINH_SAN_XUAT" runat="server"></asp:LinkButton>
                        <asp:LinkButton ID="lbtnBAN_TU_DANH_GIA" runat="server"></asp:LinkButton>
                        <asp:LinkButton ID="lbtnCONG_VAN" runat="server"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 15%; height: 18px; text-align: right">
                    </td>
                    <td align="right" style="width: 15%; height: 18px; text-align: right">
                    </td>
                    <td colspan="2" style="width: 20%; height: 18px; text-align: left">
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
