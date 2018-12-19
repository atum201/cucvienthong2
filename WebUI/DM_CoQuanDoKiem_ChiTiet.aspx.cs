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
using System.Collections.Generic;
using System.Text;
using System.IO;

public partial class WebUI_DM_CoQuanDoKiem : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            checkPermission();
            if (Request["Ma"] != null)
            {
                //khoi tao doi tuong co quan do kiem
                DmCoQuanDoKiem mDMCoQuanDK = ProviderFactory.DmCoQuanDoKiemProvider.GetById(Request["Ma"].ToString());
                txtId.Text = mDMCoQuanDK.Id;
                txtTenCoQuanTV.Text = mDMCoQuanDK.TenCoQuanDoKiem;
                txtTenCoQuanTA.Text = mDMCoQuanDK.TenTiengAnh;
                txtDiaChi.Text = mDMCoQuanDK.DiaChi;
                txtDienThoai.Text = mDMCoQuanDK.DienThoai;
                //LongHH
                List<String> files = QLCL_Patch.GetFileAttach_CoQuanDoKiem_Nop_HoSo(mDMCoQuanDK.Id);
                if (files.Count > 0)
                {
                    StringBuilder sbGIAY_PHEP_KINH_DOANH = new StringBuilder();
                    sbGIAY_PHEP_KINH_DOANH.Append("<a href='" + files[0] + "'>File &#273;&iacute;nh k&egrave;m</a>");
                    lbtnFileDinhKem.Text = sbGIAY_PHEP_KINH_DOANH.ToString();
                }
                //LongHH
            }
        }
        txtTenCoQuanTV.Focus();
    }
    /// <summary>
    /// Kiểm tra quyền của người dùng
    /// </summary>
    void checkPermission()
    {
        if (!mUserInfo.IsPermission(EnPermission.QUANLY_DM_COQUAN_DOKIEM))
        {
            btnCapNhat.Visible = false;

        }
    }
    /// <summary>
    /// Kiểm tra trùng mã
    /// </summary>
    /// <param name="MaDonVi"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Tuannd                5/5/2009              
    /// </Modified>
    public bool CheckTrungTen(string Ten)
    {
        //Neu la sua
        if (Request["Ma"] != null)
        {
            string strTenCu = ((DmCoQuanDoKiem)ProviderFactory.DmCoQuanDoKiemProvider.GetById(Request["Ma"].ToString())).TenCoQuanDoKiem.ToString();
            if (ProviderFactory.DmCoQuanDoKiemProvider.CheckExist(Ten, strTenCu))
            {
                Thong_bao(Resource.msgTrungTen);
                return false;
            }
        }
        else
        {
            if (ProviderFactory.DmCoQuanDoKiemProvider.CheckExist(Ten, string.Empty))
            {
                Thong_bao(Resource.msgTrungTen);
                return false;
            }
        }
        return true;
    }
    #region LongHH cập nhật FileAttach 
    private void UpdateFileAtachCoQuanDoKiem(string IdDonVi, ref FileUpload fu)
    {
        string TenFile = string.Empty;
        string FilePath = string.Empty;

        string ServerMapth = Server.MapPath("FileUpLoad\\");
        ServerMapth = ServerMapth.ToLower();
        ServerMapth = ServerMapth.Replace("\\webui", "");

        if (fu.HasFile)
        {
            TenFile = IdDonVi + "_FileDinhKem" + fu.FileName.Substring(fu.FileName.LastIndexOf("."));

            //cat bo cac khoang trong ve mot khoang
            while (TenFile.Contains("  "))
                TenFile = TenFile.Replace("  ", " ");
            //thay cac khoang trong bang '_'
            TenFile = TenFile.Replace(' ', '_');

            FilePath = ServerMapth + TenFile;
            TenFile = ResolveUrl("~/FileUpLoad/") + TenFile;
            try
            {
                if (!File.Exists(FilePath))
                {
                    fu.PostedFile.SaveAs(FilePath);
                }
                else
                {
                    QLCL_Patch.DeleteFile(FilePath);
                    fu.PostedFile.SaveAs(FilePath);
                }
                QLCL_Patch.SetFileAttach_CoQuanDoKiem(IdDonVi, TenFile);
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
    /// Cap nhat thong tin co quan do kiem va ghi vao database
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        if (CheckTrungTen(txtTenCoQuanTV.Text.Trim()))
        {
            DmCoQuanDoKiem mDMCoQuanDK;
            if (Request["Ma"] != null)
                //load doi tuong Co quan do kiem theo ma
                mDMCoQuanDK = ProviderFactory.DmCoQuanDoKiemProvider.GetById(Request["Ma"].ToString());
            else
                //khoi tao doi tuong co quan do kiem
                mDMCoQuanDK = new DmCoQuanDoKiem();

            mDMCoQuanDK.Id = txtId.Text;
            mDMCoQuanDK.TenCoQuanDoKiem = txtTenCoQuanTV.Text.Trim();
            mDMCoQuanDK.TenTiengAnh = txtTenCoQuanTA.Text.Trim();
            mDMCoQuanDK.DiaChi = txtDiaChi.Text.Trim();
            mDMCoQuanDK.DienThoai = txtDienThoai.Text.Trim();
            //kiem tra tinh hop le cua du lieu truoc khi ghi vao database
            if (mDMCoQuanDK.IsValid)
            {
                try
                {
                    //ghi thong tin co quan do kiem vao database
                    ProviderFactory.DmCoQuanDoKiemProvider.Save(mDMCoQuanDK);
                    //LongHH Add fileattach
                    UpdateFileAtachCoQuanDoKiem(mDMCoQuanDK.Id, ref fileupFileDinhKem);
                    //LongHh
                    if (Request["Ma"] != null)
                    {
                        ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.DM_CO_QUAN_DO_KIEM_SUA, Resources.Resource.msgDoKiemCapNhat);
                        
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "closePopUp", "<script>alert('"
                        + Resource.msgDoKiemCapNhat
                        + "');window.opener.__doPostBack('AddNewCommit','');self.close() ;</script>");
                    }
                    else
                    {
                        if (Request["PostBack"] != null)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "msg", "window.close();window.opener.__doPostBack('CoQuanDoKiemPostBack', '" + mDMCoQuanDK.Id + "');", true);
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "closePopUp", "<script>alert('"
                        + Resource.msgCoQuanDoKiem_TaoMoi
                        + "');window.opener.__doPostBack('AddNewCommit','');self.close() ;</script>");
                        }
                        ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.DM_CO_QUAN_DO_KIEM_THEM_MOI, "Tạo mới cơ quan đo kiểm");                        
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
