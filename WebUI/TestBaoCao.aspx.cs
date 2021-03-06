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
using Cuc_QLCL.Entities;
using Cuc_QLCL.AdapterData;
using Resources;
using CrystalDecisions.Shared;
using Cuc_QLCL.Data;
using System.IO;
using CucQLCL.Common;
using System.Net.Mail;

public partial class WebUI_TestBaoCao : PageBase
{

    int PhiLayMauSanPham = 1000000;
    int PhiDanhGiaQuyTrinhQLCL = 1000000;
    string strTenBaoCao;
    ReportDocument rd;
    ReportDocument mReportDocument;
    public string EmailForSend = string.Empty;
    public string TieuDeForSend = string.Empty;
    public string NoiDungForSend = string.Empty;
    /// <summary>
    /// Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Name        Date        Comment
    /// TuanND    18/05/2009    Thêm mới
    /// </Modified>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (mUserInfo.IsThanhTra)
            trPrint.Visible = false;

        // ID thong bao le phi
        string ThongBaoLePhiID = Request["ThongBaoLePhiID"].ToString();
        ThongBaoLePhi objTBLP = ProviderFactory.ThongBaoLePhiProvider.GetById(ThongBaoLePhiID);

        if (objTBLP.TrangThaiId == (int)EnTrangThaiThongBaoPhiList.CHO_DUYET_HUY)
        {
            txtLyDoHuy.Text = objTBLP.LyDoHuy;

            // Hiển thị dành cho giám đốc
            trHuy.Style.Add("Display", "true");
            btnHuyHieuLuc.Visible = false;
            btnThuPhi.Visible = false;
            btnCapNhat.Visible = true;
            soHD.Style.Add("Display", "true");
            trNgayThuTien.Style.Add("Display", "true");
        }
        else if (objTBLP.TrangThaiId == (int)EnTrangThaiThongBaoPhiList.CHO_PHE_DUYET)
        {
            // Hiển thị dành cho giám đốc
            trHuy.Style.Add("Display", "true");
            lblLyDoHuy.Visible = false;
            txtLyDoHuy.Visible = false;
            btnHuyHieuLuc.Visible = false;
            btnThuPhi.Visible = false;
            btnCapNhat.Visible = true;
            soHD.Style.Add("Display", "true");
            trNgayThuTien.Style.Add("Display", "true");
        }
        else
        {
            // Hiển thị dành cho người thu phí
            trHuy.Style.Add("Display", "none");
            btnHuyHieuLuc.Visible = true;
            btnThuPhi.Visible = true;
            btnCapNhat.Visible = false;


            if (objTBLP.TrangThaiId == (int)EnTrangThaiThongBaoPhiList.CHO_THU_PHI)
            {
                btnHuyHieuLuc.Text = "Đề xuất huỷ TBLP";
            }
            else
            {
                btnHuyHieuLuc.Text = "Huỷ TBLP";
                btnThuPhi.Visible = false;
            }
        }

        rd = new ReportDocument();

        if (objTBLP.LoaiPhiId.HasValue)
        {
            if (objTBLP.LoaiPhiId.Value == 9)
            {
                rd = GetReportDocument(ThongBaoLePhiID, 0);
            }
            else if (objTBLP.LoaiPhiId.Value == 10) {
                rd = GetReportDocument(ThongBaoLePhiID, 0);
            }
            else
            {
                ThongBaoLePhiSanPham obj = ProviderFactory.ThongBaoLePhiSanPhamProvider.GetByThongBaoLePhiId(ThongBaoLePhiID)[0];

                //string ThongBaoLePhiID = "TTCN_12_30";
                rd = GetReportDocument(ThongBaoLePhiID, (int)obj.CachTinhPhi);
            }
            
        }
        else
        {
            rd = GetReportDocument(ThongBaoLePhiID, 0);
        }
        #region Lấy tham số chung

        //string LoaiBaoCao = "";
        //if (Session["LoaiBaoCao"] != null)
        //    LoaiBaoCao = Session["LoaiBaoCao"].ToString();
        string format = "CR";
        strTenBaoCao = "";
        if (Session["TenBaoCao"] != null)
        {
            strTenBaoCao = Session["TenBaoCao"].ToString();
        }
        #endregion
        #region Xuất dữ liệu
        if (string.IsNullOrEmpty(format))
            format = "CR";

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
                this.crView.ReportSource = rd;
                this.crView.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        #endregion


