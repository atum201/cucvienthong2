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
using System.Collections.Generic;
using System.Transactions;
using Cuc_QLCL.Data;
using CucQLCL.Common;
using System.Text;

public partial class WebUI_GiamSat_SanPhamView : System.Web.UI.Page
{
    string NhomSanPhamId = string.Empty;
    string HoSoId = string.Empty;
    string TrangThaiId = string.Empty;
    string IDSanPham = string.Empty;
    string Action = string.Empty;
    string Direct = string.Empty;
    SanPham ObSanPham = new SanPham();
    List<DictionaryEntry> ListFiles = new List<DictionaryEntry>();
    #region (Form Event)
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    protected void Page_Load(object sender, EventArgs e)
    {





        if (Request["HoSoId"] != null)
            HoSoId = Request["HoSoId"].ToString();        
        if (!IsPostBack)
        {
         
            HoSo objHs = ProviderFactory.HoSoProvider.GetById(HoSoId);
            if (objHs.NguonGocId != (int)EnNguonGocList.SX_TRONG_NUOC_KHONG_CO_ISO)
                rKTCSSX.Visible = false;
            if (Request["SanPhamId"] != null)
            {
                IDSanPham = Request["SanPhamId"].ToString();                
                    Bind_SanPhamForEdit(IDSanPham);               
                
            }

        }

       }




    protected void btnBoQua_Click(object sender, EventArgs e)
    {
        Response.Redirect("GiamSat_View.aspx?direct=CN_HoSoMo&HoSoId=" + HoSoId + "&TrangThaiId=1");
    }


    #endregion

    #region (Ham tu tao)

    /// <summary>
    /// Lấy danh sách các tiêu chuẩn hiển thị ra checklistbox
    /// </summary>
    /// <param name="SanPhamID">ID của sản phẩm</param>    
    /// <Modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modified>        
    public void Bind_ListTieuChuan(string SanPhamID)
    {
        //TList<DmTieuChuan> listTieuChuan = ProviderFactory.DmTieuChuanProvider.GetAll();
        //chklstTieuChuan.DataSource = listTieuChuan;
        //chklstTieuChuan.DataTextField = "Matieuchuan";
        //chklstTieuChuan.DataValueField = "Id";
        //chklstTieuChuan.DataBind();

        chklstTieuChuan.Items.Clear();
        TList<DmSanPhamTieuChuan> listSpTieuChuan = ProviderFactory.DmSanPhamTieuChuanProvider.GetBySanPhamId(SanPhamID);
        foreach (DmSanPhamTieuChuan dmSpTieuChuan in listSpTieuChuan)
        {
            DmTieuChuan dmTieuChuan = ProviderFactory.DmTieuChuanProvider.GetById(dmSpTieuChuan.TieuChuanId);
            ListItem Item = new ListItem(dmTieuChuan.MaTieuChuan, dmTieuChuan.Id);
            chklstTieuChuan.Items.Add(Item);

        }

    }
   
   

