<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true" CodeFile="DM_TrungTam.aspx.cs" Inherits="WebUI_DM_TrungTam" Title="Danh mục trung tâm" Theme="Default"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="margin: 10px auto 10px 10px;">
        <strong>DANH MUC >> TRUNG TÂM</strong>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">

        <tr>
            <td >
                <div align="center">
                    <fieldset style="width: 97%">
                        <legend>Danh mục</legend>
                        <table border="0" width="100%">
                             <tr>
                                <td style="height: 28px; text-align:right; padding:0px 20px 0px 0px" align="right" colspan="4">
                                
                                  
                                    <asp:ImageButton ID="imgThemMoi" runat="server" Height="16px" ImageUrl="~/htmls/image/new_f2.png"
                                        Width="16px" OnClientClick="popCenter('DM_TrungTam_ChiTiet.aspx?action=add','DM_TrungTam_ChiTiet', 950,450); return false;" />&nbsp;<asp:LinkButton
                                            ID="lnkThemMoi" runat="server" OnClientClick="popCenter('DM_TrungTam_ChiTiet.aspx?action=add','DanhMucSanPham', 950,450); return false;">Thêm mới</asp:LinkButton>
                                   
                                   
                                    &nbsp;<asp:ImageButton ID="imgXoa" runat="server" Height="16px" ImageUrl="~/htmls/image/cancel_f2.png"
                                        Width="16px" OnClick="imgXoa_Click" OnClientClick="if(GridIsChecked('gvTrungTam')) { return confirm('Bạn có chắc chắn muốn trung tâm đã chọn không?');} else {alert('Bạn chưa chọn trung tâm cần xóa.'); return false;}" Enabled="False" />
                                    <asp:LinkButton ID="lnkXoa" runat="server" OnClick="lkbtnXoa_Click"
                                    OnClientClick="if(GridIsChecked('gvTrungTam')) { return confirm('Bạn có chắc chắn muốn trung tâm đã chọn không?');} else {alert('Bạn chưa chọn trung tâm cần xóa.'); return false;}" Enabled="False"
                                    >Xóa</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td colspan="4" valign="top">
                                    <asp:GridView ID="gvTrungTam" runat="server" AutoGenerateColumns="False" EmptyDataText="Không có dữ liệu !"
                                        Width="100%" OnDataBound="gvTrungTam_DataBound" DataKeyNames="Id,tentrungtam" >
                                        <Columns>                                                                                                                                    
                                            <asp:TemplateField HeaderText="T&#234;n trung t&#226;m">
                                                <ItemTemplate>
                                                <a href="#" onclick="return popCenter('DM_TrungTam_ChiTiet.aspx?Action=edit&TrungTamId=<%# Server.UrlEncode(Eval("ID").ToString()) %>','DM_TrungTam_ChiTiet',900,500);"><%# Eval("tentrungtam")%></a>
                                                <%--<asp:LinkButton ID="lnkSelect" runat="server" CausesValidation="false" CommandName="Select"                                                 
                                                Text='<%# Server.HtmlEncode(Eval("TenTrungTam").ToString()) %>' CommandArgument='<%# Eval("Id") %>' >
                                                </asp:LinkButton> --%>                                                   
                                                </ItemTemplate>
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ID" HeaderText="M&#227; TT" >
                                                <ItemStyle Width="10%" />
                                            </asp:BoundField>        
                                            <asp:BoundField DataField="diachi" HeaderText="Địa chỉ"  >
                                                <ItemStyle Width="25%" />
                                            </asp:BoundField>    					                        					            
            					            <asp:BoundField DataField="dienthoai" HeaderText="Điện thoại"  >
                                                <ItemStyle Width="15%" />
                                            </asp:BoundField>
            					            <asp:BoundField DataField="fax" HeaderText="Fax"  >
                                                <ItemStyle Width="15%" />
                                            </asp:BoundField>
            					            <asp:BoundField DataField="Email" HeaderText="Email"  >
                                                <ItemStyle Width="15%" />
                                            </asp:BoundField>
            					            <asp:TemplateField >                                              
                                                <HeaderTemplate>                                                 
                                                    <asp:CheckBox ID="chkCheckAll" runat="server" />
                                                </HeaderTemplate>                                                
                                                <ItemTemplate>                                                                                                
                                                    <asp:CheckBox ID="chkCheck" runat="server"  />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="50px" />
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

