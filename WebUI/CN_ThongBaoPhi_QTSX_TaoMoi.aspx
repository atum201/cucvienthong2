<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CN_ThongBaoPhi_QTSX_TaoMoi.aspx.cs" Inherits="WebUI_CN_ThongBaoPhi_QTSX_TaoMoi" Title="Thông báo lệ phí"%>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Thông báo nộp tiền</title>

    <script type="text/javascript" src="../Js/jquery-1.3.2-vsdoc2.js"></script>

    <script type="text/javascript" src="../Js/jquery.keypad.js"></script>

    <script type="text/javascript" src="../Js/Common.js"></script>

    <script type="text/javascript"> 
        function ReCanculateTotal(value)
        {
            autocalc(value);
        }
        function autocalc(oText)
        {
	        if (isNaN(oText.value)) //filter input
	        {
		        alert('Chỉ được nhập số! Xin vui lòng nhập lại.');
		        oText.value = '1';
	        }else
	        {
	            var PhiLayMau= document.getElementById('txtPhiLayMau').value;
	            var PhiDanhGiaQuyTrinh= document.getElementById('txtPhiDanhGiaQuyTrinh').value;
	            var TongPhi= document.getElementById('txtTongPhi');
	            
	            total = parseInt(PhiLayMau) + (parseInt(PhiDanhGiaQuyTrinh)*(oText.value));
	            TongPhi.value = total;
	            
	            document.getElementById('hdnTongPhi').value = total;
	        }
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 30px">
        </div>
        <div align="center" class="title">
            <strong>
                <asp:Label ID="lblTitle" runat="server" Text="THÔNG BÁO NỘP TIỀN LẤY MẪU SẢN PHẨM VÀ ĐÁNH GIÁ QUY TRÌNH SẢN XUẤT"></asp:Label></strong></div>
        <div>
            <table style="width: 100%">
                <tr>
                    <td style="width: 150px; text-align: left;">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px; text-align: left">
                        Số giấy thông báo phí</td>
                    <td>
                        <asp:TextBox ID="txtSoTBP" runat="server" Width="70%" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSoTBP"
                            ErrorMessage="Bạn phải nhập Số giấy thông báo lệ phí">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="width: 150px; text-align: left;">
                        Tổ chức, cá nhân</td>
                    <td>
                        <asp:TextBox ID="txtDonVi" runat="server" Width="70%" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 150px; text-align: left;">
                        Địa chỉ</td>
                    <td>
                        <asp:TextBox ID="txtDiaChi" runat="server" Width="70%" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 150px; text-align: left;">
                        Điện thoại</td>
                    <td>
                        <asp:TextBox ID="txtDienThoai" runat="server" Width="70%" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 150px; text-align: left;">
                        Fax</td>
                    <td>
                        <asp:TextBox ID="txtFax" runat="server" Width="70%" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 150px; text-align: left; height: 26px;" valign="top">
                        Phí lấy mẫu sản phẩm&nbsp;</td>
                    <td style="height: 26px">
                        <asp:TextBox ID="txtPhiLayMau" runat="server" ReadOnly="True" BackColor="#FFFFC0"></asp:TextBox>
                        (VND)
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px; text-align: left" valign="top">
                        Phí đánh giá quy trình
                    </td>
                    <td>
                        <asp:TextBox ID="txtPhiDanhGiaQuyTrinh" runat="server" ReadOnly="True" BackColor="#FFFFC0"></asp:TextBox>
                        (VND)
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px; text-align: left" valign="top">
                        Số lấy mẫu
                    </td>
                    <td>
                        <asp:TextBox ID="txtSoLayMau" runat="server" Text="1" onkeyup="return ReCanculateTotal(this);"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtSoLayMau"
                            ErrorMessage="Cần phải có ít nhất một lấy mẫu cần đánh giá" Operator="GreaterThanEqual"
                            ValueToCompare="0">*</asp:CompareValidator></td>
                </tr>
                <tr>
                    <td style="width: 150px; text-align: left" valign="top">
                        Số quy trình
                    </td>
                    <td>
                        <asp:TextBox ID="txtSoQuyTrinh" runat="server" Text="1" onkeyup="return ReCanculateTotal(this);"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtSoQuyTrinh"
                            ErrorMessage="Cần phải có ít nhất một quy trình cần đánh giá" Operator="GreaterThanEqual"
                            ValueToCompare="0">*</asp:CompareValidator></td>
                </tr>
                
                <tr>
                    <td style="width: 150px; text-align: left">
                        Tổng phí</td>
                    <td>
                        <asp:TextBox ID="txtTongPhi" runat="server" BackColor="#FFFFC0" Width="200px" ReadOnly="True">0</asp:TextBox>&nbsp;
                        (VND)
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px; text-align: left;">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" Width="90px" OnClick="btnCapNhat_Click" /><asp:Button
                            ID="btnBoQua" runat="server" Text="Bỏ qua" Width="90px" CausesValidation="False"
                            OnClick="btnBoQua_Click" /></td>
                </tr>
            </table>
        </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" />
        <asp:HiddenField ID="hdnTongPhi" runat="server" />
    </form>
</body>
</html>