using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Cuc_QLCL.AdapterData;
using Cuc_QLCL.Entities;
using System.Globalization;
using System.Collections.Generic;
using System.Text;

public partial class WebUI_HT_NhatKy_QuanLy : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Xử lý giao diện checkbox của treeview
        trvEvents.Attributes.Add("onclick", "return OnTreeClick(event)");
        if (!IsPostBack)
        {
            BindDataForm();
        }

    }
    /// <summary>
    /// Gán dữ liệu vào control trên form
    /// </summary>
    protected void BindDataForm()
    {
        //gán dữ liệu vào treeview sự kiện log
        //Ham gán dữ liệu Event the dạng tree vào treeview
        ProviderFactory.SysEventProvider.FillEvent(trvEvents);
        trvEvents.CollapseAll();
        chkDanhSachNguoiDung.DataSource = ProviderFactory.SysUserProvider.GetByTrungTamID(mUserInfo.TrungTam.Id);
        chkDanhSachNguoiDung.DataTextField = "UserName";
        chkDanhSachNguoiDung.DataValueField = "ID";
        chkDanhSachNguoiDung.DataBind();
        BindGrid();

    }
    protected void btnXoaNhatKy_Click(object sender, EventArgs e)
    {
        Response.Redirect("HT_NhatKy_Xoa.aspx");
    }

    protected void btnTimKiem_Click(object sender, EventArgs e)
    {
        DateTime dt;
        try
        {
            if (DateTime.TryParse(txtTuNgay.Text, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out dt))
            {
                if (txtDenNgay.Text != string.Empty)
                {
                    if (DateTime.TryParse(txtDenNgay.Text, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out dt))
                        BindGrid();
                }
                else
                {
                    BindGrid();
                }
            }
        }
        catch (Exception ex)
        {
            lblThongBao.Text = ex.ToString();
            lblThongBao.Visible = true;
            return;

        }
    }
    /// <summary>
    /// Hiển thị dữ liệu lên grid
    /// </summary>
    /// <Modified>
    ///     Author      Date         Comments
    ///     giangum     08/04/2009   Tạo mới
    /// </Modified>
    private void BindGrid()
    {
        //Chuỗi đanh sách User tạo log
        string strUserID = string.Empty;
        string strLogTimeFrom = string.Empty;
        string strLogTimeTo = string.Empty;
        string strlstEvent = string.Empty;
        //Lấy danh sách userID
        foreach (ListItem item in chkDanhSachNguoiDung.Items)
        {
            if (item.Selected)
                strUserID += "'" + item.Value + "'" + ",";
        }

        // bỏ dấu ',' cuối cùng
        if (strUserID.Length > 0)
            strUserID = strUserID.Substring(0, strUserID.Length - 1);

        foreach (TreeNode node in trvEvents.CheckedNodes)
        {
            strlstEvent += node.Value + ",";
        }
        // bỏ dấu ',' cuối cùng
        if (strlstEvent.Length > 0)
            strlstEvent = strlstEvent.Substring(0, strlstEvent.Length - 1);

        //convert giá trị ngày tháng để so sánh
        DateTime dtForm = new DateTime(calendarFrom.DateValue.Year, calendarFrom.DateValue.Month, calendarFrom.DateValue.Day, int.Parse(ddlTuGio.SelectedValue), int.Parse(ddlDenPhut.SelectedValue), 00);
        DateTime dtTo = new DateTime(calendarTo.DateValue.Year, calendarTo.DateValue.Month, calendarTo.DateValue.Day, int.Parse(ddlDenGio.SelectedValue), int.Parse(ddlDenPhut.SelectedValue), 00);


        //Lấy time
        strLogTimeFrom = txtTuNgay.Text.Trim() + " " + ddlTuGio.SelectedValue + ":" + ddlTuPhu.SelectedValue + ":00";
        strLogTimeTo = txtDenNgay.Text.Trim() + " " + ddlDenGio.SelectedValue + ":" + ddlDenPhut.SelectedValue + ":00";
        if (txtDenNgay.Text != string.Empty)
        {
            if (dtForm > dtTo)
            {
                lblThongBao.Text = "Thời điểm bắt đầu phải trước thời điểm kết thúc";
                lblThongBao.Visible = true;
                return;
            }
        }
        else strLogTimeTo = string.Empty;

        DataTable dtEvents = ProviderFactory.SysLogProvier.Search(strLogTimeFrom, strLogTimeTo, strUserID, strlstEvent, gvNhatKy.OrderBy, gvNhatKy.PageIndex + 1, gvNhatKy.PageSize);
        gvNhatKy.DataSource = dtEvents;
        if (dtEvents.Rows.Count > 0)
            gvNhatKy.VirtualItemCount = int.Parse(dtEvents.Rows[0]["TongSoBanGhi"].ToString());
        gvNhatKy.DataBind();
        lblThongBao.Visible = false;
    }
    protected void gvNhatKy_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvNhatKy.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void btnGhiRaFile_Click(object sender, EventArgs e)
    {
        DataTable dt = getDataTableToWrite();
        if (dt.Rows.Count > 0)
        {
            //Chuyển dữ liệu dạng bảng sang chuỗi
            string str = AppendDataTableToStringBuilder(dt);
            WriteToTextFile(str);
        }
    }

    private void WriteToTextFile(string str)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=*.txt");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.text";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        Response.Write(str);
        Response.End();
    }

    private DataTable getDataTableToWrite()
    {
        //Chuỗi đanh sách User tạo log
        string strUserID = string.Empty;
        string strLogTimeFrom = string.Empty;
        string strLogTimeTo = string.Empty;
        string strlstEvent = string.Empty;
        //Lấy danh sách userID
        foreach (ListItem item in chkDanhSachNguoiDung.Items)
        {
            if (item.Selected)
                strUserID += "'" + item.Value + "'" + ",";
        }

        // bỏ dấu ',' cuối cùng
        if (strUserID.Length > 0)
            strUserID = strUserID.Substring(0, strUserID.Length - 1);

        foreach (TreeNode node in trvEvents.CheckedNodes)
        {
            strlstEvent += node.Value + ",";
        }
        // bỏ dấu ',' cuối cùng
        if (strlstEvent.Length > 0)
            strlstEvent = strlstEvent.Substring(0, strlstEvent.Length - 1);

        //convert giá trị ngày tháng để so sánh
        DateTime dtForm = new DateTime(calendarFrom.DateValue.Year, calendarFrom.DateValue.Month, calendarFrom.DateValue.Day, int.Parse(ddlTuGio.SelectedValue), int.Parse(ddlDenPhut.SelectedValue), 00);
        DateTime dtTo = new DateTime(calendarTo.DateValue.Year, calendarTo.DateValue.Month, calendarTo.DateValue.Day, int.Parse(ddlDenGio.SelectedValue), int.Parse(ddlDenPhut.SelectedValue), 00);


        //Lấy time
        strLogTimeFrom = txtTuNgay.Text.Trim() + " " + ddlTuGio.SelectedValue + ":" + ddlTuPhu.SelectedValue + ":00";
        strLogTimeTo = txtDenNgay.Text.Trim() + " " + ddlDenGio.SelectedValue + ":" + ddlDenPhut.SelectedValue + ":00";
        if (txtDenNgay.Text != string.Empty)
        {
            if (dtForm > dtTo)
            {
                lblThongBao.Text = "Thời điểm bắt đầu phải trước thời điểm kết thúc";
                lblThongBao.Visible = true;
                return new DataTable();
            }
        }
        else strLogTimeTo = string.Empty;

        DataTable dtEvents = ProviderFactory.SysLogProvier.Search(strLogTimeFrom, strLogTimeTo, strUserID, strlstEvent,gvNhatKy.OrderBy, 0, 0);

        return dtEvents;
    }

    /// Duyệt qua các dòng và cột trong bảng và chuyển thành 1 chuỗi dữ liệu 
    /// Chuỗi này dùng để ghi vào file text
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    /// <Modified>
    /// Person  Date        Comments
    /// giangum 09/04/2009  Tạo mới
    /// </Modified>
    public string AppendDataTableToStringBuilder(DataTable dt)
    {
        StringBuilder str = new StringBuilder();
        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            str.Append(dt.Rows[i]["UserName"].ToString() + " | " + dt.Rows[i]["IP"].ToString() + " | " + dt.Rows[i]["EventTime"].ToString() + " | " + dt.Rows[i]["EventName"].ToString() + " | " + dt.Rows[i]["Detail"].ToString());
            str.Append("\r\n");
        }
        return str.ToString();
    }
    protected void gvNhatKy_Sorting(object sender, GridViewSortEventArgs e)
    {        
        BindGrid();
    }
}
