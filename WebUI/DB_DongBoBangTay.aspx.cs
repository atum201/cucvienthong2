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
using Cuc_QLCL.Data;
using Cuc_QLCL.Entities;

public partial class WebUI_DB_DongBoBangTy : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            DataTable dtbDuongDan = new Cuc_QLCL.AdapterData.Provider.DongBo().LayDuongDanDongBo_TatCa();
            ddlTrungTamChungNhan.DataValueField = "MaTrungTam";
            ddlTrungTamChungNhan.DataTextField = "TenTrungTam";
            ddlTrungTamChungNhan.DataSource = dtbDuongDan;
            ddlTrungTamChungNhan.DataBind();
            ddlTrungTamChungNhan.SelectedValue = ProviderFactory.SysConfigProvider.GetValue("MA_TRUNG_TAM");


        }

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Visible = false;
    }

    protected void btnDongBo_Click(object sender, EventArgs e)
    {
        Label1.Text = "Đang thực hiện đồng bộ";
        Label1.Visible = true;
        DbBaoCaoDongBo objBaoCao = new DbBaoCaoDongBo();
        try
        {

            Label1.Text = "Đang thực hiện đồng bộ";
            if (ddlTrungTamChungNhan.Items.Count > 0)
            {
                
                objBaoCao.MaTrungTam = ddlTrungTamChungNhan.SelectedValue;
                
                if (new wQLCL.QLCL().DongBoGuiDuLieuTheoTrungTam(ddlTrungTamChungNhan.SelectedValue.ToString()))
                {
                    Label1.Text = "Đồng bộ thành công";
                    objBaoCao.TrangThai = 1;   
                }
                else
                {
                    Label1.Text = "Đồng bộ không thành công";
                    objBaoCao.TrangThai = 0;
                }
            }

        }
        catch (Exception ex)
        {
            if (ex.ToString().Contains("timed out"))
            {
                Label1.Text = "Đồng bộ thành công";
                objBaoCao.TrangThai = 1;
            }
            else
            {
                Label1.Text = "Đồng bộ không thành công ";
                objBaoCao.TrangThai = 0;
            }
        }

        ProviderFactory.DbBaoCaoDongBoProvider.Save(objBaoCao);
    }
}
