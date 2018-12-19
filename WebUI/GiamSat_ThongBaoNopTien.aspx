<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GiamSat_ThongBaoNopTien.aspx.cs"
    Inherits="WebUI_GiamSat_ThongBaoNopTien" Title="Thông báo nộp tiền" %>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Thông báo nộp tiền</title>

    <script type="text/javascript" src="../Js/jquery-1.3.2-vsdoc2.js"></script>

    <script type="text/javascript" src="../Js/jquery.keypad.js"></script>

    <script type="text/javascript" src="../Js/Common.js"></script>

    <script type="text/javascript" src="../Js/DataGridCheckBoxAll.js"></script>

    <script type="text/javascript"> 
        var gridCheckBoxPattern="input:[id$='chkCheck']";       
        $(
            function(){                
                TinhTongPhi();               
                var txtTongPhi = GetControlByName("txtTongPhi");                      
                txtTongPhi.attr("readonly","true"); 
                
            }
        );
        
         
        function TinhTongPhi(){
            var TongPhi=0;                                
            $(gridCheckBoxPattern).each(
                function(){                    
                    if (this.checked){
                        var text = this.value;
                        //alert(text.ReplaceAll(".", "");
                        //alert(text.replace(new RegExp(/\./gi),''));
                        TongPhi+= parseInt(text.replace(new RegExp(/\./gi),''));
                        
                    }
                }
            );                        
            //alert(txtTongPhi);
            var txtTongPhi = GetControlByName("txtTongPhi");    
             var PhiLayMau= document.getElementById('txtPhiLayMau').value;
	            var PhiDanhGiaQuyTrinh= document.getElementById('txtPhiDanhGiaQuyTrinh').value; 
	            var soQT= document.getElementById('txtSoQuyTrinh').value; 
	            TongPhi += (parseInt(PhiLayMau) + (parseInt(PhiDanhGiaQuyTrinh)*parseInt(soQT)))*1000;
	            document.getElementById('hdnTongPhi').value = TongPhi;                  
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
	            
	            var TongPhi= document.getElementById('txtTongPhi');
	             var PhiLayMau= document.getElementById('txtPhiLayMau').value;
	            var PhiDanhGiaQuyTrinh= document.getElementById('txtPhiDanhGiaQuyTrinh').value;
	            total = parseInt(PhiLayMau) + (parseInt(PhiDanhGiaQuyTrinh)*(oText.value));
	            TongPhi.value = total;
	            TinhTongPhi();
	            
	        }
        }  </script>

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
                    <td style="width: 50px">
                    </td>
                    <td style="width: 150px; text-align: left;">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px">
                    </td>
                    <td style="width: 150px; text-align: left">
                        Số giấy thông báo phí</td>
                    <td>
                        <asp:TextBox ID="txtSoTBP" runat="server" Width="70%" BackColor="Transparent" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSoTBP"
                            ErrorMessage="Bạn phải nhập Số giấy thông báo lệ phí">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="width: 50px">
                    </td>
                    <td style="width: 150px; text-align: left">
                        Ngày ký</td>
                    <td>
                        <asp:TextBox ID="txtNgayKy" runat="server" TabIndex="3" Width="30%"></asp:TextBox>&nbsp;
                        <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtNgayKy" ControlFocusOnError="true"
                            Format="dd mm yyyy" InvalidDateMessage="Nhập sai định dạng ngày tháng" RequiredDate="true"
                            RequiredDateMessage="Bạn phải nhập ngày ký" Separator="/" ShowErrorMessage="False" />
                        (dd/mm/yyyy)
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNgayKy"
                            Display="Dynamic" ErrorMessage="Ngày ký không đúng" ValidationExpression="^(((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}|\d))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}|\d))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}|\d))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00|[048])))$">*</asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td style="width: 50px">
                    </td>
                    <td style="width: 150px; text-align: left">
                        Ngày thu tiền</td>
                    <td>
                        <asp:TextBox ID="txtNgayThuTien" runat="server" TabIndex="3" Width="30%"></asp:TextBox>&nbsp;
                        <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="txtNgayThuTien" ControlFocusOnError="true"
                            Format="dd mm yyyy" InvalidDateMessage="Nhập sai định dạng ngày tháng" RequiredDateMessage="Bạn phải nhập ngày thu tiền"
                            Separator="/" ShowErrorMessage="False" />
                        (dd/mm/yyyy)
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNgayKy"
                            Display="Dynamic" ErrorMessage="Ngày thu tiền không đúng" ToolTip="Ngày thu tiền không đúng"
                            ValidationExpression="^(((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}|\d))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}|\d))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}|\d))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00|[048])))$">*</asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td style="width: 50px">
                    </td>
                    <td style="width: 150px; text-align: left">
                        Số hóa đơn</td>
                    <td>
                        <asp:TextBox ID="txtSoHoaDon" runat="server" BackColor="Transparent" Width="70%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 50px">
                    </td>
                    <td style="width: 150px; text-align: left;">
                        Tổ chức, cá nhân</td>
                    <td>
                        <asp:TextBox ID="txtDonVi" runat="server" Width="70%" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 50px">
                    </td>
                    <td style="width: 150px; text-align: left;">
                        Địa chỉ</td>
                    <td>
                        <asp:TextBox ID="txtDiaChi" runat="server" Width="70%" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 50px">
                    </td>
                    <td style="width: 150px; text-align: left;">
                        Điện thoại</td>
                    <td>
                        <asp:TextBox ID="txtDienThoai" runat="server" Width="70%" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 50px">
                    </td>
                    <td style="width: 150px; text-align: left; height: 26px;">
                        Fax</td>
                    <td style="height: 26px">
                        <asp:TextBox ID="txtFax" runat="server" Width="70%" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 50px" valign="top">
                    </td>
                    <td style="width: 150px; text-align: left;" valign="top">
                        Các giấy chứng nhận cần thu phí 
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="CheckItemCheckBox"
                            ControlToValidate="txtTongPhi" ErrorMessage="Bạn phải chọn một sản phẩm." ToolTip="Bạn phải chọn một sản phẩm.">*</asp:CustomValidator></td>
                    <td style="text-align: left" valign="top">
                        <asp:GridView ID="gvPhi" runat="server" AutoGenerateColumns="False" EmptyDataText="Không có sản phẩm nào được cấp GCN"
                            PageSize="15" Width="98%" OnDataBound="gvPhi_DataBound" DataKeyNames="SanPhamID">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <input id="chkCheckAll" runat="server" type="checkbox" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <input id="chkCheck" name="ckItem" type="checkbox" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="SoGCN" HeaderText="Số giấy chứng nhận">
                                    <ItemStyle Width="140px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TenSanPham" HeaderText="Sản phẩm" />
                                <asp:BoundField DataField="LePhi" HeaderText="Lệ Ph&#237; (VNĐ)" DataFormatString="{0:#,#}.000">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px" valign="top">
                    </td>
                    <td style="width: 150px; text-align: left; height: 26px;" valign="top">
                        Phí lấy mẫu sản phẩm&nbsp;</td>
                    <td style="height: 26px">
                        <asp:TextBox ID="txtPhiLayMau" runat="server" ReadOnly="True" BackColor="#FFFFC0"></asp:TextBox>
                        (VNĐ)
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px" valign="top">
                    </td>
                    <td style="width: 150px; text-align: left" valign="top">
                        Phí đánh giá quy trình
                    </td>
                    <td>
                        <asp:TextBox ID="txtPhiDanhGiaQuyTrinh" runat="server" ReadOnly="True" BackColor="#FFFFC0"></asp:TextBox>
                        (VNĐ)
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px" valign="top">
                    </td>
                    <td style="width: 150px; text-align: left" valign="top">
                        Số quy trình
                    </td>
                    <td>
                        <asp:TextBox ID="txtSoQuyTrinh" runat="server" Text="1" onkeyup="return ReCanculateTotal(this);" MaxLength="8"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtSoQuyTrinh"
                            ErrorMessage="Cần phải có ít nhất một quy trình cần đánh giá" Operator="GreaterThanEqual"
                            ValueToCompare="0">*</asp:CompareValidator></td>
                </tr>
                <tr>
                    <td style="width: 50px">
                    </td>
                    <td style="width: 150px; text-align: left">
                        Tổng phí</td>
                    <td>
                        <asp:TextBox ID="txtTongPhi" runat="server" BackColor="#FFFFC0" Width="200px" ReadOnly="True" />&nbsp;
                        (VND)
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px">
                    </td>
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
        <script language="javascript" type="text/javascript">
            function CheckItemCheckBox(source,args)
            {
            var result=false;                             
            $(gridCheckBoxPattern).each(
                function(){                    
                    if (this.checked){                      
                        result=true;
                    }
                }
            );       
            if(result)
	         {
	         args.IsValid=true;
	         }
	         else
	         {args.IsValid=false;}
            }

        </script>

    </form>
</body>
</html>