        //-----------------------------------
        string action = "";
        if (Request["action"] != null)
            action = Request["action"].ToString();
        if (action.Equals("tracuu"))
        {
            btnThuPhi.Visible = false;
            btnHuyHieuLuc.Visible = false;
            soHD.Style.Add("Display", "none");
            trNgayThuTien.Style.Add("Display", "none");
        }
        
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        if (rd != null)
        {
            rd.Close();
            rd.Dispose();
        }
        if (mReportDocument != null)
        {
            mReportDocument.Close();
            mReportDocument.Dispose();
        }
    }
    #region Report
    /// <summary>
    /// In thông báo lệ phí
    /// </summary>
    /// <param name="ID">ID cua thong bao le phi</param>
    /// <Modified>
    /// Name        Date        Comment
    /// TuanND    18/05/2009    Thêm mới
    /// TuấnVM     12/11/2009   Sửa: Bỏ biểu mẫu GiayBaoLePhiPMD (NgonGocID = 2 ứng với nhập khẩu phi mậu dịch)
    /// </Modified>
    public ReportDocument GetReportDocument(string ID, int NguongocID)
    {
        ThongBaoLePhi objThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetById(ID);
        if (!IsPostBack)
        {
            txtSoHoaDon.Text = objThongBaoLePhi.SoHoaDon != null ? objThongBaoLePhi.SoHoaDon : string.Empty;
            txtNgayThuTien.Text = objThongBaoLePhi.NgayThuTien != null ? objThongBaoLePhi.NgayThuTien.Value.ToShortDateString() : DateTime.Now.ToShortDateString();
        }
        // Nếu là thông báo nộp tiền
        if (objThongBaoLePhi.LoaiPhiId == null)
            return GetThongBaoNopTienReport(ID);
        if (objThongBaoLePhi.LoaiPhiId == 5)
            return GetGiayBaoLePhiCongBoReport(ID);
        if (objThongBaoLePhi.LoaiPhiId == (int)EnLoaiPhiList.PHI_CHUNG_NHAN_HQ)
            //return GetGiayBaoLePhiReport_HopQuy(ID);
            return GetGiayThongBaoNopTienReport_CNHopQuy(ID);
        if (objThongBaoLePhi.LoaiPhiId == (int)EnLoaiPhiList.PHI_DANH_GIA_LAI)
            return GetGiayBaoLePhiReport_DanhGiaLai(ID);
        // LongHH: Thêm mới 1 loại phí mới là 9
        if (objThongBaoLePhi.LoaiPhiId == 9)
            return GetGiayBaoLePhiTiepNhanReport(ID);
        if (objThongBaoLePhi.LoaiPhiId == 10)
            return GetGiayBaoLePhiDanhGiaQTSXReport(ID);
        // LongHH
        // Giấy báo lệ phí chứng nhập hợp chuẩn
        return GetGiayBaoLePhiReport_HopChuan(ID);
    }

    /// <summary>
    /// Hiển thị report Giấy báo lệ phí chứng nhận hợp quy
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    private ReportDocument GetGiayBaoLePhiReport_HopQuy(string ID)
    {
        string reportPath = Request.PhysicalApplicationPath + "Report\\GiayBaoLePhi.rpt";
        int soLuongGCN = 0;

        this.Title = "In thông báo lệ phí";
        mReportDocument = new ReportDocument();
        mReportDocument.Load(reportPath);

        //Lấy dữ liệu
        string strDSSoGCN = "";
        DataTable dtTBLP = new DataTable();
        dtTBLP = ProviderFactory.ThongBaoLePhiProvider.GetReportInfor(ID);
        string SoHoSo = string.Empty;
        string NgayTiepNhan = string.Empty;
        string TenTrungTam = string.Empty;
        string HoSoId = string.Empty;
        string TenTinhThanh = string.Empty;
        string TenCoQuanThuHuong = string.Empty;
        string DiaChiCQTH = string.Empty;
        string SoTaiKhoan = string.Empty;
        string TenKhoBac = string.Empty;

        if (dtTBLP.Rows.Count > 0)
        {
            DataRow row = dtTBLP.Rows[0];
            HoSoId = row["HoSoId"].ToString();
        }

        HoSo objHoSo = ProviderFactory.HoSoProvider.GetById(HoSoId);
        SoHoSo = objHoSo.SoHoSo;
        NgayTiepNhan = objHoSo.NgayTiepNhan.Value.ToShortDateString();

        // Lấy thông tin theo trang thái hồ sơ
        if (objHoSo.TrangThaiId == (int)EnTrangThaiHoSoList.CHO_LUU_TRU || objHoSo.TrangThaiId == (int)EnTrangThaiHoSoList.DA_DONG)
        {
            TenTrungTam = objHoSo.DmTrungTamTenTrungTam;
            string TinhThanhId = objHoSo.DmTrungTamTinhThanhId;
            TenTinhThanh = ProviderFactory.DmTinhThanhProvider.GetById(TinhThanhId).TenTinhThanh;
            TenCoQuanThuHuong = objHoSo.DmTrungTamTenCoQuanThuHuongCuc;
            DiaChiCQTH = objHoSo.DmTrungTamDiaChiCoQuanThuHuongCuc;
            SoTaiKhoan = objHoSo.DmTrungTamSoTaiKhoanCuc;
            TenKhoBac = objHoSo.DmTrungTamTenKhoBacCuc;
        }
        else
        {
            DmTrungTam objTrungTam = ProviderFactory.DmTrungTamProvider.GetById(objHoSo.TrungTamId);
            TenTrungTam = objTrungTam.TenTrungTam;
            TenTinhThanh = ProviderFactory.DmTinhThanhProvider.GetById(objTrungTam.TinhThanhId).TenTinhThanh;
            TenCoQuanThuHuong = objTrungTam.TenCoQuanThuHuongCuc;
            DiaChiCQTH = objTrungTam.DiaChiCoQuanThuHuongCuc;
            SoTaiKhoan = objTrungTam.SoTaiKhoanCuc;
            TenKhoBac = objTrungTam.TenKhoBacCuc;
        }

        DataTable dtSP = new DataTable();
        dtSP = ProviderFactory.ThongBaoLePhiSanPhamProvider.GetSanPhamReportInfor(ID);
        //Parameter Field
        NumberUtil objTalkNumber = new NumberUtil();

        string strNgayPheDuyet = "ngày.......tháng........năm.........";
        if (!string.IsNullOrEmpty(dtTBLP.Rows[0]["NgayPheDuyet"].ToString()))
        {
            strNgayPheDuyet = "";
            strNgayPheDuyet += TalkDate(DateTime.Parse(dtTBLP.Rows[0]["NgayPheDuyet"].ToString()));
        }
        foreach (DataRow row in dtSP.Rows)
        {
            if (!string.IsNullOrEmpty(row["SoGCN"].ToString()))
            {
                strDSSoGCN += row["SoGCN"].ToString() + ", ";
                soLuongGCN++;
            }
        }
        if (!string.IsNullOrEmpty(strDSSoGCN))
            strDSSoGCN = strDSSoGCN.Substring(0, strDSSoGCN.Length - 2);
        string ChucVu = dtTBLP.Rows[0]["Position"].ToString();
        switch (ChucVu)
        {
            case "1":
                ChucVu = "GIÁM ĐỐC";
                break;
            case "2":
                ChucVu = "KT. GIÁM ĐỐC" + "\n" + "PHÓ GIÁM ĐỐC";
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
        }
        
        DataTable dt = ReformatDatatable(dtSP);
        mReportDocument.SetDataSource(dt);

        mReportDocument.SetParameterValue("SoGiayThongBaoLePhi", dtTBLP.Rows[0]["SoGiayThongBaoLePhi"]);
        mReportDocument.SetParameterValue("NgayPheDuyet", strNgayPheDuyet);
        mReportDocument.SetParameterValue("TenTiengViet", dtTBLP.Rows[0]["TenTiengViet"]);
        mReportDocument.SetParameterValue("DiaChi", dtTBLP.Rows[0]["DiaChi"]);
        mReportDocument.SetParameterValue("DienThoai", dtTBLP.Rows[0]["DienThoai"]);
        mReportDocument.SetParameterValue("Fax", dtTBLP.Rows[0]["Fax"].ToString());
        mReportDocument.SetParameterValue("SoGCN", strDSSoGCN);
        mReportDocument.SetParameterValue("TongPhi", FormatCurency(long.Parse(dtTBLP.Rows[0]["TongPhi"].ToString()) * 1000));
        mReportDocument.SetParameterValue("TongPhiBangChu", objTalkNumber.Speak(long.Parse(dtTBLP.Rows[0]["TongPhi"].ToString()) * 1000));
        mReportDocument.SetParameterValue("TinhThanhTrungTam", TenTinhThanh);
        mReportDocument.SetParameterValue("SoHoSo", SoHoSo);
        mReportDocument.SetParameterValue("NgayNopHoSo", NgayTiepNhan);
        mReportDocument.SetParameterValue("ChucVu", ChucVu);
        mReportDocument.SetParameterValue("NguoiKy", ProviderFactory.SysUserProvider.GetById(dtTBLP.Rows[0]["NguoiPheDuyetID"].ToString()).FullName);
        mReportDocument.SetParameterValue("ChuKy", "");

        mReportDocument.SetParameterValue("TenCoQuanThuHuong", TenCoQuanThuHuong);
        mReportDocument.SetParameterValue("DiaChiCoQuanThuHuong", DiaChiCQTH);
        mReportDocument.SetParameterValue("SoTaiKhoan", SoTaiKhoan);
        mReportDocument.SetParameterValue("TenKhoBac", TenKhoBac);

        if (soLuongGCN > 1)
            mReportDocument.SetParameterValue("SoLuongGCN", " các");
        else
            mReportDocument.SetParameterValue("SoLuongGCN", "");

        mReportDocument.SetParameterValue("TenTrungTam", TenTrungTam.ToUpper());

        return mReportDocument;
    }
    #region LongHH Giấy báo lệ phí chứng nhận hợp quy
    /// <summary>
    /// Hiển thị report Giấy báo lệ phí chứng nhận hợp quy theo mẫu mới
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    private ReportDocument GetGiayThongBaoNopTienReport_CNHopQuy(string ID)
    {
        string reportPath = Request.PhysicalApplicationPath + "Report\\ThongBaoNopTienCNHQ.rpt";
        int soLuongGCN = 0;

        this.Title = "In thông báo lệ phí CNHQ";
        mReportDocument = new ReportDocument();
        mReportDocument.Load(reportPath);
        mReportDocument.PrintOptions.PaperSize = PaperSize.PaperA4;
        
        //Lấy dữ liệu
        string strDSSoGCN = "";
        DataTable dtTBLP = new DataTable();
        dtTBLP = ProviderFactory.ThongBaoLePhiProvider.GetReportInfor(ID);
        string SoHoSo = string.Empty;
        string NgayTiepNhan = string.Empty;
        string TenTrungTam = string.Empty;
        string HoSoId = string.Empty;
        string TenTinhThanh = string.Empty;
        string TenCoQuanThuHuong = string.Empty;
        string DiaChiCQTH = string.Empty;
        string SoTaiKhoan = string.Empty;
        string TenKhoBac = string.Empty;
        string MaSoThue = string.Empty;
        string Email = string.Empty;
        long PhiDonGia = (int) QLCL_Patch.LePhi.PhiDonGia;
        long Cong = 0;
        long VAT = 0;
        long TongTien = 0;

        if (dtTBLP.Rows.Count > 0)
        {
            DataRow row = dtTBLP.Rows[0];
            HoSoId = row["HoSoId"].ToString();
        }

        HoSo objHoSo = ProviderFactory.HoSoProvider.GetById(HoSoId);
        SoHoSo = objHoSo.SoHoSo;
        NgayTiepNhan = objHoSo.NgayTiepNhan.Value.ToShortDateString();

        // Lấy thông tin theo trang thái hồ sơ
        if (objHoSo.TrangThaiId == (int)EnTrangThaiHoSoList.CHO_LUU_TRU || objHoSo.TrangThaiId == (int)EnTrangThaiHoSoList.DA_DONG)
        {
            TenTrungTam = objHoSo.DmTrungTamTenTrungTam;
            string TinhThanhId = objHoSo.DmTrungTamTinhThanhId;
            TenTinhThanh = ProviderFactory.DmTinhThanhProvider.GetById(TinhThanhId).TenTinhThanh;
            TenCoQuanThuHuong = objHoSo.DmTrungTamTenCoQuanThuHuong;
            DiaChiCQTH = objHoSo.DmTrungTamDiaChiCoQuanThuHuong;
            SoTaiKhoan = objHoSo.DmTrungTamSoTaiKhoan;
            TenKhoBac = objHoSo.DmTrungTamTenKhoBac;
        }
        else
        {
            DmTrungTam objTrungTam = ProviderFactory.DmTrungTamProvider.GetById(objHoSo.TrungTamId);
            TenTrungTam = objTrungTam.TenTrungTam;
            TenTinhThanh = ProviderFactory.DmTinhThanhProvider.GetById(objTrungTam.TinhThanhId).TenTinhThanh;
            TenCoQuanThuHuong = objTrungTam.TenCoQuanThuHuong;
            DiaChiCQTH = objTrungTam.DiaChiCoQuanThuHuong;
            SoTaiKhoan = objTrungTam.SoTaiKhoan;
            TenKhoBac = objTrungTam.TenKhoBac;
        }
        if (objHoSo != null)
        {
            DmDonVi objDonVi = new DmDonVi();
            objDonVi = ProviderFactory.DmDonViProvider.GetById(objHoSo.DonViId);
            MaSoThue = objDonVi.MaSoThue;
            Email = objDonVi.Email;
        }
        else
        {
            MaSoThue = dtTBLP.Rows[0]["MaSoThue"].ToString();
            Email = dtTBLP.Rows[0]["Email"].ToString();
        }
        //LongHH: Fix cứng tên kho bạc đối với Thông báo phí CNHQ theo yêu cầu
        TenKhoBac = "Ngân hàng Thương mại Cổ phần Á châu - Hội sở";
        DataTable dtSP = new DataTable();
        dtSP = ProviderFactory.ThongBaoLePhiSanPhamProvider.GetSanPhamReportInfor(ID);
        //Parameter Field
        NumberUtil objTalkNumber = new NumberUtil();

        string strNgayPheDuyet = "ngày.......tháng........năm.........";
        if (!string.IsNullOrEmpty(dtTBLP.Rows[0]["NgayPheDuyet"].ToString()))
        {
            strNgayPheDuyet = "";
            strNgayPheDuyet += TalkDate(DateTime.Parse(dtTBLP.Rows[0]["NgayPheDuyet"].ToString()));
        }
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
        int tongTien1 = 0, tongGCN = 0;
        foreach (DataRow row in dtSP.Rows)
        {
            if (!string.IsNullOrEmpty(row["SoGCN"].ToString()))
            {
                strDSSoGCN += row["SoGCN"].ToString() + ", ";
                soLuongGCN++;
                TList<SanPhamTieuChuanApDung> lstSanPhamTieuChuanApDung = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(row["SanPhamId"].ToString());
                DataRow row1 = returnTable.NewRow();
                row1["GCN"] = row["SoGCN"].ToString();
                row1["QuyChuanThuNhat"] = string.Empty;
                row1["QuyChuanThuHai"] = string.Empty;
                row1["DonGia1"] = string.Format("{0:0,0}.000", (int)QLCL_Patch.LePhi.DonGiaQuyChuan1);
                row1["SoLuong1"] = 1;
                row1["ThanhTien1"] = string.Format("{0:0,0}.000", (int)QLCL_Patch.LePhi.DonGiaQuyChuan1);
                row1["DonGia2"] = string.Format("{0:0,0}.000", (int)QLCL_Patch.LePhi.DonGiaQuyChuan2);
                row1["SoLuong2"] = lstSanPhamTieuChuanApDung.Count - 1;
                row1["ThanhTien2"] = string.Format("{0:0,0}.000", (int)QLCL_Patch.LePhi.DonGiaQuyChuan2 * (lstSanPhamTieuChuanApDung.Count - 1));
                returnTable.Rows.Add(row1);
                tongGCN++;
                tongTien1 += (int)QLCL_Patch.LePhi.DonGiaQuyChuan1 + (int)QLCL_Patch.LePhi.DonGiaQuyChuan2 * (lstSanPhamTieuChuanApDung.Count - 1);
            }
        }
        Cong = tongGCN * PhiDonGia + tongTien1;
        VAT = Cong / 10;
        TongTien = Cong + VAT;
        string BangChu = objTalkNumber.Speak(TongTien*1000);
        if (!string.IsNullOrEmpty(strDSSoGCN))
            strDSSoGCN = strDSSoGCN.Substring(0, strDSSoGCN.Length - 2);
        string ChucVu = dtTBLP.Rows[0]["Position"].ToString();
        switch (ChucVu)
        {
            case "1":
                ChucVu = "GIÁM ĐỐC";
                break;
            case "2":
                ChucVu = "KT. GIÁM ĐỐC" + "\n" + "PHÓ GIÁM ĐỐC";
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
        }
        mReportDocument.SetDataSource(returnTable);

        // LongHH
        mReportDocument.SetParameterValue("TenTrungTam", TenTrungTam.ToUpper());

        mReportDocument.SetParameterValue("SoGiayThongBaoLePhi", dtTBLP.Rows[0]["SoGiayThongBaoLePhi"].ToString().Trim().Replace("LPCNHQ", "PCG"));
        mReportDocument.SetParameterValue("TinhThanhTrungTam", TenTinhThanh);
        mReportDocument.SetParameterValue("TenTrungTam", TenTrungTam);
        mReportDocument.SetParameterValue("NgayPheDuyet", strNgayPheDuyet);
        mReportDocument.SetParameterValue("TenTiengViet", dtTBLP.Rows[0]["TenTiengViet"]);
        mReportDocument.SetParameterValue("DiaChi", dtTBLP.Rows[0]["DiaChi"]);
        mReportDocument.SetParameterValue("DienThoai", dtTBLP.Rows[0]["DienThoai"]);
        mReportDocument.SetParameterValue("Fax", dtTBLP.Rows[0]["Fax"].ToString());
        mReportDocument.SetParameterValue("MaSoThue", MaSoThue);
        mReportDocument.SetParameterValue("Email", Email);
        mReportDocument.SetParameterValue("SoPhieuTiepNhan", SoHoSo);
        mReportDocument.SetParameterValue("DanhSachGCN", strDSSoGCN);
        mReportDocument.SetParameterValue("NgayPheDuyet1", NgayTiepNhan);
        mReportDocument.SetParameterValue("PhiDonGia", string.Format("{0:0,0}.000", PhiDonGia));
        mReportDocument.SetParameterValue("PhiSL", tongGCN);
        mReportDocument.SetParameterValue("PhiTTien", string.Format("{0:0,0}.000", tongGCN * PhiDonGia));
        mReportDocument.SetParameterValue("TongTien1", string.Format("{0:0,0}.000", tongTien1));
        mReportDocument.SetParameterValue("CongTien", string.Format("{0:0,0}.000", Cong));

        mReportDocument.SetParameterValue("VAT", string.Format("{0:0,0}.000", VAT));
        mReportDocument.SetParameterValue("TongTien", string.Format("{0:0,0}.000", TongTien));
        mReportDocument.SetParameterValue("BangChu", BangChu);
        mReportDocument.SetParameterValue("DonViThuHuong", TenCoQuanThuHuong);
        mReportDocument.SetParameterValue("DiaChiDVThuHuong", DiaChiCQTH);
        mReportDocument.SetParameterValue("SoTaiKhoan", SoTaiKhoan);
        mReportDocument.SetParameterValue("TenKhoBac", TenKhoBac);

        mReportDocument.SetParameterValue("HoTenNguoiKy", ProviderFactory.SysUserProvider.GetById(dtTBLP.Rows[0]["NguoiPheDuyetID"].ToString()).FullName);
        mReportDocument.SetParameterValue("ChucVu", ChucVu);
        if (!IsPostBack)
        {
            EmailForSend = Email;
            TieuDeForSend = "Thông báo nộp tiền Chứng nhận hợp quy.";
            txtEmail.Text = Email;
            txtTieuDe.Text = TieuDeForSend;
        }
        // LongHH
        return mReportDocument;
    }

    private ReportDocument GetGiayBaoLePhiTiepNhanReport(string ID)
    {
        string strduongdandenteploaiBC = Server.MapPath(@"~\Report\LePhiTiepNhanCNHQ.rpt");
        rd = new ReportDocument();
        rd.Load(strduongdandenteploaiBC);
        rd.PrintOptions.PaperSize = PaperSize.PaperA4;
        ThongBaoLePhi tblp = ProviderFactory.ThongBaoLePhiProvider.GetById(Request.QueryString["ThongBaoLePhiID"].ToString());
        
        DataTable dtTBLP = new DataTable();
        dtTBLP = ProviderFactory.ThongBaoLePhiProvider.GetReportInfor(ID);
        string HoSoId = string.Empty;
        string TenTrungTam = string.Empty;
        string SoGiayThongBaoLePhi = string.Empty;
        string TinhThanhTrungTam = string.Empty;
        string NgayPheDuyet = string.Empty;
        string TenTiengViet = string.Empty;
        string DiaChi = string.Empty;
        string DienThoai = string.Empty;
        string MaSoThue = string.Empty;
        string Fax = string.Empty;
        string NgayPheDuyetShort = string.Empty;
        long CongTien = 0;
        long VAT = 0;
        long TongTien = 0;
        string BangChu = string.Empty;
        string NoiDungChuyenKhoan = string.Empty;
        string HoTenNguoiKy = string.Empty;
        string ChucVu = string.Empty;
        string TenCoQuanThuHuong = string.Empty;
        string DiaChiCQTH = string.Empty;
        string SoTaiKhoan = string.Empty;
        string TenKhoBac = string.Empty;
        string Email = string.Empty;
        
        HoSoId = tblp.HoSoId;
        HoSo objHoSo = ProviderFactory.HoSoProvider.GetById(HoSoId);
        
        //NgayTiepNhan = objHoSo.NgayTiepNhan.Value.ToShortDateString();

        // Lấy thông tin theo trang thái hồ sơ
        if (objHoSo.TrangThaiId == (int)EnTrangThaiHoSoList.CHO_LUU_TRU || objHoSo.TrangThaiId == (int)EnTrangThaiHoSoList.DA_DONG)
        {
            TenTrungTam = objHoSo.DmTrungTamTenTrungTam;
            string TinhThanhId = objHoSo.DmTrungTamTinhThanhId;
            TinhThanhTrungTam = ProviderFactory.DmTinhThanhProvider.GetById(TinhThanhId).TenTinhThanh;
            TenCoQuanThuHuong = objHoSo.DmTrungTamTenCoQuanThuHuong;
            DiaChiCQTH = objHoSo.DmTrungTamDiaChiCoQuanThuHuong;
            SoTaiKhoan = objHoSo.DmTrungTamSoTaiKhoan;
            TenKhoBac = objHoSo.DmTrungTamTenKhoBac;
        }
        else
        {
            DmTrungTam objTrungTam = ProviderFactory.DmTrungTamProvider.GetById(objHoSo.TrungTamId);
            TenTrungTam = objTrungTam.TenTrungTam;
            TinhThanhTrungTam = ProviderFactory.DmTinhThanhProvider.GetById(objTrungTam.TinhThanhId).TenTinhThanh;
            TenCoQuanThuHuong = objTrungTam.TenCoQuanThuHuong;
            DiaChiCQTH = objTrungTam.DiaChiCoQuanThuHuong;
            SoTaiKhoan = objTrungTam.SoTaiKhoan;
            TenKhoBac = objTrungTam.TenKhoBac;
        }
        TenKhoBac = "Ngân hàng Thương mại Cổ phần Á châu - Hội sở";
        NgayPheDuyet = TalkDate(objHoSo.NgayTiepNhan.Value);
        DataTable dtSP = new DataTable();
        dtSP = ProviderFactory.ThongBaoLePhiSanPhamProvider.GetSanPhamReportInfor(ID);

        if (rd != null)
        {
            int SLPhiTiepNhan = Request["SLPhiTiepNhan"] == null ? 1 : int.Parse(Request["SLPhiTiepNhan"].ToString());
            
            DmDonVi objDV = ProviderFactory.DmDonViProvider.GetById(objHoSo.DonViId);
            
            DataTable TTGiayBaoPhi = QLCL_Patch.GetTTGiayBaoPhi(objHoSo.Id);
            if (TTGiayBaoPhi.Rows.Count > 0)
            {
                try
                {
                    SLPhiTiepNhan = int.Parse(TTGiayBaoPhi.Rows[0]["SLTiepNhan"].ToString());
                }
                catch { }
            }
            SysUser nguoiky = ProviderFactory.SysUserProvider.GetById(QLCL_Patch.GetNguoiKyGiayBaoPhi(objHoSo.Id));
            NumberUtil objTalkNumber = new NumberUtil();
            
            if (objHoSo != null)
            {
                SoGiayThongBaoLePhi = tblp.SoGiayThongBaoLePhi;
                DmDonVi objDonVi = new DmDonVi();
                objDonVi = ProviderFactory.DmDonViProvider.GetById(objHoSo.DonViId);
                TenTiengViet = objDonVi.TenTiengViet;
                DiaChi = objDonVi.DiaChi;
                DienThoai = objDonVi.DienThoai;
                MaSoThue = objDonVi.MaSoThue;
                Email = objDV.Email;
                Fax = objDonVi.Fax;
                NgayPheDuyetShort = objHoSo.NgayTiepNhan.Value.ToShortDateString();

            }
            if (objDV != null)
            {
                TenTiengViet = objDV.TenTiengViet;
                DiaChi = objDV.DiaChi;
                DienThoai = objDV.DienThoai;
                MaSoThue = objDV.MaSoThue;
                Email = objDV.Email;
                Fax = objDV.Fax;
            }
            if (nguoiky != null)
            {
                HoTenNguoiKy = nguoiky.FullName;
                ChucVu = QLCL_Patch.GetChucVuKy(nguoiky.Position);
            }

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

            CongTien = SLPhiTiepNhan * (int)QLCL_Patch.LePhi.DonGiaTiepNhan + SLPhiTiepNhan * (int)QLCL_Patch.LePhi.PhiXemXet;
            VAT = CongTien / 10;
            TongTien = CongTien + VAT;

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
            rd.SetParameterValue("BangChu", objTalkNumber.Speak(TongTien * 1000));
            rd.SetParameterValue("DVThuHuongDiaChi", DiaChiCQTH);
            rd.SetParameterValue("SoTaiKhoan", SoTaiKhoan);
            rd.SetParameterValue("TenKhoBac", TenKhoBac);


            rd.SetParameterValue("HoTenNguoiKy", HoTenNguoiKy);
            rd.SetParameterValue("ChucVu", ChucVu);
            if (!IsPostBack)
            {
                EmailForSend = Email;
                TieuDeForSend = "Thông báo Lệ phí tiếp nhận Chứng nhận Hợp quy.";
                txtEmail.Text = Email;
                txtTieuDe.Text = TieuDeForSend;
            }
        }
        return rd;
    }
    private ReportDocument GetGiayBaoLePhiDanhGiaQTSXReport(string ID) { 
    
        string strduongdandenteploaiBC = Server.MapPath(@"~\Report\ThuPhiDanhGiaQTSX.rpt");
        rd = new ReportDocument();
        rd.Load(strduongdandenteploaiBC);
        rd.PrintOptions.PaperSize = PaperSize.PaperA4;
        if (rd != null)
        {
            
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
            
            HoSo hoso = ProviderFactory.HoSoProvider.GetById(HoSoID);
            string TrungTamID = hoso.TrungTamId;
            DmTrungTam TrungTamObject = ProviderFactory.DmTrungTamProvider.GetById(TrungTamID);
            ///////////////////////////

            string TinhThanhTrungTam = ProviderFactory.DmTinhThanhProvider.GetById(TrungTamObject.TinhThanhId).TenTinhThanh;
            string NgayPheDuyet = objThongBao.NgayPheDuyet.HasValue ? TalkDate(objThongBao.NgayPheDuyet.Value) : TalkDate(objThongBao.NgayCapNhatSauCung.Value);
            string Email = string.Empty;
            //string HoTenNguoiKy = string.Empty;
            NumberUtil objTalkNumber = new NumberUtil();
            ///////////////////////////////////////////////////
            if (TrungTamObject == null)
            {
                rd = null;
                return rd;
            }
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
                return rd;
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
        return rd;
                
    }
    protected void SendEmail_Click(object sender, EventArgs e)
    {
        EmailForSend = txtEmail.Text.Trim();
        TieuDeForSend = txtTieuDe.Text.Trim();
        NoiDungForSend = txtNoiDung.Text.Trim();
        if (!string.IsNullOrEmpty(EmailForSend))
        {
            MailMessage mail = new MailMessage("phongchungnhantt2@gmail.com", EmailForSend);

            mail.Subject = TieuDeForSend.Trim();
            mail.Body = NoiDungForSend.Trim();
            mail.Attachments.Add(new Attachment(rd.ExportToStream(ExportFormatType.PortableDocFormat), "ThongBao.pdf"));
            mail.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.UseDefaultCredentials = true;
            client.Credentials = new System.Net.NetworkCredential("phongchungnhantt2@gmail.com", "PCN@012019");
            client.Port = 587;
            client.EnableSsl = true;
            client.Send(mail);
        }
        else {
            //txtNoiDung.Text = EmailForSend + "__Empty";
        }
    }
    #endregion

    /// <summary>
    /// Hiển thị report Giấy báo lệ phí chứng nhận hợp chuẩn
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    private ReportDocument GetGiayBaoLePhiReport_HopChuan(string ID)
    {
        string reportPath = Request.PhysicalApplicationPath + "Report\\GiayBaoLePhi_HopChuan.rpt";
        int soLuongGCN = 0;
        this.Title = "In thông báo lệ phí";
        mReportDocument = new ReportDocument();
        mReportDocument.Load(reportPath);

        //Lấy dữ liệu
        string strDSSoGCN = "";
        DataTable dtTBLP = new DataTable();
        dtTBLP = ProviderFactory.ThongBaoLePhiProvider.GetReportInfor(ID);
        string SoHoSo = string.Empty;
        string NgayTiepNhan = string.Empty;
        string TenTrungTam = string.Empty;
        string HoSoId = string.Empty;
        string TenTinhThanh = string.Empty;
        string TenCoQuanThuHuong = string.Empty;
        string DiaChiCQTH = string.Empty;
        string SoTaiKhoan = string.Empty;
        string TenKhoBac = string.Empty;

        if (dtTBLP.Rows.Count > 0)
        {
            DataRow row = dtTBLP.Rows[0];
            HoSoId = row["HoSoId"].ToString();
        }

        HoSo objHoSo = ProviderFactory.HoSoProvider.GetById(HoSoId);
        SoHoSo = objHoSo.SoHoSo;
        NgayTiepNhan = objHoSo.NgayTiepNhan.Value.ToShortDateString();
        // Lấy thông tin theo trang thái hồ sơ
        if (objHoSo.TrangThaiId == (int)EnTrangThaiHoSoList.CHO_LUU_TRU || objHoSo.TrangThaiId == (int)EnTrangThaiHoSoList.DA_DONG)
        {
            TenTrungTam = objHoSo.DmTrungTamTenTrungTam;
            string TinhThanhId = objHoSo.DmTrungTamTinhThanhId;
            TenTinhThanh = ProviderFactory.DmTinhThanhProvider.GetById(TinhThanhId).TenTinhThanh;
            TenCoQuanThuHuong = objHoSo.DmTrungTamTenCoQuanThuHuong;
            DiaChiCQTH = objHoSo.DmTrungTamDiaChiCoQuanThuHuong;
            SoTaiKhoan = objHoSo.DmTrungTamSoTaiKhoan;
            TenKhoBac = objHoSo.DmTrungTamTenKhoBac;
        }
        else
        {
            DmTrungTam objTrungTam = ProviderFactory.DmTrungTamProvider.GetById(objHoSo.TrungTamId);
            TenTrungTam = objTrungTam.TenTrungTam;
            TenTinhThanh = ProviderFactory.DmTinhThanhProvider.GetById(objTrungTam.TinhThanhId).TenTinhThanh;
            TenCoQuanThuHuong = objTrungTam.TenCoQuanThuHuong;
            DiaChiCQTH = objTrungTam.DiaChiCoQuanThuHuong;
            SoTaiKhoan = objTrungTam.SoTaiKhoan;
            TenKhoBac = objTrungTam.TenKhoBac;
        }

        DataTable dtSP = new DataTable();
        dtSP = ProviderFactory.ThongBaoLePhiSanPhamProvider.GetSanPhamReportInfor(ID);
        //Parameter Field
        NumberUtil objTalkNumber = new NumberUtil();

        string strNgayPheDuyet = "ngày.......tháng........năm.........";
        if (!string.IsNullOrEmpty(dtTBLP.Rows[0]["NgayPheDuyet"].ToString()))
        {
            strNgayPheDuyet = "";
            strNgayPheDuyet += TalkDate(DateTime.Parse(dtTBLP.Rows[0]["NgayPheDuyet"].ToString()));
        }
        foreach (DataRow row in dtSP.Rows)
        {
            if (!string.IsNullOrEmpty(row["SoGCN"].ToString()))
            {
                strDSSoGCN += row["SoGCN"].ToString() + ", ";
                soLuongGCN++;
            }
        }
        if (!string.IsNullOrEmpty(strDSSoGCN))
            strDSSoGCN = strDSSoGCN.Substring(0, strDSSoGCN.Length - 2);

        string ChucVu = dtTBLP.Rows[0]["Position"].ToString();
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
        }

        DataTable dt = ReformatDatatable(dtSP);
        mReportDocument.SetDataSource(dt);

        mReportDocument.SetParameterValue("SoGiayThongBaoLePhi", dtTBLP.Rows[0]["SoGiayThongBaoLePhi"]);
        mReportDocument.SetParameterValue("NgayPheDuyet", strNgayPheDuyet);
        mReportDocument.SetParameterValue("TenTiengViet", dtTBLP.Rows[0]["TenTiengViet"]);
        mReportDocument.SetParameterValue("DiaChi", dtTBLP.Rows[0]["DiaChi"]);
        mReportDocument.SetParameterValue("DienThoai", dtTBLP.Rows[0]["DienThoai"]);
        mReportDocument.SetParameterValue("Fax", dtTBLP.Rows[0]["Fax"].ToString());
        mReportDocument.SetParameterValue("SoGCN", strDSSoGCN);
        mReportDocument.SetParameterValue("TongPhi", FormatCurency(long.Parse(dtTBLP.Rows[0]["TongPhi"].ToString()) * 1000));
        mReportDocument.SetParameterValue("TongPhiBangChu", objTalkNumber.Speak(long.Parse(dtTBLP.Rows[0]["TongPhi"].ToString()) * 1000));
        mReportDocument.SetParameterValue("TinhThanhTrungTam", TenTinhThanh);
        mReportDocument.SetParameterValue("SoHoSo", SoHoSo);
        mReportDocument.SetParameterValue("NgayNopHoSo", NgayTiepNhan);
        mReportDocument.SetParameterValue("ChucVu", ChucVu);
        mReportDocument.SetParameterValue("NguoiKy", ProviderFactory.SysUserProvider.GetById(dtTBLP.Rows[0]["NguoiPheDuyetID"].ToString()).FullName);
        mReportDocument.SetParameterValue("ChuKy", "");


        mReportDocument.SetParameterValue("TenCoQuanThuHuong", TenCoQuanThuHuong);
        mReportDocument.SetParameterValue("DiaChiCoQuanThuHuong", DiaChiCQTH);
        mReportDocument.SetParameterValue("SoTaiKhoan", SoTaiKhoan);
        mReportDocument.SetParameterValue("TenKhoBac", TenKhoBac);
        if (soLuongGCN > 1)
            mReportDocument.SetParameterValue("SoLuongGCN", " các");
        else
            mReportDocument.SetParameterValue("SoLuongGCN", "");

        mReportDocument.SetParameterValue("TenTrungTam", TenTrungTam.ToUpper());
        mReportDocument.SetParameterValue("MaSoThue", dtTBLP.Rows[0]["MaSoThue"].ToString());

        return mReportDocument;
    }
    /// <summary>
    /// Hiển thị report Giấy báo lệ phí công bố
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    private ReportDocument GetGiayBaoLePhiCongBoReport(string ID)
    {
        string reportPath = Request.PhysicalApplicationPath + "Report\\GiayBaoLePhiCongBo.rpt";

        this.Title = "In thông báo lệ phí";
        mReportDocument = new ReportDocument();
        mReportDocument.Load(reportPath);

        //Lấy dữ liệu
        string strDSSoBanTiepNhan = "";
        DataTable dtTBLP = new DataTable();
        dtTBLP = ProviderFactory.ThongBaoLePhiProvider.GetThongBaoLePhiCongBo(ID);
        string SoHoSo = string.Empty;
        string NgayTiepNhan = string.Empty;
        string TenTrungTam = string.Empty;
        string HoSoId = string.Empty;
        string TenTinhThanh = string.Empty;
        string TenCoQuanThuHuong = string.Empty;
        string DiaChiCQTH = string.Empty;
        string SoTaiKhoan = string.Empty;
        string TenKhoBac = string.Empty;


        if (dtTBLP.Rows.Count > 0)
        {
            DataRow row = dtTBLP.Rows[0];
            HoSoId = row["HoSoId"].ToString();
        }

        HoSo objHoSo = ProviderFactory.HoSoProvider.GetById(HoSoId);
        SoHoSo = objHoSo.SoHoSo;
        NgayTiepNhan = objHoSo.NgayTiepNhan.Value.ToShortDateString();
        // Lấy thông tin theo trang thái hồ sơ
        if (objHoSo.TrangThaiId == (int)EnTrangThaiHoSoList.CHO_LUU_TRU || objHoSo.TrangThaiId == (int)EnTrangThaiHoSoList.DA_DONG)
        {
            TenTrungTam = objHoSo.DmTrungTamTenTrungTam;
            string TinhThanhId = objHoSo.DmTrungTamTinhThanhId;
            TenTinhThanh = ProviderFactory.DmTinhThanhProvider.GetById(TinhThanhId).TenTinhThanh;
            TenCoQuanThuHuong = objHoSo.DmTrungTamTenCoQuanThuHuongCuc;
            DiaChiCQTH = objHoSo.DmTrungTamDiaChiCoQuanThuHuongCuc;
            SoTaiKhoan = objHoSo.DmTrungTamSoTaiKhoanCuc;
            TenKhoBac = objHoSo.DmTrungTamTenKhoBacCuc;
        }
        else
        {
            DmTrungTam objTrungTam = ProviderFactory.DmTrungTamProvider.GetById(objHoSo.TrungTamId);
            TenTrungTam = objTrungTam.TenTrungTam;
            TenTinhThanh = ProviderFactory.DmTinhThanhProvider.GetById(objTrungTam.TinhThanhId).TenTinhThanh;
            TenCoQuanThuHuong = objTrungTam.TenCoQuanThuHuongCuc;
            DiaChiCQTH = objTrungTam.DiaChiCoQuanThuHuongCuc;
            SoTaiKhoan = objTrungTam.SoTaiKhoanCuc;
            TenKhoBac = objTrungTam.TenKhoBacCuc;
        }

        //Parameter Field
        NumberUtil objTalkNumber = new NumberUtil();

        string strNgayPheDuyet = "Ngày.......tháng........năm.........";
        if (!string.IsNullOrEmpty(dtTBLP.Rows[0]["NgayPheDuyet"].ToString()))
        {
            strNgayPheDuyet = "";
            strNgayPheDuyet += TalkDate(DateTime.Parse(dtTBLP.Rows[0]["NgayPheDuyet"].ToString()));
        }
        foreach (DataRow row in dtTBLP.Rows)
        {
            if (!string.IsNullOrEmpty(row["SoBanTiepNhanCB"].ToString()))
                strDSSoBanTiepNhan += row["SoBanTiepNhanCB"].ToString() + ", ";
        }
        if (!string.IsNullOrEmpty(strDSSoBanTiepNhan))
            strDSSoBanTiepNhan = strDSSoBanTiepNhan.Substring(0, strDSSoBanTiepNhan.Length - 2);
        string ChucVu = dtTBLP.Rows[0]["Position"].ToString();
        switch (ChucVu)
        {
            case "1":
                ChucVu = "TUQ. CỤC TRƯỞNG" + "\n" + "GIÁM ĐỐC \n" + TenTrungTam.ToUpper();
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
                ChucVu = "CỤC TRƯỞNG";
                break;
            case "7":
                ChucVu = "KT. CỤC TRƯỞNG" + "\n" + "PHÓ CỤC TRƯỞNG";
                break;
        }

        mReportDocument.SetDataSource(dtTBLP);

        mReportDocument.SetParameterValue("SoGiayThongBaoLePhi", dtTBLP.Rows[0]["SoGiayThongBaoLePhi"]);
        mReportDocument.SetParameterValue("NgayPheDuyet", strNgayPheDuyet);
        mReportDocument.SetParameterValue("TenTiengViet", dtTBLP.Rows[0]["TenTiengViet"]);
        mReportDocument.SetParameterValue("DiaChi", dtTBLP.Rows[0]["DiaChi"]);
        mReportDocument.SetParameterValue("DienThoai", dtTBLP.Rows[0]["DienThoai"]);
        mReportDocument.SetParameterValue("Fax", dtTBLP.Rows[0]["Fax"].ToString());
        mReportDocument.SetParameterValue("SoGCN", strDSSoBanTiepNhan);
        mReportDocument.SetParameterValue("TongPhi", FormatCurency(long.Parse(dtTBLP.Rows[0]["TongPhi"].ToString())));
        mReportDocument.SetParameterValue("TongPhiBangChu", objTalkNumber.Speak(long.Parse(dtTBLP.Rows[0]["TongPhi"].ToString())));
        mReportDocument.SetParameterValue("TinhThanhTrungTam", TenTinhThanh);
        mReportDocument.SetParameterValue("SoHoSo", SoHoSo);
        mReportDocument.SetParameterValue("NgayNopHoSo", NgayTiepNhan);
        mReportDocument.SetParameterValue("ChuKy", "");
        mReportDocument.SetParameterValue("ChucVu", ChucVu);
        mReportDocument.SetParameterValue("NguoiKy", ProviderFactory.SysUserProvider.GetById(dtTBLP.Rows[0]["NguoiPheDuyetID"].ToString()).FullName);

        mReportDocument.SetParameterValue("TenCoQuanThuHuong", TenCoQuanThuHuong);
        mReportDocument.SetParameterValue("DiaChiCoQuanThuHuong", DiaChiCQTH);
        mReportDocument.SetParameterValue("SoTaiKhoan", SoTaiKhoan);
        mReportDocument.SetParameterValue("TenTrungTam", TenTrungTam.ToUpper());
        mReportDocument.SetParameterValue("TenKhoBac", TenKhoBac);
        return mReportDocument;
    }

    /// <summary>
    /// Hiển thị report Giấy báo lệ phí chứng nhận
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    private ReportDocument GetThongBaoNopTienReport(string ID)
    {
        string reportPath = Request.PhysicalApplicationPath + "Report\\ThongBaoThuTien.rpt";

        this.Title = "In thông báo nộp tiền";
        mReportDocument = new ReportDocument();
        mReportDocument.Load(reportPath);

        //Lấy dữ liệu
        string TrungTamID = mUserInfo.TrungTam.Id;
        DmTrungTam TrungTamObject = ProviderFactory.DmTrungTamProvider.GetById(TrungTamID);

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
            return null;
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
            string TenPhongBan = !string.IsNullOrEmpty(oUser.DepartmentId) ? ProviderFactory.DmPhongBanProvider.GetById(oUser.DepartmentId).TenPhongBan.ToUpper() : "";

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

        if (objThongBao.NgayPheDuyet != null)
            NgayThang = TinhThanh + ", " + TalkDate(objThongBao.NgayPheDuyet.Value);
        else
            NgayThang = TinhThanh + ", " + TalkDate(DateTime.Now);
        // Truyền giá trị cho các ParameterFields
        mReportDocument.ParameterFields["TenTrungTam"].CurrentValues.AddValue(TenTrungTam.ToUpper());
        mReportDocument.ParameterFields["SoGiayThongBao"].CurrentValues.AddValue(objThongBao.SoGiayThongBaoLePhi);
        mReportDocument.ParameterFields["DiaChiTrungTam"].CurrentValues.AddValue(DiaChiTrungTam);
        mReportDocument.ParameterFields["DiaChiDonVi"].CurrentValues.AddValue(DiaChiDonVi);
        mReportDocument.ParameterFields["TenDonVi"].CurrentValues.AddValue(TenDonVi);
        mReportDocument.ParameterFields["DienThoai"].CurrentValues.AddValue(DienThoai);
        mReportDocument.ParameterFields["Fax"].CurrentValues.AddValue(Fax);
        mReportDocument.ParameterFields["TongTienBangSo"].CurrentValues.AddValue(TongTienBangSo);
        mReportDocument.ParameterFields["TongTienBangChu"].CurrentValues.AddValue(TongTienBangChu);
        mReportDocument.ParameterFields["TenDonViThuHuong"].CurrentValues.AddValue(TenDonViThuHuong);
        mReportDocument.ParameterFields["DiaChiDonViThuHuong"].CurrentValues.AddValue(DiaChiDonViThuHuong);
        mReportDocument.ParameterFields["SoTaiKhoan"].CurrentValues.AddValue(SoTaiKhoan);
        mReportDocument.ParameterFields["TenNganHang"].CurrentValues.AddValue(TenNganHang);
        mReportDocument.ParameterFields["ChucVu"].CurrentValues.AddValue(ChucVu);
        mReportDocument.ParameterFields["HoTen"].CurrentValues.AddValue(GiamDoc);
        mReportDocument.ParameterFields["NgayThang"].CurrentValues.AddValue(NgayThang);
        mReportDocument.ParameterFields["SoPhieuNhanHS"].CurrentValues.AddValue(SoPhieuNhanHS);
        mReportDocument.ParameterFields["Line_PhiLayMau"].CurrentValues.AddValue(Line_PhiLayMau);
        mReportDocument.ParameterFields["Line_PhiDanhGiaQuyTrinh"].CurrentValues.AddValue(Line_PhiDanhGia);
        mReportDocument.ParameterFields["NgayTiepNhan"].CurrentValues.AddValue(NgayTiepNhan);
        mReportDocument.ParameterFields["MaSoThue"].CurrentValues.AddValue(MaSoThue != null ? MaSoThue : string.Empty);

        if (SoQuyTrinh > 0)
            mReportDocument.ParameterFields["DanhGiaQuyTrinh"].CurrentValues.AddValue("và đánh giá quy trình sản xuất, đảm bảo chất lượng sản phẩm");
        else
            mReportDocument.ParameterFields["DanhGiaQuyTrinh"].CurrentValues.AddValue("");

        return mReportDocument;
    }

    /// <summary>
    /// Hiển thị report Giấy báo lệ phí chứng nhận
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    private ReportDocument GetGiayBaoLePhiReport_DanhGiaLai(string ID)
    {
        string strBC = Server.MapPath(@"~\Report\ThongBaoNopTien_GiamSat.rpt");
        rd = new ReportDocument();
        rd.Load(strBC);
        DmTrungTam TrungTamObject = ProviderFactory.DmTrungTamProvider.GetById(mUserInfo.TrungTam.Id);
        ThongBaoLePhi objThongBao = ProviderFactory.ThongBaoLePhiProvider.GetById(ID);
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

        return rd;
    }
    // LongHH
    public DataTable ReformatDatatableNew(DataTable dt)
    {
        DataTable temp = new DataTable();
        ArrayList arrNhomSPDistinct = new ArrayList();
        DataTable returnTable = new DataTable();
        returnTable.Columns.Add("GCN");
        returnTable.Columns.Add("DonGia1");
        returnTable.Columns.Add("SoLuong1");
        returnTable.Columns.Add("ThanhTien1");
        returnTable.Columns.Add("DonGia2");
        returnTable.Columns.Add("SoLuong2");
        returnTable.Columns.Add("ThanhTien2");

        string strNhomSP = dt.Rows[0]["NhomSanPham"].ToString();
        arrNhomSPDistinct.Add(strNhomSP);
        //Lấy danh sách các nhóm sản phẩm khác nhau
        foreach (DataRow row in dt.Rows)
        {
            if (!arrNhomSPDistinct.Contains(row["NhomSanPham"].ToString()))
            {
                arrNhomSPDistinct.Add(row["NhomSanPham"].ToString());
            }
        }
        // Mỗi nhóm sản phẩm khác nhau thì lấy ra các bản ghi tương ứng và ghép dư liệu thành 1 row 
        // rồi insert vào bảng returnTable
        for (int i = 0; i < arrNhomSPDistinct.Count; i++)
        {
            int SoLuong = 0;
            string DsTenSP = "";
            string DsKyHieuSP = string.Empty;
            int ThanhTien = 0;
            string DsSoGCN = "";
            int LePhi = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (row["NhomSanPham"].ToString().Equals(arrNhomSPDistinct[i].ToString()))
                {
                    SoLuong += 1;
                    ThanhTien += (int)row["LePhi"];
                    DsTenSP += row["TenSanPham"].ToString().Trim() + ", ";
                    DsKyHieuSP += row["KyHieu"].ToString().Trim() + ", ";
                    DsSoGCN += row["SoGCN"].ToString().Trim() + ", ";
                    LePhi = (int)row["LePhi"];
                }
            }
            DataRow ReturnRow = returnTable.NewRow();
            ReturnRow["SoLuongSanPham"] = SoLuong.ToString();
            if (DsTenSP.Length > 2)
                ReturnRow["TenSanPham"] = DsTenSP.Substring(0, DsTenSP.Length - 2);
            if (DsKyHieuSP.Length > 2)
                ReturnRow["KyHieuSanPham"] = DsKyHieuSP.Substring(0, DsKyHieuSP.Length - 2);
            ReturnRow["TenNhomSanPham"] = arrNhomSPDistinct[i].ToString();
            ReturnRow["LePhi"] = FormatCurency(LePhi);
            ReturnRow["ThanhTien"] = FormatCurency(ThanhTien);
            returnTable.Rows.Add(ReturnRow);
        }
        return returnTable;
    }
    //LongHH
    /// <summary>
    /// Format lại bảng sản phẩm 
    /// Cu: SoLuong,TenSP,LePhi,NhomSPID, ten nhoms sp
    /// Moi:SoLuong(lay theo nhoms sp), ten sp (ds theo nhóm), ten nhóm sp,thanhtien(tong tien tinh theo nhóm)
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// TuanND    5/07/2009     Thêm mới
    /// </Modified>
    public DataTable ReformatDatatable(DataTable dt)
    {
        DataTable temp = new DataTable();
        ArrayList arrNhomSPDistinct = new ArrayList();
        DataTable returnTable = new DataTable();
        returnTable.Columns.Add("SoLuongSanPham");
        returnTable.Columns.Add("TenSanPham");
        returnTable.Columns.Add("KyHieuSanPham");
        returnTable.Columns.Add("TenNhomSanPham");
        returnTable.Columns.Add("LePhi");
        returnTable.Columns.Add("ThanhTien");

        string strNhomSP = dt.Rows[0]["NhomSanPham"].ToString();
        arrNhomSPDistinct.Add(strNhomSP);
        //Lấy danh sách các nhóm sản phẩm khác nhau
        foreach (DataRow row in dt.Rows)
        {
            if (!arrNhomSPDistinct.Contains(row["NhomSanPham"].ToString()))
            {
                arrNhomSPDistinct.Add(row["NhomSanPham"].ToString());
            }
        }
        // Mỗi nhóm sản phẩm khác nhau thì lấy ra các bản ghi tương ứng và ghép dư liệu thành 1 row 
        // rồi insert vào bảng returnTable
        for (int i = 0; i < arrNhomSPDistinct.Count; i++)
        {
            int SoLuong = 0;
            string DsTenSP = "";
            string DsKyHieuSP = string.Empty;
            int ThanhTien = 0;
            string DsSoGCN = "";
            int LePhi = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (row["NhomSanPham"].ToString().Equals(arrNhomSPDistinct[i].ToString()))
                {
                    SoLuong += 1;
                    ThanhTien += (int)row["LePhi"];
                    DsTenSP += row["TenSanPham"].ToString().Trim() + ", ";
                    DsKyHieuSP += row["KyHieu"].ToString().Trim() + ", ";
                    DsSoGCN += row["SoGCN"].ToString().Trim() + ", ";
                    LePhi = (int)row["LePhi"];
                }
            }
            DataRow ReturnRow = returnTable.NewRow();
            ReturnRow["SoLuongSanPham"] = SoLuong.ToString();
            if (DsTenSP.Length > 2)
                ReturnRow["TenSanPham"] = DsTenSP.Substring(0, DsTenSP.Length - 2);
            if (DsKyHieuSP.Length > 2)
                ReturnRow["KyHieuSanPham"] = DsKyHieuSP.Substring(0, DsKyHieuSP.Length - 2);
            ReturnRow["TenNhomSanPham"] = arrNhomSPDistinct[i].ToString();
            ReturnRow["LePhi"] = FormatCurency(LePhi);
            ReturnRow["ThanhTien"] = FormatCurency(ThanhTien);
            returnTable.Rows.Add(ReturnRow);
        }
        return returnTable;
    }
    #endregion
    #region Action
    /// <summary>
    /// Xác nhận thu phí cho thông báo lệ phí
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// TuanND    5/07/2009     Thêm mới
    /// TuanVM      05/06/2009  Sửa (sau khi thu phí, trạng thái các sản phẩm chuyển sang đã cấp GCN)
    /// </Modified>
    protected void btnThuPhi_Click(object sender, EventArgs e)
    {
        using (TransactionManager transaction = ProviderFactory.Transaction)
        {
            string ThongBaoLePhiID = Request["ThongBaoLePhiID"].ToString();
            ThongBaoLePhi objThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetById(ThongBaoLePhiID);

            // Lấy thông tin loại hồ sơ
            string HoSoId = objThongBaoLePhi.HoSoId;
            bool HoSoChungNhan = true;
            HoSo objHs = ProviderFactory.HoSoProvider.GetById(HoSoId);
            if (objHs != null)
                if (objHs.LoaiHoSo == (int)LoaiHoSo.CongBoHopQuy)
                    HoSoChungNhan = false;

            objThongBaoLePhi.TrangThaiId = (int?)EnTrangThaiThongBaoPhiList.DA_THU_PHI;

            if (!string.IsNullOrEmpty(txtNgayThuTien.Text))
                objThongBaoLePhi.NgayThuTien = Convert.ToDateTime(txtNgayThuTien.Text);

            objThongBaoLePhi.SoHoaDon = txtSoHoaDon.Text.Trim();
            DataTable dtSanPhamCuaTBP = ProviderFactory.ThongBaoLePhiProvider.GetSanPham(ThongBaoLePhiID);
            try
            {
                // Lưu thông báo lệ phí
                ProviderFactory.ThongBaoLePhiProvider.Save(transaction, objThongBaoLePhi);
                //Chuyển trạng thái của các sản phẩm trong tblp
                foreach (DataRow row in dtSanPhamCuaTBP.Rows)
                {
                    SanPham objSanPham = ProviderFactory.SanPhamProvider.GetById(row["SanPhamID"].ToString());
                    if (HoSoChungNhan)
                        objSanPham.TrangThaiId = (int)EnTrangThaiSanPhamList.DA_CAP_GCN;
                    else
                        objSanPham.TrangThaiId = (int)EnTrangThaiSanPhamList.DA_CAP_BAN_TIEP_NHAN_CB;

                    ProviderFactory.SanPhamProvider.Save(transaction, objSanPham);
                }
                transaction.Commit();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "closePopUp",
                    "<script>alert('" + Resource.msgCapNhatThanhCong + "');window.opener.location.href='CN_ThongBaoPhi.aspx';window.close();</script>");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
    }
    /// <summary>
    /// Gửi đề nghị hủy thông báo lệ phí cho lãnh đạo phê duyệt
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// TuanND    5/07/2009     Thêm mới
    /// </Modified>
    protected void btnHuyHieuLuc_Click(object sender, EventArgs e)
    {
        string ThongBaoLePhiID = Request["ThongBaoLePhiID"].ToString();
        ThongBaoLePhi objThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetById(ThongBaoLePhiID);

        // Lấy thông tin loại hồ sơ
        string HoSoId = objThongBaoLePhi.HoSoId;
        bool HoSoChungNhan = true;
        HoSo objHs = ProviderFactory.HoSoProvider.GetById(HoSoId);
        if (objHs != null)
            if (objHs.LoaiHoSo == (int)LoaiHoSo.CongBoHopQuy)
                HoSoChungNhan = false;

        if (objThongBaoLePhi.TrangThaiId == (int)EnTrangThaiThongBaoPhiList.CHO_THU_PHI)
        {
            // Gửi đề xuất huỷ
            Response.Redirect("CN_HuyThongBaoLePhi.aspx?ThongBaoLePhiID=" + Request["ThongBaoLePhiID"].ToString());
        }
        else
        {
            // Huỷ
            using (TransactionManager transaction = ProviderFactory.Transaction)
            {
                objThongBaoLePhi.TrangThaiId = (int?)EnTrangThaiThongBaoPhiList.HUY;
                DataTable dtSanPhamCuaTBP = ProviderFactory.ThongBaoLePhiProvider.GetSanPham(ThongBaoLePhiID);
                try
                {
                    ProviderFactory.ThongBaoLePhiProvider.Save(objThongBaoLePhi);
                    //Chuyển trạng thái của các sản phẩm trong tblp
                    foreach (DataRow row in dtSanPhamCuaTBP.Rows)
                    {
                        SanPham objSanPham = ProviderFactory.SanPhamProvider.GetById(row["SanPhamID"].ToString());
                        if (HoSoChungNhan)
                            objSanPham.TrangThaiId = (int)EnTrangThaiSanPhamList.HUY_GCN;
                        else
                            objSanPham.TrangThaiId = (int)EnTrangThaiSanPhamList.HUY_BTN;
                        ProviderFactory.SanPhamProvider.Save(transaction, objSanPham);
                    }
                    transaction.Commit();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "closePopUp",
                        "<script>alert('" + Resource.msgCapNhatThanhCong + "');window.opener.location.href='CN_ThongBaoPhi.aspx';window.close();</script>");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
    /// <summary>
    /// Hủy bỏ thao tác quay lại trang quản lý
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// TuanND    5/07/2009     Thêm mới
    /// </Modified>
    protected void btnBoQua_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "closePopUp",
                "<script>self.close() ;</script>");
    }

    /// <summary>
    /// Phê duyệt việc huỷ TBLP / Phê duyệt thông báo nộp tiền
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// TuanVM    5/07/2009     Thêm mới
    /// </Modified>
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        TransactionManager trans = ProviderFactory.Transaction;
        try
        {
            string ThongBaoLePhiID = Request["ThongBaoLePhiID"].ToString();
            ThongBaoLePhi objThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetById(trans, ThongBaoLePhiID);

            if (rblKetLuan.SelectedValue == "1") // Neu giam doc phe duyet
            {
                // Nếu là thông báo nộp tiền lấy mẫu sản phẩm và đánh giá quy trình sx thì chuyển sang trạng thái tuong ung (Chờ thu phí hoặc duyệt huỷ)
                if (objThongBaoLePhi.LoaiPhiId == null || objThongBaoLePhi.LoaiPhiId == 10)
                {
                    if (objThongBaoLePhi.TrangThaiId == (int)EnTrangThaiThongBaoPhiList.CHO_PHE_DUYET)
                    {
                        objThongBaoLePhi.TrangThaiId = (int)EnTrangThaiThongBaoPhiList.CHO_THU_PHI;
                        //if (objThongBaoLePhi.LoaiPhiId == null)
                        objThongBaoLePhi.SoGiayThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetSoGiayTBLP(objThongBaoLePhi.LoaiPhiId, trans);
                        objThongBaoLePhi.NguoiPheDuyetId = mUserInfo.UserID;
                        SysUser oUser = ProviderFactory.SysUserProvider.GetById(mUserInfo.UserID);
                        if (oUser != null)
                            objThongBaoLePhi.ChucVu = Convert.ToInt32(oUser.Position);
                        objThongBaoLePhi.NgayPheDuyet = ProviderFactory.HoSoProvider.GetById(trans, objThongBaoLePhi.HoSoId).NgayTiepNhan;

                        if (!string.IsNullOrEmpty(txtNgayThuTien.Text))
                            objThongBaoLePhi.NgayThuTien = Convert.ToDateTime(txtNgayThuTien.Text);

                        objThongBaoLePhi.SoHoaDon = txtSoHoaDon.Text.Trim();
                    }
                    else
                        objThongBaoLePhi.TrangThaiId = (int)EnTrangThaiThongBaoPhiList.GD_DUYET_HUY;
                }
                else  // Chuyển giấy báo lệ phí sang duyệt huỷ
                    objThongBaoLePhi.TrangThaiId = (int)EnTrangThaiThongBaoPhiList.GD_DUYET_HUY;
            }
            else // Neu giam doc khong phe duyet
            {
                // Nếu là thông báo nộp tiền lấy mẫu sản phẩm và đánh giá quy trình sx thì chuyển sang trạng thái chờ thu phí hoặc huỷ
                if (objThongBaoLePhi.LoaiPhiId == null || objThongBaoLePhi.LoaiPhiId == 10)
                {
                    // Nếu thông báo lệ phí đang chờ phê duyệt để thu phí thì huỷ TBLP
                    if (objThongBaoLePhi.TrangThaiId == (int)EnTrangThaiThongBaoPhiList.CHO_PHE_DUYET)
                        objThongBaoLePhi.TrangThaiId = (int)EnTrangThaiThongBaoPhiList.MOI_TAO;
                    if (objThongBaoLePhi.TrangThaiId == (int)EnTrangThaiThongBaoPhiList.CHO_DUYET_HUY)
                        objThongBaoLePhi.TrangThaiId = (int)EnTrangThaiThongBaoPhiList.CHO_THU_PHI;
                }
                else // Nếu không phải thông báo nộp tiền
                {
                    objThongBaoLePhi.TrangThaiId = (int)EnTrangThaiThongBaoPhiList.CHO_THU_PHI;
                }
            }

            objThongBaoLePhi.SoHoaDon = txtSoHoaDon.Text.Trim();

            ProviderFactory.ThongBaoLePhiProvider.Save(trans, objThongBaoLePhi);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "closePopUp",
                        "<script>alert('" + Resource.msgCapNhatThanhCong + "');window.opener.location.href='CN_ThongBaoPhi.aspx';window.close();</script>");

            trans.Commit();
        }
        catch (Exception ex)
        {
            trans.Rollback();
            throw ex;
        }
    }
    #endregion
    /// <summary>
    /// Chuyen ngay thang tu datetime sang chuoi ngay thang nam
    /// </summary>
    /// <param name="NgayThang"></param>
    /// <returns></returns>
    public string TalkDate(DateTime NgayThang)
    {
        string ngay = NgayThang.Day.ToString().Length > 1 ? NgayThang.Day.ToString() : "0" + NgayThang.Day.ToString();
        string thang = NgayThang.Month.ToString().Length > 1 ? NgayThang.Month.ToString() : "0" + NgayThang.Month.ToString();

        return "ngày " + ngay + " tháng " + thang + " năm " + NgayThang.Year.ToString();
    }

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
                this.crView.ReportSource = myReport;
                this.crView.DataBind();
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
