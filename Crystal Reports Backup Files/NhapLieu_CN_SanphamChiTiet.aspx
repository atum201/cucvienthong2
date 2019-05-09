<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="NhapLieu_CN_SanphamChiTiet.aspx.cs" Inherits="WebUI_NhapLieu_CN_SanphamChiTiet"
    Title="Chi tiết sản phẩm" Theme="default" %>

<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
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

    <div style="margin: 10px 10px 10px 20px">
        <span style="font-family: Arial"><strong>CHỨNG NHẬN &gt;&gt;
            <asp:Label ID="lblDsSanPham" runat="server" Text=""></asp:Label>
            SẢN PHẨM CHI TIẾT </strong></span>
        <table id="Table2" style="width: 100%; color: #000000;">
            <tr>
                <td colspan="6" width="250">
                </td>
            </tr>
            <tr>
                <td align="right" style="text-align: left;" width="250">
                    Số hồ sơ</td>
                <td colspan="2" style="width: 215px;">
                    <asp:TextBox ID="txtSoHoSo" runat="server" BackColor="#FFFFC0" ReadOnly="True" Width="212px">ABJ50K</asp:TextBox></td>
                <td colspan="1" align="left" width="150">
                    Tiêu chuẩn áp dụng</td>
                <td rowspan="5" valign="top" style="width: auto">
                    <asp:Panel ID="Panel1" runat="server" Height="120px" ScrollBars="Both" Width="350px"
                        BorderWidth="1px">
                        <asp:CheckBoxList ID="chklstTieuChuan" runat="server" Width="100%">
                        </asp:CheckBoxList></asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; height: 27px;" width="250">
                    Tên sản phẩm <span>(*)</span></td>
                <td colspan="2" style="width: 215px; height: 27px;">
                    <cc1:ComboBox ID="ddlTenSanPham" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTenSanPham_SelectedIndexChanged"
                        Width="215px">
                    </cc1:ComboBox></td>
                <td style="height: 27px" width="150">
                    <asp:RequiredFieldValidator ID="rfqTenSanPham" runat="server" ErrorMessage="Chọn tên sản phẩm"
                        ControlToValidate="ddlTenSanPham" InitialValue="0" Width="4px">*</asp:RequiredFieldValidator>
                    <a id="lnkThemMoiSanPham" runat="server" href="#">Thêm mới</a>
                </td>
            </tr>
            <tr>
                <td align="right" style="text-align: left;" width="250">
                    Ký hiệu sản phẩm</td>
                <td colspan="2" style="width: 215px">
                    <div style="float: left">
                        <asp:TextBox ID="txtKyHieuSanPham" runat="server" Width="212px" MaxLength="255" CssClass="textKeyPad"></asp:TextBox>
                    </div>
                </td>
                <td width="150">
                </td>
            </tr>
            <tr style="color: #000000">
                <td align="right" style="text-align: left; height: 28px;" width="250">
                    Nhóm sản phẩm</td>
                <td colspan="2" style="width: 215px; height: 28px">
                    <asp:TextBox ID="txtNhomSanPham" runat="server" BackColor="#FFFFC0" ReadOnly="True"
                        Width="212px"></asp:TextBox></td>
                <td style="height: 28px" width="150">
                </td>
            </tr>
            <tr style="color: #000000">
                <td align="right" style="text-align: left;" width="250">
                    Hãng sản xuất <span>(*)</span></td>
                <td colspan="2" style="width: 215px">
                    <div style="float: left">
                        <cc1:ComboBox ID="ddlHangSanXuat" runat="server" Width="215px">
                        </cc1:ComboBox>
                    </div>
                </td>
                <td width="150">
                    <asp:RequiredFieldValidator ID="rfqHangSanXuat" runat="server" ErrorMessage="Chọn hãng sản xuất"
                        ControlToValidate="ddlHangSanXuat" InitialValue="0">*</asp:RequiredFieldValidator>
                    <a id="lnkThemMoiHangSX" runat="server" href="#">Thêm mới</a>
                </td>
            </tr>
            <%-- <a href="<%if(Request.QueryString["direct"]=="CN_HoSoSanPham") ../WebUI/CN_HoSoSanPham.aspx?HoSoID=Request.QueryString["HoSoId"]&TrangThaiId==Request.QueryString["TrangThaiId"]%>"&gt; DANH SÁCH SẢN PHẨM--%&gt;<asp:Label id="lblDsSanPham" runat="server" __designer:dtid="2814749767106564" Text=""></asp:Label> SẢN PHẨM</SPAN> <asp:ValidationSummary id="ValidationSummary1" runat="server" __designer:dtid="2814749767106565" ShowSummary="false" ShowMessageBox="true"></asp:ValidationSummary> <TABLE style="WIDTH: 100%; COLOR: #000000" id="Table2" __designer:dtid="2814749767106566"><TBODY><TR __designer:dtid="2814749767106567"><TD colSpan=6 __designer:dtid="2814749767106568"></TD></TR><TR __designer:dtid="2814749767106569"><TD style="WIDTH: 15%; TEXT-ALIGN: left" align=right width="16%" __designer:dtid="2814749767106570">Số hồ sơ</TD><TD style="WIDTH: 30%" colSpan=2 __designer:dtid="2814749767106571"><asp:TextBox id="txtSoHoSo" runat="server" __designer:dtid="2814749767106572" Width="212px" ReadOnly="True" BackColor="#FFFFC0">ABJ50K</asp:TextBox></TD><TD style="WIDTH: 15%" colSpan=1 __designer:dtid="2814749767106573">Tiêu chuẩn áp dụng</TD><TD vAlign=top rowSpan=13 __designer:dtid="2814749767106574"><asp:Panel id="Panel1" runat="server" __designer:dtid="2814749767106575" Width="180px" Enabled="False" BorderWidth="1px" ScrollBars="Both" Height="300px">
                        <asp:CheckBoxList __designer:dtid="2814749767106576" ID="chklstTieuChuan" runat="server" Width="160px">
                        </asp:CheckBoxList></asp:Panel> </TD></TR><TR __designer:dtid="2814749767106577"><TD style="TEXT-ALIGN: left" align=right width="16%" __designer:dtid="2814749767106578">Ký hiệu sản phẩm</TD><TD style="WIDTH: 45%" colSpan=3 __designer:dtid="2814749767106579"><asp:TextBox id="txtKyHieuSanPham" runat="server" __designer:dtid="2814749767106580" Width="212px" MaxLength="50"></asp:TextBox></TD></TR><TR __designer:dtid="2814749767106581"><TD style="TEXT-ALIGN: left" align=right __designer:dtid="2814749767106582">Tên sản phẩm <SPAN __designer:dtid="2814749767106583">(*)</SPAN></TD><TD colSpan=3 __designer:dtid="2814749767106584">&nbsp;<cc1:ComboBox id="ddlTenSanPham" runat="server" __designer:dtid="2814749767106585" Width="215px"></cc1:ComboBox> <asp:RequiredFieldValidator id="rfqTenSanPham" runat="server" __designer:dtid="2814749767106586" Width="4px" InitialValue="0" ControlToValidate="ddlTenSanPham" ErrorMessage="Chọn tên sản phẩm">*</asp:RequiredFieldValidator><asp:LinkButton id="lnkThemMoiTenSanPham" runat="server" __designer:dtid="2814749767106587" OnClientClick="popCenter('DM_SanPham_ChiTiet.aspx?PostBack=CN_TiepNhanHoSo_TaoMoi','Popup_TaoMoiSP',width=950,height=200);return false;">
                                Thêm mới</asp:LinkButton> <asp:DropDownList id="DropDownList1" runat="server" __designer:wfdid="w2"></asp:DropDownList></TD></TR><TR style="COLOR: #000000" __designer:dtid="2814749767106588"><TD style="TEXT-ALIGN: left" align=right __designer:dtid="2814749767106589">Nhóm sản phẩm</TD><TD colSpan=3 __designer:dtid="2814749767106590"><asp:TextBox id="txtNhomSanPham" runat="server" __designer:dtid="2814749767106591" Width="212px" ReadOnly="True" BackColor="#FFFFC0"></asp:TextBox></TD></TR><TR style="COLOR: #000000" __designer:dtid="2814749767106592"><TD style="TEXT-ALIGN: left" align=right __designer:dtid="2814749767106593">Hãng sản xuất <SPAN __designer:dtid="2814749767106594">(*)</SPAN></TD><TD colSpan=3 __designer:dtid="2814749767106595"><cc1:ComboBox id="ddlHangSanXuat" runat="server" __designer:dtid="2814749767106596" Width="215px">
                    </cc1:ComboBox> <asp:RequiredFieldValidator id="rfqHangSanXuat" runat="server" __designer:dtid="2814749767106597" InitialValue="0" ControlToValidate="ddlHangSanXuat" ErrorMessage="Chọn hãng sản xuất">*</asp:RequiredFieldValidator><asp:LinkButton id="lnkThemMoiHangSanXuat" runat="server" __designer:dtid="2814749767106598" OnClientClick="popCenter('DM_HangSanXuat_ChiTiet.aspx?PostBack=CN_TiepNhanHoSo_TaoMoi','Popup_TaoMoiSP',width=600,height=150);return false;">
                                Thêm mới</asp:LinkButton><%--<asp:LinkButton ID="lbtTaoMoiHSX" runat="server" OnClick="lbtTaoMoiHSX_Click">Tạo mới</asp:LinkButton>--%>
            <%-- <a href="<%if(Request.QueryString["direct"]=="CN_HoSoSanPham") ../WebUI/CN_HoSoSanPham.aspx?HoSoID=Request.QueryString["HoSoId"]&TrangThaiId==Request.QueryString["TrangThaiId"]%>"&gt; DANH SÁCH SẢN PHẨM--%&gt; SẢN PHẨM <asp:ValidationSummary id="ValidationSummary1" runat="server" __designer:dtid="2814749767106565" ShowSummary="false" ShowMessageBox="true"></asp:ValidationSummary> </TBODY></TABLE><TABLE style="WIDTH: 100%; COLOR: #000000" id="Table2" __designer:dtid="2814749767106566"><TBODY><TR __designer:dtid="2814749767106567"><TD colSpan=6 __designer:dtid="2814749767106568"></TD></TR><TR __designer:dtid="2814749767106569"><TD style="WIDTH: 15%; TEXT-ALIGN: left" align=right width="16%" __designer:dtid="2814749767106570">Số hồ sơ</TD><TD style="WIDTH: 30%" colSpan=2 __designer:dtid="2814749767106571"></TD><TD style="WIDTH: 15%" colSpan=1 __designer:dtid="2814749767106573">Tiêu chuẩn áp dụng</TD><TD vAlign=top rowSpan=13 __designer:dtid="2814749767106574">&nbsp;</TD></TR><TR __designer:dtid="2814749767106577"><TD style="TEXT-ALIGN: left" align=right width="16%" __designer:dtid="2814749767106578">Ký hiệu sản phẩm</TD><TD style="WIDTH: 45%" colSpan=3 __designer:dtid="2814749767106579"></TD></TR><TR __designer:dtid="2814749767106581"><TD style="TEXT-ALIGN: left" align=right __designer:dtid="2814749767106582">Tên sản phẩm <SPAN __designer:dtid="2814749767106583">(*)</SPAN></TD><TD colSpan=3 __designer:dtid="2814749767106584">&nbsp; <asp:LinkButton id="lnkThemMoiTenSanPham" runat="server" __designer:dtid="2814749767106587" OnClientClick="popCenter('DM_SanPham_ChiTiet.aspx?PostBack=CN_TiepNhanHoSo_TaoMoi','Popup_TaoMoiSP',width=950,height=200);return false;">
                                Thêm mới</asp:LinkButton> <asp:DropDownList id="DropDownList1" runat="server" __designer:wfdid="w2"></asp:DropDownList></TD></TR><TR style="COLOR: #000000" __designer:dtid="2814749767106588"><TD style="TEXT-ALIGN: left" align=right __designer:dtid="2814749767106589">Nhóm sản phẩm</TD><TD colSpan=3 __designer:dtid="2814749767106590"></TD></TR><TR style="COLOR: #000000" __designer:dtid="2814749767106592"><TD style="TEXT-ALIGN: left" align=right __designer:dtid="2814749767106593">Hãng sản xuất <SPAN __designer:dtid="2814749767106594">(*)</SPAN></TD><TD colSpan=3 __designer:dtid="2814749767106595">&nbsp;<asp:LinkButton id="lnkThemMoiHangSanXuat" runat="server" __designer:dtid="2814749767106598" OnClientClick="popCenter('DM_HangSanXuat_ChiTiet.aspx?PostBack=CN_TiepNhanHoSo_TaoMoi','Popup_TaoMoiSP',width=600,height=150);return false;">
                                Thêm mới</asp:LinkButton><%--<asp:LinkButton ID="lbtTaoMoiHSX" runat="server" OnClick="lbtTaoMoiHSX_Click">Tạo mới</asp:LinkButton>--%>
            <tr>
                <td align="right" style="text-align: left; height: 29px;" width="250">
                    Giấy tờ về tư cách pháp nhân</td>
                <td colspan="4" style="height: 29px">
                    <asp:FileUpload ID="FileUploadGiayToTuCachPhapNhan" runat="server" Width="300px"
                        CssClass="file_upload" />&nbsp;
                    <%--<input type="button" id="btnrefreshTuCachPhapNha" style="height:22px; width:35px;" value="Xóa" onclick="ClearText('FileUploadGiayToTuCachPhapNhan');return false"  />--%>
                    <asp:Label ID="lblTuCachPhapNhan" runat="server" Text="" Visible="false"></asp:Label>
                    |
                    <asp:LinkButton ID="lnkXoaTuCachPhapNhan" runat="server" Height="20px" CausesValidation="false"
                        OnClientClick="return Check('FileUploadGiayToTuCachPhapNhan','lblTuCachPhapNhan');"
                        Width="20px" OnClick="lnkXoaTuCachPhapNhan_Click">Xóa</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; height: 27px;" width="250">
                    Tài liệu kỹ thuật</td>
                <td colspan="4" style="height: 27px">
                    <asp:FileUpload ID="FileUploadTaiLieuKyThuat" runat="server" Width="300px" />&nbsp;
                    <%--<input type="button" id="btnrefreshTaiLieuKyThuat"  style="height:22px; width:35px;" value="Xóa"onclick="ClearText('FileUploadTaiLieuKyThuat');return false"  />--%>
                    <asp:Label ID="lblTaiLieuKyThuat" runat="server" Text="" Visible="false"></asp:Label>
                    |
                    <asp:LinkButton ID="lnkXoaTaiLieuKyThuat" runat="server" Height="20px" CausesValidation="false"
                        OnClientClick="return Check('FileUploadTaiLieuKyThuat','lblTaiLieuKyThuat');"
                        Width="20px" OnClick="lnkXoaTaiLieuKyThuat_Click">Xóa</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left;" width="250">
                    Tài liệu quy trình sản xuất</td>
                <td colspan="4">
                    <asp:FileUpload ID="FileUploadTaiLieuQuyTrinhSanXuat" runat="server" Width="300px" />&nbsp;
                    <%--<input type="button" id="btnRefreshTaiLieuQuyTrinhSanXuat"  style="height:22px; width:35px;" value="Xóa" onclick="ClearText('FileUploadTaiLieuQuyTrinhSanXuat');return false"  />--%>
                    <asp:Label ID="lblTaiLieuQuyTrinhSanXuat" runat="server" Text="" Visible="false"></asp:Label>
                    |
                    <asp:LinkButton ID="lnkXoaTaiLieuSanXuat" runat="server" Height="20px" CausesValidation="false"
                        OnClientClick="return Check('FileUploadTaiLieuQuyTrinhSanXuat','lblTaiLieuQuyTrinhSanXuat');"
                        Width="20px" OnClick="lnkXoaTaiLieuSanXuat_Click">Xóa</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; height: 24px;" width="250">
                    Đơn đề nghị chứng nhận PH</td>
                <td colspan="4" style="height: 24px">
                    <asp:FileUpload ID="FileUploadTaiLieuDeNghiCN" runat="server" Width="300px" />&nbsp;
                    <%--<input type="button" id="Button1"  style="height:22px; width:35px;" value="Xóa"onclick="ClearText('FileUploadTaiLieuDeNghiCN');return false"  />--%>
                    <asp:Label ID="lblTaiLieuDeNghi" runat="server" Text="" Visible="false"></asp:Label>
                    |
                    <asp:LinkButton ID="lnkXoaTaiLieuDeNghi" runat="server" Height="20px" CausesValidation="false"
                        OnClientClick="return Check('FileUploadTaiLieuDeNghiCN','lblTaiLieuDeNghi');"
                        Width="20px" OnClick="lnkXoaTaiLieuDeNghi_Click">Xóa</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left;" width="250">
                    Kết quả đo kiểm</td>
                <td colspan="4" style="height: 24px">
                    <asp:FileUpload ID="FileUploadKetQuaDoKiem" runat="server" Width="300px" />&nbsp;
                    <%--<input type="button" id="Button2"  style="height:22px; width:35px;" value="Xóa" onclick="ClearText('FileUploadKetQuaDoKiem');return false"  />--%>
                    <asp:Label ID="lblKetQuaDoKiem" runat="server" Text="" Visible="false"></asp:Label>
                    |
                    <asp:LinkButton ID="lnkXoaKetQuaDoKiem" runat="server" Height="20px" CausesValidation="false"
                        OnClientClick="return Check('FileUploadKetQuaDoKiem','lblKetQuaDoKiem');" Width="20px"
                        OnClick="lnkXoaKetQuaDoKiem_Click">Xóa</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; height: 24px;" width="250">
                    Quy trình đảm bảo chất lượng</td>
                <td colspan="4">
                    <asp:FileUpload ID="FileUploadQuyTrinhDamBao" runat="server" Width="300px" />&nbsp;
                    <%--<input type="button" id="Button3"  style="height:22px; width:35px;" value="Xóa" onclick="ClearText('FileUploadQuyTrinhDamBao');return false"  />--%>
                    <asp:Label ID="lblQuyTrinhDamBao" runat="server" Text="" Visible="false"></asp:Label>
                    |
                    <asp:LinkButton ID="lnkXoaQuyTrinhDamBao" runat="server" Height="20px" CausesValidation="false"
                        OnClientClick="return Check('FileUploadQuyTrinhDamBao','lblQuyTrinhDamBao');"
                        Width="20px" OnClick="lnkXoaQuyTrinhDamBao_Click">Xóa</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left;" width="250">
                    Tiêu chuẩn tự nguyện áp dụng</td>
                <td colspan="4">
                    <asp:FileUpload ID="FileUploadTieuChuanTuNguyen" runat="server" Width="300px" />&nbsp;
                    <%--<input type="button" id="Button4"  style="height:22px; width:35px;" value="Xóa" onclick="ClearText('FileUploadTieuChuanTuNguyen');return false"  />--%>
                    <asp:Label ID="lblTieuChuanTuNguyen" runat="server" Text="" Visible="false"></asp:Label>
                    |
                    <asp:LinkButton ID="lnkXoaTieuChuanTuNguyen" runat="server" Height="20px" CausesValidation="false"
                        OnClientClick="return Check('FileUploadTieuChuanTuNguyen','lblTieuChuanTuNguyen');"
                        Width="20px" OnClick="lnkXoaTieuChuanTuNguyen_Click">Xóa</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left;" width="250">
                    Chứng chỉ hệ thống QLCL</td>
                <td colspan="4">
                    <asp:FileUpload ID="FileUploadChungChiHeThong" runat="server" Width="300px" />&nbsp;
                    <%--<input type="button" id="Button5"  style="height:22px; width:35px;" value="Xóa" onclick="ClearText('FileUploadChungChiHeThong');return false"  />--%>
                    <asp:Label ID="lblChungChiHeThong" runat="server" Text="" Visible="false"></asp:Label>
                    |
                    <asp:LinkButton ID="lnkXoaChungChiHeThong" runat="server" Height="20px" CausesValidation="false"
                        OnClientClick="return Check('FileUploadChungChiHeThong','lblChungChiHeThong');"
                        Width="20px" OnClick="lnkXoaChungChiHeThong_Click">Xóa</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left" width="250">
                    Chỉ tiêu kỹ thuật kèm theo&nbsp;</td>
                <td colspan="4">
                    <asp:FileUpload ID="FileUploadChiTieuKyThuatKemTheo" runat="server" Width="300px" />&nbsp;
                    <asp:Label ID="lblChiTieuKyThuatKemTheo" runat="server" Text="" Visible="false"></asp:Label>
                    |
                    <asp:LinkButton ID="lnkXoaChiTieuKyThuatKemTheo" runat="server" CausesValidation="false"
                        Height="20px" OnClick="lnkXoaChiTieuKyThuatKemTheo_Click" OnClientClick="return Check('FileUploadChiTieuKyThuatKemTheo','lblChiTieuKyThuatKemTheo');"
                        Width="20px">Xóa</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left" width="250">
                    Hồ sơ</td>
                <td colspan="4">
                    <asp:RadioButtonList ID="rblHopLe" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">Hợp lệ</asp:ListItem>
                        <asp:ListItem Value="0">Kh&#244;ng hợp lệ</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left" width="250">
                </td>
                <td colspan="4">
                    <asp:RadioButtonList ID="rblDayDu" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">Đầy đủ</asp:ListItem>
                        <asp:ListItem Value="0">Kh&#244;ng đầy đủ</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left" width="250">
                    Số/Ngày đo kiểm</td>
                <td colspan="4">
                    <asp:TextBox ID="txtSoDoKiem" runat="server" MaxLength="255" Width="100px"></asp:TextBox>/<asp:TextBox
                        ID="txtNgayDoKiem" runat="server" CausesValidation="True" MaxLength="10" Width="100px"></asp:TextBox><rjs:PopCalendar
                            ID="calendarFrom" runat="server" Control="txtNgayDoKiem" ScriptsValidators="No Validate"
                            Separator="/" ShowErrorMessage="False"></rjs:PopCalendar>
                    (dd/mm/yyyy)<asp:RangeValidator ID="rvCheckDate" runat="server" ControlToValidate="txtNgayDoKiem"
                        ErrorMessage="Nhập sai ngày đo kiểm" MinimumValue="01/01/1900" Type="Date">*</asp:RangeValidator></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left" width="250">
                    Cơ quan đo kiểm</td>
                <td colspan="4">
                    <div style="float: left">
                        <cc1:ComboBox ID="ddlCoQuanDoLuong" runat="server" OnChange="checkChangeCoQuanDoLuong();"
                            Width="400px">
                        </cc1:ComboBox>
                    </div>
                    <asp:LinkButton ID="lnkbtnTaoMoiCQDK" runat="server" CausesValidation="False" OnClick="lnkbtnTaoMoiCoQuanDoLuong"> Tạo mới </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="right" style="text-align: left; vertical-align: top;" width="250">
                    Nội dung đo kiểm</td>
                <td colspan="4">
                    <asp:TextBox ID="txtNoiDungDoKiem" runat="server" Rows="5" TextMode="MultiLine" Width="70%"></asp:TextBox></td>
            </tr>
            <tr id="rKTCSSX" runat="server">
                <td align="left" style="width: 20%;" class="caption">
                    Kết quả kiểm tra CSSX</td>
                <td colspan="5" style="height: auto; text-align: left">
                    <asp:RadioButtonList ID="rblKetQuaKiemTra" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">Đạt</asp:ListItem>
                        <asp:ListItem Value="0">Kh&#244;ng đạt</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr id="rTanSo" runat="server">
                <td align="left" style="width: 20%;" class="caption">
                    Quy hoạch tần số</td>
                <td colspan="5" style="text-align: left">
                    <asp:RadioButtonList ID="rblQuyHoachTanSo" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">Ph&#249; hợp</asp:ListItem>
                        <asp:ListItem Value="0">Kh&#244;ng ph&#249; hợp</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr runat="server" id="Tr1">
                <td align="left" class="caption" style="width: 20%; vertical-align: top;">
                    Nội dung xử lý</td>
                <td colspan="5" style="text-align: left">
                    <asp:TextBox ID="txtNoiDungXuLy" runat="server" Rows="4" TextMode="MultiLine" Width="70%"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="left" style="width: 20%; vertical-align: top;" class="caption">
                    Nhận xét khác</td>
                <td colspan="5" style="text-align: left">
                    <asp:TextBox ID="txtNhanXetKhac" runat="server" TextMode="MultiLine" Width="70%"
                        Rows="4">
                    </asp:TextBox></td>
            </tr>
            <tr id="rKetLuan">
                <td align="left" style="width: 20%;" class="caption">
                    Kết luận</td>
                <td colspan="5" style="text-align: left">
                    <asp:RadioButtonList ID="rblKetLuan" runat="server" RepeatDirection="Horizontal"
                        CssClass="rdKetLuan">
                        <asp:ListItem Value="2">Kh&#244;ng cấp</asp:ListItem>
                        <asp:ListItem Value="3">Kh&#244;ng phải chứng nhận</asp:ListItem>
                        <asp:ListItem Value="1" Selected="True">Cấp GCN</asp:ListItem>
                        <asp:ListItem Value="4">Hủy sản phẩm</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="rqfKetLuan" runat="server" ControlToValidate="rblKetLuan"
                        ErrorMessage="Chọn kết luận">*</asp:RequiredFieldValidator></td>
            </tr>
            <tr id="rThoiHan">
                <td style="width: 20%;" align="left" class="caption">
                    Thời hạn</td>
                <td colspan="5" style="text-align: left">
                    <asp:TextBox ID="txtThoiHan" runat="server" BackColor="#FFFFC0" MaxLength="50" ReadOnly="True"
                        Width="214px"></asp:TextBox></td>
            </tr>
            <tr id="rLephi">
                <td style="width: 20%;" align="left" class="caption">
                    Lệ phí chứng nhận</td>
                <td colspan="5" style="text-align: left">
                    <asp:TextBox ID="txtLePhi" runat="server" ReadOnly="True" Width="214px" MaxLength="50"
                        BackColor="#FFFFC0"></asp:TextBox>(VNĐ)</td>
            </tr>
            <tr id="rSogiayCN" runat="server">
                <td style="width: 20%;" align="left" class="caption">
                    Số GCN/CV</td>
                <td colspan="5" style="text-align: left">
                    <asp:TextBox ID="txtSoGCNCV" runat="server" Width="214px" MaxLength="50" BackColor="Transparent"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqfSoGCN" runat="server" ControlToValidate="txtSoGCNCV"
                        ErrorMessage="Nhập số GCN/CV" Visible="False">*</asp:RequiredFieldValidator>
                    <asp:FileUpload ID="fileUpSoCongVan" runat="server" />
                    <asp:HyperLink ID="hlCongVan" runat="server" Visible="False">Công văn</asp:HyperLink>&nbsp;
                    <asp:LinkButton ID="lnkbtnXoaSCV" runat="server" CausesValidation="False" OnClick="lnkbtnXoaSCV_Click"
                        Visible="False">Xóa</asp:LinkButton></td>
            </tr>
            <tr id="rNgayCap" runat="server">
                <td align="left" class="caption" style="width: 20%; height: 29px;">
                    <asp:Label ID="lblNgayCap" runat="server" Text="Ngày ký duyệt"></asp:Label></td>
                <td colspan="5" style="text-align: left; height: 29px;">
                    <asp:TextBox ID="txtNgayCap" runat="server" CausesValidation="True" MaxLength="10"
                        Width="100px"></asp:TextBox><rjs:PopCalendar ID="pcdNgayCap" runat="server" Control="txtNgayCap"
                            ScriptsValidators="No Validate" Separator="/" RequiredDateMessage="Bạn phải nhập ngày cấp"
                            ShowMessageBox="True" />
                    <asp:Label ID="lblDinhDangNgay" runat="server" Text="(dd/mm/yyyy)"></asp:Label><asp:RangeValidator
                        ID="RangeValidator1" runat="server" ControlToValidate="txtNgayCap" ErrorMessage="Nhập sai ngày cấp"
                        MinimumValue="01/01/1900" Type="Date" MaximumValue="01/01/2099">*</asp:RangeValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNgayCap"
                        ErrorMessage="Bạn phải nhập ngày cấp">*</asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right" style="text-align: left;" width="250">
                </td>
                <td colspan="5">
                    <asp:Button ID="btnCapNhat" runat="server" Style="text-align: center" Text="Cập nhật"
                        Width="80px" OnClick="btnCapNhat_Click" />
                    <asp:Button ID="btnBoQua" runat="server" Text="Bỏ qua" Width="80px" CausesValidation="false"
                        OnClick="btnBoQua_Click" /></td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        function ShowHideThoiHan() {
            var col = document.getElementById("<%= rblKetLuan.ClientID %>");
            var inputs = col.getElementsByTagName("input"); 
            var blShowFileUpLoad=false;
            var strCheckValue='';
            for(var i=0;i<inputs.length;i++)
            {
                if(inputs[i].checked)
                {
                    strCheckValue = inputs[i].value;
                    if(inputs[i].value!='1')
                        blShowFileUpLoad=true;
                    break;
                }                    
            } 
            if(strCheckValue=='1')
            {
                document.getElementById("rThoiHan").style.display='';
                document.getElementById("rLephi").style.display='';
                document.getElementById("<%= fileUpSoCongVan.ClientID %>").style.display=''; 
            }
            else
            {
                document.getElementById("rThoiHan").style.display='none';
                document.getElementById("rLephi").style.display='none';
            } 
        }
        
        function showHidefileUpSoCongVan()
        {    
           var controlKetLuan = document.getElementById("<%= rblKetLuan.ClientID %>");
            
           var inputsKetLuan = controlKetLuan.getElementsByTagName("input"); 
            
            var blKetLuan=false;
            for(var i=0;i<inputsKetLuan.length;i++)
            {
                if(inputsKetLuan[i].checked && inputsKetLuan[i].value!='1')
                {   
                      blKetLuan=true;  
                      break;
                }
            }          
            if(blKetLuan)
            {
                if(document.getElementById("<%= fileUpSoCongVan.ClientID %>")!=null)
                {
                    var varBanTiepNhan=document.getElementById("<%= hdSoCongVan.ClientID %>").value; 
                    document.getElementById("<%= txtSoGCNCV.ClientID %>").value=varBanTiepNhan;
                    document.getElementById("<%= fileUpSoCongVan.ClientID %>").style.display=''; 
                    document.getElementById("<%= txtSoGCNCV.ClientID %>").readOnly = false;
                }
            }
            else
            {
                if(document.getElementById("<%= fileUpSoCongVan.ClientID %>")!=null)  
                {
                    document.getElementById("<%= fileUpSoCongVan.ClientID %>").style.display='none';         
                }
            } 
        }
    </script>
    
    <asp:HiddenField ID="hdConfirm" runat="server" />
    <asp:HiddenField ID="hdTrangThaiId" runat="server" />
    <asp:HiddenField ID="hdThamDinh" runat="server" />
    <asp:HiddenField ID="hdNguonGoc" runat="server" />
    <asp:HiddenField ID="hdTanSo" runat="server" />
    <asp:HiddenField ID="hdSanPham" runat="server" />
    <asp:HiddenField ID="hdHangSanXuat" runat="server" />
    <asp:HiddenField ID="hdGiaTriLoHang" runat="server" />
    <asp:HiddenField ID="hdCoQuanDoKiem" runat="server" />
    <asp:HiddenField ID="hdIsPostBack" runat="server" />
    <asp:HiddenField ID="hdSoCongVan" runat="server" />
    <asp:HiddenField ID="hdIsHoSoMoi" runat="server" />

</asp:Content>
