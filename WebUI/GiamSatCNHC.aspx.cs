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
using Cuc_QLCL.Entities;
using CucQLCL.Common;
using System.Collections.Generic;

public partial class WebUI_GiamSatCNHC : PageBase
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

        WhereClause += "  AND hs.TrangThaiId = " + ((int)EnTrangThaiHoSoList.DA_DONG).ToString();

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
        DataTable dths = ProviderFactory.HoSoProvider.GetByWhereClause(WhereClause,(int)LoaiHoSo.ChungNhanHopChuan, "hs.STT desc ", gvHoSo.PageIndex + 1, gvHoSo.PageSize);
        gvHoSo.DataSource = dths;
        if (dths.Rows.Count > 0)
            gvHoSo.VirtualItemCount = int.Parse(dths.Rows[0]["TongSoBanGhi"].ToString());
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
    
}
