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
using System.Text;

public partial class UserControls_uc_CN_HoSoSanPhamDaGui : System.Web.UI.UserControl
{
    private string HoSoId = string.Empty;
    private int LoaiHoSo = 0;
    private string strUserControl = string.Empty;
    private string strUserControlHS = string.Empty;
    /// <summary>
    /// Hiển thị thông tin
    /// </summary>
    ///<Modified>
    /// Author      Data        Comments    
    /// Truongtv    12/05/2009  TrườngTV
    /// Quannm      18/05/2009  Edited
    ///</Modified>
    protected void Page_Load(object sender, EventArgs e)
    {
        HoSoId = Request["HosoID"];
        LoaiHoSo = DataRepository.HoSoProvider.GetById(HoSoId).LoaiHoSo;
        strUserControl = Request["UserControl"];
        strUserControlHS = Request["UserControl"];
        if (!IsPostBack)
        {
            ManageBreadCum(HoSoId, strUserControl);
            BindChiTietHoSo();
            LayTatCaSanPhamTheoQuyenDangNhap();
            BindThongBaoPhiGrid();
        }
    }
    /// <summary>
    /// Hiển thị chi tiết hồ sơ
    /// </summary>
    ///<Modified>
    /// Author      Data            Comments    
    /// Truongtv    12/05/2009      Create new
    /// 
    ///</Modified>
    private void BindChiTietHoSo()
    {
        HoSo objHoSo = ProviderFactory.HoSoProvider.GetById(HoSoId);
        lblSoHoSo.Text = objHoSo.SoHoSo;
        lblNgayTiepNhan.Text = objHoSo.NgayTiepNhan != null ? ((DateTime)objHoSo.NgayTiepNhan).ToShortDateString() : string.Empty;
        SysUser user = ProviderFactory.SysUserProvider.GetById(objHoSo.NguoiTiepNhanId);
        if (user != null)
            lblNguoiTiepNhan.Text = user.FullName;
        lblSoCongVanDen.Text = objHoSo.SoCongVanDen;
        //LongHH
        //lblDonViNopHoSo.Text = ProviderFactory.DmDonViProvider.GetById(objHoSo.DonViId).TenTiengViet;
        DmDonVi dmDonVi = ProviderFactory.DmDonViProvider.GetById(objHoSo.DonViId);
        lblDonViNopHoSo.Text = dmDonVi.TenTiengViet;
        lblTenTiengAnh.Text = Server.HtmlEncode(dmDonVi.TenTiengAnh);
        lblDiaChi.Text = Server.HtmlEncode(dmDonVi.DiaChi);
        lblMaSoThue.Text = Server.HtmlEncode(dmDonVi.MaSoThue);
        //LongHH
        lblNguoiNop.Text = objHoSo.NguoiNopHoSo;
        lblDienThoai.Text = objHoSo.DienThoai;
        lblEmail.Text = objHoSo.Email;
        if (objHoSo.NhanHoSoTuId != null)
        {
            lblLoaiHinhGui.Text = EntityHelper.GetEnumTextValue((EnNhanHoSoTuList)objHoSo.NhanHoSoTuId);
        }
        lblTrangThai.Text = objHoSo.TrangThaiId + "" == "" ? "" : ProviderFactory.EnTrangThaiHoSoProvider.GetById((int)objHoSo.TrangThaiId).MoTa;
        if (objHoSo.NguonGocId != null)
        {
            lblNguonGoc.Text = EntityHelper.GetEnumTextValue((EnNguonGocList)objHoSo.NguonGocId);
        }
        DataTable nguoiTD = ProviderFactory.HoSoProvider.LayNguoiThamDinh(HoSoId, LoaiHoSo);
        lblNguoiThamDinh.Text = nguoiTD.Rows[0][1].ToString();
        lblLoaiHinhChungNhan.Text = objHoSo.LoaiHoSo == (int)CucQLCL.Common.LoaiHoSo.ChungNhanHopChuan ? "Chứng nhận hợp chuẩn" : "Chứng nhận hợp quy";
        lblYKienTiepNhan.Text = Server.HtmlEncode(objHoSo.Luuy);
    }

