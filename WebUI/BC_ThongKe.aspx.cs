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

public partial class WebUI_BC_ThongKe : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtTuNgay.Text = DateTime.Now.AddDays(-30).ToShortDateString();
            txtDenNgay.Text = DateTime.Now.ToShortDateString();
            GetChuyenVienXuLy();
        }
    }

    protected void btnIn_Click(object sender, EventArgs e)
    {
        string sUser = "";
        string sTu = "";
        string sDen = "";
        
        sTu = txtTuNgay.Text.Trim();
        sDen = txtDenNgay.Text.Trim();
        sUser = ddlUser.SelectedValue;
        string strTrungTam = ddlUser.SelectedValue.Substring(0, 4).ToUpper() == "TTCN" ? "TTCN" : ddlUser.SelectedValue.Substring(0, 3).ToUpper();
        ClientScript.RegisterClientScriptBlock(this.GetType(), "CN_TiepNhanHoSo_TaoPhieuDanhGia",
                "<script>popCenter('../ReportForm/HienBaoCao.aspx?LoaiBaoCao=BaoCaoThongKe&tu=" + sTu
                + "&den=" + sDen + "&chuyenvien=" + sUser + "&trungtam=" + strTrungTam + "','baocaothongke',790,590);</script>");
    }

    /// <summary>
    /// Lấy danh sách các chuyên viên xử lý
    /// LinhNM
    /// </summary>
    private void GetChuyenVienXuLy()
    {
        ddlUser.Items.Clear();

       DataTable dtUser = ProviderFactory.SysUserProvider.GetByTrungTamID(mUserInfo.TrungTam.Id);
       if (dtUser.Rows.Count > 0)
        {
            ddlUser.DataSource = dtUser;
            ddlUser.DataTextField = "FullName";
            ddlUser.DataValueField = "ID";
            ddlUser.DataBind();
        }
    }
}
