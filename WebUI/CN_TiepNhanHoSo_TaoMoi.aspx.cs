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



public partial class WebUI_CN_TiepNhanHoSo_TaoMoi : PageBase
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

        }
        //giới hạn maxlenght cho mục "Nội dung xử lý" và mục "Ghi chú"
        txtGhiChu.Attributes.Add("onkeyup", " if (!checkLength('" + txtGhiChu.ClientID + "', '4000')) return false;");
        txtNoiDungXuLy.Attributes.Add("onkeyup", " if (!checkLength('" + txtNoiDungXuLy.ClientID + "', '4000')) return false;");

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

        if (Direct.ToUpper() == "CN_TRACUUHOSO_HOSOSANPHAM")
            btnInPhieuDanhGia.Visible = true;
        else
            btnInPhieuDanhGia.Visible = false;

        //thiet lam web path cho trang
        CreateWebPath();

        if (!IsPostBack)
        {
            Bind_ListTenSanPham();
            Bind_ListHangSanXuat();
            ClearFileSession();
            lnkThemMoiSanPham.Visible = mUserInfo.IsInRole(EnPermission.QUANLY_DM_SANPHAM);
            lnkThemMoiHangSX.Visible = mUserInfo.IsInRole(EnPermission.QUANLY_DM_HANGSX);
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
            //LongHH
            btnCloneSanPham.Visible = true;
            btnXoa.Visible = true;
            //LongHH
        }
        else if (Action == "add")//nếu là thêm mới
        {
            if (HoSoId.Length > 0)
            {
                HoSo hs = ProviderFactory.HoSoProvider.GetById(HoSoId);
                if (hs != null)
                    txtSoHoSo.Text = hs.SoHoSo;
                SetVisibleAnhKiemTra(hs);
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
        else if (Action == "view")//nếu chỉ xem
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
            DisableActionControls();
        }

        SetFileUploadsReadOnly();

        // Set thuộc tính của link thêm mói sản phẩm theo loại hồ sơ
        if (HoSoId.Length > 0)
        {
            HoSo objHso = DataRepository.HoSoProvider.GetById(HoSoId);
            SetVisibleAnhKiemTra(objHso);
            if (objHso.LoaiHoSo == (int)LoaiHoSo.ChungNhanHopQuy)
            {
                lnkThemMoiSanPham.Attributes.Add("onclick", "return popCenter('DM_SanPham_ChiTiet.aspx?PostBack=CN_TiepNhanHoSo_TaoMoi&LoaiHinhChungNhan=1' ,'Popup_TaoMoiSP',950,600); return false");
            }
            else
            {
                lnkThemMoiSanPham.Attributes.Add("onclick", "return popCenter('DM_SanPham_ChiTiet.aspx?PostBack=CN_TiepNhanHoSo_TaoMoi&LoaiHinhChungNhan=2' ,'Popup_TaoMoiSP',950,600); return false");
            }
        }
        lnkThemMoiHangSX.Attributes.Add("onclick", "return popCenter('DM_HangSanXuat_ChiTiet.aspx?PostBack=CN_TiepNhanHoSo_TaoMoi','Popup_TaoMoiSP',600,150); return false");

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
        string dsMaTieuChuan = string.Empty;
        string dsTenTieuChuan = string.Empty;

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
            ObSanPham.KyHieu = txtKyHieuSanPham.Text.Trim();
            ObSanPham.HangSanXuatId = ddlHangSanXuat.SelectedValue;
            ObSanPham.NhomSanPhamId = NhomSanPhamId;
            ObSanPham.SanPhamId = ddlTenSanPham.SelectedValue;
            ObSanPham.NgayCapNhatSauCung = DateTime.Now;
            if (ObSanPham.IsValid)
            {
                TransactionManager transaction = ProviderFactory.Transaction;                
                try
                {
                    //lưu "Nội dung xử lý" & "Ghi chú" vào trong bảng QuaTrinhXuly                    
                    UpdateQuatrinhXuLySanPham(IDSanPham, transaction);
                    //Cập nhật tiêu chuẩn cho sản phẩm
                    ClearTieuChuanForSanPham(IDSanPham, transaction);
                    UpdateTieuChuanForSanPham(IDSanPham, transaction, ref dsMaTieuChuan, ref dsTenTieuChuan);

                    ObSanPham.DsMaTieuChuan = dsMaTieuChuan;
                    ObSanPham.DsTenTieuChuan = dsTenTieuChuan;
                    ProviderFactory.SanPhamProvider.Update(transaction, ObSanPham);

                    transaction.Commit();
                    //cập nhật tài liệu đính kèm sản phẩm
                    UpdateTaiLieuDinhKemForSanPham(IDSanPham);
                    string ThongBao = Resources.Resource.msgCapNhatSanPham;

                    //Ghi nhật ký chương trình
                    string strLogString = "Sửa thông tin Sản phẩm chứng nhận có mã là: " + IDSanPham;
                    ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.CN_SAN_PHAM_SUA, strLogString);
                    //Hướng về trang quản lý sản phẩm của hồ sơ
                    if (Direct == "cn_hososanpham")
                        Thong_bao(this.Page, ThongBao, "CN_HoSoSanPham.aspx?direct=CN_HoSoMo&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId);
                    else if (Direct == "cn_hososanpham_quanly")
                        Thong_bao(this.Page, ThongBao, "cn_hososanpham_quanly.aspx?direct=CN_HoSoMo&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {

                    transaction.Dispose();


                }

            }
        }
        else if (Action == "add")
        {
            //Them moi san pham vao ho so
            string hsId = string.Empty;
            if (Request["HoSoID"] != null)
                hsId = Request["HoSoID"].ToString();

            SanPham ObSanPham = new SanPham();
            ObSanPham.HoSoId = hsId;
            ObSanPham.SanPhamId = ddlTenSanPham.SelectedValue;
            ObSanPham.KyHieu = txtKyHieuSanPham.Text.Trim();
            ObSanPham.NhomSanPhamId = NhomSanPhamId;
            ObSanPham.HangSanXuatId = ddlHangSanXuat.SelectedValue;
            ObSanPham.TrangThaiId = (int)EnTrangThaiSanPhamList.CHO_XU_LY;
            ObSanPham.NgayCapNhatSauCung = DateTime.Now;
            if (ObSanPham.IsValid)
            {
                TransactionManager transaction = ProviderFactory.Transaction;
                if (!transaction.IsOpen)
                    transaction.BeginTransaction();
                try
                {

                    ProviderFactory.SanPhamProvider.Insert(transaction, ObSanPham);

                    string id = ObSanPham.Id;

                    //Cập nhật tiêu chuẩn cho sản phẩm
                    UpdateTieuChuanForSanPham(id, transaction, ref dsMaTieuChuan, ref dsTenTieuChuan);

                    ObSanPham.DsMaTieuChuan = dsMaTieuChuan;
                    ObSanPham.DsTenTieuChuan = dsTenTieuChuan;
                    ProviderFactory.SanPhamProvider.Update(transaction, ObSanPham);

                    //lưu "Nội dung xử lý" & "Ghi chú" vào trong bảng QuaTrinhXuly                 
                    UpdateQuatrinhXuLySanPham(id, transaction);
                    transaction.Commit();
                    //cập nhật tài liệu đính kèm sản phẩm
                    UpdateTaiLieuDinhKemForSanPham(id);
                    string ThongBao = Resources.Resource.msgThemmoiSanPham;
                    //Hướng về trang quản lý sản phẩm của hồ sơ
                    if (Direct == "cn_hososanpham")
                        Thong_bao(this.Page, ThongBao, "CN_HoSoSanPham.aspx?direct=CN_HoSoMo&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId);
                    else if (Direct == "cn_hososanpham_quanly")
                        Thong_bao(this.Page, ThongBao, "cn_hososanpham_quanly.aspx?direct=CN_HoSoMo&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId);

                    //Ghi nhật ký chương trình
                    string strLogString = "Thêm mới Sản phẩm chứng nhận có mã là: " + id;
                    ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.CN_SAN_PHAM_THEM_MOI, strLogString);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    transaction.Dispose();


                }


            }

        }
        ClearFileSession();

    }
    //Clone thông tin về sản phẩm để tạo sản phẩm mới
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// LongHH                      19/07/2018          Tạo mới
    /// </Modifield>
    protected void btnCloneSanPham_Click(object sender, EventArgs e)
    {
        string dsMaTieuChuan = string.Empty;
        string dsTenTieuChuan = string.Empty;

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
        if (Action == "edit")
        {
            //Clone san pham vao ho so

            string hsId = string.Empty;
            if (Request["HoSoID"] != null)
                hsId = Request["HoSoID"].ToString();

            SanPham ObSanPham = new SanPham();
            ObSanPham.HoSoId = hsId;
            ObSanPham.SanPhamId = ddlTenSanPham.SelectedValue;
            ObSanPham.KyHieu = txtKyHieuSanPham.Text.Trim();
            ObSanPham.NhomSanPhamId = NhomSanPhamId;
            ObSanPham.HangSanXuatId = ddlHangSanXuat.SelectedValue;
            ObSanPham.TrangThaiId = (int)EnTrangThaiSanPhamList.CHO_XU_LY;
            ObSanPham.NgayCapNhatSauCung = DateTime.Now;
            if (ObSanPham.IsValid)
            {
                TransactionManager transaction = ProviderFactory.Transaction;
                if (!transaction.IsOpen)
                    transaction.BeginTransaction();
                try
                {

                    ProviderFactory.SanPhamProvider.Insert(transaction, ObSanPham);

                    string id = ObSanPham.Id;

                    //Cập nhật tiêu chuẩn cho sản phẩm
                    UpdateTieuChuanForSanPham(id, transaction, ref dsMaTieuChuan, ref dsTenTieuChuan);

                    ObSanPham.DsMaTieuChuan = dsMaTieuChuan;
                    ObSanPham.DsTenTieuChuan = dsTenTieuChuan;
                    ProviderFactory.SanPhamProvider.Update(transaction, ObSanPham);

                    //lưu "Nội dung xử lý" & "Ghi chú" vào trong bảng QuaTrinhXuly                 
                    UpdateQuatrinhXuLySanPham(id, transaction);
                    transaction.Commit();
                    //cập nhật tài liệu đính kèm sản phẩm
                    UpdateTaiLieuDinhKemForSanPham(id);
                    string ThongBao = "Đã sao chép sản phẩm " + IDSanPham;
                    //Hướng về trang quản lý sản phẩm của hồ sơ
                    if (Direct == "cn_hososanpham")
                        Thong_bao(this.Page, ThongBao, "CN_HoSoSanPham.aspx?direct=CN_HoSoMo&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId);
                    else if (Direct == "cn_hososanpham_quanly")
                        Thong_bao(this.Page, ThongBao, "cn_hososanpham_quanly.aspx?direct=CN_HoSoMo&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId);

                    //Ghi nhật ký chương trình
                    string strLogString = "Sao chép Sản phẩm chứng nhận có mẫ "+id+" từ Sản phẩm chứng nhận có mã " + id;
                    ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.CN_SAN_PHAM_THEM_MOI, strLogString);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    transaction.Dispose();


                }


            }

        }
        ClearFileSession();
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
        //Hướng về trang quản lý sản phẩm của hồ sơ
        if (Direct == "cn_hososanpham")
            Response.Redirect("CN_HoSoSanPham.aspx?direct=CN_HoSoMo&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId);
        else if (Direct == "cn_hososanpham_quanly")
            Response.Redirect("cn_hososanpham_quanly.aspx?direct=CN_HoSoMo&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId);
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
        //bool chk = CheckTrungSanPham(strSpId);
        //if (!chk)
        //{
        //    Thong_bao("Sản phẩm này đã có trong hồ sơ!");
        //    ddlTenSanPham.SelectedIndex = 0;
        //    return;
        //}
        txtNhomSanPham.Text = string.Empty;
        //lấy thông tin về nhóm sản phẩm
        DmSanPham dmSanPham = ProviderFactory.DmSanPhamProvider.GetById(strSpId);
        if (dmSanPham != null)
        {
            NhomSanPhamId = dmSanPham.NhomSanPhamId;
        }
        DmNhomSanPham dmNhomSanPham = ProviderFactory.DmNhomSanPhamProvider.GetById(NhomSanPhamId);
        if (dmNhomSanPham != null)
            txtNhomSanPham.Text = dmNhomSanPham.MaNhom;


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

        if (chklstTieuChuan.Items.Count == 1)
            chklstTieuChuan.Items[0].Selected = true;

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
    public void UpdateTieuChuanForSanPham(string IdSanPham, TransactionManager transaction, ref string dsMaTieuChuan, ref string dsTenTieuChuan)
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

                    DmTieuChuan objDmTieuChuan = ProviderFactory.DmTieuChuanProvider.GetById(transaction, strId);
                    if (objDmTieuChuan != null)
                    {
                        dsMaTieuChuan += objDmTieuChuan.MaTieuChuan + ", ";
                        dsTenTieuChuan += objDmTieuChuan.TenTieuChuan + ", ";
                    }

                    ProviderFactory.SanPhamTieuChuanApDungProvider.Save(transaction, o);
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }

        if (!string.IsNullOrEmpty(dsMaTieuChuan))
            dsMaTieuChuan = dsMaTieuChuan.Substring(0, dsMaTieuChuan.Length - 2);
        if (!string.IsNullOrEmpty(dsTenTieuChuan))
            dsTenTieuChuan = dsTenTieuChuan.Substring(0, dsTenTieuChuan.Length - 2);
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

        UpdateTaiLieuDinhKem(IDSanPham, ref FileUploadTaiLieuDeNghiCN, (int)EnLoaiTaiLieuList.DON_DE_NGHI_CN, "DON_DE_NGHI_CN", ref lblTaiLieuDeNghi);
        UpdateTaiLieuDinhKem(IDSanPham, ref FileUploadGiayToTuCachPhapNhan, (int)EnLoaiTaiLieuList.GIAY_TO_TU_CACH_PHAP_NHAN, "GIAY_TO_TU_CACH_PHAP_NHAN", ref lblTuCachPhapNhan);
        UpdateTaiLieuDinhKem(IDSanPham, ref FileUploadTaiLieuKyThuat, (int)EnLoaiTaiLieuList.TAI_LIEU_KY_THUAT, "TAI_LIEU_KY_THUAT", ref  lblTaiLieuKyThuat);
        UpdateTaiLieuDinhKem(IDSanPham, ref FileUploadKetQuaDoKiem, (int)EnLoaiTaiLieuList.KET_QUA_DO_KIEM, "KET_QUA_DO_KIEM", ref lblKetQuaDoKiem);
        UpdateTaiLieuDinhKem(IDSanPham, ref FileUploadTaiLieuQuyTrinhSanXuat, (int)EnLoaiTaiLieuList.QUY_TRINH_SAN_XUAT, "QUY_TRINH_SAN_XUAT", ref  lblTaiLieuQuyTrinhSanXuat);
        UpdateTaiLieuDinhKem(IDSanPham, ref FileUploadQuyTrinhDamBao, (int)EnLoaiTaiLieuList.QUY_TRINH_CHAT_LUONG, "QUY_TRINH_CHAT_LUONG", ref lblQuyTrinhDamBao);
        UpdateTaiLieuDinhKem(IDSanPham, ref FileUploadChungChiHeThong, (int)EnLoaiTaiLieuList.CHUNG_CHI_HE_THONG_QLCL, "CHUNG_CHI_HE_THONG_QLCL", ref lblChungChiHeThong);
        UpdateTaiLieuDinhKem(IDSanPham, ref FileUploadTieuChuanTuNguyen, (int)EnLoaiTaiLieuList.TIEU_CHUAN_TU_NGUYEN_AP_DUNG, "TIEU_CHUAN_TU_NGUYEN_AP_DUNG", ref lblTieuChuanTuNguyen);
        UpdateTaiLieuDinhKem(IDSanPham, ref FileUploadAnhKTCSSX, (int)EnLoaiTaiLieuList.ANH_KIEM_TRA_CSSX, "ANH_KIEM_TRA_CSSX", ref lblAnhKiemTraCSSX);
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
                    case (int)EnLoaiTaiLieuList.ANH_KIEM_TRA_CSSX: lblAnhKiemTraCSSX.Text = ShowFileLink("Ảnh kiểm tra cơ sở sản xuất", FilePath);
                        lblAnhKiemTraCSSX.Visible = true;
                        lnkXoaAnh.Visible = true;
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
        TList<QuaTrinhXuLy> listqt = ProviderFactory.QuaTrinhXuLyProvider.GetBySanPhamId(IDSanPham);
        if (listqt != null)
        {
            foreach (QuaTrinhXuLy qt in listqt)
            {
                if (qt.LoaiXuLyId == 1)
                {
                    txtGhiChu.Text = qt.GhiChu;
                    txtNoiDungXuLy.Text = qt.NoiDungXuLy;
                    break;
                }
            }
            // Nếu là giám đốc trung tâm
            if (mUserInfo.UserID == mUserInfo.GiamDoc.Id)
            {
                foreach (QuaTrinhXuLy qt in listqt)
                {
                    SysUser objSysUser = ProviderFactory.SysUserProvider.GetById(qt.NguoiXuLyId);
                    if (qt.LoaiXuLyId == (int)EnLoaiXuLyList.DANH_GIA)
                    {
                        txtNoiDungXuLy.Text += objSysUser.FullName + ": " + qt.NgayXuLy + ": " + qt.NoiDungXuLy + "\r\n";
                    }
                }
            }
        }

        GetTieuChuanForSanPham(IDSanPham);
        GetTaiLieuForSanPham(IDSanPham);


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
    /// TuanVM                                          Thêm tham số tên tài liệu (Tên tài liệu cố định cho từng loại, tránh ký tự đặc biệt)
    /// </Modifield>
    public void UpdateTaiLieuDinhKem(string IDSanPham, ref FileUpload fu, int LoaiTaiLieuID, string TenTaiLieu, ref Label lb)
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
            TenFile = MaTT + "_" + Stt + "_" + TenTaiLieu + fu.FileName.Substring(fu.FileName.LastIndexOf("."));
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
                        TenFile = IDSanPham + "_" + TenTaiLieu + TenFile.Substring(TenFile.LastIndexOf("."));
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
                                                 , mUserInfo.UserID, DateTime.Now, txtGhiChu.Text.Trim());
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
                xl.GhiChu = txtGhiChu.Text.Trim();
            }
            else
            {
                xl = QuaTrinhXuLy.CreateQuaTrinhXuLy(IDSanPham, 1, txtNoiDungXuLy.Text.Trim()
                                                , mUserInfo.UserID, DateTime.Now, txtGhiChu.Text.Trim());
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
        string fileName = string.Empty;
        string ServerMapth = ResolveUrl("~/FileUpLoad/");

        fileName = FilePath.Substring(FilePath.LastIndexOf("/") + 1, FilePath.Length - FilePath.LastIndexOf("/") - 1);
        FilePath = ServerMapth + fileName;
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
        b = CheckFileType(ref FileUploadAnhKTCSSX, "FileUploadAnhKTCSSX");
        if (!b)
        {
            c = false;
        }
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
        if (FileUploadAnhKTCSSX.HasFile)
        {
            b = CheckFileSize(ref FileUploadAnhKTCSSX);
            if (!b)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dung lượng file tải lên phải nhỏ hơn hoặc bằng 5 MB!');</script>");
                //FileUploadTieuChuanTuNguyen.Focus();
                SetFileWaitUpload("FileUploadAnhKTCSSX", null);
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
        MakeFileUploadIsReadonly(ref FileUploadAnhKTCSSX);
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
            ShowFileWaitForUpLoad("FileUploadAnhKTCSSX", ref lblAnhKiemTraCSSX);
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
        GetFileWaitForUpLoad("FileUploadAnhKTCSSX", ref FileUploadAnhKTCSSX);
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
        dic = new DictionaryEntry("FileUploadAnhKTCSSX", null);
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
        lnkXoaAnh.Visible = false;

        FileUploadChungChiHeThong.Enabled = false;
        FileUploadGiayToTuCachPhapNhan.Enabled = false;
        FileUploadKetQuaDoKiem.Enabled = false;
        FileUploadQuyTrinhDamBao.Enabled = false;
        FileUploadTaiLieuDeNghiCN.Enabled = false;
        FileUploadTaiLieuKyThuat.Enabled = false;
        FileUploadTaiLieuQuyTrinhSanXuat.Enabled = false;
        FileUploadTieuChuanTuNguyen.Enabled = false;
        FileUploadAnhKTCSSX.Enabled = false;

        ddlHangSanXuat.Enabled = false;
        ddlTenSanPham.Enabled = false;

        chklstTieuChuan.Enabled = false;
        txtGhiChu.ReadOnly = true;
        txtKyHieuSanPham.ReadOnly = true;
        txtKyHieuSanPham.CssClass = "";
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

    /// <summary>
    /// In phiếu đánh giá khi tra cứu hồ sơ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInPhieuDanhGia_Click(object sender, EventArgs e)
    {
        string strHoSoId = string.Empty;
        if (Request["HoSoId"] != null)
            strHoSoId = Request["HoSoId"].ToString();

        string strSanPhamId = string.Empty;
        if (Request["SanPhamId"] != null)
            strSanPhamId = Request["SanPhamId"].ToString();

        ClientScript.RegisterClientScriptBlock(this.GetType(), "CN_XuLyHoSo_DanhGia_GiayChungNhan",
                "<script>popCenter('../ReportForm/DieuKienInBaoCao.aspx?HoSoID=" + strHoSoId + "&SanPhamId=" + strSanPhamId + "&LoaiBaoCao=PhieuDanhGia" + "','CBPhieuDanhGia',450,300);</script>");
    }
    protected void lnkXoaAnh_Click(object sender, EventArgs e)
    {
        lblAnhKiemTraCSSX.Visible = false;
        GetFilesWaitForUpLoad();
        SetFileWaitUpload("FileUploadAnhKTCSSX", null);
        //Session["FileUploadChungChiHeThong"] = null;
        //SetBackFilesWaitForUpload();
        ShowFilesWaitForUpLoad();
    }
    //LongHH Them nut xoa san pham
    protected void btnXoa_Click(object sender, EventArgs e)
    {
        try
        {
            string strSanPhamID = Request["SanPhamID"].ToString();
            SanPham objSP = ProviderFactory.SanPhamProvider.GetById(strSanPhamID);
            //if (objSP.TrangThaiId == (int)EnTrangThaiSanPhamList.MOI_TAO)
            //{
                QLCL_Patch.Delete_SanPham(strSanPhamID);
            //}
            Thong_bao(this.Page, "Đã xóa sản phẩm", "CN_HoSoSanPham.aspx?direct=CN_HoSoMo&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId);
        }
        catch
        {
        }
    }
    // LongHH
    private void SetVisibleAnhKiemTra(HoSo objHoSo)
    {
        if (objHoSo.LoaiHoSo != (int)LoaiHoSo.ChungNhanHopChuan)
        {
            trAnh.Style.Add("Display", "none");
        }
    
    }
}


