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
using System.Collections.Generic;
using Resources;
using CucQLCL.Common;
using Cuc_QLCL.Data;


public partial class WebUI_NhapLieu_CB_HoSoChitiet : PageBase
{
    private string MaHoSo = "";
    private string TrangThaiID = "";
    private string UserControl = "";

    HoSo objHS = null;
    string passedArgument = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        MaHoSo = Server.UrlDecode(Request.QueryString["id"]);

        if (!IsPostBack)
        {
            Session["HoSoID"] = null;
            LoadDonVi();
            LoadDanhSachChuyenVien();
            BindEnums();
            LoadDefaulComponents();
            lbtTaoMoiDV.Visible = mUserInfo.IsInRole(EnPermission.QUANLY_DM_DONVI);
            /////// load thong tin don vi///////////
            DmDonVi objDonVi = new DmDonVi();
            objDonVi = ProviderFactory.DmDonViProvider.GetById(ddlDonVi.SelectedValue.ToString());
            if (objDonVi != null)
            {
                txtDienThoai.Text = objDonVi.DienThoai.ToString();
                txtEmail.Text = objDonVi.Email.ToString();
            }
        }
        else
        {
            if (this.Request["__EVENTTARGET"] == "AddNewCommit")
            {
                passedArgument = Request.Params.Get("__EVENTARGUMENT");
                LoadDonVi();
            }
        }
        if (MaHoSo == null)
            MaHoSo = Session["HoSoID"] != null ? Session["HoSoID"].ToString() : string.Empty;

