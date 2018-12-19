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
/// Quản lý danh mục phòng ban
/// </summary>
/// <Modified>
/// Người tạo              Ngày tạo                Chú thích
/// QuanNM                 7/5/2009              
/// </Modified>
public partial class WebUI_DM_PhongBan : PageBase
{
    List<string> deleteSuccess = new List<string>();
    List<string> deleteFail = new List<string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_gvPhongBan();
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
        if (!mUserInfo.IsPermission(EnPermission.QUANLY_DM_PHONGBAN))
        {
            //imgbtnThemMoi.Visible = false;
            //imgbtnXoa.Visible = false;
            //lnkThemMoi.Visible = false;
            //lnkXoa.Visible = false;
        }
    }
    /// <summary>
    /// Lấy tất cả danh sách phòng ban
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 7/5/2009              
    /// </Modified>
    protected void Bind_gvPhongBan()
    {
        //TList<DmPhongBan> list = ProviderFactory.DmPhongBanProvider.LayDanhMucPhongBan();
        gvPhongBan.DataSource = ProviderFactory.DmPhongBanProvider.LayDanhMucPhongBan(gvPhongBan.OrderBy, mUserInfo.UserID);
        gvPhongBan.DataBind();
    }

    /// <summary>
    /// Xóa phòng ban
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 7/5/2009              
    /// </Modified>
    protected void btnXoa_Click(object sender, EventArgs e)
    {
        // Thực hiện xoá
        string id = string.Empty;
        String TenPhongBan = string.Empty;
        TransactionManager transaction = ProviderFactory.Transaction;
        foreach (GridViewRow row in gvPhongBan.Rows)
        {
            try
            {
                CheckBox chk = (CheckBox)row.FindControl("chkCheck");
                if (chk.Checked)
                {
                    id = gvPhongBan.DataKeys[row.RowIndex][0].ToString();
                    TenPhongBan = gvPhongBan.DataKeys[row.RowIndex][1].ToString();
                    ProviderFactory.DmPhongBanProvider.Delete(transaction, id);
                    deleteSuccess.Add(TenPhongBan);
                }
            }
            catch (SqlException ex)
            {

                if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                {
                    if (ex.Errors[0].Number == 547)
                    {
                        deleteFail.Add(TenPhongBan);
                    }
                    else
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }

        }

        // Đưa ra thông báo thành công và ghi log sự kiện
        if (deleteFail.Count > 0)
        {
            string lsPhongBan = string.Join(", ", deleteFail.ToArray());
            Thong_bao(string.Format(Resources.Resource.msgXoaPhongBanThatBai, lsPhongBan));
            transaction.Rollback();

        }
        else
        {
            transaction.Commit();
            // Thông báo xóa thành công (danh sách đã xóa) + ghi log (danh sách đã xóa)          
            String strLogString = string.Format(Resources.Resource.msgXoaPhongBanThanhCong, string.Join(",", deleteSuccess.ToArray()));
            Thong_bao(strLogString);
            ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.DM_PHONG_XOA, strLogString);

        }
        // Load lại dữ liệu
        Bind_gvPhongBan();
    }
    /// <summary>
    /// Xóa phòng ban
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 7/5/2009              
    /// </Modified>
    protected void imgbtnXoa_Click(object sender, ImageClickEventArgs e)
    {
        btnXoa_Click(null, null);
    }
    /// <summary>
    /// Check, check all checkbox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 7/5/2009              
    /// </Modified>
    protected void gvPhongBan_DataBound(object sender, EventArgs e)
    {
        int TotalChkBx = 0;
        if (gvPhongBan.HeaderRow != null)
        {
            //lay tham chieu den checkbox header
            CheckBox cbHeader = (CheckBox)gvPhongBan.HeaderRow.FindControl("chkCheckAll");
            //Bat su kien onclick cua check toan bo
            cbHeader.Attributes["onclick"] = string.Format("HeaderClick('{0}');", gvPhongBan.ID);

            //lap cach dong cua grid
            foreach (GridViewRow gvr in gvPhongBan.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
                cb.Attributes["onclick"] = string.Format("ChildClick('{0}');", gvPhongBan.ID);
                //dem so luong checkbox con
                TotalChkBx++;
            }
        }
        //gan lai gia tri cua bien
        this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "TotalChkBx", string.Format("TotalChkBx={0};Counter={1};", TotalChkBx, 0), true);
    }
    protected void gvPhongBan_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bind_gvPhongBan();
    }
}
