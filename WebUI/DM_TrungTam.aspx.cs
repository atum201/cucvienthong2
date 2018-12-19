using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Cuc_QLCL.AdapterData;
using Cuc_QLCL.Entities;
using Cuc_QLCL.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Resources;

public partial class WebUI_DM_TrungTam : PageBase
{
    List<string> deleteSuccess = new List<string>();
    List<string> deleteFail = new List<string>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    ///  Author                      Date            Action
    /// Nguyễn Trung Tuyến          06/05/2009      Create new
    /// </modified>
    protected void Page_Load(object sender, EventArgs e)
    {

        this.ClientScript.GetPostBackEventReference(this, string.Empty);
        if (this.IsPostBack)
        {
            string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];

            if (eventTarget == "ChildWindowPostBack")
            {
                GridViewDataBind();
            }
        }
        if (!IsPostBack)
        {
            checkPermission();
            GridViewDataBind();
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
        if (!mUserInfo.IsPermission(EnPermission.QUANLY_DM_TRUNGTAM))
        {
            imgThemMoi.Visible = false;
            imgXoa.Visible = false;
            lnkXoa.Visible = false;
            lnkThemMoi.Visible = false;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <modified>
    ///  Author                      Date            Action
    /// Nguyễn Trung Tuyến          06/05/2009      Create new
    /// </modified>
    public void GridViewDataBind()
    {
        gvTrungTam.DataSource = ProviderFactory.DmTrungTamProvider.GetALL_RealTrungTam_Extend();
        gvTrungTam.DataBind();
        if (gvTrungTam.Rows.Count > 0)
        {
            imgXoa.Enabled = true;
            lnkXoa.Enabled = true;
        }
        else
        {
            imgXoa.Enabled = false;
            lnkXoa.Enabled = false;
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
        foreach (GridViewRow gvr in gvTrungTam.Rows)
        {
            CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
            //xoa hang san xuat duoc chon
            if (cb.Checked)
            {
                mMa = gvTrungTam.DataKeys[gvr.RowIndex][0].ToString();
                DmTrungTam obj = ProviderFactory.DmTrungTamProvider.GetById(mMa);
                if (obj == null)
                {
                    GridViewDataBind();
                    return true;
                }
                if (obj.DaDongBo == true)
                {
                    strDsDaDongBo += obj.TenTrungTam.ToString();
                    Thong_bao(Resource.msgDongBoError);
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    ///  Author                      Date            Action
    /// Nguyễn Trung Tuyến          06/05/2009      Create new
    /// </modified>
    protected void lkbtnXoa_Click(object sender, EventArgs e)
    {
        if (!DaDongBo())
        {
            CheckBox chk;
            String strTTId = string.Empty;
            TransactionManager transaction = ProviderFactory.Transaction;
            foreach (GridViewRow rowItem in gvTrungTam.Rows)
            {
                chk = (CheckBox)(rowItem.Cells[0].FindControl("chkCheck"));
                if (chk.Checked)
                {
                    String strMaTrungtam = gvTrungTam.DataKeys[rowItem.RowIndex][0].ToString();
                    String TenTrungTam = gvTrungTam.DataKeys[rowItem.RowIndex][1].ToString();

                    //LinkButton lnkSelect = (LinkButton)rowItem.FindControl("lnkSelect");
                    //String strTenTrungTam = lnkSelect.Text;
                    //if (!TrungTamHasChildren(strMaTrungtam, TenTrungTam))
                    //{
                    //tiến hành đưa các Tình thành hiện đang thuộc 
                    //trung tâm về một trung tâm ảo trước khi xóa trung tâm
                    SetTinhThanhOfTrungTamToDefaut(strMaTrungtam, transaction);
                    try
                    {
                        ProviderFactory.DmTrungTamProvider.Delete(transaction, strMaTrungtam);

                        // Add tên đối tượng đã xóa
                        deleteSuccess.Add(TenTrungTam);

                    }
                    catch (SqlException SqlEx)
                    {
                        if (SqlEx.Errors[0].Number == 547)
                            deleteFail.Add(TenTrungTam);
                        else
                        {
                            transaction.Rollback();
                            throw SqlEx;
                        }
                    }


                    //}

                }
            }
            if (deleteFail.Count > 0)
            {
                string lsTrungTam = string.Join(", ", deleteFail.ToArray());
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Xóa không thành công! Các danh mục: " + lsLePhi + " đang được sử dụng');</script>");
                Thong_bao(string.Format(Resources.Resource.msgXoaTrungTamThatBai, lsTrungTam));
                transaction.Rollback();

            }
            else if (deleteSuccess.Count > 0)
            {
                transaction.Commit();
                // Thông báo xóa thành công (danh sách đã xóa) + ghi log (danh sách đã xóa)                      
                String strLogString = string.Format(Resources.Resource.msgXoaTrungTamThanhCong, string.Join(",", deleteSuccess.ToArray()));
                Thong_bao(strLogString);
                ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.DM_TRUNG_TAM_XOA, strLogString);
            }
            // Load lại dữ liệu
            GridViewDataBind();
        }
    }

    /// <summary>
    /// Kiem tram xem Trung tam co ban ghi con khong
    /// </summary>
    /// <param name="TrungTamId"></param>
    /// <returns></returns>
    /// <modified>
    ///  Author                      Date            Action
    /// Nguyễn Trung Tuyến          06/05/2009      Create new
    /// </modified>
    public bool TrungTamHasChildren(String TrungTamId, String TenTrungTam)
    {
        DmTrungTam dmTrungTam = ProviderFactory.DmTrungTamProvider.GetById(TrungTamId);
        if (dmTrungTam != null)
        {
            dmTrungTam.HoSoCollection = ProviderFactory.HoSoProvider.GetByTrungTamId(TrungTamId);
            if (dmTrungTam.HoSoCollection.Count > 0)
            {
                ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                               "<script>alert('Không thể xóa vì đã có Hồ sơ đang thuộc trung tâm" + TenTrungTam + " !') </script>");
                return true;
            }
        }
        dmTrungTam.DmPhongBanCollection = ProviderFactory.DmPhongBanProvider.GetByTrungTamId(TrungTamId);
        if (dmTrungTam != null)
        {
            if (dmTrungTam.DmPhongBanCollection.Count > 0)
            {
                ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                               "<script>alert('Không thể xóa vì đã có Phòng ban thuộc trung tâm " + TenTrungTam + " !') </script>");
                return true;
            }
        }
        //dmTrungTam.DmTinhThanhCollection = ProviderFactory.DmTinhThanhProvider.GetByTrungTamId(TrungTamId);
        //if (dmTrungTam != null)
        //{
        //    if (dmTrungTam.DmTinhThanhCollection.Count > 0)
        //    {
        //        ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
        //                       "<script>alert('Không thể xóa vì đã có Tỉnh thành thuộc trung tâm " + TenTrungTam + " !') </script>");
        //        return true;
        //    }
        //}

        return false;
    }

    /// <summary>
    /// Đưa tât cả các tình thành hiện đang thuộc trung tâm về một trung tâm ảo
    /// </summary>
    /// <param name="IDTrungTam"></param>
    /// <modified>
    ///  Author                       Date            Action
    /// Nguyễn Trung Tuyến          06/05/2009      Create new
    /// </modified>
    public void SetTinhThanhOfTrungTamToDefaut(String IDTrungTam, TransactionManager transaction)
    {
        TList<DmTinhThanh> lstDmTinhThanh = ProviderFactory.DmTinhThanhProvider.GetByTrungTamId(transaction, IDTrungTam);
        //TransactionManager transaction = ProviderFactory.Transaction;
        //try
        //{
        if (lstDmTinhThanh.Count > 0)
        {
            foreach (DmTinhThanh dmTinhThanh in lstDmTinhThanh)
            {
                String strdefault = ProviderFactory.SysConfigProvider.GetValue("MA_TRUNG_TAM");
                dmTinhThanh.TrungTamId = strdefault;
                ProviderFactory.DmTinhThanhProvider.Update(transaction, dmTinhThanh);
            }
        }
        //transaction.Commit();
        //}
        //catch (Exception ex)
        //{
        //    transaction.Rollback();
        //    throw ex;
        //}
        //finally
        //{
        //    transaction.Dispose();
        //}       
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    ///  Author                       Date            Action
    /// Nguyễn Trung Tuyến          06/05/2009      Create new
    /// </modified>
    protected void gvTrungTam_DataBound(object sender, EventArgs e)
    {
        int TotalChkBx = 0;
        if (gvTrungTam.HeaderRow != null)
        {
            // Lấy tham chiếu đến checkbox header
            CheckBox cbHeader = (CheckBox)gvTrungTam.HeaderRow.FindControl("chkCheckAll");
            // Bắt sự kiện onclick của checkbox toàn bộ
            cbHeader.Attributes["onclick"] = string.Format("HeaderClick('{0}');", gvTrungTam.ID);

            // Duyệt trên gridview
            foreach (GridViewRow gvr in gvTrungTam.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
                cb.Attributes["onclick"] = string.Format("ChildClick('{0}');", gvTrungTam.ID);
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
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    ///  Author                      Date            Action
    /// Nguyễn Trung Tuyến          06/05/2009      Create new
    /// </modified>
    //protected void gvTrungTam_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        LinkButton lnkSelect = (LinkButton)e.Row.FindControl("lnkSelect");

    //        if (lnkSelect != null)
    //        {
    //            lnkSelect.OnClientClick = "return popCenter('DM_TrungTam_ChiTiet.aspx?a=2&Ma=" + DataBinder.Eval(e.Row.DataItem, "Id") + "','DM_TrungTam_ChiTiet',950,450);";                                       
    //        }

    //    }

    //}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    ///  Author                      Date            Action
    /// Nguyễn Trung Tuyến          06/05/2009      Create new
    /// </modified>
    protected void imgXoa_Click(object sender, ImageClickEventArgs e)
    {
        lkbtnXoa_Click(null, null);
    }
}
