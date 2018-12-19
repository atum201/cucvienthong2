<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MauDauHopQuy.aspx.cs" Inherits="WebUI_MauDauHopQuy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mẫu dấu </title>

    <script type="text/javascript" src="../Js/jquery-1.3.2-vsdoc2.js"></script>

    <style type="text/css">
        .hover_item
        {	       
            border-width:2px; 
            border-style:solid;
	        border-color:#D19275;	
	        	        	       	        
        }                     
    </style>

    <script>
        $(function (){            
            $(".choseImage").click(
              function(){                                     
                    $(".choseImage").removeClass("hover_item");                    
                    $(this).addClass("hover_item");                
              }
            )
        })        
        function SelectItem(){
            var url_image;
            //var imgCtrl=$(".choseImage.hover_item").find("img");
            var imgCtrl=$(".choseImage.hover_item");
            url_image = imgCtrl.attr("src");     
            //alert(url_image);
            window.opener.SetSignImage(url_image);           
            window.close();
            return false;
        }        
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 10px 10px 10px 10px;">
            <table>
                <tr>
                    <td style="width: 79px">
                        Đơn vị
                    </td>
                    <td style="width: 282px">
                        &nbsp;<asp:TextBox ID="txtDonVi" runat="server" BackColor="#FFFFC0" ReadOnly="True"
                            Width="243px"></asp:TextBox></td>
                </tr>
            </table>
            <fieldset style="width: 97%">
                <legend>Danh sách mẫu dấu</legend>
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <asp:DataList ID="dtlstMauDau" runat="server" RepeatColumns="3" SelectedItemStyle-BackColor="controlDark"
                                ItemStyle-HorizontalAlign="Center" CellPadding="5" CellSpacing="2" Width="100%">
                                <ItemTemplate>
                                    <div>
                                        <img class="choseImage" src="../Handler.ashx?MauDauId=<%# Eval("Id")%>" style="width: 100px;
                                            height: 100px;" />
                                        <br />
                                        <%# Eval("TenMauDau")%>
                                    </div>
                                </ItemTemplate>
                                <SelectedItemStyle BackColor="InactiveBorder" />
                                <ItemStyle Height="90px" HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                            </asp:DataList></td>
                    </tr>
                </table>
            </fieldset>
            <table>
                <tr>
                    <td align="right" colspan="2">
                        <input id="btnChon" onclick="SelectItem()" type="button" value="Chọn" />
                        <input id="btnBoqua" onclick="window.close()" type="button" value="Bỏ qua" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
