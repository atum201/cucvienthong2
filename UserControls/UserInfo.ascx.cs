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
using System.Security.Principal;
using Cuc_QLCL.AdapterData;
using Cuc_QLCL.Entities;
using Resources;

public partial class UserControls_UserInfo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            lblWelcome.Text = mUserInfo.FullName;
    }
    public UserInfo mUserInfo
    {
        get
        {
            UserInfo _mUserInfo = Session["User"] as UserInfo;
            if (_mUserInfo == null)
            {
                Response.Redirect("HT_DangNhap.aspx", true);
            }
            return _mUserInfo;
        }
    }
    protected void lbtDang_xuat_Click(object sender, EventArgs e)
    {        
        Response.Redirect("Logout.aspx", false);
    }
}
