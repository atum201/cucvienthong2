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
using Cuc_QLCL;
using CucQLCL.Common;

public partial class WebUI_CN_TraCuuNguoiNop : PageBase
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            string SoHoSo = string.Empty;
            Bind_ddlLoaiHinhChungNhan();
            Bind_ddlDonViNop();
            Bind_ddlNguoiNhan();
            Bind_ddlNguoiXuly();
            Bind_ddlSanPham();

            // Nếu form tra cứu được link sang từ form tra cứu giấy chứng nhận
            if (Request["sohoso"] != null)
            {
                txtSoHS.Text = Request["sohoso"].ToString();
                SoHoSo = " AND hs.sohoso LIKE N'%" + txtSoHS.Text + "%'";
            }
            Bind_HoSoFollowConditions(SoHoSo);

            Session["ThongTinTimKiem"] = null;
        }
        // chkAllTrangThai.Attributes.Add("Onclick", "return Enable(this);return false;");
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
    protected void chkAllTrangThai_CheckedChanged(object sender, EventArgs e)
    {
        if (!chkAllTrangThai.Checked)
            chklstTrangThai.Enabled = false;
        else
            chklstTrangThai.Enabled = true;
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
    /// Đổ dữ liệu cho droplist đơn vị nộp hồ sơ
    /// </summary>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    public void Bind_ddlDonViNop()
    {
        DataTable dt1 = ProviderFactory.HoSoProvider.LayDonViNop((int)LoaiHoSo.ChungNhanHopQuy);
        DataTable dt2 = ProviderFactory.HoSoProvider.LayDonViNop((int)LoaiHoSo.ChungNhanHopChuan);
        dt1.Merge(dt2);
        ddlDonViNop.DataSource = dt1;
        ddlDonViNop.DataTextField = "Tentiengviet";
        ddlDonViNop.DataValueField = "id";
        ddlDonViNop.DataBind();
        ListItem item = new ListItem("--tất cả--", "0");
        ddlDonViNop.Items.Insert(0, item);
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
    /// Đổ dữ liệu cho droplist người nhận hồ sơ
    /// </summary>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    public void Bind_ddlNguoiNhan()
    {
        //DataTable dt = ProviderFactory.HoSoProvider.LayNguoiTiepNhan((int)LoaiHoSo.ChungNhan);
        ddlNguoiTiepNhan.DataSource = ProviderFactory.SysUserProvider.GetAll();
        ddlNguoiTiepNhan.DataTextField = "Fullname";
        ddlNguoiTiepNhan.DataValueField = "id";
        ddlNguoiTiepNhan.DataBind();
        ListItem item = new ListItem("--tất cả--", "0");
        ddlNguoiTiepNhan.Items.Insert(0, item);
    }
    /// <summary>
    /// Đổ dữ liệu cho droplist người xử lý
    /// </summary>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    public void Bind_ddlNguoiXuly()
    {
        //DataTable dt = ProviderFactory.HoSoProvider.LayNguoiXuLy((int)LoaiHoSo.ChungNhan);
        ddlNguoiXuLy.DataSource = ProviderFactory.SysUserProvider.GetAll();
        ddlNguoiXuLy.DataTextField = "Fullname";
        ddlNguoiXuLy.DataValueField = "id";
        ddlNguoiXuLy.DataBind();
        ListItem item = new ListItem("--tất cả--", "0");
        ddlNguoiXuLy.Items.Insert(0, item);
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
    protected void btnTimKiem_Click(object sender, EventArgs e)
    {
        GetThongTinTimKiem();
        gvHoSo.PageIndex = 0;
        mWhereClause = GetSearchConditions();
        Bind_HoSoFollowConditions(mWhereClause);
    }
    /// <summary>
    /// Lấy các điều kiện tìm kiếm
    /// </summary>
    /// <returns></returns>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    public string GetSearchConditions()
    {
        string TrangThai = string.Empty;
        string WhereClause = string.Empty;
        if (txtSoHS.Text.Trim().Length > 0)
            WhereClause += " AND hs.sohoso LIKE N'%" + txtSoHS.Text.Trim().Replace("'", "") + "%'";
        if (txtNguoiNop.Text.Trim().Length > 0)
            WhereClause += " AND hs.nguoinophoso LIKE N'%" + txtNguoiNop.Text.Trim().Replace("'", "") + "%'";
        if (ddlDonViNop.SelectedIndex > 0)
            WhereClause += " AND hs.donviid ='" + ddlDonViNop.SelectedValue.ToString().Trim() + "'";
        if (ddlNguoiTiepNhan.SelectedIndex > 0)
            WhereClause += " AND hs.nguoitiepnhanID ='" + ddlNguoiTiepNhan.SelectedValue.Trim() + "'";
        if (ddlNguoiXuLy.SelectedIndex > 0)
            WhereClause += " AND pc.nguoixuly='" + ddlNguoiXuLy.SelectedValue.Trim() + "'";
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
    /// Đổ dữ liệu vào grid view theo điều kiện tìm kiếm
    /// </summary>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    public void Bind_HoSoFollowConditions(string WhereClause)
    {
        //DataTable dths = ProviderFactory.HoSoProvider.GetHoSoByConditions(WhereClause, (int)LoaiHoSo.ChungNhan);
        //gvHoSo.DataSource = dths;
        //gvHoSo.DataBind();
        int LoaiHoso = Convert.ToInt32(ddlLoaiHinhChungNhan.SelectedValue);
        DataTable dths = ProviderFactory.HoSoProvider.GetByWhereClause(WhereClause, LoaiHoso, gvHoSo.OrderBy, gvHoSo.PageIndex + 1, gvHoSo.PageSize);
        //LongHH
        DataColumn nguoiThamDinh = dths.Columns.Add("NguoiThamDinh", typeof(String));
        for (int i = 0; i < dths.Rows.Count; i++) {
            string idd = dths.Rows[i]["ID"].ToString();
            DataTable dt = QLCL_Patch.GetDataByQuery(@"SELECT distinct u.Fullname FROM Sys_User u join phancong p on u.id=p.NguoiThamDinh join hoso h on p.hosoid=h.ID WHERE h.ID = '"+idd+"'");
            //DataTable dt = ProviderFactory.HoSoProvider.LayNguoiThamDinh(dths.Rows[i]["ID"].ToString(), LoaiHoso);
            if (dt != null && dt.Rows.Count > 0)
                dths.Rows[i]["NguoiThamDinh"] = dt.Rows[0][0].ToString();
            //else
            //    dths.Rows[i]["NguoiThamDinh"] = idd;
        }
        gvHoSo.DataSource = dths;
        if (dths.Rows.Count > 0)
            gvHoSo.VirtualItemCount = int.Parse(dths.Rows[0]["TongSoBanGhi"].ToString());
        //LongHH
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    protected void gvHoSo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHoSo.PageIndex = e.NewPageIndex;
        SetBackThongTinTimKiem();
        mWhereClause = GetSearchConditions();
        Bind_HoSoFollowConditions(mWhereClause);
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
        mWhereClause = GetSearchConditions();
        int LoaiHoso = Convert.ToInt32(ddlLoaiHinhChungNhan.SelectedValue);
        DataTable dths = ProviderFactory.HoSoProvider.GetHoSoByConditions(mWhereClause, LoaiHoso);
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
        dths.Columns["DENGHICN"].Caption = "Đề nghị chứng nhận";
        dths.Columns["TUCACHPHAPNHAN"].Caption = "Tư cách pháp nhân";
        dths.Columns["KETQUADOKIEM"].Caption = "Kết quả đo kiểm";
        dths.Columns["TAILIEUKYTHUAT"].Caption = "Tài liệu kỹ thuật";
        dths.Columns["QUYTRINHSANXUAT"].Caption = "Quy trình sản xuất";
        dths.Columns["QUYTRINHDAMBAO"].Caption = "Quy trình đảm bảo chất lượng";
        dths.Columns["CHUNGCHIHTQLCL"].Caption = "Chứng chỉ HT QLCL";
        dths.Columns["TIEUCHUANAPDUNG"].Caption = "Tiêu chuẩn tự nguyện áp dụng";
        dths.Columns["HOSODAYDU"].Caption = "Hồ sơ đầy đủ";
        dths.Columns["TAILIEUKHAC"].Caption = "Tài liệu khác";
        dths.Columns["LUUY"].Caption = "Lưu ý";
        dths.Columns["NGAYLUUTRU"].Caption = "Ngày lưu trữ";
        dths.Columns["SOLUUTRU"].Caption = "Sô lưu trữ";
        dths.Columns["NOILUUTRU"].Caption = "Nơi lưu trữ";
        dths.Columns["GHICHULUUTRU"].Caption = "Ghi chú lưu trữ";
        if (dths == null || dths.Rows.Count == 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Không có dữ liệu');</script>");
            return;
        }
       // Cuc_QLCL.ExcelHelper.ExportData(dths, "Danhsachhoso.xls", this);
       //ExcelHelper.SendFile(dths, "Danhsachhoso.xls", this);
        ExcelHelper.ToExcel(dths, "id,trangthaiid", "Danh sách hồ sơ chứng nhận", "Danhsachhoso.xls", Page.Response);

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    protected void gvHoSo_Sorting(object sender, GridViewSortEventArgs e)
    {
        mWhereClause = GetSearchConditions();
        Bind_HoSoFollowConditions(mWhereClause);
    }
    /// <summary>
    /// 
    /// </summary>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    public void GetThongTinTimKiem()
    {
        ThongTinTk.Sohoso = txtSoHS.Text.Trim();
        ThongTinTk.nguoinhan = ddlNguoiTiepNhan.SelectedValue;
        ThongTinTk.nguoixuly = ddlNguoiXuLy.SelectedValue;
        ThongTinTk.donvinop = ddlDonViNop.SelectedValue;
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
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    public void SetBackThongTinTimKiem()
    {
        txtSoHS.Text = string.Empty;
        txtNgayNhanTu.Text = string.Empty;
        txtNgayNhanDen.Text = string.Empty;
        ddlDonViNop.SelectedIndex = 0;
        ddlNguoiTiepNhan.SelectedIndex = 0;
        ddlNguoiXuLy.SelectedIndex = 0;
        chkAllTrangThai.Checked = false;
        if (Session["ThongTinTimKiem"] != null)
        {
            ThongTinTk = (ThongTinTimkiem)Session["ThongTinTimKiem"];
            txtSoHS.Text = ThongTinTk.Sohoso;
            ddlDonViNop.SelectedValue = ThongTinTk.donvinop;
            ddlNguoiTiepNhan.SelectedValue = ThongTinTk.nguoinhan;
            ddlNguoiXuLy.SelectedValue = ThongTinTk.nguoixuly;
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
    }

    /// <summary>
    /// Hiển thị danh sách thông báo lệ phí
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
                    //LongHH
                    //e.Row.Cells[6].Controls.Add(lnk);
                    e.Row.Cells[7].Controls.Add(lnk);
                    //LongHH
                }
            }
        }
    }
}
