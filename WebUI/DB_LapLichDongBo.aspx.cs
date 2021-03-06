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
using CucQLCL.Common;
using Cuc_QLCL.AdapterData;
public partial class WebUI_DB_DongBoLapLich : PageBase
{
    int mSyncType;
    string mSyncTime;
    int mSyncDay;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Load lại thông tin
            LoadConfig();
            //Thiết lập các control
            SetControl();
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        getConfigValue();
        try
        {
            // Lưu tham số sao lưu vào bảng Sys_Config
            SetParaValue();
            //ProviderFactory.SystemManProvider.SetSchedule(mRootSyncDir, mSyncType, mSyncDate, mSyncTime, mSyncDay);
            ////Log.Write(mUserInfo, LogEventList.Sync_Config, "Hệ thống được cấu hình lại bởi" + mUserInfo.User_ID + "vào lúc:" + DateTime.Now.ToString());
            ProviderFactory.SystemManProvider.SyncSetConfigParameter(mSyncType.ToString(), mSyncTime, mSyncDay.ToString());
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Thiết lập đồng bộ hệ thống thành công!')</script>");

        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script> alert('Thiết lập đồng bộ hệ thống thất bại!');</script>");
            return;
        }
    }
    /// <summary>
    /// Gán giá trị cho các tham số
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// TuanND       08/12/2008  Thêm mới
    /// TuanVM      11/02/09     Sửa (bỏ việc kiểm tra lưu thông tin vào file config, việc này fải thực hiện sau khi đặt Job vào csdl)
    /// </Modified>
    public bool SetParaValue()
    {
        if (radNgay.Checked)
        {
            mSyncType = (int)LoaiSaoLuu.Ngay; ;
            mSyncTime = ddlNgayGio.Text + ddlNgayPhut.Text;
            mSyncDay = 1;
        }
        else if (radTuan.Checked)
        {
            mSyncType = (int)LoaiSaoLuu.Tuan;
            mSyncTime = ddlTuanGio.Text + ddlTuanPhut.Text;
            // lay ngay sao luu trong tuan
            mSyncDay = int.Parse(ddlTuanThu.SelectedValue);
        }
        else if (radThang.Checked)
        {
            mSyncType = (int)LoaiSaoLuu.Thang;
            mSyncTime = ddlThangGio.Text + ddlThangPhut.Text;
            mSyncDay = int.Parse(ddlThangNgay.Text.Trim());
        }
        return true;
    }
    /// <summary>
    /// Load các thông tin cấu hình lần trước lên form hiển thị
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// TuanND       08/12/2008  Thêm mới
    /// TuaVM       11/02/09    Sửa (Thay đổi cách lấy thông tin cấu hình từ file cấu hình)
    /// </Modified>
    public void LoadConfig()
    {
        // Lấy thông tin cấu hình từ file config
        getConfigValue();

        if (mSyncType == (int)LoaiSaoLuu.Ngay)
        {
            radNgay.Checked = true;
            radTuan.Checked = false;
            radThang.Checked = false;
            ddlNgayGio.SelectedValue = mSyncTime.Substring(0, 2);
            ddlNgayPhut.SelectedValue = mSyncTime.Substring(2, 2);
        }
        else if (mSyncType == (int)LoaiSaoLuu.Tuan)
        {
            radTuan.Checked = true;
            radNgay.Checked = false;
            radThang.Checked = false;
            ddlTuanGio.SelectedValue = mSyncTime.Substring(0, 2);
            ddlTuanPhut.SelectedValue = mSyncTime.Substring(2, 2);
            ddlTuanThu.SelectedValue = mSyncDay.ToString();
        }
        else
        {
            radThang.Checked = true;
            radNgay.Checked = false;
            radTuan.Checked = false;
            ddlThangGio.SelectedValue = mSyncTime.Substring(0, 2);
            ddlThangPhut.SelectedValue = mSyncTime.Substring(2, 2);
            ddlThangNgay.SelectedValue = mSyncDay.ToString();
        }
    }
    /// <summary>
    /// Lấy đường dẫn sao lưu file và các thông tin cấu hình khác
    /// </summary>
    /// <returns></returns>
    /// Author      Date        Comment
    /// TuanVM      11/02/09    Tạo mới
    public void getConfigValue()
    {
        mSyncTime = ProviderFactory.SysConfigProvider.GetValue(ConfigInfor.SYNC_TIME);
        mSyncDay = int.Parse(ProviderFactory.SysConfigProvider.GetValue(ConfigInfor.SYNC_DAY));
        mSyncType = int.Parse(ProviderFactory.SysConfigProvider.GetValue(ConfigInfor.SYNC_TYPE));
    }
    #region "CheckChanged"
    /// <summary>
    /// Thiết lập tình trạng hiển thị của các control trên form
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// TuanND       08/12/2008  Thêm mới
    /// </Modified>
    public void SetControl()
    {
        if (radNgay.Checked)
        {
            ddlNgayGio.Enabled = true;
            ddlNgayPhut.Enabled = true;

            ddlTuanGio.Enabled = false;
            ddlTuanPhut.Enabled = false;
            ddlTuanThu.Enabled = false;

            ddlThangGio.Enabled = false;
            ddlThangPhut.Enabled = false;
            ddlThangNgay.Enabled = false;
        }
        else if (radTuan.Checked)
        {
            ddlNgayGio.Enabled = false;
            ddlNgayPhut.Enabled = false;

            ddlTuanGio.Enabled = true;
            ddlTuanPhut.Enabled = true;
            ddlTuanThu.Enabled = true;

            ddlThangGio.Enabled = false;
            ddlThangPhut.Enabled = false;
            ddlThangNgay.Enabled = false;
        }
        else
        {
            ddlNgayGio.Enabled = false;
            ddlNgayPhut.Enabled = false;

            ddlTuanGio.Enabled = false;
            ddlTuanPhut.Enabled = false;
            ddlTuanThu.Enabled = false;

            ddlThangGio.Enabled = true;
            ddlThangPhut.Enabled = true;
            ddlThangNgay.Enabled = true;
        }
    }

    protected void radNgay_CheckedChanged(object sender, EventArgs e)
    {
        SetControl();
    }
    protected void radTuan_CheckedChanged(object sender, EventArgs e)
    {
        SetControl();
    }
    protected void radThang_CheckedChanged(object sender, EventArgs e)
    {
        SetControl();
    }
    #endregion

}
