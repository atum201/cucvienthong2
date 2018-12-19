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

public partial class WebUI_HT_DangNhap : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["USERNAME"] != null)
                txtTenDangNhap.Text = Request.Cookies["USERNAME"].Value;
            if (Request.Cookies["PASSWORD"] != null)
                txtMatKhau.Attributes.Add("value", Request.Cookies["PASSWORD"].Value);
            if (Request.Cookies["USERNAME"] != null && Request.Cookies["PASSWORD"] != null)
                chkSave.Checked = true;
        }
    }
    protected void btnDangNhap_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            UserInfo mUserInfo = new UserInfo();
            mUserInfo.Authenticate(txtTenDangNhap.Text, txtMatKhau.Text);

            if (mUserInfo.IsAuthenticated)
            {
                Session["User"] = mUserInfo;
                mUserInfo.LocalIP = Request.UserHostAddress;
                // Nếu chọn lưu thông tin, đưa vào cookie
                if (chkSave.Checked == true)
                {
                    Response.Cookies["USERNAME"].Value = txtTenDangNhap.Text;
                    Response.Cookies["USERNAME"].Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies["PASSWORD"].Value = txtMatKhau.Text;
                    Response.Cookies["PASSWORD"].Expires = DateTime.Now.AddMonths(1);
                }
                // Nếu không chọn lưu thông tin, xóa cookie
                else
                {
                    Response.Cookies["USERNAME"].Expires = DateTime.Now.AddMonths(-1);
                    Response.Cookies["PASSWORD"].Expires = DateTime.Now.AddMonths(-1);
                }
                //chuyển đến trang chính của hệ thống
                ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.HT_DANG_NHAP, LogResource.DangNhap);
                Response.Redirect("Default.aspx?notaccess=" + Request["notaccess"]);
            }
        }
        catch (Exception ex)
        {
            this.Thong_bao(ex.Message);
        }
    }
    /// <summary>
    /// Thông báo
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// Giangum     09/04/2009  Thêm mới
    /// </Modified>
    protected void Thong_bao(string message)
    {
        ((System.Web.UI.Page)this.Page).RegisterClientScriptBlock("Validate",
            "<script language = 'javascript'>alert('" + message + "');"
            + "</script>");
    }
}
