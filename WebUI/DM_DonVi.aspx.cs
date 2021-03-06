using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Cuc_QLCL.Entities;
using Cuc_QLCL.AdapterData;
using Resources;
using Cuc_QLCL.Data;
using System.Data.SqlClient;
using Cuc_QLCL;
/// <summary>
/// Quản lý đơn vị
/// </summary>
/// <Modified>
/// Name        Date        Comment
/// TuanND    5/07/2009     Thêm mới
/// NguyenQuy   09/05/2009  Sửa
/// </Modified>
public partial class WebUI_DM_DonVi : PageBase
{
    struct ThongTinTimkiem
    {
        public String MaDonVi;
        public String TenDonVi;         
        public String TinhThanh;
    }
    ThongTinTimkiem ThongTinTk = new ThongTinTimkiem();
    string mWhereClause = " ";
    List<string> deleteSuccess = new List<string>();
    List<string> deleteFail = new List<string>();
    /// <summary>
    /// Load trang
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// TuanND    5/07/2009     Thêm mới
    /// NguyenQuy   5/09/2009     Sua
    /// </Modified>
    protected void Page_Load(object sender, EventArgs e)
    {
        lnkXoa.Attributes.Add("onclick", "if (GridIsChecked('gvDonVi')){return confirm('Bạn có chắc chắn muốn xóa đơn vị này?');} else {alert('Bạn cần chọn đơn vị để xóa');return false;}");
        if (!IsPostBack)
        {
            BindCombo();
            //goi ham load du lieu len grid
            LayDanhSach();
            checkPermission();

            Session["ThongTinTimKiem"] = null;
        }
        else
        {
            if (this.Request["__EVENTTARGET"] == "AddNewCommit") LayDanhSach();
        }
    }

