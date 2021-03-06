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
using Resources;
using Cuc_QLCL.AdapterData;
using Cuc_QLCL.Data;

public partial class WebUI_HT_NguoiDung_TaoMoi : PageBase
{
    //Khai báo đối tượng người dùng
    SysUser objSysUser = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        rvdate.MaximumValue = DateTime.Now.ToShortDateString();
        if (Request["UID"] != null)
        {
            rMatKhau.Style.Add("display", "none");
            SetReadOnly(txtTenDangNhap);
            rqfieldPassWord.Visible = false;
            this.Page.Title = "SỬA THÔNG TIN NGƯỜI DÙNG";
            lblTieuDe.Text = "SỬA THÔNG TIN NGƯỜI DÙNG";
            objSysUser = ProviderFactory.SysUserProvider.GetById(Request["UID"].ToString());
        }
        else
        {
            rThayDoiMatKhau.Style.Add("display", "none");
            this.Page.Title = "Thêm mới người dùng";
        }

        if (!this.Page.IsPostBack)
        {
            //Lấy mã Trung Tâm
            if (Request["UID"] != null)
            {
                chkThayDoiMatKhau.Checked = false;
                chkThayDoiMatKhau_CheckedChanged(null, null);
            }
            else
            {
                chkThayDoiMatKhau_CheckedChanged(null, null);
            }
            //Lấy danh sách đơn vị
            LoadDMTrungTam();
            //Lấy phòng ban
            LoadDMPhongBan();
            //Lấy chức vụ
            LoadDMChucVu();

            if (objSysUser != null)
                LoadSysUserDetail(objSysUser.Id);
        }

    }

    /// <summary>
    /// Lấy thông tin chi tiết người dùng
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    private void LoadSysUserDetail(string ID)
    {
        string strMaTrungTam = mUserInfo.TrungTam.MaTrungTam + "_";
        SysUser oUser = ProviderFactory.SysUserProvider.GetById(ID);

        if (oUser != null)
        {
            ddlTenDonVi.SelectedValue = oUser.OrganizationId;
            ddlTenDonVi.Enabled = false;
            ddlTenPhongBan.SelectedValue = oUser.DepartmentId;
            ddlChucVu.SelectedValue = oUser.Position;
            DmTrungTam objDmTrungTam = ProviderFactory.DmTrungTamProvider.GetById(oUser.OrganizationId);
            if (objDmTrungTam.NguoiTiepNhanHoSoQuaMang + "" != "")
            {
                if (objDmTrungTam.NguoiTiepNhanHoSoQuaMang == oUser.Id)
                    chkTiepNhanHoSoQuaMang.Checked = true;
            }

            txtTenDangNhap.Text = oUser.UserName.Replace(strMaTrungTam, string.Empty);
            txtMatKhau.Attributes.Add("value", oUser.PassWord);
            txtHoTen.Text = oUser.FullName;
            if (oUser.DateOfBirth == null)
                txtNgaySinh.Text = string.Empty;
            else
                txtNgaySinh.Text = Convert.ToDateTime(oUser.DateOfBirth).ToShortDateString();
            txtQueQuan.Text = oUser.PlaceOfBirth;
            bool blCheck;

            if (oUser.Gender == null)
                rbNu.Checked = rbNam.Checked = false;
            else
            {
                blCheck = Convert.ToBoolean(oUser.Gender);
                rbNu.Checked = !blCheck;
                rbNam.Checked = blCheck;
            }

            txtDiaChi.Text = oUser.Address;
            txtDienThoai.Text = oUser.PhoneNumber;
            txtEmail.Text = oUser.Email;
        }
    }
    /// <summary>
    /// Lấy danh sách trung tâm
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    private void LoadDMTrungTam()
    {
        ddlTenDonVi.DataSource = ProviderFactory.DmTrungTamProvider.GetALL_RealTrungTam_Extend();
        ddlTenDonVi.DataValueField = "ID";
        ddlTenDonVi.DataTextField = "TenTrungTam";
        ddlTenDonVi.DataBind();
        ddlTenDonVi.SelectedValue = mUserInfo.TrungTam.Id;
        ddlTenDonVi.Enabled = false;
    }
    /// <summary>
    /// Lấy danh sách phòng ban
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    private void LoadDMPhongBan()
    {
        TList<DmPhongBan> lstDMPhongBan = ProviderFactory.DmPhongBanProvider.GetByTrungTamId(mUserInfo.TrungTam.Id);

        ddlTenPhongBan.DataValueField = "ID";
        ddlTenPhongBan.DataTextField = "TenPhongBan";
        ddlTenPhongBan.DataSource = lstDMPhongBan;
        ddlTenPhongBan.DataBind();
        //Tìm phòng ban là: Không xác định và chọn lấy 1 phòng ban
        if (ddlTenPhongBan.Items.FindByText("Không xác định") != null)
            ddlTenPhongBan.Items.FindByText("Không xác định").Selected = true;
    }
    /// <summary>
    /// Lấy danh sách chức vụ
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    private void LoadDMChucVu()
    {
        TList<EnChucVu> lstDMChucVu = ProviderFactory.EnChucVuProvider.GetAll();

        ddlChucVu.DataValueField = "ID";
        ddlChucVu.DataTextField = "MoTa";
        ddlChucVu.DataSource = lstDMChucVu;
        ddlChucVu.DataBind();
    }
    /// <summary>
    /// sự kiện xảy ra khi người dùng bấm nút bỏ qua
    /// load lại trang Quản lý người dùng
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    protected void btnBoQua_Click(object sender, EventArgs e)
    {
        Response.Redirect("HT_NguoiDung_QuanLy.aspx");
    }
    /// <summary>
    /// Thêm người dùng vào hệ thống
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Author                  date                comment
    /// Hùngnv                5/5/2009              tạo mới
    /// TuấnVM                  10/07               Sửa (cập nhật TruongPhongID trong DM_PhongBan, khi người dùng đc chọn làm trưởng phòng)
    /// </Modified>
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        string strMaTrungTam = mUserInfo.TrungTam.MaTrungTam + "_";
        DateTime? dateOfbirth = null;
        if (txtNgaySinh.Text.Trim().Length > 0)
            dateOfbirth = Convert.ToDateTime(txtNgaySinh.Text);
        bool? blGender = null;
        blGender = Convert.ToBoolean(rbNam.Checked);

        if (objSysUser != null)
        {//cập nhật người dùng
            objSysUser.UserName = strMaTrungTam + txtTenDangNhap.Text;
            bool blExistUserName = CheckExistUserName(objSysUser.UserName, objSysUser.Id);
            if (blExistUserName)
            {
                Thong_bao(this.Page, Resource.msgNguoiDungDaThamGiaVaoHeThong);
                return;
            }
            else
            {
                if (chkThayDoiMatKhau.Checked)
                {
                    if (!txtMatKhauMoi.Text.Equals(txtNhapLaiMatKhau.Text))
                    {
                        Thong_bao(this.Page, Resource.msgMatKhauKhongKhop);
                        return;
                    }
                    else
                        objSysUser.PassWord = Security.EncryptPassword(txtMatKhauMoi.Text);
                }
                objSysUser.FullName = txtHoTen.Text;
                objSysUser.DateOfBirth = dateOfbirth;
                objSysUser.Address = txtDiaChi.Text;
                objSysUser.PlaceOfBirth = txtQueQuan.Text;
                objSysUser.Email = txtEmail.Text;
                objSysUser.PhoneNumber = txtDienThoai.Text;
                objSysUser.Gender = blGender;
                objSysUser.OrganizationId = ddlTenDonVi.SelectedValue.ToString();
                objSysUser.DepartmentId = ddlTenPhongBan.SelectedValue.ToString();
                objSysUser.Position = ddlChucVu.SelectedValue.ToString();
                if (objSysUser.IsValid)
                {
                    TransactionManager objTransaction = ProviderFactory.Transaction;
                    try
                    {
                        // Nếu người dùng là trưởng phòng thì cập nhật lại thông tin trong danh mục phòng ban
                        if (objSysUser.Position == Convert.ToInt32(EnChucVuList.TRUONG_PHONG).ToString())
                        {
                            DmPhongBan objDmPhongBan = ProviderFactory.DmPhongBanProvider.GetById(objSysUser.DepartmentId);
                            objDmPhongBan.TruongPhongId = objSysUser.Id;
                            ProviderFactory.DmPhongBanProvider.Save(objDmPhongBan);
                        }

                        ProviderFactory.SysUserProvider.Save(objTransaction, objSysUser);

                        // Nếu người dùng là giám đốc thì cập nhật lại thông tin trong danh mục trung tâm
                        DmTrungTam objDmTrungTam = ProviderFactory.DmTrungTamProvider.GetById(objSysUser.OrganizationId);
                        if (objSysUser.Position == Convert.ToInt32(EnChucVuList.GDTT).ToString())
                        {
                            objDmTrungTam.GiamDocId = objSysUser.Id;
                        }

                        // Nếu người dùng được gán trách nhiệm nhận hồ sơ qua mạng thì cập nhật lại danh mục trung tâm
                        if (chkTiepNhanHoSoQuaMang.Checked)
                        {
                            objDmTrungTam.NguoiTiepNhanHoSoQuaMang = objSysUser.Id;
                        }
                        else
                        {
                            // Nếu người đang có trách nhiệm nhận hồ sơ qua mạng thì mà bị loại bỏ trách nhiệm này thì chuyển trách nhiệm cho admin
                            if (objDmTrungTam.NguoiTiepNhanHoSoQuaMang == objSysUser.Id)
                            {
                                objDmTrungTam.NguoiTiepNhanHoSoQuaMang = ProviderFactory.SysUserProvider.GetByUserName(ProviderFactory.SysConfigProvider.GetValue("ADMIN_USER")).Id;
                            }
                        }
                        ProviderFactory.DmTrungTamProvider.Save(objDmTrungTam);
                        if (objSysUser.Position == Convert.ToInt32(EnChucVuList.GDTT).ToString())
                            ProviderFactory.SysUserProvider.UpdateGiamDoc(objTransaction, objSysUser.Id);
                        objTransaction.Commit();
                        Thong_bao(this.Page, string.Format(Resource.msgCapNhatNguoiDungThanhCong, objSysUser.UserName.Replace(strMaTrungTam, string.Empty)), "HT_NguoiDung_QuanLy.aspx");
                    }
                    catch (Exception ex)
                    {
                        objTransaction.Rollback();
                        throw ex;
                    }
                }
                else
                {
                    Thong_bao(this.Page, string.Format(Resource.msgCapNhatNguoiDungThatBai, objSysUser.Error));
                }
            }
        }
        else//tạo mới người dùng
        {
            bool blExistUserName = CheckExistUserName(strMaTrungTam + txtTenDangNhap.Text.Trim(), null);
            if (blExistUserName)
            {
                Thong_bao(this.Page, Resource.msgNguoiDungDaThamGiaVaoHeThong);
                return;
            }
            else
            {
                SysUser objCreateUser = new SysUser();
                objCreateUser.Id = "1111";
                objCreateUser.Stt = 1;
                objCreateUser.UserName = strMaTrungTam + txtTenDangNhap.Text;
                objCreateUser.PassWord = EncryptPassword(txtMatKhau.Text);
                objCreateUser.FullName = txtHoTen.Text;
                objCreateUser.DateOfBirth = dateOfbirth;
                objCreateUser.Address = txtDiaChi.Text;
                objCreateUser.PlaceOfBirth = txtQueQuan.Text;
                objCreateUser.Email = txtEmail.Text;
                objCreateUser.PhoneNumber = txtDienThoai.Text;
                objCreateUser.Gender = blGender;
                objCreateUser.OrganizationId = ddlTenDonVi.SelectedValue.ToString();
                objCreateUser.DepartmentId = ddlTenPhongBan.SelectedValue.ToString();
                objCreateUser.Position = ddlChucVu.SelectedValue.ToString();

                if (objCreateUser.IsValid)
                {

                    TransactionManager objTransaction = ProviderFactory.Transaction;
                    try
                    {
                        // Insert thông tin người dùng vào CSDL
                        ProviderFactory.SysUserProvider.Save(objTransaction, objCreateUser);

                        // Nếu người dùng là trưởng phòng thì cập nhật lại thông tin trong danh mục phòng ban
                        if (objCreateUser.Position == Convert.ToInt32(EnChucVuList.TRUONG_PHONG).ToString())
                        {
                            DmPhongBan objDmPhongBan = ProviderFactory.DmPhongBanProvider.GetById(objCreateUser.DepartmentId);
                            objDmPhongBan.TruongPhongId = objCreateUser.Id;
                            ProviderFactory.DmPhongBanProvider.Save(objDmPhongBan);
                        }

                        // Nếu người dùng là giám đốc thì cập nhật lại thông tin trong danh mục trung tâm
                        DmTrungTam objDmTrungTam = ProviderFactory.DmTrungTamProvider.GetById(objCreateUser.OrganizationId);
                        if (objCreateUser.Position == Convert.ToInt32(EnChucVuList.GDTT).ToString())
                        {
                            objDmTrungTam.GiamDocId = objCreateUser.Id;
                        }

                        // Nếu người dùng có trách nhiệm nhận hồ sơ qua mạng thì cập nhật thông tin trong danh mục trung tâm
                        if (chkTiepNhanHoSoQuaMang.Checked)
                        {
                            objDmTrungTam.NguoiTiepNhanHoSoQuaMang = objCreateUser.Id;
                        }

                        ProviderFactory.DmTrungTamProvider.Save(objDmTrungTam);

                        if (objCreateUser.Position == Convert.ToInt32(EnChucVuList.GDTT).ToString())
                            ProviderFactory.SysUserProvider.UpdateGiamDoc(objTransaction, objCreateUser.Id);
                        objTransaction.Commit();
                        Thong_bao(this.Page, string.Format(Resource.msgTaoMoiNguoiDungThanhCong, objCreateUser.UserName.Replace(strMaTrungTam, string.Empty)), "HT_NguoiDung_QuanLy.aspx");
                    }
                    catch (Exception ex)
                    {
                        objTransaction.Rollback();
                        throw ex;
                    }
                }
                else
                {
                    Thong_bao(this.Page, string.Format(Resource.msgTaoNguoiDungThatBai, objCreateUser.Error));
                }
            }
        }

    }
    /// <summary>
    /// xác định trạng thái của các validation control
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                 5/5/2009              
    /// </Modified>
    protected void chkThayDoiMatKhau_CheckedChanged(object sender, EventArgs e)
    {
        string strDisplay = "none";
        if (chkThayDoiMatKhau.Checked)
            strDisplay = "";
        CheckEnable(strDisplay);
    }
    /// <summary>
    /// set enable cho các dòng mật khẩu khi chọn vào checkbox thay đổi mật khẩu
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    private void CheckEnable(string Display)
    {
        rMatKhauMoi1.Style.Add("display", Display);
        rMatKhauMoi2.Style.Add("display", Display);
        if (Display == "none")
        {
            RequiredFieldPassword1.Visible = false;
            RequiredFieldPassword2.Visible = false;
        }
        else
        {
            RequiredFieldPassword1.Visible = true;
            RequiredFieldPassword2.Visible = true;
        }
    }
    /// <summary>
    /// xác định xem Người dùng có trùng tên không?
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    private bool CheckExistUserName(string UserName, string UserID)
    {
        bool blExist = false;
        TList<SysUser> lstSysUser = new TList<SysUser>();
        lstSysUser = ProviderFactory.SysUserProvider.GetAll();
        foreach (SysUser obj in lstSysUser)
        {
            if (UserID != null)
            {
                if (obj.UserName.ToUpper() == UserName.ToUpper() && obj.Id != UserID && obj.DepartmentId == mUserInfo.TrungTam.Id)
                {
                    blExist = true;
                    break;
                }
            }
            else
            {
                if (obj.UserName.ToUpper() == UserName.ToUpper())
                {
                    blExist = true;
                    break;
                }
            }
        }
        return blExist;
    }

    /// <summary>
    /// Cập nhật giám đố
    /// </summary>
    /// <param name="_tran">TransactionManager</param>
    /// <param name="UserID">ID của giám đốc mới (tức là khi cập nhật ko cập nhật thằng này)</param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
    /// </Modified>
    private int UpdateGiamDoc(TransactionManager _tran, string UserID)
    {
        int iResult = 0;
        iResult = ProviderFactory.SysUserProvider.UpdateGiamDoc(_tran, UserID);
        return iResult;
    }
}
