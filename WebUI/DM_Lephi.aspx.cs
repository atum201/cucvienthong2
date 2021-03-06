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
using Cuc_QLCL.Data;
using System.Data.SqlClient;
using Resources;

/// <summary>
/// Quản lý danh mục lệ phí
/// </summary>
/// <Modified>
/// Người tạo              Ngày tạo                Chú thích
/// QuanNM                 7/5/2009              
/// </Modified>
public partial class WebUI_DM_Lephi : PageBase
{
    List<string> deleteSuccess = new List<string>();
    List<string> deleteFail = new List<string>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
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
        if (!mUserInfo.IsPermission(EnPermission.QUANLY_DM_HANGSX))
        {
            ImgBtnThemMoi.Visible = false;
            ImgBtnXoa.Visible = false;
            btnXoa.Visible = false;
            LinkBtnThemMoi.Visible = false;

        }
    }
    /// <summary>
    /// Lấy tất cả danh sách lệ phí
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 7/5/2009              
    /// </Modified>
    protected void BindGridView()
    {
        TList<DmLePhi> list = ProviderFactory.DmLePhiProvider.GetAll();
        gvLePhi.DataSource = list;
        gvLePhi.DataBind();
    }
    /// <summary>
    /// Xóa danh mục lệ phí
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 7/5/2009              
    /// </Modified>
    private void DeleteLePhi()
    {
        // Thực hiện xoá
        TransactionManager transaction = ProviderFactory.Transaction;
        try
        {
            string id = "";
            foreach (GridViewRow row in gvLePhi.Rows)
            {
                String GiatriLoHang = string.Empty;
                CheckBox chk = (CheckBox)row.FindControl("chkCheck");
                if (chk.Checked)
                {
                    try
                    {

                        id = gvLePhi.DataKeys[row.RowIndex][0].ToString();
                        GiatriLoHang = gvLePhi.DataKeys[row.RowIndex][1].ToString();
                        ProviderFactory.DmLePhiProvider.Delete(transaction, id);

                        // Add tên đối tượng đã xóa
                        deleteSuccess.Add(GiatriLoHang);
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                        {
                            if (ex.Errors[0].Number == 547) // Nếu là lỗi ràng buộc dữ liệu thì add vào danh sách xóa lỗi
                            {
                                deleteFail.Add(GiatriLoHang);
                            }
                            else
                            {
                                // Nếu lỗi khác thì rollback, ném ngoại lệ
                                transaction.Rollback();
                                throw ex;
                            }
                        }
                    }
                }
            }

            // Đưa ra thông báo lỗi ràng buộc dữ liệu quan hệ
            if (deleteFail.Count > 0)
            {
                string lsLePhi = string.Join(", ", deleteFail.ToArray());
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Xóa không thành công! Các danh mục: " + lsLePhi + " đang được sử dụng');</script>");
                Thong_bao(string.Format(Resources.Resource.msgXoaLePhiThatBai, lsLePhi));
                transaction.Rollback();

            }
            else
            {
                // Thông báo xóa thành công (danh sách đã xóa) + ghi log (danh sách đã xóa)
                //Log.Write(mUserInfo, LogEventList.ConceptScheme_Delete, "Xoá nhóm khái niệm: " + string.Join(", ", deleteSuccess.ToArray()));
                transaction.Commit();
                String strLogString = string.Format(Resources.Resource.msgXoaLePhi, string.Join(",", deleteSuccess.ToArray()));
                Thong_bao(strLogString);
                ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.DM_LE_PHI_XOA, strLogString);
             
            }
            // Load lại dữ liệu
            BindGridView();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }
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
        foreach (GridViewRow gvr in gvLePhi.Rows)
        {
            CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
            //xoa hang san xuat duoc chon
            if (cb.Checked)
            {
                mMa = gvLePhi.DataKeys[gvr.RowIndex][0].ToString();
                DmLePhi obj = ProviderFactory.DmLePhiProvider.GetById(mMa);
                if (obj == null)
                {
                    BindGridView();
                    return true;
                }
                if (obj.DaDongBo == true)
                {
                    strDsDaDongBo += obj.GiaTriLoHang.ToString();
                    Thong_bao(Resource.msgDongBoError);
                    return true;
                }
            }
        }
        return false;
    }
    /// <summary>
    /// Xóa danh mục lệ phí
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 7/5/2009              
    /// </Modified>
    protected void ImgBtnXoa_Click(object sender, ImageClickEventArgs e)
    {
        if (!DaDongBo())
        {
            DeleteLePhi();
        }
    }
    /// <summary>
    /// Xóa danh mục lệ phí
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 7/5/2009              
    /// </Modified>
    protected void btnXoa_Click(object sender, EventArgs e)
    {
        DeleteLePhi();
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
    protected void gvLePhi_DataBound(object sender, EventArgs e)
    {
        int TotalChkBx = 0;
        if (gvLePhi.HeaderRow != null)
        {
            //lay tham chieu den checkbox header
            CheckBox cbHeader = (CheckBox)gvLePhi.HeaderRow.FindControl("chkCheckAll");
            //Bat su kien onclick cua check toan bo
            cbHeader.Attributes["onclick"] = string.Format("HeaderClick('{0}');", gvLePhi.ID);

            //lap cach dong cua grid
            foreach (GridViewRow gvr in gvLePhi.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
                cb.Attributes["onclick"] = string.Format("ChildClick('{0}');", gvLePhi.ID);
                //dem so luong checkbox con
                TotalChkBx++;
            }
        }
        //gan lai gia tri cua bien
        this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "TotalChkBx", string.Format("TotalChkBx={0};Counter={1};", TotalChkBx, 0), true);
    }

}
