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
using CucQLCL.Common;
using Cuc_QLCL.Entities;
using Cuc_QLCL.AdapterData;
using Cuc_QLCL;
/// <summary>
/// Modified    : NguyenQuy
/// Date        : 15/May/2009
/// Purpose     : tra cuu thu phi theo: so thong bao phi, don vi nop ho so, trang thai, ngay duyet
/// </summary>
public partial class WebUI_CN_TraCuuThuPhi : PageBase
{
    struct ThongTinTimkiem
    {
        public string SoThongBaoLePhi;
        public string donvinop;
        public string ngayDuyetTu;
        public string ngayDuyetDen;
    }
    ThongTinTimkiem ThongTinTk = new ThongTinTimkiem();
    string mWhereClause = " WHERE tb.TrangThaiID IN (" + ((int)EnTrangThaiThongBaoPhiList.DA_THU_PHI).ToString()
                                                       + ", " + ((int)EnTrangThaiThongBaoPhiList.HUY).ToString()
                                                       + ", " + ((int)EnTrangThaiThongBaoPhiList.CHO_THU_PHI).ToString() + ")";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_ddlLoaiHinhChungNhan();
            bindDonViNop();

            // Nếu form tra cứu được link sang từ form tra cứu giấy chứng nhận
            if (Request["sotblp"] != null)
            {
                txtSoHS.Text = Request["sotblp"].ToString();
                mWhereClause += " AND tb.SoGiayThongBaoLePhi Like N'%" + txtSoHS.Text.Trim().Replace("'", "") + "%'";
            }
            // Loc theo trung tam
            mWhereClause += " AND (PATINDEX('%" + mUserInfo.MaTrungTam + "%',tb.ID) > 0)";
            bindingGridView(mWhereClause);
            Session["ThongTinTimKiem"] = null;

