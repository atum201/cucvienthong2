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
using Cuc_QLCL.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

/// <summary>
/// Hồ sơ sản phẩm
/// </summary>
///<Modified>
/// Author      Data        Comments    
/// QuanNM      12/05/2009    
///</Modified>
public partial class UserControls_uc_CN_HoSoSanPhamMoiNhan : System.Web.UI.UserControl
{
    private string HoSoId = string.Empty;
    private int LoaiHoSo = 0;
    private string strUserControl = string.Empty;
    private string strUserControlHS = string.Empty;
    //Xoa thong bao phi
    List<string> deleteSuccess = new List<string>();
    List<string> deleteFail = new List<string>();
    //Xoa san pham
    List<string> deleteSPSuccess = new List<string>();
    List<string> deleteSPFail = new List<string>();
    //Gui thong bao phi
    List<string> sendSuccess = new List<string>();
    List<string> sendFail = new List<string>();
    public int test = 0;
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
        strUserControlHS = Request["UserControlHS"];
        IBThemMoi.OnClientClick = "popCenter('CN_ThongBaoNopTien_TaoMoi.aspx?Action=Add&HoSoID=" + HoSoId + "','ThongBaoNopTienTaoMoi', 950,450); return false;";
        LBThemMoi.OnClientClick = "popCenter('CN_ThongBaoNopTien_TaoMoi.aspx?Action=Add&HoSoID=" + HoSoId + "','ThongBaoNopTienTaoMoi', 950,450); return false;";
        //LongHH
        IBThemMoiCNHQ.OnClientClick = "popCenter('CN_ThongBaoPhi_QTSX_TaoMoi.aspx?Action=Add&HoSoID=" + HoSoId + "','ThongBaoNopTienTaoMoi', 950,450); return false;";
        LBThemMoiCNHQ.OnClientClick = "popCenter('CN_ThongBaoPhi_QTSX_TaoMoi.aspx?Action=Add&HoSoID=" + HoSoId + "','ThongBaoNopTienTaoMoi', 950,450); return false;";
        //LongHH
        imgThemMoiTBP.OnClientClick = "popCenter('CN_ThongBaoPhi_TaoMoi.aspx?Action=Add&HoSoID=" + HoSoId + "','CN_ThongBaoPhi_TaoMoi', 950,450); return false;";
        lnkThemMoiTBP.OnClientClick = "popCenter('CN_ThongBaoPhi_TaoMoi.aspx?Action=Add&HoSoID=" + HoSoId + "','CN_ThongBaoPhi_TaoMoi', 950,450); return false;";
        //LongHH
        //lnkThemMoiQTSX.OnClientClick = "popCenter('CN_ThongBaoPhi_QTSX_TaoMoi.aspx?Action=Add&HoSoID=" + HoSoId + "','CN_ThongBaoPhi_QTSX_TaoMoi', 950,450); return false;";

        //DataTable tbp = QLCL_Patch.GetTTGiayBaoPhi(HoSoId);
        //DataRow rtb = QLCL_Patch.GetTBPhiTiepNhan(HoSoId);
        //if (tbp != null && tbp.Rows.Count > 0 &&rtb !=null)// đã có thông báo phí, Chỉ cho edit hoặc thu phí
        //{

        //    LBTaoTiepNhan.OnClientClick = "popCenter('TestBaoCao.aspx?HoSoID=" + HoSoId + "&ThongBaoLePhiID=" + rtb["ID"].ToString() + "','CN_ThuPhiTiepNhan',800,600)";
        //    //imgBtnSuaPhiTiepNhan.OnClientClick = "popCenter('CN_ThongBaoPhiTiepNhan_TaoMoi.aspx?Action=Edit&HoSoID=" + HoSoId + "','CN_ThongBaoPhiTiepNhan_TaoMoi', 950,450); return false;";
        //}
        //else
        //{
        //    //LBTaoTiepNhan.OnClientClick = "popCenter('CN_ThongBaoPhiTiepNhan_TaoMoi.aspx?Action=Add&HoSoID=" + HoSoId + "','CN_ThongBaoPhiTiepNhan_TaoMoi', 950,450); return false;";
        //    //imgBtnSuaPhiTiepNhan.Visible = false;
        //    LBTaoTiepNhan.Visible = false;
        //}

        if (LoaiHoSo == (int)CucQLCL.Common.LoaiHoSo.ChungNhanHopChuan)
            trThongBaoNopTienCNHQ.Attributes.Add("style", "display:none");
        //LongHH
        if (LoaiHoSo == (int)CucQLCL.Common.LoaiHoSo.ChungNhanHopQuy)
            trThongBaoNopTien.Attributes.Add("style", "display:none");

        if (!IsPostBack)
        {
            ManageBreadCum(HoSoId, strUserControl);
            BindChiTietHoSo();
            BindThongBaoNopTienGrid();
            LayTatCaSanPhamTheoQuyenDangNhap();
            BindThongBaoPhiGrid();
            EnableThongBaoPhiButton();
            EnableSanPhamButton();
            KiemTraTrangThaiSanPham(HoSoId);

            //LongHH
            BindThongBaoNopTienCNHQGrid();
            //LongHH
            //// Nếu hồ sơ đã gửi lưu trữ hoặc người đăng nhập kô phải chuyên viên xử lý thì không hiển thị các nút chức năng
            //if (Request["TrangThaiID"] != null)
            //{
            //    if (ProviderFactory.HoSoProvider.GetById(HoSoId).TrangThaiId == (int)EnTrangThaiHoSoList.CHO_LUU_TRU
            //        || !((PageBase)this.Page).mUserInfo.IsNguoiXuLy(HoSoId))
            //    {
            //        gvThongBaoNopTien.Columns.RemoveAt(8);
            //        gvPhi.Columns.RemoveAt(7);
            //        gvSanPham.Columns.RemoveAt(7);
            //    }
            //}
        }

        // Nếu hồ sơ đã gửi lưu trữ hoặc người đăng nhập kô phải chuyên viên xử lý thì không hiển thị các nút chức năng
        if (Request["TrangThaiID"] != null)
        {
            if (ProviderFactory.HoSoProvider.GetById(HoSoId).TrangThaiId == (int)EnTrangThaiHoSoList.CHO_LUU_TRU
                || !((PageBase)this.Page).mUserInfo.IsNguoiXuLy(HoSoId))
            {
                imgGuiTBP.Visible = false;
                imgThemMoiTBP.Visible = false;
                ImgBtnThemmoiSanPham.Visible = false;
                imgXoaSanPham.Visible = false;
                ImgBtnCompleteHS.Visible = false;
                imgXoaTBP.Visible = false;
                lnkGuiTBP.Visible = false;
                lnkXoaSanPham.Visible = false;
                lnkXoaTBP.Visible = false;
                lnkThemMoiSanPham.Visible = false;
                //LongHH
                lnkThemMoiTBP.Visible = false;
                //LongHH
                //lnkThemMoiQTSX.Visible = false;
                LinkBtnCompleteHS.Visible = false;

                LBThemMoi.Visible = false;
                LBXoa.Visible = false;
                LBGui.Visible = false;
                IBGui.Visible = false;
                IBThemMoi.Visible = false;
                //LongHH
                IBThemMoiCNHQ.Visible = false;
                LBThemMoiCNHQ.Visible = false;
                LBXoaCNHQ.Visible = false;
                LBGuiCNHQ.Visible = false;
                IBGuiCNHQ.Visible = false;
                //LBTaoTiepNhan.Visible = false;
                //imgBtnSuaPhiTiepNhan.Visible = false;
                //LongHh
                IBXoa.Visible = false;                
            }
        }
        //Chuyển link cho danh sách sản phẩm theo trạng thái

