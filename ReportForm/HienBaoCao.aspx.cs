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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Cuc_QLCL.Entities;
using Cuc_QLCL.AdapterData;
using Resources;
using CucQLCL.Common;

public partial class WebUI_HienBaoCao : PageBase
{
    private ReportDocument rd;
    int PhiLayMauSanPham = 1000000;
    int PhiDanhGiaQuyTrinhQLCL = 1000000;
    string strTenBaoCao;
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        if (rd != null)
        {
            rd.Close();
            rd.Dispose();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (mUserInfo.IsThanhTra)
            trPrint.Visible = false;

        UserInfo _mUserInfo = Session["User"] as UserInfo;
        this.Unload += new EventHandler(Page_UnLoad);

        #region Lấy tham số chung
        string format = "";
        string LoaiBaoCao = "";
        string strduongdandenteploaiBC = string.Empty;
        strTenBaoCao = "";
        rd = new ReportDocument();

        if (Request["LoaiBaoCao"] != null)
            LoaiBaoCao = Request["LoaiBaoCao"].ToString();
        if (Request["format"] != null)
        {
            format = Request["format"].ToString();
        }
        else
            format = "CR";
        if (Request["TenBaoCao"] != null)
        {
            strTenBaoCao = Request["TenBaoCao"].ToString();
        }
        //Trường hợp tra cứu
        if (Request["LoaiBaoCao"] != null)
        {
            LoaiBaoCao = Request["LoaiBaoCao"].ToString();
        }
        #endregion
        #region Các mẫu in
        switch (LoaiBaoCao)
        {
            #region In giấy chứng nhận-Duchh
            //Tham số:Session["SAN_PHAM_ID"]
            case "GiayChungNhan":
                strduongdandenteploaiBC = Server.MapPath(@"~\Report\GiayChungNhan.rpt");
                rd = new ReportDocument();
                rd.Load(strduongdandenteploaiBC);
                if (rd != null)
                {
                    string TEN_TRUNG_TAM_TIENG_VIET = string.Empty;
                    string TEN_TRUNG_TAM_TIENG_ANH = string.Empty;

                    string SAN_PHAM_ID = "";//Session["SAN_PHAM_ID"] != null ? Session["SAN_PHAM_ID"].ToString() : "";
                    //Trường hợp tra cứu
                    SAN_PHAM_ID = Request["SanphamID"] != null ? Request["SanphamID"].ToString() : SAN_PHAM_ID;
                    string TEN_SAN_PHAM = "";
                    string TEN_SAN_PHAM_EN = "";
                    string KY_HIEU = "";
                    string KY_HIEU_EN = "";
                    string SAN_XUAT = "";
                    string SAN_XUAT_EN = "";
                    string PHU_HOP = "";
                    string PHU_HOP_VN = "";
                    string PHU_HOP_EN = "";
                    string SO = "";
                    string SO_EN = "";
                    string DON_VI = "";
                    string DON_VI_EN = "";
                    string NOI_CAP = "";
                    string NOI_CAP_1 = "";
                    string NGAY_CAP = "";
                    string NGAY_CAP_1 = "";
                    string GIA_TRI_DEN = "";
                    string GIA_TRI_DEN_1 = "";
                    string CHUC_VU = "";
                    string GIAM_DOC = "";
                    string LOAI_HINH_CHUNG_NHAN = string.Empty;
                    string CERTIFICATE_TYPE = string.Empty;
                    string LoaiPhuHop = string.Empty;
                    string SoDoKiem = string.Empty;
                    string NgayDoKiem = string.Empty;
                    string NgayDoKiem_En = string.Empty;
                    string CoQuanDoKiem = string.Empty;
                    string CoQuanDoKiem_En = string.Empty;
                    string NGAY_CAP_EN = string.Empty;
                    string GIA_TRI_DEN_EN = string.Empty;
                    string NgayThangCap = string.Empty;
                    string NgayThangCap_EN = string.Empty;
                    string NOI_CAP_EN = string.Empty;
                    bool isHopChuan = false;
                    string KetQuaDoKiem = string.Empty;
                    string KetQuaDoKiem_En = string.Empty;
                    string KetQuaDoKiemKhac = string.Empty;
                    string KetQuaDoKiemKhac_En = string.Empty;

                    if (!string.IsNullOrEmpty(SAN_PHAM_ID))
                    {
                        SanPham SanPham = ProviderFactory.SanPhamProvider.GetById(SAN_PHAM_ID);
                        HoSo objHoSo = ProviderFactory.HoSoProvider.GetById(SanPham.HoSoId);

                        if (SanPham.NoiDungDoKiem != null)
                        {
                            string[] array = SanPham.NoiDungDoKiem.Split('\n');
                            KetQuaDoKiemKhac = array[0];
                            if (array.Length > 1)
                                KetQuaDoKiemKhac_En = array[1];
                        }
                        if (objHoSo.TrangThaiId == (int)EnTrangThaiHoSoList.CHO_LUU_TRU || objHoSo.TrangThaiId == (int)EnTrangThaiHoSoList.DA_DONG)
                        {
                            if (objHoSo.LoaiHoSo == (int)LoaiHoSo.ChungNhanHopQuy)
                            {
                                LOAI_HINH_CHUNG_NHAN = "CHỨNG NHẬN HỢP QUY";
                                CERTIFICATE_TYPE = "TYPE APPROVAL CERTIFICATE";
                                LoaiPhuHop = "Phù hợp quy chuẩn \nkỹ thuật, tiêu chuẩn";
                                strduongdandenteploaiBC = Server.MapPath(@"~\Report\GiayChungNhan.rpt");
                            }
                            else
                            {
                                LOAI_HINH_CHUNG_NHAN = "CHỨNG NHẬN HỢP CHUẨN";
                                CERTIFICATE_TYPE = "CERTIFICATE OF STANDARD CONFORMITY";
                                LoaiPhuHop = "Phù hợp tiêu chuẩn";
                                if (objHoSo.NguonGocId == (int)EnNguonGocList.NK_CHUA_DO_KIEM || objHoSo.NguonGocId == (int)EnNguonGocList.NK_KEM_KQ_DO_KIEM)
                                    strduongdandenteploaiBC = Server.MapPath(@"~\Report\GiayChungNhan_HopChuan_TheoLo.rpt");
                                else
                                    strduongdandenteploaiBC = Server.MapPath(@"~\Report\GiayChungNhan_HopChuan.rpt");
                                isHopChuan = true;
                            }
                            if (SanPham != null)
                            {
                                TEN_TRUNG_TAM_TIENG_VIET = objHoSo.DmTrungTamTenTrungTam;
                                TEN_TRUNG_TAM_TIENG_ANH = objHoSo.DmTrungTamTenTiengAnh;
                                KY_HIEU = !string.IsNullOrEmpty(SanPham.KyHieu) ? SanPham.KyHieu : "";
                                KY_HIEU_EN = "";
                                SO = !string.IsNullOrEmpty(SanPham.SoGcn) ? SanPham.SoGcn : "";
                                SO_EN = "";

                                TEN_SAN_PHAM = !string.IsNullOrEmpty(SanPham.DmSanPhamTenTiengViet) ? SanPham.DmSanPhamTenTiengViet : "";
                                TEN_SAN_PHAM_EN = !string.IsNullOrEmpty(SanPham.DmSanPhamTenTiengAnh) ? SanPham.DmSanPhamTenTiengAnh : "";

                                SAN_XUAT = !string.IsNullOrEmpty(SanPham.DmHsxTenHangSanXuat) ? SanPham.DmHsxTenHangSanXuat : "";
                                SAN_XUAT_EN = !string.IsNullOrEmpty(SanPham.DmHsxTenTiengAnh) ? SanPham.DmHsxTenTiengAnh : "";

                                TList<SanPhamTieuChuanApDung> lstSanPhamTieuChuan;
                                lstSanPhamTieuChuan = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(SAN_PHAM_ID);
                                foreach (SanPhamTieuChuanApDung objSanPhamTieuChuan in lstSanPhamTieuChuan)
                                {
                                    PHU_HOP += objSanPhamTieuChuan.DmTieuChuanMaTieuChuan + "; ";
                                    if (objSanPhamTieuChuan.DmTieuChuanTenTiengAnh != null)
                                        if (objSanPhamTieuChuan.DmTieuChuanTenTiengAnh.Length > 0)
                                            PHU_HOP_EN += objSanPhamTieuChuan.DmTieuChuanTenTiengAnh + "; ";
                                }
                                if (!string.IsNullOrEmpty(PHU_HOP) && PHU_HOP.Length >= 2)
                                {
                                    if (PHU_HOP.Substring(PHU_HOP.Length - 2, 2) == "; ")
                                    {
                                        PHU_HOP = PHU_HOP.Substring(0, PHU_HOP.Length - 2);
                                    }
                                }
                                if (!string.IsNullOrEmpty(PHU_HOP_EN) && PHU_HOP_EN.Length >= 2)
                                {
                                    if (PHU_HOP_EN.Substring(PHU_HOP_EN.Length - 2, 2) == "; ")
                                    {
                                        PHU_HOP_EN = PHU_HOP_EN.Substring(0, PHU_HOP_EN.Length - 2);
                                    }
                                }
                                HoSo hoso = ProviderFactory.HoSoProvider.GetById(SanPham.HoSoId);


                                DON_VI = !string.IsNullOrEmpty(hoso.DmDonViTenTiengViet) ? hoso.DmDonViTenTiengViet : "";
                                DON_VI_EN = !string.IsNullOrEmpty(hoso.DmDonViTenTiengAnh) ? hoso.DmDonViTenTiengAnh : "";

                                DmTinhThanh TinhThanh = ProviderFactory.DmTinhThanhProvider.GetById(objHoSo.DmTrungTamTinhThanhId);
                                if (TinhThanh != null)
                                {
                                    NOI_CAP = TinhThanh.TenTinhThanh;
                                    NOI_CAP_EN = TinhThanh.MaTinhThanh;
                                }

                                if (SanPham.NgayKyDuyet != null)
                                {
                                    DateTime temp;
                                    NGAY_CAP = string.Format("{0:dd/MM/yyyy}", SanPham.NgayKyDuyet.Value);

                                    NgayThangCap = NOI_CAP + "," + TalkDate(SanPham.NgayKyDuyet.Value);

                                    this.UICulture = "en-US";
                                    this.Culture = "en-US";
                                    NGAY_CAP_EN = SanPham.NgayKyDuyet.Value.ToString("MMM dd, yyyy");
                                    NgayThangCap_EN = NOI_CAP_EN + ", " + SanPham.NgayKyDuyet.Value.ToString("MMMM dd, yyyy");
                                    UICulture = "vi-VN";
                                    Culture = "vi-VN";

                                    if (SanPham.ThoiHanId != null)
                                    {
                                        int ThoiHanID = SanPham.ThoiHanId.Value;
                                        if (ThoiHanID == Convert.ToInt32(EnThoiHanList.HAI_NAM))
                                        {
                                            temp = new DateTime(SanPham.NgayKyDuyet.Value.Year + 2, SanPham.NgayKyDuyet.Value.Month, SanPham.NgayKyDuyet.Value.Day);
                                            GIA_TRI_DEN = string.Format("{0:dd/MM/yyyy}", temp);

                                            this.UICulture = "en-US";
                                            this.Culture = "en-US";
                                            GIA_TRI_DEN_EN = temp.ToString("MMM dd, yyyy");
                                            UICulture = "vi-VN";
                                            Culture = "vi-VN";
                                        }
                                        else if (ThoiHanID == Convert.ToInt32(EnThoiHanList.BA_NAM))
                                        {
                                            temp = new DateTime(SanPham.NgayKyDuyet.Value.Year + 3, SanPham.NgayKyDuyet.Value.Month, SanPham.NgayKyDuyet.Value.Day);
                                            GIA_TRI_DEN = string.Format("{0:dd/MM/yyyy}", temp);

                                            this.UICulture = "en-US";
                                            this.Culture = "en-US";
                                            GIA_TRI_DEN_EN = temp.ToString("MMM dd, yyyy");
                                            UICulture = "vi-VN";
                                            Culture = "vi-VN";
                                        }
                                        else if (ThoiHanID == Convert.ToInt32(EnThoiHanList.MOT_LAN_SU_DUNG))
                                        {
                                            GIA_TRI_DEN = "Một lần sử dụng";
                                        }
                                    }
                                }

                                CHUC_VU = LayTenChucVu_Them(LoaiBaoCao, SanPham.ChucVu.Value, SanPham.TenNguoiKyDuyet, SanPham.TenTrungTam, SanPham.PhongBan);
                                if (Convert.ToInt32(SanPham.ChucVu.Value) == Convert.ToInt32(EnChucVuList.GDTT))
                                {
                                    GIAM_DOC = "GIÁM ĐỐC";
                                }
                                SoDoKiem = SanPham.SoDoKiem == null ? string.Empty : SanPham.SoDoKiem;
                                NgayDoKiem = SanPham.NgayDoKiem == null ? string.Empty : Convert.ToDateTime(SanPham.NgayDoKiem).ToShortDateString();
                                this.UICulture = "en-US";
                                this.Culture = "en-US";
                                NgayDoKiem_En = SanPham.NgayDoKiem == null ? string.Empty : SanPham.NgayDoKiem.Value.ToString("MMM dd, yyyy");
                                UICulture = "vi-VN";
                                Culture = "vi-VN";
                                CoQuanDoKiem = SanPham.DmCqdkTenCoQuanDoKiem;
                                CoQuanDoKiem_En = SanPham.DmCqdkTenTiengAnh;
                            }
                        }
                        else
                        {
                            if (objHoSo.LoaiHoSo == (int)LoaiHoSo.ChungNhanHopQuy)
                            {
                                LOAI_HINH_CHUNG_NHAN = "CHỨNG NHẬN HỢP QUY";
                                CERTIFICATE_TYPE = "TYPE APPROVAL CERTIFICATE";
                                LoaiPhuHop = "Phù hợp quy chuẩn \nkỹ thuật, tiêu chuẩn";
                                strduongdandenteploaiBC = Server.MapPath(@"~\Report\GiayChungNhan.rpt");
                            }
                            else
                            {
                                LOAI_HINH_CHUNG_NHAN = "CHỨNG NHẬN HỢP CHUẨN";
                                CERTIFICATE_TYPE = "CERTIFICATE OF STANDARD CONFORMITY";
                                LoaiPhuHop = "Phù hợp tiêu chuẩn";
                                if (objHoSo.NguonGocId == (int)EnNguonGocList.NK_CHUA_DO_KIEM || objHoSo.NguonGocId == (int)EnNguonGocList.NK_KEM_KQ_DO_KIEM)
                                    strduongdandenteploaiBC = Server.MapPath(@"~\Report\GiayChungNhan_HopChuan_TheoLo.rpt");
                                else
                                    strduongdandenteploaiBC = Server.MapPath(@"~\Report\GiayChungNhan_HopChuan.rpt");
                                isHopChuan = true;
                            }

                            string TrungTamId = objHoSo.TrungTamId;
                            DmTrungTam objTrungTam = ProviderFactory.DmTrungTamProvider.GetById(TrungTamId);
                            if (objTrungTam != null)
                            {
                                TEN_TRUNG_TAM_TIENG_ANH = objTrungTam.TenTiengAnh;
                                TEN_TRUNG_TAM_TIENG_VIET = objTrungTam.TenTrungTam;
                            }

                            if (SanPham != null)
                            {
                                KY_HIEU = !string.IsNullOrEmpty(SanPham.KyHieu) ? SanPham.KyHieu : "";
                                KY_HIEU_EN = "";
                                SO = !string.IsNullOrEmpty(SanPham.SoGcn) ? SanPham.SoGcn : "";
                                SO_EN = "";
                                DmSanPham SanPhamObject = ProviderFactory.DmSanPhamProvider.GetById(SanPham.SanPhamId);
                                if (SanPhamObject != null)
                                {
                                    TEN_SAN_PHAM = !string.IsNullOrEmpty(SanPhamObject.TenTiengViet) ? SanPhamObject.TenTiengViet : "";
                                    TEN_SAN_PHAM_EN = !string.IsNullOrEmpty(SanPhamObject.TenTiengAnh) ? SanPhamObject.TenTiengAnh : "";
                                }

                                DmHangSanXuat SanXuatObject = ProviderFactory.DmHangSanXuatProvider.GetById(SanPham.HangSanXuatId);
                                if (SanXuatObject != null)
                                {
                                    SAN_XUAT = !string.IsNullOrEmpty(SanXuatObject.TenHangSanXuat) ? SanXuatObject.TenHangSanXuat : "";
                                    SAN_XUAT_EN = !string.IsNullOrEmpty(SanXuatObject.TenTiengAnh) ? SanXuatObject.TenTiengAnh : "";
                                }
                                TList<SanPhamTieuChuanApDung> lstSanPhamTieuChuan;
                                lstSanPhamTieuChuan = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(SAN_PHAM_ID);
                                DmTieuChuan objDmTC = new DmTieuChuan();
                                foreach (SanPhamTieuChuanApDung objSanPhamTieuChuan in lstSanPhamTieuChuan)
                                {
                                    objDmTC = ProviderFactory.DmTieuChuanProvider.GetById(objSanPhamTieuChuan.TieuChuanApDungId);
                                    PHU_HOP += objDmTC.MaTieuChuan + "; ";
                                    if (objDmTC.TenTiengAnh != null)
                                        if (objDmTC.TenTiengAnh.Length > 0)
                                            PHU_HOP_EN += objDmTC.TenTiengAnh + "; ";
                                }
                                if (!string.IsNullOrEmpty(PHU_HOP) && PHU_HOP.Length > 2)
                                {
                                    if (PHU_HOP.Substring(PHU_HOP.Length - 2, 2) == "; ")
                                    {
                                        PHU_HOP = PHU_HOP.Substring(0, PHU_HOP.Length - 2);
                                    }
                                }
                                if (!string.IsNullOrEmpty(PHU_HOP_EN) && PHU_HOP_EN.Length >= 2)
                                {
                                    if (PHU_HOP_EN.Substring(PHU_HOP_EN.Length - 2, 2) == "; ")
                                    {
                                        PHU_HOP_EN = PHU_HOP_EN.Substring(0, PHU_HOP_EN.Length - 2);
                                    }
                                }
                                HoSo hoso = ProviderFactory.HoSoProvider.GetById(SanPham.HoSoId);
                                DmDonVi DonViObject = ProviderFactory.DmDonViProvider.GetById(hoso.DonViId);
                                if (DonViObject != null)
                                {
                                    DON_VI = !string.IsNullOrEmpty(DonViObject.TenTiengViet) ? DonViObject.TenTiengViet : "";
                                    DON_VI_EN = !string.IsNullOrEmpty(DonViObject.TenTiengAnh) ? DonViObject.TenTiengAnh : "";
                                    DmTinhThanh TinhThanh = ProviderFactory.DmTinhThanhProvider.GetById(objTrungTam.TinhThanhId);
                                    if (TinhThanh != null)
                                    {
                                        NOI_CAP = TinhThanh.TenTinhThanh;
                                        NOI_CAP_EN = TinhThanh.MaTinhThanh;
                                    }
                                }

                                if (SanPham.NgayKyDuyet != null)
                                {
                                    DateTime temp;
                                    NGAY_CAP = string.Format("{0:dd/MM/yyyy}", SanPham.NgayKyDuyet.Value);
                                    NgayThangCap = NOI_CAP + "," + TalkDate(SanPham.NgayKyDuyet.Value);

                                    this.UICulture = "en-US";
                                    this.Culture = "en-US";
                                    NGAY_CAP_EN = SanPham.NgayKyDuyet.Value.ToString("MMM dd, yyyy");
                                    NgayThangCap_EN = NOI_CAP_EN + ", " + SanPham.NgayKyDuyet.Value.ToString("MMMM dd, yyyy");
                                    UICulture = "vi-VN";
                                    Culture = "vi-VN";

                                    if (SanPham.ThoiHanId != null)
                                    {
                                        int ThoiHanID = SanPham.ThoiHanId.Value;
                                        if (ThoiHanID == Convert.ToInt32(EnThoiHanList.HAI_NAM))
                                        {
                                            temp = new DateTime(SanPham.NgayKyDuyet.Value.Year + 2, SanPham.NgayKyDuyet.Value.Month, SanPham.NgayKyDuyet.Value.Day);
                                            GIA_TRI_DEN = string.Format("{0:dd/MM/yyyy}", temp);
                                            this.UICulture = "en-US";
                                            this.Culture = "en-US";
                                            GIA_TRI_DEN_EN = temp.ToString("MMM dd, yyyy");
                                            UICulture = "vi-VN";
                                            Culture = "vi-VN";
                                        }
                                        else if (ThoiHanID == Convert.ToInt32(EnThoiHanList.BA_NAM))
                                        {
                                            temp = new DateTime(SanPham.NgayKyDuyet.Value.Year + 3, SanPham.NgayKyDuyet.Value.Month, SanPham.NgayKyDuyet.Value.Day);
                                            GIA_TRI_DEN = string.Format("{0:dd/MM/yyyy}", temp);
                                            this.UICulture = "en-US";
                                            this.Culture = "en-US";
                                            GIA_TRI_DEN_EN = temp.ToString("MMM dd, yyyy");
                                            UICulture = "vi-VN";
                                            Culture = "vi-VN";
                                        }
                                        else if (ThoiHanID == Convert.ToInt32(EnThoiHanList.MOT_LAN_SU_DUNG))
                                        {
                                            GIA_TRI_DEN = "Một lần sử dụng";
                                        }

                                    }
                                }
                                if (SanPham.NguoiKyDuyetId != null)
                                {
                                    SysUser NguoiKyDuyet = ProviderFactory.SysUserProvider.GetById(SanPham.NguoiKyDuyetId);
                                    if (NguoiKyDuyet != null)
                                    {
                                        CHUC_VU = LayTenChucVu_Cu(LoaiBaoCao, NguoiKyDuyet);
                                        if (Convert.ToInt32(NguoiKyDuyet.Position) == Convert.ToInt32(EnChucVuList.GDTT))
                                        {
                                            GIAM_DOC = "GIÁM ĐỐC";
                                        }
                                    }
                                }
                                SoDoKiem = SanPham.SoDoKiem == null ? string.Empty : SanPham.SoDoKiem;
                                NgayDoKiem = SanPham.NgayDoKiem == null ? string.Empty : Convert.ToDateTime(SanPham.NgayDoKiem).ToShortDateString();
                                this.UICulture = "en-US";
                                this.Culture = "en-US";
                                NgayDoKiem_En = SanPham.NgayDoKiem == null ? string.Empty : SanPham.NgayDoKiem.Value.ToString("MMM dd, yyyy");
                                UICulture = "vi-VN";
                                Culture = "vi-VN";
                                string CoQuanDoKiemId = SanPham.CoQuanDoKiemId == null ? string.Empty : SanPham.CoQuanDoKiemId;
                                if (!string.IsNullOrEmpty(CoQuanDoKiemId))
                                {
                                    DmCoQuanDoKiem objDoKiem = ProviderFactory.DmCoQuanDoKiemProvider.GetById(CoQuanDoKiemId);
                                    if (objDoKiem != null)
                                    {
                                        CoQuanDoKiem = objDoKiem.TenCoQuanDoKiem;
                                        CoQuanDoKiem_En = objDoKiem.TenTiengAnh;
                                    }
                                }

                            }
                        }
                    }

                    // Noi dung do kiem tieng viet
                    if (!string.IsNullOrEmpty(SoDoKiem))
                        KetQuaDoKiem += "số " + SoDoKiem + " ";
                    if (!string.IsNullOrEmpty(NgayDoKiem))
                    {
                        KetQuaDoKiem += "ngày " + NgayDoKiem + " ";
                    }
                    if (!string.IsNullOrEmpty(CoQuanDoKiem))
                        if (KetQuaDoKiem.Length > 0)
                            KetQuaDoKiem += "của " + CoQuanDoKiem;
                        else
                            KetQuaDoKiem += CoQuanDoKiem;

                    if (!string.IsNullOrEmpty(KetQuaDoKiemKhac))
                    {
                        if (KetQuaDoKiem.Length > 0)
                            KetQuaDoKiem += "; " + KetQuaDoKiemKhac;
                        else
                            KetQuaDoKiem += KetQuaDoKiemKhac;
                    }

                    // Noi dung do kiem tieng anh
                    if (!string.IsNullOrEmpty(SoDoKiem))
                        KetQuaDoKiem_En += "No: " + SoDoKiem + " ";
                    if (!string.IsNullOrEmpty(NgayDoKiem_En))
                    {
                        KetQuaDoKiem_En += "dated " + NgayDoKiem_En + " ";
                    }
                    if (!string.IsNullOrEmpty(CoQuanDoKiem_En))
                        if (KetQuaDoKiem_En.Length > 0)
                            KetQuaDoKiem_En += "by " + CoQuanDoKiem_En;
                        else
                            KetQuaDoKiem_En += CoQuanDoKiem_En;

                    if (!string.IsNullOrEmpty(KetQuaDoKiemKhac_En))
                    {
                        if (KetQuaDoKiem_En.Length > 0)
                            KetQuaDoKiem_En += "; " + KetQuaDoKiemKhac_En;
                        else
                            KetQuaDoKiem_En += KetQuaDoKiemKhac_En;
                    }

                    rd.Load(strduongdandenteploaiBC);

                    //Set tham số cho báo cáo
                    rd.ParameterFields["LOAI_HINH_CHUNG_NHAN"].CurrentValues.AddValue(LOAI_HINH_CHUNG_NHAN);
                    rd.ParameterFields["CERTIFICATE_TYPE"].CurrentValues.AddValue(CERTIFICATE_TYPE);
                    rd.ParameterFields["LoaiPhuHop"].CurrentValues.AddValue(LoaiPhuHop);
                    rd.ParameterFields["CHUC_VU"].CurrentValues.AddValue(CHUC_VU);
                    rd.ParameterFields["GIAM_DOC"].CurrentValues.AddValue(GIAM_DOC);
                    rd.ParameterFields["TEN_TRUNG_TAM_TIENG_VIET"].CurrentValues.AddValue(TEN_TRUNG_TAM_TIENG_VIET.ToUpper());
                    rd.ParameterFields["TEN_TRUNG_TAM_TIENG_ANH"].CurrentValues.AddValue(TEN_TRUNG_TAM_TIENG_ANH);
                    rd.ParameterFields["SoDoKiem"].CurrentValues.AddValue(SoDoKiem);
                    rd.ParameterFields["NgayDoKiem"].CurrentValues.AddValue(NgayDoKiem);
                    rd.ParameterFields["NgayDoKiem_En"].CurrentValues.AddValue(NgayDoKiem_En);
                    rd.ParameterFields["CoQuanDoKiem"].CurrentValues.AddValue(CoQuanDoKiem != null ? CoQuanDoKiem : string.Empty);
                    rd.ParameterFields["CoQuanDoKiem_En"].CurrentValues.AddValue(CoQuanDoKiem_En != null ? CoQuanDoKiem_En : string.Empty);

                    rd.ParameterFields["KetQuaDoKiem"].CurrentValues.AddValue(KetQuaDoKiem != null ? KetQuaDoKiem : string.Empty);
                    rd.ParameterFields["KetQuaDoKiem_En"].CurrentValues.AddValue(KetQuaDoKiem_En != null ? KetQuaDoKiem_En : string.Empty);

                    if (string.IsNullOrEmpty(SO_EN))
                    {
                        rd.ParameterFields["SO"].CurrentValues.AddValue(SO);
                        rd.ParameterFields["SO_EN"].CurrentValues.AddValue("");
                    }
                    else
                    {
                        rd.ParameterFields["SO"].CurrentValues.AddValue("");
                        rd.ParameterFields["SO_EN"].CurrentValues.AddValue(":    " + SO + "\n" + ":    " + SO_EN);
                    }
                    if (string.IsNullOrEmpty(TEN_SAN_PHAM_EN))
                    {
                        // Nếu chỉ có 1 dòng thì hiển căn giữa dòng
                        if (TEN_SAN_PHAM.Length < 60)
                        {
                            rd.ParameterFields["TEN_SAN_PHAM"].CurrentValues.AddValue(TEN_SAN_PHAM.ToUpper());
                            rd.ParameterFields["TEN_SAN_PHAM_VN"].CurrentValues.AddValue("");
                            rd.ParameterFields["TEN_SAN_PHAM_EN"].CurrentValues.AddValue("");
                        }
                        else // Nếu có nhiều hơn 1 dòng thì căn top
                        {
                            rd.ParameterFields["TEN_SAN_PHAM"].CurrentValues.AddValue("");
                            rd.ParameterFields["TEN_SAN_PHAM_VN"].CurrentValues.AddValue(TEN_SAN_PHAM.ToUpper());
                            rd.ParameterFields["TEN_SAN_PHAM_EN"].CurrentValues.AddValue("");
                        }
                    }
                    else
                    {
                        rd.ParameterFields["TEN_SAN_PHAM"].CurrentValues.AddValue("");
                        rd.ParameterFields["TEN_SAN_PHAM_VN"].CurrentValues.AddValue(TEN_SAN_PHAM.ToUpper());
                        rd.ParameterFields["TEN_SAN_PHAM_EN"].CurrentValues.AddValue(TEN_SAN_PHAM_EN);
                    }

                    if (string.IsNullOrEmpty(KY_HIEU_EN))
                    {
                        // Nếu chỉ có 1 dòng thì hiển căn giữa dòng
                        if (KY_HIEU.Length < 60)
                        {
                            rd.ParameterFields["KY_HIEU"].CurrentValues.AddValue(KY_HIEU);
                            rd.ParameterFields["KY_HIEU_VN"].CurrentValues.AddValue("");
                            rd.ParameterFields["KY_HIEU_EN"].CurrentValues.AddValue("");
                        }
                        else
                        {
                            rd.ParameterFields["KY_HIEU"].CurrentValues.AddValue("");
                            rd.ParameterFields["KY_HIEU_VN"].CurrentValues.AddValue(KY_HIEU);
                            rd.ParameterFields["KY_HIEU_EN"].CurrentValues.AddValue("");
                        }
                    }
                    else
                    {
                        rd.ParameterFields["KY_HIEU"].CurrentValues.AddValue("");
                        rd.ParameterFields["KY_HIEU_VN"].CurrentValues.AddValue(KY_HIEU);
                        rd.ParameterFields["KY_HIEU_EN"].CurrentValues.AddValue(KY_HIEU_EN);
                    }

                    if (string.IsNullOrEmpty(SAN_XUAT_EN))
                    {
                        if (SAN_XUAT.Length < 60)
                        {
                            rd.ParameterFields["SAN_XUAT"].CurrentValues.AddValue(SAN_XUAT.ToUpper());
                            rd.ParameterFields["SAN_XUAT_VN"].CurrentValues.AddValue("");
                            rd.ParameterFields["SAN_XUAT_EN"].CurrentValues.AddValue("");
                        }
                        else
                        {
                            rd.ParameterFields["SAN_XUAT"].CurrentValues.AddValue("");
                            rd.ParameterFields["SAN_XUAT_VN"].CurrentValues.AddValue(SAN_XUAT.ToUpper());
                            rd.ParameterFields["SAN_XUAT_EN"].CurrentValues.AddValue("");
                        }
                    }
                    else
                    {
                        rd.ParameterFields["SAN_XUAT"].CurrentValues.AddValue("");
                        rd.ParameterFields["SAN_XUAT_VN"].CurrentValues.AddValue(SAN_XUAT.ToUpper());
                        rd.ParameterFields["SAN_XUAT_EN"].CurrentValues.AddValue(SAN_XUAT_EN);
                    }

                    if (string.IsNullOrEmpty(DON_VI_EN))
                    {
                        if (DON_VI.Length < 60)
                        {
                            rd.ParameterFields["DON_VI"].CurrentValues.AddValue(DON_VI.ToUpper());
                            rd.ParameterFields["DON_VI_VN"].CurrentValues.AddValue("");
                            rd.ParameterFields["DON_VI_EN"].CurrentValues.AddValue("");
                        }
                        else
                        {
                            rd.ParameterFields["DON_VI"].CurrentValues.AddValue("");
                            rd.ParameterFields["DON_VI_VN"].CurrentValues.AddValue(DON_VI.ToUpper());
                            rd.ParameterFields["DON_VI_EN"].CurrentValues.AddValue("");
                        }
                    }
                    else
                    {
                        rd.ParameterFields["DON_VI"].CurrentValues.AddValue("");
                        rd.ParameterFields["DON_VI_VN"].CurrentValues.AddValue(DON_VI.ToUpper());
                        rd.ParameterFields["DON_VI_EN"].CurrentValues.AddValue(DON_VI_EN);
                    }

                    if (string.IsNullOrEmpty(PHU_HOP_EN))
                    {
                        if (PHU_HOP.Length < 60)
                        {
                            rd.ParameterFields["PHU_HOP"].CurrentValues.AddValue(PHU_HOP);
                            rd.ParameterFields["PHU_HOP_VN"].CurrentValues.AddValue("");
                            rd.ParameterFields["PHU_HOP_EN"].CurrentValues.AddValue("");
                        }
                        else
                        {
                            rd.ParameterFields["PHU_HOP"].CurrentValues.AddValue("");
                            rd.ParameterFields["PHU_HOP_VN"].CurrentValues.AddValue(PHU_HOP);
                            rd.ParameterFields["PHU_HOP_EN"].CurrentValues.AddValue("");
                        }
                    }
                    else
                    {
                        rd.ParameterFields["PHU_HOP"].CurrentValues.AddValue("");
                        rd.ParameterFields["PHU_HOP_VN"].CurrentValues.AddValue(PHU_HOP);
                        rd.ParameterFields["PHU_HOP_EN"].CurrentValues.AddValue(PHU_HOP_EN);
                    }
                    if (string.IsNullOrEmpty(NOI_CAP_1))
                    {
                        rd.ParameterFields["NOI_CAP"].CurrentValues.AddValue(NOI_CAP);
                        rd.ParameterFields["NOI_CAP_1"].CurrentValues.AddValue("");
                    }
                    else
                    {
                        rd.ParameterFields["NOI_CAP"].CurrentValues.AddValue("");
                        rd.ParameterFields["NOI_CAP_1"].CurrentValues.AddValue(NOI_CAP + "\n" + NOI_CAP_1);
                    }
                    if (string.IsNullOrEmpty(NGAY_CAP_1))
                    {
                        rd.ParameterFields["NGAY_CAP"].CurrentValues.AddValue(NGAY_CAP);
                        rd.ParameterFields["NGAY_CAP_1"].CurrentValues.AddValue("");
                        if (isHopChuan)
                        {
                            rd.ParameterFields["NGAY_CAP_EN"].CurrentValues.AddValue(NGAY_CAP_EN);
                            rd.ParameterFields["NgayThangCap"].CurrentValues.AddValue(NgayThangCap);
                            rd.ParameterFields["NgayThangCap_EN"].CurrentValues.AddValue(NgayThangCap_EN);
                        }
                    }
                    else
                    {
                        rd.ParameterFields["NGAY_CAP"].CurrentValues.AddValue("");
                        rd.ParameterFields["NGAY_CAP_1"].CurrentValues.AddValue(NGAY_CAP + "\n" + NGAY_CAP_1);
                    }
                    if (string.IsNullOrEmpty(GIA_TRI_DEN_1))
                    {
                        rd.ParameterFields["GIA_TRI_DEN"].CurrentValues.AddValue(GIA_TRI_DEN);
                        rd.ParameterFields["GIA_TRI_DEN_1"].CurrentValues.AddValue("");
                        if (isHopChuan)
                            rd.ParameterFields["GIA_TRI_DEN_EN"].CurrentValues.AddValue(GIA_TRI_DEN_EN);
                    }
                    else
                    {
                        rd.ParameterFields["GIA_TRI_DEN"].CurrentValues.AddValue("");
                        rd.ParameterFields["GIA_TRI_DEN_1"].CurrentValues.AddValue(GIA_TRI_DEN + "\n" + GIA_TRI_DEN_1);
                    }
                }
                break;
            #endregion

            #region In Lệ phí Tiếp nhận Chứng nhận hợp Quy - LongHH
            case "LePhiTiepNhanCNHQ":
                strduongdandenteploaiBC = Server.MapPath(@"~\Report\LePhiTiepNhanCNHQ.rpt");
                rd = new ReportDocument();
                rd.Load(strduongdandenteploaiBC);
                rd.PrintOptions.PaperSize = PaperSize.PaperA4;
                if (rd != null)
                {
                    string TrungTamID = _mUserInfo.TrungTam.Id;
                    int SLPhiTiepNhan = Request["SLPhiTiepNhan"] == null ? 1 : int.Parse(Request["SLPhiTiepNhan"].ToString());
                    HoSo objHS = ProviderFactory.HoSoProvider.GetById(Request["HoSoID"].ToString());
                    DmDonVi objDV = ProviderFactory.DmDonViProvider.GetById(objHS.DonViId);
                    DmTrungTam TrungTamObject = ProviderFactory.DmTrungTamProvider.GetById(TrungTamID);
                    String IDNguoiKy = QLCL_Patch.GetNguoiKyGiayBaoPhi(Request["HoSoID"].ToString());

                    SysUser nguoiky = ProviderFactory.SysUserProvider.GetById(QLCL_Patch.GetNguoiKyGiayBaoPhi(Request["HoSoID"].ToString()));
                    NumberUtil objTalkNumber = new NumberUtil();
                    if (TrungTamObject == null)
                    {
                        rd = null;
                        break;
                    }
                    string TenTrungTam = !string.IsNullOrEmpty(TrungTamObject.TenTrungTam) ? TrungTamObject.TenTrungTam : "";
                    string SoGiayThongBaoLePhi = string.Empty;
                    string TinhThanhTrungTam = ProviderFactory.DmTinhThanhProvider.GetById(TrungTamObject.TinhThanhId).TenTinhThanh;
                    string NgayPheDuyet = TalkDate(objHS.NgayTiepNhan.Value);
                    string TenTiengViet = string.Empty;
                    string DiaChi = string.Empty;
                    string DienThoai = string.Empty;
                    string MaSoThue = string.Empty;
                    string Fax = string.Empty;
                    string Email = string.Empty;
                    string NgayPheDuyetShort = string.Empty;

                    string NoiDungChuyenKhoan = string.Empty;
                    string HoTenNguoiKy = string.Empty;
                    string ChucVu = string.Empty;
                    DataTable TTGiayBaoPhi = QLCL_Patch.GetTTGiayBaoPhi(Request["HoSoID"].ToString());
                    if (TTGiayBaoPhi.Rows.Count > 0)
                    {
                        try
                        {
                            SLPhiTiepNhan = int.Parse(TTGiayBaoPhi.Rows[0]["SLTiepNhan"].ToString());
                            SoGiayThongBaoLePhi = TTGiayBaoPhi.Rows[0]["SoGiayThongBaoLePhi"].ToString();
                        }
                        catch { }
                    }
                    long CongTien = SLPhiTiepNhan * ((int)QLCL_Patch.LePhi.DonGiaTiepNhan + (int)QLCL_Patch.LePhi.PhiXemXet);
                    long VAT = CongTien / 10;
                    long TongTien = CongTien + VAT;
                    string BangChu = objTalkNumber.Speak(TongTien * 1000);
                    if (objHS != null)
                    {
                        SoGiayThongBaoLePhi = string.IsNullOrEmpty(SoGiayThongBaoLePhi) ? objHS.SoHoSo.Replace("CNHQ", "PNHS") : SoGiayThongBaoLePhi;
                        DmDonVi objDonVi = new DmDonVi();
                        objDonVi = ProviderFactory.DmDonViProvider.GetById(objHS.DonViId);
                        TenTiengViet = objDonVi.TenTiengViet;
                        DiaChi = objDonVi.DiaChi;
                        DienThoai = objDonVi.DienThoai;
                        MaSoThue = objDonVi.MaSoThue;
                        Fax = objDonVi.Fax;
                        Email = objDonVi.Email;
                        NgayPheDuyetShort = objHS.NgayTiepNhan.Value.ToShortDateString();

                    }
                    if (objDV != null)
                    {
                        TenTiengViet = objDV.TenTiengViet;
                        DiaChi = objDV.DiaChi;
                        DienThoai = objDV.DienThoai;
                        MaSoThue = objDV.MaSoThue;
                        Fax = objDV.Fax;
                        Email = objDV.Email;
                    }
                    if (nguoiky != null)
                    {
                        HoTenNguoiKy = nguoiky.FullName;
                        ChucVu = QLCL_Patch.GetChucVuKy(nguoiky.Position);
                    }
                    String TenKhoBac = "Ngân hàng Thương mại Cổ phần Á châu - Hội sở";// Sua theo yeu cau
                    DataTable returnTable = new DataTable();
                    returnTable.TableName = "LePhiTiepNhanCNHQ";
                    returnTable.Columns.Add("STT");
                    returnTable.Columns.Add("LoaiDichVu");
                    returnTable.Columns.Add("DonGia");
                    returnTable.Columns.Add("SoLuong");
                    returnTable.Columns.Add("ThanhTien");

                    DataRow row1 = returnTable.NewRow();
                    row1["STT"] = "1";
                    row1["LoaiDichVu"] = "Phí tiếp nhận";
                    row1["DonGia"] = string.Format("{0:0,0}.000", (int)QLCL_Patch.LePhi.DonGiaTiepNhan);
                    row1["SoLuong"] = SLPhiTiepNhan;
                    row1["ThanhTien"] = string.Format("{0:0,0}.000", SLPhiTiepNhan * (int)QLCL_Patch.LePhi.DonGiaTiepNhan);
                    returnTable.Rows.Add(row1);
                    DataRow row2 = returnTable.NewRow();
                    row2["STT"] = "2";
                    row2["LoaiDichVu"] = "Phí xem xét";
                    row2["DonGia"] = string.Format("{0:0,0}.000", (int)QLCL_Patch.LePhi.PhiXemXet);
                    row2["SoLuong"] = SLPhiTiepNhan;
                    row2["ThanhTien"] = string.Format("{0:0,0}.000", SLPhiTiepNhan * (int)QLCL_Patch.LePhi.PhiXemXet);
                    returnTable.Rows.Add(row2);

                    rd.SetDataSource(returnTable);
                    rd.Refresh();

                    rd.SetParameterValue("SoGiayThongBaoLePhi", SoGiayThongBaoLePhi);
                    rd.SetParameterValue("TinhThanhTrungTam", TinhThanhTrungTam);
                    rd.SetParameterValue("TenTrungTam", TenTrungTam);
                    rd.SetParameterValue("NgayPheDuyet", NgayPheDuyet);
                    rd.SetParameterValue("TenTiengViet", TenTiengViet);
                    rd.SetParameterValue("DiaChi", DiaChi);
                    rd.SetParameterValue("DienThoai", DienThoai);
                    rd.SetParameterValue("Fax", Fax);
                    rd.SetParameterValue("MaSoThue", MaSoThue);
                    rd.SetParameterValue("Email", Email);
                    rd.SetParameterValue("NgayPheDuyetShort", NgayPheDuyetShort);
                    rd.SetParameterValue("CongTien", string.Format("{0:0,0}.000", CongTien));
                    rd.SetParameterValue("VAT", string.Format("{0:0,0}.000", VAT));
                    rd.SetParameterValue("TongTien", string.Format("{0:0,0}.000", TongTien));
                    rd.SetParameterValue("BangChu", BangChu);
                    rd.SetParameterValue("DVThuHuongDiaChi", TrungTamObject.DiaChiCoQuanThuHuong);
                    rd.SetParameterValue("SoTaiKhoan", TrungTamObject.SoTaiKhoan);
                    rd.SetParameterValue("TenKhoBac", TenKhoBac);


                    rd.SetParameterValue("HoTenNguoiKy", HoTenNguoiKy);
                    rd.SetParameterValue("ChucVu", ChucVu);

                }
                break;
            #endregion

            #region In Lệ phí Thu phí đánh giá QTSX - LongHH
            case "ThuPhiDanhGiaQTSX_Old":
                strduongdandenteploaiBC = Server.MapPath(@"~\Report\ThuPhiDanhGiaQTSX.rpt");
                rd = new ReportDocument();
                rd.Load(strduongdandenteploaiBC);
                rd.PrintOptions.PaperSize = PaperSize.PaperA4;
                if (rd != null)
                {
                    string TrungTamID = _mUserInfo.TrungTam.Id;
                    int SLDanhGia = Request["SLDanhGia"] == null ? 1 : int.Parse(Request["SLDanhGia"].ToString());
                    int SLLayMau = Request["SLLayMau"] == null ? 1 : int.Parse(Request["SLLayMau"].ToString());
                    HoSo objHS = ProviderFactory.HoSoProvider.GetById(Request["HoSoID"].ToString());
                    DmDonVi objDV = ProviderFactory.DmDonViProvider.GetById(objHS.DonViId);
                    DmTrungTam TrungTamObject = ProviderFactory.DmTrungTamProvider.GetById(TrungTamID);
                    String IDNguoiKy = QLCL_Patch.GetNguoiKyGiayBaoPhi(Request["HoSoID"].ToString());

                    SysUser nguoiky = ProviderFactory.SysUserProvider.GetById(QLCL_Patch.GetNguoiKyGiayBaoPhi(Request["HoSoID"].ToString()));
                    NumberUtil objTalkNumber = new NumberUtil();
                    if (TrungTamObject == null)
                    {
                        rd = null;
                        break;
                    }
                    string TenTrungTam = !string.IsNullOrEmpty(TrungTamObject.TenTrungTam) ? TrungTamObject.TenTrungTam : "";
                    string SoGiayThongBaoLePhi = string.Empty;
                    string TinhThanhTrungTam = ProviderFactory.DmTinhThanhProvider.GetById(TrungTamObject.TinhThanhId).TenTinhThanh;
                    string NgayPheDuyet = TalkDate(objHS.NgayTiepNhan.Value);
                    string TenTiengViet = string.Empty;
                    string DiaChi = string.Empty;
                    string DienThoai = string.Empty;
                    string MaSoThue = string.Empty;
                    string Fax = string.Empty;
                    string Email = string.Empty;
                    string NgayPheDuyetShort = string.Empty;

                    string NoiDungChuyenKhoan = string.Empty;
                    string HoTenNguoiKy = string.Empty;
                    string ChucVu = string.Empty;
                    DataTable TTGiayBaoPhi = QLCL_Patch.GetTTGiayBaoPhiDanhGiaQTSX(Request["HoSoID"].ToString());
                    if (TTGiayBaoPhi.Rows.Count > 0)
                    {
                        try
                        {
                            SLDanhGia = int.Parse(TTGiayBaoPhi.Rows[0]["SLTiepNhan"].ToString());
                            SLLayMau = int.Parse(TTGiayBaoPhi.Rows[0]["SLXemXet"].ToString());
                        }
                        catch { }
                    }
                    long CongTien = SLDanhGia * (int)QLCL_Patch.LePhi.PhiDanhGiaQuaTrinhSanXuat + SLLayMau * (int)QLCL_Patch.LePhi.PhiLayMauSanPham;
                    long VAT = CongTien / 10;
                    long TongTien = CongTien + VAT;
                    string BangChu = objTalkNumber.Speak(TongTien * 1000);
                    if (objHS != null)
                    {
                        SoGiayThongBaoLePhi = string.IsNullOrEmpty(SoGiayThongBaoLePhi) ? objHS.SoHoSo.Replace("CNHQ", "PĐGQT") : SoGiayThongBaoLePhi;
                        DmDonVi objDonVi = new DmDonVi();
                        objDonVi = ProviderFactory.DmDonViProvider.GetById(objHS.DonViId);
                        TenTiengViet = objDonVi.TenTiengViet;
                        DiaChi = objDonVi.DiaChi;
                        DienThoai = objDonVi.DienThoai;
                        MaSoThue = objDonVi.MaSoThue;
                        Fax = objDonVi.Fax;
                        Email = objDonVi.Email;
                        NgayPheDuyetShort = objHS.NgayTiepNhan.Value.ToShortDateString();

                    }
                    if (objDV != null)
                    {
                        TenTiengViet = objDV.TenTiengViet;
                        DiaChi = objDV.DiaChi;
                        DienThoai = objDV.DienThoai;
                        MaSoThue = objDV.MaSoThue;
                        Fax = objDV.Fax;
                        Email = objDV.Email;
                    }
                    if (nguoiky != null)
                    {
                        HoTenNguoiKy = nguoiky.FullName;
                        ChucVu = QLCL_Patch.GetChucVuKy(nguoiky.Position);
                    }
                    String TenKhoBac = "Ngân hàng Thương mại Cổ phần Á châu - Hội sở";// Sua theo yeu cau
                    DataTable returnTable = new DataTable();
                    returnTable.TableName = "LePhiTiepNhanCNHQ";
                    returnTable.Columns.Add("STT");
                    returnTable.Columns.Add("LoaiDichVu");
                    returnTable.Columns.Add("DonGia");
                    returnTable.Columns.Add("SoLuong");
                    returnTable.Columns.Add("ThanhTien");

                    DataRow row1 = returnTable.NewRow();
                    row1["STT"] = "1";
                    row1["LoaiDichVu"] = "Phí đánh giá quá trình sản xuất";
                    row1["DonGia"] = string.Format("{0:0,0}.000/ quá trình sản xuất", (int)QLCL_Patch.LePhi.PhiDanhGiaQuaTrinhSanXuat);
                    row1["SoLuong"] = SLDanhGia;
                    row1["ThanhTien"] = string.Format("{0:0,0}.000", SLDanhGia * (int)QLCL_Patch.LePhi.PhiDanhGiaQuaTrinhSanXuat);
                    returnTable.Rows.Add(row1);
                    DataRow row2 = returnTable.NewRow();
                    row2["STT"] = "2";
                    row2["LoaiDichVu"] = "Phí lấy mẫu sản phẩm";
                    row2["DonGia"] = string.Format("{0:0,0}.000/ chủng loại sản phẩm", (int)QLCL_Patch.LePhi.PhiLayMauSanPham);
                    row2["SoLuong"] = SLLayMau;
                    row2["ThanhTien"] = string.Format("{0:0,0}.000", SLLayMau * (int)QLCL_Patch.LePhi.PhiLayMauSanPham);
                    returnTable.Rows.Add(row2);

                    rd.SetDataSource(returnTable);
                    rd.Refresh();

                    rd.SetParameterValue("SoGiayThongBaoLePhi", SoGiayThongBaoLePhi);
                    rd.SetParameterValue("TinhThanhTrungTam", TinhThanhTrungTam);
                    rd.SetParameterValue("TenTrungTam", TenTrungTam);
                    rd.SetParameterValue("NgayPheDuyet", NgayPheDuyet);
                    rd.SetParameterValue("TenTiengViet", TenTiengViet);
                    rd.SetParameterValue("DiaChi", DiaChi);
                    rd.SetParameterValue("DienThoai", DienThoai);
                    rd.SetParameterValue("Fax", Fax);
                    rd.SetParameterValue("MaSoThue", MaSoThue);
                    rd.SetParameterValue("Email", Email);
                    rd.SetParameterValue("NgayPheDuyetShort", NgayPheDuyetShort);
                    rd.SetParameterValue("CongTien", string.Format("{0:0,0}.000", CongTien));
                    rd.SetParameterValue("VAT", string.Format("{0:0,0}.000", VAT));
                    rd.SetParameterValue("TongTien", string.Format("{0:0,0}.000", TongTien));
                    rd.SetParameterValue("BangChu", BangChu);
                    rd.SetParameterValue("DVThuHuongDiaChi", TrungTamObject.DiaChiCoQuanThuHuong);
                    rd.SetParameterValue("SoTaiKhoan", TrungTamObject.SoTaiKhoan);
                    rd.SetParameterValue("TenKhoBac", TenKhoBac);


                    rd.SetParameterValue("HoTenNguoiKy", HoTenNguoiKy);
                    rd.SetParameterValue("ChucVu", ChucVu);

                }
                break;
            #endregion

            #region In Lệ phí Cấp giấy Chứng nhận hợp Quy - LongHH
            case "ThongBaoNopTienCNHQ":
                //EnChucVuList ecv;
                //EnHinhThucList enht;
                //EnKetLuanList ekl;
                //EnLoaiPhiList elp;
                //EnLoaiTaiLieuList;
                //EnLoaiXuLyList;
                //EnNguonGocList;
                //EnNhanHoSoTuList;
                //EnThoiHanList;
                //EnTrangThaiThongBaoPhiList;
                //EnTrangThaiSanPhamList;
                //EnLoaiPhiList
                strduongdandenteploaiBC = Server.MapPath(@"~\Report\ThongBaoNopTienCNHQ.rpt");
                rd = new ReportDocument();
                rd.PrintOptions.PaperSize = PaperSize.PaperA4;
                rd.Load(strduongdandenteploaiBC);
                if (rd != null)
                {
                    string TrungTamID = _mUserInfo.TrungTam.Id;
                    HoSo objHS = ProviderFactory.HoSoProvider.GetById(Request["HoSoID"].ToString());
                    DmDonVi objDV = ProviderFactory.DmDonViProvider.GetById(objHS.DonViId);
                    DmTrungTam TrungTamObject = ProviderFactory.DmTrungTamProvider.GetById(TrungTamID);
                    NumberUtil objTalkNumber = new NumberUtil();
                    if (TrungTamObject == null)
                    {
                        rd = null;
                        break;
                    }
                    string TenTrungTam = !string.IsNullOrEmpty(TrungTamObject.TenTrungTam) ? TrungTamObject.TenTrungTam : "";
                    string SoGiayThongBaoLePhi = "";
                    string TinhThanhTrungTam = ProviderFactory.DmTinhThanhProvider.GetById(TrungTamObject.TinhThanhId).TenTinhThanh;
                    string NgayPheDuyet = TalkDate(objHS.NgayTiepNhan.Value);
                    string TenTiengViet = string.Empty;
                    string DiaChi = string.Empty;
                    string DienThoai = string.Empty;
                    string MaSoThue = string.Empty;
                    string Fax = string.Empty;
                    string Email = string.Empty;
                    string DanhSachGCN = string.Empty;
                    string SoPhieuTiepNhan = string.Empty;
                    string NgayPheDuyet1 = string.Empty;
                    string PhiDonGia = string.Empty;
                    string PhiSL = string.Empty;
                    string TongTien1 = string.Empty;
                    string PhiTTien = string.Empty;
                    long CongTien = 1530000;
                    long VAT = CongTien / 10;
                    long TongTien = CongTien + VAT;
                    string BangChu = objTalkNumber.Speak(TongTien);
                    string NoiDungChuyenKhoan = string.Empty;
                    string DonViThuHuong = TrungTamObject.TenCoQuanThuHuong;
                    string DiaChiDVThuHuong = TrungTamObject.DiaChiCoQuanThuHuong;
                    string SoTaiKhoan = TrungTamObject.SoTaiKhoan;
                    string TenKhoBac = TrungTamObject.TenKhoBac;
                    string ChucVu = string.Empty;
                    string HoTenNguoiKy = string.Empty;

                    if (objHS != null)
                    {
                        SoGiayThongBaoLePhi = objHS.SoHoSo;
                        DmDonVi objDonVi = new DmDonVi();
                        objDonVi = ProviderFactory.DmDonViProvider.GetById(objHS.DonViId);
                        TenTiengViet = objDonVi.TenTiengViet;
                        DiaChi = objDonVi.DiaChi;
                        DienThoai = objDonVi.DienThoai;
                        MaSoThue = objDonVi.MaSoThue;
                        Fax = objDonVi.Fax;
                        Email = objDonVi.Email;

                    }
                    TenKhoBac = "Ngân hàng Thương mại Cổ phần Á châu - Hội sở";

                    DataTable returnTable = new DataTable();
                    returnTable.TableName = "ThongBaoNopTienCNHQ";
                    returnTable.Columns.Add("GCN");
                    returnTable.Columns.Add("QuyChuanThuNhat");
                    returnTable.Columns.Add("QuyChuanThuHai");
                    returnTable.Columns.Add("DonGia1");
                    returnTable.Columns.Add("SoLuong1");
                    returnTable.Columns.Add("ThanhTien1");
                    returnTable.Columns.Add("DonGia2");
                    returnTable.Columns.Add("SoLuong2");
                    returnTable.Columns.Add("ThanhTien2");

                    DataRow row1 = returnTable.NewRow();
                    row1["GCN"] = "1";
                    row1["QuyChuanThuNhat"] = string.Empty;
                    row1["QuyChuanThuHai"] = string.Empty;
                    row1["DonGia1"] = 1;
                    row1["SoLuong1"] = 1;
                    row1["ThanhTien1"] = 1;
                    row1["DonGia2"] = 2;
                    row1["SoLuong2"] = 2;
                    row1["ThanhTien2"] = 2;
                    returnTable.Rows.Add(row1);
                    DataRow row2 = returnTable.NewRow();
                    row2["GCN"] = "1";
                    row2["QuyChuanThuNhat"] = string.Empty;
                    row2["QuyChuanThuHai"] = string.Empty;
                    row2["DonGia1"] = 1;
                    row2["SoLuong1"] = 1;
                    row2["ThanhTien1"] = 1;
                    row2["DonGia2"] = 2;
                    row2["SoLuong2"] = 2;
                    row2["ThanhTien2"] = 2;
                    returnTable.Rows.Add(row2);

                    rd.SetDataSource(returnTable);
                    rd.Refresh();

                    rd.SetParameterValue("SoGiayThongBaoLePhi", SoGiayThongBaoLePhi);
                    rd.SetParameterValue("TinhThanhTrungTam", TinhThanhTrungTam);
                    rd.SetParameterValue("TenTrungTam", TenTrungTam);
                    rd.SetParameterValue("NgayPheDuyet", NgayPheDuyet);
                    rd.SetParameterValue("TenTiengViet", TenTiengViet);
                    rd.SetParameterValue("DiaChi", DiaChi);
                    rd.SetParameterValue("DienThoai", DienThoai);
                    rd.SetParameterValue("Fax", Fax);
                    rd.SetParameterValue("MaSoThue", MaSoThue);
                    rd.SetParameterValue("Email", Email);
                    rd.SetParameterValue("SoPhieuTiepNhan", SoPhieuTiepNhan);
                    rd.SetParameterValue("DanhSachGCN", DanhSachGCN);
                    rd.SetParameterValue("NgayPheDuyet1", NgayPheDuyet1);
                    rd.SetParameterValue("PhiDonGia", PhiDonGia);
                    rd.SetParameterValue("PhiSL", PhiSL);
                    rd.SetParameterValue("PhiTTien", PhiTTien);
                    rd.SetParameterValue("TongTien1", TongTien1);
                    rd.SetParameterValue("CongTien", CongTien);

                    rd.SetParameterValue("VAT", VAT);
                    rd.SetParameterValue("TongTien", TongTien);
                    rd.SetParameterValue("BangChu", BangChu);
                    rd.SetParameterValue("DonViThuHuong", DonViThuHuong);
                    rd.SetParameterValue("DiaChiDVThuHuong", TrungTamObject.DiaChiCoQuanThuHuongCuc);
                    rd.SetParameterValue("SoTaiKhoan", SoTaiKhoan);
                    rd.SetParameterValue("TenKhoBac", TenKhoBac);

                    rd.SetParameterValue("HoTenNguoiKy", HoTenNguoiKy);
                    rd.SetParameterValue("ChucVu", ChucVu);

                }
                break;
            #endregion

            #region In phiếu đánh giá-Duchh
            //Tham số:Session["SAN_PHAM_ID"]
            case "PhieuDanhGia":
                strduongdandenteploaiBC = Server.MapPath(@"~\Report\PhieuDanhGia.rpt");
                rd = new ReportDocument();
                rd.Load(strduongdandenteploaiBC);
                if (rd != null)
                {
                    string TrungTamID = _mUserInfo.TrungTam.Id;
                    string SAN_PHAM_ID = Request["SanPhamId"] != null ? Request["SanPhamId"].ToString() : "";
                    DmTrungTam TrungTamObject = ProviderFactory.DmTrungTamProvider.GetById(TrungTamID);
                    if (TrungTamObject == null)
                    {
                        rd = null;
                        break;
                    }
                    string TEN_TRUNG_TAM_TIENG_VIET = !string.IsNullOrEmpty(TrungTamObject.TenTrungTam) ? TrungTamObject.TenTrungTam : "";
                    string TEN_SAN_PHAM = "";
                    string KY_HIEU = "";
                    string HANG_SAN_XUAT = "";
                    string MA_NHOM_SP = "";
                    string TO_CHUC_CA_NHAN = "";
                    string NGUOI_LIEN_HE = "";
                    string DIEN_THOAI = "";
                    string SO_CONG_VAN_DEN = "";
                    string TRUC_TIEP = "";
                    string BUU_DIEN = "";
                    string NK_KEM_KQ_DO_KIEM = "";
                    string NK_CHUA_DO_KIEM = "";
                    string SX_TRONG_NUOC_CO_ISO = "";
                    string SX_TRONG_NUOC_KHONG_CO_ISO = "";
                    string HOP_LE = "";
                    string KHONG_HOP_LE = "";
                    string DAY_DU = "";
                    string KHONG_DAY_DU = "";
                    string SO_NGAY = "";
                    string CO_QUAN_DO_KIEM = "";
                    string NOI_DUNG_DO_KIEM = "";
                    string NHAN_XET_KHAC = "";
                    string CAP_GCN = "";
                    string KHONG_CAP_GCN = "";
                    string KHONG_PHAI_CN = "";
                    string THOI_HAN_HAI_NAM = "";
                    string THOI_HAN_BA_NAM = "";
                    string THOI_HAN_KHAC = "";
                    string LE_PHI_CN = "0";
                    string SO_GCN_CV = "";
                    string FILE_DINH_KEM = "";
                    string TEN_THAM_DINH = "";
                    string TEN_DANH_GIA = "";
                    string strSubFix = string.Empty;
                    string LOAI_HINH_CHUNG_NHAN = string.Empty;
                    string strNhanHoSoTu = string.Empty;
                    string NguonGoc = string.Empty;
                    string SoGiayThongBaoLePhi = string.Empty;
                    string HopLe_DayDu = string.Empty;
                    string KetLuan = string.Empty;
                    string ThoiHan = string.Empty;
                    string KetQuaDoKiem = string.Empty;

                    if (!string.IsNullOrEmpty(SAN_PHAM_ID))
                    {
                        SanPham SanPham = ProviderFactory.SanPhamProvider.GetById(SAN_PHAM_ID);
                        HoSo objHoSo = ProviderFactory.HoSoProvider.GetById(SanPham.HoSoId);

                        if (SanPham.NgayKyDuyet != null)
                        {
                            strSubFix = TalkDate(SanPham.NgayKyDuyet.Value);
                        }
                        else
                        {
                            strSubFix = TalkDate(DateTime.Now);
                        }
                        if (objHoSo.LoaiHoSo == (int)LoaiHoSo.ChungNhanHopQuy)
                            LOAI_HINH_CHUNG_NHAN = "CHỨNG NHẬN HỢP QUY";
                        else
                            LOAI_HINH_CHUNG_NHAN = "CHỨNG NHẬN HỢP CHUẨN";
                        if (SanPham != null)
                        {
                            KY_HIEU = !string.IsNullOrEmpty(SanPham.KyHieu) ? SanPham.KyHieu : "";
                            DmNhomSanPham NhomSanPham = ProviderFactory.DmNhomSanPhamProvider.GetById(SanPham.NhomSanPhamId);
                            if (NhomSanPham != null)
                                MA_NHOM_SP = !string.IsNullOrEmpty(NhomSanPham.MaNhom) ? NhomSanPham.MaNhom : "";
                            DmSanPham SanPhamObject = ProviderFactory.DmSanPhamProvider.GetById(SanPham.SanPhamId);
                            if (SanPhamObject != null)
                            {
                                TEN_SAN_PHAM = !string.IsNullOrEmpty(SanPhamObject.TenTiengViet) ? SanPhamObject.TenTiengViet : "";
                            }
                            DmHangSanXuat SanXuatObject = ProviderFactory.DmHangSanXuatProvider.GetById(SanPham.HangSanXuatId);
                            if (SanXuatObject != null)
                            {
                                HANG_SAN_XUAT = !string.IsNullOrEmpty(SanXuatObject.TenHangSanXuat) ? SanXuatObject.TenHangSanXuat : "";
                            }
                            HoSo hoso = ProviderFactory.HoSoProvider.GetById(SanPham.HoSoId);
                            if (hoso != null)
                            {
                                DmDonVi DonViObject = ProviderFactory.DmDonViProvider.GetById(hoso.DonViId);
                                if (DonViObject != null)
                                {
                                    TO_CHUC_CA_NHAN = !string.IsNullOrEmpty(DonViObject.TenTiengViet) ? DonViObject.TenTiengViet : "";
                                }
                                NGUOI_LIEN_HE = !string.IsNullOrEmpty(hoso.NguoiNopHoSo) ? hoso.NguoiNopHoSo : "";
                                DIEN_THOAI = !string.IsNullOrEmpty(hoso.DienThoai) ? hoso.DienThoai : "";
                                string NgayThang = hoso.NgayTiepNhan != null ? "(" + string.Format("{0:dd/MM/yyyy}", hoso.NgayTiepNhan) + ")" : "";
                                SO_CONG_VAN_DEN = !string.IsNullOrEmpty(hoso.SoCongVanDen) ? hoso.SoCongVanDen + " " + NgayThang : "";
                                if (hoso.NhanHoSoTuId != null)
                                {
                                    int NhanHoSoTuID = hoso.NhanHoSoTuId.Value;
                                    if (NhanHoSoTuID == Convert.ToInt32(EnNhanHoSoTuList.BUU_DIEN))
                                    {
                                        BUU_DIEN = "x";
                                        strNhanHoSoTu = "Qua đường bưu điện";
                                    }
                                    else if (NhanHoSoTuID == Convert.ToInt32(EnNhanHoSoTuList.TRUC_TIEP))
                                    {
                                        TRUC_TIEP = "x";
                                        strNhanHoSoTu = "Trực tiếp";
                                    }
                                    else if (NhanHoSoTuID == Convert.ToInt32(EnNhanHoSoTuList.QUA_MANG))
                                    {
                                        strNhanHoSoTu = "Qua mạng Internet";
                                    }
                                }
                                //if (hoso.NguonGocId != null && hoso.NguonGocId == Convert.ToInt32(EnNguonGocList.NK_PHI_MAU_DICH))
                                //{
                                //    //lấy lệ phí theo giá trị lô hàng                                    
                                //    DmLePhi objDmLePhi = ProviderFactory.DmLePhiProvider.GetById(SanPham.LePhiId);
                                //    if (objDmLePhi != null)
                                //    {
                                //        LE_PHI_CN = objDmLePhi.LePhi.ToString();
                                //    }

                                //}
                                //else
                                //{
                                //lấy giá trị theo nhóm sản phẩm
                                if (NhomSanPham != null)
                                {
                                    LE_PHI_CN = string.Format("{0:0,0}.000", Convert.ToInt32(NhomSanPham.MucLePhi.ToString()));
                                }
                                //}
                                TList<PhanCong> phancongList = ProviderFactory.PhanCongProvider.GetByHoSoId(hoso.Id);
                                if (phancongList != null && phancongList.Count > 0)
                                {
                                    SysUser NguoiThamDinh = ProviderFactory.SysUserProvider.GetById(phancongList[0].NguoiThamDinh);
                                    if (NguoiThamDinh != null)
                                    {
                                        TEN_THAM_DINH = !string.IsNullOrEmpty(NguoiThamDinh.FullName) ? NguoiThamDinh.FullName : "";
                                    }
                                    SysUser NguoiDanhGia = ProviderFactory.SysUserProvider.GetById(phancongList[0].NguoiXuLy);
                                    if (NguoiDanhGia != null)
                                    {
                                        TEN_DANH_GIA = !string.IsNullOrEmpty(NguoiDanhGia.FullName) ? NguoiDanhGia.FullName : "";
                                    }
                                }

                            }
                            if (hoso.NguonGocId != null)
                            {
                                int NguonGocID = hoso.NguonGocId.Value;

                                if (NguonGocID == Convert.ToInt32(EnNguonGocList.NK_CHUA_DO_KIEM))
                                {
                                    NK_CHUA_DO_KIEM = "x";
                                    NguonGoc = EntityHelper.GetEnumTextValue(EnNguonGocList.NK_CHUA_DO_KIEM);
                                }
                                else if (NguonGocID == Convert.ToInt32(EnNguonGocList.NK_KEM_KQ_DO_KIEM))
                                {
                                    NK_KEM_KQ_DO_KIEM = "x";
                                    NguonGoc = EntityHelper.GetEnumTextValue(EnNguonGocList.NK_KEM_KQ_DO_KIEM);
                                }
                                else if (NguonGocID == Convert.ToInt32(EnNguonGocList.SX_TRONG_NUOC_CO_ISO))
                                {
                                    SX_TRONG_NUOC_CO_ISO = "x";
                                    NguonGoc = EntityHelper.GetEnumTextValue(EnNguonGocList.SX_TRONG_NUOC_CO_ISO);
                                }
                                else
                                {
                                    SX_TRONG_NUOC_KHONG_CO_ISO = "x";
                                    NguonGoc = EntityHelper.GetEnumTextValue(EnNguonGocList.SX_TRONG_NUOC_KHONG_CO_ISO);
                                }
                            }

                            if (SanPham.HopLe != null)
                            {
                                bool HopLe = SanPham.HopLe.Value;
                                if (HopLe)
                                {
                                    HOP_LE = "x";
                                    HopLe_DayDu = "- Hợp lệ";
                                }
                                else
                                {
                                    KHONG_HOP_LE = "x";
                                    HopLe_DayDu = "- Không hợp lệ";
                                }
                            }
                            if (SanPham.DayDu != null)
                            {
                                bool DayDu = SanPham.DayDu.Value;
                                if (DayDu)
                                {
                                    DAY_DU = "x";
                                    HopLe_DayDu += ", đầy đủ";
                                }
                                else
                                {
                                    KHONG_DAY_DU = "x";
                                    HopLe_DayDu += ", không đầy đủ";
                                }
                            }
                            if (!string.IsNullOrEmpty(SanPham.SoDoKiem))
                                SO_NGAY = SanPham.SoDoKiem;
                            if (SanPham.NgayDoKiem != null)
                            {
                                string NgayDoKiem = string.Format("{0:dd/MM/yyyy}", SanPham.NgayDoKiem);
                                SO_NGAY = SO_NGAY + ", " + NgayDoKiem;
                            }
                            DmCoQuanDoKiem CoQuanDoKiem = ProviderFactory.DmCoQuanDoKiemProvider.GetById(SanPham.CoQuanDoKiemId);
                            if (CoQuanDoKiem != null)
                                CO_QUAN_DO_KIEM = !string.IsNullOrEmpty(CoQuanDoKiem.TenCoQuanDoKiem) ? CoQuanDoKiem.TenCoQuanDoKiem : "";

                            string strNoiDungDoKiemTamThoi = string.Empty;
                            TList<SanPhamTieuChuanApDung> lstSanPhamTieuChuanApDung = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(SanPham.Id);
                            foreach (SanPhamTieuChuanApDung obj in lstSanPhamTieuChuanApDung)
                            {
                                DmTieuChuan objDmTieuChuan = new DmTieuChuan();
                                objDmTieuChuan = ProviderFactory.DmTieuChuanProvider.GetById(obj.TieuChuanApDungId);
                                if (objDmTieuChuan != null)
                                    strNoiDungDoKiemTamThoi += objDmTieuChuan.MaTieuChuan + "; ";
                            }
                            if (strNoiDungDoKiemTamThoi.Length > 2)
                                strNoiDungDoKiemTamThoi = strNoiDungDoKiemTamThoi.Substring(0, strNoiDungDoKiemTamThoi.Length - 2);
                            NOI_DUNG_DO_KIEM = strNoiDungDoKiemTamThoi.Trim();

                            if (!string.IsNullOrEmpty(SanPham.SoDoKiem))
                                KetQuaDoKiem += "- Số " + SanPham.SoDoKiem + " ";
                            if (SanPham.NgayDoKiem != null)
                            {
                                KetQuaDoKiem += "ngày " + SanPham.NgayDoKiem.Value.ToShortDateString() + " ";
                            }
                            if (!string.IsNullOrEmpty(CO_QUAN_DO_KIEM))
                                if (KetQuaDoKiem.Length > 0)
                                    KetQuaDoKiem += "của " + CO_QUAN_DO_KIEM;
                                else
                                    KetQuaDoKiem += CO_QUAN_DO_KIEM;

                            if (SanPham.NoiDungDoKiem != null)
                            {
                                string[] array = SanPham.NoiDungDoKiem.Split('\n');

                                if (KetQuaDoKiem.Length > 0)
                                    KetQuaDoKiem += "; " + array[0];
                                else
                                    KetQuaDoKiem += array[0];
                            }

                            NHAN_XET_KHAC = !string.IsNullOrEmpty(SanPham.NhanXetKhac) ? SanPham.NhanXetKhac : "";
                            if (SanPham.KetLuanId != null)
                            {
                                int KetLuanID = SanPham.KetLuanId.Value;
                                if (KetLuanID == Convert.ToInt32(EnKetLuanList.CAP_GCN))
                                {
                                    CAP_GCN = "x";
                                    KetLuan = "Cấp Giấy chứng nhận";
                                }
                                else if (KetLuanID == Convert.ToInt32(EnKetLuanList.KHONG_CAP_GCN))
                                {
                                    KHONG_CAP_GCN = "x";
                                    LE_PHI_CN = "0";
                                    KetLuan = "Không cấp Giấy chứng nhận";
                                }
                                else if (KetLuanID == Convert.ToInt32(EnKetLuanList.KHONG_PHAI_CN))
                                {
                                    KHONG_PHAI_CN = "x";
                                    LE_PHI_CN = "0";
                                    KetLuan = "Không phải chứng nhận";
                                }
                                else if (KetLuanID == Convert.ToInt32(EnKetLuanList.HUY))
                                {
                                    LE_PHI_CN = "0";
                                    KetLuan = "Huỷ sản phẩm";
                                }
                            }
                            if (SanPham.ThoiHanId != null)
                            {
                                int ThoiHanID = SanPham.ThoiHanId.Value;
                                if (ThoiHanID == Convert.ToInt32(EnThoiHanList.HAI_NAM))
                                {
                                    THOI_HAN_HAI_NAM = "x";
                                    ThoiHan = "Hai năm";
                                }
                                else if (ThoiHanID == Convert.ToInt32(EnThoiHanList.BA_NAM))
                                {
                                    THOI_HAN_BA_NAM = "x";
                                    ThoiHan = "Ba năm";
                                }
                                else if (ThoiHanID == Convert.ToInt32(EnThoiHanList.MOT_LAN_SU_DUNG))
                                {
                                    THOI_HAN_KHAC = "x";
                                    ThoiHan = "Sử dụng một lần";
                                }
                            }

                            if (hoso.LoaiHoSo == (int)LoaiHoSo.ChungNhanHopChuan && (hoso.NguonGocId == (int)EnNguonGocList.NK_CHUA_DO_KIEM || hoso.NguonGocId == (int)EnNguonGocList.NK_KEM_KQ_DO_KIEM))
                            {
                                ThoiHan = "Sử dụng một lần";
                            }
                            SO_GCN_CV = !string.IsNullOrEmpty(SanPham.SoGcn) ? SanPham.SoGcn : "";
                            if (SanPham.HinhThucId != null)
                            {
                                int HinhThucID = SanPham.HinhThucId.Value;
                                if (HinhThucID == Convert.ToInt32(EnHinhThucList.TU_DANH_GIA))
                                {
                                    //Microsoft.Office.Interop.Word.Application ObjWord = new Microsoft.Office.Interop.Word.Application();
                                    //object falseValue = false;
                                    //object trueValue = true;
                                    //object missing = Type.Missing;
                                    //object saveobjections = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
                                    //object filepath = @"G:\hoang duc\Dot net\source\QuanLyChatLuong\Source\Document\Bieu mau\Mau GCN (mat sau).doc";
                                    //Microsoft.Office.Interop.Word.Document wrdoc;
                                    //wrdoc = ObjWord.Documents.Open(ref filepath, ref missing, ref trueValue, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                                    //Microsoft.Office.Interop.Word.Document wrdoc1 = ObjWord.ActiveDocument;
                                    //string m_Content = wrdoc1.Content.Text;
                                    //FILE_DINH_KEM = m_Content;
                                }
                            }

                            // Lấy số của giấy thông báo lệ phí 
                            TList<ThongBaoLePhiSanPham> lstTBLPSP = ProviderFactory.ThongBaoLePhiSanPhamProvider.GetBySanPhamId(SanPham.Id);
                            if (lstTBLPSP != null)
                            {
                                lstTBLPSP.Sort(" Stt ASC ");
                                if (lstTBLPSP.Count > 0)
                                {
                                    ThongBaoLePhiSanPham objTBLPSP = lstTBLPSP[0];
                                    if (objTBLPSP != null)
                                    {
                                        ThongBaoLePhi objTBLP = ProviderFactory.ThongBaoLePhiProvider.GetById(objTBLPSP.ThongBaoLePhiId);
                                        if (objTBLP != null)
                                            SoGiayThongBaoLePhi = objTBLP.SoGiayThongBaoLePhi;
                                    }
                                }
                            }

                            //Lấy datasource cho subreport
                            DataTable dtSubPhieuDanhGia = ProviderFactory.SanPhamProvider.GetDataForSubReportCN(SanPham.Id);
                            rd.Subreports[0].SetDataSource(dtSubPhieuDanhGia);

                        }
                    }
                    string NOI_CAP = "";

                    //Trung tam chung nhan
                    if (TrungTamObject.MaTrungTam.ToUpper() == "TTCN")
                    {
                        if (!string.IsNullOrEmpty(strSubFix))
                            NOI_CAP = "Hà Nội," + strSubFix;
                    }
                    //Trung tam 2
                    else if (TrungTamObject.MaTrungTam.ToUpper() == "TT2")
                    {
                        if (!string.IsNullOrEmpty(strSubFix))
                            NOI_CAP = "Tp. Hồ Chí Minh," + strSubFix;
                    }
                    //Trung tam 3
                    else if (TrungTamObject.MaTrungTam.ToUpper() == "TT3")
                    {
                        if (!string.IsNullOrEmpty(strSubFix))
                            NOI_CAP = "Đà Nẵng," + strSubFix;
                    }
                    rd.ParameterFields["LOAI_HINH_CHUNG_NHAN"].CurrentValues.AddValue(LOAI_HINH_CHUNG_NHAN);
                    rd.ParameterFields["TEN_THAM_DINH"].CurrentValues.AddValue(TEN_THAM_DINH.ToUpper());
                    rd.ParameterFields["TEN_DANH_GIA"].CurrentValues.AddValue(TEN_DANH_GIA.ToUpper());
                    rd.ParameterFields["NOI_CAP"].CurrentValues.AddValue(NOI_CAP);
                    rd.ParameterFields["FILE_DINH_KEM"].CurrentValues.AddValue(FILE_DINH_KEM);
                    rd.ParameterFields["TEN_TRUNG_TAM"].CurrentValues.AddValue(TEN_TRUNG_TAM_TIENG_VIET);
                    rd.ParameterFields["NGUOI_LIEN_HE"].CurrentValues.AddValue(NGUOI_LIEN_HE);
                    rd.ParameterFields["DIEN_THOAI"].CurrentValues.AddValue(DIEN_THOAI);
                    rd.ParameterFields["TO_CHUC_CA_NHAN"].CurrentValues.AddValue(TO_CHUC_CA_NHAN);
                    rd.ParameterFields["SO_CONG_VAN_DEN"].CurrentValues.AddValue(SO_CONG_VAN_DEN);
                    rd.ParameterFields["TRUC_TIEP"].CurrentValues.AddValue(TRUC_TIEP);
                    rd.ParameterFields["BUU_DIEN"].CurrentValues.AddValue(BUU_DIEN);
                    rd.ParameterFields["TEN_SAN_PHAM"].CurrentValues.AddValue(TEN_SAN_PHAM);
                    rd.ParameterFields["KY_HIEU"].CurrentValues.AddValue(KY_HIEU);
                    rd.ParameterFields["HANG_SAN_XUAT"].CurrentValues.AddValue(HANG_SAN_XUAT);
                    rd.ParameterFields["MA_NHOM_SP"].CurrentValues.AddValue(MA_NHOM_SP);
                    rd.ParameterFields["NK_KEM_KQ_DO_KIEM"].CurrentValues.AddValue(NK_KEM_KQ_DO_KIEM);
                    rd.ParameterFields["NK_CHUA_DO_KIEM"].CurrentValues.AddValue(NK_CHUA_DO_KIEM);
                    rd.ParameterFields["SX_TRONG_NUOC_CO_ISO"].CurrentValues.AddValue(SX_TRONG_NUOC_CO_ISO);
                    rd.ParameterFields["SX_TRONG_NUOC_KHONG_CO_ISO"].CurrentValues.AddValue(SX_TRONG_NUOC_KHONG_CO_ISO);
                    rd.ParameterFields["HOP_LE"].CurrentValues.AddValue(HOP_LE);
                    rd.ParameterFields["KHONG_HOP_LE"].CurrentValues.AddValue(KHONG_HOP_LE);
                    rd.ParameterFields["DAY_DU"].CurrentValues.AddValue(DAY_DU);
                    rd.ParameterFields["KHONG_DAY_DU"].CurrentValues.AddValue(KHONG_DAY_DU);
                    rd.ParameterFields["SO_NGAY"].CurrentValues.AddValue(SO_NGAY);
                    rd.ParameterFields["CO_QUAN_DO_KIEM"].CurrentValues.AddValue(CO_QUAN_DO_KIEM);
                    rd.ParameterFields["NOI_DUNG_DO_KIEM"].CurrentValues.AddValue(NOI_DUNG_DO_KIEM);
                    rd.ParameterFields["NHAN_XET_KHAC"].CurrentValues.AddValue(NHAN_XET_KHAC);

                    rd.ParameterFields["CAP_GCN"].CurrentValues.AddValue(CAP_GCN);
                    rd.ParameterFields["KHONG_CAP_GCN"].CurrentValues.AddValue(KHONG_CAP_GCN);
                    rd.ParameterFields["KHONG_PHAI_CN"].CurrentValues.AddValue(KHONG_PHAI_CN);
                    rd.ParameterFields["THOI_HAN_HAI_NAM"].CurrentValues.AddValue(THOI_HAN_HAI_NAM);
                    rd.ParameterFields["THOI_HAN_BA_NAM"].CurrentValues.AddValue(THOI_HAN_BA_NAM);
                    rd.ParameterFields["THOI_HAN_KHAC"].CurrentValues.AddValue(THOI_HAN_KHAC);
                    rd.ParameterFields["LE_PHI_CN"].CurrentValues.AddValue(LE_PHI_CN + " VNĐ");
                    rd.ParameterFields["SO_GCN_CV"].CurrentValues.AddValue(SO_GCN_CV);
                    rd.ParameterFields["NhanHoSoTu"].CurrentValues.AddValue(strNhanHoSoTu);
                    rd.ParameterFields["NguonGoc"].CurrentValues.AddValue(NguonGoc);
                    rd.ParameterFields["SoGiayThongBaoLePhi"].CurrentValues.AddValue(SoGiayThongBaoLePhi);
                    rd.ParameterFields["HopLe_DayDu"].CurrentValues.AddValue(HopLe_DayDu);
                    rd.ParameterFields["KetLuan"].CurrentValues.AddValue(KetLuan);
                    rd.ParameterFields["ThoiHan"].CurrentValues.AddValue(ThoiHan);
                    rd.ParameterFields["KetQuaDoKiem"].CurrentValues.AddValue(KetQuaDoKiem);
                }
                break;
            #endregion

            #region In bản tiếp nhận-Duchh
            //Tham số:Session["HO_SO_ID"];
            case "BanTiepNhan_OLD":
                strduongdandenteploaiBC = Server.MapPath(@"~\Report\BanTiepNhan.rpt");
                rd = new ReportDocument();
                rd.Load(strduongdandenteploaiBC);
                if (rd != null)
                {
                    string HoSoID = string.Empty;
                    string IdSanPham = Request["SanPhamId"] != null ? Request["SanPhamId"].ToString() : string.Empty;
                    HoSoID = Request["HoSoId"] != null ? Request["HoSoId"].ToString() : HoSoID;
                    string SO = "";
                    string SO_BAN_CONG_BO = string.Empty;
                    string NGAY_CONG_BO = string.Empty;
                    string TEN_DON_VI = "";
                    string DIA_CHI = "";
                    string NOI_CAP = "";
                    string NGUOI_CAP = "";
                    string HIEN_THI_SUB = "0";
                    string SO_BTN = string.Empty;
                    string TEN_SAN_PHAM = string.Empty;
                    string KY_HIEU = string.Empty;
                    string HANG_SAN_XUAT = string.Empty;
                    string TIEU_CHUAN = string.Empty;
                    string TenTinhThanh = string.Empty;
                    string subfix = string.Empty;
                    DataTable dtDuLieu = new DataTable();
                    dtDuLieu.Columns.Add(new DataColumn("SanPham"));
                    dtDuLieu.Columns.Add(new DataColumn("TieuChuan"));
                    DataTable dtSubDataSourceCB = new DataTable();
                    dtSubDataSourceCB.Columns.Add(new DataColumn("SanPham"));
                    dtSubDataSourceCB.Columns.Add(new DataColumn("KyHieu"));
                    dtSubDataSourceCB.Columns.Add(new DataColumn("HangSanXuat"));
                    dtSubDataSourceCB.Columns.Add(new DataColumn("TieuChuan"));
                    HoSo hoso = ProviderFactory.HoSoProvider.GetById(HoSoID);
                    SanPham sanpham = ProviderFactory.SanPhamProvider.GetById(IdSanPham);

                    string TrungTamID = hoso.TrungTamId;
                    DmTrungTam TrungTamObject = ProviderFactory.DmTrungTamProvider.GetById(TrungTamID);
                    if (TrungTamObject == null)
                    {
                        rd = null;
                        break;
                    }

                    if (hoso != null && sanpham != null)
                    {
                        SO_BAN_CONG_BO = sanpham.SoBanCongBo;
                        NGAY_CONG_BO = sanpham.NgayCongBo != null ? TalkDate(sanpham.NgayCongBo.Value) : string.Empty;
                        // Lay tong tin chung
                        if (sanpham.KetLuanId != null && Convert.ToInt32(sanpham.KetLuanId) == (int)EnKetLuanList.CAP_BAN_TIEP_NHAN_CB)
                        {
                            if (sanpham.SoBanTiepNhanCb != null)
                                SO_BTN = sanpham.SoBanTiepNhanCb;
                        }
                        subfix = TalkDate(sanpham.NgayKyDuyet.Value);
                        SysUser NguoiKy = ProviderFactory.SysUserProvider.GetById(sanpham.NguoiKyDuyetId);
                        //Trung tam chung nhan
                        if (TrungTamObject.MaTrungTam.ToUpper() == "TTCN")
                        {
                            NOI_CAP = "Hà Nội," + subfix;
                            NGUOI_CAP = "TUQ. CỤC TRƯỞNG" + "\n" + "GIÁM ĐỐC" + "\n" + "TRUNG TÂM CHỨNG NHẬN"
                                        + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKy.FullName;
                        }
                        //Trung tam 2
                        else if (TrungTamObject.MaTrungTam.ToUpper() == "TT2")
                        {
                            NOI_CAP = "Tp. Hồ Chí Minh," + subfix;
                            NGUOI_CAP = "TUQ. CỤC TRƯỞNG" + "\n" + "GIÁM ĐỐC" + "\n" + "TRUNG TÂM KIỂM ĐỊNH VÀ CHỨNG NHẬN 2"
                                        + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKy.FullName;
                        }
                        //Trung tam 3
                        else if (TrungTamObject.MaTrungTam.ToUpper() == "TT3")
                        {
                            NOI_CAP = "Đà Nẵng," + subfix;
                            NGUOI_CAP = "TUQ. CỤC TRƯỞNG" + "\n" + "GIÁM ĐỐC" + "\n" + "TRUNG TÂM KIỂM ĐỊNH VÀ CHỨNG NHẬN 3"
                                         + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKy.FullName;
                        }

                        // Nếu người ký là cục trưởng hoặc cục phó
                        if (NguoiKy != null)
                        {
                            if (NguoiKy.Position == "6")
                            {
                                NGUOI_CAP = "CỤC TRƯỞNG" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKy.FullName;
                            }
                            else if (NguoiKy.Position == "7")
                            {
                                NGUOI_CAP = "KT. CỤC TRƯỞNG" + "\n" + "PHÓ CỤC TRƯỞNG" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKy.FullName;
                            }
                        }
                        // Neu ho so chua dong thi hien thi thong tin theo tham chieu trong danh muc, nguoc lai thi hien thi thong tin da fix
                        if (hoso.TrangThaiId == (int)EnTrangThaiHoSoList.DA_DONG || hoso.TrangThaiId == (int)EnTrangThaiHoSoList.CHO_LUU_TRU)
                        {

                            TEN_DON_VI = !string.IsNullOrEmpty(hoso.DmDonViTenTiengViet) ? hoso.DmDonViTenTiengViet : "";
                            DIA_CHI = !string.IsNullOrEmpty(hoso.DmDonViDiaChi) ? hoso.DmDonViDiaChi : "";
                            DmTinhThanh objTinhThanh = ProviderFactory.DmTinhThanhProvider.GetById(hoso.DmDonViTinhThanhId);
                            if (objTinhThanh != null)
                                TenTinhThanh = objTinhThanh.TenTinhThanh;
                            TEN_SAN_PHAM = sanpham.DmSanPhamTenTiengViet;
                            if (!string.IsNullOrEmpty(sanpham.KyHieu))
                                KY_HIEU = sanpham.KyHieu;
                            HANG_SAN_XUAT = sanpham.DmHsxTenHangSanXuat;

                            TList<SanPhamTieuChuanApDung> listSPTieuChuan = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(sanpham.Id);
                            if (listSPTieuChuan != null && listSPTieuChuan.Count > 0)
                            {
                                foreach (SanPhamTieuChuanApDung SPTieuChuan in listSPTieuChuan)
                                {
                                    TIEU_CHUAN += SPTieuChuan.DmTieuChuanMaTieuChuan.ToUpper() + " - " + SPTieuChuan.DmTieuChuanTenTieuChuan + "; ";
                                }
                                if (TIEU_CHUAN.Length > 3)
                                    TIEU_CHUAN = TIEU_CHUAN.Substring(0, TIEU_CHUAN.Length - 2);
                            }
                        }
                        else
                        {
                            string DonViID = hoso.DonViId;
                            DmDonVi donvi = ProviderFactory.DmDonViProvider.GetById(DonViID);
                            if (donvi != null)
                            {
                                TEN_DON_VI = donvi.TenTiengViet;
                                DIA_CHI = donvi.DiaChi;
                                DmTinhThanh objTinhThanh = ProviderFactory.DmTinhThanhProvider.GetById(donvi.TinhThanhId);
                                if (objTinhThanh != null)
                                    TenTinhThanh = objTinhThanh.TenTinhThanh;
                            }

                            DmSanPham dmSanPham = ProviderFactory.DmSanPhamProvider.GetById(sanpham.SanPhamId);
                            if (dmSanPham != null)
                            {
                                TEN_SAN_PHAM = dmSanPham.TenTiengViet;

                                if (!string.IsNullOrEmpty(sanpham.KyHieu))
                                {
                                    KY_HIEU = sanpham.KyHieu;
                                }
                                DmHangSanXuat dmHangSanXuat = ProviderFactory.DmHangSanXuatProvider.GetById(sanpham.HangSanXuatId);
                                if (dmHangSanXuat != null)
                                {
                                    HANG_SAN_XUAT = dmHangSanXuat.TenHangSanXuat;
                                }
                                TList<SanPhamTieuChuanApDung> listSPTieuChuan = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(sanpham.Id);
                                if (listSPTieuChuan != null && listSPTieuChuan.Count > 0)
                                {
                                    foreach (SanPhamTieuChuanApDung SPTieuChuan in listSPTieuChuan)
                                    {
                                        DmTieuChuan dmTieuChuan = ProviderFactory.DmTieuChuanProvider.GetById(SPTieuChuan.TieuChuanApDungId);
                                        if (dmTieuChuan != null)
                                        {
                                            TIEU_CHUAN += dmTieuChuan.MaTieuChuan.ToUpper() + " - " + dmTieuChuan.TenTieuChuan + "; ";
                                        }
                                    }
                                    if (TIEU_CHUAN.Length > 3)
                                        TIEU_CHUAN = TIEU_CHUAN.Substring(0, TIEU_CHUAN.Length - 2);
                                }
                            }

                        }
                    }
                    rd.SetDataSource(dtDuLieu);
                    rd.ParameterFields["SO"].CurrentValues.AddValue(SO);
                    rd.ParameterFields["HIEN_THI_SUB"].CurrentValues.AddValue(HIEN_THI_SUB);
                    rd.ParameterFields["TEN_DON_VI"].CurrentValues.AddValue(TEN_DON_VI.ToUpper());
                    rd.ParameterFields["DIA_CHI"].CurrentValues.AddValue(DIA_CHI);
                    rd.ParameterFields["NOI_CAP"].CurrentValues.AddValue(NOI_CAP);
                    rd.ParameterFields["NGUOI_CAP"].CurrentValues.AddValue(NGUOI_CAP.ToUpper());
                    rd.ParameterFields["SO_BTN"].CurrentValues.AddValue(SO_BTN);
                    rd.ParameterFields["KY_HIEU"].CurrentValues.AddValue(KY_HIEU);
                    rd.ParameterFields["HANG_SAN_XUAT"].CurrentValues.AddValue(HANG_SAN_XUAT.ToUpper());
                    rd.ParameterFields["TIEU_CHUAN"].CurrentValues.AddValue(TIEU_CHUAN);
                    rd.ParameterFields["TEN_SAN_PHAM"].CurrentValues.AddValue(TEN_SAN_PHAM.ToUpper());
                    rd.ParameterFields["SO_BAN_CONG_BO"].CurrentValues.AddValue(SO_BAN_CONG_BO);
                    rd.ParameterFields["NGAY_CONG_BO"].CurrentValues.AddValue(NGAY_CONG_BO);
                    rd.ParameterFields["TenTinhThanh"].CurrentValues.AddValue(TenTinhThanh);
                    rd.ParameterFields["TenDonVi_InThuong"].CurrentValues.AddValue(TEN_DON_VI);
                }
                break;
            #endregion

            #region In bản tiếp nhận theo mau moi 2019-LongHH
            //Tham số:Session["HO_SO_ID"];
            case "BanTiepNhan":
                string HoSoID1 = string.Empty;
                string IdSanPham1 = Request["SanPhamId"] != null ? Request["SanPhamId"].ToString() : string.Empty;
                HoSoID1 = Request["HoSoId"] != null ? Request["HoSoId"].ToString() : HoSoID1;
                HoSo hoso1 = ProviderFactory.HoSoProvider.GetById(HoSoID1);
                SanPham sanpham1 = ProviderFactory.SanPhamProvider.GetById(IdSanPham1);
                if (hoso1 != null && sanpham1 != null && sanpham1.NgayKyDuyet.HasValue && sanpham1.NgayKyDuyet.Value.Year == 2019)
                {
                    strduongdandenteploaiBC = Server.MapPath(@"~\Report\ThongBaoTiepNhanCBHQ.rpt");
                    rd = new ReportDocument();
                    rd.Load(strduongdandenteploaiBC);
                    rd.PrintOptions.PaperSize = PaperSize.PaperA4;
                    if (rd != null)
                    {
                        string HoSoID = string.Empty;
                        string IdSanPham = Request["SanPhamId"] != null ? Request["SanPhamId"].ToString() : string.Empty;
                        HoSoID = Request["HoSoId"] != null ? Request["HoSoId"].ToString() : HoSoID;
                        string SO = "";
                        string SO_BAN_CONG_BO = string.Empty;
                        string NGAY_CONG_BO = string.Empty;
                        string TEN_DON_VI = "";
                        string DIA_CHI = "";
                        string NOI_CAP = "";
                        string NGUOI_CAP = "";
                        string HIEN_THI_SUB = "0";
                        string SO_BTN = string.Empty;
                        string TEN_SAN_PHAM = string.Empty;
                        string KY_HIEU = string.Empty;
                        //string NoiSanXuat = string.Empty;
                        string HANG_SAN_XUAT = string.Empty;
                        string TIEU_CHUAN = string.Empty;
                        string TenTinhThanh = string.Empty;
                        string subfix = string.Empty;
                        string ngayHetHieuLuc = string.Empty;
                        DataTable dtDuLieu = new DataTable();
                        dtDuLieu.Columns.Add(new DataColumn("SanPham"));
                        dtDuLieu.Columns.Add(new DataColumn("TieuChuan"));
                        DataTable dtSubDataSourceCB = new DataTable();
                        dtSubDataSourceCB.Columns.Add(new DataColumn("SanPham"));
                        dtSubDataSourceCB.Columns.Add(new DataColumn("KyHieu"));
                        dtSubDataSourceCB.Columns.Add(new DataColumn("HangSanXuat"));
                        dtSubDataSourceCB.Columns.Add(new DataColumn("TieuChuan"));
                        HoSo hoso = ProviderFactory.HoSoProvider.GetById(HoSoID);
                        SanPham sanpham = ProviderFactory.SanPhamProvider.GetById(IdSanPham);

                        string TrungTamID = hoso.TrungTamId;
                        DmTrungTam TrungTamObject = ProviderFactory.DmTrungTamProvider.GetById(TrungTamID);
                        if (TrungTamObject == null)
                        {
                            rd = null;
                            break;
                        }

                        if (hoso != null && sanpham != null)
                        {
                            if (sanpham.HinhThucId == (int)EnHinhThucList.TU_DANH_GIA)
                            {
                                ngayHetHieuLuc = sanpham.NgayKyDuyet != null ? TalkDate(sanpham.NgayKyDuyet.Value.AddYears(3)) : string.Empty;
                            }
                            else
                            {
                                string stdanhgia = sanpham.SoGcn;
                                if (stdanhgia.Length > 2)
                                {
                                    int year = 19;
                                    int month = 1;
                                    int day = 1;
                                    int.TryParse(stdanhgia.Substring(9, 2), out year);
                                    int.TryParse(stdanhgia.Substring(7, 2), out month);
                                    int.TryParse(stdanhgia.Substring(5, 2), out day);
                                    DateTime d = new DateTime(2000 + year + (stdanhgia.Substring(stdanhgia.Length - 2, 2).ToUpper() == "A2" ? 2 : 3), month, day);
                                    ngayHetHieuLuc = TalkDate(d);
                                }
                                else
                                {
                                    ngayHetHieuLuc = sanpham.NgayCongBo != null ? TalkDate(sanpham.NgayCongBo.Value.AddYears(3)) : string.Empty;
                                }
                            }
                            SO_BAN_CONG_BO = sanpham.SoBanCongBo;
                            NGAY_CONG_BO = sanpham.NgayCongBo != null ? TalkDate(sanpham.NgayCongBo.Value) : string.Empty;
                            // Lay tong tin chung
                            if (sanpham.KetLuanId != null && Convert.ToInt32(sanpham.KetLuanId) == (int)EnKetLuanList.CAP_BAN_TIEP_NHAN_CB)
                            {
                                if (sanpham.SoBanTiepNhanCb != null)
                                    SO_BTN = sanpham.SoBanTiepNhanCb;
                            }
                            subfix = TalkDate(sanpham.NgayKyDuyet.Value);
                            SysUser NguoiKy = ProviderFactory.SysUserProvider.GetById(sanpham.NguoiKyDuyetId);
                            //Trung tam chung nhan
                            if (TrungTamObject.MaTrungTam.ToUpper() == "TTCN")
                            {
                                NOI_CAP = "Hà Nội," + subfix;
                                NGUOI_CAP = "TUQ. CỤC TRƯỞNG" + "\n" + "GIÁM ĐỐC" + "\n" + "TRUNG TÂM CHỨNG NHẬN"
                                            + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKy.FullName;
                            }
                            //Trung tam 2
                            else if (TrungTamObject.MaTrungTam.ToUpper() == "TT2")
                            {
                                NOI_CAP = "TP. Hồ Chí Minh," + subfix;
                                NGUOI_CAP = "TUQ. CỤC TRƯỞNG" + "\n" + "GIÁM ĐỐC" + "\n" + "TRUNG TÂM KIỂM ĐỊNH VÀ CHỨNG NHẬN 2"
                                            + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKy.FullName;
                            }
                            //Trung tam 3
                            else if (TrungTamObject.MaTrungTam.ToUpper() == "TT3")
                            {
                                NOI_CAP = "Đà Nẵng," + subfix;
                                NGUOI_CAP = "TUQ. CỤC TRƯỞNG" + "\n" + "GIÁM ĐỐC" + "\n" + "TRUNG TÂM KIỂM ĐỊNH VÀ CHỨNG NHẬN 3"
                                             + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKy.FullName;
                            }

                            // Nếu người ký là cục trưởng hoặc cục phó
                            if (NguoiKy != null)
                            {
                                if (NguoiKy.Position == "6")
                                {
                                    NGUOI_CAP = "CỤC TRƯỞNG" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKy.FullName;
                                }
                                else if (NguoiKy.Position == "7")
                                {
                                    NGUOI_CAP = "KT. CỤC TRƯỞNG" + "\n" + "PHÓ CỤC TRƯỞNG" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKy.FullName;
                                }
                            }
                            // Neu ho so chua dong thi hien thi thong tin theo tham chieu trong danh muc, nguoc lai thi hien thi thong tin da fix
                            if (hoso.TrangThaiId == (int)EnTrangThaiHoSoList.DA_DONG || hoso.TrangThaiId == (int)EnTrangThaiHoSoList.CHO_LUU_TRU)
                            {

                                TEN_DON_VI = !string.IsNullOrEmpty(hoso.DmDonViTenTiengViet) ? hoso.DmDonViTenTiengViet : "";
                                DIA_CHI = !string.IsNullOrEmpty(hoso.DmDonViDiaChi) ? hoso.DmDonViDiaChi : "";
                                DmTinhThanh objTinhThanh = ProviderFactory.DmTinhThanhProvider.GetById(hoso.DmDonViTinhThanhId);
                                if (objTinhThanh != null)
                                    TenTinhThanh = objTinhThanh.TenTinhThanh;
                                TEN_SAN_PHAM = sanpham.DmSanPhamTenTiengViet;
                                if (!string.IsNullOrEmpty(sanpham.KyHieu))
                                    KY_HIEU = sanpham.KyHieu;
                                HANG_SAN_XUAT = sanpham.DmHsxTenHangSanXuat;

                                TList<SanPhamTieuChuanApDung> listSPTieuChuan = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(sanpham.Id);
                                if (listSPTieuChuan != null && listSPTieuChuan.Count > 0)
                                {
                                    //LongHH
                                    foreach (SanPhamTieuChuanApDung SPTieuChuan in listSPTieuChuan)
                                    {
                                        
                                        //TIEU_CHUAN += SPTieuChuan.DmTieuChuanMaTieuChuan.ToUpper() + " - " + SPTieuChuan.DmTieuChuanTenTieuChuan + "; ";
                                        TIEU_CHUAN += SPTieuChuan.DmTieuChuanMaTieuChuan.ToUpper()+"; ";
                                    
                                    }

                                    //if (TIEU_CHUAN.Length > 3)
                                    //    TIEU_CHUAN = TIEU_CHUAN.Substring(0, TIEU_CHUAN.Length - 2);
                                    //LongHH
                                }
                            }
                            else
                            {
                                string DonViID = hoso.DonViId;
                                DmDonVi donvi = ProviderFactory.DmDonViProvider.GetById(DonViID);
                                if (donvi != null)
                                {
                                    TEN_DON_VI = donvi.TenTiengViet;
                                    DIA_CHI = donvi.DiaChi;
                                    DmTinhThanh objTinhThanh = ProviderFactory.DmTinhThanhProvider.GetById(donvi.TinhThanhId);
                                    if (objTinhThanh != null)
                                        TenTinhThanh = objTinhThanh.TenTinhThanh;
                                }

                                DmSanPham dmSanPham = ProviderFactory.DmSanPhamProvider.GetById(sanpham.SanPhamId);
                                if (dmSanPham != null)
                                {
                                    TEN_SAN_PHAM = dmSanPham.TenTiengViet;

                                    if (!string.IsNullOrEmpty(sanpham.KyHieu))
                                    {
                                        KY_HIEU = sanpham.KyHieu;
                                    }
                                    DmHangSanXuat dmHangSanXuat = ProviderFactory.DmHangSanXuatProvider.GetById(sanpham.HangSanXuatId);
                                    if (dmHangSanXuat != null)
                                    {
                                        HANG_SAN_XUAT = dmHangSanXuat.TenHangSanXuat;
                                    }
                                    TList<SanPhamTieuChuanApDung> listSPTieuChuan = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(sanpham.Id);
                                    if (listSPTieuChuan != null && listSPTieuChuan.Count > 0)
                                    {
                                        //LongHH
                                        foreach (SanPhamTieuChuanApDung SPTieuChuan in listSPTieuChuan)
                                        {
                                            DmTieuChuan dmTieuChuan = ProviderFactory.DmTieuChuanProvider.GetById(SPTieuChuan.TieuChuanApDungId);
                                            if (dmTieuChuan != null)
                                            {
                                                
                                                //TIEU_CHUAN += dmTieuChuan.MaTieuChuan.ToUpper() + " - " + dmTieuChuan.TenTieuChuan + "; ";
                                                TIEU_CHUAN += dmTieuChuan.MaTieuChuan.ToUpper()+"; ";
                                               
                                            }
                                        }
                                        //if (TIEU_CHUAN.Length > 3)
                                        //    TIEU_CHUAN = TIEU_CHUAN.Substring(0, TIEU_CHUAN.Length - 2);
                                        //LongHH
                                    }
                                }

                            }
                        }
                        //rd.SetDataSource(dtDuLieu);
                        //rd.ParameterFields["SO"].CurrentValues.AddValue(SO);
                        //rd.ParameterFields["HIEN_THI_SUB"].CurrentValues.AddValue(HIEN_THI_SUB);
                        rd.ParameterFields["TEN_DON_VI"].CurrentValues.AddValue(TEN_DON_VI.ToUpper());
                        rd.ParameterFields["DIA_CHI"].CurrentValues.AddValue(DIA_CHI);
                        rd.ParameterFields["NOI_CAP"].CurrentValues.AddValue(NOI_CAP);
                        rd.ParameterFields["NGUOI_CAP"].CurrentValues.AddValue(NGUOI_CAP.ToUpper());
                        rd.ParameterFields["SO_BTN"].CurrentValues.AddValue(SO_BTN);
                        rd.ParameterFields["KY_HIEU"].CurrentValues.AddValue(KY_HIEU);
                        rd.ParameterFields["NoiSanXuat"].CurrentValues.AddValue(HANG_SAN_XUAT.ToUpper());
                        rd.ParameterFields["TIEU_CHUAN"].CurrentValues.AddValue(TIEU_CHUAN);
                        rd.ParameterFields["Ten_SP"].CurrentValues.AddValue(TEN_SAN_PHAM.Trim().ToUpper());
                        rd.ParameterFields["SO_BAN_CONG_BO"].CurrentValues.AddValue(SO_BAN_CONG_BO);
                        rd.ParameterFields["NGAY_CONG_BO"].CurrentValues.AddValue(NGAY_CONG_BO);
                        //rd.ParameterFields["TenTinhThanh"].CurrentValues.AddValue(TenTinhThanh);
                        rd.ParameterFields["TenDonVi_InThuong"].CurrentValues.AddValue(TEN_DON_VI);
                        rd.ParameterFields["GiaTriDenNgay"].CurrentValues.AddValue(ngayHetHieuLuc);
                    }
                }
                else
                {
                    strduongdandenteploaiBC = Server.MapPath(@"~\Report\BanTiepNhan.rpt");
                    rd = new ReportDocument();
                    rd.Load(strduongdandenteploaiBC);
                    rd.PrintOptions.PaperSize = PaperSize.PaperA4;
                    if (rd != null)
                    {
                        string HoSoID = string.Empty;
                        string IdSanPham = Request["SanPhamId"] != null ? Request["SanPhamId"].ToString() : string.Empty;
                        HoSoID = Request["HoSoId"] != null ? Request["HoSoId"].ToString() : HoSoID;
                        string SO = "";
                        string SO_BAN_CONG_BO = string.Empty;
                        string NGAY_CONG_BO = string.Empty;
                        string TEN_DON_VI = "";
                        string DIA_CHI = "";
                        string NOI_CAP = "";
                        string NGUOI_CAP = "";
                        string HIEN_THI_SUB = "0";
                        string SO_BTN = string.Empty;
                        string TEN_SAN_PHAM = string.Empty;
                        string KY_HIEU = string.Empty;
                        string HANG_SAN_XUAT = string.Empty;
                        string TIEU_CHUAN = string.Empty;
                        string TenTinhThanh = string.Empty;
                        string subfix = string.Empty;
                        DataTable dtDuLieu = new DataTable();
                        dtDuLieu.Columns.Add(new DataColumn("SanPham"));
                        dtDuLieu.Columns.Add(new DataColumn("TieuChuan"));
                        DataTable dtSubDataSourceCB = new DataTable();
                        dtSubDataSourceCB.Columns.Add(new DataColumn("SanPham"));
                        dtSubDataSourceCB.Columns.Add(new DataColumn("KyHieu"));
                        dtSubDataSourceCB.Columns.Add(new DataColumn("HangSanXuat"));
                        dtSubDataSourceCB.Columns.Add(new DataColumn("TieuChuan"));
                        HoSo hoso = ProviderFactory.HoSoProvider.GetById(HoSoID);
                        SanPham sanpham = ProviderFactory.SanPhamProvider.GetById(IdSanPham);

                        string TrungTamID = hoso.TrungTamId;
                        DmTrungTam TrungTamObject = ProviderFactory.DmTrungTamProvider.GetById(TrungTamID);
                        if (TrungTamObject == null)
                        {
                            rd = null;
                            break;
                        }

                        if (hoso != null && sanpham != null)
                        {
                            SO_BAN_CONG_BO = sanpham.SoBanCongBo;
                            NGAY_CONG_BO = sanpham.NgayCongBo != null ? TalkDate(sanpham.NgayCongBo.Value) : string.Empty;
                            // Lay tong tin chung
                            if (sanpham.KetLuanId != null && Convert.ToInt32(sanpham.KetLuanId) == (int)EnKetLuanList.CAP_BAN_TIEP_NHAN_CB)
                            {
                                if (sanpham.SoBanTiepNhanCb != null)
                                    SO_BTN = sanpham.SoBanTiepNhanCb;
                            }
                            subfix = TalkDate(sanpham.NgayKyDuyet.Value);
                            SysUser NguoiKy = ProviderFactory.SysUserProvider.GetById(sanpham.NguoiKyDuyetId);
                            //Trung tam chung nhan
                            if (TrungTamObject.MaTrungTam.ToUpper() == "TTCN")
                            {
                                NOI_CAP = "Hà Nội," + subfix;
                                NGUOI_CAP = "TUQ. CỤC TRƯỞNG" + "\n" + "GIÁM ĐỐC" + "\n" + "TRUNG TÂM CHỨNG NHẬN"
                                            + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKy.FullName;
                            }
                            //Trung tam 2
                            else if (TrungTamObject.MaTrungTam.ToUpper() == "TT2")
                            {
                                NOI_CAP = "TP. Hồ Chí Minh," + subfix;
                                NGUOI_CAP = "TUQ. CỤC TRƯỞNG" + "\n" + "GIÁM ĐỐC" + "\n" + "TRUNG TÂM KIỂM ĐỊNH VÀ CHỨNG NHẬN 2"
                                            + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKy.FullName;
                            }
                            //Trung tam 3
                            else if (TrungTamObject.MaTrungTam.ToUpper() == "TT3")
                            {
                                NOI_CAP = "Đà Nẵng," + subfix;
                                NGUOI_CAP = "TUQ. CỤC TRƯỞNG" + "\n" + "GIÁM ĐỐC" + "\n" + "TRUNG TÂM KIỂM ĐỊNH VÀ CHỨNG NHẬN 3"
                                             + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKy.FullName;
                            }

                            // Nếu người ký là cục trưởng hoặc cục phó
                            if (NguoiKy != null)
                            {
                                if (NguoiKy.Position == "6")
                                {
                                    NGUOI_CAP = "CỤC TRƯỞNG" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKy.FullName;
                                }
                                else if (NguoiKy.Position == "7")
                                {
                                    NGUOI_CAP = "KT. CỤC TRƯỞNG" + "\n" + "PHÓ CỤC TRƯỞNG" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKy.FullName;
                                }
                            }
                            // Neu ho so chua dong thi hien thi thong tin theo tham chieu trong danh muc, nguoc lai thi hien thi thong tin da fix
                            if (hoso.TrangThaiId == (int)EnTrangThaiHoSoList.DA_DONG || hoso.TrangThaiId == (int)EnTrangThaiHoSoList.CHO_LUU_TRU)
                            {

                                TEN_DON_VI = !string.IsNullOrEmpty(hoso.DmDonViTenTiengViet) ? hoso.DmDonViTenTiengViet : "";
                                DIA_CHI = !string.IsNullOrEmpty(hoso.DmDonViDiaChi) ? hoso.DmDonViDiaChi : "";
                                DmTinhThanh objTinhThanh = ProviderFactory.DmTinhThanhProvider.GetById(hoso.DmDonViTinhThanhId);
                                if (objTinhThanh != null)
                                    TenTinhThanh = objTinhThanh.TenTinhThanh;
                                TEN_SAN_PHAM = sanpham.DmSanPhamTenTiengViet;
                                if (!string.IsNullOrEmpty(sanpham.KyHieu))
                                    KY_HIEU = sanpham.KyHieu;
                                HANG_SAN_XUAT = sanpham.DmHsxTenHangSanXuat;

                                TList<SanPhamTieuChuanApDung> listSPTieuChuan = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(sanpham.Id);
                                if (listSPTieuChuan != null && listSPTieuChuan.Count > 0)
                                {
                                    foreach (SanPhamTieuChuanApDung SPTieuChuan in listSPTieuChuan)
                                    {
                                        TIEU_CHUAN += SPTieuChuan.DmTieuChuanMaTieuChuan.ToUpper() + " - " + SPTieuChuan.DmTieuChuanTenTieuChuan + "; ";
                                    }
                                    if (TIEU_CHUAN.Length > 3)
                                        TIEU_CHUAN = TIEU_CHUAN.Substring(0, TIEU_CHUAN.Length - 2);
                                }
                            }
                            else
                            {
                                string DonViID = hoso.DonViId;
                                DmDonVi donvi = ProviderFactory.DmDonViProvider.GetById(DonViID);
                                if (donvi != null)
                                {
                                    TEN_DON_VI = donvi.TenTiengViet;
                                    DIA_CHI = donvi.DiaChi;
                                    DmTinhThanh objTinhThanh = ProviderFactory.DmTinhThanhProvider.GetById(donvi.TinhThanhId);
                                    if (objTinhThanh != null)
                                        TenTinhThanh = objTinhThanh.TenTinhThanh;
                                }

                                DmSanPham dmSanPham = ProviderFactory.DmSanPhamProvider.GetById(sanpham.SanPhamId);
                                if (dmSanPham != null)
                                {
                                    TEN_SAN_PHAM = dmSanPham.TenTiengViet;

                                    if (!string.IsNullOrEmpty(sanpham.KyHieu))
                                    {
                                        KY_HIEU = sanpham.KyHieu;
                                    }
                                    DmHangSanXuat dmHangSanXuat = ProviderFactory.DmHangSanXuatProvider.GetById(sanpham.HangSanXuatId);
                                    if (dmHangSanXuat != null)
                                    {
                                        HANG_SAN_XUAT = dmHangSanXuat.TenHangSanXuat;
                                    }
                                    TList<SanPhamTieuChuanApDung> listSPTieuChuan = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(sanpham.Id);
                                    if (listSPTieuChuan != null && listSPTieuChuan.Count > 0)
                                    {
                                        foreach (SanPhamTieuChuanApDung SPTieuChuan in listSPTieuChuan)
                                        {
                                            DmTieuChuan dmTieuChuan = ProviderFactory.DmTieuChuanProvider.GetById(SPTieuChuan.TieuChuanApDungId);
                                            if (dmTieuChuan != null)
                                            {
                                                TIEU_CHUAN += dmTieuChuan.MaTieuChuan.ToUpper() + " - " + dmTieuChuan.TenTieuChuan + "; ";
                                            }
                                        }
                                        if (TIEU_CHUAN.Length > 3)
                                            TIEU_CHUAN = TIEU_CHUAN.Substring(0, TIEU_CHUAN.Length - 2);
                                    }
                                }

                            }
                        }
                        rd.SetDataSource(dtDuLieu);
                        rd.ParameterFields["SO"].CurrentValues.AddValue(SO);
                        rd.ParameterFields["HIEN_THI_SUB"].CurrentValues.AddValue(HIEN_THI_SUB);
                        rd.ParameterFields["TEN_DON_VI"].CurrentValues.AddValue(TEN_DON_VI.ToUpper());
                        rd.ParameterFields["DIA_CHI"].CurrentValues.AddValue(DIA_CHI);
                        rd.ParameterFields["NOI_CAP"].CurrentValues.AddValue(NOI_CAP);
                        rd.ParameterFields["NGUOI_CAP"].CurrentValues.AddValue(NGUOI_CAP.ToUpper());
                        rd.ParameterFields["SO_BTN"].CurrentValues.AddValue(SO_BTN);
                        rd.ParameterFields["KY_HIEU"].CurrentValues.AddValue(KY_HIEU);
                        rd.ParameterFields["HANG_SAN_XUAT"].CurrentValues.AddValue(HANG_SAN_XUAT.ToUpper());
                        rd.ParameterFields["TIEU_CHUAN"].CurrentValues.AddValue(TIEU_CHUAN);
                        rd.ParameterFields["TEN_SAN_PHAM"].CurrentValues.AddValue(TEN_SAN_PHAM.ToUpper());
                        rd.ParameterFields["SO_BAN_CONG_BO"].CurrentValues.AddValue(SO_BAN_CONG_BO);
                        rd.ParameterFields["NGAY_CONG_BO"].CurrentValues.AddValue(NGAY_CONG_BO);
                        rd.ParameterFields["TenTinhThanh"].CurrentValues.AddValue(TenTinhThanh);
                        rd.ParameterFields["TenDonVi_InThuong"].CurrentValues.AddValue(TEN_DON_VI);
                    }
                }

                break;
            #endregion

            #region In phiếu tiếp nhận hồ sơ công bố -Duchh
            //Tham số:Session["HO_SO_ID"]
            case "CBPhieuNhanHoSo":
                strduongdandenteploaiBC = Server.MapPath(@"~\Report\PhieuTiepNhan.rpt");
                rd = new ReportDocument();
                rd.Load(strduongdandenteploaiBC);
                rd.PrintOptions.PaperSize = PaperSize.PaperA4;
                if (rd != null)
                {
                    string TrungTamID = _mUserInfo.TrungTam.Id;
                    DmTrungTam TrungTamObject = ProviderFactory.DmTrungTamProvider.GetById(TrungTamID);
                    if (TrungTamObject == null)
                    {
                        rd = null;
                        break;
                    }
                    string HoSoID = string.Empty;// Session["HO_SO_ID"] != null ? Session["HO_SO_ID"].ToString() : "";
                    if (Request["HoSoID"] != null)
                        HoSoID = Request["HoSoID"].ToString();

                    string SO = "";
                    string DON_VI_NOP = "";
                    string TEN_VAT_TU = "";
                    string KY_HIEU = "";
                    string DA_CAP_PHTC = "";
                    string TU_DANH_GIA = "";
                    string NGUOI_TIEP_NHAN = "";
                    string NGUOI_NOP = "";
                    string THOI_GIAN = "";
                    string DIEN_THOAI = string.Empty;
                    string FAX = string.Empty;
                    string TAI_LIEU_KHAC = "";
                    string THONG_TIN_TRUY_CAP = "";
                    HoSo hoso = ProviderFactory.HoSoProvider.GetById(HoSoID);
                    if (hoso != null)
                    {
                        SO = hoso.SoHoSo;
                        string DonViID = hoso.DonViId;
                        DmDonVi donvi = ProviderFactory.DmDonViProvider.GetById(DonViID);
                        if (donvi != null)
                        {
                            DON_VI_NOP = donvi.TenTiengViet.Trim();
                        }
                        if (hoso.HinhThucId != null)
                        {
                            int HinhThucID = hoso.HinhThucId.Value;
                            if (HinhThucID == Convert.ToInt32(EnHinhThucList.DA_CAP_PHTC))
                            {
                                DA_CAP_PHTC = "x";
                            }
                            else if (HinhThucID == Convert.ToInt32(EnHinhThucList.TU_DANH_GIA))
                            {
                                TU_DANH_GIA = "x";
                            }
                        }
                        SysUser oUser = ProviderFactory.SysUserProvider.GetById(hoso.NguoiTiepNhanId);
                        NGUOI_TIEP_NHAN = !string.IsNullOrEmpty(oUser.FullName) ? oUser.FullName : "";
                        NGUOI_NOP = !string.IsNullOrEmpty(hoso.NguoiNopHoSo) ? hoso.NguoiNopHoSo : "";
                        //Lấy chi tiết tài liệu của hồ sơ
                        DataTable dtDuLieu = new DataTable();
                        dtDuLieu.Columns.Add(new DataColumn("ChiTietHoSo"));
                        dtDuLieu.Columns.Add(new DataColumn("Checked"));
                        dtDuLieu.Columns.Add(new DataColumn("Title"));
                        DataRow dr1 = dtDuLieu.NewRow();
                        dr1["ChiTietHoSo"] = ProviderFactory.EnLoaiTaiLieuProvider.GetById(Convert.ToInt32(EnLoaiTaiLieuList.BAN_CONG_BO)).MoTa;
                        dr1["Checked"] = (hoso.BanCongBo != null && hoso.BanCongBo.Value == true) ? "x" : "";
                        dr1["Title"] = "Hồ sơ gồm" + "\t" + "\t" + " :";
                        dtDuLieu.Rows.Add(dr1);

                        DataRow dr0 = dtDuLieu.NewRow();
                        dr0["ChiTietHoSo"] = ProviderFactory.EnLoaiTaiLieuProvider.GetById(Convert.ToInt32(EnLoaiTaiLieuList.BAN_SAO_GIAY_CHUNG_NHAN_HOP_QUY)).MoTa;
                        dr0["Checked"] = (hoso.BanSaoGiayChungNhanHopQuy != null && hoso.BanSaoGiayChungNhanHopQuy.Value == true) ? "x" : "";
                        dr0["Title"] = "";
                        dtDuLieu.Rows.Add(dr0);

                        DataRow dr2 = dtDuLieu.NewRow();
                        dr2["ChiTietHoSo"] = ProviderFactory.EnLoaiTaiLieuProvider.GetById(Convert.ToInt32(EnLoaiTaiLieuList.GIAY_TO_TU_CACH_PHAP_NHAN)).MoTa;
                        dr2["Checked"] = (hoso.GiayToTuCachPhapNhan != null && hoso.GiayToTuCachPhapNhan.Value == true) ? "x" : "";
                        dr2["Title"] = "";
                        dtDuLieu.Rows.Add(dr2);
                        DataRow dr3 = dtDuLieu.NewRow();
                        dr3["ChiTietHoSo"] = ProviderFactory.EnLoaiTaiLieuProvider.GetById(Convert.ToInt32(EnLoaiTaiLieuList.BAN_TU_DANH_GIA)).MoTa;
                        dr3["Checked"] = (hoso.BanTuDanhGia != null && hoso.BanTuDanhGia.Value == true) ? "x" : "";
                        dr3["Title"] = "";
                        dtDuLieu.Rows.Add(dr3);
                        DataRow dr4 = dtDuLieu.NewRow();
                        dr4["ChiTietHoSo"] = ProviderFactory.EnLoaiTaiLieuProvider.GetById(Convert.ToInt32(EnLoaiTaiLieuList.TAI_LIEU_KY_THUAT)).MoTa;
                        dr4["Checked"] = (hoso.TaiLieuKyThuat != null && hoso.TaiLieuKyThuat.Value == true) ? "x" : "";
                        dr4["Title"] = "";
                        dtDuLieu.Rows.Add(dr4);
                        //==>Bỏ tài liệu quy trình sản xuất
                        //DataRow dr5 = dtDuLieu.NewRow();
                        //dr5["ChiTietHoSo"] = ProviderFactory.EnLoaiTaiLieuProvider.GetById(Convert.ToInt32(EnLoaiTaiLieuList.QUY_TRINH_SAN_XUAT)).MoTa;
                        //dr5["Checked"] = (hoso.QuyTrinhSanXuat != null && hoso.QuyTrinhSanXuat.Value == true) ? "  X" : "";
                        //dr5["Title"] = "";
                        //dtDuLieu.Rows.Add(dr5);
                        DataRow dr6 = dtDuLieu.NewRow();
                        dr6["ChiTietHoSo"] = ProviderFactory.EnLoaiTaiLieuProvider.GetById(Convert.ToInt32(EnLoaiTaiLieuList.KET_QUA_DO_KIEM)).MoTa;
                        dr6["Checked"] = (hoso.KetQuaDoKiem != null && hoso.KetQuaDoKiem.Value == true) ? "x" : "";
                        dr6["Title"] = "";
                        dtDuLieu.Rows.Add(dr6);

                        // Mau dau hop quy
                        DataRow dr7 = dtDuLieu.NewRow();
                        dr7["ChiTietHoSo"] = ProviderFactory.EnLoaiTaiLieuProvider.GetById(Convert.ToInt32(EnLoaiTaiLieuList.MAU_DAU_HOP_QUY)).MoTa;
                        dr7["Checked"] = (hoso.MauDauHopQuy != null && hoso.MauDauHopQuy.Value == true) ? "x" : "";
                        dr7["Title"] = "";
                        dtDuLieu.Rows.Add(dr7);
                        //==>Bỏ Chứng chỉ hệ thống quản lý chất lượng
                        //DataRow dr8 = dtDuLieu.NewRow();
                        //dr8["ChiTietHoSo"] = ProviderFactory.EnLoaiTaiLieuProvider.GetById(Convert.ToInt32(EnLoaiTaiLieuList.CHUNG_CHI_HE_THONG_QLCL)).MoTa;
                        //dr8["Checked"] = (hoso.ChungChiHtqlcl != null && hoso.ChungChiHtqlcl.Value == true) ? "  X" : "";
                        //dr8["Title"] = "";
                        //dtDuLieu.Rows.Add(dr8);
                        //Tai lieu khac
                        TAI_LIEU_KHAC = (hoso.TaiLieuKhac != null && !string.IsNullOrEmpty(hoso.TaiLieuKhac)) ?
                            ProviderFactory.EnLoaiTaiLieuProvider.GetById(Convert.ToInt32(EnLoaiTaiLieuList.TAI_LIEU_KHAC)).MoTa + ": " + hoso.TaiLieuKhac.Replace("\r\n", "; ") : "";
                        rd.SetDataSource(dtDuLieu);

                        TEN_VAT_TU = !string.IsNullOrEmpty(hoso.DanhSachSanPham) ? hoso.DanhSachSanPham : "";
                        KY_HIEU = !string.IsNullOrEmpty(hoso.DanhSachKyHieuSanPham) ? hoso.DanhSachKyHieuSanPham : "";

                        string check = Request["InThongTinTruyCap"] != null ? Request["InThongTinTruyCap"].ToString() : "";
                        if (check.Equals("1"))
                        {
                            if (hoso.DonViId != null)
                            {
                                DmDonVi _donvi = ProviderFactory.DmDonViProvider.GetById(hoso.DonViId);
                                if (_donvi != null)
                                {
                                    string DiaChiInternet = ConfigurationManager.AppSettings["DiaChiInternet"] != null ? ConfigurationManager.AppSettings["DiaChiInternet"].ToString() : "";
                                    THONG_TIN_TRUY_CAP = "Quý khách có thể tra cứu thông tin xử lý hồ sơ tại địa chỉ: " + DiaChiInternet + " bằng tên truy cập: " + _donvi.MaDonVi + " và mật khẩu: " + _donvi.MatKhauKhongMaHoa;
                                }
                            }
                        }
                        else
                        {
                            THONG_TIN_TRUY_CAP = "";
                        }
                    }
                    else
                    {
                        rd = null;
                        break;
                    }

                    DmTrungTam objDmTrungTam = ProviderFactory.DmTrungTamProvider.GetById(_mUserInfo.MaTrungTam);
                    string strSubFix = string.Empty;
                    if (hoso.NgayTiepNhan != null)
                    {
                        strSubFix = TalkDate(hoso.NgayTiepNhan.Value);
                    }
                    //
                    //Trung tam chung nhan
                    if (objDmTrungTam != null && objDmTrungTam.MaTrungTam.ToUpper() == "TTCN")
                    {
                        DIEN_THOAI = objDmTrungTam.DienThoai;
                        FAX = objDmTrungTam.Fax;
                        THOI_GIAN = "Hà Nội, ngày ..... tháng ...... năm ...........";
                        if (!string.IsNullOrEmpty(strSubFix))
                            THOI_GIAN = "Hà Nội," + strSubFix;

                    }
                    //Trung tam 2
                    else if (objDmTrungTam != null && objDmTrungTam.MaTrungTam.ToUpper() == "TT2")
                    {
                        DIEN_THOAI = objDmTrungTam.DienThoai;
                        FAX = objDmTrungTam.Fax;
                        THOI_GIAN = "Tp. Hồ Chí Minh, ngày ..... tháng ...... năm ...........";
                        if (!string.IsNullOrEmpty(strSubFix))
                            THOI_GIAN = "Tp. Hồ Chí Minh," + strSubFix;
                    }
                    //Trung tam 3
                    else if (objDmTrungTam != null && objDmTrungTam.MaTrungTam.ToUpper() == "TT3")
                    {

                        DIEN_THOAI = objDmTrungTam.DienThoai;
                        FAX = objDmTrungTam.Fax;
                        THOI_GIAN = "Đà Nẵng, ngày ..... tháng ...... năm ...........";
                        if (!string.IsNullOrEmpty(strSubFix))
                            THOI_GIAN = "Đà Nẵng," + strSubFix;
                    }
                    rd.ParameterFields["TAI_LIEU_KHAC"].CurrentValues.AddValue(TAI_LIEU_KHAC);
                    rd.ParameterFields["THOI_GIAN"].CurrentValues.AddValue(THOI_GIAN);
                    rd.ParameterFields["DON_VI_NOP"].CurrentValues.AddValue(DON_VI_NOP);
                    rd.ParameterFields["TEN_VAT_TU"].CurrentValues.AddValue(TEN_VAT_TU);
                    rd.ParameterFields["KY_HIEU"].CurrentValues.AddValue(KY_HIEU);
                    rd.ParameterFields["SO"].CurrentValues.AddValue(SO);
                    rd.ParameterFields["DA_CAP_PHTC"].CurrentValues.AddValue(DA_CAP_PHTC);
                    rd.ParameterFields["TU_DANH_GIA_PHTC"].CurrentValues.AddValue(TU_DANH_GIA);
                    rd.ParameterFields["NGUOI_TIEP_NHAN"].CurrentValues.AddValue(NGUOI_TIEP_NHAN.ToUpper());
                    rd.ParameterFields["NGUOI_NOP"].CurrentValues.AddValue(NGUOI_NOP.ToUpper());
                    rd.ParameterFields["DIEN_THOAI"].CurrentValues.AddValue(DIEN_THOAI);
                    rd.ParameterFields["FAX"].CurrentValues.AddValue(FAX);
                    rd.ParameterFields["THONG_TIN_TRUY_CAP"].CurrentValues.AddValue(THONG_TIN_TRUY_CAP);
                    rd.ParameterFields["NoiDungXuLy"].CurrentValues.AddValue(hoso.Luuy);
                    rd.ParameterFields["NgayXuLy"].CurrentValues.AddValue(hoso.NgayTiepNhan.Value.ToShortDateString());
                    rd.ParameterFields["NguoiXuLy"].CurrentValues.AddValue(NGUOI_TIEP_NHAN);
                }
                break;
            #endregion

            #region In phiếu tiếp nhận hồ sơ công bố KTCL -LongHH
            //Tham số:Session["HO_SO_ID"]
            case "CBPhieuNhanHoSoKTCL":
                strduongdandenteploaiBC = Server.MapPath(@"~\Report\PhieuTiepNhanKTCLNN.rpt");
                rd = new ReportDocument();
                rd.Load(strduongdandenteploaiBC);
                rd.PrintOptions.PaperSize = PaperSize.PaperA4;
                if (rd != null)
                {
                    string TrungTamID = _mUserInfo.TrungTam.Id;
                    DmTrungTam TrungTamObject = ProviderFactory.DmTrungTamProvider.GetById(TrungTamID);
                    if (TrungTamObject == null)
                    {
                        rd = null;
                        break;
                    }
                    string HoSoID = string.Empty;// Session["HO_SO_ID"] != null ? Session["HO_SO_ID"].ToString() : "";
                    if (Request["HoSoID"] != null)
                        HoSoID = Request["HoSoID"].ToString();

                    string SO = "";
                    string DON_VI_NOP = "";
                    string DIA_CHI = "";
                    //string TEN_VAT_TU = "";
                    //string KY_HIEU = "";
                    //string DA_CAP_PHTC = "";
                    //string TU_DANH_GIA = "";
                    string NGUOI_TIEP_NHAN = "";
                    string NGUOI_NOP = "";
                    string THOI_GIAN = "";
                    string DIEN_THOAI = string.Empty;
                    string FAX = string.Empty;
                    string LUUY = string.Empty;
                    //string TAI_LIEU_KHAC = "";
                    //string THONG_TIN_TRUY_CAP = "";
                    HoSo hoso = ProviderFactory.HoSoProvider.GetById(HoSoID);
                    if (hoso != null)
                    {
                        SO = hoso.SoHoSo;
                        string DonViID = hoso.DonViId;
                        DmDonVi donvi = ProviderFactory.DmDonViProvider.GetById(DonViID);
                        if (donvi != null)
                        {
                            DON_VI_NOP = donvi.TenTiengViet.Trim();
                            DIA_CHI = donvi.DiaChi.Trim();
                        }
                        SysUser oUser = ProviderFactory.SysUserProvider.GetById(hoso.NguoiTiepNhanId);
                        NGUOI_TIEP_NHAN = !string.IsNullOrEmpty(oUser.FullName) ? oUser.FullName : "";
                        NGUOI_NOP = !string.IsNullOrEmpty(hoso.NguoiNopHoSo) ? hoso.NguoiNopHoSo : "";
                        LUUY = hoso.Luuy;
                        //Lấy chi tiết tài liệu của hồ sơ

                    }
                    else
                    {
                        rd = null;
                        break;
                    }

                    DmTrungTam objDmTrungTam = ProviderFactory.DmTrungTamProvider.GetById(_mUserInfo.MaTrungTam);
                    string strSubFix = string.Empty;
                    if (hoso.NgayTiepNhan != null)
                    {
                        strSubFix = TalkDate(hoso.NgayTiepNhan.Value);
                    }
                    //
                    //Trung tam chung nhan
                    if (objDmTrungTam != null && objDmTrungTam.MaTrungTam.ToUpper() == "TTCN")
                    {
                        DIEN_THOAI = objDmTrungTam.DienThoai;
                        FAX = objDmTrungTam.Fax;
                        THOI_GIAN = "Hà Nội, ngày ..... tháng ...... năm ...........";
                        if (!string.IsNullOrEmpty(strSubFix))
                            THOI_GIAN = "Hà Nội," + strSubFix;

                    }
                    //Trung tam 2
                    else if (objDmTrungTam != null && objDmTrungTam.MaTrungTam.ToUpper() == "TT2")
                    {
                        DIEN_THOAI = objDmTrungTam.DienThoai;
                        FAX = objDmTrungTam.Fax;
                        THOI_GIAN = "Tp. Hồ Chí Minh, ngày ..... tháng ...... năm ...........";
                        if (!string.IsNullOrEmpty(strSubFix))
                            THOI_GIAN = "Tp. Hồ Chí Minh," + strSubFix;
                    }
                    //Trung tam 3
                    else if (objDmTrungTam != null && objDmTrungTam.MaTrungTam.ToUpper() == "TT3")
                    {

                        DIEN_THOAI = objDmTrungTam.DienThoai;
                        FAX = objDmTrungTam.Fax;
                        THOI_GIAN = "Đà Nẵng, ngày ..... tháng ...... năm ...........";
                        if (!string.IsNullOrEmpty(strSubFix))
                            THOI_GIAN = "Đà Nẵng," + strSubFix;
                    }
                    rd.ParameterFields["THOI_GIAN"].CurrentValues.AddValue(THOI_GIAN);
                    rd.ParameterFields["DON_VI_NOP"].CurrentValues.AddValue(DON_VI_NOP);
                    rd.ParameterFields["DiaChi"].CurrentValues.AddValue(DIA_CHI);
                    rd.ParameterFields["SO"].CurrentValues.AddValue(SO);
                    rd.ParameterFields["NGUOI_TIEP_NHAN"].CurrentValues.AddValue(NGUOI_TIEP_NHAN.ToUpper());
                    rd.ParameterFields["NGUOI_NOP"].CurrentValues.AddValue(NGUOI_NOP.ToUpper());
                    rd.ParameterFields["DIEN_THOAI"].CurrentValues.AddValue(DIEN_THOAI);
                    rd.ParameterFields["FAX"].CurrentValues.AddValue(FAX);
                    rd.ParameterFields["LuuY"].CurrentValues.AddValue(LUUY);

                    //rd.ParameterFields["NgayXuLy"].CurrentValues.AddValue(hoso.NgayTiepNhan.Value.ToShortDateString());
                    //rd.ParameterFields["NguoiXuLy"].CurrentValues.AddValue(NGUOI_TIEP_NHAN);
                }
                break;
            #endregion

            #region In phiếu tiếp nhận hồ sơ công bố MGKT -LongHH
            //Tham số:Session["HO_SO_ID"]
            case "CBPhieuNhanHoSoMGKT":
                strduongdandenteploaiBC = Server.MapPath(@"~\Report\PhieuTiepNhanMGKT.rpt");
                rd = new ReportDocument();
                rd.Load(strduongdandenteploaiBC);
                rd.PrintOptions.PaperSize = PaperSize.PaperA4;
                if (rd != null)
                {
                    string TrungTamID = _mUserInfo.TrungTam.Id;
                    DmTrungTam TrungTamObject = ProviderFactory.DmTrungTamProvider.GetById(TrungTamID);
                    if (TrungTamObject == null)
                    {
                        rd = null;
                        break;
                    }
                    string HoSoID = string.Empty;// Session["HO_SO_ID"] != null ? Session["HO_SO_ID"].ToString() : "";
                    if (Request["HoSoID"] != null)
                        HoSoID = Request["HoSoID"].ToString();

                    string SO = "";
                    string DON_VI_NOP = "";
                    string DIA_CHI = "";
                    //string TEN_VAT_TU = "";
                    //string KY_HIEU = "";
                    //string DA_CAP_PHTC = "";
                    //string TU_DANH_GIA = "";
                    string NGUOI_TIEP_NHAN = "";
                    string NGUOI_NOP = "";
                    string THOI_GIAN = "";
                    string DIEN_THOAI = string.Empty;
                    string FAX = string.Empty;
                    //string LUUY = string.Empty;
                    //string TAI_LIEU_KHAC = "";
                    //string THONG_TIN_TRUY_CAP = "";
                    string SAN_PHAM = string.Empty;
                    string KY_HIEU = string.Empty;
                    HoSo hoso = ProviderFactory.HoSoProvider.GetById(HoSoID);
                    if (hoso != null)
                    {
                        SO = hoso.SoHoSo;
                        string DonViID = hoso.DonViId;
                        DmDonVi donvi = ProviderFactory.DmDonViProvider.GetById(DonViID);
                        if (donvi != null)
                        {
                            DON_VI_NOP = donvi.TenTiengViet.Trim();
                            DIA_CHI = donvi.DiaChi.Trim();
                        }
                        SysUser oUser = ProviderFactory.SysUserProvider.GetById(hoso.NguoiTiepNhanId);
                        NGUOI_TIEP_NHAN = !string.IsNullOrEmpty(oUser.FullName) ? oUser.FullName : "";
                        NGUOI_NOP = !string.IsNullOrEmpty(hoso.NguoiNopHoSo) ? hoso.NguoiNopHoSo : "";
                        //LUUY = hoso.Luuy;
                        //Lấy chi tiết tài liệu của hồ sơ
                        SAN_PHAM = hoso.DanhSachSanPham;
                        KY_HIEU = hoso.DanhSachKyHieuSanPham;

                    }
                    else
                    {
                        rd = null;
                        break;
                    }

                    DmTrungTam objDmTrungTam = ProviderFactory.DmTrungTamProvider.GetById(_mUserInfo.MaTrungTam);
                    string strSubFix = string.Empty;
                    if (hoso.NgayTiepNhan != null)
                    {
                        strSubFix = TalkDate(hoso.NgayTiepNhan.Value);
                    }
                    //
                    //Trung tam chung nhan
                    if (objDmTrungTam != null && objDmTrungTam.MaTrungTam.ToUpper() == "TTCN")
                    {
                        DIEN_THOAI = objDmTrungTam.DienThoai;
                        FAX = objDmTrungTam.Fax;
                        THOI_GIAN = "Hà Nội, ngày ..... tháng ...... năm ...........";
                        if (!string.IsNullOrEmpty(strSubFix))
                            THOI_GIAN = "Hà Nội," + strSubFix;

                    }
                    //Trung tam 2
                    else if (objDmTrungTam != null && objDmTrungTam.MaTrungTam.ToUpper() == "TT2")
                    {
                        DIEN_THOAI = objDmTrungTam.DienThoai;
                        FAX = objDmTrungTam.Fax;
                        THOI_GIAN = "Tp. Hồ Chí Minh, ngày ..... tháng ...... năm ...........";
                        if (!string.IsNullOrEmpty(strSubFix))
                            THOI_GIAN = "Tp. Hồ Chí Minh," + strSubFix;
                    }
                    //Trung tam 3
                    else if (objDmTrungTam != null && objDmTrungTam.MaTrungTam.ToUpper() == "TT3")
                    {

                        DIEN_THOAI = objDmTrungTam.DienThoai;
                        FAX = objDmTrungTam.Fax;
                        THOI_GIAN = "Đà Nẵng, ngày ..... tháng ...... năm ...........";
                        if (!string.IsNullOrEmpty(strSubFix))
                            THOI_GIAN = "Đà Nẵng," + strSubFix;
                    }
                    rd.ParameterFields["THOI_GIAN"].CurrentValues.AddValue(THOI_GIAN);
                    rd.ParameterFields["DON_VI_NOP"].CurrentValues.AddValue(DON_VI_NOP);
                    rd.ParameterFields["DiaChi"].CurrentValues.AddValue(DIA_CHI);
                    rd.ParameterFields["SO"].CurrentValues.AddValue(SO);
                    rd.ParameterFields["NGUOI_TIEP_NHAN"].CurrentValues.AddValue(NGUOI_TIEP_NHAN.ToUpper());
                    rd.ParameterFields["NGUOI_NOP"].CurrentValues.AddValue(NGUOI_NOP.ToUpper());
                    rd.ParameterFields["DIEN_THOAI"].CurrentValues.AddValue(DIEN_THOAI);
                    rd.ParameterFields["FAX"].CurrentValues.AddValue(FAX);
                    rd.ParameterFields["SanPham"].CurrentValues.AddValue(SAN_PHAM);
                    rd.ParameterFields["KyHieu"].CurrentValues.AddValue(KY_HIEU);
                    //rd.ParameterFields["LuuY"].CurrentValues.AddValue(LUUY + "12323");

                    //rd.ParameterFields["NgayXuLy"].CurrentValues.AddValue(hoso.NgayTiepNhan.Value.ToShortDateString());
                    //rd.ParameterFields["NguoiXuLy"].CurrentValues.AddValue(NGUOI_TIEP_NHAN);
                }
                break;
            #endregion

            #region In phiếu nhận hồ sơ-Quynv
            case "PhieuNhanHoSo":
                strduongdandenteploaiBC = Server.MapPath(@"~\Report\CN_PhieuTiepNhan.rpt");
                rd = new ReportDocument();
                rd.Load(strduongdandenteploaiBC);
                rd.PrintOptions.PaperSize = PaperSize.PaperA4;
                if (rd != null)
                {
                    string TrungTamID = _mUserInfo.TrungTam.Id;// Session["TrungTamID"] != null ? Session["TrungTamID"].ToString() : "";
                    string HosoID = Request["HoSoID"] != null ? Request["HoSoID"].ToString() : "";
                    HoSo OHoso = ProviderFactory.HoSoProvider.GetById(HosoID);
                    //if (TrungTamID.Equals(""))
                    //    TrungTamID = OHoso.TrungTamId;
                    DmTrungTam TrungTamObject = ProviderFactory.DmTrungTamProvider.GetById(TrungTamID);
                    string TEN_TRUNG_TAM = !string.IsNullOrEmpty(TrungTamObject.TenTrungTam) ? TrungTamObject.TenTrungTam : "";
                    string FAX = string.Empty;
                    string DIEN_THOAI = string.Empty;
                    if (TrungTamObject == null)
                    {
                        rd = null;
                        break;
                    }
                    else
                    {
                        DIEN_THOAI = TrungTamObject.DienThoai;
                        FAX = TrungTamObject.Fax;
                    }
                    //gan gia tri cho cac tham so cua bao cao
                    string TEN_SAN_PHAM = !string.IsNullOrEmpty(OHoso.DanhSachSanPham) ? OHoso.DanhSachSanPham : "";
                    string KY_HIEU = !string.IsNullOrEmpty(OHoso.DanhSachKyHieuSanPham) ? OHoso.DanhSachKyHieuSanPham : "";

                    string SO = !string.IsNullOrEmpty(OHoso.SoHoSo) ? OHoso.SoHoSo : "";
                    DmDonVi dvObject = ProviderFactory.DmDonViProvider.GetById(OHoso.DonViId);
                    string DON_VI_NOP_HS = !string.IsNullOrEmpty(dvObject.TenTiengViet) ? dvObject.TenTiengViet : "";
                    string NK_KEM_KQ_DO_KIEM = "";
                    string NK_CHUA_DO_KIEM = "";
                    string SX_TRONG_NUOC_KHONG_CO_ISO = "";
                    string SX_TRONG_NUOC_CO_ISO = "";
                    string THONG_TIN_TRUY_CAP = "";
                    string NGUON_GOC = string.Empty;
                    if (OHoso.NguonGocId != null)
                    {
                        int nguonGoc = OHoso.NguonGocId.Value;
                        NGUON_GOC = EntityHelper.GetEnumTextValue((EnNguonGocList)nguonGoc);
                        if (nguonGoc == (int)EnNguonGocList.NK_KEM_KQ_DO_KIEM)
                        {
                            NK_KEM_KQ_DO_KIEM = "x";
                        }
                        else if (nguonGoc == (int)EnNguonGocList.NK_CHUA_DO_KIEM)
                        {
                            NK_CHUA_DO_KIEM = "x";
                        }
                        else if (nguonGoc == (int)EnNguonGocList.SX_TRONG_NUOC_CO_ISO)
                        {
                            SX_TRONG_NUOC_CO_ISO = "x";
                        }
                        else
                            SX_TRONG_NUOC_KHONG_CO_ISO = "x";
                    }

                    string BAT_BUOC_CN_PH = "";
                    string TU_NGUYEN = "";
                    string DON_DE_NGHI = "";
                    if (OHoso.DonDeNghiChungNhan != null)
                    {
                        bool DeNghi = OHoso.DonDeNghiChungNhan.Value;
                        if (DeNghi)
                        {
                            DON_DE_NGHI = "x";
                        }
                    }
                    string NGAY_NHAN = "Hà nội, ngày ";
                    string strSubFix = string.Empty;
                    if (OHoso.NgayTiepNhan != null)
                    {
                        strSubFix = TalkDate(OHoso.NgayTiepNhan.Value);
                    }
                    //Trung tam chung nhan
                    if (TrungTamObject != null && TrungTamObject.MaTrungTam.ToUpper() == "TTCN")
                    {
                        NGAY_NHAN = "Hà Nội, ngày ..... tháng ...... năm ...........";
                        if (!string.IsNullOrEmpty(strSubFix))
                            NGAY_NHAN = "Hà Nội," + strSubFix;
                    }
                    //Trung tam 2
                    else if (TrungTamObject != null && TrungTamObject.MaTrungTam.ToUpper() == "TT2")
                    {
                        NGAY_NHAN = "Tp. Hồ Chí Minh, ngày ..... tháng ...... năm ...........";
                        if (!string.IsNullOrEmpty(strSubFix))
                            NGAY_NHAN = "Tp. Hồ Chí Minh," + strSubFix;

                    }
                    //Trung tam 3
                    else if (TrungTamObject != null && TrungTamObject.MaTrungTam.ToUpper() == "TT3")
                    {
                        NGAY_NHAN = "Đà Nẵng, ngày ..... tháng ...... năm ...........";
                        if (!string.IsNullOrEmpty(strSubFix))
                            NGAY_NHAN = "Đà Nẵng," + strSubFix;
                    }
                    string GIAY_VE_TU_CACH_PN = "";
                    if (OHoso.GiayToTuCachPhapNhan != null)
                    {
                        bool Giayto = OHoso.GiayToTuCachPhapNhan.Value;
                        if (Giayto)
                        {
                            GIAY_VE_TU_CACH_PN = "x";
                        }
                    }
                    SysUser oUser = ProviderFactory.SysUserProvider.GetById(OHoso.NguoiTiepNhanId);
                    string NGUOI_TIEP_NHAN = !string.IsNullOrEmpty(oUser.FullName) ? oUser.FullName : "";

                    string TAI_LIEU_KY_THUAT = "";
                    if (OHoso.TaiLieuKyThuat != null)
                    {
                        bool taiLieu = OHoso.TaiLieuKyThuat.Value;
                        if (taiLieu)
                        {
                            TAI_LIEU_KY_THUAT = "x";
                        }
                    }
                    string NGUOI_NOP = !string.IsNullOrEmpty(OHoso.NguoiNopHoSo) ? OHoso.NguoiNopHoSo : "";
                    string KET_QUA_DO_KIEM = "";
                    if (OHoso.KetQuaDoKiem != null)
                    {
                        bool ketQua = OHoso.KetQuaDoKiem.Value;
                        if (ketQua)
                        {
                            KET_QUA_DO_KIEM = "x";
                        }
                    }
                    string QUY_TRINH_SAN_XUAT = "";
                    if (OHoso.QuyTrinhSanXuat != null)
                    {
                        bool Qtsx = OHoso.QuyTrinhSanXuat.Value;
                        if (Qtsx)
                        {
                            QUY_TRINH_SAN_XUAT = "x";
                        }
                    }
                    string QUY_TRINH_DAM_BAO_CL = "";
                    if (OHoso.QuyTrinhDamBaoChatLuong != null)
                    {
                        bool Qtdb = OHoso.QuyTrinhDamBaoChatLuong.Value;
                        if (Qtdb)
                        {
                            QUY_TRINH_DAM_BAO_CL = "x";
                        }
                    }
                    string CHUNG_CHI_HT_QL_CL = "";
                    if (OHoso.ChungChiHtqlcl != null)
                    {
                        bool chungchi = OHoso.ChungChiHtqlcl.Value;
                        if (chungchi)
                        {
                            CHUNG_CHI_HT_QL_CL = "x";
                        }
                    }
                    string TIEU_CHUAN_TU_NGUYEN_AD = "";
                    if (OHoso.TieuChuanApDung != null)
                    {
                        bool tieuchuan = OHoso.TieuChuanApDung.Value;
                        if (tieuchuan)
                        {
                            TIEU_CHUAN_TU_NGUYEN_AD = "x";
                        }
                    }
                    string TAI_LIEU_KHAC = !string.IsNullOrEmpty(OHoso.TaiLieuKhac) ? OHoso.TaiLieuKhac : "";
                    string check = Request["InThongTinTruyCap"] != null ? Request["InThongTinTruyCap"].ToString() : "";
                    if (check.Equals("1"))
                    {
                        if (OHoso.DonViId != null)
                        {
                            DmDonVi _donvi = ProviderFactory.DmDonViProvider.GetById(OHoso.DonViId);
                            if (_donvi != null)
                            {
                                string DiaChiInternet = ConfigurationManager.AppSettings["DiaChiInternet"] != null ? ConfigurationManager.AppSettings["DiaChiInternet"].ToString() : "";
                                THONG_TIN_TRUY_CAP = "Quý khách có thể tra cứu thông tin xử lý hồ sơ tại địa chỉ: " + DiaChiInternet + " bằng tên truy cập: " + _donvi.MaDonVi + " và mật khẩu: " + _donvi.MatKhauKhongMaHoa;
                            }
                        }
                    }
                    else
                    {
                        THONG_TIN_TRUY_CAP = "";
                    }

                    string LoaiHinhChungNhan = string.Empty;
                    if (OHoso.LoaiHoSo == (int)LoaiHoSo.ChungNhanHopQuy)
                    {
                        LoaiHinhChungNhan = "chứng nhận hợp quy";
                    }
                    else
                    {
                        LoaiHinhChungNhan = "chứng nhận hợp chuẩn";
                    }

                    //doc vao gia tri cho tham so cua bao cao
                    rd.ParameterFields["TEN_TRUNG_TAM"].CurrentValues.AddValue(TEN_TRUNG_TAM.ToUpper());
                    rd.ParameterFields["SO"].CurrentValues.AddValue(SO);
                    rd.ParameterFields["TEN_SAN_PHAM"].CurrentValues.AddValue(TEN_SAN_PHAM);
                    rd.ParameterFields["KY_HIEU"].CurrentValues.AddValue(KY_HIEU);
                    rd.ParameterFields["DON_VI_NOP_HS"].CurrentValues.AddValue(DON_VI_NOP_HS);
                    rd.ParameterFields["NK_CHUA_DO_KIEM"].CurrentValues.AddValue(NK_CHUA_DO_KIEM);
                    rd.ParameterFields["BAT_BUOC_CN_PH"].CurrentValues.AddValue(BAT_BUOC_CN_PH);
                    rd.ParameterFields["TU_NGUYEN"].CurrentValues.AddValue(TU_NGUYEN);
                    rd.ParameterFields["DON_DE_NGHI"].CurrentValues.AddValue(DON_DE_NGHI);
                    rd.ParameterFields["NGAY_NHAN"].CurrentValues.AddValue(NGAY_NHAN);
                    rd.ParameterFields["GIAY_VE_TU_CACH_PN"].CurrentValues.AddValue(GIAY_VE_TU_CACH_PN);
                    rd.ParameterFields["NGUOI_TIEP_NHAN"].CurrentValues.AddValue(NGUOI_TIEP_NHAN.ToUpper());
                    rd.ParameterFields["TAI_LIEU_KY_THUAT"].CurrentValues.AddValue(TAI_LIEU_KY_THUAT);
                    rd.ParameterFields["NGUOI_NOP"].CurrentValues.AddValue(NGUOI_NOP.ToUpper());
                    rd.ParameterFields["NK_KEM_KQ_DO_KIEM"].CurrentValues.AddValue(NK_KEM_KQ_DO_KIEM);
                    rd.ParameterFields["SX_TRONG_NUOC_CO_ISO"].CurrentValues.AddValue(SX_TRONG_NUOC_CO_ISO);
                    rd.ParameterFields["SX_TRONG_NUOC_KHONG_CO_ISO"].CurrentValues.AddValue(SX_TRONG_NUOC_KHONG_CO_ISO);
                    rd.ParameterFields["KET_QUA_DO_KIEM"].CurrentValues.AddValue(KET_QUA_DO_KIEM);
                    rd.ParameterFields["QUY_TRINH_SAN_XUAT"].CurrentValues.AddValue(QUY_TRINH_SAN_XUAT);
                    rd.ParameterFields["QUY_TRINH_DAM_BAO_CL"].CurrentValues.AddValue(QUY_TRINH_DAM_BAO_CL);
                    rd.ParameterFields["CHUNG_CHI_HT_QL_CL"].CurrentValues.AddValue(CHUNG_CHI_HT_QL_CL);
                    rd.ParameterFields["TIEU_CHUAN_TU_NGUYEN_AD"].CurrentValues.AddValue(TIEU_CHUAN_TU_NGUYEN_AD);
                    rd.ParameterFields["TAI_LIEU_KHAC"].CurrentValues.AddValue(TAI_LIEU_KHAC);
                    rd.ParameterFields["DIEN_THOAI"].CurrentValues.AddValue(DIEN_THOAI);
                    rd.ParameterFields["FAX"].CurrentValues.AddValue(FAX);
                    rd.ParameterFields["THONG_TIN_TRUY_CAP"].CurrentValues.AddValue(THONG_TIN_TRUY_CAP);
                    rd.ParameterFields["NGUON_GOC"].CurrentValues.AddValue(NGUON_GOC);
                    rd.ParameterFields["LOAI_HINH_CHUNG_NHAN"].CurrentValues.AddValue(LoaiHinhChungNhan.ToUpper());
                    rd.ParameterFields["LoaiDon"].CurrentValues.AddValue(LoaiHinhChungNhan);
                    rd.ParameterFields["NoiDungXuLy"].CurrentValues.AddValue(OHoso.Luuy);
                    rd.ParameterFields["NgayXuLy"].CurrentValues.AddValue(OHoso.NgayTiepNhan.Value.ToShortDateString());
                    rd.ParameterFields["NguoiXuLy"].CurrentValues.AddValue(NGUOI_TIEP_NHAN);
                }
                break;
            #endregion

            #region In báo cáo thống kê-Linhnm
            case "BaoCaoThongKe":
                strduongdandenteploaiBC = Server.MapPath(@"~\Report\BaoCaoThongKe1.rpt");
                rd = new ReportDocument();
                rd.Load(strduongdandenteploaiBC);
                if (rd != null)
                {
                    DataTable dtDuLieu = ProviderFactory.SanPhamProvider.GetDuLieuBaoCaoThongKe(mUserInfo.OrganizationCode, Request["chuyenvien"].ToString(), Request["tu"].ToString(), Request["den"].ToString());
                    string strMaTrungTam = ProviderFactory.DmTrungTamProvider.GetByMaTrungTam(_mUserInfo.MaTrungTam)[1];
                    rd.SetDataSource(dtDuLieu);

                    rd.ParameterFields["TenTrungTam"].CurrentValues.AddValue(LayTenTrungTam(_mUserInfo.MaTrungTam).ToUpper());
                    rd.ParameterFields["MaTrungTam"].CurrentValues.AddValue(strMaTrungTam.ToUpper());
                    rd.ParameterFields["GiamDoc"].CurrentValues.AddValue(_mUserInfo.TenGiamDoc.ToUpper());
                    rd.ParameterFields["Tu"].CurrentValues.AddValue(Request["tu"].ToString());
                    rd.ParameterFields["Den"].CurrentValues.AddValue(Request["den"].ToString());
                    string[] TrungTamInfo = ProviderFactory.DmTrungTamProvider.GetByMaTrungTam(mUserInfo.TrungTam.Id);
                    string thoigianky = TrungTamInfo[0] + "," + TalkDate(DateTime.Now);
                    rd.ParameterFields["ThoiGianKy"].CurrentValues.AddValue(thoigianky);
                }
                break;
            #endregion

            #region In báo cáo tuần-Linhnm
            case "BaoCaoTuan":
                {
                    switch (Request["type"].ToString())
                    {
                        case "1":
                            {
                                strduongdandenteploaiBC = Server.MapPath(@"~\Report\BaoCaoTuan1.rpt");
                                rd = LayDuLieuTuanQuy(strduongdandenteploaiBC, Request["trungtam"].ToString(), Request["tu"].ToString(), Request["den"].ToString());
                                break;
                            }
                        case "2":
                            {
                                strduongdandenteploaiBC = Server.MapPath(@"~\Report\BaoCaoTuan2.rpt");
                                rd = LayDuLieuTuanQuy(strduongdandenteploaiBC, Request["trungtam"].ToString(), Request["tu"].ToString(), Request["den"].ToString());
                                break;
                            }
                        case "3":
                            {
                                strduongdandenteploaiBC = Server.MapPath(@"~\Report\BaoCaoTuan.rpt");
                                rd = LayDuLieuTuanQuy(strduongdandenteploaiBC, Request["trungtam"].ToString(), Request["tu"].ToString(), Request["den"].ToString());
                                break;
                            }
                    }
                }
                break;
            #endregion

            #region In báo cáo quý-Linhnm
            case "BaoCaoQuy":
                {
                    switch (Request["type"].ToString())
                    {
                        case "1":
                            {
                                strduongdandenteploaiBC = Server.MapPath(@"~\Report\BaoCaoQuy1.rpt");
                                rd = LayDuLieuTuanQuy(strduongdandenteploaiBC, Request["trungtam"].ToString(), Request["tu"].ToString(), Request["den"].ToString());
                                break;
                            }
                        case "2":
                            {
                                strduongdandenteploaiBC = Server.MapPath(@"~\Report\BaoCaoQuy2.rpt");
                                rd = LayDuLieuTuanQuy(strduongdandenteploaiBC, Request["trungtam"].ToString(), Request["tu"].ToString(), Request["den"].ToString());
                                break;
                            }
                        case "3":
                            {
                                strduongdandenteploaiBC = Server.MapPath(@"~\Report\BaoCaoQuy.rpt");
                                rd = LayDuLieuTuanQuy(strduongdandenteploaiBC, Request["trungtam"].ToString(), Request["tu"].ToString(), Request["den"].ToString());
                                break;
                            }
                    }
                }
                break;
            #endregion

            #region In báo cáo tổng hợp tiếp nhận hồ sơ
            case "BaoCaoTiepNhanHoSo":
                strduongdandenteploaiBC = Server.MapPath(@"~\Report\TongHopTiepNhanHoSo.rpt");
                rd = new ReportDocument();
                rd.Load(strduongdandenteploaiBC);
                if (rd != null)
                {
                    string NgayTiepNhan = Request["NgayTiepNhan"].ToString();
                    int LoaiHoSo = Convert.ToInt32(Request["LoaiHoSo"].ToString());
                    DataTable dtDuLieu = ProviderFactory.HoSoProvider.TongHopHoSoMoiNhan(mUserInfo.TrungTam.Id, NgayTiepNhan, LoaiHoSo);
                    rd.SetDataSource(dtDuLieu);

                    rd.ParameterFields["NgayTiepNhan"].CurrentValues.AddValue(NgayTiepNhan);
                }
                break;
            #endregion

            #region In phiếu đánh giá công bố-Hùngnv
            case "CBPhieuDanhGia":
                string SanPhamId = Request["SanPhamId"] != null ? Request["SanPhamId"].ToString() : "";
                SanPham objSanPhamDG = ProviderFactory.SanPhamProvider.GetById(SanPhamId);

                if (objSanPhamDG != null)
                {
                    if (objSanPhamDG.HinhThucId == (int)EnHinhThucList.DA_CAP_PHTC)
                    {
                        strduongdandenteploaiBC = Server.MapPath(@"~\Report\PhieuDanhGia_CB.rpt");
                    }
                    else
                        strduongdandenteploaiBC = Server.MapPath(@"~\Report\PhieuDanhGia_CB_TuDG.rpt");
                    rd = new ReportDocument();
                    rd.Load(strduongdandenteploaiBC);
                    if (rd != null)
                    {
                        string TrungTamID = _mUserInfo.TrungTam.Id;

                        DmTrungTam TrungTamObject = ProviderFactory.DmTrungTamProvider.GetById(TrungTamID);
                        if (TrungTamObject == null)
                        {
                            rd = null;
                            break;
                        }
                        string TEN_TRUNG_TAM_TIENG_VIET = !string.IsNullOrEmpty(TrungTamObject.TenTrungTam) ? TrungTamObject.TenTrungTam : "";
                        string TEN_SAN_PHAM = "";
                        string KY_HIEU = "";
                        string HANG_SAN_XUAT = "";
                        string MA_NHOM_SP = "";
                        string TO_CHUC_CA_NHAN = "";
                        string NGUOI_LIEN_HE = "";
                        string DIEN_THOAI = "";
                        string SO_CONG_VAN_DEN = "";
                        string TRUC_TIEP = "";
                        string BUU_DIEN = "";
                        string NK_KEM_KQ_DO_KIEM = "";
                        string NK_CHUA_DO_KIEM = "";
                        string SX_TRONG_NUOC_CO_ISO = "";
                        string SX_TRONG_NUOC_KHONG_CO_ISO = "";
                        string HOP_LE = "";
                        string KHONG_HOP_LE = "";
                        string DAY_DU = "";
                        string KHONG_DAY_DU = "";
                        string SO_NGAY = "";
                        string CO_QUAN_DO_KIEM = "";
                        string NOI_DUNG_DO_KIEM = "";
                        string NHAN_XET_KHAC = "";
                        string CAP_BTN = "";
                        string KHONG_CAP_BTN = "";
                        string KHONG_PHAI_CN = "";
                        string THOI_HAN_HAI_NAM = "";
                        string THOI_HAN_BA_NAM = "";
                        string THOI_HAN_KHAC = "";
                        string LE_PHI_CN = "";
                        string SO_BanTiepNhan = "";
                        string FILE_DINH_KEM = "";
                        string TEN_THAM_DINH = "";
                        string TEN_DANH_GIA = "";
                        string strSubFix = string.Empty;
                        string DA_CAP_GCN = string.Empty;
                        string TU_DANH_GIA = string.Empty;
                        string HUY_HO_SO = string.Empty;
                        string SoTuDanhGia_GCN = string.Empty;
                        string NgayDanhGia_CN = string.Empty;
                        string strNhanHoSoTu = string.Empty;
                        string SoGiayThongBaoLePhi = string.Empty;
                        string HopLe_DayDu = string.Empty;
                        string HinhThucCongBo = string.Empty;
                        string NguonGoc = string.Empty;
                        string KetLuan = string.Empty;
                        string KetQuaDoKiem = string.Empty;

                        HoSo objHoSo = ProviderFactory.HoSoProvider.GetById(objSanPhamDG.HoSoId);

                        if (objSanPhamDG.HinhThucId == (int)EnHinhThucList.DA_CAP_PHTC)
                        {
                            HinhThucCongBo = "Đã cấp GCN";
                            DA_CAP_GCN = "x";
                            DateTime ngayCapGCN = new DateTime();
                            SoTuDanhGia_GCN = objSanPhamDG.SoGcn;
                            DateTime.TryParse(SoTuDanhGia_GCN.Substring(5, 2) + "/" + SoTuDanhGia_GCN.Substring(7, 2) + "/" + SoTuDanhGia_GCN.Substring(9, 2), out ngayCapGCN);
                            if (ngayCapGCN != null)
                                NgayDanhGia_CN = ngayCapGCN.ToShortDateString();
                        }
                        else
                        {
                            HinhThucCongBo = "Tự đánh giá";
                            TU_DANH_GIA = "x";
                            if (objSanPhamDG.NgayDanhGia != null)
                                NgayDanhGia_CN = objSanPhamDG.NgayDanhGia.Value.ToShortDateString();
                            SoTuDanhGia_GCN = objSanPhamDG.SoBanTuDanhGia;
                        }
                        if (objSanPhamDG != null)
                        {
                            if (objSanPhamDG.NgayKyDuyet != null)
                            {
                                strSubFix = TalkDate(objSanPhamDG.NgayKyDuyet.Value);
                            }
                            else
                            {
                                strSubFix = TalkDate(DateTime.Now);
                            }

                            KY_HIEU = !string.IsNullOrEmpty(objSanPhamDG.KyHieu) ? objSanPhamDG.KyHieu : "";
                            DmNhomSanPham NhomSanPham = ProviderFactory.DmNhomSanPhamProvider.GetById(objSanPhamDG.NhomSanPhamId);
                            if (NhomSanPham != null)
                                MA_NHOM_SP = !string.IsNullOrEmpty(NhomSanPham.MaNhom) ? NhomSanPham.MaNhom : "";
                            DmSanPham SanPhamObject = ProviderFactory.DmSanPhamProvider.GetById(objSanPhamDG.SanPhamId);
                            if (SanPhamObject != null)
                            {
                                TEN_SAN_PHAM = !string.IsNullOrEmpty(SanPhamObject.TenTiengViet) ? SanPhamObject.TenTiengViet : "";
                            }
                            DmHangSanXuat SanXuatObject = ProviderFactory.DmHangSanXuatProvider.GetById(objSanPhamDG.HangSanXuatId);
                            if (SanXuatObject != null)
                            {
                                HANG_SAN_XUAT = !string.IsNullOrEmpty(SanXuatObject.TenHangSanXuat) ? SanXuatObject.TenHangSanXuat : "";
                            }
                            HoSo hoso = ProviderFactory.HoSoProvider.GetById(objSanPhamDG.HoSoId);
                            if (hoso != null)
                            {
                                DmDonVi DonViObject = ProviderFactory.DmDonViProvider.GetById(hoso.DonViId);
                                if (DonViObject != null)
                                {
                                    TO_CHUC_CA_NHAN = !string.IsNullOrEmpty(DonViObject.TenTiengViet) ? DonViObject.TenTiengViet : "";
                                }
                                NGUOI_LIEN_HE = !string.IsNullOrEmpty(hoso.NguoiNopHoSo) ? hoso.NguoiNopHoSo : "";
                                DIEN_THOAI = !string.IsNullOrEmpty(hoso.DienThoai) ? hoso.DienThoai : "";
                                string NgayThang = hoso.NgayTiepNhan != null ? "(" + string.Format("{0:dd/MM/yyyy}", hoso.NgayTiepNhan) + ")" : "";
                                SO_CONG_VAN_DEN = !string.IsNullOrEmpty(hoso.SoCongVanDen) ? hoso.SoCongVanDen + NgayThang : "";
                                if (hoso.NhanHoSoTuId != null)
                                {
                                    int NhanHoSoTuID = hoso.NhanHoSoTuId.Value;
                                    if (NhanHoSoTuID == Convert.ToInt32(EnNhanHoSoTuList.BUU_DIEN))
                                    {
                                        BUU_DIEN = "x";
                                        strNhanHoSoTu = "Qua đường bưu điện";
                                    }
                                    else if (NhanHoSoTuID == Convert.ToInt32(EnNhanHoSoTuList.TRUC_TIEP))
                                    {
                                        TRUC_TIEP = "x";
                                        strNhanHoSoTu = "Trực tiếp";
                                    }
                                    else if (NhanHoSoTuID == Convert.ToInt32(EnNhanHoSoTuList.QUA_MANG))
                                    {
                                        strNhanHoSoTu = "Qua mạng Internet";
                                    }
                                }

                                if (objSanPhamDG.KetLuanId == (int)EnKetLuanList.CAP_BAN_TIEP_NHAN_CB)
                                    LE_PHI_CN = string.Format("{0:0,0}", Convert.ToInt32(ConfigurationManager.AppSettings["PhiCongBoHQ"].ToString())) + " VNĐ";
                                else
                                    LE_PHI_CN = "0 VNĐ";
                                TList<PhanCong> phancongList = ProviderFactory.PhanCongProvider.GetByHoSoId(hoso.Id);
                                if (phancongList != null && phancongList.Count > 0)
                                {
                                    SysUser NguoiThamDinh = ProviderFactory.SysUserProvider.GetById(phancongList[0].NguoiThamDinh);
                                    if (NguoiThamDinh != null)
                                    {
                                        TEN_THAM_DINH = !string.IsNullOrEmpty(NguoiThamDinh.FullName) ? NguoiThamDinh.FullName : "";
                                    }
                                    SysUser NguoiDanhGia = ProviderFactory.SysUserProvider.GetById(phancongList[0].NguoiXuLy);
                                    if (NguoiDanhGia != null)
                                    {
                                        TEN_DANH_GIA = !string.IsNullOrEmpty(NguoiDanhGia.FullName) ? NguoiDanhGia.FullName : "";
                                    }
                                }

                            }
                            if (hoso.NguonGocId != null)
                            {
                                int NguonGocID = hoso.NguonGocId.Value;

                                if (NguonGocID == Convert.ToInt32(EnNguonGocList.NHAP_KHAU))
                                {
                                    NK_KEM_KQ_DO_KIEM = "x";
                                    NguonGoc = "Nhập khẩu";
                                }
                                else
                                {
                                    SX_TRONG_NUOC_KHONG_CO_ISO = "x";
                                    NguonGoc = "Sản xuất trong nước";
                                }
                            }

                            if (objSanPhamDG.HopLe != null)
                            {
                                bool HopLe = objSanPhamDG.HopLe.Value;
                                if (HopLe)
                                {
                                    HOP_LE = "x";
                                    HopLe_DayDu = "- Hợp lệ";
                                }
                                else
                                {
                                    KHONG_HOP_LE = "x";
                                    HopLe_DayDu = "- Không hợp lệ";
                                }
                            }
                            if (objSanPhamDG.DayDu != null)
                            {
                                bool DayDu = objSanPhamDG.DayDu.Value;
                                if (DayDu)
                                {
                                    DAY_DU = "x";
                                    HopLe_DayDu += ", đầy đủ";
                                }
                                else
                                {
                                    KHONG_DAY_DU = "x";
                                    HopLe_DayDu += ", không đầy đủ";
                                }
                            }
                            if (!string.IsNullOrEmpty(objSanPhamDG.SoDoKiem))
                                SO_NGAY = objSanPhamDG.SoDoKiem;
                            if (objSanPhamDG.NgayDoKiem != null)
                            {
                                string NgayDoKiem = string.Format("{0:dd/MM/yyyy}", objSanPhamDG.NgayDoKiem);
                                SO_NGAY = SO_NGAY + ", " + NgayDoKiem;
                            }
                            DmCoQuanDoKiem CoQuanDoKiem = ProviderFactory.DmCoQuanDoKiemProvider.GetById(objSanPhamDG.CoQuanDoKiemId);
                            if (CoQuanDoKiem != null)
                                CO_QUAN_DO_KIEM = !string.IsNullOrEmpty(CoQuanDoKiem.TenCoQuanDoKiem) ? CoQuanDoKiem.TenCoQuanDoKiem : "";

                            string strNoiDungDoKiemTamThoi = string.Empty;
                            TList<SanPhamTieuChuanApDung> lstSanPhamTieuChuanApDung = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(objSanPhamDG.Id);
                            foreach (SanPhamTieuChuanApDung obj in lstSanPhamTieuChuanApDung)
                            {
                                DmTieuChuan objDmTieuChuan = new DmTieuChuan();
                                objDmTieuChuan = ProviderFactory.DmTieuChuanProvider.GetById(obj.TieuChuanApDungId);
                                if (objDmTieuChuan != null)
                                    strNoiDungDoKiemTamThoi += objDmTieuChuan.MaTieuChuan + "; ";
                            }
                            if (strNoiDungDoKiemTamThoi.Length > 2)
                                strNoiDungDoKiemTamThoi = strNoiDungDoKiemTamThoi.Substring(0, strNoiDungDoKiemTamThoi.Length - 2);
                            NOI_DUNG_DO_KIEM = strNoiDungDoKiemTamThoi.Trim();

                            if (!string.IsNullOrEmpty(objSanPhamDG.SoDoKiem))
                                KetQuaDoKiem += "- Số " + objSanPhamDG.SoDoKiem + " ";
                            if (objSanPhamDG.NgayDoKiem != null)
                            {
                                KetQuaDoKiem += "ngày " + objSanPhamDG.NgayDoKiem.Value.ToShortDateString() + " ";
                            }
                            if (!string.IsNullOrEmpty(CO_QUAN_DO_KIEM))
                                if (KetQuaDoKiem.Length > 0)
                                    KetQuaDoKiem += "của " + CO_QUAN_DO_KIEM;
                                else
                                    KetQuaDoKiem += CO_QUAN_DO_KIEM;

                            if (objSanPhamDG.NoiDungDoKiem != null)
                            {
                                if (KetQuaDoKiem.Length > 0)
                                    KetQuaDoKiem += "; " + objSanPhamDG.NoiDungDoKiem;
                                else
                                    KetQuaDoKiem += objSanPhamDG.NoiDungDoKiem;
                            }

                            NHAN_XET_KHAC = !string.IsNullOrEmpty(objSanPhamDG.NhanXetKhac) ? objSanPhamDG.NhanXetKhac : "";
                            if (objSanPhamDG.KetLuanId != null)
                            {
                                int KetLuanID = objSanPhamDG.KetLuanId.Value;
                                if (KetLuanID == Convert.ToInt32(EnKetLuanList.CAP_BAN_TIEP_NHAN_CB))
                                {
                                    CAP_BTN = "x";
                                    KetLuan = "Cấp Bản Tiếp nhận";
                                }
                                else if (KetLuanID == Convert.ToInt32(EnKetLuanList.KHONG_CAP_BTN_CB))
                                {
                                    KHONG_CAP_BTN = "x";
                                    KetLuan = "Không cấp Bản tiếp nhận";
                                }
                                else if (KetLuanID == Convert.ToInt32(EnKetLuanList.KHONG_PHAI_CBHQ))
                                {
                                    KHONG_PHAI_CN = "x";
                                    KetLuan = "Không phải công bố hợp quy";
                                }
                                else
                                {
                                    HUY_HO_SO = "x";
                                    KetLuan = "Huỷ hồ sơ";
                                }
                            }


                            SO_BanTiepNhan = !string.IsNullOrEmpty(objSanPhamDG.SoBanTiepNhanCb) ? objSanPhamDG.SoBanTiepNhanCb : "";
                            if (objSanPhamDG.HinhThucId != null)
                            {
                                int HinhThucID = objSanPhamDG.HinhThucId.Value;
                                if (HinhThucID == Convert.ToInt32(EnHinhThucList.TU_DANH_GIA))
                                {
                                    //Microsoft.Office.Interop.Word.Application ObjWord = new Microsoft.Office.Interop.Word.Application();
                                    //object falseValue = false;
                                    //object trueValue = true;
                                    //object missing = Type.Missing;
                                    //object saveobjections = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
                                    //object filepath = @"G:\hoang duc\Dot net\source\QuanLyChatLuong\Source\Document\Bieu mau\Mau GCN (mat sau).doc";
                                    //Microsoft.Office.Interop.Word.Document wrdoc;
                                    //wrdoc = ObjWord.Documents.Open(ref filepath, ref missing, ref trueValue, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                                    //Microsoft.Office.Interop.Word.Document wrdoc1 = ObjWord.ActiveDocument;
                                    //string m_Content = wrdoc1.Content.Text;
                                    //FILE_DINH_KEM = m_Content;
                                }
                            }

                            // Lấy số của giấy thông báo lệ phí 
                            TList<ThongBaoLePhiSanPham> lstTBLPSP = ProviderFactory.ThongBaoLePhiSanPhamProvider.GetBySanPhamId(objSanPhamDG.Id);

                            if (lstTBLPSP != null)
                            {
                                lstTBLPSP.Sort(" Stt ASC ");
                                if (lstTBLPSP.Count > 0)
                                {
                                    ThongBaoLePhiSanPham objTBLPSP = lstTBLPSP[0];
                                    if (objTBLPSP != null)
                                    {
                                        ThongBaoLePhi objTBLP = ProviderFactory.ThongBaoLePhiProvider.GetById(objTBLPSP.ThongBaoLePhiId);
                                        if (objTBLP != null)
                                            SoGiayThongBaoLePhi = objTBLP.SoGiayThongBaoLePhi;
                                    }
                                }
                            }

                            //Lấy datasource cho subreport
                            DataTable dtSubPhieuDanhGia = ProviderFactory.SanPhamProvider.GetDataForSubReportCN(objSanPhamDG.Id);
                            rd.Subreports[0].SetDataSource(dtSubPhieuDanhGia);

                        }
                        string NOI_CAP = "";
                        //Trung tam chung nhan
                        if (TrungTamObject.MaTrungTam.ToUpper() == "TTCN")
                        {
                            NOI_CAP = "Hà Nội, ngày ..... tháng ...... năm ...........";
                            if (!string.IsNullOrEmpty(strSubFix))
                                NOI_CAP = "Hà Nội," + strSubFix;
                        }
                        //Trung tam 2
                        else if (TrungTamObject.MaTrungTam.ToUpper() == "TT2")
                        {
                            NOI_CAP = "Tp. Hồ Chí Minh, ngày ..... tháng ...... năm ...........";
                            if (!string.IsNullOrEmpty(strSubFix))
                                NOI_CAP = "Tp. Hồ Chí Minh," + strSubFix;
                        }
                        //Trung tam 3
                        else if (TrungTamObject.MaTrungTam.ToUpper() == "TT3")
                        {
                            NOI_CAP = "Đà Nẵng, ngày ..... tháng ...... năm ...........";
                            if (!string.IsNullOrEmpty(strSubFix))
                                NOI_CAP = "Đà Nẵng," + strSubFix;
                        }
                        rd.ParameterFields["TEN_THAM_DINH"].CurrentValues.AddValue(TEN_THAM_DINH.ToUpper());
                        rd.ParameterFields["TEN_DANH_GIA"].CurrentValues.AddValue(TEN_DANH_GIA.ToUpper());
                        rd.ParameterFields["NOI_CAP"].CurrentValues.AddValue(NOI_CAP);
                        rd.ParameterFields["FILE_DINH_KEM"].CurrentValues.AddValue(FILE_DINH_KEM);
                        rd.ParameterFields["TEN_TRUNG_TAM"].CurrentValues.AddValue(TEN_TRUNG_TAM_TIENG_VIET.ToUpper());
                        rd.ParameterFields["NGUOI_LIEN_HE"].CurrentValues.AddValue(NGUOI_LIEN_HE);
                        rd.ParameterFields["DIEN_THOAI"].CurrentValues.AddValue(DIEN_THOAI);
                        rd.ParameterFields["TO_CHUC_CA_NHAN"].CurrentValues.AddValue(TO_CHUC_CA_NHAN);
                        rd.ParameterFields["SO_CONG_VAN_DEN"].CurrentValues.AddValue(SO_CONG_VAN_DEN);
                        rd.ParameterFields["TRUC_TIEP"].CurrentValues.AddValue(TRUC_TIEP);
                        rd.ParameterFields["BUU_DIEN"].CurrentValues.AddValue(BUU_DIEN);
                        rd.ParameterFields["TEN_SAN_PHAM"].CurrentValues.AddValue(TEN_SAN_PHAM);
                        rd.ParameterFields["KY_HIEU"].CurrentValues.AddValue(KY_HIEU);
                        rd.ParameterFields["HANG_SAN_XUAT"].CurrentValues.AddValue(HANG_SAN_XUAT);
                        rd.ParameterFields["MA_NHOM_SP"].CurrentValues.AddValue(MA_NHOM_SP);
                        rd.ParameterFields["NK_KEM_KQ_DO_KIEM"].CurrentValues.AddValue(NK_KEM_KQ_DO_KIEM);
                        rd.ParameterFields["NK_CHUA_DO_KIEM"].CurrentValues.AddValue(NK_CHUA_DO_KIEM);
                        rd.ParameterFields["SX_TRONG_NUOC_CO_ISO"].CurrentValues.AddValue(SX_TRONG_NUOC_CO_ISO);
                        rd.ParameterFields["SX_TRONG_NUOC_KHONG_CO_ISO"].CurrentValues.AddValue(SX_TRONG_NUOC_KHONG_CO_ISO);
                        rd.ParameterFields["HOP_LE"].CurrentValues.AddValue(HOP_LE);
                        rd.ParameterFields["KHONG_HOP_LE"].CurrentValues.AddValue(KHONG_HOP_LE);
                        rd.ParameterFields["DAY_DU"].CurrentValues.AddValue(DAY_DU);
                        rd.ParameterFields["KHONG_DAY_DU"].CurrentValues.AddValue(KHONG_DAY_DU);
                        rd.ParameterFields["SO_NGAY"].CurrentValues.AddValue(SO_NGAY);
                        rd.ParameterFields["CO_QUAN_DO_KIEM"].CurrentValues.AddValue(CO_QUAN_DO_KIEM);
                        rd.ParameterFields["NOI_DUNG_DO_KIEM"].CurrentValues.AddValue(NOI_DUNG_DO_KIEM);
                        rd.ParameterFields["NHAN_XET_KHAC"].CurrentValues.AddValue(NHAN_XET_KHAC);

                        rd.ParameterFields["CAP_BTN"].CurrentValues.AddValue(CAP_BTN);
                        rd.ParameterFields["KHONG_CAP_BTN"].CurrentValues.AddValue(KHONG_CAP_BTN);
                        rd.ParameterFields["KHONG_PHAI_CN"].CurrentValues.AddValue(KHONG_PHAI_CN);
                        rd.ParameterFields["THOI_HAN_HAI_NAM"].CurrentValues.AddValue(THOI_HAN_HAI_NAM);
                        rd.ParameterFields["THOI_HAN_BA_NAM"].CurrentValues.AddValue(THOI_HAN_BA_NAM);
                        rd.ParameterFields["THOI_HAN_KHAC"].CurrentValues.AddValue(THOI_HAN_KHAC);
                        rd.ParameterFields["LE_PHI_CN"].CurrentValues.AddValue(LE_PHI_CN);
                        rd.ParameterFields["DA_CAP_GCN"].CurrentValues.AddValue(DA_CAP_GCN);
                        rd.ParameterFields["TU_DANH_GIA"].CurrentValues.AddValue(TU_DANH_GIA);
                        rd.ParameterFields["HUY_HO_SO"].CurrentValues.AddValue(HUY_HO_SO);
                        rd.ParameterFields["SO_BanTiepNhan"].CurrentValues.AddValue(SO_BanTiepNhan == null ? string.Empty : SO_BanTiepNhan);
                        rd.ParameterFields["SoTuDanhGia_GCN"].CurrentValues.AddValue(SoTuDanhGia_GCN == null ? string.Empty : SoTuDanhGia_GCN);
                        rd.ParameterFields["NgayDanhGia_CN"].CurrentValues.AddValue(NgayDanhGia_CN == null ? string.Empty : NgayDanhGia_CN);
                        rd.ParameterFields["NhanHoSoTu"].CurrentValues.AddValue(strNhanHoSoTu);
                        rd.ParameterFields["HopLe_DayDu"].CurrentValues.AddValue(HopLe_DayDu);
                        rd.ParameterFields["HinhThucCongBo"].CurrentValues.AddValue(HinhThucCongBo);
                        rd.ParameterFields["NguonGoc"].CurrentValues.AddValue(NguonGoc);
                        rd.ParameterFields["KetLuan"].CurrentValues.AddValue(KetLuan);
                        rd.ParameterFields["KetQuaDoKiem"].CurrentValues.AddValue(KetQuaDoKiem);
                        rd.ParameterFields["SoGiayThongBaoLePhi"].CurrentValues.AddValue(SoGiayThongBaoLePhi);

                    }
                }
                break;
            #endregion

            #region In Thông báo nộp tiền lấy mẫu sản phẩm và đánh giá quy trình
            //Tham số:Session["HO_SO_ID"]
            case "ThongBaoNopTien":
                strduongdandenteploaiBC = Server.MapPath(@"~\Report\ThongBaoThuTien.rpt");
                rd = new ReportDocument();
                rd.Load(strduongdandenteploaiBC);
                if (rd != null)
                {
                    string TrungTamID = _mUserInfo.TrungTam.Id;
                    DmTrungTam TrungTamObject = ProviderFactory.DmTrungTamProvider.GetById(TrungTamID);
                    if (TrungTamObject == null)
                    {
                        rd = null;
                        break;
                    }
                    string ThongBaoId = string.Empty;
                    if (Request["ThongBaoLePhiId"] != null)
                        ThongBaoId = Request["ThongBaoLePhiId"].ToString();

                    // Khởi tạo đối tượng thông báo lệ phí
                    ThongBaoLePhi objThongBao = new ThongBaoLePhi();
                    if (ThongBaoId != string.Empty)
                        objThongBao = ProviderFactory.ThongBaoLePhiProvider.GetById(ThongBaoId);

                    string HoSoID = string.Empty;
                    if (objThongBao.HoSoId != null)
                        HoSoID = objThongBao.HoSoId; ;

                    string TenTrungTam = string.Empty;
                    string MaTrungTam = string.Empty;
                    string SoGiayThongBao = string.Empty;
                    string DiaChiTrungTam = string.Empty;
                    string DiaChiDonVi = string.Empty;
                    string TenDonVi = string.Empty;
                    string DienThoai = string.Empty;
                    string Fax = string.Empty;
                    int PhiDanhGia = 0;
                    int SoQuyTrinh = 0;
                    string TongTienBangSo = string.Empty;
                    string TongTienBangChu = string.Empty;
                    string TenDonViThuHuong = string.Empty;
                    string DiaChiDonViThuHuong = string.Empty;
                    string SoTaiKhoan = string.Empty;
                    string TenNganHang = string.Empty;
                    string ChucVu = string.Empty;
                    string GiamDoc = string.Empty;
                    string TinhThanh = string.Empty;
                    string SoPhieuNhanHS = string.Empty;
                    string NgayTiepNhan = string.Empty;
                    string Line_PhiLayMau = string.Empty;
                    string Line_PhiDanhGia = string.Empty;
                    string MaSoThue = string.Empty;
                    string NgayThang = string.Empty;

                    HoSo hoso = ProviderFactory.HoSoProvider.GetById(HoSoID);
                    if (hoso != null)
                    {
                        SoPhieuNhanHS = hoso.SoHoSo;
                        NgayTiepNhan = Convert.ToDateTime(hoso.NgayTiepNhan).ToShortDateString();

                        // Lấy thông tin phí
                        if (hoso.NguonGocId != (int)EnNguonGocList.NK_KEM_KQ_DO_KIEM)
                            PhiLayMauSanPham = Convert.ToInt32(ConfigurationManager.AppSettings["PhiLayMau"].ToString());
                        if (hoso.NguonGocId == (int)EnNguonGocList.SX_TRONG_NUOC_KHONG_CO_ISO)
                            PhiDanhGiaQuyTrinhQLCL = Convert.ToInt32(ConfigurationManager.AppSettings["PhiDanhGia"].ToString());

                        if (objThongBao.Id != null)
                        {
                            TList<ThongBaoLePhiChiTiet> lstChiTiet = ProviderFactory.ThongBaoLePhiChiTietProvider.GetByThongBaoLePhiId(ThongBaoId);
                            if (lstChiTiet.Count == 2)
                            {
                                ThongBaoLePhiChiTiet objPhiLayMau = Convert.ToInt32(lstChiTiet[0].LoaiPhiId) == (int)EnLoaiPhiList.PHI_LAY_MAU_SAN_PHAM ? lstChiTiet[0] : lstChiTiet[1];
                                ThongBaoLePhiChiTiet objphiDanhGia = Convert.ToInt32(lstChiTiet[1].LoaiPhiId) == (int)EnLoaiPhiList.PHI_DANH_GIA_QUY_TRINH_SX ? lstChiTiet[1] : lstChiTiet[0];
                                SoQuyTrinh = objphiDanhGia.SoLuong.Value;
                            }
                            PhiDanhGia = SoQuyTrinh * PhiDanhGiaQuyTrinhQLCL;
                        }
                        else
                        {
                            SoQuyTrinh = 0;
                            PhiDanhGia = 0;
                        }
                        int tongtien = PhiDanhGia + PhiLayMauSanPham;
                        TongTienBangSo = FormatCurency(long.Parse(tongtien.ToString()));
                        NumberUtil objTalkNumber = new NumberUtil();
                        TongTienBangChu = objTalkNumber.Speak(tongtien);
                        if (PhiLayMauSanPham > 0)
                            Line_PhiLayMau = "-   Chi phí lấy mẫu sản phẩm: " + FormatCurency(long.Parse(PhiLayMauSanPham.ToString())) + "đ/Lần";
                        if (PhiDanhGia > 0)
                        {
                            Line_PhiDanhGia = "-   Chi phí đánh giá quy trình SX, đảm bảo CLSP: "
                                                + FormatCurency(long.Parse(PhiDanhGia.ToString())) + "đ/" + SoQuyTrinh + "Quy trình";
                        }
                    }
                    else
                    {
                        rd = null;
                        break;
                    }

                    // Lấy thông tin theo trạng thái
                    if (hoso.TrangThaiId == (int)EnTrangThaiHoSoList.DA_DONG || hoso.TrangThaiId == (int)EnTrangThaiHoSoList.CHO_LUU_TRU)
                    {
                        TenTrungTam = hoso.DmTrungTamTenTrungTam;
                        string TinhThanhId = hoso.DmTrungTamTinhThanhId;
                        TinhThanh = ProviderFactory.DmTinhThanhProvider.GetById(TinhThanhId).TenTinhThanh;
                        TenDonViThuHuong = hoso.DmTrungTamTenCoQuanThuHuongCuc;
                        DiaChiDonViThuHuong = hoso.DmTrungTamDiaChiCoQuanThuHuongCuc;
                        SoTaiKhoan = hoso.DmTrungTamSoTaiKhoanCuc;
                        TenNganHang = hoso.DmTrungTamTenKhoBacCuc;
                        NgayThang = TinhThanh + "," + TalkDate(DateTime.Now);
                        TenDonVi = hoso.DmDonViTenTiengViet;
                        DiaChiDonVi = hoso.DmDonViDiaChi;
                        DienThoai = hoso.DmDonViDienThoai;
                        Fax = hoso.DmDonViFax;
                        MaSoThue = hoso.DmDonViMaSoThue;
                        GiamDoc = objThongBao.TenNguoiKyDuyet;
                        ChucVu = objThongBao.ChucVu.ToString();
                        switch (ChucVu)
                        {
                            case "1":
                                ChucVu = "GIÁM ĐỐC";
                                break;
                            case "2":
                                ChucVu = "TUQ. GIÁM ĐỐC" + "\n" + "PHÓ GIÁM ĐỐC";
                                break;
                            case "3":
                                ChucVu = "TUQ. GIÁM ĐỐC" + "\n" + "TRƯỞNG PHÒNG CHỨNG NHẬN";
                                break;
                            case "4":
                                ChucVu = "TUQ. GIÁM ĐỐC" + "\n" + "PHÓ PHÒNG CHỨNG NHẬN";
                                break;
                            case "5":
                                ChucVu = "CHUYÊN VIÊN";
                                break;
                            case "6":
                                ChucVu = "CỤC TRƯỞNG ";
                                break;
                            case "7":
                                ChucVu = "KT. CỤC TRƯỞNG" + "\n" + "PHÓ CỤC TRƯỞNG ";
                                break;
                        }
                    }
                    else
                    {
                        string DonViID = hoso.DonViId;
                        DmDonVi donvi = ProviderFactory.DmDonViProvider.GetById(DonViID);
                        if (donvi != null)
                        {
                            TenDonVi = donvi.TenTiengViet.Trim();
                            DiaChiDonVi = donvi.DiaChi;
                            DienThoai = donvi.DienThoai;
                            Fax = donvi.Fax;
                            MaSoThue = donvi.MaSoThue;
                        }

                        DmTrungTam objDmTrungTam = ProviderFactory.DmTrungTamProvider.GetById(hoso.TrungTamId);
                        if (objDmTrungTam != null)
                        {
                            TinhThanh = ProviderFactory.DmTinhThanhProvider.GetById(objDmTrungTam.TinhThanhId).TenTinhThanh;
                            NgayThang = TinhThanh + "," + TalkDate(DateTime.Now);
                        }

                        //Trung tam chung nhan
                        if (objDmTrungTam != null)
                        {
                            TenTrungTam = objDmTrungTam.TenTrungTam;
                            TenDonViThuHuong = objDmTrungTam.TenCoQuanThuHuong;
                            DiaChiDonViThuHuong = objDmTrungTam.DiaChiCoQuanThuHuong;
                            SoTaiKhoan = objDmTrungTam.SoTaiKhoan;
                            TenNganHang = objDmTrungTam.TenKhoBac;
                        }

                        string NguoiPheDuyetId = objThongBao.NguoiPheDuyetId != null ? objThongBao.NguoiPheDuyetId : string.Empty;
                        if (NguoiPheDuyetId == string.Empty)
                            NguoiPheDuyetId = mUserInfo.GiamDoc.Id;
                        SysUser oUser = ProviderFactory.SysUserProvider.GetById(NguoiPheDuyetId);
                        GiamDoc = !string.IsNullOrEmpty(oUser.FullName) ? oUser.FullName : "";
                        ChucVu = !string.IsNullOrEmpty(oUser.Position) ? oUser.Position : "";
                        switch (ChucVu)
                        {
                            case "1":
                                ChucVu = "GIÁM ĐỐC";
                                break;
                            case "2":
                                ChucVu = "TUQ. GIÁM ĐỐC" + "\n" + "PHÓ GIÁM ĐỐC";
                                break;
                            case "3":
                                ChucVu = "TUQ. GIÁM ĐỐC" + "\n" + "TRƯỞNG PHÒNG CHỨNG NHẬN";
                                break;
                            case "4":
                                ChucVu = "TUQ. GIÁM ĐỐC" + "\n" + "PHÓ PHÒNG CHỨNG NHẬN";
                                break;
                            case "5":
                                ChucVu = "CHUYÊN VIÊN";
                                break;
                            case "6":
                                ChucVu = "CỤC TRƯỞNG ";
                                break;
                            case "7":
                                ChucVu = "KT. CỤC TRƯỞNG" + "\n" + "PHÓ CỤC TRƯỞNG ";
                                break;
                        }
                    }

                    // Truyền giá trị cho các ParameterFields
                    rd.ParameterFields["TenTrungTam"].CurrentValues.AddValue(TenTrungTam.ToUpper());
                    rd.ParameterFields["SoGiayThongBao"].CurrentValues.AddValue(objThongBao.SoGiayThongBaoLePhi);
                    rd.ParameterFields["DiaChiTrungTam"].CurrentValues.AddValue(DiaChiTrungTam);
                    rd.ParameterFields["DiaChiDonVi"].CurrentValues.AddValue(DiaChiDonVi);
                    rd.ParameterFields["TenDonVi"].CurrentValues.AddValue(TenDonVi);
                    rd.ParameterFields["DienThoai"].CurrentValues.AddValue(DienThoai);
                    rd.ParameterFields["Fax"].CurrentValues.AddValue(Fax);
                    rd.ParameterFields["TongTienBangSo"].CurrentValues.AddValue(TongTienBangSo);
                    rd.ParameterFields["TongTienBangChu"].CurrentValues.AddValue(TongTienBangChu);
                    rd.ParameterFields["TenDonViThuHuong"].CurrentValues.AddValue(TenDonViThuHuong);
                    rd.ParameterFields["DiaChiDonViThuHuong"].CurrentValues.AddValue(DiaChiDonViThuHuong);
                    rd.ParameterFields["SoTaiKhoan"].CurrentValues.AddValue(SoTaiKhoan);
                    rd.ParameterFields["TenNganHang"].CurrentValues.AddValue(TenNganHang);
                    rd.ParameterFields["ChucVu"].CurrentValues.AddValue(ChucVu);
                    rd.ParameterFields["HoTen"].CurrentValues.AddValue(GiamDoc);
                    rd.ParameterFields["NgayThang"].CurrentValues.AddValue(NgayThang);
                    rd.ParameterFields["SoPhieuNhanHS"].CurrentValues.AddValue(SoPhieuNhanHS);
                    rd.ParameterFields["Line_PhiLayMau"].CurrentValues.AddValue(Line_PhiLayMau);
                    rd.ParameterFields["Line_PhiDanhGiaQuyTrinh"].CurrentValues.AddValue(Line_PhiDanhGia);
                    rd.ParameterFields["NgayTiepNhan"].CurrentValues.AddValue(NgayTiepNhan);
                    rd.ParameterFields["MaSoThue"].CurrentValues.AddValue(MaSoThue != null ? MaSoThue : string.Empty);

                    if (SoQuyTrinh > 0)
                        rd.ParameterFields["DanhGiaQuyTrinh"].CurrentValues.AddValue("và đánh giá quy trình sản xuất, đảm bảo chất lượng sản phẩm ");
                    else
                        rd.ParameterFields["DanhGiaQuyTrinh"].CurrentValues.AddValue("");
                }
                break;
            #endregion
            #region In Thông báo nộp tiền lấy mẫu sản phẩm và đánh giá quy trình - CNHQ _ LongHH
            //Tham số:Session["HO_SO_ID"]
            case "ThuPhiDanhGiaQTSX":
                strduongdandenteploaiBC = Server.MapPath(@"~\Report\ThuPhiDanhGiaQTSX.rpt");
                rd = new ReportDocument();
                rd.Load(strduongdandenteploaiBC);
                rd.PrintOptions.PaperSize = PaperSize.PaperA4;
                if (rd != null)
                {
                    string TrungTamID = _mUserInfo.TrungTam.Id;
                    DmTrungTam TrungTamObject = ProviderFactory.DmTrungTamProvider.GetById(TrungTamID);
                    if (TrungTamObject == null)
                    {
                        rd = null;
                        break;
                    }
                    string ThongBaoId = string.Empty;
                    if (Request["ThongBaoLePhiId"] != null)
                        ThongBaoId = Request["ThongBaoLePhiId"].ToString();

                    // Khởi tạo đối tượng thông báo lệ phí
                    ThongBaoLePhi objThongBao = new ThongBaoLePhi();
                    if (ThongBaoId != string.Empty)
                        objThongBao = ProviderFactory.ThongBaoLePhiProvider.GetById(ThongBaoId);

                    string HoSoID = string.Empty;
                    if (objThongBao.HoSoId != null)
                        HoSoID = objThongBao.HoSoId; ;

                    string TenTrungTam = string.Empty;
                    string MaTrungTam = string.Empty;
                    string SoGiayThongBao = string.Empty;
                    string DiaChiTrungTam = string.Empty;
                    string DiaChiDonVi = string.Empty;
                    string TenDonVi = string.Empty;
                    string DienThoai = string.Empty;
                    string Fax = string.Empty;
                    int SoQuyTrinh = 0;
                    int SoLayMau = 0;
                    string TongTienBangSo = string.Empty;
                    string TongTienBangChu = string.Empty;
                    string TenDonViThuHuong = string.Empty;
                    string DiaChiDonViThuHuong = string.Empty;
                    string SoTaiKhoan = string.Empty;
                    string TenNganHang = string.Empty;
                    string ChucVu = string.Empty;
                    string GiamDoc = string.Empty;
                    string TinhThanh = string.Empty;
                    string SoPhieuNhanHS = string.Empty;
                    string NgayTiepNhan = string.Empty;
                    string Line_PhiLayMau = string.Empty;
                    string Line_PhiDanhGia = string.Empty;
                    string MaSoThue = string.Empty;
                    string NgayThang = string.Empty;
                    ///////////////////////////
                    
                    string TinhThanhTrungTam = ProviderFactory.DmTinhThanhProvider.GetById(TrungTamObject.TinhThanhId).TenTinhThanh;
                    string NgayPheDuyet = objThongBao.NgayPheDuyet.HasValue ?TalkDate(objThongBao.NgayPheDuyet.Value) : TalkDate(objThongBao.NgayCapNhatSauCung.Value);
                    string Email = string.Empty;
                    //string HoTenNguoiKy = string.Empty;
                    NumberUtil objTalkNumber = new NumberUtil();
                    ///////////////////////////////////////////////////
                    HoSo hoso = ProviderFactory.HoSoProvider.GetById(HoSoID);
                    if (hoso != null)
                    {
                        SoPhieuNhanHS = hoso.SoHoSo;
                        NgayTiepNhan = Convert.ToDateTime(hoso.NgayTiepNhan).ToShortDateString();
                        //NgayPheDuyet = TalkDate(objThongBao.NgayPheDuyet.Value); 
                        // Lấy thông tin phí
                        //if (hoso.NguonGocId != (int)EnNguonGocList.NK_KEM_KQ_DO_KIEM)
                            PhiLayMauSanPham = ((int)QLCL_Patch.LePhi.PhiLayMauSanPham * 1000);
                        //if (hoso.NguonGocId == (int)EnNguonGocList.SX_TRONG_NUOC_KHONG_CO_ISO)
                            PhiDanhGiaQuyTrinhQLCL = ((int)QLCL_Patch.LePhi.PhiDanhGiaQuaTrinhSanXuat * 1000);

                        if (objThongBao.Id != null)
                        {
                            TList<ThongBaoLePhiChiTiet> lstChiTiet = ProviderFactory.ThongBaoLePhiChiTietProvider.GetByThongBaoLePhiId(ThongBaoId);
                            if (lstChiTiet.Count == 2) {
                                ThongBaoLePhiChiTiet tbct_DanhGia = Convert.ToInt32(lstChiTiet[0].LoaiPhiId) == (int)QLCL_Patch.LoaiPhi.DanhGiaQTSX ? lstChiTiet[0] : lstChiTiet[1];
                                ThongBaoLePhiChiTiet tbct_LayMau = Convert.ToInt32(lstChiTiet[0].LoaiPhiId) == (int)QLCL_Patch.LoaiPhi.LayMauQTSX ? lstChiTiet[0] : lstChiTiet[1];
                                if (tbct_DanhGia != null)
                                {
                                    SoQuyTrinh = tbct_DanhGia.SoLuong.Value;
                                }


                                if (tbct_LayMau != null)
                                {
                                    SoLayMau = tbct_LayMau.SoLuong.Value;
                                }
                            }
                            
                        }
                        else
                        {
                            SoQuyTrinh = 0;
                            SoLayMau = 0;
                        }
                        int tongtien = PhiLayMauSanPham * SoLayMau + PhiDanhGiaQuyTrinhQLCL * SoQuyTrinh;
                        TongTienBangSo = FormatCurency(long.Parse(tongtien.ToString()));
                        
                        TongTienBangChu = objTalkNumber.Speak(tongtien);

                        //if (PhiLayMauSanPham > 0)
                        //    Line_PhiLayMau = "-   Chi phí lấy mẫu sản phẩm: " + FormatCurency(long.Parse(PhiLayMauSanPham.ToString())) + "đ/Lần";
                        //if (PhiDanhGia > 0)
                        //{
                        //    Line_PhiDanhGia = "-   Chi phí đánh giá quy trình SX, đảm bảo CLSP: "
                        //                        + FormatCurency(long.Parse(PhiDanhGia.ToString())) + "đ/" + SoQuyTrinh + "Quy trình";
                        //}
                    }
                    else
                    {
                        rd = null;
                        break;
                    }

                    // Lấy thông tin theo trạng thái
                    if (hoso.TrangThaiId == (int)EnTrangThaiHoSoList.DA_DONG || hoso.TrangThaiId == (int)EnTrangThaiHoSoList.CHO_LUU_TRU)
                    {
                        TenTrungTam = hoso.DmTrungTamTenTrungTam;
                        string TinhThanhId = hoso.DmTrungTamTinhThanhId;
                        TinhThanh = ProviderFactory.DmTinhThanhProvider.GetById(TinhThanhId).TenTinhThanh;
                        TenDonViThuHuong = hoso.DmTrungTamTenCoQuanThuHuongCuc;
                        DiaChiDonViThuHuong = hoso.DmTrungTamDiaChiCoQuanThuHuongCuc;
                        SoTaiKhoan = hoso.DmTrungTamSoTaiKhoanCuc;
                        TenNganHang = hoso.DmTrungTamTenKhoBacCuc;
                        NgayThang = TinhThanh + "," + TalkDate(DateTime.Now);
                        TenDonVi = hoso.DmDonViTenTiengViet;
                        DiaChiDonVi = hoso.DmDonViDiaChi;
                        DienThoai = hoso.DmDonViDienThoai;
                        Fax = hoso.DmDonViFax;
                        MaSoThue = hoso.DmDonViMaSoThue;
                        GiamDoc = objThongBao.TenNguoiKyDuyet;
                        ChucVu = objThongBao.ChucVu.ToString();
                        switch (ChucVu)
                        {
                            case "1":
                                ChucVu = "GIÁM ĐỐC";
                                break;
                            case "2":
                                ChucVu = "TUQ. GIÁM ĐỐC" + "\n" + "PHÓ GIÁM ĐỐC";
                                break;
                            case "3":
                                ChucVu = "TUQ. GIÁM ĐỐC" + "\n" + "TRƯỞNG PHÒNG CHỨNG NHẬN";
                                break;
                            case "4":
                                ChucVu = "TUQ. GIÁM ĐỐC" + "\n" + "PHÓ PHÒNG CHỨNG NHẬN";
                                break;
                            case "5":
                                ChucVu = "CHUYÊN VIÊN";
                                break;
                            case "6":
                                ChucVu = "CỤC TRƯỞNG ";
                                break;
                            case "7":
                                ChucVu = "KT. CỤC TRƯỞNG" + "\n" + "PHÓ CỤC TRƯỞNG ";
                                break;
                        }
                    }
                    else
                    {
                        string DonViID = hoso.DonViId;
                        DmDonVi donvi = ProviderFactory.DmDonViProvider.GetById(DonViID);
                        if (donvi != null)
                        {
                            TenDonVi = donvi.TenTiengViet.Trim();
                            DiaChiDonVi = donvi.DiaChi;
                            DienThoai = donvi.DienThoai;
                            Fax = donvi.Fax;
                            MaSoThue = donvi.MaSoThue;
                            Email = donvi.Email;
                        }

                        DmTrungTam objDmTrungTam = ProviderFactory.DmTrungTamProvider.GetById(hoso.TrungTamId);
                        if (objDmTrungTam != null)
                        {
                            TinhThanh = ProviderFactory.DmTinhThanhProvider.GetById(objDmTrungTam.TinhThanhId).TenTinhThanh;
                            NgayThang = TinhThanh + "," + TalkDate(DateTime.Now);
                        }

                        //Trung tam chung nhan
                        if (objDmTrungTam != null)
                        {
                            TenTrungTam = objDmTrungTam.TenTrungTam;
                            TenDonViThuHuong = objDmTrungTam.TenCoQuanThuHuong;
                            DiaChiDonViThuHuong = objDmTrungTam.DiaChiCoQuanThuHuong;
                            SoTaiKhoan = objDmTrungTam.SoTaiKhoan;
                            TenNganHang = objDmTrungTam.TenKhoBac;
                        }

                        string NguoiPheDuyetId = objThongBao.NguoiPheDuyetId != null ? objThongBao.NguoiPheDuyetId : string.Empty;
                        if (NguoiPheDuyetId == string.Empty)
                            NguoiPheDuyetId = mUserInfo.GiamDoc.Id;
                        SysUser oUser = ProviderFactory.SysUserProvider.GetById(NguoiPheDuyetId);
                        GiamDoc = !string.IsNullOrEmpty(oUser.FullName) ? oUser.FullName : "";
                        ChucVu = !string.IsNullOrEmpty(oUser.Position) ? oUser.Position : "";
                        switch (ChucVu)
                        {
                            case "1":
                                ChucVu = "GIÁM ĐỐC";
                                break;
                            case "2":
                                ChucVu = "TUQ. GIÁM ĐỐC" + "\n" + "PHÓ GIÁM ĐỐC";
                                break;
                            case "3":
                                ChucVu = "TUQ. GIÁM ĐỐC" + "\n" + "TRƯỞNG PHÒNG CHỨNG NHẬN";
                                break;
                            case "4":
                                ChucVu = "TUQ. GIÁM ĐỐC" + "\n" + "PHÓ PHÒNG CHỨNG NHẬN";
                                break;
                            case "5":
                                ChucVu = "CHUYÊN VIÊN";
                                break;
                            case "6":
                                ChucVu = "CỤC TRƯỞNG ";
                                break;
                            case "7":
                                ChucVu = "KT. CỤC TRƯỞNG" + "\n" + "PHÓ CỤC TRƯỞNG ";
                                break;
                        }
                    }

                    long CongTien = PhiLayMauSanPham * SoLayMau + PhiDanhGiaQuyTrinhQLCL * SoQuyTrinh;
                    long VAT = CongTien / 10;
                    long TongTien = CongTien + VAT;
                    string BangChu = objTalkNumber.Speak(TongTien * 1000);
                    String TenKhoBac = "Ngân hàng Thương mại Cổ phần Á châu - Hội sở";// Sua theo yeu cau

                    ////////////////////////////////////////////////
                    DataTable returnTable = new DataTable();
                    returnTable.TableName = "LePhiTiepNhanCNHQ";
                    returnTable.Columns.Add("STT");
                    returnTable.Columns.Add("LoaiDichVu");
                    returnTable.Columns.Add("DonGia");
                    returnTable.Columns.Add("SoLuong");
                    returnTable.Columns.Add("ThanhTien");

                    DataRow row1 = returnTable.NewRow();
                    row1["STT"] = "1";
                    row1["LoaiDichVu"] = "Phí đánh giá quá trình sản xuất";
                    row1["DonGia"] = string.Format("{0:0,0}.000/ quá trình sản xuất", (int)QLCL_Patch.LePhi.PhiDanhGiaQuaTrinhSanXuat);
                    row1["SoLuong"] = SoQuyTrinh;
                    row1["ThanhTien"] = string.Format("{0:0,0}.000", SoQuyTrinh * PhiDanhGiaQuyTrinhQLCL);
                    returnTable.Rows.Add(row1);
                    DataRow row2 = returnTable.NewRow();
                    row2["STT"] = "2";
                    row2["LoaiDichVu"] = "Phí lấy mẫu sản phẩm";
                    row2["DonGia"] = string.Format("{0:0,0}.000/ chủng loại sản phẩm", (int)QLCL_Patch.LePhi.PhiLayMauSanPham);
                    row2["SoLuong"] = SoLayMau;
                    row2["ThanhTien"] = string.Format("{0:0,0}.000", SoLayMau * PhiLayMauSanPham);
                    returnTable.Rows.Add(row2);

                    rd.SetDataSource(returnTable);
                    rd.Refresh();

                    rd.SetParameterValue("SoGiayThongBaoLePhi", objThongBao.SoGiayThongBaoLePhi);
                    rd.SetParameterValue("TinhThanhTrungTam", TinhThanhTrungTam);
                    rd.SetParameterValue("TenTrungTam", TenTrungTam.ToUpper());
                    rd.SetParameterValue("SoHoSo", SoPhieuNhanHS);
                    rd.SetParameterValue("NgayPheDuyet", NgayPheDuyet);
                    rd.SetParameterValue("TenTiengViet", TenDonVi);
                    rd.SetParameterValue("DiaChi", DiaChiDonVi);
                    rd.SetParameterValue("DienThoai", DienThoai);
                    rd.SetParameterValue("Fax", Fax);
                    rd.SetParameterValue("MaSoThue", MaSoThue);
                    rd.SetParameterValue("Email", Email);
                    rd.SetParameterValue("NgayPheDuyetShort", NgayTiepNhan);
                    rd.SetParameterValue("CongTien", string.Format("{0:0,0}.000", CongTien));
                    rd.SetParameterValue("VAT", string.Format("{0:0,0}.000", VAT));
                    rd.SetParameterValue("TongTien", string.Format("{0:0,0}.000", TongTien));
                    rd.SetParameterValue("BangChu", BangChu);
                    rd.SetParameterValue("DVThuHuongDiaChi", TrungTamObject.DiaChiCoQuanThuHuong);
                    rd.SetParameterValue("SoTaiKhoan", TrungTamObject.SoTaiKhoan);
                    rd.SetParameterValue("TenKhoBac", TenKhoBac);
                    rd.SetParameterValue("HoTenNguoiKy", GiamDoc);
                    rd.SetParameterValue("ChucVu", ChucVu);

                    //// Truyền giá trị cho các ParameterFields
                    //rd.ParameterFields["TenTrungTam"].CurrentValues.AddValue(TenTrungTam.ToUpper());
                    //rd.ParameterFields["SoGiayThongBao"].CurrentValues.AddValue(objThongBao.SoGiayThongBaoLePhi);
                    //rd.ParameterFields["DiaChiTrungTam"].CurrentValues.AddValue(DiaChiTrungTam);
                    //rd.ParameterFields["DiaChiDonVi"].CurrentValues.AddValue(DiaChiDonVi);
                    //rd.ParameterFields["TenDonVi"].CurrentValues.AddValue(TenDonVi);
                    //rd.ParameterFields["DienThoai"].CurrentValues.AddValue(DienThoai);
                    //rd.ParameterFields["Fax"].CurrentValues.AddValue(Fax);
                    //rd.ParameterFields["TongTienBangSo"].CurrentValues.AddValue(TongTienBangSo);
                    //rd.ParameterFields["TongTienBangChu"].CurrentValues.AddValue(TongTienBangChu);
                    //rd.ParameterFields["TenDonViThuHuong"].CurrentValues.AddValue(TenDonViThuHuong);
                    //rd.ParameterFields["DiaChiDonViThuHuong"].CurrentValues.AddValue(DiaChiDonViThuHuong);
                    //rd.ParameterFields["SoTaiKhoan"].CurrentValues.AddValue(SoTaiKhoan);
                    //rd.ParameterFields["TenNganHang"].CurrentValues.AddValue(TenNganHang);
                    //rd.ParameterFields["ChucVu"].CurrentValues.AddValue(ChucVu);
                    //rd.ParameterFields["HoTen"].CurrentValues.AddValue(GiamDoc);
                    //rd.ParameterFields["NgayThang"].CurrentValues.AddValue(NgayThang);
                    //rd.ParameterFields["SoPhieuNhanHS"].CurrentValues.AddValue(SoPhieuNhanHS);
                    //rd.ParameterFields["Line_PhiLayMau"].CurrentValues.AddValue(Line_PhiLayMau);
                    //rd.ParameterFields["Line_PhiDanhGiaQuyTrinh"].CurrentValues.AddValue(Line_PhiDanhGia);
                    //rd.ParameterFields["NgayTiepNhan"].CurrentValues.AddValue(NgayTiepNhan);
                    //rd.ParameterFields["MaSoThue"].CurrentValues.AddValue(MaSoThue != null ? MaSoThue : string.Empty);

                    //if (SoQuyTrinh > 0)
                    //    rd.ParameterFields["DanhGiaQuyTrinh"].CurrentValues.AddValue("và đánh giá quy trình sản xuất, đảm bảo chất lượng sản phẩm ");
                    //else
                    //    rd.ParameterFields["DanhGiaQuyTrinh"].CurrentValues.AddValue("");
                }
                break;
            #endregion
            #region In biên lai
            case "ThongBaoNopTienGS":
                ThongBaoNopTienGiamSat();
                break;
            case "InBienLai":
                InBienLai();
                break;
                #endregion
        }
        #endregion Các mẫu in

        #region Xuất dữ liệu
        if ((format == "") || (format == null))
            format = "CR";
        if (rd == null)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "TaiLaiTrangChinh", "alert('Không có dữ liệu cho báo cáo!');", true);
            return;
        }
        try
        {
            if (format == "Excel")
            {
                rd.ExportToHttpResponse(ExportFormatType.Excel, Response, true, strTenBaoCao);
            }
            else if (format == "Word")
            {
                rd.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, true, strTenBaoCao);
            }
            else if (format == "PDF")
            {
                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, strTenBaoCao);
            }
            else if (format == "HTML")
            {
                string strHTMLFile = Server.MapPath(@"~\Upload\Exported.htm");
                rd.ExportToDisk(ExportFormatType.HTML40, strHTMLFile);
                Response.Redirect(@"~\Upload\Exported.htm");
            }

            if (format != "CR")
            {
                Response.Flush();
                Response.Close();
            }

            if (format != "HTML")
            {
                this.hienBC.ReportSource = rd;
                this.hienBC.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        #endregion
    }
    /// <summary>
    /// Hiển thị báo cáo thông báo nộp tiền giám sát
    /// </summary>
    /// <Modified>
    ///Author           Date                Comment 
    ///Hoàng Văn Út     8/1/2011            Tạo mới
    ///
    ///</Modified>
    private void ThongBaoNopTienGiamSat()
    {
        string strBC = Server.MapPath(@"~\Report\ThongBaoNopTien_GiamSat.rpt");
        rd = new ReportDocument();
        rd.Load(strBC);
        if (Request["LePhiID"] != null)
        {

            DmTrungTam TrungTamObject = ProviderFactory.DmTrungTamProvider.GetById(mUserInfo.TrungTam.Id);
            ThongBaoLePhi objThongBao = ProviderFactory.ThongBaoLePhiProvider.GetById(Request["LePhiID"].ToString());
            DataTable dt = ProviderFactory.ThongBaoLePhiSanPhamProvider.GetListSPByThongBaoLePhiID(objThongBao.Id);
            HoSo hoso = ProviderFactory.HoSoProvider.GetById(objThongBao.HoSoId);
            int PhiDanhGia = 0;
            int SoQuyTrinh = 1;
            string TongTienBangSo = string.Empty;
            string TongTienBangChu = string.Empty;
            string ChucVu = string.Empty;
            string TinhThanh = string.Empty;
            string MaSoThue = string.Empty;
            string NgayThang = string.Empty;
            // Lấy thông tin phí
            if (hoso.NguonGocId != (int)EnNguonGocList.NK_KEM_KQ_DO_KIEM)
                PhiLayMauSanPham = Convert.ToInt32(ConfigurationManager.AppSettings["PhiLayMau"].ToString());
            if (hoso.NguonGocId == (int)EnNguonGocList.SX_TRONG_NUOC_KHONG_CO_ISO)
                PhiDanhGiaQuyTrinhQLCL = Convert.ToInt32(ConfigurationManager.AppSettings["PhiDanhGia"].ToString());

            if (objThongBao.Id != null)
            {
                TList<ThongBaoLePhiChiTiet> lstChiTiet = ProviderFactory.ThongBaoLePhiChiTietProvider.GetByThongBaoLePhiId(objThongBao.Id);
                if (lstChiTiet.Count == 2)
                {
                    ThongBaoLePhiChiTiet objPhiLayMau = Convert.ToInt32(lstChiTiet[0].LoaiPhiId) == (int)EnLoaiPhiList.PHI_LAY_MAU_SAN_PHAM_DGL ? lstChiTiet[0] : lstChiTiet[1];
                    ThongBaoLePhiChiTiet objphiDanhGia = Convert.ToInt32(lstChiTiet[1].LoaiPhiId) == (int)EnLoaiPhiList.PHI_DANH_GIA_LAI_QUY_TRINH_SX ? lstChiTiet[1] : lstChiTiet[0];
                    SoQuyTrinh = objphiDanhGia.SoLuong.Value;
                }
                PhiDanhGia = SoQuyTrinh * PhiDanhGiaQuyTrinhQLCL;
            }
            else
            {
                SoQuyTrinh = 0;
                PhiDanhGia = 0;
            }
            int tongtien = PhiDanhGia + PhiLayMauSanPham;
            if (dt.Rows.Count > 0)
            {
                tongtien += Convert.ToInt32(dt.Compute("sum(MucThu)", ""));
            }


            TongTienBangSo = FormatCurency(long.Parse(tongtien.ToString()));
            NumberUtil objTalkNumber = new NumberUtil();
            TongTienBangChu = objTalkNumber.Speak(tongtien);

            string TinhThanhId = hoso.DmTrungTamTinhThanhId;
            TinhThanh = ProviderFactory.DmTinhThanhProvider.GetById(TinhThanhId).TenTinhThanh;
            NgayThang = TinhThanh + "," + TalkDate(DateTime.Now);
            ChucVu = objThongBao.ChucVu.ToString();
            switch (ChucVu)
            {
                case "1":
                    ChucVu = "GIÁM ĐỐC";
                    break;
                case "2":
                    ChucVu = "TUQ. GIÁM ĐỐC" + "\n" + "PHÓ GIÁM ĐỐC";
                    break;
                case "3":
                    ChucVu = "TUQ. GIÁM ĐỐC" + "\n" + "TRƯỞNG PHÒNG CHỨNG NHẬN";
                    break;
                case "4":
                    ChucVu = "TUQ. GIÁM ĐỐC" + "\n" + "PHÓ PHÒNG CHỨNG NHẬN";
                    break;
                case "5":
                    ChucVu = "CHUYÊN VIÊN";
                    break;
                case "6":
                    ChucVu = "CỤC TRƯỞNG ";
                    break;
                case "7":
                    ChucVu = "KT. CỤC TRƯỞNG" + "\n" + "PHÓ CỤC TRƯỞNG ";
                    break;
            }


            rd.SetDataSource(dt);
            // Thông tin hồ sơ
            rd.ParameterFields["SoHoSo"].CurrentValues.AddValue(hoso.SoHoSo);
            rd.ParameterFields["NgayNop"].CurrentValues.AddValue(hoso.NgayTiepNhan.Value.ToShortDateString());
            // Thông tin tên trung tâm và trung tâm thụ hưởng
            rd.ParameterFields["TenTrungTam"].CurrentValues.AddValue(hoso.DmTrungTamTenTrungTam);
            rd.ParameterFields["SoThongBao"].CurrentValues.AddValue(objThongBao.SoGiayThongBaoLePhi);
            rd.ParameterFields["DonViThuHuong"].CurrentValues.AddValue(hoso.DmTrungTamTenCoQuanThuHuongCuc);
            rd.ParameterFields["DiaChi"].CurrentValues.AddValue(hoso.DmTrungTamDiaChiCoQuanThuHuongCuc);
            rd.ParameterFields["SoTaiKhoan"].CurrentValues.AddValue(hoso.DmTrungTamSoTaiKhoanCuc);
            rd.ParameterFields["TenNganHang"].CurrentValues.AddValue(hoso.DmTrungTamTenKhoBacCuc);
            //Thông tin doanh nghiệp
            rd.ParameterFields["DiaChiDN"].CurrentValues.AddValue(hoso.DmDonViDiaChi);
            rd.ParameterFields["TenCoQuan"].CurrentValues.AddValue(hoso.DmDonViTenTiengViet);
            rd.ParameterFields["DienThoai"].CurrentValues.AddValue(hoso.DmDonViDienThoai != null ? hoso.DmDonViDienThoai : "");
            rd.ParameterFields["Fax"].CurrentValues.AddValue(hoso.DmDonViFax != null ? hoso.DmDonViFax : "");
            rd.ParameterFields["MaSoThue"].CurrentValues.AddValue(hoso.DmDonViMaSoThue != null ? hoso.DmDonViMaSoThue : "");
            //Thong tin phí
            rd.ParameterFields["TongTienSo"].CurrentValues.AddValue(TongTienBangSo);
            rd.ParameterFields["TongTienChu"].CurrentValues.AddValue(TongTienBangChu);
            rd.ParameterFields["PhiLayMau"].CurrentValues.AddValue(FormatCurency(PhiLayMauSanPham) + " đ/mẫu");
            rd.ParameterFields["PhiDanhGia"].CurrentValues.AddValue(FormatCurency(PhiDanhGia) + " đ/" + SoQuyTrinh + " Quy trình");
            //Thông tin người ký
            rd.ParameterFields["ChucVu"].CurrentValues.AddValue(ChucVu);
            rd.ParameterFields["TenGiamDoc"].CurrentValues.AddValue(objThongBao.TenNguoiKyDuyet);
            rd.ParameterFields["NgayThang"].CurrentValues.AddValue(NgayThang);
        }

    }



    /// <summary>
    /// 
    /// </summary>
    private void InBienLai()
    {
        string strduongdandenteploaiBC = Server.MapPath(@"../Report/InBienLai.rpt");
        rd = new ReportDocument();
        rd.Load(strduongdandenteploaiBC);
        string id = string.Empty;
        if (Request["LePhiID"] != null)
        {
            id = Request["LePhiID"].ToString();
        }

        ThongBaoLePhi objTBLP = ProviderFactory.ThongBaoLePhiProvider.GetById(id);
        DmDonVi donvi = ProviderFactory.DmDonViProvider.GetById(objTBLP.DonViId);
        string loaiLePhi = string.Empty;
        switch (objTBLP.LoaiPhiId)
        {
            case 1:
                loaiLePhi = "PHI_CHUNG_NHAN_HQ";
                break;
            case 2:
                loaiLePhi = "PHI_CHUNG_NHAN_HC";
                break;
            case 3:
                loaiLePhi = "PHI_LAY_MAU_SAN_PHAM";
                break;
            case 4:
                loaiLePhi = "PHI_DANH_GIA_QUY_TRINH_SX";
                break;
            case 5:
                loaiLePhi = "PHI_CONG_BO_HQ";
                break;
            default:
                break;
        }
        DateTime dt = DateTime.Today;
        if (objTBLP.NgayThuTien != null)
        {
            dt = objTBLP.NgayThuTien.Value;
        }
        NumberUtil objTalkNumber = new NumberUtil();
        rd.ParameterFields["TenDonVi"].CurrentValues.AddValue(donvi.TenTiengViet.ToUpper());
        rd.ParameterFields["DiaChi"].CurrentValues.AddValue(donvi.DiaChi);
        rd.ParameterFields["MaSoThue"].CurrentValues.AddValue(donvi.MaSoThue);
        rd.ParameterFields["TenLoaiPhi"].CurrentValues.AddValue(loaiLePhi);
        rd.ParameterFields["SoTien"].CurrentValues.AddValue(FormatCurency(long.Parse((objTBLP.TongPhi * 1000).ToString())));
        rd.ParameterFields["TienChu"].CurrentValues.AddValue(objTalkNumber.Speak(objTBLP.TongPhi * 1000) + " đồng.");
        rd.ParameterFields["GiayThongBaoLePhi"].CurrentValues.AddValue(objTBLP.SoGiayThongBaoLePhi);
        rd.ParameterFields["NgayThongBao"].CurrentValues.AddValue(objTBLP.NgayPheDuyet.Value.ToShortDateString());
        rd.ParameterFields["HinhThucThanhToan"].CurrentValues.AddValue("Tiền mặt");
        rd.ParameterFields["NgayThu"].CurrentValues.AddValue(dt.Day.ToString());
        rd.ParameterFields["ThangThu"].CurrentValues.AddValue(dt.Month.ToString());
        rd.ParameterFields["NamThu"].CurrentValues.AddValue(dt.Year.ToString());


    }
    #region Helper method
    /// <summary>
    /// Lấy dữ liệu báo cáo đánh giá
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                12/5/2009              
    /// </Modified>
    private DataTable LayDuLieuBaoCaoDanhGia(string HO_SO_ID)
    {
        //Khai báo datatable
        DataTable dtDanhGia = new DataTable();
        DataColumn dcSanPhamID = new DataColumn("SanPhamID", typeof(string));
        DataColumn dcTenSanPham = new DataColumn("TenSanPham", typeof(string));
        DataColumn dcKyHieu = new DataColumn("KyHieu", typeof(string));
        DataColumn dcHangSanXuat = new DataColumn("HangSanXuat", typeof(string));
        DataColumn dcTieuChuan = new DataColumn("TieuChuan", typeof(string));
        dtDanhGia.Columns.Add(dcSanPhamID);
        dtDanhGia.Columns.Add(dcTenSanPham);
        dtDanhGia.Columns.Add(dcKyHieu);
        dtDanhGia.Columns.Add(dcHangSanXuat);
        dtDanhGia.Columns.Add(dcTieuChuan);
        //gán dữ liệu
        DataTable dtSanPham = ProviderFactory.SanPhamProvider.getDataTableByHoSoID(HO_SO_ID);
        foreach (DataRow dr in dtSanPham.Rows)
        {
            DataRow drDanhGia = dtDanhGia.NewRow();
            drDanhGia["SanPhamID"] = dr["ID"];
            drDanhGia["TenSanPham"] = dr["TenSanPham"];
            drDanhGia["KyHieu"] = dr["KyHieu"];
            //hãng sản xuất
            if (dr["HangSanXuatID"] != DBNull.Value)
            {
                DmHangSanXuat objDmHangSanXuat = ProviderFactory.DmHangSanXuatProvider.GetById(dr["HangSanXuatID"].ToString());
                if (objDmHangSanXuat != null)
                {
                    drDanhGia["HangSanXuat"] = objDmHangSanXuat.TenHangSanXuat;
                }
            }
            else
                drDanhGia["HangSanXuat"] = "";
            //Tiêu chuẩn
            string strTieuChuan = string.Empty;
            if (dr["ID"] != DBNull.Value)
            {
                TList<SanPhamTieuChuanApDung> lstSanPhamTieuChuanApDung =
                                                ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(dr["ID"].ToString());
                //nếu có==> đã chứng nhận==>lấy trong bảng SanPhamTieuChuanApDung
                if (lstSanPhamTieuChuanApDung.Count > 0)
                {
                    foreach (SanPhamTieuChuanApDung obj in lstSanPhamTieuChuanApDung)
                    {
                        DmTieuChuan objDmTieuChuan = ProviderFactory.DmTieuChuanProvider.GetById(obj.TieuChuanApDungId);
                        if (objDmTieuChuan != null)
                        {
                            strTieuChuan += objDmTieuChuan.MaTieuChuan + ", " + objDmTieuChuan.TenTieuChuan + "\r\n";
                        }
                    }
                }
                ////nếu không có==> chưa chứng nhận==>lấy trong danh mục
                //else
                //{
                //    TList<DmSanPhamTieuChuan> lstSanPhamTieuChuan =
                //                                ProviderFactory.DmSanPhamTieuChuanProvider.GetBySanPhamId(dr["ID"].ToString());
                //    foreach (DmSanPhamTieuChuan obj in lstSanPhamTieuChuan)
                //    {
                //        DmTieuChuan objDmTieuChuan = ProviderFactory.DmTieuChuanProvider.GetById(obj.TieuChuanId);
                //        if (objDmTieuChuan != null)
                //        {
                //            strTieuChuan += objDmTieuChuan.MaTieuChuan + ", " + objDmTieuChuan.TenTieuChuan + "\r\n";
                //        }
                //    }
                //}
            }
            if (strTieuChuan.Length > 2)
                strTieuChuan = strTieuChuan.Remove(strTieuChuan.Length - 2, 2);
            drDanhGia["TieuChuan"] = strTieuChuan;

            dtDanhGia.Rows.Add(drDanhGia);
        }
        return dtDanhGia;

    }



    private string LayTenTrungTam(string TrungTamID)
    {
        DmTrungTam TrungTam = ProviderFactory.DmTrungTamProvider.GetById(TrungTamID);
        if (TrungTam != null)
        {
            if (TrungTam.MaTrungTam.ToUpper() == "TTCN")
                return "Trung tâm chứng nhận";
            else if (TrungTam.MaTrungTam.ToUpper() == "TT2")
                return "Trung tâm 2";
            else if (TrungTam.MaTrungTam.ToUpper() == "TT3")
                return "Trung tâm 3";
        }
        return "";
    }

    public ReportDocument LayDuLieuTuanQuy(string sDuongDanBC, string sTrungTam, string sTuTuan, string sDenTuan)
    {
        UserInfo _mUserInfo = Session["User"] as UserInfo;
        ReportDocument rd = new ReportDocument();

        rd.Load(sDuongDanBC);
        DataTable dtDuLieu = ProviderFactory.SanPhamProvider.GetDuLieuBaoCaoTuanQuy(sTrungTam, sTuTuan, sDenTuan);

        rd.SetDataSource(dtDuLieu);

        string[] arrTrungTam = sTrungTam.Split(",".ToCharArray());
        string sTTCN = "";
        string sTT2 = "";
        string sTT3 = "";
        for (int i = 0; i < arrTrungTam.Length; i++)
        {
            if (arrTrungTam[i].ToString().ToUpper() == "TTCN")
                sTTCN = "TTCN";
            else if (arrTrungTam[i].ToString().ToUpper() == "TT2")
                sTT2 = "TT2";
            else if (arrTrungTam[i].ToString().ToUpper() == "TT3")
                sTT3 = "TT3";
        }

        rd.ParameterFields["TenTrungTam"].CurrentValues.AddValue(LayTenTrungTam(_mUserInfo.MaTrungTam).ToUpper());
        if (sTTCN != "")
            rd.ParameterFields["TTCN"].CurrentValues.AddValue(sTTCN);
        else
            rd.ParameterFields["TTCN"].CurrentValues.AddValue("");
        if (sTT2 != "")
            rd.ParameterFields["TT2"].CurrentValues.AddValue(sTT2);
        else
            rd.ParameterFields["TT2"].CurrentValues.AddValue("");
        if (sTT3 != "")
            rd.ParameterFields["TT3"].CurrentValues.AddValue(sTT3);
        else
            rd.ParameterFields["TT3"].CurrentValues.AddValue("");

        rd.ParameterFields["GiamDoc"].CurrentValues.AddValue(_mUserInfo.TenGiamDoc.ToUpper());
        rd.ParameterFields["Tu"].CurrentValues.AddValue(sTuTuan);
        rd.ParameterFields["Den"].CurrentValues.AddValue(sDenTuan);
        string[] TrungTamInfo = ProviderFactory.DmTrungTamProvider.GetByMaTrungTam(mUserInfo.TrungTam.Id);
        string thoigianky = TrungTamInfo[0] + ", ngày " + DateTime.Now.Day.ToString() + " tháng " + DateTime.Now.Month.ToString() + " năm " + DateTime.Now.Year.ToString();
        rd.ParameterFields["ThoiGianKy"].CurrentValues.AddValue(thoigianky);

        return rd;
    }

    /// <summary>
    /// Lấy mô tả của enum NhanHoSoTu
    /// </summary>
    /// <Modified>
    /// Người tạo Ngày tạo Chú thích
    /// Hùngnv 20/05/2009
    /// </Modified>
    private string NhanHoSoTu(int NhanHoSoTuID)
    {
        string strReturn = string.Empty;
        switch (NhanHoSoTuID)
        {
            //Nhận trực tiếp
            case 1:
                strReturn = "Nhận trực tiếp";
                break;
            //Nhận qua đường bưu điện
            case 2:
                strReturn = "Nhận qua đường bưu điện";
                break;
            //Chuyển từ trung tâm khác
            case 3:
                strReturn = "Chuyển từ trung tâm khác";
                break;
        }
        return strReturn;
    }

    #region Lay Ten Chuc Vu Tu Nguoi Ky Duyet
    private string LayTenChucVu_Them(string LoaiBaoCao, int position, string TenNguoiKyDuyet, string TenTrungTam, string TenPhongBan)
    {
        if (LoaiBaoCao == "GiayChungNhan")
        {
            if (position == Convert.ToInt32(EnChucVuList.GDTT))
            {
                return ((string)("\n Director" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + TenNguoiKyDuyet));
            }
            else if (position == Convert.ToInt32(EnChucVuList.PGDTT))
            {
                return ((string)("KT. GIÁM ĐỐC" + "\n" + "PHÓ GIÁM ĐỐC" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + TenNguoiKyDuyet));
            }
            else if (position == Convert.ToInt32(EnChucVuList.TRUONG_PHONG))
            {
                return ((string)("TUQ. GIÁM ĐỐC" + "\n" + "TRƯỞNG PHÒNG " + TenPhongBan.ToUpper() + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + TenNguoiKyDuyet));
            }
            else if (position == Convert.ToInt32(EnChucVuList.PHO_PHONG))
            {
                return ((string)("TUQ. GIÁM ĐỐC" + "\n" + "PHÓ PHÒNG " + TenPhongBan.ToUpper() + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + TenNguoiKyDuyet));
            }
        }
        //Thong bao le phi
        else
        {
            if (position == Convert.ToInt32(EnChucVuList.GDTT))
            {
                return ((string)("TUQ. CỤC TRƯỞNG" + "\n" + "GIÁM ĐỐC TRUNG TÂM " + TenTrungTam + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + TenNguoiKyDuyet));
            }
            else if (position == Convert.ToInt32(EnChucVuList.PGDTT))
            {
                return ((string)("TUQ. CỤC TRƯỞNG" + "\n" + "TUQ. GIÁM ĐỐC" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + TenNguoiKyDuyet));
            }
        }
        return "";
    }

    private string LayTenChucVu_Cu(string LoaiBaoCao, SysUser NguoiKyDuyet)
    {
        if (LoaiBaoCao == "GiayChungNhan")
        {
            if (Convert.ToInt32(NguoiKyDuyet.Position) == Convert.ToInt32(EnChucVuList.GDTT))
            {
                return ((string)("\n Director" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKyDuyet.FullName));
            }
            else if (Convert.ToInt32(NguoiKyDuyet.Position) == Convert.ToInt32(EnChucVuList.PGDTT))
            {
                return ((string)("KT. GIÁM ĐỐC" + "\n" + "PHÓ GIÁM ĐỐC" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKyDuyet.FullName));
            }
            else if (Convert.ToInt32(NguoiKyDuyet.Position) == Convert.ToInt32(EnChucVuList.TRUONG_PHONG))
            {
                return ((string)("TUQ. GIÁM ĐỐC" + "\n" + "TRƯỞNG PHÒNG " + ProviderFactory.DmPhongBanProvider.GetById(NguoiKyDuyet.DepartmentId).TenPhongBan.ToUpper() + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKyDuyet.FullName));
            }
            else if (Convert.ToInt32(NguoiKyDuyet.Position) == Convert.ToInt32(EnChucVuList.PHO_PHONG))
            {
                return ((string)("TUQ. GIÁM ĐỐC" + "\n" + "PHÓ PHÒNG " + ProviderFactory.DmPhongBanProvider.GetById(NguoiKyDuyet.DepartmentId).TenPhongBan.ToUpper() + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKyDuyet.FullName));
            }
        }
        //Thong bao le phi
        else
        {
            if (Convert.ToInt32(NguoiKyDuyet.Position) == Convert.ToInt32(EnChucVuList.GDTT))
            {
                return ((string)("TUQ. CỤC TRƯỞNG" + "\n" + "GIÁM ĐỐC TRUNG TÂM " + ProviderFactory.DmTrungTamProvider.GetById(NguoiKyDuyet.OrganizationId).TenTrungTam + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKyDuyet.FullName));
            }
            else if (Convert.ToInt32(NguoiKyDuyet.Position) == Convert.ToInt32(EnChucVuList.PGDTT))
            {
                return ((string)("TUQ. CỤC TRƯỞNG" + "\n" + "TUQ. GIÁM ĐỐC" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKyDuyet.FullName));
            }
        }
        return "";
    }

    private string LayTenChucVu(string LoaiBaoCao, SysUser NguoiKyDuyet)
    {
        if (LoaiBaoCao == "GiayChungNhan")
        {
            if (Convert.ToInt32(NguoiKyDuyet.Position) == Convert.ToInt32(EnChucVuList.GDTT))
            {
                return ((string)("GIÁM ĐỐC " + "\n Director" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKyDuyet.FullName));
            }
            else if (Convert.ToInt32(NguoiKyDuyet.Position) == Convert.ToInt32(EnChucVuList.PGDTT))
            {
                return ((string)("KT. GIÁM ĐỐC" + "\n" + "PHÓ GIÁM ĐỐC" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKyDuyet.FullName));
            }
            else if (Convert.ToInt32(NguoiKyDuyet.Position) == Convert.ToInt32(EnChucVuList.TRUONG_PHONG))
            {
                return ((string)("TUQ. GIÁM ĐỐC" + "\n" + "TRƯỞNG PHÒNG " + ProviderFactory.DmPhongBanProvider.GetById(NguoiKyDuyet.DepartmentId).TenPhongBan.ToUpper() + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKyDuyet.FullName));
            }
            else if (Convert.ToInt32(NguoiKyDuyet.Position) == Convert.ToInt32(EnChucVuList.PHO_PHONG))
            {
                return ((string)("TUQ. GIÁM ĐỐC" + "\n" + "PHÓ PHÒNG " + ProviderFactory.DmPhongBanProvider.GetById(NguoiKyDuyet.DepartmentId).TenPhongBan.ToUpper() + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKyDuyet.FullName));
            }
        }
        //Thong bao le phi
        else
        {
            if (Convert.ToInt32(NguoiKyDuyet.Position) == Convert.ToInt32(EnChucVuList.GDTT))
            {
                return ((string)("TUQ. CỤC TRƯỞNG" + "\n" + "GIÁM ĐỐC TRUNG TÂM " + ProviderFactory.DmTrungTamProvider.GetById(NguoiKyDuyet.OrganizationId).TenTrungTam + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKyDuyet.FullName));
            }
            else if (Convert.ToInt32(NguoiKyDuyet.Position) == Convert.ToInt32(EnChucVuList.PGDTT))
            {
                return ((string)("TUQ. CỤC TRƯỞNG" + "\n" + "TUQ. GIÁM ĐỐC" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + NguoiKyDuyet.FullName));
            }
        }
        return "";
    }

    /// <summary>
    /// Chuyen ngay thang tu datetime sang chuoi ngay thang nam
    /// </summary>
    /// <param name="NgayThang"></param>
    /// <returns></returns>
    public string TalkDate(DateTime NgayThang)
    {
        string ngay = NgayThang.Day.ToString().Length > 1 ? NgayThang.Day.ToString() : "0" + NgayThang.Day.ToString();
        string thang = NgayThang.Month.ToString().Length > 1 ? NgayThang.Month.ToString() : "0" + NgayThang.Month.ToString();

        return " ngày " + ngay + " tháng " + thang + " năm " + NgayThang.Year.ToString();
    }
    #endregion
    #endregion

    #region Xu ly su kien nguoi dung click vao cac bieu tuong xuat file

    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        rd.PrintToPrinter(1, false, 0, 0);
    }
    protected void lnkWord_Click(object sender, EventArgs e)
    {
        ShowReport("Word", rd);
    }
    protected void lnkExcel_Click(object sender, EventArgs e)
    {
        ShowReport("Excel", rd);
    }
    protected void lnkPDF_Click(object sender, EventArgs e)
    {
        ShowReport("PDF", rd);
    }
    protected void lnkHTML_Click(object sender, EventArgs e)
    {
        ShowReport("HTML", rd);
    }

    #endregion

    #region ShowReport
    private void ShowReport(string format, ReportDocument myReport)
    {

        #region Xuất dữ liệu
        if ((format == "") || (format == null))
            format = "CR";
        if (myReport == null)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "TaiLaiTrangChinh", "alert('Không có dữ liệu cho báo cáo!');", true);
            return;
        }
        try
        {
            if (format == "Excel")
            {
                myReport.ExportToHttpResponse(ExportFormatType.Excel, Response, true, strTenBaoCao);
            }
            else if (format == "Word")
            {
                myReport.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, true, strTenBaoCao);
            }
            else if (format == "PDF")
            {
                myReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, strTenBaoCao);
            }
            else if (format == "HTML")
            {
                string strHTMLFile = Server.MapPath(@"~\Upload\Exported.htm");
                myReport.ExportToDisk(ExportFormatType.HTML40, strHTMLFile);
                Response.Redirect(@"~\Upload\Exported.htm");
            }

            if (format != "CR")
            {
                Response.Flush();
                Response.Close();
            }

            if (format != "HTML")
            {
                this.hienBC.ReportSource = myReport;
                this.hienBC.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        #endregion
    }
    #endregion

}
