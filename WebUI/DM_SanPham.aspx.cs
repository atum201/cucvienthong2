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
using System.Collections.Generic;
using Resources;

/// <summary>
/// Danh mục sản phẩm
/// </summary>
/// <Modified>
/// Name		Date		Comments
/// TruongTv	6/6/2009	Thêm mới
/// NguyenQuy   12/05/2009  Sua
/// </Modified>>
public partial class WebUI_DM_SanPham : PageBase
{
    struct ThongTinTimkiem
    {
        //public string MaSanPham;
        public string TenTiengViet;
        public string TenTiengAnh;
        public string MaNhom;
        public string LoaiTieuChuan;
        public string TieuChuanApDung;        
    }
    ThongTinTimkiem ThongTinTk = new ThongTinTimkiem();
    string mWhereClause = " ";
    List<string> deleteSuccess = new List<string>();
    List<string> deleteFail = new List<string>();

    /// <summary>
    /// Hiển thị dữ liệu
    /// </summary>
    /// <Modified>
    /// Name		Date		Comments
    /// TruongTv	6/6/2009	Thêm mới
    /// NguyenQuy   12/05/2009  Sua
    /// </Modified>>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            Session["ThongTinTimKiem"] = null;            
            LayDanhSach(mWhereClause);
            this.LoadNhomSanPham();
            this.LoadTieuChuanToListCheckbox();
            checkPermission();
        }
    }
    /// <summary>
    /// Kiểm tra quyền trên trang
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// giangum                 7/5/2009              
    /// </Modified>
    void checkPermission()
    {
        if (!mUserInfo.IsPermission(EnPermission.QUANLY_DM_SANPHAM))
        {
            imgThemMoi.Visible = false;
            lnkXoa.Visible = false;
            llbThemMoi.Visible = false;
        }
    }
    /// <summary>
    /// Lay du lieu tu database ra grid
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// NguyenQuy              9/5/2009              
    /// </Modified>
    private void LayDanhSach(string WhereClause)
    {
        //DataTable dtSanPhams = ProviderFactory.DmSanPhamProvider.Search(WhereClause, gvSanPham.OrderBy, gvSanPham.PageIndex + 1, gvSanPham.PageSize);
        //gvSanPham.DataSource = dtSanPhams;
        //if (dtSanPhams.Rows.Count > 0)
        //    gvSanPham.VirtualItemCount = int.Parse(dtSanPhams.Rows[0]["TongSoBanGhi"].ToString());
        DataTable dtSanPhams = ProviderFactory.DmSanPhamProvider.GetByConditions(WhereClause);
        DataTable dt = new DataTable();
        dt.Columns.Add("id");
        dt.Columns.Add("tentiengviet");
        dt.Columns.Add("tentienganh");
        dt.Columns.Add("manhom");
        dt.Columns.Add("LoaiTieuChuan_Text");
        dt.Columns.Add("TieuChuan");
        foreach (DataRow row in dtSanPhams.Rows)
        {
            DataRow r = dt.NewRow();
            r["id"] = row["id"].ToString();
            r["tentiengviet"] = row["tentiengviet"].ToString();
            r["tentienganh"] = row["tentienganh"].ToString();
            r["manhom"] = row["manhom"].ToString();
            r["LoaiTieuChuan_Text"] = row["LoaiTieuChuan_Text"].ToString();
            //tiến hành ghép các tiêu chuẩn áp dụng cho sản phẩm
            string IdSp = row["id"].ToString();
            string TieuChuan = string.Empty;
            TList<DmSanPhamTieuChuan> ListSanPhamTieuChuan = ProviderFactory.DmSanPhamTieuChuanProvider.GetBySanPhamId(IdSp);
            if (ListSanPhamTieuChuan != null)
            {
                foreach (DmSanPhamTieuChuan dmSpTieuChuan in ListSanPhamTieuChuan)
                {
                    DmTieuChuan dmTieuChuan = ProviderFactory.DmTieuChuanProvider.GetById(dmSpTieuChuan.TieuChuanId);
                    TieuChuan += dmTieuChuan.MaTieuChuan + ",";
                }
            }
            if(TieuChuan.Length>0)
                TieuChuan = TieuChuan.Remove(TieuChuan.Length - 1, 1);
            r["TieuChuan"] = TieuChuan;
            dt.Rows.Add(r);
            
        }
        gvSanPham.DataSource = dt;
        gvSanPham.DataBind();
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
        DmNhomSanPham objNhomSanPham = new DmNhomSanPham();
        objNhomSanPham.Id = "";
        objNhomSanPham.MaNhom = "--Tất cả--";
        lstNhomSanPham.Insert(0, objNhomSanPham);

        cbNhomSP.DataValueField = "ID";
        cbNhomSP.DataTextField = "MaNhom";
        //Đổ dữ liệu lên Grid      
        cbNhomSP.DataSource = lstNhomSanPham;
        cbNhomSP.DataBind();
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

        TList<DmTieuChuan> lstDMTieuChuan = ProviderFactory.DmTieuChuanProvider.GetAll();
        chkTieuChuan.DataValueField = "Id";
        chkTieuChuan.DataTextField = "matieuchuan";
        chkTieuChuan.DataSource = lstDMTieuChuan;
        chkTieuChuan.DataBind();
    }
    /// <summary>
    /// Kiểm tra xem item này đã đồng bộ hay chưa
    /// </summary>
    /// <returns></returns>
    /// Name        Date        Comment
    /// TuanND    1/6/2009     Thêm mới
    /// </Modified>
    public bool DaDongBo()
    {
        string strDsDaDongBo = "";
        string mMa = "";
        foreach (GridViewRow gvr in gvSanPham.Rows)
        {
            CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
            //xoa hang san xuat duoc chon
            if (cb.Checked)
            {
                mMa = gvSanPham.DataKeys[gvr.RowIndex][0].ToString();
                DmSanPham obj = ProviderFactory.DmSanPhamProvider.GetById(mMa);
                if (obj == null)
                {
                    // Load lại dữ liệu
                    setWhereClause();
                    LayDanhSach(mWhereClause);
                    return true;
                }
                if (obj.DaDongBo == true)
                {
                    strDsDaDongBo += obj.TenTiengViet.ToString();
                    Thong_bao(Resource.msgDongBoError);
                    return true;
                }
            }
        }
        return false;
    }
    protected void lnkXoa_Click(object sender, EventArgs e)
    {
        if (!DaDongBo())
        {
            // Thực hiện xoá
            try
            {
                string TenSanPham = string.Empty;
                TransactionManager transaction = ProviderFactory.Transaction;
                foreach (GridViewRow row in gvSanPham.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkCheck");
                    if (chk.Checked)
                    {
                        string id = gvSanPham.DataKeys[row.RowIndex][0].ToString();
                        TenSanPham = gvSanPham.DataKeys[row.RowIndex][1].ToString();
                        TList<DmSanPhamTieuChuan> lstSanphamTieuChuan = ProviderFactory.DmSanPhamTieuChuanProvider.GetBySanPhamId(id);

                        try
                        {

                            for (int i = 0; i < lstSanphamTieuChuan.Count; i++)
                            {
                                ProviderFactory.DmSanPhamTieuChuanProvider.Delete(lstSanphamTieuChuan[i]);
                            }
                            ProviderFactory.DmSanPhamProvider.Delete(transaction, id);
                            deleteSuccess.Add(TenSanPham);
                        }
                        catch (SqlException ex)
                        {

                            if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                            {
                                if (ex.Errors[0].Number == 547)
                                {
                                    deleteFail.Add(TenSanPham);
                                }
                                else
                                {
                                    transaction.Rollback();
                                    throw ex;
                                }
                            }
                        }


                    }


                }

                if (deleteFail.Count > 0)
                {
                    string lsSanPham = string.Join(", ", deleteFail.ToArray());
                    Thong_bao(string.Format(Resources.Resource.msgXoaSanPhamThatBai, lsSanPham));
                    transaction.Rollback();

                }
                else // Đưa ra thông báo thành công và ghi log sự kiện
                {
                    transaction.Commit();
                    // Thông báo xóa thành công (danh sách đã xóa) + ghi log (danh sách đã xóa)          
                    string strLogString = string.Format(Resources.Resource.msgXoaSanPhamThanhCong, string.Join(",", deleteSuccess.ToArray()));
                    Thong_bao(strLogString);
                    ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.DM_SAN_PHAM_XOA, strLogString);
               
                }
                // Load lại dữ liệu
                setWhereClause();
                LayDanhSach(mWhereClause);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
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


    }
    protected void btnTimKiem_Click(object sender, EventArgs e)
    {
        GetThongTinTimKiem();
        setWhereClause();
        gvSanPham.PageIndex = 0;
        LayDanhSach(mWhereClause);
    }
    public void setWhereClause()
    {
        string strMaTieuChuan = string.Empty;
        foreach (ListItem item in chkTieuChuan.Items)
        {
            if (item.Selected)
            {
                strMaTieuChuan += "'" + item.Value + "',";
            }
        }
        
        //if (!txtMaSP.Text.Trim().Equals(""))
        //    mWhereClause += " AND sp.MaSanPham LIKE N'%" + txtMaSP.Text.Trim().Replace("'", "") + "%'";

        if (!txtTenSP.Text.Trim().Equals(""))
            mWhereClause += " AND sp.TenTiengViet LIKE N'%" + txtTenSP.Text.Trim().Replace("'", "") + "%'";

        if (!txtTenSPTA.Text.Trim().Equals(""))
            mWhereClause += " AND sp.TenTiengAnh LIKE N'%" + txtTenSPTA.Text.Trim().Replace("'", "") + "%'";

        if (cbNhomSP.SelectedIndex!=0)
            mWhereClause += " AND sp.NhomSanPhamID = N'" + cbNhomSP.SelectedItem.Value.ToString() + "'";

        if (strMaTieuChuan.Length > 0)
        {
            strMaTieuChuan = strMaTieuChuan.Substring(0, strMaTieuChuan.Length - 1);
            mWhereClause += " AND sptc.tieuchuanid IN (" + strMaTieuChuan + ")";
        }
        if (ddlLoaiTieuChuanApDung.SelectedIndex!=0)
            mWhereClause += " AND sp.LoaiTieuChuanApDung = N'" + ddlLoaiTieuChuanApDung.SelectedItem.Value.ToString() + "'";
    }
    protected void gvSanPham_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //bind value for Extension facility Hour from, Hour to
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    string ObjectID = gvSanPham.DataKeys[e.Row.RowIndex][0].ToString();
        //    //Label lblStandard = (Label)e.Row.FindControl("lblStandard");
        //    Label lblStandard = new Label();
        //    lblStandard.Text = ProviderFactory.DmTieuChuanProvider.GetChuanApDungbySanPhamID(ObjectID);
        //    e.Row.Cells[3].Controls.Add(lblStandard);
        //}
    }
    protected void gvSanPham_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {        
        gvSanPham.PageIndex = e.NewPageIndex;
        GetThongTinTimKiem();
        SetBackThongTinTimKiem();
        setWhereClause();
        LayDanhSach(mWhereClause);
    }
    protected void gvSanPham_Sorting(object sender, GridViewSortEventArgs e)
    {
        setWhereClause();
        LayDanhSach(mWhereClause);
    }
    /// <summary>
    /// 
    /// </summary>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    public void GetThongTinTimKiem()
    {
        //ThongTinTk.MaSanPham = txtMaSP.Text;
        ThongTinTk.TenTiengViet = txtTenSP.Text;
        ThongTinTk.TenTiengAnh = txtTenSPTA.Text;
        ThongTinTk.MaNhom = cbNhomSP.SelectedValue;
        ThongTinTk.LoaiTieuChuan = ddlLoaiTieuChuanApDung.SelectedValue;        
        string temp = string.Empty;
        foreach (ListItem item in chkTieuChuan.Items)
        {
            if (item.Selected)
            {

                temp += item.Value + ",";
            }
        }
        if (temp.Length > 0)
            temp= temp.Remove(temp.LastIndexOf(','), 1);
        ThongTinTk.TieuChuanApDung = temp;
       
        Session["ThongTinTimKiem"] = ThongTinTk;
    }
    /// <summary>
    /// 
    /// </summary>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    public void SetBackThongTinTimKiem()
    {

        //txtMaSP.Text = string.Empty;
        txtTenSP.Text = string.Empty;
        txtTenSPTA.Text = string.Empty;
        ddlLoaiTieuChuanApDung.SelectedIndex =0;
        cbNhomSP.SelectedIndex = 0;
        chkTieuChuan.ClearSelection();
        if (Session["ThongTinTimKiem"] != null)
        {
            ThongTinTk = (ThongTinTimkiem)Session["ThongTinTimKiem"];
           // txtMaSP.Text = ThongTinTk.MaSanPham;
            txtTenSP.Text = ThongTinTk.TenTiengViet;
            txtTenSPTA.Text = ThongTinTk.TenTiengAnh;
            ddlLoaiTieuChuanApDung.SelectedValue = ThongTinTk.LoaiTieuChuan;
            cbNhomSP.SelectedValue = ThongTinTk.MaNhom;
            if (ThongTinTk.TieuChuanApDung.Length > 0)
            {
                String[] arr = ThongTinTk.TieuChuanApDung.Split(',');
                foreach (ListItem item in chkTieuChuan.Items)
                {
                   
                    if (Array.IndexOf(arr, item.Value) >= 0)
                        item.Selected = true;
                }
            }
        }
    }
}
