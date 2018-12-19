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
using Cuc_QLCL.Entities;
using Cuc_QLCL.AdapterData;
using CucQLCL.Common;
using Cuc_QLCL;

/// <summary>
/// Modified    : NguyenQuy GetThongBaoLePhiByClause
/// Date        : 13/May/2009
/// Purpose     : find ho so trong database voi cac dieu kien: so Ho so, don vi nop ho so, trang thai ho so, nguoi tiep nhan, nguoi xu ly, ngay nhan
/// </summary>
public partial class WebUI_CB_TraCuuPHTC : PageBase
{
    struct ThongTinTimkiem
    {
        public string Sohoso;
        public string donvinop;
        public string nguoinhan;
        public string nguoixuly;
        public string trangthai;
        public string ngaybatdau;
        public string ngayketthuc;
    }
    string mWhereClause = "";
    ThongTinTimkiem ThongTinTk = new ThongTinTimkiem();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindNguoiXuly();
            bindDonViNop();
            bindNguoiTiepNhan();
            Bind_ddlSanPham();
            // Nếu form tra cứu được link sang từ form tra cứu giấy chứng nhận
            if (Request["sohoso"] != null)
            {
                txtSoHS.Text = Request["sohoso"].ToString();
                mWhereClause = " AND hs.sohoso LIKE N'%" + txtSoHS.Text + "%'";
            }
            bindingGridView(mWhereClause);
            Session["ThongTinTimKiem"] = null;
        }
        //gvHoSo.DataSource = dtSanPham;
        //gvHoSo.DataBind();

    }
    
   
    /// <summary>
    /// Modified by : NguyenQuy
    /// Date        : May 13, 2009
    /// Purpose     : Binding du cho combo nguoi xu ly
    /// </summary>
    /// <returns></returns>
    private void bindNguoiXuly()
    {
        //ddlNguoiXuly.DataSource = ProviderFactory.HoSoProvider.LayNguoiTiepNhan((int)LoaiHoSo.CongBo);
        ddlNguoiXuly.DataSource = ProviderFactory.SysUserProvider.GetAll();
        ddlNguoiXuly.DataTextField = "fullName";
        ddlNguoiXuly.DataValueField = "id";
        ddlNguoiXuly.DataBind();
        ddlNguoiXuly.Items.Insert(0, new ListItem("-- tất cả --", "0"));
    }
    /// <summary>
    /// Modified by : NguyenQuy
    /// Date        : May 13, 2009
    /// Purpose     : Binding du cho combo Don vi nop ho so
    /// </summary>
    /// <returns></returns>
    private void bindDonViNop()
    {
        ddlDonVi.DataSource = ProviderFactory.HoSoProvider.LayDonViNop((int)LoaiHoSo.CongBoHopQuy);
        ddlDonVi.DataTextField = "TenTiengViet";
        ddlDonVi.DataValueField = "id";
        ddlDonVi.DataBind();
        ddlDonVi.Items.Insert(0, new ListItem("-- tất cả --", "0"));
    }

    /// <summary>
    /// Đổ dữ liệu cho droplist tên sản phẩm
    /// </summary>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    public void Bind_ddlSanPham()
    {
        TList<DmSanPham> lstSanPham = ProviderFactory.DmSanPhamProvider.GetAll();
        cboTenSanPham.DataSource = lstSanPham;
        cboTenSanPham.DataTextField = "Tentiengviet";
        cboTenSanPham.DataValueField = "Id";
        cboTenSanPham.DataBind();
        ListItem item = new ListItem("--tất cả--", "0");
        cboTenSanPham.Items.Insert(0, item);
    }
    /// <summary>
    /// Modified by : NguyenQuy
    /// Date        : May 13, 2009
    /// Purpose     : Binding du cho combo nguoi tiep nhan
    /// </summary>
    /// <returns></returns>
    private void bindNguoiTiepNhan()
    {
        //DataTable dt = ProviderFactory.HoSoProvider.LayNguoiTiepNhan((int)LoaiHoSo.CongBo);
        ddlNguoiTiepNhan.DataSource = ProviderFactory.SysUserProvider.GetAll();
        ddlNguoiTiepNhan.DataTextField = "FullName";
        ddlNguoiTiepNhan.DataValueField = "id";
        ddlNguoiTiepNhan.DataBind();
        ddlNguoiTiepNhan.Items.Insert(0, new ListItem("-- tất cả --", "0"));
    }

    /// <summary>
    /// Hien thi danh sach TBLP
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvHoSo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int count = 100;
            string dsId = DataBinder.Eval(e.Row.DataItem, "DanhSachTBLP").ToString();
            string dsSoTBLP = DataBinder.Eval(e.Row.DataItem, "DanhSachSoGiayTBLP").ToString();
            if (dsId.Length > 0)
            {
                string[] arrId = dsId.Split(',');
                string[] arrSoTBLP = dsSoTBLP.Split(',');

                for (int i = 0; i < arrId.Length - 1; i++)
                {
                    LinkButton lnk = new LinkButton();
                    lnk.OnClientClick = "return popCenter('TestBaoCao.aspx?ThongBaoLePhiID=" + arrId[i] + "&action=tracuu','CN_ThuPhi',800,600);";
                    if (i == arrId.Length - 2)
                        lnk.Text = arrSoTBLP[i] + " ";
                    else
                        lnk.Text = arrSoTBLP[i] + "; ";
                    e.Row.Cells[5].Controls.Add(lnk);
                }
            }
        }
    }
    /// <summary>
    /// Modified by : NguyenQuy
    /// Date        : May 13, 2009
    /// Purpose     : Binding data cho gridview voi dieu kien tim kiem nhap vao tu cac o nhap
    /// </summary>
    private void bindingGridView(string whereClause)
    {
        //goi ham lay du lieu voi tham so truyen vao la dieu kien cua nguoi nhap tu cac o nhap
        DataTable dtHosos = ProviderFactory.HoSoProvider.GetByWhereClause(whereClause, (int)LoaiHoSo.CongBoHopQuy, gvHoSo.OrderBy, gvHoSo.PageIndex + 1, gvHoSo.PageSize);
        gvHoSo.DataSource = dtHosos;
        if (dtHosos.Rows.Count > 0)
            gvHoSo.VirtualItemCount = int.Parse(dtHosos.Rows[0]["TongSoBanGhi"].ToString());
        gvHoSo.DataBind();
        if (gvHoSo.Rows.Count > 0)
        {
            btnExcel.ForeColor = System.Drawing.Color.Black;
            btnExcel.Enabled = true;
        }
        else
        {
            btnExcel.ForeColor = System.Drawing.Color.Silver;
            btnExcel.Enabled = false;

        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvHoSo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHoSo.PageIndex = e.NewPageIndex;
        //set condition of portfolio
        SetBackThongTinTimKiem();
        mWhereClause= getWhereClause();
        //goi ham bindinggrid
        bindingGridView(mWhereClause);
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
        gvHoSo.PageIndex = 0;
        //set condition of portfolio
        mWhereClause= getWhereClause();
        //goi ham bindinggrid
        bindingGridView(mWhereClause);
    }
    /// <summary>
    /// Modified by : NguyenQuy
    /// Date        : May 14, 2009
    /// Purpose     : dat dieu kien cho cac thong so tim kiem ho so
    /// </summary>
    private string getWhereClause()
    {
        string WhereClause = string.Empty;
        string TrangThai = string.Empty;
        

        if (!txtSoHS.Text.Trim().Equals(""))
            WhereClause += " AND hs.SoHoSo LIKE N'%" + txtSoHS.Text.Trim().Replace("'", "") + "%'";
        if (!ddlNguoiXuly.SelectedValue.Equals("0"))
            WhereClause += " AND pc.nguoixuly ='" + ddlNguoiXuly.SelectedValue + "'";
        if (!ddlNguoiTiepNhan.SelectedValue.Equals("0"))
            WhereClause += " AND hs.nguoitiepnhanid ='" + ddlNguoiTiepNhan.SelectedValue + "'";
        if (!ddlDonVi.SelectedValue.Equals("0"))
            WhereClause += " AND hs.DonViID ='" + ddlDonVi.SelectedValue + "'";

        if (txtNgayNhanTu.Text.Length > 0)
        {
            if (txtNgayNhanDen.Text.Length == 0)
                WhereClause += " AND  DATEADD(dd, 0, DATEDIFF(dd, 0, hs.NgayTiepNhan)) >='" + ConvertDateFormat(txtNgayNhanTu.Text) + "'";
            else
                WhereClause += " AND  DATEADD(dd, 0, DATEDIFF(dd, 0, hs.NgayTiepNhan)) BETWEEN '" + ConvertDateFormat(txtNgayNhanTu.Text) + "' AND '" + ConvertDateFormat(txtNgayNhanDen.Text) + "'";
        }
        else if (txtNgayNhanDen.Text.Length > 0)
        {
            WhereClause += " AND   DATEADD(dd, 0, DATEDIFF(dd, 0, hs.NgayTiepNhan)) <= '" + ConvertDateFormat(txtNgayNhanDen.Text) + "'";
        }

        if (chkAllTrangThai.Checked)
        {
            foreach (ListItem item in chklstTrangThai.Items)
            {
                if (item.Selected)
                {

                    TrangThai += "'" + item.Value + "',";
                }
            }

            if (TrangThai.Length > 0)
            {
                TrangThai = TrangThai.Remove(TrangThai.Length - 1, 1);
                WhereClause += " AND TrangThaiID IN(" + TrangThai + ")";
            }
        }

        // Tim kiem theo san pham
        if (cboTenSanPham.SelectedIndex > 0)
            WhereClause += " AND hs.Id IN (SELECT HoSoId FROM SanPham WHERE SanPhamId = '" + cboTenSanPham.SelectedValue + "')";
        if (!string.IsNullOrEmpty(txtKyHieuSanPham.Text))
            WhereClause += " AND hs.Id IN (SELECT HoSoId FROM SanPham WHERE KyHieu Like '%" + txtKyHieuSanPham.Text + "%')";

        return WhereClause;
    }
    /// <summary>
    /// Modified by : NguyenQuy
    /// Date        : May 14, 2009
    /// Purpose     : xuat ra excel file theo dieu kien tra cuu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        mWhereClause= getWhereClause();
        DataTable dths = ProviderFactory.HoSoProvider.GetHoSoByConditions(mWhereClause, (int)LoaiHoSo.CongBoHopQuy);
        if (dths == null || dths.Rows.Count == 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Không có dữ liệu');</script>");
            return;
        }
        if (dths.Rows.Count>=65000)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dữ liệu quá lớn.');</script>");
            return;
        }
        dths.Columns["Sohoso"].Caption = "Số hồ sơ";
        dths.Columns["SOCONGVANDEN"].Caption = "Số công văn đến";
        dths.Columns["SOCONGVANDONVI"].Caption = "Số công văn đơn vị";
        dths.Columns["DONVI"].Caption = "Đơn vị gửi";
        dths.Columns["NGAYTIEPNHAN"].Caption = "Ngày nhận";
        dths.Columns["NGUOITIEPNHAN"].Caption = "Người nhận";
        dths.Columns["NGUOINOPHOSO"].Caption = "Người nộp";
        dths.Columns["DIENTHOAI"].Caption = "Điện thoại";
        dths.Columns["EMAIL"].Caption = "Email";
        dths.Columns["FAX"].Caption = "Fax";
        dths.Columns["NGUOIXULY"].Caption = "Người xử lý";
        dths.Columns["NGUONGOC"].Caption = "Nguồn gốc";
        dths.Columns["HINHTHUC"].Caption = "Hình thức";
        dths.Columns["TRANGTHAI"].Caption = "Trạng thái";
        dths.Columns["NGUOIXULY"].Caption = "Người xử lý";
        dths.Columns["BANCONGBO"].Caption = "Công bố";
        dths.Columns["BANTUDANHGIA"].Caption = "Tự đánh giá";
        dths.Columns["TUCACHPHAPNHAN"].Caption = "Tư cách pháp nhân";
        dths.Columns["KETQUADOKIEM"].Caption = "Kết quả đo kiểm";
        dths.Columns["TAILIEUKYTHUAT"].Caption = "Tài liệu kỹ thuật";
        dths.Columns["KETQUADOKIEM"].Caption = "Kết quả đo kiểm";
        dths.Columns["QUYTRINHSANXUAT"].Caption = "Quy trình sản xuất";
        dths.Columns["QUYTRINHDAMBAO"].Caption = "Quy trình đảm bảo chất lượng";
        dths.Columns["CHUNGCHIHTQLCL"].Caption = "Chứng chỉ HT QLCL";
        dths.Columns["TIEUCHUANAPDUNG"].Caption = "Tiêu chuẩn tự nguyện áp dụng";
        dths.Columns["HOSODAYDU"].Caption = "Hồ sơ đầy đủ";
        dths.Columns["TAILIEUKHAC"].Caption = "Tài liệu khác";
        dths.Columns["LUUY"].Caption = "Lưu ý";
        dths.Columns["NGAYLUUTRU"].Caption = "Ngày lưu trữ";
        dths.Columns["SOLUUTRU"].Caption = "Số lưu trữ";
        dths.Columns["NOILUUTRU"].Caption = "Nơi lưu trữ";
        dths.Columns["GHICHULUUTRU"].Caption = "Ghi chú lưu trữ";
       
       // ExcelHelper.ExportData(dths, "Danhsachhoso.xls", this);
        ExcelHelper.ToExcel(dths, "id,trangthaiid,nguongoc,hinhthuc", "Danh sách hồ sơ Công bố hợp quy", "Danhsachhoso.xls", Page.Response);
    }
    /// <summary>
    /// Chuyển đổi ngày từ kiểu dd/mm/yyyy sang kiểu mm/dd/yyyy
    /// </summary>
    /// <param name="date"></param>
    /// <param name="oldFormatdate"></param>
    /// <param name="newFormatdate"></param>
    /// <returns></returns>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    public string ConvertDateFormat(string date)
    {
        string s = date;
        string d, m, y;
        d = s.Substring(0, s.IndexOf("/"));
        s = s.Substring(s.IndexOf("/") + 1, s.Length - s.IndexOf("/") - 1);
        m = s.Substring(0, s.IndexOf("/"));
        s = s.Substring(s.IndexOf("/") + 1, s.Length - s.IndexOf("/") - 1);
        y = s;
        s = m + "/" + d + "/" + y;
        return s;
    }
    protected void gvHoSo_Sorting(object sender, GridViewSortEventArgs e)
    {
        mWhereClause= getWhereClause();
        bindingGridView(mWhereClause);
    }
    protected void chkAllTrangThai_CheckedChanged(object sender, EventArgs e)
    {
        if (!chkAllTrangThai.Checked)
            chklstTrangThai.Enabled = false;
        else
            chklstTrangThai.Enabled = true;
    }
    /// <summary>
    /// 
    /// </summary>
    public void GetThongTinTimKiem()
    {
        ThongTinTk.Sohoso = txtSoHS.Text.Trim();
        ThongTinTk.nguoinhan = ddlNguoiTiepNhan.SelectedValue;
        ThongTinTk.nguoixuly = ddlNguoiXuly.SelectedValue;
        ThongTinTk.donvinop = ddlDonVi.SelectedValue;
        ThongTinTk.ngaybatdau = txtNgayNhanTu.Text;
        ThongTinTk.ngayketthuc = txtNgayNhanDen.Text;
        if (chkAllTrangThai.Checked)
        {
            string temp = string.Empty;
            foreach (ListItem item in chklstTrangThai.Items)
            {
                if (item.Selected)
                {

                    temp += item.Value + ",";
                }
            }
            if (temp.Length > 0)
                temp.Remove(temp.LastIndexOf(','), 1);
            ThongTinTk.trangthai = temp;
        }
        else
            ThongTinTk.trangthai = string.Empty;
        Session["ThongTinTimKiem"] = ThongTinTk;
    }
    /// <summary>
    /// 
    /// </summary>
    public void SetBackThongTinTimKiem()
    {
        if (Session["ThongTinTimKiem"] != null)
        {
            ThongTinTk = (ThongTinTimkiem)Session["ThongTinTimKiem"];
            txtSoHS.Text = ThongTinTk.Sohoso;
            ddlDonVi.SelectedValue = ThongTinTk.donvinop;
            ddlNguoiTiepNhan.SelectedValue = ThongTinTk.nguoinhan;
            ddlNguoiXuly.SelectedValue = ThongTinTk.nguoixuly;
            txtNgayNhanTu.Text = ThongTinTk.ngaybatdau;
            txtNgayNhanDen.Text = ThongTinTk.ngayketthuc;
            if (ThongTinTk.trangthai.Length > 0)
            {
                chkAllTrangThai.Checked = true;
                String[] arr = ThongTinTk.trangthai.Split(',');
                foreach (ListItem item in chklstTrangThai.Items)
                    if (Array.IndexOf(arr, item.Value) >= 0)
                        item.Selected = true;
            }
        }
        else
        {

            txtSoHS.Text = string.Empty;
            txtNgayNhanTu.Text = string.Empty;
            txtNgayNhanDen.Text = string.Empty;
            ddlDonVi.SelectedIndex = 0;
            ddlNguoiTiepNhan.SelectedIndex = 0;
            ddlNguoiXuly.SelectedIndex = 0;
            chkAllTrangThai.Checked = false;
        }
    }
}
