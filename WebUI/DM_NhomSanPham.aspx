<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="DM_NhomSanPham.aspx.cs" Inherits="WebUI_DM_NhomSanPham" Title="Danh mục nhóm sản phẩm"
    Theme="Default" %>

<%@ Register Namespace="Fadrian.Web.Control" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
        <strong>DANH MUC >> NHÓM SẢN PHẨM</strong>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <div align="center">
                    <fieldset style="width: 97%">
                        <legend>Tìm kiếm</legend>
                        <table align="center" border="0" width="100%">
                            <tr>
                                <td align="right" style="height: 26px; width: 200px;">
                                    Mã nhóm trong GCN</td>
                                <td align="left" colspan="3" style="height: 26px">
                                    <asp:TextBox ID="txtMaTT" runat="server"></asp:TextBox>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right" style="height: 26px">
                                    Tên nhóm</td>
                                <td align="left" colspan="3" style="height: 26px">
                                    <asp:TextBox ID="txtTenNhomSP" runat="server" Width="337px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="right" style="height: 15px">
                                    Mức lệ phí</td>
                                <td align="left" colspan="3" style="height: 15px">
                                    <asp:TextBox ID="txtMucLePhi" runat="server" Width="155px"></asp:TextBox>(Đơn vị:1000đ)</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td style="width: 162px; text-align: left">
                                    <asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" OnClick="btnTimKiem_Click" /></td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div align="center">
                    <fieldset style="width: 97%">
                        <legend>Danh mục</legend>
                        <table border="0" width="100%">
                            <tr>
                                <td align="right" colspan="4" style="text-align: right; height: 22px;">
                                    &nbsp;<asp:ImageButton ID="imgThemMoi" runat="server" Height="16px" ImageUrl="~/htmls/image/new_f2.png"
                                        Width="16px" OnClientClick="popCenter('DM_NhomSanPham_ChiTiet.aspx?direct=add','DM_NhomSanPham_ChiTiet',720,230);return false;" />&nbsp;
                                    <asp:LinkButton ID="lnkThemMoi" runat="server" OnClientClick="popCenter('DM_NhomSanPham_ChiTiet.aspx?direct=add','DM_NhomSanPham_ChiTiet',720,250);return false;">Thêm mới</asp:LinkButton>
                                    &nbsp;
                                    <asp:LinkButton ID="lnkXoa" runat="server" OnClick="btnXoa_Click" OnClientClick="if(GridIsChecked('gvNhomSP')) { return confirm('Bạn có chắc chắn muốn xóa các nhóm sản phẩm đã chọn không?');} else {alert('Bạn chưa chọn nhóm sản phẩm cần xóa.'); return false;}"><img src="../htmls/image/cancel_f2.png" border="0" alt="" width="16px" />&nbsp;Xóa</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td colspan="4" valign="top">
                                    <cc1:PagingGridView ID="gvNhomSP" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,TenNhom"
                                        EmptyDataText="Không có dữ liệu !" OnDataBound="gvNhomSP_DataBound" OnPageIndexChanging="gvNhomSP_PageIndexChanging"
                                        PageSize="15" VirtualItemCount="-1" Width="100%" AllowPaging="True" AllowMultiColumnSorting="True"
                                        AllowSorting="True" OnSorting="gvNhomSP_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="T&#234;n nh&#243;m sản phẩm" SortExpression="TenNhom">
                                                <itemtemplate>
                                                    <a href="#" onclick="return popCenter('DM_NhomSanPham_ChiTiet.aspx?direct=edit&id=<%# Server.UrlEncode(Eval("ID").ToString()) %>','DM_NhomSanPham_ChiTiet',800,250);">
                                                        <%# Eval("TenNhom")%>
                                                    </a>
                                                
</itemtemplate>
                                                <ItemStyle Font-Underline="False" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="MaNhom" HeaderText="M&#227; nh&#243;m trong GCN" SortExpression="MaNhom"/>
                                            <asp:BoundField DataField="MucLePhi" HeaderText="Mức lệ ph&#237; (VNĐ)" DataFormatString="{0:#,#}.000"
                                                HtmlEncode="False" SortExpression="MucLePhi"/>
                                            <asp:BoundField DataField="ThoiHan" HeaderText="Thời hạn" SortExpression="ThoiHanGCN"/>
                                            <asp:CheckBoxField DataField="LienQuanTanSo" HeaderText="Li&#234;n quan tần số" ReadOnly="True" SortExpression="LienQuanTanSo">
                                                <itemstyle width="70px" />
                                            </asp:CheckBoxField>
                                            <asp:TemplateField>
                                                <headertemplate>
                                                    <asp:CheckBox ID="chkCheckAll" runat="server" />
                                                
</headertemplate>
                                                <itemtemplate>
                                                    <asp:CheckBox ID="chkCheck" runat="server" />
                                                
                                                
</itemtemplate>
                                                <headerstyle width="1px" />
                                                <itemstyle width="1px" />
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
