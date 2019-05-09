<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.master" AutoEventWireup="true"
    CodeFile="HT_NhatKy_QuanLy.aspx.cs" Inherits="WebUI_HT_NhatKy_QuanLy" Title="Quản lý nhật ký hệ thống"
    Theme="Default" %>

<%@ Register Namespace="Fadrian.Web.Control" TagPrefix="cc1" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin: 10px auto 10px 10px;">
        <strong>HỆ THỐNG >> QUẢN LÝ NHẬT KÝ HỆ THỐNG</strong>
    </div>
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="lblThongBao" runat="server" ForeColor="Red"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="center" style="height: 19px">
                <fieldset style="width: 97%">
                    <legend><strong>Thông tin tìm kiếm</strong></legend>
                    <table style="width: 100%" cellpadding="2" cellspacing="2">
                        <tr>
                            <td align="left" class="title_2" valign="top" style="height: 21px;">
                                <strong>Danh sách người dùng</strong></td>
                            <td align="left" style="height: 21px;">
                                <strong>Danh sách sự kiện</strong></td>
                        </tr>
                        <tr>
                            <td align="left" style="height: 110px; width: 470px" valign="top">
                                <div style="overflow-y: auto; width: 100%; height: 110px; border: solid 1px gray;
                                    background: WhiteSmoke">
                                    <asp:CheckBoxList ID="chkDanhSachNguoiDung" runat="server" BorderStyle="None" Width="98%">
                                        <asp:ListItem>TuanVM</asp:ListItem>
                                        <asp:ListItem>TruongTV</asp:ListItem>
                                        <asp:ListItem>DucLV</asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>
                                <br />
                                <table border="0" width="100%">
                                    <tr>
                                        <td align="right" valign="middle" style="width: 90px">
                                            Từ thời điểm</td>
                                        <td align="left" style="height: 29px" valign="top" width="45">
                                            <asp:DropDownList ID="ddlTuGio" runat="server" Width="45px">
                                                <asp:ListItem>00</asp:ListItem>
                                                <asp:ListItem>01</asp:ListItem>
                                                <asp:ListItem>02</asp:ListItem>
                                                <asp:ListItem>03</asp:ListItem>
                                                <asp:ListItem>04</asp:ListItem>
                                                <asp:ListItem>05</asp:ListItem>
                                                <asp:ListItem>06</asp:ListItem>
                                                <asp:ListItem>07</asp:ListItem>
                                                <asp:ListItem>08</asp:ListItem>
                                                <asp:ListItem>09</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                                <asp:ListItem>13</asp:ListItem>
                                                <asp:ListItem>14</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>16</asp:ListItem>
                                                <asp:ListItem>17</asp:ListItem>
                                                <asp:ListItem>18</asp:ListItem>
                                                <asp:ListItem>19</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>21</asp:ListItem>
                                                <asp:ListItem>22</asp:ListItem>
                                                <asp:ListItem>23</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td align="left" style="height: 29px" valign="middle" width="40">
                                            Giờ</td>
                                        <td align="right" style="width: 48px; height: 29px" valign="top">
                                            <asp:DropDownList ID="ddlTuPhu" runat="server" Width="45px">
                                                <asp:ListItem>00</asp:ListItem>
                                                <asp:ListItem>01</asp:ListItem>
                                                <asp:ListItem>02</asp:ListItem>
                                                <asp:ListItem>03</asp:ListItem>
                                                <asp:ListItem>04</asp:ListItem>
                                                <asp:ListItem>05</asp:ListItem>
                                                <asp:ListItem>06</asp:ListItem>
                                                <asp:ListItem>07</asp:ListItem>
                                                <asp:ListItem>08</asp:ListItem>
                                                <asp:ListItem>09</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                                <asp:ListItem>13</asp:ListItem>
                                                <asp:ListItem>14</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>16</asp:ListItem>
                                                <asp:ListItem>17</asp:ListItem>
                                                <asp:ListItem>18</asp:ListItem>
                                                <asp:ListItem>19</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>21</asp:ListItem>
                                                <asp:ListItem>22</asp:ListItem>
                                                <asp:ListItem>23</asp:ListItem>
                                                <asp:ListItem>24</asp:ListItem>
                                                <asp:ListItem>25</asp:ListItem>
                                                <asp:ListItem>26</asp:ListItem>
                                                <asp:ListItem>27</asp:ListItem>
                                                <asp:ListItem>28</asp:ListItem>
                                                <asp:ListItem>29</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>31</asp:ListItem>
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td align="left" style="height: 29px; width: 32px;" valign="middle">
                                            Phút</td>
                                        <td align="right" style="width: 36px; height: 29px" valign="middle">
                                            Ngày</td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtTuNgay" runat="server" CausesValidation="True" Width="100px">01/01/2009</asp:TextBox>&nbsp;<rjs:PopCalendar
                                                ID="calendarFrom" runat="server" Control="txtTuNgay" ScriptsValidators="No Validate"
                                                Separator="/" ShowErrorMessage="False" />
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtTuNgay"
                                                ErrorMessage="Nhập định dạng ngày tháng chưa đúng" Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="middle" style="width: 90px">
                                            Đến thời điểm</td>
                                        <td align="left" style="height: 28px" valign="top">
                                            <asp:DropDownList ID="ddlDenGio" runat="server" Width="45px">
                                                <asp:ListItem>00</asp:ListItem>
                                                <asp:ListItem>01</asp:ListItem>
                                                <asp:ListItem>02</asp:ListItem>
                                                <asp:ListItem>03</asp:ListItem>
                                                <asp:ListItem>04</asp:ListItem>
                                                <asp:ListItem>05</asp:ListItem>
                                                <asp:ListItem>06</asp:ListItem>
                                                <asp:ListItem>07</asp:ListItem>
                                                <asp:ListItem>08</asp:ListItem>
                                                <asp:ListItem>09</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                                <asp:ListItem>13</asp:ListItem>
                                                <asp:ListItem>14</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>16</asp:ListItem>
                                                <asp:ListItem>17</asp:ListItem>
                                                <asp:ListItem>18</asp:ListItem>
                                                <asp:ListItem>19</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>21</asp:ListItem>
                                                <asp:ListItem>22</asp:ListItem>
                                                <asp:ListItem>23</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td align="left" style="height: 28px" valign="middle">
                                            Giờ</td>
                                        <td align="right" style="width: 48px; height: 28px" valign="top">
                                            <asp:DropDownList ID="ddlDenPhut" runat="server" Width="45px">
                                                <asp:ListItem>00</asp:ListItem>
                                                <asp:ListItem>01</asp:ListItem>
                                                <asp:ListItem>02</asp:ListItem>
                                                <asp:ListItem>03</asp:ListItem>
                                                <asp:ListItem>04</asp:ListItem>
                                                <asp:ListItem>05</asp:ListItem>
                                                <asp:ListItem>06</asp:ListItem>
                                                <asp:ListItem>07</asp:ListItem>
                                                <asp:ListItem>08</asp:ListItem>
                                                <asp:ListItem>09</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                                <asp:ListItem>13</asp:ListItem>
                                                <asp:ListItem>14</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>16</asp:ListItem>
                                                <asp:ListItem>17</asp:ListItem>
                                                <asp:ListItem>18</asp:ListItem>
                                                <asp:ListItem>19</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>21</asp:ListItem>
                                                <asp:ListItem>22</asp:ListItem>
                                                <asp:ListItem>23</asp:ListItem>
                                                <asp:ListItem>24</asp:ListItem>
                                                <asp:ListItem>25</asp:ListItem>
                                                <asp:ListItem>26</asp:ListItem>
                                                <asp:ListItem>27</asp:ListItem>
                                                <asp:ListItem>28</asp:ListItem>
                                                <asp:ListItem>29</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>31</asp:ListItem>
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td align="left" style="height: 28px; width: 32px;" valign="middle">
                                            Phút</td>
                                        <td align="right" style="width: 36px; height: 28px" valign="middle">
                                            Ngày</td>
                                        <td align="left" style="height: 28px" valign="top">
                                            <asp:TextBox ID="txtDenNgay" runat="server" Width="100px"></asp:TextBox>&nbsp;<rjs:PopCalendar
                                                ID="calendarTo" runat="server" Control="txtDenNgay" ScriptsValidators="No Validate"
                                                Separator="/" ShowErrorMessage="False" />
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtDenNgay"
                                                ErrorMessage="Ngày tháng không đúng định dạng" Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator></td>
                                    </tr>
                                </table>
                            </td>
                            <td align="left" style="height: 110px;" valign="top">
                                <div style="width: 97%; overflow: auto; border: #cccccc 1px solid; height: 190px;">
                                    <asp:TreeView ID="trvEvents" runat="server" ShowCheckBoxes="All" SkinID="TreeViewCheckBox">
                                        <Nodes>
                                            <asp:TreeNode Text="Nghiệp vụ" Value="Nghiệp vụ">
                                                <asp:TreeNode Text="Chứng nhận" Value="Nh&#243;m kinh tế x&#227; hội">
                                                    <asp:TreeNode Text="Ph&#226;n c&#244;ng xử l&#253;" Value="Ph&#226;n c&#244;ng xử l&#253;">
                                                    </asp:TreeNode>
                                                    <asp:TreeNode Text="Xử l&#253; hồ sơ" Value="Kinh tế"></asp:TreeNode>
                                                    <asp:TreeNode Text="Thẩm Định hồ sơ" Value="V&#249;ng l&#227;nh thổ"></asp:TreeNode>
                                                    <asp:TreeNode Text="Ph&#234; duyệt" Value="Ph&#234; duyệt"></asp:TreeNode>
                                                </asp:TreeNode>
                                                <asp:TreeNode Text="C&#244;ng bố" Value="C&#244;ng bố"></asp:TreeNode>
                                                <asp:TreeNode Text="Cấp ph&#233;p nhập khẩu" Value="Cấp ph&#233;p nhập khẩu"></asp:TreeNode>
                                            </asp:TreeNode>
                                        </Nodes>
                                    </asp:TreeView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                &nbsp;<asp:Button ID="btnTimKiem" runat="server" Text="Lọc" Width="69px" OnClick="btnTimKiem_Click" /></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td align="center" style="height: 19px">
                <fieldset style="width: 97%">
                    <legend><strong>Nhật ký hệ thống </strong></legend>
                    <cc1:PagingGridView ID="gvNhatKy" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        HorizontalAlign="Center" PageSize="15" VirtualItemCount="-1" Width="100%" CellPadding="4"
                        ForeColor="#333333" GridLines="None" OnPageIndexChanging="gvNhatKy_PageIndexChanging"
                        EmptyDataText="Không tìm thấy bản ghi nhật ký thỏa mãn" AllowMultiColumnSorting="true"
                        SortDescImageUrl="~/Images/sortdescending.gif" SortAscImageUrl="~/Images/sortascending.gif"
                        OnSorting="gvNhatKy_Sorting">
                        <Columns>
                            <asp:BoundField DataField="UserName" HeaderText="Người d&#249;ng" SortExpression="UserName">
                                <headerstyle horizontalalign="Left" />
                                <itemstyle horizontalalign="Left" width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IP" HeaderText="IP" SortExpression="IP">
                                <headerstyle horizontalalign="Left" />
                                <itemstyle horizontalalign="Left" width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EventTime" HeaderText="Thời gian" HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                SortExpression="EventTime">
                                <headerstyle horizontalalign="Left" />
                                <itemstyle horizontalalign="Left" width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EventName" HeaderText="Sự kiện" SortExpression="EventName">
                                <headerstyle horizontalalign="Left" />
                                <itemstyle horizontalalign="Left" width="20%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Detail" HeaderText="Chi tiết" SortExpression="Detail">
                                <headerstyle horizontalalign="Left" />
                                <itemstyle width="34%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="STT" HeaderText="STT" Visible="False">
                                <itemstyle width="4%" />
                            </asp:BoundField>
                        </Columns>
                        <PagerStyle CssClass="pagerSC" HorizontalAlign="Center" BackColor="#2461BF" ForeColor="White" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                        <PagerSettings Mode="NumericFirstLast" />
                    </cc1:PagingGridView>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td align="right" style="border-top: black 1px solid; padding-top: 4px; text-align: left">
                <asp:Button ID="btnGhiRaFile" runat="server" Text="Ghi ra file" Width="80px" OnClick="btnGhiRaFile_Click" /><asp:Button
                    ID="btnXoaNhatKy" runat="server" CausesValidation="False" PostBackUrl="" Text="Xóa nhật ký"
                    Width="100px" OnClick="btnXoaNhatKy_Click" /><br />
            </td>
        </tr>
    </table>
</asp:Content>
