<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="DB_DongBoTuFile.aspx.cs" Inherits="WebUI_DB_DongBoTuFile" Title="Đồng bộ từ file"
    Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin: 10px auto 10px 10px;">
        <strong>ĐỒNG BỘ >> ĐỒNG BỘ TỪ FILE</strong>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="height: 133px">
                <fieldset style="width: 97%">
                    <legend style="width: 101px" title="Đồng bộ từ file">Đồng bộ từ file</legend>
                    <table align="center" border="0" width="100%">
                        <tr>
                            <td align="right" style="width: 281px; height: 26px; text-align: right">
                                Chọn đơn vị đồng bộ&nbsp;</td>
                            <td align="left" colspan="2" style="height: 26px">
                                <asp:DropDownList ID="ddlTrungTamChungNhan" runat="server" AutoPostBack="True" Width="100%">
                                </asp:DropDownList></td>
                            <td align="left" style="height: 26px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 281px; height: 26px; text-align: right">
                                Chọn file&nbsp;</td>
                            <td align="left" colspan="2" style="height: 26px">
                                &nbsp;<asp:FileUpload ID="fileUploadDuLieuDongBo" runat="server" Width="254px" /></td>
                            <td align="left" style="height: 26px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 281px; height: 26px; text-align: right">
                            </td>
                            <td align="left" colspan="2" style="height: 26px">
                                <asp:Label ID="lblKetQuaDongBo" runat="server" Visible="False"></asp:Label></td>
                            <td align="left" style="height: 26px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 281px; height: 21px">
                                &nbsp;</td>
                            <td style="width: 182px; height: 21px; text-align: left">
                                <asp:Button ID="btnDongBo" runat="server" Text="Đồng bộ" Width="84px" OnClick="btnDongBo_Click"  /></td>
                            <td style="width: 78px; height: 21px">
                                &nbsp;</td>
                            <td style="height: 21px">
                                &nbsp;</td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset style="width: 97%">
                    <legend style="width: 101px" title="Đồng bộ từ file">Xuất dữ liệu đồng bộ ra file</legend>
                    <table align="center" border="0" width="100%">
                        <tr>
                            <td align="right" style="width: 281px; height: 26px; text-align: right">
                                Chọn đơn vị đồng bộ&nbsp;</td>
                            <td align="left" colspan="2" style="height: 26px">
                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Width="100%">
                                    <asp:ListItem Value="TTCN">Trung t&#226;m chứng nhận</asp:ListItem>
                                    <asp:ListItem Value="TT2">Trung t&#226;m kiểm định v&#224; chứng nhận 2</asp:ListItem>
                                    <asp:ListItem Value="TT3">Trung t&#226;m kiểm định v&#224; chứng nhận 3</asp:ListItem>
                                </asp:DropDownList></td>
                            <td align="left" style="height: 26px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 281px; height: 21px">
                                &nbsp;</td>
                            <td style="width: 182px; height: 21px; text-align: left">
                                <asp:Button ID="btnXuatFile" runat="server" Text="Xuất file" Width="84px" OnClick="btnXuatFile_Click" /></td>
                            <td style="width: 78px; height: 21px">
                                &nbsp;</td>
                            <td style="height: 21px">
                                &nbsp;</td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
