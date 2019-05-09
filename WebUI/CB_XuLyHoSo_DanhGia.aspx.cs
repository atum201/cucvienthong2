using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Cuc_QLCL.Entities;
using Cuc_QLCL.AdapterData;
using Cuc_QLCL.Data;
using Resources;
using System.Transactions;
using System.IO;
using System.Text;
using TinhVan.WebUI.WebControls;
using CucQLCL.Common;

public partial class WebUI_CB_XuLyHoSo_DanhGia : PageBase
{
    //lưu lệ phí
    string strLePhi = string.Empty;
    //Lưu nhóm sản phẩm
    //*được gán giá trị khi load dữ liệu; thay đổi tên sản phẩm
    string strNhomSanPhamID = string.Empty;
    //xác định xem có gửi thẩm định trước không
    bool blGuiThamDinh = false;
    bool blCapNhatNoiDanhGia = true;
    //xác định hồ sơ đã gửi hay chưa gửi
    private string strDanhSachHoSo = string.Empty;
    //xác định sản phẩm đã gửi hay chưa gửi
    private string strDanhSachSanPham = string.Empty;
    //Sản phẩm cần thao tác
    SanPham objSanPham;

    protected void Page_Load(object sender, EventArgs e)
    {
        bool blHasCreate = false;

        this.PreRender += new EventHandler(WebUI_CB_XuLyHoSo_DanhGia_PreRender);

        txtNoiDungDoKiem.Attributes.Add("onkeyup", " if (!checkLength('" + txtNoiDungDoKiem.ClientID + "', '255')) return false;");
        txtNhanXetKhac.Attributes.Add("onkeyup", " if (!checkLength('" + txtNhanXetKhac.ClientID + "', '255')) return false;");
        txtNoiDungXuLy.Attributes.Add("onkeyup", " if (!checkLength('" + txtNoiDungXuLy.ClientID + "', '255')) return false;");
        txtSoBTNCV.Attributes.Add("onchange", "return getTextValue();");
        //Lấy link của HS
        //CB_HoSo_QuanLy.aspx?UserControl=
        //CB_HoSoSanPham_QuanLy.aspx?UserControl=??&HoSoId=
        if (Request["UserControlHS"] != null)
            strDanhSachHoSo = "CB_HoSoDen";
        else
            strDanhSachHoSo = Request["UserControlHS"];
        if (Request["UserControl"] != null)
            strDanhSachSanPham = Request["UserControl"];
        else
            strDanhSachSanPham = "CB_HoSoDen";
        //LongHH
        SetReadOnly(txtTenTiengAnhCQDK);
        SetReadOnly(txtDiaChiCQDK);
        SetReadOnly(txtSDTCQDK);
        //LongHh
        //txtSoBTNCV.Attributes.Add("Readonly", "Readonly");

        if (Request["TrangThaiID"] != null)
            hdTrangThaiId.Value = Request["TrangThaiID"].ToString();
        else
        {
            hdTrangThaiId.Value = "0";
        }
        if (this.IsPostBack)
        {
            hdIsPostBack.Value = "true";
            string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];
            if (eventTarget == "SanPhamPostBack")
            {
                BindTListDmSanPham();

                string passedArgument = Request.Params.Get("__EVENTARGUMENT");
                ddlTenSanPham.SelectedValue = passedArgument;
                ddlTenSanPham_SelectedIndexChanged(null, null);
            }
            else if (eventTarget == "HangSanXuatPostBack")
            {
                BindTListDmHangSanXuat();
                string passedArgument = Request.Params.Get("__EVENTARGUMENT");
                ddlHangSanXuat.SelectedValue = passedArgument;
            }
            else if (eventTarget == "CoQuanDoKiemPostBack")
            {
                BindTListDmCoQuanDoKiem();
                string passedArgument = Request.Params.Get("__EVENTARGUMENT");
                ddlCoQuanDoLuong.SelectedValue = passedArgument;
            }
            //this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(),"checkChange",,true)
        }

        //ngày đo kiểm tối đa bằng ngày hiện tại
        rvCheckDate.MaximumValue = DateTime.Now.ToShortDateString();
        //đặt maxlength
        txtChuyenVienTiepNhan.Attributes.Add("onkeyup", " if (!checkLength('" + txtChuyenVienTiepNhan.ClientID + "', '2000')) return false;");
        txtYKienLanhDao.Attributes.Add("onkeyup", " if (!checkLength('" + txtYKienLanhDao.ClientID + "', '2000')) return false;");
        txtYKienThamDinh.Attributes.Add("onkeyup", " if (!checkLength('" + txtYKienThamDinh.ClientID + "', '2000')) return false;");
        txtNoiDungDoKiem.Attributes.Add("onkeyup", " if (!checkLength('" + txtNoiDungDoKiem.ClientID + "', '2000')) return false;");
        txtNhanXetKhac.Attributes.Add("onkeyup", " if (!checkLength('" + txtNhanXetKhac.ClientID + "', '2000')) return false;");
        txtNoiDungXuLyTruoc.Attributes.Add("onkeyup", " if (!checkLength('" + txtNoiDungXuLy.ClientID + "', '2000')) return false;");
        txtNoiDungXuLy.Attributes.Add("onkeyup", " if (!checkLength('" + txtNoiDungXuLy.ClientID + "', '2000')) return false;");

