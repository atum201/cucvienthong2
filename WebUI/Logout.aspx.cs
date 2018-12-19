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
using Resources;

public partial class WebUI_Logout : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.HT_DANG_XUAT, LogResource.DangXuat);

        Session["User"] = null;        
        Response.Redirect("HT_DangNhap.aspx");
    }
}
