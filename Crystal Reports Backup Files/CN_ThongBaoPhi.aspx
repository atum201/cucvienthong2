<%@ Page AutoEventWireup="true" MasterPageFile="~/MasterPage/Main.master" CodeFile="CN_ThongBaoPhi.aspx.cs"
    Inherits="WebUI_CN_ThongBaoPhi" Language="C#" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin: 10px auto 10px 10px;">
        <strong>THU PHÍ >> DANH SÁCH THÔNG BÁO LỆ PHÍ</strong>
    </div>
    <table style="width: 100%">
        <tr>
            <td style="height: 21px">
            </td>
        </tr>
        <tr>
            <td style="width: 100%; text-align: left">
                <fieldset style="width: 98%">
                    <legend>Danh sách thông báo lệ phí &nbsp;</legend>
                    <div style="width: 100%; text-align: right;">
                        Lọc theo loại phí
                        <asp:DropDownList ID="ddlLoaiPhi" runat="server" OnSelectedIndexChanged="ddlLoaiPhi_SelectedIndexChanged"
                            AutoPostBack="True" Width="362px">
                        </asp:DropDownList>
                    </div>
                    <asp:GridView ID="gvPhi" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        DataKeyNames="ID,TrangThaiID" EmptyDataText="Không có dữ liệu !" OnDataBound="gvPhi_DataBound"
                        OnPageIndexChanging="gvPhi_PageIndexChanging" PageSize="15" Width="100%" OnRowDataBound="gvPhi_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Số th&#244;ng b&#225;o lệ ph&#237;">
                                <ItemStyle Font-Underline="False" />
                                <ItemTemplate>
                                    <a href="#" onclick="return popCenter('TestBaoCao.aspx?ThongBaoLePhiID=<%# Server.UrlEncode(Eval("ID").ToString()) %>&trangthaiid=<%# Server.UrlEncode(Eval("TrangThaiID").ToString()) %>','CN_ThuPhi',800,600);">
                                        <%# Eval("SoGiayThongBaoLePhi")%>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TenTiengViet" HeaderText="Đơn vị nộp hồ sơ, c&#225; nh&#226;n" />
                            <asp:BoundField DataField="DiaChi" HeaderText="Địa chỉ" />
                            <asp:BoundField DataField="DienThoai" HeaderText="Điện thoại" />
                            <asp:BoundField DataField="Fax" HeaderText="Fax" />
                            <asp:BoundField DataField="TongPhi" HeaderText="Tổng ph&#237; (VNĐ)" DataFormatString="{0:#,#}.000"
                                HtmlEncode="False" />
                            <%--LongHH--%>
                            <%--<asp:BoundField DataField="MoTa" HeaderText="Trạng th&#225;i" />--%>
                            <asp:TemplateField HeaderText="Trạng th&#225;i">
                                <ItemTemplate></ItemTemplate>
                            </asp:TemplateField>
                            <%--LongHH--%>
                            <asp:TemplateField>                                
                                <ItemTemplate>
                                   <a id="lnkIn" href="#" onclick="return loadBaoCao('<%# Eval("Id") %>');">
                                        <img runat="server" id="lnkInLePhi" title="In Bằng, CCCM" style="border: none" src="../images/printer.png"
                                            alt="" /></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </fieldset>
            </td>
        </tr>
    </table>
    
    <script language="javascript" type="text/javascript">    
        function loadBaoCao(gID) {
            popCenter("../ReportForm/HienBaoCao.aspx?LoaiBaoCao=InBienLai&LePhiID=" + gID.toString(), "rptInBienLai", 1024, 600); return false;
        }
    </script>
    
</asp:Content>
