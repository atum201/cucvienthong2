<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HT_DangNhap.aspx.cs" Inherits="WebUI_HT_DangNhap" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Đăng nhập</title>
</head>
<body style="margin-left: 0; margin-top: 0; margin-right:0;">
    <form id="form1" runat="server">
    <table width="100%" align="center" cellpadding="0px" border="0" cellspacing="0" bgcolor="white"
            style="position: absolute; left: 0; top: 0; right: 0; bottom: 0; height: 90px;">
            <tr>
                <td style="height: 90px; background: #3873d9" colspan="3"
                    valign="top">
                    <img src="../htmls/image/banner.jpg" /></td>
            </tr>
            </table>
  <br />
    <br />
    
      <br />
        <br />
          <br />
            <br />
              <br />
                <br />
                  <br />
                    <br />
                    
                
                  <br />
      <br />
       
        <table align="center"  class="login_form">
            <tr>
                <td background="../Image/login_01.jpg" colspan="1" rowspan="5" valign="top" >
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/htmls/image/locked_area_m15.jpg" /></td>
                <td background="../Image/login_01.jpg" colspan="1" valign="top" style="height: 13px; width: 92px;">
                </td>
                <td background="../Image/login_01.jpg" colspan="1" style="width: 2%" valign="top">
                </td>
                <td background="../Image/login_01.jpg" colspan="3" valign="top" style="height: 13px; width: 150px;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td background="../Image/login_01.jpg" colspan="1" style="width: 92px; height: 29px; text-align: left;"
                    valign="top" nowrap="nowrap">
                    Tên đăng nhập</td>
                <td background="../Image/login_01.jpg" colspan="1" style="width: 2%; height: 29px;
                    text-align: right" valign="top">
                </td>
                <td background="../Image/login_01.jpg" colspan="3" valign="top" style="height: 29px; width: 150px;">
                    <asp:TextBox ID="txtTenDangNhap" runat="server" Width="150"></asp:TextBox></td>
            </tr>
            <tr>
                <td background="../Image/login_01.jpg" colspan="1" valign="top" style="height: 18px; text-align: left; width: 92px;">
                    Mật khẩu</td>
                <td background="../Image/login_01.jpg" colspan="1" style="height: 18px; text-align: right"
                    valign="top">
                </td>
                <td background="../Image/login_01.jpg" colspan="3" valign="top" style="height: 18px; width: 150px;">
                        <asp:TextBox ID="txtMatKhau" runat="server" TextMode="Password" Width="150"></asp:TextBox></td>
            </tr>
            <tr>
                <td background="../Image/login_01.jpg" colspan="1" valign="top" style="height: 21px; width: 92px;">
                </td>
                <td background="../Image/login_01.jpg" colspan="1" style="height: 21px" valign="top">
                </td>
                <td background="../Image/login_01.jpg" colspan="3" valign="top" style="height: 21px; width: 200px;" nowrap="nowrap">
                                <input id="chkSave" runat="server" name="chkSave" type="checkbox" />
                                Lưu thông tin đăng nhập</td>
            </tr>
            <tr>
                <td background="../Image/login_01.jpg" colspan="1" valign="top" style="height: 77px; width: 92px;">
                </td>
                <td background="../Image/login_01.jpg" colspan="1" style="height: 77px" valign="top">
                </td>
                <td background="../Image/login_01.jpg" colspan="3" valign="top" style="height: 77px; width: 150px;">
                                <asp:ImageButton ID="btnDangNhap" runat="server" ImageUrl="~/htmls/image/dangnhap.gif" OnClick="btnDangNhap_Click" /></td>
            </tr>
        </table>
    
   
    </form>
</body>
</html>
