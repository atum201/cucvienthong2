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

public partial class WebUI_CB_TiepNhanHoSo_TaoMoi : PageBase
{
    string NhomSanPhamId = string.Empty;
    string HoSoId = string.Empty;
    string TrangThaiId = string.Empty;
    string IDSanPham = string.Empty;
    string Action = string.Empty;
    string Direct = string.Empty;
    string DonViId = string.Empty;
    List<DictionaryEntry> ListFiles = new List<DictionaryEntry>();
    public string sValue = "";

    #region (Form Events)

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    protected void Page_Load(object sender, EventArgs e)
    {
        sValue = LayDanhSachGCN();
        if (Request["HoSoId"] != null)
            HoSoId = Request["HoSoId"].ToString();

        if (Request["Action"] != null)
            Action = Request["Action"].ToString().Trim().ToLower();

        if (Request["TrangThaiId"] != null)
            TrangThaiId = Request["TrangThaiId"].ToString().Trim().ToLower();

        if (Request["direct"] != null)
            Direct = Request["direct"].ToString().Trim().ToLower();

        if (Direct.ToUpper() == "CB_TRACUUHOSO_HOSOSANPHAM")
            btnInPhieuDanhGia.Visible = true;
        else
            btnInPhieuDanhGia.Visible = false;

        if (Request["SanPhamId"] != null)
            IDSanPham = Request["SanPhamId"].ToString();

        if (!IsPostBack)
        {
            //đổ các dữ liệu cần thiết về hồ sơ và sản phẩm
            GetPartOfHosoInform();
            Bind_ListTenSanPham(2);
            Bind_ListHangSanXuat();
            //reset các session lưu thông tin về file chờ upload
            ClearFileSession();
            //hiển thị các link thêm mới theo quyền
            link1.Visible = mUserInfo.IsInRole(EnPermission.QUANLY_DM_SANPHAM);
            link2.Visible = mUserInfo.IsInRole(EnPermission.QUANLY_DM_HANGSX);
        }
        //khởi tạo danh sách file lưu thông tin file chờ upload
        InitListFile();
        SetBackFilesWaitForUpload();

        //ddlTenSanPham.Attributes.Add("onchange", "if(CheckAllFileType()) javascript:setTimeout('__doPostBack(\\'" + ddlTenSanPham.ClientID + "\\',\\'\\')', 0); return false;");
        // bắt các sự kiện post back từ trang popup
        if (this.IsPostBack)
        {
            string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];
            //nếu postback từ trang thêm mới sản phẩm
            if (eventTarget == "SanPhamPostBack")
            {
                Bind_ListTenSanPham(2);//load danh sách sản phẩm loại công bố
                string passedArgument = Request.Params.Get("__EVENTARGUMENT");
                ddlTenSanPham.SelectedValue = passedArgument;
                ddlTenSanPham_SelectedIndexChanged(null, null);
            }
            //nếu postback từ trang thêm mới hãng sản xuất
            else if (eventTarget == "HangSanXuatPostBack")
            {
                Bind_ListHangSanXuat();
                string passedArgument = Request.Params.Get("__EVENTARGUMENT");
                ddlHangSanXuat.SelectedValue = passedArgument;
            }
        }

        //lấy mã đơn vị
        if (HoSoId.Length > 0)
        {
            HoSo hsinform = ProviderFactory.HoSoProvider.GetById(HoSoId);
            if (hsinform != null)
            {
                DonViId = hsinform.DonViId;
            }
        }



