<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="QuanLyMauDau.aspx.cs" Inherits="WebUI_QuanLyMauDau" Title="Quản lý mẫu dấu hợp quy" %>

<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="../Js/jquery-1.3.2-vsdoc2.js"></script>

    <script type="text/javascript">
        $(function (){            
            $(".choseImage").click(
              function(){                                     
                    $(".choseImage").removeClass("hover_item");                    
                    $(this).addClass("hover_item");  
                    $('#<%=hdMauDauId.ClientID%>').val($(this).attr("src"));             
              }
            )
        })        
        function SelectItem(){
            var url_image;
            var imgCtrl=$(".choseImage.hover_item");
            url_image = imgCtrl.attr("src");                
        }  
        
        function checkSelected()
        {
            var id = $('#<%=hdMauDauId.ClientID%>').val();
            if(id.length < 1)
            {
                alert('Bạn chưa chọn mẫu dấu cần xoá');
                return false;
            }
            return true;
        }      
    </script>

    <div align="left">
        <strong>DANH SÁCH MẪU DẤU HỢP QUY<br />
        </strong>
        <br />
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="height: auto">
                    <div align="center">
                        <fieldset style="width: 97%">
                            <legend>Mẫu dấu theo đơn vị</legend>
                            <table align="center" border="0" width="100%">
                                <tr>
                                    <td align="right" style="height: 26px; width: 73px; text-align: left;">
                                        Tên đơn vị</td>
                                    <td align="left" style="height: 26px;" colspan="2">
                                        <cc1:ComboBox ID="ddlDonVi" runat="server" Width="580px" AutoPostBack="true" OnSelectedIndexChanged="ddlDonVi_SelectedIndexChanged">
                                        </cc1:ComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDonVi"
                                            ErrorMessage="Bạn chưa chọn đơn vị cần thêm mấu dấu" ToolTip="Bạn chưa chọn đơn vị cần thêm mấu dấu">*</asp:RequiredFieldValidator>
                                    </td>
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
                            <legend>Danh sách mẫu dấu hợp quy</legend>
                            <table border="0" width="100%">
                                <tr>
                                    <td align="right" colspan="4" style="text-align: right" id="tdAction" runat="server">
                                        <asp:ImageButton ID="imgThemMoi" runat="server" Height="16px" ImageUrl="~/htmls/image/new_f2.png"
                                            Width="16px" OnClick="imgThemMoi_Click" />&nbsp;
                                        <asp:LinkButton ID="llbThemMoi" runat="server" OnClick="llbThemMoi_Click">Thêm mới</asp:LinkButton>
                                        &nbsp;
                                        <asp:LinkButton ID="lnkXoa" runat="server" OnClick="lnkXoa_Click" OnClientClick="if(checkSelected()) return confirm('Bạn có chắc chắn muốn xóa các mẫu dấu đã chọn không?');"
                                            CausesValidation="False"><img src="../htmls/image/cancel_f2.png" border="0" alt="" width="16px" />&nbsp;Xóa</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" valign="top">
                                        <asp:Label ID="lblThongbao" runat="server" Text="Lựa chọn đơn vị cần xem mẫu dấu"
                                            Font-Bold="False" Font-Size="14pt" ForeColor="Red"></asp:Label>
                                        <asp:DataList ID="dtlstMauDau" runat="server" RepeatColumns="5" SelectedItemStyle-BackColor="controlDark"
                                            ItemStyle-HorizontalAlign="Center" CellPadding="5" CellSpacing="2" Width="100%">
                                            <ItemTemplate>
                                                <div>
                                                    <img class="choseImage" src="../Handler.ashx?MauDauId=<%# Eval("Id")%>" style="width: 100px;
                                                        height: 100px;" /><br />
                                                    <a href="#" title="Sửa thông tin mẫu dấu" onclick="return popCenter('MauDauHopQuy_ChiTiet.aspx?id=<%# Eval("Id")%>&&openner=ql','Dm_MauDau',600,300);">
                                                        <%# Eval("TenMauDau")%>
                                                    </a>
                                                </div>
                                            </ItemTemplate>
                                            <SelectedItemStyle BackColor="InactiveBorder" />
                                            <ItemStyle Height="90px" HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                                        </asp:DataList>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" />
        <asp:HiddenField ID="hdMauDauId" runat="server" />
</asp:Content>
