<%@ Control AutoEventWireup="true" CodeFile="uc_CB_HoSoDaGui.ascx.cs" Inherits="UserControls_uc_CB_HoSoDaGui"
    Language="C#" %>
<span style="font-family: Arial"><strong>CÔNG BỐ >>
    DANH SÁCH HỒ SƠ ĐÃ GỬI</strong></span><br />
<table style="width: 100%">
    <tr>
        <td style="width: 100%; height: 21px; text-align: right">
            
        </td>
    </tr>
    <tr>
        <td style="width: 95%; height: 268px; text-align: left; vertical-align: top;">
            <fieldset style="width: 97%">
                <legend>Danh sách hồ sơ đã gửi</legend>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblTrangThaiHoSo" runat="server" Text="Lọc theo trạng thái hồ sơ" />
                            <asp:DropDownList ID="ddlFilterTrangThai" runat="server" OnSelectedIndexChanged="ddlFilterTrangThai_SelectedIndexChanged" AutoPostBack="true" />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvHoSo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    EmptyDataText="Không có dữ liệu !" PageSize="30" Width="100%" DataKeyNames="ID,TrangThaiID" OnRowDataBound="gvHoSo_RowDataBound" OnPageIndexChanging="gvHoSo_PageIndexChanging" >
                    <Columns>
                        <asp:TemplateField HeaderText="Số hồ sơ">
                            <ItemStyle Font-Underline="False" Width="120px" />
                            <ItemTemplate>
                                <%# Eval("SoHoSo") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TenTiengViet" HeaderText="Đơn vị nộp HS" ItemStyle-VerticalAlign="Top" />
                        <asp:BoundField DataField="DiaChi" HeaderText="Địa chỉ" ItemStyle-VerticalAlign="Top" Visible="false" />
                        <asp:BoundField DataField="DienThoai" HeaderText="Điện thoại" ItemStyle-Width="80px" ItemStyle-VerticalAlign="Top"/>
                        <asp:TemplateField HeaderText="Ngày nhận HS">
                            <ItemStyle Width="80px" VerticalAlign="Top"/>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "NgayTiepNhan") == DBNull.Value? "" : ((DateTime)DataBinder.Eval(Container.DataItem, "NgayTiepNhan")).ToShortDateString()%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trạng thái" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <asp:Label ID="lblTrangThai" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TrangThaiID") == DBNull.Value? "" : Cuc_QLCL.Entities.EntityHelper.GetEnumTextValue((Cuc_QLCL.Entities.EnTrangThaiHoSoList)Convert.ToInt32(DataBinder.Eval(Container.DataItem, "TrangThaiID"))) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NguoiXuLy" HeaderText="Người xử lý" ItemStyle-VerticalAlign="Top" ItemStyle-Width="100px" />
                        <asp:TemplateField HeaderText="Chức năng">
                            <ItemStyle Width="105px" VerticalAlign="Top" />
                            <ItemTemplate>
                                <asp:Panel ID="Panel1" runat="server" Visible="false" ScrollBars="None" SkinID="FU">
                                    <asp:HyperLink ID="HyperLink1" runat="server" />
                                </asp:Panel>
                                <asp:Panel ID="Panel2" runat="server" Visible="false" ScrollBars="None" SkinID="FU">
                                    <asp:HyperLink ID="HyperLink2" runat="server" />
                                </asp:Panel>
                                <asp:Panel ID="Panel3" runat="server" Visible="false" ScrollBars="None" SkinID="FU">
                                    <asp:HyperLink ID="HyperLink3" runat="server" />
                                </asp:Panel>
                                <asp:Panel ID="Panel4" runat="server" Visible="false" ScrollBars="None" SkinID="FU">
                                    <asp:HyperLink ID="HyperLink4" runat="server" />
                                </asp:Panel>
                                <asp:Panel ID="Panel5" runat="server" Visible="false" ScrollBars="None" SkinID="FU">
                                    <asp:HyperLink ID="HyperLink5" runat="server" />
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:BoundField Visible="false" DataField="CheckDaDoc" />
                    </Columns>
                </asp:GridView>
            </fieldset>
        </td>
    </tr>
</table>