    /// <summary>
    /// Lay cac tieu chuan ap dung cho san pham
    /// </summary>
    /// <param name="IDSanPham"></param>   
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    public void GetTieuChuanForSanPham(string IDSanPham)
    {
        TList<SanPhamTieuChuanApDung> listSPTieuChuan = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(IDSanPham);
        foreach (SanPhamTieuChuanApDung o in listSPTieuChuan)
        {
            foreach (ListItem item in chklstTieuChuan.Items)
            {
                string strId = item.Value;
                if (strId == o.TieuChuanApDungId)
                    item.Selected = true;
            }
        }
    }
    /// <summary>
    /// Lấy danh sách các tài liệu đính kèm sản phẩm
    /// </summary>
    /// <param name="IDSanPham"></param>
    /// <Modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modified>
    public void GetTaiLieuForSanPham(string IDSanPham)
    {
        TList<TaiLieuDinhKem> listTaiLieu = ProviderFactory.TaiLieuDinhKemProvider.GetBySanPhamId(IDSanPham);
        if (listTaiLieu != null)
        {
            foreach (TaiLieuDinhKem tl in listTaiLieu)
            {
                string FilePath = tl.TenFile.Trim();
                string FileName = FilePath.Substring(FilePath.LastIndexOf("/") + 1, FilePath.Length - FilePath.LastIndexOf("/") - 1);
                switch (tl.LoaiTaiLieuId)
                {
                    case (int)EnLoaiTaiLieuList.DON_DE_NGHI_CN: lblTaiLieuDeNghi.Text = ShowFileLink("Đơn đề nghị chứng nhận ", FilePath);
                        lblTaiLieuDeNghi.Visible = true;
                        break;
                    case (int)EnLoaiTaiLieuList.GIAY_TO_TU_CACH_PHAP_NHAN: lblTuCachPhapNhan.Text = ShowFileLink("Tư cách pháp nhân", FilePath);
                        lblTuCachPhapNhan.Visible = true;
                        break;
                    case (int)EnLoaiTaiLieuList.TAI_LIEU_KY_THUAT: lblTaiLieuKyThuat.Text = ShowFileLink("Tài liệu kỹ thuật", FilePath);
                        lblTaiLieuKyThuat.Visible = true;
                        break;
                    case (int)EnLoaiTaiLieuList.KET_QUA_DO_KIEM: lblKetQuaDoKiem.Text = ShowFileLink("Kết quả đo kiểm", FilePath);
                        lblKetQuaDoKiem.Visible = true;
                        break;
                    case (int)EnLoaiTaiLieuList.QUY_TRINH_SAN_XUAT: lblTaiLieuQuyTrinhSanXuat.Text = ShowFileLink("Quy trình sản xuất", FilePath);
                        lblTaiLieuQuyTrinhSanXuat.Visible = true;
                        break;
                    case (int)EnLoaiTaiLieuList.QUY_TRINH_CHAT_LUONG: lblQuyTrinhDamBao.Text = ShowFileLink("Quy trình đảm bảo CL", FilePath);
                        lblQuyTrinhDamBao.Visible = true;
                        break;
                    case (int)EnLoaiTaiLieuList.CHUNG_CHI_HE_THONG_QLCL: lblChungChiHeThong.Text = ShowFileLink("Chứng chỉ hệ thống QLCL", FilePath);
                        lblChungChiHeThong.Visible = true;
                        break;
                    case (int)EnLoaiTaiLieuList.TIEU_CHUAN_TU_NGUYEN_AP_DUNG: lblTieuChuanTuNguyen.Text = ShowFileLink("Tiêu chuẩn tự nguyện", FilePath);
                        lblTieuChuanTuNguyen.Visible = true;
                        break;
                    case (int)EnLoaiTaiLieuList.CHI_TIEU_KY_THUAT_KEM_THEO: lblChiTieuKyThuatKemTheo.Text = ShowFileLink("Chỉ tiêu kỹ thuật kèm theo", FilePath);
                        lblChiTieuKyThuatKemTheo.Visible = true;
                        break;
                }
            }
        }

    }
    /// <summary>
    /// Hiển thị các thông tin về sản phẩm
    /// </summary>
    /// <param name="IDSanPham"></param>
    /// <Modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modified>
    public void Bind_SanPhamForEdit(string IDSanPham)
    {
        if (HoSoId.Length > 0)
        {
            HoSo hs = ProviderFactory.HoSoProvider.GetById(HoSoId);
            if (hs != null)
                txtSoHoSo.Text = hs.SoHoSo;
        }
        SanPham sp = ProviderFactory.SanPhamProvider.GetById(IDSanPham);
        txtKyHieuSanPham.Text = sp.KyHieu;        
        lblTenSanPham.Text = ProviderFactory.DmSanPhamProvider.GetById(sp.SanPhamId).TenTiengViet;
        lblHangSanXuat.Text = ProviderFactory.DmHangSanXuatProvider.GetById(sp.HangSanXuatId).TenHangSanXuat;
        BindTListDmTieuChuan(sp.Id, sp.SanPhamId);
        txtSoDoKiem.Text = sp.SoDoKiem;        
        if (sp.NgayDoKiem != null)
            txtNgayDoKiem.Text = Convert.ToDateTime(sp.NgayDoKiem).ToShortDateString();
        //*Cơ quan đo lường
        lblCoQuanDoKiem.Text=ProviderFactory.DmCoQuanDoKiemProvider.GetById(sp.CoQuanDoKiemId).TenCoQuanDoKiem;
        
        //* hợp lệ
        bool blHopLe = false;
        if (sp.HopLe != null)
        { blHopLe = Convert.ToBoolean(sp.HopLe);
        if (blHopLe)
        {
            lblHopLe.Text = "Hợp lệ";
        }
        else
        {
            lblHopLe.Text = "Không hợp lệ";
        }
        
        }
       
        //* đầy đủ
        bool blDayDu = false;
        if (sp.DayDu != null)
         blDayDu = Convert.ToBoolean(sp.DayDu);
     if (blDayDu)
     {
         lblDayDu.Text = "Đầy đủ";
     }
     else
     {
         lblDayDu.Text = "Không đầy đủ";
     }
        
        //kết quả kiểm cơ sở sản xuất
        bool blDat = false;
        if (sp.KetQuaKiemTraCssx != null)
            blDat = Convert.ToBoolean(sp.KetQuaKiemTraCssx);
        if (blDat)
        {
            lbDat.Text = "Đạt";
        }
        else
        {
            lbDat.Text = "Không đạt";
        }
        //Quy hoạch tần số
        bool blPhuHop = false;
        if (sp.QuyHoachTanSo != null)
            blPhuHop = Convert.ToBoolean(sp.QuyHoachTanSo);
        if (blPhuHop)
        {
            lblPhuHop.Text = "Phù hợp";
        }
        else
        {
            lblPhuHop.Text = "Không phù hợp";
        }
        //*Nội dung đo kiểm
        //if (sp.NoiDungDoKiem == null && intTrangThai == Convert.ToInt32(EnTrangThaiSanPhamList.CHO_XU_LY))
        //    txtNoiDungDoKiem.Text = GetNoiDungDoKiemTamThoi();
        //else
        if (!string.IsNullOrEmpty(sp.NoiDungDoKiem))
            txtNoiDungDoKiem.Text = sp.NoiDungDoKiem;
        else
            txtNoiDungDoKiem.Text = string.Empty;
        //*Nhận xét khác
        txtNhanXetKhac.Text = sp.NhanXetKhac;

        int intKetLuan = 0;
        if (sp.KetLuanId != null)
            intKetLuan = Convert.ToInt32(sp.KetLuanId);
        switch (intKetLuan)
        {
            case 1: lblCapGCN.Text = "Cấp giấy chứng nhận";
                break;
            case 2: lblCapGCN.Text = "Không cấp giấy chứng nhận.";
                break;
            case 3: lblCapGCN.Text = "Không phải chứng nhận";
                break;
            case 4: lblCapGCN.Text = "Huỷ sản phẩm";
                break;
            default:
                break;
        }

        TList<QuaTrinhXuLy> listqt = ProviderFactory.QuaTrinhXuLyProvider.GetBySanPhamId(IDSanPham);
        if (listqt != null)
        {
            foreach (QuaTrinhXuLy qt in listqt)
            {
                if (qt.LoaiXuLyId == 1)
                {
                    txtNoiDungXuLy.Text = qt.NoiDungXuLy;
                    break;
                }
            }
        }
        txtNgayCap.Text = sp.NgayKyDuyet.Value.ToShortDateString();
        txtSoGCNCV.Text = sp.SoGcn;
        DmNhomSanPham dmNhomSanPham = ProviderFactory.DmNhomSanPhamProvider.GetById(sp.NhomSanPhamId);
            if (dmNhomSanPham != null)
            {
                txtNhomSanPham.Text = dmNhomSanPham.MaNhom;
                txtLePhi.Text = string.Format("{0:0,0}.000", dmNhomSanPham.MucLePhi);
                if (dmNhomSanPham.ThoiHanGcn == (int)EnThoiHanList.HAI_NAM)
                    txtThoiHan.Text = "2 năm";
                if (dmNhomSanPham.ThoiHanGcn == (int)EnThoiHanList.BA_NAM)
                    txtThoiHan.Text = "3 năm";
                if (!dmNhomSanPham.LienQuanTanSo)
                    rTanSo.Visible = false;
            }
        
        GetTieuChuanForSanPham(IDSanPham);
        GetTaiLieuForSanPham(IDSanPham);


    }

