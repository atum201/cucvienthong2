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

/// <summary>
/// Thêm mới, chỉnh sủa và xem chi tiết hồ sơ mới nhận
/// </summary>
/// <Modified>
/// Người tạo              Ngày tạo                Chú thích
/// QuanNM                 8/5/2009              
/// </Modified>
public partial class WebUI_CN_HoSo_ChiTiet : PageBase
{
    private string MaHoSo = "";
    private string TrangThaiID = "";
    private string UserControl = "";
    //LongHH
    private bool edit = false;
    private string ThongBaoLePhiID = string.Empty;
    //LongHH
    HoSo objHS = null;
    string passedArgument = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        MaHoSo = Server.UrlDecode(Request.QueryString["id"]);
        UserControl = Server.UrlDecode(Request.QueryString["UserControl"]);
        if (chkHoSoMoi.Checked)
        {
            txtSoHoSo.Attributes.Add("Readonly", "Readonly");
            //txtSoCongVanDen.Attributes.Add("Readonly", "Readonly");
            txtSoHoSo.Attributes.Add("style", "background-color:#FFFFC0");
            //txtSoCongVanDen.Attributes.Add("style", "background-color:#FFFFC0");
        }
        TrangThaiID = Server.UrlDecode(Request.QueryString["TrangThaiId"]);
        //kiem tra trang thai ho so
        if (Convert.ToInt32(TrangThaiID) == (int)EnTrangThaiHoSoList.DANG_XU_LY)
        {
            ChangeControlStatus(false);
        }
        //LongHH
        hfDonGiaTiepNhan.Value = ((int)QLCL_Patch.LePhi.DonGiaTiepNhan).ToString();
        hfDonGiaXemXet.Value = ((int)QLCL_Patch.LePhi.PhiXemXet).ToString();
        DataTable TTGiayBaoPhi = QLCL_Patch.GetTTGiayBaoPhi(MaHoSo);
        DataRow tblp = QLCL_Patch.GetTBPhiTiepNhan(MaHoSo);
        edit = tblp != null;
        SetReadOnly(txtTongPhi);
        //LongHH
        if (!IsPostBack)
        {
            btnInPhieuNhan.Visible = false;
            btnInGiayBaoPhi.Visible = false;
            btnCopy.Visible = false;
            chkHoSoMoi.Checked = true;
            Session["HoSoID"] = null;
            LoadDonVi();
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
                //LongHH
                txtTenTiengAnh.Text = objDonVi.TenTiengAnh.ToString();
                txtDiaChi.Text = objDonVi.DiaChi.ToString();
                txtMaSoThue.Text = objDonVi.MaSoThue.ToString();
                //LongHH
            }
            // LongHH
            txtTaiLieuKhac.Text = "Trả kết quả sau 10 ngày làm việc kể từ khi hồ sơ đầy đủ, hợp lệ";
            DataTable dsnguoiky = QLCL_Patch.GetDSNguoiKyGiayBaoPhi();

            if (dsnguoiky.Rows.Count > 0)
            {
                for (int i = 0; i < dsnguoiky.Rows.Count; i++)
                {
                    ddlNguoiKy.Items.Add(new ListItem(dsnguoiky.Rows[i]["FullName"].ToString(), dsnguoiky.Rows[i]["ID"].ToString()));
                    ddlThamQuyen.Items.Add(new ListItem(QLCL_Patch.GetChucVuKy(dsnguoiky.Rows[i]["Position"].ToString()), dsnguoiky.Rows[i]["ID"].ToString()));
                }
            }
            
            if (edit && TTGiayBaoPhi != null && TTGiayBaoPhi.Rows.Count > 0) // nếu đã có trong patch thì đã có thông báo phí, và bật chế độ edit, load dữ liệu từ db
            {
                ddlNguoiKy.SelectedIndex = ddlNguoiKy.Items.IndexOf(ddlNguoiKy.Items.FindByValue(TTGiayBaoPhi.Rows[0]["NguoiKy"].ToString()));
                ddlThamQuyen.SelectedIndex = ddlThamQuyen.Items.IndexOf(ddlThamQuyen.Items.FindByValue(TTGiayBaoPhi.Rows[0]["NguoiKy"].ToString()));
                txtSLTiepNhan.Text = TTGiayBaoPhi.Rows[0]["SLTiepNhan"].ToString();
                
                try
                {
                    int sl = int.Parse(txtSLTiepNhan.Text.Trim());
                    txtTongPhi.Text = (sl * ((int)QLCL_Patch.LePhi.PhiXemXet + (int)QLCL_Patch.LePhi.PhiXemXet)).ToString();
                }
                catch
                {

                }
            }
            

