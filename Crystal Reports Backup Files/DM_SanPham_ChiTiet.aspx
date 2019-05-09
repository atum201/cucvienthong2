<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DM_SanPham_ChiTiet.aspx.cs"
    Inherits="WebUI_DM_SanPham_ChiTiet" Title="Tạo mới sản phẩm" Theme="Default" %>
<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
    <%@ Register Namespace="Fadrian.Web.Control" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tỉnh thành</title>

    <script type="text/javascript" src="../Js/Common.js"></script>

    <script type="text/javascript" src="../Js/dhtmlxcombo.js"></script>

    <script type="text/javascript" src="../Js/dhtmlxcommon.js"></script>

    <script type="text/javascript" src="../Js/DataGridCheckBoxAll.js"></script>

    <script type="text/javascript" src="../Js/TreeViewCheckBox.js"></script>

    <script type="text/javascript" src="../Js/TreeViewCheckAll.js"></script>
    <script type="text/javascript" src="../Js/jquery-1.3.2-vsdoc2.js"></script>
    
    <script type="text/javascript">
        function CloseForm(){
            self.opener.window.document.forms[0].submit();
            window.close();
        }    
    </script>
   

</head>
<body class="body_popup">

    <form id="form1" runat="server">
     <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" />
        <div align="center">
            <fieldset style="width: 97%; text-align: left;">
                
                    <legend>
                        <div style="margin: 10px auto 10px 10px;">
                            <strong>THÔNG TIN SẢN PHẨM</strong>&nbsp;</div>
                    </legend>
                    <table align="center" border="0" width="100%" style="color: #000000">
                     
                        <tr>
                            <td align="left" style="width: 150px;" class="caption">
                                Tên sản phẩm<span>(*)</span></td>
                            <td align="left" style="width: 200px;">
                                <asp:TextBox ID="txtTenSP" runat="server" Width="90%" MaxLength="255"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTenSP"
                                    ErrorMessage="Bạn phải nhập tên sản phẩm">*</asp:RequiredFieldValidator></td>
                            <td align="left" style="width: 150px;" class="caption">
                                Tiêu chuẩn áp dụng (*)</td>
                            <td align="left" style="width: 152px;" rowspan="4" valign="top">
                                <asp:Panel ID="Panel1" runat="server" BorderWidth="1px" Height="100px" ScrollBars="Both"
                                    Width="150px">
                                    <asp:CheckBoxList ID="chkTieuChuan" runat="server" Width="100%">
                                    </asp:CheckBoxList>
                                </asp:Panel></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 150px;" class="caption">
                                Tên tiếng Anh</td>
                            <td align="left" style="width: 200px;">
                                <asp:TextBox ID="txtTenSPTA" runat="server" Width="90%" MaxLength="255"></asp:TextBox></td>
                            <td align="left" style="width: 150px;" class="caption">
                            </td>
                        </tr>
                       <%-- <tr>
                            <td style="width: 165px;" align="left" class="caption">
                                <asp:Label ID="lblMaSanPham" runat="server" Text="Mã sản phẩm"></asp:Label></td>
                            <td style="width: 489px;" align="left">
                                <asp:TextBox ID="txtMaSP" runat="server" Width="235px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td style="width: 301px" align="left" class="caption">
                            </td>
                        </tr>--%>
                        <tr>
                            <td style="width: 150px;" align="left" class="caption">
                               <asp:Label ID="lblMaNhom" runat="server" Text=" Mã nhóm sản phẩm"></asp:Label></td>
                            <td style="width: 200px;" align="left">
                                <cc1:combobox id="cbNhomSP" runat="server" width="91%" Height="26px" AutoPostBack="True" OnSelectedIndexChanged="cbNhomSP_SelectedIndexChanged">
                                </cc1:combobox>
                            </td>
                            <td style="width: 150px" align="left" class="caption">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px;" align="left" class="caption">
                                Loại sản phẩm:</td>
                            <td colspan="2" style="width: 200px;" align="left">
                                <asp:DropDownList ID="ddlLoaiSanPham" runat="server" Width="116px">
                                    <asp:ListItem Value="1">Chứng nhận</asp:ListItem>
                                    <asp:ListItem Value="2">C&#244;ng bố</asp:ListItem>
                                </asp:DropDownList></td>
                            
                        </tr>
                        <tr>
                            <td align="left" class="caption" style="width: 150px; align: left">
                                <asp:Label ID="lblLoaiTieuChuan" runat="server" Text="Loại hình, tiêu chuẩn CN"></asp:Label></td>
                            <td align="left" colspan="3">
                                <asp:DropDownList ID="ddlLoaiTieuChuanApDung" runat="server" Width="546px">
                                    <asp:ListItem Value="A">Ti&#234;u chuẩn Ng&#224;nh hoặc ti&#234;u chuẩn quốc tế bắt buộc &#225;p dụng</asp:ListItem>
                                    <asp:ListItem Value="B">Ti&#234;u chuẩn quốc tế kh&#244;ng bắt buộc &#225;p dụng</asp:ListItem>
                                    <asp:ListItem Value="C">Ti&#234;u chuẩn do tổ chức, c&#225; nh&#226;n đăng k&#253;</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" class="caption" style="width: 150px;">
                            </td>
                            <td align="left" colspan="2" style="width: 200px;">
                                <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" OnClick="btnCapNhat_Click"
                                    Width="79px" /><asp:Button ID="btnCancel" runat="server" Text="Bỏ qua" Width="78px" OnClientClick="javascript:window.close();"
                                    CausesValidation="False" /></td>
                            <td align="left" rowspan="1" style="width: 152px; height: 26px" valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="caption" style="width: 150px">
                            </td>
                            <td align="left" colspan="2" style="width: 200px">
                            </td>
                            <td align="left" rowspan="1" style="width: 152px" valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="caption" colspan="4">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">                              
                              
                                <tr>
                                    <td>
                                        <div align="center">
                                            <fieldset style="width: 97%">
                                                <legend>Danh sách tiêu chuẩn</legend>
                                                <table border="0" width="100%">
                                                    <tr>
                                                        <td align="left">
                                                            Mã tiêu chuẩn</td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtMaTC" runat="server" Width="365px" MaxLength="50"></asp:TextBox></td>
                                                        <td >
                                                            </td>
                                                    </tr>
                                                        
                                                    <tr>
                                                          <td align="left">
                                                              Tên tiêu chuẩn</td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtTenTieuChuan" runat="server" Width="365px" MaxLength="255"></asp:TextBox>
                                                            <asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" OnClick="btnTimKiem_Click" CausesValidation="false" /></td>
                                                        <td align="right"  style="text-align: right">
                                                            <asp:ImageButton ID="imgThemMoi" runat="server" Height="16px" ImageUrl="~/htmls/image/new_f2.png"
                                                                Width="16px" OnClientClick="popCenter('DM_TieuChuanApDung_ChiTiet.aspx?direct=sanphamchitiet','DM_TieuChuanApDung_ChiTiet',800,250); return false;" />&nbsp;<asp:LinkButton
                                                                    ID="lnkThemMoi" runat="server" OnClientClick="popCenter('DM_TieuChuanApDung_ChiTiet.aspx?direct=sanphamchitiet','DM_TieuChuanApDung_ChiTiet',800,250); return false;">Thêm mới</asp:LinkButton></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" valign="top">
                                                            <cc2:PagingGridView ID="gvTieuChuan" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                DataKeyNames="ID,Matieuchuan" EmptyDataText="Không có dữ liệu !" OnDataBound="gvTieuChuan_DataBound"
                                                                OnPageIndexChanging="gvTieuChuan_PageIndexChanging" PageSize="10" VirtualItemCount="-1"
                                                                AllowPaging="true" Width="100%" AllowMultiColumnSorting="true" OnSorting="gvTieuChuan_Sorting" OnRowDataBound="gvTieuChuan_RowDataBound">
                                                                <Columns>
                                                                <asp:TemplateField>
                                                                        <%--<headertemplate>
                                                                            <asp:CheckBox ID="chkCheckAll" runat="server" />                                                
                                                                        </headertemplate>--%>
                                                                        <itemstyle horizontalalign="Justify" />
                                                                        <headerstyle cssclass="unsortable" width="1px" />
                                                                        <itemtemplate>
                                                                        <asp:HiddenField runat="server" ID="hidden"></asp:HiddenField>                                                                            
                                                                            <asp:CheckBox ID="chkCheck"  Font-Size="0" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox_CheckedChanged" Text='<%# Eval("ID") %>' />
                                                                            
                                                                        </itemtemplate>
                                                                    </asp:TemplateField>
                                                                    <%--<asp:TemplateField HeaderText="T&#234;n Ti&#234;u Chuẩn" SortExpression="TenTieuChuan">
                                                                        <controlstyle width="40%" />
                                                                        <itemtemplate>
                                                                            <a href="#" onclick="return popCenter('DM_TieuChuanApDung_ChiTiet.aspx?Ma=<%# Server.UrlEncode(Eval("ID").ToString()) %>','DM_TieuChuanApDung_ChiTiet',800,250);"><%# Eval("TenTieuChuan").ToString()%></a>
                                                                        
                                                                        </itemtemplate>
                                                                        
                                                                    </asp:TemplateField>--%>
                                                                    <asp:BoundField DataField="TenTieuChuan" HeaderText="Tên tiêu chuẩn" SortExpression="TenTieuChuan">
                                                                        <controlstyle width="350px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="MaTieuChuan" HeaderText="M&#227; TC" SortExpression="MaTieuChuan">
                                                                        <controlstyle width="150px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="GhiChu" HeaderText="Ghi ch&#250;" SortExpression="GhiChu">
                                                                        <controlstyle width="300px" />
                                                                    </asp:BoundField>
                                                                    
                                                                </Columns>
                                                                <FooterStyle CssClass="sortbottom" />
                                                                <PagerStyle CssClass="sortbottom" />
                                                                <PagerSettings Mode="NumericFirstLast" />
                                                            </cc2:PagingGridView>
                                                                       
                                                        </td>
                                                    </tr>
                                                </table>                                                 
                                            </fieldset>
                                        </div>
                                    </td>
                                </tr>
                               
                            </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 165px;" align="left" class="caption">
                                &nbsp;</td>
                            <td style="width: 200px;" align="left">
                                &nbsp;</td>
                            <td style="width: 150px" align="left" class="caption">
                                &nbsp;</td>
                            <td style="width: 152px" align="left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" class="caption" colspan="4">
                              
                            </td>
                        </tr>
                    </table>                                
            </fieldset>
        </div>
    </form>
</body>
</html>
