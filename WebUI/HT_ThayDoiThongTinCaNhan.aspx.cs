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

public partial class WebUI_HT_ThayDoiThongTinCaNhan : PageBase
{
    //Đối tượng người dùng 
    SysUser objSysUser = new SysUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        rvdate.MaximumValue = DateTime.Now.ToShortDateString();

        objSysUser = ProviderFactory.SysUserProvider.GetById(mUserInfo.UserID);
        if (!IsPostBack)
        {
            chkThayDoiMatKhau.Checked = false;
            CheckEnable("none");            
            //Lấy danh sách đơn vị
            LoadDMTrungTam();
            //Lấy phòng ban
            LoadDMPhongBan();
            if (objSysUser != null)
            {
                //Lấy thông tin người dùng
                ddlTenDonVi.SelectedValue = objSysUser.OrganizationId;
                ddlTenPhongBan.SelectedValue = objSysUser.DepartmentId;
                //ddlChucVu.SelectedValue = oUser.Position;
                string strMatrungTam = mUserInfo.TrungTam.MaTrungTam;
                txtTenDangNhap.Text = objSysUser.UserName.Replace(strMatrungTam + "_",string.Empty);
                //txtMatKhau.Attributes.Add("value", oUser.PassWord);
                txtHoTen.Text = objSysUser.FullName;
                if (objSysUser.DateOfBirth == null)
                    txtNgaySinh.Text = string.Empty;
                else
                    txtNgaySinh.Text = Convert.ToDateTime(objSysUser.DateOfBirth).ToShortDateString() ;
                txtQueQuan.Text = objSysUser.PlaceOfBirth;
                bool blCheck;
                if (objSysUser.Gender == null)
                    rbNu.Checked = rbNam.Checked = false;
                else
                {
                    blCheck = Convert.ToBoolean(objSysUser.Gender);
                    rbNu.Checked = !blCheck;
                    rbNam.Checked = blCheck;
                }

                txtDiaChi.Text = objSysUser.Address;
                txtDienThoai.Text = objSysUser.PhoneNumber;
                txtEmail.Text = objSysUser.Email;
            }
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
        TList<DmTrungTam> lstDmTrungTam = ProviderFactory.DmTrungTamProvider.GetAll();

        ddlTenDonVi.DataValueField = "MaTrungTam";
        ddlTenDonVi.DataTextField = "TenTrungTam";
        ddlTenDonVi.DataSource = lstDmTrungTam;
        ddlTenDonVi.DataBind();
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
        TList<DmPhongBan> lstDMPhongBan = ProviderFactory.DmPhongBanProvider.GetAll();

        ddlTenPhongBan.DataValueField = "ID";
        ddlTenPhongBan.DataTextField = "TenPhongBan";
        ddlTenPhongBan.DataSource = lstDMPhongBan;
        ddlTenPhongBan.DataBind();
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
        //TList<EnChucVu> lstDMChucVu = ProviderFactory.EnChucVuProvider.GetAll();

        //ddlChucVu.DataValueField = "ID";
        //ddlChucVu.DataTextField = "MoTa";
        //ddlChucVu.DataSource = lstDMChucVu;
        //ddlChucVu.DataBind();
    }
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        if (objSysUser != null)
        {//cập nhật người dùng
            if (chkThayDoiMatKhau.Checked)
            {
                if (!Security.EncryptPassword(txtMatKhauCu.Text).Equals(objSysUser.PassWord))
                {
                    Thong_bao(this.Page, Resource.msgMatKhauCuKhongDung);
                    return;
                }
                else if (!txtMatKhauMoi.Text.Equals(txtNhapLaiMatKhau.Text))
                {
                    Thong_bao(this.Page, Resource.msgMatKhauKhongKhop);
                    return;
                }
                else
                    objSysUser.PassWord = Security.EncryptPassword(txtMatKhauMoi.Text);
            }
            DateTime? dateOfbirth = null;
            if (txtNgaySinh.Text.Trim().Length > 0)
                dateOfbirth = Convert.ToDateTime(txtNgaySinh.Text);
            bool? blGender = null;
            blGender = Convert.ToBoolean(rbNam.Checked);

            objSysUser.UserName = mUserInfo.MaTrungTam  + "_" + txtTenDangNhap.Text;
            objSysUser.FullName = txtHoTen.Text;
            objSysUser.DateOfBirth = dateOfbirth;
            objSysUser.Address = txtDiaChi.Text;
            objSysUser.PlaceOfBirth = txtQueQuan.Text;
            objSysUser.Email = txtEmail.Text;
            objSysUser.PhoneNumber = txtDienThoai.Text;
            objSysUser.Gender = blGender;
            objSysUser.OrganizationId = ddlTenDonVi.SelectedValue.ToString();
            objSysUser.DepartmentId = ddlTenPhongBan.SelectedValue.ToString();
            //objSysUser.Position = ddlChucVu.SelectedValue.ToString();
            if (objSysUser.IsValid)
            {
                TransactionManager objTransaction = ProviderFactory.Transaction;
                try
                {
                    ProviderFactory.SysUserProvider.Save(objTransaction, objSysUser);
                    objTransaction.Commit();
                    Thong_bao(this.Page, string.Format(Resource.msgCapNhatNguoiDungThanhCong, txtTenDangNhap.Text), "Default.aspx");
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
    protected void btnBoQua_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    /// <summary>
    /// sự kiện xảy ra khi người dùng check vào checkbox thay đổi mật khẩu
    /// chương trình ẩn hiện các dòng cho phép người dùng gõ lại mật khẩu
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Hùngnv                5/5/2009              
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
        rMatKhauCu.Style.Add("display", Display);
        rMatKhauMoi1.Style.Add("display", Display);
        rMatKhauMoi2.Style.Add("display", Display);
        if (Display == "none")
        {
            RequiredFieldPassWord.Visible = false;
            RequiredFieldPassword1.Visible = false;
            RequiredFieldPassword2.Visible = false;
        }
        else {
            RequiredFieldPassWord.Visible = true;
            RequiredFieldPassword1.Visible = true;
            RequiredFieldPassword2.Visible = true;
        }
    }
}
