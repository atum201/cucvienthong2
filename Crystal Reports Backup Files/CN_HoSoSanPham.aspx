<%@ Page AutoEventWireup="true" CodeFile="CN_HoSoSanPham.aspx.cs" Inherits="WebUI_CN_HoSoSanPham"
    Language="C#" MasterPageFile="~/MasterPage/Main.master" Theme="Default" Title="Quản lý hồ sơ" %>

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

    <div style="margin: 10px auto 10px 10px">
        <span style="font-family: Arial; font-weight: bold;">CHỨNG NHẬN&gt;&gt;<%--<a
            href="../WebUI/CN_HoSo_QuanLy.aspx?UserControl=CN_HoSoDen"> DANH SÁCH HỒ SƠ MỚI</a>--%>
            <asp:Label ID="lblPath" runat="server" Text=""></asp:Label>&gt;&gt DANH SÁCH SẢN
            PHẨM</span>
    </div>
    <table cellpadding="5" cellspacing="0" width="100%" border="0">
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
                <fieldset style="width: 98%">
                    <legend>Thông tin hồ sơ</legend>
                    <div style="text-align: left">
                        <table align="center" border="0" width="100%" cellspacing="5">
                            <tr>
                                <td align="right" style="width: 120px; text-align: left" valign="top">
                                    Đơn vị nộp:</td>
                                <td colspan="5">
                                    <asp:Label ID="lblDonVi" runat="server" Width="529px" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <%--LongHH Start--%>
                            <tr>
                                <%--<td style="width: 10px; height: 21px;">
                                </td>--%>
                                <td style="height: 21px">
                                    Tên tiếng anh:</td>
                                <td style="height: 21px">
                                    <asp:Label ID="lblTenTiengAnh" runat="server" Font-Bold="True"></asp:Label></td>
                                <td style="height: 21px; width: 129px;">
                                    Địa chỉ:</td>
                                <td style="height: 21px">
                                    <strong>
                                        <asp:Label ID="lblDiaChi" runat="server" Font-Bold="True"></asp:Label></strong></td>
                                <td>
                                    Mã số thuế:</td>
                                <td>
                                    <strong>
                                        <asp:Label ID="lblMaSoThue" runat="server" Font-Bold="True"></asp:Label></strong></td>
                            </tr>
                            <%--LongHH End--%>
                            <tr>
                                <td align="right" style="text-align: left; width: 120px;" valign="top">
                                    Số hồ sơ:</td>
                                <td colspan="1" style="width: auto;">
                                    <strong>
                                        <asp:Label ID="lblSohoso" runat="server"></asp:Label></strong></td>
                                <td colspan="1" style="width: 75px;">
                                    Người nộp:</td>
                                <td colspan="1" style="width: auto;">
                                    <strong>
                                        <asp:Label ID="lblNguoiNop" runat="server"></asp:Label></strong></td>
                                <td colspan="1" style="width: 130px;">
                                    Loại hình chứng nhận:</td>
                                <td style="width: auto;">
                                    <asp:Label ID="lblLoaiHinhChungNhan" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" style="text-align: left; width: 120px;" valign="top">
                                    Người tiếp nhận</td>
                                <td colspan="1" style="width: auto;">
                                    <strong>
                                        <asp:Label ID="lblNguoiNhan" runat="server"></asp:Label></strong></td>
                                <td colspan="1" style="width: 75px;">
                                    Điện thoại:</td>
                                <td colspan="1" style="width: auto;">
                                    <strong>
                                        <asp:Label ID="lblDienthoai" runat="server"></asp:Label></strong></td>
                                <td colspan="1" style="width: 120px;">
                                    Hình thức tiếp nhận:</td>
                                <td style="width: auto;">
                                    <strong>
                                        <asp:Label ID="lblLoaiHinh" runat="server"></asp:Label></strong></td>
                            </tr>
                            <tr>
                                <td align="right" style="text-align: left; width: 120px;" valign="top">
                                    Ngày tiếp nhận:</td>
                                <td colspan="1" style="width: auto;">
                                    <strong>
                                        <asp:Label ID="lblNgayNhan" runat="server"></asp:Label></strong></td>
                                <td colspan="1" style="width: 75px;">
                                    Email:</td>
                                <td colspan="1" style="width: auto;">
                                    <strong>
                                        <asp:Label ID="lblEmail" runat="server"></asp:Label></strong></td>
                                <td colspan="1" style="width: 88px;">
                                    Trạng thái:</td>
                                <td style="width: auto;">
                                    <strong>
                                        <asp:Label ID="lblTrangThai" runat="server"></asp:Label></strong></td>
                            </tr>
                            <tr>
                                <td align="right" style="text-align: left; width: 120px;" valign="top">
                                    &nbsp;Số công văn đến:</td>
                                <td colspan="1" style="width: auto;">
                                    <strong>
                                        <asp:Label ID="lblSoCongVanDen" runat="server"></asp:Label></strong></td>
                                <td colspan="1" style="width: 75px;">
                                </td>
                                <td colspan="1" style="width: auto;">
                                </td>
                                <td colspan="1" style="width: 88px;">
                                    Nguồn gốc:</td>
                                <td style="width: auto;">
                                    <strong>
                                        <asp:Label ID="lblNguonGoc" runat="server"></asp:Label></strong></td>
                            </tr>
                        </table>
                    </div>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 96%;" valign="top">
                <fieldset style="width: 98%">
                    <legend>Danh sách sản phẩm</legend>
                    <table style="width: 100%;" id="action">
                        <tr>
                            <td style="text-align: right; height: 33px;">
                                <asp:ImageButton ID="ImgBtnThemmoi" runat="server" Height="17px" ImageUrl="~/htmls/image/new_f2.png"
                                    Width="17px" />
                                <asp:LinkButton ID="lnkThemMoi" runat="server" Font-Bold="False">Thêm mới</asp:LinkButton>
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
                    <asp:GridView ID="gvSanPham" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        EmptyDataText="Không có dữ liệu !" OnDataBound="gvSanPham_DataBound" PageSize="15"
                        Width="100%" OnRowDataBound="gvSanPham_RowDataBound" OnPageIndexChanging="gvSanPham_PageIndexChanging"
                        DataKeyNames="Id,TenSanPham">
                        <Columns>
                            <asp:TemplateField HeaderText="T&#234;n sản phẩm">
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="KyHieu" HeaderText="K&#253; hiệu" />
                            <asp:BoundField DataField="MaNhom" HeaderText="M&#227; nh&#243;m SP" />
                            <asp:BoundField DataField="TieuChuan" HeaderText="T.Chuẩn &#225;p dụng" />
                            <asp:BoundField DataField="NoiDungXuLy" HeaderText="Nội dung xử l&#253;" />
                            <asp:BoundField DataField="GhiChu" HeaderText="Ghi ch&#250;" />
                            <asp:TemplateField HeaderText="Trạng thái sản phẩm">
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkCheckAll" runat="server" />
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkCheck" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </fieldset>
            </td>
        </tr>
        <tr runat="server" id="trThongBaoNopTien">
            <td align="left" style="width: 96%;" valign="top">
                <fieldset style="width: 98%">
                    <legend>Thông báo nộp tiền lấy mẫu sản phẩm và đánh giá quy trình sản xuất</legend>
                    <table style="width: 100%;" id="Table1">
                        <tr runat="server" id="trChucNang">
                            <td style="text-align: right; height: 33px;">
                                <asp:ImageButton ID="IBThemMoi" runat="server" Height="17px" ImageUrl="~/htmls/image/new_f2.png"
                                    Width="17px" OnClick="IBThemMoi_Click" />
                                <asp:LinkButton ID="LBThemMoi" runat="server" Font-Bold="False" OnClick="LBThemMoi_Click">Thêm mới</asp:LinkButton>
                                <asp:ImageButton ID="IBGui" runat="server" Height="14px" ImageUrl="~/htmls/image/send.JPG"
                                    OnClientClick="return confirm('Bạn có chắc chắn muốn gửi thông báo này không?');"
                                    Width="23px" OnClick="IBGui_Click" />
                                <asp:LinkButton ID="LBGui" runat="server" Font-Bold="False" OnClick="LBGui_Click"
                                    OnClientClick="return confirm('Bạn có chắc chắn muốn gửi thông báo này không?');">
                                 Gửi lãnh đạo</asp:LinkButton>
                                <asp:ImageButton ID="IBXoa" runat="server" Enabled="False" Height="16px" ImageUrl="~/htmls/image/cancel_f2.png"
                                    Width="16px" OnClick="IBXoa_Click" />
                                <asp:LinkButton ID="LBXoa" runat="server" Font-Bold="False" Enabled="False" OnClick="LBXoa_Click">Xóa</asp:LinkButton>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gvPhi" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        EmptyDataText="Không có dữ liệu !" PageSize="15" Width="100%" OnPageIndexChanging="gvPhi_PageIndexChanging"
                        DataKeyNames="ID,TrangThaiID,SoGiayThongBaoLePhi" OnRowDataBound="gvPhi_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Số th&#244;ng b&#225;o lệ ph&#237;">
                                <ItemStyle Font-Underline="False" />
                                <ItemTemplate>
                                    <a href="#" runat="server" id="lnkThongBaoLePhi">
                                        <%# Eval("SoGiayThongBaoLePhi")%>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TenTiengViet" HeaderText="Đơn vị nộp hồ sơ, c&#225; nh&#226;n" />
                            <asp:BoundField DataField="DiaChi" HeaderText="Địa chỉ" />
                            <asp:BoundField DataField="DienThoai" HeaderText="Điện thoại" />
                            <asp:BoundField DataField="Fax" HeaderText="Fax" />
                            <asp:BoundField DataField="TongPhi" HeaderText="Tổng ph&#237; (VNĐ)" DataFormatString="{0:#,#}.000" />
                            <asp:BoundField DataField="MoTa" HeaderText="Trạng th&#225;i" />
                            <asp:TemplateField HeaderText="In th&#244;ng b&#225;o">
                                <ItemTemplate>
                                    <a onclick="javascript:popCenter('../ReportForm/HienBaoCao.aspx?ThongBaoLePhiId=<%#DataBinder.Eval(Container.DataItem,"ID")%>&LoaiBaoCao=ThongBaoNopTien','_blank',790,590);"
                                        href="#">
                                        <img src="../Images/printer.png" style="border: none;" alt="In biên bản" /></a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkCheckAll" runat="server" onclick="HeaderClick('gvPhi')" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkCheck" runat="server" onclick="ChildClick('gvPhi')" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
