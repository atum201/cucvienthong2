<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DieuKienInBaoCao.aspx.cs" Inherits="WebUI_DieuKienInBaoCao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Phần mền quản lý chất lượng sản phẩm chuyên ngành viễn thông</title>

    <script type="text/javascript" src="../Js/Common.js"></script>

    <script type="text/javascript" src="../Js/DataGridCheckBoxAll.js"></script>

    <script type="text/javascript" src="../Js/TreeViewCheckBox.js"></script>

    <script type="text/javascript" src="../Js/TreeViewCheckAll.js"></script>

    <%--Các JavaScript dùng để tạo Resizeable div--%>

    <script type="text/javascript" src="../Js/genresize.js"></script>

    <script type="text/javascript" src="../Js/ieemu.js"></script>

    <script type="text/javascript">
        if (moz) {
	        extendElementModel();
	        extendEventObject();
	        emulateEventHandlers(["mousemove", "mousedown", "mouseup"]);
        }       
        var CR = false;
        function ChonLoaiBaoCao(){	 
	
	         var objList=document.getElementById('ChonDinhDangBaoCao');	
	         if(objList!=null) 
	         {	 	        
		            for(var i = 0 ; i < objList.options.length; i++)
		            {
		                if(objList.options[i].selected)
		                {
		                    var val = objList.options[i].value;
	                        if('CR'==val)
	                        {	                          
	                            CR  = true;
	                            break;
	                        }	
	                        else 
	                            CR = false;                        
	                    }
	                    else 
	                        CR = false;
		            }
	         } 
        }
    </script>

    <%--End of Các JavaScript dùng để tạo Resizeable div--%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0" width="100%" style="padding-top: 5px" border="0">
    <tr>
        <td colspan="3">
            <asp:Label ID="Label68" runat="server">Điều kiện bắt buộc:</asp:Label>
        </td>
    </tr>   
    <tr>
        <td align="left" style="width: 40%; padding-left: 10px">
            <asp:Label runat="Server" ID="Label18">Định dạng xuất báo cáo:</asp:Label>
        </td>
        <td align="left" style="width: 60%; text-align: left">
            <asp:DropDownList ID="ChonDinhDangBaoCao" runat="server" Width="200" >
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="left" style="width: 40%; padding-left: 10px">
            <asp:Label runat="Server" ID="NhanInThongTinMatKhau">In thông tin về tên truy cập, mật khẩu:</asp:Label>
        </td>
        <td align="left" style="width: 60%; text-align: left">
            <asp:CheckBox ID="InThongTinMatKhau" runat="server">
            </asp:CheckBox>
        </td>
    </tr>
        <tr>
            <td align="left" style="padding-left: 10px; width: 40%">
            </td>
            <td align="left" style="width: 60%; text-align: left">
        <asp:Button ID="btnIn" runat="server" Text="In báo cáo" CausesValidation="False" OnClick="btnPrint_Click" OnClientClick="if(CR) { self.resizeTo(850,700);window.moveTo(80,50); } else {  } return true;"/>
        <asp:Button ID="btnDong" runat="server" Text="Đóng" CausesValidation="False" OnClientClick="window.close();return false;"/></td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
