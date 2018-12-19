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

public partial class WebUI_CB_SanPhamChiTiet : PageBase
{
    public string strSanPhamID = "";
    SanPham objSanPham = null;
    QuaTrinhXuLy objQuaTrinhXuLy = null;
    DmLePhi objDmLePhi = null;
    DmSanPham objDmSanPham = null;
    ThongBaoLePhiSanPham objThongBaoLPSP = null;
    ThongBaoLePhi objThongBaoLP = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        // Đặt style hiển thị giờ theo kiểu việt nam
        UICulture = "vi-VN";
        Culture = "vi-VN";
        if (!IsPostBack)
        {
            if (Request["SanPhamID"] != null)
            {
                strSanPhamID = Request["SanPhamID"].ToString();
                HienThiThongTin(strSanPhamID);
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


            if (!IsPostBack)
            {

                objDmSanPham = new DmSanPham();
                objDmSanPham = ProviderFactory.DmSanPhamProvider.GetById(objSanPham.SanPhamId);
                lblTenSanPham.Text = objDmSanPham.TenTiengViet;
                lblKyHieu.Text = objSanPham.KyHieu;
                lblNgaynhan.Text = objSanPham.NgayCapNhatSauCung.ToString();
                lblhangsx.Text = ProviderFactory.DmHangSanXuatProvider.GetById(objSanPham.HangSanXuatId).TenHangSanXuat;
                lblTrangThai.Text = EntityHelper.GetEnumTextValue((EnTrangThaiSanPhamList)objSanPham.TrangThaiId);
                TList<SanPhamTieuChuanApDung> lstSanPhamTieuChuan;
                lstSanPhamTieuChuan = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(objSanPham.Id);

                foreach (SanPhamTieuChuanApDung objSanPhamTieuChuan in lstSanPhamTieuChuan)
                {
                    lblTCAdung.Text += ProviderFactory.DmTieuChuanProvider.GetById(objSanPhamTieuChuan.TieuChuanApDungId).MaTieuChuan + ",";
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
                        StringBuilder sbCONG_VAN = new StringBuilder();
                        sbCONG_VAN.Append("<a href='" + FilePath + "'>" + EntityHelper.GetEnumTextValue(EnLoaiTaiLieuList.CONG_VAN) + "</a>");
                        lbtnCONG_VAN.Text = sbCONG_VAN.ToString() + " | ";
                    }

                }

            }
        }

    }
}
