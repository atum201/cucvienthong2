<%@ Page AutoEventWireup="true" CodeFile="CN_SanPhamChiTiet.aspx.cs" Inherits="WebUI_CN_SanPhamChiTiet"
    Language="C#" Theme="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 10px auto 10px 10px;">
        <strong>THÔNG TIN SẢN PHẨM</strong>
    </div>
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
                                Mã nhóm sản phẩm:</td>
                            <td colspan="1" style="width: 20%; height: 18px; text-align: left">
                                <asp:Label ID="lblManhomSP" runat="server" Font-Bold="True"></asp:Label></td>
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
                                <asp:LinkButton ID="lbtnDON_DE_NGHI_CN" runat="server"></asp:LinkButton>
                                &nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lbtnGIAY_TO_TU_CACH_PHAP_NHAN" runat="server"></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="lbtnTAI_LIEU_KY_THUAT" runat="server"></asp:LinkButton>
                                |&nbsp;
                                <asp:LinkButton ID="lbtnKET_QUA_DO_KIEM" runat="server"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnQUY_TRINH_SAN_XUAT" runat="server"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnQUY_TRINH_CHAT_LUONG" runat="server"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnCHUNG_CHI_HE_THONG_QLCL" runat="server"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnTIEU_CHUAN_TU_NGUYEN_AP_DUNG" runat="server"></asp:LinkButton></td>
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
    </form>
</body>
</html>
