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
using Cuc_QLCL.Data;
using Resources;

public partial class WebUI_DM_TrungTam_ChiTiet : PageBase
{
    DmTrungTam updateDmTrungTam = null;
    String Action = string.Empty;
    String TrungTamId = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    ///  Author                      Date            Action
    /// Nguyễn Trung Tuyến          06/05/2009      Create new
    /// </modified>
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (Request["action"] != null) 
             Action = Request["action"].ToString().ToLower();
         if (Request["TrungTamId"] != null) 
             TrungTamId = Request["TrungTamId"].ToString();
        if (!IsPostBack)
        {
            LoadTinhThanhToListCheckbox();
            LoadTinhThanhToDropList();
        }
        if (Action == "edit")
        {
            if (!IsPostBack)
            {
                GetTrungTamInformation();
            }
        }
        CheckPermission();
       
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
        if (!mUserInfo.IsPermission(EnPermission.QUANLY_DM_TRUNGTAM))
        {
            btnCapNhat.Visible = false;
        }

    }
    /// <summary>
    /// Lay danh sach cac tinh thanh chua duoc gan cho trung tam nao
    /// </summary>
    /// <modified>
    ///  Author                      Date            Action
    /// Nguyễn Trung Tuyến          06/05/2009      Create new
    /// </modified>
    public void LoadTinhThanhToListCheckbox()
    {
        TList<DmTinhThanh> listTinhThanh = ProviderFactory.DmTinhThanhProvider.GetAll();
        chklTinhThanh.DataValueField = "Id";
        chklTinhThanh.DataTextField = "TenTinhThanh";
        chklTinhThanh.DataSource = listTinhThanh;
        chklTinhThanh.DataBind();
    }
    /// <summary>
    /// Lay danh sach tat ca cac tinh thanh 
    /// </summary>
    /// <modified>
    ///  Author                      Date            Action
    /// Nguyễn Trung Tuyến          06/05/2009      Create new
    /// </modified>
    public void LoadTinhThanhToDropList()
    {
        TList<DmTinhThanh> listTinhThanh = ProviderFactory.DmTinhThanhProvider.GetAll();
        drlTinhThanh.DataTextField = "TenTinhThanh";
        drlTinhThanh.DataValueField= "Id";
        drlTinhThanh.DataSource = listTinhThanh;
        drlTinhThanh.DataBind();
        ListItem item = new ListItem("Chọn...", "0");
        drlTinhThanh.Items.Insert(0, item);
       
    }
    /// <summary>
    /// Lay thong tin cua trung tam can sua doi
    /// </summary>
    ///  Author                      Date            Action
    /// Nguyễn Trung Tuyến          06/05/2009      Create new
    /// </modified>
    public void GetTrungTamInformation()
    {
        if (TrungTamId.Length > 0) 
        {
            updateDmTrungTam = ProviderFactory.DmTrungTamProvider.GetById(TrungTamId);
            txtTenTrungTam.Text = updateDmTrungTam.TenTrungTam;
            txtTenTrungTamTA.Text = updateDmTrungTam.TenTiengAnh;
            txtMaTrungTam1.Text = updateDmTrungTam.MaTrongGcn;
            txtMaTrungTam2.Text = updateDmTrungTam.MaTrungTam;
            txtSoTaiKhoan.Text = updateDmTrungTam.SoTaiKhoan;
            txtTenKhoBac.Text = updateDmTrungTam.TenKhoBac;
            txtTenDonViThuHuong.Text = updateDmTrungTam.TenCoQuanThuHuong;
            txtDienThoai.Text = updateDmTrungTam.DienThoai;
            txtDiaChi.Text = updateDmTrungTam.DiaChi;
            txtEmail.Text = updateDmTrungTam.Email;
            txtFax.Text = updateDmTrungTam.Fax;
            txtDiaChiDonViThuHuong.Text = updateDmTrungTam.DiaChiCoQuanThuHuong;
            // Lấy thông tin đơn vị thụ hưởng CNHQ
            txtSoTaiKhoanHQ.Text = updateDmTrungTam.SoTaiKhoanCuc;
            txtTenKhoBacHQ.Text = updateDmTrungTam.TenKhoBacCuc;
            txtDonViThuHuongHQ.Text = updateDmTrungTam.TenCoQuanThuHuongCuc;
            txtDiaChiDonViThuHuongHQ.Text = updateDmTrungTam.DiaChiCoQuanThuHuongCuc;

            GetTinhThanhForTrungTam(updateDmTrungTam.Id);
            drlTinhThanh.SelectedValue = updateDmTrungTam.TinhThanhId.Trim();
        }

    }
    /// <summary>
    /// Kiểm tra trùng mã
    /// </summary>
    public bool CheckExist(string Ma,string Ten)
    {
        //Neu la sua
        if (Request["TrungTamId"] != null)
        {
            DmTrungTam objTrungTam = ProviderFactory.DmTrungTamProvider.GetById(Request["TrungTamId"].ToString());
            string strMaCu = objTrungTam.MaTrungTam;
            string strTenCu = objTrungTam.TenTrungTam;
            if (ProviderFactory.DmTrungTamProvider.CheckExist(Ma, strMaCu,Ten,strTenCu))
            {
                Thong_bao(Resource.msgTrungMaTen);
                return false;
            }
        }
        //Neu la them moi
        else
        {
            if (ProviderFactory.DmTrungTamProvider.CheckExist(Ma, string.Empty,Ten,string.Empty))
            {
                Thong_bao(Resource.msgTrungMaTen);
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Cập nhật đối tượng
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    ///  Author                       Date            Action 
    /// Nguyễn Trung Tuyến          06/05/2009      Create new
    /// </modified>
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        if (CheckExist(txtMaTrungTam2.Text.Trim(), txtTenTrungTam.Text.Trim()))
        {
            //String Id= string.Empty;     
            if (TrungTamId.Length > 0 && Action == "edit")
            {
                //tiến hanh lấy thông tin sửa đổi của trung tâm
                updateDmTrungTam = ProviderFactory.DmTrungTamProvider.GetById(TrungTamId);
                updateDmTrungTam.TenTrungTam = txtTenTrungTam.Text;
                updateDmTrungTam.TenTiengAnh = txtTenTrungTamTA.Text;
                updateDmTrungTam.MaTrongGcn = txtMaTrungTam1.Text;
                updateDmTrungTam.MaTrungTam = txtMaTrungTam2.Text;
                updateDmTrungTam.SoTaiKhoan = txtSoTaiKhoan.Text;
                updateDmTrungTam.TenKhoBac = txtTenKhoBac.Text;
                updateDmTrungTam.TenCoQuanThuHuong = txtTenDonViThuHuong.Text;
                updateDmTrungTam.DienThoai = txtDienThoai.Text;
                updateDmTrungTam.DiaChi = txtDiaChi.Text;
                updateDmTrungTam.Email = txtEmail.Text;
                updateDmTrungTam.Fax = txtFax.Text;
                updateDmTrungTam.DiaChiCoQuanThuHuong = txtDiaChiDonViThuHuong.Text.Trim();
                updateDmTrungTam.TinhThanhId = drlTinhThanh.SelectedValue;
                // Lấy thông tin đơn vị thụ hưởng CNHQ
                updateDmTrungTam.SoTaiKhoanCuc = txtSoTaiKhoanHQ.Text;
                updateDmTrungTam.TenKhoBacCuc = txtTenKhoBacHQ.Text;
                updateDmTrungTam.DiaChiCoQuanThuHuongCuc = txtDiaChiDonViThuHuongHQ.Text;
                updateDmTrungTam.TenCoQuanThuHuongCuc = txtDonViThuHuongHQ.Text;

                if (updateDmTrungTam.IsValid)
                {
                    //Cập nhật đối tượng, sử dụng transaction

                    TransactionManager transaction = ProviderFactory.Transaction;
                    try
                    {
                        ProviderFactory.DmTrungTamProvider.Save(transaction, updateDmTrungTam);
                        String Id = updateDmTrungTam.Id;
                        string strLogString = "Sửa thông tin Trung Tâm có mã là: " + Id;
                        //tiến hành thiết lập các tỉnh thành hiện thuộc đang thuộc trung tâm về một trung tâm ảo
                        SetTinhThanhTTIDToDefault(Id,updateDmTrungTam.MaTrongGcn, transaction);
                        //cập nhật lại các tỉnh thành thuôc trung tâm 
                        UpdateTinhThanhForTrungTam(Id, transaction);
                        
                        transaction.Commit();

                        ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.DM_TRUNG_TAM_SUA, strLogString.Substring(0, strLogString.Length - 1));
                        Thong_bao("Cập nhật thành công");
                        ClientScript.RegisterStartupScript(this.GetType(), "msg", "window.close();window.opener.__doPostBack('ChildWindowPostBack', '');", true);
                        //đóng trang popup và gọi phương thức postback tai vua trang goi trang popup

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
                else
                {
                    throw new Exception(updateDmTrungTam.Error);
                }

            }
            else
            {
                DmTrungTam dmTrungTam = new DmTrungTam();
                dmTrungTam.MaTrongGcn = txtMaTrungTam1.Text;
                dmTrungTam.MaTrungTam = txtMaTrungTam2.Text;
                dmTrungTam.TenCoQuanThuHuong = txtTenDonViThuHuong.Text;
                dmTrungTam.TenKhoBac = txtTenKhoBac.Text;
                dmTrungTam.TenTiengAnh = txtTenTrungTamTA.Text;
                dmTrungTam.TenTrungTam = txtTenTrungTam.Text;
                dmTrungTam.TinhThanhId = drlTinhThanh.SelectedValue;
                dmTrungTam.SoTaiKhoan = txtSoTaiKhoan.Text;
                dmTrungTam.DiaChi = txtDiaChi.Text;
                dmTrungTam.DiaChiCoQuanThuHuong = txtDiaChiDonViThuHuong.Text;
                dmTrungTam.DienThoai = txtDienThoai.Text;
                dmTrungTam.Email = txtEmail.Text;
                dmTrungTam.Fax = txtFax.Text;
                dmTrungTam.NgayCapNhatSauCung = DateTime.Now;

                // Lấy thông tin đơn vị thụ hưởng CNHQ
                dmTrungTam.SoTaiKhoanCuc = txtSoTaiKhoanHQ.Text;
                dmTrungTam.TenKhoBacCuc = txtTenKhoBacHQ.Text;
                dmTrungTam.DiaChiCoQuanThuHuongCuc = txtDiaChiDonViThuHuongHQ.Text;
                dmTrungTam.TenCoQuanThuHuongCuc = txtDonViThuHuongHQ.Text;

                //Thêm mới đối tượng, sử dụng transaction
                TransactionManager transaction = ProviderFactory.Transaction;
                try
                {
                    ProviderFactory.DmTrungTamProvider.Insert(transaction, dmTrungTam);

                    transaction.Commit();
                    transaction.Dispose();
                    //lấy ID của trung tâm mới được tạo ra

                    String id = dmTrungTam.Id;
                    string strLogString = "Thêm mói Trung Tâm có mã là: " + id;
                    transaction = ProviderFactory.Transaction;                  
                    //cập nhật lại các tỉnh thành thuôc trung tâm 
                    UpdateTinhThanhForTrungTam(id, transaction);                    
                    transaction.Commit();

                    ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.DM_TRUNG_TAM_THEM_MOI, strLogString.Substring(0, strLogString.Length - 1));
                    ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                            "<script>alert('Thêm mới thành công!') </script>");
                    //đóng trang popup và gọi phương thức postback tại trang vua gọi trang popup
                    ClientScript.RegisterStartupScript(this.GetType(), "msg", "window.close();window.opener.__doPostBack('ChildWindowPostBack', '');", true);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }

    /// <summary>
    /// Cập nhật MaTrungTam cua TinhThanh theo TrungTam
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    ///  Author                       Date            Action 
    /// Nguyễn Trung Tuyến          06/05/2009      Create new
    /// </modified>
    public void UpdateTinhThanhForTrungTam(String TrungTamId,TransactionManager transaction)
    {
        
        foreach (ListItem item in chklTinhThanh.Items)
        {
            if (item.Selected)
            {
                String strId = item.Value;                        
                    try
                    {
                        DmTinhThanh o = ProviderFactory.DmTinhThanhProvider.GetById(transaction, strId);
                        o.TrungTamId = TrungTamId;
                        ProviderFactory.DmTinhThanhProvider.Update(transaction, o);         
                    }
                    catch (Exception ex)
                    {        
                        throw ex;
                    }
               
            }
        }

    }

    /// <summary>
    ///Chuyen ca TrungTamId trong danh sach cac tinh thanh thuoc trung tam ve mac dinh.
    /// </summary>
    /// <param name="TrungTamId"></param>
    /// <modified>
    ///  Author                       Date            Action 
    /// Nguyễn Trung Tuyến          06/05/2009      Create new
    /// </modified>
    public void SetTinhThanhTTIDToDefault(String TrungTamId,String MaTT,TransactionManager transaction)
    {
        try
        {
            String strdefault = string.Empty;
            if (MaTT == "A")
                strdefault = "B";
            else if (MaTT == "B")
                strdefault = "A";
            else
                strdefault = "B";
            DmTrungTam dmTrungTam = ProviderFactory.DmTrungTamProvider.GetById(transaction,TrungTamId);
            dmTrungTam.DmTinhThanhCollection = ProviderFactory.DmTinhThanhProvider.GetByTrungTamId(transaction,dmTrungTam.Id);
            TList<DmTinhThanh> listdmTinhThanh = dmTrungTam.DmTinhThanhCollection;
            foreach (DmTinhThanh o in listdmTinhThanh)
            {
                DataTable dt = ProviderFactory.DmTrungTamProvider.GetTrungTamIDByMaGCN(strdefault, transaction);
                if (dt.Rows.Count > 0)
                {
                    o.TrungTamId = dt.Rows[0]["id"].ToString();
                    ProviderFactory.DmTinhThanhProvider.Save(transaction, o);
                }
            }
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
      
    }

    /// <summary>
    /// Lay cac tinh thanh truc thuoc trung tam
    /// </summary>
    /// <modified>
    ///  Author                       Date            Action 
    /// Nguyễn Trung Tuyến          06/05/2009      Create new
    /// </modified>
    public void GetTinhThanhForTrungTam(String TrungTamId)
    {
         DmTrungTam dmTrungTam = ProviderFactory.DmTrungTamProvider.GetById(TrungTamId);
         dmTrungTam.DmTinhThanhCollection = ProviderFactory.DmTinhThanhProvider.GetByTrungTamId(dmTrungTam.Id);
         TList<DmTinhThanh> listdmTinhThanh = dmTrungTam.DmTinhThanhCollection;
         foreach (DmTinhThanh o in listdmTinhThanh)
         {
             foreach (ListItem item in chklTinhThanh.Items)
             {
                 String strId = item.Value;
                 if (strId == o.Id)
                     item.Selected = true;
             }
         }
    }

    
}
