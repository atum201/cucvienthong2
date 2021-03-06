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

public partial class WebUI_DM_HangSanXuat : PageBase
{
    List<string> deleteSuccess = new List<string>();
    List<string> deleteFail = new List<string>();
    /// <summary>
    /// Modified by : NguyenQuy
    /// Date        : May 20, 2009
    /// Purpose     : lay hoac xu ly du lieu tu trong database theo cac dieu kien khac nhau sau do bind vao grid hang san xuat
    /// </summary>
    struct ThongTinTimkiem
    {
        public String TenHangSanXuat;       
    }
    ThongTinTimkiem ThongTinTk = new ThongTinTimkiem();
    string mWhereClause = " ";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //goi ham load du lieu len grid
            LayDanhSach(mWhereClause);
            checkPermission();

            Session["ThongTinTimKiem"] = null;
        }
        else
            if (this.Request["__EVENTTARGET"] == "HangSanXuatPostBack")
            {
                setWhereClause();
                LayDanhSach(mWhereClause);
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
            imgBtnThemMoi.Visible = false;

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
        DataTable dtHangSxs = ProviderFactory.DmHangSanXuatProvider.Search(WhereClause, gvHangSanXuat.OrderBy, gvHangSanXuat.PageIndex + 1, gvHangSanXuat.PageSize);
        gvHangSanXuat.DataSource = dtHangSxs;
        if (dtHangSxs.Rows.Count > 0)
            gvHangSanXuat.VirtualItemCount = int.Parse(dtHangSxs.Rows[0]["TongSoBanGhi"].ToString());
        gvHangSanXuat.DataBind();
    }
    /// <summary>
    /// tim hang san xuat theo ten hang san xuat
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnTimKiem_Click(object sender, EventArgs e)
    {
        gvHangSanXuat.PageIndex = 0;
        GetThongTinTimKiem();
        setWhereClause();
        LayDanhSach(mWhereClause);
    }
    void setWhereClause()
    {
        if (!txtTen.Text.Trim().Equals(""))
            mWhereClause = " TenHangSanXuat LIKE N'%" + txtTen.Text.Trim().Replace("'", "") + "%'";
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
    protected void gvHangSanXuat_DataBound(object sender, EventArgs e)
    {
        int TotalChkBx = 0;
        if (gvHangSanXuat.HeaderRow != null)
        {
            // Lấy tham chiếu đến checkbox header
            CheckBox cbHeader = (CheckBox)gvHangSanXuat.HeaderRow.FindControl("chkCheckAll");
            // Bắt sự kiện onclick của checkbox toàn bộ
            cbHeader.Attributes["onclick"] = string.Format("HeaderClick('{0}');", gvHangSanXuat.ID);

            // Duyệt trên gridview
            foreach (GridViewRow gvr in gvHangSanXuat.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
                cb.Attributes["onclick"] = string.Format("ChildClick('{0}');", gvHangSanXuat.ID);
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
    /// Name        Date        Comment
    /// TuanND    1/6/2009     Thêm mới
    /// </Modified>
    public bool DaDongBo()
    {
        string strDsDaDongBo = "";
        string mMa = "";
        foreach (GridViewRow gvr in gvHangSanXuat.Rows)
        {
            CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
            //xoa hang san xuat duoc chon
            if (cb.Checked)
            {
                mMa = gvHangSanXuat.DataKeys[gvr.RowIndex][0].ToString();
                DmHangSanXuat obj = ProviderFactory.DmHangSanXuatProvider.GetById(mMa);
                if (obj == null)
                {
                    setWhereClause();
                    LayDanhSach(mWhereClause);
                    return true;
                }
                if (obj.DaDongBo == true)
                {
                    strDsDaDongBo += obj.TenHangSanXuat.ToString();
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
    /// </Modified>
    protected void lnkXoa_Click(object sender, EventArgs e)
    {
        if (!DaDongBo())
        {
            string mMa = string.Empty;
            String TenHangSanXuat = string.Empty;
            TransactionManager transaction = ProviderFactory.Transaction;

            // Duyệt trên gridview
            foreach (GridViewRow gvr in gvHangSanXuat.Rows)
            {
                try
                {
                    CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
                    //xoa hang san xuat duoc chon
                    if (cb.Checked)
                    {
                        mMa = gvHangSanXuat.DataKeys[gvr.RowIndex][0].ToString();
                        TenHangSanXuat = gvHangSanXuat.DataKeys[gvr.RowIndex][1].ToString();
                        ProviderFactory.DmHangSanXuatProvider.Delete(transaction, mMa);
                        deleteSuccess.Add(TenHangSanXuat);
                    }
                }
                catch (SqlException ex)
                {

                    if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                    {
                        if (ex.Errors[0].Number == 547)
                        {
                            deleteFail.Add(TenHangSanXuat);
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
                string lsHangSanXuat = string.Join(", ", deleteFail.ToArray());
                Thong_bao(string.Format(Resources.Resource.msgXoaHangSanXuatThatBai, lsHangSanXuat));
                transaction.Rollback();

            }
            else
            {
                transaction.Commit();
                // Thông báo xóa thành công (danh sách đã xóa) + ghi log (danh sách đã xóa)          
                String strLogString = string.Format(Resources.Resource.msgXoaHangSanXuatThanhCong, string.Join(",", deleteSuccess.ToArray()));
                Thong_bao(strLogString);
                ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.DM_HANG_SAN_XUAT_XOA, strLogString);
                // Load lại dữ liệu
                
            }
            setWhereClause();
            LayDanhSach(mWhereClause);
        }
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
        setWhereClause();
        LayDanhSach(mWhereClause);
    }
    protected void gvHangSanXuat_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHangSanXuat.PageIndex = e.NewPageIndex;
        SetBackThongTinTimKiem();
        setWhereClause();
        LayDanhSach(mWhereClause);
    }
    protected void gvHangSanXuat_Sorting(object sender, GridViewSortEventArgs e)
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
        ThongTinTk.TenHangSanXuat = txtTen.Text;
    
       
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

        txtTen.Text = string.Empty;
        
        if (Session["ThongTinTimKiem"] != null)
        {
            ThongTinTk = (ThongTinTimkiem)Session["ThongTinTimKiem"];
            txtTen.Text = ThongTinTk.TenHangSanXuat;
           
        }
    }
}
