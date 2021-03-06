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
public partial class WebUI_HT_SaoLuu_CauHinh : PageBase
{
    // Đường dẫn sao lưu
    string mRootBackupDir = "";

    int mBackupType;
    //Start date of job get default in Store
    string mBackupDate = "";
    string mBackupTime;
    int mBackupDay;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblThongBao.Text = ""; 
            // Load lại thông tin
            LoadConfig();
            //Thiết lập các control
            SetControl();
        }
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

        if (mBackupType == (int)LoaiSaoLuu.Ngay)
        {
            radNgay.Checked = true;
            radTuan.Checked = false;
            radThang.Checked = false;
            ddlNgayGio.SelectedValue = mBackupTime.Substring(0, 2);
            ddlNgayPhut.SelectedValue = mBackupTime.Substring(2, 2);
        }
        else if (mBackupType == (int)LoaiSaoLuu.Tuan)
        {
            radTuan.Checked = true;
            radNgay.Checked = false;
            radThang.Checked = false;
            ddlTuanGio.SelectedValue = mBackupTime.Substring(0, 2);
            ddlTuanPhut.SelectedValue = mBackupTime.Substring(2, 2);
            ddlTuanThu.SelectedValue = mBackupDay.ToString();
        }
        else
        {
            radThang.Checked = true;
            radNgay.Checked = false;
            radTuan.Checked = false;
            ddlThangGio.SelectedValue = mBackupTime.Substring(0, 2);
            ddlThangPhut.SelectedValue = mBackupTime.Substring(2, 2);
            ddlThangNgay.SelectedValue = mBackupDay.ToString();
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
        mRootBackupDir = ProviderFactory.SysConfigProvider.GetValue(ConfigInfor.BACKUP_PATH);
        txtDuongDan.Text = mRootBackupDir;
        mBackupTime = ProviderFactory.SysConfigProvider.GetValue(ConfigInfor.BACKUP_TIME);
        mBackupDay = int.Parse(ProviderFactory.SysConfigProvider.GetValue(ConfigInfor.BACKUP_DAY));
        mBackupType = int.Parse(ProviderFactory.SysConfigProvider.GetValue(ConfigInfor.BACKUP_TYPE));
    }
    /// <summary>
    /// Cập nhật thông tin
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Name        Date        Comment
    /// TuanND       08/12/2008  Thêm mới
    /// TuanVM      11/02/09    Sửa (Lưu thông tin cấu hình sau khi dặt job thành công trong CSDL)
    /// </Modified>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        getConfigValue();
        try
        {
            // Lấy các tham số sao lưu
            SetParaValue();
            ProviderFactory.SystemManProvider.SetSchedule(mRootBackupDir, mBackupType, mBackupDate, mBackupTime, mBackupDay);
            //Log.Write(mUserInfo, LogEventList.Backup_Config, "Hệ thống được cấu hình lại bởi" + mUserInfo.User_ID + "vào lúc:" + DateTime.Now.ToString());
            ProviderFactory.SystemManProvider.BackupSetConfigParameter(mBackupType.ToString(), mBackupTime, mBackupDay.ToString());
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Thiết lập hệ thống sao lưu thành công!')</script>");
         
        }
        catch (Exception ex)
        {
            //lblMessage.Text = "Thiết lập hệ thống sao lưu không thành công!";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script> alert('Thiết lập hệ thống sao lưu thất bại!');</script>");
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
            mBackupType = (int)LoaiSaoLuu.Ngay; ;
            mBackupTime = ddlNgayGio.Text + ddlNgayPhut.Text;
            mBackupDay = 1;
        }
        else if (radTuan.Checked)
        {
            mBackupType = (int)LoaiSaoLuu.Tuan;
            mBackupTime = ddlTuanGio.Text + ddlTuanPhut.Text;
            // lay ngay sao luu trong tuan
            mBackupDay = int.Parse(ddlTuanThu.SelectedValue);
        }
        else if (radThang.Checked)
        {
            mBackupType = (int)LoaiSaoLuu.Thang;
            mBackupTime = ddlThangGio.Text + ddlThangPhut.Text;
            mBackupDay = int.Parse(ddlThangNgay.Text.Trim());
        }
        return true;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("HT_QuanLySaoLuuPhucHoi.aspx");
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
