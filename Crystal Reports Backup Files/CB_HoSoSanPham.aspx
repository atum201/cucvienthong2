<%@ Page AutoEventWireup="true" CodeFile="CB_HoSoSanPham.aspx.cs" Inherits="WebUI_CB_HoSoSanPham"
    Language="C#" MasterPageFile="~/MasterPage/Main.master" Theme="Default" Title="Quản lý hồ sơ" %>

<%@ Register Namespace="Fadrian.Web.Control" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="Server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript">
    function ShowAction(isDisplay)
    {       
        var tdaction=$('#action');                
        if (!isDisplay)
            tdaction.hide();
        else
            tdaction.show();        
    }        
    </script>

    <div style="margin-left: 10px;">
        <div style="margin: 10px auto 10px 10px;">
            <span style="font-family: Arial"><strong>CÔNG BỐ &gt;&gt;
                <asp:Label ID="lblPath" runat="server" Text=""></asp:Label>
                <%--<a href="../WebUI/CN_HoSo_QuanLy.aspx?UserControl=CB_HoSoDen"">      
            DANH SÁCH HỒ SƠ</a> --%>
                &gt;&gt;HỒ SƠ SẢN PHẨM</strong></span>
        </div>
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 96%;" valign="top">
                    <fieldset style="width: 98%">
                        <legend>Thông tin hồ sơ</legend>
                        <div style="text-align: left">
                            <table align="center" border="0" width="100%">
                                <tr>
                                    <td class="caption" style="width: 15%" valign="top">
                                        Đơn vị nộp HS:</td>
                                    <td colspan="5" style="height: 21px; text-align: left">
                                        <asp:Label ID="lblDonVi" runat="server" Font-Bold="True" Width="571px"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;" class="caption" valign="top">
                                        Số hồ sơ:</td>
                                    <td colspan="1" style="width: 17%; height: 21px; text-align: left">
                                        <asp:Label ID="lblSoHoSo" runat="server" Font-Bold="True"></asp:Label></td>
                                    <td colspan="1" style="width: 15%;" align="left" class="caption">
                                        Người nộp HS:</td>
                                    <td colspan="1" style="width: 20%; height: 21px; text-align: left">
                                        <asp:Label ID="lblNguoiNop" runat="server" Font-Bold="True"></asp:Label></td>
                                    <td colspan="1" style="width: 15%;" align="left" class="caption">
                                        Số công văn đơn vị:</td>
                                    <td style="width: 25%; height: 21px; text-align: left">
                                        <asp:Label ID="lblSoCongVanDonVi" runat="server" Font-Bold="True"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;" class="caption" valign="top">
                                        Người tiếp nhận:</td>
                                    <td colspan="1" style="width: 17%; height: 21px; text-align: left">
                                        <asp:Label ID="lblNguoiTiepNhan" runat="server" Font-Bold="True"></asp:Label></td>
                                    <td colspan="1" style="width: 15%;" align="left" class="caption">
                                        Điện thoại:</td>
                                    <td colspan="1" style="width: 20%; height: 21px; text-align: left">
                                        <asp:Label ID="lblDienThoai" runat="server" Font-Bold="True"></asp:Label></td>
                                    <td colspan="1" style="width: 15%;" align="left" class="caption">
                                        Trạng thái:</td>
                                    <td style="width: 25%; height: 21px; text-align: left">
                                        <asp:Label ID="lblTrangThai" runat="server" Font-Bold="True"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;" class="caption" valign="top">
                                        Ngày nhận:</td>
                                    <td colspan="1" style="width: 17%; height: 21px; text-align: left">
                                        <asp:Label ID="lblNgayNhan" runat="server" Font-Bold="True"></asp:Label></td>
                                    <td colspan="1" style="width: 15%;" align="left" class="caption">
                                        Email:</td>
                                    <td colspan="1" style="width: 20%; height: 21px; text-align: left">
                                        <asp:Label ID="lblEmail" runat="server" Font-Bold="True"></asp:Label></td>
                                    <td colspan="1" style="width: 15%;" align="left" class="caption">
                                    </td>
                                    <td style="width: 25%; height: 21px; text-align: left">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 96%;" valign="top">
                    <fieldset style="width: 98%">
                        <legend>Danh sách sản phẩm</legend>
                        <table style="width: 100%" id="action">
                            <tr id="traction" runat="server">
                                <td style="text-align: right" colspan="2">
                                    <a href="../WebUI/CB_TiepNhanHoSo_TaoMoi.aspx?direct=CB_HoSoSanPham&Action=add&HoSoId=<%=Request.QueryString["HoSoId"]%>&TrangThaiId=<%=Request.QueryString["TrangThaiId"]%>"
                                        style="text-decoration: none;">
                                        <img src="../htmls/image/new_f2.png" style="border: none; width: 16px; height: 16px" />&nbsp;Thêm
                                        mới</a>
                                    <%-- <asp:ImageButton ID="ImgBtnThemmoi" runat="server" Enabled="False" Height="17px" ImageUrl="~/htmls/image/new_f2.png"
                                    OnClientClick="~/WebUI/CB_TiepNhanHoSo_TaoMoi.aspx"
                                    Width="17px" />
                               <asp:LinkButton ID="lnkThemMoi" runat="server" Font-Bold="False" OnClick="lnkThemMoi_Click">Thêm mới</asp:LinkButton>--%>
                                    <asp:ImageButton ID="ImgBtnGui" runat="server" Height="14px" ImageUrl="~/htmls/image/send.JPG"
                                        OnClientClick="return confirm('Bạn có chắc chắn muốn gửi Hồ sơ này không?');"
                                        Width="23px" OnClick="ImgBtnGui_Click" />
                                    <asp:LinkButton ID="lnkGui" runat="server" Font-Bold="False" OnClick="lnkGui_Click"
                                        OnClientClick="return confirm('Bạn có chắc chắn muốn gửi Hồ sơ này không?');">
                                 Gửi lãnh đạo</asp:LinkButton>
                                    <asp:ImageButton ID="ImgBtnXoa" runat="server" Enabled="False" Height="16px" ImageUrl="~/htmls/image/cancel_f2.png"
                                        Width="16px" OnClick="ImgBtnXoa_Click" />
                                    <asp:LinkButton ID="lnkXoa" runat="server" Font-Bold="False" OnClick="lnkXoa_Click"
                                        Enabled="False">Xóa</asp:LinkButton>
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <cc1:PagingGridView ID="gvSanPham" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            EmptyDataText="Không có dữ liệu !" OnDataBound="gvSanPham_DataBound" PageSize="15"
                            AllowMultiColumnSorting="True" OnSorting="gvSanPham_Sorting" Width="100%" OnRowDataBound="gvSanPham_RowDataBound"
                            DataKeyNames="Id,TenSanPham" OnPageIndexChanging="gvSanPham_PageIndexChanging"
                            AllowSorting="True" VirtualItemCount="-1">
                            <Columns>
                                <asp:TemplateField HeaderText="T&#234;n sản phẩm">
                                    <itemtemplate>                                                                   
                                    </itemtemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="KyHieu" HeaderText="K&#253; hiệu" SortExpression="KyHieu" />
                                <asp:BoundField DataField="HinhThuc" HeaderText="H&#236;nh thức" />
                                <asp:BoundField DataField="TieuChuan" HeaderText="T.Chuẩn &#225;p dụng" />
                                <asp:BoundField DataField="GhiChu" HeaderText="Ghi ch&#250;" />
                                <asp:TemplateField HeaderText="Trạng thái sản phẩm">
                                    <itemtemplate>                                
                                </itemtemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <headertemplate>
                                    <asp:CheckBox ID="chkCheckAll" runat="server" />                                
                                    </headertemplate>
                                    <itemtemplate>
                                    <asp:CheckBox ID="chkCheck" runat="server"  />                                                                    
                                    </itemtemplate>
                                    <headerstyle horizontalalign="Center" width="50px" />
                                    <itemstyle horizontalalign="Center" width="50px" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" />
                        </cc1:PagingGridView>
                    </fieldset>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
