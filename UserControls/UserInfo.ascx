<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserInfo.ascx.cs" Inherits="UserControls_UserInfo" %>

<script type="text/javascript">
function popCenter(URL,name,w,h) {	
	l = (screen.width - w) / 2 ;
	t = (screen.height - h) / 2;
	
	params = 'toolbars=1, scrollbars=1, location=0, statusbars=0, menubars=1, resizable=1,';
	popCenterWin = window.open(URL, name, params + 'width=' + w + ', height=' + h + ', left=' + l + ', top=' + t);
	popCenterWin.focus();
	return false;
}
</script>

<table id="table2" cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td nowrap="nowrap" align="right" style="height: 22px; width:auto">
            &nbsp;
            <asp:Label ID="lblWelcome" runat="server" Font-Size="11px" Text="Chào mừng: Admin " Font-Bold="True" ForeColor="#4D4D4D" CssClass="sitepath"></asp:Label>
        </td>
        <td align="right" style="white-space: nowrap; width: 100px; height: 22px;" class="LoginUser">
            &nbsp; -&nbsp;
            <asp:LinkButton ID="lbtDang_xuat" runat="server" CausesValidation="False"
                Font-Bold="True" Font-Underline="True" ForeColor="#4d4d4d" OnClick="lbtDang_xuat_Click" PostBackUrl="~/WebUI/Logout.aspx" CssClass="sitepath">Đăng xuất</asp:LinkButton></td>
    </tr>
</table>
