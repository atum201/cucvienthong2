<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="CB_TiepNhanHoSo_TaoMoi.aspx.cs" Inherits="WebUI_CB_TiepNhanHoSo_TaoMoi"
    Theme="Default" EnableEventValidation="false" %>

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
    <%--LongHH--%>

    <div>
        <span style="font-family: Arial">CÔNG BỐ &gt;&gt;
            <asp:Label ID="lblDsSanPham" runat="server" Text=""></asp:Label>
            CHI TIẾT SẢN PHẨM CÔNG BỐ </span>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
        ShowMessageBox="true" ShowSummary="false" />
    <table id="Table2" style="width: 100%">
        <tr>
            <td colspan="6" class="caption" width="250">
            </td>
        </tr>
        <tr>
            <td class="caption" width="14%">
                Trung tâm</td>
            <td style="width: 32%;">
                <asp:TextBox ID="txtTrungTam" runat="server" BackColor="#FFFFC0" ReadOnly="True"
                    Width="70%"></asp:TextBox></td>
            <td align="right" colspan="1" style="width: 10%;" class="caption">
            </td>
            <td style="height: 24px;" width="350">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="caption" width="14%">
                Số hồ sơ</td>
            <td style="width: 32%;">
                <asp:TextBox ID="txtSoHoSo" runat="server" BackColor="#FFFFC0" ReadOnly="True" Width="70%"></asp:TextBox></td>
            <td style="width: 10%;" class="caption">
                Trạng thái hồ sơ</td>
            <td style="height: 24px" width="32%">
                <asp:TextBox ID="txtTrangThai" runat="server" Width="300px" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="caption" width="14%" style="height: 26px">
                Hình thức</td>
            <td style="width: 32%; height: 26px;">
                <asp:RadioButton ID="rdTuDanhGia" runat="server" Text="Tự đánh giá" GroupName="HinhThuc"
                    AutoPostBack="true" OnCheckedChanged="rdTuDanhGia_CheckedChanged" />
                <asp:RadioButton ID="rdDaCapChungNhan" runat="server" Text="Đã cấp GCN" GroupName="HinhThuc"
                    AutoPostBack="true" OnCheckedChanged="rdDaCapChungNhan_CheckedChanged" Checked="True" />
            </td>
            <td style="width: 10%; height: 26px;" class="caption" id="tdSoGCN1"> <%--LongHH width:156--%>
                <asp:Label ID="lblSoGCN" runat="server" Text="Số GCN(*)" Visible="False"></asp:Label></td>
            <td style="height: 26px;" id="tdSoGCN2" width="32%">
                <asp:TextBox ID="txtSoGCN" runat="server" Width="300px" Visible="False" OnTextChanged="txtSoGCN_TextChanged" AutoPostBack="true"
                    MaxLength="17"></asp:TextBox>
                <asp:LinkButton ID="lnkPostBack" runat="server" OnClick="lnkPostBack_Click"></asp:LinkButton>
                <asp:RequiredFieldValidator ID="rqfSoGCN" runat="server" ErrorMessage="Nhập số GCN hợp quy"
                    ControlToValidate="txtSoGCN" InitialValue="" Visible="False">*</asp:RequiredFieldValidator>&nbsp;
                <asp:CustomValidator ID="CstValidatorSoGCN" runat="server" ControlToValidate="txtSoGCN"
                    ErrorMessage="Chuỗi Số GCN phải đúng 17 ký tự" ClientValidationFunction="checkLength"
                    ToolTip="Chuỗi Số GCN phải đúng 17 ký tự" ValidateEmptyText="True">*</asp:CustomValidator>
            </td>
        </tr>
        <tr runat="server" id="trTuDanhGia">
            <td class="caption" style="height: 26px" width="14%">
                Số bản tự đánh giá</td>
            <td style="width: 32%; height: 26px">

                <asp:TextBox ID="txtSoBanTuDanhGia" runat="server" MaxLength="255" Width="70%"></asp:TextBox></td>
            <td class="caption" colspan="1" style="width: 10%; height: 26px">
                Ngày đánh giá</td>
            <td style="height: 26px" width="32%">
                <asp:TextBox ID="txtNgayDanhGia" runat="server" BorderColor="Transparent" TabIndex="3"
                    Width="30%"></asp:TextBox>
                <rjs:PopCalendar ID="calendarFrom" runat="server" Control="txtNgayDanhGia" ForeColor="#FFFFC0"
                    ScriptsValidators="No Validate" Separator="/" ShowErrorMessage="False"></rjs:PopCalendar>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNgayDanhGia"
                    Display="Dynamic" ErrorMessage="Ngày không đúng định dạng DD/MM/YYYY" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d">*</asp:RegularExpressionValidator>
                (dd/mm/yyyy)</td>
        </tr>
        <tr>
            <td style="height: 24px;" class="caption" width="14%">
                Số bản công bố</td>
            <td style="width: 32%; height: 24px;">
                <asp:TextBox ID="txtSoBanCongBo" runat="server" MaxLength="255" Width="70%"></asp:TextBox></td>
            <td style="width: 10%; height: 24px;" class="caption" valign="top">
                    Tiêu chuẩn
            </td>
            <td rowspan="7" style="vertical-align: top; text-align: left" width="32%">
                 <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Width="300px"
                    BorderWidth="1px" Wrap="False" >
                    <asp:CheckBoxList ID="chklstTieuChuan" runat="server" Width="100%"
                        CellPadding="0" CellSpacing="0" >
                    </asp:CheckBoxList></asp:Panel>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="caption" style="height: 24px" width="14%">
                Ngày công bố</td>
            <td style="width: 32%; height: 24px"> <%--246 to 396--%>
                <asp:TextBox ID="txtNgayCongBo" runat="server" BorderColor="Transparent" TabIndex="3"
                    Width="70%"></asp:TextBox>
                <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtNgayCongBo" ForeColor="#FFFFC0"
                    ScriptsValidators="No Validate" Separator="/" ShowErrorMessage="False" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNgayCongBo"
                    Display="Dynamic" ErrorMessage="Ngày không đúng định dạng DD/MM/YYYY" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d">*</asp:RegularExpressionValidator>
                (dd/mm/yyyy)</td>
            <td class="caption" style="width: 10%; height: 24px" valign="top">
            </td>
        </tr>
        <tr>
            <td style="height: 24px;" class="caption" width="14%">
                Tên sản phẩm(*)</td>
            <td style="width: 32%; height: 24px;">
                <div style="float: left;width:70%;margin-right:10px;">
                    <cc1:ComboBox ID="ddlTenSanPham" runat="server" Width="100%" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlTenSanPham_SelectedIndexChanged">
                    </cc1:ComboBox>

                </div>
                <a id="link1" runat="server" title="Thêm mới sản phẩm" href="javascript:void(popCenter('DM_SanPham_ChiTiet.aspx?PostBack=CB_TiepNhanHoSo_TaoMoi','Popup_TaoMoiSP',width=950,height=650))">
                        Tạo mới</a>
                <%--<a id="link1" runat="server" title="Thêm mới sản phẩm" href="javascript:void(popCenter('DM_SanPham_ChiTiet.aspx?PostBack=CB_TiepNhanHoSo_TaoMoi','Popup_TaoMoiSP',width=950,height=650))">
                        Tạo mới</a>--%>
            </td>
            <td style="width: 10%; height: 28px;" valign="middle">
                <div style="float: left; margin-left: 0px">
                    <asp:RequiredFieldValidator ID="rqfTenSanPham" runat="server" ErrorMessage="Chọn tên sản phẩm"
                        InitialValue="0" ControlToValidate="ddlTenSanPham">*</asp:RequiredFieldValidator>
                    
                </div>
            </td>
        </tr>
        <%--LongHH them TenTiengAnh, masanpham sanpham--%>
        <tr>
            <td style="height: 28px;" class="caption" width="14%">
                <span>Tên TA sản phẩm</span></td>
            <td style="width: 32%; height: 28px;" id="Tda">
                <asp:TextBox ID="txtTenTiengAnhSanPham" runat="server" Width="70%" MaxLength="255"></asp:TextBox></td>
            <td style="width:10%"></td>
        </tr>
        <tr>
            <td style="height: 28px;" class="caption" width="14%">
                <span>Mã nhóm sản phẩm</span></td>
            <td style="width: 32%; height: 28px;" id="Tdb">
                <asp:TextBox ID="txtMaSanPham" runat="server" Width="70%" MaxLength="255"></asp:TextBox></td>
            
            <td style="width:10%"></td>
        </tr>
        <%--LongHH end--%>
        <tr>
            <td style="height: 28px;" class="caption" width="14%">
                <span>Ký hiệu sản phẩm</span></td>
            <td style="width: 32%; height: 28px;" id="Td1">
                <asp:TextBox ID="txtKyHieuSanPham" runat="server" Width="70%" MaxLength="255" CssClass="textKeyPad"></asp:TextBox></td>
            <td style="width: 10%">
            </td>
        </tr>
        <tr>
            <td style="height: 24px;" class="caption" width="14%">
                Hãng sản xuất(*)</td>
            <td style="width: 32%;" id="Td2">
                <div style="float: left; width:70%; margin-right:10px;">
                    <cc1:ComboBox ID="ddlHangSanXuat" runat="server" Width="100%">
                    </cc1:ComboBox>
                </div>
                <%--<asp:LinkButton ID="lnkTaoMoiHangSanXuat" runat="server" CausesValidation="false"  >Tạo mới</asp:LinkButton>--%>
                <a id="link2" runat="server" title="Thêm mới hãng sản xuất" href="javascript:void(popCenter('DM_HangSanXuat_ChiTiet.aspx?PostBack=CB_TiepNhanHoSo_TaoMoi','Popup_TaoMoiSP',width=600,height=150))">
                            Tạo mới</a>
            </td>
            <td style="width: 10%">
                <asp:RequiredFieldValidator ID="rqfHangSanXuat" runat="server" ErrorMessage="Chọn hãng sản xuất"
                    InitialValue="0" ControlToValidate="ddlHangSanXuat">*</asp:RequiredFieldValidator><span
                        style="color: #0000ff; text-decoration: underline"> </span>
                 &nbsp;&nbsp;</td>
        </tr>
        <tr>
            <td style="height: 24px;" class="caption" width="14%">
                Bản công bố</td>
            <td style="width: 32%;">
                <asp:FileUpload ID="fileupBanCongBo" runat="server" Width="70%" />
                <%--<input type="button" id="Button0" style="height:22px; width:35px;" value="Xóa" onclick="ClearText('fileupBanCongBo');return false"  />--%>
                <asp:Label ID="lblBanCongBo" runat="server" Visible="False"></asp:Label>
                |
                <%--<asp:LinkButton ID="lnkXoaBanCongBo" runat="server" Font-Underline="True" OnClick="lnkXoaBanCongBo_Click"
                    OnClientClick="return confirm('Bạn có muốn xóa file đính kèm này?');" Visible="False">Xóa</asp:LinkButton>--%>
                <asp:LinkButton ID="lnkXoaBanCongBo" runat="server" CausesValidation="false" Height="20px"
                    OnClick="lnkXoaBanCongBo_Click" OnClientClick="return Check('fileupBanCongBo','lblBanCongBo');"
                    Width="20px">Xóa</asp:LinkButton></td>
            <td style="width: auto;" colspan="2">
            </td>
        </tr>
        <tr id="trf1" runat="server">
            <td style="height: 24px;" class="caption" width="14%">
                <asp:Label ID="lblstrTuCachPhapNhan" runat="server" Text="Giấy tờ tư cách pháp nhân"></asp:Label></td>
            <td style="width: 32%;">
                <asp:FileUpload ID="fileupTuCachPhapNhan" runat="server" Width="48%" />
                <%--<input type="button" id="Button1" style="height:22px; width:35px;" value="Xóa" onclick="ClearText('fileupTuCachPhapNhan');return false"  />--%>
                <asp:Label ID="lblTuCachPhapNhan" runat="server" Text="" Visible="false"></asp:Label>
                |
                <%--<asp:LinkButton ID="lnkXoaTuCachPhapNhan" runat="server" Font-Underline="True" OnClick="lnkXoaTuCachPhapNhan_Click"
                    OnClientClick="return confirm('Bạn có muốn xóa file đính kèm này?');" Visible="False">Xóa</asp:LinkButton>--%>
                <asp:LinkButton ID="lnkXoaTuCachPhapNhan" runat="server" CausesValidation="false"
                    Height="20px" OnClick="lnkXoaTuCachPhapNhan_Click" OnClientClick="return Check('fileupTuCachPhapNhan','lblTuCachPhapNhan');"
                    Width="20px">Xóa</asp:LinkButton></td>
            <td style="width: auto;" colspan="2">
            </td>
        </tr>
        <tr id="trf2" runat="server">
            <td class="caption" width="14%">
                <asp:Label ID="lblstrTuDanhGia" runat="server" Text="Bản tự đánh giá"></asp:Label></td>
            <td style="width: 32%;">
                <asp:FileUpload ID="fileupBanTuDanhGia" runat="server" Width="70%" />
                <%--<input type="button" id="Button2" style="height:22px; width:35px;" value="Xóa" onclick="ClearText('fileupBanTuDanhGia');return false"  />--%>
                <asp:Label ID="lblTuDanhGia" runat="server" Visible="False"></asp:Label>
                |
                <%--<asp:LinkButton ID="lnkXoaTuDanhGia" runat="server" Font-Underline="True" OnClick="lnkXoaTuDanhGia_Click"
                    OnClientClick="return confirm('Bạn có muốn xóa file đính kèm này?');" Visible="False">Xóa</asp:LinkButton>--%>
                <asp:LinkButton ID="lnkXoaTuDanhGia" runat="server" CausesValidation="false" Height="20px"
                    OnClick="lnkXoaTuDanhGia_Click" OnClientClick="return Check('fileupBanTuDanhGia','lblTuDanhGia');"
                    Width="20px">Xóa</asp:LinkButton></td>
            <td style="width: auto;" colspan="2">
            </td>
        </tr>
        <tr id="trf3" runat="server">
            <td class="caption" width="14%">
                <asp:Label ID="lblstrTaiLieuKyThuat" runat="server" Text="Tài liệu kỹ thuật"></asp:Label></td>
            <td style="width: 32%;">
                <asp:FileUpload ID="fileupTaiLieuKyThuat" runat="server" Width="70%" />
                <%--<input type="button" id="Button3" style="height:22px; width:35px;" value="Xóa" onclick="ClearText('fileupTaiLieuKyThuat');return false"  />--%>
                <asp:Label ID="lblTaiLieuKyThuat" runat="server" Text="" Visible="false"></asp:Label>
                |
                <%--<asp:LinkButton ID="lnkXoaTaiLieuKyThuat" runat="server" Font-Underline="True" OnClick="lnkXoaTaiLieuKyThuat_Click"
                    OnClientClick="return confirm('Bạn có muốn xóa file đính kèm này?');" Visible="False">Xóa</asp:LinkButton>--%>
                <asp:LinkButton ID="lnkXoaTaiLieuKyThuat" runat="server" CausesValidation="false"
                    Height="20px" OnClick="lnkXoaTaiLieuKyThuat_Click" OnClientClick="return Check('fileupTaiLieuKyThuat','lblTaiLieuKyThuat');"
                    Width="20px">Xóa</asp:LinkButton></td>
            <td style="width: auto;" colspan="2"></td>
        </tr>
        <%--<tr id="trf4">
            <td class="caption" width="250">
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
            <td class="caption" width="14%">
                <asp:Label ID="lblstrKetQuaDoKiem" runat="server" Text="Kết quả đo kiểm"></asp:Label></td>
            <td style="width: 32%;">
                <asp:FileUpload ID="fileupKetQuaDoKiem" runat="server" Width="70%" />
                <asp:Label ID="lblKetQuaDoKiem" runat="server" Text="" Visible="false"></asp:Label>
                |
                <asp:LinkButton ID="lnkXoaKetQuaDoKiem" runat="server" CausesValidation="false" Height="20px"
                    OnClick="lnkXoaKetQuaDoKiem_Click" OnClientClick="return Check('fileupKetQuaDoKiem','lblKetQuaDoKiem');"
                    Width="20px">Xóa</asp:LinkButton></td>
            <td style="width: auto;" colspan="2">
            </td>
        </tr>
        <%--<tr id="trf6">
            <td class="caption" style="height: 24px;" width="250">
                <asp:Label ID="lblstrQuyTrinhDamBao" runat="server" Text="Quy trình đảm bảo chất lượng"></asp:Label></td>
            <td colspan="5" style="width: auto;">
                <asp:FileUpload ID="fileupDamBaoChatLuong" runat="server" Width="48%" />                
                <asp:Label
                    ID="lblQuyTrinhDamBao" runat="server" Text="" Visible="false"></asp:Label>
                |              
                <asp:LinkButton ID="lnkXoaQuyTrinhDamBao" runat="server" CausesValidation="false" Height="20px"
                     OnClick="lnkXoaQuyTrinhDamBao_Click"
                    OnClientClick="return Check('fileupDamBaoChatLuong','lblQuyTrinhDamBao');" Width="20px" >Xóa</asp:LinkButton></td>
        </tr>--%>
        <%--<tr id="trf7">
            <td class="caption" width="250">
                <asp:Label ID="lblstrChungChiHeThong" runat="server" Text="Chứng chỉ HTQL CL"></asp:Label></td>
            <td colspan="5" style="width: auto;">
                <asp:FileUpload ID="fileupChungChi" runat="server" Width="48%" />
               
                <asp:Label ID="lblChungChiHeThong"
                    runat="server" Text="" Visible="false"></asp:Label>
                |              
                <asp:LinkButton ID="lnkXoaChungChiHeThong" runat="server" CausesValidation="false" Height="20px"
                     OnClick="lnkXoaChungChiHeThong_Click"
                    OnClientClick="return Check('fileupChungChi','lblChungChiHeThong');" Width="20px" >Xóa</asp:LinkButton></td>
        </tr>--%>
        <tr id="trf8" runat="server">
            <td style="height: 24px;" class="caption" width="14%">
                <asp:Label ID="lblstrTaiLieuKhac" runat="server" Text="Tài liệu khác"></asp:Label></td>
            <td style="width: 32%;">
                <asp:FileUpload ID="fileupTaiLieuKhac" runat="server" Width="48%" />
                <%--<input type="button" id="Button8" style="height:22px; width:35px;" value="Xóa" onclick="ClearText('fileupTaiLieuKhac');return false"  />--%>
                <asp:Label ID="lblTaiLieuKhac" runat="server" Visible="False"></asp:Label>
                |
                <%--<asp:LinkButton ID="lnkXoaTaiLieuKhac" runat="server" Font-Underline="True" OnClick="lnkXoaTaiLieuKhac_Click"
                    OnClientClick="return confirm('Bạn có muốn xóa file đính kèm này?');" Visible="False">Xóa</asp:LinkButton>--%>
                <asp:LinkButton ID="lnkXoaTaiLieuKhac" runat="server" CausesValidation="false" Height="20px"
                    OnClick="lnkXoaTaiLieuKhac_Click" OnClientClick="return Check('fileupTaiLieuKhac','lblTaiLieuKhac');"
                    Width="20px">Xóa</asp:LinkButton></td>
            <td style="width: auto;" colspan="2">
            </td>
        </tr>
        <tr id="Tr1" runat="server">
            <td class="caption" valign="top" width="14%">
                Mẫu dấu hợp quy</td>
            <td style="width: 32%">
                <asp:HiddenField ID="hidMauDau" runat="server" />
                <asp:Image ID="imgMauDau" runat="server" Height="90px" Width="90px" AlternateText="Mẫu dấu" />&nbsp;&nbsp;
                <asp:Button ID="btnChonMauDau" runat="server" Text="Chọn ..." />
                <asp:Button ID="btnThemMoiMauDau" runat="server" Text="Thêm mới" /></td>
            <td style="width: auto;" colspan="2"></td>
        </tr>
        <tr id="rYKien" runat="server">
            <td valign="top" class="caption" width="14%">
                Nội dung xử lý</td>
            <td colspan="3" style="width: auto">
                <asp:TextBox ID="txtNoiDungXuLy" runat="server" Height="70px" TextMode="MultiLine"
                    Width="70%"></asp:TextBox></td>
        </tr>
        <tr>
            <td valign="top" class="caption" width="14%">
                Ghi chú</td>
            <td colspan="3" style="width: auto;">
                <asp:TextBox ID="txtGhiChu" runat="server" Height="70px" TextMode="MultiLine" Width="70%"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="caption" width="14%">
            </td>
            <td style="width: 245px;">
                <asp:Button ID="btnCapNhat" runat="server" Style="text-align: center" Text="Cập nhật"
                    Width="80px" OnClick="btnCapNhat_Click" />
                <asp:Button ID="btnCloneSanPham" runat="server" CausesValidation="false" OnClick="btnCloneSanPham_Click"
                    Text="Sao chép sản phẩm" Visible="False" />
                <asp:Button ID="btnInPhieuDanhGia" runat="server" CausesValidation="false" OnClick="btnInPhieuDanhGia_Click"
                    Text="In phiếu đánh giá" Visible="False" />
                <%--<asp:Button ID="btnDanhGia" runat="server"
                        Style="text-align: center" Text="Phiếu đánh giá" Width="120px" Visible="False"
                        OnClick="btnDanhGia_Click" /><asp:Button ID="btnInPhieuDanhGia" runat="server" Style="text-align: center"
                            Text="In phiếu đánh giá" Width="145px" Visible="False" />--%>
                <asp:Button ID="btnBoQua" runat="server" Text="Bỏ qua" Width="80px" CausesValidation="False"
                    OnClick="btnBoQua_Click" />
                &nbsp;
            </td>
            <td style="width: auto;" colspan="2">
            </td>
        </tr>
       
    </table>
    <%--<script type="text/javascript">
      ShowSoGCN(<%=rdTuDanhGia.Checked.ToString().ToLower()%>);
    </script>--%>
</asp:Content>
