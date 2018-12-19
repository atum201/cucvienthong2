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
using CrystalDecisions.CrystalReports.Engine;
using Cuc_QLCL.Entities;
using Cuc_QLCL.AdapterData;
using System.Collections.Generic;

public partial class WebUI_BC_BaoCaoTuanIn : PageBase
{

    private ReportDocument rd;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (CheckValue(Request["type"]) && CheckValue(Request["trungtam"]) 
                && CheckValue(Request["tu"]) && CheckValue(Request["den"]))
        {
            string sDuongDanBC = "" ;

            switch (Request["type"].ToString())
            {
                case "1":
                    {
                        sDuongDanBC = Server.MapPath(@"..\Report\BaoCaoTuan1.rpt");
                        rd = LayDuLieu(sDuongDanBC, Request["trungtam"].ToString(), Request["tu"].ToString(), Request["den"].ToString());
                        break;
                    }
                case "2":
                    {
                        sDuongDanBC = Server.MapPath(@"..\Report\BaoCaoTuan2.rpt");
                        rd = LayDuLieu(sDuongDanBC, Request["trungtam"].ToString(), Request["tu"].ToString(), Request["den"].ToString());
                        break;
                    }
                case "3":
                    {
                        sDuongDanBC = Server.MapPath(@"..\Report\BaoCaoTuan.rpt");
                        rd = LayDuLieu(sDuongDanBC, Request["trungtam"].ToString(), Request["tu"].ToString(), Request["den"].ToString());
                        break;
                    }
            }

            this.HienBaoCao.ReportSource = rd;
            this.HienBaoCao.DataBind();
        }
    }

    private string LayMaTrungTam()
    {
        UserInfo _mUserInfo = Session["User"] as UserInfo;
        string sUserID = _mUserInfo.UserID.ToString();
        if (sUserID.Substring(0, 4).ToUpper() == "TTCN")
            return "TTCN";
        else if (sUserID.Substring(0, 3).ToUpper() == "TT2")
            return "TT2";
        else if (sUserID.Substring(0, 3).ToUpper() == "TT3")
            return "TT3";
        return "";
    }

    private string LayTenTrungTam()
    {
        if (LayMaTrungTam() == "TTCN")
            return "Trung tâm chứng nhận";
        else if (LayMaTrungTam() == "TT2")
            return "Trung tâm 2";
        else if (LayMaTrungTam() == "TT3")
            return "Trung tâm 3";
        return "";
    }

    public ReportDocument LayDuLieu(string sDuongDanBC, string sTrungTam, string sTuTuan, string sDenTuan)
    {
        ReportDocument rd = new ReportDocument();

        //rd.Load(sDuongDanBC);
        //DataTable dtDuLieu = ProviderFactory.SanPhamProvider.GetDuLieuBaoCaoTuanQuy(sTrungTam, Convert.ToDateTime(sTuTuan).ToString("MM/dd/yyyy"), Convert.ToDateTime(sDenTuan).ToString("MM/dd/yyyy"));

        //rd.SetDataSource(dtDuLieu);

        //string[] arrTrungTam = sTrungTam.Split(",".ToCharArray());
        //string sTTCN = "";
        //string sTT2 = "";
        //string sTT3 = "";
        //for (int i = 0; i < arrTrungTam.Length; i++)
        //{
        //    if (arrTrungTam[i].ToString().ToUpper() == "TTCN")
        //        sTTCN = "TTCN";
        //    else if (arrTrungTam[i].ToString().ToUpper() == "TT2")
        //        sTT2 = "TT2";
        //    else if (arrTrungTam[i].ToString().ToUpper() == "TT3")
        //        sTT3 = "TT3";
        //}

        //rd.ParameterFields["TenTrungTam"].CurrentValues.AddValue(LayTenTrungTam());
        //if(sTTCN != "")
        //    rd.ParameterFields["TTCN"].CurrentValues.AddValue(sTTCN);
        //else
        //    rd.ParameterFields["TTCN"].CurrentValues.AddValue("");
        //if (sTT2 != "")
        //    rd.ParameterFields["TT2"].CurrentValues.AddValue(sTT2);
        //else
        //    rd.ParameterFields["TT2"].CurrentValues.AddValue("");
        //if (sTT3 != "")
        //    rd.ParameterFields["TT3"].CurrentValues.AddValue(sTT3);
        //else
        //    rd.ParameterFields["TT3"].CurrentValues.AddValue("");

        //rd.ParameterFields["GiamDoc"].CurrentValues.AddValue("");
        //rd.ParameterFields["TuTuan"].CurrentValues.AddValue(sTuTuan);
        //rd.ParameterFields["DenTuan"].CurrentValues.AddValue(sDenTuan);
        
        return rd;
    }

    

    private bool CheckValue(object obj)
    {
        if(obj == null)
            return false;
        if(obj.ToString()=="")
            return false;
        return true;
    }

    protected void HienBaoCao_Unload(object sender, EventArgs e)
    {
        if (rd != null)
        {
            rd.Close();
            rd.Dispose();
        }
    }
}