    /// <summary>
    /// Lấy tiêu chuẩn của sản phẩm
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                12/5/2009              
    /// </Modified>
    private void BindTListDmTieuChuan(string ID, string SanphamID)
    {
        TList<DmSanPhamTieuChuan> lstDmSanPhamTieuChuan = ProviderFactory.DmSanPhamTieuChuanProvider.GetBySanPhamId(SanphamID);
        TList<DmTieuChuan> lstDmTieuChuan = new TList<DmTieuChuan>();
        foreach (DmSanPhamTieuChuan objDmSanPhamTieuChuan in lstDmSanPhamTieuChuan)
        {
            DmTieuChuan objDmTieuChuan = ProviderFactory.DmTieuChuanProvider.GetById(objDmSanPhamTieuChuan.TieuChuanId);
            if (objDmTieuChuan != null && !lstDmTieuChuan.Contains(objDmTieuChuan))
                lstDmTieuChuan.Add(objDmTieuChuan);
        }
        chklstTieuChuan.DataValueField = "ID";
        chklstTieuChuan.DataTextField = "MaTieuChuan";
        chklstTieuChuan.DataSource = lstDmTieuChuan;
        chklstTieuChuan.DataBind();
        if (!string.IsNullOrEmpty(ID))
        {
            TList<SanPhamTieuChuanApDung> lstSanPhamTieuChuanApDung = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(ID);
            foreach (SanPhamTieuChuanApDung obj in lstSanPhamTieuChuanApDung)
            {
                if (chklstTieuChuan.Items.FindByValue(obj.TieuChuanApDungId) != null)
                    chklstTieuChuan.Items.FindByValue(obj.TieuChuanApDungId).Selected = true;
            }
        }
    }

    public string ShowFileLink(string funame, string FilePath)
    {
        StringBuilder sb = new StringBuilder();
        //string temp = FileName;
        //int l = FileName.Length;
        //if (l > 40)
        //{

        //    temp = FileName.Substring(0, 10) + "...";
        //    temp += FileName.Substring(l - 10, 10);
        //}
        sb.Append("<a href=javascript:void(window.open('" + FilePath + "'))>");
        sb.Append(funame);
        sb.Append("</a>");
        return sb.ToString();
    }





    

    #endregion


    




}
