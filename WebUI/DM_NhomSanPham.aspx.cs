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
using System.ComponentModel;
using Cuc_QLCL.AdapterData;
using Cuc_QLCL.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using Cuc_QLCL.Data;
using Resources;

/// <summary>
/// Danh mục nhóm sản phẩm 
/// Thêm mới, sửa, xóa nhóm sản phẩm
/// </summary>
/// <Modified>
/// Name		Date		Comments
/// Trươngtv	2/6/2009	Thêm mới
/// NguyenQuy   09/05/2009  Sua
/// </Modified>>
public partial class WebUI_DM_NhomSanPham : PageBase
{
    struct ThongTinTimkiem
    {
        public String MaNhomTrongGCN;
        public String TenNhom;
        public String LePhi;
        
    }

    ThongTinTimkiem ThongTinTk = new ThongTinTimkiem();
    string mWhereClause = "";
    List<string> deleteSuccess = new List<string>();
    List<string> deleteFail = new List<string>();
    /// <summary>
    /// Load dữ liệu nhóm sản phẩm
    /// </summary>
    /// <Modified>
    /// Name		Date		Comments
    /// Trươngtv	2/6/2009	Thêm mới
    /// </Modified>>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //goi ham load du lieu len grid
            LayDanhSach(mWhereClause);
            checkPermission();
            Session["ThongTinTimKiem"] = null;
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
        if (!mUserInfo.IsPermission(EnPermission.QUANLY_DM_NHOMSP))
        {
            imgThemMoi.Visible = false;
            lnkThemMoi.Visible = false;
            lnkXoa.Visible = false;
        }
    }
    /// <summary>
    /// Lay du lieu tu database ra grid
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// NguyenQuy                 7/5/2009              
    /// </Modified>
    private void LayDanhSach(string WhereClause)
    {
        DataTable dtNhomSP = ProviderFactory.DmNhomSanPhamProvider.Search(mWhereClause, gvNhomSP.OrderBy, gvNhomSP.PageIndex + 1, gvNhomSP.PageSize);
        gvNhomSP.DataSource = dtNhomSP;
        if (dtNhomSP.Rows.Count > 0)
            gvNhomSP.VirtualItemCount = int.Parse(dtNhomSP.Rows[0]["TongSoBanGhi"].ToString());
        gvNhomSP.DataBind();
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
        foreach (GridViewRow gvr in gvNhomSP.Rows)
        {
            CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
            //xoa hang san xuat duoc chon
            if (cb.Checked)
            {
                mMa = gvNhomSP.DataKeys[gvr.RowIndex][0].ToString();
                DmNhomSanPham obj = ProviderFactory.DmNhomSanPhamProvider.GetById(mMa);
                if (obj.DaDongBo == true)
                {
                    strDsDaDongBo += obj.TenNhom.ToString();
                    Thong_bao(Resource.msgDongBoError);
                    return true;
                }
            }
        }
        return false;
    }
    /// <summary>
    /// Xoá 1 hoặc nhiều nhóm sản phẩm
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnXoa_Click(object sender, EventArgs e)
    {

        if (!DaDongBo())
        {
            TransactionManager transaction = ProviderFactory.Transaction;

            // Thực hiện xoá
            String TenNhom = string.Empty;
            foreach (GridViewRow row in gvNhomSP.Rows)
            {
                try
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkCheck");
                    if (chk.Checked)
                    {
                        string id = gvNhomSP.DataKeys[row.RowIndex][0].ToString();

                        TenNhom = gvNhomSP.DataKeys[row.RowIndex][1].ToString();
                        ProviderFactory.DmNhomSanPhamProvider.Delete(transaction, id);
                        deleteSuccess.Add(TenNhom);

                    }
                }
                catch (SqlException ex)
                {

                    if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                    {
                        if (ex.Errors[0].Number == 547)
                        {
                            deleteFail.Add(TenNhom);
                        }
                        else
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            if (deleteFail.Count > 0)
            {
                string lsNhom = string.Join(", ", deleteFail.ToArray());
                Thong_bao(string.Format(Resources.Resource.msgXoaNhomSanPhamThatBai, lsNhom));
                transaction.Rollback();
                setWhereClase();
                LayDanhSach(mWhereClause);

            }
            else
            {
                transaction.Commit();
                // Thông báo xóa thành công (danh sách đã xóa) + ghi log (danh sách đã xóa)          
                String strLogString = string.Format(Resources.Resource.msgXoaNhomSanPhamThanhCong, string.Join(",", deleteSuccess.ToArray()));
                Thong_bao(strLogString);
                ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.DM_NHOM_SAN_PHAM_XOA, strLogString);
                // Load lại dữ liệu
                setWhereClase();
                LayDanhSach(mWhereClause);
            }
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvNhomSP_DataBound(object sender, EventArgs e)
    {

        int TotalChkBx = 0;
        if (gvNhomSP.HeaderRow != null)
        {
            // Lấy tham chiếu đến checkbox header
            CheckBox cbHeader = (CheckBox)gvNhomSP.HeaderRow.FindControl("chkCheckAll");
            // Bắt sự kiện onclick của checkbox toàn bộ
            cbHeader.Attributes["onclick"] = string.Format("HeaderClick('{0}');", gvNhomSP.ID);

            // Duyệt trên gridview
            foreach (GridViewRow gvr in gvNhomSP.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
                cb.Attributes["onclick"] = string.Format("ChildClick('{0}');", gvNhomSP.ID);
                // Đếm số lượng check box con
                TotalChkBx++;
            }
        }
        // Gán giá trị của biến
        this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "TotalChkBx", string.Format("TotalChkBx={0};Counter={1};", TotalChkBx, 0), true);

    }
    /// <summary>
    /// 
    /// </summary>
    void setWhereClase()
    {
        if (!txtMaTT.Text.Trim().Equals(""))
            mWhereClause += " AND MaNhom LIKE N'%" + txtMaTT.Text.Trim().Replace("'", "") + "%'";
        if (!txtTenNhomSP.Text.Trim().Equals(""))
            mWhereClause += " AND TenNhom LIKE N'%" + txtTenNhomSP.Text.Trim().Replace("'", "") + "%'";
        if (!txtMucLePhi.Text.Trim().Equals(""))
            mWhereClause += " AND MucLePhi LIKE N'%" + txtMucLePhi.Text.Trim().Replace(".", "").Replace("'", "") + "%'";

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnTimKiem_Click(object sender, EventArgs e)
    {
        gvNhomSP.PageIndex = 0;
        GetThongTinTimKiem();
        setWhereClase();
        LayDanhSach(mWhereClause);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvNhomSP_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvNhomSP.PageIndex = e.NewPageIndex;
        SetBackThongTinTimKiem();
        setWhereClase();
        LayDanhSach(mWhereClause);
    }

    protected void gvNhomSP_Sorting(object sender, GridViewSortEventArgs e)
    {
        setWhereClase();
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
        ThongTinTk.MaNhomTrongGCN = txtMaTT.Text;
        ThongTinTk.TenNhom = txtTenNhomSP.Text;
        ThongTinTk.LePhi =txtMucLePhi.Text;        

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

        txtMaTT.Text = string.Empty;
        txtTenNhomSP.Text = string.Empty;
        txtMucLePhi.Text = string.Empty;
       
        if (Session["ThongTinTimKiem"] != null)
        {
            ThongTinTk = (ThongTinTimkiem)Session["ThongTinTimKiem"];
            txtMaTT.Text = ThongTinTk.MaNhomTrongGCN;
            txtTenNhomSP.Text = ThongTinTk.TenNhom;
            txtMucLePhi.Text = ThongTinTk.LePhi;
            
        }
    }
}
