// giangum
// required:jquery
// JScript File
var gridCheckBoxPattern="input:not([disabled])[id$='chkCheck']";       
var gridCheckedBoxPattern="input:not([disabled])[id$='chkCheck']:checked";   
var gridCheckHeader="input:not([disabled])[id$='chkCheckAll']";  
function HeaderClick(gridName)
{    
    var isChecked=this.checked;
    var grid = GetControlByName(gridName); 
    var HeaderCheckBox=grid.find(gridCheckHeader);        
    grid.find(gridCheckBoxPattern).each(
        function(){
            this.checked=HeaderCheckBox.attr("checked");
        }
    )  
    
}      
function ChildClick(gridName)
{
    var grid = GetControlByName(gridName);
    iCounter = NumberCheckbox(gridName);
    iCheckedBox=GetCheckBoxIsChecked(gridName);
    var HeaderCheckBox=grid.find(gridCheckHeader);
    HeaderCheckBox.attr("checked",(iCheckedBox>=iCounter));
}

function NumberCheckbox(gridName){    
    var grid = GetControlByName(gridName); 
    var allCheckBox = grid.find(gridCheckBoxPattern);
    return allCheckBox.size();
}

function GetCheckBoxIsChecked(gridName){
    var grid = GetControlByName(gridName); 
    var allCheckBox = grid.find(gridCheckedBoxPattern);
    return allCheckBox.size();
}

function GridIsChecked(gridName){

    return (GetCheckBoxIsChecked(gridName)>0);
}


$(
    function(){
       $(".oneselect").find("tr.rowitem").click(
           function(event)
           {                               
                $(".oneselect").find("tr.rowitem").removeClass("highlight");
                $(this).addClass("highlight");
           }
       )
        $(".textKeyPad").keypad({keypadOnly: false, 
         layout: ['αβγδεζηθ', 'ικλμνξρπ','§στυφχψω','≤≥≠≈©®™∞','µ€£¥±÷×→','Ωⱷ℗Ø₲ⱷₔ₡'], 
         prompt: 'Bảng ký tự đặc biệt',
         showOn: 'button', 
         buttonImageOnly: true, 
         buttonImage: '../Images/keypad.png',duration:'fast'}
         
        ); 
             
    }
)