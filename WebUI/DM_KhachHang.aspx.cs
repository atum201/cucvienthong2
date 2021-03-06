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

public partial class WebUI_DM_ToChuc : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtKhachHang = new DataTable();
        dtKhachHang.Columns.Add("MaKH");
        dtKhachHang.Columns.Add("TenKH");
        dtKhachHang.Columns.Add("Mail");
        dtKhachHang.Columns.Add("TinhThanh");


        dtKhachHang.Rows.Add("669B-MGM-2008", "Lê việt Đức", "vietduc124@gmail.com", "Hà Nội");
        dtKhachHang.Rows.Add("669B-MGM-2008", "Lê việt Đức", "vietduc124@gmail.com", "Hà Nội");
        dtKhachHang.Rows.Add("669B-MGM-2008", "Lê việt Đức", "vietduc124@gmail.com", "Hà Nội");
        dtKhachHang.Rows.Add("669B-MGM-2008", "Lê việt Đức", "vietduc124@gmail.com", "Hà Nội");
        dtKhachHang.Rows.Add("669B-MGM-2008", "Lê việt Đức", "vietduc124@gmail.com", "Hà Nội");
        dtKhachHang.Rows.Add("669B-MGM-2008", "Lê việt Đức", "vietduc124@gmail.com", "Hà Nội");
        gvKhachHang.DataSource = dtKhachHang;
        gvKhachHang.DataBind();
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "DM_KhachHang_ChiTiet.aspx",
                       "<script>popCenter('DM_KhachHang_ChiTiet.aspx','DM_KhachHang_ChiTiet',720,520);</script>");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "DM_CoQuanDoKiem_ChiTiet",
					  "<script>popCenter('DM_KhachHang_ChiTiet.aspx','DM_CoQuanDoKiem_ChiTiet',800,250);</script>");
    }
}
