<%@ Page AutoEventWireup="true" CodeFile="DM_SanPham.aspx.cs" Inherits="WebUI_DM_SanPham"
    Language="C#" MasterPageFile="~/MasterPage/Main.master" Title="Danh mục sản phẩm" %>

<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc2" %>
<%@ Register Namespace="Fadrian.Web.Control" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="Server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script>
document.attachEvent("onkeydown", my_onkeydown_handler); 
function my_onkeydown_handler() 

{ 

switch (event.keyCode) 

{ 

 case 116 : // 'F5' 

event.returnValue = false; 

event.keyCode = 0; 

window.status = "You can't refresh this page............."; 

break; 

} 

}



    </script>

    <div style="margin: 10px auto 10px 10px;">
        <strong>DANH MUC >> SẢN PHẨM</strong>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="height: 181px">
                <div align="center">
                </div>
                <fieldset style="width: 98%;">
                    <legend>Tìm kiếm</legend>
                    <table align="center" border="0" width="100%">
                        <tr>
                            <%--<td align="right" style="height: 26px; vertical-align: top; text-align: left;">
                                Mã sản phẩm</td>
                            <td align="left" style="height: 26px; width: 215px;">
                                <asp:TextBox ID="txtMaSP" runat="server" Width="250px"></asp:TextBox></td>--%>
                            <td style="height: 26px; text-align: left">
                                Tên tiếng Việt</td>
                            <td align="left" style="height: 26px; width: 269px;">
                                <asp:TextBox ID="txtTenSP" runat="server" Width="250px"></asp:TextBox></td>
                            <td rowspan="3" style="vertical-align: top; text-align: right">
                                Tiêu chuẩn áp dụng</td>
                            <td rowspan="4" style="vertical-align: top; text-align: left">
                                <asp:Panel ID="Panel1" runat="server" BorderWidth="1px" Height="100px" ScrollBars="Both"
                                    Width="200px">
                                    <asp:CheckBoxList ID="chkTieuChuan" runat="server" Width="100%">
                                    </asp:CheckBoxList></asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 12px; text-align: left;">
                                Tên tiếng Anh</td>
                            <td align="left" style="height: 12px; width: 269px;">
                                <asp:TextBox ID="txtTenSPTA" runat="server" Width="250px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="height: 24px; text-align: left">
                                Mã nhóm sản phẩm</td>
                            <td align="left" style="height: 24px; width: 269px;">
                                <cc2:ComboBox ID="cbNhomSP" runat="server" Width="255px">
                                </cc2:ComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top; height: 26px; text-align: left">
                                Loại tiêu chuẩn</td>
                            <td align="left" colspan="2" style="height: 26px">
                                <asp:DropDownList ID="ddlLoaiTieuChuanApDung" runat="server" Width="428px">
                                    <asp:ListItem>--tất cả--</asp:ListItem>
                                    <asp:ListItem Value="A">Ti&#234;u chuẩn Ng&#224;nh hoặc ti&#234;u chuẩn quốc tế bắt buộc &#225;p dụng</asp:ListItem>
                                    <asp:ListItem Value="B">Ti&#234;u chuẩn quốc tế kh&#244;ng bắt buộc &#225;p dụng</asp:ListItem>
                                    <asp:ListItem Value="C">Ti&#234;u chuẩn do tổ chức, c&#225; nh&#226;n đăng k&#253;</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="height: 26px; text-align: left; vertical-align: top;">
                            </td>
                            <td style="height: 21px; text-align: left; width: 269px;">
                                <asp:Button ID="btnTimKiem" runat="server" OnClick="btnTimKiem_Click" Text="Tìm kiếm" /></td>
                            <td style="height: 21px; text-align: right;">
                                &nbsp;</td>
                            <td style="height: 21px; text-align: left;">
                                &nbsp;</td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <div align="center">
                    <fieldset style="width: 98%;">
                        <legend>Danh mục</legend>
                        <table border="0" width="100%">
                            <tr>
                                <td align="right" colspan="4" style="text-align: right">
                                    &nbsp;<asp:ImageButton ID="imgThemMoi" runat="server" Height="16px" ImageUrl="~/htmls/image/new_f2.png"
                                        OnClientClick="popCenter('DM_SanPham_ChiTiet.aspx','DanhMucSanPham', 950,600); return false;"
                                        Width="16px" />&nbsp;<asp:LinkButton ID="llbThemMoi" runat="server" OnClientClick="popCenter('DM_SanPham_ChiTiet.aspx','DanhMucSanPham', 950,600); return false;">Thêm mới</asp:LinkButton>
                                    &nbsp;
                                    <asp:LinkButton ID="lnkXoa" runat="server" OnClick="lnkXoa_Click" OnClientClick="if(GridIsChecked('gvSanPham')) { return confirm('Bạn có chắc chắn muốn xóa các sản phẩm đã chọn không?');} else {alert('Bạn chưa chọn sản phẩm cần xóa.'); return false;}"><img src="../htmls/image/cancel_f2.png" border="0" alt="" width="16px" />&nbsp;Xóa</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td colspan="4" valign="top">
                                    <cc1:PagingGridView ID="gvSanPham" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="ID,TenTiengViet" EmptyDataText="Không có dữ liệu !" OnDataBound="gvSanPham_DataBound"
                                        OnPageIndexChanging="gvSanPham_PageIndexChanging" OnRowDataBound="gvSanPham_RowDataBound"
                                        PageSize="15" VirtualItemCount="-1" Width="100%" AllowMultiColumnSorting="True"
                                        AllowSorting="True" OnSorting="gvSanPham_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="T&#234;n  sản phẩm" SortExpression="TenTiengViet">
                                                <itemtemplate>
                                                    <a href="#" onclick="return popCenter('DM_SanPham_ChiTiet.aspx?id=<%# Server.UrlEncode(Eval("ID").ToString()) %>','DM_SanPham_ChiTiet',950,600);">
                                                        <%# Eval("TenTiengViet")%>
                                                    </a>
                                                
                                                </itemtemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TenTiengAnh" HeaderText="T&#234;n sản phẩm Tiếng Anh"
                                                SortExpression="TenTiengAnh" />
                                            <asp:BoundField DataField="manhom" HeaderText="M&#227; nhóm SP" />
                                            <asp:BoundField DataField="Tieuchuan" HeaderText="Ti&#234;u chuẩn &#225;p dụng" />
                                            <asp:BoundField DataField="LoaiTieuChuan_Text" HeaderText="Loại ti&#234;u chuẩn" />
                                            <asp:TemplateField>
                                                <headertemplate>
                                                    <asp:CheckBox ID="chkCheckAll" runat="server" />                                                
                                                </headertemplate>
                                                <itemstyle width="1px" />
                                                <headerstyle width="1px" />
                                                <itemtemplate>
                                                    <asp:CheckBox ID="chkCheck" runat="server" />                                                
                                                </itemtemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings Mode="NumericFirstLast" />
                                    </cc1:PagingGridView>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
