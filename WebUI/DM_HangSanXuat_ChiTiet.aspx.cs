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
using Cuc_QLCL.Entities;
using Resources;

public partial class WebUI_DM_HangSanXuat_ChiTiet : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DmHangSanXuat mDMHangSX;
            CheckPermission();
            if (Request["Ma"] != null)
            {
                //khoi tao doi tuong hang san xuat
                mDMHangSX = ProviderFactory.DmHangSanXuatProvider.GetById(Request["Ma"].ToString());
                txtId.Text = mDMHangSX.Id;
                txtTen.Text = mDMHangSX.TenHangSanXuat.ToString();
                txtTenTA.Text = mDMHangSX.TenTiengAnh.ToString();
            }
        }
        txtTen.Focus();
    }
    /// <summary>
    /// Kiểm tra người dùng có quyền cập nhật danh mục 
    /// </summary>
    /// <param name="MaDonVi"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// giangum                 5/5/2009              
    /// </Modified>
    void CheckPermission()
    {
        if (!mUserInfo.IsPermission(EnPermission.QUANLY_DM_HANGSX))
        {
            btnCapnhat.Visible = false;
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
            string strTenCu = ((DmHangSanXuat)ProviderFactory.DmHangSanXuatProvider.GetById(Request["Ma"].ToString())).TenHangSanXuat.ToString();
            if (ProviderFactory.DmHangSanXuatProvider.CheckExist(Ten, strTenCu))
            {
                Thong_bao(Resource.msgTrungTen);
                return false;
            }
        }
        else
        {
            if (ProviderFactory.DmHangSanXuatProvider.CheckExist(Ten, string.Empty))
            {
                Thong_bao(Resource.msgTrungTen);
                return false;
            }
        }
        return true;
    }
    protected void btnCapnhat_Click(object sender, EventArgs e)
    {
        if (CheckTrungTen(txtTen.Text.Trim()))
        {
            DmHangSanXuat mDMHangSX;
            if (Request["Ma"] != null)
                //load doi tuong hang san xuat theo ma
                mDMHangSX = ProviderFactory.DmHangSanXuatProvider.GetById(Request["Ma"].ToString());
            else
                //khoi tao doi tuong hang san xuat
                mDMHangSX = new DmHangSanXuat();

            mDMHangSX.Id = txtId.Text;
            mDMHangSX.TenHangSanXuat = txtTen.Text.Trim();
            mDMHangSX.TenTiengAnh = txtTenTA.Text.Trim();
            //kiem tra tinh hop le cua du lieu truoc khi ghi vao database
            if (mDMHangSX.IsValid)
            {
                try
                {
                    //ghi hang san xuat vao database
                    ProviderFactory.DmHangSanXuatProvider.Save(mDMHangSX);
                    if (Request["Ma"] != null)
                    {
                        ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.DM_HANG_SAN_XUAT_SUA, Resources.Resource.msgCapNhatHangSanXuat);
                        ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                               "<script>alert('" + Resources.Resource.msgCapNhatHangSanXuat + "'); opener.__doPostBack('HangSanXuatPostBack','');window.close();</script>");
                    }
                    else
                    {
                        ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.DM_HANG_SAN_XUAT_THEM_MOI, Resources.Resource.msgTaoMoiHangSanXuat);
                        if (Request["PostBack"] != null)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "msg", "window.close();window.opener.__doPostBack('HangSanXuatPostBack', '" + mDMHangSX.Id + "');", true);
                        }
                        else
                        {
                            //gán giá trị sử dụng khi cần postbak và reload lại data của form cha
                            Session["childForm"] = "HangSanXuat";
                            //ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                            //   "<script>alert('" + Resources.Resource.msgTaoMoiHangSanXuat + "'); </script>");
                            ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                               "<script>alert('" + Resources.Resource.msgTaoMoiHangSanXuat
                               + "'); opener.__doPostBack('HangSanXuatPostBack','');window.close();</script>");
                        }
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
