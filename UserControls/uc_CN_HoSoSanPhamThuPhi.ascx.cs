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

public partial class UserControls_uc_CN_HoSoSanPhamThuPhi : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {        
      
        LayDanhSachPhi();
        HienThiThongTin();
        if (!IsPostBack)
        {

        }
        else
        {

        }
    }

    /// <summary>
    /// Lấy danh sách hồ sơ sản phẩm mới nhận
    /// </summary>
   
    
    private void LayDanhSachPhi(){
		DataTable dtHoSo = new DataTable();
		dtHoSo.Columns.Add("TenSanPham");
		dtHoSo.Columns.Add("SoGiayCN");		
		dtHoSo.Columns.Add("MaSanPham");
		dtHoSo.Columns.Add("NguonGoc");
		dtHoSo.Columns.Add("HinhThuc");
		dtHoSo.Columns.Add("MaTrangThai", typeof(int));
		dtHoSo.Columns.Add("TrangThai");
		dtHoSo.Columns.Add("TongPhi");
        dtHoSo.Rows.Add("TBNP001", "100911905093B0", "Công ty Tinh Vân", "Làng sinh viên Hacinco", "0985597986", 0, "0123344398", "1000.0000");
        dtHoSo.Rows.Add("TBNP002", "100921905093B0", "Cục sở hữu trí tuệ", "Hà Nội", "089873948", 1, "938840383", "2000.0000");
        dtHoSo.Rows.Add("TBNP003", "100931905093B0", "Cục tần số", "Thanh Xuân - Hà Nội", "0987348337", 2, "098449585", "4000.0000");
		gvPhi.DataSource = dtHoSo;	
		gvPhi.DataBind();
    
    }
    protected void lnkXoa_Click(object sender, EventArgs e)
    {

    }
    protected void lnkGui_Click(object sender, EventArgs e)
    {

    }
    protected void gvSanPham_DataBound(object sender, EventArgs e)
    {
		//int TotalChkBx = 0;
		//if (gvSanPham.HeaderRow != null)
		//{
		//    // Lấy tham chiếu đến checkbox header
		//    CheckBox cbHeader = (CheckBox)gvSanPham.HeaderRow.FindControl("chkCheckAll");
		//    // Bắt sự kiện onclick của checkbox toàn bộ
		//    cbHeader.Attributes["onclick"] = string.Format("HeaderClick('{0}');", gvSanPham.ClientID);

		//    // Duyệt trên gridview
		//    foreach (GridViewRow gvr in gvSanPham.Rows)
		//    {
		//        CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
		//        cb.Attributes["onclick"] = string.Format("ChildClick('{0}');", cbHeader.ClientID);
		//        // Đếm số lượng check box con
		//        TotalChkBx++;
		//    }
		//}
		//// Gán giá trị của biến
		//this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "TotalChkBx", string.Format("TotalChkBx={0};Counter={1};", TotalChkBx, 0), true);
    }
    protected void lnkThemMoi_Click(object sender, EventArgs e)
    {
		Response.Redirect("CN_HoSoSanPham_ChiTiet.aspx?MaTrangThai=0");
    }
    protected void ddlTrangThai_SelectedIndexChanged(object sender, EventArgs e)
    {
		//if (ddlTrangThai.SelectedIndex < 2)
		//    return;
		//DataTable dtSource = gvSanPham.DataSource as DataTable;
		//DataView dvSource = dtSource.DefaultView;
		//dvSource.RowFilter = string.Format("TrangThai like '%{0}%'", ddlTrangThai.Text);
		//gvSanPham.DataSource = null;
		//gvSanPham.DataSource = dvSource.Table;
		//gvSanPham.DataBind();
    }
    private void HienThiThongTin()
    {
        lblSoHoSo.Text = "668B-MGM-2008";
        lblDonVi.Text = "Tinh Vân";
        lblDienThoai.Text = "0431234567";
        lblFax.Text = "0431234568";
        lblNguoiNopHoSo.Text = "Nguyễn Văn A";
        lblNguonGoc.Text = "Sản xuất trong nước";
        lblHinhThuc.Text = "Bắt buộc CNPH";
        lblNgayNhan.Text = "12/03/2009";
        lblEmail.Text = "thanhnv@gmail.com";
        lblSoCVden.Text = "668BMGM2008";
        lblNhanhstu.Text = "Bưu điện";
    }
	protected void lnkThemMoi1_Click(object sender, EventArgs e)
	{
		Response.Redirect("CN_ThongBaoPhi_TaoMoi.aspx?add=add");
	}
}