        if ((Request["SanPhamID"] != null) && (Request["UserControl"] != null))
            if (Request["UserControl"] == "CN_HoSoDen")
                ReturnLinkDirect(Request["TrangThaiID"].ToString(), Request["HosoID"].ToString(), Request["SanPhamID"].ToString());
    }
    /// <summary>
    /// Hiển thị chi tiết hồ sơ
    /// </summary>
    ///<Modified>
    /// Author      Data            Comments    
    /// Truongtv    12/05/2009      Create new
    ///</Modified>
    private void BindChiTietHoSo()
    {
        HoSo hsInform = ProviderFactory.HoSoProvider.GetById(HoSoId);
        lblSoHoSo.Text = Server.HtmlEncode(hsInform.SoHoSo);
        lblTrangThai.Text = EntityHelper.GetEnumTextValue((EnTrangThaiHoSoList)hsInform.TrangThaiId);

        lblSoCongVanDen.Text = Server.HtmlEncode(hsInform.SoCongVanDen);
        lblNguoiNop.Text = Server.HtmlEncode(hsInform.NguoiNopHoSo);
        if (hsInform.NhanHoSoTuId != null)
            lblLoaiHinhGui.Text = EntityHelper.GetEnumTextValue((EnNhanHoSoTuList)hsInform.NhanHoSoTuId);

        if (hsInform.NgayTiepNhan != null)
        {
            lblNgayTiepNhan.Text = ((DateTime)hsInform.NgayTiepNhan).ToShortDateString();
        }
        DataTable nguoiTD = ProviderFactory.HoSoProvider.LayNguoiThamDinh(HoSoId, LoaiHoSo);
        lblNguoiThamDinh.Text = nguoiTD.Rows[0][1].ToString();


        //lblFax.Text = Server.HtmlEncode(hsInform.Fax);
        lblDienThoai.Text = Server.HtmlEncode(hsInform.DienThoai);
        lblEmail.Text = Server.HtmlEncode(hsInform.Email);
        int? ob = hsInform.HinhThucId;

        SysUser user = ProviderFactory.SysUserProvider.GetById(hsInform.NguoiTiepNhanId);
        if (user != null)
            lblNguoiTiepNhan.Text = user.FullName;
        DmDonVi dmDonVi = ProviderFactory.DmDonViProvider.GetById(hsInform.DonViId);
        if (dmDonVi != null)
            lblDonViNopHoSo.Text = Server.HtmlEncode(dmDonVi.TenTiengViet);
            //LongHH
            lblTenTiengAnh.Text = Server.HtmlEncode(dmDonVi.TenTiengAnh);
            lblDiaChi.Text = Server.HtmlEncode(dmDonVi.DiaChi);
            lblMaSoThue.Text = Server.HtmlEncode(dmDonVi.MaSoThue);
            //LongHH

        if (hsInform.NguonGocId != null)
        {
            lblNguonGoc.Text = EntityHelper.GetEnumTextValue((EnNguonGocList)hsInform.NguonGocId);
        }
        //LongHH
        if (hsInform.NhanHoSoTuId.ToString().Equals(5.ToString())) {
            LBThemMoi.Visible = false;
            IBThemMoi.Visible = false;
        }
        //LongHH
        lblLoaiHinhChungNhan.Text = hsInform.LoaiHoSo == (int)CucQLCL.Common.LoaiHoSo.ChungNhanHopChuan ? "Chứng nhận hợp chuẩn" : "Chứng nhận hợp quy";
        lblYKienTiepNhan.Text = Server.HtmlEncode(hsInform.Luuy);
    }
    /// <summary>
    /// Lấy danh sách sản phẩm của hồ sơ theo quyền đăng nhập
    /// </summary>
    /// <Modified>
    /// Author      Date            Comments    
    /// Quannm      16/05/2009      Create new
    ///</Modified>
    private void LayTatCaSanPhamTheoQuyenDangNhap()
    {
        //((PageBase)this.Page).mUserInfo.StartWith = "01";
        DataTable dtbSanPham = ProviderFactory.SanPhamProvider.LayTatCaSanPhamTheoQuyenDangNhap(HoSoId, ((PageBase)this.Page).mUserInfo.UserID, ((PageBase)this.Page).mUserInfo.GetPermissionList("01"));
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
        DataTable dtThongBaoPhi = new DataTable();
        dtThongBaoPhi = ProviderFactory.ThongBaoLePhiProvider.GetThongBaoLePhiByHoSoID(HoSoId, ((PageBase)this.Page).mUserInfo.GetPermissionList("01"), ((PageBase)this.Page).mUserInfo.UserID);
        gvPhi.DataSource = dtThongBaoPhi;
        gvPhi.DataBind();
    }
    /// <summary>
    /// Đổi màu bản ghi nếu DaDoc field = false
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 13/5/2009              
    /// </Modified>
    protected void gvSanPham_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "CheckDaDoc")) == false)
            {
                e.Row.CssClass = "unread rowitem";
            }

            //chi cho phep sua thong tin cua cac san pham o trang thai: Cho xu ly, tham dinh dong y, tham dinh khong dong y, giam doc khong phe duyet
            if ((Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiSanPhamList.CHO_XU_LY)
                    || (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiSanPhamList.THAM_DINH_DONG_Y)
                    || (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiSanPhamList.THAM_DINH_KHONG_DONG_Y)
                    || (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiSanPhamList.GD_KHONG_DUYET)
                )
            {
                Control div = e.Row.FindControl("View");
                div.Visible = true;
            }
            SanPham sp = ProviderFactory.SanPhamProvider.GetById(DataBinder.Eval(e.Row.DataItem, "ID").ToString());
            if(sp!=null){
                int KetLuanId = sp.KetLuanId.HasValue ? sp.KetLuanId.Value : 0;
                int TrangThaiId = sp.TrangThaiId;
                String TrangThaiSp = ProviderFactory.EnTrangThaiSanPhamProvider.GetById(sp.TrangThaiId).MoTa;
                //LinkButton lnkTrangThai = new LinkButton();
                HtmlGenericControl lnkTrangThai = new System.Web.UI.HtmlControls.HtmlGenericControl("A");  
                bool CoCongVanTraLoi = false;
                if (KetLuanId != (int)EnKetLuanList.CAP_GCN && KetLuanId != 0)
                {
                    //  lấy danh sách tài liệu
                    TList<TaiLieuDinhKem> lstTaiLieuDinhKem = ProviderFactory.TaiLieuDinhKemProvider.GetBySanPhamId(sp.Id);

                    foreach (TaiLieuDinhKem objTaiLieu in lstTaiLieuDinhKem)
                    {
                        string FilePath = objTaiLieu.TenFile.Trim();
                        if (objTaiLieu.LoaiTaiLieuId == (int)EnLoaiTaiLieuList.CONG_VAN)
                        {
                            StringBuilder sbCongVan = new StringBuilder();
                            string TrangThai_KetLuan = TrangThaiSp;
                            if (TrangThaiId == (int)EnTrangThaiSanPhamList.CHO_XU_LY)
                                TrangThai_KetLuan = "Đang xử lý";
                            else if (TrangThaiId == (int)EnTrangThaiSanPhamList.CHO_XU_LY)
                                TrangThai_KetLuan = "Huỷ";
                            else
                            {
                                if (TrangThaiSp.Contains("Chờ"))
                                {
                                    if (KetLuanId == (int)EnKetLuanList.KHONG_CAP_GCN)
                                        TrangThai_KetLuan = TrangThaiSp + " Không cấp GCN";
                                    if (KetLuanId == (int)EnKetLuanList.KHONG_PHAI_CN)
                                        TrangThai_KetLuan = TrangThaiSp + " Không phải CN";
                                    if (KetLuanId == (int)EnKetLuanList.HUY)
                                        TrangThai_KetLuan = TrangThaiSp + " Huỷ";
                                }
                                else
                                {
                                    if (KetLuanId == (int)EnKetLuanList.KHONG_CAP_GCN)
                                        TrangThai_KetLuan = "Không cấp GCN, " + TrangThaiSp;
                                    if (KetLuanId == (int)EnKetLuanList.KHONG_PHAI_CN)
                                        TrangThai_KetLuan = "Không phải CN, " + TrangThaiSp;
                                    if (KetLuanId == (int)EnKetLuanList.HUY)
                                        TrangThai_KetLuan = "Huỷ, " + TrangThaiSp;
                                }
                            }

                            lnkTrangThai.Attributes.Add("href", "../" + FilePath);
                            lnkTrangThai.InnerText = TrangThai_KetLuan;
                            CoCongVanTraLoi = true;
                        }

                    }
                }
                if (KetLuanId == (int)EnKetLuanList.CAP_GCN)
                {
                    if (TrangThaiId == (int)EnTrangThaiSanPhamList.DA_CAP_GCN
                        || TrangThaiId == (int)EnTrangThaiSanPhamList.GD_PHE_DUYET
                        || TrangThaiId == (int)EnTrangThaiSanPhamList.HUY_GCN)
                    {
                        if (TrangThaiId == (int)EnTrangThaiSanPhamList.DA_CAP_GCN)
                        {
                            lnkTrangThai.InnerText = "Đã cấp GCN";
                            lnkTrangThai.Attributes.Add("href", "../ReportForm/HienBaoCao.aspx?LoaiBaoCao=GiayChungNhan&SanphamID=" + sp.Id + "&TenBaoCao=GiayChungNhan&format=Word");
                            //lnkTrangThai.OnClientClick = "return popCenter('../ReportForm/HienBaoCao.aspx?LoaiBaoCao=GiayChungNhan&SanphamID=" + sp.Id + "','Tra_cuu_giay_chung_nhan',800,600);";
                            //lnkTrangThai.OnClientClick = "return PageRedirect('');";
                        }
                        if (TrangThaiId == (int)EnTrangThaiSanPhamList.GD_PHE_DUYET)
                        {
                            lnkTrangThai.InnerText = "Giám đốc phê duyệt cấp GCN";
                            lnkTrangThai.Attributes.Add("href","../ReportForm/HienBaoCao.aspx?LoaiBaoCao=GiayChungNhan&SanphamID=" + sp.Id + "&TenBaoCao=GiayChungNhan&format=Word");
                            //lnkTrangThai.ToolTip = "Xem Giấy chứng nhận";
                            //lnkTrangThai.OnClientClick = "return PageRedirect('../ReportForm/HienBaoCao.aspx?LoaiBaoCao=GiayChungNhan&SanphamID=" + sp.Id + "&TenBaoCao=GiayChungNhan&format=word');";
                        }
                        if (TrangThaiId == (int)EnTrangThaiSanPhamList.HUY_GCN)
                        {
                            lnkTrangThai.InnerText = "Hủy cấp GCN";
                        }
                        e.Row.Cells[5].Controls.Add(lnkTrangThai);
                    }
                    else
                    {
                        if (TrangThaiId < 2)
                            e.Row.Cells[5].Text = "Đang xử lý";
                        else
                        {
                            if (TrangThaiSp.Contains("Chờ"))
                                e.Row.Cells[5].Text = "Cấp GCN, " + TrangThaiSp;
                            else
                                e.Row.Cells[5].Text = TrangThaiSp + " Cấp GCN";
                        }
                    }
                }
                else
                {
                    if (CoCongVanTraLoi)
                        e.Row.Cells[5].Controls.Add(lnkTrangThai);
                    else
                    {
                        string TrangThai_KetLuan = TrangThaiSp;
                        if (TrangThaiId == (int)EnTrangThaiSanPhamList.CHO_XU_LY)
                            TrangThai_KetLuan = "Đang xử lý";
                        else if (TrangThaiId == (int)EnTrangThaiSanPhamList.HUY)
                            TrangThai_KetLuan = "Huỷ";
                        else
                        {
                            if (TrangThaiSp.Contains("Chờ"))
                            {
                                if (KetLuanId == (int)EnKetLuanList.KHONG_CAP_GCN)
                                    TrangThai_KetLuan = TrangThaiSp + " Không cấp GCN";
                                if (KetLuanId == (int)EnKetLuanList.KHONG_PHAI_CN)
                                    TrangThai_KetLuan = TrangThaiSp + " Không phải CN";
                                if (KetLuanId == (int)EnKetLuanList.HUY)
                                    TrangThai_KetLuan = TrangThaiSp + " Huỷ";
                            }
                            else
                            {
                                if (KetLuanId == (int)EnKetLuanList.KHONG_CAP_GCN)
                                    TrangThai_KetLuan = "Không cấp GCN, " + TrangThaiSp;
                                if (KetLuanId == (int)EnKetLuanList.KHONG_PHAI_CN)
                                    TrangThai_KetLuan = "Không phải CN, " + TrangThaiSp;
                                if (KetLuanId == (int)EnKetLuanList.HUY)
                                    TrangThai_KetLuan = "Huỷ, " + TrangThaiSp;
                            }
                        }
                        if (String.IsNullOrEmpty(TrangThai_KetLuan.Trim()))
                            TrangThai_KetLuan = sp.TrangThaiId.ToString();
                        e.Row.Cells[5].Text = TrangThai_KetLuan;
                    }
                }
            }
            

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
    /// Phân trang
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    ///     Author      Date          Comments
    ///     QuanNM      18/05/2009    Tạo mới
    /// </Modified>
    protected void gvPhi_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPhi.PageIndex = e.NewPageIndex;
        BindThongBaoPhiGrid();
    }
    /// <summary>
    /// Kiểm tra user có quyền tạo mới thông báo phí hay không?
    /// </summary>
    /// <param name="Permission"></param>
    /// <Modified>
    /// Author      Date            Comments    
    /// Quannm      16/05/2009      Create new
    ///</Modified>
    private void EnableThongBaoPhiButton()
    {
        if (((PageBase)this.Page).mUserInfo.IsNguoiXuLy(HoSoId))
        {
            //link
            lnkThemMoiTBP.Visible = true;
            //LongHH
            //lnkThemMoiQTSX.Visible = true;
            //LongHH
            lnkGuiTBP.Visible = true;
            lnkXoaTBP.Visible = true;
            //anh
            imgThemMoiTBP.Visible = true;
            imgXoaTBP.Visible = true;
            imgGuiTBP.Visible = true;
        }
        else
        {
            //link
            lnkThemMoiTBP.Visible = false;
            //LongHH
            //lnkThemMoiQTSX.Visible = false;
            //LongHH
            lnkGuiTBP.Visible = false;
            lnkXoaTBP.Visible = false;
            //anh
            imgThemMoiTBP.Visible = false;
            imgXoaTBP.Visible = false;
            imgGuiTBP.Visible = false;
        }
    }
    /// <summary>
    /// Kiểm tra user có quyền tạo mới sản phẩm hay không?
    /// </summary>
    /// <param name="Permission"></param>
    /// <Modified>
    /// Author      Date            Comments    
    /// Quannm      16/05/2009      Create new
    ///</Modified>
    private void EnableSanPhamButton()
    {
        if (((PageBase)this.Page).mUserInfo.IsNguoiXuLy(HoSoId))
        {
            lnkThemMoiSanPham.Visible = !CheckHoSoDayDu(HoSoId);
            ImgBtnThemmoiSanPham.Visible = !CheckHoSoDayDu(HoSoId);

            lnkXoaSanPham.Visible = true;
            imgXoaSanPham.Visible = true;
        }
        else
        {
            lnkThemMoiSanPham.Visible = false;
            lnkXoaSanPham.Visible = false;
            ImgBtnThemmoiSanPham.Visible = false;
            imgXoaSanPham.Visible = false;
        }
    }
    /// <summary>
    /// Chuyển trang cho từng trạng thái của Hồ sơ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// TuanVM                 11/5/2009              
    /// </Modified>
    private void ReturnLinkDirect(string strTrangThaiID, string HoSoID, string SanPhamID)
    {
        string link = "";
        int TrangThaiID;
        if ((strTrangThaiID != string.Empty) && (strTrangThaiID != null))
        {
            //Chuyển trạng thái chưa đọc sang đã đọc của sản phẩm
            ProviderFactory.SanPhamProvider.ChangeStatusDaDoc(((PageBase)this.Page).mUserInfo.UserID, SanPhamID, "Add");
            TrangThaiID = Convert.ToInt32(strTrangThaiID);
            switch (TrangThaiID)
            {
                case (int)EnTrangThaiSanPhamList.CHO_THAM_DINH:
                    link = "CN_ThamDinhHoSo.aspx?direct=CN_HoSoMoi&HoSoID=" + HoSoID + "&SanPhamID=" + SanPhamID + "&TrangThaiId=" + TrangThaiID + "&UserControlHS=" + strUserControlHS + "&UserControl=" + Request["UserControl"];
                    break;
                case (int)EnTrangThaiSanPhamList.CHO_PHE_DUYET:
                    link = "CN_PheDuyetHoSo.aspx?direct=CN_HoSoMoi&HoSoID=" + HoSoID + "&SanPhamID=" + SanPhamID + "&TrangThaiId=" + TrangThaiID + "&UserControlHS=" + strUserControlHS + "&UserControl=" + Request["UserControl"];
                    break;
                default:    // Chờ xử lý, thẩm định đồng ý, thẩm định không đồng ý, GĐ phê duyệt, không phê duyệt
                    link = "CN_XuLyHoSo_DanhGia.aspx?HoSoID=" + HoSoID + "&SanPhamID=" + SanPhamID + "&TrangThaiId=" + TrangThaiID + "&UserControlHS=" + strUserControlHS + "&UserControl=" + Request["UserControl"];
                    break;
            }
            Response.Redirect(link);
        }
    }
    /// <summary>
    /// Kiểm tra hồ sơ là đầy đủ hay chưa
    /// </summary>
    /// <param name="HoSoId"></param>
    /// <Modified>
    /// Author      Date            Comments    
    /// Quannm      16/05/2009      Create new
    ///</Modified>
    private bool CheckHoSoDayDu(string HoSoId)
    {
        HoSo objHs = ProviderFactory.HoSoProvider.GetById(HoSoId);
        if (Convert.ToBoolean(objHs.HoSoDayDu))
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// Quản lý breadcum
    /// </summary>
    private void ManageBreadCum(string HoSoId, string UserControl)
    {
        linkCum.Text = "QUẢN LÝ HỒ SƠ";
        linkCum.NavigateUrl = "../WebUI/CN_HoSo_QuanLy.aspx?UserControl=" + UserControl + "&HoSoId=" + HoSoId + "";
    }
    #region "Hàm sử dụng chung cho các sự kiện trên ảnh và link"
    private void DeleteSanPham()
    {
        // Thực hiện xoá
        foreach (GridViewRow row in gvSanPham.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkCheck");
            if (chk.Checked)
            {
                string sanPhamId = gvSanPham.DataKeys[row.RowIndex][0].ToString();
                //kiem tra trang thai cua thong bao phi, chỉ có thể xóa thong bao phi mới
                int trangThaiId = Convert.ToInt32(gvSanPham.DataKeys[row.RowIndex][1].ToString());
                if (trangThaiId == (int)EnTrangThaiSanPhamList.CHO_XU_LY
                    || trangThaiId == (int)EnTrangThaiSanPhamList.THAM_DINH_DONG_Y
                    || trangThaiId == (int)EnTrangThaiSanPhamList.THAM_DINH_KHONG_DONG_Y)
                {
                    TransactionManager transaction = ProviderFactory.Transaction;
                    try
                    {
                        //Xoa tai lieu dinh kem truoc khi xoa san pham
                        ProviderFactory.TaiLieuDinhKemProvider.XoaTaiLieuDinhKemBySanPhamID(sanPhamId, transaction);
                        //Xoa tieu chuan ap dung truoc khi xoa san pham
                        ProviderFactory.SanPhamTieuChuanApDungProvider.XoaTieuChuanApDungBySanPhamID(sanPhamId, transaction);
                        //Xoa qua trinh xu ly
                        ProviderFactory.QuaTrinhXuLyProvider.XoaQuaTrinhXuLyBySanPhamID(sanPhamId, transaction);
                        //Xoa san pham
                        ProviderFactory.SanPhamProvider.Delete(transaction, sanPhamId);
                        transaction.Commit();
                        deleteSPSuccess.Add(sanPhamId);
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                        {
                            switch (ex.Errors[0].Number)
                            {
                                case 547: // Foreign Key violation
                                    deleteSPFail.Add(sanPhamId);
                                    break;
                                default:
                                    deleteSPFail.Add(sanPhamId);
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        deleteSPFail.Add(sanPhamId);
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                }
                else
                {
                    deleteSPFail.Add(sanPhamId);
                }
            }
        }

        // Đưa ra thông báo thành công và ghi log sự kiện
        string thongbao = "";
        if (deleteSPSuccess.Count > 0)
        {
            thongbao = String.Format(Resources.Resource.msgXoaSanPhamThanhCong, string.Join(",", deleteSPSuccess.ToArray()));
            ProviderFactory.SysLogProvier.Write(((PageBase)this.Page).mUserInfo, SysEventList.CN_SAN_PHAM_XOA, thongbao);
        }
        if (deleteSPFail.Count > 0)
        {
            thongbao += String.Format(Resources.Resource.msgXoaSanPhamThatBai, string.Join(",", deleteSPFail.ToArray()));
        }

        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('" + thongbao + "');location.href='CN_HoSoSanPham_QuanLy.aspx?HoSoId=" + HoSoId + "&UserControl=CN_HoSoDen';</script>");
    }
    private void AddSanPham()
    {
        Response.Redirect("CN_TiepNhanHoSo_TaoMoi.aspx?Direct=CN_HoSoSanPham_QuanLy&Action=add&HoSoID=" + HoSoId + "&UserControl=CN_HoSoDen");
    }
    private void SendThongBaoPhi()
    {
        // Thực hiện gui
        foreach (GridViewRow row in gvPhi.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkCheck");
            if (chk.Checked)
            {
                string thongbaophiId = gvPhi.DataKeys[row.RowIndex][0].ToString();
                //kiem tra trang thai cua thong bao phi, chỉ có thể xóa thong bao phi mới
                int trangThaiId = Convert.ToInt32(gvPhi.DataKeys[row.RowIndex][1].ToString());
                //so giay thong bao phi
                string sothongbaophi = gvPhi.DataKeys[row.RowIndex][2].ToString();
                if (trangThaiId == (int)EnTrangThaiThongBaoPhiList.MOI_TAO)
                {
                    TransactionManager transaction = ProviderFactory.Transaction;
                    try
                    {
                        //liet ke tat ca cac sanphamId theo thongbaophiId
                        List<string> lstSanPhamId = ProviderFactory.ThongBaoLePhiSanPhamProvider.LaySanPhamIDTheoThongBaoLePhiID(thongbaophiId);
                        foreach (string sanphamId in lstSanPhamId)
                        {
                            //chuyen trang thai thanh cho phe duyet
                            SanPham objSp = ProviderFactory.SanPhamProvider.GetById(sanphamId);
                            objSp.TrangThaiId = (int)EnTrangThaiSanPhamList.CHO_PHE_DUYET;
                            ProviderFactory.SanPhamProvider.Save(transaction, objSp);
                        }
                        //chuyen trang thai thong bao phi ve cho phe duyet
                        ThongBaoLePhi objTBLP = ProviderFactory.ThongBaoLePhiProvider.GetById(thongbaophiId);
                        objTBLP.TrangThaiId = (int)EnTrangThaiThongBaoPhiList.CHO_PHE_DUYET;
                        ProviderFactory.ThongBaoLePhiProvider.Save(transaction, objTBLP);

                        transaction.Commit();
                        sendSuccess.Add(sothongbaophi);
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                        {
                            switch (ex.Errors[0].Number)
                            {
                                case 547: // Foreign Key violation
                                    sendFail.Add(sothongbaophi);
                                    break;
                                default:
                                    sendFail.Add(sothongbaophi);
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        sendFail.Add(sothongbaophi);
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                }
                else
                {
                    sendFail.Add(sothongbaophi);
                }
            }
        }

        // Đưa ra thông báo thành công và ghi log sự kiện
        string thongbao = "";
        if (sendSuccess.Count > 0)
        {
            thongbao = String.Format(Resources.Resource.msgGuiThongBaoPhiThanhCong, string.Join(",", sendSuccess.ToArray()));
            ProviderFactory.SysLogProvier.Write(((PageBase)this.Page).mUserInfo, SysEventList.CB_HO_SO_XOA, thongbao);
        }
        if (sendFail.Count > 0)
        {
            thongbao += String.Format(Resources.Resource.msgGuiThongBaoPhiThatBai, string.Join(",", sendFail.ToArray()));
        }

        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('" + thongbao + "');location.href='CN_HoSoSanPham_QuanLy.aspx?HoSoId=" + HoSoId + "&UserControl=CN_HoSoDen';</script>");
    }
    private void DeleteThongBaoPhi()
    {
        // Thực hiện xoá
        foreach (GridViewRow row in gvPhi.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkCheck");
            if (chk.Checked)
            {
                //ma thong bao phi
                string thongbaophiId = gvPhi.DataKeys[row.RowIndex][0].ToString();
                //kiem tra trang thai cua thong bao phi, chỉ có thể xóa thong bao phi mới
                int trangThaiId = Convert.ToInt32(gvPhi.DataKeys[row.RowIndex][1].ToString());
                //so giay thong bao phi
                string sothongbaophi = gvPhi.DataKeys[row.RowIndex][2].ToString();
                if (trangThaiId == (int)EnTrangThaiThongBaoPhiList.MOI_TAO)
                {
                    TransactionManager transaction = ProviderFactory.Transaction;
                    try
                    {
                        ProviderFactory.ThongBaoLePhiSanPhamProvider.XoaThongBaoLePhiSanPhamTheoThongBaoLePhiID(thongbaophiId, transaction);
                        ProviderFactory.ThongBaoLePhiChiTietProvider.DeleteByThongBaoLePhiId(thongbaophiId, transaction);
                        ProviderFactory.ThongBaoLePhiProvider.Delete(transaction, thongbaophiId);
                        transaction.Commit();
                        deleteSuccess.Add(sothongbaophi);
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                        {
                            switch (ex.Errors[0].Number)
                            {
                                case 547: // Foreign Key violation
                                    deleteFail.Add(sothongbaophi);
                                    break;
                                default:
                                    deleteFail.Add(sothongbaophi);
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        deleteFail.Add(sothongbaophi);
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                }
                else
                {
                    deleteFail.Add(sothongbaophi);
                }
            }
        }

        // Đưa ra thông báo thành công và ghi log sự kiện
        string thongbao = "";
        if (deleteSuccess.Count > 0)
        {
            thongbao = String.Format(Resources.Resource.msgXoaThongBaoPhi, string.Join(",", deleteSuccess.ToArray()));
            ProviderFactory.SysLogProvier.Write(((PageBase)this.Page).mUserInfo, SysEventList.CB_HO_SO_XOA, thongbao);
        }
        if (deleteFail.Count > 0)
        {
            thongbao += String.Format(Resources.Resource.msgXoaThongBaoPhiThatBai, string.Join(",", deleteFail.ToArray()));
        }

        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('" + thongbao + "');location.href='CN_HoSoSanPham_QuanLy.aspx?HoSoId=" + HoSoId + "&UserControl=CN_HoSoDen';</script>");
    }
    #endregion "Hàm sử dụng chung cho các sự kiện trên ảnh và link"

    #region "Thêm mới, xóa sản phẩm. Gửi, xóa thông báo phí"
    /// <summary>
    /// Thêm mới sản phẩm
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkThemMoiSanPham_Click(object sender, EventArgs e)
    {
        AddSanPham();
    }
    /// <summary>
    /// Xóa sản phẩm, chỉ áp dụng với các sản phẩm có trạng thái là đang xử lý
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkXoaSanPham_Click(object sender, EventArgs e)
    {
        DeleteSanPham();
    }
    /// <summary>
    /// Gửi thông báo phí
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    ///<Modified>
    /// Author      Data        Comments
    /// Quannm      18/05/2009  Edited
    ///</Modified>
    protected void lnkGuiTBP_Click(object sender, EventArgs e)
    {
        SendThongBaoPhi();
    }
    /// <summary>
    /// Xóa thông báo phí, chỉ áp dụng cho role xử lý và trạng thái là mới tạo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    ///<Modified>
    /// Author      Data        Comments
    /// Quannm      18/05/2009  Edited
    ///</Modified>
    protected void lnkXoaTBP_Click(object sender, EventArgs e)
    {
        DeleteThongBaoPhi();
    }
    protected void ImgBtnThemmoiSanPham_Click(object sender, ImageClickEventArgs e)
    {
        AddSanPham();
    }
    protected void imgXoaSanPham_Click(object sender, ImageClickEventArgs e)
    {
        DeleteSanPham();
    }
    protected void imgGuiTBP_Click(object sender, ImageClickEventArgs e)
    {
        SendThongBaoPhi();
    }
    protected void imgXoaTBP_Click(object sender, ImageClickEventArgs e)
    {
        DeleteThongBaoPhi();
    }
    #endregion "Thêm mới, xóa sản phẩm. Gửi, xóa thông báo phí"

    private void KiemTraTrangThaiSanPham(string HoSoId)
    {
        // Kiểm tra danh sách các sản phẩm trong hồ sơ, nếu tất cả đã xử lý xong thì chuyển hồ sơ sang trạng thái chờ lưu trữ
        TList<SanPham> lstSanPham = ProviderFactory.SanPhamProvider.GetByHoSoId(HoSoId);
        if (lstSanPham.Count > 0)
        {
            int count = 0;
            foreach (SanPham objSP in lstSanPham)
            {
                if (objSP.TrangThaiId == (int)EnTrangThaiSanPhamList.GD_PHE_DUYET || objSP.TrangThaiId == (int)EnTrangThaiSanPhamList.DA_CAP_GCN || objSP.TrangThaiId == (int)EnTrangThaiSanPhamList.HUY_GCN)
                    //|| objSP.TrangThaiId == (int)EnTrangThaiSanPhamList.KET_THUC
                    //|| objSP.TrangThaiId == (int)EnTrangThaiSanPhamList.HUY
                    //
                    //
                    count++;
            }
            if (count == lstSanPham.Count && ((PageBase)this.Page).mUserInfo.IsNguoiXuLy(HoSoId))
            {
                //LongHH
                HoSo objHs = ProviderFactory.HoSoProvider.GetById(HoSoId);
                LinkBtnCompleteHS.Visible = (EnNguonGocList)objHs.NguonGocId == EnNguonGocList.SX_TRONG_NUOC_KHONG_CO_ISO;
                //LinkBtnCompleteHS.Visible = true;
                //LongHH
                ImgBtnCompleteHS.Visible = true;

                gvSanPham.Columns[7].Visible = false;

                lnkXoaSanPham.Visible = false;
                imgXoaSanPham.Visible = false;
            }
        }
    }

    protected void LinkBtnCompleteHS_Click(object sender, EventArgs e)
    {
        GuiLuuTru();
    }
    protected void ImgBtnCompleteHS_Click(object sender, ImageClickEventArgs e)
    {
        GuiLuuTru();
    }

    private void GuiLuuTru()
    {
        //Chuyển hồ sơ sang trạng thái chờ lưu trữ
        HoSo objHoSo = ProviderFactory.HoSoProvider.GetById(HoSoId);
        objHoSo.TrangThaiId = (int)EnTrangThaiHoSoList.CHO_LUU_TRU;

        //thêm thông tin hồ sơ+sản phẩm(có tên người ký và chức vụ)+sản phẩm tiêu chuẩn áp dụng, thong bao le phi
        string strDmDonViId = objHoSo.DonViId;
        string strDmTrungTamId = objHoSo.TrungTamId;
        DmDonVi objDonVi = ProviderFactory.DmDonViProvider.GetById(strDmDonViId);
        DmTrungTam objTrungTam = ProviderFactory.DmTrungTamProvider.GetById(strDmTrungTamId);
        objHoSo.DmDonViDiaChi = objDonVi.DiaChi;
        objHoSo.DmDonViDienThoai = objDonVi.DienThoai;
        objHoSo.DmDonViEmail = objDonVi.Email;
        objHoSo.DmDonViFax = objDonVi.Fax;
        objHoSo.DmDonViMaDonVi = objDonVi.MaDonVi;
        objHoSo.DmDonViTenTiengAnh = objDonVi.TenTiengAnh;
        objHoSo.DmDonViTenTiengViet = objDonVi.TenTiengViet;
        objHoSo.DmDonViTenVietTat = objDonVi.TenVietTat;
        objHoSo.DmDonViTinhThanhId = objDonVi.TinhThanhId;

        objHoSo.DmTrungTamDiaChi = objTrungTam.DiaChi;
        objHoSo.DmTrungTamDiaChiCoQuanThuHuong = objTrungTam.DiaChiCoQuanThuHuong;
        objHoSo.DmTrungTamDiaChiCoQuanThuHuongCuc = objTrungTam.DiaChiCoQuanThuHuongCuc;
        objHoSo.DmTrungTamDienThoai = objTrungTam.DienThoai;
        objHoSo.DmTrungTamEmail = objTrungTam.Email;
        objHoSo.DmTrungTamFax = objTrungTam.Fax;
        objHoSo.DmTrungTamGiamDocId = objTrungTam.GiamDocId;
        objHoSo.DmTrungTamMaTrongGcn = objTrungTam.MaTrongGcn;
        objHoSo.DmTrungTamMaTrungTam = objTrungTam.MaTrungTam;
        objHoSo.DmTrungTamSoTaiKhoan = objTrungTam.SoTaiKhoan;
        objHoSo.DmTrungTamSoTaiKhoanCuc = objTrungTam.SoTaiKhoanCuc;
        objHoSo.DmTrungTamTenCoQuanThuHuong = objTrungTam.TenCoQuanThuHuong;
        objHoSo.DmTrungTamTenCoQuanThuHuongCuc = objTrungTam.TenCoQuanThuHuongCuc;
        objHoSo.DmTrungTamTenKhoBac = objTrungTam.TenKhoBac;
        objHoSo.DmTrungTamTenKhoBacCuc = objTrungTam.TenKhoBacCuc;
        objHoSo.DmTrungTamTenTiengAnh = objTrungTam.TenTiengAnh;
        objHoSo.DmTrungTamTenTrungTam = objTrungTam.TenTrungTam;
        objHoSo.DmTrungTamTinhThanhId = objTrungTam.TinhThanhId;

        // Cập nhật thông tin sản phẩm
        TList<SanPham> listSanPham = ProviderFactory.SanPhamProvider.GetByHoSoId(objHoSo.Id);
        foreach (SanPham sp in listSanPham)
        {
            DmSanPham objSanPham = ProviderFactory.DmSanPhamProvider.GetById(sp.SanPhamId);
            DmNhomSanPham objNhomSanPham = ProviderFactory.DmNhomSanPhamProvider.GetById(sp.NhomSanPhamId);
            DmCoQuanDoKiem objCoQuanDoKiem = ProviderFactory.DmCoQuanDoKiemProvider.GetById(sp.CoQuanDoKiemId);
            DmHangSanXuat objHangSanXuat = ProviderFactory.DmHangSanXuatProvider.GetById(sp.HangSanXuatId);
            SysUser objUser = ProviderFactory.SysUserProvider.GetById(sp.NguoiKyDuyetId);

            DmTrungTam trungtam = ProviderFactory.DmTrungTamProvider.GetById(objUser.OrganizationId);
            sp.TenTrungTam = trungtam.TenTrungTam;

            DmPhongBan phongban = ProviderFactory.DmPhongBanProvider.GetById(objUser.DepartmentId);
            if (phongban != null)
                sp.PhongBan = phongban.TenPhongBan;

            if (objCoQuanDoKiem != null)
            {
                sp.DmCqdkDiaChi = objCoQuanDoKiem.DiaChi;
                sp.DmCqdkTenCoQuanDoKiem = objCoQuanDoKiem.TenCoQuanDoKiem;
                sp.DmCqdkTenTiengAnh = objCoQuanDoKiem.TenTiengAnh;
            }
            if (objHangSanXuat != null)
            {
                sp.DmHsxTenHangSanXuat = objHangSanXuat.TenHangSanXuat;
                sp.DmHsxTenTiengAnh = objHangSanXuat.TenTiengAnh;
            }
            if (objNhomSanPham != null)
            {
                sp.DmNhomSpLienQuanTanSo = objNhomSanPham.LienQuanTanSo;
                sp.DmNhomSpLoaiHinhChungNhan = objNhomSanPham.LoaiHinhChungNhan;
                sp.DmNhomSpMaNhom = objNhomSanPham.MaNhom;
                sp.DmNhomSpMucLePhi = objNhomSanPham.MucLePhi;
                sp.DmNhomSpTenNhom = objNhomSanPham.TenNhom;
                sp.DmNhomSpThoiHanGcn = objNhomSanPham.ThoiHanGcn;
            }
            if (objSanPham != null)
            {
                sp.DmSanPhamLoaiSanPham = objSanPham.LoaiSanPham;
                sp.DmSanPhamLoaiTieuChuanApDung = objSanPham.LoaiTieuChuanApDung;
                sp.DmSanPhamMaSanPham = objSanPham.MaSanPham;
                sp.DmSanPhamNhomSanPhamId = objSanPham.NhomSanPhamId;
                sp.DmSanPhamTenTiengAnh = objSanPham.TenTiengAnh;
                sp.DmSanPhamTenTiengViet = objSanPham.TenTiengViet;
            }

            if (objUser != null)
            {
                sp.TenNguoiKyDuyet = objUser.FullName;
                sp.ChucVu = Convert.ToInt32(objUser.Position);
            }
            // Cập nhật thông tin tiêu chuẩn
            TList<SanPhamTieuChuanApDung> listSanPhamTieuChuanApDung = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(sp.Id);
            foreach (SanPhamTieuChuanApDung spad in listSanPhamTieuChuanApDung)
            {
                DmTieuChuan objTieuChuan = ProviderFactory.DmTieuChuanProvider.GetById(spad.TieuChuanApDungId);
                if (objTieuChuan != null)
                {
                    spad.DmTieuChuanMaTieuChuan = objTieuChuan.MaTieuChuan;
                    spad.DmTieuChuanTenTieuChuan = objTieuChuan.TenTieuChuan;
                    ProviderFactory.SanPhamTieuChuanApDungProvider.Save(spad);
                }
            }

            // Cập nhật thông tin lệ phí
           TList< ThongBaoLePhiSanPham> lstLPSP = ProviderFactory.ThongBaoLePhiSanPhamProvider.GetBySanPhamId(sp.Id);
           if (lstLPSP.Count > 0)
           {
               ThongBaoLePhiSanPham objLPSP = lstLPSP[0];
               if (objLPSP != null)
               {
                   objLPSP.TenSanPham = sp.DmSanPhamTenTiengViet;
                   objLPSP.LePhi = sp.DmNhomSpMucLePhi;
                   ProviderFactory.ThongBaoLePhiSanPhamProvider.Save(objLPSP);
               }
           }
            if (sp.KetLuanId != (int)EnKetLuanList.CAP_GCN)
                sp.TrangThaiId = (int)EnTrangThaiSanPhamList.KET_THUC;
            ProviderFactory.SanPhamProvider.Save(sp);
        }

        // Cập nhật thông tin trong thông báo lệ phí
        TList<ThongBaoLePhi> lstThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetByHoSoId(HoSoId);
        foreach (ThongBaoLePhi objTBLP in lstThongBaoLePhi)
        {
            SysUser objUser = ProviderFactory.SysUserProvider.GetById(objTBLP.NguoiPheDuyetId);
            if (objUser != null)
            {
                objTBLP.TenNguoiKyDuyet = objUser.FullName;
                objTBLP.ChucVu = Convert.ToInt32(objUser.Position);
                ProviderFactory.ThongBaoLePhiProvider.Save(objTBLP);
            }

            // Cập nhật thông tin lệ phí chi tiết
            TList<ThongBaoLePhiChiTiet> lstChiTiet = ProviderFactory.ThongBaoLePhiChiTietProvider.GetByThongBaoLePhiId(objTBLP.Id);
            foreach (ThongBaoLePhiChiTiet objChiTiet in lstChiTiet)
            {
                if (objChiTiet.LoaiPhiId == (int)EnLoaiPhiList.PHI_DANH_GIA_QUY_TRINH_SX)
                    objChiTiet.MucPhi = Convert.ToInt32(EntityHelper.GetEnumTextValue(EnLoaiPhiList.PHI_DANH_GIA_QUY_TRINH_SX));

                if (objChiTiet.LoaiPhiId == (int)EnLoaiPhiList.PHI_LAY_MAU_SAN_PHAM)
                    objChiTiet.MucPhi = Convert.ToInt32(EntityHelper.GetEnumTextValue(EnLoaiPhiList.PHI_LAY_MAU_SAN_PHAM));

                if (objChiTiet.LoaiPhiId == (int)EnLoaiPhiList.PHI_CHUNG_NHAN_HC)
                    objChiTiet.MucPhi = Convert.ToInt32(EntityHelper.GetEnumTextValue(EnLoaiPhiList.PHI_CHUNG_NHAN_HC));

                if (objChiTiet.LoaiPhiId == (int)EnLoaiPhiList.PHI_CHUNG_NHAN_HQ)
                    objChiTiet.MucPhi = Convert.ToInt32(EntityHelper.GetEnumTextValue(EnLoaiPhiList.PHI_CHUNG_NHAN_HQ));

                ProviderFactory.ThongBaoLePhiChiTietProvider.Save(objChiTiet);
            }
        }

        ProviderFactory.HoSoProvider.Save(objHoSo);
        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Gửi lưu trữ thành công');location.href='CN_HoSo_QuanLy.aspx?UserControl=CN_HoSoDen';</script>");
    }
    protected void IBThemMoi_Click(object sender, ImageClickEventArgs e)
    {

    }
    //LongHH
    protected void imgBtnSuaPhiTiepNhan_Click(object sender, EventArgs e)
    {

    }
    protected void LBTaoTiepNhan_Click(object sender, EventArgs e)
    {

    }
    #region Danh Gia QTSX - CNHQ
    protected void IBThemMoiCNHQ_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void LBThemMoiCNHQ_Click(object sender, EventArgs e)
    {

    }
    protected void IBGuiCNHQ_Click(object sender, ImageClickEventArgs e)
    {
        SendThongBaoNopTienCNHQ();
    }
    protected void LBGuiCNHQ_Click(object sender, EventArgs e)
    {
        SendThongBaoNopTienCNHQ();
    }
    protected void IBXoaCNHQ_Click(object sender, ImageClickEventArgs e)
    {
        DeleteThongBaoNopTienCNHQ();
    }
    protected void LBXoaCNHQ_Click(object sender, EventArgs e)
    {
        DeleteThongBaoNopTienCNHQ();
    }
    private void SendThongBaoNopTienCNHQ()
    {
        CheckBox chk;
        // Thực hiện gui
        foreach (GridViewRow row in gvThongBaoNopTienCNHQ.Rows)
        {
            chk = (CheckBox)(row.Cells[7].FindControl("chkCheck"));
            if (chk.Checked)
            {
                string thongbaophiId = gvThongBaoNopTienCNHQ.DataKeys[row.RowIndex][0].ToString();
                //kiem tra trang thai cua thong bao phi, chỉ có thể xóa thong bao phi mới
                int trangThaiId = Convert.ToInt32(gvThongBaoNopTienCNHQ.DataKeys[row.RowIndex][1].ToString());
                //so giay thong bao phi
                string sothongbaophi = gvThongBaoNopTienCNHQ.DataKeys[row.RowIndex][2].ToString();
                if (trangThaiId == (int)EnTrangThaiThongBaoPhiList.MOI_TAO || trangThaiId == (int)EnTrangThaiThongBaoPhiList.GD_KHONG_PHE_DUYET)
                {
                    TransactionManager transaction = ProviderFactory.Transaction;
                    try
                    {
                        //chuyen trang thai thong bao phi ve cho phe duyet
                        ThongBaoLePhi objTBLP = ProviderFactory.ThongBaoLePhiProvider.GetById(thongbaophiId);
                        objTBLP.TrangThaiId = (int)EnTrangThaiThongBaoPhiList.CHO_PHE_DUYET;
                        ProviderFactory.ThongBaoLePhiProvider.Save(transaction, objTBLP);

                        transaction.Commit();
                        sendSuccess.Add(sothongbaophi);
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                        {
                            switch (ex.Errors[0].Number)
                            {
                                case 547: // Foreign Key violation
                                    sendFail.Add(sothongbaophi);
                                    break;
                                default:
                                    sendFail.Add(sothongbaophi);
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        sendFail.Add(sothongbaophi);
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                }
                else
                {
                    sendFail.Add(sothongbaophi);
                }
            }
        }

        // Đưa ra thông báo thành công và ghi log sự kiện
        string thongbao = "";
        if (sendSuccess.Count > 0)
        {
            thongbao = String.Format(Resources.Resource.msgGuiThongBaoPhiThanhCong, string.Join(",", sendSuccess.ToArray()));
            ProviderFactory.SysLogProvier.Write(((PageBase)this.Page).mUserInfo, SysEventList.CN_THU_LE_PHI, thongbao);
        }
        if (sendFail.Count > 0)
        {
            thongbao += String.Format(Resources.Resource.msgGuiThongBaoPhiThatBai, string.Join(",", sendFail.ToArray()));
        }
        //Response.Redirect("CN_HoSoSanPham.aspx?direct=CN_HoSoMoi&HoSoID=" + HoSoId + "&UserControl=CN_HoSoDen&TrangThaiId=" + TrangThaiId, false);
        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('" + thongbao + "');location.href='CN_HoSoSanPham_QuanLy.aspx?HoSoId=" + HoSoId + "&UserControl=CN_HoSoDen';</script>");
    }
    private void DeleteThongBaoNopTienCNHQ()
    {
        CheckBox chk;
        // Thực hiện xoá
        foreach (GridViewRow row in gvThongBaoNopTienCNHQ.Rows)
        {
            chk = (CheckBox)(row.Cells[7].FindControl("chkCheck"));
            if (chk.Checked)
            {
                //ma thong bao phi
                string thongbaophiId = gvThongBaoNopTienCNHQ.DataKeys[row.RowIndex][0].ToString();
                //kiem tra trang thai cua thong bao phi, chỉ có thể xóa thong bao phi mới
                int trangThaiId = Convert.ToInt32(gvThongBaoNopTienCNHQ.DataKeys[row.RowIndex][1].ToString());
                //so giay thong bao phi
                string sothongbaophi = gvThongBaoNopTienCNHQ.DataKeys[row.RowIndex][2].ToString();
                if (trangThaiId == (int)EnTrangThaiThongBaoPhiList.MOI_TAO
                    || trangThaiId == (int)EnTrangThaiThongBaoPhiList.GD_KHONG_PHE_DUYET
                    || trangThaiId == (int)EnTrangThaiThongBaoPhiList.CHO_PHE_DUYET)
                {
                    TransactionManager transaction = ProviderFactory.Transaction;
                    try
                    {
                        ProviderFactory.ThongBaoLePhiChiTietProvider.DeleteByThongBaoLePhiId(thongbaophiId, transaction);
                        ProviderFactory.ThongBaoLePhiProvider.Delete(transaction, thongbaophiId);
                        transaction.Commit();
                        deleteSuccess.Add(sothongbaophi);
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                        {
                            switch (ex.Errors[0].Number)
                            {
                                case 547: // Foreign Key violation
                                    deleteFail.Add(sothongbaophi);
                                    break;
                                default:
                                    deleteFail.Add(sothongbaophi);
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        deleteFail.Add(sothongbaophi);
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                }
                else
                {
                    deleteFail.Add(sothongbaophi);
                }
            }
        }

        // Đưa ra thông báo thành công và ghi log sự kiện
        string thongbao = "";
        if (deleteSuccess.Count > 0)
        {
            thongbao = String.Format(Resources.Resource.msgXoaThongBaoPhi, string.Join(",", deleteSuccess.ToArray()));
            ProviderFactory.SysLogProvier.Write(((PageBase)this.Page).mUserInfo, SysEventList.CB_HO_SO_XOA, thongbao);
        }
        if (deleteFail.Count > 0)
        {
            thongbao += String.Format(Resources.Resource.msgXoaThongBaoPhiThatBai, string.Join(",", deleteFail.ToArray()));
        }

        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('" + thongbao + "');location.href='CN_HoSoSanPham_QuanLy.aspx?HoSoId=" + HoSoId + "&UserControl=CN_HoSoDen';</script>");
    }
    /// <summary>
    /// Danh sách thông báo nộp tiền của hồ sơ nếu có
    /// </summary>
    /// <Modified>
    /// Author      Date            Comments    
    /// Quannm      03/03/2010      Create new
    ///</Modified>
    private void BindThongBaoNopTienCNHQGrid()
    {
        DataTable dtThongBaoPhi = new DataTable();
        dtThongBaoPhi = ProviderFactory.ThongBaoLePhiProvider.ThongBaoNopTienTheoHoSo(HoSoId);
        gvThongBaoNopTienCNHQ.DataSource = dtThongBaoPhi;
        gvThongBaoNopTienCNHQ.DataBind();

        if (dtThongBaoPhi.Rows.Count > 0)
        {
            IBXoaCNHQ.Enabled = true;
            LBXoaCNHQ.Enabled = true;
        }
    }
    /// <summary>
    /// Phân trang
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    ///     Author      Date          Comments
    ///     Quannm      03/03/2010      Create new
    /// </Modified>
    protected void gvThongBaoNopTienCNHQ_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvThongBaoNopTienCNHQ.PageIndex = e.NewPageIndex;
        BindThongBaoNopTienCNHQGrid();
    }
    protected void gvThongBaoNopTienCNHQ_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Chi cho phep check (xoa, gui lanh dao) voi cac thong bao phi moi tao, hoac GD ko phe duyet
            if (((Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) != (int)EnTrangThaiThongBaoPhiList.MOI_TAO)
                && (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) != (int)EnTrangThaiThongBaoPhiList.GD_KHONG_PHE_DUYET))
                || !((PageBase)this.Page).mUserInfo.IsNguoiXuLy(Request["HosoID"].ToString())
                )
            {
                Control chk = e.Row.FindControl("chkCheck");
                chk.Visible = false;

                Control chkAll = gvThongBaoNopTienCNHQ.HeaderRow.FindControl("chkCheckAll");
                chkAll.Visible = false;
            }


        }
    }
    #endregion
    //LongHH
    #region Danh Gia QTSX - CNHC
    protected void LBThemMoi_Click(object sender, EventArgs e)
    {

    }
    protected void IBGui_Click(object sender, ImageClickEventArgs e)
    {
        SendThongBaoNopTien();
    }
    protected void LBGui_Click(object sender, EventArgs e)
    {
        SendThongBaoNopTien();
    }

    protected void IBXoa_Click(object sender, ImageClickEventArgs e)
    {
        DeleteThongBaoNopTien();
    }
    protected void LBXoa_Click(object sender, EventArgs e)
    {
        DeleteThongBaoNopTien();
    }
    private void SendThongBaoNopTien()
    {
        CheckBox chk;
        // Thực hiện gui
        foreach (GridViewRow row in gvThongBaoNopTien.Rows)
        {
            chk = (CheckBox)(row.Cells[7].FindControl("chkCheck"));
            if (chk.Checked)
            {
                string thongbaophiId = gvThongBaoNopTien.DataKeys[row.RowIndex][0].ToString();
                //kiem tra trang thai cua thong bao phi, chỉ có thể xóa thong bao phi mới
                int trangThaiId = Convert.ToInt32(gvThongBaoNopTien.DataKeys[row.RowIndex][1].ToString());
                //so giay thong bao phi
                string sothongbaophi = gvThongBaoNopTien.DataKeys[row.RowIndex][2].ToString();
                if (trangThaiId == (int)EnTrangThaiThongBaoPhiList.MOI_TAO || trangThaiId == (int)EnTrangThaiThongBaoPhiList.GD_KHONG_PHE_DUYET)
                {
                    TransactionManager transaction = ProviderFactory.Transaction;
                    try
                    {
                        //chuyen trang thai thong bao phi ve cho phe duyet
                        ThongBaoLePhi objTBLP = ProviderFactory.ThongBaoLePhiProvider.GetById(thongbaophiId);
                        objTBLP.TrangThaiId = (int)EnTrangThaiThongBaoPhiList.CHO_PHE_DUYET;
                        ProviderFactory.ThongBaoLePhiProvider.Save(transaction, objTBLP);

                        transaction.Commit();
                        sendSuccess.Add(sothongbaophi);
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                        {
                            switch (ex.Errors[0].Number)
                            {
                                case 547: // Foreign Key violation
                                    sendFail.Add(sothongbaophi);
                                    break;
                                default:
                                    sendFail.Add(sothongbaophi);
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        sendFail.Add(sothongbaophi);
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                }
                else
                {
                    sendFail.Add(sothongbaophi);
                }
            }
        }

        // Đưa ra thông báo thành công và ghi log sự kiện
        string thongbao = "";
        if (sendSuccess.Count > 0)
        {
            thongbao = String.Format(Resources.Resource.msgGuiThongBaoPhiThanhCong, string.Join(",", sendSuccess.ToArray()));
            ProviderFactory.SysLogProvier.Write(((PageBase)this.Page).mUserInfo, SysEventList.CN_THU_LE_PHI, thongbao);
        }
        if (sendFail.Count > 0)
        {
            thongbao += String.Format(Resources.Resource.msgGuiThongBaoPhiThatBai, string.Join(",", sendFail.ToArray()));
        }
        //Response.Redirect("CN_HoSoSanPham.aspx?direct=CN_HoSoMoi&HoSoID=" + HoSoId + "&UserControl=CN_HoSoDen&TrangThaiId=" + TrangThaiId, false);
        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('" + thongbao + "');location.href='CN_HoSoSanPham_QuanLy.aspx?HoSoId=" + HoSoId + "&UserControl=CN_HoSoDen';</script>");
    }
    private void DeleteThongBaoNopTien()
    {
        CheckBox chk;
        // Thực hiện xoá
        foreach (GridViewRow row in gvThongBaoNopTien.Rows)
        {
            chk = (CheckBox)(row.Cells[7].FindControl("chkCheck"));
            if (chk.Checked)
            {
                //ma thong bao phi
                string thongbaophiId = gvThongBaoNopTien.DataKeys[row.RowIndex][0].ToString();
                //kiem tra trang thai cua thong bao phi, chỉ có thể xóa thong bao phi mới
                int trangThaiId = Convert.ToInt32(gvThongBaoNopTien.DataKeys[row.RowIndex][1].ToString());
                //so giay thong bao phi
                string sothongbaophi = gvThongBaoNopTien.DataKeys[row.RowIndex][2].ToString();
                if (trangThaiId == (int)EnTrangThaiThongBaoPhiList.MOI_TAO 
                    || trangThaiId == (int)EnTrangThaiThongBaoPhiList.GD_KHONG_PHE_DUYET
                    || trangThaiId == (int)EnTrangThaiThongBaoPhiList.CHO_PHE_DUYET)
                {
                    TransactionManager transaction = ProviderFactory.Transaction;
                    try
                    {
                        ProviderFactory.ThongBaoLePhiChiTietProvider.DeleteByThongBaoLePhiId(thongbaophiId, transaction);
                        ProviderFactory.ThongBaoLePhiProvider.Delete(transaction, thongbaophiId);
                        transaction.Commit();
                        deleteSuccess.Add(sothongbaophi);
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                        {
                            switch (ex.Errors[0].Number)
                            {
                                case 547: // Foreign Key violation
                                    deleteFail.Add(sothongbaophi);
                                    break;
                                default:
                                    deleteFail.Add(sothongbaophi);
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        deleteFail.Add(sothongbaophi);
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                }
                else
                {
                    deleteFail.Add(sothongbaophi);
                }
            }
        }

        // Đưa ra thông báo thành công và ghi log sự kiện
        string thongbao = "";
        if (deleteSuccess.Count > 0)
        {
            thongbao = String.Format(Resources.Resource.msgXoaThongBaoPhi, string.Join(",", deleteSuccess.ToArray()));
            ProviderFactory.SysLogProvier.Write(((PageBase)this.Page).mUserInfo, SysEventList.CB_HO_SO_XOA, thongbao);
        }
        if (deleteFail.Count > 0)
        {
            thongbao += String.Format(Resources.Resource.msgXoaThongBaoPhiThatBai, string.Join(",", deleteFail.ToArray()));
        }

        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('" + thongbao + "');location.href='CN_HoSoSanPham_QuanLy.aspx?HoSoId=" + HoSoId + "&UserControl=CN_HoSoDen';</script>");
    }



    /// <summary>
    /// Danh sách thông báo nộp tiền của hồ sơ nếu có
    /// </summary>
    /// <Modified>
    /// Author      Date            Comments    
    /// Quannm      03/03/2010      Create new
    ///</Modified>
    private void BindThongBaoNopTienGrid()
    {
        DataTable dtThongBaoPhi = new DataTable();
        dtThongBaoPhi = ProviderFactory.ThongBaoLePhiProvider.ThongBaoNopTienTheoHoSo(HoSoId);
        gvThongBaoNopTien.DataSource = dtThongBaoPhi;
        gvThongBaoNopTien.DataBind();

        if (dtThongBaoPhi.Rows.Count > 0)
        {
            IBXoa.Enabled = true;
            LBXoa.Enabled = true;
        }
    }
    
    /// <summary>
    /// Phân trang
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    ///     Author      Date          Comments
    ///     Quannm      03/03/2010      Create new
    /// </Modified>
    protected void gvThongBaoNopTien_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvThongBaoNopTien.PageIndex = e.NewPageIndex;
        BindThongBaoNopTienGrid();
    }
    protected void gvThongBaoNopTien_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Chi cho phep check (xoa, gui lanh dao) voi cac thong bao phi moi tao, hoac GD ko phe duyet
            if (((Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) != (int)EnTrangThaiThongBaoPhiList.MOI_TAO)
                && (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) != (int)EnTrangThaiThongBaoPhiList.GD_KHONG_PHE_DUYET))
                || !((PageBase)this.Page).mUserInfo.IsNguoiXuLy(Request["HosoID"].ToString())
                )
            {
                Control chk = e.Row.FindControl("chkCheck");
                chk.Visible = false;

                Control chkAll = gvThongBaoNopTien.HeaderRow.FindControl("chkCheckAll");
                chkAll.Visible = false;
            }


        }
    }
    #endregion
    /// <summary>
    /// Ẩn check box với các thông báo phi không phải là mới tạo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPhi_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Chi cho phep check (xoa, gui lanh dao) voi cac thong bao phi moi tao
            if ((Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) != (int)EnTrangThaiThongBaoPhiList.MOI_TAO)
                || !((PageBase)this.Page).mUserInfo.IsNguoiXuLy(Request["HosoID"].ToString()))
            {
                Control chk = e.Row.FindControl("chkCheck");
                chk.Visible = false;

                Control chkAll = gvPhi.HeaderRow.FindControl("chkCheckAll");
                chkAll.Visible = false;
            }


        }
    }
    
}
