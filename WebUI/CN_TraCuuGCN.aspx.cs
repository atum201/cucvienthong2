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
using Cuc_QLCL.Data;
using Cuc_QLCL.Entities;
using CucQLCL.Common;
using System.IO;
using ExcelLibrary.SpreadSheet;
/// <summary>
/// Modified by : NguyenQuy
/// Date        : May 18, 2009
/// Purpose     : tra cuu giay chung nhan cua san pham, theo cac dieu kien: don vi nop, ngay cap, san pham,...
/// </summary>
public partial class WebUI_CN_TraCuuGCN : PageBase
{
    struct ThongTinTimkiem
    {
        public string SoGCN;
        public string DonViNop;
        public string TenSanPham;
        public string kyhieu;        
        public string NgayCapTu;
        public string NgayCapDen;
        //public string NgayHetHanTu;
        //public string NgayHetHanDen;
        public string HangSanXuat;
        public string TieuChuanApdung;
    }
    ThongTinTimkiem ThongTinTk = new ThongTinTimkiem();
    string mWhereClause =string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_ddlLoaiHinhChungNhan();
            bindDonViNop();
            bindSanPham();
            BindHangSanXuat();
            bindingGridView(mWhereClause);
            BindListTieuChuanApDung();
            Session["ThongTinTimKiem"] = null;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSanPham_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SetBackThongTinTimKiem();
        gvSanPham.PageIndex = e.NewPageIndex;
        //set condition of portfolio
        getWhereClause();
        //goi ham bindinggrid
        bindingGridView(mWhereClause);
    }
    /// <summary>
    /// Modified by : NguyenQuy
    /// Date        : May 13, 2009
    /// Purpose     : Binding data cho gridview voi dieu kien tim kiem nhap vao tu cac o nhap
    /// </summary>
    private void bindingGridView(string whereClause)
    {
        //goi ham lay du lieu voi tham so truyen vao la dieu kien cua nguoi nhap tu cac o nhap
        DataTable dtSanPhams = ProviderFactory.SanPhamProvider.GetByWhereClause(whereClause, gvSanPham.OrderBy, gvSanPham.PageIndex + 1, gvSanPham.PageSize);                
        gvSanPham.DataSource = dtSanPhams;
        if (dtSanPhams.Rows.Count > 0)
            gvSanPham.VirtualItemCount = int.Parse(dtSanPhams.Rows[0]["TongSoBanGhi"].ToString());
        gvSanPham.DataBind();
    }
    /// <summary>
    /// Modified by : NguyenQuy
    /// Date        : May 14, 2009
    /// Purpose     : Tim cac ho so thoa man dieu kien nguoi dung truyen vao qua cac o nhap
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnTimKiem_Click(object sender, EventArgs e)
    {
        GetThongTinTimKiem();
        gvSanPham.PageIndex = 0;
        //set condition of portfolio
        getWhereClause();
        //goi ham bindinggrid
        bindingGridView(mWhereClause);
    }
    /// <summary>
    /// Modified by : NguyenQuy
    /// Date        : May 14, 2009
    /// Purpose     : dat dieu kien cho cac thong so tim kiem ho so
    /// </summary>
    private void getWhereClause()
    {
        if (ddlDonviNop.SelectedIndex > 0)
            mWhereClause += " AND h.donviid ='" + ddlDonviNop.SelectedValue + "'";
        if (!txtSoGCN.Text.Trim().Equals(""))
            mWhereClause += " AND o.[SoGCN] LIKE N'%" + txtSoGCN.Text.Trim().Replace("'", "") + "%'";
        if (txtKyHieu.Text.Trim().Length>0)
            mWhereClause += " AND o.[kyhieu] LIKE N'%" + txtKyHieu.Text.Trim().Replace("'", "") + "%'";
        if (!ddlSanPham.SelectedValue.Equals("0"))
            mWhereClause += " AND o.SanphamID ='" + ddlSanPham.SelectedValue + "'";
        if (!ddlDonviNop.SelectedValue.Equals("0"))
            mWhereClause += " AND o.HoSoID IN ( select HoSoID from hoso where donviid ='" + ddlDonviNop.SelectedValue + "')";

        if (!txtNgayCap.Text.Trim().Equals("") && txtNgayCapDen.Text.Trim().Equals(""))
        {
            string[] dateTime = txtNgayCap.Text.Trim().Split('/');
            mWhereClause += " AND  DATEADD(dd, 0, DATEDIFF(dd, 0,  o.NgayKyDuyet )) >='" + dateTime[1] + "-" + dateTime[0] + "-" + dateTime[2] + "'";
        }
        else if (txtNgayCap.Text.Trim().Equals("") && !txtNgayCapDen.Text.Trim().Equals(""))
        {
            string[] dateTime = txtNgayCapDen.Text.Trim().Split('/');
            mWhereClause += " AND  DATEADD(dd, 0, DATEDIFF(dd, 0,  o.NgayKyDuyet )) <='" + dateTime[1] + "-" + dateTime[0] + "-" + dateTime[2] + "'";
        }
        else if (!txtNgayCap.Text.Trim().Equals("") && !txtNgayCapDen.Text.Trim().Equals(""))
        {
            string[] dateTimet = txtNgayCap.Text.Trim().Split('/');
            string[] dateTimed = txtNgayCapDen.Text.Trim().Split('/');
            mWhereClause += " AND  DATEADD(dd, 0, DATEDIFF(dd, 0,  o.NgayKyDuyet )) between '" + dateTimet[1] + "-" + dateTimet[0] + "-" + dateTimet[2] + "' AND '" + dateTimed[1] + "-" + dateTimed[0] + "-" + dateTimed[2] + "'";
        }
        
        if (ddlHangSanXuat.SelectedIndex > 0)
        {
            mWhereClause += " AND o.hangsanxuatid ='" + ddlHangSanXuat.SelectedValue + "'"; 
        }
        string TieuChuan = string.Empty;
        foreach (ListItem item in chklstTieuchuan.Items)
        {
            if (item.Selected)
                TieuChuan += "'" + item.Value + "',";
        }
        if (TieuChuan.Length > 0)
        {
            TieuChuan = TieuChuan.Remove(TieuChuan.Length - 1, 1);
            mWhereClause += " AND sp_tc.TieuChuanApDungID in (" + TieuChuan + ")";
        }

        // Tìm kiếm theo loại hình chứng nhận
        if (ddlLoaiHinhChungNhan.SelectedValue == "0")
            mWhereClause += " AND h.LoaiHoSo IN (1, 3) ";
        else
            mWhereClause += " AND h.LoaiHoSo = " + ddlLoaiHinhChungNhan.SelectedValue;
    }

    /// <summary>
    /// Đổ dữ liệu cho droplist loại hình chứng nhận
    /// </summary>
    /// <Modifield>
    /// Người tạo       ngày tạo            chú thích
    /// TuấnVM          16/11/2009           Tạo mới
    /// </Modifield>
    public void Bind_ddlLoaiHinhChungNhan()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("Text");
        dt.Columns.Add("Value");

        dt.Rows.Add("Chứng nhận hợp quy", "1");
        dt.Rows.Add("Chứng nhận hợp chuẩn", "3");
        dt.Rows.Add("-- Cả hai --", "0");
        ddlLoaiHinhChungNhan.DataSource = dt;
        ddlLoaiHinhChungNhan.DataTextField = "Text";
        ddlLoaiHinhChungNhan.DataValueField = "Value";
        ddlLoaiHinhChungNhan.DataBind();
        ddlLoaiHinhChungNhan.SelectedValue = "0";
    }

    /// <summary>
    /// Modified by : NguyenQuy
    /// Date        : May 18, 2009
    /// Purpose     : Binding du lieu cho combo san pham
    /// </summary>
    /// <returns></returns>
    private void bindSanPham()
    {
        ddlSanPham.Items.Clear();
        DataTable dt = ProviderFactory.SanPhamProvider.SanPham();
        ddlSanPham.DataSource = dt;
        ddlSanPham.DataTextField = "TenTiengViet";
        ddlSanPham.DataValueField = "SanphamID";
        ddlSanPham.DataBind();
        ddlSanPham.Items.Insert(0, new ListItem("-- tất cả --", "0"));
    }
    /// <summary>
    /// Modified by : NguyenQuy
    /// Date        : May 18, 2009
    /// Purpose     : Binding du cho combo Don vi nop ho so
    /// </summary>
    /// <returns></returns>
    private void bindDonViNop()
    {
        DataTable dt = ProviderFactory.HoSoProvider.LayDonViNop((int)LoaiHoSo.ChungNhanHopQuy);
        dt.Merge(ProviderFactory.HoSoProvider.LayDonViNop((int)LoaiHoSo.ChungNhanHopChuan));
        ddlDonviNop.DataSource = dt;

        ddlDonviNop.DataTextField = "TenTiengViet";
        ddlDonviNop.DataValueField = "id";
        ddlDonviNop.DataBind();
        ddlDonviNop.Items.Insert(0, new ListItem("-- tất cả --", "0"));
    }
    /// <summary>
    /// 
    /// </summary>
    public void BindHangSanXuat()
    {
        TList<DmHangSanXuat> ListHang = ProviderFactory.DmHangSanXuatProvider.GetAll();
        ddlHangSanXuat.DataSource = ListHang;
        ddlHangSanXuat.DataTextField = "TenHangSanXuat";
        ddlHangSanXuat.DataValueField = "id";
        ddlHangSanXuat.DataBind();
        ddlHangSanXuat.Items.Insert(0, new ListItem("-- tất cả --", "0"));

    }
    /// <summary>
    /// 
    /// </summary>
    public void BindListTieuChuanApDung()
    {
        DataTable ListTieuChuan = ProviderFactory.DmTieuChuanProvider.Search("","Matieuchuan",0,0);
        chklstTieuchuan.DataSource = ListTieuChuan;
        chklstTieuchuan.DataTextField = "Matieuchuan";
        chklstTieuchuan.DataValueField = "id";
        chklstTieuchuan.DataBind();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSanPham_Sorting(object sender, GridViewSortEventArgs e)
    {
        getWhereClause();
        bindingGridView(mWhereClause);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>    
    public void GetThongTinTimKiem()
    {
        ThongTinTk.SoGCN = txtSoGCN.Text.Trim();
        ThongTinTk.DonViNop = ddlDonviNop.SelectedValue;
        ThongTinTk.kyhieu = txtKyHieu.Text;
        ThongTinTk.TenSanPham = ddlSanPham.SelectedValue;
        ThongTinTk.NgayCapTu = txtNgayCap.Text;
        ThongTinTk.NgayCapDen = txtNgayCapDen.Text;
        //ThongTinTk.NgayHetHanTu = txtngayHethan.Text;
        //ThongTinTk.NgayHetHanDen = txtngayHethanDen.Text;
        ThongTinTk.HangSanXuat = ddlHangSanXuat.SelectedValue;
        string temp = string.Empty;
        foreach (ListItem item in chklstTieuchuan.Items)
        {
            if (item.Selected)
            {

                temp += item.Value + ",";
            }
        }
        if (temp.Length > 0)
            temp.Remove(temp.LastIndexOf(','), 1);
        ThongTinTk.TieuChuanApdung = temp;      
        Session["ThongTinTimKiem"] = ThongTinTk;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>    
    public void SetBackThongTinTimKiem()
    {
        txtSoGCN.Text = string.Empty;
        txtKyHieu.Text = string.Empty;
        ddlDonviNop.SelectedIndex = 0;
        ddlSanPham.SelectedIndex = 0;
        ddlHangSanXuat.SelectedIndex = 0;
        txtNgayCap.Text = string.Empty;
        txtNgayCapDen.Text = string.Empty;
        //txtngayHethan.Text = string.Empty;
        //txtngayHethanDen.Text = string.Empty;
        chklstTieuchuan.ClearSelection();
        if (Session["ThongTinTimKiem"] != null)
        {
            ThongTinTk = (ThongTinTimkiem)Session["ThongTinTimKiem"];
            txtSoGCN.Text = ThongTinTk.SoGCN;
            txtKyHieu.Text = ThongTinTk.kyhieu;
            ddlDonviNop.SelectedValue = ThongTinTk.DonViNop ;
            ddlSanPham.SelectedValue = ThongTinTk.TenSanPham;
            txtNgayCap.Text = ThongTinTk.NgayCapTu;
            txtNgayCapDen.Text = ThongTinTk.NgayCapDen;
            //txtngayHethan.Text = ThongTinTk.NgayHetHanTu;
            //txtngayHethanDen.Text = ThongTinTk.NgayHetHanDen;
            ddlHangSanXuat.SelectedValue = ThongTinTk.HangSanXuat;
            if (ThongTinTk.TieuChuanApdung.Length > 0)
            {
                String[] arr = ThongTinTk.TieuChuanApdung.Split(',');
                foreach (ListItem item in chklstTieuchuan.Items)
                    if (Array.IndexOf(arr, item.Value) >= 0)
                        item.Selected = true;
            }
            
        }
        
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        getWhereClause();
        int LoaiHoso = Convert.ToInt32(ddlLoaiHinhChungNhan.SelectedValue);
        DataTable dths = ProviderFactory.SanPhamProvider.GetToExcel(mWhereClause, gvSanPham.OrderBy);       
        if (dths == null || dths.Rows.Count == 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Không có dữ liệu');</script>");
            return;
        }
        dths.Columns["Sohoso"].Caption = "Số hồ sơ";
        dths.Columns["SoGCN"].Caption = "Số giấy chứng nhận";
        dths.Columns["SoTBLP"].Caption = "Số TBLP";
        dths.Columns["TenTiengViet"].Caption = "Tên sản phẩm";
        dths.Columns["KyHieu"].Caption = "Ký hiệu";
        dths.Columns["TenHangSanXuat"].Caption = "Tên hãng sản xuất";
        dths.Columns["TrangThai"].Caption = "Trạng thái";
        dths.Columns["donvi"].Caption = "Đơn vị nộp hồ sơ";
        dths.Columns["manhomsanpham"].Caption = "Mã nhóm sản phẩm";
        dths.Columns["sobantiepnhanCB"].Caption = "Số bản tiếp nhận CB";
        dths.Columns["nguongoc"].Caption = "Nguồn gốc";
        dths.Columns["TieuChuanApdung"].Caption = "Tiêu chuẩn áp dụng";
        dths.Columns["NgayKyDuyet"].Caption = "Ngày ký duyệt";
        dths.Columns["ngayhethan"].Caption = "Ngày hết hạn";        
       // Cuc_QLCL.ExcelHelper.ExportData(dths, "TraCuuGCN.xls", this);
        Cuc_QLCL.ExcelHelper.ToExcel(dths, "", "Danh sách giấy chứng nhận", "TraCuuGCN.xls", Page.Response);
        //Cuc_QLCL.ExcelHelper.SendFile(dths, "TraCuuGCN.xls", this);
    }
   
}
