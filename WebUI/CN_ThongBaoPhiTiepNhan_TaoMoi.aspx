<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CN_ThongBaoPhiTiepNhan_TaoMoi.aspx.cs" Inherits="WebUI_CN_ThongBaoPhiTiepNhan_TaoMoi" Title="Thông báo lệ phí tiếp nhận"%>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script type="text/javascript" src="../Js/jquery-1.3.2-vsdoc2.js"></script>

    <script type="text/javascript" src="../Js/jquery.keypad.js"></script>

    <script type="text/javascript" src="../Js/Common.js"></script>

    <script type="text/javascript" src="../Js/DataGridCheckBoxAll.js"></script>

    <script type="text/javascript"> 
        function TinhTongPhi() {
            var txtTongPhi = GetControlByName("txtTongPhi");
            var sl = GetControlByName("txtSLTiepNhan").val();
            var tn = parseInt(GetControlByName("hfDonGiaTiepNhan").val());
            var xx = parseInt(GetControlByName("hfDonGiaXemXet").val());
            var tongphi = parseInt(sl) * (tn + xx);
            txtTongPhi.val(tongphi);
        }
        $(document).ready(function () {
            TinhTongPhi();
            $(".sltnchange").change(TinhTongPhi);
        })
         
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 30px">
            <asp:HiddenField ID="hfDonGiaTiepNhan" runat="server" />
            <asp:HiddenField ID="hfDonGiaXemXet" runat="server" />
        </div>
        <div align="center" class="title">
            <strong>
                <asp:Label ID="lblTitle" runat="server" Text="GIẤY BÁO LỆ PHÍ TIẾP NHẬN & XEM XÉT HỒ SƠ"></asp:Label></strong></div>
        <div>
            <table style="width: 100%">
                <tr>
                    <td style="width: 50px; text-align: left">
                    </td>
                    <td style="width: 150px; text-align: left;">
                    </td>
                    <td>
                        &nbsp;</td>
                    <td style="width: 50px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px; text-align: left">
                    </td>
                    <td style="width: 150px; text-align: left">
                        Người ký giấy báo phí</td>
                    <td>
                        <asp:DropDownList ID="ddlNguoiKy" runat="server" Width="90%" TabIndex="24"></asp:DropDownList></td>
                    <td style="width: 50px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px; text-align: left">
                    </td>
                    <td style="width: 150px; text-align: left;">
                        Thẩm quyền</td>
                    <td>
                        <asp:DropDownList ID="ddlThamQuyen" runat="server" Width="90%" TabIndex="25"></asp:DropDownList></td>
                    <td style="width: 50px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px; text-align: left">
                    </td>
                    <td style="width: 150px; text-align: left;">
                        Số lượng tiếp nhận</td>
                    <td>
                        <asp:TextBox ID="txtSLTiepNhan" class="sltnchange" runat="server" Width="90%" TabIndex="26"></asp:TextBox></td>
                    <td style="width: 50px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px; text-align: left">
                    </td>
                    <td style="width: 150px; text-align: left">
                        Tổng phí</td>
                    <td>
                        <asp:TextBox ID="txtTongPhi" runat="server" BackColor="#FFFFC0" Width="200px"></asp:TextBox>&nbsp;
                        (VND)</td>
                    <td style="width: 50px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px; text-align: left">
                    </td>
                    <td style="width: 150px; text-align: left;">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" Width="90px" OnClick="btnCapNhat_Click" /><asp:Button
                            ID="btnBoQua" runat="server" Text="Bỏ qua" Width="90px" CausesValidation="False"
                            OnClick="btnBoQua_Click" /></td>
                    <td style="width: 50px">
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
