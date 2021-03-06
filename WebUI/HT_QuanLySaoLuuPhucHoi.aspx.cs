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
using System.IO;
using System.Xml;
using Cuc_QLCL.AdapterData;
using Cuc_QLCL.Entities;

public partial class WebUI_HT_QuanLySaoLuuPhucHoi : PageBase
{
    string mRootBackupDir = "";
    string mImageBackupDir = "";
    /// <summary>
    /// Load form
    /// </summary>
    /// <Modified>
    ///     Author      Date        Comments
    ///     TuanND    09/12/2008    Tạo mới
    /// </Modified>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Lấy thông tin cấu hình (thư mục chứ file backup)
        getConfigValue();
        if (!IsPostBack)
        {
            // Lấy danh sách file backup
            BackupFileGetAll();
        }
        btnXoa.Attributes.Add("OnClick", "javascript:return confirm('Bạn có thực sự muốn xóa nhật ký đã chọn?');");
    }

    /// <summary>
    /// Lấy toàn bộ các file backup trong thư mục backup tai server và hiển thị trên gridview
    /// </summary>
    /// <Modified>
    ///     Author      Date        Comments
    ///     TuanND    09/12/2008    Tạo mới
    /// </Modified>
    public void BackupFileGetAll()
    {
        DataTable dt = new DataTable();
        DirectoryInfo theFolder = new DirectoryInfo(mRootBackupDir);
        dt.Columns.Add("BackupDate");
        dt.Columns.Add("BackupFile");
        string strBackupDate = "";
        string strBackupFile = "";
        if (theFolder.Exists)
        {
            foreach (FileInfo nextFile in theFolder.GetFiles())
            {
                //strBackupDate = nextFile.CreationTime.ToShortDateString() + " " + nextFile.CreationTime.Hour.ToString() + ":" + nextFile.CreationTime.Minute.ToString();

                strBackupFile = nextFile.Name;
                //dt.Rows.Add(strBackupDate,strBackupFile);
                if(strBackupFile.Contains(".bak"))
                    dt.Rows.Add(nextFile.CreationTime, strBackupFile);
            }
        }
        DataView dv = new DataView(dt);
        dv.Sort = "BackupDate desc";
        gvNhatKySaoLuu.DataSource = dv;
        gvNhatKySaoLuu.DataBind();
    }
    /// <summary>
    /// Lấy đường dẫn sao lưu file và các thông tin cấu hình khác
    /// </summary>
    /// <returns></returns>
    /// Author      Date        Comment
    /// TuanVM      11/02/09    Tạo mới
    public void getConfigValue()
    {
        mRootBackupDir = ProviderFactory.SysConfigProvider.GetValue("BACKUP_PATH");
    }


    protected void btnSaoLuuBangTay_Click(object sender, EventArgs e)
    {
        try
        {
            ProviderFactory.SystemManProvider.Backup();
            Thong_bao(this, "Sao lưu hệ thống thành công!");
            BackupFileGetAll();
        }
        catch(Exception ex)
        { throw ex; }
    }
    /// <summary>
    /// Sự kiện thay đổi trang hiển thị
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    ///     Author      Date        Comments
    ///     TuanND    09/12/2008    Tạo mới
    /// </Modified>
    protected void gvNhatKySaoLuu_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvNhatKySaoLuu.PageIndex = e.NewPageIndex;
        BackupFileGetAll();
    }
    /// <summary>
    /// Xóa file backup
    /// </summary>
    /// <param name="fileName">Tên file cần xóa</param>
    /// <Modified>
    ///     Author      Date        Comments
    ///     TuanND    09/12/2008    Tạo mới
    /// </Modified>
    public void DeleteBackupFile(string fileName)
    {
        DirectoryInfo theFolder = new DirectoryInfo(mRootBackupDir);
        if (theFolder.Exists)
        {
            foreach (FileInfo nextFile in theFolder.GetFiles())
            {
                string strFileName = nextFile.Name.ToString();
                if (fileName == strFileName)
                {
                    nextFile.Delete();
                    ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.HT_SAO_LUU_XOA_FILE, "Xoá file sao lưu: " + strFileName);
                }
            }
        }
    }
    /// <summary>
    /// Xóa file backup
    /// </summary>
    /// <param name="fileName">Tên file cần xóa</param>
    /// <Modified>
    ///     Author      Date        Comments
    ///     TuanND    09/12/2008    Tạo mới
    /// </Modified>
    protected void btnXoa_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow rowItem in gvNhatKySaoLuu.Rows)
        {
            CheckBox chk = (CheckBox)(rowItem.Cells[0].FindControl("chkCheck"));
            if (chk.Checked)
            {
                DeleteBackupFile(rowItem.Cells[2].Text);
            }
        }
        Thong_bao("Xoá thành công");
        BackupFileGetAll();
    }
    /// <summary>
    /// Sự kiện restore
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    ///     Author      Date        Comments
    ///     TuanND    09/12/2008    Tạo mới
    /// </Modified>
    protected void gvNhatKySaoLuu_RowCommand(object sender, GridViewCommandEventArgs e)
    {       
        // Lấy thông tin cấu hình (đường dẫn thư mục chứa file backup
        getConfigValue();
        if (e.CommandName == "Restore")
        {
            try
            {
                // Get the selected index
                int _selectedIndex = int.Parse(e.CommandArgument.ToString());

                // Lấy đường dẫn file backup
                string strRootFileName = mRootBackupDir + "\\" + gvNhatKySaoLuu.Rows[_selectedIndex].Cells[2].Text;
                
               FileInfo Rootfile = new FileInfo(strRootFileName);

                // Kiểm tra file có tồn tại không
               if (Rootfile.Exists)
               {
                   // Thực hiện phục hồi
                   //ProviderFactory.SystemManProvider.Restore(strRootFileName);
                   ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.HT_PHUC_HOI, "Hệ thống được phục hồi bởi" + mUserInfo.UserID + " vào lúc:" + DateTime.Now.ToString());
                   Thong_bao(this, "Phục hồi hệ thống thành công!");
                   //Thong_bao(this, "Hệ thống phục hồi thành công!");
               }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    protected void gvNhatKySaoLuu_DataBound(object sender, EventArgs e)
    {
        //int TotalChkBx = 0;
        //if (gvNhatKySaoLuu.HeaderRow != null)
        //{
        //    //lay tham chieu den checkbox header
        //    CheckBox cbHeader = (CheckBox)gvNhatKySaoLuu.HeaderRow.FindControl("chkAll");
        //    //Bat su kien onclick cua check toan bo
        //    cbHeader.Attributes["onclick"] = string.Format("HeaderClick('{0}');", gvNhatKySaoLuu.ClientID);

        //    //lap cach dong cua grid
        //    foreach (GridViewRow gvr in gvNhatKySaoLuu.Rows)
        //    {
        //        CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
        //        cb.Attributes["onclick"] = string.Format("ChildClick('{0}');", cbHeader.ClientID);
        //        //dem so luong checkbox con
        //        TotalChkBx++;
        //    }
        //}
        ////gan lai gia tri cua bien
        //this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "TotalChkBx", string.Format("TotalChkBx={0};Counter={1};", TotalChkBx, 0), true);
    }

}
