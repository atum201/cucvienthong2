using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Cuc_QLCL.AdapterData;
using Cuc_QLCL.Entities;
using Cuc_QLCL.Data;
using CucQLCL.Common;
using System.Text;


public partial class WebUI_CB_HoSoSanPham : PageBase
{
    string HoSoId = string.Empty;
    int TrangThaiId = 0;
    string Action = string.Empty;
    string Direct = string.Empty;
    int KetLuanId = 0;
    string mSanPhamId = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["direct"] != null)
            Direct = Request["direct"].ToString().ToLower();
        if (Request["HosoId"] != null)
            HoSoId = Request["HosoId"].ToString();
        if (Request["action"] != null)
        {
            Action = Request["action"].ToString().ToLower();
            if (Action == "view")
            {
                //thiết lập webpath        
                StringBuilder sb = new StringBuilder();
                if (Direct == "cb_trahoso")
                    sb.Append(@"<a href='../WebUI/CB_TraCuuPHTC.aspx'> TRA CỨU HỒ SƠ CÔNG BỐ HỢP QUY  </a>");
                else if (Direct == "cb_hosodi")
                    sb.Append(@"<a href='../WebUI/CN_HoSo_QuanLy.aspx?UserControl=CB_HoSoDi'> DANH SÁCH HỒ SƠ CÔNG BỐ ĐÃ GỬI </a>");
                lblPath.Text = sb.ToString();
                traction.Style.Add("display", "none");

                if (!IsPostBack)
                {
                    Bind_HoSo(HoSoId);
                }
            }
        }
        else
        {
            //thiết lập webpath        
            StringBuilder sb = new StringBuilder();
            sb.Append("<a href='../WebUI/CN_HoSo_QuanLy.aspx?UserControl=CB_HoSoDen'> DANH SÁCH HỒ SƠ MỚI</a>");
            lblPath.Text = sb.ToString();
            if (!IsPostBack)
            {
                Bind_HoSo(HoSoId);
            }


        }
    }
    /// <summary>
    /// Lấy thông tin về hồ sơ
    /// </summary>
    /// <param name="HoSoId"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    private void Bind_HoSo(string HoSoId)
    {
        HoSo hsInform = ProviderFactory.HoSoProvider.GetById(HoSoId);
        if (hsInform.TrangThaiId != (int)EnTrangThaiHoSoList.HO_SO_MOI && Action != "view")
            return;
        lblSoHoSo.Text = Server.HtmlEncode(hsInform.SoHoSo);
        lblSoCongVanDonVi.Text = Server.HtmlEncode(hsInform.SoCongVanDonVi);
        lblNguoiNop.Text = Server.HtmlEncode(hsInform.NguoiNopHoSo);
        DateTime? d = hsInform.NgayTiepNhan;
        if (d != null)
        {
            DateTime da = Convert.ToDateTime(d);
            lblNgayNhan.Text = da.ToShortDateString();
        }
        //lblFax.Text = Server.HtmlEncode(hsInform.Fax);
        lblDienThoai.Text = Server.HtmlEncode(hsInform.DienThoai);
        lblEmail.Text = Server.HtmlEncode(hsInform.Email);
        DmDonVi dmDonVi = ProviderFactory.DmDonViProvider.GetById(hsInform.DonViId);
        if (dmDonVi != null)
            lblDonVi.Text = Server.HtmlEncode(dmDonVi.TenTiengViet);
        SysUser Ngnhan = ProviderFactory.SysUserProvider.GetById(hsInform.NguoiTiepNhanId);
        if (Ngnhan != null)
            lblNguoiTiepNhan.Text = Ngnhan.FullName;
        int? ob = hsInform.TrangThaiId;
        if (ob != null)
        {
            int ttid = Convert.ToInt16(ob);
            EnTrangThaiHoSo TrangThai = ProviderFactory.EnTrangThaiHoSoProvider.GetById(ttid);
            lblTrangThai.Text = TrangThai.MoTa;
        }
        //lấy thông tin về sản phẩm của hồ sơ
        GetSanPhamOFHoSo(HoSoId);

    }
    /// <summary>
    /// Lấy danh sách sản phẩm của nhận
    /// </summary>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    private void GetSanPhamOFHoSo(string HoSoId)
    {
        TList<SanPham> listSanPham = ProviderFactory.SanPhamProvider.GetByHoSoId(HoSoId);
        DataTable dt = new DataTable();
        dt.Columns.Add("TenSanPham");
        dt.Columns.Add("MaSanPham");
        dt.Columns.Add("KyHieu");
        dt.Columns.Add("MaNhom");
        dt.Columns.Add("HinhThuc");
        dt.Columns.Add("TieuChuan");
        dt.Columns.Add("GhiChu");
        dt.Columns.Add("TrangThai");
        dt.Columns.Add("TrangThaiId");
        dt.Columns.Add("KetLuanId");
        dt.Columns.Add("Id");

        // tiến hành ghép các thông tin cho sản phẩm từ các bảng liên quan
        if (listSanPham != null)
        {
            foreach (SanPham sp in listSanPham)
            {
                DataRow row = dt.NewRow();
                row["Id"] = sp.Id;
                row["TenSanPham"] = string.Empty;
                row["KyHieu"] = sp.KyHieu;
                row["MaNhom"] = string.Empty;
                row["HinhThuc"] = string.Empty;
                row["TieuChuan"] = string.Empty;
                row["GhiChu"] = string.Empty;
                row["TrangThai"] = string.Empty;
                row["TrangThaiId"] = string.Empty;
                row["KetLuanId"] = "0";

                DmSanPham dmSanPham = ProviderFactory.DmSanPhamProvider.GetById(sp.SanPhamId);
                if (dmSanPham != null)
                {
                    row["TenSanPham"] = dmSanPham.TenTiengViet;

                }
                DmNhomSanPham dmNhomSP = ProviderFactory.DmNhomSanPhamProvider.GetById(sp.NhomSanPhamId);
                if (dmNhomSP != null)
                    row["MaNhom"] = dmNhomSP.MaNhom;

                int? ob = sp.HinhThucId;
                if (ob != null)
                {
                    int htId = Convert.ToInt16(ob);
                    EnHinhThuc HinhThuc = ProviderFactory.EnHinhThucProvider.GetById(htId);
                    row["HinhThuc"] = HinhThuc.MoTa;
                }
                //danh sách tiêu chuẩn cho sản phẩm 
                TList<SanPhamTieuChuanApDung> listSPTieuChuan = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(sp.Id);
                if (listSPTieuChuan != null)
                {
                    string strTieuChuan = string.Empty;
                    foreach (SanPhamTieuChuanApDung SPTieuChuan in listSPTieuChuan)
                    {
                        DmTieuChuan dmTieuChuan = ProviderFactory.DmTieuChuanProvider.GetById(SPTieuChuan.TieuChuanApDungId);
                        if (dmTieuChuan != null)
                            strTieuChuan += dmTieuChuan.MaTieuChuan + ",";
                    }
                    if (strTieuChuan.Length > 0)
                        strTieuChuan = strTieuChuan.Remove(strTieuChuan.Length - 1, 1);
                    row["TieuChuan"] = strTieuChuan;
                }
                // lấy thông tin về quá trình xử lý cho sanpham
                TList<QuaTrinhXuLy> listQTXL = ProviderFactory.QuaTrinhXuLyProvider.GetBySanPhamId(sp.Id);
                if (listQTXL != null)
                {
                    foreach (QuaTrinhXuLy qtxl in listQTXL)
                    {
                        if (qtxl.LoaiXuLyId == 1)
                        {
                            row["GhiChu"] = qtxl.GhiChu;
                            //row["NoiDungXuLy"] = qtxl.NoiDungXuLy;
                            break;
                        }
                    }
                }
                if (sp.TrangThaiId != null)
                {
                    string TenTrangThai = ProviderFactory.EnTrangThaiSanPhamProvider.GetById(sp.TrangThaiId).MoTa;
                    row["TrangThai"] = TenTrangThai;
                    row["TrangThaiId"] = sp.TrangThaiId.ToString();
                }
                if (sp.KetLuanId != null)
                    row["KetLuanId"] = sp.KetLuanId.ToString();

                dt.Rows.Add(row);
            }
        }
        gvSanPham.DataSource = dt;
        gvSanPham.DataBind();

        //thực hiện việc bật tắt các sự kiện trên nút xóa
        if (gvSanPham.Rows.Count > 0)
        {
            ImgBtnXoa.Enabled = true;
            lnkXoa.Enabled = true;

            ImgBtnXoa.Attributes.Add("onclick", "if(GridIsChecked('gvSanPham')) { return confirm('Bạn có chắc chắn muốn sản phẩm đã chọn không?');} else {alert('Bạn chưa chọn sản phẩm cần xóa.'); return false;}");
            lnkXoa.Attributes.Add("onclick", "if(GridIsChecked('gvSanPham')) { return confirm('Bạn có chắc chắn muốn sản phẩm đã chọn không?');} else {alert('Bạn chưa chọn sản phẩm cần xóa.'); return false;}");

        }

        else
        {
            ImgBtnXoa.Enabled = false;
            lnkXoa.Enabled = false;
            ImgBtnXoa.Attributes.Remove("onclick");
            lnkXoa.Attributes.Remove("onclick");
        }



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
    protected void gvSanPham_DataBound(object sender, EventArgs e)
    {
        int TotalChkBx = 0;
        if (gvSanPham.HeaderRow != null)
        {
            // Lấy tham chiếu đến checkbox header
            CheckBox cbHeader = (CheckBox)gvSanPham.HeaderRow.FindControl("chkCheckAll");
            // Bắt sự kiện onclick của checkbox toàn bộ
            cbHeader.Attributes["onclick"] = string.Format("HeaderClick('{0}');", gvSanPham.ID);

            // Duyệt trên gridview
            foreach (GridViewRow gvr in gvSanPham.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
                cb.Attributes["onclick"] = string.Format("ChildClick('{0}');", gvSanPham.ID);
                // Đếm số lượng check box con
                TotalChkBx++;
            }
        }
        // Gán giá trị của biến
        this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "TotalChkBx", string.Format("TotalChkBx={0};Counter={1};", TotalChkBx, 0), true);
        if (Action == "view")
            gvSanPham.Columns[6].Visible = false;
    }

    /// <summary>
    /// Xóa sản phẩm
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    protected void lnkXoa_Click(object sender, EventArgs e)
    {
        CheckBox chk;
        string IdSanPham = string.Empty;
        List<string> tensanpham = new List<string>();
        TransactionManager transaction = ProviderFactory.Transaction;
        try
        {
            foreach (GridViewRow rowItem in gvSanPham.Rows)
            {
                chk = (CheckBox)(rowItem.Cells[6].FindControl("chkCheck"));

                if (chk.Checked)
                {
                    IdSanPham = gvSanPham.DataKeys[rowItem.RowIndex][0].ToString();
                    TList<SanPhamTieuChuanApDung> listspap = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(transaction, IdSanPham);
                    //xóa các tiêu chuẩn áp dụng cho sản phẩm 
                    if (listspap != null && listspap.Count > 0)
                    {
                        ProviderFactory.SanPhamTieuChuanApDungProvider.Delete(transaction, listspap);

                    }


                    //xóa các tài liệu đính kèm cùng sản phẩm

                    TList<TaiLieuDinhKem> listTaiLieu = ProviderFactory.TaiLieuDinhKemProvider.GetBySanPhamId(transaction, IdSanPham);
                    if (listTaiLieu != null && listTaiLieu.Count > 0)
                    {
                        //Xóa các file tai liệu lưu trữ tại Server
                        foreach (TaiLieuDinhKem tl in listTaiLieu)
                        {
                            string FileName = tl.TenFile;
                            DeleteFile(FileName);

                        }
                        ProviderFactory.TaiLieuDinhKemProvider.Delete(transaction, listTaiLieu);

                    }


                    //xóa các thông tin về quát trình xử lý sản phẩm

                    TList<QuaTrinhXuLy> listxuly = ProviderFactory.QuaTrinhXuLyProvider.GetBySanPhamId(transaction, IdSanPham);
                    if (listxuly != null && listxuly.Count > 0)
                    {
                        ProviderFactory.QuaTrinhXuLyProvider.Delete(transaction, listxuly);

                    }



                    //xóa sản phẩm

                    SanPham sp = ProviderFactory.SanPhamProvider.GetById(IdSanPham);
                    if (sp != null)
                    {
                        ProviderFactory.SanPhamProvider.Delete(transaction, sp);
                        tensanpham.Add(gvSanPham.DataKeys[rowItem.RowIndex][1].ToString());

                    }




                }
            }
            if (tensanpham.Count > 0)
            {
                transaction.Commit();
                string strlog = string.Join(", ", tensanpham.ToArray());
                Thong_bao(string.Format(Resources.Resource.msgXoaSanPhamThanhCong, strlog));
            }

        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }

        GetSanPhamOFHoSo(HoSoId);
    }
    /// <summary>
    /// Sự kiện nút gửi hồ sơ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    protected void lnkGui_Click(object sender, EventArgs e)
    {
        HoSo hs = ProviderFactory.HoSoProvider.GetById(HoSoId);
        if (hs != null)
        {
            hs.TrangThaiId = (int)EnTrangThaiHoSoList.CHO_PHAN_CONG;
            TransactionManager transaction = ProviderFactory.Transaction;
            try
            {
                ProviderFactory.HoSoProvider.Update(hs);
                transaction.Commit();
                string ThongBao = Resources.Resource.msgGuiHoSoThanhCong;
                Thong_bao(this.Page, ThongBao, "CN_HoSo_QuanLy.aspx?UserControl=CB_HoSoDen");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
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
    protected void gvSanPham_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            mSanPhamId = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
            TrangThaiId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiId"));
            KetLuanId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "KetLuanId"));
            string TrangThaiSp = DataBinder.Eval(e.Row.DataItem, "TrangThai").ToString();

            //Nếu hướng chuyển đến là trang quản lý hồ sơ thì
            if (Action != "view")
            {
                LinkButton lnk = new LinkButton();
                lnk.OnClientClick = string.Format("window.location.href='CB_TiepNhanHoSo_TaoMoi.aspx?direct=CB_HoSoSanPham&Action=Edit&HoSoId={0}&SanPhamID={1}&TrangThaiID={2}';return false;", HoSoId, DataBinder.Eval(e.Row.DataItem, "ID"), TrangThaiId);
                lnk.Text = DataBinder.Eval(e.Row.DataItem, "TenSanPham").ToString();
                e.Row.Cells[0].Controls.Add(lnk);

            }
            else
            {
                //nếu hướng chuyển đến là trang tra cứu thông tin hồ sơ
                if (Action == "view" && Direct == "cb_trahoso")
                {
                    LinkButton lnk = new LinkButton();
                    lnk.OnClientClick = string.Format("window.location.href='CB_TiepNhanHoSo_TaoMoi.aspx?direct=CB_TraCuuHoSo_HoSoSanPham&Action=view&HoSoId={0}&SanPhamID={1}&TrangThaiID={2}';return false;", HoSoId, DataBinder.Eval(e.Row.DataItem, "ID"), TrangThaiId);
                    lnk.Text = DataBinder.Eval(e.Row.DataItem, "TenSanPham").ToString();
                    e.Row.Cells[0].Controls.Add(lnk);

                }
                //nếu trang chuyển tới chỉ để xem thông tin
                else
                {
                    LinkButton lnk = new LinkButton();
                    lnk.OnClientClick = string.Format("window.location.href='CB_TiepNhanHoSo_TaoMoi.aspx?direct=CB_HoSoSanPham&Action=view&HoSoId={0}&SanPhamID={1}&TrangThaiID={2}';return false;", HoSoId, DataBinder.Eval(e.Row.DataItem, "ID"), TrangThaiId);
                    lnk.Text = DataBinder.Eval(e.Row.DataItem, "TenSanPham").ToString();
                    e.Row.Cells[0].Controls.Add(lnk);

                }

                // Thiết lập giá trị cho cột trạng thái sản phẩm
                LinkButton lnkTrangThai = new LinkButton();

                // Kiểm tra trạng thái, lấy công văn trả lời nếu có
                bool CoCongVanTraLoi = false;
                if (KetLuanId != (int)EnKetLuanList.CAP_BAN_TIEP_NHAN_CB && KetLuanId != 0)
                {
                    //  lấy danh sách tài liệu
                    TList<TaiLieuDinhKem> lstTaiLieuDinhKem = ProviderFactory.TaiLieuDinhKemProvider.GetBySanPhamId(mSanPhamId);

                    foreach (TaiLieuDinhKem objTaiLieu in lstTaiLieuDinhKem)
                    {
                        string FilePath = objTaiLieu.TenFile.Trim();
                        if (objTaiLieu.LoaiTaiLieuId == (int)EnLoaiTaiLieuList.CONG_VAN)
                        {
                            StringBuilder sbCongVan = new StringBuilder();
                            string TrangThai_KetLuan = TrangThaiSp;
                            if (TrangThaiId == (int)EnTrangThaiSanPhamList.CHO_XU_LY)
                                TrangThai_KetLuan = "Đang xử lý";
                            else if (TrangThaiId == (int)EnTrangThaiSanPhamList.CHO_XU_LY)
                                TrangThai_KetLuan = "Huỷ";
                            else
                            {
                                if (TrangThaiSp.Contains("Chờ"))
                                {
                                    if (KetLuanId == (int)EnKetLuanList.KHONG_CAP_BTN_CB)
                                        TrangThai_KetLuan = TrangThaiSp + " Không cấp BTN";
                                    if (KetLuanId == (int)EnKetLuanList.KHONG_PHAI_CBHQ)
                                        TrangThai_KetLuan = TrangThaiSp + " Không phải CBHQ";
                                    if (KetLuanId == (int)EnKetLuanList.HUY)
                                        TrangThai_KetLuan = TrangThaiSp + " Huỷ";
                                }
                                else
                                {
                                    if (KetLuanId == (int)EnKetLuanList.KHONG_CAP_BTN_CB)
                                        TrangThai_KetLuan = "Không cấp BTN, " + TrangThaiSp;
                                    if (KetLuanId == (int)EnKetLuanList.KHONG_PHAI_CBHQ)
                                        TrangThai_KetLuan = "Không phải CBHQ, " + TrangThaiSp;
                                    if (KetLuanId == (int)EnKetLuanList.HUY)
                                        TrangThai_KetLuan = "Huỷ, " + TrangThaiSp;
                                }
                            }

                            sbCongVan.Append("<a href='../" + FilePath + "' title='Xem công văn trả lời'>" + TrangThai_KetLuan + "</a>");
                            lnkTrangThai.Text = sbCongVan.ToString();
                            CoCongVanTraLoi = true;
                        }

                    }
                }

                if (KetLuanId == (int)EnKetLuanList.CAP_BAN_TIEP_NHAN_CB)
                {
                    if (TrangThaiId == (int)EnTrangThaiSanPhamList.DA_CAP_BAN_TIEP_NHAN_CB
                        || TrangThaiId == (int)EnTrangThaiSanPhamList.GD_PHE_DUYET
                        || TrangThaiId == (int)EnTrangThaiSanPhamList.HUY_BTN)
                    {
                        lnkTrangThai.Text = DataBinder.Eval(e.Row.DataItem, "TrangThai").ToString();
                        if (TrangThaiId == (int)EnTrangThaiSanPhamList.GD_PHE_DUYET)
                            lnkTrangThai.Text = "Giám đốc phê duyệt cấp BTN";
                        lnkTrangThai.ToolTip = "Xem Bản tiếp nhận";
                        lnkTrangThai.OnClientClick = "return popCenter('../ReportForm/HienBaoCao.aspx?LoaiBaoCao=BanTiepNhan&HoSoId=" + HoSoId + "&SanphamID=" + mSanPhamId + "','Tra_cuu_ban_tiep_nhan',800,600);";
                        e.Row.Cells[5].Controls.Add(lnkTrangThai);
                    }
                    else
                    {
                        if (TrangThaiId < 2)
                            e.Row.Cells[5].Text = "Đang xử lý";
                        else
                        {
                            if (TrangThaiSp.Contains("Chờ"))
                                e.Row.Cells[5].Text = "Cấp BTN, " + TrangThaiSp;
                            else
                                e.Row.Cells[5].Text = TrangThaiSp + " Cấp BTN";
                        }
                    }
                }
                else
                {
                    if (CoCongVanTraLoi)
                        e.Row.Cells[5].Controls.Add(lnkTrangThai);
                    else
                    {
                        string TrangThai_KetLuan = TrangThaiSp;
                        if (TrangThaiId == (int)EnTrangThaiSanPhamList.CHO_XU_LY)
                            TrangThai_KetLuan = "Đang xử lý";
                        else if (TrangThaiId == (int)EnTrangThaiSanPhamList.HUY)
                            TrangThai_KetLuan = "Huỷ";
                        else
                        {
                            if (TrangThaiSp.Contains("Chờ"))
                            {
                                if (KetLuanId == (int)EnKetLuanList.KHONG_CAP_BTN_CB)
                                    TrangThai_KetLuan = TrangThaiSp + " Không cấp BTN";
                                if (KetLuanId == (int)EnKetLuanList.KHONG_PHAI_CBHQ)
                                    TrangThai_KetLuan = TrangThaiSp + " Không phải CBHQ";
                                if (KetLuanId == (int)EnKetLuanList.HUY)
                                    TrangThai_KetLuan = TrangThaiSp + " Huỷ";
                            }
                            else
                            {
                                if (KetLuanId == (int)EnKetLuanList.KHONG_CAP_BTN_CB)
                                    TrangThai_KetLuan = "Không cấp BTN, " + TrangThaiSp;
                                if (KetLuanId == (int)EnKetLuanList.KHONG_PHAI_CBHQ)
                                    TrangThai_KetLuan = "Không phải CB, " + TrangThaiSp;
                                if (KetLuanId == (int)EnKetLuanList.HUY)
                                    TrangThai_KetLuan = "Huỷ, " + TrangThaiSp;
                            }
                        }

                        e.Row.Cells[5].Text = TrangThai_KetLuan;
                    }
                }
            }
        }
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
    protected void ImgBtnGui_Click(object sender, ImageClickEventArgs e)
    {
        lnkGui_Click(null, null);
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
    protected void ImgBtnXoa_Click(object sender, ImageClickEventArgs e)
    {
        lnkXoa_Click(null, null);
    }
    /// <summary>
    /// Xóa file vật lý trên server
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
            throw ex;
        }
        return false;
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
    protected void gvSanPham_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSanPham.PageIndex = e.NewPageIndex;
        GetSanPhamOFHoSo(HoSoId);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSanPham_Sorting(object sender, GridViewSortEventArgs e)
    {
        GetSanPhamOFHoSo(HoSoId);
    }
}