            // LongHH
        }
        else
        {
            if (this.Request["__EVENTTARGET"] == "AddNewCommit")
            {
                if (chkHoSoMoi.Checked)
                {
                    txtSoHoSo.Attributes.Add("Readonly", "Readonly");
                    //txtSoCongVanDen.Attributes.Add("Readonly", "Readonly");
                    txtSoHoSo.Attributes.Add("style", "background-color:#FFFFC0");
                    //txtSoCongVanDen.Attributes.Add("style", "background-color:#FFFFC0");
                }
                else
                {
                    txtSoHoSo.Attributes.Remove("Readonly");
                    //txtSoCongVanDen.Attributes.Remove("Readonly");
                    txtSoHoSo.Attributes.Add("style", "background-color:#FFFFFF");
                    //txtSoCongVanDen.Attributes.Add("style", "background-color:#FFFFFF");
                }

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
        if (MaHoSo == "" || MaHoSo == null)
        {
            txtTrangThai.Text = EntityHelper.GetEnumTextValue(EnTrangThaiHoSoList.HO_SO_MOI);
            txtSoHoSo.Text = "Số sinh tự động";
            //txtSoCongVanDen.Text = "Số sinh tự động";
            txtNguoiTiepNhan.Text = mUserInfo.FullName;
            txtNgayNhan.Text = DateTime.Now.ToShortDateString();
            rdgNhanTu.SelectedValue = ((int)EnNhanHoSoTuList.TRUC_TIEP).ToString();
        }
        else
        {
            trLoaiHoSo.Attributes.Add("style", "display:none");
        }
        txtLuuY.Attributes.Add("onkeyup", " if (!checkLength('" + txtLuuY.ClientID + "', '4000')) return false;");
        txtTaiLieuKhac.Attributes.Add("onkeyup", " if (!checkLength('" + txtTaiLieuKhac.ClientID + "', '4000')) return false;");
        txtSanPham.Attributes.Add("onkeyup", " if (!checkLength('" + txtSanPham.ClientID + "', '4000')) return false;");
        txtKyHieu.Attributes.Add("onkeyup", " if (!checkLength('" + txtKyHieu.ClientID + "', '2000')) return false;");
    }
    /// <summary>
    /// Chuyển trạng thái các controls khi trạng thái hồ sơ là đang xử lý
    /// </summary>
    /// <param name="status"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 9/5/2009              
    /// </Modified>
    private void ChangeControlStatus(bool status)
    {
        //Control control = Master.FindControl("ContentPlaceHolder1");
        //foreach (Control ctrl in control.Controls)
        //{
        //    if (ctrl is TextBox)
        //        ((TextBox)ctrl).Enabled = status;
        //    //else if (ctrl is Button)
        //    //((Button)ctrl).Enabled = status;
        //    if (ctrl is LinkButton)
        //        ((LinkButton)ctrl).Enabled = status;
        //    else if (ctrl is RadioButton)
        //        ((RadioButton)ctrl).Enabled = status;
        //    else if (ctrl is ImageButton)
        //        ((ImageButton)ctrl).Enabled = status;
        //    else if (ctrl is CheckBox)
        //        ((CheckBox)ctrl).Enabled = status;
        //    else if (ctrl is DropDownList)
        //        ((DropDownList)ctrl).Enabled = status;
        //    else if (ctrl is HyperLink)
        //        ((HyperLink)ctrl).Enabled = status;
        //    else
        //        if (ctrl is RadioButtonList)
        //        {
        //            if (((RadioButtonList)ctrl).ID != "grlHoSoDayDu")
        //            {
        //                ((RadioButtonList)ctrl).Enabled = status;
        //            }
        //        }
        //}
        ddlDonVi.Enabled = false;
        lbtTaoMoiDV.Enabled = false;
        rdgLoaiHinhChungNhan.Enabled = false;
    }

    /// <summary>
    /// Lấy tất cả danh sách phòng ban
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
        BUU_DIEN.Text = "Qua đường bưu điện";
        BUU_DIEN.Value = ((int)EnNhanHoSoTuList.BUU_DIEN).ToString();

        ListItem TRUC_TIEP = new ListItem();
        TRUC_TIEP.Text = "Trực tiếp";
        TRUC_TIEP.Value = ((int)EnNhanHoSoTuList.TRUC_TIEP).ToString();

        ListItem QUA_MANG = new ListItem();
        QUA_MANG.Text = "Qua mạng Internet";
        QUA_MANG.Value = ((int)EnNhanHoSoTuList.QUA_MANG).ToString();

        rdgNhanTu.Items.Add(BUU_DIEN);
        rdgNhanTu.Items.Add(TRUC_TIEP);
        rdgNhanTu.Items.Add(QUA_MANG);

        //gán dữ liệu cho RadioButtonList Nguồn gốc
        ListItem NK_CHUA_DO_KIEM = new ListItem();
        NK_CHUA_DO_KIEM.Text = EntityHelper.GetEnumTextValue(EnNguonGocList.NK_CHUA_DO_KIEM);
        NK_CHUA_DO_KIEM.Value = ((int)EnNguonGocList.NK_CHUA_DO_KIEM).ToString();

        ListItem NK_KEM_KQ_DO_KIEM = new ListItem();
        NK_KEM_KQ_DO_KIEM.Text = EntityHelper.GetEnumTextValue(EnNguonGocList.NK_KEM_KQ_DO_KIEM);
        NK_KEM_KQ_DO_KIEM.Value = ((int)EnNguonGocList.NK_KEM_KQ_DO_KIEM).ToString();

        ListItem SX_TRONG_NUOC_CO_ISO = new ListItem();
        SX_TRONG_NUOC_CO_ISO.Text = EntityHelper.GetEnumTextValue(EnNguonGocList.SX_TRONG_NUOC_CO_ISO);
        SX_TRONG_NUOC_CO_ISO.Value = ((int)EnNguonGocList.SX_TRONG_NUOC_CO_ISO).ToString();

        ListItem SX_TRONG_NUOC_KHONG_CO_ISO = new ListItem();
        SX_TRONG_NUOC_KHONG_CO_ISO.Text = EntityHelper.GetEnumTextValue(EnNguonGocList.SX_TRONG_NUOC_KHONG_CO_ISO);
        SX_TRONG_NUOC_KHONG_CO_ISO.Value = ((int)EnNguonGocList.SX_TRONG_NUOC_KHONG_CO_ISO).ToString();

        rdgNguonGoc.Items.Add(SX_TRONG_NUOC_CO_ISO);
        rdgNguonGoc.Items.Add(NK_KEM_KQ_DO_KIEM);
        rdgNguonGoc.Items.Add(SX_TRONG_NUOC_KHONG_CO_ISO);
        rdgNguonGoc.Items.Add(NK_CHUA_DO_KIEM);

        // Gan du lieu cho radionbutton list loại hình chứng nhận
        ListItem ChungNhanHopChuan = new ListItem();
        ChungNhanHopChuan.Text = "Chứng nhận hợp chuẩn";
        ChungNhanHopChuan.Value = ((int)LoaiHoSo.ChungNhanHopChuan).ToString();
        ListItem ChungNhanHopQuy = new ListItem();
        ChungNhanHopQuy.Text = "Chứng nhận hợp quy";
        ChungNhanHopQuy.Value = ((int)LoaiHoSo.ChungNhanHopQuy).ToString();
        rdgLoaiHinhChungNhan.Items.Add(ChungNhanHopChuan);
        rdgLoaiHinhChungNhan.Items.Add(ChungNhanHopQuy);
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
        //Kiem tra xem neu co la nguoi xu lý thi hien thi radio danh gia
        if (mUserInfo.IsNguoiXuLy(MaHoSo))
        {
            Nhanxet.Style.Add("display", "display");
            RequireDanhGia.Visible = true;
        }
        else
        {
            Nhanxet.Style.Add("display", "none");
            RequireDanhGia.Visible = false;
        }

        objHS = new HoSo();
        if ((MaHoSo != "") && (MaHoSo != null))
        {
            btnCapNhat.Text = "Cập nhật";
            objHS = ProviderFactory.HoSoProvider.GetById(MaHoSo);
            //HIEN THi nut in phieu nhan ho so chung nhan
            btnInPhieuNhan.Visible = true;
            btnInGiayBaoPhi.Visible = true;
            btnHoSoChiTiet.Visible = true;
            //btnInPhieuNhan.OnClientClick = "OpenBaoCao(this,'../ReportForm/DieuKienInBaoCao.aspx?LoaiBaoCao=PhieuNhanHoSo&HoSoID=" + objHS.Id + "');return false;";
            Session["HoSoID"] = MaHoSo;
            if (!IsPostBack)
            {
                //gán dữ liệu cho các control
                txtSoHoSo.Text = objHS.SoHoSo;
                //lay nguoi tiep nhan theo ID
                SysUser sysUser = ProviderFactory.SysUserProvider.GetById(objHS.NguoiTiepNhanId);
                txtNguoiTiepNhan.Text = sysUser.FullName;

                if (objHS.NgayTiepNhan != null)
                {
                    txtNgayNhan.Text = ((DateTime)objHS.NgayTiepNhan).ToShortDateString();
                }
                txtNguoiNopHoSo.Text = objHS.NguoiNopHoSo;

                //liệt kê tên sản phẩm
                //List<string> ListTenSanPham = new List<string>();
                //foreach(SanPham objSanPham in objHS.SanPhamCollection)
                //{
                //    ListTenSanPham.Add(objSanPham.SanPhamId);
                //}
                //txtSanPham.Text         = String.Join(", ", ListTenSanPham.ToArray());
                txtSanPham.Text = objHS.DanhSachSanPham;
                txtKyHieu.Text = objHS.DanhSachKyHieuSanPham;
                //gán thông tin checkbox
                chkDon.Checked = Convert.ToBoolean(objHS.DonDeNghiChungNhan);
                chkGiayTo.Checked = Convert.ToBoolean(objHS.GiayToTuCachPhapNhan);
                chkKetQua.Checked = Convert.ToBoolean(objHS.KetQuaDoKiem);
                chkQuyTrinhCL.Checked = Convert.ToBoolean(objHS.QuyTrinhDamBaoChatLuong);
                chkQuyTrinhSX.Checked = Convert.ToBoolean(objHS.QuyTrinhSanXuat);
                chkTaiLieu.Checked = Convert.ToBoolean(objHS.TaiLieuKyThuat);
                chkTieuChuan.Checked = Convert.ToBoolean(objHS.TieuChuanApDung);
                chkChungChi.Checked = Convert.ToBoolean(objHS.ChungChiHtqlcl);

                txtTaiLieuKhac.Text = objHS.TaiLieuKhac;
                txtTrangThai.Text = EntityHelper.GetEnumTextValue((EnTrangThaiHoSoList)objHS.TrangThaiId);
                txtSoCongVanDonVi.Text = objHS.SoCongVanDonVi;
                txtDienThoai.Text = objHS.DienThoai;
                txtEmail.Text = objHS.Email;
                txtLuuY.Text = objHS.Luuy;

                //LongHH
                if (!edit)// nếu ko phải edit tự động load số lượng sản phẩm trong hồ sơ
                {
                    UserInfo _mUserInfo = Session["User"] as UserInfo;
                    if (_mUserInfo == null || !_mUserInfo.IsAuthenticated)
                    {
                        Response.Redirect("~/WebUI/HT_DangNhap.aspx", true);
                    }
                    DataTable dtbSanPham = ProviderFactory.SanPhamProvider.LayTatCaSanPhamTheoQuyenDangNhap(objHS.Id, _mUserInfo.UserID, _mUserInfo.GetPermissionList("01"));
                    txtSLTiepNhan.Text = dtbSanPham.Rows.Count.ToString();
                    
                }
                //LongHH

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
                        //LongHH
                        DmDonVi objDonVi1 = new DmDonVi();
                        objDonVi1 = ProviderFactory.DmDonViProvider.GetById(objHS.DonViId);
                        txtTenTiengAnh.Text = objDonVi1.TenTiengAnh.ToString();
                        txtDiaChi.Text = objDonVi1.DiaChi.ToString();
                        txtMaSoThue.Text = objDonVi1.MaSoThue.ToString();
                        //LongHH
                    }
                }

                // chon lai tinh trang ho so
                if (Convert.ToBoolean(objHS.HoSoDayDu))
                {
                    grlHoSoDayDu.SelectedValue = "true";
                }
                else
                {
                    grlHoSoDayDu.SelectedValue = "false";
                }

                rdgLoaiHinhChungNhan.SelectedValue = objHS.LoaiHoSo.ToString();
            }
        }
            //LongHH
        else
        {
            RowNguoiKy.Style.Add("display", "None");
            RowSoLuong.Style.Add("display", "None");
        }
        // LongHH
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
                objHS.TrangThaiId = (int)EnTrangThaiHoSoList.HO_SO_MOI;
                objHS.NguoiTiepNhanId = mUserInfo.UserID;
                // Nếu người dùng chọn nhập hồ sơ cũ thì cập nhật lại số hồ sơ theo số người dùng nhập vào
                if (chkHoSoMoi.Checked == false)
                {
                    SinhSoHoSo(objHS);
                    objHS.HoSoMoi = false;
                }
                else
                {
                    objHS.HoSoMoi = true;
                }
            }
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
            //Da Gen trong CSDL
            //objHS.SoHoSo                    = txtSoHoSo.Text;
            if (!string.IsNullOrEmpty(rdgLoaiHinhChungNhan.SelectedValue))
            {
                objHS.LoaiHoSo = Convert.ToInt16(rdgLoaiHinhChungNhan.SelectedValue);
            }

            objHS.DaDoc = mUserInfo.UserID;
            //Da Gen trong CSDL
            //objHS.TrungTamId = "GET_MA_TRUNG_TAM";
            if (txtNgayNhan.Text != null && txtNgayNhan.Text != "")
            {
                objHS.NgayTiepNhan = Convert.ToDateTime(txtNgayNhan.Text.Trim());
            }
            objHS.NguoiNopHoSo = txtNguoiNopHoSo.Text.Trim();
            objHS.DanhSachSanPham = txtSanPham.Text.Trim();
            objHS.DanhSachKyHieuSanPham = txtKyHieu.Text.Trim();
            if (rdgNhanTu.SelectedValue != null && rdgNhanTu.SelectedValue != "")
            {
                objHS.NhanHoSoTuId = Convert.ToInt16(rdgNhanTu.SelectedValue);
            }
            objHS.DonViId = ddlDonVi.SelectedValue;
            //
            objHS.DonDeNghiChungNhan = chkDon.Checked;
            objHS.GiayToTuCachPhapNhan = chkGiayTo.Checked;
            objHS.KetQuaDoKiem = chkKetQua.Checked;
            objHS.QuyTrinhDamBaoChatLuong = chkQuyTrinhCL.Checked;
            objHS.QuyTrinhSanXuat = chkQuyTrinhSX.Checked;
            objHS.TaiLieuKyThuat = chkTaiLieu.Checked;
            objHS.TieuChuanApDung = chkTieuChuan.Checked;
            objHS.ChungChiHtqlcl = chkChungChi.Checked;
            if (rdgNguonGoc.SelectedValue != null && rdgNguonGoc.SelectedValue != "")
            {
                objHS.NguonGocId = Convert.ToInt16(rdgNguonGoc.SelectedValue);
            }

            objHS.TaiLieuKhac = txtTaiLieuKhac.Text;

            objHS.SoCongVanDonVi = txtSoCongVanDonVi.Text;
            //Da Gen trong CSDL
            //objHS.SoCongVanDen = txtSoCongVanDen.Text;
            objHS.DienThoai = txtDienThoai.Text;
            objHS.Email = txtEmail.Text;
            objHS.Luuy = txtLuuY.Text;
            //Check quyen, neu la nguoi xu ly moi dc danh gia ho so 
            if (mUserInfo.IsNguoiXuLy(MaHoSo))
            {
                objHS.HoSoDayDu = bool.Parse(grlHoSoDayDu.SelectedValue);
            }

            //if (rdgNguonGoc.SelectedValue == ((int)EnNguonGocList.SX_TRONG_NUOC_KHONG_CO_ISO).ToString())
            //    objHS.SoQuyTrinhCanDanhGiaCl = Convert.ToInt32(txtSoLuongQuyTrinh.Text.Trim());
            //else
            //    objHS.SoQuyTrinhCanDanhGiaCl = 0;

            //insert
            try
            {
                if (objHS.Id == null)
                {
                    btnCopy.Visible = true;
                }
                ProviderFactory.HoSoProvider.Save(objHS);
                ProviderFactory.SysLogProvier.Write(mUserInfo, (MaHoSo == "" || MaHoSo == null) ? SysEventList.CN_HO_SO_SUA : SysEventList.CN_HO_SO_THEM_MOI, ThongBao);

                ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo", "<script>alert('" + ThongBao + "'); </script>");

            }
            catch (Exception ex)
            {
                throw ex;
            }
            btnInPhieuNhan.Visible = !objHS.Id.Equals("");
            btnInGiayBaoPhi.Visible = !objHS.Id.Equals("");
            //hiển thị nút hồ sơ chi tiết khi hồ sơ Id != ""
            if (!objHS.Id.Equals(""))
                btnHoSoChiTiet.Visible = true;
            if (chkHoSoMoi.Checked)
            {
                Session["HoSoID"] = objHS.Id;
                HoSo objSoHoSo = ProviderFactory.HoSoProvider.GetById(Session["HoSoID"].ToString());
                txtSoHoSo.Text = objSoHoSo.SoHoSo;
            }
            if (objHS.Id != string.Empty)
            {
                btnCapNhat.Text = "Cập nhật";
                //btnCapNhat.Enabled = false;
                trLoaiHoSo.Style.Add("display", "none");
                hidHoSoID.Value = objHS.Id;
            }

            btnHoSoChiTiet.Visible = true;
        }
        //LongHH Cập nhật thông báo phí tiếp nhận
        if (edit)// trong che do edit, sử dụng các hàm Patch
        {
            DataRow rtb = QLCL_Patch.GetTBPhiTiepNhan(objHS.Id);

            if (rtb != null)
            {
                int sl = 1, xx = 1, tp = 0;
                try
                {
                    sl = int.Parse(txtSLTiepNhan.Text);
                    tp = sl * ((int)QLCL_Patch.LePhi.DonGiaTiepNhan + (int)QLCL_Patch.LePhi.PhiXemXet);
                }
                catch { }
                ThongBaoLePhiID = rtb["ID"].ToString();
                ThongBaoLePhi objThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetById(ThongBaoLePhiID);
                QLCL_Patch.UpdateThongBaoPhiTiepNhan(objHS.Id, ddlNguoiKy.SelectedValue, tp, ddlNguoiKy.SelectedItem.Text);
                QLCL_Patch.SetNguoiKy_GiayBaoPhi(objHS.Id, ddlNguoiKy.SelectedValue, sl, xx);
            }
            else
            {
                edit = false;
            }
        }
        if (!edit)// Chế đồ thêm mới
        {
            string sogiaycn = objHS.SoHoSo.Replace("CNHQ", "PNHS");
            ThongBaoLePhi tblp = ProviderFactory.ThongBaoLePhiProvider.GetBySoGiayThongBaoLePhi(sogiaycn);
            int sl = 1, xx = 1, tp = 0;
            try
            {
                sl = int.Parse(txtSLTiepNhan.Text);
                tp = sl * ((int)QLCL_Patch.LePhi.DonGiaTiepNhan + (int)QLCL_Patch.LePhi.PhiXemXet);
            }
            catch { }

            Cuc_QLCL.Data.TransactionManager transaction = ProviderFactory.Transaction;
            try
            {
                if (tblp != null)
                {
                    QLCL_Patch.UpdateThongBaoPhiTiepNhan(objHS.Id, ddlNguoiKy.SelectedValue, tp, ddlNguoiKy.SelectedItem.Text);
                }
                else
                {
                    ThongBaoLePhi objThongBaoLePhi = new ThongBaoLePhi();
                    objThongBaoLePhi.DonViId = objHS.DonViId;
                    objThongBaoLePhi.TongPhi = tp;
                    objThongBaoLePhi.TrangThaiId = (int?)EnTrangThaiThongBaoPhiList.CHO_THU_PHI;
                    objThongBaoLePhi.HoSoId = objHS.Id;
                    objThongBaoLePhi.LoaiPhiId = 9;
                    objThongBaoLePhi.NguoiPheDuyetId = ddlNguoiKy.SelectedValue;
                    objThongBaoLePhi.TenNguoiKyDuyet = ddlNguoiKy.SelectedItem.Text;
                    objThongBaoLePhi.SoGiayThongBaoLePhi = sogiaycn;
                    
                    ProviderFactory.ThongBaoLePhiProvider.Insert(transaction, objThongBaoLePhi);
                    transaction.Commit();
                    transaction.Dispose();
                }
                

                QLCL_Patch.SetNguoiKy_GiayBaoPhi(objHS.Id, ddlNguoiKy.SelectedValue, sl, xx);

                Response.Redirect("CN_HoSo_ChiTiet.aspx?id=" + objHS.Id + "&TrangThaiId=1&UserControl=CN_HoSoDen");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
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

            if (Convert.ToInt32(rdgLoaiHinhChungNhan.SelectedValue) == (int)LoaiHoSo.ChungNhanHopChuan)
                objHoSo.SoHoSo = SoCongVan + "/" + NgayTiepNhan.Year.ToString() + "/CNHC-" + MaTrungTam;
            else
                objHoSo.SoHoSo = SoCongVan + "/" + NgayTiepNhan.Year.ToString() + "/CNHQ-" + MaTrungTam;
            txtSoHoSo.Text = objHoSo.SoHoSo;
        }
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
        Response.Redirect("CN_HoSo_QuanLy.aspx");
    }

    // LongHH
    protected void btnInGiayBaoPhi_Click(object sender, EventArgs e)
    {
        if (edit)
        {
            DataRow rtb = QLCL_Patch.GetTBPhiTiepNhan(objHS.Id);
            
            if (rtb != null)
            {
                int sl = 1, xx = 1, tp = 0;
                try
                {
                    sl = int.Parse(txtSLTiepNhan.Text);
                    tp = sl * ((int)QLCL_Patch.LePhi.DonGiaTiepNhan + (int)QLCL_Patch.LePhi.PhiXemXet);
                }
                catch { }
                ThongBaoLePhiID = rtb["ID"].ToString();
                ThongBaoLePhi objThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetById(ThongBaoLePhiID);
                QLCL_Patch.UpdateThongBaoPhiTiepNhan(objHS.Id, ddlNguoiKy.SelectedValue, tp, ddlNguoiKy.SelectedItem.Text);
                
                QLCL_Patch.SetNguoiKy_GiayBaoPhi(objHS.Id, ddlNguoiKy.SelectedValue, sl, xx);
                Response.Redirect("../ReportForm/HienBaoCao.aspx?HoSoID=" + objHS.Id + "&LoaiBaoCao=LePhiTiepNhanCNHQ&TenBaoCao=Le_Phi_Tiep_NhanCNHQ&format=Word");
            }
            else
            {
                edit = false;
            }
        }
        if (!edit)
        {
            string sogiaycn = objHS.SoHoSo.Replace("CNHQ", "PNHS");
            ThongBaoLePhi tblp = ProviderFactory.ThongBaoLePhiProvider.GetBySoGiayThongBaoLePhi(sogiaycn);
            int sl = 1, xx = 1, tp = 0;
            try
            {
                sl = int.Parse(txtSLTiepNhan.Text);
                tp = sl * ((int)QLCL_Patch.LePhi.DonGiaTiepNhan + (int)QLCL_Patch.LePhi.PhiXemXet);
            }
            catch { }

            Cuc_QLCL.Data.TransactionManager transaction = ProviderFactory.Transaction;
            try
            {
                if (tblp != null)
                {
                    QLCL_Patch.UpdateThongBaoPhiTiepNhan(objHS.Id, ddlNguoiKy.SelectedValue, tp, ddlNguoiKy.SelectedItem.Text);
                }
                else
                {
                    ThongBaoLePhi objThongBaoLePhi = new ThongBaoLePhi();
                    objThongBaoLePhi.DonViId = objHS.DonViId;
                    objThongBaoLePhi.TongPhi = tp;
                    objThongBaoLePhi.TrangThaiId = (int?)EnTrangThaiThongBaoPhiList.CHO_THU_PHI;
                    objThongBaoLePhi.HoSoId = objHS.Id;
                    objThongBaoLePhi.LoaiPhiId = 9;
                    objThongBaoLePhi.NguoiPheDuyetId = ddlNguoiKy.SelectedValue;
                    objThongBaoLePhi.TenNguoiKyDuyet = ddlNguoiKy.SelectedItem.Text;
                    objThongBaoLePhi.SoGiayThongBaoLePhi = sogiaycn;

                    ProviderFactory.ThongBaoLePhiProvider.Insert(transaction, objThongBaoLePhi);
                    transaction.Commit();
                    transaction.Dispose();
                }


                QLCL_Patch.SetNguoiKy_GiayBaoPhi(objHS.Id, ddlNguoiKy.SelectedValue, sl, xx);

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
        
    }

    // LongHH
    protected void btnInPhieuNhan_Click(object sender, EventArgs e)
    {
        string strHoSoId = string.Empty;
        if (Request["Id"] != null)
            strHoSoId = Request["Id"].ToString();
        else
            strHoSoId = hidHoSoID.Value.ToString();
        ClientScript.RegisterClientScriptBlock(this.GetType(), "CB_XuLyHoSo_DanhGia_BanTiepNhan",
                "<script>popCenter('../ReportForm/DieuKienInBaoCao.aspx?LoaiBaoCao=PhieuNhanHoSo&HoSoId=" + strHoSoId + "','PhieuNhanHoSo',450,300);</script>");

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
    /// Kiểm tra trùng số công văn đơn vị
    /// </summary>
    /// <param name="MaDonVi"></param>
    public bool CheckTrungSoCVDonVi(string SoCongVanDonVi)
    {
        //Neu la sua
        if (Request["ID"] != null)
        {
            string strSoCongVanDonViCu = ((HoSo)ProviderFactory.HoSoProvider.GetById(Request["ID"].ToString())).SoCongVanDonVi.ToString();
            if (ProviderFactory.HoSoProvider.CheckTrungSoCVDonVi(SoCongVanDonVi, strSoCongVanDonViCu))
            {
                Thong_bao(Resource.msgTrungSoCVDonVi);
                return false;
            }
        }
        else
        {
            if (ProviderFactory.HoSoProvider.CheckTrungSoCVDonVi(SoCongVanDonVi, string.Empty))
            {
                Thong_bao(Resource.msgTrungSoCVDonVi);
                return false;
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
        //LongHH
        txtTenTiengAnh.Text = objDonVi.TenTiengAnh.ToString();
        txtDiaChi.Text = objDonVi.DiaChi.ToString();
        txtMaSoThue.Text = objDonVi.MaSoThue.ToString();
        //LongHH
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
        if (string.IsNullOrEmpty(TrangThaiID))
            TrangThaiID = "1";
        Response.Redirect("CN_HoSoSanPham.aspx?direct=CN_HoSoMoi&HoSoID=" + strHoSoId + "&TrangThaiId=" + TrangThaiID + "&UserControl=CN_HoSoDen", false);
    }

    /// <summary>
    /// Cho phép thêm mới một hồ sơ giống hồ sơ đã nhập
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// Author          Date        Comment
    /// TuanVM          13/04/2010  Tao moi
    protected void btnCopy_Click(object sender, EventArgs e)
    {
        // Đặt MaHoSo ve null
        MaHoSo = string.Empty;
        txtSoHoSo.Text = "Số sinh tự động";
        // Gọi lại sự kiện cập nhật
        btnCapNhat_Click(sender, e);
    }
}