    void checkPermission()
    {
        if (!mUserInfo.IsPermission(EnPermission.QUANLY_DM_DONVI))
        {
            imgAdd.Visible = false;
            lnkThemMoi.Visible = false;
            lnkXoa.Visible = false;

        }
    }
    /// <summary>
    /// Lấy danh sách tinh thanh vào combo
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// TuanND    5/07/2009     Thêm mới
    /// </Modified>
    public void BindCombo()
    {
        TList<DmTinhThanh> lstTinhThanh = ProviderFactory.DmTinhThanhProvider.GetAll();
        DmTinhThanh objTinhThanh = new DmTinhThanh();
        objTinhThanh.Id = "";
        objTinhThanh.TenTinhThanh = "Tất cả";
        lstTinhThanh.Insert(0, objTinhThanh);
        ddlTinhThanh.DataSource = lstTinhThanh;
        ddlTinhThanh.DataTextField = "TenTinhThanh";
        ddlTinhThanh.DataValueField = "ID";
        ddlTinhThanh.DataBind();
    }
    /// <summary>
    /// Lay du lieu tu database ra grid
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// NguyenQuy              9/5/2009              
    /// </Modified>
    private void LayDanhSach()
    {
        setWhereClause();
        DataTable dtDonvis = ProviderFactory.DmDonViProvider.Search(mWhereClause, gvDonVi.OrderBy, gvDonVi.PageIndex + 1, gvDonVi.PageSize);
        gvDonVi.DataSource = dtDonvis;
        if (dtDonvis.Rows.Count > 0)
            gvDonVi.VirtualItemCount = int.Parse(dtDonvis.Rows[0]["TongSoBanGhi"].ToString());
        gvDonVi.DataBind();
    }
    /// <summary>
    /// Thêm sự kiên check cho checkbox trong grid
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// TuanND    5/07/2009     Thêm mới
    /// </Modified>
    protected void gvDonVi_DataBound(object sender, EventArgs e)
    {
        int TotalChkBx = 0;
        if (gvDonVi.HeaderRow != null)
        {
            // Lấy tham chiếu đến checkbox header
            CheckBox cbHeader = (CheckBox)gvDonVi.HeaderRow.FindControl("chkCheckAll");
            // Bắt sự kiện onclick của checkbox toàn bộ
            cbHeader.Attributes["onclick"] = string.Format("HeaderClick('{0}');", gvDonVi.ID);

            // Duyệt trên gridview
            foreach (GridViewRow gvr in gvDonVi.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
                cb.Attributes["onclick"] = string.Format("ChildClick('{0}');", gvDonVi.ID);
                // Đếm số lượng check box con
                TotalChkBx++;
            }
        }
        // Gán giá trị của biến
        this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "TotalChkBx", string.Format("TotalChkBx={0};Counter={1};", TotalChkBx, 0), true);
    }
    protected void gvDonVi_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //List<String> files = QLCL_Patch.GetFileAttach_DonVi_Nop_HoSo(DataBinder.Eval(e.Row.DataItem, "ID").ToString());
        //if (files.Count > 0 && String.IsNullOrEmpty(files[0]))
        //{
        //    e.Row.Cells[4].Text = "<a href='" + files[0] + "'>" + DataBinder.Eval(e.Row.DataItem, "MaDonVi").ToString() + "</a>";
        //}
        //else
        //{
        e.Row.Cells[1].Text = "121341";// +DataBinder.Eval(e.Row.DataItem, "ID").ToString();
        //}
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
        foreach (GridViewRow gvr in gvDonVi.Rows)
        {
            CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
            //xoa hang san xuat duoc chon
            if (cb.Checked)
            {
                mMa = gvDonVi.DataKeys[gvr.RowIndex][0].ToString();
                DmDonVi obj = ProviderFactory.DmDonViProvider.GetById(mMa);
                if (obj == null)
                {
                    LayDanhSach();
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
    /// <summary>
    /// Xóa bản ghi
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// TuanND    5/07/2009     Thêm mới
    /// </Modified>
    protected void lnkXoa_Click(object sender, EventArgs e)
    {
        if (!DaDongBo())
        {
            String TenDonVi = string.Empty;
            TransactionManager transaction = ProviderFactory.Transaction;

            foreach (GridViewRow row in gvDonVi.Rows)
            {
                CheckBox chkControl = (CheckBox)row.FindControl("chkCheck");
                if (chkControl.Checked)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(gvDonVi.DataKeys[row.RowIndex][0].ToString()))
                        {
                            string id = gvDonVi.DataKeys[row.RowIndex][0].ToString();
                            TenDonVi = gvDonVi.DataKeys[row.RowIndex][1].ToString();
                            ProviderFactory.DmDonViProvider.Delete(transaction, id);
                            deleteSuccess.Add(TenDonVi);
                        }
                    }
                    catch (SqlException ex)
                    {

                        if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                        {
                            if (ex.Errors[0].Number == 547)
                            {
                                deleteFail.Add(TenDonVi);
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
                string lsDonVi = string.Join(", ", deleteFail.ToArray());
                Thong_bao(string.Format(Resources.Resource.msgXoaDonViThatBai, lsDonVi));
                transaction.Rollback();

            }
            else
            {
                transaction.Commit();
                // Thông báo xóa thành công (danh sách đã xóa) + ghi log (danh sách đã xóa)          
                String strLogString = string.Format(Resources.Resource.msgXoaDonViThanhCong, string.Join(",", deleteSuccess.ToArray()));
                Thong_bao(strLogString);
                ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.DM_DON_VI_XOA, strLogString);
            }
            // Load lại dữ liệu
            LayDanhSach();
        }
    }
    /// <summary>
    /// tim don vi tuong ung voi dk
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Name        Date        Comment
    /// TuanND    5/07/2009     Thêm mới
    /// NguyenQuy   09/05/2009  Sửa
    /// </Modified>
    protected void btnTimKiem_Click(object sender, EventArgs e)
    {
        gvDonVi.PageIndex = 0;
        GetThongTinTimKiem();
        LayDanhSach();
    }
    void setWhereClause()
    {
        string strTinhThanhID = ddlTinhThanh.SelectedValue;

        if (!txtMaDonVi.Text.Trim().Equals(""))
            mWhereClause += " AND MaDonVi LIKE N'%" + txtMaDonVi.Text.Trim().Replace("'", "") + "%'";
        if (!txtTenDonVi.Text.Trim().Equals(""))
            mWhereClause += " AND TenTiengViet LIKE N'%" + txtTenDonVi.Text.Trim().Replace("'", "") + "%'";
        if (!ddlTinhThanh.SelectedValue.Equals(""))
            mWhereClause += " AND TinhThanhID = N'" + ddlTinhThanh.SelectedValue + "'";
    }
    protected void gvDonVi_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDonVi.PageIndex = e.NewPageIndex;
        SetBackThongTinTimKiem();
        LayDanhSach();

    }

    protected void gvDonVi_Sorting(object sender, GridViewSortEventArgs e)
    {
        LayDanhSach();
    }

    /// <summary>
    /// 
    /// </summary>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          12/05/2009           Tạo mới
    /// </Modifield>
    public void GetThongTinTimKiem()
    {
        ThongTinTk.TenDonVi = txtTenDonVi.Text;
        ThongTinTk.MaDonVi = txtMaDonVi.Text;      
        ThongTinTk.TinhThanh = ddlTinhThanh.SelectedValue;            
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

        txtTenDonVi.Text = string.Empty;
        txtMaDonVi.Text = string.Empty;
        ddlTinhThanh.SelectedIndex = 0;

        if (Session["ThongTinTimKiem"] != null)
        {
            ThongTinTk = (ThongTinTimkiem)Session["ThongTinTimKiem"];
            txtMaDonVi.Text = ThongTinTk.MaDonVi;
            ddlTinhThanh.SelectedValue = ThongTinTk.TinhThanh;          
        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        GetThongTinTimKiem();
        setWhereClause();
        DataTable dtDonvis = ProviderFactory.DmDonViProvider.Search(mWhereClause, gvDonVi.OrderBy, 0, 1000000);
        if (dtDonvis == null || dtDonvis.Rows.Count == 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Không có dữ liệu');</script>");
            return;
        }

        dtDonvis.Columns.Remove("STT");
        dtDonvis.Columns.Remove("ID");
        dtDonvis.Columns.Remove("TinhThanhID");
        dtDonvis.Columns.Remove("NgayCapNhatSauCung");
        dtDonvis.Columns.Remove("DaDongBo");
        dtDonvis.Columns.Remove("OSTT");
        dtDonvis.Columns.Remove("TongSoBanGhi");
        dtDonvis.Columns.Remove("MatKhau");
        dtDonvis.Columns.Remove("MatKhauKhongMaHoa");

        dtDonvis.Columns["MaDonVi"].Caption = "Mã đơn vị";
        dtDonvis.Columns["TenTiengViet"].Caption = "Tên tiếng việt";
        dtDonvis.Columns["TenTiengAnh"].Caption = "Tên tiếng anh";
        dtDonvis.Columns["TenVietTat"].Caption = "Tên viết tắt";
        dtDonvis.Columns["DiaChi"].Caption = "Địa chỉ";
        dtDonvis.Columns["DienThoai"].Caption = "Điện thoại";
        dtDonvis.Columns["Fax"].Caption = "Fax";
        dtDonvis.Columns["Email"].Caption = "Email";
        dtDonvis.Columns["MaSoThue"].Caption = "Mã số thuế";
        ExcelHelper.ToExcel(dtDonvis, "id", "Danh sach don vi", "DanhsachDonVi.xls", Page.Response);
    }
}
