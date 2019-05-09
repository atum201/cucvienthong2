<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="Delete_HoSo.aspx.cs" Inherits="WebUI_Delete_HoSo" Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--LongHH--%>
    <style type="text/css">
        .dhx_combo_box{
            width:100% !important;
        }
        .dhx_combo_list{
            height:550px;
        }
    </style>
    <%if(objHS != null){ %>
    <table id="Table2" style="width: 100%; padding-left: 8px;">
        <tr>
            <td align="left" colspan="5" style="text-align: left; padding-top: 10px">
                <strong>Xóa Hồ Sơ</strong></td>
        </tr>
        
        <%if(objDonVi != null) {%>
        <tr>
            <td align="left" style="text-align: left; height: 26px;" width="150px">
                Số hồ sơ</td>
            <td style="width: 465px; height: 26px;">
                <%=objHS.SoHoSo %></td>
            <td align="left" style="text-align: left;">
                Đơn vị nộp hồ sơ</td>
            <td colspan="" style="height: auto; text-align: left; width: 465px;">
                <%=objDonVi.TenTiengViet %></td>
        </tr>
        <%} %>
        <tr>
            <td>
                Người nộp hồ sơ (*)</td>
            <td style="text-align: left;">
                <%=objHS.NguoiNopHoSo %></td>
            <td align="left" style="text-align: left">
                Địa chỉ</td>
            <td colspan="" style="text-align: left; width: 465px;">
                <%=objDonVi.DiaChi %>
                </td>
        </tr>
        <tr>
            <td align="left" style="text-align: left;">
                Người tiếp nhận</td>
            <td colspan="" style="text-align: left; width: 465px;">
                <%=Cuc_QLCL.AdapterData.ProviderFactory.SysUserProvider.GetById(objHS.NguoiTiepNhanId).FullName %></td>
            <td>
                Điện thoại liên hệ (*)</td>
            <td>
                <%=objHS.DienThoai %>
            </td>
        </tr>
        <tr>
            <td align="left" style="text-align: left;">
                Ngày tiếp nhận (*)</td>
            <td style="text-align: left; width: 465px;">
                <%=((DateTime)objHS.NgayTiepNhan).ToShortDateString() %></td>
            <td>
                Email liên hệ</td>
            <td style="text-align: left">
                <%=objHS.Email %></td>
        </tr>
        <%--LongHH--%>
        <tr>
            <td style="text-align: left" align="left">
                &nbsp;</td>
            <td style="text-align: left;" colspan="3">
                <asp:Button ID="btnXoa" runat="server" OnClick="btnXoa_Click" Style="text-align: center"
                    Text="Xóa Hồ Sơ" Width="105px" TabIndex="28" />
            </td>
        </tr>
    </table>
    <%}else{ %>
        Mã hồ sơ <%=MaHoSo %> đã bị xóa hoặc không tồn tại
    <%} %>

</asp:Content>
