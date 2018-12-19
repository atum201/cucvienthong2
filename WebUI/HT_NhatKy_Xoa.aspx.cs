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
using System.Globalization;
using Cuc_QLCL.AdapterData;
using CucQLCL.Common;
using Cuc_QLCL.Entities;

public partial class WebUI_HT_NhatKy_Xoa : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// Xóa log theo điều kiện truyền vào
    /// </summary>
    private void DeleteLog()
    {
        string strLogTimeFrom = "";
        string strLogTimeTo = "";
        //Lấy time
        strLogTimeFrom = txtDateFrom.Text.Trim() + " " + ddlHourFrom.SelectedValue + ":" + ddlMinuteFrom.SelectedValue + ":00";
        strLogTimeTo = txtDateTo.Text.Trim() + " " + ddlHourTo.SelectedValue + ":" + ddlMinuteTo.SelectedValue + ":00";
        //Check thời gian
        DateTime dtForm = new DateTime(calendarFrom.DateValue.Year, calendarFrom.DateValue.Month, calendarFrom.DateValue.Day, int.Parse(ddlHourFrom.SelectedValue), int.Parse(ddlMinuteFrom.SelectedValue), 00);
        DateTime dtTo = new DateTime(calendarTo.DateValue.Year, calendarTo.DateValue.Month, calendarTo.DateValue.Day, int.Parse(ddlHourTo.SelectedValue), int.Parse(ddlMinuteTo.SelectedValue), 00);

        if (dtForm > dtTo)
        {
            lblMessage.Text = "Thời điểm bắt đầu phải trước thời điểm kết thúc";
            return;
        }
        try
        {
            ProviderFactory.SysLogProvier.DeleteLog(strLogTimeFrom, strLogTimeTo); 
            ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.HT_NHAT_KY_XOA, "Xóa log trong khoảng thời gian từ " + strLogTimeFrom + " đến " + strLogTimeTo);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Xoá thành công');</script>");
        }
        catch (Exception ex)
        {           
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Xoá không thành công');</script>");
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dt;
            if (DateTime.TryParse(txtDateFrom.Text, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out dt))
            {
                if (DateTime.TryParse(txtDateTo.Text, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out dt))
                {
                    DeleteLog();
                }

            }
        }
        catch (Exception ex)
        {

            lblMessage.Text = ex.StackTrace.ToString();
            lblMessage.Visible = true;
            return;

        }
    }
}
