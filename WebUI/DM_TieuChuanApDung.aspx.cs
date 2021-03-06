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
using Cuc_QLCL.Entities;
using Cuc_QLCL.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Resources;
/// <summary>
/// NguyenQuy Modified 9/5/2009
/// sua lai su kien search va phan trang
/// </summary>
public partial class WebUI_DM_TieuChuanApDung : PageBase
{
    string mWhereClause = " ";
    List<string> deleteSuccess = new List<string>();
    List<string> deleteFail = new List<string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //goi ham load du lieu len grid
            LayDanhSach(mWhereClause);
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
        if (!mUserInfo.IsPermission(EnPermission.QUANLY_DM_TIEUCHUAN_APDUNG))
        {
            //imgThemMoi.Visible = false;
            //lnkXoa.Visible = false;
            //lnkThemMoi.Visible = false;
        }
    }
    /// <summary>
    /// Lay du lieu tu database ra grid
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// NguyenQuy                 9/5/2009              
    /// </Modified>
    private void LayDanhSach(string WhereClause)
    {
        DataTable dtTieuChuans = ProviderFactory.DmTieuChuanProvider.Search(WhereClause, gvTieuChuan.OrderBy, gvTieuChuan.PageIndex + 1, gvTieuChuan.PageSize);
        gvTieuChuan.DataSource = dtTieuChuans;
        if (dtTieuChuans.Rows.Count > 0)
            gvTieuChuan.VirtualItemCount = int.Parse(dtTieuChuans.Rows[0]["TongSoBanGhi"].ToString());
        gvTieuChuan.DataBind();
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
        foreach (GridViewRow gvr in gvTieuChuan.Rows)
        {
            CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
            //xoa hang san xuat duoc chon
            if (cb.Checked)
            {
                mMa = gvTieuChuan.DataKeys[gvr.RowIndex][0].ToString();
                DmTieuChuan obj = ProviderFactory.DmTieuChuanProvider.GetById(mMa);
                if (obj == null)
                {
                    // Load lại dữ liệu
                    setWhereClause();
                    LayDanhSach(mWhereClause);
                    return true;
                }
                if (obj.DaDongBo == true)
                {
                    strDsDaDongBo += obj.TenTieuChuan.ToString();
                    Thong_bao(Resource.msgDongBoError);
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// xóa danh mục tiêu chuẩn
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// DucLv                 7/5/2009              
    /// </Modified>
    protected void btnXoa_Click(object sender, EventArgs e)
    {
        if (!DaDongBo())
        {
            // Thực hiện xoá
            String TenTieuChuan = string.Empty;
            TransactionManager transaction = ProviderFactory.Transaction;

            foreach (GridViewRow row in gvTieuChuan.Rows)
            {
                try
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkCheck");
                    if (chk.Checked)
                    {

                        string id = gvTieuChuan.DataKeys[row.RowIndex][0].ToString();
                        TenTieuChuan = gvTieuChuan.DataKeys[row.RowIndex][1].ToString();
                        ProviderFactory.DmTieuChuanProvider.Delete(transaction, id);
                        deleteSuccess.Add(TenTieuChuan);
                    }
                }
                catch (SqlException ex)
                {

                    if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                    {
                        if (ex.Errors[0].Number == 547)
                        {
                            deleteFail.Add(TenTieuChuan);
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
                string lsTieuChuan = string.Join(", ", deleteFail.ToArray());
                Thong_bao(string.Format(Resources.Resource.msgXoaTieuChuanThatBai, lsTieuChuan));
                transaction.Rollback();

            }
            else // Đưa ra thông báo thành công và ghi log sự kiện
            {
                transaction.Commit();
                // Thông báo xóa thành công (danh sách đã xóa) + ghi log (danh sách đã xóa)          
                String strLogString = string.Format(Resources.Resource.msgXoaTieuChuan, string.Join(",", deleteSuccess.ToArray()));
                Thong_bao(strLogString);
                ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.DM_TIEU_CHUAN_XOA, strLogString);
                // Load lại dữ liệu
                setWhereClause();
                LayDanhSach(mWhereClause);
            }
        }

        
       
    }
    
    protected void gvTieuChuan_DataBound(object sender, EventArgs e)
    {
        int TotalChkBx = 0;
        if (gvTieuChuan.HeaderRow != null)
        {
            // Lấy tham chiếu đến checkbox header
            CheckBox cbHeader = (CheckBox)gvTieuChuan.HeaderRow.FindControl("chkCheckAll");
            // Bắt sự kiện onclick của checkbox toàn bộ
            cbHeader.Attributes["onclick"] = string.Format("HeaderClick('{0}');", gvTieuChuan.ID);

            // Duyệt trên gridview
            foreach (GridViewRow gvr in gvTieuChuan.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
                cb.Attributes["onclick"] = string.Format("ChildClick('{0}');", gvTieuChuan.ID);
                // Đếm số lượng check box con
                TotalChkBx++;
            }
        }
        // Gán giá trị của biến
        this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "TotalChkBx", string.Format("TotalChkBx={0};Counter={1};", TotalChkBx, 0), true);

    }
    protected void btnTimKiem_Click(object sender, EventArgs e)
    {
        setWhereClause();
        LayDanhSach(mWhereClause);
    }
    void setWhereClause()
    {
        if (!txtMaTC.Text.Trim().Equals(""))
            mWhereClause += " AND MaTieuChuan LIKE N'%" + txtMaTC.Text.Trim().Replace("'", "") + "%'";
        if (!txtTenTieuChuan.Text.Trim().Equals(""))
            mWhereClause += " AND TenTieuChuan LIKE N'%" + txtTenTieuChuan.Text.Trim().Replace("'", "") + "%'";
    }
    protected void gvTieuChuan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTieuChuan.PageIndex = e.NewPageIndex;
        setWhereClause();
        LayDanhSach(mWhereClause);
    }

    protected void gvTieuChuan_Sorting(object sender, GridViewSortEventArgs e)
    {       
        setWhereClause();
        LayDanhSach(mWhereClause);
    }
   
}
