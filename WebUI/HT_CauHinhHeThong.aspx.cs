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
using Cuc_QLCL.Entities;
using Cuc_QLCL.AdapterData;

public partial class WebUI_HT_CauHinhHeThong : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadConfig();
    }
    /// <summary>
    /// Load các thông tin cấu hình lần trước lên form hiển thị
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// NguyenQuy       08/05/2009  Thêm mới
    /// </Modified>
    public void LoadConfig()
    {
        // Lấy thông tin cấu hình từ bang config
        TList<SysConfig> lstISysConfig = ProviderFactory.SysConfigProvider.GetAll();
        TList<SysConfig> tmplist1, tmplist2;
        tmplist1 = lstISysConfig;
        tmplist2 = lstISysConfig;
        SysConfig mSysConfig1 = lstISysConfig.Find(delegate(SysConfig s) { return s.Key == "TEN_TRUNG_TAM"; });
        SysConfig mSysConfig2 = lstISysConfig.Find(delegate(SysConfig s) { return s.Key == "BACKUP_PATH"; });
        SysConfig mSysConfig3 = lstISysConfig.Find(delegate(SysConfig s) { return s.Key == "WEB_SERVICE_URL"; });
        txtTenTrungTam.Text = mSysConfig1.Value;
        txtTenTrungTamHide.Text = mSysConfig1.Id.ToString();

        txtDuongdansaoluu.Text = mSysConfig2.Value;
        txtDuongdansaoluuHide.Text = mSysConfig2.Id.ToString();

        txtDiachiwebservice.Text = mSysConfig3.Value;
        txtDiachiwebserviceHide.Text = mSysConfig3.Id.ToString();
    }

    /// <summary>
    /// Cap nhat thong tin cau hinh vao CSDL chi thao tac voi csdl 1 lan
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            TList<SysConfig> l = new TList<SysConfig>();
            SysConfig mSysConfig1 = new SysConfig();
            mSysConfig1.Id = Convert.ToInt32(txtTenTrungTamHide.Text);
            mSysConfig1.Value = txtTenTrungTam.Text;
            mSysConfig1.Key = "TEN_TRUNG_TAM";
            mSysConfig1.EntityState = EntityState.Changed;
            l.Add(mSysConfig1);

            mSysConfig1 = new SysConfig();
            mSysConfig1.Id = Convert.ToInt32(txtDuongdansaoluuHide.Text);
            mSysConfig1.Value = txtDuongdansaoluu.Text;
            mSysConfig1.Key = "BACKUP_PATH";
            mSysConfig1.EntityState = EntityState.Changed;
            l.Add(mSysConfig1);

            mSysConfig1 = new SysConfig();
            mSysConfig1.Id = Convert.ToInt32(txtDiachiwebserviceHide.Text);
            mSysConfig1.Value = txtDiachiwebservice.Text;
            mSysConfig1.Key = "WEB_SERVICE_URL";
            mSysConfig1.EntityState = EntityState.Changed;
            l.Add(mSysConfig1);
            //ghi list object config vao csdl
            ProviderFactory.SysConfigProvider.Save(l);
            ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                   "<script>alert('" + Resources.Resource.msgSysConfigCapNhat + "');</script>");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
