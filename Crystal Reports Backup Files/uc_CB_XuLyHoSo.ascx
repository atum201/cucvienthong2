<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_CB_XuLyHoSo.ascx.cs" Inherits="UserControls_uc_CB_DanhGiaHoSo" %>
<div><span style="font-family: Arial"><a href = "../WebUI/CN_HoSo_QuanLy.aspx">CÔNG BỐ</a> &gt;&gt; <% string SoHoSo = Request.QueryString["SoHoSo"];%><a href = "../WebUI/CN_HoSoSanPham_QuanLy.aspx?SoHoSo=<%=Request.QueryString["SoHoSo"]%>&direct=<%=Request.QueryString["direct"]%>">DANH SÁCH HỒ SƠ SẢN PHẨM MỚI NHẬN</a> </span>
</div>
<div align="center">

    <fieldset style="width: 97%">
        <legend>Tìm kiếm</legend>
        <table align="center" border="0" width="100%">
            <tr>
                <td width="24%">
                    &nbsp;</td>
                <td width="17%">
                    &nbsp;</td>
                <td width="28%">
                    &nbsp;</td>
                <td width="31%">
                </td>
                <td width="31%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right" style="height: 26px">
                    Đơn vị cá nhân&nbsp;</td>
                <td align="left" colspan="2" style="height: 26px">
                    &nbsp;
                    <asp:TextBox ID="txtNgayPhanCong" runat="server" Width="213px"></asp:TextBox></td>
                <td align="left" style="height: 26px; text-align: right">
                    ĐT</td>
                <td align="left" style="height: 26px">
                    &nbsp;<asp:TextBox ID="TextBox1" runat="server" Width="143px"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    Nhận hồ sơ từ</td>
                <td align="left" colspan="2">
                    &nbsp;<asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">Trực tiếp</asp:ListItem>
                        <asp:ListItem Value="0">Bưu điện</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td align="left" style="text-align: right">
                    Fax</td>
                <td align="left">
                    &nbsp;<asp:TextBox ID="TextBox2" runat="server" Width="143px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="height: 21px; text-align: right">
                    &nbsp;Số ngày/đơn</td>
                <td colspan="4" style="height: 21px">
                    <asp:TextBox ID="TextBox3" runat="server" Width="213px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="height: 21px; text-align: right">
                    Sản phẩm</td>
                <td colspan="4" style="height: 21px">
                    <asp:TextBox ID="TextBox4" runat="server" Width="213px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="height: 21px; text-align: right">
                    Ký hiệu</td>
                <td colspan="4" style="height: 21px">
                    <asp:TextBox ID="TextBox5" runat="server" Width="213px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="height: 21px; text-align: right">
                </td>
                <td colspan="4" style="height: 21px">
                    &nbsp; &nbsp;&nbsp;<asp:RadioButtonList ID="rbHinhThuc" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">Đ&#227; cấp GCN</asp:ListItem>
                        <asp:ListItem Value="0">Tự đ&#225;nh gi&#225; sự phù hợp </asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td style="height: 21px; text-align: right">
                    Liên hệ với</td>
                <td colspan="4" style="height: 21px">
                    <asp:TextBox ID="TextBox6" runat="server" Width="213px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="height: 21px; text-align: right">
                </td>
                <td colspan="4" style="height: 21px">
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="height: 21px">
                    &nbsp;</td>
                <td style="height: 21px">
                    &nbsp;</td>
                <td style="height: 21px">
                    &nbsp;</td>
                <td style="height: 21px">
                </td>
                <td style="height: 21px">
                    &nbsp;</td>
            </tr>
        </table>
        <p>
            &nbsp;</p>
    </fieldset>
    <div align="center">
        <fieldset style="width: 97%">
            <legend>Quá trình xử lý</legend>
            <table border="0" width="100%">
                <tr>
                    <td colspan="4" style="width: 468px">
                        &nbsp; &nbsp;&nbsp;</td>
                    <td width="5%">
                    </td>
                    <td width="5%">
                    </td>
                    <td width="5%">
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="width: 468px" valign="top">
                        &nbsp;Nội dung xử lý(CV1)</td>
                    <td colspan="3" valign="top">
                        <asp:TextBox ID="TextBox7" runat="server" Height="100px" TextMode="MultiLine" Width="250px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="4" style="width: 468px" valign="top">
                        Ghi chú(CV1)</td>
                    <td colspan="3" valign="top">
                        <asp:TextBox ID="TextBox8" runat="server" Height="100px" TextMode="MultiLine" Width="250px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="4" style="width: 468px" valign="top">
                        Nội dung xử lý(CV2)</td>
                    <td colspan="3" valign="top">
                        <asp:TextBox ID="TextBox9" runat="server" Height="100px" TextMode="MultiLine" Width="250px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="4" style="width: 468px" valign="top">
                        Ghi chú(CV2)</td>
                    <td colspan="3" valign="top">
                        <asp:TextBox ID="TextBox10" runat="server" Height="100px" TextMode="MultiLine" Width="250px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="4" style="width: 468px; text-align: left">
                        &nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" Text="Cập nhật" />
                        <asp:Button ID="Button2" runat="server" Text="Bỏ qua" />
                        &nbsp; &nbsp;&nbsp;</td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <p>
                &nbsp;</p>
        </fieldset>
    </div>
</div>
