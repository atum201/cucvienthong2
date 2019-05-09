<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="CN_XuLyHoSo_DanhGia.aspx.cs" Inherits="WebUI_CN_XuLyHoSo_DanhGia"%>

<%@ Register Assembly="Cuc_QLCL.AdapterData" Namespace="TinhVan.WebUI.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <%--LongHH--%>
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
           var controlCSSX = document.getElementById("<%= rblKetQuaKiemTra.ClientID %>");
           var controlQHTS = document.getElementById("<%= rblQuyHoachTanSo.ClientID %>");
                   
           var inputsHopLe = controlHopLe.getElementsByTagName("input");  
           var inputsDayDu = controlDayDu.getElementsByTagName("input"); 
           var inputsCSSX;
           if(controlCSSX!=null)
            inputsCSSX= controlCSSX.getElementsByTagName("input");  
           var inputsQHTS;
           if(controlQHTS!=null)
            inputsQHTS= controlQHTS.getElementsByTagName("input"); 
           
           var TanSoValue='false';
           if(document.getElementById("<%= hdTanSo.ClientID %>")!=null)
            TanSoValue=document.getElementById("<%= hdTanSo.ClientID %>").value;
           var NguonGocValue='false';
           if(document.getElementById("<%= hdNguonGoc.ClientID %>")!=null)
            NguonGocValue=document.getElementById("<%= hdNguonGoc.ClientID %>").value;
           
            var blHopLe=false;
            var blDayDu=false;
            var blCSSX=false;
            var blQHTS=false;
            
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
            if(NguonGocValue=='false'){
                        blCSSX=true;
                    }
                    else{
                        for(var i=0;i<inputsCSSX.length;i++)
                        {
                            if(inputsCSSX[i].checked && inputsCSSX[i].value=='1')
                            {   
                                  blCSSX=true;                 
                                  break;
                            }
                        }
                    }
                    if(TanSoValue=='false'){
                        blQHTS=true;
                    }
                    else{
                        for(var i=0;i<inputsQHTS.length;i++)
                        {
                            if(inputsQHTS[i].checked && inputsQHTS[i].value=='1')
                            {   
                                  blQHTS=true;                 
                                  break;
                            }
                        }
                    }
            if(blDayDu && blHopLe && blCSSX && blQHTS )
            {  
                 HideRadioButton(4);
                 ShowRadioButton(2);             
                ShowHideThoiHan();             
                
                              
            }
            else
            {
                HideRadioButton(2);
                ShowRadioButton(4);                 
                document.getElementById("rThoiHan").style.display='none';
                document.getElementById("rLephi").style.display='none';
//                if(document.getElementById("<%= btnGuiThamDinh.ClientID %>")!=null)
//                    document.getElementById("<%= btnGuiThamDinh.ClientID %>").style.display='none';
                             
            }            
        }
        
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
                        {
                            blShowFileUpLoad=true;
                            break;
                        }
                    }                    
                } 
                if(strCheckValue=='1')
                {
                    document.getElementById("rThoiHan").style.display='';
                    document.getElementById("rLephi").style.display='';
                }
                else
                {
                    document.getElementById("rThoiHan").style.display='none';
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
                        
                        document.getElementById("<%= fileUpSoCongVan.ClientID %>").style.display=''; 
                        
                        //  Cho nhap so cong van
                        document.getElementById("<%= txtSoGCNCV.ClientID %>").value='';
                        document.getElementById("<%= txtSoGCNCV.ClientID %>").readOnly = false;
                        document.getElementById("<%= txtSoGCNCV.ClientID %>").style.backgroundColor = '#ffffff';  
                    }
                }
                else
                {
                    if(document.getElementById("<%= fileUpSoCongVan.ClientID %>")!=null)  
                    {
                        if(document.getElementById("<%= hdIsHoSoMoi.ClientID %>").value == '1' && strCheckValue == '1')
                        {
                            document.getElementById("<%= txtSoGCNCV.ClientID %>").value='Số sinh tự động';
                            document.getElementById("<%= txtSoGCNCV.ClientID %>").readOnly = true;
                            document.getElementById("<%= txtSoGCNCV.ClientID %>").style.backgroundColor = '#FFFFC0';  
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
                    if(document.getElementById("<%= hdIsHoSoMoi.ClientID %>").value == '1')
                        document.getElementById("<%= txtSoGCNCV.ClientID %>").value='Số sinh tự động';
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
                    var controlCSSX = document.getElementById("<%= rblKetQuaKiemTra.ClientID %>");
                    var controlQHTS = document.getElementById("<%= rblQuyHoachTanSo.ClientID %>");

                    var inputsHopLe = controlHopLe.getElementsByTagName("input");  
                    var inputsDayDu = controlDayDu.getElementsByTagName("input"); 
                    var inputsCSSX = controlCSSX.getElementsByTagName("input");  
                    var inputsQHTS = controlQHTS.getElementsByTagName("input"); 

                    var TanSoValue='false';
                    if(document.getElementById("<%= hdTanSo.ClientID %>")!=null)
                        TanSoValue= document.getElementById("<%= hdTanSo.ClientID %>").value;
                    var NguonGocValue='false';
                    if(document.getElementById("<%= hdNguonGoc.ClientID %>")!=null)
                        NguonGocValue= document.getElementById("<%= hdNguonGoc.ClientID %>").value;
                   
                    var blHopLe=false;
                    var blDayDu=false;
                    var blCSSX=false;
                    var blQHTS=false;
                    
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
//                    if(NguonGocValue=='false'){
//                        alert('null');
//                        blCSSX=true;
//                    }
//                    else{
                    if(NguonGocValue!='false')
                    {
                        for(var i=0;i<inputsCSSX.length;i++)
                        {
                            if(inputsCSSX[i].checked && inputsCSSX[i].value=='1')
                            {   
                                  blCSSX=true;                 
                                  break;
                            }
                        }
                    }
//                    if(TanSoValue=='false'){
//                        blQHTS=true;
//                    }
//                    else{
                    if(TanSoValue!='false'){
                        for(var i=0;i<inputsQHTS.length;i++)
                        {
                            if(inputsQHTS[i].checked && inputsQHTS[i].value=='1')
                            {   
                                  blQHTS=true;                 
                                  break;
                            }
                        }
                    }
//                    if(blDayDu && blHopLe && (blCSSX || NguonGocValue=='false') && (blQHTS||TanSoValue=='false') )
//                    {  
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
                    //}
                    //LongHH Ẩn phần liên quan đến nút gửi lãnh đạo
                     // Ẩn hiện nút gửi lãnh đạo
                    //if(btnGuiLanhDao != true)
                    //{
                    //    if(strCheckValue !='7' && strCheckValue != '1')
                    //    {
                    //        if(btn.style.display !='none')
                    //        {
                    //            btnGuiLanhDao.style.display = 'none';
                    //        }
                    //        else
                    //        {
                    //            btnGuiLanhDao.style.display = '';
                    //        }
                    //    }
                    //    else
                    //    {
                    //        btnGuiLanhDao.style.display='none';
                    //    }
                    //}
                    // LongHH
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
    <script type="text/javascript">
        //LongHH
        function reheight(p, tc) {
            var l = tc.find("tr").length;
            if (l > 10) {
                tc.css("height", 300);
                p.css("height", 319);
            } else if (l === 0) {
                tc.css("height",  30);
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
    <div>
        <span style="font-family: Arial"><strong>CHỨNG NHẬN&gt;&gt;<asp:HyperLink ID="hlDanhSachHoSo"
            runat="server"></asp:HyperLink></strong><a href="../WebUI/CN_HoSo_QuanLy.aspx?direct=CN_HoSoDen"></a><strong>&gt;&gt;<asp:HyperLink
                ID="hlDanhSachSanPham" runat="server"></asp:HyperLink></strong><strong>&gt;&gt; </strong>
            <asp:Label ID="lblTrangThai" runat="server"></asp:Label></span></div>
    <table id="Table6" style="width: 100%">
        <tr>
            <td align="right" style="height: 21px; text-align: right; width: 100%;" colspan="6">
                <fieldset style="width: 98%">
                    <legend>Thông tin sản phẩm</legend>
                        <table width="100%" cellspacing="0">
                            <tr>
                                <td width="60%" valign="top">
                                    <table width="100%" cellspacing="0">
                                        <tr>
                                            <td align="left" class="caption" style="width: 33%">
                                            </td>
                                            <td align="left" style="width: 66%; text-align: left" valign="top">
                                                <asp:ValidationSummary ID="vsXyLyHoSoDanhGia" runat="server" ShowMessageBox="True"
                                                    ShowSummary="False" />
                                            </td>
                                            <%--<td align="left" style="width: 30%; text-align: left" valign="top">
                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 33%;" class="caption">
                                                <span style="width: 20%; height: 24px; text-align: right"><span>T&ecirc;n s&#7843;n
                                                    ph&#7849;m</span></span></td>
                                            <td valign="top" align="left" style="width: 66%; text-align: left">
                                                <div style="float: left;width:70.2%;margin-right:10px;">
                                                    <cc1:ComboBox ID="ddlTenSanPham" runat="server" AutoPostBack="True" Width="59.5%" BackColor="#FFFFC0" ReadOnly="True" CssClass="input_readonly" Visible="false"
                                                        OnSelectedIndexChanged="ddlTenSanPham_SelectedIndexChanged" onChange="checkChangeSanPham();">
                                                    </cc1:ComboBox>
                                                    <asp:TextBox ID="txtTenSanPham" runat="server" Width="100%" BackColor="#FFFFC0"
                                                    ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                <asp:LinkButton ID="lnkbtnTaoMoiSP" runat="server" OnClick="lnkbtnTaoMoiSP_Click"
                                                    CausesValidation="False"> Tạo mới </asp:LinkButton>
                                            </td>
                                            <%--<td align="left" valign="top" width="44%" style="width: 30%; text-align: left">
                                                &nbsp;Tiêu chuẩn áp dụng</td>--%>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 33%;" class="caption">
                                                <span style="width: 20%; height: 3px; text-align: right">Nh&oacute;m sản phẩm</span></td>
                                            <td valign="top" align="left" style="width: 66%; text-align: left">
                                                <asp:TextBox ID="txtNhomSanPham" runat="server" Width="70%" BackColor="#FFFFC0"
                                                    ReadOnly="True"></asp:TextBox></td>
                                            <%--<td align="left" valign="top" style="width: 30%; text-align: left" rowspan="9">
                                                <asp:Panel ID="Panel1" runat="server" Height="400px" ScrollBars="Both" Width="400px"
                                                    BorderWidth="1px" Wrap="False">
                                                    <asp:CheckBoxList ID="chklstTieuChuan" runat="server" Width="400px" Height="250px" 
                                                        CellPadding="0" CellSpacing="0" OnSelectedIndexChanged="chklstTieuChuan_SelectedIndexChanged">
                                                    </asp:CheckBoxList></asp:Panel>
                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 33%;" class="caption">
                                                K&yacute; hi&#7879;u</td>
                                            <td valign="top" align="left" style="width: 66%; text-align: left">
                                                <asp:TextBox ID="txtKyHieu" runat="server" Width="70%" MaxLength="255" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 33%;" class="caption">
                                                Tr&#7841;ng th&aacute;i</td>
                                            <td valign="top" align="left" style="width: 66%; text-align: left">
                                                <asp:TextBox ID="txtTrangThai" runat="server" Width="70%" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 33%;" class="caption">
                                                H&atilde;ng s&#7843;n xu&#7845;t</td>
                                            <td valign="top" align="left" style="width: 66%; text-align: left">
                                                <div style="float: left; width:70.2%; margin-right:10px">
                                                    <cc1:ComboBox ID="ddlHangSanXuat" runat="server" Width="59.5%" onkeypress="checkChangeHangSanXuat();" CssClass="input_readonly" ReadOnly="True" Visible="false">
                                                    </cc1:ComboBox>
                                                    <asp:TextBox ID="txtHangSanXuat" runat="server" Width="100%" BackColor="#FFFFC0"
                                                    ReadOnly="True"></asp:TextBox>
                                                </div>
                                                <asp:LinkButton ID="lnkbtnTaoMoiHSX" runat="server" OnClick="lnkbtnTaoMoiHSX_Click"
                                                    CausesValidation="False"> Tạo mới </asp:LinkButton></td>
                                        </tr>
                                        <tr id="rGiaTriLoHang" runat="server">
                                            <td align="left" style="width: 33%;" class="caption">
                                                Gi&aacute; tr&#7883; l&ocirc; h&agrave;ng</td>
                                            <td valign="top" align="left" style="width: 66%; text-align: left">
                                                <asp:DropDownList ID="ddlGiaTriLoHang" runat="server" Width="70%" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlGiaTriLoHang_SelectedIndexChanged" onChange="checkChangeGiaTriLoHang();" Height="22px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rqfGiaTriLoHang" runat="server" ErrorMessage="Nhập giá trị lô hàng"
                                                    ControlToValidate="ddlGiaTriLoHang" Visible="False">*</asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="caption" style="width: 33%;">
                                            </td>
                                            <td align="left" colspan="3" style="text-align: left; width: 66%;" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 33%;" class="caption">
                                                T&agrave;i li&#7879;u</td>
                                            <td align="left" colspan="3" style="text-align: left; width: 66%;" valign="top">
                                                <asp:LinkButton ID="lbtnDON_DE_NGHI_CN" runat="server"></asp:LinkButton>
                                                &nbsp;&nbsp;
                                                <asp:LinkButton ID="lbtnGIAY_TO_TU_CACH_PHAP_NHAN" runat="server"></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="lbtnTAI_LIEU_KY_THUAT" runat="server"></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="lbtnKET_QUA_DO_KIEM" runat="server"></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="lbtnQUY_TRINH_SAN_XUAT" runat="server"></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="lbtnQUY_TRINH_CHAT_LUONG" runat="server"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCHUNG_CHI_HE_THONG_QLCL" runat="server"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnTIEU_CHUAN_TU_NGUYEN_AP_DUNG" runat="server"></asp:LinkButton></td>
                                        </tr>
                                        <tr id="rChiTieuKyThuatKemTheo" runat="server">
                                            <td align="left" style="width: 33%;" class="caption">
                                                Chỉ tiêu kỹ thuật kèm theo&nbsp;</td>
                                            <td align="left" colspan="3" style="text-align: left; width: 66%;" valign="top">
                                                <asp:FileUpload ID="FileGiayToTuCachPhapNhan" runat="server" Width="52%" />
                                                <asp:LinkButton ID="lnkbtnChiTieuKyThuatKemTheo" runat="server">Chỉ tiêu kỹ thuật kèm theo</asp:LinkButton></td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 33%;" class="caption">
                                                Ghi chú</td>
                                            <td align="left" colspan="3" style="text-align: left; width: 66%;" valign="top">
                                                <asp:TextBox ID="txtGhiChu" runat="server" TextMode="MultiLine" Rows="3" Width="70%" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="40%">
                                    <table>
                                        <tr>
                                            <td align="left" valign="top" style="width: 100%; text-align: left">
                                                &nbsp;Tiêu chuẩn áp dụng</td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top" style="width: 60%; text-align: left" >
                                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Width="330px"
                                                    BorderWidth="1px" Wrap="False" >
                                                    <asp:CheckBoxList ID="chklstTieuChuan" runat="server" Width="95%"
                                                        CellPadding="0" CellSpacing="0" OnSelectedIndexChanged="chklstTieuChuan_SelectedIndexChanged" CssClass="input_readonly" Enabled="false" >
                                                    </asp:CheckBoxList></asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
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
                        </tr>
                        <tr>
                            <td align="left" style="width: 20%;" class="caption">
                            </td>
                            <td style="width: 247px; text-align: left">
                                <asp:RadioButtonList ID="rblDayDu" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="1">Đầy đủ</asp:ListItem>
                                    <asp:ListItem Value="0">Kh&#244;ng đầy đủ</asp:ListItem>
                                </asp:RadioButtonList></td>
                            
                        </tr>
                        <tr>
                            <td align="left" style="width: 20%;" class="caption">
                                Số/Ngày đo kiểm</td>
                            <td style="height: 12px; text-align: left">
                                <asp:TextBox ID="txtSoDoKiem" runat="server" Width="34.7%" MaxLength="255"></asp:TextBox>/<asp:TextBox
                                    ID="txtNgayDoKiem" runat="server" CausesValidation="True" Width="34.7%" MaxLength="10" CssClass="mr10"></asp:TextBox><rjs:PopCalendar
                                        ID="calendarFrom" runat="server" Control="txtNgayDoKiem" ScriptsValidators="No Validate"
                                        Separator="/" ShowErrorMessage="False" />
                                (dd/mm/yyyy)
                                 
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 20%;" class="caption">
                                
                            </td>
                            <td>
                                <div style="width:100%">
                                    <div style="width:50%">
                                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtSoDoKiem" ID="rvCheckLengthSoDoKiem" 
                                    ValidationExpression = "^[\s\S]{0,100}$" runat="server" ErrorMessage="Nhập tối đa 100 kí tự."></asp:RegularExpressionValidator>
                                    </div>
                                    <div style="width:50%">
                                        <asp:RangeValidator ID="rvCheckDate" runat="server" ErrorMessage="Nhập sai ngày đo kiểm"
                                    MinimumValue="01/01/1900" Type="Date" ControlToValidate="txtNgayDoKiem">*</asp:RangeValidator>
                                    </div>
                                </div>
                                
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 20%;" class="caption">
                                Cơ quan đo kiểm</td>
                            <td style="height: 13px; text-align: left; ">
                                <div style="float: left;width:70.2%;margin-right: 10px;">
                                    <cc1:ComboBox ID="ddlCoQuanDoLuong" runat="server" OnChange="checkChangeCoQuanDoLuong();" Width="99%" AutoPostBack="true" OnSelectedIndexChanged="ddlCoQuanDoLuong_SelectedIndexChanged">
                                    </cc1:ComboBox>
                                </div>
                                <asp:LinkButton ID="lnkbtnTaoMoiCQDK" runat="server" OnClick="lnkbtnTaoMoiCoQuanDoLuong"  Visible="false" CausesValidation="False"> Tạo mới </asp:LinkButton>&nbsp;
                            </td>
                        </tr>
                        <%--LongHH--%>
                        <tr id="trTenTiengAnh" runat ="server">
                            <td align="left" style="width: 20%;" class="caption">
                                Tên tiếng anh CQĐK</td>
                            <td style="height: 13px; text-align: left;">
                                <asp:TextBox ID="txtTenTiengAnhCQDK" runat="server" Width="70%"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr id="trDiaChi" runat ="server">
                            <td align="left" style="width: 20%;" class="caption">
                                Địa chỉ CQĐK</td>
                            <td style="height: 13px; text-align: left;">
                                <asp:TextBox ID="txtDiaChiCQDK" runat="server" Width="70%"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr id="trSoDienThoai" runat ="server">
                            <td align="left" style="width: 20%;" class="caption">
                                Số điện thoại CQĐK</td>
                            <td style="height: 13px; text-align: left;">
                                <asp:TextBox ID="txtSDTCQDK" runat="server" Width="70%"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr id="tr1" runat ="server">
                            <td align="left" style="width: 20%;" class="caption">
                                File đính kèm</td>
                            <td colspan="5" style="height: 13px; text-align: left;">
                                <asp:LinkButton ID="lbtnFileDinhKem" runat="server"></asp:LinkButton>&nbsp;
                            </td>
                        </tr>
                        <%--LongHH--%>
                        <tr>
                            <td align="left" style="width: 20%;" class="caption">
                                Kết quả đo kiểm khác</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtNoiDungDoKiem" runat="server" Rows="5" TextMode="MultiLine" Width="70%"></asp:TextBox>&nbsp;</td>
                        </tr>
                        <tr id="rKTCSSX" runat="server">
                            <td align="left" style="width: 20%;" class="caption">
                                Kết quả kiểm tra CSSX</td>
                            <td style="height: auto; text-align: left">
                                <asp:RadioButtonList ID="rblKetQuaKiemTra" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="1">Đạt</asp:ListItem>
                                    <asp:ListItem Value="0">Kh&#244;ng đạt</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr id="rTanSo" runat="server">
                            <td align="left" style="width: 20%;" class="caption">
                                Quy hoạch tần số</td>
                            <td style="text-align: left">
                                <asp:RadioButtonList ID="rblQuyHoachTanSo" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="1">Ph&#249; hợp</asp:ListItem>
                                    <asp:ListItem Value="0">Kh&#244;ng ph&#249; hợp</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 20%;" class="caption">
                                Nhận xét khác</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtNhanXetKhac" runat="server" TextMode="MultiLine" Width="70%"
                                    Rows="4">
                                </asp:TextBox></td>
                        </tr>
                        <tr id="rKetLuan">
                            <td align="left" style="width: 20%;" class="caption">
                                Kết luận</td>
                            <td style="text-align: left">
                                <asp:RadioButtonList ID="rblKetLuan" runat="server" RepeatDirection="Horizontal"
                                    CssClass="rdKetLuan">
                                    <asp:ListItem Value="2">Kh&#244;ng cấp</asp:ListItem>
                                    <asp:ListItem Value="3">Kh&#244;ng phải chứng nhận</asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True">Cấp GCN</asp:ListItem>
                                    <asp:ListItem Value="4">Hủy sản phẩm</asp:ListItem>
                                    <asp:ListItem Value="7">Kh&#225;c</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rqfKetLuan" runat="server" ControlToValidate="rblKetLuan"
                                    ErrorMessage="Chọn kết luận">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr id="rThoiHan">
                            <td style="width: 20%;" align="left" class="caption">
                                Thời hạn</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtThoiHan" runat="server" BackColor="#FFFFC0" MaxLength="50" ReadOnly="True"
                                    Width="70%"></asp:TextBox></td>
                        </tr>
                        <tr id="rLephi">
                            <td style="width: 20%;" align="left" class="caption">
                                Lệ phí chứng nhận</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtLePhi" runat="server" ReadOnly="True" Width="70%" MaxLength="50"
                                    BackColor="#FFFFC0"></asp:TextBox>(VNĐ)</td>
                        </tr>
                        <tr id="rSogiayCN" runat="server">
                            <td style="width: 20%;" align="left" class="caption">
                                Số GCN/CV</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtSoGCNCV" runat="server" Width="70%" MaxLength="50" BackColor="#FFFFC0"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rqfSoGCN" runat="server" ControlToValidate="txtSoGCNCV"
                                    ErrorMessage="Nhập số GCN/CV" Visible="False">*</asp:RequiredFieldValidator>
                                <asp:FileUpload ID="fileUpSoCongVan" runat="server" />
                                <asp:HyperLink ID="hlCongVan" runat="server" Visible="False">Công văn</asp:HyperLink>&nbsp;&nbsp;
                                &nbsp; &nbsp;
                                <asp:LinkButton ID="lnkbtnXoaSCV" runat="server" CausesValidation="False" OnClick="lnkbtnXoaSCV_Click"
                                    Visible="False">Xóa</asp:LinkButton></td>
                        </tr>
                        <tr id="rNgayCap" runat="server" >
                            <td align="left" class="caption" style="width: 20%; height: 29px;">
                                <asp:Label ID="lblNgayCap" runat="server" Text="Ngày cấp"></asp:Label></td>
                            <td style="text-align: left; height: 29px;">
                                <asp:TextBox ID="txtNgayCap" runat="server" CausesValidation="True" MaxLength="10"
                                    Width="100px"></asp:TextBox><rjs:PopCalendar
                                        ID="pcdNgayCap" runat="server" Control="txtNgayCap" ScriptsValidators="No Validate"
                                        Separator="/" RequiredDateMessage="Bạn phải nhập ngày cấp" ShowMessageBox="True" />
                                <asp:Label ID="lblDinhDangNgay" runat="server" Text="(dd/mm/yyyy)"></asp:Label><asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtNgayCap"
                                    ErrorMessage="Nhập sai ngày cấp" MinimumValue="01/01/1900" Type="Date" MaximumValue="01/01/2099">*</asp:RangeValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNgayCap"
                                    ErrorMessage="Bạn phải nhập ngày cấp">*</asp:RequiredFieldValidator></td>
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
                <asp:Button ID="btnInGiayChungNhan" runat="server" Text="In giấy chứng nhận" Width="150px"
                    OnClick="btnInGiayChungNhan_Click" />
                <asp:Button ID="btnBoQua" runat="server" Text="Bỏ qua" Width="70px" OnClick="btnBoQua_Click"
                    CausesValidation="False" />
            </td>
        </tr>
    </table>

    <script type="text/javascript">  
        ShowHideThoiHan();
        //ShowHideKetLuan();    
    </script>

    
</asp:Content>
