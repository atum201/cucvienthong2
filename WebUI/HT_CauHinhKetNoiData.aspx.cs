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

public partial class WebUI_HT_CauHinhKetNoiData : PageBase
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
        // Lấy thông tin cấu hình từ file config
        string stringConfigConnection = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Cuc_QLCL.Data.ConnectionString"].ConnectionString;
        string[] sConnection = stringConfigConnection.Split(';');
        string[] server=sConnection[0].Split('=');
        string[] data = sConnection[1].Split('=');
        string[] login = sConnection[3].Split('=');
        string[] password = sConnection[4].Split('=');

        txtServerName.Text= server[1];
        txtDatabaseName.Text = data[1];
        txtLogin.Text = login[1];
        txtPassword.Text = password[1];


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
            string strConnectionString = "Data Source={};Initial Catalog={};Persist Security Info=True;User ID={};Password={}";
            Configuration myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            myConfiguration.ConnectionStrings.ConnectionStrings["Cuc_QLCL.Data.ConnectionString"].ConnectionString = txtServerName.Text;
            myConfiguration.Save();
            ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                   "<script>alert('" + Resources.Resource.msgSysConfigCapNhat + "');</script>");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
