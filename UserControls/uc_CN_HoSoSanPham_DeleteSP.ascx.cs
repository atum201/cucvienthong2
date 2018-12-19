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
public partial class UserControls_uc_CN_HoSoSanPham_DeleteSP : System.Web.UI.UserControl
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
    public int countsanpham = 0;
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
        //LongHH
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


        //LongHH

        if (!IsPostBack)
        {
            ManageBreadCum(HoSoId, strUserControl);
            BindChiTietHoSo();
            LayTatCaSanPhamTheoQuyenDangNhap();
            KiemTraTrangThaiSanPham(HoSoId);
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
        //DataTable dtbSanPham = ProviderFactory.SanPhamProvider.LayTatCaSanPhamTheoQuyenDangNhap(HoSoId, ((PageBase)this.Page).mUserInfo.UserID, ((PageBase)this.Page).mUserInfo.GetPermissionList("01"));
        DataTable dtbSanPham = QLCL_Patch.GetAllSanPhamByHoSo(HoSoId);
        gvSanPham.DataSource = dtbSanPham;
        gvSanPham.DataBind();
        countsanpham = dtbSanPham.Rows.Count;
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
            if (sp != null)
            {
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
                            lnkTrangThai.Attributes.Add("href", "../ReportForm/HienBaoCao.aspx?LoaiBaoCao=GiayChungNhan&SanphamID=" + sp.Id + "&TenBaoCao=GiayChungNhan&format=Word");
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
                bool d = QLCL_Patch.Delete_SanPham(sanPhamId);
                if(d)
                    deleteSPSuccess.Add(sanPhamId);
                else
                    deleteSPFail.Add(sanPhamId);
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

        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('" + thongbao + "');location.href='/WebUI/HoSo_Delete_SanPham.aspx?HoSoID=" + HoSoId + "'</script>");

    }
    
    
    #endregion "Hàm sử dụng chung cho các sự kiện trên ảnh và link"

    #region "Thêm mới, xóa sản phẩm. Gửi, xóa thông báo phí"
    
    /// <summary>
    /// Xóa sản phẩm, chỉ áp dụng với các sản phẩm có trạng thái là đang xử lý
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkXoaSanPham_Click(object sender, EventArgs e)
    {
        DeleteSanPham();
    }
    protected void imgXoaSanPham_Click(object sender, ImageClickEventArgs e)
    {
        DeleteSanPham();
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
        }
    }


}