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

public partial class WebUI_BC_TiepNhanHoSo : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtNgayTiepNhan.Text = DateTime.Now.ToShortDateString();
        }
    }
    protected void btnIn_Click(object sender, EventArgs e)
    {
        string LoaiHoSo = ddlLoaiHoSo.SelectedValue;
        ClientScript.RegisterClientScriptBlock(this.GetType(), "BC_TiepNhanHoSo",
                "<script>popCenter('../ReportForm/HienBaoCao.aspx?LoaiBaoCao=BaoCaoTiepNhanHoSo&NgayTiepNhan=" + txtNgayTiepNhan.Text.Trim() + "&LoaiHoSo=" + LoaiHoSo + "','baocaotiepnhan',790,590);</script>");
    }
}
