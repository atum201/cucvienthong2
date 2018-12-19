// Script check trên treeview
// Edit by LamDS

// Đặt trạng thái check các ô liên quan khi check một ô trên Treeview
// Khi check 1 ô, mọi ô cha sẽ check theo, mọi ô con sẽ check theo
// Khi bỏ check 1 ô, mọi ô con sẽ bỏ check theo
function OnTreeViewCheckBoxClick_A(evt)
{ 
    var src = window.event != window.undefined ? window.event.srcElement : evt.target;
    // Nếu không phải check vào checkbox thì thoát
    var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
     if (!isChkBoxClick) 
     {
        var src = src.previousSibling;
        if (src){
            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
            if (!isChkBoxClick)
                return true;
        }
     }
     var parentTable = TreeViewGetParentByTagName("table", src);
     // Lấy nút con của nút hiện tại
     var nxtSibling = parentTable.nextSibling;
     // Kiểm tra tồn tại nút con
      if(nxtSibling && nxtSibling.nodeType == 1 && nxtSibling.tagName.toLowerCase() == "div")
     {
        // Nếu check/uncheck các nút con
        TreeViewCheckUncheckChildren(parentTable.nextSibling, src.checked);
     }     
    //nếu check nút con thì check nút cha
    if (src.checked)
        TreeViewCheckUncheckParents(src, src.checked);
     return true;
}

// Đặt trạng thái check các ô liên quan khi check một ô trên Treeview
// Khi check 1 ô, mọi ô con sẽ check theo
// Khi bỏ check 1 ô, mọi ô cha sẽ bỏ check theo, mọi ô con sẽ bỏ check theo
function OnTreeViewCheckBoxClick_B(evt)
{ 
    var src = window.event != window.undefined ? window.event.srcElement : evt.target;
    // Nếu không phải check vào checkbox thì thoát
    var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
     if (!isChkBoxClick) 
     {
        var src = src.previousSibling;
        if (src){
            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
            if (!isChkBoxClick)
                return true;
        }
     }
     var parentTable = TreeViewGetParentByTagName("table", src);
     // Lấy nút con của nút hiện tại
     var nxtSibling = parentTable.nextSibling;
     // Kiểm tra tồn tại nút con
      if(nxtSibling && nxtSibling.nodeType == 1 && nxtSibling.tagName.toLowerCase() == "div")
     {
        // Check/uncheck tất cả các nút con
        TreeViewCheckUncheckChildren(parentTable.nextSibling, src.checked);
     }     
    // Nếu bỏ check nút con, bỏ check các nút cha
    if (!src.checked) {
        TreeViewCheckUncheckParents(src, src.checked);
    }
     return true;
}

// Check/uncheck cho các nút thuộc childContainer
function TreeViewCheckUncheckChildren(childContainer, check)
{
    // Lấy các nút con
    var childChkBoxes = childContainer.getElementsByTagName("input");
    // Check/Uncheck các nút con
    for(var i=0;i<childChkBoxes.length;i++)
    {
        childChkBoxes[i].checked = check;
    }
}

//Check/uncheck mọi nút cha của srcChild
function TreeViewCheckUncheckParents(srcChild, check)
{ 
    // Lấy tất cả các nút cha
    var allParents = new Array();
    var countParent = 0;
    while (srcChild) {
        // Lấy nút cha
        var parentDiv = TreeViewGetParentByTagName("div", srcChild);
        var parentNodeTable = parentDiv.previousSibling;
        if (!parentNodeTable)
            break;     
        var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
        if(inpElemsInParentTable.length <= 0)
            break;
        // Thêm nút cha vào bảng
        var srcChild = inpElemsInParentTable[0];
        allParents[countParent] = srcChild;
        countParent++;
    }
    // Check/uncheck mọi nút cha
    for (var i = 0; i < countParent; i++) 
    {
        allParents[i].checked = check;
    }
}

// Kiểm tra mọi nút ngang hàng đều check/uncheck = checkstatus
function TreeViewAreAllSiblingsCheckedUnchecked(chkBox, checkstatus)
{
    var parentDiv = TreeViewGetParentByTagName("div", chkBox);
    // Duyệt qua mọi nút ngang hàng
    // Kiểm tra tình trạng check của mỗi nút
    for(var i=0;i<parentDiv.childNodes.length;i++)
    {
        if(parentDiv.childNodes[i].nodeType == 1 && parentDiv.childNodes[i].tagName.toLowerCase() == "table")
        {
            var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];

            if (prevChkBox && prevChkBox.checked != checkstatus)
                return false;
        }
    }
    return true;
}

//Lấy đối tượng chứa childElementObj thông qua tag name
function TreeViewGetParentByTagName(parentTagName, childElementObj)
{
    var parent = childElementObj.parentNode;
    while(parent.tagName.toLowerCase() != parentTagName.toLowerCase())
    {
        parent = parent.parentNode;
    }
    return parent;
}