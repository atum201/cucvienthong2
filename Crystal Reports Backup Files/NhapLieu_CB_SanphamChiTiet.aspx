<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="NhapLieu_CB_SanphamChiTiet.aspx.cs" Inherits="WebUI_NhapLieu_CB_SanphamChiTiet"
    Title="Chi tiết sản phẩm" Theme="default" %>

<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="../Js/jquery-1.3.2-vsdoc2.js"></script>

    <script type="text/javascript" src="../Js/jquery.bgiframe.min.js"></script>

    <script type="text/javascript" src="../Js/jquery.autocomplete.js"></script>

    <script type="text/javascript" src="../Js/jquery.js"></script>

    <script type="text/javascript" src="../Js/Common.js"></script>

    <script type="text/javascript" src="../Js/dhtmlxcombo.js"></script>

    <script type="text/javascript" src="../Js/dhtmlxcommon.js"></script>

    <script type="text/javascript" src="../Js/DataGridCheckBoxAll.js"></script>

    <script type="text/javascript" src="../Js/TreeViewCheckBox.js"></script>

    <script type="text/javascript" src="../Js/TreeViewCheckAll.js"></script>

    <script src="../Js/jquery.keypad.js" type="text/javascript"></script>

    <script type="text/javascript">
        function findValue(li) {
            __doPostBack('#<%= lnkPostBack.ClientID %>','');
        }

        function selectItem(li) {
	        findValue(li);
        }

        $(document).ready(function() {
	       $("#<%= txtSoGCN.ClientID %>").autocompleteArray(
		        [
			        <%= sValue %>
		        ],
		        {
			        delay:10,
			        minChars:1,
			        matchSubset:1,
			        onItemSelect:selectItem,
			        onFindValue:findValue,
			        autoFill:true,
			        maxItemsToShow:15
		        }
	        );
        });
    </script>

    <script type="text/javascript">
    
     // Kiem tra do dai so GCN
    function checkLength(source, args)
    {
        var txtSoGCN = document.getElementById("<%= txtSoGCN.ClientID %>").value;
        if(txtSoGCN.length < 17)
        {
            args.IsValid  = false;
            return;
        }
       
        args.IsValid  = true;
        return;
    }  
        
    
    // Kiểm tra tiêu chuẩn 
    function checkTieuChuan(source, args)
    {
        var chklstTieuChuan = document.getElementById("<%= chklstTieuChuan.ClientID %>").value;
        var ischecked = false;
        for(var i=0;i<chklstTieuChuan.length;i++)
        {
            if(chklstTieuChuan[i].checked)
            {   
                  ischecked=true;                 
                  break;
            }
        }
        if(ischecked = false)
        {
            args.IsValid  = false;
            return;
        }
       
        args.IsValid  = true;
        return;
    }  
    
    function ShowSoGCN(isDisplay)
    {
        var tdSoGCN1=document.getElementById('tdSoGCN1');
        var tdSoGCN2=document.getElementById('tdSoGCN2');       
        tdSoGCN1.style.display=isDisplay?"none":"";
        tdSoGCN2.style.display=isDisplay?"none":"";
        document.getElementById('ddlTenSanPhamtd').onclick=showEvent;
        document.getElementById('ddlHangSanXuattd').disabled=isDisplay;   
        
    }       
    function SetSignImage(image_url){
        var  imgSign = GetControlByName("imgMauDau");
        var  hidMauDau = GetControlByName("hidMauDau");        
        imgSign.attr("src",image_url);        
        hidMauDau.val(image_url);
    }
    function CheckMauDau(){
        var  hidMauDau = GetControlByName("hidMauDau");   
        if(hidMauDau.val()=="")
            return false;
        return true;
    }
 
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
         if(ob1.value == '' && ob2 == null )
         {
            //alert(1);
            return false;          
         }
         return confirm('Bạn có muốn xóa file đính kèm này?');
       }
    extArray = new Array(".zip",".rar",".jpeg",".jpg",".bmp",".pdf",".doc",".docx",".xls",".xlsx",".txt")
    function CheckAllFileType() {                
        if(!CheckFileType("ctl00_ContentPlaceHolder1_fileupBanCongBo"))
            return false;
        if(!CheckFileType("ctl00_ContentPlaceHolder1_fileupTuCachPhapNhan"))
            return false;
        if(!CheckFileType("ctl00_ContentPlaceHolder1_fileupBanTuDanhGia"))
            return false;
        if(!CheckFileType("ctl00_ContentPlaceHolder1_fileupTaiLieuKyThuat"))
            return false;
        if(!CheckFileType("ctl00_ContentPlaceHolder1_fileupQuyTrinhSanXuat"))
            return false;
        if(!CheckFileType("ctl00_ContentPlaceHolder1_fileupKetQuaDoKiem"))
            return false;
        if(!CheckFileType("ctl00_ContentPlaceHolder1_fileupDamBaoChatLuong"))
            return false;
        if(!CheckFileType("ctl00_ContentPlaceHolder1_fileupChungChi"))
            return false;
        if(!CheckFileType("ctl00_ContentPlaceHolder1_fileupTaiLieuKhac"))
            return false;
       // alert(1);
        return true;                                                                        
    }
    function CheckFileType(controlname)
    {
        allowSubmit = false;
        var fileob = document.getElementById(controlname);
        if(fileob!=null)
        {
            var file = fileob.value;
            //alert(file);
            
            if (!file) return true;
            while (file.indexOf("\\") != -1)
                file = file.slice(file.indexOf("\\") + 1);
           ext = file.slice(file.lastIndexOf(".",0)).toLowerCase();
            for (var i = 0; i < extArray.length; i++) {
                if (extArray[i] == ext) {  allowSubmit = true; break; }
            alert(ext);
            }
            if (allowSubmit==false)
            {
                alert("Chỉ cho phép các file có định dạng("
                        + extArray.join(" ") + ")\n Hãy chọn loại file khác");
                
            }
             return allowSubmit;
        }
        return true;
    }
    
    </script>

    <div>
        <span style="font-family: Arial">CÔNG BỐ &gt;&gt;
            <asp:Label ID="lblDsSanPham" runat="server" Text=""></asp:Label>
            CHI TIẾT SẢN PHẨM CÔNG BỐ </span>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
        ShowMessageBox="true" ShowSummary="false" />
    <table id="Table2" style="width: 100%">
        <tr>
            <td colspan="6" class="caption" style="width: 250px;">
            </td>
        </tr>
        <tr>
            <td class="caption" style="width: 250px;">
                Trung tâm</td>
            <td colspan="1" style="width: 246px;">
                <asp:TextBox ID="txtTrungTam" runat="server" BackColor="#FFFFC0" ReadOnly="True"
                    Width="242px"></asp:TextBox></td>
            <td align="right" colspan="1" style="width: 156px;" class="caption">
            </td>
            <td colspan="3" style="width: 350px;">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="caption" style="width: 250px;">
                Số hồ sơ</td>
            <td colspan="1" style="width: 246px;">
                <asp:TextBox ID="txtSoHoSo" runat="server" BackColor="#FFFFC0" ReadOnly="True" Width="242px">ABJ50K</asp:TextBox></td>
            <td colspan="1" style="width: 156px;" class="caption">
                Trạng thái hồ sơ</td>
            <td colspan="3" style="width: 350px;">
                <asp:TextBox ID="txtTrangThai" runat="server" Width="300px" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="caption" style="width: 250px;">
                Hình thức</td>
            <td colspan="1" style="width: 246px; height: 26px">
                <asp:RadioButton ID="rdTuDanhGia" runat="server" AutoPostBack="true" GroupName="HinhThuc"
                    OnCheckedChanged="rdTuDanhGia_CheckedChanged" Text="Tự đánh giá" />
                <asp:RadioButton ID="rdDaCapChungNhan" runat="server" AutoPostBack="true" Checked="True"
                    GroupName="HinhThuc" OnCheckedChanged="rdDaCapChungNhan_CheckedChanged" Text="Đã cấp GCN" />
            </td>
            <td id="Td3" class="caption" colspan="1" style="width: 156px; height: 26px">
                <asp:Label ID="lblSoGCN" runat="server" Text="Số GCN(*)" Visible="False"></asp:Label></td>
            <td id="Td4" colspan="3" style="width: 350px;">
                <asp:TextBox ID="txtSoGCN" runat="server" MaxLength="17" OnTextChanged="txtSoGCN_TextChanged"
                    Visible="False" Width="235px"></asp:TextBox>
                <asp:LinkButton ID="lnkPostBack" runat="server" OnClick="lnkPostBack_Click"></asp:LinkButton>
                <asp:RequiredFieldValidator ID="rqfSoGCN" runat="server" ControlToValidate="txtSoGCN"
                    ErrorMessage="Nhập số GCN hợp quy" InitialValue="" Visible="False">*</asp:RequiredFieldValidator>&nbsp;
                <asp:CustomValidator ID="CstValidatorSoGCN" runat="server" ClientValidationFunction="checkLength"
                    ControlToValidate="txtSoGCN" ErrorMessage="Chuỗi Số GCN phải đúng 17 ký tự" ToolTip="Chuỗi Số GCN phải đúng 17 ký tự"
                    ValidateEmptyText="True">*</asp:CustomValidator>
            </td>
        </tr>
        <tr runat="server" id="trTuDanhGia">
            <td class="caption" style="width: 250px; height: 26px">
                Số bản tự đánh giá</td>
            <td colspan="1" style="width: 246px; height: 26px">
                <asp:TextBox ID="txtSoBanTuDanhGia" runat="server" MaxLength="255" Width="242px"></asp:TextBox></td>
            <td class="caption" colspan="1" style="width: 156px; height: 26px">
                Ngày đánh giá</td>
            <td colspan="3" style="width: 350px; height: 26px">
                <asp:TextBox ID="txtNgayDanhGia" runat="server" BorderColor="Transparent" TabIndex="3"
                    Width="30%"></asp:TextBox>
                <rjs:PopCalendar ID="calendarFrom" runat="server" Control="txtNgayDanhGia" ForeColor="#FFFFC0"
                    ScriptsValidators="No Validate" Separator="/" ShowErrorMessage="False" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNgayDanhGia"
                    Display="Dynamic" ErrorMessage="Ngày không đúng định dạng DD/MM/YYYY" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d">*</asp:RegularExpressionValidator>
                (dd/mm/yyyy)</td>
        </tr>
        <tr>
            <td style="width: 250px; height: 24px;" class="caption">
                Số bản công bố</td>
            <td colspan="1" style="width: 246px; height: 24px;">
                <asp:TextBox ID="txtSoBanCongBo" runat="server" MaxLength="255" Width="242px"></asp:TextBox></td>
            <td colspan="1" style="width: 156px; height: 24px;" class="caption" valign="top">
                <div style="float: right; margin-right: 0px;">
                    Tiêu chuẩn</div>
            </td>
            <td colspan="3" rowspan="5" style="width: 350px; vertical-align: top; text-align: left">
                <asp:Panel ID="Panel1" runat="server" Height="110px" ScrollBars="Both" Width="300px">
                    <asp:CheckBoxList ID="chklstTieuChuan" runat="server" Width="100%" Height="15px">
                    </asp:CheckBoxList></asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="caption" style="width: 250px;">
                Ngày công bố</td>
            <td colspan="1" style="width: 246px; height: 24px">
                <asp:TextBox ID="txtNgayCongBo" runat="server" BorderColor="Transparent" TabIndex="3"
                    Width="30%"></asp:TextBox>
                <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="txtNgayCongBo" ForeColor="#FFFFC0"
                    ScriptsValidators="No Validate" Separator="/" ShowErrorMessage="False" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNgayCongBo"
                    Display="Dynamic" ErrorMessage="Ngày không đúng định dạng DD/MM/YYYY" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d">*</asp:RegularExpressionValidator>
                (dd/mm/yyyy)</td>
            <td class="caption" colspan="1" style="width: 156px; height: 24px" valign="top">
            </td>
        </tr>
        <tr>
            <td style="width: 250px; height: 24px;" class="caption">
                Tên sản phẩm(*)</td>
            <td colspan="1" style="width: 246px; height: 24px;">
                <cc1:ComboBox ID="ddlTenSanPham" runat="server" Width="245px" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlTenSanPham_SelectedIndexChanged">
                </cc1:ComboBox></td>
            <td style="width: 156px; height: 28px;" valign="middle">
                <div style="float: left; margin-left: 0px">
                    <asp:RequiredFieldValidator ID="rqfTenSanPham" runat="server" ErrorMessage="Chọn tên sản phẩm"
                        InitialValue="0" ControlToValidate="ddlTenSanPham">*</asp:RequiredFieldValidator>
                    <a id="lnkThemMoiSanPham" runat="server" title="Thêm mới sản phẩm" href="javascript:void(popCenter('DM_SanPham_ChiTiet.aspx?PostBack=CB_TiepNhanHoSo_TaoMoi','Popup_TaoMoiSP',width=950,height=650))">
                        Tạo mới</a>
                </div>
            </td>
        </tr>
        <tr>
            <td class="caption" style="width: 250px;">
                <span>Ký hiệu sản phẩm</span></td>
            <td colspan="1" style="width: 246px; height: 28px;" id="Td1">
                <asp:TextBox ID="txtKyHieuSanPham" runat="server" Width="242px" MaxLength="255" CssClass="textKeyPad"></asp:TextBox></td>
            <td style="width: 156px">
            </td>
        </tr>
        <tr>
            <td style="width: 250px; height: 24px;" class="caption">
                Hãng sản xuất(*)</td>
            <td colspan="1" style="width: 246px;" id="Td2">
                <div style="float: left">
                    <cc1:ComboBox ID="ddlHangSanXuat" runat="server" Width="245px">
                    </cc1:ComboBox>
                </div>
                <%--<asp:LinkButton ID="lnkTaoMoiHangSanXuat" runat="server" CausesValidation="false"  >Tạo mới</asp:LinkButton>--%>
            </td>
            <td style="width: 156px">
                <asp:RequiredFieldValidator ID="rqfHangSanXuat" runat="server" ErrorMessage="Chọn hãng sản xuất"
                    InitialValue="0" ControlToValidate="ddlHangSanXuat">*</asp:RequiredFieldValidator><span
                        style="color: #0000ff; text-decoration: underline"> </span><a id="link2" runat="server"
                            title="Thêm mới hãng sản xuất" href="javascript:void(popCenter('DM_HangSanXuat_ChiTiet.aspx?PostBack=CB_TiepNhanHoSo_TaoMoi','Popup_TaoMoiSP',width=600,height=150))">
                            Tạo mới</a> &nbsp;&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 250px; height: 24px;" class="caption">
                Bản công bố</td>
            <td colspan="5" style="width: auto;">
                <asp:FileUpload ID="fileupBanCongBo" runat="server" Width="48%" />
                <%--<input type="button" id="Button0" style="height:22px; width:35px;" value="Xóa" onclick="ClearText('fileupBanCongBo');return false"  />--%>
                <asp:Label ID="lblBanCongBo" runat="server" Visible="False"></asp:Label>
                |
                <%--<asp:LinkButton ID="lnkXoaBanCongBo" runat="server" Font-Underline="True" OnClick="lnkXoaBanCongBo_Click"
                    OnClientClick="return confirm('Bạn có muốn xóa file đính kèm này?');" Visible="False">Xóa</asp:LinkButton>--%>
                <asp:LinkButton ID="lnkXoaBanCongBo" runat="server" CausesValidation="false" Height="20px"
                    OnClick="lnkXoaBanCongBo_Click" OnClientClick="return Check('fileupBanCongBo','lblBanCongBo');"
                    Width="20px">Xóa</asp:LinkButton></td>
        </tr>
        <tr id="trf1" runat="server">
            <td style="width: 250px; height: 24px;" class="caption">
                <asp:Label ID="lblstrTuCachPhapNhan" runat="server" Text="Giấy tờ tư cách pháp nhân"></asp:Label></td>
            <td colspan="5" style="width: auto;">
                <asp:FileUpload ID="fileupTuCachPhapNhan" runat="server" Width="48%" />
                <%--<input type="button" id="Button1" style="height:22px; width:35px;" value="Xóa" onclick="ClearText('fileupTuCachPhapNhan');return false"  />--%>
                <asp:Label ID="lblTuCachPhapNhan" runat="server" Text="" Visible="false"></asp:Label>
                |
                <%--<asp:LinkButton ID="lnkXoaTuCachPhapNhan" runat="server" Font-Underline="True" OnClick="lnkXoaTuCachPhapNhan_Click"
                    OnClientClick="return confirm('Bạn có muốn xóa file đính kèm này?');" Visible="False">Xóa</asp:LinkButton>--%>
                <asp:LinkButton ID="lnkXoaTuCachPhapNhan" runat="server" CausesValidation="false"
                    Height="20px" OnClick="lnkXoaTuCachPhapNhan_Click" OnClientClick="return Check('fileupTuCachPhapNhan','lblTuCachPhapNhan');"
                    Width="20px">Xóa</asp:LinkButton></td>
        </tr>
        <tr id="trf2" runat="server">
            <td class="caption" style="width: 250px;">
                <asp:Label ID="lblstrTuDanhGia" runat="server" Text="Bản tự đánh giá"></asp:Label></td>
            <td colspan="5" style="width: auto;">
                <asp:FileUpload ID="fileupBanTuDanhGia" runat="server" Width="48%" />
                <%--<input type="button" id="Button2" style="height:22px; width:35px;" value="Xóa" onclick="ClearText('fileupBanTuDanhGia');return false"  />--%>
                <asp:Label ID="lblTuDanhGia" runat="server" Visible="False"></asp:Label>
                |
                <%--<asp:LinkButton ID="lnkXoaTuDanhGia" runat="server" Font-Underline="True" OnClick="lnkXoaTuDanhGia_Click"
                    OnClientClick="return confirm('Bạn có muốn xóa file đính kèm này?');" Visible="False">Xóa</asp:LinkButton>--%>
                <asp:LinkButton ID="lnkXoaTuDanhGia" runat="server" CausesValidation="false" Height="20px"
                    OnClick="lnkXoaTuDanhGia_Click" OnClientClick="return Check('fileupBanTuDanhGia','lblTuDanhGia');"
                    Width="20px">Xóa</asp:LinkButton></td>
        </tr>
        <tr id="trf3" runat="server">
            <td class="caption" style="width: 250px;">
                <asp:Label ID="lblstrTaiLieuKyThuat" runat="server" Text="Tài liệu kỹ thuật"></asp:Label></td>
            <td colspan="5" style="width: auto;">
                <asp:FileUpload ID="fileupTaiLieuKyThuat" runat="server" Width="48%" />
                <%--<input type="button" id="Button3" style="height:22px; width:35px;" value="Xóa" onclick="ClearText('fileupTaiLieuKyThuat');return false"  />--%>
                <asp:Label ID="lblTaiLieuKyThuat" runat="server" Text="" Visible="false"></asp:Label>
                |
                <%--<asp:LinkButton ID="lnkXoaTaiLieuKyThuat" runat="server" Font-Underline="True" OnClick="lnkXoaTaiLieuKyThuat_Click"
                    OnClientClick="return confirm('Bạn có muốn xóa file đính kèm này?');" Visible="False">Xóa</asp:LinkButton>--%>
                <asp:LinkButton ID="lnkXoaTaiLieuKyThuat" runat="server" CausesValidation="false"
                    Height="20px" OnClick="lnkXoaTaiLieuKyThuat_Click" OnClientClick="return Check('fileupTaiLieuKyThuat','lblTaiLieuKyThuat');"
                    Width="20px">Xóa</asp:LinkButton></td>
        </tr>
        <%--<tr id="trf4">
            <td class="caption" style="width:250px;">
                <asp:Label ID="lblstrQuyTrinhSanXuat" runat="server" Text="Tài liệu quy trình sản xuât"></asp:Label></td>
            <td colspan="5" style="width: auto;">
                <asp:FileUpload ID="fileupQuyTrinhSanXuat" runat="server" Width="48%" />               
                <asp:Label
                    ID="lblQuyTrinhSanXuat" runat="server" Visible="False"></asp:Label>             |               
                <asp:LinkButton ID="lnkXoaTaiLieuSanXuat" runat="server" CausesValidation="false"
                    Height="20px"  OnClick="lnkXoaTaiLieuSanXuat_Click"
                   OnClientClick="return Check('fileupQuyTrinhSanXuat','lblQuyTrinhSanXuat');" Width="20px" >Xóa</asp:LinkButton></td>
        </tr>--%>
        <tr id="trf5" runat="server">
            <td class="caption" style="width: 250px;">
                <asp:Label ID="lblstrKetQuaDoKiem" runat="server" Text="Kết quả đo kiểm"></asp:Label></td>
            <td colspan="5" style="width: auto;">
                <asp:FileUpload ID="fileupKetQuaDoKiem" runat="server" Width="48%" />
                <asp:Label ID="lblKetQuaDoKiem" runat="server" Text="" Visible="false"></asp:Label>
                |
                <asp:LinkButton ID="lnkXoaKetQuaDoKiem" runat="server" CausesValidation="false" Height="20px"
                    OnClick="lnkXoaKetQuaDoKiem_Click" OnClientClick="return Check('fileupKetQuaDoKiem','lblKetQuaDoKiem');"
                    Width="20px">Xóa</asp:LinkButton></td>
        </tr>
        <tr id="trf8" runat="server">
            <td style="height: 24px; width: 250px;" class="caption">
                <asp:Label ID="lblstrTaiLieuKhac" runat="server" Text="Tài liệu khác"></asp:Label></td>
            <td colspan="5" style="width: auto;">
                <asp:FileUpload ID="fileupTaiLieuKhac" runat="server" Width="48%" />
                <%--<input type="button" id="Button8" style="height:22px; width:35px;" value="Xóa" onclick="ClearText('fileupTaiLieuKhac');return false"  />--%>
                <asp:Label ID="lblTaiLieuKhac" runat="server" Visible="False"></asp:Label>
                |
                <%--<asp:LinkButton ID="lnkXoaTaiLieuKhac" runat="server" Font-Underline="True" OnClick="lnkXoaTaiLieuKhac_Click"
                    OnClientClick="return confirm('Bạn có muốn xóa file đính kèm này?');" Visible="False">Xóa</asp:LinkButton>--%>
                <asp:LinkButton ID="lnkXoaTaiLieuKhac" runat="server" CausesValidation="false" Height="20px"
                    OnClick="lnkXoaTaiLieuKhac_Click" OnClientClick="return Check('fileupTaiLieuKhac','lblTaiLieuKhac');"
                    Width="20px">Xóa</asp:LinkButton></td>
        </tr>
        <tr id="Tr1" runat="server">
            <td class="caption" valign="top" style="width: 250px;">
                Mẫu dấu hợp quy</td>
            <td colspan="5" style="width: auto">
                <asp:HiddenField ID="hidMauDau" runat="server" />
                <asp:Image ID="imgMauDau" runat="server" Height="90px" Width="90px" AlternateText="Mẫu dấu" />&nbsp;&nbsp;
                <asp:Button ID="btnChonMauDau" runat="server" Text="Chọn ..." />
                <asp:Button ID="btnThemMoiMauDau" runat="server" Text="Thêm mới" /></td>
        </tr>
        <tr>
            <td align="left" style="width: 20%; height: 22px;" class="caption">
                Hồ sơ</td>
            <td style="text-align: left; width: 247px; height: 22px;">
                <asp:RadioButtonList ID="rblHopLe" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="1">Hợp lệ</asp:ListItem>
                    <asp:ListItem Value="0">Kh&#244;ng hợp lệ</asp:ListItem>
                </asp:RadioButtonList></td>
            <td colspan="4" style="text-align: left; height: 22px;">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 20%;" class="caption">
            </td>
            <td style="width: 247px; text-align: left">
                <asp:RadioButtonList ID="rblDayDu" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="1">Đầy đủ</asp:ListItem>
                    <asp:ListItem Value="0">Kh&#244;ng đầy đủ</asp:ListItem>
                </asp:RadioButtonList></td>
            <td colspan="4" style="height: 21px; text-align: left">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 20%;" class="caption">
                Số/Ngày đo kiểm</td>
            <td colspan="5" style="height: 12px; text-align: left">
                <asp:TextBox ID="txtSoDoKiem" runat="server" Width="100px" MaxLength="255"></asp:TextBox>/<asp:TextBox
                    ID="txtNgayDoKiem" runat="server" CausesValidation="True" Width="100px" MaxLength="10"></asp:TextBox><rjs:PopCalendar
                        ID="PopCalendar1" runat="server" Control="txtNgayDoKiem" ScriptsValidators="No Validate"
                        Separator="/" ShowErrorMessage="False" />
                (dd/mm/yyyy)<asp:RangeValidator ID="rvCheckDate" runat="server" ErrorMessage="Nhập sai ngày đo kiểm"
                    MinimumValue="01/01/1900" Type="Date" ControlToValidate="txtNgayDoKiem">*</asp:RangeValidator></td>
        </tr>
        <tr>
            <td align="left" style="width: 20%;" class="caption">
                Cơ quan đo kiểm</td>
            <td colspan="5" style="height: 13px; text-align: left;">
                <div style="float: left">
                    <cc1:ComboBox ID="ddlCoQuanDoLuong" runat="server" Width="400px" OnChange="checkChangeCoQuanDoLuong();">
                    </cc1:ComboBox>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCoQuanDoLuong"
                    ErrorMessage="Bạn phải chọn cơ quan đo kiểm">*</asp:RequiredFieldValidator>
                <asp:LinkButton ID="lnkbtnTaoMoiCQDK" runat="server" OnClick="lnkbtnTaoMoiCoQuanDoLuong"
                    CausesValidation="False"> Tạo mới </asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 20%;" class="caption">
                Nội dung đo kiểm</td>
            <td colspan="5" style="text-align: left">
                <asp:TextBox ID="txtNoiDungDoKiem" runat="server" Rows="5" TextMode="MultiLine" Width="70%"></asp:TextBox>&nbsp;</td>
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
        <tr>
            <td align="left" class="caption" style="vertical-align: top; width: 20%">
                Nội dung xử lý</td>
            <td colspan="5" style="text-align: left">
                <asp:TextBox ID="txtNoiDungXuLy" runat="server" Rows="4" TextMode="MultiLine" Width="70%"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="left" style="width: 20%;" class="caption">
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
                    <asp:ListItem Value="6">Kh&#244;ng cấp</asp:ListItem>
                    <asp:ListItem Value="8">Kh&#244;ng phải c&#244;ng bố</asp:ListItem>
                    <asp:ListItem Value="5" Selected="True">Cấp BTN</asp:ListItem>
                    <asp:ListItem Value="4">Hủy sản phẩm</asp:ListItem>
                    <asp:ListItem Value="7">Kh&#225;c</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="rqfKetLuan" runat="server" ControlToValidate="rblKetLuan"
                    ErrorMessage="Chọn kết luận">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr id="rSogiayCB" runat="server">
            <td style="width: 20%;" align="left" class="caption">
                Số BTN/CV</td>
            <td colspan="5" style="text-align: left">
                <asp:TextBox ID="txtSoBTNCV" runat="server" Width="214px" MaxLength="50" BackColor="Transparent"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rqfSoBTN" runat="server" ControlToValidate="txtSoBTNCV"
                    ErrorMessage="Nhập số BTN/CV" Visible="False">*</asp:RequiredFieldValidator>
                <asp:FileUpload ID="fileUpSoCongVan" runat="server" />
                <asp:HyperLink ID="hlCongVan" runat="server" Visible="False">Công văn</asp:HyperLink>&nbsp;
                <asp:LinkButton ID="lnkbtnXoaSCV" runat="server" CausesValidation="False" OnClick="lnkbtnXoaSCV_Click"
                    Visible="False">Xóa</asp:LinkButton></td>
        </tr>
        <tr id="Tr2" runat="server">
            <td align="left" class="caption" style="width: 20%; height: 29px">
                <asp:Label ID="lblNgayCap" runat="server" Text="Ngày ký duyệt"></asp:Label></td>
            <td colspan="5" style="height: 29px; text-align: left">
                <asp:TextBox ID="txtNgayCap" runat="server" CausesValidation="True" MaxLength="10"
                    Width="100px">01/01/2009</asp:TextBox><rjs:PopCalendar ID="pcdNgayCap" runat="server"
                        Control="txtNgayCap" RequiredDateMessage="Bạn phải nhập ngày cấp" ScriptsValidators="No Validate"
                        Separator="/" ShowMessageBox="True" />
                <asp:Label ID="lblDinhDangNgay" runat="server" Text="(dd/mm/yyyy)"></asp:Label><asp:RangeValidator
                    ID="RangeValidator1" runat="server" ControlToValidate="txtNgayCap" ErrorMessage="Nhập sai ngày cấp"
                    MaximumValue="01/01/2099" MinimumValue="01/01/1900" Type="Date">*</asp:RangeValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNgayCap"
                    ErrorMessage="Bạn phải nhập ngày cấp">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td valign="top" class="caption" style="width: 250px;">
                Ghi chú</td>
            <td colspan="5" style="width: auto;">
                <asp:TextBox ID="txtGhiChu" runat="server" Height="70px" TextMode="MultiLine" Width="48%"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="caption" style="width: 250px;">
            </td>
            <td colspan="5" style="width: 245px;">
                <asp:Button ID="btnCapNhat" runat="server" Style="text-align: center" Text="Cập nhật"
                    Width="80px" OnClick="btnCapNhat_Click" />
                <%--<asp:Button ID="btnDanhGia" runat="server"
                        Style="text-align: center" Text="Phiếu đánh giá" Width="120px" Visible="False"
                        OnClick="btnDanhGia_Click" /><asp:Button ID="btnInPhieuDanhGia" runat="server" Style="text-align: center"
                            Text="In phiếu đánh giá" Width="145px" Visible="False" />--%>
                <asp:Button ID="btnBoQua" runat="server" Text="Bỏ qua" Width="80px" CausesValidation="False"
                    OnClick="btnBoQua_Click" /></td>
        </tr>
        <tr>
            <td class="caption" style="width: 250px;">
            </td>
            <td colspan="2" style="width: 245px;">
            </td>
            <td colspan="3" style="width: 350px;">
            </td>
        </tr>
        <tr>
            <td class="caption" style="width: 250px;">
            </td>
            <td colspan="1" style="width: 246px;">
            </td>
            <td colspan="1" style="width: 156px;" class="caption">
            </td>
            <td colspan="3" style="width: 350px;">
            </td>
        </tr>
    </table>
    <%--<script type="text/javascript">
      ShowSoGCN(<%=rdTuDanhGia.Checked.ToString().ToLower()%>);
    </script>--%>
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
