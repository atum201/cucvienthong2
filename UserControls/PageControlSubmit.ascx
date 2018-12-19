<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageControlSubmit.ascx.cs" Inherits="UserControls_PageControlSubmit" %>
<table border="0" cellpadding="0" cellspacing="4">
    <tr>
        <td>
            <span id="ctlPaging" runat="server">Trang:&nbsp;</span></td>
        <td id="tdFirst" runat="server">
            <a id="hrefFirst" runat="server" href="#" target="_parent">
                <img id="imgFirst" runat="server" alt="First page" border="0" src="~/htmls/image/first.gif"></a></td>
        <td id="tdLeft" runat="server">
            <a id="hrefLeft" runat="server" href="#" target="_parent">
                <img id="imgLeft" runat="server" alt="Previous page" border="0" src="~/htmls/image/left.gif"></a></td>
        <td id="tdPaging" runat="server" valign="middle">
            <a class="Paging" href="#" target="_parent">1</a> <span class="PagingSeperator">|</span>
            <a class="Paging" href="#" target="_parent">2</a> <span class="PagingSeperator">|</span>
            <a class="Paging" href="#" target="_parent">3</a> <span class="PagingSeperator">|</span>
            <a class="Paging" href="#" target="_parent">4</a> <span class="PagingSeperator">|</span>
            <a class="Paging" href="#" target="_parent">5</a> <span class="PagingSeperator">|</span>
            <a class="Paging" href="#" target="_parent">6</a> <span class="PagingSeperator">|</span>
            <a class="Paging" href="#" target="_parent">7</a> <span class="PagingSeperator">|</span>
            <a class="Paging" href="#" target="_parent">8</a>
        </td>
        <td id="tdRight" runat="server">
            <a id="hrefRight" runat="server" href="#" target="_parent">
                <img id="imgRight" runat="server" alt="Next page" border="0" src="~/htmls/image/right.gif"></a></td>
        <td id="tdLast" runat="server">
            <a id="hrefLast" runat="server" href="#" target="_parent">
                <img id="imgLast" runat="server" alt="Last page" border="0" src="~/htmls/image/last.gif"></a></td>
        <td>
            <font id="ctlTotalPage" runat="server" color="#ff0000" size="1">150</font></td>
    </tr>
</table>
