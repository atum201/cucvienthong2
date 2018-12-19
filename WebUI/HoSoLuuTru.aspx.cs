﻿using System;
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
using System.IO;
using Cuc_QLCL.Data.SqlClient;
using Cuc_QLCL.Data;

public partial class WebUI_HoSoLuuTru : PageBase
{
    HoSo objHS = new HoSo();
    string url = "";
    string action = "";
    DateTime dt = DateTime.Now;
    protected void Page_Load(object sender, EventArgs e)
    {
        url = Request["direct"];
        action = Request["action"];
        if (!IsPostBack)
        {
            txtTuNgay.Text = DateTime.Now.ToShortDateString();
            Session["TaiLieuDinhKem"] = new List<TaiLieuDinhKem>();
        }

        //if (action != null || action != "")
        //{
            //if (action == "view")
            //{
                ChangeControlStatus(false);
            //}
        //}
        //else
        //{
        LuuTruHoSo(Request["HoSoID"], Request["TrangThaiId"]);
        //}
    }
    private void BindLaiThongTinDaLuuTru(string HoSoId)
    {
        objHS = ProviderFactory.HoSoProvider.GetById(HoSoId);
        txtNoiLuuTru.Text = objHS.NoiLuuTru;
        txtLuuTruSo.Text = objHS.SoLuuTru;
        txtGhiChu.Text = objHS.GhiChuLuuTru;
        txtTuNgay.Text = ((DateTime)objHS.NgayLuuTru).ToShortDateString();
        txtSoHoSo.Text = objHS.SoLuuTru;
    }
    /// <summary>
    /// Lấy mã Hồ sơ và kiểm tra trạng thái.
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 11/5/2009              
    /// </Modified>
    private void LuuTruHoSo(string HoSoID, string TrangThaiID)
    {
        if ((HoSoID != string.Empty) && (HoSoID != null))
        {
            objHS = ProviderFactory.HoSoProvider.GetById(HoSoID);
            if (objHS.TrangThaiId == (int)EnTrangThaiHoSoList.DA_DONG)
            {
                BindLaiThongTinDaLuuTru(HoSoID);
                dtlFile.DataSource = ProviderFactory.TaiLieuDinhKemProvider.GetBySanPhamId(HoSoID);
                dtlFile.DataKeyField = "TenFile";
                dtlFile.DataBind();
                lnkThemFile.Visible = false;
                btnCapNhat.Visible = false;
                fileUploadScanFile.Visible = false;
                divScanFile.Visible = false;
                return;
            }
            if (objHS == null)
            {
                Response.Redirect("CN_HoSo_QuanLy.aspx");
            }
            else
            {
                txtSoHoSo.Text = objHS.SoHoSo;
                txtLuuTruSo.Text = objHS.SoHoSo;
            }
        }
        else
        {
            Response.Redirect("CN_HoSo_QuanLy.aspx");
        }
        txtNoiLuuTru.Attributes.Add("onkeyup", " if (!checkLength('" + txtNoiLuuTru.ClientID + "', '4000')) return false;");
        txtGhiChu.Attributes.Add("onkeyup", " if (!checkLength('" + txtGhiChu.ClientID + "', '4000')) return false;");
    }
    /// <summary>
    /// Lưu trữ hồ sơ
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 11/5/2009              
    /// </Modified>
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        objHS.NoiLuuTru = txtNoiLuuTru.Text;
        if ((txtTuNgay.Text != string.Empty) && (txtTuNgay.Text != null))
        {
            objHS.NgayLuuTru = Convert.ToDateTime(txtTuNgay.Text);
        }
        objHS.SoLuuTru = txtLuuTruSo.Text;
        objHS.GhiChuLuuTru = txtGhiChu.Text;

