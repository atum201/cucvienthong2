<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true" CodeFile="DB_DongBoBangTay.aspx.cs" Inherits="WebUI_DB_DongBoBangTy" Title="Đồng bộ bằng tay" Theme="Default"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    <div class="tieude"> ĐỒNG BỘ BẰNG TAY</div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <div align="center">
                        <fieldset style="width: 97%">
                            <legend></legend>
                            <table align="center" border="0" width="100%">
                                <tr>
                                    <td align="right" style="width: 281px; height: 26px">
                                        Chọn đơn vị đồng bộ&nbsp;</td>
                                    <td align="left" colspan="2" style="height: 26px">
                                        <asp:DropDownList ID="ddlTrungTamChungNhan" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                            Width="40%">
                                            <asp:ListItem>Trung t&#226;m chứng nhận</asp:ListItem>
                                            <asp:ListItem>Trung t&#226;m thẩm đinh </asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td align="left" style="height: 26px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 281px; height: 26px">
                                    </td>
                                    <td align="left" colspan="2" style="height: 26px">
                                        <asp:Label ID="Label1" runat="server" Text="Đang thực hiện đồng bộ" Visible="False"></asp:Label></td>
                                    <td align="left" style="height: 26px">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 281px; height: 26px">
                                    </td>
                                    <td align="left" colspan="2" style="height: 26px">
                                        <asp:Button ID="btnDongBo" runat="server" OnClick="btnDongBo_Click" Text="Đồng bộ"
                                            Width="84px" /></td>
                                    <td align="left" style="height: 26px">
                                        &nbsp;</td>
                                </tr>
                            </table>
                            
                        </fieldset>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height: 272px">
                    <div align="center">
                       </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

