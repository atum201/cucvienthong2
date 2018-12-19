<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true" CodeFile="DM_Lephi.aspx.cs" Inherits="WebUI_DM_Lephi" Title="Danh mục lệ phí" Theme="Default"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div  style="margin:10px auto 10px 10px;">
        <strong>DANH MUC >> LỆ PHÍ</strong>
    </div>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td >
                <div align="center">
                    <fieldset style="width: 97%">
                        <legend>Danh mục lệ phí</legend>
                        <table border="0" width="100%">
                             <tr>
                                <td align="right" colspan="4" style="text-align:right;">
                                    &nbsp;<asp:ImageButton ID="ImgBtnThemMoi" runat="server" Height="16px" ImageUrl="~/htmls/image/new_f2.png"
                                        Width="16px" OnClientClick="popCenter('DM_LePhi_ChiTiet.aspx','DM_LePhi_ChiTiet',720,250); return false;" />&nbsp;<asp:LinkButton
                                            ID="LinkBtnThemMoi" runat="server" OnClientClick="popCenter('DM_LePhi_ChiTiet.aspx','DM_LePhi_ChiTiet',720,250); return false;">Thêm mới</asp:LinkButton>
                                    &nbsp;<asp:ImageButton ID="ImgBtnXoa" runat="server" Height="16px" ImageUrl="~/htmls/image/cancel_f2.png"
                                        Width="16px" OnClick="ImgBtnXoa_Click" OnClientClick="if(GridIsChecked('gvLePhi')) { return confirm('Bạn có chắc chắn muốn xóa các danh mục lệ phí đã chọn không?');} else {alert('Bạn chưa chọn danh mục lệ phí cần xóa.'); return false;}" />
                                    <asp:LinkButton ID="btnXoa" runat="server" OnClick="btnXoa_Click" OnClientClick="if(GridIsChecked('gvLePhi')) { return confirm('Bạn có chắc chắn muốn xóa các danh mục lệ phí đã chọn không?');} else {alert('Bạn chưa chọn danh mục lệ phí cần xóa.'); return false;}">Xóa</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td colspan="4" valign="top">
                                    <asp:GridView ID="gvLePhi" runat="server" AutoGenerateColumns="False" EmptyDataText="Không có dữ liệu !"
                                        Width="100%" OnDataBound="gvLePhi_DataBound" DataKeyNames="ID,GiaTriLoHang" >
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="M&#227; Danh mục lệ ph&#237;" Visible="False" />
                                            <asp:TemplateField HeaderText="Gi&#225; trị l&#244; h&#224;ng">
                                                <ItemTemplate>                                            
                                                    <a href="#" onclick="return popCenter('DM_LePhi_ChiTiet.aspx?id=<%# Server.UrlEncode(Eval("ID").ToString()) %>','DM_LePhi_ChiTiet',800,250);"><%# Eval("GiaTriLoHang")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="LePhi" HeaderText="Mức lệ ph&#237; (VNĐ)" DataFormatString="{0:#,#}.000" HtmlEncode="False" />
                                            <asp:TemplateField>
                                                <HeaderStyle HorizontalAlign="Center" Width="10px" />
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkCheckAll" runat="server" />
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCheck" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                      
                    </fieldset>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

