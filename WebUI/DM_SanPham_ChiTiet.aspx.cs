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
using Cuc_QLCL.Data;
using Cuc_QLCL.Entities;
using System.Data.SqlClient;
using System.ComponentModel;
using CucQLCL.Common;

/// <summary>
/// Danh mục sản phẩm chi tiết
/// </summary>
/// <Modified>
/// Name		Date		Comments
/// TruongTv	6/6/2009	Thêm mới
/// </Modified>>
public partial class WebUI_DM_SanPham_ChiTiet : PageBase
{
    /// <summary>
    /// Hiên thị dữ liệu
    /// </summary>
    /// <Modified>
    /// Name		Date		Comments
    /// TruongTv	6/6/2009	Thêm mới
    /// </Modified>>
    string mWhereClause = " ";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (this.IsPostBack)
        {
            string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];
            if (eventTarget == "TieuChuanPostBack")
            {
                String passedArgument = Request.Params.Get("__EVENTARGUMENT");
                String[] arr = passedArgument.Split(',');
                ListItem Item = new ListItem(arr[1], arr[0]);
                Item.Selected = true;
                chkTieuChuan.Items.Add(Item);
            }

        }
        if (!IsPostBack)
        {
            // Nếu thêm mới sản phẩm cho phần công bố thì ẩn trường mã sản phẩm, mã mhóm và loại tiêu chuẩn áp dụng
            if (Request["PostBack"] != null)
            {
                if (Request["PostBack"].ToString() == "CB_TiepNhanHoSo_TaoMoi")
                {
                    lblLoaiTieuChuan.Visible = false;
                    lblMaNhom.Visible = false;
                    //lblMaSanPham.Visible = false;
                    //txtMaSP.Visible = false;
                    ddlLoaiTieuChuanApDung.Visible = false;
                    cbNhomSP.Visible = false;
                    ddlLoaiSanPham.SelectedIndex = 1;
                    ddlLoaiSanPham.Enabled = false;
                }
                else if (Request["PostBack"].ToString() == "CN_TiepNhanHoSo_TaoMoi" || Request["PostBack"].ToString() == "CN_XuLyHoSo_DanhGia")
                {
                    ddlLoaiSanPham.SelectedIndex = 0;
                    ddlLoaiSanPham.Enabled = false;
                }
            }

            CheckPermission();
            string ID = Request["ID"];
            //Load dữ liệu Combo Nhóm sản phẩm.
            this.LoadNhomSanPham();
            //Load Tiêu chuẩn 
            Bind_ddlLoaiTieuChuanApDung();
            if (ID != null)
            {
                BinSanPhamForEdit(ID);

            }
            else
            {
                //txtMaSP.Text = string.Empty;
                txtTenSP.Text = string.Empty;
                txtTenSPTA.Text = string.Empty;

            }
            LayDanhSach(mWhereClause);
        }


    }
    /// <summary>
    /// Kiểm tra người dùng có quyền cập nhật danh mục 
    /// </summary>
    /// <param name="MaDonVi"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// giangum                 5/5/2009              
    /// </Modified>
    void CheckPermission()
    {
        if (!mUserInfo.IsPermission(EnPermission.QUANLY_DM_SANPHAM))
        {
            btnCapNhat.Visible = false;
        }

    }
    /// <summary>
    /// Load nhóm sản phẩm
    /// </summary>
    /// <modified>
    ///  Author                      Date            Action
    ///	Truongtv          06/05/2009      Create new
    /// </modified>
    private void LoadNhomSanPham()
    {
        //Lấy dữ liệu nhóm sản phẩm
        TList<DmNhomSanPham> lstNhomSanPham = ProviderFactory.DmNhomSanPhamProvider.GetAll();

        // Nếu là thêm mới mở từ form thêm sản phẩm trong hồ sơ hoặc form xử lý, đánh giá sản phẩm thì chỉ load nhóm sản phẩm theo loại hồ sơ
        if (Request["LoaiHinhChungNhan"] != null)
        {
            string LoaiHinhCN = string.Empty;
            if (Request["LoaiHinhChungNhan"] != null)
            {
                LoaiHinhCN = Request["LoaiHinhChungNhan"].ToString();
                lstNhomSanPham.Filter = "LoaiHinhChungNhan = " + LoaiHinhCN;
            }
        }
        cbNhomSP.DataValueField = "ID";
        cbNhomSP.DataTextField = "Manhom";
        //Đổ dữ liệu lên Grid		
        cbNhomSP.DataSource = lstNhomSanPham;
        cbNhomSP.DataBind();
    }
    /// <summary>
    /// Load dữ liệu vào combobox loại tiêu chuẩn áp dụng
    /// </summary>
    /// <modified>
    /// Author      Date            Comment
    /// TuanVM      20/05/2009      Tạo mới
    /// </modified>
    public void Bind_ddlLoaiTieuChuanApDung()
    {
        string NhomSanPhamID = cbNhomSP.SelectedValue;
        DmNhomSanPham objNhomSP = ProviderFactory.DmNhomSanPhamProvider.GetById(NhomSanPhamID);
        if (objNhomSP != null)
        {
            DataTable dtLoaiTieuChuanApDung = new DataTable();
            dtLoaiTieuChuanApDung.Columns.Add("Value");
            dtLoaiTieuChuanApDung.Columns.Add("Text");
            if (objNhomSP.LoaiHinhChungNhan == 1)
            {
                // Hop quy
                dtLoaiTieuChuanApDung.Rows.Add("A", "Chứng nhận hợp quy bắt buộc");
                dtLoaiTieuChuanApDung.Rows.Add("B", "Chứng nhận hợp quy tự nguyện");
            }
            else
            {
                // Hop quy
                dtLoaiTieuChuanApDung.Rows.Add("C", "Chứng nhận hợp chuẩn theo Tiêu chuẩn Quốc gia (TCVN)");
                dtLoaiTieuChuanApDung.Rows.Add("D", "Chứng nhận hợp chuẩn theo Tiêu chuẩn quốc tế");
                dtLoaiTieuChuanApDung.Rows.Add("E", "Chứng nhận hợp chuẩn theo Tiêu chuẩn cơ sở do tổ chức cá nhân đăng ký áp dụng");
            }
            ddlLoaiTieuChuanApDung.DataSource = dtLoaiTieuChuanApDung;
            ddlLoaiTieuChuanApDung.DataTextField = "Text";
            ddlLoaiTieuChuanApDung.DataValueField = "Value";
            ddlLoaiTieuChuanApDung.DataBind();
        }
    }
    /// <summary>
    /// Lay danh sach cac tinh thanh chua duoc gan cho trung tam nao
    /// </summary>
    /// <modified>
    ///  Author                      Date            Action
    ///	Truongtv          06/05/2009      Create new
    /// </modified>
    public void LoadTieuChuanToListCheckbox()
    {

        DataTable lstDMTieuChuan = ProviderFactory.DmTieuChuanProvider.Search("", "MaTieuChuan", 0, 0);
        chkTieuChuan.DataValueField = "Id";
        chkTieuChuan.DataTextField = "matieuchuan";
        chkTieuChuan.DataSource = lstDMTieuChuan;
        chkTieuChuan.DataBind();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
    }
    /// <summary>
    /// Kiểm tra trùng tên
    /// </summary>
    /// <param name="MaDonVi"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Tuannd                5/5/2009              
    /// </Modified>
    public bool CheckTrungTen(string Ten)
    {
        //Neu la sua
        if (Request["ID"] != null)
        {
            string strTenCu = ((DmSanPham)ProviderFactory.DmSanPhamProvider.GetById(Request["ID"].ToString())).TenTiengViet.ToString();
            if (ProviderFactory.DmSanPhamProvider.CheckExist(Ten, strTenCu))
            {
                Thong_bao(Resources.Resource.msgTrungTen);
                return false;
            }
        }
        else
        {
            if (ProviderFactory.DmSanPhamProvider.CheckExist(Ten, string.Empty))
            {
                Thong_bao(Resources.Resource.msgTrungTen);
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Cập nhập sản phẩm
    /// </summary>
    /// <modified>
    ///  Author                      Date            Action
    ///	Truongtv          06/05/2009      Create new
    /// </modified>
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        //SetTieuChuanForSanPham();
        if (CheckTrungTen(txtTenSP.Text.Trim()))
        {
            //Thêm mới
            string id = Request["ID"];
            int intCountItem = 0;
            foreach (ListItem item in chkTieuChuan.Items)
            {
                if (item.Selected)
                {
                    intCountItem++;
                }
            }
            if (intCountItem == 0)
            {
                ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
         "<script>alert('" + "Bạn phải chọn tiêu chuẩn áp dụng" + "');</script>");
                return;
            }
            if (id == null)
            {
                try
                {
                    this.ThemMoi();
                }
                catch (SqlException ex)
                {
                    if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                    {
                        switch (ex.Errors[0].Number)
                        {
                            case 2601: // Primary key violation (Duplicate row)
                                Thong_bao("Mã Sản phẩm  đã tồn tại");
                                break;
                        }
                    }
                }
                ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.DM_SAN_PHAM_THEM_MOI, "Thêm mới sản phẩm");

            }
            else
            {
                try
                {
                    this.CapNhat();
                }
                catch (SqlException ex)
                {
                    if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                    {
                        switch (ex.Errors[0].Number)
                        {
                            case 2601: // Primary key violation (Duplicate row)
                                Thong_bao("Mã Sản phẩm đã tồn tại");
                                break;
                        }
                    }
                }
                ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.DM_SAN_PHAM_SUA, "Sửa sản phẩm");
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    private void CapNhat()
    {
        string id = Request["ID"];
        //Thêm mới 
        DmSanPham objSanPham = ProviderFactory.DmSanPhamProvider.GetById(id);
        objSanPham.TenTiengViet = txtTenSP.Text;
        objSanPham.TenTiengAnh = txtTenSPTA.Text;
        objSanPham.LoaiSanPham = Convert.ToInt16(ddlLoaiSanPham.SelectedValue);
        if (Request["PostBack"] != null)
        {
            if (Request["PostBack"].ToString() == "CB_TiepNhanHoSo_TaoMoi")
            {
                //objSanPham.MaSanPham = "CB";
                objSanPham.NhomSanPhamId = "SPCB";
                objSanPham.LoaiTieuChuanApDung = "C";
            }
            else
            {
                //objSanPham.MaSanPham = txtMaSP.Text;
                objSanPham.NhomSanPhamId = cbNhomSP.SelectedValue.ToString();
                objSanPham.LoaiTieuChuanApDung = ddlLoaiTieuChuanApDung.SelectedValue;
            }
        }
        else
        {
            //objSanPham.MaSanPham = txtMaSP.Text;
            objSanPham.NhomSanPhamId = cbNhomSP.SelectedValue.ToString();
            objSanPham.LoaiTieuChuanApDung = ddlLoaiTieuChuanApDung.SelectedValue;
        }
        if (objSanPham.IsValid)
            ProviderFactory.DmSanPhamProvider.Update(objSanPham);
        else
            throw new Exception(objSanPham.Error);
        //Xoa cu 

        TList<DmSanPhamTieuChuan> lstSanphamTieuChuan = ProviderFactory.DmSanPhamTieuChuanProvider.GetBySanPhamId(id);
        for (int i = 0; i < lstSanphamTieuChuan.Count; i++)
        {
            ProviderFactory.DmSanPhamTieuChuanProvider.Delete(lstSanphamTieuChuan[i]);
        }

        //Duyệt danh mục tiêu chuẩn
        foreach (ListItem item in chkTieuChuan.Items)
        {
            if (item.Selected)
            {
                String strId = item.Value;
                try
                {
                    //Thêm mới sản phẩm tiêu chuẩn
                    DmSanPhamTieuChuan objSanPhamTieuChuan = new DmSanPhamTieuChuan();
                    objSanPhamTieuChuan.SanPhamId = objSanPham.Id;
                    objSanPhamTieuChuan.Id = "1";
                    //Duyệt danh mục tiêu chuẩn

                    objSanPhamTieuChuan.TieuChuanId = strId;
                    ProviderFactory.DmSanPhamTieuChuanProvider.Save(objSanPhamTieuChuan);

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        //LongHH
        if (QLCL_Patch.CheckGhiChu_SanPham(objSanPham.Id))
        {
            QLCL_Patch.UpdateGhiChu_SanPham(objSanPham.Id, txtGhiChu.Text.Trim());
        }
        else {
            QLCL_Patch.SetGhiChu_SanPham(objSanPham.Id, txtGhiChu.Text.Trim());
        }
        
        //LongHH
        ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                       "<script>alert('" + Resources.Resource.msgCapNhatSanPham + "'); window.opener.location.href='DM_SanPham.aspx';window.close();</script>");

    }
    /// <summary>
    /// 
    /// </summary>
    private void ThemMoi()
    {

        //Thêm mới 
        DmSanPham objSanPham = new DmSanPham();
        objSanPham.TenTiengViet = txtTenSP.Text;
        objSanPham.TenTiengAnh = txtTenSPTA.Text;
        //objSanPham.MaSanPham = txtMaSP.Text;
        objSanPham.Id = "1";
        objSanPham.NhomSanPhamId = cbNhomSP.SelectedValue.ToString();
        objSanPham.LoaiTieuChuanApDung = ddlLoaiTieuChuanApDung.SelectedValue;
        objSanPham.LoaiSanPham = Convert.ToInt16(ddlLoaiSanPham.SelectedValue);
        if (objSanPham.IsValid)
            ProviderFactory.DmSanPhamProvider.Save(objSanPham);
        else
            throw new Exception(objSanPham.Error);
        //LongHH
        bool c = false;
        if (!string.IsNullOrEmpty(txtGhiChu.Text.Trim()))
            c = QLCL_Patch.SetGhiChu_SanPham(objSanPham.Id, txtGhiChu.Text.Trim());
        //LongHH
        //Duyệt danh mục tiêu chuẩn

        foreach (ListItem item in chkTieuChuan.Items)
        {
            if (item.Selected)
            {
                String strId = item.Value;
                try
                {
                    //Thêm mới sản phẩm tiêu chuẩn
                    DmSanPhamTieuChuan objSanPhamTieuChuan = new DmSanPhamTieuChuan();

                    objSanPhamTieuChuan.SanPhamId = objSanPham.Id;
                    objSanPhamTieuChuan.Id = "1";

                    objSanPhamTieuChuan.TieuChuanId = strId;
                    ProviderFactory.DmSanPhamTieuChuanProvider.Save(objSanPhamTieuChuan);

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        
        if (Request["PostBack"] != null)
        {
            Thong_bao(Resources.Resource.msgThemmoiSanPham);
            
            ClientScript.RegisterStartupScript(this.GetType(), "msg", "window.close();window.opener.__doPostBack('SanPhamPostBack', '" + objSanPham.Id + "');", true);
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                    "<script>alert('" + Resources.Resource.msgThemmoiSanPham +  "');window.opener.location.href='DM_SanPham.aspx';window.close();</script>");
        }



    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ID"></param>
    public void BinSanPhamForEdit(String ID)
    {
        DmSanPham objDMSanPham = ProviderFactory.DmSanPhamProvider.GetById(ID);
        //txtMaSP.Text = objDMSanPham.MaSanPham;
        txtTenSP.Text = objDMSanPham.TenTiengViet;
        txtTenSPTA.Text = objDMSanPham.TenTiengAnh;
        cbNhomSP.SelectedValue = objDMSanPham.NhomSanPhamId;
        Bind_ddlLoaiTieuChuanApDung();
        ddlLoaiTieuChuanApDung.SelectedValue = objDMSanPham.LoaiTieuChuanApDung;
        ddlLoaiSanPham.SelectedValue = objDMSanPham.LoaiSanPham.ToString();
        //LongHH
        txtGhiChu.Text = QLCL_Patch.GetGhiChu_SanPham(objDMSanPham.Id);
        //LongHH
        //Lấy danh sách các sanpham_tieuchuanapdung theo sản phẩm                
        TList<SanPhamTieuChuanApDung> lstSPTieuChuanApDung = new TList<SanPhamTieuChuanApDung>();
        TList<SanPham> lstSanPham = ProviderFactory.SanPhamProvider.GetBySanPhamId(ID);
        foreach (SanPham objSanPham in lstSanPham)
        {
            TList<SanPhamTieuChuanApDung> lstSanPhamTieuChuanApDung_Temp = new TList<SanPhamTieuChuanApDung>();
            lstSanPhamTieuChuanApDung_Temp = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(objSanPham.Id);
            foreach (SanPhamTieuChuanApDung objSPTCAP in lstSanPhamTieuChuanApDung_Temp)
            {
                if (!lstSPTieuChuanApDung.Contains(objSPTCAP))
                    lstSPTieuChuanApDung.Add(objSPTCAP);
            }

        }

        TList<DmSanPhamTieuChuan> lstTieuChuan = ProviderFactory.DmSanPhamTieuChuanProvider.GetBySanPhamId(ID);
        foreach (DmSanPhamTieuChuan sptc in lstTieuChuan)
        {
            DmTieuChuan TieuChuan = ProviderFactory.DmTieuChuanProvider.GetById(sptc.TieuChuanId);
            if (TieuChuan != null)
            {
                ListItem Item = new ListItem(TieuChuan.MaTieuChuan, TieuChuan.Id);
                Item.Selected = true;
                chkTieuChuan.Items.Add(Item);
            }
        }


        //for (int iTieuChuan = 0; iTieuChuan < lstTieuChuan.Count; iTieuChuan++)
        //{
        //    for (int i = 0; i < chkTieuChuan.Items.Count; i++)
        //    {
        //        if (chkTieuChuan.Items[i].Value == lstTieuChuan[iTieuChuan].TieuChuanId)
        //        {
        //            chkTieuChuan.Items[i].Selected = true;
        //            //disable những item đã được sử dụng
        //            //foreach (SanPhamTieuChuanApDung objSanPhamTieuChuanApDung in lstSPTieuChuanApDung)
        //            //{
        //            //    if (chkTieuChuan.Items[i].Value == objSanPhamTieuChuanApDung.TieuChuanApDungId)
        //            //        chkTieuChuan.Items[i].Enabled = false;
        //            //}
        //        }

        //    }
        //}
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="WhereClause"></param>
    private void LayDanhSach(string WhereClause)
    {
        DataTable dtTieuChuans = ProviderFactory.DmTieuChuanProvider.Search(WhereClause, gvTieuChuan.OrderBy, gvTieuChuan.PageIndex + 1, gvTieuChuan.PageSize);
        gvTieuChuan.DataSource = dtTieuChuans;
        if (dtTieuChuans.Rows.Count > 0)
            gvTieuChuan.VirtualItemCount = int.Parse(dtTieuChuans.Rows[0]["TongSoBanGhi"].ToString());
        gvTieuChuan.DataBind();
    }
    /// <summary>
    /// 
    /// </summary>
    public void setWhereClause()
    {
        if (!txtMaTC.Text.Trim().Equals(""))
            mWhereClause += " AND MaTieuChuan LIKE N'%" + txtMaTC.Text.Trim().Replace("'", "") + "%'";
        if (!txtTenTieuChuan.Text.Trim().Equals(""))
            mWhereClause += " AND TenTieuChuan LIKE N'%" + txtTenTieuChuan.Text.Trim().Replace("'", "") + "%'";
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnTimKiem_Click(object sender, EventArgs e)
    {
        //SetTieuChuanForSanPham();
        setWhereClause();
        gvTieuChuan.PageIndex = 0;
        LayDanhSach(mWhereClause);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvTieuChuan_DataBound(object sender, EventArgs e)
    {
       
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvTieuChuan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTieuChuan.PageIndex = e.NewPageIndex;
        setWhereClause();
        //SetTieuChuanForSanPham();
        LayDanhSach(mWhereClause);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvTieuChuan_Sorting(object sender, GridViewSortEventArgs e)
    {
        setWhereClause();
        LayDanhSach(mWhereClause);
    }
    /// <summary>
    /// 
    /// </summary>
    public void SetTieuChuanForSanPham()
    {
        String id = string.Empty;
        String TenTieuChuan = string.Empty;
        foreach (GridViewRow row in gvTieuChuan.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkCheck");
            if (chk.Checked)
            {

                id = gvTieuChuan.DataKeys[row.RowIndex][0].ToString();
                TenTieuChuan = gvTieuChuan.DataKeys[row.RowIndex][1].ToString();
                if (CheckTrungTieuChuan(id))
                {
                    ListItem Item = new ListItem(TenTieuChuan, id);
                    Item.Selected = true;
                    chkTieuChuan.Items.Add(Item);
                }
            }




        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    public void RemoveTieuChuanForSanPham(String TieuChuanId)
    {
        foreach (ListItem item in chkTieuChuan.Items)
        {
            if (item.Value == TieuChuanId)
            {
                chkTieuChuan.Items.Remove(item);
                break;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool CheckTrungTieuChuan(String value)
    {
        foreach (ListItem item in chkTieuChuan.Items)
        {
            if (item.Value == value)
            {
                if (!item.Selected)
                    item.Selected = true;
                return false;
            }

        }
        return true;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckBox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        if (chk.Checked)
        {
            SetTieuChuanForSanPham();
        }
        else
        {
            String id = chk.Text;
            RemoveTieuChuanForSanPham(id);

        }

    }
    protected void gvTieuChuan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    CheckBox chk = new CheckBox();
        //    chk.ID = "chkCheck";
        //    chk.SkinID = DataBinder.Eval(e.Row.DataItem,"id");
        //    chk.AutoPostBack = true;
        //    chk.Attributes.Add("OnCheckedChanged", "CheckBox_CheckedChanged");
        //    e.Row.Cells[0].Controls.Add(chk);
        //}
    }
    protected void cbNhomSP_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_ddlLoaiTieuChuanApDung();
    }
}