        EditHoSo(MaHoSo);
    }

    /// <summary>
    /// Load các giá trị default
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 9/5/2009   
    /// TuanVM                  10/06                   Sửa checkbox Hồ sơ mới
    /// </Modified>
    private void LoadDefaulComponents()
    {
        txtLuuY.Attributes.Add("onkeyup", " if (!checkLength('" + txtLuuY.ClientID + "', '4000')) return false;");
        txtTaiLieuKhac.Attributes.Add("onkeyup", " if (!checkLength('" + txtTaiLieuKhac.ClientID + "', '4000')) return false;");
        txtSanPham.Attributes.Add("onkeyup", " if (!checkLength('" + txtSanPham.ClientID + "', '4000')) return false;");
        txtKyHieu.Attributes.Add("onkeyup", " if (!checkLength('" + txtKyHieu.ClientID + "', '2000')) return false;");
    }

    /// <summary>
    /// Lấy tất cả danh sách đơn vị
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 8/5/2009              
    /// </Modified>
    private void LoadDonVi()
    {
        DataTable dtDonVi = ProviderFactory.DmDonViProvider.GetByTinhThanhThuocTrungTam();
        ddlDonVi.DataSource = dtDonVi;
        ddlDonVi.DataValueField = "ID";
        ddlDonVi.DataTextField = "TenTiengViet";
        ddlDonVi.DataBind();
        if (passedArgument != "" && passedArgument != null)
        {
            ddlDonVi.SelectedValue = passedArgument;
        }
    }

    private void LoadDanhSachChuyenVien()
    {
        DataTable dtChuyenVien = ProviderFactory.SysUserProvider.GetByTrungTamID(mUserInfo.TrungTam.Id);

        // Nguoi tiep nhan
        cboNguoiTiepNhan.DataSource = dtChuyenVien;
        cboNguoiTiepNhan.DataTextField = "FullName";
        cboNguoiTiepNhan.DataValueField = "ID";
        cboNguoiTiepNhan.DataBind();

        // Nguoi xu ly
        cboNguoiXuLy.DataSource = dtChuyenVien;
        cboNguoiXuLy.DataTextField = "FullName";
        cboNguoiXuLy.DataValueField = "ID";
        cboNguoiXuLy.DataBind();

        // Nguoi tham dinh
        cboNguoiThamDinh.DataSource = dtChuyenVien;
        cboNguoiThamDinh.DataTextField = "FullName";
        cboNguoiThamDinh.DataValueField = "ID";
        cboNguoiThamDinh.DataBind();


    }
    /// <summary>
    /// Gán các enum
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 8/5/2009              
    /// </Modified>
    private void BindEnums()
    {
        //gán dữ liệu cho RadioButtonList Nhận từ
        ListItem BUU_DIEN = new ListItem();
        BUU_DIEN.Text = EntityHelper.GetEnumTextValue(EnNhanHoSoTuList.BUU_DIEN);
        BUU_DIEN.Value = ((int)EnNhanHoSoTuList.BUU_DIEN).ToString();

        ListItem TRUC_TIEP = new ListItem();
        TRUC_TIEP.Text = EntityHelper.GetEnumTextValue(EnNhanHoSoTuList.TRUC_TIEP);
        TRUC_TIEP.Value = ((int)EnNhanHoSoTuList.TRUC_TIEP).ToString();
        rdgNhanTu.Items.Add(BUU_DIEN);
        rdgNhanTu.Items.Add(TRUC_TIEP);
        rdgNhanTu.SelectedValue = ((int)EnNhanHoSoTuList.TRUC_TIEP).ToString();
        //gán dữ liệu cho RadioButtonList Nguồn gốc
        ListItem NHAP_KHAU = new ListItem();
        NHAP_KHAU.Text = EntityHelper.GetEnumTextValue(EnNguonGocList.NHAP_KHAU);
        NHAP_KHAU.Value = ((int)EnNguonGocList.NHAP_KHAU).ToString();

        ListItem SX_TRONG_NUOC = new ListItem();
        SX_TRONG_NUOC.Text = EntityHelper.GetEnumTextValue(EnNguonGocList.SX_TRONG_NUOC);
        SX_TRONG_NUOC.Value = ((int)EnNguonGocList.SX_TRONG_NUOC).ToString();

        rdgNguonGoc.Items.Add(NHAP_KHAU);
        rdgNguonGoc.Items.Add(SX_TRONG_NUOC);
    }

    /// <summary>
    /// Chỉnh sửa hồ sơ
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 8/5/2009      
    /// DucLv                  27/5/2009               them phan check day du hoac khong day du
    /// </Modified>
    protected void EditHoSo(string MaHoSo)
    {
        objHS = new HoSo();
        if ((MaHoSo != "") && (MaHoSo != null))
        {
            btnCapNhat.Text = "Cập nhật";
            objHS = ProviderFactory.HoSoProvider.GetById(MaHoSo);

            // Hiển thị nút ho so chi tiet
            if (objHS.NguonGocId != (int)EnNguonGocList.NK_KEM_KQ_DO_KIEM && objHS.LoaiHoSo == (int)LoaiHoSo.ChungNhanHopChuan)
                btnHoSoChiTiet.Visible = true;

            Session["HoSoID"] = MaHoSo;
            if (!IsPostBack)
            {
                //gán dữ liệu cho các control
                txtSoHoSo.Text = objHS.SoHoSo;
                //lay nguoi tiep nhan theo ID
                cboNguoiTiepNhan.SelectedValue = objHS.NguoiTiepNhanId;

                if (objHS.NgayTiepNhan != null)
                {
                    txtNgayNhan.Text = ((DateTime)objHS.NgayTiepNhan).ToShortDateString();
                }
                txtNguoiNopHoSo.Text = objHS.NguoiNopHoSo;

                txtSanPham.Text = objHS.DanhSachSanPham;
                txtKyHieu.Text = objHS.DanhSachKyHieuSanPham;

                //gán thông tin checkbox
                chkTL_BanCongBo.Checked = Convert.ToBoolean(objHS.BanCongBo);
                chkTl_BanSaoGiayChungNhanHQ.Checked = Convert.ToBoolean(objHS.BanSaoGiayChungNhanHopQuy);
                chkTl_MauDau.Checked = Convert.ToBoolean(objHS.MauDauHopQuy);
                chkTl_BanTuDanhGia.Checked = Convert.ToBoolean(objHS.BanTuDanhGia);
                chkTl_KetQuaDoKiem.Checked = Convert.ToBoolean(objHS.KetQuaDoKiem);
                chkTL_TaiLieuKT.Checked = Convert.ToBoolean(objHS.TaiLieuKyThuat);
                chkTL_TuCachPN.Checked = Convert.ToBoolean(objHS.GiayToTuCachPhapNhan);

                txtTaiLieuKhac.Text = objHS.TaiLieuKhac;
                txtDienThoai.Text = objHS.DienThoai;
                txtEmail.Text = objHS.Email;
                txtLuuY.Text = objHS.Luuy;

                //chọn lại nhận hồ sơ từ
                foreach (ListItem item in rdgNhanTu.Items)
                {
                    if (item.Value == objHS.NhanHoSoTuId.ToString())
                    {
                        rdgNhanTu.SelectedValue = objHS.NhanHoSoTuId.ToString();
                    }
                }

                //chọn lại nguồn gốc
                foreach (ListItem item in rdgNguonGoc.Items)
                {
                    if (item.Value == objHS.NguonGocId.ToString())
                    {
                        rdgNguonGoc.SelectedValue = objHS.NguonGocId.ToString();
                    }
                }
                //chọn lai đơn vị
                foreach (ListItem item in ddlDonVi.Items)
                {
                    if (item.Value == objHS.DonViId)
                    {
                        ddlDonVi.SelectedValue = objHS.DonViId;
                    }
                }

                //hiển thị nút hồ sơ chi tiết khi hồ sơ Id != "" và loại hình chứng nhận là hợp chuẩn
                if (!objHS.Id.Equals("") && (objHS.LoaiHoSo == 3))
                {
                    btnHoSoChiTiet.Visible = true;
                }

                // Hien thi thong tin phan cong xu ly, tham dinh
                TList<PhanCong> lstPhanCong = ProviderFactory.PhanCongProvider.GetByHoSoId(objHS.Id);
                if (lstPhanCong.Count > 0)
                {
                    PhanCong objPhanCong = lstPhanCong[0];
                    if (objPhanCong != null)
                    {
                        cboNguoiXuLy.SelectedValue = objPhanCong.NguoiXuLy;
                        cboNguoiThamDinh.SelectedValue = objPhanCong.NguoiThamDinh;
                    }
                }
            }
        }

    }

    /// <summary>
    /// Cập nhật hồ sơ vào CSDL
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 9/5/2009     
    /// TuanVM                  10/06/2009      Sửa (Lỗi cập nhật lại người tiếp nhận thành người xử khi người xử lý sửa trạng thái hồ sơ + bổ sung trường ký hiệu sản phẩm)
    /// </Modified>
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        if (CheckTrungSoHoSo(txtSoHoSo.Text.Trim()))
        {
            objHS = ProviderFactory.HoSoProvider.GetById(MaHoSo);
            if (objHS == null)
            {
                objHS = new HoSo();
                objHS.TrangThaiId = (int)EnTrangThaiHoSoList.DA_DONG;
                objHS.HoSoMoi = false;
            }

            objHS.NguoiTiepNhanId = cboNguoiTiepNhan.SelectedValue;
            SinhSoHoSo(objHS);
            objHS.LoaiHoSo = (int)LoaiHoSo.CongBoHopQuy;
            string ThongBao = "";
            if ((MaHoSo != "") && (MaHoSo != null))
            {
                ThongBao = Resource.msgCapNhatHoSo;
            }
            else
            {
                ThongBao = Resource.msgTaoMoiHoSo;
            }
            //gán dữ liệu cho object cần insert

            objHS.DaDoc = mUserInfo.UserID;
            if (txtNgayNhan.Text != null && txtNgayNhan.Text != "")
            {
                objHS.NgayTiepNhan = Convert.ToDateTime(txtNgayNhan.Text.Trim());
            }
            objHS.NguoiNopHoSo = txtNguoiNopHoSo.Text.Trim();
            objHS.DanhSachSanPham = txtSanPham.Text.Trim();
            objHS.DanhSachKyHieuSanPham = txtKyHieu.Text.Trim();

            objHS.DonViId = ddlDonVi.SelectedValue;
            //
            objHS.BanCongBo = chkTL_BanCongBo.Checked;
            objHS.BanSaoGiayChungNhanHopQuy = chkTl_BanSaoGiayChungNhanHQ.Checked;
            objHS.MauDauHopQuy = chkTl_MauDau.Checked;
            objHS.BanTuDanhGia = chkTl_BanTuDanhGia.Checked;
            objHS.KetQuaDoKiem = chkTl_KetQuaDoKiem.Checked;
            objHS.TaiLieuKyThuat = chkTL_TaiLieuKT.Checked;
            objHS.GiayToTuCachPhapNhan = chkTL_TuCachPN.Checked;
            if (rdgNhanTu.SelectedValue != null && rdgNhanTu.SelectedValue != "")
            {
                objHS.NhanHoSoTuId = Convert.ToInt16(rdgNhanTu.SelectedValue);
            }

            if (rdgNguonGoc.SelectedValue != null && rdgNguonGoc.SelectedValue != "")
            {
                objHS.NguonGocId = Convert.ToInt16(rdgNguonGoc.SelectedValue);
            }

            objHS.TaiLieuKhac = txtTaiLieuKhac.Text;
            objHS.DienThoai = txtDienThoai.Text;
            objHS.Email = txtEmail.Text;
            objHS.Luuy = txtLuuY.Text;
            objHS.TrungTamId = mUserInfo.TrungTam.Id;
            TransactionManager transaction = ProviderFactory.Transaction;
            // Insert, update thong tin
            try
            {
                // Cap nhat thong tin ho so
                ProviderFactory.HoSoProvider.Save(transaction, objHS);

                // Cap nhat thong tin phan cong xu ly     
                TList<PhanCong> lstPhanCong = ProviderFactory.PhanCongProvider.GetByHoSoId(transaction, objHS.Id);
                if (lstPhanCong.Count > 0)
                {
                    // Update
                    PhanCong objPhanCong = lstPhanCong[0];
                    objPhanCong.HoSoId = objHS.Id;
                    objPhanCong.NguoiXuLy = cboNguoiXuLy.SelectedValue;
                    objPhanCong.NguoiThamDinh = cboNguoiThamDinh.SelectedValue;
                    objPhanCong.NguoiPhanCong = mUserInfo.TrungTam.GiamDocId;
                    objPhanCong.NgayPhanCong = objHS.NgayTiepNhan.Value;
                    ProviderFactory.PhanCongProvider.Update(transaction, objPhanCong);
                }
                else
                {
                    // Insert
                    PhanCong objPhanCong = new PhanCong();
                    objPhanCong.HoSoId = objHS.Id;
                    objPhanCong.NguoiXuLy = cboNguoiXuLy.SelectedValue;
                    objPhanCong.NguoiThamDinh = cboNguoiThamDinh.SelectedValue;
                    objPhanCong.NguoiPhanCong = mUserInfo.TrungTam.GiamDocId;
                    objPhanCong.NgayPhanCong = objHS.NgayTiepNhan.Value;
                    ProviderFactory.PhanCongProvider.Save(transaction, objPhanCong);
                }
                //ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                //       "<script>alert('" + ThongBao + "'); </script>");
                Thong_bao(this.Page, ThongBao, "NhapLieu_CB_HoSoQuanLy.aspx");
                // Copy du lieu danh muc vao ho so
                SaoChepThongTinTuDanhMuc(objHS, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            //hiển thị nút hồ sơ chi tiết khi hồ sơ Id != "" và loại hình chứng nhận là hợp chuẩn
            if (!objHS.Id.Equals("") && (objHS.LoaiHoSo == 3))
            {
                btnHoSoChiTiet.Visible = true;
            }

            if (objHS.Id != string.Empty)
            {
                btnCapNhat.Text = "Cập nhật";
                //btnCapNhat.Enabled = false;
                hidHoSoID.Value = objHS.Id;
            }

            if (objHS.NguonGocId != (int)EnNguonGocList.NK_KEM_KQ_DO_KIEM && objHS.LoaiHoSo == (int)LoaiHoSo.ChungNhanHopChuan)
            {
                btnHoSoChiTiet.Visible = true;
            }
            else
                btnHoSoChiTiet.Visible = false;
        }
    }

    /// <summary>
    /// Sao chep du lieu danh muc vao ho so
    /// </summary>
    private void SaoChepThongTinTuDanhMuc(HoSo objHoSo, TransactionManager transaction)
    {
        //thêm thông tin hồ sơ+sản phẩm(có tên người ký và chức vụ)+sản phẩm tiêu chuẩn áp dụng
        string strDmDonViId = objHoSo.DonViId;
        string strDmTrungTamId = mUserInfo.TrungTam.Id;
        DmDonVi objDonVi = ProviderFactory.DmDonViProvider.GetById(transaction, strDmDonViId);
        DmTrungTam objTrungTam = ProviderFactory.DmTrungTamProvider.GetById(transaction, strDmTrungTamId);
        objHoSo.DmDonViDiaChi = objDonVi.DiaChi;
        objHoSo.DmDonViDienThoai = objDonVi.DienThoai;
        objHoSo.DmDonViEmail = objDonVi.Email;
        objHoSo.DmDonViFax = objDonVi.Fax;
        objHoSo.DmDonViMaDonVi = objDonVi.MaDonVi;
        objHoSo.DmDonViTenTiengAnh = objDonVi.TenTiengAnh;
        objHoSo.DmDonViTenTiengViet = objDonVi.TenTiengViet;
        objHoSo.DmDonViTenVietTat = objDonVi.TenVietTat;
        objHoSo.DmDonViTinhThanhId = objDonVi.TinhThanhId;

        objHoSo.DmTrungTamDiaChi = objTrungTam.DiaChi;
        objHoSo.DmTrungTamDiaChiCoQuanThuHuong = objTrungTam.DiaChiCoQuanThuHuong;
        objHoSo.DmTrungTamDiaChiCoQuanThuHuongCuc = objTrungTam.DiaChiCoQuanThuHuongCuc;
        objHoSo.DmTrungTamDienThoai = objTrungTam.DienThoai;
        objHoSo.DmTrungTamEmail = objTrungTam.Email;
        objHoSo.DmTrungTamFax = objTrungTam.Fax;
        objHoSo.DmTrungTamGiamDocId = objTrungTam.GiamDocId;
        objHoSo.DmTrungTamMaTrongGcn = objTrungTam.MaTrongGcn;
        objHoSo.DmTrungTamMaTrungTam = objTrungTam.MaTrungTam;
        objHoSo.DmTrungTamSoTaiKhoan = objTrungTam.SoTaiKhoan;
        objHoSo.DmTrungTamSoTaiKhoanCuc = objTrungTam.SoTaiKhoanCuc;
        objHoSo.DmTrungTamTenCoQuanThuHuong = objTrungTam.TenCoQuanThuHuong;
        objHoSo.DmTrungTamTenCoQuanThuHuongCuc = objTrungTam.TenCoQuanThuHuongCuc;
        objHoSo.DmTrungTamTenKhoBac = objTrungTam.TenKhoBac;
        objHoSo.DmTrungTamTenKhoBacCuc = objTrungTam.TenKhoBacCuc;
        objHoSo.DmTrungTamTenTiengAnh = objTrungTam.TenTiengAnh;
        objHoSo.DmTrungTamTenTrungTam = objTrungTam.TenTrungTam;
        objHoSo.DmTrungTamTinhThanhId = objTrungTam.TinhThanhId;

        ProviderFactory.HoSoProvider.Save(transaction, objHoSo);
    }

    /// <summary>
    /// Hàm sinh số hồ sơ theo số công văn do người dùng nhập vào
    /// </summary>
    /// <returns></returns>
    private void SinhSoHoSo(HoSo objHoSo)
    {
        string SoCongVan = txtSoHoSo.Text;
        string Stt = txtSoHoSo.Text;
        string MaTrungTam = mUserInfo.MaTrungTam;
        DateTime NgayTiepNhan = Convert.ToDateTime(txtNgayNhan.Text);
        if (!SoCongVan.Contains("/"))
        {
            if (Stt.Length == 1)
                SoCongVan = "000" + Stt;
            else if (Stt.Length == 2)
                SoCongVan = "00" + Stt;
            else if (Stt.Length == 3)
                SoCongVan = "0" + Stt;
            else if (Stt.Length >= 4)
                SoCongVan = Stt.Substring(0, 4);

            objHoSo.SoCongVanDen = SoCongVan;

            objHoSo.SoHoSo = SoCongVan + "/" + NgayTiepNhan.Year.ToString() + "/CBHQ-" + MaTrungTam;
            txtSoHoSo.Text = objHoSo.SoHoSo;
        }
        else
            objHoSo.SoHoSo = txtSoHoSo.Text;
    }

    /// <summary>
    /// Dừng việc chỉnh sửa
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 9/5/2009              
    /// </Modified>
    protected void btnBoQua_Click(object sender, EventArgs e)
    {
        Response.Redirect("NhapLieu_CB_HoSoQuanLy.aspx");
    }

    /// <summary>
    /// Kiểm tra trùng số hồ sơ
    /// </summary>
    /// <param name="MaDonVi"></param>
    /// Author          Date            comment
    /// DucLV           ????            Taoj moi
    /// TuanVM          14/06           Sửa (bỏ readonly của textbox số hồ sơ và số công văn đến)
    public bool CheckTrungSoHoSo(string SoHoSo)
    {
        //Neu la sua
        if (MaHoSo != null && MaHoSo != "")
        {
            string strSoHoSoCu = ((HoSo)ProviderFactory.HoSoProvider.GetById(MaHoSo)).SoHoSo.ToString();
            if (ProviderFactory.HoSoProvider.CheckTrungSoHoSo(SoHoSo, strSoHoSoCu))
            {
                txtSoHoSo.Attributes.Remove("Readonly");
                //txtSoCongVanDen.Attributes.Remove("Readonly");
                txtSoHoSo.Attributes.Add("style", "background-color:#FFFFFF");
                //txtSoCongVanDen.Attributes.Add("style", "background-color:#FFFFFF");
                Thong_bao(Resource.msgTrungSoHoSo);
                return false;
            }
            else
            {
                txtSoHoSo.Attributes.Add("Readonly", "Readonly");
                //txtSoCongVanDen.Attributes.Add("Readonly", "Readonly");
                txtSoHoSo.Attributes.Add("style", "background-color:#FFFFC0");
                // txtSoCongVanDen.Attributes.Add("style", "background-color:#FFFFC0");
            }
        }
        else
        {
            if (ProviderFactory.HoSoProvider.CheckTrungSoHoSo(SoHoSo, string.Empty))
            {
                txtSoHoSo.Attributes.Remove("Readonly");
                //txtSoCongVanDen.Attributes.Remove("Readonly");
                txtSoHoSo.Attributes.Add("style", "background-color:#FFFFFF");
                //txtSoCongVanDen.Attributes.Add("style", "background-color:#FFFFFF");
                Thong_bao(Resource.msgTrungSoHoSo);
                return false;
            }
            else
            {
                txtSoHoSo.Attributes.Add("Readonly", "Readonly");
                //txtSoCongVanDen.Attributes.Add("Readonly", "Readonly");
                txtSoHoSo.Attributes.Add("style", "background-color:#FFFFC0");
                //txtSoCongVanDen.Attributes.Add("style", "background-color:#FFFFC0");
            }
        }
        return true;
    }

    /// <summary>
    /// Bắt sự kiện thay đổi đơn vị nộp hồ sơ, load lại thông tin điện thoại, email
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDonVi_SelectedIndexChanged(object sender, EventArgs e)
    {
        DmDonVi objDonVi = new DmDonVi();
        objDonVi = ProviderFactory.DmDonViProvider.GetById(ddlDonVi.SelectedValue.ToString());
        txtDienThoai.Text = objDonVi.DienThoai.ToString();
        txtEmail.Text = objDonVi.Email.ToString();
    }
    /// <summary>
    /// Chuyển trang sang hồ sơ chi tiết
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    /// Author      Date        comment
    /// QUANNM      03/03/2010  Tạo mới
    /// </modified>
    protected void btnHoSoChiTiet_Click(object sender, EventArgs e)
    {

        string strHoSoId = string.Empty;
        if (Request["Id"] != null)
            strHoSoId = Request["Id"].ToString();
        else
            strHoSoId = hidHoSoID.Value.ToString();
        //CB_HoSo_QuanLy.aspx?HoSoId=<%# Server.UrlEncode(Eval("ID").ToString()) %>&TrangThaiId=<%# Server.UrlEncode(Eval("TrangThaiID").ToString()) %>&UserControl=CB_HoSoDen
        //CB_HoSoSanPham.aspx?direct=CB_HoSoMoi&HoSoID=TTCB_13956&TrangThaiId=1&UserControl=CB_HoSoDen
        Response.Redirect("CB_HoSoSanPham.aspx?direct=CB_HoSoMoi&HoSoID=" + strHoSoId + "&TrangThaiId=" + TrangThaiID + "&UserControl=CB_HoSoDen", false);
    }
}
