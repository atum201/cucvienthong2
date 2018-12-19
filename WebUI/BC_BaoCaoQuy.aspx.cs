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

public partial class WebUI_BC_BaoCaoQuy : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            txtTuNgay.Text = DateTime.Now.AddDays(-90).ToShortDateString();
            txtDenNgay.Text = DateTime.Now.ToShortDateString();
        }
    }

    protected void btnIn_Click(object sender, EventArgs e)
    {
        string sTrungTam = "";
        string sTuQuy = "";
        string sDenQuy = "";
        int iCountPrint = 3;

        sTuQuy = txtTuNgay.Text.Trim();

        sDenQuy = txtDenNgay.Text.Trim();

        sTrungTam = "TTCN,TT2,TT3";

        ClientScript.RegisterClientScriptBlock(this.GetType(), "CN_TiepNhanHoSo_TaoPhieuDanhGia",
                "<script>popCenter('../ReportForm/HienBaoCao.aspx?LoaiBaoCao=BaoCaoQuy&type=" + iCountPrint + "&trungtam=" + sTrungTam
                + "&tu=" + sTuQuy + "&den=" + sDenQuy + "','baocaoquy',790,590);</script>");
    }

    private string GetQuaterTime(int iQuy, int iNam)
    {
        if (iQuy == 1)
            return "1/1/" + iNam.ToString();
        else if (iQuy == 2)
            return "1/4/" + iNam.ToString();
        else if (iQuy == 3)
            return "1/7/" + iNam.ToString();
        else if (iQuy == 4)
            return "1/10/" + iNam.ToString();
        return "";
    }
}
