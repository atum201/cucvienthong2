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

public partial class WebUI_BC_BaoCaoTuan : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtTuNgay.Text = DateTime.Now.AddDays(-7).ToShortDateString();
            txtDenNgay.Text = DateTime.Now.ToShortDateString();
        }
    }


    protected void btnIn_Click(object sender, EventArgs e)
    {
        string sTrungTam = "";
        string sTuTuan = "";
        string sDenTuan = "";
        int iCountPrint = 0;
       
        sTuTuan = txtTuNgay.Text;
        
        sDenTuan = txtDenNgay.Text;

        sTrungTam = "TTCN,TT2,TT3";
        iCountPrint = 3;

        ClientScript.RegisterClientScriptBlock(this.GetType(), "BaoCaoTuan",
                "<script>popCenter('../ReportForm/HienBaoCao.aspx?LoaiBaoCao=BaoCaoTuan&type=" + iCountPrint + "&trungtam=" + sTrungTam 
                +"&tu="+ sTuTuan +"&den="+ sDenTuan +"','baocaotuan',780,590);</script>");
    }

    /// <summary>
    /// Kiểm tra ngày tháng hợp lệ hay không
    /// </summary>
    /// <returns></returns>
    /// Author          Date        Comment
    /// TuanVM          29/05/2009  Tạo mới
    public bool NgayHopLe()
    {
        DateTime tuNgay = Convert.ToDateTime(txtTuNgay.Text);
        DateTime denNgay = Convert.ToDateTime(txtDenNgay.Text);
        if (tuNgay.CompareTo(denNgay) < 0)
            return true;
        return false;
    }
}
