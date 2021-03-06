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
/// Modified by : NguyenQuy
/// Date        : May 20, 2009
/// Purpose     : lay ra tu csdl danh sach co quan do kiem
/// </summary>
public partial class WebUI_DM_CoQuanDoKiem : PageBase
{
    List<string> deleteSuccess = new List<string>();
    List<string> deleteFail = new List<string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //goi ham load du lieu len grid
            LayDanhSach();
            checkPermission();
        }
        else
        {
            //Refresh lai trang sau khi them moi
            if (this.Request["__EVENTTARGET"] == "AddNewCommit")
            { LayDanhSach(); }
        }
    }
    /// <summary>
    /// Kiểm tra quyền của người dùng
    /// </summary>
    void checkPermission()
    {
        if (!mUserInfo.IsPermission(EnPermission.QUANLY_DM_COQUAN_DOKIEM))
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
    private void LayDanhSach()
    {
        string whereClause = string.Empty;
        if(!string.IsNullOrEmpty( txtTenCQDK.Text))
            whereClause = " AND UPPER(TenCoQuanDoKiem) LIKE N'%" + txtTenCQDK.Text.Trim() + "%'";
        if (!string.IsNullOrEmpty(txtTenTiengAnh.Text))
            whereClause += " AND UPPER(TenTiengAnh) LIKE N'%" + txtTenTiengAnh.Text.Trim() + "%'";
        DataTable dtCoQuanDK = ProviderFactory.DmCoQuanDoKiemProvider.Search(whereClause, gvCoQuanDK.OrderBy, gvCoQuanDK.PageIndex + 1, gvCoQuanDK.PageSize);
        gvCoQuanDK.DataSource = dtCoQuanDK;
        if (dtCoQuanDK.Rows.Count > 0)
            gvCoQuanDK.VirtualItemCount = int.Parse(dtCoQuanDK.Rows[0]["TongSoBanGhi"].ToString());
        gvCoQuanDK.DataBind();
    }
    /// <summary>
    /// Check, check all checkbox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// NguyenQuy                 7/5/2009              
    /// </Modified>
    protected void gvCoQuanDK_DataBound(object sender, EventArgs e)
    {
        int TotalChkBx = 0;
        if (gvCoQuanDK.HeaderRow != null)
        {
            // Lấy tham chiếu đến checkbox header
            CheckBox cbHeader = (CheckBox)gvCoQuanDK.HeaderRow.FindControl("chkCheckAll");
            // Bắt sự kiện onclick của checkbox toàn bộ
            cbHeader.Attributes["onclick"] = string.Format("HeaderClick('{0}');", gvCoQuanDK.ID);

            // Duyệt trên gridview
            foreach (GridViewRow gvr in gvCoQuanDK.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
                cb.Attributes["onclick"] = string.Format("ChildClick('{0}');", gvCoQuanDK.ID);
                // Đếm số lượng check box con
                TotalChkBx++;
            }
        }
        // Gán giá trị của biến
        this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "TotalChkBx", string.Format("TotalChkBx={0};Counter={1};", TotalChkBx, 0), true);
    }
    /// <summary>
    /// Kiểm tra xem item này đã đồng bộ hay chưa
    /// </summary>
    /// <returns></returns>
    public bool DaDongBo()
    {
        string strDsDaDongBo = "";
        string mMa = "";
        foreach (GridViewRow gvr in gvCoQuanDK.Rows)
        {
            CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
            //xoa hang san xuat duoc chon
            if (cb.Checked)
            {
                mMa = gvCoQuanDK.DataKeys[gvr.RowIndex][0].ToString();
                DmCoQuanDoKiem obj = ProviderFactory.DmCoQuanDoKiemProvider.GetById(mMa);
                if (obj == null)
                {
                    LayDanhSach();
                    return true;
                }
                if (obj.DaDongBo == true)
                {
                    strDsDaDongBo += obj.TenCoQuanDoKiem.ToString();
                    Thong_bao(Resource.msgDongBoError);
                    return true;
                }
            }
        }
        return false;
    }
    /// <summary>
    /// Xoa cac hang san xuat duoc chon trong grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// NguyenQuy                 7/5/2009      
    /// Nguyễn Trung Tuyến      21/05/09                sửa
    /// </Modified>
    protected void lnkXoa_Click(object sender, EventArgs e)
    {
        if (!DaDongBo())
        {
            string mMa = string.Empty;
            String TenCoQuan = string.Empty;
            TransactionManager transaction = ProviderFactory.Transaction;
            try
            {
                // Duyệt trên gridview
                foreach (GridViewRow gvr in gvCoQuanDK.Rows)
                {
                    CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
                    //xoa hang san xuat duoc chon
                    if (cb.Checked)
                    {
                        mMa = gvCoQuanDK.DataKeys[gvr.RowIndex][0].ToString();
                        TenCoQuan = gvCoQuanDK.DataKeys[gvr.RowIndex][1].ToString();
                        ProviderFactory.DmCoQuanDoKiemProvider.Delete(transaction, mMa);
                        deleteSuccess.Add(TenCoQuan);
                    }
                }
                //transaction.Commit();

            }
            catch (SqlException SqlEx)
            {
                if (SqlEx.Errors[0].Number == 547)
                    deleteFail.Add(TenCoQuan);
                else
                {
                    transaction.Rollback();
                    throw SqlEx;
                }
            }
            // Đưa ra thông báo thành công và ghi log sự kiện
            if (deleteFail.Count > 0)
            {
                string lsCoQuan = string.Join(", ", deleteFail.ToArray());
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Xóa không thành công! Các danh mục: " + lsLePhi + " đang được sử dụng');</script>");
                Thong_bao(string.Format(Resources.Resource.msgXoaCoQuanDoKiemThatBai, lsCoQuan));
                transaction.Rollback();
                //LayDanhSach();

            }
            if (deleteSuccess.Count > 0)
            {
                transaction.Commit();
                string lsCoQuan = string.Join(", ", deleteSuccess.ToArray());
                ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.DM_CO_QUAN_DO_KIEM_XOA, Resources.Resource.msgDoKiemXoa);
                Thong_bao(string.Format(Resources.Resource.msgXoaCoQuanDoKiemThanhCong, lsCoQuan));

            }
        }
        LayDanhSach();
    }
    /// <summary>
    /// load lai du lieu ra grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// NguyenQuy                 7/5/2009              
    /// </Modified>
    protected void lnkRefresh_Click(object sender, EventArgs e)
    {
        LayDanhSach();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvCoQuanDK_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCoQuanDK.PageIndex = e.NewPageIndex;
        LayDanhSach();
    }

    protected void gvCoQuanDK_Sorting(object sender, GridViewSortEventArgs e)
    {
        LayDanhSach();
    }

    /// <summary>
    /// Xử lý sự kiện tìm kiếm
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnTimKiem_Click(object sender, EventArgs e)
    {
        gvCoQuanDK.PageIndex = 0;
        LayDanhSach();
    }
}
