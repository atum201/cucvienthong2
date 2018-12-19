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

public partial class WebUI_CN_HuyThongBaoLePhi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGui_Click(object sender, EventArgs e)
    {
        string ThongBaoLePhiID = Request["ThongBaoLePhiID"].ToString();
        try
        {
            ThongBaoLePhi objThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetById(ThongBaoLePhiID);
            objThongBaoLePhi.TrangThaiId = (int?)EnTrangThaiThongBaoPhiList.CHO_DUYET_HUY;
            objThongBaoLePhi.LyDoHuy = txtLyDoHuy.Text.Trim();
            ProviderFactory.ThongBaoLePhiProvider.Save(objThongBaoLePhi);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "closePopUp",
                        "<script>alert('Gửi thành công!');window.opener.location.href='CN_ThongBaoPhi.aspx';window.close();</script>");
        }
        catch
        {
        }
    }
    protected void btnBoQua_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "closePopUp",
                "<script>self.close() ;</script>");
    }
}