        if (Action.Length > 0)
        {
            if (Action == "edit")//nếu là sửa thông tin sản phẩm
            {

                if (IDSanPham.Length > 0)
                {
                    if (!IsPostBack)
                    {
                        Bind_SanPhamForEdit(IDSanPham);
                    }
                    //lấy Mã nhóm sản phẩm theo tên sản phẩm
                    string strSpId = ddlTenSanPham.SelectedValue;
                    DmSanPham dmSanPham = ProviderFactory.DmSanPhamProvider.GetById(strSpId);
                    if (dmSanPham != null)
                    {
                        NhomSanPhamId = dmSanPham.NhomSanPhamId;
                    }

                }
            }
            else if (Action == "add")//nếu là thêm mới sản phẩm
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

                }
            }
            else if (Action == "view") //NẾU là xem thông tin
            {


                if (!IsPostBack)
                {

                    Bind_SanPhamForEdit(IDSanPham);
                }
                //lấy Mã nhóm sản phẩm theo tên sản phẩm
                string strSpId = ddlTenSanPham.SelectedValue;
                DmSanPham dmSanPham = ProviderFactory.DmSanPhamProvider.GetById(strSpId);
                if (dmSanPham != null)
                {
                    NhomSanPhamId = dmSanPham.NhomSanPhamId;
                }
                //ẩn các action control
                DisableActionControls();

            }

        }

        SetFileUploadsReadOnly();
        CreateWebPath();

        btnThemMoiMauDau.Attributes.Add("onclick", "return popCenter('MauDauHopQuy_ChiTiet.aspx?DonViId=" + DonViId + "','ThemMoi',600,300); return false");
        btnChonMauDau.Attributes.Add("onclick", "return popCenter('MauDauHopQuy.aspx?DonViId=" + DonViId + "','Maudau',600,500); return false");

        // LongHH SetReadOnly
        this.SetReadOnly(txtTenTiengAnhSanPham);
        this.SetReadOnly(txtMaSanPham);
        // LongHH
        //nếu sanpham đã có số gcn
        if (rdDaCapChungNhan.Checked)
        {
            ShowAndHideObject(false);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        string dsMaTieuChuan = string.Empty;
        string dsTenTieuChuan = string.Empty;

        GetFilesWaitForUpLoad(); //lấy thông tin về các file mà người dùng đã chọn chờ upload             
        string ImgUrl = hidMauDau.Value;//lấy đường dẫn file ảnh mẫu dấu
        string MauDauId = string.Empty;
        imgMauDau.ImageUrl = ImgUrl;


        //kiểm tra kích thước file upload
        bool ck = CheckFileUploadsSize();
        if (!ck)//nếu sai
        {

            SetBackFilesWaitForUpload();//
            ShowFilesWaitForUpLoad();

            return;

        }
        //kiểm tra kiểu file upload lên
        ck = CheckFilesType();
        if (!ck)
        {

            SetBackFilesWaitForUpload();
            ShowFilesWaitForUpLoad();
            return;
        }
        ShowFilesWaitForUpLoad();

        if (ImgUrl.Contains("MauDauId"))
        {
            MauDauId = ImgUrl.Substring(ImgUrl.IndexOf("MauDauId") + 9, ImgUrl.Length - ImgUrl.IndexOf("MauDauId") - 9);
        }
        //else
        //{
        //    Thong_bao("Chọn mẫu dấu PHTC!");
        //    return;
        //}

        ////kiêm tra số gcn
        //if (rdDaCapChungNhan.Checked)
        //{           
        //   CheckSoGCN();
        //   //return;
        //}

        //kiểm tra có tiêu chuẩn được chọn hay ko?
        ck = CheckListTieuChuan();
        if (!ck)
        {

            return;
        }

        if (Action == "edit" && IDSanPham.Length > 0)//nếu là sửa thông tin sản phẩm
        {
            //Update thong tin san pham thuoc ho so
            SanPham ObSanPham = ProviderFactory.SanPhamProvider.GetById(IDSanPham);
            ObSanPham.KyHieu = txtKyHieuSanPham.Text;
            ObSanPham.HangSanXuatId = ddlHangSanXuat.SelectedValue;
            ObSanPham.NhomSanPhamId = NhomSanPhamId;
            ObSanPham.SanPhamId = ddlTenSanPham.SelectedValue;
            ObSanPham.NgayCapNhatSauCung = DateTime.Now;
            ObSanPham.MauDauId = MauDauId;
            ObSanPham.SoBanCongBo = txtSoBanCongBo.Text.Trim();

            if (rdTuDanhGia.Checked)
            {
                ObSanPham.HinhThucId = (int)EnHinhThucList.TU_DANH_GIA;
                ObSanPham.SoGcn = string.Empty;
                ObSanPham.SoBanTuDanhGia = txtSoBanTuDanhGia.Text.Trim();
                if (txtNgayDanhGia.Text.Length > 1)
                    ObSanPham.NgayDanhGia = Convert.ToDateTime(txtNgayDanhGia.Text);
                else
                    ObSanPham.NgayDanhGia = null;
            }
            else
            {
                ObSanPham.HinhThucId = (int)EnHinhThucList.DA_CAP_PHTC;
                ObSanPham.SoGcn = txtSoGCN.Text;
                ObSanPham.SoBanTuDanhGia = string.Empty;
                ObSanPham.NgayDanhGia = null;
            }

            if (txtNgayCongBo.Text.Length > 0)
                ObSanPham.NgayCongBo = Convert.ToDateTime(txtNgayCongBo.Text);
            else
                ObSanPham.NgayCongBo = null;

            if (ObSanPham.IsValid)
            {
                TransactionManager transaction = ProviderFactory.Transaction;
                try
                {                                        
                    //update thông tin về ghi chú và nội dung xử lý vào bảng Quatrinhxuly                    
                    UpdateQuatrinhXuLySanPham(IDSanPham, transaction);

                    //tiến hành update tiêu chuẩn cho sản phẩm
                    ClearTieuChuanForSanPham(IDSanPham, transaction);

                    UpdateTieuChuanForSanPham(IDSanPham, transaction, ref dsMaTieuChuan, ref dsTenTieuChuan);

                    ObSanPham.DsMaTieuChuan = dsMaTieuChuan;
                    ObSanPham.DsTenTieuChuan = dsTenTieuChuan;
                    ProviderFactory.SanPhamProvider.Update(transaction, ObSanPham);
                    transaction.Commit();
                    transaction.Dispose();

                    //update tài liệu đính kèm cho sản phẩm
                    UpdateTaiLieuDinhKemForSanPham(IDSanPham);
                    string ThongBao = Resources.Resource.msgCapNhatSanPham;

                    //ghi lại vào nhật kí
                    string strLogString = "Sửa thông tin Sản phẩm công bố có mã là: " + IDSanPham;
                    ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.CB_SAN_PHAM_SUA, strLogString);
                    //xóa bỏ thông tin về mẫu dấu trên session
                    Session["MauDau"] = string.Empty;
                    //thực hiện chuyển tới trang quản lý sản phẩm của hồ sơ
                    if (Direct == "cb_hososanpham")
                        Thong_bao(this.Page, ThongBao, "CB_HoSoSanPham.aspx?direct=CB_HoSoMo&HosoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "&UserControl=CB_HoSoDen");
                    else if (Direct == "cb_hososanpham_quanly")
                        Thong_bao(this.Page, ThongBao, "cb_hososanpham_quanly.aspx?direct=CB_HoSoDen&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "&UserControl=CB_HoSoDen");
                    else if (Direct == "cb_xulyhoso_danhgia")
                    {
                        if (Request["PostBack"] != null)
                        {
                            Thong_bao(this.Page, ThongBao, "CB_XuLyHoSo_DanhGia.aspx?PostBack=1&direct=CB_HoSoDen&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "&UserControl=CB_HoSoDen");
                        }
                        else
                            Thong_bao(this.Page, ThongBao, "CB_XuLyHoSo_DanhGia.aspx?direct=CB_HoSoDen&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "&UserControl=CB_HoSoDen");
                    }

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }

            }
        }
        else if (Action == "add")//Thêm mới sản phẩm
        {
            //Them moi san pham vao ho so
            string hsId = string.Empty;
            if (Request["HoSoId"] != null)
                hsId = Request["HoSoId"].ToString();

            SanPham ObSanPham = new SanPham();

            if (rdTuDanhGia.Checked)
            {
                ObSanPham.HinhThucId = (int)EnHinhThucList.TU_DANH_GIA;
                ObSanPham.SoGcn = string.Empty;
                ObSanPham.SoBanTuDanhGia = txtSoBanTuDanhGia.Text.Trim();
                if (txtNgayDanhGia.Text.Length > 1)
                    ObSanPham.NgayDanhGia = Convert.ToDateTime(txtNgayDanhGia.Text);
            }
            else
            {
                ObSanPham.HinhThucId = (int)EnHinhThucList.DA_CAP_PHTC;
                ObSanPham.SoGcn = txtSoGCN.Text;
                ObSanPham.SoBanTuDanhGia = string.Empty;
                ObSanPham.NgayDanhGia = null;
            }


            if (txtNgayCongBo.Text.Length > 0)
                ObSanPham.NgayCongBo = Convert.ToDateTime(txtNgayCongBo.Text);
            else
                ObSanPham.NgayCongBo = null;

            ObSanPham.HoSoId = hsId;
            ObSanPham.SanPhamId = ddlTenSanPham.SelectedValue;
            ObSanPham.KyHieu = txtKyHieuSanPham.Text.Trim();
            ObSanPham.NhomSanPhamId = NhomSanPhamId;
            ObSanPham.HangSanXuatId = ddlHangSanXuat.SelectedValue;
            ObSanPham.TrangThaiId = (int)EnTrangThaiSanPhamList.CHO_XU_LY;
            ObSanPham.NgayCapNhatSauCung = DateTime.Now;
            ObSanPham.MauDauId = MauDauId;
            ObSanPham.SoBanCongBo = txtSoBanCongBo.Text.Trim();
            if (ObSanPham.IsValid)
            {
                TransactionManager transaction = ProviderFactory.Transaction;
                try
                {
                    ProviderFactory.SanPhamProvider.Insert(transaction, ObSanPham);
                    //int stt = ObSanPham.Stt;
                    //DataTable db = ProviderFactory.SanPhamProvider.GetSanPhamID_Extend(stt);
                    //DataRow row = db.Rows[0];
                    string id = ObSanPham.Id;
                    //thêm mới tiêu chuẩn cho sanpham vào bảng SanPham_TieuChuanApDung
                    UpdateTieuChuanForSanPham(id, transaction, ref dsMaTieuChuan, ref dsTenTieuChuan);

                    //chèn nội dung xử lý và ghi chú vào bảng QuaTrinhXuLy
                    UpdateQuatrinhXuLySanPham(id, transaction);

                    ObSanPham.DsTenTieuChuan = dsTenTieuChuan;
                    ObSanPham.DsMaTieuChuan = dsMaTieuChuan;

                    ProviderFactory.SanPhamProvider.Update(transaction, ObSanPham);
                    transaction.Commit();
                    transaction.Dispose();

                    //Chèn tài liệu đính kèm cho sanpham vào bảng tailieudinhkem
                    UpdateTaiLieuDinhKemForSanPham(id);
                    string ThongBao = Resources.Resource.msgThemmoiSanPham;

                    //ghi vào nhật ký chương trình                  
                    string strLogString = "Thêm mới Sản phẩm công bố có mã là: " + id;
                    ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.CB_SAN_PHAM_THEM_MOI, strLogString);
                    //xóa bỏ thông tin về mẫu dấu trên session
                    Session["MauDau"] = string.Empty;
                    //thực hiện chuyển tới trang quản lý sản phẩm của hồ sơ
                    if (Direct == "cb_hososanpham")
                        Thong_bao(this.Page, ThongBao, "CB_HoSoSanPham.aspx?direct=CB_HoSoMo&HosoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId);
                    else if (Direct == "cb_hososanpham_quanly")
                        Thong_bao(this.Page, ThongBao, "cb_hososanpham_quanly.aspx?direct=CB_HoSoDen&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "&UserControl=CB_HoSoDen");
                    else if (Direct == "cb_xulyhoso_danhgia")
                        Thong_bao(this.Page, ThongBao, "cb_xulyhoso_danhgia.aspx?direct=CB_HoSoDen&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "&UserControl=CB_HoSoDen");

                }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                    throw ex;
                }


            }


        }
        //reset lai các thông tin về file chờ upload trên session
        ClearFileSession();

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    protected void lnkXoaTuCachPhapNhan_Click(object sender, EventArgs e)
    {
        lblTuCachPhapNhan.Visible = false;
        //lấy thông tin vê các file chờ upload
        GetFilesWaitForUpLoad();
        //reset thông tin về fileupTuCachPhapNhan trên session 
        SetFileWaitUpload("fileupTuCachPhapNhan", null);
        //hiển thị lại các thông tin về file chờ upload lên màn hình
        ShowFilesWaitForUpLoad();
        string ImgUrl = hidMauDau.Value;//lấy đường dẫn file ảnh mẫu dấu        
        imgMauDau.ImageUrl = ImgUrl;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    protected void lnkXoaTaiLieuKyThuat_Click(object sender, EventArgs e)
    {
        lblTaiLieuKyThuat.Visible = false;
        //lấy thông tin vê các file chờ upload
        GetFilesWaitForUpLoad();
        //reset thông tin về fileupTaiLieuKyThuat trên session 
        SetFileWaitUpload("fileupTaiLieuKyThuat", null);
        //hiển thị lại các thông tin về file chờ upload lên màn hình
        ShowFilesWaitForUpLoad();
        string ImgUrl = hidMauDau.Value;//lấy đường dẫn file ảnh mẫu dấu        
        imgMauDau.ImageUrl = ImgUrl;

    }
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    ///// <Modifield>
    ///// Người tạo                   ngày tạo            chú thích
    ///// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    ///// </Modifield>
    //protected void lnkXoaTaiLieuSanXuat_Click(object sender, EventArgs e)
    //{
    //    lblQuyTrinhSanXuat.Visible = false;
    //    GetFilesWaitForUpLoad();
    //    SetFileWaitUpload("fileupQuyTrinhSanXuat", null);
    //    //Session["fileupQuyTrinhSanXuat"] = null;
    //    //SetBackFilesWaitForUpload();
    //    ShowFilesWaitForUpLoad();
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    protected void lnkXoaBanCongBo_Click(object sender, EventArgs e)
    {
        lblBanCongBo.Visible = false;
        GetFilesWaitForUpLoad();
        SetFileWaitUpload("fileupBanCongBo", null);
        //Session["fileupBanCongBo"] = null;
        //SetBackFilesWaitForUpload();
        ShowFilesWaitForUpLoad();
        string ImgUrl = hidMauDau.Value;//lấy đường dẫn file ảnh mẫu dấu        
        imgMauDau.ImageUrl = ImgUrl;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    protected void lnkXoaTuDanhGia_Click(object sender, EventArgs e)
    {
        lblTuDanhGia.Visible = false;
        GetFilesWaitForUpLoad();
        SetFileWaitUpload("fileupBanTuDanhGia", null);
        //Session["fileupBanTuDanhGia"] = null;
        //SetBackFilesWaitForUpload();
        ShowFilesWaitForUpLoad();
        string ImgUrl = hidMauDau.Value;//lấy đường dẫn file ảnh mẫu dấu        
        imgMauDau.ImageUrl = ImgUrl;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    protected void lnkXoaKetQuaDoKiem_Click(object sender, EventArgs e)
    {
        lblKetQuaDoKiem.Visible = false;
        GetFilesWaitForUpLoad();
        SetFileWaitUpload("fileupKetQuaDoKiem", null);
        //Session["fileupKetQuaDoKiem"] = null;
        //SetBackFilesWaitForUpload();
        ShowFilesWaitForUpLoad();
        string ImgUrl = hidMauDau.Value;//lấy đường dẫn file ảnh mẫu dấu        
        imgMauDau.ImageUrl = ImgUrl;
    }
   
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    protected void lnkXoaTaiLieuKhac_Click(object sender, EventArgs e)
    {
        lblTaiLieuKhac.Visible = false;
        SetFileWaitUpload("fileupTaiLieuKhac", null);
        Session["fileupTaiLieuKhac"] = null;
        //SetBackFilesWaitForUpload();
        ShowFilesWaitForUpLoad();
        string ImgUrl = hidMauDau.Value;//lấy đường dẫn file ảnh mẫu dấu        
        imgMauDau.ImageUrl = ImgUrl;
    }


    /// <summary>
    /// Sự kiện khi radiobutton tự đánh giá được chọn
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    protected void rdTuDanhGia_CheckedChanged(object sender, EventArgs e)
    {
        if (rdTuDanhGia.Checked)
        {
            //load dữ liệu cho ddlTenSanPham vơi loại sản phẩm công bố
            Bind_ListTenSanPham(2);
            //load dữ liệu cho ddlTenSanPham vơi loại sản phẩm chứng nhận
            ClearFileSession();
            //nạp lại danh sách các file chờ upload 
            SetBackFilesWaitForUpload();
            // ẩn các đối tượng control
            ShowAndHideObject(true);
            // thiết lập lại các giá trị cho mặc định cho các dối tượng
            ddlHangSanXuat.SelectedIndex = 0;
            ddlTenSanPham.SelectedIndex = 0;
            txtKyHieuSanPham.Text = string.Empty;
            txtSoGCN.Text = string.Empty;
            chklstTieuChuan.Items.Clear();
            btnCapNhat.Visible = true;
            // LongHH
            if(Action=="edit")
                btnCloneSanPham.Visible = true;
        }
    }
    /// <summary>
    /// Sự kiện radiobutton Đã cấp giấy chứng nhận được chọn
    /// </summary>
    /// <param name="sender"></param>    
    /// <param name="e"></param> 
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    protected void rdDaCapChungNhan_CheckedChanged(object sender, EventArgs e)
    {
        if (rdDaCapChungNhan.Checked)
        {
            //load dữ liệu cho ddlTenSanPham vơi loại sản phẩm chứng nhận
            Bind_ListTenSanPham(1);
            //xóa các thông tin về file chờ upload trên session
            ClearFileSession();
            //nạp lại danh sách các file chờ upload 
            SetBackFilesWaitForUpload();
            // ẩn các đối tượng control
            ShowAndHideObject(false);

            // thiết lập lại các giá trị cho mặc định các dối tượng
            ddlHangSanXuat.SelectedIndex = 0;
            ddlTenSanPham.SelectedIndex = 0;
            txtKyHieuSanPham.Text = string.Empty;
            chklstTieuChuan.Items.Clear();
            txtSoGCN.Text = string.Empty;

            lblBanCongBo.Text = string.Empty;
            lblBanCongBo.Visible = false;

            //lblChungChiHeThong.Text = string.Empty;
            //lblChungChiHeThong.Visible = false;

            lblKetQuaDoKiem.Text = string.Empty;
            lblKetQuaDoKiem.Visible = false;

            //lblQuyTrinhDamBao.Text = string.Empty;
            //lblQuyTrinhDamBao.Visible = false;

            //lblQuyTrinhSanXuat.Text = string.Empty;
            //lblQuyTrinhSanXuat.Visible = false;

            lblTaiLieuKhac.Text = string.Empty;
            lblTaiLieuKhac.Visible = false;

            lblTaiLieuKyThuat.Text = string.Empty;
            lblTaiLieuKyThuat.Visible = false;

            lblTuCachPhapNhan.Text = string.Empty;
            lblTuCachPhapNhan.Visible = false;

            lblTuDanhGia.Text = string.Empty;
            lblTuDanhGia.Visible = false;

            imgMauDau.ImageUrl = null;
            //LongHH
            btnCloneSanPham.Visible = false;
        }
    }
    /// <summary>
    /// Sự kiện dropdownlist Tên sản phẩm thay đổi
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    protected void ddlTenSanPham_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strSpId = ddlTenSanPham.SelectedValue;

        //bool chk = CheckTrungSanPham(strSpId,string.Empty);
        //if (!chk)
        //{
        //    Thong_bao("Sản phẩm này đã có trong hồ sơ!");
        //    ddlTenSanPham.SelectedIndex = 0;
        //    return;
        //}
        txtKyHieuSanPham.Text = string.Empty;

        Bind_ListTieuChuan(strSpId);

        //lấy mã nhóm sản phẩm và ID của nhóm sản phâm
        DmSanPham dmSanPham = ProviderFactory.DmSanPhamProvider.GetById(strSpId);
        // LongHH
        SanPham sanpham = ProviderFactory.SanPhamProvider.LayThongTinSanPhamBySanPhamId(strSpId);
        if (sanpham != null)
        {
            txtTenTiengAnhSanPham.Text = sanpham.DmSanPhamTenTiengAnh;
            txtMaSanPham.Text = sanpham.DmNhomSpMaNhom;
        }
        // LongHH
        if (dmSanPham != null)
        {
            NhomSanPhamId = dmSanPham.NhomSanPhamId;
        }

        //lấy mẫu dấu đã được chọn
        string ImgUrl = hidMauDau.Value;
        if (ImgUrl.Length > 0)
            imgMauDau.ImageUrl = ImgUrl;

        //lấy các thông tin về file chờ upload và kiểm tra các điều kiện kèm theo file
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
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009             Tạo mới
    /// </Modifield>
    protected void txtSoGCN_TextChanged(object sender, EventArgs e)
    {
        bool b = true;
        //kiểm tra kích thước và kiểu file upload
        if (fileupBanCongBo.HasFile)
        {
            b = CheckFileSize(ref fileupBanCongBo);
            if (!b)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dung lượng file " + fileupBanCongBo.FileName + " tải lên phải nhỏ hơn hoặc bằng 5 MB!');</script>");

                SetFileWaitUpload("fileupBanCongBo", null);

                return;
            }
        }

        b = CheckFileType(ref fileupBanCongBo, "fileupBanCongBo");
        if (!b)
            return;
        //lấy các thông tin về file chờ upload nếu như thỏa mãn điều kiện
        GetFileWaitForUpLoad("fileupBanCongBo", ref fileupBanCongBo);
        ShowFileWaitForUpLoad("fileupBanCongBo", ref lblBanCongBo);
        //kiểm tra xem số giấy chứng nhận đã tồn tại trong hoso hay ko?
        CheckSoGCN();

    }
    #endregion

    #region(Hàm tự tạo)

    /// <summary>
    /// lay cac thong tin ve Ho so
    /// </summary>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    public void GetPartOfHosoInform()
    {
        if (HoSoId.Length > 0)
        {
            HoSo hsinform = ProviderFactory.HoSoProvider.GetById(HoSoId);
            if (hsinform != null)
            {
                txtSoHoSo.Text = hsinform.SoHoSo;
                int? ob = hsinform.TrangThaiId;

                string ttid = ProviderFactory.SysConfigProvider.GetValue("MA_TRUNG_TAM");
                if (ttid.Length > 0)
                {
                    DmTrungTam dmtt = ProviderFactory.DmTrungTamProvider.GetById(ttid);
                    if (dmtt != null)
                        txtTrungTam.Text = dmtt.TenTrungTam;
                }
                if (ob != null)
                {
                    TrangThaiId = ob.ToString();
                    int thId = Convert.ToInt16(ob);
                    EnTrangThaiHoSo TrangThai = ProviderFactory.EnTrangThaiHoSoProvider.GetById(thId);
                    if (TrangThai != null)
                        txtTrangThai.Text = TrangThai.MoTa;
                }
            }
        }
    }
    /// <summary>
    /// Hiển thị các thông tin về sản phẩm
    /// </summary>
    /// <param name="IdSanPham"></param>
    /// <Modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
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
        txtSoBanCongBo.Text = sp.SoBanCongBo;
        ddlHangSanXuat.SelectedValue = sp.HangSanXuatId;
        //lấy các thông tin về ghi chú và nội dung xử lý
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

        if (sp.HinhThucId == (int)EnHinhThucList.DA_CAP_PHTC)//nếu như san phẩm đã có giấy chứng nhận
        {
            txtSoGCN.Text = sp.SoGcn;
            rdDaCapChungNhan.Checked = true;
            rdTuDanhGia.Checked = false;
            Bind_ListTenSanPham(1);

            ShowAndHideObject(false);
        }
        else
        {
            rdTuDanhGia.Checked = true;
            btnCloneSanPham.Visible = true;
            rdDaCapChungNhan.Checked = false;
            Bind_ListTieuChuan(sp.SanPhamId);
            txtSoBanTuDanhGia.Text = sp.SoBanTuDanhGia;
            txtNgayDanhGia.Text = sp.NgayDanhGia == null ? string.Empty : Convert.ToDateTime(sp.NgayDanhGia).ToShortDateString();
        }

        txtNgayCongBo.Text = sp.NgayCongBo == null ? string.Empty : sp.NgayCongBo.Value.ToShortDateString();
        ddlTenSanPham.SelectedValue = sp.SanPhamId;
        GetNhomSanPhamInform(sp.SanPhamId);

        //hiển thị mẫu dấu PHTC nếu có
        if (sp.MauDauId != null)
        {
            imgMauDau.ImageUrl = "../Handler.ashx?MauDauId=" + sp.MauDauId;
            hidMauDau.Value = imgMauDau.ImageUrl;
        }
        //lấy tiêu chuẩn cho sản phẩm
        GetTieuChuanForSanPham(IDSanPham);
        //lấy tài liệu đính kèm cùng sản phẩm
        GetTaiLieuForSanPham(IDSanPham);


    }
    /// <summary>
    /// Lấy danh sách các ten san pham 
    /// </summary>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    public void Bind_ListTenSanPham(int typeSp)
    {
        DataTable dmSanPham = ProviderFactory.DmSanPhamProvider.GetByLoaiHoSo(1, 2);
        ddlTenSanPham.DataSource = dmSanPham;
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
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
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
    /// Lấy danh sách các tiêu chuẩn hiển thị ra checklistbox
    /// </summary>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    //  public void Bind_ListTieuChuan()
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
    /// Lấy danh sách các tài liệu đính kèm sản phẩm
    /// </summary>
    /// <param name="IDSanPham"></param>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    public void GetTaiLieuForSanPham(string IDSanPham)
    {
        TList<TaiLieuDinhKem> listTaiLieu = ProviderFactory.TaiLieuDinhKemProvider.GetBySanPhamId(IDSanPham);
        if (listTaiLieu != null)
        {
            foreach (TaiLieuDinhKem tl in listTaiLieu)
            {
                string FilePath = tl.TenFile.Trim();
                string FileName = FilePath.Substring(FilePath.LastIndexOf("/") + 1, FilePath.Length - FilePath.LastIndexOf("/") - 1);
                if (tl.LoaiTaiLieuId != null)
                {
                    switch (tl.LoaiTaiLieuId)
                    {
                        case (int)EnLoaiTaiLieuList.BAN_CONG_BO: lblBanCongBo.Text = ShowFileLink("Bản công bố", FilePath);
                            lblBanCongBo.Visible = true;
                            lnkXoaBanCongBo.Visible = true;
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
                        //case (int)EnLoaiTaiLieuList.QUY_TRINH_SAN_XUAT: lblQuyTrinhSanXuat.Text = ShowFileLink("quy trình sản xuât", FilePath);
                        //    lblQuyTrinhSanXuat.Visible = true;
                        //    lnkXoaTaiLieuSanXuat.Visible = true;
                        //    break;
                        //case (int)EnLoaiTaiLieuList.QUY_TRINH_CHAT_LUONG: lblQuyTrinhDamBao.Text = ShowFileLink("Quy trình chất lượng", FilePath);
                        //    lblQuyTrinhDamBao.Visible = true;
                        //    lnkXoaQuyTrinhDamBao.Visible = true;
                        //    break;
                        //case (int)EnLoaiTaiLieuList.CHUNG_CHI_HE_THONG_QLCL: lblChungChiHeThong.Text = ShowFileLink("Chứng chỉ HTQL CL", FilePath);
                        //    lblChungChiHeThong.Visible = true;
                        //    lnkXoaChungChiHeThong.Visible = true;
                        //    break;
                        case (int)EnLoaiTaiLieuList.BAN_TU_DANH_GIA: lblTuDanhGia.Text = ShowFileLink("Bản tự đánh giá", FilePath);
                            lblTuDanhGia.Visible = true;
                            lnkXoaTuDanhGia.Visible = true;
                            break;
                        case (int)EnLoaiTaiLieuList.TAI_LIEU_KHAC: lblTaiLieuKhac.Text = ShowFileLink("Tài liệu khác", FilePath);
                            lblTaiLieuKhac.Visible = true;
                            lnkXoaTaiLieuKhac.Visible = true;
                            break;
                    }
                }
            }
        }

    }
    /// <summary>
    /// Lay cac tieu chuan ap dung cho san pham
    /// </summary>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    public void GetTieuChuanForSanPham(string IDSanPham)
    {
        if (rdTuDanhGia.Checked)//nếu tự đánh giá sản phẩm
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
        else//nếu sản phẩm đã có số giấy chứng nhận
        {
            TList<SanPhamTieuChuanApDung> listSPTieuChuan = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(IDSanPham);
            foreach (SanPhamTieuChuanApDung o in listSPTieuChuan)
            {
                DmTieuChuan dmTieuChuan = ProviderFactory.DmTieuChuanProvider.GetById(o.TieuChuanApDungId);
                ListItem Item = new ListItem(dmTieuChuan.MaTieuChuan, dmTieuChuan.Id);
                Item.Selected = true;
                chklstTieuChuan.Items.Add(Item);

            }
        }
    }
    /// <summary>
    /// Lấy thông tin về nhóm sản phẩm 
    /// </summary>
    /// <param name="IDSanPham"></param>
    public void GetNhomSanPhamInform(string IDSanPham)
    {
        DmSanPham dmSanPham = ProviderFactory.DmSanPhamProvider.GetById(IDSanPham);
        if (dmSanPham != null)
        {
            NhomSanPhamId = dmSanPham.NhomSanPhamId;
        }
    }
    /// <summary>
    /// Hiển thị tên file và đường link tới file lên nền web
    /// </summary>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
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
    /// Ẩn hiện các đối tượng
    /// </summary>
    /// <param name="b">false: Da cap GCN, True: Tu danh gia</param> 
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    public void ShowAndHideObject(bool b)
    {
        lblSoGCN.Visible = !b;
        txtSoGCN.Visible = !b;
        rqfSoGCN.Visible = !b;

        fileupBanTuDanhGia.Visible = b;
        lblstrTuDanhGia.Visible = b;

        lblTuDanhGia.Visible = b;
        lnkXoaTuDanhGia.Visible = b;

        fileupKetQuaDoKiem.Visible = b;
        lblstrKetQuaDoKiem.Visible = b;
        lblKetQuaDoKiem.Visible = b;
        lnkXoaKetQuaDoKiem.Visible = b;

        fileupTaiLieuKhac.Visible = b;
        lblstrTaiLieuKhac.Visible = b;
        lblTaiLieuKhac.Visible = b;
        lnkXoaTaiLieuKhac.Visible = b;


        fileupTaiLieuKyThuat.Visible = b;
        lblstrTaiLieuKyThuat.Visible = b;
        lblTaiLieuKyThuat.Visible = b;
        lnkXoaTaiLieuKyThuat.Visible = b;


        fileupTuCachPhapNhan.Visible = b;
        lblstrTuCachPhapNhan.Visible = b;

        lblTuCachPhapNhan.Visible = b;
        lnkXoaTuCachPhapNhan.Visible = b;

        if (b)
        {
            trf1.Style.Add("display", "");
            trf2.Style.Add("display", "");
            trf3.Style.Add("display", "");
            trf5.Style.Add("display", "");
            trf8.Style.Add("display", "");
            trTuDanhGia.Style.Add("display", "");
        }
        else
        {
            trf1.Style.Add("display", "none");
            trf2.Style.Add("display", "none");
            trf3.Style.Add("display", "none");
            trf8.Style.Add("display", "none");
            trf5.Style.Add("display", "none");
            trTuDanhGia.Style.Add("display", "none");
        }
    }
    /// <summary>
    /// Xóa bỏ các tiêu chuẩn gắn với sản phẩm
    /// </summary>
    /// <param name="IDSanPham"></param>
    /// <param name="transaction"></param>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
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
    /// Cập nhật cac Tieu chuan cho  San pham
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
    /// Cập nhật quá trình xử lý sản phàm vào CSDL
    /// </summary>
    /// <param name="IDSanPham"></param>
    /// <param name="transaction"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    public void UpdateQuatrinhXuLySanPham(string IDSanPham, TransactionManager transaction)
    {
        QuaTrinhXuLy xl = null;
        if (Request["SanPhamID"] == null)//nếu như là thêm mới sản phẩm
        {
            xl = QuaTrinhXuLy.CreateQuaTrinhXuLy(IDSanPham, 1, txtNoiDungXuLy.Text.Trim()
                                                 , mUserInfo.UserID, DateTime.Now, txtGhiChu.Text.Trim());
        }
        else//nếu là edit thông tin sản phẩm
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
    /// Cập nhật cac tai lieu dinh kem cho  San pham
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    public void UpdateTaiLieuDinhKemForSanPham(string IDSanPham)
    {

        UpdateTaiLieuDinhKem(IDSanPham, ref fileupBanCongBo, (int)EnLoaiTaiLieuList.BAN_CONG_BO, "BAN_CONG_BO", ref lblBanCongBo);
        UpdateTaiLieuDinhKem(IDSanPham, ref fileupTuCachPhapNhan, (int)EnLoaiTaiLieuList.GIAY_TO_TU_CACH_PHAP_NHAN, "GIAY_TO_TU_CACH_PHAP_NHAN", ref lblTuCachPhapNhan);
        UpdateTaiLieuDinhKem(IDSanPham, ref fileupTaiLieuKyThuat, (int)EnLoaiTaiLieuList.TAI_LIEU_KY_THUAT, "TAI_LIEU_KY_THUAT", ref  lblTaiLieuKyThuat);
        UpdateTaiLieuDinhKem(IDSanPham, ref fileupKetQuaDoKiem, (int)EnLoaiTaiLieuList.KET_QUA_DO_KIEM, "KET_QUA_DO_KIEM", ref lblKetQuaDoKiem);
        //UpdateTaiLieuDinhKem(IDSanPham, ref fileupQuyTrinhSanXuat, (int)EnLoaiTaiLieuList.QUY_TRINH_SAN_XUAT, ref  lblQuyTrinhSanXuat);
        //UpdateTaiLieuDinhKem(IDSanPham, ref  fileupDamBaoChatLuong, (int)EnLoaiTaiLieuList.QUY_TRINH_CHAT_LUONG, ref lblQuyTrinhDamBao);
        //UpdateTaiLieuDinhKem(IDSanPham, ref fileupChungChi, (int)EnLoaiTaiLieuList.CHUNG_CHI_HE_THONG_QLCL, ref lblChungChiHeThong);
        UpdateTaiLieuDinhKem(IDSanPham, ref fileupBanTuDanhGia, (int)EnLoaiTaiLieuList.BAN_TU_DANH_GIA, "BAN_TU_DANH_GIA", ref lblTuDanhGia);
        UpdateTaiLieuDinhKem(IDSanPham, ref fileupTaiLieuKhac, (int)EnLoaiTaiLieuList.TAI_LIEU_KHAC, "TAI_LIEU_KHAC", ref lblTaiLieuKhac);

    }
    /// <summary>
    /// Cập nhập thông tin tai lieu kem theo vao CSDL
    /// </summary>
    /// <param name="IDSanPham"></param>
    /// <param name="fu"></param>
    /// <param name="LoaiTaiLieuID"></param>
    /// <param name="lbfileName"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
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
            //tiến hành tạo tên file theo quy tắc "SanPhamId_Tênfile"
            TenFile = IDSanPham + "_" + TenTaiLieu + fu.FileName.Substring(fu.FileName.LastIndexOf("."));

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
                    //lưu file lên server
                    if (!File.Exists(FilePath))
                        fu.PostedFile.SaveAs(FilePath);
                    string tailieuId = string.Empty;
                    //tiến hành  file tài liệu đính kèm vừa bị thay dổi
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
                    if (tailieuId.Length > 0)
                    {
                        tl = ProviderFactory.TaiLieuDinhKemProvider.GetById(tailieuId.Trim());
                        if (tl != null)
                        {
                            //tiến hành xóa file tài liệu cũ 
                            string FileName = tl.TenFile;
                            FileName = FileName.Substring(FileName.LastIndexOf("/") + 1, FileName.Length - FileName.LastIndexOf("/") - 1);
                            FilePath = ServerMapth + FileName;
                            DeleteFile(FilePath);
                            //gán lại đường dẫn file mới trường tenfile trong bản ghi
                            tl.TenFile = TenFile;
                        }
                    }
                }
                //tiến hành cập nhật vào bảng tài liệu đính kèm
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
        else//nếu như ko có file lựa chọn trong đối tượng fileupload
        {
            TaiLieuDinhKem tl = new TaiLieuDinhKem();
            //neu nhu da co file luu tam tren server
            if (lb.Visible && !lb.Text.Contains("<a href"))
            {
                //Tiến hành duyệt từng file trong danh sách file chờ upload
                foreach (DictionaryEntry dic in ListFiles)
                {
                    if (dic.Key == fu.ID && dic.Value != null)
                    {
                        string TempFile = dic.Value.ToString();
                        TenFile = TempFile;
                        //tiến hành tạo tên file theo quy tắc "MaTT_STT_Tênfile"
                        TenFile = TenFile.Substring(TenFile.LastIndexOf("\\") + 1, TenFile.Length - TenFile.LastIndexOf("\\") - 1);
                        //lấy stt tiếp theo trong bảng tài liệu đính kèm
                        string Stt = ProviderFactory.TaiLieuDinhKemProvider.GetNextSTT();
                        string MaTT = ProviderFactory.SysConfigProvider.GetValue("MA_TRUNG_TAM");
                        TenFile = TenFile.Replace(fu.ID, "");
                        TenFile = IDSanPham + "_" + TenTaiLieu + TenFile.Substring(TenFile.LastIndexOf("."));
                        //cat bo cac khoang trong ve mot khoang
                        while (TenFile.Contains("  "))
                            TenFile = TenFile.Replace("  ", " ");
                        //thay cac khoang trong bang '_'
                        TenFile = TenFile.Replace(' ', '_');
                        //lấy đường dẫn lưu trên server
                        FilePath = ServerMapth + TenFile;
                        TenFile = ResolveUrl("~/FileUpLoad/") + TenFile;
                        //tiến hành copy file từ thư mục lưu tạm sang thư muc chính
                        File.Copy(TempFile, FilePath);
                        //thêm môt bản ghi vào bảng tài liệu đính kèm sản phẩm
                        tl = TaiLieuDinhKem.CreateTaiLieuDinhKem("", IDSanPham, LoaiTaiLieuID, TenFile, DateTime.Now, null);
                        ProviderFactory.TaiLieuDinhKemProvider.Save(transaction, tl);
                        transaction.Commit();
                        break;
                    }
                }
            }
            //Nếu có file bị xóa thì tiến hành xóa trong CSDL và file vật lý trên Server
            if (lb.Visible == false && lb.Text.Contains("<a href"))
            {
                try
                {
                    string tailieuId = string.Empty;
                    //lấy ID của bản ghi lưu thông tin file đính kèm bị xóa
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
                    if (tailieuId.Length > 0)// nếu tồn tại ID của bản ghi đó
                    {
                        tl = ProviderFactory.TaiLieuDinhKemProvider.GetById(tailieuId.Trim());

                        if (tl != null)
                        {
                            //tiến hành xóa bản ghi và file vật lý trên server
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
    /// Xóa file vật lý trên Server
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
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
    /// Kiểm tra dung lượng của tất cả các file tải lên  qua FileUpload
    /// </summary>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    public bool CheckFileUploadsSize()
    {


        bool b = true;
        bool c = true;
        //if (fileupChungChi.HasFile)
        //{
        //    b = CheckFileSize(ref fileupChungChi);
        //    if (!b)
        //    {
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dung lượng file tải lên phải nhỏ hơn hoặc bằng 5 MB!');</script>");
        //        //fileupChungChi.Focus();
        //        SetFileWaitUpload("fileupChungChi", null);
        //       // Session["fileupChungChi"] = null;
        //        c = false;
        //    }
        //}
        if (fileupTuCachPhapNhan.HasFile)
        {
            b = CheckFileSize(ref fileupTuCachPhapNhan);
            if (!b)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dung lượng file tải lên phải nhỏ hơn hoặc bằng 5 MB!');</script>");
                //reset lại thông tin về file upload trên session
                SetFileWaitUpload("fileupTuCachPhapNhan", null);
                c = false;
            }
        }
        if (fileupKetQuaDoKiem.HasFile)
        {
            b = CheckFileSize(ref fileupKetQuaDoKiem);
            if (!b)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dung lượng file tải lên phải nhỏ hơn hoặc bằng 5 MB!');</script>");
                //reset lại thông tin về file upload trên session
                SetFileWaitUpload("fileupKetQuaDoKiem", null);
                //Session["fileupKetQuaDoKiem"] = null;
                c = false;
            }
        }
        //if (fileupDamBaoChatLuong.HasFile)
        //{
        //    b = CheckFileSize(ref fileupDamBaoChatLuong);
        //    if (!b)
        //    {
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dung lượng file tải lên phải nhỏ hơn hoặc bằng 5 MB!');</script>");
        //       // fileupDamBaoChatLuong.Focus()
        //        SetFileWaitUpload("fileupDamBaoChatLuong", null);
        //        //Session["fileupDamBaoChatLuong"] = null;
        //        c = false;
        //    }
        //}
        if (fileupBanCongBo.HasFile)
        {
            b = CheckFileSize(ref fileupBanCongBo);
            if (!b)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dung lượng file " + fileupBanCongBo.FileName + " tải lên phải nhỏ hơn hoặc bằng 5 MB!');</script>");
                //reset lại thông tin về file upload trên session
                SetFileWaitUpload("fileupBanCongBo", null);
                //Session["fileupBanCongBo"] = null;
                c = false;
            }
        }
        if (fileupTaiLieuKyThuat.HasFile)
        {
            b = CheckFileSize(ref fileupTaiLieuKyThuat);
            if (!b)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dung lượng file " + fileupTaiLieuKyThuat.FileName + " tải lên phải nhỏ hơn hoặc bằng 5 MB!');</script>");
                //reset lại thông tin về file upload trên session
                SetFileWaitUpload("fileupTaiLieuKyThuat", null);
                // Session["fileupTaiLieuKyThuat"] = null;
                c = false;
            }
        }
        //if (fileupQuyTrinhSanXuat.HasFile)
        //{
        //    b = CheckFileSize(ref fileupQuyTrinhSanXuat);
        //    if (!b)
        //    {
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dung lượng file " + fileupQuyTrinhSanXuat.FileName + " tải lên phải nhỏ hơn hoặc bằng 5 MB!');</script>");
        //        //fileupQuyTrinhSanXuat.Focus();
        //        SetFileWaitUpload("fileupQuyTrinhSanXuat", null);
        //        // Session["fileupQuyTrinhSanXuat"] = null;
        //        c = false;
        //    }
        //}
        if (fileupBanTuDanhGia.HasFile)
        {
            b = CheckFileSize(ref fileupBanTuDanhGia);
            if (!b)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dung lượng file " + fileupBanTuDanhGia.FileName + " tải lên phải nhỏ hơn hoặc bằng 5 MB!');</script>");
                //reset lại thông tin về file upload trên session
                SetFileWaitUpload("fileupBanTuDanhGia", null);
                //Session["fileupBanTuDanhGia"] = null;
                c = false;
            }
        }
        if (fileupTaiLieuKhac.HasFile)
        {
            b = CheckFileSize(ref fileupTaiLieuKhac);
            if (!b)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dung lượng file " + fileupTaiLieuKhac.FileName + " tải lên phải nhỏ hơn hoặc bằng 5 MB!');</script>");
                //reset lại thông tin về file upload trên session
                SetFileWaitUpload("fileupTaiLieuKhac", null);
                //Session["fileupTaiLieuKhac"] = null;
                c = false;
            }
        }

        return c;
    }
    /// <summary>
    /// Kiểm tra dung lượng file tải lên qua FileUpload
    /// </summary>
    /// <param name="fu"></param>
    /// <returns></returns>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
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
    /// 
    /// </summary>
    /// <returns></returns>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    public bool CheckFilesType()
    {
        bool b = true;
        bool c = true;
        b = CheckFileType(ref fileupBanCongBo, "fileupBanCongBo");
        if (!b)
            c = false;
        b = CheckFileType(ref fileupBanTuDanhGia, "fileupBanTuDanhGia");
        if (!b)
            c = false;

        //b = CheckFileType(ref fileupChungChi, "fileupChungChi");
        //if (!b)
        //    c = false;
        //b = CheckFileType(ref fileupDamBaoChatLuong, "fileupDamBaoChatLuong");
        //if (!b)
        //    c = false;

        b = CheckFileType(ref fileupKetQuaDoKiem, "fileupKetQuaDoKiem");
        if (!b)
            c = false;

        //b = CheckFileType(ref fileupQuyTrinhSanXuat, "fileupQuyTrinhSanXuat");
        //if (!b)
        //    c = false;

        b = CheckFileType(ref fileupTaiLieuKhac, "fileupTaiLieuKhac");
        if (!b)
            c = false;
        b = CheckFileType(ref fileupTaiLieuKyThuat, "fileupTaiLieuKyThuat");
        if (!b)
            c = false;
        b = CheckFileType(ref fileupTuCachPhapNhan, "fileupTuCachPhapNhan");
        if (!b)
            c = false;

        return c;
    }
    /// <summary>
    /// Kiểm tra kiểu file upload lên
    /// </summary>
    /// <param name="fu"></param>
    /// <returns></returns>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
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
                //fu.Focus();
                SetFileWaitUpload(funame, null);
                Session["ListFilesUpload"] = ListFiles;
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Kiểm tra xem người dùng đã lựa chọn tiêu chuẩn hay chưa
    /// </summary>
    /// <param name="fu"></param>
    /// <returns></returns>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
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
        //if (rdDaCapChungNhan.Checked)
        //    ShowAndHideObject(false);
        return false;
    }
    /// <summary>
    /// Kiểm tra san phẩm có bị trùng hay ko
    /// </summary>
    /// <param name="SanPhamId"></param>
    /// <returns></returns>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009             Tạo mới
    /// </Modifield>
    public bool CheckTrungSanPham(string ChkSanPhamId, string SoGCN)
    {

        TList<SanPham> listSP = ProviderFactory.SanPhamProvider.GetByHoSoId(HoSoId);
        foreach (SanPham sp in listSP)
        {
            if (Action == "add")
            {
                if (rdDaCapChungNhan.Checked)
                {
                    if (SoGCN.Length > 0)
                    {
                        if (sp.SoGcn == SoGCN)
                            return false;
                    }
                }
                else
                    if (sp.SanPhamId == ChkSanPhamId)
                        return false;
            }
            else if (Action == "edit")
            {
                SanPham sp1 = ProviderFactory.SanPhamProvider.GetById(IDSanPham);
                if (rdDaCapChungNhan.Checked)
                {
                    if (SoGCN.Length > 0)
                    {
                        if (sp.SoGcn == SoGCN && sp.SoGcn != sp1.SoGcn)
                            return false;
                    }
                }
                else
                {

                    if (sp.SanPhamId == ChkSanPhamId && sp.SanPhamId != sp1.SanPhamId)
                        return false;
                }
            }
        }
        return true;
    }
    /// <summary>
    /// thiết lập chê độ chỉ xem trên các textbox của các fileupload
    /// </summary>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009             Tạo mới
    /// </Modifield>
    public void SetFileUploadsReadOnly()
    {
        MakeFileUploadIsReadonly(ref fileupBanCongBo);
        MakeFileUploadIsReadonly(ref fileupBanTuDanhGia);
        //MakeFileUploadIsReadonly(ref fileupChungChi);
        //MakeFileUploadIsReadonly(ref fileupDamBaoChatLuong);
        MakeFileUploadIsReadonly(ref fileupKetQuaDoKiem);
        //MakeFileUploadIsReadonly(ref fileupQuyTrinhSanXuat);
        MakeFileUploadIsReadonly(ref fileupTaiLieuKhac);
        MakeFileUploadIsReadonly(ref fileupTaiLieuKyThuat);
        MakeFileUploadIsReadonly(ref fileupTuCachPhapNhan);
    }
    /// <summary>
    /// thiết lập chê độ chỉ xem trên textbox của  đối tượng fileupload
    /// </summary>
    /// <param name="fu"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009             Tạo mới
    /// </Modifield>
    public void MakeFileUploadIsReadonly(ref FileUpload fu)
    {
        fu.Attributes.Add("onkeypress", "return false;");
        fu.Attributes.Add("onkeyup", "return false;");
        fu.Attributes.Add("onpaste", "return false;");
    }
    /// <summary>
    /// Kiểm tra xem số giấy chứng nhận nhập vào đã có trong hồ sơ hay chưa
    /// </summary>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009             Tạo mới
    /// </Modifield>
    public void CheckSoGCN()
    {
        string SoGCN = txtSoGCN.Text.Trim();
        //txtKyHieuSanPham.Text = string.Empty;
        //ddlHangSanXuat.SelectedIndex = 0;
        //ddlTenSanPham.SelectedIndex = 0;
        //chklstTieuChuan.Items.Clear();
        if (SoGCN.Length > 0)
        {
            //string WhereClause = " sogcn=N'" + SoGCN + "'";
            //int count = 0;
            DataTable db = ProviderFactory.SanPhamProvider.CheckSoGCN(SoGCN, DonViId);
            if (db.Rows.Count > 0)
            {
                DataRow sp = db.Rows[0];
                bool chk = CheckTrungSanPham(sp["Id"].ToString(), sp["SoGcn"].ToString());
                if (!chk)
                {
                    Thong_bao("Sản phẩm với số GCN này đã có trong hồ sơ!");
                    //ShowAndHideObject(false);
                    txtSoGCN.Text = string.Empty;
                    return;
                }
                txtKyHieuSanPham.Text = sp["KyHieu"].ToString();
                ddlTenSanPham.SelectedValue = sp["SanPhamId"].ToString();
                //LongHH
                if (!string.IsNullOrEmpty(sp["SanPhamId"].ToString()))
                {
                    SanPham sanpham = ProviderFactory.SanPhamProvider.LayThongTinSanPhamBySanPhamId(sp["SanPhamId"].ToString());
                    txtTenTiengAnhSanPham.Text = sanpham.DmSanPhamTenTiengAnh;
                    txtMaSanPham.Text = sanpham.DmNhomSpMaNhom;
                }
                //LongHH
                GetNhomSanPhamInform(sp["SanPhamId"].ToString());
                ddlHangSanXuat.SelectedValue = sp["HangSanXuatId"].ToString();
                GetTieuChuanForSanPham(sp["Id"].ToString());
                btnCapNhat.Visible = true;

            }
            //ShowAndHideObject(false);
        }
    }
    /// <summary>
    /// Tiến hành ẩn cũng như vô hiệu các control khi chỉ xem nội dung mà ko cho thay đổi
    /// </summary>
    public void DisableActionControls()
    {
        btnCapNhat.Visible = false;

        lnkXoaBanCongBo.Visible = false;
        //lnkXoaChungChiHeThong.Visible = false;
        lnkXoaKetQuaDoKiem.Visible = false;
        //lnkXoaQuyTrinhDamBao.Visible = false;
        lnkXoaTaiLieuKhac.Visible = false;
        lnkXoaTaiLieuKyThuat.Visible = false;
        //lnkXoaTaiLieuSanXuat.Visible = false;
        lnkXoaTuCachPhapNhan.Visible = false;
        lnkXoaTuDanhGia.Visible = false;
        btnChonMauDau.Visible = false;
        btnThemMoiMauDau.Visible = false;
        txtGhiChu.ReadOnly = true;
        txtNoiDungXuLy.ReadOnly = true;
        txtKyHieuSanPham.ReadOnly = true;
        ddlHangSanXuat.Enabled = false;
        ddlTenSanPham.Enabled = false;
        chklstTieuChuan.Enabled = false;
        rdDaCapChungNhan.Enabled = false;
        rdTuDanhGia.Enabled = false;
        fileupBanCongBo.Enabled = false;
        fileupBanTuDanhGia.Enabled = false;
        //fileupChungChi.Enabled = false;
        //fileupDamBaoChatLuong.Enabled = false;
        fileupKetQuaDoKiem.Enabled = false;
        //fileupQuyTrinhSanXuat.Enabled = false;
        fileupTaiLieuKhac.Enabled = false;
        fileupTaiLieuKyThuat.Enabled = false;
        fileupTuCachPhapNhan.Enabled = false;
        link1.Style.Add("display", "none");
        link2.Style.Add("display", "none");
    }
    /// <summary>
    /// Hiển thị các file chờ upload lên màn hình
    /// </summary>
    public void ShowFilesWaitForUpLoad()
    {
        if (Session["ListFilesUpload"] != null)
        {
            ListFiles = (List<DictionaryEntry>)Session["ListFilesUpload"];
            ShowFileWaitForUpLoad("fileupBanCongBo", ref lblBanCongBo);
            ShowFileWaitForUpLoad("fileupBanTuDanhGia", ref lblTuDanhGia);
            //ShowFileWaitForUpLoad("fileupChungChi", ref lblChungChiHeThong);
            //ShowFileWaitForUpLoad("fileupDamBaoChatLuong", ref lblQuyTrinhDamBao);
            ShowFileWaitForUpLoad("fileupKetQuaDoKiem", ref lblKetQuaDoKiem);
            //ShowFileWaitForUpLoad("fileupQuyTrinhSanXuat", ref lblQuyTrinhSanXuat);
            ShowFileWaitForUpLoad("fileupTaiLieuKhac", ref lblTaiLieuKhac);
            ShowFileWaitForUpLoad("fileupTaiLieuKyThuat", ref lblTaiLieuKyThuat);
            ShowFileWaitForUpLoad("fileupTuCachPhapNhan", ref lblTuCachPhapNhan);
        }
    }
    /// <summary>
    /// Hiển thị file chờ up load lên màn hình
    /// </summary>
    /// <param name="funame"></param>
    /// <param name="lb"></param>
    public void ShowFileWaitForUpLoad(string funame, ref Label lb)
    {
        //FileUpload fu = new FileUpload();
        //if (Session[funame] != null)
        //{
        //    fu = (FileUpload)Session[funame];
        //    lb.Text = fu.FileName;
        //    lb.Visible = true;
        //    //lnk.Visible = false;
        //}
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
    /// lấy và lưu các file chờ  upload lên
    /// </summary>
    public void GetFilesWaitForUpLoad()
    {
        GetFileWaitForUpLoad("fileupBanCongBo", ref fileupBanCongBo);
        GetFileWaitForUpLoad("fileupBanTuDanhGia", ref fileupBanTuDanhGia);
        //GetFileWaitForUpLoad("fileupChungChi", ref fileupChungChi);
        //GetFileWaitForUpLoad("fileupDamBaoChatLuong", ref fileupDamBaoChatLuong);
        GetFileWaitForUpLoad("fileupKetQuaDoKiem", ref fileupKetQuaDoKiem);
        //GetFileWaitForUpLoad("fileupQuyTrinhSanXuat", ref fileupQuyTrinhSanXuat);
        GetFileWaitForUpLoad("fileupTaiLieuKhac", ref fileupTaiLieuKhac);
        GetFileWaitForUpLoad("fileupTaiLieuKyThuat", ref fileupTaiLieuKyThuat);
        GetFileWaitForUpLoad("fileupTuCachPhapNhan", ref fileupTuCachPhapNhan);


    }
    /// <summary>
    /// lấy thông tin về file chờ upload và lưu tạm file lên server và thông tin file lên session
    /// </summary>
    /// <param name="funame"></param>
    /// <param name="fu"></param>
    public void GetFileWaitForUpLoad(string funame, ref FileUpload fu)
    {
        //if (fu.HasFile)
        //    Session[funame] = fu;
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
    /// Gán các thông tin về file chờ upload trên session vào danh sách ListFiles
    /// </summary>
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
    public void SetBackFileWaitForUpload(string funame, ref FileUpload fu)
    {
        if (Session[funame] != null)
            fu = (FileUpload)Session[funame];
    }
    /// <summary>
    /// Reset các session lưu thông tin về file chờ upload
    /// </summary>
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
    /// Khởi tạo danh sách lưu thông tin file chờ upload
    /// </summary>
    public void InitListFile()
    {
        DictionaryEntry dic = new DictionaryEntry("fileupBanCongBo", null);
        ListFiles.Add(dic);
        dic = new DictionaryEntry("fileupBanTuDanhGia", null);
        ListFiles.Add(dic);
        //dic = new DictionaryEntry("fileupChungChi", null);
        //ListFiles.Add(dic);
        //dic = new DictionaryEntry("fileupDamBaoChatLuong", null);
        //ListFiles.Add(dic);
        dic = new DictionaryEntry("fileupKetQuaDoKiem", null);
        ListFiles.Add(dic);
        //dic = new DictionaryEntry("fileupQuyTrinhSanXuat", null);
        //ListFiles.Add(dic);
        dic = new DictionaryEntry("fileupTaiLieuKhac", null);
        ListFiles.Add(dic);
        dic = new DictionaryEntry("fileupTaiLieuKyThuat", null);
        ListFiles.Add(dic);
        dic = new DictionaryEntry("fileupTuCachPhapNhan", null);
        ListFiles.Add(dic);

    }
    /// <summary>
    /// Gán thông tin về file chờ upload vào danh sách lưu
    /// </summary>
    /// <param name="funame"></param>
    /// <param name="value"></param>
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
    /// Khởi tạo các webpath cho trang
    /// </summary>
    public void CreateWebPath()
    {
        //thiết lập webpath cho trang
        if (Direct.Length > 0)
        {
            StringBuilder sb = new StringBuilder();
            //nếu trang chuyển tới là trang quản lý sản phẩm của hồ sơ
            if (Direct == "cb_hososanpham" && Action != "view")
            {
                sb.Append("<a href='../WebUI/CN_HoSo_QuanLy.aspx?UserControl=CB_HoSoDen'> DANH SÁCH HỒ SƠ MỚI</a>&gt;&gt;");
                sb.Append(" <a href='../WebUI/cb_hososanpham.aspx?direct=cb_hosoden&HosoId=" + HoSoId + "'>");
                sb.Append("DANH SÁCH SẢN PHẨM</a> >>");
            }
            //nếu trang chuyển tới là trang quản lý sản phẩm của hồ sơ với mục đích chỉ xem thông tin
            else if (Direct == "cb_hososanpham" && Action == "view")
            {
                sb.Append(@"<a href='../WebUI/CN_HoSo_QuanLy.aspx?UserControl=CB_HoSoDi'> DANH SÁCH HỒ SƠ CÔNG BỐ ĐÃ GỬI </a>&gt;&gt;");
                sb.Append(" <a href='../WebUI/cb_hososanpham.aspx?direct=cb_hosodi&action=view&HosoId=" + HoSoId + "'>");
                sb.Append("DANH SÁCH SẢN PHẨM</a> >>");
            }
            // nếu trang chuyển tới là trang xylyhoso_danhgia
            else if (Direct == "cb_xulyhoso_danhgia")
            {
                sb.Append(" <a href='../WebUI/cb_xulyhoso_danhgia.aspx?HoSoID=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "'>");
                sb.Append("DANH SÁCH SẢN PHẨM</a> >>");
            }
            // nếu trang chuyển tới là trang cb_thamdinhhoso
            else if (Direct == "cb_thamdinhhoso")
            {
                sb.Append(" <a href='../WebUI/cb_thamdinhhoso.aspx?HoSoID=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "'>");
                sb.Append("THÔNG TIN HỒ SƠ THẨM ĐỊNH</a> >>");
            }
            // nếu trang chuyển tới là trang tra cứu thông tin hồ sơ
            else if (Direct == "cb_tracuuhoso_hososanpham")
            {
                sb.Append("<a href='CB_TraCuuPHTC.aspx'>TRA CỨU HỒ SƠ CÔNG BỐ</a>&gt;&gt;");
                sb.Append(" <a href='../WebUI/CB_HoSoSanPham.aspx?direct=cb_trahoso&action=view&HosoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "'>");
                sb.Append("DANH SÁCH SẢN PHẨM</a> >>");
            }

            lblDsSanPham.Text = sb.ToString();
        }

        // thiết lập hướng tới cho nút bỏ qua
        if (Direct == "cb_hososanpham" && Action != "view")
            btnBoQua.Attributes.Add("onclick", "javascript:window.location='cb_hososanpham.aspx?direct=CB_HoSoDen&HosoId=" + HoSoId + "'; return false;");
        else if (Direct == "cb_hososanpham" && Action == "view")
            btnBoQua.Attributes.Add("onclick", "javascript:window.location='cb_hososanpham.aspx?direct=CB_HoSoDI&action=view&HosoId=" + HoSoId + "'; return false;");
        else if (Direct == "cb_thamdinhhoso")
            btnBoQua.Attributes.Add("onclick", "javascript:window.location='CB_ThamDinhHoSo.aspx?action=" + Request["parentAction"].ToString() + "&HoSoID=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "&UserControl=cb_hosoden&direct=" + Request["UserControl"].ToString() + "'; return false;");
        else if (Direct == "cb_pheduyethoso")
            btnBoQua.Attributes.Add("onclick", "javascript:window.location='CB_PheDuyetHoSo.aspx?HoSoID=" + HoSoId + "&UserControl=cb_hosoden'; return false;");
        else if (Direct == "cb_xulyhoso_danhgia")
            btnBoQua.Attributes.Add("onclick", "javascript:window.location='cb_xulyhoso_danhgia.aspx?direct=CB_HoSoDen&HoSoID=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "&UserControl=CB_HoSoDen'; return false;");


    }

    #endregion
    //LongHH Nut Clone Sản phẩm: thay vì, edit sản phẩm hiện tại, sẽ thêm sản phẩm mới.
    protected void btnCloneSanPham_Click(object sender, EventArgs e)
    {
        string dsMaTieuChuan = string.Empty;
        string dsTenTieuChuan = string.Empty;

        GetFilesWaitForUpLoad(); //lấy thông tin về các file mà người dùng đã chọn chờ upload             
        string ImgUrl = hidMauDau.Value;//lấy đường dẫn file ảnh mẫu dấu
        string MauDauId = string.Empty;
        imgMauDau.ImageUrl = ImgUrl;

        
        //kiểm tra kích thước file upload
        bool ck = CheckFileUploadsSize();
        if (!ck)//nếu sai
        {

            SetBackFilesWaitForUpload();//
            ShowFilesWaitForUpLoad();

            return;

        }
        //kiểm tra kiểu file upload lên
        ck = CheckFilesType();
        if (!ck)
        {

            SetBackFilesWaitForUpload();
            ShowFilesWaitForUpLoad();
            return;
        }
        ShowFilesWaitForUpLoad();

        if (ImgUrl.Contains("MauDauId"))
        {
            MauDauId = ImgUrl.Substring(ImgUrl.IndexOf("MauDauId") + 9, ImgUrl.Length - ImgUrl.IndexOf("MauDauId") - 9);
        }
        //else
        //{
        //    Thong_bao("Chọn mẫu dấu PHTC!");
        //    return;
        //}

        ////kiêm tra số gcn
        //if (rdDaCapChungNhan.Checked)
        //{           
        //   CheckSoGCN();
        //   //return;
        //}

        //kiểm tra có tiêu chuẩn được chọn hay ko?
        ck = CheckListTieuChuan();
        if (!ck)
        {

            return;
        }

        if (Action == "edit")//Clone sản phẩm
        {
            //Them moi san pham clone vao ho so
            string hsId = string.Empty;
            if (Request["HoSoId"] != null)
                hsId = Request["HoSoId"].ToString();
            // Sản phẩm clone
            SanPham ObSanPham = new SanPham();

            if (rdTuDanhGia.Checked)
            {
                ObSanPham.HinhThucId = (int)EnHinhThucList.TU_DANH_GIA;
                ObSanPham.SoGcn = string.Empty;
                ObSanPham.SoBanTuDanhGia = txtSoBanTuDanhGia.Text.Trim();
                if (txtNgayDanhGia.Text.Length > 1)
                    ObSanPham.NgayDanhGia = Convert.ToDateTime(txtNgayDanhGia.Text);
            }
            else
            {
                ObSanPham.HinhThucId = (int)EnHinhThucList.DA_CAP_PHTC;
                ObSanPham.SoGcn = txtSoGCN.Text;
                ObSanPham.SoBanTuDanhGia = string.Empty;
                ObSanPham.NgayDanhGia = null;
            }


            if (txtNgayCongBo.Text.Length > 0)
                ObSanPham.NgayCongBo = Convert.ToDateTime(txtNgayCongBo.Text);
            else
                ObSanPham.NgayCongBo = null;

            ObSanPham.HoSoId = hsId;
            ObSanPham.SanPhamId = ddlTenSanPham.SelectedValue;
            ObSanPham.KyHieu = txtKyHieuSanPham.Text.Trim();
            ObSanPham.NhomSanPhamId = NhomSanPhamId;
            ObSanPham.HangSanXuatId = ddlHangSanXuat.SelectedValue;
            ObSanPham.TrangThaiId = (int)EnTrangThaiSanPhamList.CHO_XU_LY;
            ObSanPham.NgayCapNhatSauCung = DateTime.Now;
            ObSanPham.MauDauId = MauDauId;
            ObSanPham.SoBanCongBo = txtSoBanCongBo.Text.Trim();
            if (ObSanPham.IsValid)
            {
                TransactionManager transaction = ProviderFactory.Transaction;
                try
                {
                    ProviderFactory.SanPhamProvider.Insert(transaction, ObSanPham);
                    //int stt = ObSanPham.Stt;
                    //DataTable db = ProviderFactory.SanPhamProvider.GetSanPhamID_Extend(stt);
                    //DataRow row = db.Rows[0];
                    string id = ObSanPham.Id;
                    //thêm mới tiêu chuẩn cho sanpham vào bảng SanPham_TieuChuanApDung
                    UpdateTieuChuanForSanPham(id, transaction, ref dsMaTieuChuan, ref dsTenTieuChuan);

                    //chèn nội dung xử lý và ghi chú vào bảng QuaTrinhXuLy
                    UpdateQuatrinhXuLySanPham(id, transaction);

                    ObSanPham.DsTenTieuChuan = dsTenTieuChuan;
                    ObSanPham.DsMaTieuChuan = dsMaTieuChuan;

                    ProviderFactory.SanPhamProvider.Update(transaction, ObSanPham);
                    transaction.Commit();
                    transaction.Dispose();

                    //Chèn tài liệu đính kèm cho sanpham vào bảng tailieudinhkem
                    UpdateTaiLieuDinhKemForSanPham(id);
                    string ThongBao = Resources.Resource.msgThemmoiSanPham;

                    //ghi vào nhật ký chương trình                  
                    string strLogString = "Thêm mới Sản phẩm CLONE công bố có mã là: " + id;
                    ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.CB_SAN_PHAM_THEM_MOI, strLogString);
                    //xóa bỏ thông tin về mẫu dấu trên session
                    Session["MauDau"] = string.Empty;
                    //thực hiện chuyển tới trang quản lý sản phẩm của hồ sơ
                    if (Direct == "cb_hososanpham")
                        Thong_bao(this.Page, ThongBao, "CB_HoSoSanPham.aspx?direct=CB_HoSoMo&HosoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId);
                    else if (Direct == "cb_hososanpham_quanly")
                        Thong_bao(this.Page, ThongBao, "cb_hososanpham_quanly.aspx?direct=CB_HoSoDen&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "&UserControl=CB_HoSoDen");
                    else if (Direct == "cb_xulyhoso_danhgia")
                        Thong_bao(this.Page, ThongBao, "cb_xulyhoso_danhgia.aspx?direct=CB_HoSoDen&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "&UserControl=CB_HoSoDen");

                }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                    throw ex;
                }


            }


        }
        //reset lai các thông tin về file chờ upload trên session
        ClearFileSession();
    }

    protected void btnBoQua_Click(object sender, EventArgs e)
    {
        //thực hiện chuyển tới trang quản lý sản phẩm của hồ sơ
        if (Direct == "cb_hososanpham")
            Response.Redirect("CB_HoSoSanPham.aspx?direct=CB_HoSoMo&HosoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "&UserControl=CB_HoSoDen");
        else if (Direct == "cb_hososanpham_quanly")
            Response.Redirect("cb_hososanpham_quanly.aspx?direct=CB_HoSoDen&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "&UserControl=CB_HoSoDen");
        else if (Direct == "cb_xulyhoso_danhgia")
        {
            if (Request["PostBack"] != null)
            {
                Response.Redirect("CB_XuLyHoSo_DanhGia.aspx?PostBack=1&direct=CB_HoSoDen&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "&UserControl=CB_HoSoDen");
            }
            else
                Response.Redirect("CB_XuLyHoSo_DanhGia.aspx?direct=CB_HoSoDen&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "&UserControl=CB_HoSoDen");
        }
        if (Direct.ToLower() == "cb_tracuuhoso_hososanpham")
            Response.Redirect("CB_HoSoSanPham.aspx?direct=CB_TraHoSo&action=view&HosoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "&UserControl=CB_HoSoDen");
    }

    #region "AutoComplete"


    /// <summary>
    /// Lấy danh sách GCN đã có trong CSDL
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// Author      Date        comment
    /// TuanVM      08/09/2009  Tạo mới
    private string LayDanhSachGCN()
    {
        HoSoId = Request["HoSoId"].ToString();
        DonViId = ProviderFactory.HoSoProvider.GetById(HoSoId).DonViId;

        DataTable dtSanPhams = ProviderFactory.SanPhamProvider.GetDanhSachGCN_ByDonVi(DonViId);
        TList<SanPham> lstSanPhamExist = ProviderFactory.SanPhamProvider.GetByHoSoId(HoSoId);
        string strSoGCN = string.Empty;
        int count = 0;
        bool NotExist = true;
        foreach (DataRow row in dtSanPhams.Rows)
        {
            NotExist = true;
            // Nếu Sản phẩm với SoGCN nhập vào đã có trong hồ sơ thì không đưa SoGCN và list
            foreach (SanPham objSP in lstSanPhamExist)
            {
                if (objSP.SoGcn == row["SoGCN"].ToString())
                {
                    NotExist = false;
                    break;
                }
            }
            if (NotExist)
            {
                strSoGCN += "'" + row["SoGCN"].ToString() + "', ";
                count++;
            }
        }
        if (count > 0)
            strSoGCN = strSoGCN.Substring(0, strSoGCN.Length - 2);
        return strSoGCN;

    }

    /// <summary>
    /// Thực hiện postback khi người dùng chọn số GCN từ CSDL
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkPostBack_Click(object sender, EventArgs e)
    {
    }
    #endregion

    /// <summary>
    /// In phiếu đánh giá khi tra cứu
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

        ClientScript.RegisterClientScriptBlock(this.GetType(), "CB_XuLyHoSo_DanhGia_BanTiepNhan",
                "<script>popCenter('../ReportForm/DieuKienInBaoCao.aspx?HoSoID=" + strHoSoId + "&SanPhamId=" + strSanPhamId + "&LoaiBaoCao=CBPhieuDanhGia" + "','CBPhieuDanhGia',450,300);</script>");
    }
   
}

