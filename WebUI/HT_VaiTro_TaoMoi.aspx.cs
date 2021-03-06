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
using Cuc_QLCL.Data;
using Resources;

public partial class WebUI_HT_VaiTro_TaoMoi : PageBase
{
    //Khai báo đối tượng vai trò
    SysRole objRole;

    protected void Page_Load(object sender, EventArgs e)
    {
        txtMota.Attributes.Add("onkeyup", " if (!checkLength('" + txtMota.ClientID + "', '255')) return false;");
        if (Request["RoleID"] != null)
        {
            this.Page.Title = "Sửa thông tin vai trò";
            lblHanhDong.Text = "SỬA THÔNG TIN VAI TRÒ";

            if (!this.Page.IsPostBack)
            {
                objRole = ProviderFactory.SysRoleProvider.GetById(Request["RoleID"].ToString());
                if (objRole != null)
                    LoadSysRoleDetail(objRole);
            }
        }
        else { this.Page.Title = "Thêm mới vai trò"; }
        
    }
    /// <summary>
    /// Lấy thông tin chi tiết vai trò
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    private void LoadSysRoleDetail(SysRole objRole)
    {
        txtTenVaiTro.Text = objRole.RoleName;
        txtMota.Text = objRole.Description;
    }
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {

        if (Request["RoleID"] != null)
        {
            objRole = ProviderFactory.SysRoleProvider.GetById(Request["RoleID"].ToString());
            //cập nhật người dùng
            objRole.Id = Request["RoleID"].ToString();
            objRole.RoleName = txtTenVaiTro.Text;
            objRole.Description = txtMota.Text;

            if (objRole.IsValid)
            {
                //xác định xem vai trò có trùng tên không?                
                bool blHasRoleName = CheckExistRoleName(txtTenVaiTro.Text, Request["RoleID"].ToString());
                if (blHasRoleName)
                {
                    Thong_bao(this.Page, Resource.msgTenVaiTroDaCo);
                }
                else
                {
                    TransactionManager objTransaction = ProviderFactory.Transaction;
                    try
                    {
                        objRole.RoleName = txtTenVaiTro.Text;
                        objRole.Description = txtMota.Text;
                        ProviderFactory.SysRoleProvider.Save(objTransaction, objRole);
                        objTransaction.Commit();
                        Thong_bao(this.Page, string.Format(Resource.msgCapNhatVaiTroThanhCong, objRole.RoleName), "HT_VaiTro_QuanLy.aspx");
                    }
                    catch (Exception ex)
                    {
                        objTransaction.Rollback();
                        throw ex;
                    }
                }
            }
            else
            {
                Thong_bao(this.Page, string.Format(Resource.msgCapNhatVaiTroThatBai, objRole.Error));
                PageBase_Error(sender, e);
            }
        }
        else//tạo mới người dùng
        {
            //xác định xem vai trò có trùng tên không?
            TList<SysRole> lstSysRole = new TList<SysRole>();
            lstSysRole = ProviderFactory.SysRoleProvider.GetAll();
            bool blHasRoleName = CheckExistRoleName(txtTenVaiTro.Text.Trim(), null);
            if (blHasRoleName)
            {
                Thong_bao(this.Page, Resource.msgTenVaiTroDaCo);
            }
            else
            {
                SysRole objSysRole = new SysRole();
                objSysRole.Id = "1111";
                objSysRole.Stt = 1;
                objSysRole.RoleName = txtTenVaiTro.Text;
                objSysRole.Description = txtMota.Text;

                if (objSysRole.IsValid)
                {
                    TransactionManager objTransaction = ProviderFactory.Transaction;
                    try
                    {
                        ProviderFactory.SysRoleProvider.Save(objTransaction, objSysRole);
                        objTransaction.Commit();
                        Thong_bao(this.Page, string.Format(Resource.msgTaoMoiVaiTroThanhCong, objSysRole.RoleName), "HT_VaiTro_QuanLy.aspx");
                    }
                    catch (Exception ex)
                    {
                        objTransaction.Rollback();
                        throw ex;
                    }
                }
                else
                {
                    Thong_bao(this.Page, string.Format(Resource.msgTaoVaiTroThatBai, objSysRole.Error));
                    PageBase_Error(sender, e);
                }
            }
        }

    }
    protected void btnBoQua_Click(object sender, EventArgs e)
    {
        Response.Redirect("HT_VaiTro_QuanLy.aspx");
    }
    /// <summary>
    /// xác định xem vai trò có trùng tên không?
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    private bool CheckExistRoleName(string RoleName, string RoleID)
    {
        bool blExist = false;
        TList<SysRole> lstSysRole = new TList<SysRole>();
        lstSysRole = ProviderFactory.SysRoleProvider.GetRoleByTrungTamID(mUserInfo.TrungTam.Id);
        foreach (SysRole obj in lstSysRole)
        {
            if (!string.IsNullOrEmpty(RoleID))
            {
                if (obj.RoleName.ToUpper() == RoleName.ToUpper() && obj.Id != RoleID )
                {
                    blExist = true;
                    break;
                }
            }
            else
            {
                if (obj.RoleName.ToUpper() == RoleName.ToUpper())
                {
                    blExist = true;
                    break;
                }
            }
        }
        return blExist;
    }
}
