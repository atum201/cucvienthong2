// JScript File
var popUpWin = null;
var popCenterWin = null;

function popUpLogin(URL,name,w,h){ 

	myW = 'width=' + w + ',';
	myH = 'height=' + h + ',';
	
	params = 'toolbars=1, scrollbars=0, location=0, statusbars=0, menubars=1, resizable=1,';
		

	popUpWin = window.open(URL,name,myW + myH + params); 
	popUpWin.focus();	
}

function popUp(URL,name,w,h){ 

	myW = 'width=' + w + ',';
	myH = 'height=' + h + ',';
	
	params = 'toolbars=1, scrollbars=1, location=0, statusbars=0, menubars=1, resize=0,';
		

	popUpWin = window.open(URL,name,myW + myH + params); 
	popUpWin.focus();
	//return false;
}

function parent_disable() {    
    if(popCenterWin && !popCenterWin.closed)
    {        
        popCenterWin.focus();
    }
}

function popModalDialog(URL,name,w,h)
{
    //IE
    if(window.showModalDialog)
    {       
        myW = "dialogWidth:" + w + "px;";
	    myH = "dialogHeight:" + h + "px";
	    if (URL.indexOf("?") >=0)
	        URL = URL + "&modal=true";
	    else
	        URL = URL + "?modal=true"; 
	    var returnValue = window.showModalDialog(URL ,name,myW + myH);
	    //Neu goi refresh parrent window
	    if (returnValue =='refresh') window.location.href=window.location.href; 	    
	}else //Firefox va cac trinh duyet khac
	{
	    myW = 'width=' + w + ',';
	    myH = 'height=' + h + ',';
	    
	    params = 'toolbars=1, scrollbars=1, location=0, statusbars=0, menubars=1, resizable=1, modal=yes';
	    window.open(URL,name,myW + myH + params); 
	}
}
function closeModalDialog(RefreshParentPage)
{   
    //Firefox va cac trình duyet khac
    if(window.opener)
    {            
        if (RefreshParentPage==true) window.opener.location.href=window.opener.location.href;
    }else //IE
    {   
        if (RefreshParentPage==true) 
            window.returnValue = "refresh";
        else
            window.returnValue = "norefresh";
    }
    self.close();
}
function popCenter(URL,name,w,h) {	
	l = (screen.width - w) / 2 ;
	t = (screen.height - h) / 2;
	
	params = 'toolbars=1, scrollbars=1, location=0, statusbars=1, menubars=1, resize=0,';
	popCenterWin = window.open(URL, name, params + 'width=' + w + ', height=' + h + ', left=' + l + ', top=' + t);
	popCenterWin.focus();
	return false;
}

//Creater: Tuanhv
function xreplace(inputString,fromString,toString){
	var temp = inputString;
	var i = temp.indexOf(fromString);
while(i > -1){
	temp = temp.replace(fromString, toString);
	i = temp.indexOf(fromString, i + toString.length + 1);
}
	return temp;
}
function checkTextAreaMaxLength(e,el) {
    switch(e.keyCode) {
        case 37: // left
            return true;
        case 38: // up
            return true;
        case 39: // right
            return true;
        case 40: // down
            return true;
        case 8: // backspace
            return true;
        case 46: // delete
            return true;
        case 27: // escape
            el.value='';
            return true;
        }
   return (el.value.length<el.getAttribute("TAMaxLength"));
}
function ismaxlength(obj){
    var mlength=obj.getAttribute? parseInt(obj.getAttribute("TAMaxLength")) : ""
    if (obj.getAttribute && obj.value.length>mlength)
    obj.value=obj.value.substring(0,mlength)
}

 function GetControlByName(id)
        {
         var strID = document.getElementById("hidIdPrefix").value + id;
         var obj  = document.getElementById(strID);
         return obj;
        }

 
    // Bắt độ dài của text box control phải nhỏ hơn hoặc bằng Maxlength
    function checkLength(NameofControl, MaxLength){
        var txtDescription=document.getElementById(NameofControl);                
        
        if (txtDescription.value.length > MaxLength){
            txtDescription.value = txtDescription.value.substring(0, MaxLength);
            return false;
        }
        return true;
    }    
    
    var beforeIndex=0;
    function checkName(txtControlName, lblContrlName, controlSelect){
        var txtControl=document.getElementById(txtControlName);
        var lblMes = document.getElementById(lblContrlName);   
        
        var white = true; 
        for (var i = 0; i < txtControl.value.length; i++)
        {
            if (txtControl.value[i] != ' ')
                white = false;
        }
        if (white)    
        {  
            controlSelect.selectedIndex=beforeIndex;
            lblMes.innerHTML = 'Bạn phải nhập tên khái niệm';
            return false;
        }
        lblMes.Visible = false;
        return true;
    }
    
    // Gán giá trị của dropdownlist cho hiddenfield khi có sự kiện thay đổi giá trị của dropdownlist
    function SetValue(hidName, ddlName)
    {
        var ddlControl = document.getElementById(ddlName);
        var hidField = document.getElementById(hidName);
        hidField.value = ddlControl.value;    
        return true;
    }
    
    function OpenBaoCao(obj,Url){            
        obj.style.color='brown';        
        popCenterBaoCao(Url,'BaoCao',450,300);    
    }
    
    function popCenterBaoCao(URL,name,w,h) {	
	    l = (screen.width - w) / 2 ;
	    t = (screen.height - h-100) / 2;
    	
	    params = 'toolbars=1, scrollbars=1, location=0, statusbars=0, menubars=1, resizable=1,';
	    popCenterWin = window.open(URL, name, params + 'width=' + w + ', height=' + h + ', left=' + l + ', top=' + t);
	    popCenterWin.focus();
	    return false;
    }
    
    
      function getNextElement(node) {
	    if(node.nodeType == 1){
		    return node;
		    }
	    if(node.nextSibling) {
		    return getNextElement(node.nextSibling)
		    }
	    return null;
    }
   // lay control theo name
   // yeu cau fai co jquery
   function GetControlByName(Name){
        var strId= "*[id*='"+Name+"']";
        return $(strId);
   }
   ///quannm in phieu tiep nhan ho so
   function PrintReport(locationUrl, strLoaiBaoCao,HoSoID) 
    { 
        var ans; 
        ans = window.confirm('Bạn có muốn in phiếu tiếp nhận không?'); 
        if (ans == true) { 
            popCenterBaoCao('../ReportForm/DieuKienInBaoCao.aspx?HoSoID='+HoSoID+'&LoaiBaoCao='+strLoaiBaoCao,'BaoCao',400,300);
        } 
        else { 
            location.href = locationUrl;
        }         
    }
    function formatCurrency(num) {
        num = num.toString().replace(/\$|\,/g,'');
        if(isNaN(num))
        num = "0";
        sign = (num == (num = Math.abs(num)));
        num = Math.floor(num*100+0.50000000001);
        cents = num%100;
        num = Math.floor(num/100).toString();
        if(cents<10)
        cents = "0" + cents;
        for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
        num = num.substring(0,num.length-(4*i+3))+'.'+
        num.substring(num.length-(4*i+3));
        return (((sign)?'':'-') + num);
    }

    //$(document).ready(function () {
    //    $("select").attr('onfocus', 'this.size=19');
    //    $("select").attr('onblur', 'this.size=1');
    //    $("select").attr('onchange', 'this.size=1; this.blur();');
    //})