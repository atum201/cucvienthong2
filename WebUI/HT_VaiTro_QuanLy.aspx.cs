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
using Resources;
using Cuc_QLCL.Data;
using System.Collections.Generic;

public partial class WebUI_HT_VaiTro_QuanLy : PageBase
{
    //khai báo biến lưu danh sách quyền cha của một permission
    TList<SysPermission> lstParent = new TList<SysPermission>();
    //Datatable lưu rolepermission
    DataTable dtRolePermission = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        //Xử lý giao diện checkbox của treeview
        tvQuyenChuaCap.Attributes.Add("onclick", "return OnTreeClick(event)");
        tvQuyenDaCap.Attributes.Add("onclick", "return OnTreeClick(event)");

        if (!this.Page.IsPostBack)
        {
            LoadDSVaiTro();
        }
    }
    protected void btnThem_Click(object sender, EventArgs e)
    {
        Response.Redirect("HT_VaiTro_TaoMoi.aspx");
    }
    protected void btnSua_Click(object sender, EventArgs e)
    {
        string strRoleID = string.Empty;
        if (lstDanhSachVaiTro.Items.Count > 0 && lstDanhSachVaiTro.SelectedValue != null)
            strRoleID = lstDanhSachVaiTro.SelectedValue.ToString();

        Response.Redirect("HT_VaiTro_TaoMoi.aspx?RoleID=" + strRoleID);
    }
    /// <summary>
    /// Lấy danh sách vai trò trong hệ thống
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    private void LoadDSVaiTro()
    {
        TList<SysRole> lstSysRole = ProviderFactory.SysRoleProvider.GetRoleByTrungTamID(mUserInfo.TrungTam.Id);
        lstDanhSachVaiTro.Items.Clear();
        lstDanhSachVaiTro.DataValueField = "ID";
        lstDanhSachVaiTro.DataTextField = "RoleName";
        lstDanhSachVaiTro.DataSource = lstSysRole;
        lstDanhSachVaiTro.DataBind();
    }
    protected void btnXoa_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(lstDanhSachVaiTro.SelectedValue))
            return;

        string strRoleID = string.Empty;
        if (lstDanhSachVaiTro.Items.Count > 0 && !string.IsNullOrEmpty(lstDanhSachVaiTro.SelectedValue))
            strRoleID = lstDanhSachVaiTro.SelectedValue.ToString();
        SysRole oRole = ProviderFactory.SysRoleProvider.GetById(strRoleID);
        if (oRole == null)
        {
            LoadDSVaiTro();
            return;
        }
        else
        {
            TList<SysUserRole> lstSysUserRole = ProviderFactory.SysUserRoleProvider.GetByRoleId(strRoleID);

            if (lstSysUserRole.Count > 0)
            {
                Thong_bao(this.Page, Resource.msgVaiTroDaDuocGanChoNguoiDung);
                return;
            }
            else
            {
                bool isuccessed = false;
                bool iDeleteSuccess = false;

                //Xóa danh sách vai trò được cấp cho người dùng
                TransactionManager objTransaction = ProviderFactory.Transaction;
                try
                {
                    TList<SysRolePermission> lstSysRolePermission = ProviderFactory.SysRolePermissionProvider.GetByRoleId(strRoleID);
                    foreach (SysRolePermission objSysRolePermission in lstSysRolePermission)
                    {
                        isuccessed = ProviderFactory.SysRolePermissionProvider.Delete(objTransaction, objSysRolePermission);
                        if (isuccessed == false)
                            objTransaction.Rollback();
                    }
                    iDeleteSuccess = ProviderFactory.SysRoleProvider.Delete(objTransaction, strRoleID);
                    objTransaction.Commit();
                    if (iDeleteSuccess)
                    {
                        LoadDSVaiTro();
                        Thong_bao(this.Page, string.Format(Resource.msgXoaVaiTroThanhCong, oRole.RoleName));
                    }
                    else
                    {
                        Thong_bao(this.Page, string.Format(Resource.msgXoaVaiTroThatBai, oRole.Error));
                        objTransaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    PageBase_Error(sender, e);
                    objTransaction.Rollback();
                    Thong_bao(this.Page, string.Format(Resource.msgXoaVaiTroThatBai, oRole.Error));
                    throw ex;
                }
            }
        }
    }
    /// <summary>
    /// sự kiện xảy ra khi người dùng thay đổi focus vai trò
    /// Chương trình load danh sách quyền được cấp và không được cấp tương ứng
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    protected void lstDanhSachVaiTro_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstDanhSachVaiTro.Items.Count <= 0)
            return;
        string strRoleID = string.Empty;
        if (lstDanhSachVaiTro.Items.Count > 0 && lstDanhSachVaiTro.SelectedValue != null)
            strRoleID = lstDanhSachVaiTro.SelectedValue.ToString();
        LoadDSRolePermission(strRoleID);
        LoadDSRolePermissionNotAssign(strRoleID);
    }
    /// <summary>
    /// Lấy danh sách quyền chưa được cấp cho vai trò đã chọn trong hệ thống
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    private void LoadDSRolePermissionNotAssign(string strRoleID)
    {

        TList<SysPermission> lstSYS_PERMISSION_ALL = ProviderFactory.SysPermissionProvider.GetAll();
        TList<SysPermission> lstSYS_PERMISSION_Not_tmp = new TList<SysPermission>();
        //danh sách sysRolePermission
        TList<SysRolePermission> lstSysRolePermission = ProviderFactory.SysRolePermissionProvider.GetByRoleId(strRoleID);
        //lấy danh quyền được gán
        foreach (SysRolePermission objSysRolePermission in lstSysRolePermission)
        {
            SysPermission objSysPermission = ProviderFactory.SysPermissionProvider.GetById(objSysRolePermission.PermissionId);
            if (lstSYS_PERMISSION_ALL.Contains(objSysPermission))
                lstSYS_PERMISSION_ALL.Remove(objSysPermission);
        }


        foreach (SysPermission obj in lstSYS_PERMISSION_ALL)
        {
            lstParent = new TList<SysPermission>();
            TList<SysPermission> objSYS_PERMISSION = GetTListParentPermission(obj.Id);
            if (!lstSYS_PERMISSION_Not_tmp.Contains(obj))
                lstSYS_PERMISSION_Not_tmp.Add(obj);

            if (objSYS_PERMISSION.Count > 0)
                foreach (SysPermission objtemp in objSYS_PERMISSION)
                {
                    if (!lstSYS_PERMISSION_Not_tmp.Contains(objtemp))
                    {
                        lstSYS_PERMISSION_Not_tmp.Add(objtemp);
                    }
                }
        }

        GetNotAssignTree(lstSYS_PERMISSION_Not_tmp, tvQuyenChuaCap, strRoleID);
    }
    /// <summary>
    /// xây dựng cây chứa danh quyền chưa gán
    /// </summary>
    /// <param name="lstSYS_ROLE"> danh sách quyền</param>
    /// <param name="strintRoleIDSelected"> mã vai trò</param>
    /// <param name="strintRoleIDSelected"> tên cây</param>
    /// <Modified>
    ///     Author      Date        Comments
    ///     Hùngnv   09/05/2009  /  Tạo mới
    /// </Modified>
    private void GetNotAssignTree(TList<SysPermission> lstSYS_PERMISSION, TreeView objTreeView, string strRoleID)
    {
        //Xóa cây
        objTreeView.Nodes.Clear();

        TList<SysPermission> lstPERMISSION_parent = new TList<SysPermission>();
        foreach (SysPermission obj in lstSYS_PERMISSION)
        {
            if (obj.ParentPermission == null)
                lstPERMISSION_parent.Add(obj);
        }
        foreach (SysPermission objSYS_PERMISSION in lstPERMISSION_parent)
        {
            TreeNode node = new TreeNode(objSYS_PERMISSION.Description);
            node.Value = objSYS_PERMISSION.Id.ToString();
            node.NavigateUrl = "#void(0);";
            AddNotAssignedChildNode(node, lstSYS_PERMISSION);
            objTreeView.Nodes.Add(node);
        }
        objTreeView.ExpandAll();

        //if (objTreeView.Nodes.Count > 0)
        //{
        //    if ((preNodeOnLeft != null) && (objTreeView.Nodes.Contains(preNodeOnLeft)))
        //    {
        //        objTreeView.FindNode(preNodeOnLeft.Value).Selected = true;
        //    }
        //}
    }
    /// <summary>
    /// Thêm lá vào cây chứa danh sách quyền chưa gán
    /// </summary>
    /// <param name="lstSYS_ROLE"> danh sách quyền</param>
    /// <param name="strintRoleIDSelected"> mã vai trò</param>
    /// <param name="strintRoleIDSelected"> tên node cha</param>
    /// <Modified>
    ///     Author      Date        Comments
    ///     Hùngnv   09/05/2009  /  Tạo mới
    /// </Modified>
    private void AddNotAssignedChildNode(TreeNode ParentNode, TList<SysPermission> lstSYS_PERMISSION)
    {
        foreach (SysPermission obj in lstSYS_PERMISSION)
        {
            if (obj.ParentPermission == ParentNode.Value)
            {
                TreeNode node = new TreeNode(obj.Description);
                node.Value = obj.Id.ToString();
                node.NavigateUrl = "#void(0);";
                AddNotAssignedChildNode(node, lstSYS_PERMISSION);
                ParentNode.ChildNodes.Add(node);
            }
        }
    }
    /// <summary>
    /// Lấy danh sách quyền được cấp cho vai trò đã chọn trong hệ thống
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    private void LoadDSRolePermission(string strRoleID)
    {
        //danh sách quyền được gán theo vai trò
        TList<SysPermission> lstSYS_PERMISSION = new TList<SysPermission>();
        TList<SysPermission> lstSYS_PERMISSION_tmp = new TList<SysPermission>();
        //danh sách sysRolePermission
        TList<SysRolePermission> lstSysRolePermission = ProviderFactory.SysRolePermissionProvider.GetByRoleId(strRoleID);
        //lấy danh quyền được gán
        foreach (SysRolePermission objSysRolePermission in lstSysRolePermission)
        {
            SysPermission objSysPermission = ProviderFactory.SysPermissionProvider.GetById(objSysRolePermission.PermissionId);
            if (!lstSYS_PERMISSION.Contains(objSysPermission))
                lstSYS_PERMISSION.Add(objSysPermission);
        }

        foreach (SysPermission obj in lstSYS_PERMISSION)
        {
            lstParent = new TList<SysPermission>();
            TList<SysPermission> objSYS_PERMISSION = GetTListParentPermission(obj.Id);
            if (!lstSYS_PERMISSION_tmp.Contains(obj))
                lstSYS_PERMISSION_tmp.Add(obj);

            if (objSYS_PERMISSION.Count > 0)
                foreach (SysPermission objtemp in objSYS_PERMISSION)
                {
                    if (!lstSYS_PERMISSION_tmp.Contains(objtemp))
                    {
                        lstSYS_PERMISSION_tmp.Add(objtemp);
                    }
                }
        }

        GetAssignedPermissionTree(lstSYS_PERMISSION_tmp, tvQuyenDaCap, strRoleID);
    }
    /// <summary>
    /// Xây dựng cây chứa danh sách quyền được gán
    /// </summary>
    /// <param name="strUsername">danh sách quyền</param>
    /// <param name="strUsername">Tên cây</param>
    ///  <param name="strUsername">Tên người dùng</param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                24/12/2008              
    /// </Modified>
    private void GetAssignedPermissionTree(TList<SysPermission> lstSYS_PERMISSION, TreeView objTreeView, string RoleID)
    {
        //xóa cây
        objTreeView.Nodes.Clear();

        TList<SysPermission> lstPERMISSION_parent = new TList<SysPermission>();
        foreach (SysPermission obj in lstSYS_PERMISSION)
        {
            if (obj.ParentPermission == null)
                lstPERMISSION_parent.Add(obj);
        }
        foreach (SysPermission objSYS_PERMISSION in lstPERMISSION_parent)
        {
            TreeNode node = new TreeNode(objSYS_PERMISSION.Description);
            node.Value = objSYS_PERMISSION.Id.ToString();
            node.NavigateUrl = "#void(0);";
            AddAssignedChildNode(node, lstSYS_PERMISSION);
            objTreeView.Nodes.Add(node);
        }
        objTreeView.ExpandAll();
        //if (objTreeView.Nodes.Count > 0)
        //{
        //    if ((preNodeOnRight != null) && (objTreeView.Nodes.Contains(preNodeOnRight)))
        //    {
        //        objTreeView.FindNode(preNodeOnRight.Value).Selected = true;
        //    }
        //}
    }
    /// <summary>
    /// Thêm các nút con vào cây chứa danh sách quyền được gán
    /// </summary>
    /// <param name="strUsername">Tên node cha</param>
    /// <param name="strUsername">mã quyền</param>
    ///  <param name="strUsername">tên người dùng</param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                24/12/2008              
    /// </Modified>
    private void AddAssignedChildNode(TreeNode ParentNode, TList<SysPermission> lstSYS_PERMISSION)
    {
        foreach (SysPermission obj in lstSYS_PERMISSION)
        {
            if (obj.ParentPermission == ParentNode.Value)
            {
                TreeNode node = new TreeNode(obj.Description);
                node.Value = obj.Id.ToString();
                node.NavigateUrl = "#void(0);";
                AddAssignedChildNode(node, lstSYS_PERMISSION);
                ParentNode.ChildNodes.Add(node);
            }
        }
    }
    /// <summary>
    ///Lấy quyền cha
    /// </summary>
    /// <param name="strintRoleIDSelected"> mã vai trò</param>       
    /// <Modified>
    ///     Author      Date        Comments
    ///     Hùngnv   09/05/2009  /  Tạo mới
    /// </Modified>
    private SysPermission GetParentPermission(string PermissionID)
    {
        SysPermission objSYS_PERMISSION = null;
        DataTable dtParentPermission = ProviderFactory.SysPermissionProvider.GetParent(PermissionID);
        if (dtParentPermission.Rows.Count > 0)
        {
            objSYS_PERMISSION = new SysPermission();
            objSYS_PERMISSION.Id = dtParentPermission.Rows[0]["Id"].ToString();
            objSYS_PERMISSION.PermissionName = dtParentPermission.Rows[0]["PermissionName"].ToString();
            objSYS_PERMISSION.Description = dtParentPermission.Rows[0]["Description"].ToString();
            if (dtParentPermission.Rows[0]["ParentPermission"] is DBNull)
                objSYS_PERMISSION.ParentPermission = null;
            else
                objSYS_PERMISSION.ParentPermission = dtParentPermission.Rows[0]["ParentPermission"].ToString();
        }
        return objSYS_PERMISSION;
    }
    /// <summary>
    /// Lấy danh sach quyền cha
    /// </summary>
    /// <param name="strUsername">mã quyền</param>        
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                24/12/2008              
    /// </Modified>
    private TList<SysPermission> GetTListParentPermission(string PermissionID)
    {
        //Lấy quyền cha
        DataTable dtParentPermission = ProviderFactory.SysPermissionProvider.GetParent(PermissionID);
        SysPermission objSYS_PERMISSION = null;
        if (dtParentPermission.Rows.Count > 0)
        {
            objSYS_PERMISSION = new SysPermission();
            objSYS_PERMISSION.Id = dtParentPermission.Rows[0]["Id"].ToString();
            objSYS_PERMISSION.PermissionName = dtParentPermission.Rows[0]["PermissionName"].ToString();
            objSYS_PERMISSION.Description = dtParentPermission.Rows[0]["Description"].ToString();
            if (dtParentPermission.Rows[0]["ParentPermission"] is DBNull)
                objSYS_PERMISSION.ParentPermission = null;
            else
                objSYS_PERMISSION.ParentPermission = dtParentPermission.Rows[0]["ParentPermission"].ToString();
        }
        if (objSYS_PERMISSION != null)
        {
            if (objSYS_PERMISSION.ParentPermission == null)
            {
                if (!lstParent.Contains(objSYS_PERMISSION))
                    lstParent.Add(objSYS_PERMISSION);
            }
            else
            {
                if (!lstParent.Contains(objSYS_PERMISSION))
                    lstParent.Add(objSYS_PERMISSION);
                GetTListParentPermission(objSYS_PERMISSION.Id);
            }
        }

        return lstParent;
    }
    protected void btnThemQuyen_Click(object sender, ImageClickEventArgs e)
    {
        if (tvQuyenChuaCap.CheckedNodes.Count == 0)
            return;
        AddPermission();
        lstDanhSachVaiTro_SelectedIndexChanged(null, null);
    }
    /// <summary>
    /// thêm quyền        
    /// </summary>        
    /// <Modified>
    ///     Author      Date        Comments
    ///     Hùngnv   09/05/2009  /  Tạo mới
    /// </Modified>
    private void AddPermission()
    {
        string strRoleID = lstDanhSachVaiTro.SelectedValue.ToString();
        foreach (TreeNode node in tvQuyenChuaCap.CheckedNodes)
        {
            string strPermissionID = node.Value.ToString();
            TList<SysPermission> lstChildPERMISSION = GetChild(strPermissionID);
            SysPermission obj = ProviderFactory.SysPermissionProvider.GetById(strPermissionID);
            if (!lstChildPERMISSION.Contains(obj))
                lstChildPERMISSION.Add(obj);

            lstParent = new TList<SysPermission>();
            TList<SysPermission> lstParentPERMISSION = GetTListParentPermission(strPermissionID);
            //neu cha co 1 con ==>add
            foreach (SysPermission objPermission in lstParentPERMISSION)
            {
                if (!lstChildPERMISSION.Contains(objPermission))
                    lstChildPERMISSION.Add(objPermission);
                SysPermission objparent = GetParentPermission(objPermission.Id);
                if (objparent != null)
                {
                    TList<SysPermission> lstChild = GetChild(objparent.Id);
                    if (lstChild.Count == 1)
                        if (!lstChildPERMISSION.Contains(objparent))
                            lstChildPERMISSION.Add(objparent);
                }
            }

            UpdatePermissionbyintRoleID(strRoleID, lstChildPERMISSION);
        }
    }
    /// <summary>
    /// Cập nhật quyền của người dùng đã chọn vào hệ thống
    /// </summary>
    /// <param name="lstSYS_ROLE"> danh sách quyền</param>
    /// <param name="strintRoleIDSelected"> mã vai trò</param>
    /// <Modified>
    ///     Author      Date        Comments
    ///     Hùngnv   09/05/2009  /  Tạo mới
    /// </Modified>
    private void UpdatePermissionbyintRoleID(string strRoleID, TList<SysPermission> lstSYS_PERMISSION)
    {
        bool blSuccess = false;
        //kiểm tra xem người dùng đó đã quyen trong hệ thống chưa??            
        //nếu chưa có thì insert vào bảng SYS_ROLE_PERMISSION
        //nếu có rồi thì chẳng làm gì cả
        foreach (SysPermission objSYS_PERMISSION in lstSYS_PERMISSION)
        {
            if (CheckExist(strRoleID, objSYS_PERMISSION.Id)) { }
            else
            {
                SysRolePermission objSYS_ROLE_PERMISSION = new SysRolePermission();
                objSYS_ROLE_PERMISSION.Id = "Insert";
                objSYS_ROLE_PERMISSION.RoleId = strRoleID;
                objSYS_ROLE_PERMISSION.PermissionId = objSYS_PERMISSION.Id;

                try
                {
                    blSuccess = ProviderFactory.SysRolePermissionProvider.Insert(objSYS_ROLE_PERMISSION);
                }
                catch (System.Exception ex)
                {
                    PageBase_Error(null, null);
                    throw ex;
                }
                if (!blSuccess)
                    Thong_bao(this.Page, Resource.msgCapNhatThatBai);
                // else
                //  SYS_LOG.Write(UserInfo.Instance().UserID, LogEventList.User_Insert, string.Format(Resources.msglogThemQuyenChoVaiTro, objSYS_PERMISSION.permission_name, intRole));
            }
        }
    }
    /// <summary>
    /// kiểm tra quyền có trong vai trò không
    /// </summary>
    /// <param name="RoleID"> mã vai trò</param>
    /// <param name="PermissionID">Mã quyền</param>    
    /// <Modified>
    ///     Author      Date        Comments
    ///     Hùngnv   09/05/2009  /  Tạo mới
    /// </Modified>   
    private bool CheckExist(string RoleID, string PermissionID)
    {
        DataTable dtRP = ProviderFactory.SysRolePermissionProvider.GetByRoleIDPermissionID(RoleID, PermissionID);
        dtRolePermission = dtRP;
        return dtRP.Rows.Count > 0;
    }
    /// <summary>
    /// Lấy quyền con
    /// </summary>
    /// <param name="PermissionID">Mã quyền</param>    
    /// <Modified>
    ///     Author      Date        Comments
    ///     Hùngnv   09/05/2009  /  Tạo mới
    /// </Modified> 
    private TList<SysPermission> GetChild(string PermissionID)
    {
        TList<SysPermission> lstChildPERMISSION = new TList<SysPermission>();
        DataTable dtSYS_PERMISSION = ProviderFactory.SysPermissionProvider.GetChild(PermissionID);
        SysPermission objSYS_PERMISSION = null;
        //bind dữ liệu vào object
        if (dtSYS_PERMISSION.Rows.Count > 0)
        {
            foreach (DataRow dr in dtSYS_PERMISSION.Rows)
            {
                objSYS_PERMISSION = new SysPermission();
                objSYS_PERMISSION.Id = dr["Id"].ToString();
                objSYS_PERMISSION.PermissionName = dr["PermissionName"].ToString();
                objSYS_PERMISSION.Description = dr["Description"].ToString();
                if (dr["ParentPermission"] is DBNull)
                    objSYS_PERMISSION.ParentPermission = null;
                else
                    objSYS_PERMISSION.ParentPermission = dr["ParentPermission"].ToString();
                if (!lstChildPERMISSION.Contains(objSYS_PERMISSION))
                    lstChildPERMISSION.Add(objSYS_PERMISSION);
            }
        }

        return lstChildPERMISSION;
    }
    protected void btnXoaQuyen_Click(object sender, ImageClickEventArgs e)
    {
        if (tvQuyenDaCap.CheckedNodes.Count == 0)
            return;
        RemovePermission();
        lstDanhSachVaiTro_SelectedIndexChanged(null, null);
    }
    /// <summary>
    /// loại bỏ quyền được gán
    /// </summary>      
    /// <Modified>
    ///     Author      Date        Comments
    ///     Hùngnv   09/05/2009  /  Tạo mới
    /// </Modified>
    private void RemovePermission()
    {
        string strRoleID = lstDanhSachVaiTro.SelectedValue.ToString();
        foreach (TreeNode node in tvQuyenDaCap.CheckedNodes)
        {
            string strPermisionID = node.Value.ToString();

            TList<SysPermission> lstChildPERMISSION = GetChild(strPermisionID);
            SysPermission objSYS_PERMISSION = ProviderFactory.SysPermissionProvider.GetById(strPermisionID);
            if (!lstChildPERMISSION.Contains(objSYS_PERMISSION))
                lstChildPERMISSION.Add(objSYS_PERMISSION);

            TList<SysPermission> lstParentPERMISSION = GetTListParentPermission(objSYS_PERMISSION.Id);
            //neu cha co 1 con ==>add
            foreach (SysPermission obj in lstParentPERMISSION)
            {
                if (!lstChildPERMISSION.Contains(obj))
                    lstChildPERMISSION.Add(obj);
                SysPermission objparent = GetParentPermission(obj.Id);
                if (objparent != null)
                {
                    TList<SysPermission> lstChild = GetTListParentPermission(objparent.Id);
                    if (lstChild.Count == 1)
                        if (!lstChildPERMISSION.Contains(objparent))
                            lstChildPERMISSION.Add(objparent);
                }
            }

            DeletePermissionByRoleID(strRoleID, lstChildPERMISSION);
        }
    }
    /// <summary>
    /// xóa vai trò của người dùng đã chọn
    /// </summary>
    /// <param name="lstSYS_ROLE"></param>
    /// <param name="strintRoleIDSelected"></param>
    /// <Modified>
    ///     Author      Date        Comments
    ///     Hùngnv   09/05/2009  /  Tạo mới
    /// </Modified>
    private void DeletePermissionByRoleID(string strRoleID, TList<SysPermission> lstSYS_PERMISSION)
    {
        bool iSuccess = false;
        //kiểm tra xem người dùng đó đã có vai trò trong bảng sys_user_role chưa?            
        //nếu chưa có thì chẳng làm gì cả
        //nếu có rồi thì delete khỏi bảng sys_user_role
        foreach (SysPermission objSYS_PERMISSION in lstSYS_PERMISSION)
        {
            if (CheckExist(strRoleID, objSYS_PERMISSION.Id))
            {
                SysRolePermission objSYS_ROLE_PERMISSION = new SysRolePermission();

                objSYS_ROLE_PERMISSION.Id = dtRolePermission.Rows[0]["Id"].ToString();
                objSYS_ROLE_PERMISSION.RoleId = dtRolePermission.Rows[0]["RoleId"].ToString();
                objSYS_ROLE_PERMISSION.PermissionId = dtRolePermission.Rows[0]["PermissionId"].ToString();
                try
                {
                    iSuccess = ProviderFactory.SysRolePermissionProvider.Delete(objSYS_ROLE_PERMISSION);
                }
                catch (System.Exception ex)
                {
                    PageBase_Error(null, null);
                    throw ex;
                }
                if (!iSuccess)
                    Thong_bao(this.Page, Resource.msgCapNhatThatBai);
                //else
                //    SYS_LOG.Write(UserInfo.Instance().UserID, LogEventList.User_Delete, string.Format(Resources.msglogQuyenChoVaiTro, objSYS_PERMISSION.permission_name, intRoleID));
            }
        }
    }
    /// <summary>
    /// Thêm hết quyền
    /// chuyển toàn bộ cây phân quyền từ danh sách quyền chưa được cấp sang cây phân quyền danh sách quyền được cấp
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    ///     Author      Date        Comments
    ///     Hùngnv   09/05/2009  /  Tạo mới
    /// </Modified>
    protected void btnThemHetQuyen_Click(object sender, ImageClickEventArgs e)
    {
        string strRoleID = lstDanhSachVaiTro.SelectedValue.ToString();
        List<string> lstPermisionID = new List<string>();
        foreach (TreeNode objTreeNode in tvQuyenChuaCap.Nodes)
        {
            lstPermisionID.Add(objTreeNode.Value);
            if (objTreeNode.ChildNodes.Count > 0)
            {
                if (!lstPermisionID.Contains(objTreeNode.Value))
                    lstPermisionID.Add(objTreeNode.Value);
                foreach (TreeNode obj in objTreeNode.ChildNodes)
                {
                    if (!lstPermisionID.Contains(obj.Value))
                        lstPermisionID.Add(obj.Value);
                }

            }
        }
        TList<SysPermission> lstSYS_PERMISSION = new TList<SysPermission>();

        foreach (string permissionid in lstPermisionID)
        {
            TList<SysPermission> lstChildPERMISSION = GetChild(permissionid);
            SysPermission objSYS_PERMISSION = ProviderFactory.SysPermissionProvider.GetById(permissionid);
            if (!lstChildPERMISSION.Contains(objSYS_PERMISSION))
                lstChildPERMISSION.Add(objSYS_PERMISSION);
            for (int i = 0; i <= lstChildPERMISSION.Count - 1; i++)
            {
                if (!lstSYS_PERMISSION.Contains(lstChildPERMISSION[i]))
                    lstSYS_PERMISSION.Add(lstChildPERMISSION[i]);
            }
        }
        UpdatePermissionbyintRoleID(strRoleID, lstSYS_PERMISSION);

        //load lại
        lstDanhSachVaiTro_SelectedIndexChanged(null, null);
    }
    /// <summary>
    /// bỏ hết quyền
    /// chuyển toàn bộ cây phân quyền từ danh sách quyền được cấp sang cây phân quyền danh sách quyền chưa được cấp
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    ///     Author      Date        Comments
    ///     Hùngnv   09/05/2009  /  Tạo mới
    /// </Modified>
    protected void btnXoaHetQuyen_Click(object sender, ImageClickEventArgs e)
    {
        string strRoleID = lstDanhSachVaiTro.SelectedValue.ToString();
        List<string> lstPermisionID = new List<string>();
        foreach (TreeNode objTreeNode in tvQuyenDaCap.Nodes)
        {
            lstPermisionID.Add(objTreeNode.Value);
            if (objTreeNode.ChildNodes.Count > 0)
            {
                lstPermisionID.Add(objTreeNode.Value);
                foreach (TreeNode obj in objTreeNode.ChildNodes)
                {
                    lstPermisionID.Add(obj.Value);
                }

            }
        }
        TList<SysPermission> lstSYS_PERMISSION = new TList<SysPermission>();

        foreach (string permissionid in lstPermisionID)
        {
            TList<SysPermission> lstChildPERMISSION = GetChild(permissionid);
            SysPermission objSYS_PERMISSION = ProviderFactory.SysPermissionProvider.GetById(permissionid);
            if (!lstChildPERMISSION.Contains(objSYS_PERMISSION))
                lstChildPERMISSION.Add(objSYS_PERMISSION);
            for (int i = 0; i <= lstChildPERMISSION.Count - 1; i++)
            {
                if (!lstSYS_PERMISSION.Contains(lstChildPERMISSION[i]))
                    lstSYS_PERMISSION.Add(lstChildPERMISSION[i]);
            }
        }

        DeletePermissionByRoleID(strRoleID, lstSYS_PERMISSION);
        //load lai trang
        lstDanhSachVaiTro_SelectedIndexChanged(null, null);
    }
}