    /// <summary>
    /// Lấy danh sách sản phẩm theo quyền đăng nhập
    /// </summary>
    /// <Modified>
    /// Author      Date            Comments    
    /// Quannm      16/05/2009      Create new
    ///</Modified>
    private void LayTatCaSanPhamTheoQuyenDangNhap()
    {
        DataTable dtbSanPham = ProviderFactory.SanPhamProvider.LayTatCaSanPhamDaGuiTheoQuyenDangNhap(HoSoId, ((PageBase)this.Page).mUserInfo.UserID, ((PageBase)this.Page).mUserInfo.GetPermissionList("01"));
        gvSanPham.DataSource = dtbSanPham;
        gvSanPham.DataBind();
    }
    /// <summary>
    /// Danh sách thông báo phí của hồ sơ nếu có
    /// </summary>
    /// <Modified>
    /// Author      Date            Comments    
    /// Quannm      16/05/2009      Create new
    ///</Modified>
    private void BindThongBaoPhiGrid()
    {
        DataTable dtThongBaoPhi = ProviderFactory.ThongBaoLePhiProvider.GetThongBaoLePhiDaGuiByHoSoID(HoSoId, ((PageBase)this.Page).mUserInfo.GetPermissionList("01"), ((PageBase)this.Page).mUserInfo.UserID);
        gvPhi.DataSource = dtThongBaoPhi;
        gvPhi.DataBind();
    }
    protected void gvSanPham_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "CheckDaDoc")) == false)
            {
                e.Row.CssClass = "unread rowitem";
            }

            #region "Trang thai Cho xu ly"
            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiSanPhamList.CHO_XU_LY)
            {
                if (((PageBase)this.Page).mUserInfo.IsNguoiTiepNhan(HoSoId))
                {
                    ((Panel)e.Row.FindControl("Panel1")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink1")).Text = "- Xem sản phẩm";
                    ((HyperLink)e.Row.FindControl("HyperLink1")).NavigateUrl = "javascript:popCenter('CN_SanPhamChiTiet.aspx?SanPhamID=" + DataBinder.Eval(e.Row.DataItem, "Id").ToString() + "','SanPhamChiTiet', 500, 400);";
                }
            }
            #endregion "Trang thai Cho xu ly"

            #region "Trang thai Cho tham dinh"
            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiSanPhamList.CHO_THAM_DINH)
            {
                if (((PageBase)this.Page).mUserInfo.IsNguoiTiepNhan(HoSoId))
                {
                    ((Panel)e.Row.FindControl("Panel1")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink1")).Text = "- Xem sản phẩm";
                    ((HyperLink)e.Row.FindControl("HyperLink1")).Attributes.Add("OnClick", "popCenter('CN_SanPhamChiTiet.aspx?SanPhamID=" + DataBinder.Eval(e.Row.DataItem, "Id").ToString() + "','SanPhamChiTiet', 500, 400);return false;");
                }
                if (((PageBase)this.Page).mUserInfo.IsNguoiXuLy(HoSoId))
                {
                    ((Panel)e.Row.FindControl("Panel2")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink2")).Text = "- Xử lý sản phẩm";
                    ((HyperLink)e.Row.FindControl("HyperLink2")).NavigateUrl = "../WebUI/CN_XuLyHoSo_DanhGia.aspx?HoSoID=" + HoSoId + "&SanPhamID=" + DataBinder.Eval(e.Row.DataItem, "Id").ToString() + "&TrangThaiID=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&action=view&UserControl=CN_HoSoDi&UserControlHS=" + strUserControlHS;
                }
            }
            #endregion "Trang thai Cho tham dinh"

            #region "Trang thai Tham dinh dong y"
            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiSanPhamList.THAM_DINH_DONG_Y)
            {
                if (((PageBase)this.Page).mUserInfo.IsNguoiTiepNhan(HoSoId))
                {
                    ((Panel)e.Row.FindControl("Panel1")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink1")).Text = "- Xem sản phẩm";
                    ((HyperLink)e.Row.FindControl("HyperLink1")).Attributes.Add("OnClick", "popCenter('CN_SanPhamChiTiet.aspx?SanPhamID=" + DataBinder.Eval(e.Row.DataItem, "Id").ToString() + "','SanPhamChiTiet', 500, 400);return false;");
                }
                if (((PageBase)this.Page).mUserInfo.IsNguoiXuLy(HoSoId))
                {
                    ((Panel)e.Row.FindControl("Panel2")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink2")).Text = "- Xử lý sản phẩm";
                    ((HyperLink)e.Row.FindControl("HyperLink2")).NavigateUrl = "../WebUI/CN_XuLyHoSo_DanhGia.aspx?HoSoID=" + HoSoId + "&SanPhamID=" + DataBinder.Eval(e.Row.DataItem, "Id").ToString() + "&TrangThaiID=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&action=view&UserControl=CN_HoSoDi&UserControlHS=" + strUserControlHS;
                }
                if (((PageBase)this.Page).mUserInfo.IsNguoiThamDinh(HoSoId))
                {
                    ((Panel)e.Row.FindControl("Panel3")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink3")).Text = "- Thẩm định sản phẩm";
                    ((HyperLink)e.Row.FindControl("HyperLink3")).NavigateUrl = "../WebUI/CN_ThamDinhHoSo.aspx?HoSoID=" + HoSoId + "&SanPhamID=" + DataBinder.Eval(e.Row.DataItem, "Id").ToString() + "&TrangThaiID=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&action=view&UserControl=CN_HoSoDi&UserControlHS=" + strUserControlHS;
                }
            }
            #endregion "Trang thai Tham dinh dong y"

            #region "Trang thai Tham dinh khong dong y"
            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiSanPhamList.THAM_DINH_KHONG_DONG_Y)
            {
                if (((PageBase)this.Page).mUserInfo.IsNguoiTiepNhan(HoSoId))
                {
                    ((Panel)e.Row.FindControl("Panel1")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink1")).Text = "- Xem sản phẩm";
                    ((HyperLink)e.Row.FindControl("HyperLink1")).Attributes.Add("OnClick", "popCenter('CN_SanPhamChiTiet.aspx?SanPhamID=" + DataBinder.Eval(e.Row.DataItem, "Id").ToString() + "','SanPhamChiTiet', 500, 400);return false;");
                }
                if (((PageBase)this.Page).mUserInfo.IsNguoiXuLy(HoSoId))
                {
                    ((Panel)e.Row.FindControl("Panel2")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink2")).Text = "- Xử lý sản phẩm";
                    ((HyperLink)e.Row.FindControl("HyperLink2")).NavigateUrl = "../WebUI/CN_XuLyHoSo_DanhGia.aspx?HoSoID=" + HoSoId + "&SanPhamID=" + DataBinder.Eval(e.Row.DataItem, "Id").ToString() + "&TrangThaiID=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&action=view&UserControl=CN_HoSoDi&UserControlHS=" + strUserControlHS;
                }
                if (((PageBase)this.Page).mUserInfo.IsNguoiThamDinh(HoSoId))
                {
                    ((Panel)e.Row.FindControl("Panel3")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink3")).Text = "- Thẩm định sản phẩm";
                    ((HyperLink)e.Row.FindControl("HyperLink3")).NavigateUrl = "../WebUI/CN_ThamDinhHoSo.aspx?HoSoID=" + HoSoId + "&SanPhamID=" + DataBinder.Eval(e.Row.DataItem, "Id").ToString() + "&TrangThaiID=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&action=view&UserControl=CN_HoSoDi&UserControlHS=" + strUserControlHS;
                }
            }
            #endregion "Trang thai Tham dinh khong dong y"

            #region "Trang thai Cho phe duyet"
            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiSanPhamList.CHO_PHE_DUYET)
            {
                if (((PageBase)this.Page).mUserInfo.IsNguoiTiepNhan(HoSoId))
                {
                    ((Panel)e.Row.FindControl("Panel1")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink1")).Text = "- Xem sản phẩm";
                    ((HyperLink)e.Row.FindControl("HyperLink1")).Attributes.Add("OnClick", "popCenter('CN_SanPhamChiTiet.aspx?SanPhamID=" + DataBinder.Eval(e.Row.DataItem, "Id").ToString() + "','SanPhamChiTiet', 500, 400);return false;");
                }
                if (((PageBase)this.Page).mUserInfo.IsNguoiXuLy(HoSoId))
                {
                    ((Panel)e.Row.FindControl("Panel2")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink2")).Text = "- Xử lý sản phẩm";
                    ((HyperLink)e.Row.FindControl("HyperLink2")).NavigateUrl = "../WebUI/CN_XuLyHoSo_DanhGia.aspx?HoSoID=" + HoSoId + "&SanPhamID=" + DataBinder.Eval(e.Row.DataItem, "Id").ToString() + "&TrangThaiID=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&action=view&UserControl=CN_HoSoDi&UserControlHS=" + strUserControlHS;
                }
                if (((PageBase)this.Page).mUserInfo.IsNguoiThamDinh(HoSoId))
                {
                    ((Panel)e.Row.FindControl("Panel3")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink3")).Text = "- Thẩm định sản phẩm";
                    ((HyperLink)e.Row.FindControl("HyperLink3")).NavigateUrl = "../WebUI/CN_ThamDinhHoSo.aspx?HoSoID=" + HoSoId + "&SanPhamID=" + DataBinder.Eval(e.Row.DataItem, "Id").ToString() + "&TrangThaiID=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&action=view&UserControl=CN_HoSoDi&UserControlHS=" + strUserControlHS;
                }
            }
            #endregion "Trang thai Cho phe duyet"

            #region "Trang thai Giam doc phe duyet"
            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiSanPhamList.GD_PHE_DUYET)
            {
                if (((PageBase)this.Page).mUserInfo.IsNguoiTiepNhan(HoSoId))
                {
                    ((Panel)e.Row.FindControl("Panel1")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink1")).Text = "- Xem sản phẩm";
                    ((HyperLink)e.Row.FindControl("HyperLink1")).Attributes.Add("OnClick", "popCenter('CN_SanPhamChiTiet.aspx?SanPhamID=" + DataBinder.Eval(e.Row.DataItem, "Id").ToString() + "','SanPhamChiTiet', 500, 400);return false;");
                }
                if (((PageBase)this.Page).mUserInfo.IsNguoiXuLy(HoSoId))
                {
                    ((Panel)e.Row.FindControl("Panel2")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink2")).Text = "- Xử lý sản phẩm";
                    ((HyperLink)e.Row.FindControl("HyperLink2")).NavigateUrl = "../WebUI/CN_XuLyHoSo_DanhGia.aspx?HoSoID=" + HoSoId + "&SanPhamID=" + DataBinder.Eval(e.Row.DataItem, "Id").ToString() + "&TrangThaiID=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&action=view&UserControl=CN_HoSoDi&UserControlHS=" + strUserControlHS;
                }
                if (((PageBase)this.Page).mUserInfo.IsNguoiThamDinh(HoSoId))
                {
                    ((Panel)e.Row.FindControl("Panel3")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink3")).Text = "- Thẩm định sản phẩm";
                    ((HyperLink)e.Row.FindControl("HyperLink3")).NavigateUrl = "../WebUI/CN_ThamDinhHoSo.aspx?HoSoID=" + HoSoId + "&SanPhamID=" + DataBinder.Eval(e.Row.DataItem, "Id").ToString() + "&TrangThaiID=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&action=view&UserControl=CN_HoSoDi&UserControlHS=" + strUserControlHS;
                }
                if (((PageBase)this.Page).mUserInfo.IsPermission(EnPermission.CN_PHE_DUYET))
                {
                    ((Panel)e.Row.FindControl("Panel4")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink4")).Text = "- Phê duyệt sản phẩm";
                    ((HyperLink)e.Row.FindControl("HyperLink4")).NavigateUrl = "../WebUI/CN_PheDuyetHoSo.aspx?HoSoID=" + HoSoId + "&SanPhamID=" + DataBinder.Eval(e.Row.DataItem, "Id").ToString() + "&TrangThaiID=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&action=view&UserControl=CN_HoSoDi&UserControlHS=" + strUserControlHS;
                }
                //LongHH
                HtmlGenericControl lnkTrangThai = new System.Web.UI.HtmlControls.HtmlGenericControl("A");
                lnkTrangThai.InnerText = EntityHelper.GetEnumTextValue((EnTrangThaiSanPhamList)(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID"))));
                StringBuilder sbCongVan = new StringBuilder();
                if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiSanPhamList.GD_PHE_DUYET)//(DataBinder.Eval(e.Row.DataItem, "MoTa") == "Giám đốc phê duyệt")
                {
                    lnkTrangThai.Attributes.Add("href", "../ReportForm/HienBaoCao.aspx?HoSoID=" + Request["HoSoID"].ToString() + "&SanPhamId=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&LoaiBaoCao=BanTiepNhan&format=Word");
                }
                e.Row.Cells[4].Controls.Add(lnkTrangThai);
                //LongHH
            }
            #endregion "Trang thai Giam doc phe duyet"

            #region "Trang thai Giam doc khong phe duyet"
            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiSanPhamList.GD_KHONG_DUYET)
            {
                if (((PageBase)this.Page).mUserInfo.IsNguoiTiepNhan(HoSoId))
                {
                    ((Panel)e.Row.FindControl("Panel1")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink1")).Text = "- Xem sản phẩm";
                    ((HyperLink)e.Row.FindControl("HyperLink1")).Attributes.Add("OnClick", "popCenter('CN_SanPhamChiTiet.aspx?SanPhamID=" + DataBinder.Eval(e.Row.DataItem, "Id").ToString() + "','SanPhamChiTiet', 500, 400);return false;");
                }
                if (((PageBase)this.Page).mUserInfo.IsNguoiXuLy(HoSoId))
                {
                    ((Panel)e.Row.FindControl("Panel2")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink2")).Text = "- Xử lý sản phẩm";
                    ((HyperLink)e.Row.FindControl("HyperLink2")).NavigateUrl = "../WebUI/CN_XuLyHoSo_DanhGia.aspx?HoSoID=" + HoSoId + "&SanPhamID=" + DataBinder.Eval(e.Row.DataItem, "Id").ToString() + "&TrangThaiID=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&action=view&UserControl=CN_HoSoDi&UserControlHS=" + strUserControlHS;
                }
                if (((PageBase)this.Page).mUserInfo.IsNguoiThamDinh(HoSoId))
                {
                    ((Panel)e.Row.FindControl("Panel3")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink3")).Text = "- Thẩm định sản phẩm";
                    ((HyperLink)e.Row.FindControl("HyperLink3")).NavigateUrl = "../WebUI/CN_ThamDinhHoSo.aspx?HoSoID=" + HoSoId + "&SanPhamID=" + DataBinder.Eval(e.Row.DataItem, "Id").ToString() + "&TrangThaiID=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&action=view&UserControl=CN_HoSoDi&UserControlHS=" + strUserControlHS;
                }
                if (((PageBase)this.Page).mUserInfo.IsPermission(EnPermission.CN_PHE_DUYET))
                {
                    ((Panel)e.Row.FindControl("Panel4")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink4")).Text = "- Phê duyệt sản phẩm";
                    ((HyperLink)e.Row.FindControl("HyperLink4")).NavigateUrl = "../WebUI/CN_PheDuyetHoSo.aspx?HoSoID=" + HoSoId + "&SanPhamID=" + DataBinder.Eval(e.Row.DataItem, "Id").ToString() + "&TrangThaiID=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&action=view&UserControl=CN_HoSoDi&UserControlHS=" + strUserControlHS;
                }
            }
            #endregion "Trang thai Giam doc khong phe duyet"
        }
    }
    /// <summary>
    /// Phân trang
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    ///     Author      Date          Comments
    ///     QuanNM      18/05/2009    Tạo mới
    /// </Modified>
    protected void gvSanPham_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSanPham.PageIndex = e.NewPageIndex;
        LayTatCaSanPhamTheoQuyenDangNhap();
    }

    /// <summary>
    /// Quản lý breadcum
    /// </summary>
    private void ManageBreadCum(string HoSoId, string UserControl)
    {
        linkCum.Text = "QUẢN LÝ HỒ SƠ";
        linkCum.NavigateUrl = "../WebUI/CN_HoSo_QuanLy.aspx?UserControl=" + UserControl + "&HoSoId=" + HoSoId + "";
    }
}
