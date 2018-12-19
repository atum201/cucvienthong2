using System;
using System.Data;
using System.IO;
using System.Text;
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
using Cuc_QLCL.Data;
using Resources;
public partial class WebUI_CB_PheDuyetHoSo : PageBase
{
    public string strSanPhamID = "";
    SanPham objSanPham = null;
    QuaTrinhXuLy objQuaTrinhXuLy = null;
    DmLePhi objDmLePhi = null;
    DmSanPham objDmSanPham = null;
    ThongBaoLePhiSanPham objThongBaoLPSP = null;
    ThongBaoLePhi objThongBaoLP = null;
    int intLePhi, intTongPhi, intTongPhiMoi;

    protected void Page_Load(object sender, EventArgs e)
    {
        btnInBanTiepNhan.OnClientClick = "OpenBaoCao(this,'../ReportForm/DieuKienInBaoCao.aspx?HoSoID=" + Request["HoSoID"].ToString() + "&SanPhamID=" + Request["SanPhamID"].ToString() + "&LoaiBaoCao=BanTiepNhan');return false;";
        if (!IsPostBack)
        {
            if (Request["SanPhamID"] != null)
            {
                strSanPhamID = Request["SanPhamID"].ToString();
                //Tryền tham số cho báo cáo
                Session["SAN_PHAM_ID"] = Request["SanPhamID"] != null ? Request["SanPhamID"].ToString() : "";
                HienThiThongTin(strSanPhamID);
                NoiDungXuLy(strSanPhamID);
                btnInBanTiepNhan.Visible = false;

            }

            //// dat mac dinh la khong phe duyet

            if (Request["action"] != null)
            {
                if (Request["action"].ToString() == "view")
                {
                    btnCapNhat.Visible = false;

                    // Disable các control ...
                }
            }
        }
    }
    /// <summary>
    ///Hien thi thông tin về hồ sơ sản phẩm
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// DucLv                 10/5/2009              
    /// </Modified>
    private void HienThiThongTin(string strSanPhamID)
    {

        if ((strSanPhamID != "") && (strSanPhamID != null))
        {
            objSanPham = new SanPham();
            objSanPham = ProviderFactory.SanPhamProvider.GetById(strSanPhamID);
            HoSo objHoSo = ProviderFactory.HoSoProvider.GetById(objSanPham.HoSoId);

            if (!IsPostBack)
            {

                objDmSanPham = new DmSanPham();
                objDmSanPham = ProviderFactory.DmSanPhamProvider.GetById(objSanPham.SanPhamId);
                lblTenSanPham.Text = objDmSanPham.TenTiengViet;                
                lblKyHieu.Text = objSanPham.KyHieu;
                lblNgaynhan.Text = objSanPham.NgayCapNhatSauCung.Value.ToShortDateString();
                lblhangsx.Text = ProviderFactory.DmHangSanXuatProvider.GetById(objSanPham.HangSanXuatId).TenHangSanXuat;
                lblSoGCN_TuDanhGia.Text = objSanPham.SoGcn.Length > 0 ? objSanPham.SoGcn : objSanPham.SoBanTuDanhGia;
                lblNgayDanhGia.Text = objSanPham.NgayDanhGia != null ? objSanPham.NgayDanhGia.Value.ToShortDateString() : string.Empty;
                lblSoBanCongBo.Text = objSanPham.SoBanCongBo;
                lblNgayCongBo.Text = objSanPham.NgayCongBo != null ? objSanPham.NgayCongBo.Value.ToShortDateString() : string.Empty;
                lblTrangThai.Text = EntityHelper.GetEnumTextValue((EnTrangThaiSanPhamList)objSanPham.TrangThaiId);
                TList<SanPhamTieuChuanApDung> lstSanPhamTieuChuan;
                lstSanPhamTieuChuan = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(strSanPhamID);

                string DsTieuChuan = string.Empty;
                foreach (SanPhamTieuChuanApDung objSanPhamTieuChuan in lstSanPhamTieuChuan)
                {
                    DsTieuChuan += ProviderFactory.DmTieuChuanProvider.GetById(objSanPhamTieuChuan.TieuChuanApDungId).TenTieuChuan + ", ";
                }

                if (DsTieuChuan.Length > 2)
                    lblTCAdung.Text = DsTieuChuan.Substring(0, DsTieuChuan.Length - 2);
                string giatri = ConfigurationManager.AppSettings["PhiCongBoHQ"].ToString();                

                if (objSanPham.TrangThaiId == (int)EnTrangThaiSanPhamList.GD_KHONG_DUYET || objSanPham.KetLuanId != (int)EnKetLuanList.CAP_BAN_TIEP_NHAN_CB)
                {
                    btnInBanTiepNhan.Visible = false;
                }
                //  lấy danh sách tài liệu
                TList<TaiLieuDinhKem> lstTaiLieuDinhKem;
                lstTaiLieuDinhKem = ProviderFactory.TaiLieuDinhKemProvider.GetBySanPhamId(strSanPhamID);

                foreach (TaiLieuDinhKem objTaiLieu in lstTaiLieuDinhKem)
                {
                    string FilePath = objTaiLieu.TenFile.Trim();
                    string FileName = FilePath.Substring(FilePath.LastIndexOf("/") + 1, FilePath.Length - FilePath.LastIndexOf("/") - 1);


                    if (objTaiLieu.LoaiTaiLieuId == (int)EnLoaiTaiLieuList.QUY_TRINH_SAN_XUAT)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<a href='" + FilePath + "'>" + EntityHelper.GetEnumTextValue(EnLoaiTaiLieuList.QUY_TRINH_SAN_XUAT) + "</a>");
                        lbtnQUY_TRINH_SAN_XUAT.Text = sb.ToString() + " | ";
                    }

                    if (objTaiLieu.LoaiTaiLieuId == (int)EnLoaiTaiLieuList.BAN_CONG_BO)
                    {
                        StringBuilder sbBAN_CONG_BO = new StringBuilder();
                        sbBAN_CONG_BO.Append("<a href='" + FilePath + "'>" + EntityHelper.GetEnumTextValue(EnLoaiTaiLieuList.BAN_CONG_BO) + "</a>");
                        lbtnBAN_CONG_BO.Text = sbBAN_CONG_BO.ToString() + " | ";
                    }
                    if (objTaiLieu.LoaiTaiLieuId == (int)EnLoaiTaiLieuList.GIAY_TO_TU_CACH_PHAP_NHAN)
                    {
                        StringBuilder sbGIAY_TO_TU_CACH_PHAP_NHAN = new StringBuilder();
                        sbGIAY_TO_TU_CACH_PHAP_NHAN.Append("<a href='" + FilePath + "'>" + EntityHelper.GetEnumTextValue(EnLoaiTaiLieuList.GIAY_TO_TU_CACH_PHAP_NHAN) + "</a>");
                        lbtnGIAY_TO_TU_CACH_PHAP_NHAN.Text = sbGIAY_TO_TU_CACH_PHAP_NHAN.ToString() + " | ";
                    }
                    if (objTaiLieu.LoaiTaiLieuId == (int)EnLoaiTaiLieuList.KET_QUA_DO_KIEM)
                    {
                        StringBuilder sbKET_QUA_DO_KIEM = new StringBuilder();
                        sbKET_QUA_DO_KIEM.Append("<a href='" + FilePath + "'>" + EntityHelper.GetEnumTextValue(EnLoaiTaiLieuList.KET_QUA_DO_KIEM) + "</a>");
                        lbtnKET_QUA_DO_KIEM.Text = sbKET_QUA_DO_KIEM.ToString() + " | ";
                    }

                    if (objTaiLieu.LoaiTaiLieuId == (int)EnLoaiTaiLieuList.TAI_LIEU_KY_THUAT)
                    {
                        StringBuilder sbTAI_LIEU_KY_THUAT = new StringBuilder();
                        sbTAI_LIEU_KY_THUAT.Append("<a href='" + FilePath + "'>" + EntityHelper.GetEnumTextValue(EnLoaiTaiLieuList.TAI_LIEU_KY_THUAT) + "</a>");
                        lbtnTAI_LIEU_KY_THUAT.Text = sbTAI_LIEU_KY_THUAT.ToString() + " | ";
                    }
                    if (objTaiLieu.LoaiTaiLieuId == (int)EnLoaiTaiLieuList.BAN_TU_DANH_GIA)
                    {
                        StringBuilder sbBAN_TU_DANH_GIA = new StringBuilder();
                        sbBAN_TU_DANH_GIA.Append("<a href='" + FilePath + "'>" + EntityHelper.GetEnumTextValue(EnLoaiTaiLieuList.BAN_TU_DANH_GIA) + "</a>");
                        lbtnBAN_TU_DANH_GIA.Text = sbBAN_TU_DANH_GIA.ToString() + " | ";
                    }

                    if (objTaiLieu.LoaiTaiLieuId == (int)EnLoaiTaiLieuList.CONG_VAN)
                    {
                        lnkCongVan.Visible = true;
                        lnkCongVan.NavigateUrl = @"~/" + objTaiLieu.TenFile.Trim();
                    }
                }
                // Hiển thị công văn
                if (objSanPham.KetLuanId != (int)EnKetLuanList.CAP_BAN_TIEP_NHAN_CB)
                {
                    trCongVan.Visible = true;
                }
                else
                    trCongVan.Visible = false;

            }
        }

    }
    /// <summary>
    ///Hien thi thông tin xử lý
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// DucLv                 10/5/2009              
    /// </Modified>
    private void NoiDungXuLy(string strSanPhamID)
    {

        if ((strSanPhamID != "") && (strSanPhamID != null))
        {
            objSanPham = new SanPham();
            objSanPham = ProviderFactory.SanPhamProvider.GetById(strSanPhamID);
            ListItem GD_PHE_DUYET = new ListItem();
            GD_PHE_DUYET.Text = EntityHelper.GetEnumTextValue(EnTrangThaiSanPhamList.GD_PHE_DUYET);
            GD_PHE_DUYET.Value = ((int)EnTrangThaiSanPhamList.GD_PHE_DUYET).ToString();

            ListItem GD_KHONG_DUYET = new ListItem();
            GD_KHONG_DUYET.Text = EntityHelper.GetEnumTextValue(EnTrangThaiSanPhamList.GD_KHONG_DUYET);
            GD_KHONG_DUYET.Value = ((int)EnTrangThaiSanPhamList.GD_KHONG_DUYET).ToString();
            grdlPheDuyet.Items.Add(GD_PHE_DUYET);
            grdlPheDuyet.Items.Add(GD_KHONG_DUYET);
            if (!IsPostBack)
            {
                TList<QuaTrinhXuLy> lstQuaTrinhXuLy = ProviderFactory.QuaTrinhXuLyProvider.GetBySanPhamId(strSanPhamID);
                SanPham objsp = ProviderFactory.SanPhamProvider.GetById(strSanPhamID);
                HoSo objHoSo = ProviderFactory.HoSoProvider.GetById(objsp.HoSoId);
                foreach (QuaTrinhXuLy obj in lstQuaTrinhXuLy)
                {
                    SysUser objSysUser = ProviderFactory.SysUserProvider.GetById(obj.NguoiXuLyId);
                    if (obj.LoaiXuLyId == (int)EnLoaiXuLyList.TIEP_NHAN_HO_SO)
                    {
                        TList<PhanCong> lstPhanCongXuLy = ProviderFactory.PhanCongProvider.GetByHoSoId(objHoSo.Id);

                        if (objHoSo.NguoiTiepNhanId == objSysUser.Id || objHoSo.NguoiTiepNhanId == lstPhanCongXuLy[0].NguoiXuLy)
                            txtNoiDungXuLyCV1.Text = objSysUser.FullName + " : " + obj.NgayXuLy + " : " + obj.NoiDungXuLy;
                        else
                            txtGhiChuCV1.Text += objSysUser.FullName + " : " + obj.NgayXuLy + " : " + obj.NoiDungXuLy + "\r\n";

                    }
                }
                SetReadOnly(txtNoiDungXuLyCV1);
                SetReadOnly(txtGhiChuCV1);
                SetReadOnly(txtNoiDungXuLyCV2);
                ///// lay noi dung chuyen vien xu ly
                foreach (QuaTrinhXuLy obj in lstQuaTrinhXuLy)
                {
                    SysUser objSysUser = ProviderFactory.SysUserProvider.GetById(obj.NguoiXuLyId);
                    if (obj.LoaiXuLyId == (int)EnLoaiXuLyList.DANH_GIA)
                        txtGhiChuCV1.Text += objSysUser.FullName + " : " + obj.NgayXuLy + " : " + obj.NoiDungXuLy + "\r\n";
                    SetReadOnly(txtGhiChuCV1);
                }
                ///// lay noi dung chuyen vien xu ly 2

                foreach (QuaTrinhXuLy obj in lstQuaTrinhXuLy)
                {
                    SysUser objSysUser = ProviderFactory.SysUserProvider.GetById(obj.NguoiXuLyId);
                    if (obj.LoaiXuLyId == (int)EnLoaiXuLyList.THAM_DINH)
                        txtNoiDungXuLyCV2.Text += objSysUser.FullName + " : " + obj.NgayXuLy + " : " + obj.NoiDungXuLy + "\r\n";

                }
                ///// lay noi dung da phe duyet

                foreach (QuaTrinhXuLy obj in lstQuaTrinhXuLy)
                {
                    SysUser objSysUser = ProviderFactory.SysUserProvider.GetById(obj.NguoiXuLyId);
                    if (obj.LoaiXuLyId == (int)EnLoaiXuLyList.PHE_DUYET)
                    {
                        txtYKienPheDuyet.Text = obj.NoiDungXuLy;
                        //txtYKienPheDuyet.Enabled = false;
                        foreach (ListItem item in grdlPheDuyet.Items)
                        {
                            if (item.Value == objSanPham.TrangThaiId.ToString())
                            {
                                grdlPheDuyet.SelectedValue = objSanPham.TrangThaiId.ToString();
                            }
                        }
                    }

                }
                // ket luan
                lblKetLuan.Text = EntityHelper.GetEnumTextValue((EnKetLuanList)objSanPham.KetLuanId);
                //Ý kiến chỉ đạo
                TList<PhanCong> lstPhanCong = ProviderFactory.PhanCongProvider.GetByHoSoId(objSanPham.HoSoId);
                string strYKienChiDao = string.Empty;
                foreach (PhanCong obj in lstPhanCong)
                {
                    SysUser objSysUser = ProviderFactory.SysUserProvider.GetById(obj.NguoiPhanCong);
                    strYKienChiDao += objSysUser.FullName + " : " + obj.NgayPhanCong + ": " + obj.YkienCuaLanhDao + "\r\n";
                }
                if (strYKienChiDao.Length >= 2)
                    strYKienChiDao = strYKienChiDao.Remove(strYKienChiDao.Length - 2, 2);
                txtYKienChiDao.Text = strYKienChiDao;
                SetReadOnly(txtYKienChiDao);

            }
        }

    }
    /// <summary>
    ///Lưu thông tin phê duyệt
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// DucLv                 10/5/2009   
    /// TuanVM                  19/05/09                Sửa cập nhật trạng thái thông báo lệ phí
    /// TuanVM                  28/05/09                Cập nhật số GCB
    /// </Modified>
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        if (Request["SanPhamID"] != null)
        {
            TransactionManager trans = ProviderFactory.Transaction;
            // cập nhật thông tin hồ sơ sản phẩm
            try
            {
                btnCapNhat.Visible = false;
                objSanPham = ProviderFactory.SanPhamProvider.GetById(trans, Request["SanPhamID"].ToString());
                string ThongBao = "";

                // Nếu kết quả xử lý là cấp BTN
                if (objSanPham.KetLuanId == (int)EnKetLuanList.CAP_BAN_TIEP_NHAN_CB)
                {
                    // Lấy thông tin liên quan đến thông báo lệ phí
                    objThongBaoLPSP = ProviderFactory.ThongBaoLePhiSanPhamProvider.GetBySanPhamId(trans, Request["SanPhamID"].ToString())[0];

                    // Lấy lệ phí của sản phẩm
                    try
                    {
                        intLePhi = Int32.Parse(objThongBaoLPSP.LePhi.ToString());
                    }
                    catch
                    {
                        ThongBao = "lệ phí của sản phẩm " + objThongBaoLPSP.LePhi.ToString();
                        return;
                    }
                    
                    string strThongBaoLePhiID = objThongBaoLPSP.ThongBaoLePhiId;
                    objThongBaoLP = ProviderFactory.ThongBaoLePhiProvider.GetById(trans, strThongBaoLePhiID);

                    // Nếu giám đốc phê duyệt
                    if (grdlPheDuyet.SelectedItem.Value == ((int)EnTrangThaiSanPhamList.GD_PHE_DUYET).ToString())
                    {
                        ThongBao = Resource.msgPheDuyet;

                        // Cập nhật số giấy chứng nhận, người phê duyệt, ngày phê duyệt
                        string day = "";
                        if (DateTime.Now.Day < 10)
                            day = "0" + DateTime.Now.Day.ToString();
                        else
                            day = DateTime.Now.Day.ToString();

                        string month = "";
                        if (DateTime.Now.Month < 10)
                            month = "0" + DateTime.Now.Month.ToString();
                        else
                            month = DateTime.Now.Month.ToString();
                        string year = DateTime.Now.Year.ToString().Substring(2, 2);
                        string ngayThang = day + month + year;

                        if ((bool)ProviderFactory.HoSoProvider.GetById(trans, objSanPham.HoSoId).HoSoMoi)
                            objSanPham.SoBanTiepNhanCb = ProviderFactory.SanPhamProvider.GetSoBanTiepNhanCB(trans);
                        objSanPham.NguoiKyDuyetId = mUserInfo.UserID;
                        objSanPham.NgayKyDuyet = DateTime.Now;
                        objSanPham.PhaiThamDinhLai = false;
                    }
                    else // Nếu giám đốc không phê duyệt
                    {
                        ThongBao = Resource.msgKhongPheDuyet;
                        intTongPhi = objThongBaoLP.TongPhi;
                        intTongPhiMoi = intTongPhi - intLePhi;
                        objThongBaoLP.TongPhi = intTongPhiMoi;
                        // xoa sản phẩm trong bảng thông báo phí
                        ProviderFactory.ThongBaoLePhiSanPhamProvider.DeleteLePhiSanPhamBySanPhamID_Extend(Request["SanPhamID"].ToString(), trans);


                        // Nếu tất cả các sản phẩm không được phê duyệt  thì xoá thống báo lệ phí
                        if (ProviderFactory.ThongBaoLePhiSanPhamProvider.GetByThongBaoLePhiId(trans, objThongBaoLP.Id).Count == 0)
                        {
                            ProviderFactory.ThongBaoLePhiChiTietProvider.DeleteByThongBaoLePhiId(strThongBaoLePhiID, trans);
                            ProviderFactory.ThongBaoLePhiProvider.Delete(trans, strThongBaoLePhiID);
                        }
                    }

                    try
                    {
                        objSanPham.TrangThaiId = Int32.Parse(grdlPheDuyet.SelectedValue);
                    }
                    catch
                    {
                        ThongBao = "grdlPheDuyet.SelectedValue: " + grdlPheDuyet.SelectedValue;
                        return;
                    }

                    // Cập nhật trạng thái sản phẩm
                    ProviderFactory.SanPhamProvider.Save(trans, objSanPham);



                    // Cập nhật trạng thái TBLP nếu tất cả các sản phẩm đã phê duyệt xong
                    TList<ThongBaoLePhiSanPham> lstObjLPSP = ProviderFactory.ThongBaoLePhiSanPhamProvider.GetByThongBaoLePhiId(trans, strThongBaoLePhiID);
                    int number = 0;

                    foreach (ThongBaoLePhiSanPham obj in lstObjLPSP) // Duyệt danh sách các sản phẩm trong TBLP
                    {
                        SanPham objSP = ProviderFactory.SanPhamProvider.GetById(trans, obj.SanPhamId);
                        // Nếu sản phẩm đã được phê duyệt
                        if (objSP.TrangThaiId == (int)EnTrangThaiSanPhamList.GD_KHONG_DUYET || objSP.TrangThaiId == (int)EnTrangThaiSanPhamList.GD_PHE_DUYET || objSP.TrangThaiId == (int)EnTrangThaiSanPhamList.KET_THUC)
                            number++;
                    }
                    if (number == lstObjLPSP.Count)  // Nếu tất cả các sản phẩm đã phê duyệt xong thì chuyển TBLP sang chờ thu phí là lấy số TBLP chính thức
                    {
                        objThongBaoLP.TrangThaiId = (int)EnTrangThaiThongBaoPhiList.CHO_THU_PHI;
                        objThongBaoLP.SoGiayThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetSoGiayTBLP(objThongBaoLP.LoaiPhiId, trans);
                    }
                    if (objSanPham.NgayKyDuyet != null)
                        objThongBaoLP.NgayPheDuyet = objSanPham.NgayKyDuyet;
                    if (objSanPham.NguoiKyDuyetId != null)
                        objThongBaoLP.NguoiPheDuyetId = mUserInfo.UserID;
                    ProviderFactory.ThongBaoLePhiProvider.Save(trans, objThongBaoLP);
                }
                else  // Nếu không cấp bản tiếp nhận
                {
                    if (grdlPheDuyet.SelectedItem.Value == ((int)EnTrangThaiSanPhamList.GD_PHE_DUYET).ToString())
                    {
                        objSanPham.TrangThaiId = (int)EnTrangThaiSanPhamList.GD_PHE_DUYET;

                        objSanPham.NguoiKyDuyetId = mUserInfo.UserID;
                        objSanPham.NgayKyDuyet = DateTime.Now;
                        objSanPham.PhaiThamDinhLai = false;
                    }
                    else
                    {
                        ThongBao = Resource.msgKhongPheDuyet;
                        objSanPham.TrangThaiId = (int)EnTrangThaiSanPhamList.GD_KHONG_DUYET;
                    }

                    // Cập nhật trạng thái sản phẩm
                    ProviderFactory.SanPhamProvider.Save(trans, objSanPham);
                }

                // Lưu nội dung phê duyệt
                objQuaTrinhXuLy = new QuaTrinhXuLy();
                objQuaTrinhXuLy.NoiDungXuLy = txtYKienPheDuyet.Text.ToString();
                objQuaTrinhXuLy.LoaiXuLyId = (int)EnLoaiXuLyList.PHE_DUYET; // lay ma xu ly là phê duyệt
                objQuaTrinhXuLy.SanPhamId = objSanPham.Id;
                objQuaTrinhXuLy.NgayXuLy = DateTime.Now;
                objQuaTrinhXuLy.NguoiXuLyId = mUserInfo.UserID;
                ProviderFactory.QuaTrinhXuLyProvider.Save(trans, objQuaTrinhXuLy);

                trans.Commit();
                // ghi log su kien
                ProviderFactory.SysLogProvier.Write(((PageBase)this.Page).mUserInfo, SysEventList.CB_PHE_DUYET, Resources.Resource.msgPheDuyet);

                // Thông báo cập nhật thành công và quay về trang quản lý
                ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
              "<script>alert('Cập nhật thành công!'); window.location.href='CB_HoSoSanPham_QuanLy.aspx?UserControl=CB_HoSoDen&direct=CB_HoSoMoi&HoSoID=" + objSanPham.HoSoId + "&TrangThaiId=4';</script>");
                //}
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
        }

    }

    protected void btnBoQua_Click(object sender, EventArgs e)
    {
        objSanPham = new SanPham();
        objSanPham = ProviderFactory.SanPhamProvider.GetById(Request["SanPhamID"].ToString());
        string UserControl = Request["UserControl"].ToString();

        string HoSoID = Request["HoSoID"].ToString();
        string TrangThaiID = Request["TrangThaiID"].ToString();
        //Response.Redirect("CB_HoSoSanPham_QuanLy.aspx?direct="+UserControl+"&HoSoID=" + objSanPham.HoSoId + "&TrangThaiId=4");
        Response.Redirect("CB_HoSoSanPham_QuanLy.aspx?UserControl=" + UserControl + "&HoSoID=" + HoSoID + "&TrangThaiId=" + TrangThaiID + "");
    }

    protected void btnInBanTiepNhan_Click(object sender, EventArgs e)
    {
        string strHoSoId = string.Empty;
        if (Request["HoSoID"] != null)
            strHoSoId = Request["HoSoID"].ToString();

        string strSanPhamId = string.Empty;
        if (Request["SanPhamID"] != null)
            strSanPhamId = Request["SanPhamID"].ToString();

        ClientScript.RegisterClientScriptBlock(this.GetType(), "CB_XuLyHoSo_DanhGia_BanTiepNhan",
                "<script>popCenter('../ReportForm/DieuKienInBaoCao.aspx?HoSoID=" + strHoSoId + "&SanPhamId=" + strSanPhamId + "&LoaiBaoCao=BanTiepNhan" + "','CBPhieuDanhGia',450,300);</script>");
    }

    protected void btnInPhieuDanhGia_Click(object sender, EventArgs e)
    {
        string strHoSoId = string.Empty;
        if (Request["HoSoID"] != null)
            strHoSoId = Request["HoSoID"].ToString();

        string strSanPhamId = string.Empty;
        if (Request["SanPhamID"] != null)
            strSanPhamId = Request["SanPhamID"].ToString();

        ClientScript.RegisterClientScriptBlock(this.GetType(), "CB_XuLyHoSo_DanhGia_BanTiepNhan",
                "<script>popCenter('../ReportForm/DieuKienInBaoCao.aspx?HoSoID=" + strHoSoId + "&SanPhamId=" + strSanPhamId + "&LoaiBaoCao=CBPhieuDanhGia" + "','CBPhieuDanhGia',450,300);</script>");
    }
}
