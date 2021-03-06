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

public partial class WebUI_DM_TinhThanh : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtTinhThanh = new DataTable();
        dtTinhThanh.Columns.Add("MaTT");
        dtTinhThanh.Columns.Add("TenTinhThanh");
        dtTinhThanh.Columns.Add("TenTrungTam");
        dtTinhThanh.Columns.Add("MaTrungTam");
        dtTinhThanh.Columns.Add("GhiChu");


        dtTinhThanh.Rows.Add("LC-1", "Hà Nội", "Trung tâm chứng nhận", "1", "Thủ đô");
        dtTinhThanh.Rows.Add("LC-2", "Hải Phòng", "Trung tâm chứng nhận", "1", "TP");
        gvTinhThanh.DataSource = dtTinhThanh;
        gvTinhThanh.DataBind();
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "DM_TinhThanh_ChiTiet.aspx",
                   "<script>popCenter('DM_TinhThanh_ChiTiet.aspx','DM_TinhThanh_ChiTiet',720,520);</script>");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "DM_TinhThanh_ChiTiet.aspx",
						   "<script>popCenter('DM_TinhThanh_ChiTiet.aspx','DM_TinhThanh_ChiTiet',600,150);</script>");
    }
}