        objHS.TrangThaiId = (int)EnTrangThaiHoSoList.DA_DONG;
        TransactionManager trans = ProviderFactory.Transaction;
        try
        {
            ProviderFactory.HoSoProvider.Save(trans, objHS);
            List<TaiLieuDinhKem> lstTaiLieuDinhKem = Session["TaiLieuDinhKem"] as List<TaiLieuDinhKem>;
            foreach (TaiLieuDinhKem obj in lstTaiLieuDinhKem)
            {
                string TenFile = String.Empty;
                obj.SanPhamId = objHS.Id;
                if (File.Exists(Server.MapPath(obj.TenFile)))
                {
                    TenFile = Server.MapPath(obj.TenFile).Replace("TempFile", "FileUpload");
                    File.Copy(Server.MapPath(obj.TenFile), TenFile);
                    File.Delete(Server.MapPath(obj.TenFile));
                    ProviderFactory.TaiLieuDinhKemProvider.Save(trans, obj);
                }
            }
            trans.Commit();
            ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.CN_LUU_TRU, Resource.msgLuuTruHoSo);
            ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                   "<script>alert('" + Resource.msgLuuTruHoSo + "'); location.href='CN_HoSo_QuanLy.aspx';</script>");
        }
        catch (Exception ex)
        {
            trans.Rollback();
            throw ex;
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CN_HoSo_QuanLy.aspx?UserControl=" + url);
    }

    private void ChangeControlStatus(bool status)
    {
        Control control = Master.FindControl("ContentPlaceHolder1");
        foreach (Control ctrl in control.Controls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Enabled = status;
            else if (ctrl is Button)
                ((Button)ctrl).Enabled = status;
            if (ctrl is LinkButton)
                ((LinkButton)ctrl).Enabled = status;
            else if (ctrl is RadioButton)
                ((RadioButton)ctrl).Enabled = status;
            else if (ctrl is ImageButton)
                ((ImageButton)ctrl).Enabled = status;
            else if (ctrl is CheckBox)
                ((CheckBox)ctrl).Enabled = status;
            else if (ctrl is DropDownList)
                ((DropDownList)ctrl).Enabled = status;
            else if (ctrl is HyperLink)
                ((HyperLink)ctrl).Enabled = status;
        }
    }

    protected void chkbInsert_CheckedChanged(object sender, EventArgs e)
    {
        if (chkbInsert.Checked)
        {
            txtLuuTruSo.ReadOnly = false;
            txtLuuTruSo.Text = "";
        }
        else
        {
            txtLuuTruSo.ReadOnly = true;
            txtLuuTruSo.Text = objHS.SoHoSo + "/" + dt.Year.ToString();
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        if (File.Exists(Server.MapPath(lnk.CommandArgument)))
        {
            TaiLieuDinhKem objTaiLieuDinhKem = null;
            File.Delete(Server.MapPath(lnk.CommandArgument));
            List<TaiLieuDinhKem> lstTaiLieuDinhKem = (List<TaiLieuDinhKem>)Session["TaiLieuDinhKem"];
            foreach (TaiLieuDinhKem obj in lstTaiLieuDinhKem)
            {
                if (obj.TenFile == lnk.CommandArgument)
                {
                    objTaiLieuDinhKem = obj;
                }
            }
            lstTaiLieuDinhKem.Remove(objTaiLieuDinhKem);

            Session["TaiLieuDinhKem"] = lstTaiLieuDinhKem;
            dtlFile.DataSource = lstTaiLieuDinhKem;
            dtlFile.DataKeyField = "TenFile";
            dtlFile.DataBind();
        }

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (fileUploadScanFile.PostedFile.FileName != string.Empty)
        {
            string FileName = string.Empty;
            String Stt = ProviderFactory.TaiLieuDinhKemProvider.GetNextSTT();
            string TrungTamID = mUserInfo.TrungTam.Id;
            string strName = System.IO.Path.GetFileName(fileUploadScanFile.PostedFile.FileName);
            FileName = @"../TempFile/LT_" + TrungTamID + "_" + Stt + "_" + strName;

            fileUploadScanFile.SaveAs(Server.MapPath(FileName));

            List<TaiLieuDinhKem> lstTaiLieuDinhKem = null;
            if (!(Session["TaiLieuDinhKem"] is List<TaiLieuDinhKem>))
            {
                lstTaiLieuDinhKem = new List<TaiLieuDinhKem>();
            }
            else
            {
                lstTaiLieuDinhKem = (List<TaiLieuDinhKem>)Session["TaiLieuDinhKem"];
            }
            TaiLieuDinhKem objTaiLieuDinhKem = TaiLieuDinhKem.CreateTaiLieuDinhKem(Convert.ToString(lstTaiLieuDinhKem.Count + 1), Convert.ToString(lstTaiLieuDinhKem.Count + 1), 12, FileName, DateTime.Now, false);

            bool chkExist = false;
            foreach (TaiLieuDinhKem obj in lstTaiLieuDinhKem)
            {
                if (obj.TenFile == objTaiLieuDinhKem.TenFile)
                {
                    chkExist = true;
                    break;
                }
            }
            if (!chkExist)
            {
                lstTaiLieuDinhKem.Insert(0, objTaiLieuDinhKem);
                dtlFile.DataSource = lstTaiLieuDinhKem;
                dtlFile.DataKeyField = "TenFile";
                dtlFile.DataBind();
                Session["TaiLieuDinhKem"] = lstTaiLieuDinhKem;
            }
        }
    }
    protected void dtlFile_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        HyperLink lnk = (HyperLink)e.Item.FindControl("lnkTenFile");
        LinkButton lnkBtn = (LinkButton)e.Item.FindControl("lnkXoaFile");
        if (lnk != null)
        {
            string strTenFile = DataBinder.Eval(e.Item.DataItem, "TenFile").ToString();
            string[] arrTen = strTenFile.Split('_');
            lnk.Text = arrTen[arrTen.Length - 1];
            lnk.NavigateUrl = strTenFile;
            lnk.Target = "blank";
            lnkBtn.CommandArgument = strTenFile;
            lnkBtn.Visible = (objHS.TrangThaiId != (int)EnTrangThaiHoSoList.DA_DONG);
        }

    }
}
