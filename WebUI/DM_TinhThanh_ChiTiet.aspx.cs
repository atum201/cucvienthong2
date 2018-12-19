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

public partial class WebUI_DM_TinhThanh_ChiTiet : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                 "<script>alert('Cập nhật thành công!'); location.href='DM_TinhThanh_ChiTiet.aspx'; </script>");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
    }
}
