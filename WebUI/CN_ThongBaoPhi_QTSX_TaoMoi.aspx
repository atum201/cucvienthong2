﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CN_ThongBaoPhi_QTSX_TaoMoi.aspx.cs" Inherits="WebUI_CN_ThongBaoPhi_QTSX_TaoMoi" Title="Thông báo lệ phí"%>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>

    <script type="text/javascript" src="../Js/jquery-1.3.2-vsdoc2.js"></script>

    <script type="text/javascript" src="../Js/jquery.keypad.js"></script>

    <script type="text/javascript" src="../Js/Common.js"></script>

    <script type="text/javascript" src="../Js/DataGridCheckBoxAll.js"></script>

    <script type="text/javascript">
        var gridCheckBoxPattern = "input:[id$='chkCheck']";
        $(
            function () {
                TinhTongPhi();
                var txtTongPhi = GetControlByName("txtTongPhi");
                txtTongPhi.attr("readonly", "true");
            }
        );


        function TinhTongPhi() {
            var TongPhi = 0;
            $(gridCheckBoxPattern).each(
                function () {
                    if (this.checked) {
                        var text = this.value;
                        //alert(text.ReplaceAll(".", "");
                        //alert(text.replace(new RegExp(/\./gi),''));
                        TongPhi += parseInt(text.replace(new RegExp(/\./gi), ''));

                    }
                }
            );
            //alert(txtTongPhi);
            var txtTongPhi = GetControlByName("txtTongPhi");
            txtTongPhi.val(formatCurrency(TongPhi));
        }
        //        function CalculateFeeTotal(checkBox,TongPhiID,value){
        //            var ctrlTongPhi=document.getElementById(TongPhiID);            
        //            if (checkBox.checked)
        //                ctrlTongPhi.value=value;
        //            else
        //                ctrlTongPhi.value=0;
        //        }
        //        function FillToChucInfor(checkBox,TongPhiID,value){          
        //            var ctrlTongPhi=document.getElementById(TongPhiID);
        //            if (checkBox.checked)
        //                ctrlTongPhi.value=parseInt(ctrlTongPhi.value)+ value;
        //            else
        //                ctrlTongPhi.value=parseInt(ctrlTongPhi.value)- value;            
        //        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 30px">
        </div>
        <div align="center" class="title">
            <strong>
                <asp:Label ID="lblTitle" runat="server" Text="GIẤY BÁO LỆ PHÍ CHỨNG NHẬN"></asp:Label></strong></div>
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
                        Số giấy thông báo phí</td>
                    <td>
                        <asp:TextBox ID="txtSoTBP" runat="server" Width="70%" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSoTBP"
                            ErrorMessage="Bạn phải nhập số giấy thông báo phí">*</asp:RequiredFieldValidator></td>
                    <td style="width: 50px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px; text-align: left">
                    </td>
                    <td style="width: 150px; text-align: left;">
                        Tổ chức, cá nhân</td>
                    <td>
                        <asp:TextBox ID="txtDonVi" runat="server" Width="70%" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox></td>
                    <td style="width: 50px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px; text-align: left">
                    </td>
                    <td style="width: 150px; text-align: left;">
                        Địa chỉ</td>
                    <td>
                        <asp:TextBox ID="txtDiaChi" runat="server" Width="70%" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox></td>
                    <td style="width: 50px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px; text-align: left">
                    </td>
                    <td style="width: 150px; text-align: left;">
                        Điện thoại</td>
                    <td>
                        <asp:TextBox ID="txtDienThoai" runat="server" Width="70%" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox></td>
                    <td style="width: 50px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px; text-align: left">
                    </td>
                    <td style="width: 150px; text-align: left;">
                        Fax</td>
                    <td>
                        <asp:TextBox ID="txtFax" runat="server" Width="70%" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox></td>
                    <td style="width: 50px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px; text-align: left">
                    </td>
                    <td style="width: 150px; text-align: left;">
                        Số lượng Đánh giá QTSX</td>
                    <td>
                        <asp:TextBox ID="txtSlDanhGiaQTSX" runat="server" Width="70%"></asp:TextBox></td>
                    <td style="width: 50px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px; text-align: left">
                    </td>
                    <td style="width: 150px; text-align: left;">
                        Số lượng Lấy mẫu sản phẩm</td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" Width="70%" ></asp:TextBox></td>
                    <td style="width: 50px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px; text-align: left">
                    </td>
                    <td style="width: 150px; text-align: left">
                        Tổng phí</td>
                    <td>
                        <asp:TextBox ID="txtTongPhi" runat="server" BackColor="#FFFFC0" Width="200px">0</asp:TextBox>&nbsp;
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
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" />
    </form>
</body>
</html>