        //ddlCoQuanDoLuong.Attributes.Add("onChange", "alert(this);checkChange(hdCoQuanDoKiem ,ddlCoQuanDoKiem);");
        //this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "CheckChange", "<script> alert(this);checkChange('" + hdCoQuanDoKiem + "' ,'" + ddlCoQuanDoLuong + "')</script>");
        //querystring:direct=CB_HoSoMoi&HoSoID=TTCB_12_10&SanPhamID=Nokia1&TrangThaiId=1
        if (!IsPostBack)
        {

            hdIsPostBack.Value = "false";
            //Lấy thông tin danh mục
            //danh mục sản phẩm
            BindTListDmSanPham();
            //danh mục hãng sản xuất
            BindTListDmHangSanXuat();
            //danh mục giá trị lô hàng
            BindTListDmGiaTriLoHang();
            //danh mục cơ quan đo kiểm
            BindTListDmCoQuanDoKiem();
            if (Request["HoSoID"] != null && Request["SanPhamID"] != null)
            {
                ManageBredCum(strDanhSachHoSo, Request["HoSoID"].ToString(), strDanhSachSanPham, Request["SanPhamID"].ToString());

                objSanPham = ProviderFactory.SanPhamProvider.GetById(Request["SanPhamID"].ToString());

                HoSo objHoSoTemp = ProviderFactory.HoSoProvider.GetById(Request["HoSoID"].ToString());

                if (objSanPham.HinhThucId == (int)EnHinhThucList.DA_CAP_PHTC)
                {
                    rSoBanTuDanhGia.Visible = false;
                    rNgayTuDanhGia.Visible = false;
                }
                else
                {
                    rSoBanTuDanhGia.Visible = true;
                    rNgayTuDanhGia.Visible = true;
                    txtSoBanTuDanhGia.Text = objSanPham.SoBanTuDanhGia;
                    txtNgayDanhGia.Text = objSanPham.NgayDanhGia != null ? objSanPham.NgayDanhGia.Value.ToShortDateString() : string.Empty; ;
                }
                if (objHoSoTemp != null)
                {
                    ddlGiaTriLoHang.Visible = false;
                    rqfGiaTriLoHang.Visible = false;
                }
                //danh mục tiêu chuẩn
                //BindTListDmTieuChuan(Request["SanPhamID"].ToString());
                if (Request["TrangThaiID"] != null)
                {
                    //set CSS cho sản phẩm
                    #region xét form theo trạng thái sản phẩm
                    if (Request["TrangThaiID"].ToString() == Convert.ToInt32(EnTrangThaiSanPhamList.CHO_XU_LY).ToString())// trang thai cho xu ly
                    {
                        //duoc tao moi
                        blHasCreate = true;
                        lblTrangThai.Text = GetTextStatus();
                        trYKienThamDinh.Style.Add("display", "none");
                        //rSogiayCB.Style.Add("display", "none");
                        SetReadOnly(txtChuyenVienTiepNhan);
                        SetReadOnly(txtYKienLanhDao);
                        SetReadOnly(txtNoiDungXuLyTruoc);
                        btnInBanTiepNhan.Visible = false;
                        btnGuiLanhDao.Visible = false;
                        rblDayDu.Attributes.Add("onclick", "ShowHideKetLuan();");
                        rblHopLe.Attributes.Add("onclick", "ShowHideKetLuan();");
                        rblKetLuan.Attributes.Add("onclick", "ShowHideLePhi();");

                    }
                    else if (Request["TrangThaiID"].ToString() == Convert.ToInt32(EnTrangThaiSanPhamList.CHO_THAM_DINH).ToString())// trang thai tham dinh dong y
                    {
                        lblTrangThai.Text = GetTextStatus();
                        rNoiDungXuLy.Style.Add("display", "none");
                        //rSogiayCB.Style.Add("display", "none");
                        SetReadOnly(txtChuyenVienTiepNhan);
                        SetReadOnly(txtYKienLanhDao);
                        SetReadOnly(txtNoiDungXuLyTruoc);
                        SetReadOnly(txtNhanXetKhac);
                        SetReadOnly(txtYKienThamDinh);
                        SetReadOnly(txtNoiDungXuLy);
                        SetReadOnly(txtNoiDungDoKiem);
                        SetReadOnly(txtKyHieu);
                        SetReadOnly(txtSoBanCongBo);
                        SetReadOnly(txtNgayCongBo);
                        SetReadOnly(txtSoBanTuDanhGia);
                        SetReadOnly(txtNgayDanhGia);
                        pclNgayCongBo.Visible = false;
                        pclNgayDanhGia.Visible = false;
                        SetReadOnly(txtNgayDoKiem);
                        SetReadOnly(txtSoDoKiem);
                        calendarFrom.Visible = false;
                        ddlCoQuanDoLuong.Enabled = false;
                        rqfGiaTriLoHang.Visible = false;
                        ddlGiaTriLoHang.Enabled = false;
                        ddlHangSanXuat.Enabled = false;
                        ddlTenSanPham.Enabled = false;
                        btnCapNhat.Visible = false;
                        btnGuiThamDinh.Visible = false;
                        btnInBanTiepNhan.Visible = false;
                        btnGuiLanhDao.Visible = false;
                        chklstTieuChuan.Enabled = false;
                        rblDayDu.Enabled = false;
                        rblHopLe.Enabled = false;
                        rblKetLuan.Enabled = false;
                        lnkbtnTaoMoiHSX.Visible = false;
                        lnkbtnTaoMoiSP.Visible = false;
                        lnkbtnTaoMoiCQDK.Visible = false;
                        rNoiDungXuLy.Style.Add("display", "none");

                        rqfKetLuan.Visible = false;
                    }
                    else if (Request["TrangThaiID"].ToString() == Convert.ToInt32(EnTrangThaiSanPhamList.THAM_DINH_DONG_Y).ToString())// trang thai tham dinh dong y
                    {
                        lblTrangThai.Text = GetTextStatus();
                        //duoc tao moi
                        blHasCreate = true;
                        //rqfKetLuan.Visible = false;
                        trYKienThamDinh.Style.Add("display", "");
                        rNoiDungXuLy.Style.Add("display", "");
                        //rSogiayCB.Style.Add("display", "none");
                        SetReadOnly(txtChuyenVienTiepNhan);
                        SetReadOnly(txtYKienLanhDao);
                        SetReadOnly(txtNoiDungXuLyTruoc);
                        SetReadOnly(txtYKienThamDinh);
                        btnInBanTiepNhan.Visible = false;
                        btnCapNhat.Visible = false;
                        if (objSanPham.KetLuanId == (int)EnKetLuanList.CAP_BAN_TIEP_NHAN_CB)
                            btnGuiLanhDao.Visible = false;
                        rblDayDu.Attributes.Add("onclick", "ShowHideKetLuan();");
                        rblHopLe.Attributes.Add("onclick", "ShowHideKetLuan();");
                        rblKetLuan.Attributes.Add("onclick", "ShowHideLePhi();");
                    }
                    else if (Request["TrangThaiID"].ToString() == Convert.ToInt32(EnTrangThaiSanPhamList.THAM_DINH_KHONG_DONG_Y).ToString())// trang thai tham dinh dong y
                    {
                        //duoc tao moi
                        blHasCreate = true;

                        lblTrangThai.Text = GetTextStatus();
                        trYKienThamDinh.Style.Add("display", "");
                        rNoiDungXuLy.Style.Add("display", "");
                        //rSogiayCB.Style.Add("display", "none");
                        SetReadOnly(txtChuyenVienTiepNhan);
                        SetReadOnly(txtYKienLanhDao);
                        SetReadOnly(txtNoiDungXuLyTruoc);
                        SetReadOnly(txtYKienThamDinh);
                        btnInBanTiepNhan.Visible = false;
                        btnCapNhat.Visible = false;
                        if (objSanPham.KetLuanId == (int)EnKetLuanList.CAP_BAN_TIEP_NHAN_CB)
                            btnGuiLanhDao.Visible = false;
                        rblDayDu.Attributes.Add("onclick", "ShowHideKetLuan();");
                        rblHopLe.Attributes.Add("onclick", "ShowHideKetLuan();");
                        rblKetLuan.Attributes.Add("onclick", "ShowHideLePhi();");
                    }
                    else if (Request["TrangThaiID"].ToString() == Convert.ToInt32(EnTrangThaiSanPhamList.CHO_PHE_DUYET).ToString())// trang thai cho phe duyet
                    {
                        lblTrangThai.Text = GetTextStatus();
                        trYKienThamDinh.Style.Add("display", "");
                        SetReadOnly(txtChuyenVienTiepNhan);
                        SetReadOnly(txtYKienLanhDao);
                        SetReadOnly(txtNoiDungXuLyTruoc);
                        SetReadOnly(txtNhanXetKhac);
                        SetReadOnly(txtYKienThamDinh);
                        SetReadOnly(txtNoiDungXuLy);
                        SetReadOnly(txtNoiDungDoKiem);
                        rNoiDungXuLy.Style.Add("display", "none");
                        //rSogiayCB.Style.Add("display", "none");
                        ddlCoQuanDoLuong.Enabled = false;
                        rqfGiaTriLoHang.Visible = false;
                        ddlGiaTriLoHang.Enabled = false;
                        ddlHangSanXuat.Enabled = false;
                        ddlTenSanPham.Enabled = false;
                        btnCapNhat.Visible = false;
                        btnGuiLanhDao.Visible = false;
                        chklstTieuChuan.Enabled = false;
                        btnGuiThamDinh.Visible = false;
                        btnInBanTiepNhan.Visible = false;
                        btnInPhieuDanhGia.Visible = false;
                        rblDayDu.Enabled = false;
                        rblHopLe.Enabled = false;
                        rblKetLuan.Enabled = false;
                        //rblThoiHan.Enabled = false;
                        chklstTieuChuan.Enabled = false;
                        lnkbtnTaoMoiHSX.Visible = false;
                        lnkbtnTaoMoiSP.Visible = false;
                        lnkbtnTaoMoiCQDK.Visible = false;
                        SetReadOnly(txtKyHieu);
                        SetReadOnly(txtSoBanCongBo);
                        SetReadOnly(txtNgayCongBo);
                        SetReadOnly(txtSoBanTuDanhGia);
                        SetReadOnly(txtNgayDanhGia);
                        pclNgayCongBo.Visible = false;
                        pclNgayDanhGia.Visible = false;
                        SetReadOnly(txtNgayDoKiem);
                        SetReadOnly(txtSoDoKiem);
                        calendarFrom.Visible = false;
                        rNoiDungXuLy.Style.Add("display", "none");
                        rqfKetLuan.Visible = false;
                    }
                    else if (Request["TrangThaiID"].ToString() == Convert.ToInt32(EnTrangThaiSanPhamList.GD_KHONG_DUYET).ToString())// trang thai o dc phe duyet
                    {
                        //duoc tao moi
                        blHasCreate = true;
                        lblTrangThai.Text = GetTextStatus();
                        trYKienThamDinh.Style.Add("display", "");
                        //rSogiayCB.Style.Add("display", "none");
                        SetReadOnly(txtChuyenVienTiepNhan);
                        SetReadOnly(txtYKienLanhDao);
                        SetReadOnly(txtNoiDungXuLyTruoc);
                        btnInBanTiepNhan.Visible = false;
                        btnGuiLanhDao.Visible = false;
                        rblDayDu.Attributes.Add("onclick", "ShowHideKetLuan();");
                        rblHopLe.Attributes.Add("onclick", "ShowHideKetLuan();");
                        rblKetLuan.Attributes.Add("onclick", "ShowHideLePhi();");
                    }
                    else if (Request["TrangThaiID"].ToString() == Convert.ToInt32(EnTrangThaiSanPhamList.GD_PHE_DUYET).ToString())// trang thai da dc phe duyet
                    {
                        lblTrangThai.Text = GetTextStatus();
                        SetReadOnly(txtChuyenVienTiepNhan);
                        SetReadOnly(txtYKienLanhDao);
                        SetReadOnly(txtNoiDungXuLyTruoc);
                        SetReadOnly(txtNhanXetKhac);
                        SetReadOnly(txtYKienThamDinh);
                        SetReadOnly(txtNoiDungXuLy);
                        SetReadOnly(txtNoiDungDoKiem);
                        ddlCoQuanDoLuong.Enabled = false;
                        rqfGiaTriLoHang.Visible = false;
                        ddlGiaTriLoHang.Enabled = false;
                        ddlHangSanXuat.Enabled = false;
                        ddlTenSanPham.Enabled = false;
                        btnCapNhat.Visible = false;
                        btnGuiThamDinh.Visible = false;
                        btnGuiLanhDao.Visible = false;
                        if (objSanPham.KetLuanId != (int)EnKetLuanList.CAP_BAN_TIEP_NHAN_CB)
                        {
                            btnInBanTiepNhan.Visible = false;
                            btnCapNhat.Visible = true; // Hien thi nut cap nhat de cap nhat so cong van
                            txtSoBTNCV.ReadOnly = false;
                            txtSoBTNCV.Attributes.Add("style", "background-color:#FFFFFF");
                        }
                        chklstTieuChuan.Enabled = false;
                        rblDayDu.Enabled = false;
                        rblHopLe.Enabled = false;
                        rblKetLuan.Enabled = false;
                        chklstTieuChuan.Enabled = false;
                        lnkbtnTaoMoiHSX.Visible = false;
                        lnkbtnTaoMoiSP.Visible = false;
                        lnkbtnTaoMoiCQDK.Visible = false;
                        SetReadOnly(txtKyHieu);
                        SetReadOnly(txtSoBanCongBo);
                        SetReadOnly(txtNgayCongBo);
                        SetReadOnly(txtSoBanTuDanhGia);
                        SetReadOnly(txtNgayDanhGia);
                        pclNgayCongBo.Visible = false;
                        pclNgayDanhGia.Visible = false;
                        SetReadOnly(txtNgayDoKiem);
                        SetReadOnly(txtSoDoKiem);
                        calendarFrom.Visible = false;
                        rNoiDungXuLy.Style.Add("display", "none");
                        btnGuiThamDinh.Style.Add("display", "none");
                        rqfKetLuan.Visible = false;
                    }

                    if (Request["action"] != null && Request["action"].ToString() == "view")
                    {
                        lblTrangThai.Text = GetTextStatus();
                        if (lblTrangThai.Text == string.Empty)
                            lblTrangThai.Text = "XEM THÔNG TIN ĐÁNH GIÁ SẢN PHẨM";

                        SetReadOnly(txtChuyenVienTiepNhan);
                        SetReadOnly(txtYKienLanhDao);
                        SetReadOnly(txtNoiDungXuLyTruoc);
                        SetReadOnly(txtNhanXetKhac);
                        SetReadOnly(txtYKienThamDinh);
                        SetReadOnly(txtNoiDungXuLy);
                        SetReadOnly(txtNoiDungDoKiem);
                        ddlCoQuanDoLuong.Enabled = false;
                        rqfGiaTriLoHang.Visible = false;
                        ddlGiaTriLoHang.Enabled = false;
                        ddlHangSanXuat.Enabled = false;
                        ddlTenSanPham.Enabled = false;
                        btnCapNhat.Visible = false;
                        btnGuiThamDinh.Visible = false;
                        chklstTieuChuan.Enabled = false;
                        rblDayDu.Enabled = false;
                        rblHopLe.Enabled = false;
                        rblKetLuan.Enabled = false;
                        chklstTieuChuan.Enabled = false;
                        lnkbtnTaoMoiHSX.Visible = false;
                        lnkbtnTaoMoiSP.Visible = false;
                        lnkbtnTaoMoiCQDK.Visible = false;
                        SetReadOnly(txtKyHieu);
                        SetReadOnly(txtSoBanCongBo);
                        SetReadOnly(txtNgayCongBo);
                        SetReadOnly(txtSoBanTuDanhGia);
                        SetReadOnly(txtNgayDanhGia);
                        pclNgayCongBo.Visible = false;
                        pclNgayDanhGia.Visible = false;
                        SetReadOnly(txtNgayDoKiem);
                        SetReadOnly(txtSoDoKiem);
                        calendarFrom.Visible = false;
                        SetReadOnly(txtSoBTNCV);

                        rqfKetLuan.Visible = false;
                        rqfGiaTriLoHang.Visible = false;
                    }
                    #endregion
                    LoadDataForPage();

                    lnkbtnTaoMoiSP.Visible = (mUserInfo.IsInRole(EnPermission.QUANLY_DM_SANPHAM) && blHasCreate);
                    lnkbtnTaoMoiHSX.Visible = (mUserInfo.IsInRole(EnPermission.QUANLY_DM_HANGSX) && blHasCreate);
                    lnkbtnTaoMoiCQDK.Visible = (mUserInfo.IsInRole(EnPermission.QUANLY_DM_COQUAN_DOKIEM) && blHasCreate);
                }

                if (rblKetLuan.SelectedValue != ((int)EnKetLuanList.CAP_BAN_TIEP_NHAN_CB).ToString() && mUserInfo.IsNguoiXuLy(objHoSoTemp.Id))
                {
                    txtSoBTNCV.ReadOnly = false;
                    txtSoBTNCV.Attributes.Add("style", "background-color:#FFFFFF");
                }

                if (objSanPham.HinhThucId == (int)EnHinhThucList.DA_CAP_PHTC)
                {
                    trSoDoKiem.Visible = false;
                    trCoQuanKiem.Visible = false;
                    trNoiDungDoKiem.Visible = false;
                    rvCheckDate.IsValid = true;
                }
                else
                {
                    trSoDoKiem.Visible = true;
                    trCoQuanKiem.Visible = true;
                    trNoiDungDoKiem.Visible = true;
                }
                //LongHH
                txtSoGCN.Text = objSanPham.SoGcn;
                //LongHH
            }
            if (Convert.ToInt32(rblKetLuan.SelectedValue) == (int)EnKetLuanList.CAP_BAN_TIEP_NHAN_CB)
            {
                fileUpSoCongVan.Style.Add("Display", "none");
            }
        }
    }

    void WebUI_CB_XuLyHoSo_DanhGia_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
            SetDefaultValue();
    }
    /// <summary>
    /// Thiết lập link cho mapsite
    /// </summary>
    /// <param name="strDanhSachHoSo">Loại Usercontrol của HoSo_QuanLy</param>
    /// <param name="HoSoId">Id Hồ sơ</param>
    /// <param name="strDanhSachSanPham">Loại Usercontrol của HoSoSanPham_QuanLy</param>
    /// <param name="SanPhamId">Id sản phẩm</param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                12/5/2009              
    /// </Modified>
    private void ManageBredCum(string strDanhSachHoSo, string HoSoId, string strDanhSachSanPham, string SanPhamId)
    {
        if (strDanhSachHoSo == "CB_HoSoDi")
        {
            hlDanhSachHoSo.Text = string.Format("Danh sách Hồ Sơ đã gửi").ToUpper();
        }
        else
        {
            hlDanhSachHoSo.Text = string.Format("Danh sách Hồ Sơ đến").ToUpper();
        }
        if (strDanhSachSanPham == "CB_HoSoDi")
        {
            hlDanhSachSanPham.Text = string.Format("Danh sách Sản phẩm đã gửi").ToUpper();
        }
        else
        {
            hlDanhSachSanPham.Text = string.Format("Danh sách Sản phẩm đến").ToUpper();
        }
        hlDanhSachHoSo.NavigateUrl = "CN_HoSo_QuanLy.aspx?UserControl=" + strDanhSachHoSo;
        hlDanhSachSanPham.NavigateUrl = "CB_HoSoSanPham_QuanLy.aspx?UserControl=" + strDanhSachSanPham + "&HoSoId=" + HoSoId;
    }

    void SetValue(Control ctrl)
    {
        string DefaultValue = string.Empty;
        if (ctrl is TextBox)
            DefaultValue = ((TextBox)ctrl).Text;
        if (ctrl is DropDownList)
            DefaultValue = ((DropDownList)ctrl).SelectedValue.ToString();
        if (ctrl is ComboBox)
        {
            foreach (ListItem lstItem in ((ComboBox)ctrl).Items)
            {
                lstItem.Attributes.Add("defaultvalue", lstItem.Selected.ToString().ToLower());
                lstItem.Attributes.Add("class", "defaultvalue");
            }
        }
        if ((ctrl is TextBox) | (ctrl is DropDownList))
        {
            ((WebControl)ctrl).Attributes.Add("defaultvalue", DefaultValue);
            ((WebControl)ctrl).CssClass += " defaultvalue";
        }
        if (ctrl is CheckBoxList)
        {
            foreach (ListItem lstItem in ((CheckBoxList)ctrl).Items)
            {
                lstItem.Attributes.Add("defaultvalue", lstItem.Selected.ToString().ToLower());
                lstItem.Attributes.Add("class", "defaultvalue");
            }
        }
        if (ctrl is RadioButtonList)
        {
            foreach (ListItem lstItem in ((RadioButtonList)ctrl).Items)
            {
                lstItem.Attributes.Add("defaultvalue", lstItem.Selected.ToString().ToLower());
                lstItem.Attributes.Add("class", "defaultvalue");

            }
        }
    }
    void SetDefaultValue()
    {
        foreach (Control ctrl in this.Page.Controls)
        {
            if (ctrl.HasControls())
                SetDefaultValue(ctrl);
            SetValue(ctrl);

        }

    }

    void SetDefaultValue(Control control)
    {
        foreach (Control ctrl in control.Controls)
        {
            if (ctrl.HasControls())
                SetDefaultValue(ctrl);
            string DefaultValue = string.Empty;
            SetValue(ctrl);

        }
    }

    /// <summary>
    /// Lấy Tên trạng thái
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                12/5/2009              
    /// </Modified>
    private string GetTextStatus()
    {
        string strStatus = string.Empty;
        if (Request["TrangThaiID"] != null)
        {
            if (Request["TrangThaiID"].ToString() == Convert.ToInt32(EnTrangThaiSanPhamList.CHO_XU_LY).ToString())
                strStatus = "CHỜ XỬ LÝ";
            else if (Request["TrangThaiID"].ToString() == Convert.ToInt32(EnTrangThaiSanPhamList.CHO_THAM_DINH).ToString())
                strStatus = "CHỜ THẨM ĐỊNH";
            else if (Request["TrangThaiID"].ToString() == Convert.ToInt32(EnTrangThaiSanPhamList.THAM_DINH_DONG_Y).ToString())
                strStatus = "THẨM ĐỊNH ĐỒNG Ý";
            else if (Request["TrangThaiID"].ToString() == Convert.ToInt32(EnTrangThaiSanPhamList.THAM_DINH_KHONG_DONG_Y).ToString())
                strStatus = "THẨM ĐỊNH KHÔNG ĐỒNG Ý";
            else if (Request["TrangThaiID"].ToString() == Convert.ToInt32(EnTrangThaiSanPhamList.GD_KHONG_DUYET).ToString())
                strStatus = "KHÔNG PHÊ DUYỆT";
            else if (Request["TrangThaiID"].ToString() == Convert.ToInt32(EnTrangThaiSanPhamList.GD_PHE_DUYET).ToString())
                strStatus = "ĐÃ PHÊ DUYỆT";
            else if (Request["TrangThaiID"].ToString() == Convert.ToInt32(EnTrangThaiSanPhamList.CHO_PHE_DUYET).ToString())
                strStatus = "CHỜ PHÊ DUYỆT";
            else if (Request["TrangThaiID"].ToString() == Convert.ToInt32(EnTrangThaiSanPhamList.DA_CAP_BAN_TIEP_NHAN_CB).ToString())
                strStatus = "ĐÃ CẤP BẢN TIẾP NHẬN CÔNG BỐ";
            else if (Request["TrangThaiID"].ToString() == Convert.ToInt32(EnTrangThaiSanPhamList.MOI_TAO).ToString())
                strStatus = "MỚI TẠO";
        }

        return strStatus;
    }
    /// <summary>
    /// Lấy dữ liệu khi load lại trang
    /// purpose: tạo ra để tiện khi cần load lại dữ liệu
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                12/5/2009              
    /// </Modified>
    private void LoadDataForPage()
    {
        int intTrangthai = Convert.ToInt32(Request["TrangThaiID"].ToString());
        LoadDetailSanPham(intTrangthai, Request["SanPhamID"].ToString());

        //Gán giá trị tính checkchange
        if (intTrangthai == Convert.ToInt32(EnTrangThaiSanPhamList.THAM_DINH_DONG_Y)
        || intTrangthai == Convert.ToInt32(EnTrangThaiSanPhamList.THAM_DINH_KHONG_DONG_Y))
        {
            hdCoQuanDoKiem.Value = ddlCoQuanDoLuong.SelectedValue;
            hdSanPham.Value = ddlTenSanPham.SelectedValue;
            hdHangSanXuat.Value = ddlHangSanXuat.SelectedValue;
            hdGiaTriLoHang.Value = ddlGiaTriLoHang.SelectedValue;
        }
    }
    /// <summary>
    /// Lấy danh mục sản phẩm
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                12/5/2009              
    /// </Modified>
    private void BindTListDmSanPham()
    {
        TList<DmSanPham> lstDmSanPham = ProviderFactory.DmSanPhamProvider.GetAll();

        ddlTenSanPham.DataValueField = "ID";
        ddlTenSanPham.DataTextField = "TenTiengViet";
        ddlTenSanPham.DataSource = lstDmSanPham;
        ddlTenSanPham.DataBind();

    }
    /// <summary>
    /// Lấy danh mục hãng sản xuất
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                12/5/2009              
    /// </Modified>
    private void BindTListDmHangSanXuat()
    {
        TList<DmHangSanXuat> lstDmHangSanXuat = ProviderFactory.DmHangSanXuatProvider.GetAll();

        ddlHangSanXuat.DataValueField = "ID";
        ddlHangSanXuat.DataTextField = "TenHangSanXuat";
        ddlHangSanXuat.DataSource = lstDmHangSanXuat;
        ddlHangSanXuat.DataBind();
    }
    /// <summary>
    /// Lấy danh mục giá trị lô hàng
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                12/5/2009              
    /// </Modified>
    private void BindTListDmGiaTriLoHang()
    {
        TList<DmLePhi> lstDmLePhi = ProviderFactory.DmLePhiProvider.GetAll();

        ddlGiaTriLoHang.DataValueField = "ID";
        ddlGiaTriLoHang.DataTextField = "GiaTriLoHang";
        ddlGiaTriLoHang.DataSource = lstDmLePhi;
        ddlGiaTriLoHang.DataBind();
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
    /// Lấy danh mục cơ quan đo kiểm
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                12/5/2009              
    /// </Modified>
    private void BindTListDmCoQuanDoKiem()
    {
        TList<DmCoQuanDoKiem> lstDmCoQuanDoKiem = ProviderFactory.DmCoQuanDoKiemProvider.GetAll();
        DmCoQuanDoKiem objNull = new DmCoQuanDoKiem();
        objNull.Id = "-1";
        objNull.TenCoQuanDoKiem = "Chọn Cơ quan đo kiểm ..";
        lstDmCoQuanDoKiem.Add(objNull);

        ddlCoQuanDoLuong.DataValueField = "Id";
        ddlCoQuanDoLuong.DataTextField = "TenCoQuanDoKiem";
        ddlCoQuanDoLuong.DataSource = lstDmCoQuanDoKiem;

        ddlCoQuanDoLuong.DataBind();
        ddlCoQuanDoLuong.SelectedValue = "-1";
    }
    //LongHH
    protected void ddlCoQuanDoLuong_SelectedIndexChanged(object sender, EventArgs e)
    {
        DmCoQuanDoKiem cqdk = ProviderFactory.DmCoQuanDoKiemProvider.GetById(ddlCoQuanDoLuong.SelectedValue);
        if (cqdk != null)
        {
            txtTenTiengAnhCQDK.Text = cqdk.TenTiengAnh;
            txtDiaChiCQDK.Text = cqdk.DiaChi;
            txtSDTCQDK.Text = cqdk.DienThoai;
            //LongHH
            List<String> files = QLCL_Patch.GetFileAttach_CoQuanDoKiem_Nop_HoSo(cqdk.Id);
            if (files.Count > 0)
            {
                StringBuilder sbGIAY_PHEP_KINH_DOANH = new StringBuilder();
                sbGIAY_PHEP_KINH_DOANH.Append("<a href='" + files[0] + "'>File đính kèm</a>");
                lbtnFileDinhKem.Text = sbGIAY_PHEP_KINH_DOANH.ToString();
            }
            //LongHH
        }
        else
        {
            txtTenTiengAnhCQDK.Text = string.Empty;
            txtDiaChiCQDK.Text = string.Empty;
            txtSDTCQDK.Text = string.Empty;
            lbtnFileDinhKem.Text = string.Empty;
        }
    }
    //LongHH
    /// <summary>
    /// bấm nút cập nhật
    /// tiến hành cập nhật dữ liệu
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                12/5/2009              
    /// </Modified>
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        if (Request["HoSoID"] != null && Request["SanPhamID"] != null)
        {
            if (Request["TrangThaiID"] != null)
            {
                if (CountCheckedItem(chklstTieuChuan) > 0)
                {
                    string strSanPhamID = Request["SanPhamID"].ToString();
                    SanPham objSanPham = ProviderFactory.SanPhamProvider.GetById(strSanPhamID);

                    if (Request["TrangThaiID"].ToString() == Convert.ToInt32(EnTrangThaiSanPhamList.CHO_XU_LY).ToString()
                        || Request["TrangThaiID"].ToString() == Convert.ToInt32(EnTrangThaiSanPhamList.THAM_DINH_KHONG_DONG_Y).ToString()
                        || Request["TrangThaiID"].ToString() == Convert.ToInt32(EnTrangThaiSanPhamList.GD_KHONG_DUYET).ToString()
                        || Request["TrangThaiID"].ToString() == Convert.ToInt32(EnTrangThaiSanPhamList.THAM_DINH_DONG_Y).ToString())// trang thai cho xu ly
                    {
                        CapNhat_ChoXuLy(strSanPhamID, blGuiThamDinh);
                    }
                    else if (Request["TrangThaiID"].ToString() == Convert.ToInt32(EnTrangThaiSanPhamList.GD_PHE_DUYET).ToString() && objSanPham.KetLuanId != (int)EnKetLuanList.CAP_GCN)
                    {
                        // Cập nhật số công văn
                        objSanPham.SoBanTiepNhanCb = GetSoCongVan();
                        Cuc_QLCL.Data.TransactionManager _transaction = ProviderFactory.Transaction;
                        ProviderFactory.SanPhamProvider.Save(objSanPham);

                        //Cập nhật công văn đính kèm 
                        try
                        {
                            CapNhatSoCongVan(strSanPhamID, _transaction);
                            _transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            _transaction.Rollback();
                        }
                        btnGuiThamDinh.Visible = false;
                        btnGuiLanhDao.Visible = false;
                        Thong_bao("Cập nhật số công văn thành công");
                    }
                }
                else
                {
                    Thong_bao(this.Page, Resource.msgChuaChonChiTieuApDung);
                }
            }
        }
        ViewState["CapNhat"] = "1";
    }
    /// <summary>
    /// cập nhật đánh giá trạng thái chờ xử lý
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                12/5/2009              
    /// </Modified>
    private void CapNhat_ChoXuLy(string SanPhamID, bool blGuiThamDinh)
    {
        Cuc_QLCL.Data.TransactionManager _transaction = ProviderFactory.Transaction;
        bool blCapNhatThongTin = false;
        bool blCapNhatXuLy = false;
        bool blCapNhatTieuChuan = false;
        bool blCapNhatCongVan = false;
        try
        {
            //Cập nhật số công văn
            blCapNhatCongVan = CapNhatSoCongVan(SanPhamID, _transaction);
            //Cập thông tin sản phẩm
            blCapNhatThongTin = CapnhatThongTinSanPham(SanPhamID, _transaction);
            //Cập nhật tiêu chuẩn
            blCapNhatTieuChuan = CapNhatTieuChuan(SanPhamID, _transaction);
            //Cập nhật tài liệu kỹ thuật
            CapNhatChiTieuKyThuat(SanPhamID);
            //Cập nhật quá trình xử lý
            int intLoaiXuLyID = Convert.ToInt32(EnLoaiXuLyList.DANH_GIA);
            blCapNhatXuLy = CapNhatQuaTringXuLy(SanPhamID, intLoaiXuLyID, _transaction);
            _transaction.Commit();
        }
        catch (Exception ex)
        {
            _transaction.Rollback();
            throw ex;
        }
        finally
        {
            _transaction.Dispose();

            if (blCapNhatThongTin && blCapNhatXuLy && blCapNhatTieuChuan && blCapNhatCongVan)
            {
                if (!blGuiThamDinh)
                    Thong_bao(this.Page, Resource.msgCapNhatThanhCong);
                LoadDataForPage();
            }
            else
            {
                if (!blGuiThamDinh)
                    Thong_bao(this.Page, Resource.msgCapNhatThatBai);
            }
        }
        //}
    }
    /// <summary>
    /// Cập nhật tiêu chuẩn
    /// </summary>
    /// <param name="SanPhamID"></param>
    /// <param name="_transaction"></param>
    /// <returns></returns>
    private bool CapNhatTieuChuan(string SanPhamID, Cuc_QLCL.Data.TransactionManager _transaction)
    {
        string dsMaTieuChuan = string.Empty;
        string dsTenTieuChuan = string.Empty;

        bool blUpdateSuccess = false;
        List<string> lstTieuChuanID = new List<string>();
        for (int i = 0; i <= chklstTieuChuan.Items.Count - 1; i++)
        {
            if (chklstTieuChuan.Items[i].Selected)
                if (!lstTieuChuanID.Contains(chklstTieuChuan.Items[i].Value))
                    lstTieuChuanID.Add(chklstTieuChuan.Items[i].Value);
        }
        if (lstTieuChuanID.Count > 0)
        {
            try
            {
                ProviderFactory.SanPhamTieuChuanApDungProvider.XoaTieuChuanApDungBySanPhamID(SanPhamID, _transaction);
                foreach (string strTieuChuanID in lstTieuChuanID)
                {
                    SanPhamTieuChuanApDung objSanPhamTieuChuanApDung = new SanPhamTieuChuanApDung();
                    objSanPhamTieuChuanApDung.SanPhamId = SanPhamID;
                    objSanPhamTieuChuanApDung.TieuChuanApDungId = strTieuChuanID;
                    objSanPhamTieuChuanApDung.NgayCapNhatSauCung = DateTime.Now;
                    if (objSanPhamTieuChuanApDung.IsValid)
                    {
                        ProviderFactory.SanPhamTieuChuanApDungProvider.Insert(_transaction, objSanPhamTieuChuanApDung);

                        DmTieuChuan objDmTieuChuan = ProviderFactory.DmTieuChuanProvider.GetById(_transaction, strTieuChuanID);
                        if (objDmTieuChuan != null)
                        {
                            dsMaTieuChuan += objDmTieuChuan.MaTieuChuan + ", ";
                            dsTenTieuChuan += objDmTieuChuan.TenTieuChuan + ", ";
                        }
                    }
                }
                blUpdateSuccess = true;
            }
            catch (Exception ex)
            {
                blUpdateSuccess = false;
                _transaction.Rollback();
                throw ex;
            }

            // Cập nhật danh sách tiêu chuẩn của sản phẩm 
            if (!string.IsNullOrEmpty(dsMaTieuChuan))
                dsMaTieuChuan = dsMaTieuChuan.Substring(0, dsMaTieuChuan.Length - 2);
            if (!string.IsNullOrEmpty(dsTenTieuChuan))
                dsTenTieuChuan = dsTenTieuChuan.Substring(0, dsTenTieuChuan.Length - 2);
            SanPham objSP = ProviderFactory.SanPhamProvider.GetById(_transaction, SanPhamID);
            objSP.DsTenTieuChuan = dsTenTieuChuan;
            objSP.DsMaTieuChuan = dsMaTieuChuan;
            ProviderFactory.SanPhamProvider.Update(_transaction, objSP);
        }
        else
            blUpdateSuccess = true;

        return blUpdateSuccess;
    }

    /// <summary>
    /// cập nhật chỉ tiêu kỹ thuật
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                12/5/2009              
    /// </Modified>
    private void CapNhatChiTieuKyThuat(string SanPhamID)
    {
        UpdateTaiLieuDinhKem(SanPhamID, ref FileGiayToTuCachPhapNhan, Convert.ToInt32(EnLoaiTaiLieuList.CHI_TIEU_KY_THUAT_KEM_THEO), ref lnkbtnChiTieuKyThuatKemTheo);
    }
    /// <summary>
    /// cập nhật quá trình xử lý
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                12/5/2009              
    /// </Modified>
    private bool CapNhatQuaTringXuLy(string SanPhamID, int intLoaiXuLyID, Cuc_QLCL.Data.TransactionManager _transaction)
    {
        bool blUpdateSuccess = false;

        QuaTrinhXuLy objQuaTrinhXuLy = new QuaTrinhXuLy();

        objQuaTrinhXuLy.SanPhamId = SanPhamID;
        objQuaTrinhXuLy.LoaiXuLyId = intLoaiXuLyID;
        if (blCapNhatNoiDanhGia)
            objQuaTrinhXuLy.NoiDungXuLy = txtNoiDungXuLy.Text.Trim();
        objQuaTrinhXuLy.NguoiXuLyId = mUserInfo.UserID;
        objQuaTrinhXuLy.NgayXuLy = DateTime.Now;
        if (objQuaTrinhXuLy.IsValid)
        {
            //Cuc_QLCL.Data.TransactionManager objtran = ProviderFactory.Transaction;
            try
            {
                ProviderFactory.QuaTrinhXuLyProvider.Save(_transaction, objQuaTrinhXuLy);
                blUpdateSuccess = true;
                //Xóa trắng ô xử lý
                txtNoiDungXuLy.Text = string.Empty;
                //objtran.Commit();
            }
            catch (Exception ex)
            {
                PageBase_Error(null, null);
                _transaction.Rollback();
                throw ex;
            }
        }
        return blUpdateSuccess;
    }
    /// <summary>
    /// cập nhật thông tin sản phẩm
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                12/5/2009              
    /// </Modified>
    private bool CapnhatThongTinSanPham(string SanPhamID, Cuc_QLCL.Data.TransactionManager _transaction)
    {
        bool UpdateSuccess = false;

        SanPham objSanPhamSave = ProviderFactory.SanPhamProvider.GetById(_transaction, SanPhamID);
        if (objSanPhamSave != null)
        {
            objSanPhamSave.SanPhamId = ddlTenSanPham.SelectedValue;
            DmSanPham objDanhMucSp = ProviderFactory.DmSanPhamProvider.GetById(_transaction, ddlTenSanPham.SelectedValue);
            objSanPhamSave.NhomSanPhamId = objDanhMucSp.NhomSanPhamId;
            objSanPhamSave.KyHieu = txtKyHieu.Text;
            objSanPhamSave.SoBanCongBo = txtSoBanCongBo.Text.Trim();
            if (!string.IsNullOrEmpty(txtNgayCongBo.Text.Trim()))
                objSanPhamSave.NgayCongBo = Convert.ToDateTime(txtNgayCongBo.Text);
            if (txtSoBanTuDanhGia.Visible)
            {
                objSanPhamSave.SoBanTuDanhGia = txtSoBanTuDanhGia.Text;
                if (txtNgayDanhGia.Text.Length > 1)
                    objSanPhamSave.NgayDanhGia = Convert.ToDateTime(txtNgayDanhGia.Text);
                else
                    objSanPhamSave.NgayDanhGia = null;
            }

            objSanPhamSave.HangSanXuatId = ddlHangSanXuat.SelectedValue;
            objSanPhamSave.SoDoKiem = txtSoDoKiem.Text.Trim();
            //cơ quan đo kiểm
            if (!string.IsNullOrEmpty(ddlCoQuanDoLuong.SelectedValue) && ddlCoQuanDoLuong.SelectedValue != "-1")
                objSanPhamSave.CoQuanDoKiemId = ddlCoQuanDoLuong.SelectedValue;
            else
                objSanPhamSave.CoQuanDoKiemId = null;
            //hồ sơ đầy đủ
            if (!string.IsNullOrEmpty(rblDayDu.SelectedValue))
                objSanPhamSave.DayDu = Convert.ToBoolean(Convert.ToInt32(rblDayDu.SelectedValue));
            else
                objSanPhamSave.DayDu = null;
            //hồ sơ hợp lệ
            if (!string.IsNullOrEmpty(rblHopLe.SelectedValue))
                objSanPhamSave.HopLe = Convert.ToBoolean(Convert.ToInt32(rblHopLe.SelectedValue));
            else
                objSanPhamSave.HopLe = null;

            if (txtNgayDoKiem.Text.Trim() == string.Empty)
                objSanPhamSave.NgayDoKiem = null;
            else
                objSanPhamSave.NgayDoKiem = Convert.ToDateTime(txtNgayDoKiem.Text);
            if (rblKetLuan.Enabled)
                objSanPhamSave.KetLuanId = Convert.ToInt32(rblKetLuan.SelectedValue);


            //lệ phí
            HoSo objHoSoNguonGoc = ProviderFactory.HoSoProvider.GetById(_transaction, objSanPhamSave.HoSoId);

            objSanPhamSave.LePhiId = null;
            //thời hạn
            if (objSanPhamSave.KetLuanId == Convert.ToInt32(EnKetLuanList.CAP_BAN_TIEP_NHAN_CB))
            {
                //if (Convert.ToInt32(objHoSoNguonGoc.NguonGocId) == Convert.ToInt32(EnNguonGocList.NK_PHI_MAU_DICH))
                //    objSanPhamSave.ThoiHanId = Convert.ToInt32(EnThoiHanList.MOT_LAN_SU_DUNG);
                //else
                objSanPhamSave.ThoiHanId = GetThoiHanId(_transaction, objSanPhamSave.NhomSanPhamId);
            }
            else
                objSanPhamSave.ThoiHanId = null;

            objSanPhamSave.NoiDungDoKiem = txtNoiDungDoKiem.Text.Trim();
            //Nhận xét khác
            objSanPhamSave.NhanXetKhac = txtNhanXetKhac.Text.Trim();
            //Số công văn; trạng thái
            if (rblKetLuan.Visible && rblKetLuan.SelectedValue != Convert.ToInt32(EnKetLuanList.CAP_BAN_TIEP_NHAN_CB).ToString())
            {
                objSanPhamSave.SoBanTiepNhanCb = GetSoCongVan();
                objSanPhamSave.NgayKyDuyet = DateTime.Now;
            }
            else
            {
                //objSanPhamSave.TrangThaiId = Convert.ToInt32(Request["TrangThaiId"].ToString());
                if (objHoSoNguonGoc.HoSoMoi != null && objHoSoNguonGoc.HoSoMoi.Value == false)
                {
                    if (txtSoBTNCV.Text.Contains("CB"))
                        objSanPhamSave.SoBanTiepNhanCb = txtSoBTNCV.Text;
                    else
                    {
                        string Stt = txtSoBTNCV.Text.Trim();

                        if (Stt.Length == 1)
                            Stt = "000" + Stt;
                        else if (Stt.Length == 2)
                            Stt = "00" + Stt;
                        else if (Stt.Length == 3)
                            Stt = "0" + Stt;
                        else if (Stt.Length >= 4)
                            Stt = Stt.Substring(0, 4);
                        objSanPhamSave.SoBanTiepNhanCb = Stt + "/CB-" + mUserInfo.MaTrungTam;
                    }
                }
                else
                    objSanPhamSave.SoBanTiepNhanCb = null;
            }

            if (rblDayDu.SelectedIndex == 1 || rblHopLe.SelectedIndex == 1)
            {
                objSanPhamSave.ThoiHanId = null;
                objSanPhamSave.LePhiId = null;
            }
            string strTrangThaiID = string.Empty;
            if (Request["TrangThaiID"] != null)
                strTrangThaiID = Request["TrangThaiID"];

            if (strTrangThaiID == Convert.ToInt32(EnTrangThaiHoSoList.THAM_DINH_DONG_Y).ToString()
                || strTrangThaiID == Convert.ToInt32(EnTrangThaiHoSoList.THAM_DINH_KHONG_DONG_Y).ToString())
            {
                objSanPhamSave.PhaiThamDinhLai = true;
            }
            if (objSanPhamSave.IsValid)
            {
                // Cuc_QLCL.Data.TransactionManager objTransaction = ProviderFactory.Transaction;
                try
                {

                    ProviderFactory.SanPhamProvider.Save(_transaction, objSanPhamSave);
                    string strSanPhamIdUpdate = objSanPhamSave.Id;
                    UpdateSuccess = true;

                    if (objSanPhamSave.KetLuanId != (int)EnKetLuanList.CAP_BAN_TIEP_NHAN_CB && mUserInfo.IsNguoiXuLy(objHoSoNguonGoc.Id))
                    {
                        txtSoBTNCV.ReadOnly = false;
                        txtSoBTNCV.Attributes.Add("style", "background-color:#FFFFFF");
                    }
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();
                    PageBase_Error(null, null);
                    throw ex;
                }
            }
        }
        return UpdateSuccess;
    }
    protected void btnBoQua_Click(object sender, EventArgs e)
    {
        string strdirect = "CB_HoSoDen";
        if (Request["UserControl"] != null)
            strdirect = Request["UserControl"];
        Response.Redirect("CB_HoSoSanPham_QuanLy.aspx?UserControl=" + strdirect + "&HoSoId=" + Request["HoSoId"].ToString());
    }
    protected void lnkbtnTaoMoiSP_Click(object sender, EventArgs e)
    {
        HoSo objHs = DataRepository.HoSoProvider.GetById(Request["HoSoId"].ToString());
        string LoaiHinhChungNhan = objHs.LoaiHoSo == (int)LoaiHoSo.ChungNhanHopQuy ? "1" : "2";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "Popup_TaoMoiSP",
                        "<script>popCenter('DM_SanPham_ChiTiet.aspx?PostBack=CB_XuLyHoSo_DanhGia&LoaiHinhChungNhan=" + LoaiHinhChungNhan + "','Popup_TaoMoiSP',950,400);</script>");
    }

    protected void lnkbtnTaoMoiHSX_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "Popup_TaoMoiSP",
                           "<script>popCenter('DM_HangSanXuat_ChiTiet.aspx?PostBack=CB_XuLyHoSo_DanhGia','Popup_TaoMoiSP',570,380);</script>");
    }
    protected void lnkbtnTaoMoiCoQuanDoLuong(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "Popup_TaoMoiCQ",
                                      "<script>popCenter('DM_CoQuanDoKiem_ChiTiet.aspx?PostBack=CB_XuLyHoSo_DanhGia','Popup_TaoMoiCQ',570,280);</script>");

    }
    /// <summary>
    /// Lấy giá trị nhóm sản phẩm
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                12/5/2009              
    /// </Modified>
    protected void ddlTenSanPham_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strNhomSanPham = string.Empty;
        DmSanPham objSanPhamSelect = ProviderFactory.DmSanPhamProvider.GetById(ddlTenSanPham.SelectedValue);
        if (objSanPhamSelect != null)
        {
            DmNhomSanPham objDmNhomSanPham = ProviderFactory.DmNhomSanPhamProvider.GetById(objSanPhamSelect.NhomSanPhamId);
            if (objDmNhomSanPham != null)
            {
                strNhomSanPham = objDmNhomSanPham.MaNhom;
                strNhomSanPhamID = objDmNhomSanPham.Id;

                //tính lệ phí
                HoSo objHS = ProviderFactory.HoSoProvider.GetById(Request["HoSoID"].ToString());

                txtLePhi.Text = string.Format("{0:0,0}", Convert.ToInt32(ConfigurationManager.AppSettings["PhiCongBoHQ"].ToString()));
            }
        }

        chklstTieuChuan.Items.Clear();
        BindTListDmTieuChuan(null, ddlTenSanPham.SelectedValue);
        //hiện nút gửi thẩm định
        #region Ẩn hiện nút gửi thẩm định
        int iTrangThai = 0;
        if (Request["TrangThaiId"] != null)
        {
            iTrangThai = Convert.ToInt32(Request["TrangThaiId"].ToString());
            if (iTrangThai == Convert.ToInt32(EnTrangThaiSanPhamList.THAM_DINH_DONG_Y)
                || iTrangThai == Convert.ToInt32(EnTrangThaiSanPhamList.THAM_DINH_KHONG_DONG_Y))
            {
                bool blDayDu = false;
                bool blHopLe = false;
                bool blKetLuan = false;

                if (!string.IsNullOrEmpty(rblDayDu.SelectedValue) && rblDayDu.SelectedValue == "1")
                    blDayDu = true;
                if (!string.IsNullOrEmpty(rblHopLe.SelectedValue) && rblHopLe.SelectedValue == "1")
                    blHopLe = true;
                if (!string.IsNullOrEmpty(rblKetLuan.SelectedValue)
                    && rblKetLuan.SelectedValue == Convert.ToInt32(EnKetLuanList.CAP_BAN_TIEP_NHAN_CB).ToString())
                    blKetLuan = true;
                if (blHopLe && blDayDu && blKetLuan)
                    btnGuiThamDinh.Style.Add("display", "");
                else
                    btnGuiThamDinh.Style.Add("display", "none");
            }
        }
        #endregion

    }
    /// <summary>
    /// Lấy thông tin chi tiết sản phẩm
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                13/5/2009         
    /// TuanVM                 27/05/2009               Sửa ẩn hiện giá trị lô hàng theo nguồn gốc trong bảng hồ sơ + hiển thị mã nhóm sản phẩm thay cho tên nhóm sản phẩm 
    /// </Modified>
    private void LoadDetailSanPham(int intTrangThai, string strSanPhamId)
    {
        SanPham objSanPhamDetail = ProviderFactory.SanPhamProvider.GetById(strSanPhamId);
        DmNhomSanPham objDmNhomSanPham = ProviderFactory.DmNhomSanPhamProvider.GetById(objSanPhamDetail.NhomSanPhamId);
        HoSo objHoSo = ProviderFactory.HoSoProvider.GetById(objSanPhamDetail.HoSoId);
        if (objSanPhamDetail != null && objDmNhomSanPham != null && objHoSo != null)
        {
            //Có phải thẩm định đồng ý hoặc thẩm định ko đồng ý không? nếu có thì xét trạng thái
            if (Request["TrangThaiID"] != null &&
                (Request["TrangThaiID"] == Convert.ToInt32(EnTrangThaiHoSoList.THAM_DINH_KHONG_DONG_Y).ToString()
                || Request["TrangThaiID"] == Convert.ToInt32(EnTrangThaiHoSoList.THAM_DINH_DONG_Y).ToString()))
            {
                //trạng thái của nút gửi thẩm định
                if (objSanPhamDetail.PhaiThamDinhLai != null && objSanPhamDetail.PhaiThamDinhLai.Value)
                    btnGuiThamDinh.Visible = true;
                else
                    btnGuiThamDinh.Visible = false;
            }
            //lưu nhóm sản phẩm
            strNhomSanPhamID = objSanPhamDetail.NhomSanPhamId;
            //tiêu chuẩn áp dụng
            BindTListDmTieuChuan(objSanPhamDetail.Id, objSanPhamDetail.SanPhamId);

            //Hình thức
            int intHinhThucID = 0;
            if (objHoSo.HinhThucId != null)
                intHinhThucID = Convert.ToInt32(objHoSo.HinhThucId);
            if (intHinhThucID == Convert.ToInt32(EnHinhThucList.TU_NGUYEN))
                rChiTieuKyThuatKemTheo.Style.Add("display", "");
            else
                rChiTieuKyThuatKemTheo.Style.Add("display", "none");

            //  lấy danh sách tài liệu
            TList<TaiLieuDinhKem> lstTaiLieuDinhKem;
            lstTaiLieuDinhKem = ProviderFactory.TaiLieuDinhKemProvider.GetBySanPhamId(objSanPhamDetail.Id);

            foreach (TaiLieuDinhKem objTaiLieu in lstTaiLieuDinhKem)
            {
                string FilePath = objTaiLieu.TenFile.Trim();
                string FileName = FilePath.Substring(FilePath.LastIndexOf("/") + 1, FilePath.Length - FilePath.LastIndexOf("/") - 1);



                if (objTaiLieu.LoaiTaiLieuId == (int)EnLoaiTaiLieuList.BAN_CONG_BO)
                {
                    StringBuilder sbBanCongBo = new StringBuilder();
                    sbBanCongBo.Append("<a href='" + FilePath + "'>" + EntityHelper.GetEnumTextValue(EnLoaiTaiLieuList.BAN_CONG_BO) + "</a>");
                    lbtnBAN_CONG_BO.Text = sbBanCongBo.ToString() + " | ";
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
                    StringBuilder sbBanTuDanhGia = new StringBuilder();
                    sbBanTuDanhGia.Append("<a href='" + FilePath + "'>" + EntityHelper.GetEnumTextValue(EnLoaiTaiLieuList.BAN_TU_DANH_GIA) + "</a>");
                    lbtnBAN_TU_DANH_GIA.Text = sbBanTuDanhGia.ToString() + " | ";
                }
                if (objTaiLieu.LoaiTaiLieuId == (int)EnLoaiTaiLieuList.CONG_VAN)
                {
                    hlCongVan.Visible = true;
                    lnkbtnXoaSCV.Visible = true;
                    fileUpSoCongVan.Visible = false;
                    hlCongVan.NavigateUrl = @"~/" + objTaiLieu.TenFile.Trim();
                }

            }
            //Chỉ tiêu kỹ thuật kèm theo
            TList<TaiLieuDinhKem> lstTaiLieu = ProviderFactory.TaiLieuDinhKemProvider.GetBySanPhamId(objSanPhamDetail.Id);

            foreach (TaiLieuDinhKem objTaiLieuDinhKem in lstTaiLieu)
            {
                if (objTaiLieuDinhKem.LoaiTaiLieuId == Convert.ToInt32(EnLoaiTaiLieuList.CHI_TIEU_KY_THUAT_KEM_THEO))
                {
                    FileGiayToTuCachPhapNhan.Visible = false;
                    lnkbtnChiTieuKyThuatKemTheo.PostBackUrl = objTaiLieuDinhKem.TenFile;
                }

            }
            lnkbtnChiTieuKyThuatKemTheo.Visible = !FileGiayToTuCachPhapNhan.Visible;

            #region Thẩm định đồng ý; Chờ phê duyệt; Giám đốc không duyệt
            if (intTrangThai == Convert.ToInt32(EnTrangThaiSanPhamList.THAM_DINH_DONG_Y)
                || intTrangThai == Convert.ToInt32(EnTrangThaiSanPhamList.CHO_PHE_DUYET)
                || intTrangThai == Convert.ToInt32(EnTrangThaiSanPhamList.GD_KHONG_DUYET)
                || intTrangThai == Convert.ToInt32(EnTrangThaiSanPhamList.GD_PHE_DUYET)
                || intTrangThai == Convert.ToInt32(EnTrangThaiSanPhamList.THAM_DINH_KHONG_DONG_Y)
                || intTrangThai == Convert.ToInt32(EnTrangThaiSanPhamList.CHO_XU_LY)
                || intTrangThai == Convert.ToInt32(EnTrangThaiSanPhamList.CHO_PHE_DUYET)
                || intTrangThai == Convert.ToInt32(EnTrangThaiSanPhamList.CHO_THAM_DINH)
                || (Request["action"] != null && Request["action"].ToString() == "view"))
            {

                //tên sản phẩm
                if (ddlTenSanPham.Items.FindByValue(objSanPhamDetail.SanPhamId) != null)
                {
                    ddlTenSanPham.SelectedIndex = -1;
                    ddlTenSanPham.Items.FindByValue(objSanPhamDetail.SanPhamId).Selected = true;
                }
                //Hãng sản xuất
                if (ddlHangSanXuat.Items.FindByValue(objSanPhamDetail.HangSanXuatId) != null)
                {
                    ddlHangSanXuat.SelectedIndex = -1;
                    ddlHangSanXuat.Items.FindByValue(objSanPhamDetail.HangSanXuatId).Selected = true;
                }
                //ký hiệu
                txtKyHieu.Text = objSanPhamDetail.KyHieu;
                txtSoBanCongBo.Text = objSanPhamDetail.SoBanCongBo;
                txtNgayCongBo.Text = objSanPhamDetail.NgayCongBo != null ? objSanPhamDetail.NgayCongBo.Value.ToShortDateString() : string.Empty;
                //trạng thái sản phẩm
                txtTrangThai.Text = ToStandardString(lblTrangThai.Text);

                // kết luận
                int intKetLuan = 0;
                if (objSanPhamDetail.KetLuanId != null)
                    intKetLuan = Convert.ToInt32(objSanPhamDetail.KetLuanId);
                if (rblKetLuan.Items.FindByValue(intKetLuan.ToString()) != null)
                {
                    rblKetLuan.SelectedIndex = -1;
                    rblKetLuan.Items.FindByValue(intKetLuan.ToString()).Selected = true;
                }

                rGiaTriLoHang.Style.Add("display", "none");

                txtLePhi.Text = string.Format("{0:0,0}", Convert.ToInt32(ConfigurationManager.AppSettings["PhiCongBoHQ"].ToString()));

                string strYKienChiDao = string.Empty;
                string strLanhDao = string.Empty;

                TList<PhanCong> lstPhanCong = ProviderFactory.PhanCongProvider.GetByHoSoId(objHoSo.Id);
                foreach (PhanCong obj in lstPhanCong)
                {
                    SysUser objSysUser = ProviderFactory.SysUserProvider.GetById(obj.NguoiPhanCong);
                    if (objSysUser != null)
                        strLanhDao = objSysUser.FullName;
                    strYKienChiDao += strLanhDao + " : " + obj.NgayPhanCong + ": " + obj.YkienCuaLanhDao + "\r\n";
                }

                //Ý kiến thẩm định //Nội dung xử lý trước
                TList<QuaTrinhXuLy> lstQuaTrinhXuLy = ProviderFactory.QuaTrinhXuLyProvider.GetBySanPhamId(objSanPhamDetail.Id);
                string strYKienThamDinh = string.Empty;
                string strNoiDungXuLyTruoc = string.Empty;
                string strChuyenVienTiepNhan = string.Empty;
                foreach (QuaTrinhXuLy obj in lstQuaTrinhXuLy)
                {
                    SysUser objSysUser = ProviderFactory.SysUserProvider.GetById(obj.NguoiXuLyId);
                    //Nếu xử lý là xử lý thẩm định thì lấy nội dung xử lý
                    if (obj.LoaiXuLyId == Convert.ToInt32(EnLoaiXuLyList.THAM_DINH))
                        strYKienThamDinh += objSysUser.FullName + " : " + obj.NgayXuLy + ": " + obj.NoiDungXuLy + "\r\n";
                    //lấy nội dung đánh giá
                    if (obj.LoaiXuLyId == Convert.ToInt32(EnLoaiXuLyList.DANH_GIA))
                        strNoiDungXuLyTruoc += objSysUser.FullName + " : " + obj.NgayXuLy + ": " + obj.NoiDungXuLy + "\r\n";
                    //lấy nội dung tiếp nhận
                    if (obj.LoaiXuLyId == Convert.ToInt32(EnLoaiXuLyList.TIEP_NHAN_HO_SO))
                    {
                        // Neu chuyen vien xu ly chinh la nguoi tiep nhan ho so
                        if (mUserInfo.IsNguoiTiepNhan(objHoSo.Id))
                        {
                            strChuyenVienTiepNhan += objSysUser.FullName + " : " + obj.NgayXuLy + ": " + obj.NoiDungXuLy + "\r\n";
                        }
                        else
                        {
                            // Neu chuyen vien tiep nhan la nguoi Them moi san pham vao ho so
                            if (objSysUser.Id == mUserInfo.UserID)
                            {
                                strNoiDungXuLyTruoc += objSysUser.FullName + " : " + obj.NgayXuLy + ": " + obj.NoiDungXuLy + "\r\n";
                            }
                            else
                            {
                                strChuyenVienTiepNhan += objSysUser.FullName + " : " + obj.NgayXuLy + ": " + obj.NoiDungXuLy + "\r\n";
                            }
                        }
                    }

                    // Lấy nội dung phê duyệt
                    if (obj.LoaiXuLyId == Convert.ToInt32(EnLoaiXuLyList.PHE_DUYET))
                    {
                        strYKienChiDao += objSysUser.FullName + " : " + obj.NgayXuLy + ": " + obj.NoiDungXuLy + "\r\n";
                    }
                }

                //Chuyên viên tiếp nhận
                if (strChuyenVienTiepNhan.Length >= 2)
                    strChuyenVienTiepNhan = strChuyenVienTiepNhan.Remove(strChuyenVienTiepNhan.Length - 2, 2);
                txtChuyenVienTiepNhan.Text = strChuyenVienTiepNhan;
                //Nội dung xử lý trước
                if (strNoiDungXuLyTruoc.Length >= 2)
                    strNoiDungXuLyTruoc = strNoiDungXuLyTruoc.Remove(strNoiDungXuLyTruoc.Length - 2, 2);
                txtNoiDungXuLyTruoc.Text = strNoiDungXuLyTruoc;
                //Ý kiến thẩm định
                if (strYKienThamDinh.Length >= 2)
                    strYKienThamDinh = strYKienThamDinh.Remove(strYKienThamDinh.Length - 2, 2);
                txtYKienThamDinh.Text = strYKienThamDinh;
               
                //Ý kiến chỉ đạo
                if (strYKienChiDao.Length >= 2)
                    strYKienChiDao = strYKienChiDao.Remove(strYKienChiDao.Length - 2, 2);
                txtYKienLanhDao.Text = strYKienChiDao;
                //số giấy công bố
                if (!string.IsNullOrEmpty(objSanPhamDetail.SoBanTiepNhanCb))
                    txtSoBTNCV.Text = objSanPhamDetail.SoBanTiepNhanCb;
                else
                {
                    if (objHoSo.HoSoMoi != null && objHoSo.HoSoMoi.Value && objSanPhamDetail.KetLuanId == (int)EnKetLuanList.CAP_BAN_TIEP_NHAN_CB)
                    {
                        txtSoBTNCV.Text = "Số sinh tự động";
                        hdIsHoSoMoi.Value = "1";
                    }
                    else
                    {
                        txtSoBTNCV.Text = string.Empty;
                        hdIsHoSoMoi.Value = "0";
                    }
                }

                hdSoCongVan.Value = txtSoBTNCV.Text;
                //legend nội dung đánh giá
                //*số đo kiểm
                txtSoDoKiem.Text = objSanPhamDetail.SoDoKiem;
                //*ngày đo kiểm
                if (objSanPhamDetail.NgayDoKiem != null)
                    txtNgayDoKiem.Text = Convert.ToDateTime(objSanPhamDetail.NgayDoKiem).ToShortDateString();
                //*Cơ quan đo lường
                if (ddlCoQuanDoLuong.Items.FindByValue(objSanPhamDetail.CoQuanDoKiemId) != null)
                {
                    ddlCoQuanDoLuong.SelectedIndex = -1;
                    ddlCoQuanDoLuong.Items.FindByValue(objSanPhamDetail.CoQuanDoKiemId).Selected = true;
                    //LongHH
                    DmCoQuanDoKiem cqdk = ProviderFactory.DmCoQuanDoKiemProvider.GetById(ddlCoQuanDoLuong.SelectedValue);
                    if (cqdk != null)
                    {
                        txtTenTiengAnhCQDK.Text = cqdk.TenTiengAnh;
                        txtDiaChiCQDK.Text = cqdk.DiaChi;
                        txtSDTCQDK.Text = cqdk.DienThoai;
                        //LongHH
                        List<String> files = QLCL_Patch.GetFileAttach_CoQuanDoKiem_Nop_HoSo(cqdk.Id);
                        if (files.Count > 0)
                        {
                            StringBuilder sbGIAY_PHEP_KINH_DOANH = new StringBuilder();
                            sbGIAY_PHEP_KINH_DOANH.Append("<a href='" + files[0] + "'>File đính kèm</a>");
                            lbtnFileDinhKem.Text = sbGIAY_PHEP_KINH_DOANH.ToString();
                        }
                        //LongHH
                    }
                    else
                    {
                        txtTenTiengAnhCQDK.Text = string.Empty;
                        txtDiaChiCQDK.Text = string.Empty;
                        txtSDTCQDK.Text = string.Empty;
                        lbtnFileDinhKem.Text = string.Empty;
                    }
                    //LongHH
                }
                //* hợp lệ
                bool blHopLe = false;
                if (objSanPhamDetail.HopLe != null)
                    blHopLe = Convert.ToBoolean(objSanPhamDetail.HopLe);
                if (rblHopLe.Items.FindByValue(Convert.ToInt32(blHopLe).ToString()) != null)
                {
                    rblHopLe.SelectedIndex = -1;
                    rblHopLe.Items.FindByValue(Convert.ToInt32(blHopLe).ToString()).Selected = true;
                }
                //* đầy đủ
                bool blDayDu = false;
                if (objSanPhamDetail.DayDu != null)
                    blDayDu = Convert.ToBoolean(objSanPhamDetail.DayDu);
                if (rblDayDu.Items.FindByValue(Convert.ToInt32(blDayDu).ToString()) != null)
                {
                    rblDayDu.SelectedIndex = -1;
                    rblDayDu.Items.FindByValue(Convert.ToInt32(blDayDu).ToString()).Selected = true;
                }

                //*Nội dung đo kiểm
                if (!string.IsNullOrEmpty(objSanPhamDetail.NoiDungDoKiem))
                    txtNoiDungDoKiem.Text = objSanPhamDetail.NoiDungDoKiem;
                else
                    txtNoiDungDoKiem.Text = string.Empty;
                //*Nhận xét khác
                txtNhanXetKhac.Text = objSanPhamDetail.NhanXetKhac;
                if (objHoSo.HoSoMoi != null && !objHoSo.HoSoMoi.Value)
                {
                    txtSoBTNCV.ReadOnly = false;
                    txtSoBTNCV.Attributes.Add("style", "background-color:#FFFFFF");
                }
                else
                {
                    if (rblKetLuan.SelectedValue == ((int)EnKetLuanList.CAP_BAN_TIEP_NHAN_CB).ToString())
                        SetReadOnly(txtSoBTNCV);
                }

            }
            #endregion

        }
    }
    /// <summary>
    /// Lấy xâu chuẩn
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                13/5/2009              
    /// </Modified>
    private string ToStandardString(string str)
    {
        char[] lstChar = str.Trim().ToLower().ToCharArray();
        string strStandard = string.Empty;
        for (int i = 0; i < lstChar.Length; i++)
        {
            if (i == 0)
            {
                lstChar[i] = lstChar[i].ToString().ToUpper().ToCharArray()[0];
            }
            if (lstChar[i] == ' ')
            {
                lstChar[i + 1] = lstChar[i + 1].ToString().ToUpper().ToCharArray()[0];
            }
            strStandard += lstChar[i].ToString();
        }
        return strStandard;
    }

    /// <summary>
    /// bỏ check của CheckBoxList
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                13/5/2009              
    /// </Modified>
    private void ClearChecked(CheckBoxList objCheckBoxList)
    {
        for (int i = 0; i < objCheckBoxList.Items.Count; i++)
        {
            if (objCheckBoxList.Items[i].Selected)
                objCheckBoxList.Items[i].Selected = false;
        }
    }
    protected void btnGuiThamDinh_Click(object sender, EventArgs e)
    {
        int iTrangThai = 0;
        if (Request["TrangThaiId"] != null)
        {
            iTrangThai = Convert.ToInt32(Request["TrangThaiId"].ToString());
        }
        //nếu có confirm thì xác định trạng thái theo điều kiện và tiến hành cập nhật nếu điều kiện đúng
        if (hdConfirm.Value != null && hdConfirm.Value.ToString() == "true" && (
            iTrangThai == (int)EnTrangThaiSanPhamList.GD_KHONG_DUYET
            || iTrangThai == (int)EnTrangThaiSanPhamList.THAM_DINH_DONG_Y
            || iTrangThai == (int)EnTrangThaiSanPhamList.THAM_DINH_KHONG_DONG_Y
            || iTrangThai == (int)EnTrangThaiSanPhamList.CHO_XU_LY)
            && btnCapNhat.Visible)
        {
            blGuiThamDinh = true;
            btnCapNhat_Click(null, null);
            blGuiThamDinh = false;
        }
        //Xác định trường hdThamDinh có giá trị là true không và trạng thái của sản phẩm có đúng theo điều kiện ko. nếu đúng thì cập nhật
        else if (hdThamDinh.Value != null && hdThamDinh.Value == "true" && (iTrangThai == (int)EnTrangThaiSanPhamList.THAM_DINH_DONG_Y
            || iTrangThai == (int)EnTrangThaiSanPhamList.THAM_DINH_KHONG_DONG_Y))
        {
            blCapNhatNoiDanhGia = true;
            blGuiThamDinh = true;
            btnCapNhat_Click(null, null);
            blGuiThamDinh = false;
        }
        else
        {
            blCapNhatNoiDanhGia = false;
            blGuiThamDinh = true;
            btnCapNhat_Click(null, null);
            blGuiThamDinh = false;
        }


        if (Request["HoSoID"] != null && Request["SanPhamID"] != null)
        {
            objSanPham = ProviderFactory.SanPhamProvider.GetById(Request["SanPhamID"].ToString());
            //Cập nhật lại trạng thái sản phẩm
            if (objSanPham != null)
            {
                objSanPham.TrangThaiId = Convert.ToInt32(EnTrangThaiSanPhamList.CHO_THAM_DINH);
                if (objSanPham.IsValid)
                {
                    try
                    {
                        ProviderFactory.SanPhamProvider.Save(objSanPham);
                        Thong_bao(this.Page, Resource.msgGuiSanPhamThanhCong, "CB_HoSoSanPham_QuanLy.aspx?UserControl=CB_HoSoDen&dirtect=CB_HoSoDen&HoSoId=" + Request["HoSoId"].ToString());
                    }
                    catch (Exception ex)
                    {
                        PageBase_Error(null, null);
                        throw ex;
                    }
                }

            }
        }
    }

    /// <summary>
    /// Lấy số công văn
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                13/5/2009              
    /// </Modified>
    private string GetSoCongVan()
    {
        string strSoCongVan = string.Empty;

        string Stt = txtSoBTNCV.Text.Trim();
        if (Stt == "Số sinh tự động")
        {
            txtSoBTNCV.Text = string.Empty;
            return string.Empty;
        }

        if (Stt.Contains("/"))
            strSoCongVan = txtSoBTNCV.Text.Trim();
        else
        {
            if (Stt.Length == 1)
                Stt = "000" + Stt;
            else if (Stt.Length == 2)
                Stt = "00" + Stt;
            else if (Stt.Length == 3)
                Stt = "0" + Stt;
            else if (Stt.Length >= 4)
                Stt = Stt.Substring(0, 4);

            if (string.IsNullOrEmpty(Stt))
                strSoCongVan = string.Empty;
            else
                strSoCongVan = Stt + "/" + mUserInfo.MaTrungTam;
        }
        txtSoBTNCV.Text = strSoCongVan;
        return strSoCongVan;
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
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    public void UpdateTaiLieuDinhKem(string IDSanPham, ref FileUpload fu, int LoaiTaiLieuID, ref LinkButton lb)
    {
        string TenFile = string.Empty;
        string FilePath = string.Empty;
        string ServerMapth = Server.MapPath("FileUpLoad\\");
        ServerMapth = ServerMapth.ToLower();
        ServerMapth = ServerMapth.Replace("\\webui", "");
        Cuc_QLCL.Data.TransactionManager transaction = ProviderFactory.Transaction;

        if (fu.HasFile)
        {
            FilePath = ServerMapth + fu.FileName;
            TenFile = ResolveUrl("~/FileUpLoad/") + fu.FileName;
            try
            {
                //neu nhu chua co tai lieu dinh kem voi san pham thi them moi
                TaiLieuDinhKem tl = new TaiLieuDinhKem();
                if (lb.PostBackUrl.Length == 0)
                {
                    if (!File.Exists(FilePath))
                        fu.PostedFile.SaveAs(FilePath);
                    tl = TaiLieuDinhKem.CreateTaiLieuDinhKem("", IDSanPham, LoaiTaiLieuID, TenFile, DateTime.Now, null);

                }
                //Neu nhu da co va  thay doi
                else
                {
                    if (!File.Exists(FilePath))
                        fu.PostedFile.SaveAs(FilePath);
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
                    if (tailieuId.Length > 0)
                    {
                        tl = ProviderFactory.TaiLieuDinhKemProvider.GetById(tailieuId.Trim());
                        if (tl != null)
                        {
                            string FileName = tl.TenFile;
                            FileName = FileName.Substring(FileName.LastIndexOf("/") + 1, FileName.Length - FileName.LastIndexOf("/") - 1);
                            FilePath = ServerMapth + FileName;
                            DeleteFile(FilePath);
                            tl.TenFile = TenFile;
                        }
                    }
                }

                ProviderFactory.TaiLieuDinhKemProvider.Save(transaction, tl);
                transaction.Commit();
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
        else
        {
            TaiLieuDinhKem tl = new TaiLieuDinhKem();
            //Nếu có file bị xóa thì tiến hành xóa trong CSDL và file vật lý trên Server
            if (lb.Visible == false && lb.PostBackUrl.Length > 0)
            {
                try
                {
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
                        }

                    }
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
    }
    /// <summary>
    /// Xóa file vật lý trên Server
    /// </summary>
    /// <param name="filename"></param>
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
    /// Đếm số item checkboxlist đã check
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                13/5/2009              
    /// </Modified>
    private int CountCheckedItem(CheckBoxList chklst)
    {
        int iCount = 0;
        if (chklst.Items.Count > 0)
        {
            for (int i = 0; i <= chklst.Items.Count - 1; i++)
            {
                if (chklst.Items[i].Selected)
                    iCount++;
            }
        }
        return iCount;
    }
    /// <summary>
    /// Lấy giá trị lô hàng
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                13/5/2009              
    /// </Modified>
    protected void ddlGiaTriLoHang_SelectedIndexChanged(object sender, EventArgs e)
    {
        DmLePhi objGiaTriLoHang = ProviderFactory.DmLePhiProvider.GetById(ddlGiaTriLoHang.SelectedValue);
        if (objGiaTriLoHang != null)
            txtLePhi.Text = string.Format("{0:0,0}.000", objGiaTriLoHang.LePhi);
        //hiện nút gửi thẩm định
        #region Ẩn hiện nút gửi thẩm định
        int iTrangThai = 0;
        if (Request["TrangThaiId"] != null)
        {
            iTrangThai = Convert.ToInt32(Request["TrangThaiId"].ToString());
            if (iTrangThai == Convert.ToInt32(EnTrangThaiSanPhamList.THAM_DINH_DONG_Y)
                || iTrangThai == Convert.ToInt32(EnTrangThaiSanPhamList.THAM_DINH_KHONG_DONG_Y))
            {
                bool blDayDu = false;
                bool blHopLe = false;
                bool blKetLuan = false;

                if (!string.IsNullOrEmpty(rblDayDu.SelectedValue) && rblDayDu.SelectedValue == "1")
                    blDayDu = true;
                if (!string.IsNullOrEmpty(rblHopLe.SelectedValue) && rblHopLe.SelectedValue == "1")
                    blHopLe = true;

                if (blHopLe && blDayDu && blKetLuan)
                    btnGuiThamDinh.Style.Add("display", "");
                else
                    btnGuiThamDinh.Style.Add("display", "none");
            }
        }
        #endregion
    }
    /// <summary>
    /// Cập nhật sản phẩm trước khi in
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                13/5/2009              
    /// </Modified>
    protected void btnInPhieuDanhGia_Click(object sender, EventArgs e)
    {
        if (hdConfirm.Value != null && hdConfirm.Value.ToString() == "true" && btnCapNhat.Visible)
        {
            if (txtNoiDungXuLy.Enabled)
            {
                blGuiThamDinh = true;
                btnCapNhat_Click(null, null);
                blGuiThamDinh = false;
            }
        }
        string strHoSoId = string.Empty;
        if (Request["HoSoId"] != null)
            strHoSoId = Request["HoSoId"].ToString();

        string strSanPhamId = string.Empty;
        if (Request["SanPhamId"] != null)
            strSanPhamId = Request["SanPhamId"].ToString();

        ClientScript.RegisterClientScriptBlock(this.GetType(), "CB_XuLyHoSo_DanhGia_BanTiepNhan",
                "<script>popCenter('../ReportForm/DieuKienInBaoCao.aspx?HoSoID=" + strHoSoId + "&SanPhamId=" + strSanPhamId + "&LoaiBaoCao=CBPhieuDanhGia" + "','CBPhieuDanhGia',450,300);</script>");
    }
    /// <summary>
    /// Kiểm tra sản phẩm có tồn tại hay không
    /// </summary>
    /// <param name="SanPhamId"></param>
    /// <returns></returns>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Hùngnv          30/05/2009             Tạo mới
    /// </Modifield>
    public bool CheckSanPhamExist(string SanPhamId, string oldID)
    {
        string HoSoId = string.Empty;
        if (Request["HoSoId"] != null)
            HoSoId = Request["HoSoId"].ToString();
        TList<SanPham> listSP = ProviderFactory.SanPhamProvider.GetByHoSoId(HoSoId);
        foreach (SanPham sp in listSP)
        {
            if (sp.SanPhamId == SanPhamId && sp.SanPhamId != oldID)
                return true;
        }
        return false;
    }
    /// <summary>
    /// Cập nhật sản phẩm trước khi in
    /// in giấy công bố
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                13/5/2009              
    /// </Modified>
    protected void btnInBanTiepNhan_Click(object sender, EventArgs e)
    {
        //thằng này khi đã được in là khi được phê duyệt rồi==> không cần cập nhật
        //blGuiThamDinh = true;
        //btnCapNhat_Click(null, null);
        //blGuiThamDinh = false;

        string strHoSoId = string.Empty;
        if (Request["HoSoID"] != null)
            strHoSoId = Request["HoSoID"].ToString();

        string strSanPhamId = string.Empty;
        if (Request["SanPhamID"] != null)
            strSanPhamId = Request["SanPhamID"].ToString();

        ClientScript.RegisterClientScriptBlock(this.GetType(), "CB_XuLyHoSo_DanhGia_BanTiepNhan",
                "<script>popCenter('../ReportForm/DieuKienInBaoCao.aspx?HoSoID=" + strHoSoId + "&SanPhamId=" + strSanPhamId + "&LoaiBaoCao=BanTiepNhan" + "','CBPhieuDanhGia',450,300);</script>");
    }
    /// <summary>
    /// Lấy lệ phí theo nhóm sản phẩm
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                13/5/2009              
    /// </Modified>
    private string GetLePhi(string strNhomSanPhamId)
    {
        string strLePhi = "0";
        DmNhomSanPham objNhomSanPham = ProviderFactory.DmNhomSanPhamProvider.GetById(strNhomSanPhamId);
        if (objNhomSanPham != null)
        {
            strLePhi = string.Format("{0:0,0}", objNhomSanPham.MucLePhi);
        }
        return strLePhi;
    }
    /// <summary>
    /// Lấy thời hạn theo nhóm sản phẩm
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                13/5/2009              
    /// </Modified>
    private int GetThoiHanId(string strNhomSanPhamId)
    {
        int intThoiHanId = 0;
        DmNhomSanPham objNhomSanPham = ProviderFactory.DmNhomSanPhamProvider.GetById(strNhomSanPhamId);
        if (objNhomSanPham != null)
        {
            intThoiHanId = objNhomSanPham.ThoiHanGcn;
        }
        return intThoiHanId;
    }
    /// <summary>
    /// Lấy thời hạn theo nhóm sản phẩm
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                13/5/2009              
    /// </Modified>
    private int GetThoiHanId(Cuc_QLCL.Data.TransactionManager _transaction, string strNhomSanPhamId)
    {
        int intThoiHanId = 0;
        DmNhomSanPham objNhomSanPham = ProviderFactory.DmNhomSanPhamProvider.GetById(_transaction, strNhomSanPhamId);
        if (objNhomSanPham != null)
        {
            intThoiHanId = objNhomSanPham.ThoiHanGcn;
        }
        return intThoiHanId;
    }
    /// <summary>
    /// Lấy nội dung đánh giá tạm thời
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                13/5/2009              
    /// </Modified>
    private string GetNoiDungDoKiemTamThoi()
    {
        string strNoiDungDoKiemTamThoi = string.Empty;
        if (chklstTieuChuan.Items.Count > 0)
        {
            for (int i = 0; i <= chklstTieuChuan.Items.Count - 1; i++)
            {
                if (chklstTieuChuan.Items[i].Selected)
                {
                    strNoiDungDoKiemTamThoi += chklstTieuChuan.Items[i].Text + " (" + chklstTieuChuan.Items[i].Value + "); ";
                }
            }
        }
        return strNoiDungDoKiemTamThoi;
    }
    /// <summary>
    /// Lấy nội dung đánh giá tạm thời khi thay đổi lựa chọn tiêu chuẩn
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                13/5/2009              
    /// </Modified>
    protected void chklstTieuChuan_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string strStringRePlace = string.Empty;
        //strStringRePlace = chklstTieuChuan.Items[chklstTieuChuan.SelectedIndex].Text + " (" + chklstTieuChuan.Items[chklstTieuChuan.SelectedIndex].Value + "); ";
        //if (chklstTieuChuan.Items[chklstTieuChuan.SelectedIndex].Selected)
        //    txtNoiDungDoKiem.Text = strStringRePlace + txtNoiDungDoKiem.Text;
        //else
        //    txtNoiDungDoKiem.Text = txtNoiDungDoKiem.Text.Replace(strStringRePlace, "");
    }
    /// <summary>
    /// Lấy nội dung đánh giá tạm thời khi thay đổi lựa chọn tiêu chuẩn
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                13/5/2009              
    /// </Modified>
    private string[] ConvertToArray(string strConvert, char interuptChar)
    {
        string[] arrString = new string[100];
        int iIndex = 0;
        for (int i = 0; i <= strConvert.Length - 1; i++)
        {
            string tem = string.Empty;
            if (strConvert[i] != interuptChar)
                tem += strConvert[i].ToString();
            else
            {
                if (!string.IsNullOrEmpty(tem))
                {
                    arrString.SetValue(tem, iIndex);
                    iIndex++;
                }
            }
        }
        return arrString;
    }
    /// <summary>
    /// Upload số công văn
    /// </summary>
    /// <param name="strFileName"></param>
    /// <returns></returns>
    bool UploadSoCongVan(ref string strFileName, string SanPhamId)
    {
        // Lấy định dạng file
        string fileStyle = fileUpSoCongVan.PostedFile.FileName.Substring(fileUpSoCongVan.PostedFile.FileName.LastIndexOf("."));

        // Đặt lại tên file theo định dạng chuẩn
        string strDuongDanVaTenFile = SanPhamId + "_CongVanTraLoi" + fileStyle;

        string strDuongDanUpload = ConfigurationManager.AppSettings["UploadFolder"].ToString();
        string strDuongDan = Server.MapPath(strDuongDanUpload).Replace("\\WebUI", "");
        double maxFileLength = Convert.ToDouble(ConfigurationManager.AppSettings["MaxFileUpLoadSize"].ToString());
        if (strDuongDanVaTenFile == string.Empty)
        {
            //Chưa nhập file cần nhập liệu
            //Thong_bao(this.Page, Resource.msgChuaSCV);
            return true;
        }
        else
        {
            try
            {
                string strStringFileName = string.Empty;
                if (CheckFileExtension(fileUpSoCongVan, ref strStringFileName))
                {
                    string strMaTrungTam = string.Empty;
                    if (mUserInfo.TrungTam != null)
                        strMaTrungTam = mUserInfo.TrungTam.MaTrungTam;

                    strDuongDanVaTenFile = strDuongDan + SanPhamId + "_CongVanTraLoi" + fileStyle; ;
                    if (checkExistFile(strDuongDanUpload, SanPhamId + "_CongVanTraLoi" + fileStyle))
                    {
                        FileInfo fileInfo = new FileInfo(strDuongDanVaTenFile);
                        fileInfo.Delete();
                    }

                    if (!CheckFileLength(maxFileLength, fileUpSoCongVan))
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo"
                       , "<script>alert('Dung lượng file quá lớn')</script>");
                        return false;
                    }
                    else
                    {
                        fileUpSoCongVan.PostedFile.SaveAs(strDuongDanVaTenFile);
                        strFileName = strDuongDanUpload + SanPhamId + "_CongVanTraLoi" + fileStyle;
                        hlCongVan.Visible = true;
                        fileUpSoCongVan.Visible = false;
                        lnkbtnXoaSCV.Visible = false;
                        hlCongVan.NavigateUrl = @"~/" + strFileName;
                        return true;
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo"
                   , "<script>alert('Chỉ cho phép file có định dạng (*.jpg,*.doc,*.docx, *.txt, *.pdf,)')</script>");
                    return false;
                }
            }
            catch (Exception err)
            {
                return false;
            }
        }

        return false;
    }
    /// <summary>
    /// kiểm tra loại file
    /// </summary>
    /// <param name="fileUP"></param>
    /// <param name="strFileName"></param>
    /// <returns></returns>
    private bool CheckFileExtension(FileUpload fileUP, ref string strFileName)
    {
        if (fileUP.HasFile)
        {
            string fileExtension =
                System.IO.Path.GetExtension(fileUP.FileName).ToLower();
            String[] allowedExtensions ={ ".txt", ".pdf", ".doc", ".docx", ".jpg" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    strFileName = fileExtension;
                    return true;
                }
            }
        }
        return false;
    }
    /// <summary>
    /// Kiểm tra độ dài cho phép của file
    /// </summary>
    /// <param name="mFileLength"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    public bool CheckFileLength(double mFileLength, FileUpload file)
    {
        bool bolCheck = true;
        if ((float)(file.PostedFile.ContentLength / 1024) > mFileLength)
            bolCheck = false;
        return bolCheck;
    }
    /// <summary>
    /// Kiểm tra xem file có tồn tại trong đường dẫn không
    /// </summary>
    /// <param name="strDuongDan"></param>
    /// <param name="strTenFile"></param>
    /// <returns></returns>
    private bool checkExistFile(string strDuongDan, string strTenFile)
    {
        strDuongDan = Server.MapPath(strDuongDan).Replace("\\WebUI", "");
        if (!Directory.Exists(strDuongDan))
        {
            Directory.CreateDirectory(strDuongDan);
            return false;
        }
        else
        {
            DirectoryInfo Parent = new DirectoryInfo(strDuongDan);
            FileInfo[] files = Parent.GetFiles();
            foreach (FileInfo file in files)
            {
                if (file.Name == strTenFile)
                    return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Cập nhật số công văn
    /// </summary>
    /// <param name="SanPhamID" type="string"></param>
    /// <param name="transactionManager">Transaction Manager</param>
    /// <returns></returns>
    /// <modify>
    /// Author          Date        comment
    /// HungNV          ???         Tạo mới
    /// TuấnVM          Sửa         Nếu kết luận là cấp BTN thì không cần upload công văn. Nếu người dùng không chọn file thì cũng không upload
    /// </modify>
    private bool CapNhatSoCongVan(string SanPhamID, Cuc_QLCL.Data.TransactionManager transactionManager)
    {
        // Nếu kết luận là cấp BTN thì không cần upload công văn
        if (rblKetLuan.SelectedValue == Convert.ToInt32(EnKetLuanList.CAP_BAN_TIEP_NHAN_CB).ToString())
            return true;
        bool Success = false;
        //Kiểm tra nếu có thì ko không làm gì cả
        bool blCoCongVan = false;
        TList<TaiLieuDinhKem> lstTaiLieuDinhKem = ProviderFactory.TaiLieuDinhKemProvider.GetBySanPhamId(transactionManager, SanPhamID);
        foreach (TaiLieuDinhKem obj in lstTaiLieuDinhKem)
        {
            if (obj.LoaiTaiLieuId == Convert.ToInt32(EnLoaiTaiLieuList.CONG_VAN))
            {
                blCoCongVan = true;
                break;
            }
        }
        if (blCoCongVan)
            return true;

        // Nếu người dùng không chọn file đính kèm thì return true, và không cập nhật fille đính kèm
        if (string.IsNullOrEmpty(fileUpSoCongVan.PostedFile.FileName))
            return true;
        string strFilename = string.Empty;

        if (UploadSoCongVan(ref strFilename, SanPhamID))
        {
            TaiLieuDinhKem objTaiLieuDinhKem = new TaiLieuDinhKem();
            objTaiLieuDinhKem.Id = string.Empty;
            objTaiLieuDinhKem.SanPhamId = SanPhamID;
            objTaiLieuDinhKem.LoaiTaiLieuId = Convert.ToInt32(EnLoaiTaiLieuList.CONG_VAN);
            objTaiLieuDinhKem.TenFile = strFilename;
            try
            {
                ProviderFactory.TaiLieuDinhKemProvider.Save(transactionManager, objTaiLieuDinhKem);
                Success = true;
            }
            catch (Exception ex)
            {
                //xóa file đã up
                FileInfo fileInfo = new FileInfo(strFilename);
                if (fileInfo.Exists)
                    fileInfo.Delete();

                Success = false;
                PageBase_Error(null, null);
                transactionManager.Rollback();
                throw ex;
            }
        }
        lnkbtnXoaSCV.Visible = true;
        return Success;
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
    protected void btnGuiLanhDao_Click(object sender, EventArgs e)
    {
        try
        {
            string strSanPhamID = Request["SanPhamID"].ToString();
            SanPham objSP = ProviderFactory.SanPhamProvider.GetById(strSanPhamID);
            objSP.TrangThaiId = (int)EnTrangThaiSanPhamList.CHO_PHE_DUYET;
            ProviderFactory.SanPhamProvider.Save(objSP);
            Thong_bao(this.Page, "Gửi thành công", "CB_HoSoSanPham_QuanLy.aspx?UserControl=CB_HoSoDen&dirtect=CB_HoSoDen&HoSoId=" + Request["HoSoId"].ToString());
        }
        catch
        {
        }
    }
   
}