            if (mUserInfo.IsThanhTra)
            {
                gvPhi.Columns[2].Visible = false;
                gvPhi.Columns[8].Visible = false;
                gvPhi.Columns[9].Visible = false;
                btnExcel.Visible = false;
            }
        }
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
        DataTable dtLoaiPhi = new DataTable();
        dtLoaiPhi.Columns.Add("Text");
        dtLoaiPhi.Columns.Add("Value");

        dtLoaiPhi.Rows.Add("-- Tất cả --", "-1");
        dtLoaiPhi.Rows.Add("Lệ phí cấp Giấy chứng nhận hợp Quy", ((int)EnLoaiPhiList.PHI_CHUNG_NHAN_HQ).ToString());
        dtLoaiPhi.Rows.Add("Lệ phí cấp Giấy chứng nhận hợp chuẩn", ((int)EnLoaiPhiList.PHI_CHUNG_NHAN_HC).ToString());
        dtLoaiPhi.Rows.Add("CNHC - Phí lấy mẫu sản phẩm và đánh giá quy trình SX", "0");
        dtLoaiPhi.Rows.Add("CNHQ - Phí lấy mẫu sản phẩm và đánh giá quy trình SX", "10");
        dtLoaiPhi.Rows.Add("Lệ phí cấp Bản tiếp nhận công bố hợp quy", ((int)EnLoaiPhiList.PHI_CONG_BO_HQ).ToString());
        dtLoaiPhi.Rows.Add("Lệ phí giám sát chứng nhận hợp chuẩn", ((int)EnLoaiPhiList.PHI_DANH_GIA_LAI).ToString());

        ddlLoaiPhi.DataSource = dtLoaiPhi;
        ddlLoaiPhi.DataTextField = "Text";
        ddlLoaiPhi.DataValueField = "Value";
        ddlLoaiPhi.DataBind();
    }

    /// <summary>
    /// Modified by : NguyenQuy
    /// Date        : May 15, 2009
    /// Purpose     : Binding du cho combo Don vi nop ho so
    /// </summary>
    /// <returns></returns>
    private void bindDonViNop()
    {
        DataTable dt = ProviderFactory.HoSoProvider.LayDonViNop((int)LoaiHoSo.ChungNhanHopQuy);
        dt.Merge(ProviderFactory.HoSoProvider.LayDonViNop((int)LoaiHoSo.ChungNhanHopChuan));
        ddlDonVi.DataSource = dt;
        ddlDonVi.DataTextField = "TenTiengViet";
        ddlDonVi.DataValueField = "id";
        ddlDonVi.DataBind();
        ddlDonVi.Items.Insert(0, new ListItem("-- tất cả --", "0"));
    }

    /// <summary>
    /// Modified by : NguyenQuy
    /// Date        : May 13, 2009
    /// Purpose     : Binding data cho gridview voi dieu kien tim kiem nhap vao tu cac o nhap
    /// </summary>
    private void bindingGridView(string whereClause)
    {
        //goi ham lay du lieu voi tham so truyen vao la dieu kien cua nguoi nhap tu cac o nhap
        DataTable dttbPhis = ProviderFactory.ThongBaoLePhiProvider.GetThongBaoLePhiByClause(whereClause, gvPhi.OrderBy, gvPhi.PageIndex + 1, gvPhi.PageSize);
        gvPhi.DataSource = dttbPhis;
        if (dttbPhis.Rows.Count > 0)
            gvPhi.VirtualItemCount = int.Parse(dttbPhis.Rows[0]["TongSoBanGhi"].ToString());
        gvPhi.DataBind();
        if (gvPhi.Rows.Count > 0)
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
    /// Modified by : NguyenQuy
    /// Date        : May 15, 2009
    /// Purpose     : Get list values of Fee Ticket Status from Enum class
    ///             : Return a data table with 2 columns: StatusID, and StatusName - return a data table of All Fee ticket Status values in the system. (similar to the way EntityType list is built. )
    /// </summary>
    /// <returns></returns>
    public DataTable GetDatatableFromEnum()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("StatusID", typeof(string));
        dt.Columns.Add("StatusName", typeof(string));

        //add values and texts of enum to the datatable
        dt.Rows.Add((int)EnTrangThaiThongBaoPhiList.MOI_TAO, "Mới tạo");
        dt.Rows.Add((int)EnTrangThaiThongBaoPhiList.CHO_PHE_DUYET, "Chờ phê duyệt");
        dt.Rows.Add((int)EnTrangThaiThongBaoPhiList.CHO_THU_PHI, "Chờ thu phí");
        dt.Rows.Add((int)EnTrangThaiThongBaoPhiList.DA_THU_PHI, "Đã thu phí");
        dt.Rows.Add((int)EnTrangThaiThongBaoPhiList.HUY, "Huỷ");
        return dt;
    }
    /// <summary>
    /// Modified by : NguyenQuy
    /// Date        : May 14, 2009
    /// Purpose     : dat dieu kien cho cac thong so tim kiem ho so
    /// </summary>
    private void getWhereClause()
    {
        if (!txtSoHS.Text.Trim().Equals(""))
            mWhereClause += " AND tb.SoGiayThongBaoLePhi Like N'%" + txtSoHS.Text.Trim().Replace("'", "") + "%'";

        if (!ddlDonVi.SelectedValue.Equals("0"))
            mWhereClause += " AND tb.DonViID ='" + ddlDonVi.SelectedValue + "'";

        if (txtNgayDuyetTu.Text.Length > 0)
        {
            if (txtNgayDuyetDen.Text.Length == 0)
                mWhereClause += " AND  DATEADD(dd, 0, DATEDIFF(dd, 0, tb.NgayPheDuyet)) >='" + ConvertDateFormat(txtNgayDuyetTu.Text) + "'";
            else
                mWhereClause += " AND  DATEADD(dd, 0, DATEDIFF(dd, 0, tb.NgayPheDuyet)) BETWEEN '" + ConvertDateFormat(txtNgayDuyetTu.Text) + "' AND '" + ConvertDateFormat(txtNgayDuyetDen.Text) + "'";
        }
        else if (txtNgayDuyetDen.Text.Length > 0)
        {
            mWhereClause += " AND   DATEADD(dd, 0, DATEDIFF(dd, 0, tb.NgayPheDuyet)) <= '" + ConvertDateFormat(txtNgayDuyetDen.Text) + "'";
        }
        if (txtSoHoaDon.Text.Trim().Length > 0)
        {
            mWhereClause += "AND tb.SoHoaDon Like N'%" + txtSoHoaDon.Text.Trim() + "%'";
        }

        if (ddlLoaiPhi.SelectedValue != "-1")
        {
            if (ddlLoaiPhi.SelectedValue == "0")
                mWhereClause += " AND tb.LoaiPhiID IS NULL ";
            else
                mWhereClause += " AND tb.LoaiPhiID = " + ddlLoaiPhi.SelectedValue;
        }
        //Loc theo ma trung tam
        mWhereClause += " AND (PATINDEX('%" + mUserInfo.MaTrungTam + "%',tb.ID) > 0)";
    }
    /// <summary>
    /// Modified by : NguyenQuy
    /// Date        : May 14, 2009
    /// Purpose     : Tim thong bao le phi thoa man dieu kien nguoi dung truyen vao qua cac o nhap
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnTimKiem_Click(object sender, EventArgs e)
    {
        GetThongTinTimKiem();
        gvPhi.PageIndex = 0;
        //set condition of fee ticket
        getWhereClause();
        //goi ham bindinggrid
        bindingGridView(mWhereClause);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        getWhereClause();
        DataTable dttbPhis = ProviderFactory.ThongBaoLePhiProvider.GetThongBaoLePhiToExcel(mWhereClause);
        if (dttbPhis == null || dttbPhis.Rows.Count == 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Không có dữ liệu');</script>");
            return;
        }
        //tb.ID, tb.SoGiayThongBaoLePhi, tb.DonViID, tb.TongPhi, tb.NgayPheDuyet, 
        //tb.TrangThaiID, tt.MoTa, dv.TenTiengViet, dv.DiaChi, dv.DienThoai, dv.Fax,

        dttbPhis.Columns["SoGiayThongBaoLePhi"].Caption = "Số thông báo lệ phí";
        dttbPhis.Columns["TongPhi"].Caption = "Tổng lệ phí (VNĐ)";
        dttbPhis.Columns["SoHoaDon"].Caption = "Số hóa đơn";
        dttbPhis.Columns["NgayThuTien"].Caption = "Ngày thu tiền";
        dttbPhis.Columns["NgayPheDuyet"].Caption = "Ngày phê duyệt";
        dttbPhis.Columns["MoTa"].Caption = "Trạng Thái";
        dttbPhis.Columns["TenTiengViet"].Caption = "Đơn vị nộp hồ sơ";
        dttbPhis.Columns["DiaChi"].Caption = "Địa chỉ";
        dttbPhis.Columns["DienThoai"].Caption = "Điện thoại";
        dttbPhis.Columns["Fax"].Caption = "Fax";
        //ExcelHelper.ExportData(dttbPhis, "DanhsachThongbaoLP.xls", this);
        ExcelHelper.ToExcel(dttbPhis, "id,DonViID,TrangThaiID", "Danh sách Thông báo lệ phí", "DanhsachThongbaoLP.xls", Page.Response);

    }
    protected void ddlDonVi_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlTrangthai_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// Modified by : NguyenQuy
    /// Date        : May 15, 2009
    /// Purpose     : phan trang cua gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPhi_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SetBackThongTinTimKiem();
        gvPhi.PageIndex = e.NewPageIndex;
        getWhereClause();
        bindingGridView(mWhereClause);
    }
    protected void gvPhi_Sorting(object sender, GridViewSortEventArgs e)
    {
        getWhereClause();
        bindingGridView(mWhereClause);
    }
    /// <summary>
    /// 
    /// </summary>
    public void GetThongTinTimKiem()
    {
        ThongTinTk.SoThongBaoLePhi = txtSoHS.Text.Trim();
        ThongTinTk.donvinop = ddlDonVi.SelectedValue;
        ThongTinTk.ngayDuyetTu = txtNgayDuyetTu.Text;
        ThongTinTk.ngayDuyetDen = txtNgayDuyetDen.Text;
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
            txtSoHS.Text = ThongTinTk.SoThongBaoLePhi;
            ddlDonVi.SelectedValue = ThongTinTk.donvinop;
            txtNgayDuyetTu.Text = ThongTinTk.ngayDuyetTu;
            txtNgayDuyetDen.Text = ThongTinTk.ngayDuyetDen;

        }
        else
        {
            txtSoHS.Text = string.Empty;
            ddlDonVi.SelectedIndex = 0;
            txtNgayDuyetTu.Text = string.Empty;
            txtNgayDuyetDen.Text = string.Empty;
        }
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
    protected void gvPhi_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlImage lnkA = (HtmlImage)e.Row.FindControl("lnkInLePhi");
            if (mUserInfo.IsPermission(EnPermission.QUANLY_THUPHI) || mUserInfo.IsPermission(EnPermission.CB_PHE_DUYET))
            {

            }
            else
            {
                //lnkA.Style.Add("Display", "none");
                if (lnkA != null)
                {
                    lnkA.Visible = false;
                }
            }

            // Phân quyền theo chuyên viên và thanh tra, người dùng của cục            
            HtmlAnchor lnkSoTBLP = (HtmlAnchor)e.Row.FindControl("lnkSoTBLP");
            if (lnkSoTBLP != null)
            {
                if (mUserInfo.IsThanhTra)
                {
                    // Bỏ chức năng in giấy báo lệ phí
                    lnkSoTBLP.Attributes.Clear();
                }
                else
                {
                    string ID = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
                    lnkSoTBLP.Attributes.Add("onclick", "return popCenter('TestBaoCao.aspx?ThongBaoLePhiID=" + ID + "&action=tracuu','CN_ThuPhi',800,600);");
                }
            }
        }
    }
}
