using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
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

public partial class WebUI_NhapLieu_CB_HoSoQuanLy : PageBase
{
    struct ThongTinTimkiem
    {
        public string Sohoso;
        public string donvinop;
        public string nguoinhan;
        public string nguoixuly;
        public string ngaybatdau;
        public string ngayketthuc;
    }
    List<string> deleteSuccess = new List<string>();
    List<string> deleteFail = new List<string>();
    string TrangThaiID = "";
    string mWhereClause = "";
    ThongTinTimkiem ThongTinTk = new ThongTinTimkiem();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_ddlDonViNop();
            Bind_ddlNguoiNhan();
            Bind_ddlNguoiXuly();
            string WhereClause = " AND hs.TrangThaiId = " + ((int)EnTrangThaiHoSoList.DA_DONG).ToString();

            // Nếu người dùng là giám đốc trung tâm thì cho phép sửa tất cả các loại hồ sơ
            if (mUserInfo.UserID != mUserInfo.GiamDoc.Id)
                WhereClause += (" AND hs.HoSoMoi = 0 ");

            Bind_HoSoFollowConditions(WhereClause);
            Session["ThongTinTimKiem"] = null;
        }

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
        string WhereClause = string.Empty;
        if (txtSoHS.Text.Trim().Length > 0)
            WhereClause += " AND hs.sohoso LIKE N'%" + txtSoHS.Text.Trim().Replace("'", "") + "%'";
        if (ddlDonViNop.SelectedIndex > 0)
            WhereClause += " AND hs.donviid ='" + ddlDonViNop.SelectedValue + "'";
        if (ddlNguoiTiepNhan.SelectedIndex > 0)
            WhereClause += " AND hs.nguoitiepnhanID ='" + ddlNguoiTiepNhan.SelectedValue + "'";
        if (ddlNguoiXuLy.SelectedIndex > 0)
            WhereClause += " AND pc.nguoixuly='" + ddlNguoiXuLy.SelectedValue + "'";
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

        WhereClause += " AND hs.TrangThaiId = " + ((int)EnTrangThaiHoSoList.DA_DONG).ToString();

        // Nếu người dùng là giám đốc trung tâm thì cho phép sửa tất cả các loại hồ sơ
        if (mUserInfo.UserID != mUserInfo.GiamDoc.Id)
            WhereClause += (" AND hs.HoSoMoi = 0 ");

        return WhereClause;

    }
    /// <summary>
    /// Đổ dữ liệu vào grid view theo điều kiện tìm kiếm
    /// </summary>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    public void Bind_HoSoFollowConditions(string whereClause)
    {
        //goi ham lay du lieu voi tham so truyen vao la dieu kien cua nguoi nhap tu cac o nhap
        DataTable dtHosos = ProviderFactory.HoSoProvider.GetByWhereClause(whereClause, (int)LoaiHoSo.CongBoHopQuy, gvHoSo.OrderBy, gvHoSo.PageIndex + 1, gvHoSo.PageSize);
        gvHoSo.DataSource = dtHosos;
        if (dtHosos.Rows.Count > 0)
            gvHoSo.VirtualItemCount = int.Parse(dtHosos.Rows[0]["TongSoBanGhi"].ToString());
        gvHoSo.DataBind();
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
        if (Session["ThongTinTimKiem"] != null)
        {
            ThongTinTk = (ThongTinTimkiem)Session["ThongTinTimKiem"];
            txtSoHS.Text = ThongTinTk.Sohoso;
            ddlDonViNop.SelectedValue = ThongTinTk.donvinop;
            ddlNguoiTiepNhan.SelectedValue = ThongTinTk.nguoinhan;
            ddlNguoiXuLy.SelectedValue = ThongTinTk.nguoixuly;
            txtNgayNhanTu.Text = ThongTinTk.ngaybatdau;
            txtNgayNhanDen.Text = ThongTinTk.ngayketthuc;
        }
    }

    protected void lnkThemMoi_Click(object sender, EventArgs e)
    {
        Response.Redirect("NhapLieu_CB_HoSoChitiet.aspx");
    }
    protected void lnkXoa_Click(object sender, EventArgs e)
    {
        DeleteHoSo();
    }
    protected void ImgBtnXoa_Click(object sender, ImageClickEventArgs e)
    {
        DeleteHoSo();
    }
    protected void imgButtonAdd_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("NhapLieu_CB_HoSoChitiet.aspx");
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
        DataTable dt1 = ProviderFactory.HoSoProvider.LayDonViNop((int)LoaiHoSo.CongBoHopQuy);
        ddlDonViNop.DataSource = dt1;
        ddlDonViNop.DataTextField = "Tentiengviet";
        ddlDonViNop.DataValueField = "id";
        ddlDonViNop.DataBind();
        ListItem item = new ListItem("--tất cả--", "0");
        ddlDonViNop.Items.Insert(0, item);
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

    private void DeleteHoSo()
    {
        // Thực hiện xoá
        foreach (GridViewRow row in gvHoSo.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkCheck");
            if (chk.Checked)
            {
                string hoSoId = gvHoSo.DataKeys[row.RowIndex][0].ToString();
                //kiem tra trang thai cua HS, chỉ có thể xóa HS mới
                int trangThaiId = Convert.ToInt32(gvHoSo.DataKeys[row.RowIndex][1].ToString());
                string soHoSo = gvHoSo.DataKeys[row.RowIndex][2].ToString();
                TransactionManager transaction = ProviderFactory.Transaction;
                try
                {
                    ProviderFactory.SanPhamProvider.XoaSanPhamTheoMaHoSo(hoSoId, transaction);
                    ProviderFactory.PhanCongProvider.Delete(transaction, ProviderFactory.PhanCongProvider.GetByHoSoId(hoSoId)[0]);
                    ProviderFactory.HoSoProvider.Delete(transaction, hoSoId);

                    transaction.Commit();
                    deleteSuccess.Add(soHoSo);
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                    {
                        switch (ex.Errors[0].Number)
                        {
                            case 547: // Foreign Key violation
                                deleteFail.Add(soHoSo);
                                break;
                            default:
                                deleteFail.Add(soHoSo);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    deleteFail.Add(soHoSo);
                    //throw ex;
                }
                finally
                {
                    transaction.Dispose();
                }

            }
        }

        // Đưa ra thông báo thành công và ghi log sự kiện
        string thongbao = "";
        if (deleteSuccess.Count > 0)
        {
            thongbao = String.Format(Resources.Resource.msgXoaHoSo, string.Join(",", deleteSuccess.ToArray()));
            ProviderFactory.SysLogProvier.Write(((PageBase)this.Page).mUserInfo, SysEventList.CB_HO_SO_XOA, thongbao);
        }
        if (deleteFail.Count > 0)
        {
            thongbao += String.Format(Resources.Resource.msgXoaHoSoThatBai, string.Join(",", deleteFail.ToArray()));
        }

        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('" + thongbao + "');location.href='NhapLieu_CB_HoSoQuanLy.aspx?UserControl=CB_HoSoDen';</script>");
    }
}
