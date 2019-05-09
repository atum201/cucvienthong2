<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_DK_CongBo_PHTC.ascx.cs" Inherits="UserControls_ucDK_CongBo_PHTC" %>
<table border="0" width="100%">
    <tr>
        <td colspan="3">
            Đăng ký công bố hợp quy
        </td>
    </tr>
    <tr>
        <td width="18%">
            Số</td>
        <td colspan="2" width="82%">
            &nbsp;<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>/TTCN</td>
    </tr>
    <tr>
        <td>
            Đơn vị nộp hồ sơ
        </td>
        <td colspan="2">
            &nbsp;<asp:TextBox ID="txtDonVi" runat="server" Width="276px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDonVi"
                ErrorMessage="Nhập tên Đơn vị nộp HS !">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr style="color: #000000">
        <td>
            Tên vật tư thiết bị
        </td>
        <td colspan="2">
            &nbsp;<asp:TextBox ID="txtTenThietBi" runat="server" Width="276px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTenThietBi"
                ErrorMessage="Nhập tên Thiết bị !">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr style="color: #000000">
        <td>
            Ký hiêụ</td>
        <td colspan="2">
            &nbsp;<asp:TextBox ID="txtKyhieu" runat="server" Width="276px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtKyhieu"
                ErrorMessage="Nhập ký hiệu">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr style="color: #000000">
        <td>
            Hình thức
        </td>
        <td>
            &nbsp;<asp:RadioButtonList ID="rbHinhThuc" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True" Value="1">Đ&#227; cấp GCN</asp:ListItem>
                <asp:ListItem Value="0">Tự đ&#225;nh gi&#225; sự phù hợp </asp:ListItem>
            </asp:RadioButtonList></td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rbHinhThuc"
                ErrorMessage="Chọn hình thức !">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr style="color: #000000">
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr style="color: #000000">
        <td>
            Hồ sơ gồm
        </td>
        <td>
            <asp:CheckBox ID="chkTL_BanCongBo" runat="server" />
            Bản công bố
        </td>
        <td>
            &nbsp;<asp:FileUpload ID="FileUpload1" runat="server" Width="275px" /></td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:CheckBox ID="chkTL_BanSaoGCN" runat="server" />
            Bản tư cách pháp nhân
        </td>
        <td>
            &nbsp;<asp:FileUpload ID="FileUpload2" runat="server" Width="275px" /></td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:CheckBox ID="chkTL_TaiLieuKT" runat="server" />
            Tài liệu kỹ thuật
        </td>
        <td>
            &nbsp;<asp:FileUpload ID="FileUpload3" runat="server" Width="275px" /></td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:CheckBox ID="chkTl_BanTuDanhGia" runat="server" />
            Bản tự đánh giá
        </td>
        <td>
            &nbsp;<asp:FileUpload ID="FileUpload4" runat="server" Width="275px" /></td>
    </tr>
    <tr>
        <td style="height: 36px">
            &nbsp;</td>
        <td style="height: 36px">
            <asp:CheckBox ID="chkTl_KetQuaDoKiem" runat="server" />
            Kết quả đo kiểm
        </td>
        <td style="height: 36px">
            &nbsp;<asp:FileUpload ID="FileUpload5" runat="server" Width="275px" /></td>
    </tr>
    <tr>
        <td style="height: 21px">
            &nbsp;</td>
        <td style="height: 21px" valign="top">
            &nbsp;Tài liệu khác<asp:TextBox ID="TextBox1" runat="server" Height="100px" Rows="2"
                TextMode="MultiLine" Width="307px"></asp:TextBox></td>
        <td style="height: 21px">
            &nbsp;<asp:FileUpload ID="FileUpload6" runat="server" Width="275px" /></td>
    </tr>
    <tr>
        <td style="height: 10px">
            &nbsp;</td>
        <td colspan="2" style="height: 10px">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="height: 7px">
            &nbsp;</td>
        <td colspan="2" style="height: 7px">
            &nbsp;
            <asp:Button ID="Button2" runat="server" Text="Đăng ký" />
            <asp:Button ID="Button1" runat="server" Text="Viết lại" UseSubmitBehavior="False" />
            <asp:Button ID="Button3" runat="server" Text="In ấn" />
            <asp:Button ID="Button4" runat="server" Text="Xuất file" />
        </td>
    </tr>
</table>
