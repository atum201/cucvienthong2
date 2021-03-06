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
public partial class WebUI_CB_HoSo_ChiTiet : PageBase
{
    /// <summary>
    /// Thêm mới, chỉnh sủa và xem chi tiết hồ sơ cong bo mới nhận
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 9/5/2009              
    /// DucLv                 16/5/2009                sua de phu hop voi phan cong bo           
    /// </Modified>
    private string MaHoSo = "";
    HoSo objHS = null;
    string passedArgument = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        MaHoSo = Server.UrlDecode(Request.QueryString["id"]);

        if (!IsPostBack)
        {
            btnCopy.Visible = false;
            txtSoHoSo.Attributes.Add("Readonly", "Readonly");
            //txtSoCongVanDen.Attributes.Add("Readonly", "Readonly");

            txtSoHoSo.Attributes.Add("style", "background-color:#FFFFC0");
            //txtSoCongVanDen.Attributes.Add("style", "background-color:#FFFFC0");
            BindEnums();
            LoadDonVi();
            Session["HoSoID"] = null;
            LoadDefaulComponents();
            lnkbtnThemDonVi.Visible = mUserInfo.IsInRole(EnPermission.QUANLY_DM_DONVI);
            /////// load thong tin don vi///////////
            DmDonVi objDonVi = new DmDonVi();
            objDonVi = ProviderFactory.DmDonViProvider.GetById(ddlDonVi.SelectedValue.ToString());
            txtDienThoai.Text = objDonVi.DienThoai.ToString();
            txtEmail.Text = objDonVi.Email.ToString();
            // LongHH
            if (objDonVi != null)
            {
                txtDiaChi.Text = objDonVi.DiaChi.ToString();
                txtTenTiengAnh.Text = objDonVi.TenTiengAnh.ToString();
                txtMaSoThue.Text = objDonVi.MaSoThue.ToString();
                lbtSuaDonVi.OnClientClick = "popCenter('DM_DonVi_ChiTiet.aspx?ID=" + objDonVi.Id + "','DM_DonVi_ChiTiet',800,400);return false;";
            }
            else {
                lbtSuaDonVi.Visible = false;
            }
            //txtLuuY.Text = "Trả kết quả sau ...ngày làm việc kể từ khi hồ sơ đầy đủ, hợp lệ.";
            // LongHH End
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
                    // txtSoCongVanDen.Attributes.Add("style", "background-color:#FFFFFF");
                }

