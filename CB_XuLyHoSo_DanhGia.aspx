<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="CB_XuLyHoSo_DanhGia.aspx.cs" Inherits="WebUI_CB_XuLyHoSo_DanhGia" Theme="Default" %>

<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        
        function HideRadioButton(index){
            $($("#rKetLuan input[type=radio]").get(index)).attr('checked', false);
            $($("#rKetLuan input[type=radio]").get(index)).hide();
            $($("#rKetLuan label").get(index)).hide();            
        }
         function ShowRadioButton(index){
            $($("#rKetLuan input[type=radio]").get(index)).show();
            $($("#rKetLuan label").get(index)).show();            
        }
        
        function CheckConfirm(msgMessage){
            var iTrangThaiID;
            iTrangThaiID=document.getElementById("<%= hdTrangThaiId.ClientID %>").value;
            
            if(iTrangThaiID=='1'||iTrangThaiID=='4'||iTrangThaiID=='7'){            
                var blValue= confirm(msgMessage);                    
                document.getElementById("<%= hdConfirm.ClientID %>").value=blValue;
            }else{
                document.getElementById("<%= hdConfirm.ClientID %>").value=false;
            }
            return true;
        }
        
        function ShowHideKetLuan() {
           var controlHopLe = document.getElementById("<%= rblHopLe.ClientID %>");
           var controlDayDu = document.getElementById("<%= rblDayDu.ClientID %>");
                   
           var inputsHopLe = controlHopLe.getElementsByTagName("input");  
           var inputsDayDu = controlDayDu.getElementsByTagName("input"); 
           
            var blHopLe=false;
            var blDayDu=false;
            
            for(var i=0;i<inputsHopLe.length;i++)
            {
                if(inputsHopLe[i].checked && inputsHopLe[i].value=='1')
                {   
                      blHopLe=true;                 
                      break;
                }
            }
            
            for(var i=0;i<inputsDayDu.length;i++)
            {
                if(inputsDayDu[i].checked && inputsDayDu[i].value=='1')
                {   
                      blDayDu=true;                 
                      break;
                }
            }
            
            if(blDayDu && blHopLe)
            {  
                 HideRadioButton(4);
                 ShowRadioButton(2);             
                ShowHideLePhi();             
                
                              
            }
            else
            {
                HideRadioButton(2);
                ShowRadioButton(4);                 
                document.getElementById("rLephi").style.display='none';
            }            
        }
        
         function ShowHideLePhi() {
         
                var col = document.getElementById("<%= rblKetLuan.ClientID %>");
                            
                var inputs = col.getElementsByTagName("input"); 
                var blShowFileUpLoad=false;
                var strCheckValue='';
                for(var i=0;i<inputs.length;i++)
                {
                    if(inputs[i].checked)
                    {
                        strCheckValue = inputs[i].value;
                        if(inputs[i].value!='5')
                        {
                            blShowFileUpLoad=true;
                            break;
                        }
                    }                    
                } 
                if(strCheckValue=='5')
                {
                    document.getElementById("rLephi").style.display='none';
                }
                else
                {
                    document.getElementById("rLephi").style.display='none';
                } 
                 //không cho in phiếu đánh giá ở trạng thái khác
                if(strCheckValue=='7')
                {
                    if(document.getElementById("<%= btnInPhieuDanhGia.ClientID %>")!=null)
                        document.getElementById("<%= btnInPhieuDanhGia.ClientID %>").style.display='none';  
                }
                else 
                {
                    if(document.getElementById("<%= btnInPhieuDanhGia.ClientID %>")!=null)
                        document.getElementById("<%= btnInPhieuDanhGia.ClientID %>").style.display=''; 
                }
                //file upload
                if(blShowFileUpLoad)
                {
                    if(document.getElementById("<%= fileUpSoCongVan.ClientID %>")!=null)
                    {
                        var varBanTiepNhan=document.getElementById("<%= hdSoCongVan.ClientID %>").value; 
                        if(varBanTiepNhan != 'Số sinh tự động')
                            document.getElementById("<%= txtSoBTNCV.ClientID %>").value=varBanTiepNhan;
                        document.getElementById("<%= fileUpSoCongVan.ClientID %>").style.display=''; 
                        //  Cho nhap so cong van
                        document.getElementById("<%= txtSoBTNCV.ClientID %>").value='';
                        document.getElementById("<%= txtSoBTNCV.ClientID %>").readOnly = false;
                        document.getElementById("<%= txtSoBTNCV.ClientID %>").style.backgroundColor = '#ffffff';  
                    }
                }
                else
                {
                    if(document.getElementById("<%= fileUpSoCongVan.ClientID %>")!=null)  
                    {
                        if(strCheckValue == '5')
                        {
                            document.getElementById("<%= txtSoBTNCV.ClientID %>").value='Số sinh tự động';
                            document.getElementById("<%= txtSoBTNCV.ClientID %>").readOnly = true;
                            document.getElementById("<%= txtSoBTNCV.ClientID %>").style.backgroundColor = '#FFFFC0';  
                        }
                        document.getElementById("<%= fileUpSoCongVan.ClientID %>").style.display='none';         
                    }
                } 
        }
        
        function showHidefileUpSoCongVan()
        {    
           var controlKetLuan = document.getElementById("<%= rblKetLuan.ClientID %>");
            
           var inputsKetLuan = controlKetLuan.getElementsByTagName("input"); 
            
            var blKetLuan=false;
            for(var i=0;i<inputsKetLuan.length;i++)
            {
                if(inputsKetLuan[i].checked && inputsKetLuan[i].value != '5')
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
                    document.getElementById("<%= txtSoBTNCV.ClientID %>").value=varBanTiepNhan;
                    document.getElementById("<%= fileUpSoCongVan.ClientID %>").style.display=''; 
                    
                     //  Cho nhap so cong van
                    document.getElementById("<%= txtSoBTNCV.ClientID %>").value='';
                    document.getElementById("<%= txtSoBTNCV.ClientID %>").readOnly = false;
                    document.getElementById("<%= txtSoBTNCV.ClientID %>").style.backgroundColor = '#ffffff';  
                }
            }
            else
            {
                if(document.getElementById("<%= fileUpSoCongVan.ClientID %>")!=null)  
                {
                    document.getElementById("<%= txtSoBTNCV.ClientID %>").value='Số sinh tự động';
                    document.getElementById("<%= txtSoBTNCV.ClientID %>").readOnly = true;
                    document.getElementById("<%= txtSoBTNCV.ClientID %>").style.backgroundColor = '#FFFFC0';  
                    document.getElementById("<%= fileUpSoCongVan.ClientID %>").style.display='none';         
                }
            } 
        }
        
        function ShowHideButton(val){
            var iTrangThai=document.getElementById("<%= hdTrangThaiId.ClientID %>").value;
            var btn=document.getElementById("<%= btnGuiThamDinh.ClientID %>");
            var btnGuiLanhDao=document.getElementById("<%= btnGuiLanhDao.ClientID %>");
            
            if (btn!=null)
            {
                if(iTrangThai=='3'||iTrangThai=='4')
                {
                    var controlHopLe = document.getElementById("<%= rblHopLe.ClientID %>");
                    var controlDayDu = document.getElementById("<%= rblDayDu.ClientID %>");

                    var inputsHopLe = controlHopLe.getElementsByTagName("input");  
                    var inputsDayDu = controlDayDu.getElementsByTagName("input"); 

                    var blHopLe=false;
                    var blDayDu=false;
                 
                    for(var i=0;i<inputsHopLe.length;i++)
                    {
                        if(inputsHopLe[i].checked && inputsHopLe[i].value=='1')
                        {   
                              blHopLe=true;                 
                              break;
                        }
                    }
                    
                    for(var i=0;i<inputsDayDu.length;i++)
                    {
                        if(inputsDayDu[i].checked && inputsDayDu[i].value=='1')
                        {   
                              blDayDu=true;                 
                              break;
                        }
                    }

                    var col = document.getElementById("<%= rblKetLuan.ClientID %>");
                                
                    var inputs = col.getElementsByTagName("input"); 
                    var strCheckValue='';
                    for(var i=0;i<inputs.length;i++)
                    {
                        if(inputs[i].checked)
                        {
                            strCheckValue = inputs[i].value;
                            break;
                        }
                    }
                    if(strCheckValue !='7')
                    {
                        if (val)
                        {
                         
                            btn.style.display='';
                            document.getElementById("<%= hdThamDinh.ClientID %>").value='true';
                        }
                        else
                        {
                            btn.style.display='none';
                            document.getElementById("<%= hdThamDinh.ClientID %>").value='false';
                        }
                    }
                    else
                    {
                        btn.style.display='none';
                        document.getElementById("<%= hdThamDinh.ClientID %>").value='false';
                    }
                    
                    // Ẩn hiện nút gửi lãnh đạo
                    if(btnGuiLanhDao != true)
                    {
                        if(strCheckValue !='7' && strCheckValue != '1')
                        {
                            if(btn.style.display !='none')
                            {
                                btnGuiLanhDao.style.display = 'none';
                            }
                            else
                            {
                                btnGuiLanhDao.style.display = '';
                            }
                        }
                        else
                        {
                            btnGuiLanhDao.style.display='none';
                        }
                    }
                }
            }
            
           
        }
        
        function getValueOfControl(ctrl){  
            if (ctrl.type=="checkbox")
                return ''+ctrl.checked;
            if (ctrl.type=="radio")
                return ''+ctrl.checked;
            if (ctrl.tagName=="select")
                return ''+ctrl.selectedIndex; 
            return ''+ctrl.value;
        }
        
        function getDefaultValue(ctrl){                 
            return $(ctrl).attr("defaultvalue");                                   
        }      
        
        $(
            function()
            {    
                            
                $("span.defaultvalue").find("input").each(
                    function(){
                        $(this).attr("defaultvalue",$(this).parent().attr("defaultvalue")); 
                        $(this).addClass("defaultvalue");
                    }
                );  
                $("span.state").find("input").each(
                    function(){
                        $(this).attr("state",$(this).parent().attr("state")); 
                        $(this).addClass("state");
                    }
                );
                var checkChange= false;  
                $("input.defaultvalue,textarea.defaultvalue,select.defaultvalue").each(
                            function(){  
                                if (getDefaultValue(this)!=getValueOfControl(this))
                                {  
                                    checkChange=true;                
                                }                    
                            }
                          )
                  var iTrangThai=document.getElementById("<%= hdTrangThaiId.ClientID %>").value;                      
                  if(iTrangThai=='3'||iTrangThai=='4'){
                    var ButtonThamDinh=document.getElementById("<%= btnGuiThamDinh.ClientID %>");
                    if(ButtonThamDinh!=null){
                        var blPostBack='false';
                        if(document.getElementById("<%= hdIsPostBack.ClientID %>")!=null)
                            blPostBack=document.getElementById("<%= hdIsPostBack.ClientID %>").value;
                        if(blPostBack=='false')
                            ButtonThamDinh.style.display='none';
                    }
                  }
                                
                $("input.defaultvalue,textarea.defaultvalue,select.defaultvalue").change(
                    function(){
                         var checkChange= false;     
                         //alert(getDefaultValue(this) +'  111111111 ' + getValueOfControl(this)+' id: ' +this.id+'  visible '+this.style.display);
                         if (getDefaultValue(this).replace(new RegExp( "\\n|\\r", "g" ),"") != getValueOfControl(this).replace(new RegExp( "\\n|\\r", "g" ),"")){
                               
                                checkChange=true; 
                         }
                         if (!checkChange) {                                                 
                             $("input.defaultvalue,textarea.defaultvalue,select.defaultvalue").each(
                                function(){ 
                                    var a, b;
                                    a = getDefaultValue(this).replace(new RegExp( "\\n|\\r", "g" ),"");                                    
                                    b = getValueOfControl(this).replace(new RegExp( "\\n", "g" ),"");
                                   
                                    if (a != b)
                                    { 
                                        
                                        var strVisibility=$(this).parent().attr("state");
                                        if(strVisibility!='hidden'){                                  
                                            checkChange=true;                                             
                                        }
                                    }                                      
                                }
                              )
                          }
                          ShowHideButton(checkChange);                        
                    }
                )    
            }
        );
        
        function checkChangeCoQuanDoLuong(){
            var hiddenValue=document.getElementById("<%= hdCoQuanDoKiem.ClientID %>").value;
            var currentValue=document.getElementById("<%= ddlCoQuanDoLuong.ClientID %>").value;
            var iTrangThai=document.getElementById("<%= hdTrangThaiId.ClientID %>").value;                      
                  if(iTrangThai=='3'||iTrangThai=='4'){
                    if((hiddenValue!=currentValue)){
                        ShowHideButton(true);
                    }else
                        ShowHideButton(false);
                  }
        }
        function checkChangeSanPham(){
            var hiddenValue=document.getElementById("<%= hdSanPham.ClientID %>").value;
            var currentValue=document.getElementById("<%= ddlTenSanPham.ClientID %>").value;
            var iTrangThai=document.getElementById("<%= hdTrangThaiId.ClientID %>").value;                      
                  if(iTrangThai=='3'||iTrangThai=='4'){
                    if((hiddenValue!=currentValue)){
                        ShowHideButton(true);
                    }else
                        ShowHideButton(false);
                  }
        }
        function checkChangetxtSanPham(){
            var hiddenValue=document.getElementById("<%= hdSanPham.ClientID %>").value;
            var currentValue=document.getElementById("<%= ddlTenSanPham.ClientID %>").value;
            var iTrangThai=document.getElementById("<%= hdTrangThaiId.ClientID %>").value;                      
                  if(iTrangThai=='3'||iTrangThai=='4'){
                    if((hiddenValue!=currentValue)){
                        ShowHideButton(true);
                    }else
                        ShowHideButton(false);
                  }
        }
        function checkChangeHangSanXuat(){
            var hiddenValue=document.getElementById("<%= hdHangSanXuat.ClientID %>").value;
            var currentValue=document.getElementById("<%= ddlHangSanXuat.ClientID %>").value;
            var iTrangThai=document.getElementById("<%= hdTrangThaiId.ClientID %>").value;                      
                  if(iTrangThai=='3'||iTrangThai=='4'){
                    if((hiddenValue!=currentValue)){
                        ShowHideButton(true);
                    }else
                        ShowHideButton(false);
                  }
        }
        function checkChangeGiaTriLoHang(){
            var hiddenValue=document.getElementById("<%= hdGiaTriLoHang.ClientID %>").value;
            var currentValue=document.getElementById("<%= ddlGiaTriLoHang.ClientID %>").value;
            var iTrangThai=document.getElementById("<%= hdTrangThaiId.ClientID %>").value;                      
                  if(iTrangThai=='3'||iTrangThai=='4'){
                    if((hiddenValue!=currentValue)){
                        ShowHideButton(true);
                    }else
                        ShowHideButton(false);
                  }
        }
     
    </script>

    <div>
        <span style="font-family: Arial"><strong>CÔNG BỐ&gt;&gt;<asp:HyperLink ID="hlDanhSachHoSo"
            runat="server"></asp:HyperLink></strong><a href="../WebUI/CN_HoSo_QuanLy.aspx?direct=CB_HoSoDen"></a><strong>&gt;&gt;<asp:HyperLink
                ID="hlDanhSachSanPham" runat="server"></asp:HyperLink></strong><strong>&gt;&gt; </strong>
            <asp:Label ID="lblTrangThai" runat="server"></asp:Label></span></div>
    <table id="Table6" style="width: 100%">
        <tr style="font-family: Times New Roman">
        </tr>
        <tr>
            <td align="right" style="height: 21px; text-align: right; width: 100%;" colspan="6">
                <fieldset style="width: 98%">
                    <legend>Thông tin sản phẩm</legend>
                    <table id="Table2" style="width: 100%">
                        <tr>
                            <td colspan="2" align="left" valign="top" style="width: 20%;">
                                <div align="left">
                                    <table width="100%" cellspacing="0">
                                        <tr>
                                            <td align="left" class="caption" style="width: 15%">
                                            </td>
                                            <td align="left" style="width: 30%; text-align: left" valign="top">
                                                <asp:ValidationSummary ID="vsXyLyHoSoDanhGia" runat="server" ShowMessageBox="True"
                                                    ShowSummary="False" />
                                                </td>
                                            <td align="left" style="width: 30%; text-align: left" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 15%;" class="caption">
                                                <span style="width: 20%; height: 24px; text-align: right"><span>T&ecirc;n s&#7843;n
                                                    ph&#7849;m</span></span></td>
                                            <td valign="top" align="left" style="width: 30%; text-align: left">
                                                <div style="float: left">
                                                    <cc1:ComboBox ID="ddlTenSanPham" runat="server" AutoPostBack="True" Width="290px"
                                                        OnSelectedIndexChanged="ddlTenSanPham_SelectedIndexChanged" onChange="checkChangeSanPham();">
                                                    </cc1:ComboBox></div>
                                                <asp:LinkButton ID="lnkbtnTaoMoiSP" runat="server" OnClick="lnkbtnTaoMoiSP_Click"
                                                    CausesValidation="False"> Tạo mới </asp:LinkButton>
                                            </td>
                                            <td align="left" valign="top" width="44%" style="width: 30%; text-align: left">
                                                &nbsp;Tiêu chuẩn áp dụng</td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 15%; height: 26px;" class="caption">
                                                K&yacute; hi&#7879;u</td>
                                            <td valign="top" align="left" style="width: 30%; text-align: left; height: 26px;">
                                                <asp:TextBox ID="txtKyHieu" runat="server" Width="288px" MaxLength="255"></asp:TextBox></td>
                                            <td align="left" valign="top" style="width: 30%; text-align: left" rowspan="8">
                                                <asp:Panel ID="Panel1" runat="server" Height="100px" ScrollBars="Both" Width="80%"
                                                    BorderWidth="1px" Wrap="False">
                                                    <asp:CheckBoxList ID="chklstTieuChuan" runat="server" Width="300px" Height="100px"
                                                        CellPadding="0" CellSpacing="0" OnSelectedIndexChanged="chklstTieuChuan_SelectedIndexChanged">
                                                    </asp:CheckBoxList></asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="caption" style="width: 15%">
                                                Số bản công bố</td>
                                            <td align="left" style="width: 30%; text-align: left" valign="top">
                                                <asp:TextBox ID="txtSoBanCongBo" runat="server" Width="288px" MaxLength="255"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="caption" style="width: 15%">
                                                Ngày công bố</td>
                                            <td align="left" style="width: 30%; text-align: left" valign="top">
                                                <asp:TextBox ID="txtNgayCongBo" runat="server" BorderColor="Transparent" TabIndex="3"
                                                    Width="30%"></asp:TextBox>
                                                <rjs:PopCalendar ID="pclNgayCongBo" runat="server" ForeColor="#FFFFC0"
                                                    ScriptsValidators="No Validate" Separator="/" ShowErrorMessage="False" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNgayCongBo"
                                                    Display="Dynamic" ErrorMessage="Ngày không đúng định dạng DD/MM/YYYY" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d">*</asp:RegularExpressionValidator>
                                                (dd/mm/yyyy)</td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="caption" style="width: 15%">
                                                Số bản tự đánh giá</td>
                                            <td align="left" style="width: 30%; text-align: left" valign="top">
                                                <asp:TextBox ID="txtSoBanTuDanhGia" runat="server" MaxLength="255" Width="288px"></asp:TextBox></td>
                                        </tr>
                                        <tr runat="server" id="rSoBanTuDanhGia">
                                            <td align="left" class="caption" style="width: 15%">
                                                Ngày đánh giá</td>
                                            <td align="left" style="width: 30%; text-align: left" valign="top">
                                                <asp:TextBox ID="txtNgayDanhGia" runat="server" BorderColor="Transparent" TabIndex="3"
                                                    Width="30%"></asp:TextBox>
                                                <rjs:PopCalendar ID="pclNgayDanhGia" runat="server" Control="txtNgayDanhGia" ForeColor="#FFFFC0"
                                                    ScriptsValidators="No Validate" Separator="/" ShowErrorMessage="False" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNgayDanhGia"
                                                    Display="Dynamic" ErrorMessage="Ngày không đúng định dạng DD/MM/YYYY" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d">*</asp:RegularExpressionValidator>
                                                (dd/mm/yyyy)</td>
                                        </tr>
                                        <tr runat="server" id="rNgayTuDanhGia">
                                            <td align="left" style="width: 15%;" class="caption">
                                                Tr&#7841;ng th&aacute;i</td>
                                            <td valign="top" align="left" style="width: 30%; text-align: left">
                                                <asp:TextBox ID="txtTrangThai" runat="server" Width="288px" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 15%;" class="caption">
                                                H&atilde;ng s&#7843;n xu&#7845;t</td>
                                            <td valign="top" align="left" style="width: 30%; text-align: left">
                                                <div style="float: left">
                                                    <cc1:ComboBox ID="ddlHangSanXuat" runat="server" Width="290px" Height="22px" onChange="checkChangeHangSanXuat();">
                                                    </cc1:ComboBox>
                                                </div>
                                                <asp:LinkButton ID="lnkbtnTaoMoiHSX" runat="server" OnClick="lnkbtnTaoMoiHSX_Click"
                                                    CausesValidation="False"> Tạo mới </asp:LinkButton></td>
                                        </tr>
                                        <tr runat="server" id="rGiaTriLoHang">
                                            <td align="left" style="width: 15%;" class="caption">
                                                Gi&aacute; tr&#7883; l&ocirc; h&agrave;ng</td>
                                            <td valign="top" align="left" style="width: 30%; text-align: left">
                                                <asp:DropDownList ID="ddlGiaTriLoHang" runat="server" Width="290px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlGiaTriLoHang_SelectedIndexChanged" onChange="checkChangeGiaTriLoHang();"
                                                    Height="22px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rqfGiaTriLoHang" runat="server" ErrorMessage="Nhập giá trị lô hàng"
                                                    ControlToValidate="ddlGiaTriLoHang" Visible="False">*</asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="caption" style="width: 15%;">
                                            </td>
                                            <td align="left" colspan="3" style="text-align: left; width: 30%;" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 15%;" class="caption">
                                                T&agrave;i li&#7879;u</td>
                                            <td align="left" colspan="3" style="text-align: left; width: 30%;" valign="top">
                                                <asp:LinkButton ID="lbtnBAN_CONG_BO" runat="server"></asp:LinkButton>
                                                &nbsp;&nbsp;
                                                <asp:LinkButton ID="lbtnGIAY_TO_TU_CACH_PHAP_NHAN" runat="server"></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="lbtnBAN_TU_DANH_GIA" runat="server"></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="lbtnMAU_DAU_HOP_QUY" runat="server"></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="lbtnTAI_LIEU_KY_THUAT" runat="server"></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="lbtnKET_QUA_DO_KIEM" runat="server"></asp:LinkButton>&nbsp;
                                            </td>
                                        </tr>
                                        <tr id="rChiTieuKyThuatKemTheo" runat="server">
                                            <td align="left" style="width: 15%;" class="caption">
                                                Chỉ tiêu kỹ thuật kèm theo&nbsp;</td>
                                            <td align="left" colspan="3" style="text-align: left; width: 30%;" valign="top">
                                                <asp:FileUpload ID="FileGiayToTuCachPhapNhan" runat="server" Width="52%" />
                                                <asp:LinkButton ID="lnkbtnChiTieuKyThuatKemTheo" runat="server">Chỉ tiêu kỹ thuật kèm theo</asp:LinkButton></td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td align="right" style="height: 21px; text-align: right; width: 100%;" colspan="6">
                <fieldset style="width: 98%">
                    <legend>Thông tin xử lý</legend>
                    <table id="Table1" style="width: 100%">
                        <tr>
                            <td align="left" style="width: 20%;" class="caption">
                                Chuyên viên tiếp nhận</td>
                            <td colspan="2" style="text-align: left">
                                <asp:TextBox ID="txtChuyenVienTiepNhan" runat="server" Height="50px" Rows="2" TextMode="MultiLine"
                                    Width="70%" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 20%;" class="caption">
                                Ý kiến chỉ đạo của lãnh đạo</td>
                            <td colspan="2" style="text-align: left">
                                <asp:TextBox ID="txtYKienLanhDao" runat="server" Height="50px" Rows="2" TextMode="MultiLine"
                                    Width="70%" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr id="trYKienThamDinh" runat="server">
                            <td align="left" style="width: 20%;" class="caption">
                                Ý kiến thẩm định</td>
                            <td colspan="2" style="text-align: left">
                                <asp:TextBox ID="txtYKienThamDinh" runat="server" Height="50px" Rows="2" TextMode="MultiLine"
                                    Width="70%" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td align="right" style="height: 21px; text-align: right; width: 100%;" colspan="6">
                <fieldset style="width: 98%">
                    <legend>Nội dung đánh giá</legend>
                    <table id="Table7" style="width: 100%">
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
                        <tr id="trSoDoKiem" runat ="server">
                            <td align="left" style="width: 20%;" class="caption">
                                Số/Ngày đo kiểm</td>
                            <td colspan="5" style="height: 12px; text-align: left">
                                <asp:TextBox ID="txtSoDoKiem" runat="server" Width="100px" MaxLength="255"></asp:TextBox>/<asp:TextBox
                                    ID="txtNgayDoKiem" runat="server" CausesValidation="True" Width="100px" MaxLength="10"></asp:TextBox><rjs:PopCalendar
                                        ID="calendarFrom" runat="server" Control="txtNgayDoKiem" ScriptsValidators="No Validate"
                                        Separator="/" ShowErrorMessage="False" />
                                (dd/mm/yyyy)<asp:RangeValidator ID="rvCheckDate" runat="server" ErrorMessage="Nhập sai ngày đo kiểm"
                                    MinimumValue="01/01/1900" Type="Date" ControlToValidate="txtNgayDoKiem">*</asp:RangeValidator></td>
                        </tr>
                        <tr id="trCoQuanKiem" runat ="server">
                            <td align="left" style="width: 20%;" class="caption">
                                Cơ quan đo kiểm</td>
                            <td colspan="5" style="height: 13px; text-align: left;">
                                <div style="float: left">
                                    <cc1:ComboBox ID="ddlCoQuanDoLuong" runat="server" Width="400px" OnChange="checkChangeCoQuanDoLuong();">
                                    </cc1:ComboBox>
                                </div>
                                <asp:LinkButton ID="lnkbtnTaoMoiCQDK" runat="server" OnClick="lnkbtnTaoMoiCoQuanDoLuong"
                                    CausesValidation="False"> Tạo mới </asp:LinkButton>
                            </td>
                        </tr>
                        <tr id="trNoiDungDoKiem" runat ="server">
                            <td align="left" style="width: 20%;" class="caption">
                                Nội dung đo kiểm</td>
                            <td colspan="5" style="text-align: left">
                                <asp:TextBox ID="txtNoiDungDoKiem" runat="server" Rows="5" TextMode="MultiLine" Width="70%"></asp:TextBox>&nbsp;</td>
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
                        <tr id="rLephi" visible="false">
                            <td style="width: 20%;" align="left" class="caption">
                                Lệ phí cấp BTN</td>
                            <td colspan="5" style="text-align: left">
                                <asp:TextBox ID="txtLePhi" runat="server" ReadOnly="True" Width="214px" MaxLength="50"
                                    BackColor="#FFFFC0"></asp:TextBox>(VNĐ)</td>
                        </tr>
                        <tr id="rSogiayCB" runat="server">
                            <td style="width: 20%;" align="left" class="caption">
                                Số BTN/CV</td>
                            <td colspan="5" style="text-align: left">
                                <asp:TextBox ID="txtSoBTNCV" runat="server" Width="214px" MaxLength="50" BackColor="#FFFFC0"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rqfSoBTN" runat="server" ControlToValidate="txtSoBTNCV"
                                    ErrorMessage="Nhập số BTN/CV" Visible="False">*</asp:RequiredFieldValidator>
                                <asp:FileUpload ID="fileUpSoCongVan" runat="server" />
                                <asp:HyperLink ID="hlCongVan" runat="server" Visible="False">Công văn</asp:HyperLink>&nbsp;&nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:LinkButton ID="lnkbtnXoaSCV" runat="server" CausesValidation="False" OnClick="lnkbtnXoaSCV_Click"
                                    Visible="False">Xóa</asp:LinkButton></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td align="right" style="height: 21px; text-align: right; width: 100%;" colspan="6">
                <fieldset style="width: 98%">
                    <legend>Nội dung xử lý</legend>
                    <table id="Table3" style="width: 100%">
                        <tr>
                            <td align="left" style="width: 20%;" class="caption">
                                Nội dung đã xử lý</td>
                            <td colspan="2" style="text-align: left; height: 60px;">
                                <asp:TextBox ID="txtNoiDungXuLyTruoc" runat="server" Rows="5" TextMode="MultiLine"
                                    Width="70%" ReadOnly="True" BackColor="#FFFFC0"></asp:TextBox></td>
                        </tr>
                        <tr id="rNoiDungXuLy" runat="server">
                            <td align="left" style="width: 20%;" class="caption">
                                Nội dung xử lý hiện tại</td>
                            <td colspan="2" style="text-align: left">
                                <asp:TextBox ID="txtNoiDungXuLy" runat="server" Rows="4" TextMode="MultiLine" Width="70%"></asp:TextBox></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr style="font-family: Times New Roman">
            <td style="width: 20%">
            </td>
            <td colspan="5" style="width: 80%; height: auto; text-align: left;">
                <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" Width="79px" OnClick="btnCapNhat_Click" />
                <asp:Button ID="btnGuiLanhDao" runat="server" OnClick="btnGuiLanhDao_Click" Text="Gửi lãnh đạo"
                    Width="98px" />
                <asp:Button ID="btnGuiThamDinh" runat="server" Text="Gửi cho thẩm định" Width="157px"
                    OnClick="btnGuiThamDinh_Click" OnClientClick="CheckConfirm('Bạn có muốn cập nhật Nội dung xử lý và Nội dung đánh giá trước khi gửi thẩm định?');" />&nbsp;
                <asp:Button ID="btnInPhieuDanhGia" runat="server" Text="In phiếu đánh giá" Width="150px"
                    OnClick="btnInPhieuDanhGia_Click" OnClientClick="CheckConfirm('Bạn có muốn cập nhật Nội dung xử lý và Nội dung đánh giá trước khi in phiếu đánh giá?');" />
                <asp:Button ID="btnInBanTiepNhan" runat="server" Text="In Bản tiếp nhận" Width="150px"
                    OnClick="btnInBanTiepNhan_Click" />
                <asp:Button ID="btnBoQua" runat="server" Text="Bỏ qua" Width="70px" OnClick="btnBoQua_Click"
                    CausesValidation="False" />
            </td>
        </tr>
    </table>

    <script type="text/javascript">  
        ShowHideLePhi();
        //ShowHideKetLuan(); 
        function getTextValue()
        {
            document.getElementById("<%= hdSoCongVan.ClientID %>").value = document.getElementById("<%= txtSoBTNCV.ClientID %>").value;   
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
