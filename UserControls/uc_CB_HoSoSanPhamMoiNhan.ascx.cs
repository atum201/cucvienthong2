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
using Cuc_QLCL.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

/// <summary>
/// Hồ sơ sản phẩm
/// </summary>
///<Modified>
/// Author      Data        Comments    
/// QuanNM      12/05/2009    
///</Modified>
public partial class UserControls_uc_CB_HoSoSanPhamMoiNhan : System.Web.UI.UserControl
{
    private string HoSoId = string.Empty;
    private int LoaiHoSo = 0;
    private string strUserControl = string.Empty;
    private string strUserControlHS = string.Empty;
    //Xoa thong bao phi
    List<string> deleteSuccess = new List<string>();
    List<string> deleteFail = new List<string>();
    //Xoa san pham
    List<string> deleteSPSuccess = new List<string>();
    List<string> deleteSPFail = new List<string>();
    //Gui thong bao phi
    List<string> sendSuccess = new List<string>();
    List<string> sendFail = new List<string>();

    /// <summary>
    /// Hiển thị thông tin
    /// </summary>
    ///<Modified>
    /// Author      Data        Comments    
    /// Truongtv    12/05/2009  TrườngTV
    /// Quannm      18/05/2009  Edited
    ///</Modified>
    protected void Page_Load(object sender, EventArgs e)
    {
        HoSoId = Request["HosoID"];
        LoaiHoSo = DataRepository.HoSoProvider.GetById(HoSoId).LoaiHoSo;
        strUserControl = Request["UserControl"];
        strUserControlHS = Request["UserControlHS"];
        imgThemMoiTBP.OnClientClick = "popCenter('CB_ThongBaoPhi_TaoMoi.aspx?Action=Add&HoSoID=" + HoSoId + "','DM_TrungTam_ChiTiet', 950,450); return false;";
        lnkThemMoiTBP.OnClientClick = "popCenter('CB_ThongBaoPhi_TaoMoi.aspx?Action=Add&HoSoID=" + HoSoId + "','DM_TrungTam_ChiTiet', 950,450); return false;";
        if (!IsPostBack)
        {
            ManageBreadCum(HoSoId, strUserControl);
            BindChiTietHoSo();
            LayTatCaSanPhamTheoQuyenDangNhap();
            BindThongBaoPhiGrid();
            EnableThongBaoPhiButton();
            EnableSanPhamButton();
            KiemTraTrangThaiSanPham(HoSoId);
        }
        //Chuyển link cho danh sách sản phẩm theo trạng thái

        if ((Request["SanPhamID"] != null) && (Request["UserControl"] != null))
            if (Request["UserControl"] == "CB_HoSoDen")
                ReturnLinkDirect(Request["TrangThaiID"].ToString(), Request["HosoID"].ToString(), Request["SanPhamID"].ToString());

        // Nếu hồ sơ đã gửi lưu trữ thì không hiển thị các nút chức năng
        if (Request["TrangThaiID"] != null)
        {
            if (ProviderFactory.HoSoProvider.GetById(HoSoId).TrangThaiId == (int)EnTrangThaiHoSoList.CHO_LUU_TRU
                || !((PageBase)this.Page).mUserInfo.IsNguoiXuLy(HoSoId))
            {
                imgGuiTBP.Visible = false;
                imgThemMoiTBP.Visible = false;
                ImgBtnThemmoiSanPham.Visible = false;
                imgXoaSanPham.Visible = false;
                ImgBtnCompleteHS.Visible = false;
                imgXoaTBP.Visible = false;
                lnkGuiTBP.Visible = false;
                lnkXoaSanPham.Visible = false;
                lnkXoaTBP.Visible = false;
                lnkThemMoiSanPham.Visible = false;
                lnkThemMoiTBP.Visible = false;
                LinkBtnCompleteHS.Visible = false;
            }
        }
    }
    /// <summary>
    /// Hiển thị chi tiết hồ sơ
    /// </summary>
    ///<Modified>
    /// Author      Data            Comments    
    /// Truongtv    12/05/2009      Create new
    ///</Modified>
    private void BindChiTietHoSo()
    {
        HoSo hsInform = ProviderFactory.HoSoProvider.GetById(HoSoId);
        lblSoHoSo.Text = Server.HtmlEncode(hsInform.SoHoSo);
        lblTrangThai.Text = EntityHelper.GetEnumTextValue((EnTrangThaiHoSoList)hsInform.TrangThaiId);

        lblSoCongVanDen.Text = Server.HtmlEncode(hsInform.SoCongVanDen);
        lblNguoiNop.Text = Server.HtmlEncode(hsInform.NguoiNopHoSo);
        if (hsInform.NhanHoSoTuId != null)
            lblLoaiHinhGui.Text = EntityHelper.GetEnumTextValue((EnNhanHoSoTuList)hsInform.NhanHoSoTuId);

        if (hsInform.NgayTiepNhan != null)
        {
            lblNgayTiepNhan.Text = ((DateTime)hsInform.NgayTiepNhan).ToShortDateString();
        }
        DataTable nguoiTD = ProviderFactory.HoSoProvider.LayNguoiThamDinh(HoSoId, LoaiHoSo);
        lblNguoiThamDinh.Text = nguoiTD.Rows[0][1].ToString();


        //lblFax.Text = Server.HtmlEncode(hsInform.Fax);
        lblDienThoai.Text = Server.HtmlEncode(hsInform.DienThoai);
        lblEmail.Text = Server.HtmlEncode(hsInform.Email);
        int? ob = hsInform.HinhThucId;

        SysUser user = ProviderFactory.SysUserProvider.GetById(hsInform.NguoiTiepNhanId);
        if (user != null)
            lblNguoiTiepNhan.Text = user.FullName;
        DmDonVi dmDonVi = ProviderFactory.DmDonViProvider.GetById(hsInform.DonViId);
        if (dmDonVi != null)
        {
            lblDonViNopHoSo.Text = Server.HtmlEncode(dmDonVi.TenTiengViet);
            //LongHH
            lblTenTiengAnh.Text = Server.HtmlEncode(dmDonVi.TenTiengAnh);
            lblDiaChi.Text = Server.HtmlEncode(dmDonVi.DiaChi);
            lblMaSoThue.Text = Server.HtmlEncode(dmDonVi.MaSoThue);
            //LongHH
        }
            
            

        if (hsInform.NguonGocId != null)
        {
            lblNguonGoc.Text = EntityHelper.GetEnumTextValue((EnNguonGocList)hsInform.NguonGocId);
        }

        lblYKienTiepNhan.Text = Server.HtmlEncode(hsInform.Luuy);
    }
    /// <summary>
    /// Lấy danh sách sản phẩm của hồ sơ theo quyền đăng nhập
    /// </summary>
    /// <Modified>
    /// Author      Date            Comments    
    /// Quannm      16/05/2009      Create new
    ///</Modified>
    private void LayTatCaSanPhamTheoQuyenDangNhap()
    {
        //((PageBase)this.Page).mUserInfo.StartWith = "01";
        DataTable dtbSanPham = ProviderFactory.SanPhamProvider.LayTatCaSanPhamTheoQuyenDangNhap(HoSoId, ((PageBase)this.Page).mUserInfo.UserID, ((PageBase)this.Page).mUserInfo.GetPermissionList("02"));
        gvSanPham.DataSource = dtbSanPham;
        gvSanPham.DataBind();
    }
    /// <summary>
    /// Danh sách thông báo phí của hồ sơ nếu có
    /// </summary>
    /// <Modified>
    /// Author      Date            Comments    
    /// Quannm      16/05/2009      Create new
    ///</Modified>
    private void BindThongBaoPhiGrid()
    {
        DataTable dtThongBaoPhi = new DataTable();
        dtThongBaoPhi = ProviderFactory.ThongBaoLePhiProvider.GetThongBaoLePhiByHoSoID(HoSoId, ((PageBase)this.Page).mUserInfo.GetPermissionList("01"), ((PageBase)this.Page).mUserInfo.UserID);
        gvPhi.DataSource = dtThongBaoPhi;
        gvPhi.DataBind();
    }
    /// <summary>
    /// Đổi màu bản ghi nếu DaDoc field = false
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 13/5/2009              
    /// </Modified>
    protected void gvSanPham_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "CheckDaDoc")) == false)
            {
                e.Row.CssClass = "unread rowitem";
            }

            //chi cho phep sua thong tin cua cac san pham o trang thai: Cho xu ly, tham dinh dong y, tham dinh khong dong y, giam doc khong phe duyet
            if ((Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiSanPhamList.CHO_XU_LY)
                    || (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiSanPhamList.THAM_DINH_DONG_Y)
                    || (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiSanPhamList.THAM_DINH_KHONG_DONG_Y)
                    || (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiSanPhamList.GD_KHONG_DUYET)
                )
            {
                Control div = e.Row.FindControl("View");
                div.Visible = true;
            }
            HtmlGenericControl lnkTrangThai = new System.Web.UI.HtmlControls.HtmlGenericControl("A");
            lnkTrangThai.InnerText = EntityHelper.GetEnumTextValue((EnTrangThaiSanPhamList)(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")))) ;
            StringBuilder sbCongVan = new StringBuilder();
            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiSanPhamList.GD_PHE_DUYET)//(DataBinder.Eval(e.Row.DataItem, "MoTa") == "Giám đốc phê duyệt")
            {
                lnkTrangThai.Attributes.Add("href", "../ReportForm/HienBaoCao.aspx?HoSoID=" + Request["HoSoID"].ToString() + "&SanPhamId=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&LoaiBaoCao=BanTiepNhan&format=Word");
            }
            e.Row.Cells[4].Controls.Add(lnkTrangThai);
        }
    }
    /// <summary>
    /// Phân trang
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    ///     Author      Date          Comments
    ///     QuanNM      18/05/2009    Tạo mới
    /// </Modified>
    protected void gvSanPham_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSanPham.PageIndex = e.NewPageIndex;
        LayTatCaSanPhamTheoQuyenDangNhap();
    }
    /// <summary>
    /// Phân trang
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    ///     Author      Date          Comments
    ///     QuanNM      18/05/2009    Tạo mới
    /// </Modified>
    protected void gvPhi_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPhi.PageIndex = e.NewPageIndex;
        BindThongBaoPhiGrid();
    }
    /// <summary>
    /// Kiểm tra user có quyền tạo mới thông báo phí hay không?
    /// </summary>
    /// <param name="Permission"></param>
    /// <Modified>
    /// Author      Date            Comments    
    /// Quannm      16/05/2009      Create new
    ///</Modified>
    private void EnableThongBaoPhiButton()
    {
        if (((PageBase)this.Page).mUserInfo.IsNguoiXuLy(HoSoId))
        {
            //link
            lnkThemMoiTBP.Visible = true;
            lnkGuiTBP.Visible = true;
            lnkXoaTBP.Visible = true;
            //anh
            imgThemMoiTBP.Visible = true;
            imgXoaTBP.Visible = true;
            imgGuiTBP.Visible = true;
        }
        else
        {
            //link
            lnkThemMoiTBP.Visible = false;
            lnkGuiTBP.Visible = false;
            lnkXoaTBP.Visible = false;
            //anh
            imgThemMoiTBP.Visible = false;
            imgXoaTBP.Visible = false;
            imgGuiTBP.Visible = false;
        }
    }
    /// <summary>
    /// Kiểm tra user có quyền tạo mới sản phẩm hay không?
    /// </summary>
    /// <param name="Permission"></param>
    /// <Modified>
    /// Author      Date            Comments    
    /// Quannm      16/05/2009      Create new
    ///</Modified>
    private void EnableSanPhamButton()
    {
        if (((PageBase)this.Page).mUserInfo.IsNguoiXuLy(HoSoId))
        {
            lnkThemMoiSanPham.Visible = !CheckHoSoDayDu(HoSoId);
            ImgBtnThemmoiSanPham.Visible = !CheckHoSoDayDu(HoSoId);

            lnkXoaSanPham.Visible = true;
            imgXoaSanPham.Visible = true;
        }
        else
        {
            lnkThemMoiSanPham.Visible = false;
            lnkXoaSanPham.Visible = false;
            ImgBtnThemmoiSanPham.Visible = false;
            imgXoaSanPham.Visible = false;
        }
    }
    /// <summary>
    /// Chuyển trang cho từng trạng thái của Hồ sơ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// TuanVM                 11/5/2009              
    /// </Modified>
    private void ReturnLinkDirect(string strTrangThaiID, string HoSoID, string SanPhamID)
    {
        string link = "";
        int TrangThaiID;
        if ((strTrangThaiID != string.Empty) && (strTrangThaiID != null))
        {
            //Chuyển trạng thái chưa đọc sang đã đọc của sản phẩm
            ProviderFactory.SanPhamProvider.ChangeStatusDaDoc(((PageBase)this.Page).mUserInfo.UserID, SanPhamID, "Add");
            TrangThaiID = Convert.ToInt32(strTrangThaiID);
            switch (TrangThaiID)
            {
                case (int)EnTrangThaiSanPhamList.CHO_THAM_DINH:
                    link = "CB_ThamDinhHoSo.aspx?direct=CB_HoSoMoi&HoSoID=" + HoSoID + "&SanPhamID=" + SanPhamID + "&TrangThaiId=" + TrangThaiID + "&UserControlHS=" + strUserControlHS + "&UserControl=" + Request["UserControl"];
                    break;
                case (int)EnTrangThaiSanPhamList.CHO_PHE_DUYET:
                    link = "CB_PheDuyetHoSo.aspx?direct=CB_HoSoMoi&HoSoID=" + HoSoID + "&SanPhamID=" + SanPhamID + "&TrangThaiId=" + TrangThaiID + "&UserControlHS=" + strUserControlHS + "&UserControl=" + Request["UserControl"];
                    break;
                default:    // Chờ xử lý, thẩm định đồng ý, thẩm định không đồng ý, GĐ phê duyệt, không phê duyệt
                    link = "CB_XuLyHoSo_DanhGia.aspx?HoSoID=" + HoSoID + "&SanPhamID=" + SanPhamID + "&TrangThaiId=" + TrangThaiID + "&UserControlHS=" + strUserControlHS + "&UserControl=" + Request["UserControl"];
                    break;
            }
            Response.Redirect(link);
        }
    }
    /// <summary>
    /// Kiểm tra hồ sơ là đầy đủ hay chưa
    /// </summary>
    /// <param name="HoSoId"></param>
    /// <Modified>
    /// Author      Date            Comments    
    /// Quannm      16/05/2009      Create new
    ///</Modified>
    private bool CheckHoSoDayDu(string HoSoId)
    {
        HoSo objHs = ProviderFactory.HoSoProvider.GetById(HoSoId);
        if (Convert.ToBoolean(objHs.HoSoDayDu))
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// Quản lý breadcum
    /// </summary>
    private void ManageBreadCum(string HoSoId, string UserControl)
    {
        linkCum.Text = "QUẢN LÝ HỒ SƠ";
        linkCum.NavigateUrl = "../WebUI/CN_HoSo_QuanLy.aspx?UserControl=" + UserControl + "&HoSoId=" + HoSoId + "";
    }
    #region "Hàm sử dụng chung cho các sự kiện trên ảnh và link"
    private void DeleteSanPham()
    {
        // Thực hiện xoá
        foreach (GridViewRow row in gvSanPham.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkCheck");
            if (chk.Checked)
            {
                string sanPhamId = gvSanPham.DataKeys[row.RowIndex][0].ToString();
                //kiem tra trang thai cua San pham, chi cho phep xoa cac san pham chua duoc phe duyet
                int trangThaiId = Convert.ToInt32(gvSanPham.DataKeys[row.RowIndex][1].ToString());
                if (trangThaiId == (int)EnTrangThaiSanPhamList.CHO_XU_LY
                    || trangThaiId == (int)EnTrangThaiSanPhamList.THAM_DINH_DONG_Y
                    || trangThaiId == (int)EnTrangThaiSanPhamList.THAM_DINH_KHONG_DONG_Y)
                {
                    TransactionManager transaction = ProviderFactory.Transaction;
                    try
                    {
                        //Xoa tai lieu dinh kem truoc khi xoa san pham
                        ProviderFactory.TaiLieuDinhKemProvider.XoaTaiLieuDinhKemBySanPhamID(sanPhamId, transaction);
                        //Xoa tieu chuan ap dung truoc khi xoa san pham
                        ProviderFactory.SanPhamTieuChuanApDungProvider.XoaTieuChuanApDungBySanPhamID(sanPhamId, transaction);
                        //Xoa qua trinh xu ly
                        ProviderFactory.QuaTrinhXuLyProvider.XoaQuaTrinhXuLyBySanPhamID(sanPhamId, transaction);
                        //Xoa san pham
                        ProviderFactory.SanPhamProvider.Delete(transaction, sanPhamId);
                        transaction.Commit();
                        deleteSPSuccess.Add(sanPhamId);
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                        {
                            switch (ex.Errors[0].Number)
                            {
                                case 547: // Foreign Key violation
                                    deleteSPFail.Add(sanPhamId);
                                    break;
                                default:
                                    deleteSPFail.Add(sanPhamId);
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        deleteSPFail.Add(sanPhamId);
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                }
                else
                {
                    deleteSPFail.Add(sanPhamId);
                }
            }
        }

        // Đưa ra thông báo thành công và ghi log sự kiện
        string thongbao = "";
        if (deleteSPSuccess.Count > 0)
        {
            thongbao = String.Format(Resources.Resource.msgXoaSanPhamThanhCong, string.Join(",", deleteSPSuccess.ToArray()));
            ProviderFactory.SysLogProvier.Write(((PageBase)this.Page).mUserInfo, SysEventList.CB_SAN_PHAM_XOA, thongbao);
        }
        if (deleteSPFail.Count > 0)
        {
            thongbao += String.Format(Resources.Resource.msgXoaSanPhamThatBai, string.Join(",", deleteSPFail.ToArray()));
        }

        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('" + thongbao + "');location.href='CB_HoSoSanPham_QuanLy.aspx?HoSoId=" + HoSoId + "&UserControl=CB_HoSoDen';</script>");
    }
    private void AddSanPham()
    {
        Response.Redirect("CB_TiepNhanHoSo_TaoMoi.aspx?Direct=CB_HoSoSanPham_QuanLy&Action=add&HoSoID=" + HoSoId + "&UserControl=CB_HoSoDen");
    }
    private void SendThongBaoPhi()
    {
        // Thực hiện gui
        foreach (GridViewRow row in gvPhi.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkCheck");
            if (chk.Checked)
            {
                string thongbaophiId = gvPhi.DataKeys[row.RowIndex][0].ToString();
                //kiem tra trang thai cua thong bao phi, chỉ có thể xóa thong bao phi mới
                int trangThaiId = Convert.ToInt32(gvPhi.DataKeys[row.RowIndex][1].ToString());
                //so giay thong bao phi
                string sothongbaophi = gvPhi.DataKeys[row.RowIndex][2].ToString();
                if (trangThaiId == (int)EnTrangThaiThongBaoPhiList.MOI_TAO)
                {
                    TransactionManager transaction = ProviderFactory.Transaction;
                    try
                    {
                        //liet ke tat ca cac sanphamId theo thongbaophiId
                        List<string> lstSanPhamId = ProviderFactory.ThongBaoLePhiSanPhamProvider.LaySanPhamIDTheoThongBaoLePhiID(thongbaophiId);
                        foreach (string sanphamId in lstSanPhamId)
                        {
                            //chuyen trang thai thanh cho phe duyet
                            SanPham objSp = ProviderFactory.SanPhamProvider.GetById(sanphamId);
                            objSp.TrangThaiId = (int)EnTrangThaiSanPhamList.CHO_PHE_DUYET;
                            ProviderFactory.SanPhamProvider.Save(transaction, objSp);
                        }
                        //chuyen trang thai thong bao phi ve cho phe duyet
                        ThongBaoLePhi objTBLP = ProviderFactory.ThongBaoLePhiProvider.GetById(thongbaophiId);
                        objTBLP.TrangThaiId = (int)EnTrangThaiThongBaoPhiList.CHO_PHE_DUYET;
                        ProviderFactory.ThongBaoLePhiProvider.Save(transaction, objTBLP);

                        transaction.Commit();
                        sendSuccess.Add(sothongbaophi);
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                        {
                            switch (ex.Errors[0].Number)
                            {
                                case 547: // Foreign Key violation
                                    sendFail.Add(sothongbaophi);
                                    break;
                                default:
                                    sendFail.Add(sothongbaophi);
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        sendFail.Add(sothongbaophi);
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                }
                else
                {
                    sendFail.Add(sothongbaophi);
                }
            }
        }

        // Đưa ra thông báo thành công và ghi log sự kiện
        string thongbao = "";
        if (sendSuccess.Count > 0)
        {
            thongbao = String.Format(Resources.Resource.msgGuiThongBaoPhiThanhCong, string.Join(",", sendSuccess.ToArray()));
            ProviderFactory.SysLogProvier.Write(((PageBase)this.Page).mUserInfo, SysEventList.CB_HO_SO_XOA, thongbao);
        }
        if (sendFail.Count > 0)
        {
            thongbao += String.Format(Resources.Resource.msgGuiThongBaoPhiThatBai, string.Join(",", sendFail.ToArray()));
        }

        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('" + thongbao + "');location.href='CB_HoSoSanPham_QuanLy.aspx?HoSoId=" + HoSoId + "&UserControl=CB_HoSoDen';</script>");
    }
    private void DeleteThongBaoPhi()
    {
        TransactionManager transaction = ProviderFactory.Transaction;

        // Thực hiện xoá
        foreach (GridViewRow row in gvPhi.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkCheck");
            if (chk.Checked)
            {
                //ma thong bao phi
                string thongbaophiId = gvPhi.DataKeys[row.RowIndex][0].ToString();
                //kiem tra trang thai cua thong bao phi, chỉ có thể xóa thong bao phi mới
                int trangThaiId = Convert.ToInt32(gvPhi.DataKeys[row.RowIndex][1].ToString());
                //so giay thong bao phi
                string sothongbaophi = gvPhi.DataKeys[row.RowIndex][2].ToString();
                if (trangThaiId == (int)EnTrangThaiThongBaoPhiList.MOI_TAO)
                {

                    try
                    {
                        ProviderFactory.ThongBaoLePhiSanPhamProvider.XoaThongBaoLePhiSanPhamTheoThongBaoLePhiID(thongbaophiId, transaction);
                        ProviderFactory.ThongBaoLePhiChiTietProvider.DeleteByThongBaoLePhiId(thongbaophiId, transaction);
                        ProviderFactory.ThongBaoLePhiProvider.Delete(transaction, thongbaophiId);
                        deleteSuccess.Add(sothongbaophi);
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                        {
                            switch (ex.Errors[0].Number)
                            {
                                case 547: // Foreign Key violation
                                    deleteFail.Add(sothongbaophi);
                                    break;
                                default:
                                    deleteFail.Add(sothongbaophi);
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        deleteFail.Add(sothongbaophi);
                    }
                }
                else
                {
                    deleteFail.Add(sothongbaophi);
                }
            }
        }

        // Đưa ra thông báo thành công và ghi log sự kiện
        string thongbao = "";
        if (deleteFail.Count > 0)
        {
            thongbao += String.Format(Resources.Resource.msgXoaThongBaoPhiThatBai, string.Join(",", deleteFail.ToArray()));
            transaction.Rollback();
        }
        else
        {
            thongbao = String.Format(Resources.Resource.msgXoaThongBaoPhi, string.Join(",", deleteSuccess.ToArray()));
            ProviderFactory.SysLogProvier.Write(((PageBase)this.Page).mUserInfo, SysEventList.CB_HO_SO_XOA, thongbao);
            transaction.Commit();
        }


        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('" + thongbao + "');location.href='CB_HoSoSanPham_QuanLy.aspx?HoSoId=" + HoSoId + "&UserControl=CB_HoSoDen';</script>");
    }
    #endregion "Hàm sử dụng chung cho các sự kiện trên ảnh và link"

    #region "Thêm mới, xóa sản phẩm. Gửi, xóa thông báo phí"
    /// <summary>
    /// Thêm mới sản phẩm
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkThemMoiSanPham_Click(object sender, EventArgs e)
    {
        AddSanPham();
    }
    /// <summary>
    /// Xóa sản phẩm, chỉ áp dụng với các sản phẩm có trạng thái là đang xử lý
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkXoaSanPham_Click(object sender, EventArgs e)
    {
        DeleteSanPham();
    }
    /// <summary>
    /// Gửi thông báo phí
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    ///<Modified>
    /// Author      Data        Comments
    /// Quannm      18/05/2009  Edited
    ///</Modified>
    protected void lnkGuiTBP_Click(object sender, EventArgs e)
    {
        SendThongBaoPhi();
    }
    /// <summary>
    /// Xóa thông báo phí, chỉ áp dụng cho role xử lý và trạng thái là mới tạo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    ///<Modified>
    /// Author      Data        Comments
    /// Quannm      18/05/2009  Edited
    ///</Modified>
    protected void lnkXoaTBP_Click(object sender, EventArgs e)
    {
        DeleteThongBaoPhi();
    }
    protected void ImgBtnThemmoiSanPham_Click(object sender, ImageClickEventArgs e)
    {
        AddSanPham();
    }
    protected void imgXoaSanPham_Click(object sender, ImageClickEventArgs e)
    {
        DeleteSanPham();
    }
    protected void imgGuiTBP_Click(object sender, ImageClickEventArgs e)
    {
        SendThongBaoPhi();
    }
    protected void imgXoaTBP_Click(object sender, ImageClickEventArgs e)
    {
        DeleteThongBaoPhi();
    }
    #endregion "Thêm mới, xóa sản phẩm. Gửi, xóa thông báo phí"

    private void KiemTraTrangThaiSanPham(string HoSoId)
    {
        // Kiểm tra danh sách các sản phẩm trong hồ sơ, nếu tất cả đã xử lý xong thì chuyển hồ sơ sang trạng thái chờ lưu trữ
        TList<SanPham> lstSanPham = ProviderFactory.SanPhamProvider.GetByHoSoId(HoSoId);
        if (lstSanPham.Count > 0)
        {
            int count = 0;
            foreach (SanPham objSP in lstSanPham)
            {
                if (objSP.TrangThaiId == (int)EnTrangThaiSanPhamList.GD_PHE_DUYET
                    || objSP.TrangThaiId == (int)EnTrangThaiSanPhamList.HUY_BTN
                    || objSP.TrangThaiId == (int)EnTrangThaiSanPhamList.DA_CAP_BAN_TIEP_NHAN_CB)
                    //|| objSP.TrangThaiId == (int)EnTrangThaiSanPhamList.KET_THUC
                    //|| objSP.TrangThaiId == (int)EnTrangThaiSanPhamList.HUY
                    //|| objSP.TrangThaiId == (int)EnTrangThaiSanPhamList.DA_CAP_GCN
                    //|| objSP.TrangThaiId == (int)EnTrangThaiSanPhamList.HUY_GCN
                    count++;
            }
            if (count == lstSanPham.Count && ((PageBase)this.Page).mUserInfo.IsNguoiXuLy(HoSoId))
            {
                LinkBtnCompleteHS.Visible = true;
                ImgBtnCompleteHS.Visible = true;

                gvSanPham.Columns[6].Visible = false;

                lnkXoaSanPham.Visible = false;
                imgXoaSanPham.Visible = false;
            }
        }
    }

    protected void LinkBtnCompleteHS_Click(object sender, EventArgs e)
    {
        GuiLuuTru();
    }
    /// <summary>
    /// Chuyển hồ sơ sang trạng thái chờ lưu trữ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImgBtnCompleteHS_Click(object sender, ImageClickEventArgs e)
    {
        GuiLuuTru();
    }

    private void GuiLuuTru()
    {
        //Chuyển hồ sơ sang trạng thái chờ lưu trữ
        HoSo objHoSo = ProviderFactory.HoSoProvider.GetById(HoSoId);
        objHoSo.TrangThaiId = (int)EnTrangThaiHoSoList.CHO_LUU_TRU;

        //thêm thông tin hồ sơ+sản phẩm(có tên người ký và chức vụ)+sản phẩm tiêu chuẩn áp dụng
        string strDmDonViId = objHoSo.DonViId;
        string strDmTrungTamId = objHoSo.TrungTamId;
        DmDonVi objDonVi = ProviderFactory.DmDonViProvider.GetById(strDmDonViId);
        DmTrungTam objTrungTam = ProviderFactory.DmTrungTamProvider.GetById(strDmTrungTamId);
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

        TList<SanPham> listSanPham = ProviderFactory.SanPhamProvider.GetByHoSoId(objHoSo.Id);
        foreach (SanPham sp in listSanPham)
        {
            DmSanPham objSanPham = ProviderFactory.DmSanPhamProvider.GetById(sp.SanPhamId);
            DmNhomSanPham objNhomSanPham = ProviderFactory.DmNhomSanPhamProvider.GetById(sp.NhomSanPhamId);
            DmCoQuanDoKiem objCoQuanDoKiem = ProviderFactory.DmCoQuanDoKiemProvider.GetById(sp.CoQuanDoKiemId);
            DmHangSanXuat objHangSanXuat = ProviderFactory.DmHangSanXuatProvider.GetById(sp.HangSanXuatId);
            SysUser objUser = ProviderFactory.SysUserProvider.GetById(sp.NguoiKyDuyetId);

            DmTrungTam trungtam = ProviderFactory.DmTrungTamProvider.GetById(objUser.OrganizationId);
            sp.TenTrungTam = trungtam.TenTrungTam;
            DmPhongBan phongban = ProviderFactory.DmPhongBanProvider.GetById(objUser.DepartmentId);
            if (phongban != null)
                sp.PhongBan = phongban.TenPhongBan;

            if (objCoQuanDoKiem != null)
            {
                sp.DmCqdkDiaChi = objCoQuanDoKiem.DiaChi;
                sp.DmCqdkTenCoQuanDoKiem = objCoQuanDoKiem.TenCoQuanDoKiem;
                sp.DmCqdkTenTiengAnh = objCoQuanDoKiem.TenTiengAnh;
            }
            if (objHangSanXuat != null)
            {
                sp.DmHsxTenHangSanXuat = objHangSanXuat.TenHangSanXuat;
                sp.DmHsxTenTiengAnh = objHangSanXuat.TenTiengAnh;
            }
            if (objNhomSanPham != null)
            {
                sp.DmNhomSpLienQuanTanSo = objNhomSanPham.LienQuanTanSo;
                sp.DmNhomSpLoaiHinhChungNhan = objNhomSanPham.LoaiHinhChungNhan;
                sp.DmNhomSpMaNhom = objNhomSanPham.MaNhom;
                sp.DmNhomSpMucLePhi = objNhomSanPham.MucLePhi;
                sp.DmNhomSpTenNhom = objNhomSanPham.TenNhom;
                sp.DmNhomSpThoiHanGcn = objNhomSanPham.ThoiHanGcn;
            }
            if (objSanPham != null)
            {
                sp.DmSanPhamLoaiSanPham = objSanPham.LoaiSanPham;
                sp.DmSanPhamLoaiTieuChuanApDung = objSanPham.LoaiTieuChuanApDung;
                sp.DmSanPhamMaSanPham = objSanPham.MaSanPham;
                sp.DmSanPhamNhomSanPhamId = objSanPham.NhomSanPhamId;
                sp.DmSanPhamTenTiengAnh = objSanPham.TenTiengAnh;
                sp.DmSanPhamTenTiengViet = objSanPham.TenTiengViet;
            }

            if (objUser != null)
            {
                sp.TenNguoiKyDuyet = objUser.FullName;
                sp.ChucVu = Convert.ToInt32(objUser.Position);
            }

            // Cập nhật thông tin tiêu chuẩn
            TList<SanPhamTieuChuanApDung> listSanPhamTieuChuanApDung = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(sp.Id);
            foreach (SanPhamTieuChuanApDung spad in listSanPhamTieuChuanApDung)
            {
                DmTieuChuan objTieuChuan = ProviderFactory.DmTieuChuanProvider.GetById(spad.TieuChuanApDungId);
                if (objTieuChuan != null)
                {
                    spad.DmTieuChuanMaTieuChuan = objTieuChuan.MaTieuChuan;
                    spad.DmTieuChuanTenTieuChuan = objTieuChuan.TenTieuChuan;
                    ProviderFactory.SanPhamTieuChuanApDungProvider.Save(spad);
                }
            }

            // Nếu sản phẩm không được cấp BTN thì chuyển sang trạng thái hủy hoặc kết thúc
            if (sp.KetLuanId != (int)EnKetLuanList.CAP_BAN_TIEP_NHAN_CB)
                sp.TrangThaiId = (int)EnTrangThaiSanPhamList.KET_THUC;
            ProviderFactory.SanPhamProvider.Save(sp);
        }

        // Cập nhật thông tin trong thông báo lệ phí
        TList<ThongBaoLePhi> lstThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetByHoSoId(HoSoId);
        foreach (ThongBaoLePhi objTBLP in lstThongBaoLePhi)
        {
            SysUser objUser = ProviderFactory.SysUserProvider.GetById(objTBLP.NguoiPheDuyetId);
            if (objUser != null)
            {
                objTBLP.TenNguoiKyDuyet = objUser.FullName;
                objTBLP.ChucVu = Convert.ToInt32(objUser.Position);
                ProviderFactory.ThongBaoLePhiProvider.Save(objTBLP);
            }

            // Cập nhật thông tin lệ phí chi tiết
            TList<ThongBaoLePhiChiTiet> lstChiTiet = ProviderFactory.ThongBaoLePhiChiTietProvider.GetByThongBaoLePhiId(objTBLP.Id);
            foreach (ThongBaoLePhiChiTiet objChiTiet in lstChiTiet)
            {
                if (objChiTiet.LoaiPhiId == (int)EnLoaiPhiList.PHI_CONG_BO_HQ)
                    objChiTiet.MucPhi = Convert.ToInt32(ConfigurationManager.AppSettings["PhiCongBoHQ"].ToString());
                ProviderFactory.ThongBaoLePhiChiTietProvider.Save(objChiTiet);
            }
        }

        ProviderFactory.HoSoProvider.Save(objHoSo);
        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Gửi lưu trữ thành công');location.href='CN_HoSo_QuanLy.aspx?UserControl=CB_HoSoDen';</script>");
    }



    /// <summary>
    /// Ẩn check box với các thông báo phi không phải là mới tạo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPhi_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Chi cho phep check (xoa, gui lanh dao) voi cac thong bao phi moi tao
            if ((Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) != (int)EnTrangThaiThongBaoPhiList.MOI_TAO)
                 || !((PageBase)this.Page).mUserInfo.IsNguoiXuLy(Request["HosoID"].ToString()))
            {
                Control chk = e.Row.FindControl("chkCheck");
                chk.Visible = false;

                Control chkAll = gvPhi.HeaderRow.FindControl("chkCheckAll");
                chkAll.Visible = false;
            }


        }
    }
}
