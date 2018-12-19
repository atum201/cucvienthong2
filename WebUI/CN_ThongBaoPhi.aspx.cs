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

/// <summary>
/// Giấy thông báo lệ phí
/// </summary>
///<Modified>
/// Author      Date        Comments
/// TrươngTV    13/05/2009  Tạo mới
///</Modified>>
public partial class WebUI_CN_ThongBaoPhi : PageBase
{
    string HoSoId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        HoSoId = Request["HoSoId"];
        if (!IsPostBack)
        {
            ddlLoaiPhi_BindData();
            BindThongBaoPhiGrid();
        }
    }

    private void ddlLoaiPhi_BindData()
    {
        DataTable dtLoaiPhi = new DataTable();
        dtLoaiPhi.Columns.Add("Text");
        dtLoaiPhi.Columns.Add("Value");

        dtLoaiPhi.Rows.Add("-- Tất cả --", "-1");
        //LongHH
        dtLoaiPhi.Rows.Add("Lệ phí tiếp nhận & xem xét chứng nhận hợp quy", 9.ToString());
        //LongHH
        dtLoaiPhi.Rows.Add("Lệ phí cấp Giấy chứng nhận hợp Quy", ((int)EnLoaiPhiList.PHI_CHUNG_NHAN_HQ).ToString());
        dtLoaiPhi.Rows.Add("Lệ phí cấp Giấy chứng nhận hợp chuẩn", ((int)EnLoaiPhiList.PHI_CHUNG_NHAN_HC).ToString());
        dtLoaiPhi.Rows.Add("Phí lấy mẫu sản phẩm và đánh giá quy trình SX", "0");
        dtLoaiPhi.Rows.Add("Lệ phí cấp Bản tiếp nhận công bố hợp quy", ((int)EnLoaiPhiList.PHI_CONG_BO_HQ).ToString());
        

        ddlLoaiPhi.DataSource = dtLoaiPhi;
        ddlLoaiPhi.DataTextField = "Text";
        ddlLoaiPhi.DataValueField = "Value";
        ddlLoaiPhi.DataBind();
    }

    /// <summary>
    /// Danh sách thông báo phí của hồ sơ nếu có
    /// </summary>
    /// <Modified>
    /// Author      Date            Comments    
    /// Quannm      16/05/2009      Create new
    ///</Modified>
    private void BindThongBaoPhiGrid()
    {
        int LoaiPhi = Convert.ToInt32(ddlLoaiPhi.SelectedValue);

        DataTable dtThongBaoPhi = new DataTable();
        if (mUserInfo.GetPermissionList("0109") != null && mUserInfo.GetPermissionList("0109").Count > 0)
        {
            // Thong bao le phi cho thu phi
            dtThongBaoPhi = ProviderFactory.ThongBaoLePhiProvider.GetThongBaoPhiChoThuPhi(mUserInfo.GetPermissionList("0109"), mUserInfo.UserID, mUserInfo.MaTrungTam);

            // Thong bao le phi cho huy
            dtThongBaoPhi.Merge(ProviderFactory.ThongBaoLePhiProvider.GetThongBaoPhiChoHuy(mUserInfo.GetPermissionList("0109"), mUserInfo.UserID, mUserInfo.MaTrungTam));

            // Thong bao nop tien cho thu phi
            dtThongBaoPhi.Merge(ProviderFactory.ThongBaoLePhiProvider.GetThongBaoNopTienChoThuPhi(mUserInfo.GetPermissionList("0109"), mUserInfo.UserID, mUserInfo.MaTrungTam));

            // Thong bao nop tien cho huy
            dtThongBaoPhi.Merge(ProviderFactory.ThongBaoLePhiProvider.GetThongBaoNopTienChoHuy(mUserInfo.GetPermissionList("0109"), mUserInfo.UserID, mUserInfo.MaTrungTam));
            //LongHH Add Thông báo phí tiếp nhận tới danh sách
            dtThongBaoPhi.Merge(addMotaTrangThai(QLCL_Patch.GetTBPhiTiepNhans("")));
            //LongHH
        }
        if (mUserInfo.GetPermissionList("0106") != null && mUserInfo.GetPermissionList("0106").Count > 0)
        {
            // Lay thông báo phí chờ duyệt huỷ
            dtThongBaoPhi.Merge(ProviderFactory.ThongBaoLePhiProvider.GetThongBaoPhiChoDuyetHuy(mUserInfo.GetPermissionList("0106"), mUserInfo.UserID, mUserInfo.MaTrungTam));

            // Lấy thông báo nộp tiền chờ phê duyệt, duyệt hủy
            dtThongBaoPhi.Merge(ProviderFactory.ThongBaoLePhiProvider.GetThongBaoNopTienChoPheDuyet(mUserInfo.GetPermissionList("0106"), mUserInfo.UserID, mUserInfo.MaTrungTam));
        }
        
        DataView dv = new DataView(dtThongBaoPhi);

        if (LoaiPhi == 0)
        {
            // Lấy thông báo nộp tiền chờ phê duyệt
            dv.RowFilter = " LoaiPhiId IS NULL ";
        }
        if (LoaiPhi > 0)
        {
            // Lấy thông báo nộp tiền chờ phê duyệt
            dv.RowFilter = " LoaiPhiId = " + LoaiPhi;
        }

        dv.Sort = "SoGiayThongBaoLePhi DESC";
        dtThongBaoPhi = dv.ToTable();

        gvPhi.DataSource = dtThongBaoPhi;
        gvPhi.DataBind();
    }

    // LongHH
    private DataTable addMotaTrangThai(DataTable dt)
    {
        
        if (dt != null & dt.Rows.Count > 0)
        {
            dt.Columns.Add("MoTa",typeof(string));
            for (int i = 0; i < dt.Rows.Count; i++)
                try
                {
                    dt.Rows[i]["MoTa"] = QLCL_Patch.GetTrangThaiHoSo(int.Parse(dt.Rows[i]["TrangThaiId"].ToString()));
                }
                catch { }
        }
        return dt;
    }
    // LongHH
    protected void gvPhi_DataBound(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPhi_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPhi.PageIndex = e.NewPageIndex;
        BindThongBaoPhiGrid();
    }

    /// <summary>
    /// Lọc danh sách thông báo phí theo loại
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modifide>
    /// Author          Date        comment
    /// TuấnVM          03/03/2010  Tạo mới
    /// </modifide>
    protected void ddlLoaiPhi_SelectedIndexChanged(object sender, EventArgs e)
    {
        int LoaiPhi = Convert.ToInt32(ddlLoaiPhi.SelectedValue);

        DataTable dtThongBaoPhi = new DataTable();
        if (mUserInfo.GetPermissionList("0109") != null && mUserInfo.GetPermissionList("0109").Count > 0)
        {
            dtThongBaoPhi = ProviderFactory.ThongBaoLePhiProvider.GetThongBaoPhiChoThuPhi(mUserInfo.GetPermissionList("0109"), mUserInfo.UserID, mUserInfo.MaTrungTam);
            dtThongBaoPhi.Merge(ProviderFactory.ThongBaoLePhiProvider.GetThongBaoPhiChoHuy(mUserInfo.GetPermissionList("0109"), mUserInfo.UserID, mUserInfo.MaTrungTam));

            // Thong bao nop tien cho thu phi
            dtThongBaoPhi.Merge(ProviderFactory.ThongBaoLePhiProvider.GetThongBaoNopTienChoThuPhi(mUserInfo.GetPermissionList("0109"), mUserInfo.UserID, mUserInfo.MaTrungTam));

            // Thong bao nop tien cho huy
            dtThongBaoPhi.Merge(ProviderFactory.ThongBaoLePhiProvider.GetThongBaoNopTienChoHuy(mUserInfo.GetPermissionList("0109"), mUserInfo.UserID, mUserInfo.MaTrungTam));
            //LongHH Add Thông báo phí tiếp nhận tới danh sách
            dtThongBaoPhi.Merge(QLCL_Patch.GetTBPhiTiepNhans(""));
            //LongHH
        }
        if (mUserInfo.GetPermissionList("0106") != null && mUserInfo.GetPermissionList("0106").Count > 0)
        {
            // Lay thông báo phí chờ duyệt huỷ
            dtThongBaoPhi.Merge(ProviderFactory.ThongBaoLePhiProvider.GetThongBaoPhiChoDuyetHuy(mUserInfo.GetPermissionList("0106"), mUserInfo.UserID, mUserInfo.MaTrungTam));

            // Lấy thông báo nộp tiền chờ phê duyệt
            dtThongBaoPhi.Merge(ProviderFactory.ThongBaoLePhiProvider.GetThongBaoNopTienChoPheDuyet(mUserInfo.GetPermissionList("0106"), mUserInfo.UserID, mUserInfo.MaTrungTam));

        }

        DataView dv = new DataView(dtThongBaoPhi);

        if (LoaiPhi == 0)
        {
            // Lấy thông báo nộp tiền chờ phê duyệt
            dv.RowFilter = " LoaiPhiId IS NULL ";
        }
        if (LoaiPhi > 0)
        {
            // Lấy thông báo nộp tiền chờ phê duyệt
            dv.RowFilter = " LoaiPhiId = " + LoaiPhi;
        }

        dv.Sort = "SoGiayThongBaoLePhi DESC";
        dtThongBaoPhi = dv.ToTable();

        gvPhi.DataSource = dtThongBaoPhi;
        gvPhi.DataBind();
    }

    protected void gvPhi_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlImage lnkA = (HtmlImage)e.Row.FindControl("lnkInLePhi");
            if ((int)DataBinder.Eval(e.Row.DataItem, "TrangThaiID") == (int)EnTrangThaiThongBaoPhiList.CHO_THU_PHI)
            {

            }
            else
            {
                //lnkA.Style.Add("Display", "none");
                if (lnkA!=null)
                {
                    lnkA.Visible = false;
                }
            }
            //LongHH Thêm bound cột trạng thái thông báo phí

            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "LoaiPhiID")) == 9)
            {
                e.Row.Cells[6].Text = QLCL_Patch.GetTrangThaiThongBaoPhi(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")));
            }
            else
            {
                e.Row.Cells[6].Text = DataBinder.Eval(e.Row.DataItem, "MoTa").ToString();
            }
            //LongHH
        }
    }
}