                passedArgument = Request.Params.Get("__EVENTARGUMENT");
                LoadDonVi();
            }
        }
        // LongHH SetReadOnly
        this.SetReadOnly(txtDiaChi);
        this.SetReadOnly(txtTenTiengAnh);
        this.SetReadOnly(txtMaSoThue);
        // LongHH
        if (MaHoSo == null)
            MaHoSo = Session["HoSoID"] != null ? Session["HoSoID"].ToString() : string.Empty;

        EditHoSo(MaHoSo);
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
        ListItem NHAP_KHAU = new ListItem();
        NHAP_KHAU.Text = EntityHelper.GetEnumTextValue(EnNguonGocList.NHAP_KHAU);
        NHAP_KHAU.Value = ((int)EnNguonGocList.NHAP_KHAU).ToString();

        ListItem SX_TRONG_NUOC = new ListItem();
        SX_TRONG_NUOC.Text = EntityHelper.GetEnumTextValue(EnNguonGocList.SX_TRONG_NUOC);
        SX_TRONG_NUOC.Value = ((int)EnNguonGocList.SX_TRONG_NUOC).ToString();

        // LongHH
        ListItem MIEN_KIEM_TRA = new ListItem();
        MIEN_KIEM_TRA.Text = "Miễn kiểm tra";
        MIEN_KIEM_TRA.Value = ((int)QLCL_Patch.DuoiHoSo.CongBo_MiemKiemTra).ToString();
        
        rdgNguonGoc.Items.Add(NHAP_KHAU);
        rdgNguonGoc.Items.Add(SX_TRONG_NUOC);
        rdgNguonGoc.Items.Add(MIEN_KIEM_TRA);
        // LongHH
    }
    /// <summary>
    /// Load các giá trị default
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 9/5/2009              
    /// DucLv                 16/5/2009                sua de phu hop voi phan cong bo
    /// </Modified>
    private void LoadDefaulComponents()
    {
        if (MaHoSo == "" || MaHoSo == null)
        {
            txtTrangThai.Text = EntityHelper.GetEnumTextValue(EnTrangThaiHoSoList.HO_SO_MOI);
            txtSoHoSo.Text = "Số sinh tự động";
            //txtSoCongVanDen.Text = "Số sinh tự động";
            txtNguoiTiepNhan.Text = mUserInfo.FullName;
        }
        else
        {
            chkHoSoMoi.Attributes.Add("style", "display:none");
        }
        txtNgayNhan.Text = DateTime.Now.ToShortDateString();
        txtLuuY.Attributes.Add("onkeyup", " if (!checkLength('" + txtLuuY.ClientID + "', '4000')) return false;");
        txtTaiLieuKhac.Attributes.Add("onkeyup", " if (!checkLength('" + txtTaiLieuKhac.ClientID + "', '4000')) return false;");
        txtSanPham.Attributes.Add("onkeyup", " if (!checkLength('" + txtSanPham.ClientID + "', '4000')) return false;");
        txtKyHieu.Attributes.Add("onkeyup", " if (!checkLength('" + txtKyHieu.ClientID + "', '2000')) return false;");
        btnInPhieuNhan.Visible = false;
    }

    /// <summary>
    /// Lấy tất cả danh sách đơn vị
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 9/5/2009              
    /// DucLv                 16/5/2009                sua de phu hop voi phan cong bo      
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
    /// Chỉnh sửa hồ sơ
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 9/5/2009              
    /// DucLv                 16/5/2009                sua de phu hop voi phan cong bo                      
    /// </Modified>
    protected void EditHoSo(string MaHoSo)
    {
        ///// kiem tra xem neu co la nguoi xu lý thi hien thi radio danh gia
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
        //////// disable ////////
        if (Request["action"] != null && Request["action"].ToString() == "view")
        {
            txtNgayNhan.Enabled = false;
            txtNguoiNopHoSo.Enabled = false;

            txtSanPham.Enabled = false;
            txtSanPham.Enabled = false;

            chkTL_BanCongBo.Enabled = false;
            chkTl_BanTuDanhGia.Enabled = false;
            chkTl_KetQuaDoKiem.Enabled = false;
            chkTL_TaiLieuKT.Enabled = false;
            chkTL_TuCachPN.Enabled = false;

            txtTaiLieuKhac.Enabled = false;
            txtTrangThai.Enabled = false;
            txtSoCongVanDonVi.Enabled = false;
            txtDienThoai.Enabled = false;
            txtEmail.Enabled = false;

            // LongHH
            txtDiaChi.Enabled = false;
            txtTenTiengAnh.Enabled = false;
            txtMaSoThue.Enabled = false;
            // LongHH End

            txtLuuY.Enabled = false;
            grlHoSoDayDu.Enabled = false;
            btnCapNhat.Enabled = false;
            btnInPhieuNhan.Enabled = false;
        }
        objHS = new HoSo();
        if ((MaHoSo != "") && (MaHoSo != null))
        {
            btnInPhieuNhan.Visible = true;
            Session["HoSoID"] = MaHoSo;
            objHS = ProviderFactory.HoSoProvider.GetById(MaHoSo);

            if (!IsPostBack)
            {
                if (objHS.TrangThaiId == (int)EnTrangThaiHoSoList.DANG_XU_LY)
                {
                    ddlDonVi.Enabled = false;
                    lnkbtnThemDonVi.Visible = false;
                }
                //gán dữ liệu cho các control
                txtSoHoSo.Text = objHS.SoHoSo;
                //lay nguoi tiep nhan theo ID
                SysUser sysUser = ProviderFactory.SysUserProvider.GetById(objHS.NguoiTiepNhanId);
                txtNguoiTiepNhan.Text = sysUser.FullName;

                if (objHS.NgayTiepNhan != null)
                {
                    txtNgayNhan.Text = ((DateTime)objHS.NgayTiepNhan).ToShortDateString();
                }
                else
                    txtNgayNhan.Text = ((DateTime)DateTime.Now).ToShortDateString();
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
                chkTL_BanCongBo.Checked = Convert.ToBoolean(objHS.BanCongBo);
                chkTl_BanSaoGiayChungNhanHQ.Checked = Convert.ToBoolean(objHS.BanSaoGiayChungNhanHopQuy);
                chkTl_MauDau.Checked = Convert.ToBoolean(objHS.MauDauHopQuy);
                chkTl_BanTuDanhGia.Checked = Convert.ToBoolean(objHS.BanTuDanhGia);
                chkTl_KetQuaDoKiem.Checked = Convert.ToBoolean(objHS.KetQuaDoKiem);
                chkTL_TaiLieuKT.Checked = Convert.ToBoolean(objHS.TaiLieuKyThuat);
                chkTL_TuCachPN.Checked = Convert.ToBoolean(objHS.GiayToTuCachPhapNhan);

                txtTaiLieuKhac.Text = objHS.TaiLieuKhac;
                txtTrangThai.Text = EntityHelper.GetEnumTextValue((EnTrangThaiHoSoList)objHS.TrangThaiId);
                txtSoCongVanDonVi.Text = objHS.SoCongVanDonVi;
                txtDienThoai.Text = objHS.DienThoai;
                txtEmail.Text = objHS.Email;

                // LongHH
                //txtDiaChi.Text = objHS.DiaChi.ToString();
                //txtTenTiengAnh.Text = objHS.TenTiengAnh.ToString();
                //txtMaSoThue.Text = objHS.MaSoThue.ToString();
                // LongHH End

                txtLuuY.Text = objHS.Luuy;

                rdgNguonGoc.SelectedValue = objHS.NguonGocId.ToString();

                //chọn lại nhận hồ sơ từ
                foreach (ListItem item in rdgNhanTu.Items)
                {
                    if (item.Value == objHS.NhanHoSoTuId.ToString())
                    {
                        rdgNhanTu.SelectedValue = objHS.NhanHoSoTuId.ToString();
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
                // chon lai tinh trang ho so
                if (Convert.ToBoolean(objHS.HoSoDayDu))
                {
                    grlHoSoDayDu.SelectedValue = "true";
                }
                else
                {
                    grlHoSoDayDu.SelectedValue = "false";
                }
            }
        }
            //LongHH Thêm check nguồn gốc
        else {
            if (!IsPostBack && Request.QueryString["NguonGoc"] != null) {
                rdgNguonGoc.SelectedValue = Request.QueryString["NguonGoc"].ToString();
            }
        }
        //LongHH


    }

    /// <summary>
    /// Cập nhật hồ sơ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// Author          Date        comment
    /// ĐứcLV           ????        Tạo mới
    /// TuấnVm          10/06/2009  Sửa (Bổ sung trường ký hiệu sản phẩm, sửa lỗi cập nhật lại người tiếp nhận khi chuyên viên xử lý sửa trạng thái hồ sơ)
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        objHS = ProviderFactory.HoSoProvider.GetById(MaHoSo);

        if (CheckTrungSoHoSo(txtSoHoSo.Text.Trim()))
        {
            if ((MaHoSo != null) & (MaHoSo != string.Empty))
            {
                objHS = ProviderFactory.HoSoProvider.GetById(MaHoSo);
            }
            else
            {
                objHS = new HoSo();
                objHS.TrangThaiId = (int)EnTrangThaiHoSoList.HO_SO_MOI;
                objHS.NguoiTiepNhanId = mUserInfo.UserID;
                // Nếu người dùng chọn nhập hồ sơ cũ thì cập nhật lại số hồ sơ theo số người dùng nhập vào
                if (chkHoSoMoi.Checked == false)
                {
                    SinhSoHoSo(objHS);
                    objHS.SoHoSo = txtSoHoSo.Text.Trim();
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
            objHS.LoaiHoSo = (int)LoaiHoSo.CongBoHopQuy;
            //lay trong session user(lam sau)
            //objHS.NguoiTiepNhanId           = txtNguoiTiepNhan.Text;        
            objHS.DaDoc = mUserInfo.UserID;
            objHS.HoSoXuLyDungHan = 0;
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
            objHS.BanCongBo = chkTL_BanCongBo.Checked;
            objHS.BanSaoGiayChungNhanHopQuy = chkTl_BanSaoGiayChungNhanHQ.Checked;
            objHS.MauDauHopQuy = chkTl_MauDau.Checked;
            objHS.BanTuDanhGia = chkTl_BanTuDanhGia.Checked;
            objHS.KetQuaDoKiem = chkTl_KetQuaDoKiem.Checked;
            objHS.TaiLieuKyThuat = chkTL_TaiLieuKT.Checked;
            objHS.GiayToTuCachPhapNhan = chkTL_TuCachPN.Checked;
            objHS.TaiLieuKhac = txtTaiLieuKhac.Text;
            objHS.NguonGocId = Convert.ToInt32(rdgNguonGoc.SelectedValue);
            //if (objHS.NguonGocId == (int)EnNguonGocList.NHAP_KHAU)
            //{
            //    objHS.SoHoSo = objHS.SoHoSo.Replace("CBHQ", QLCL_Patch.duoiCBNK);
            //    objHS.SoHoSo = objHS.SoHoSo.Replace("KTCLNN", QLCL_Patch.duoiCBNK);
            //}
            //else {
            //    objHS.SoHoSo = objHS.SoHoSo.Replace(QLCL_Patch.duoiCBNK, "CBHQ");
            //}

            objHS.SoCongVanDonVi = txtSoCongVanDonVi.Text;
            //Da Gen trong CSDL
            //objHS.SoCongVanDen = txtSoCongVanDen.Text;
            objHS.DienThoai = txtDienThoai.Text;
            objHS.Email = txtEmail.Text;
            objHS.Luuy = txtLuuY.Text;
            //// check quyen, neu la nguoi xu ly moi dc danh gia ho so 
            if (mUserInfo.IsNguoiXuLy(MaHoSo))
            {
                objHS.HoSoDayDu = bool.Parse(grlHoSoDayDu.SelectedValue);
            }
            //insert
            try
            {
                if (objHS.Id == null)
                {
                    btnCopy.Visible = true;
                }

                ProviderFactory.HoSoProvider.Save(objHS);
                ProviderFactory.SysLogProvier.Write(mUserInfo, (MaHoSo == "" || MaHoSo == null) ? SysEventList.CB_HO_SO_SUA : SysEventList.CB_HO_SO_THEM_MOI, ThongBao);
                //if (Request.QueryString["id"] != null)
                //{
                ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo", "<script>alert('" + ThongBao + "'); </script>");
                //return;
                //    }

                //    ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                //           "<script>$(function(){PrintReport('CN_HoSo_QuanLy.aspx?UserControl=CB_HoSoDen', 'CBPhieuNhanHoSo','" + objHS.Id + "');});</script>");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            btnInPhieuNhan.Visible = !objHS.Id.Equals("");
            if (chkHoSoMoi.Checked)
            {
                Session["HoSoID"] = objHS.Id;
                //txtSoHoSo.Text = objHS.Id;
                HoSo objSoHoSo = ProviderFactory.HoSoProvider.GetById(Session["HoSoID"].ToString());
                txtSoHoSo.Text = objSoHoSo.SoHoSo;
            }
            if (objHS.Id != string.Empty)
            {
                btnCapNhat.Text = "Cập nhật";
                //btnCapNhat.Enabled = false;
                chkHoSoMoi.Visible = false;
                hidHoSoID.Value = objHS.Id;
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
            //LongHH

            //string tail = Convert.ToInt32(rdgNguonGoc.SelectedValue) == (int)EnNguonGocList.NHAP_KHAU ? "/"+QLCL_Patch.duoiCBNK+"-" : "/CBHQ-"; 
            string tail = "/"+EntityHelper.GetEnumTextValue((QLCL_Patch.DuoiHoSo)Convert.ToInt32(rdgNguonGoc.SelectedValue)) + "-";
            objHoSo.SoHoSo = SoCongVan + "/" + NgayTiepNhan.Year.ToString() + tail + MaTrungTam;
            // LongHH
            txtSoHoSo.Text = objHoSo.SoHoSo;
        }
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
                //txtSoCongVanDen.Attributes.Add("style", "background-color:#FFFFC0");
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
    protected void btnBoQua_Click(object sender, EventArgs e)
    {
        Response.Redirect("CN_HoSo_QuanLy.aspx?UserControl=CB_HoSoDen");
    }

    protected void btnInPhieuNhan_Click(object sender, EventArgs e)
    {
        string strHoSoId = string.Empty;
        if (Request["Id"] != null)
            strHoSoId = Request["Id"].ToString();
        else
            strHoSoId = hidHoSoID.Value.ToString();
        HoSo hs = ProviderFactory.HoSoProvider.GetById(strHoSoId);
        string nhapkhau = string.Empty;
        if (hs.NguonGocId == (int)EnNguonGocList.NHAP_KHAU) {
            nhapkhau = QLCL_Patch.duoiCBNK;
        }
        if (hs.NguonGocId == (int)QLCL_Patch.DuoiHoSo.CongBo_MiemKiemTra)
        {
            nhapkhau = EntityHelper.GetEnumTextValue(QLCL_Patch.DuoiHoSo.CongBo_MiemKiemTra);
        }
        ClientScript.RegisterClientScriptBlock(this.GetType(), "CB_XuLyHoSo_DanhGia_BanTiepNhan",
                "<script>popCenter('../ReportForm/DieuKienInBaoCao.aspx?LoaiBaoCao=CBPhieuNhanHoSo"+nhapkhau+"&HoSoId=" + strHoSoId + "','CBPhieuNhanHoSo',450,300);</script>");
    }
    protected void ddlDonVi_SelectedIndexChanged(object sender, EventArgs e)
    {
        DmDonVi objDonVi = new DmDonVi();
        objDonVi = ProviderFactory.DmDonViProvider.GetById(ddlDonVi.SelectedValue.ToString());
        txtDienThoai.Text = objDonVi.DienThoai.ToString();
        txtEmail.Text = objDonVi.Email.ToString();
        // LongHH
        txtDiaChi.Text = objDonVi.DiaChi.ToString();
        txtTenTiengAnh.Text = objDonVi.TenTiengAnh.ToString();
        txtMaSoThue.Text = objDonVi.MaSoThue.ToString();
        lbtSuaDonVi.OnClientClick = "popCenter('DM_DonVi_ChiTiet.aspx?ID=" + objDonVi.Id + "','DM_DonVi_ChiTiet',800,400);return false;";
        // LongHH End
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
