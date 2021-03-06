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
using System.Collections.Generic;

public partial class WebUI_CB_ThamDinhHoSo : PageBase
{
    //Hồ sơ
    HoSo objHoSo = null;
    public string strSanPhamID = "";
    SanPham objSanPham = null;
    QuaTrinhXuLy objQuaTrinhXuLy = null;

    DmSanPham objDmSanPham = null;
    ThongBaoLePhiSanPham objThongBaoLPSP = null;
    ThongBaoLePhi objThongBaoLP = null;
    //xác định hồ sơ đã gửi hay chưa gửi
    private string strDanhSachHoSo = string.Empty;
    //xác định sản phẩm đã gửi hay chưa gửi
    private string strDanhSachSanPham = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["UserControlHS"] != null)
            strDanhSachHoSo = Request["UserControlHS"];
        if (Request["UserControl"] != null)
            strDanhSachSanPham = Request["UserControl"];

        if (!IsPostBack)
        {
            if (Request["SanPhamID"] != null)
            {
                ManageBredCum(strDanhSachHoSo, Request["HoSoID"].ToString(), strDanhSachSanPham, Request["SanPhamID"].ToString());

                strSanPhamID = Request["SanPhamID"].ToString();
                //Tryền tham số cho báo cáo
                Session["SAN_PHAM_ID"] = Request["SanPhamID"] != null ? Request["SanPhamID"].ToString() : "";
                HienThiThongTin(strSanPhamID);
                NoiDungXuLy(strSanPhamID);


            }

            //rdblThamDinh.SelectedValue = ((int)EnTrangThaiSanPhamList.THAM_DINH_KHONG_DONG_Y).ToString();
            if (Request["action"] != null)
            {
                if (Request["action"].ToString() == "view")
                {
                    btnCapNhat.Visible = false;

                    // Disable các control ...
                    txtYKienThamDinh.Enabled = false;
                    rdblThamDinh.Enabled = false;
                }
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
            ListItem TD_DONGY = new ListItem();
            TD_DONGY.Text = "Nhất trí";
            TD_DONGY.Value = ((int)EnTrangThaiSanPhamList.THAM_DINH_DONG_Y).ToString();

            ListItem TD_KHONGDONGY = new ListItem();
            TD_KHONGDONGY.Text = "Không nhất trí";
            TD_KHONGDONGY.Value = ((int)EnTrangThaiSanPhamList.THAM_DINH_KHONG_DONG_Y).ToString();
            rdblThamDinh.Items.Add(TD_DONGY);
            rdblThamDinh.Items.Add(TD_KHONGDONGY);
            objSanPham = new SanPham();
            objSanPham = ProviderFactory.SanPhamProvider.GetById(strSanPhamID);

            if (!IsPostBack)
            {
                TList<QuaTrinhXuLy> lstQuaTrinhXuLy = ProviderFactory.QuaTrinhXuLyProvider.GetBySanPhamId(strSanPhamID);
                foreach (QuaTrinhXuLy obj in lstQuaTrinhXuLy)
                {
                    SysUser objSysUser = ProviderFactory.SysUserProvider.GetById(obj.NguoiXuLyId);
                    if (obj.LoaiXuLyId == (int)EnLoaiXuLyList.TIEP_NHAN_HO_SO)
                    {
                        TList< PhanCong> lstPhanCongXuLy = ProviderFactory.PhanCongProvider.GetByHoSoId(objHoSo.Id);

                        if (objHoSo.NguoiTiepNhanId == objSysUser.Id || objHoSo.NguoiTiepNhanId == lstPhanCongXuLy[0].NguoiXuLy)
                            txtChuyenVienTiepNhan.Text = objSysUser.FullName + " : " + obj.NgayXuLy + " : " + obj.NoiDungXuLy;
                        else
                            txtChuyenVienXuLy.Text += objSysUser.FullName + " : " + obj.NgayXuLy + " : " + obj.NoiDungXuLy + "\r\n";
                    }
                    SetReadOnly(txtChuyenVienTiepNhan);
                }
                //LongHH
                SetReadOnly(txtSoNgayDoKiem);
                SetReadOnly(txtCoQuanDoKiem);
                SetReadOnly(txtTenTiengAnhCQDK);
                SetReadOnly(txtDiaChiCQDK);
                SetReadOnly(txtSoDienThoaiCQDK);
                SetReadOnly(txtKetQuaDoKiem);
                SetReadOnly(txtSoDoKiem);
                //LongHH
                ///// lay noi dung chuyen vien xu ly
                foreach (QuaTrinhXuLy obj in lstQuaTrinhXuLy)
                {
                    SysUser objSysUser = ProviderFactory.SysUserProvider.GetById(obj.NguoiXuLyId);
                    if (obj.LoaiXuLyId == (int)EnLoaiXuLyList.DANH_GIA)
                        txtChuyenVienXuLy.Text += objSysUser.FullName + " : " + obj.NgayXuLy + " : " + obj.NoiDungXuLy + "\r\n";
                    SetReadOnly(txtChuyenVienXuLy);
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
                txtYKienLanhDao.Text = strYKienChiDao;
                SetReadOnly(txtYKienLanhDao);

                foreach (QuaTrinhXuLy obj in lstQuaTrinhXuLy)
                {
                    SysUser objSysUser = ProviderFactory.SysUserProvider.GetById(obj.NguoiXuLyId);
                    if (obj.LoaiXuLyId == (int)EnLoaiXuLyList.THAM_DINH)
                    {
                        // hien thi thong tin tham dinh

                        foreach (ListItem item in rdblThamDinh.Items)
                        {
                            if (item.Value == objSanPham.TrangThaiId.ToString())
                            {
                                rdblThamDinh.SelectedValue = objSanPham.TrangThaiId.ToString();
                            }
                        }
                        txtYKienThamDinh.Text = obj.NoiDungXuLy;
                    }
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
            objHoSo = ProviderFactory.HoSoProvider.GetById(objSanPham.HoSoId);

            if (!IsPostBack)
            {

                objDmSanPham = new DmSanPham();
                objDmSanPham = ProviderFactory.DmSanPhamProvider.GetById(objSanPham.SanPhamId);
                lblTenSanPham.Text = objDmSanPham.TenTiengViet;                
                lblKyHieu.Text = objSanPham.KyHieu;
                lblSoGCN_TuDanhGia.Text = objSanPham.SoGcn.Length > 0 ? objSanPham.SoGcn : objSanPham.SoBanTuDanhGia;
                lblNgayDanhGia.Text = objSanPham.NgayDanhGia != null ? objSanPham.NgayDanhGia.Value.ToShortDateString() : string.Empty;
                lblSoBanCongBo.Text = objSanPham.SoBanCongBo;
                lblNgayCongBo.Text = objSanPham.NgayCongBo != null ? objSanPham.NgayCongBo.Value.ToShortDateString() : string.Empty;
                lblNgaynhan.Text = ((DateTime)objHoSo.NgayTiepNhan).ToShortDateString();
                lblhangsx.Text = ProviderFactory.DmHangSanXuatProvider.GetById(objSanPham.HangSanXuatId).TenHangSanXuat;
                lblTrangThai.Text = EntityHelper.GetEnumTextValue((EnTrangThaiSanPhamList)objSanPham.TrangThaiId);
                TList<SanPhamTieuChuanApDung> lstSanPhamTieuChuan;
                lstSanPhamTieuChuan = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(objSanPham.Id);
                //LongHH
                DmCoQuanDoKiem cqdk = ProviderFactory.DmCoQuanDoKiemProvider.GetById(objSanPham.CoQuanDoKiemId);
                if (cqdk != null)
                {
                    txtCoQuanDoKiem.Text = cqdk.TenCoQuanDoKiem;
                    txtTenTiengAnhCQDK.Text = cqdk.TenTiengAnh;
                    txtDiaChiCQDK.Text = cqdk.DiaChi;
                    txtSoDienThoaiCQDK.Text = cqdk.DienThoai;
                    //LongHH
                    List<String> files = QLCL_Patch.GetFileAttach_CoQuanDoKiem_Nop_HoSo(cqdk.Id);
                    if (files.Count > 0)
                    {
                        StringBuilder sbGIAY_PHEP_KINH_DOANH = new StringBuilder();
                        sbGIAY_PHEP_KINH_DOANH.Append("<a href='" + files[0] + "'>File đính kèm</a>");
                        lbtnFileDinhKem.Text = sbGIAY_PHEP_KINH_DOANH.ToString();
                    }
                    //LongHH
                }
                else
                {
                    txtTenTiengAnhCQDK.Text = string.Empty;
                    txtDiaChiCQDK.Text = string.Empty;
                    txtSoDienThoaiCQDK.Text = string.Empty;
                    lbtnFileDinhKem.Text = string.Empty;
                }
                if (objSanPham.NgayDoKiem != null)
                    txtSoNgayDoKiem.Text = Convert.ToDateTime(objSanPham.NgayDoKiem).ToShortDateString();
                lblTenSanPhamTA.Text = objDmSanPham.TenTiengAnh;
                txtKetQuaDoKiem.Text = objSanPham.NoiDungDoKiem;
                txtSoDoKiem.Text = objSanPham.SoDoKiem;
                //LongHH
                string DsTieuChuan = string.Empty;
                foreach (SanPhamTieuChuanApDung objSanPhamTieuChuan in lstSanPhamTieuChuan)
                {
                    DsTieuChuan += ProviderFactory.DmTieuChuanProvider.GetById(objSanPhamTieuChuan.TieuChuanApDungId).MaTieuChuan + ", ";
                }

                if (DsTieuChuan.Length > 2)
                    lblTCAdung.Text = DsTieuChuan.Substring(0, DsTieuChuan.Length - 2);

                if (objSanPham.KetLuanId == (int)EnKetLuanList.CAP_BAN_TIEP_NHAN_CB)
                {

                    string giatri = ConfigurationManager.AppSettings["PhiCongBoHQ"].ToString();
                    //lblLePhi.Text = string.Format("{0:0,0}", Convert.ToInt32(giatri));
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
    /// cập nhật quá trình xử lý
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// DucLv               27/5/2009              
    /// </Modified>

    protected void btnCapNhat_Click(object sender, EventArgs e)
    {

        if (Request["SanPhamID"] != null)
        {
            TransactionManager trans = ProviderFactory.Transaction;
            // cập nhật thông tin hồ sơ sản phẩm
            try
            {
                objSanPham = ProviderFactory.SanPhamProvider.GetById(trans, Request["SanPhamID"].ToString());
                objQuaTrinhXuLy = new QuaTrinhXuLy();

                string ThongBao = "";

                if (rdblThamDinh.SelectedItem.Value == ((int)EnTrangThaiSanPhamList.THAM_DINH_DONG_Y).ToString())
                {
                    ThongBao = Resource.msgThamDinhDongY;

                }
                else
                {
                    ThongBao = Resource.msgThamDinhKhongDongY;

                }

                objSanPham.TrangThaiId = Int32.Parse(rdblThamDinh.SelectedValue);


                // Cập nhật trạng thái sản phẩm
                ProviderFactory.SanPhamProvider.Save(trans, objSanPham);

                // Lưu nội dung thẩm định
                objQuaTrinhXuLy.NoiDungXuLy = txtYKienThamDinh.Text.ToString();
                objQuaTrinhXuLy.LoaiXuLyId = (int)EnLoaiXuLyList.THAM_DINH; // lay ma xu ly là thẩm định
                objQuaTrinhXuLy.SanPhamId = objSanPham.Id;
                objQuaTrinhXuLy.NgayXuLy = DateTime.Now;
                objQuaTrinhXuLy.NguoiXuLyId = mUserInfo.UserID;
                ProviderFactory.QuaTrinhXuLyProvider.Save(trans, objQuaTrinhXuLy);



                trans.Commit();
                ////// ghi log su kien
                ProviderFactory.SysLogProvier.Write(((PageBase)this.Page).mUserInfo, SysEventList.CB_THAM_DINH, ThongBao);
                ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                  "<script>alert('Cập nhật thành công!'); window.location.href='CB_HoSoSanPham_QuanLy.aspx?direct=CB_HoSoMoi&HoSoID=" + objSanPham.HoSoId + "&TrangThaiId=4';</script>");

            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
        }

    }
    /// <summary>
    /// quay lại trang hồ sơ sản phẩm
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    protected void btnBoQua_Click(object sender, EventArgs e)
    {
        if (Request["UserControl"] != null)
            strDanhSachSanPham = Request["UserControl"];

        Response.Redirect("CB_HoSoSanPham_QuanLy.aspx?UserControl=" + strDanhSachSanPham + "&HoSoId=" + Request["HoSoId"].ToString());
    }
    /// <summary>
    /// Thiết lập link cho mapsite
    /// </summary>
    /// <param name="strDanhSachHoSo">Loại Usercontrol của HoSo_QuanLy</param>
    /// <param name="HoSoId">Id Hồ sơ</param>
    /// <param name="strDanhSachSanPham">Loại Usercontrol của HoSoSanPham_QuanLy</param>
    /// <param name="SanPhamId">Id sản phẩm</param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                12/5/2009              
    /// </Modified>
    private void ManageBredCum(string strDanhSachHoSo, string HoSoId, string strDanhSachSanPham, string SanPhamId)
    {
        if (strDanhSachHoSo == "CB_HoSoDi")
        {
            hlDanhSachHoSo.Text = string.Format("Danh sách Hồ Sơ đã gửi").ToUpper();
        }
        else
        {
            hlDanhSachHoSo.Text = string.Format("Danh sách Hồ Sơ đến").ToUpper();
        }
        if (strDanhSachSanPham == "CB_HoSoDi")
        {
            hlDanhSachSanPham.Text = string.Format("Danh sách Sản phẩm đã gửi").ToUpper();
        }
        else
        {
            hlDanhSachSanPham.Text = string.Format("Danh sách Sản phẩm đến").ToUpper();
        }
        hlDanhSachHoSo.NavigateUrl = "CN_HoSo_QuanLy.aspx?UserControl=" + strDanhSachHoSo;
        hlDanhSachSanPham.NavigateUrl = "CB_HoSoSanPham_QuanLy.aspx?UserControl=" + strDanhSachSanPham + "&HoSoId=" + HoSoId;
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
