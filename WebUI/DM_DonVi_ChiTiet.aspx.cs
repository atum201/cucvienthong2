using System;
using System.Data;
using System.Security.Cryptography;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Cuc_QLCL.AdapterData;
using Cuc_QLCL.Entities;
using Resources;
using System.Collections.Generic;
using System.Text;
using System.IO;
/// <summary>
/// Thêm mới hoặc sửa thông tin chi tiết của đơn vị
/// </summary>
/// <Modified>
/// Name        Date        Comment
/// TuanND    5/07/2009     Thêm mới
/// </Modified>
public partial class WebUI_DM_DonVi_ChiTiet : PageBase
{
    /// <summary>
    /// Load thông tin chi tiết
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// TuanND    5/07/2009     Thêm mới
    /// </Modified>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCombo();
            checkPermission();
            
            if (Request.QueryString["ID"] != null)
            {
                //khoi tao doi tuong don vi

                DmDonVi mDmDonVi = ProviderFactory.DmDonViProvider.GetById(Request["ID"].ToString());
                txtMaDonVi.Text = mDmDonVi.MaDonVi.ToString();
                txtMatKhau.Attributes.Add("value", mDmDonVi.MatKhauKhongMaHoa.ToString());
                txtTenTiengAnh.Text = mDmDonVi.TenTiengAnh;
                txtTenTiengViet.Text = mDmDonVi.TenTiengViet;
                txtTenTat.Text = mDmDonVi.TenVietTat != null ? mDmDonVi.TenVietTat : String.Empty;
                cbTinhThanh.SelectedValue = mDmDonVi.TinhThanhId.ToString();
                txtDiaChi.Text = mDmDonVi.DiaChi.ToString();
                txtMail.Text = mDmDonVi.Email != null ? mDmDonVi.Email : string.Empty;
                txtPhone.Text = mDmDonVi.DienThoai.ToString();
                txtFax.Text = mDmDonVi.Fax != null ? mDmDonVi.Fax : string.Empty;
                txtMaSoThue.Text = mDmDonVi.MaSoThue != null ? mDmDonVi.MaSoThue : string.Empty;
                chkPassChange.Visible = true;
                chkPassChange.Checked = false;
                txtMatKhau.ReadOnly = true;
                RequiredFieldValidator1.Enabled = chkPassChange.Checked;
                //LongHH
                List<String> files = QLCL_Patch.GetFileAttach_DonVi_Nop_HoSo(mDmDonVi.Id);
                if(files.Count>0){
                    StringBuilder sbGIAY_PHEP_KINH_DOANH = new StringBuilder();
                    sbGIAY_PHEP_KINH_DOANH.Append("<a href='" + files[0] + "'>GiayPhepKinhDoanh.doc</a>");
                    lbtnGIAY_PHEP_KINH_DOANH.Text = sbGIAY_PHEP_KINH_DOANH.ToString();
                }
                string IDNguoiTao = QLCL_Patch.GetNguoiTao_DonVi(mDmDonVi.Id);
                
                if (!string.IsNullOrEmpty(IDNguoiTao)) {
                    SysUser u = ProviderFactory.SysUserProvider.GetById(IDNguoiTao);
                    if (u != null) {
                        txtNguoiTao.Text = u.FullName;
                    }
                }
                //LongHH
            }
            else
            {
                // Hiển thị mật khẩu sinh ngẫu nhiên và mã đơn vị theo quy tắc [A-C]xxxx
                txtMatKhau.Text = SinhMatKhauNgauNhien();
                string prefix = "A";
                if (mUserInfo.TrungTam.Id == "TTCN")
                    prefix = "A";
                else if (mUserInfo.TrungTam.Id == "TT2")
                    prefix = "B";
                else
                    prefix = "C";

                int SoLuong = ProviderFactory.DmDonViProvider.GetByTinhThanhThuocTrungTam().Rows.Count + 1;
                string Stt = SoLuong.ToString();
                if (Stt.Length == 1)
                    Stt = "000" + Stt;
                else if (Stt.Length == 2)
                    Stt = "00" + Stt;
                else if (Stt.Length == 3)
                    Stt = "0" + Stt;
                else if (Stt.Length >= 4)
                    Stt = Stt.Substring(0, 4);
                
                txtMaDonVi.Text = prefix + Stt;
            }
        }
    }

    /// <summary>
    /// Sinh ngẫu nhiêu chuỗi ký tự dùng làm mật khẩu
    /// </summary>
    /// <returns></returns>
    /// Author          date        comment
    /// TuấnVM          10/07/2009  Tạo mới
    public string SinhMatKhauNgauNhien()
    {
        string str = "ZXCVBNMLKJHGFDSAQWERTYUIOPzxcvbnmlkjhgfdsaqwertyuio0123456789";
        char[] charArr = str.ToCharArray();
        string pass = string.Empty;
        Random autoRand = new Random();
        for (int length = 0; length < 10; length++)
        {
            // Lấy số thứ tự ngẫu nhiên là một số nhỏ hơn độ dài của chuỗi str 
            int index = autoRand.Next() % (str.Length);

            // Thêm ký tự ngẫu nhiên trong mảng charArr vào chuỗi
            pass += charArr[index].ToString();
        }
        return pass;
    }

    /// <summary>
    /// Kiểm tra quyền của người dùng
    /// </summary>
    void checkPermission()
    {
        if (!mUserInfo.IsPermission(EnPermission.QUANLY_DM_DONVI))
        {
            btnCapNhat.Visible = false;

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
        TList<DmTinhThanh> lstDonVi = ProviderFactory.DmTinhThanhProvider.GetByTrungTamId(ProviderFactory.SysConfigProvider.GetValue("MA_TRUNG_TAM"));
        DmTinhThanh objTinhThanh = new DmTinhThanh();
        objTinhThanh.Id = "";
        objTinhThanh.TenTinhThanh = "Chọn tỉnh thành";
        lstDonVi.Insert(0, objTinhThanh);
        cbTinhThanh.DataSource = lstDonVi;

        cbTinhThanh.DataTextField = "TenTinhThanh";
        cbTinhThanh.DataValueField = "ID";
        cbTinhThanh.DataBind();
    }
    /// <summary>
    /// Kiểm tra trùng mã
    /// </summary>
    /// <param name="MaDonVi"></param>
    public bool CheckTrungMa(string MaDonVi)
    {
        //Neu la sua
        if (Request.QueryString["ID"] != null)
        {
            string strMaCu = ((DmDonVi)ProviderFactory.DmDonViProvider.GetById(Request["ID"].ToString())).MaDonVi.ToString();
            if (ProviderFactory.DmDonViProvider.CheckExist(MaDonVi, strMaCu))
            {
                Thong_bao(Resource.msgTrungMa);
                return false;
            }
        }
        else
        {
            if (ProviderFactory.DmDonViProvider.CheckExist(MaDonVi, string.Empty))
            {
                Thong_bao(Resource.msgTrungMa);
                return false;
            }
        }
        return true;
    }
    
    #region LongHH cập nhật FileAttach Giấy phép kinh doanh
    public void UpdateGiayPhepKinhDoanh(string IdDonVi, ref FileUpload fu)
    {
        string TenFile = string.Empty;
        string FilePath = string.Empty;
        
        string ServerMapth = Server.MapPath("FileUpLoad\\");
        ServerMapth = ServerMapth.ToLower();
        ServerMapth = ServerMapth.Replace("\\webui", "");
        
        if (fu.HasFile)
        {
            TenFile = IdDonVi + "_GIAYPHEPKINHDOANH" + fu.FileName.Substring(fu.FileName.LastIndexOf("."));

            //cat bo cac khoang trong ve mot khoang
            while (TenFile.Contains("  "))
                TenFile = TenFile.Replace("  ", " ");
            //thay cac khoang trong bang '_'
            TenFile = TenFile.Replace(' ', '_');

            FilePath = ServerMapth + TenFile;
            TenFile = ResolveUrl("~/FileUpLoad/") + TenFile;
            try
            {
                if (!File.Exists(FilePath)){
                    fu.PostedFile.SaveAs(FilePath);
                }
                else
                {
                    QLCL_Patch.DeleteFile(FilePath);
                    fu.PostedFile.SaveAs(FilePath);
                }
                QLCL_Patch.SetFileAttach_DonVi_Nop_HoSo(IdDonVi, TenFile);
            }
            catch
            {
            }
        }
    }

    public bool CheckFileSize(ref FileUpload fu)
    {
        if (!fu.HasFile)
            return true;
        int max = Convert.ToInt16(ConfigurationManager.AppSettings["MaxFileUpLoadSize"]);
        int filesize = fu.PostedFile.ContentLength / 1024;
        if (filesize > max)
            return false;
        return true;
    }
    #endregion
    /// <summary>
    /// Xác nhận cập nhật dữ liệu
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// TuanND    5/07/2009     Thêm mới
    /// </Modified>
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        if (fileupGiayPhepKinhDoanh.HasFile)
        {
            bool b = CheckFileSize(ref fileupGiayPhepKinhDoanh);
            if (!b)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Dung lượng file " + fileupGiayPhepKinhDoanh.FileName + " tải lên phải nhỏ hơn hoặc bằng 5 MB!');</script>");
                return;
            }
        }
        if (cbVanTao.Checked==false && QLCL_Patch.CheckTrungTenDonVi(txtTenTiengViet.Text.Trim())) {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo",
                        "<script>alert('Tên Đơn vị bị trùng, xin hãy kiểm tra đơn vị đã tồn tại chưa. Hoặc tích chọn \"Vẫn tạo khi trùng tên\" để tạo Đơn vị.');</script>");
            return;
        }
        if (CheckTrungMa(txtMaDonVi.Text.Trim()))
        {
            DmDonVi objDonVi;
            if (Request.QueryString["ID"] != null)
            {
                objDonVi = ProviderFactory.DmDonViProvider.GetById(Request["ID"].ToString());
            }
            else
            {
                objDonVi = new DmDonVi();
            }
            objDonVi.MaDonVi = txtMaDonVi.Text.Trim();
            //Ma hoa
            if (!chkPassChange.Visible)
            {
                objDonVi.MatKhau = EncryptPassword(txtMatKhau.Text.Trim());
                objDonVi.MatKhauKhongMaHoa = txtMatKhau.Text;
            }
            else
            {
                if (chkPassChange.Checked)
                {
                    objDonVi.MatKhau = EncryptPassword(txtMatKhau.Text.Trim());
                    objDonVi.MatKhauKhongMaHoa = txtMatKhau.Text;
                }

            }
            objDonVi.TenTiengAnh = txtTenTiengAnh.Text.Trim();
            objDonVi.TenTiengViet = txtTenTiengViet.Text.Trim();
            objDonVi.TenVietTat = txtTenTat.Text.Trim();
            objDonVi.TinhThanhId = cbTinhThanh.SelectedValue;
            objDonVi.DiaChi = txtDiaChi.Text.Trim();
            objDonVi.Email = txtMail.Text.Trim();
            objDonVi.DienThoai = txtPhone.Text.Trim();
            objDonVi.Fax = txtFax.Text.Trim();
            objDonVi.MaSoThue = txtMaSoThue.Text.Trim();

            try
            {
                EntityState state = objDonVi.EntityState;
                ProviderFactory.DmDonViProvider.Save(objDonVi);
                //LongHH Add fileattach
                UpdateGiayPhepKinhDoanh(objDonVi.Id, ref fileupGiayPhepKinhDoanh);
                QLCL_Patch.SetNguoiTao_DonVi(objDonVi.Id, mUserInfo.UserID);
                //LongHh
                //Ghi log
                if (state == EntityState.Added)
                {   
                    ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.DM_DON_VI_THEM_MOI, "Thêm mới đơn vị: " + objDonVi.TenTiengViet);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "closePopUp",
                        "<script>alert('" + Resource.msgTaoMoiThanhCong + "');opener.__doPostBack('AddNewCommit','" + objDonVi.Id + "');self.close() ;</script>");
                }
                else
                {
                    ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.DM_DON_VI_SUA, "Cập nhật đơn vị: " + objDonVi.TenTiengViet);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "closePopUp",
                    "<script>alert('" + Resource.msgCapNhatThanhCong + "');opener.__doPostBack('AddNewCommit','" + objDonVi.Id + "');self.close() ;</script>");
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    /// <summary>
    /// Thoát
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// TuanND    5/07/2009     Thêm mới
    /// </Modified>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);

    }
    protected void chkPassChange_CheckedChanged(object sender, EventArgs e)
    {
        txtMatKhau.Attributes.Add("value", "");
        txtMatKhau.ReadOnly = !chkPassChange.Checked;
        RequiredFieldValidator1.Enabled = chkPassChange.Checked;
    }
}
