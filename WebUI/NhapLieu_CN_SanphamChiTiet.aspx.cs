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
using System.Resources;
using System.Collections.Generic;
using CucQLCL.Common;

public partial class WebUI_NhapLieu_CN_SanphamChiTiet : PageBase
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
        rvCheckDate.MaximumValue = DateTime.Now.ToShortDateString();
        InitListFile();
        SetBackFilesWaitForUpload();
        if (this.IsPostBack)
        {

            string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];
            //nếu trang postback là trang thêm mới sản phẩm trong danh mục sản phẩm
            if (eventTarget == "SanPhamPostBack")
            {
                Bind_ListTenSanPham();
                string passedArgument = Request.Params.Get("__EVENTARGUMENT");
                ddlTenSanPham.SelectedValue = passedArgument;
                ddlTenSanPham_SelectedIndexChanged(null, null);
            }
            //nếu trang postback là trang thêm mới hãng sản xuất
            else if (eventTarget == "HangSanXuatPostBack")
            {
                Bind_ListHangSanXuat();
                string passedArgument = Request.Params.Get("__EVENTARGUMENT");
                ddlHangSanXuat.SelectedValue = passedArgument;
            }
            else if (eventTarget == "CoQuanDoKiemPostBack")
            {
                BindTListDmCoQuanDoKiem();
                string passedArgument = Request.Params.Get("__EVENTARGUMENT");
                ddlCoQuanDoLuong.SelectedValue = passedArgument;
            }

        }

        if (Request["Action"] != null)
        {
            Action = Request["Action"].ToString().Trim().ToLower();
        }
        if (Request["HoSoId"] != null)
            HoSoId = Request["HoSoId"].ToString();
        if (Request["TrangThaiId"] != null)
            TrangThaiId = Request["TrangThaiId"].ToString().Trim().ToLower();
        if (Request["direct"] != null)
            Direct = Request["direct"].ToString().Trim().ToLower();

        //thiet lam web path cho trang
        CreateWebPath();

        if (!IsPostBack)
        {
            Bind_ListTenSanPham();
            Bind_ListHangSanXuat();
            BindTListDmCoQuanDoKiem();
            ClearFileSession();
            lnkThemMoiSanPham.Visible = mUserInfo.IsInRole(EnPermission.QUANLY_DM_SANPHAM);
            lnkThemMoiHangSX.Visible = mUserInfo.IsInRole(EnPermission.QUANLY_DM_HANGSX);
            rblKetLuan.Attributes.Add("onclick", "ShowHideThoiHan();");

            HoSo objHs = ProviderFactory.HoSoProvider.GetById(HoSoId);
            if (objHs.NguonGocId != (int)EnNguonGocList.SX_TRONG_NUOC_KHONG_CO_ISO)
                rKTCSSX.Visible = false;

        }

        if (Action == "edit")//nếu là sửa thông tin sản phẩm
        {
            if (Request["SanPhamId"] != null)
            {
                IDSanPham = Request["SanPhamId"].ToString();
                if (!IsPostBack)
                    Bind_SanPhamForEdit(IDSanPham);

                if (ddlTenSanPham.SelectedIndex > 0)
                {
                    string strSpId = ddlTenSanPham.SelectedValue;
                    DmSanPham dmSanPham = ProviderFactory.DmSanPhamProvider.GetById(strSpId);
                    if (dmSanPham != null)
                    {
                        NhomSanPhamId = dmSanPham.NhomSanPhamId;
                    }

                }
            }
        }
        else //nếu là thêm mới
        {
            if (HoSoId.Length > 0)
            {
                HoSo hs = ProviderFactory.HoSoProvider.GetById(HoSoId);
                if (hs != null)
                    txtSoHoSo.Text = hs.SoHoSo;
            }
            if (ddlTenSanPham.SelectedIndex > 0)
            {
                string strSpId = ddlTenSanPham.SelectedValue;
                DmSanPham dmSanPham = ProviderFactory.DmSanPhamProvider.GetById(strSpId);
                if (dmSanPham != null)
                {
                    NhomSanPhamId = dmSanPham.NhomSanPhamId;
                }
                //Bind_ListTieuChuan(strSpId);
            }
        }

        SetFileUploadsReadOnly();

        // Set thuộc tính của link thêm mói sản phẩm theo loại hồ sơ
        if (HoSoId.Length > 0)
        {
            HoSo objHso = DataRepository.HoSoProvider.GetById(HoSoId);
            if (objHso.LoaiHoSo == (int)LoaiHoSo.ChungNhanHopQuy)
            {
                lnkThemMoiSanPham.Attributes.Add("onclick", "return popCenter('DM_SanPham_ChiTiet.aspx?PostBack=NhapLieu_CN_SanphamChiTiet&LoaiHinhChungNhan=1' ,'Popup_TaoMoiSP',950,600); return false");
            }
            else
            {
                lnkThemMoiSanPham.Attributes.Add("onclick", "return popCenter('DM_SanPham_ChiTiet.aspx?PostBack=NhapLieu_CN_SanphamChiTiet&LoaiHinhChungNhan=2' ,'Popup_TaoMoiSP',950,600); return false");
            }
        }
        lnkThemMoiHangSX.Attributes.Add("onclick", "return popCenter('DM_HangSanXuat_ChiTiet.aspx?PostBack=NhapLieu_CN_SanphamChiTiet','Popup_TaoMoiSP',600,150); return false");

    }

    //Cập nhật thông tin về sản phẩm
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        TransactionManager transaction = ProviderFactory.Transaction;

        HoSo objHoSo = ProviderFactory.HoSoProvider.GetById(HoSoId);
        //lấy các thông tin về file chờ upload
        GetFilesWaitForUpLoad();

        //kiểm tra dung lượng và loai file up load lên
        bool ck = CheckFileUploadsSize();
        if (!ck)
        {
            SetBackFilesWaitForUpload();
            ShowFilesWaitForUpLoad();
            return;
        }
        ck = CheckFilesType();
        if (!ck)
        {

            SetBackFilesWaitForUpload();
            ShowFilesWaitForUpLoad();
            return;
        }

        ShowFilesWaitForUpLoad();
        //kiểm tra xem có tiêu chuẩn được chọn hay ko?
        ck = CheckListTieuChuan();
        if (!ck)
            return;

        if (Action == "edit" && IDSanPham.Length > 0)
        {
            //Update thong tin san pham thuoc ho so
            ObSanPham = ProviderFactory.SanPhamProvider.GetById(IDSanPham);
            LayThongTinSanPham(ref ObSanPham, objHoSo, transaction);

            if (ObSanPham.IsValid)
            {
                try
                {
                    ProviderFactory.SanPhamProvider.Update(transaction, ObSanPham);
                    if (ObSanPham.KetLuanId == (int)EnKetLuanList.CAP_GCN)
                        ObSanPham.SoGcn = SinhSoGCN(ObSanPham, transaction);
                    ProviderFactory.SanPhamProvider.Save(transaction, ObSanPham);
                    transaction.Commit();
                    transaction.Dispose();

                    //lưu "Nội dung xử lý" & "Ghi chú" vào trong bảng QuaTrinhXuly
                    transaction = ProviderFactory.Transaction;
                    UpdateQuatrinhXuLySanPham(IDSanPham, transaction);
                    transaction.Commit();
                    transaction.Dispose();

                    //Cập nhật tiêu chuẩn cho sản phẩm
                    transaction = ProviderFactory.Transaction;
                    ClearTieuChuanForSanPham(IDSanPham, transaction);
                    transaction.Commit();
                    transaction.Dispose();

                    transaction = ProviderFactory.Transaction;
                    UpdateTieuChuanForSanPham(IDSanPham, transaction);
                    transaction.Commit();
                    transaction.Dispose();

                    //cập nhật tài liệu đính kèm sản phẩm
                    UpdateTaiLieuDinhKemForSanPham(IDSanPham);
                    string ThongBao = Resources.Resource.msgCapNhatSanPham;

                    // Sao chep thong tin
                    SanPham_SaoChepDuLieu(ObSanPham);

                    //Ghi nhật ký chương trình
                    string strLogString = "Sửa thông tin Sản phẩm chứng nhận có mã là: " + IDSanPham;
                    ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.CN_SAN_PHAM_SUA, strLogString);
                    //Hướng về trang quản lý sản phẩm của hồ sơ
                    Thong_bao(this.Page, ThongBao, "NhapLieu_CN_SanPhamQuanLy.aspx?direct=CN_HoSoMo&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }

            }
        }
        else
        {
            //Them moi san pham vao ho so
            SanPham ObSanPham = new SanPham();
            ObSanPham.HoSoId = objHoSo.Id;
            LayThongTinSanPham(ref ObSanPham, objHoSo, transaction);
            if (ObSanPham.IsValid)
            {
                try
                {
                    ProviderFactory.SanPhamProvider.Insert(transaction, ObSanPham);
                    if (ObSanPham.KetLuanId == (int)EnKetLuanList.CAP_GCN)
                        ObSanPham.SoGcn = SinhSoGCN(ObSanPham, transaction);
                    ProviderFactory.SanPhamProvider.Save(transaction, ObSanPham);
                    string id = ObSanPham.Id;

                    //Cập nhật tiêu chuẩn cho sản phẩm
                    UpdateTieuChuanForSanPham(id, transaction);

                    //lưu "Nội dung xử lý" & "Ghi chú" vào trong bảng QuaTrinhXuly
                    UpdateQuatrinhXuLySanPham(id, transaction);

                    transaction.Commit();
                    //cập nhật tài liệu đính kèm sản phẩm
                    UpdateTaiLieuDinhKemForSanPham(id);
                    string ThongBao = Resources.Resource.msgThemmoiSanPham;

                    // Sao chep thong tin
                    SanPham_SaoChepDuLieu(ObSanPham);

                    //Hướng về trang quản lý sản phẩm của hồ sơ
                    Thong_bao(this.Page, ThongBao, "NhapLieu_CN_SanPhamQuanLy.aspx?direct=CN_HoSoMo&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId);

                    //Ghi nhật ký chương trình
                    string strLogString = "Thêm mới Sản phẩm chứng nhận có mã là: " + id;
                    ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.CN_SAN_PHAM_THEM_MOI, strLogString);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }



            }

        }
        ClearFileSession();

    }

    private void LayThongTinSanPham(ref SanPham ObSanPham, HoSo objHoSo, TransactionManager transaction)
    {
        ObSanPham.KyHieu = txtKyHieuSanPham.Text.Trim();
        ObSanPham.HangSanXuatId = ddlHangSanXuat.SelectedValue;
        ObSanPham.NhomSanPhamId = NhomSanPhamId;
        ObSanPham.SanPhamId = ddlTenSanPham.SelectedValue;
        ObSanPham.NgayCapNhatSauCung = DateTime.Now;

        ObSanPham.SoDoKiem = txtSoDoKiem.Text.Trim();
        //cơ quan đo kiểm
        if (!string.IsNullOrEmpty(ddlCoQuanDoLuong.SelectedValue))
            ObSanPham.CoQuanDoKiemId = ddlCoQuanDoLuong.SelectedValue;
        else
            ObSanPham.CoQuanDoKiemId = null;
        //hồ sơ đầy đủ
        if (!string.IsNullOrEmpty(rblDayDu.SelectedValue))
            ObSanPham.DayDu = Convert.ToBoolean(Convert.ToInt32(rblDayDu.SelectedValue));
        else
            ObSanPham.DayDu = null;
        //hồ sơ hợp lệ
        if (!string.IsNullOrEmpty(rblHopLe.SelectedValue))
            ObSanPham.HopLe = Convert.ToBoolean(Convert.ToInt32(rblHopLe.SelectedValue));
        else
            ObSanPham.HopLe = null;

        if (txtNgayDoKiem.Text.Trim() == string.Empty)
            ObSanPham.NgayDoKiem = null;
        else
            ObSanPham.NgayDoKiem = Convert.ToDateTime(txtNgayDoKiem.Text);

        //cơ sở sản xuất
        if (objHoSo.NguonGocId != null &&
                                (Convert.ToInt32(objHoSo.NguonGocId) == Convert.ToInt32(EnNguonGocList.SX_TRONG_NUOC_KHONG_CO_ISO)))
        {
            if (!string.IsNullOrEmpty(rblKetQuaKiemTra.SelectedValue))
                ObSanPham.KetQuaKiemTraCssx = Convert.ToBoolean(Convert.ToInt32(rblKetQuaKiemTra.SelectedValue));

        }
        else
            ObSanPham.KetQuaKiemTraCssx = null;
        //quy hoạch tần số
        DmNhomSanPham objNhomSanPham = ProviderFactory.DmNhomSanPhamProvider.GetById(transaction, ObSanPham.NhomSanPhamId);
        if (objNhomSanPham.LienQuanTanSo)
        {
            if (!string.IsNullOrEmpty(rblQuyHoachTanSo.SelectedValue))
                ObSanPham.QuyHoachTanSo = Convert.ToBoolean(Convert.ToInt32(rblQuyHoachTanSo.SelectedValue));
        }
        else
            ObSanPham.QuyHoachTanSo = null;

        ObSanPham.NoiDungDoKiem = txtNoiDungDoKiem.Text;
        ObSanPham.NgayKyDuyet = Convert.ToDateTime(txtNgayCap.Text);
        ObSanPham.NhanXetKhac = txtNhanXetKhac.Text;

        ObSanPham.KetLuanId = Convert.ToInt32(rblKetLuan.SelectedValue);
        if (ObSanPham.KetLuanId == (int)EnKetLuanList.CAP_GCN)
        {
            ObSanPham.TrangThaiId = (int)EnTrangThaiSanPhamList.DA_CAP_GCN;
            ObSanPham.ThoiHanId = objNhomSanPham.ThoiHanGcn;
            ObSanPham.SoGcn = SinhSoGCN(ObSanPham, transaction);
        }
        else
        {
            ObSanPham.TrangThaiId = (int)EnTrangThaiSanPhamList.KET_THUC;
            ObSanPham.SoGcn = txtSoGCNCV.Text;
        }

        ObSanPham.NguoiKyDuyetId = mUserInfo.GiamDoc.Id;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    protected void btnBoQua_Click(object sender, EventArgs e)
    {
        Response.Redirect("NhapLieu_CN_SanPhamQuanLy.aspx?direct=CN_HoSoMo&HoSoId=" + HoSoId + "&TrangThaiId=1");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    protected void ddlTenSanPham_SelectedIndexChanged(object sender, EventArgs e)
    {

        string strSpId = ddlTenSanPham.SelectedValue;

        txtNhomSanPham.Text = string.Empty;
        //lấy thông tin về nhóm sản phẩm
        DmSanPham dmSanPham = ProviderFactory.DmSanPhamProvider.GetById(strSpId);
        if (dmSanPham != null)
        {
            NhomSanPhamId = dmSanPham.NhomSanPhamId;
        }
        DmNhomSanPham dmNhomSanPham = ProviderFactory.DmNhomSanPhamProvider.GetById(NhomSanPhamId);
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
        Bind_ListTieuChuan(strSpId);
        GetFilesWaitForUpLoad();
        CheckFileUploadsSize();
        CheckFilesType();
        SetBackFilesWaitForUpload();
        ShowFilesWaitForUpLoad();


    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>    
    protected void lnkXoaTuCachPhapNhan_Click(object sender, EventArgs e)
    {
        lblTuCachPhapNhan.Visible = false;
        GetFilesWaitForUpLoad();
        //reset thông tin về FileUploadGiayToTuCachPhapNhan  
        SetFileWaitUpload("FileUploadGiayToTuCachPhapNhan", null);
        ShowFilesWaitForUpLoad();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>    
    protected void lnkXoaTaiLieuKyThuat_Click(object sender, EventArgs e)
    {
        lblTaiLieuKyThuat.Visible = false;

        GetFilesWaitForUpLoad();
        //reset thông tin về FileUploadTaiLieuKyThuat  
        SetFileWaitUpload("FileUploadTaiLieuKyThuat", null);
        ShowFilesWaitForUpLoad();

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>    
    protected void lnkXoaTaiLieuSanXuat_Click(object sender, EventArgs e)
    {
        lblTaiLieuQuyTrinhSanXuat.Visible = false;
        GetFilesWaitForUpLoad();
        //reset thông tin về FileUploadTaiLieuQuyTrinhSanXuat  
        SetFileWaitUpload("FileUploadTaiLieuQuyTrinhSanXuat", null);
        ShowFilesWaitForUpLoad();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>    
    protected void lnkXoaTaiLieuDeNghi_Click(object sender, EventArgs e)
    {
        lblTaiLieuDeNghi.Visible = false;
        GetFilesWaitForUpLoad();
        //reset thông tin về FileUploadTaiLieuDeNghiCN  
        SetFileWaitUpload("FileUploadTaiLieuDeNghiCN", null);
        SetBackFilesWaitForUpload();
        ShowFilesWaitForUpLoad();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>    
    protected void lnkXoaKetQuaDoKiem_Click(object sender, EventArgs e)
    {
        lblKetQuaDoKiem.Visible = false;
        GetFilesWaitForUpLoad();
        SetFileWaitUpload("FileUploadKetQuaDoKiem", null);
        //Session["FileUploadKetQuaDoKiem"] = null;
        //SetBackFilesWaitForUpload();
        ShowFilesWaitForUpLoad();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>    
    protected void lnkXoaQuyTrinhDamBao_Click(object sender, EventArgs e)
    {
        lblQuyTrinhDamBao.Visible = false;
        GetFilesWaitForUpLoad();
        SetFileWaitUpload("FileUploadQuyTrinhDamBao", null);
        //Session["FileUploadQuyTrinhDamBao"] = null;
        //SetBackFilesWaitForUpload();
        ShowFilesWaitForUpLoad();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>    
    protected void lnkXoaTieuChuanTuNguyen_Click(object sender, EventArgs e)
    {
        lblTieuChuanTuNguyen.Visible = false;
        GetFilesWaitForUpLoad();
        SetFileWaitUpload("FileUploadTieuChuanTuNguyen", null);
        //Session["FileUploadTieuChuanTuNguyen"] = null;
        //SetBackFilesWaitForUpload();
        ShowFilesWaitForUpLoad();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>    
    protected void lnkXoaChungChiHeThong_Click(object sender, EventArgs e)
    {

        lblChungChiHeThong.Visible = false;
        GetFilesWaitForUpLoad();
        SetFileWaitUpload("FileUploadChungChiHeThong", null);
        //Session["FileUploadChungChiHeThong"] = null;
        //SetBackFilesWaitForUpload();
        ShowFilesWaitForUpLoad();
    }

    protected void lnkXoaChiTieuKyThuatKemTheo_Click(object sender, EventArgs e)
    {
        lblChiTieuKyThuatKemTheo.Visible = false;
        GetFilesWaitForUpLoad();
        SetFileWaitUpload("FileUploadChiTieuKyThuatKemTheo", null);
        ShowFilesWaitForUpLoad();
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
    /// Lấy danh sách các ten san pham 
    /// </summary>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    public void Bind_ListTenSanPham()
    {
        int LoaiHinhChungNhan = 0;
        HoSo objHoso = DataRepository.HoSoProvider.GetById(Request["HoSoId"].ToString());
        if (objHoso.LoaiHoSo == (int)LoaiHoSo.ChungNhanHopQuy)
            LoaiHinhChungNhan = 1;
        else
            LoaiHinhChungNhan = 2;

        DataTable dmSanPham = ProviderFactory.DmSanPhamProvider.GetByLoaiHoSo(LoaiHinhChungNhan, 1);
        DataView dvSanPham = new DataView(dmSanPham);
        dvSanPham.Sort = "TenTiengViet";
        ddlTenSanPham.DataSource = dvSanPham.ToTable();
        ddlTenSanPham.DataTextField = "tentiengviet";
        ddlTenSanPham.DataValueField = "id";
        ddlTenSanPham.DataBind();
        ListItem item = new ListItem("Chọn...", "0");
        ddlTenSanPham.Items.Insert(0, item);

    }
    /// <summary>
    /// Lấy danh sách các hãng sản xuất
    /// </summary>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    public void Bind_ListHangSanXuat()
    {
        TList<DmHangSanXuat> dmHangSanXuat = ProviderFactory.DmHangSanXuatProvider.GetAll();
        ddlHangSanXuat.DataSource = dmHangSanXuat;
        ddlHangSanXuat.DataTextField = "TenHangSanXuat";
        ddlHangSanXuat.DataValueField = "Id";
        ddlHangSanXuat.DataBind();
        ListItem item = new ListItem("Chọn...", "0");
        ddlHangSanXuat.Items.Insert(0, item);
    }
    /// <summary>
    /// Cập nhật cac Tieu chuan cho  San pham
    /// </summary>
    /// <param name="IdSanPham"></param>
    /// <param name="transaction"></param>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    public void UpdateTieuChuanForSanPham(string IdSanPham, TransactionManager transaction)
    {
        foreach (ListItem item in chklstTieuChuan.Items)
        {
            if (item.Selected)
            {
                string strId = item.Value;
                try
                {
                    SanPhamTieuChuanApDung o = new SanPhamTieuChuanApDung();
                    o.SanPhamId = IdSanPham;
                    o.TieuChuanApDungId = strId;
                    o.NgayCapNhatSauCung = DateTime.Now;
                    ProviderFactory.SanPhamTieuChuanApDungProvider.Save(transaction, o);
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }

    }
    /// <summary>
    /// Cập nhật cac tai lieu dinh kem cho  San pham
    /// </summary>
    /// <param name="IDSanPham"></param>   
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    public void UpdateTaiLieuDinhKemForSanPham(string IDSanPham)
    {

        UpdateTaiLieuDinhKem(IDSanPham, ref FileUploadTaiLieuDeNghiCN, (int)EnLoaiTaiLieuList.DON_DE_NGHI_CN, ref lblTaiLieuDeNghi);
        UpdateTaiLieuDinhKem(IDSanPham, ref FileUploadGiayToTuCachPhapNhan, (int)EnLoaiTaiLieuList.GIAY_TO_TU_CACH_PHAP_NHAN, ref lblTuCachPhapNhan);
        UpdateTaiLieuDinhKem(IDSanPham, ref FileUploadTaiLieuKyThuat, (int)EnLoaiTaiLieuList.TAI_LIEU_KY_THUAT, ref  lblTaiLieuKyThuat);
        UpdateTaiLieuDinhKem(IDSanPham, ref FileUploadKetQuaDoKiem, (int)EnLoaiTaiLieuList.KET_QUA_DO_KIEM, ref lblKetQuaDoKiem);
        UpdateTaiLieuDinhKem(IDSanPham, ref FileUploadTaiLieuQuyTrinhSanXuat, (int)EnLoaiTaiLieuList.QUY_TRINH_SAN_XUAT, ref  lblTaiLieuQuyTrinhSanXuat);
        UpdateTaiLieuDinhKem(IDSanPham, ref FileUploadQuyTrinhDamBao, (int)EnLoaiTaiLieuList.QUY_TRINH_CHAT_LUONG, ref lblQuyTrinhDamBao);
        UpdateTaiLieuDinhKem(IDSanPham, ref FileUploadChungChiHeThong, (int)EnLoaiTaiLieuList.CHUNG_CHI_HE_THONG_QLCL, ref lblChungChiHeThong);
        UpdateTaiLieuDinhKem(IDSanPham, ref FileUploadTieuChuanTuNguyen, (int)EnLoaiTaiLieuList.TIEU_CHUAN_TU_NGUYEN_AP_DUNG, ref lblTieuChuanTuNguyen);
        UpdateTaiLieuDinhKem(IDSanPham, ref FileUploadChiTieuKyThuatKemTheo, (int)EnLoaiTaiLieuList.CHI_TIEU_KY_THUAT_KEM_THEO, ref lblChiTieuKyThuatKemTheo);

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
                        lnkXoaTaiLieuDeNghi.Visible = true;
                        break;
                    case (int)EnLoaiTaiLieuList.GIAY_TO_TU_CACH_PHAP_NHAN: lblTuCachPhapNhan.Text = ShowFileLink("Tư cách pháp nhân", FilePath);
                        lblTuCachPhapNhan.Visible = true;
                        lnkXoaTuCachPhapNhan.Visible = true;
                        break;
                    case (int)EnLoaiTaiLieuList.TAI_LIEU_KY_THUAT: lblTaiLieuKyThuat.Text = ShowFileLink("Tài liệu kỹ thuật", FilePath);
                        lblTaiLieuKyThuat.Visible = true;
                        lnkXoaTaiLieuKyThuat.Visible = true;
                        break;
                    case (int)EnLoaiTaiLieuList.KET_QUA_DO_KIEM: lblKetQuaDoKiem.Text = ShowFileLink("Kết quả đo kiểm", FilePath);
                        lblKetQuaDoKiem.Visible = true;
                        lnkXoaKetQuaDoKiem.Visible = true;
                        break;
                    case (int)EnLoaiTaiLieuList.QUY_TRINH_SAN_XUAT: lblTaiLieuQuyTrinhSanXuat.Text = ShowFileLink("Quy trình sản xuất", FilePath);
                        lblTaiLieuQuyTrinhSanXuat.Visible = true;
                        lnkXoaTaiLieuSanXuat.Visible = true;
                        break;
                    case (int)EnLoaiTaiLieuList.QUY_TRINH_CHAT_LUONG: lblQuyTrinhDamBao.Text = ShowFileLink("Quy trình đảm bảo CL", FilePath);
                        lblQuyTrinhDamBao.Visible = true;
                        lnkXoaQuyTrinhDamBao.Visible = true;
                        break;
                    case (int)EnLoaiTaiLieuList.CHUNG_CHI_HE_THONG_QLCL: lblChungChiHeThong.Text = ShowFileLink("Chứng chỉ hệ thống QLCL", FilePath);
                        lblChungChiHeThong.Visible = true;
                        lnkXoaChungChiHeThong.Visible = true;
                        break;
                    case (int)EnLoaiTaiLieuList.TIEU_CHUAN_TU_NGUYEN_AP_DUNG: lblTieuChuanTuNguyen.Text = ShowFileLink("Tiêu chuẩn tự nguyện", FilePath);
                        lblTieuChuanTuNguyen.Visible = true;
                        lnkXoaTieuChuanTuNguyen.Visible = true;
                        break;
                    case (int)EnLoaiTaiLieuList.CHI_TIEU_KY_THUAT_KEM_THEO: lblChiTieuKyThuatKemTheo.Text = ShowFileLink("Chỉ tiêu kỹ thuật kèm theo", FilePath);
                        lblChiTieuKyThuatKemTheo.Visible = true;
                        lnkXoaChiTieuKyThuatKemTheo.Visible = true;
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
        ddlTenSanPham.SelectedValue = sp.SanPhamId;
        ddlTenSanPham_SelectedIndexChanged(null, null);
        ddlHangSanXuat.SelectedValue = sp.HangSanXuatId;

        //lưu nhóm sản phẩm
        //string strNhomSanPhamID = sp.NhomSanPhamId;
        //tiêu chuẩn áp dụng
        BindTListDmTieuChuan(sp.Id, sp.SanPhamId);

        hdSoCongVan.Value = txtSoGCNCV.Text;
        //legend nội dung đánh giá
        //*số đo kiểm
        txtSoDoKiem.Text = sp.SoDoKiem;
        //*ngày đo kiểm
        if (sp.NgayDoKiem != null)
            txtNgayDoKiem.Text = Convert.ToDateTime(sp.NgayDoKiem).ToShortDateString();
        //*Cơ quan đo lường
        if (ddlCoQuanDoLuong.Items.FindByValue(sp.CoQuanDoKiemId) != null)
        {
            ddlCoQuanDoLuong.SelectedIndex = -1;
            ddlCoQuanDoLuong.Items.FindByValue(sp.CoQuanDoKiemId).Selected = true;
        }
        //* hợp lệ
        bool blHopLe = false;
        if (sp.HopLe != null)
            blHopLe = Convert.ToBoolean(sp.HopLe);
        if (rblHopLe.Items.FindByValue(Convert.ToInt32(blHopLe).ToString()) != null)
        {
            rblHopLe.SelectedIndex = -1;
            rblHopLe.Items.FindByValue(Convert.ToInt32(blHopLe).ToString()).Selected = true;
        }
        //* đầy đủ
        bool blDayDu = false;
        if (sp.DayDu != null)
            blDayDu = Convert.ToBoolean(sp.DayDu);
        if (rblDayDu.Items.FindByValue(Convert.ToInt32(blDayDu).ToString()) != null)
        {
            rblDayDu.SelectedIndex = -1;
            rblDayDu.Items.FindByValue(Convert.ToInt32(blDayDu).ToString()).Selected = true;
        }
        //kết quả kiểm cơ sở sản xuất
        bool blDat = false;
        if (sp.KetQuaKiemTraCssx != null)
            blDat = Convert.ToBoolean(sp.KetQuaKiemTraCssx);
        if (rblKetQuaKiemTra.Items.FindByValue(Convert.ToInt32(blDat).ToString()) != null)
        {
            rblKetQuaKiemTra.SelectedIndex = -1;
            rblKetQuaKiemTra.Items.FindByValue(Convert.ToInt32(blDat).ToString()).Selected = true;
        }
        //Quy hoạch tần số
        bool blPhuHop = false;
        if (sp.QuyHoachTanSo != null)
            blPhuHop = Convert.ToBoolean(sp.QuyHoachTanSo);
        if (rblQuyHoachTanSo.Items.FindByValue(Convert.ToInt32(blPhuHop).ToString()) != null)
        {
            rblQuyHoachTanSo.SelectedIndex = -1;
            rblQuyHoachTanSo.Items.FindByValue(Convert.ToInt32(blPhuHop).ToString()).Selected = true;
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
        if (rblKetLuan.Items.FindByValue(intKetLuan.ToString()) != null)
        {
            rblKetLuan.SelectedIndex = -1;
            rblKetLuan.Items.FindByValue(intKetLuan.ToString()).Selected = true;
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
    /// <summary>
    /// Xóa bỏ các tiêu chuẩn gắn với sản phẩm
    /// </summary>
    /// <param name="IDSanPham"></param>
    /// <param name="transaction"></param>
    /// <Modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modified>
    public void ClearTieuChuanForSanPham(string IDSanPham, TransactionManager transaction)
    {
        TList<SanPhamTieuChuanApDung> listTieuChuan = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(IDSanPham);
        if (listTieuChuan != null)
        {
            try
            {
                ProviderFactory.SanPhamTieuChuanApDungProvider.Delete(transaction, listTieuChuan);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    /// <summary>
    /// Xóa file vật lý trên Server
    /// </summary>
    /// <param name="filename">Tên file</param>
    /// <returns></returns>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    public Boolean DeleteFile(string filename)
    {
        try
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
                return true;
            }
            else
                return false;
        }
        catch (Exception ex)
        {

        }
        return false;
    }
    /// <summary>
    /// Cập nhập thông tin tai lieu kem theo vao CSDL
    /// </summary>
    /// <param name="IDSanPham">ID của sản phẩm có tài liệu đính kèm</param>
    /// <param name="fu">Đối tượng FileUpload</param>
    /// <param name="LoaiTaiLieuID">Loại tài liệu upload lên</param>
    /// <param name="lb">Lable thê hiện thông tin về file bên cạnh đối tượng FileUpload</param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    public void UpdateTaiLieuDinhKem(string IDSanPham, ref FileUpload fu, int LoaiTaiLieuID, ref Label lb)
    {
        string TenFile = string.Empty;
        string FilePath = string.Empty;
        string ServerMapth = Server.MapPath("FileUpLoad\\");
        ServerMapth = ServerMapth.ToLower();
        ServerMapth = ServerMapth.Replace("\\webui", "");
        TransactionManager transaction = ProviderFactory.Transaction;

        if (fu.HasFile)
        {
            //Đặt lại tên file theo quy tắc MaTT_STT_TenFile
            //lấy STT tiếp theo trong bảng tài liệu đính kèm
            string Stt = ProviderFactory.TaiLieuDinhKemProvider.GetNextSTT();
            string MaTT = ProviderFactory.SysConfigProvider.GetValue("MA_TRUNG_TAM");
            TenFile = MaTT + "_" + Stt + "_" + fu.FileName;
            //cat bo cac khoang trong ve mot khoang
            while (TenFile.Contains("  "))
                TenFile = TenFile.Replace("  ", " ");
            //thay cac khoang trong bang '_'
            TenFile = TenFile.Replace(' ', '_');

            FilePath = ServerMapth + TenFile;
            TenFile = ResolveUrl("~/FileUpLoad/") + TenFile;
            try
            {
                //neu nhu chua co tai lieu dinh kem voi san pham thi them moi
                TaiLieuDinhKem tl = new TaiLieuDinhKem();
                if (!lb.Text.Contains("<a href"))
                {
                    if (!File.Exists(FilePath))
                        fu.PostedFile.SaveAs(FilePath);
                    tl = TaiLieuDinhKem.CreateTaiLieuDinhKem("", IDSanPham, LoaiTaiLieuID, TenFile, DateTime.Now, null);

                }
                //Neu nhu da co va  thay doi
                else
                {
                    //lưu file thay dổi lên server
                    if (!File.Exists(FilePath))
                        fu.PostedFile.SaveAs(FilePath);
                    //lấy ID của tài liệu vừa bị thay dổi trong bảng Tailieudinhkem
                    string tailieuId = string.Empty;
                    TList<TaiLieuDinhKem> listTaiLieu = ProviderFactory.TaiLieuDinhKemProvider.GetBySanPhamId(IDSanPham);
                    if (listTaiLieu != null)
                    {
                        foreach (TaiLieuDinhKem o in listTaiLieu)
                        {
                            if (o.LoaiTaiLieuId == LoaiTaiLieuID)
                            {
                                tailieuId = o.Id;
                                break;
                            }
                        }
                    }
                    if (tailieuId.Length > 0)//nếu như có
                    {
                        tl = ProviderFactory.TaiLieuDinhKemProvider.GetById(tailieuId.Trim());
                        if (tl != null)
                        {

                            string FileName = tl.TenFile;
                            FileName = FileName.Substring(FileName.LastIndexOf("/") + 1, FileName.Length - FileName.LastIndexOf("/") - 1);
                            FilePath = ServerMapth + FileName;
                            //xóa file cũ đã bị thay thế
                            DeleteFile(FilePath);
                            //gán lại tên file mới vào bản ghi đó
                            tl.TenFile = TenFile;
                        }
                    }
                }
                //cập nhật thay đổi
                ProviderFactory.TaiLieuDinhKemProvider.Save(transaction, tl);
                transaction.Commit();
            }
            catch (Exception ex)
            {

                throw ex;
                transaction.Rollback();
            }
            finally
            {
                transaction.Dispose();
            }
        }
        else//nếu đối tượng fileupload ko co file
        {
            TaiLieuDinhKem tl = new TaiLieuDinhKem();
            //neu nhu da co file luu tam tren server
            if (lb.Visible && !lb.Text.Contains("<a href"))
            {
                //duyệt từng file trong danh sách các file chờ upload lên 
                foreach (DictionaryEntry dic in ListFiles)
                {
                    if (dic.Key == fu.ID && dic.Value != null)
                    {
                        string TempFile = dic.Value.ToString();//lấy thông tin về đường dẫn file đã được lưu tam trên server
                        TenFile = TempFile;
                        //lấy tên của file trong toàn bộ đường dẫn file                        
                        TenFile = TenFile.Substring(TenFile.LastIndexOf("\\") + 1, TenFile.Length - TenFile.LastIndexOf("\\") - 1);
                        //Đặt lại tên file theo quy tắc MaTT_STT_TenFile
                        //lấy STT tiếp theo trong bảng tài liệu đính kèm
                        string Stt = ProviderFactory.TaiLieuDinhKemProvider.GetNextSTT();
                        string MaTT = ProviderFactory.SysConfigProvider.GetValue("MA_TRUNG_TAM");
                        TenFile = TenFile.Replace(fu.ID, "");
                        TenFile = MaTT + "_" + Stt + "_" + TenFile;
                        //cat bo cac khoang trong ve mot khoang
                        while (TenFile.Contains("  "))
                            TenFile = TenFile.Replace("  ", " ");
                        //thay cac khoang trong bang '_'
                        TenFile = TenFile.Replace(' ', '_');

                        FilePath = ServerMapth + TenFile;
                        TenFile = ResolveUrl("~/FileUpLoad/") + TenFile;
                        //Copy file từ thư mục lưu trũ tạm sang thư mục đích với tên file đã được dặt lại
                        File.Copy(TempFile, FilePath);
                        tl = TaiLieuDinhKem.CreateTaiLieuDinhKem("", IDSanPham, LoaiTaiLieuID, TenFile, DateTime.Now, null);
                        ProviderFactory.TaiLieuDinhKemProvider.Save(transaction, tl);
                        transaction.Commit();
                        break;
                    }
                }
            }
            //Nếu có file bị xóa thì tiến hành xóa trong CSDL và file vật lý trên Server
            else if (lb.Visible == false && lb.Text.Contains("<a href"))
            {
                try
                {
                    string tailieuId = string.Empty;
                    //lấy thông tin về file bị xóa trong bảng TaiLieuDinhKem
                    TList<TaiLieuDinhKem> listTaiLieu = ProviderFactory.TaiLieuDinhKemProvider.GetBySanPhamId(IDSanPham);
                    if (listTaiLieu != null)
                    {
                        foreach (TaiLieuDinhKem o in listTaiLieu)
                        {
                            if (o.LoaiTaiLieuId == LoaiTaiLieuID)
                            {
                                tailieuId = o.Id;
                                break;
                            }
                        }
                    }
                    //Tiến hành xóa file vât lý và bản ghi về file đó
                    if (tailieuId.Length > 0)
                    {
                        tl = ProviderFactory.TaiLieuDinhKemProvider.GetById(tailieuId.Trim());
                        if (tl != null)
                        {
                            string FileName = tl.TenFile;
                            FileName = FileName.Substring(FileName.LastIndexOf("/") + 1, FileName.Length - FileName.LastIndexOf("/") - 1);
                            FilePath = ServerMapth + FileName;
                            DeleteFile(FilePath);
                            ProviderFactory.TaiLieuDinhKemProvider.Delete(transaction, tl);
                            transaction.Commit();
                            lb.Text = string.Empty;
                        }

                    }

                }
                catch (Exception ex)
                {

                    throw ex;
                    transaction.Rollback();
                }
                finally
                {
                    transaction.Dispose();
                }

            }
        }
    }
    /// <summary>
    /// Cập nhật quá trình xử lý sản phẩm vào CSDL bao gồm 'Nội dung xử lý' & 'Ghi chú'
    /// </summary>
    /// <param name="IDSanPham"></param>
    /// <param name="transaction"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    public void UpdateQuatrinhXuLySanPham(string IDSanPham, TransactionManager transaction)
    {
        QuaTrinhXuLy xl = new QuaTrinhXuLy();
        if (Request["SanPhamID"] == null)
        {
            xl = QuaTrinhXuLy.CreateQuaTrinhXuLy(IDSanPham, 1, txtNoiDungXuLy.Text.Trim()
                                                 , mUserInfo.UserID, DateTime.Now, string.Empty);
        }
        else
        {
            int xlId = 0;
            TList<QuaTrinhXuLy> listxl = ProviderFactory.QuaTrinhXuLyProvider.GetBySanPhamId(IDSanPham);
            foreach (QuaTrinhXuLy o in listxl)
            {
                if (o.LoaiXuLyId == 1)
                {
                    xlId = o.Id;
                    break;
                }
            }
            if (xlId != 0)
            {
                xl = ProviderFactory.QuaTrinhXuLyProvider.GetById(xlId);
                xl.NoiDungXuLy = txtNoiDungXuLy.Text.Trim();
            }

        }
        if (xl != null)
            ProviderFactory.QuaTrinhXuLyProvider.Save(transaction, xl);
    }
    /// <summary>
    /// Hiển thị tên file và đường link tới file lên nền web
    /// </summary>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
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
    /// <summary>
    /// Kiểm tra toàn bộ kiểu của tất cả các file muốn upload lên 
    /// </summary>
    /// <returns></returns>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    public bool CheckFilesType()
    {
        bool b = true;
        bool c = true;
        b = CheckFileType(ref FileUploadChungChiHeThong, "FileUploadChungChiHeThong");
        if (!b)
            c = false;
        b = CheckFileType(ref FileUploadGiayToTuCachPhapNhan, "FileUploadGiayToTuCachPhapNhan");
        if (!b)
            c = false;

        b = CheckFileType(ref FileUploadKetQuaDoKiem, "FileUploadKetQuaDoKiem");
        if (!b)
            c = false;
        b = CheckFileType(ref FileUploadQuyTrinhDamBao, "FileUploadQuyTrinhDamBao");
        if (!b)
            c = false;

        b = CheckFileType(ref FileUploadTaiLieuDeNghiCN, "FileUploadTaiLieuDeNghiCN");
        if (!b)
            c = false;

        b = CheckFileType(ref FileUploadTaiLieuKyThuat, "FileUploadTaiLieuKyThuat");
        if (!b)
            c = false;

        b = CheckFileType(ref FileUploadTaiLieuQuyTrinhSanXuat, "FileUploadTaiLieuQuyTrinhSanXuat");
        if (!b)
            c = false;
        b = CheckFileType(ref FileUploadTieuChuanTuNguyen, "FileUploadTieuChuanTuNguyen");
        if (!b)
            c = false;

        b = CheckFileType(ref FileUploadChiTieuKyThuatKemTheo, "FileUploadChiTieuKyThuatKemTheo");
        if (!b)
            c = false;

        return c;
    }
    /// <summary>
    /// Kiểm tra kiểu file 
    /// </summary>
    /// <param name="fu">Đối tượng FileUpload</param>
    /// <param name="funame">ID đối tượng FileUpload</param>
    /// <returns></returns>      
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    public bool CheckFileType(ref FileUpload fu, string funame)
    {
        if (fu.HasFile)
        {
            string FileName = fu.PostedFile.FileName.ToLower();
            string extraType = FileName.Substring(FileName.LastIndexOf('.'), FileName.Length - FileName.LastIndexOf('.'));
            if (extraType == ".zip"
                  || extraType == ".rar"
                  || extraType == ".jpeg"
                  || extraType == ".jpg"
                  || extraType == ".bmp"
                  || extraType == ".pdf"
                  || extraType == ".doc"
                  || extraType == ".docx"
                  || extraType == ".xls"
                  || extraType == ".xlsx"
                  || extraType == ".txt")
                return true;
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo"
                   , "<script>alert('Chỉ cho phép các file có định dạng (*.bmp,*.jpeg,*.jpg,*.zip,*.rar,*.doc,*.docx,*.xls,*.xlsx,pdf)')</script>");
                // fu.Focus();
                SetFileWaitUpload(funame, null);
                Session["ListFilesUpload"] = ListFiles;
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Kiểm tra dung lượng của tất cả các file tải lên  qua FileUpload
    /// </summary>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    public bool CheckFileUploadsSize()
    {
        bool b = true;
        bool c = true;
        if (FileUploadChungChiHeThong.HasFile)
        {
            b = CheckFileSize(ref FileUploadChungChiHeThong);
            if (!b)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dung lượng file tải lên phải nhỏ hơn hoặc bằng 5 MB!');</script>");
                //FileUploadChungChiHeThong.Focus();
                SetFileWaitUpload("FileUploadChungChiHeThong", null);
                //Session["FileUploadChungChiHeThong"] = null;
                c = false;
            }
        }
        if (FileUploadGiayToTuCachPhapNhan.HasFile)
        {
            b = CheckFileSize(ref FileUploadGiayToTuCachPhapNhan);
            if (!b)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dung lượng file tải lên phải nhỏ hơn hoặc bằng 5 MB!');</script>");
                // FileUploadGiayToTuCachPhapNhan.Focus();
                SetFileWaitUpload("FileUploadGiayToTuCachPhapNhan", null);
                //Session["FileUploadGiayToTuCachPhapNhan"] = null;
                c = false;
            }
        }
        if (FileUploadKetQuaDoKiem.HasFile)
        {
            b = CheckFileSize(ref FileUploadKetQuaDoKiem);
            if (!b)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dung lượng file tải lên phải nhỏ hơn hoặc bằng 5 MB!');</script>");
                //FileUploadKetQuaDoKiem.Focus();
                SetFileWaitUpload("FileUploadKetQuaDoKiem", null);
                //Session["FileUploadKetQuaDoKiem"] = null;
                c = false;
            }
        }
        if (FileUploadQuyTrinhDamBao.HasFile)
        {
            b = CheckFileSize(ref FileUploadQuyTrinhDamBao);
            if (!b)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dung lượng file tải lên phải nhỏ hơn hoặc bằng 5 MB!');</script>");
                //FileUploadQuyTrinhDamBao.Focus();
                SetFileWaitUpload("FileUploadQuyTrinhDamBao", null);
                //Session["FileUploadQuyTrinhDamBao"] = null;
                //return false;
            }
        }
        if (FileUploadTaiLieuDeNghiCN.HasFile)
        {
            b = CheckFileSize(ref FileUploadTaiLieuDeNghiCN);
            if (!b)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dung lượng file tải lên phải nhỏ hơn hoặc bằng 5 MB!');</script>");
                //FileUploadTaiLieuDeNghiCN.Focus();
                SetFileWaitUpload("FileUploadTaiLieuDeNghiCN", null);
                //Session["FileUploadTaiLieuDeNghiCN"] = null;
                c = false;
            }
        }
        if (FileUploadTaiLieuKyThuat.HasFile)
        {
            b = CheckFileSize(ref FileUploadTaiLieuKyThuat);
            if (!b)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dung lượng file tải lên phải nhỏ hơn hoặc bằng 5 MB!');</script>");
                //FileUploadTaiLieuKyThuat.Focus();
                SetFileWaitUpload("FileUploadTaiLieuKyThuat", null);
                //Session["FileUploadTaiLieuKyThuat"] = null;
                c = false;
            }
        }
        if (FileUploadTaiLieuQuyTrinhSanXuat.HasFile)
        {
            b = CheckFileSize(ref FileUploadTaiLieuQuyTrinhSanXuat);
            if (!b)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dung lượng file tải lên phải nhỏ hơn hoặc bằng 5 MB!');</script>");
                //FileUploadTaiLieuQuyTrinhSanXuat.Focus();
                SetFileWaitUpload("FileUploadTaiLieuQuyTrinhSanXuat", null);
                //Session["FileUploadTaiLieuQuyTrinhSanXuat"] = null;
                c = false;
            }
        }
        if (FileUploadTieuChuanTuNguyen.HasFile)
        {
            b = CheckFileSize(ref FileUploadTieuChuanTuNguyen);
            if (!b)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dung lượng file tải lên phải nhỏ hơn hoặc bằng 5 MB!');</script>");
                //FileUploadTieuChuanTuNguyen.Focus();
                SetFileWaitUpload("FileUploadTieuChuanTuNguyen", null);
                //Session["FileUploadTieuChuanTuNguyen"] = null;  
                c = false;
            }
        }
        if (FileUploadChiTieuKyThuatKemTheo.HasFile)
        {
            b = CheckFileSize(ref FileUploadChiTieuKyThuatKemTheo);
            if (!b)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dung lượng file tải lên phải nhỏ hơn hoặc bằng 5 MB!');</script>");
                //FileUploadTieuChuanTuNguyen.Focus();
                SetFileWaitUpload("FileUploadChiTieuKyThuatKemTheo", null);
                //Session["FileUploadTieuChuanTuNguyen"] = null;  
                c = false;
            }
        }
        return c;
    }
    /// <summary>
    /// Kiểm tra dung lượng file tải lên qua FileUpload
    /// </summary>
    /// <param name="fu">Đối tượng FileUpload muốn kiểm tra</param>
    /// <returns></returns>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    public bool CheckFileSize(ref FileUpload fu)
    {
        int max = Convert.ToInt16(ConfigurationSettings.AppSettings["MaxFileUpLoadSize"]);
        int filesize = fu.PostedFile.ContentLength / 1024;
        if (filesize > max)
            return false;
        return true;
    }
    /// <summary>
    /// Kiểm tra xem có tiêu chuẩn nào được chọn hay ko?
    /// </summary>
    /// <param name="fu"></param>
    /// <returns></returns>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009             Tạo mới
    /// </Modifield>
    public bool CheckListTieuChuan()
    {
        foreach (ListItem chk in chklstTieuChuan.Items)
        {
            if (chk.Selected)
            {
                return true;
            }
        }
        Thong_bao("Bạn phải chọn ít nhất một tiêu chuẩn cho sản phẩm!");
        return false;
    }
    /// <summary>
    /// Kiểm tra xem sản phẩm lựa chọn đã có trong hồ sơ hay chưa
    /// </summary>
    /// <param name="SanPhamId"></param>
    /// <returns></returns>
    /// <Modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009             Tạo mới
    /// </Modified>
    public bool CheckTrungSanPham(string SanPhamId)
    {
        TList<SanPham> listSP = ProviderFactory.SanPhamProvider.GetByHoSoId(HoSoId);
        foreach (SanPham sp in listSP)
        {
            if (Action == "add")
            {
                if (sp.SanPhamId == SanPhamId)
                    return false;
            }
            else if (Action == "edit")
            {
                SanPham sp1 = ProviderFactory.SanPhamProvider.GetById(IDSanPham);

                if (sp.SanPhamId == SanPhamId && sp.SanPhamId != sp1.SanPhamId)
                    return false;

            }
        }
        return true;
    }
    /// <summary>
    /// Thiết lập chế độ chỉ xem đối với các textbox của các đối tượng FileUpload
    /// </summary>
    /// <Modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009             Tạo mới
    /// </Modified>
    public void SetFileUploadsReadOnly()
    {
        MakeFileUploadIsReadonly(ref FileUploadChungChiHeThong);
        MakeFileUploadIsReadonly(ref FileUploadGiayToTuCachPhapNhan);
        MakeFileUploadIsReadonly(ref FileUploadKetQuaDoKiem);
        MakeFileUploadIsReadonly(ref FileUploadQuyTrinhDamBao);
        MakeFileUploadIsReadonly(ref FileUploadTaiLieuDeNghiCN);
        MakeFileUploadIsReadonly(ref FileUploadTaiLieuKyThuat);
        MakeFileUploadIsReadonly(ref FileUploadTaiLieuQuyTrinhSanXuat);
        MakeFileUploadIsReadonly(ref FileUploadTieuChuanTuNguyen);
        MakeFileUploadIsReadonly(ref FileUploadChiTieuKyThuatKemTheo);

    }
    /// <summary>
    /// Thiết lập chế độ chỉ xem đối với textbox của đối tượng FileUpload
    /// </summary>
    /// <param name="fu">Đối tượng FileUpload</param>
    /// <Modified>    
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modified>    
    public void MakeFileUploadIsReadonly(ref FileUpload fu)
    {
        fu.Attributes.Add("onkeypress", "return false;");
        fu.Attributes.Add("onkeyup", "return false;");
        fu.Attributes.Add("onpaste", "return false;");
    }
    /// <summary>
    /// Hiển thị toàn bộ file chờ upload lên man hình
    /// </summary>
    /// <Modified>    
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modified>    
    public void ShowFilesWaitForUpLoad()
    {
        if (Session["ListFilesUpload"] != null)
        {
            ListFiles = (List<DictionaryEntry>)Session["ListFilesUpload"];
            ShowFileWaitForUpLoad("FileUploadChungChiHeThong", ref lblChungChiHeThong);
            ShowFileWaitForUpLoad("FileUploadGiayToTuCachPhapNhan", ref lblTuCachPhapNhan);
            ShowFileWaitForUpLoad("FileUploadKetQuaDoKiem", ref lblKetQuaDoKiem);
            ShowFileWaitForUpLoad("FileUploadQuyTrinhDamBao", ref lblQuyTrinhDamBao);
            ShowFileWaitForUpLoad("FileUploadTaiLieuDeNghiCN", ref lblTaiLieuDeNghi);
            ShowFileWaitForUpLoad("FileUploadTaiLieuKyThuat", ref lblTaiLieuKyThuat);
            ShowFileWaitForUpLoad("FileUploadTaiLieuQuyTrinhSanXuat", ref lblTaiLieuQuyTrinhSanXuat);
            ShowFileWaitForUpLoad("FileUploadTieuChuanTuNguyen", ref lblTieuChuanTuNguyen);
            ShowFileWaitForUpLoad("FileUploadChiTieuKyThuatKemTheo", ref lblChiTieuKyThuatKemTheo);
        }


    }
    /// <summary>
    /// Hiển thị file chờ upload lên màn hình
    /// </summary>
    /// <param name="funame">ID của đối tượng fileupload</param>
    /// <param name="lb">Lable hiển thị các thông tin tương ứng bên cạnh đối tượng Fileupload</param>
    /// <Modified>    
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modified>    
    public void ShowFileWaitForUpLoad(string funame, ref Label lb)
    {

        foreach (DictionaryEntry dic in ListFiles)
        {
            if (dic.Key.ToString() == funame && dic.Value != null)
            {
                string fn = dic.Value.ToString();
                fn = fn.Substring(fn.LastIndexOf("\\") + 1, fn.Length - fn.LastIndexOf("\\") - 1);
                lb.Text = fn;
                lb.Visible = true;
                break;
            }
        }

    }
    /// <summary>
    /// Lấy toàn bộ thông tin của các file đang chờ upload và lưu chúng tạm lên server và thông tin của file len session
    /// </summary>
    /// <Modified>    
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modified>    
    public void GetFilesWaitForUpLoad()
    {
        GetFileWaitForUpLoad("FileUploadChungChiHeThong", ref FileUploadChungChiHeThong);
        GetFileWaitForUpLoad("FileUploadGiayToTuCachPhapNhan", ref FileUploadGiayToTuCachPhapNhan);
        GetFileWaitForUpLoad("FileUploadKetQuaDoKiem", ref FileUploadKetQuaDoKiem);
        GetFileWaitForUpLoad("FileUploadQuyTrinhDamBao", ref FileUploadQuyTrinhDamBao);
        GetFileWaitForUpLoad("FileUploadTaiLieuDeNghiCN", ref FileUploadTaiLieuDeNghiCN);
        GetFileWaitForUpLoad("FileUploadTaiLieuKyThuat", ref FileUploadTaiLieuKyThuat);
        GetFileWaitForUpLoad("FileUploadTaiLieuQuyTrinhSanXuat", ref FileUploadTaiLieuQuyTrinhSanXuat);
        GetFileWaitForUpLoad("FileUploadTieuChuanTuNguyen", ref FileUploadTieuChuanTuNguyen);
        GetFileWaitForUpLoad("FileUploadChiTieuKyThuatKemTheo", ref FileUploadChiTieuKyThuatKemTheo);
    }
    /// <summary>
    /// Lấy thông tin và upload tạm thời file lên server và session
    /// </summary>
    /// <param name="funame">ID của đối tượng fileUpload</param>
    /// <param name="fu">Đối tượng fileupload</param>
    /// <Modified>    
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modified>    
    public void GetFileWaitForUpLoad(string funame, ref FileUpload fu)
    {
        //string TenFile = string.Empty;
        string FilePath = string.Empty;
        string ServerMapth = Server.MapPath("TempFile\\");
        ServerMapth = ServerMapth.ToLower();
        ServerMapth = ServerMapth.Replace("\\webui", "");
        if (fu.HasFile)
        {
            FilePath = ServerMapth + funame + fu.FileName;
            if (!File.Exists(FilePath))
                fu.PostedFile.SaveAs(FilePath);
            SetFileWaitUpload(funame, FilePath);

        }
        Session["ListFilesUpload"] = ListFiles;
    }
    /// <summary>
    /// Gán lại đối tượng Session lưu danh sách các file chờ upload vào 1 danh sách 
    /// </summary>
    /// <Modified>    
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modified>    
    public void SetBackFilesWaitForUpload()
    {

        if (Session["ListFilesUpload"] != null)
            ListFiles = (List<DictionaryEntry>)Session["ListFilesUpload"];
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="funame"></param>
    /// <param name="fu"></param>
    /// <Modified>    
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modified>    
    public void SetBackFileWaitForUpload(string funame, ref FileUpload fu)
    {
        if (Session[funame] != null)
            fu = (FileUpload)Session[funame];
    }
    /// <summary>
    /// Xóa trắng thông tin về file chờ upload trên Session và trong danh sách
    /// </summary>
    /// <Modified>    
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modified>    
    public void ClearFileSession()
    {

        foreach (DictionaryEntry dic in ListFiles)
        {
            if (dic.Value != null)

                DeleteFile(dic.Value.ToString());
        }
        ListFiles.Clear();
        InitListFile();
        Session["ListFilesUpload"] = ListFiles;
    }
    /// <summary>
    /// Khởi tạo danh sách chứa thông tin về file đang chờ upload
    /// </summary>
    /// <Modified>    
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modified>    
    public void InitListFile()
    {
        DictionaryEntry dic = new DictionaryEntry("FileUploadChungChiHeThong", null);
        ListFiles.Add(dic);
        dic = new DictionaryEntry("FileUploadGiayToTuCachPhapNhan", null);
        ListFiles.Add(dic);
        dic = new DictionaryEntry("FileUploadKetQuaDoKiem", null);
        ListFiles.Add(dic);
        dic = new DictionaryEntry("FileUploadQuyTrinhDamBao", null);
        ListFiles.Add(dic);
        dic = new DictionaryEntry("FileUploadTaiLieuDeNghiCN", null);
        ListFiles.Add(dic);
        dic = new DictionaryEntry("FileUploadTaiLieuKyThuat", null);
        ListFiles.Add(dic);
        dic = new DictionaryEntry("FileUploadTaiLieuQuyTrinhSanXuat", null);
        ListFiles.Add(dic);
        dic = new DictionaryEntry("FileUploadTieuChuanTuNguyen", null);
        ListFiles.Add(dic);
        dic = new DictionaryEntry("FileUploadChiTieuKyThuatKemTheo", null);
        ListFiles.Add(dic);
    }
    /// <summary>
    /// Gán các thông tin về file chờ upload vào danh sách.
    /// </summary>
    /// <param name="funame"></param>
    /// <param name="value"></param>
    /// <Modified>    
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modified>    
    public void SetFileWaitUpload(string funame, object value)
    {
        int index = -1;
        for (index = 0; index < ListFiles.Count; index++)
        {
            if (ListFiles[index].Key == funame)
                break;
        }
        if (value == null)
        {
            if (ListFiles[index].Value != null)
            {
                string fn = ListFiles[index].Value.ToString();
                DeleteFile(fn);
            }
        }
        if (index >= 0)
        {
            ListFiles.RemoveAt(index);
            DictionaryEntry dic = new DictionaryEntry(funame, value);
            ListFiles.Insert(index, dic);
        }
    }
    /// <summary>
    /// Ẩn các action control cho trường hợp xem thông tin
    /// </summary>
    /// <Modified>   
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modified>    
    public void DisableActionControls()
    {
        btnCapNhat.Visible = false;

        lnkXoaChungChiHeThong.Visible = false;
        lnkXoaKetQuaDoKiem.Visible = false;
        lnkXoaQuyTrinhDamBao.Visible = false;
        lnkXoaTaiLieuDeNghi.Visible = false;
        lnkXoaTaiLieuKyThuat.Visible = false;
        lnkXoaTaiLieuSanXuat.Visible = false;
        lnkXoaTieuChuanTuNguyen.Visible = false;
        lnkXoaTuCachPhapNhan.Visible = false;
        lnkXoaChiTieuKyThuatKemTheo.Visible = false;

        FileUploadChungChiHeThong.Enabled = false;
        FileUploadGiayToTuCachPhapNhan.Enabled = false;
        FileUploadKetQuaDoKiem.Enabled = false;
        FileUploadQuyTrinhDamBao.Enabled = false;
        FileUploadTaiLieuDeNghiCN.Enabled = false;
        FileUploadTaiLieuKyThuat.Enabled = false;
        FileUploadTaiLieuQuyTrinhSanXuat.Enabled = false;
        FileUploadTieuChuanTuNguyen.Enabled = false;
        FileUploadChiTieuKyThuatKemTheo.Visible = false;

        ddlHangSanXuat.Enabled = false;
        ddlTenSanPham.Enabled = false;

        chklstTieuChuan.Enabled = false;
        txtKyHieuSanPham.ReadOnly = true;
        txtNoiDungXuLy.ReadOnly = true;
        lnkThemMoiSanPham.Style.Add("display", "none");
        lnkThemMoiHangSX.Style.Add("display", "none");
        //        string script = string.Empty;
        //        script = @"<script>
        //                            $(document).ready(function(){                              
        //                             $('#Button0').hide();
        //                             $('#Button1').hide();
        //                             $('#Button2').hide();
        //                             $('#Button3').hide();
        //                             $('#Button4').hide();
        //                             $('#Button5').hide();
        //                             $('#Button6').hide();
        //                             $('#Button7').hide();                                                                                        
        //                             $('#Button8').hide();                            
        //                             $('#lnkThemMoiSanPham').hide();                                                                                        
        //                             $('#lnkThemMoiHangSX').hide();   
        //                            });
        //                            </script>";
        //        Page.RegisterClientScriptBlock("ShowAndHide", script);
    }
    /// <summary>
    /// Thiết lập web path và định hướng cho trang
    /// </summary>
    /// <Modified>    
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modified>    
    public void CreateWebPath()
    {
        StringBuilder sb = new StringBuilder();
        if (Direct == "cn_hososanpham" && Action != "view")
        {
            sb.Append(@"<a href='../WebUI/CN_HoSo_QuanLy.aspx?direct=CN_HoSoMoi'>
                        <strong>DANH SÁCH HỒ SƠ MỚI</strong></a><strong> &gt;&gt; </strong>");
            sb.Append(" <a href='../WebUI/CN_HoSoSanPham.aspx?HoSoID=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "'>");
            sb.Append("DANH SÁCH SẢN PHẨM</a> >>");
        }
        else if (Direct == "cn_hososanpham" && Action == "view")
        {
            sb.Append(@"<a href='../WebUI/CN_HoSo_QuanLy.aspx?UserControl=CN_HoSoDi'>
                        <strong>DANH SÁCH HỒ SƠ CHƯNG NHẬN ĐÃ GỬI</strong></a><strong> &gt;&gt; </strong>");
            sb.Append(" <a href='../WebUI/CN_HoSoSanPham.aspx?direct=cn_hosodi&action=view&HoSoID=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "'>");
            sb.Append("DANH SÁCH SẢN PHẨM</a> >>");
        }
        else if (Direct == "cn_tracuuhoso_hososanpham" && Action == "view")
        {
            sb.Append(@"<a href='../WebUI/CN_TraCuuThongTinHoSo.aspx'> TRA CỨU HỒ SƠ CHỨNG NHẬN</a> &gt;&gt;");
            sb.Append(" <a href='../WebUI/CN_HoSoSanPham.aspx?direct=cn_trahoso&action=view&HoSoID=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "'>");
            sb.Append("DANH SÁCH SẢN PHẨM</a> >>");
        }
        else if (Direct == "cn_hososanpham_quanly")
        {
            sb.Append(" <a href='../WebUI/CN_HoSoSanPham_QuanLy.aspx?direct=CN_HoSoMoi&HoSoID=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "'>");
            sb.Append("DANH SÁCH SẢN PHẨM</a> >>");
        }

        lblDsSanPham.Text = sb.ToString();

        if (Direct == "cn_hososanpham" && Action != "view")
            btnBoQua.Attributes.Add("onclick", "javascript:window.location='CN_HoSoSanPham.aspx?direct=CN_HoSoDen&HoSoID=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "'; return false;");
        else if (Direct == "cn_hososanpham" && Action == "view")
            btnBoQua.Attributes.Add("onclick", "javascript:window.location='CN_HoSoSanPham.aspx?direct=CN_HoSoDi&action=view&HoSoID=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "'; return false;");
        else if (Direct == "cn_tracuuhoso_hososanpham" && Action == "view")
            btnBoQua.Attributes.Add("onclick", "javascript:window.location='CN_HoSoSanPham.aspx?direct=cn_trahoso&action=view&HoSoID=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "'; return false;");
        else if (Direct == "cn_hososanpham_quanly")
            btnBoQua.Attributes.Add("onclick", "javascript:window.location='CN_HoSoSanPham_QuanLy.aspx?direct=CN_HoSoDen&HoSoID=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "'; return false;");


    }
    #endregion

    protected void lnkbtnTaoMoiCoQuanDoLuong(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "Popup_TaoMoiCQ",
                                      "<script>popCenter('DM_CoQuanDoKiem_ChiTiet.aspx?PostBack=NhapLieu_CN_SanphamChiTiet','Popup_TaoMoiCQ',570,280);</script>");

    }

    /// <summary>
    /// Xóa công văn
    /// xóa trong bảng tài liệu đính kèm và xóa file
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkbtnXoaSCV_Click(object sender, EventArgs e)
    {
        string strSanPhamID = string.Empty;
        if (Request["SanPhamID"] != null)
            strSanPhamID = Request["SanPhamID"].ToString();
        //nếu không có sản phẩm id thì break luôn
        if (string.IsNullOrEmpty(strSanPhamID))
            return;
        TList<TaiLieuDinhKem> lstTaiLieuDinhKem = new TList<TaiLieuDinhKem>();
        lstTaiLieuDinhKem = ProviderFactory.TaiLieuDinhKemProvider.GetBySanPhamId(strSanPhamID);
        Cuc_QLCL.Data.TransactionManager _tran = ProviderFactory.Transaction;
        foreach (TaiLieuDinhKem obj in lstTaiLieuDinhKem)
        {
            if (obj.LoaiTaiLieuId == Convert.ToInt32(EnLoaiTaiLieuList.CONG_VAN))
            {
                try
                {
                    string fileName = Server.MapPath(obj.TenFile).Replace(@"WebUI\", string.Empty);
                    FileInfo objFileInfo = new FileInfo(fileName);
                    if (objFileInfo.Exists)
                        objFileInfo.Delete();
                    ProviderFactory.TaiLieuDinhKemProvider.Delete(_tran, obj);

                    lnkbtnXoaSCV.Visible = false;
                    fileUpSoCongVan.Visible = true;
                    hlCongVan.Visible = false;
                }
                catch (Exception ex)
                {
                    _tran.Rollback();
                    PageBase_Error(null, null);
                }
            }
        }
        if (_tran.IsOpen)
            _tran.Commit();
        _tran.Dispose();
    }

    /// <summary>
    /// Lấy danh mục cơ quan đo kiểm
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                12/5/2009              
    /// </Modified>
    private void BindTListDmCoQuanDoKiem()
    {
        TList<DmCoQuanDoKiem> lstDmCoQuanDoKiem = ProviderFactory.DmCoQuanDoKiemProvider.GetAll();

        ddlCoQuanDoLuong.DataValueField = "ID";
        ddlCoQuanDoLuong.DataTextField = "TenCoQuanDoKiem";
        ddlCoQuanDoLuong.DataSource = lstDmCoQuanDoKiem;
        ddlCoQuanDoLuong.DataBind();
    }

    private string SinhSoGCN(SanPham ObSanPham, TransactionManager _transaction)
    {
        string Stt = txtSoGCNCV.Text.Trim();

        if (Stt.Length == 1)
            Stt = "000" + Stt;
        else if (Stt.Length == 2)
            Stt = "00" + Stt;
        else if (Stt.Length == 3)
            Stt = "0" + Stt;
        else if (Stt.Length == 4)
            Stt = Stt.Substring(0, 4);
        
        if(Stt.Contains("A") || Stt.Contains("B") || Stt.Contains("C"))
            Stt = Stt.Substring(1, 4);

        DateTime NgayCap = Convert.ToDateTime(txtNgayCap.Text);

        string day = "";
        if (NgayCap.Day < 10)
            day = "0" + NgayCap.Day.ToString();
        else
            day = NgayCap.Day.ToString();

        string month = "";
        if (NgayCap.Month < 10)
            month = "0" + NgayCap.Month.ToString();
        else
            month = NgayCap.Month.ToString();
        string year = NgayCap.Year.ToString().Substring(2, 2);
        string ngayThang = day + month + year;

        return ProviderFactory.SanPhamProvider.GetSoGiayChungNhan_Old(ObSanPham.Id, Stt + ngayThang, _transaction);
    }

    private void SanPham_SaoChepDuLieu(SanPham sp)
    {
        DmSanPham objSanPham = ProviderFactory.DmSanPhamProvider.GetById(sp.SanPhamId);
        DmNhomSanPham objNhomSanPham = ProviderFactory.DmNhomSanPhamProvider.GetById(sp.NhomSanPhamId);
        DmCoQuanDoKiem objCoQuanDoKiem = ProviderFactory.DmCoQuanDoKiemProvider.GetById(sp.CoQuanDoKiemId);
        DmHangSanXuat objHangSanXuat = ProviderFactory.DmHangSanXuatProvider.GetById(sp.HangSanXuatId);
        SysUser objUser = ProviderFactory.SysUserProvider.GetById(sp.NguoiKyDuyetId);

        sp.TenNguoiKyDuyet = objUser.FullName;
        if (objUser.Position != null)
            sp.ChucVu = Convert.ToInt32(objUser.Position);
        DmTrungTam trungtam = ProviderFactory.DmTrungTamProvider.GetById(objUser.OrganizationId);
        sp.TenTrungTam = trungtam.TenTrungTam;
        DmPhongBan phongban = ProviderFactory.DmPhongBanProvider.GetById(objUser.DepartmentId);
        sp.PhongBan = phongban.TenPhongBan;

        sp.DmCqdkDiaChi = objCoQuanDoKiem.DiaChi;
        sp.DmCqdkTenCoQuanDoKiem = objCoQuanDoKiem.TenCoQuanDoKiem;
        sp.DmCqdkTenTiengAnh = objCoQuanDoKiem.TenTiengAnh;
        sp.DmHsxTenHangSanXuat = objHangSanXuat.TenHangSanXuat;
        sp.DmHsxTenTiengAnh = objHangSanXuat.TenTiengAnh;
        sp.DmNhomSpLienQuanTanSo = objNhomSanPham.LienQuanTanSo;
        sp.DmNhomSpLoaiHinhChungNhan = objNhomSanPham.LoaiHinhChungNhan;
        sp.DmNhomSpMaNhom = objNhomSanPham.MaNhom;
        sp.DmNhomSpMucLePhi = objNhomSanPham.MucLePhi;
        sp.DmNhomSpTenNhom = objNhomSanPham.TenNhom;
        sp.DmNhomSpThoiHanGcn = objNhomSanPham.ThoiHanGcn;
        sp.DmSanPhamLoaiSanPham = objSanPham.LoaiSanPham;
        sp.DmSanPhamLoaiTieuChuanApDung = objSanPham.LoaiTieuChuanApDung;
        sp.DmSanPhamMaSanPham = objSanPham.MaSanPham;
        sp.DmSanPhamNhomSanPhamId = objSanPham.NhomSanPhamId;
        sp.DmSanPhamTenTiengAnh = objSanPham.TenTiengAnh;
        sp.DmSanPhamTenTiengViet = objSanPham.TenTiengViet;

        sp.TenNguoiKyDuyet = objUser.FullName;
        if (objUser.Position != null)
            sp.ChucVu = Convert.ToInt32(objUser.Position);

        TList<SanPhamTieuChuanApDung> listSanPhamTieuChuanApDung = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(sp.Id);
        foreach (SanPhamTieuChuanApDung spad in listSanPhamTieuChuanApDung)
        {
            DmTieuChuan objTieuChuan = ProviderFactory.DmTieuChuanProvider.GetById(spad.TieuChuanApDungId);
            spad.DmTieuChuanMaTieuChuan = objTieuChuan.MaTieuChuan;
            spad.DmTieuChuanTenTieuChuan = objTieuChuan.TenTieuChuan;
            spad.DmTieuChuanTenTiengAnh = objTieuChuan.TenTiengAnh;
            ProviderFactory.SanPhamTieuChuanApDungProvider.Save(spad);
        }
        ProviderFactory.SanPhamProvider.Save(sp);
    }
}



