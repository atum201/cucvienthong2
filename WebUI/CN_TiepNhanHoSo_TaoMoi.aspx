<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="CN_TiepNhanHoSo_TaoMoi.aspx.cs" Inherits="WebUI_CN_TiepNhanHoSo_TaoMoi"
    Theme="Default" %>

<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>
       function ClearText(name)
       {
         var strID = "ctl00$ContentPlaceHolder1$" + name;        
         var who=document.getElementsByName(strID)[0];;         
         who.value="";
         var who2= who.cloneNode(false);
         who2.onchange= who.onchange;
         who.parentNode.replaceChild(who2,who);         
       }
       function  Check(name1, name2)
       {
         var strID1 = "ctl00_ContentPlaceHolder1_" + name1; 
         var strID2 = "ctl00_ContentPlaceHolder1_" +  name2;        
         var ob1=document.getElementById(strID1);
         var ob2= document.getElementById(strID2); 
         //alert(ob2);                 
         if(ob1.value == '' && ob2 == null )
         {
            //alert(1);
            return false;          
         }
         
         return confirm('Bạn có muốn xóa file đính kèm này?');
       }
      
      
    </script>

    <%--LongHH--%>
    <style type="text/css">
        .dhx_combo_box{
            width:100% !important;
        }
        .mr10{
            margin-right:10px;
        }
        .dhx_combo_list{
            height:550px;
        }
    </style>
    <script type="text/javascript">
        //LongHH
        function reheight(p, tc) {
            var l = tc.find("tr").length;
            if (l > 10) {
                tc.css("height", 300);
                p.css("height", 319);
            } else if (l === 0) {
                tc.css("height", 30);
                p.css("height", 30 + 19);
            }
            else {
                tc.css("height", l * 30);
                p.css("height", l * 30 + 19);
            }
        }
        //LongHH
        $(document).ready(function () {
            reheight($("#<%=Panel1.ClientID %>"), $("#<%=chklstTieuChuan.ClientID %>"));
        })
    </script>

    <div style="margin: 10px 10px 10px 20px">
        <span style="font-family: Arial"><strong>CHỨNG NHẬN &gt;&gt;
            <asp:Label ID="lblDsSanPham" runat="server" Text=""></asp:Label>
            SẢN PHẨM CHI TIẾT
            </strong></span>
        <table id="Table2" style="width: 100%; color: #000000;">
            <tr>
                <td align="right" style="text-align: left;" width="15%">
                    Số hồ sơ</td>
                <td style="width: 40%;">
                    <asp:TextBox ID="txtSoHoSo" runat="server" BackColor="#FFFFC0" ReadOnly="True" Width="80%"></asp:TextBox></td>
                <td align="left" width="150">
                    Tiêu chuẩn áp dụng</td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; height: 27px;" width="15%">
                    Tên sản phẩm <span>(*)</span></td>
                <td style="width: 40%; height: 27px;">
                    <div style="float: left;width:80.2%;">
                    <cc1:ComboBox ID="ddlTenSanPham" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTenSanPham_SelectedIndexChanged" 
                        Width="100%">
                    </cc1:ComboBox>
                    </div>
                    <asp:RequiredFieldValidator ID="rfqTenSanPham" runat="server" ErrorMessage="Chọn tên sản phẩm"
                        ControlToValidate="ddlTenSanPham" InitialValue="0" Width="4px">*</asp:RequiredFieldValidator>
                    <a id="lnkThemMoiSanPham" runat="server" href="#">Thêm mới</a>
                </td>
                <td valign="top" style="width: 30%" rowspan="12">
                    <asp:Panel ID="Panel1" runat="server" Height="120px" ScrollBars="Both" Width="400px"
                        BorderWidth="1px">
                        <asp:CheckBoxList ID="chklstTieuChuan" runat="server" Width="100%">
                        </asp:CheckBoxList></asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right" style="text-align: left;" width="15%">
                    Ký hiệu sản phẩm</td>
                <td style="width: 40%">
                    <asp:TextBox ID="txtKyHieuSanPham" runat="server" Width="80%" MaxLength="255" CssClass="textKeyPad"></asp:TextBox>
                </td>
            </tr>
            <tr style="color: #000000">
                <td align="right" style="text-align: left; height: 28px;" width="15%">
                    Nhóm sản phẩm</td>
                <%--LongHH 300->400=>500--%>
                <td style="width: 40%; height: 28px">
                    <asp:TextBox ID="txtNhomSanPham" runat="server" BackColor="#FFFFC0" ReadOnly="True"
                        Width="80%"></asp:TextBox></td>
                <td style="height: 28px" width="150">
                </td>
            </tr>
            <tr style="color: #000000">
                <td align="right" style="text-align: left;" width="15%">
                    Hãng sản xuất <span>(*)</span></td>
                <%--LongHH 300->400=>500--%>
                <td style="width: 40%">
                    <div style="float: left; width:80%;">
                        <cc1:ComboBox ID="ddlHangSanXuat" runat="server" Width="100%">
                        </cc1:ComboBox>
                    </div>
                    <asp:RequiredFieldValidator ID="rfqHangSanXuat" runat="server" ErrorMessage="Chọn hãng sản xuất"
                            ControlToValidate="ddlHangSanXuat" InitialValue="0">*</asp:RequiredFieldValidator>
                    <a id="lnkThemMoiHangSX" runat="server" href="#" style="margin-top:5px;height:27px">
                            Thêm mới</a>
                </td>
                <td width="150">
                </td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; height: 29px;" width="15%">
                    Giấy tờ về tư cách pháp nhân</td>
                <td style="height: 29px;width:40%;">
                    <%--LongHH 300-<400--%>
                    <asp:FileUpload ID="FileUploadGiayToTuCachPhapNhan" runat="server" Width="400px"
                        CssClass="file_upload" />&nbsp;
                    <%--<input type="button" id="btnrefreshTuCachPhapNha" style="height:22px; width:35px;" value="Xóa" onclick="ClearText('FileUploadGiayToTuCachPhapNhan');return false"  />--%>
                    <asp:Label ID="lblTuCachPhapNhan" runat="server" Text="" Visible="false"></asp:Label>
                    |
                    <asp:LinkButton ID="lnkXoaTuCachPhapNhan" runat="server" Height="20px" 
                        CausesValidation="false"  OnClientClick="return Check('FileUploadGiayToTuCachPhapNhan','lblTuCachPhapNhan');"
                        Width="20px" OnClick="lnkXoaTuCachPhapNhan_Click" >Xóa</asp:LinkButton>
                      
                </td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; height: 27px;" width="15%">
                    Tài liệu kỹ thuật</td>
                <td style="height: 27px;width:40%;">
                    <%--LongHH 300-<400--%>
                    <asp:FileUpload ID="FileUploadTaiLieuKyThuat" runat="server" Width="400px" />&nbsp;
                    <%--<input type="button" id="btnrefreshTaiLieuKyThuat"  style="height:22px; width:35px;" value="Xóa"onclick="ClearText('FileUploadTaiLieuKyThuat');return false"  />--%>
                    <asp:Label ID="lblTaiLieuKyThuat" runat="server" Text="" Visible="false"></asp:Label>
                    |
                    <asp:LinkButton ID="lnkXoaTaiLieuKyThuat" runat="server" Height="20px" 
                        CausesValidation="false" OnClientClick="return Check('FileUploadTaiLieuKyThuat','lblTaiLieuKyThuat');"
                        Width="20px" OnClick="lnkXoaTaiLieuKyThuat_Click" >Xóa</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left;" width="15%">
                    Tài liệu quy trình sản xuất</td>
                <td style="height: 27px;width:40%;">
                    <%--LongHH 300-<400--%>
                    <asp:FileUpload ID="FileUploadTaiLieuQuyTrinhSanXuat" runat="server" Width="400px" />&nbsp;
                    <%--<input type="button" id="btnRefreshTaiLieuQuyTrinhSanXuat"  style="height:22px; width:35px;" value="Xóa" onclick="ClearText('FileUploadTaiLieuQuyTrinhSanXuat');return false"  />--%>
                    <asp:Label ID="lblTaiLieuQuyTrinhSanXuat" runat="server" Text="" Visible="false"></asp:Label>
                    |
                    <asp:LinkButton ID="lnkXoaTaiLieuSanXuat" runat="server" Height="20px" 
                        CausesValidation="false" OnClientClick="return Check('FileUploadTaiLieuQuyTrinhSanXuat','lblTaiLieuQuyTrinhSanXuat');"
                        Width="20px" OnClick="lnkXoaTaiLieuSanXuat_Click" >Xóa</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; height: 24px;" width="155">
                    Đơn đề nghị chứng nhận PH</td>
                <td style="height: 27px;width:40%;">
                    <%--LongHH 300-<400--%>
                    <asp:FileUpload ID="FileUploadTaiLieuDeNghiCN" runat="server" Width="400px" />&nbsp;
                    <%--<input type="button" id="Button1"  style="height:22px; width:35px;" value="Xóa"onclick="ClearText('FileUploadTaiLieuDeNghiCN');return false"  />--%>
                    <asp:Label ID="lblTaiLieuDeNghi" runat="server" Text="" Visible="false"></asp:Label>
                    |
                    <asp:LinkButton ID="lnkXoaTaiLieuDeNghi" runat="server" Height="20px" 
                        CausesValidation="false" OnClientClick="return Check('FileUploadTaiLieuDeNghiCN','lblTaiLieuDeNghi');"
                        Width="20px" OnClick="lnkXoaTaiLieuDeNghi_Click" >Xóa</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left;" width="15%">
                    Kết quả đo kiểm</td>
                <td style="height: 27px;width:40%;">
                    <%--LongHH 300-<400--%>
                    <asp:FileUpload ID="FileUploadKetQuaDoKiem" runat="server" Width="400px" />&nbsp;
                    <%--<input type="button" id="Button2"  style="height:22px; width:35px;" value="Xóa" onclick="ClearText('FileUploadKetQuaDoKiem');return false"  />--%>
                    <asp:Label ID="lblKetQuaDoKiem" runat="server" Text="" Visible="false"></asp:Label>
                    |
                    <asp:LinkButton ID="lnkXoaKetQuaDoKiem" runat="server" Height="20px" 
                        CausesValidation="false" OnClientClick="return Check('FileUploadKetQuaDoKiem','lblKetQuaDoKiem');"
                        Width="20px" OnClick="lnkXoaKetQuaDoKiem_Click" >Xóa</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; height: 24px;" width="15%">
                    Quy trình đảm bảo chất lượng</td>
                <td style="height: 27px;width:40%;">
                    <%--LongHH 300-<400--%>
                    <asp:FileUpload ID="FileUploadQuyTrinhDamBao" runat="server" Width="400px" />&nbsp;
                    <%--<input type="button" id="Button3"  style="height:22px; width:35px;" value="Xóa" onclick="ClearText('FileUploadQuyTrinhDamBao');return false"  />--%>
                    <asp:Label ID="lblQuyTrinhDamBao" runat="server" Text="" Visible="false"></asp:Label>
                    |
                    <asp:LinkButton ID="lnkXoaQuyTrinhDamBao" runat="server" Height="20px" 
                        CausesValidation="false" OnClientClick="return Check('FileUploadQuyTrinhDamBao','lblQuyTrinhDamBao');"
                        Width="20px"  OnClick="lnkXoaQuyTrinhDamBao_Click" >Xóa</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left;" width="15%">
                    Tiêu chuẩn tự nguyện áp dụng</td>
                <td style="height: 27px;width:40%;">
                    <asp:FileUpload ID="FileUploadTieuChuanTuNguyen" runat="server" Width="400px" />&nbsp;
                    <%--<input type="button" id="Button4"  style="height:22px; width:35px;" value="Xóa" onclick="ClearText('FileUploadTieuChuanTuNguyen');return false"  />--%>
                    <asp:Label ID="lblTieuChuanTuNguyen" runat="server" Text="" Visible="false"></asp:Label>
                    |
                    <asp:LinkButton ID="lnkXoaTieuChuanTuNguyen" runat="server" Height="20px" 
                        CausesValidation="false" OnClientClick="return Check('FileUploadTieuChuanTuNguyen','lblTieuChuanTuNguyen');"
                        Width="20px" OnClick="lnkXoaTieuChuanTuNguyen_Click" >Xóa</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left;" width="15%">
                    Chứng chỉ hệ thống QLCL</td>
                <td style="height: 27px;width:40%;">
                    <%--LongHH 300-<400--%>
                    <asp:FileUpload ID="FileUploadChungChiHeThong" runat="server" Width="400px" />&nbsp;
                    <%--<input type="button" id="Button5"  style="height:22px; width:35px;" value="Xóa" onclick="ClearText('FileUploadChungChiHeThong');return false"  />--%>
                    <asp:Label ID="lblChungChiHeThong" runat="server" Text="" Visible="false"></asp:Label>
                    |
                    <asp:LinkButton ID="lnkXoaChungChiHeThong" runat="server" Height="20px" 
                        CausesValidation="false" OnClientClick="return Check('FileUploadChungChiHeThong','lblChungChiHeThong');"
                        Width="20px" OnClick="lnkXoaChungChiHeThong_Click" >Xóa</asp:LinkButton></td>
            </tr>
            <tr id="trAnh" runat="server">
                <td align="right" style="text-align: left" width="15%">
                    Ảnh kiểm tra CSSX/lấy mẫu</td>
                <td style="height: 27px;width:40%;">
                    <asp:FileUpload ID="FileUploadAnhKTCSSX" runat="server" Width="300px" />
                    <asp:Label ID="lblAnhKiemTraCSSX" runat="server" Visible="False"></asp:Label>&nbsp;
                    |
                    <asp:LinkButton ID="lnkXoaAnh" runat="server" CausesValidation="false" Height="20px"
                         OnClientClick="return Check('FileUploadAnh','lblAnhSanPham');"
                        Width="20px" OnClick="lnkXoaAnh_Click">Xóa</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left;" valign="top" width="15%">
                    Nội dung xử lý</td>
                <td valign="top" width="40%">
                    <%--LongHH 480-<550--%>
                    <asp:TextBox ID="txtNoiDungXuLy" runat="server" Height="75px" TextMode="MultiLine"
                        Width="80%" MaxLength="20"></asp:TextBox>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                        ShowSummary="false" />
                </td>
            </tr>
            <tr>
                <td align="right" style="text-align: left;" valign="top" width="15%">
                    Ghi chú</td>
                <td valign="top" width="40%">
                    <asp:TextBox ID="txtGhiChu" runat="server" Height="75px" TextMode="MultiLine" Width="80%"
                        MaxLength="4000"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left;" width="250">
                </td>
                <td colspan="3">
                    <asp:Button ID="btnCapNhat" runat="server" Style="text-align: center" Text="Cập nhật"
                        Width="80px" OnClick="btnCapNhat_Click" />
                     <%--LongHH--%>
                    <asp:Button ID="btnCloneSanPham" runat="server" Style="text-align: center" Text="Sao chép SP"
                        Width="80px" OnClick="btnCloneSanPham_Click"  Visible="false"/>
                    <asp:Button ID="btnXoa" runat="server" Text="Xóa sản phẩm" Width="150px" Visible="false"
                        OnClick="btnXoa_Click" />
                    <%--LongHH--%>
                    <asp:Button ID="btnInPhieuDanhGia" runat="server" Text="In phiếu đánh giá" CausesValidation="false"
                        OnClick="btnInPhieuDanhGia_Click" Visible="False"/>
                    <asp:Button ID="btnBoQua" runat="server" Text="Bỏ qua" Width="80px" CausesValidation="false"
                        OnClick="btnBoQua_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
