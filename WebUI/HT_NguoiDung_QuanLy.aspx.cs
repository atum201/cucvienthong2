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

public partial class WebUI_HT_NguoiDung_QuanLy : PageBase
{
    //DataTable lưu UserRole
    DataTable dtUserRole = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            //Lấy danh sách người dùng
            LoadDSNguoiDung();
        }
    }
    /// <summary>
    /// Lấy danh sách người dùng trong hệ thống
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    private void LoadDSNguoiDung()
    {
        string strTrungTam = mUserInfo.TrungTam.MaTrungTam + "_";

        TList<SysUser> lstSysUser = ProviderFactory.SysUserProvider.GetUserByTrungTamID(mUserInfo.TrungTam.Id);
        TList<SysUser> lstSysUserSource = new TList<SysUser>();
        foreach (SysUser objReFullName in lstSysUser)
        {
            objReFullName.FullName += " (" + objReFullName.UserName.Replace(strTrungTam, string.Empty) + ")";
            if (!lstSysUserSource.Contains(objReFullName))
                lstSysUserSource.Add(objReFullName);
        }

        lstDanhSachNguoiDung.DataValueField = "ID";
        lstDanhSachNguoiDung.DataTextField = "FullName";
        lstDanhSachNguoiDung.DataSource = lstSysUserSource;
        lstDanhSachNguoiDung.DataBind();
    }
    /// <summary>
    /// sự kiện xảy ra khi người dùng bấm vào nút Thêm mới
    /// Chương trình redirect tới form tạo mới người dùng
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    protected void btnThem_Click(object sender, EventArgs e)
    {
        Response.Redirect("HT_NguoiDung_TaoMoi.aspx");
    }
    /// <summary>
    /// sự kiện xảy ra khi người dùng thay đổi thông tin người dùng
    /// Chương trình redirect đến trang Tạo mới người dùng
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    protected void btnSua_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(lstDanhSachNguoiDung.SelectedValue))
            return;

        string strUserNameID = string.Empty;
        if (lstDanhSachNguoiDung.Items.Count > 0 && lstDanhSachNguoiDung.SelectedValue != null)
            strUserNameID = lstDanhSachNguoiDung.SelectedValue.ToString();

        Response.Redirect("HT_NguoiDung_TaoMoi.aspx?UID=" + strUserNameID);
    }
    /// <summary>
    /// Lấy danh sách vai trò được cấp cho người dùng đã chọn trong hệ thống
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    private void LoadDSUserRole(string UserID)
    {
        TList<SysUserRole> lstSysUserRole = ProviderFactory.SysUserRoleProvider.GetByUserId(UserID);
        TList<SysRole> lstSysRole = new TList<SysRole>();
        foreach (SysUserRole objUserRole in lstSysUserRole)
        {
            SysRole objSysRole = ProviderFactory.SysRoleProvider.GetById(objUserRole.RoleId);
            if (!lstSysRole.Contains(objSysRole))
                lstSysRole.Add(objSysRole);
        }

        lstDanhSachVaiTroDaCap.DataValueField = "ID";
        lstDanhSachVaiTroDaCap.DataTextField = "RoleName";
        lstDanhSachVaiTroDaCap.DataSource = lstSysRole;
        lstDanhSachVaiTroDaCap.DataBind();
    }
    /// <summary>
    /// Lấy danh sách vai trò chưa được cấp cho người dùng đã chọn trong hệ thống
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    private void LoadDSUserRoleNotAssign(string UserID)
    {
        TList<SysUserRole> lstSysUserRole = ProviderFactory.SysUserRoleProvider.GetByUserId(UserID);
        TList<SysRole> lstSysRoleAll = ProviderFactory.SysRoleProvider.GetAll();
        TList<SysRole> lstSysRole = new TList<SysRole>();
        //lấy danh sách vai trò theo người dùng
        foreach (SysUserRole objUserRole in lstSysUserRole)
        {
            SysRole objSysRole = ProviderFactory.SysRoleProvider.GetById(objUserRole.RoleId);
            if (!lstSysRole.Contains(objSysRole))
                lstSysRole.Add(objSysRole);
        }
        //Lấy sách vai trò chưa được gán
        foreach (SysRole objSysRole in lstSysRole)
        {
            if (lstSysRoleAll.Contains(objSysRole))
                lstSysRoleAll.Remove(objSysRole);
        }
        lstDanhSachVaiTroChuaCap.DataValueField = "ID";
        lstDanhSachVaiTroChuaCap.DataTextField = "RoleName";
        lstDanhSachVaiTroChuaCap.DataSource = lstSysRoleAll;
        lstDanhSachVaiTroChuaCap.DataBind();
    }
    /// <summary>
    /// sự kiện xảy ra khi người dùng thay đổi focus người dùng
    /// Chương trình load danh sách vai trò được cấp và không được cấp tương ứng
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    protected void lstDanhSachNguoiDung_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(lstDanhSachNguoiDung.SelectedValue))
            return;

        string strUserNameID = string.Empty;
        if (lstDanhSachNguoiDung.Items.Count > 0 && !string.IsNullOrEmpty(lstDanhSachNguoiDung.SelectedValue))
            strUserNameID = lstDanhSachNguoiDung.SelectedValue.ToString();
        LoadDSUserRole(strUserNameID);
        LoadDSUserRoleNotAssign(strUserNameID);
        CheckEnable();
    }
    /// <summary>
    /// Xóa người dùng đã chọn
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    protected void btnXoa_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(lstDanhSachNguoiDung.SelectedValue))
            return;

        string strUserNameID = string.Empty;
        if (lstDanhSachNguoiDung.Items.Count > 0 && lstDanhSachNguoiDung.SelectedValue != null)
            strUserNameID = lstDanhSachNguoiDung.SelectedValue.ToString();
        SysUser oUser = ProviderFactory.SysUserProvider.GetById(strUserNameID);
        if (oUser == null)
        {
            LoadDSNguoiDung();
            return;
        }
        else
        {
            //Kiểm tra người dùng đã tham gia vào hệ thống chưa, nếu có ko được xóa
            if (CheckUsed(oUser.Id))
            {
                Thong_bao(this.Page, Resource.msgNguoiDungDaThamGiaVaoHeThong);
                return;
            }
            else
            {
                //Xóa danh sách vai trò được cấp cho người dùng
                TransactionManager objTransaction = ProviderFactory.Transaction;
                try
                {
                    TList<SysUserRole> lstSysUserRole = ProviderFactory.SysUserRoleProvider.GetByUserId(strUserNameID);
                    ProviderFactory.SysUserRoleProvider.Delete(lstSysUserRole);
                    ProviderFactory.SysUserProvider.Delete(strUserNameID);

                    // Nếu người đang có trách nhiệm nhận hồ sơ qua mạng thì chuyển trách nhiệm cho admin
                    DmTrungTam objDmTrungTam = ProviderFactory.DmTrungTamProvider.GetById(oUser.OrganizationId);
                    if (objDmTrungTam.NguoiTiepNhanHoSoQuaMang == strUserNameID)
                    {
                        objDmTrungTam.NguoiTiepNhanHoSoQuaMang = ProviderFactory.SysUserProvider.GetByUserName(ProviderFactory.SysConfigProvider.GetValue("ADMIN_USER")).Id;
                        ProviderFactory.DmTrungTamProvider.Save(objDmTrungTam);
                    }
                    objTransaction.Commit();
                    Thong_bao(this.Page, string.Format(Resource.msgXoaNguoiDungThanhCong, oUser.FullName));
                    LoadDSNguoiDung();
                }
                catch (Exception ex)
                {
                    objTransaction.Rollback();
                    PageBase_Error(sender, e);
                    throw ex;

                }
            }
        }
    }
    /// <summary>
    /// Kiểm tra người dùng đã tham gia vào hệ thống chưa
    /// </summary>               
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                24/12/2008              
    /// </Modified>
    private bool CheckUsed(string UserID)
    {
        DataTable dtUser = ProviderFactory.SysUserProvider.CheckUsed(UserID);
        return dtUser.Rows.Count > 0;
    }

    protected void btnThemVaiTro_Click(object sender, ImageClickEventArgs e)
    {
        if (string.IsNullOrEmpty(lstDanhSachVaiTroChuaCap.SelectedValue))
            return;
        AddRole();
        lstDanhSachNguoiDung_SelectedIndexChanged(null, null);
    }
    /// <summary>
    /// thêm  1 vai trò
    /// </summary>               
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                24/12/2008              
    /// </Modified>
    private void AddRole()
    {
        string strUsernameSelected = lstDanhSachNguoiDung.SelectedValue.ToString();

        TList<SysRole> lstSYS_ROLE = new TList<SysRole>();
        for (int i = 0; i <= lstDanhSachVaiTroChuaCap.Items.Count - 1; i++)
        {
            if (lstDanhSachVaiTroChuaCap.Items[i].Selected)
            {
                SysRole objSYS_ROLE = ProviderFactory.SysRoleProvider.GetById(lstDanhSachVaiTroChuaCap.Items[i].Value);
                if (!lstSYS_ROLE.Contains(objSYS_ROLE))
                    lstSYS_ROLE.Add(objSYS_ROLE);
            }
        }
        UpdateRoleByUser(strUsernameSelected, lstSYS_ROLE);
    }
    /// <summary>
    ///Trao đổi dữ liệu giữa 2 listbox
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                24/12/2008              
    /// </Modified>
    private void AddRemoveItem(ListBox lstSource, ListBox lstTarget, ImageButton btnSource, ImageButton btnTarget, ImageButton btnAllSource, ImageButton btnAllTarget)
    {
        ArrayList lst = new ArrayList();
        ArrayList lstFrom = new ArrayList();
        foreach (object obj in lstSource.Items)
        {
            lstFrom.Add(obj);
        }
        foreach (object obj in lstTarget.Items)
        {
            lst.Add(obj);
        }

        for (int i = 0; i <= lstSource.Items.Count - 1; i++)
        {
            if (lstSource.Items[i].Selected)
            {
                lst.Add(lstSource.Items[i]);
                lstFrom.Remove(lstSource.Items[i]);
            }
        }
        lstTarget.DataValueField = "ID";
        lstTarget.DataTextField = "RoleName";
        lstTarget.DataSource = lst;
        lstTarget.DataBind();
        lstSource.DataValueField = "ID";
        lstSource.DataTextField = "RoleName";
        lstSource.DataSource = lstFrom;
        lstSource.DataBind();
        if (lstTarget.Items.Count > 0)
            lstTarget.SelectedIndex = 0;
        if (lstSource.Items.Count > 0)
            lstSource.SelectedIndex = 0;
        if (lstSource.Items.Count <= 0)
        {
            btnSource.Enabled = false;
            btnAllSource.Enabled = false;
        }
        else
        {
            btnSource.Enabled = true;
            btnAllSource.Enabled = true;
        }

        if (lstTarget.Items.Count <= 0)
        {
            btnTarget.Enabled = false;
            btnAllTarget.Enabled = false;
        }
        else
        {
            btnTarget.Enabled = true;
            btnAllTarget.Enabled = true;
        }
    }
    /// <summary>
    /// Cập nhật vai trò của người dùng đã chọn vào hệ thống
    /// </summary>
    /// <param name="lstSYS_ROLE"></param>
    /// <param name="strUsernameSelected"></param>
    /// <Modified>
    ///     Author      Date        Comments
    ///     Hùngnv   25/12/2008  /  Tạo mới
    /// </Modified>
    private void UpdateRoleByUser(string UserID, TList<SysRole> lstSYS_ROLE)
    {
        bool iSuccess = false;
        //kiểm tra xem người dùng đó đã có vai trò trong bảng sys_user_role chưa?            
        //nếu chưa có thì insert vào bảng sys_user_role
        //nếu có rồi thì chẳng làm gì cả
        foreach (SysRole objSYS_ROLE in lstSYS_ROLE)
        {
            if (CheckExist(UserID, objSYS_ROLE.Id)) { }
            else
            {
                SysUserRole objSYS_USER_ROLE = new SysUserRole();
                objSYS_USER_ROLE.Id = "insert";
                objSYS_USER_ROLE.RoleId = objSYS_ROLE.Id;
                objSYS_USER_ROLE.UserId = UserID;
                try
                {
                    iSuccess = ProviderFactory.SysUserRoleProvider.Insert(objSYS_USER_ROLE);
                }
                catch (System.Exception ex)
                {
                    PageBase_Error(null, null);
                    throw ex;
                }
                if (!iSuccess)
                    Thong_bao(this.Page, Resource.msgCapNhatThatBai);
                //else
                //    SYS_LOG.Write(UserInfo.Instance().UserID, LogEventList.User_Insert, string.Format(Resources.msgLogThemMoiVaiTro, objSYS_ROLE.ROLE_NAME, strUsernameSelected));
            }
        }
    }
    /// <summary>
    /// Kiểm trả vai trò được cấp cho người dùng đã chọn có trong hệ thống hay không.
    /// </summary>
    /// <param name="intRoleID"></param>
    /// <param name="strUsernameSelected"></param>
    /// <Modified>
    ///     Author      Date        Comments
    ///     Hùngnv   24/12/2008  /  Tạo mới
    /// </Modified>
    private bool CheckExist(string UserID, string strRoleID)
    {
        //kiểm tra xem người dùng đó đã có trong bảng sys_user_role chưa?
        DataTable dtUR = ProviderFactory.SysUserRoleProvider.GetByUserIDRoleID(UserID, strRoleID);
        dtUserRole = dtUR;
        return dtUR.Rows.Count > 0;
    }
    protected void btnXoaVaiTro_Click(object sender, ImageClickEventArgs e)
    {
        if (string.IsNullOrEmpty(lstDanhSachVaiTroDaCap.SelectedValue))
            return;
        RemoveRole();
        lstDanhSachNguoiDung_SelectedIndexChanged(null, null);
    }
    /// <summary>
    ///  xóa  1 vai trò
    /// </summary>               
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                24/12/2008              
    /// </Modified>
    private void RemoveRole()
    {
        string strUserID = lstDanhSachNguoiDung.SelectedValue.ToString();
        TList<SysRole> lstSYS_ROLE = new TList<SysRole>();
        for (int i = 0; i <= lstDanhSachVaiTroDaCap.Items.Count - 1; i++)
        {
            if (lstDanhSachVaiTroDaCap.Items[i].Selected)
            {
                SysRole objSYS_ROLE = ProviderFactory.SysRoleProvider.GetById(lstDanhSachVaiTroDaCap.Items[i].Value);
                lstSYS_ROLE.Add(objSYS_ROLE);
            }
        }
        DeleteRoleByUser(strUserID, lstSYS_ROLE);
        lstDanhSachNguoiDung_SelectedIndexChanged(null, null);
    }

    private void CheckEnable()
    {
        btnThemVaiTro.Enabled = lstDanhSachVaiTroChuaCap.Items.Count > 0;
        btnXoaVaiTro.Enabled = lstDanhSachVaiTroDaCap.Items.Count > 0;
        btnThemHetVaiTro.Enabled = lstDanhSachVaiTroChuaCap.Items.Count > 0;
        btnXoaHetVaiTro.Enabled = lstDanhSachVaiTroDaCap.Items.Count > 0;
    }
    /// <summary>
    /// xóa vai trò của người dùng đã chọn
    /// </summary>
    /// <param name="lstSYS_ROLE"></param>
    /// <param name="strUsernameSelected"></param>
    /// <Modified>
    ///     Author      Date        Comments
    ///     Hùngnv   25/12/2008  /  Tạo mới
    /// </Modified>
    private void DeleteRoleByUser(string UserID, TList<SysRole> lstSYS_ROLE)
    {
        bool iSuccess = false;
        //kiểm tra xem người dùng đó đã có vai trò trong bảng sys_user_role chưa?            
        //nếu chưa có thì chẳng làm gì cả
        //nếu có rồi thì delete khỏi bảng sys_user_role
        foreach (SysRole objSYS_ROLE in lstSYS_ROLE)
        {
            if (CheckExist(UserID, objSYS_ROLE.Id))
            {
                SysUserRole objSYS_USER_ROLE = new SysUserRole();

                objSYS_USER_ROLE.Id = dtUserRole.Rows[0]["Id"].ToString();
                objSYS_USER_ROLE.UserId = dtUserRole.Rows[0]["UserId"].ToString();
                objSYS_USER_ROLE.RoleId = dtUserRole.Rows[0]["RoleId"].ToString();

                iSuccess = ProviderFactory.SysUserRoleProvider.Delete(objSYS_USER_ROLE);

                if (!iSuccess)
                    Thong_bao(this.Page, Resource.msgCapNhatThatBai);
                //else
                //    SYS_LOG.Write(UserInfo.Instance().UserID, LogEventList.User_Delete, string.Format(Resources.msgLogXoaVaiTro, objSYS_ROLE.ROLE_NAME, strUsernameSelected));
            }
        }

    }
    protected void btnThemHetVaiTro_Click(object sender, ImageClickEventArgs e)
    {
        string UserID = lstDanhSachNguoiDung.SelectedValue.ToString();

        TList<SysRole> lstSYS_ROLE = new TList<SysRole>();
        for (int i = 0; i <= lstDanhSachVaiTroChuaCap.Items.Count - 1; i++)
        {
            SysRole objSYS_ROLE = ProviderFactory.SysRoleProvider.GetById(lstDanhSachVaiTroChuaCap.Items[i].Value);
            lstSYS_ROLE.Add(objSYS_ROLE);
        }
        UpdateRoleByUser(UserID, lstSYS_ROLE);
        lstDanhSachNguoiDung_SelectedIndexChanged(null, null);
    }
    /// <summary>
    ///Trao đổi dữ liệu giữa 2 listbox
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                24/12/2008              
    /// </Modified>
    private void AddRemoveAllItem(ListBox lstSource, ListBox lstTarget, ImageButton btnAllSource, ImageButton btnAllTarget, ImageButton btnSource, ImageButton btnTarget)
    {
        ArrayList lst = new ArrayList();
        ArrayList lstFrom = new ArrayList();
        foreach (object obj in lstSource.Items)
        {
            lstFrom.Add(obj);
        }
        foreach (object obj in lstTarget.Items)
        {
            lst.Add(obj);
        }

        foreach (object obj in lstSource.Items)
        {
            lst.Add(obj);
            lstFrom.Remove(obj);
        }
        lstTarget.DataValueField = "ID";
        lstTarget.DataTextField = "RoleName";
        lstTarget.DataSource = lst;
        lstTarget.DataBind();
        lstSource.DataValueField = "ID";
        lstSource.DataTextField = "RoleName";
        lstSource.DataSource = lstFrom;
        lstSource.DataBind();
        if (lstTarget.Items.Count > 0)
            lstTarget.SelectedIndex = 0;
        if (lstSource.Items.Count > 0)
            lstSource.SelectedIndex = 0;
        if (lstSource.Items.Count <= 0)
        {
            btnSource.Enabled = false;
            btnAllSource.Enabled = false;
        }
        else
        {
            btnSource.Enabled = true;
            btnAllSource.Enabled = true;
        }

        if (lstTarget.Items.Count <= 0)
        {
            btnTarget.Enabled = false;
            btnAllTarget.Enabled = false;
        }
        else
        {
            btnTarget.Enabled = true;
            btnAllTarget.Enabled = true;
        }
    }
    protected void btnXoaHetVaiTro_Click(object sender, ImageClickEventArgs e)
    {
        string UserID = lstDanhSachNguoiDung.SelectedValue.ToString();

        DataTable dtRole = ProviderFactory.SysRoleProvider.GetSysUserRoleByUser(UserID);

        TList<SysRole> lstSYS_ROLE_Bandau = new TList<SysRole>();
        if (dtRole.Rows.Count > 0)
        {
            foreach (DataRow dr in dtRole.Rows)
            {
                SysRole objSysRole = new SysRole();
                objSysRole.Id = dr["Id"].ToString();
                objSysRole.Stt = Convert.ToInt32(dr["Stt"].ToString());
                objSysRole.RoleName = dr["RoleName"].ToString();
                objSysRole.Description = dr["Description"].ToString();

                if (!lstSYS_ROLE_Bandau.Contains(objSysRole))
                    lstSYS_ROLE_Bandau.Add(objSysRole);
            }
        }
        DeleteRoleByUser(UserID, lstSYS_ROLE_Bandau);

        lstDanhSachNguoiDung_SelectedIndexChanged(null, null);
    }
}
