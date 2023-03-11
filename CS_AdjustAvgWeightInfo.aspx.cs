using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class CS_AdjustAvgWeightInfo : System.Web.UI.Page
{
    ConnectDB oCon = new ConnectDB();
    AvgWeightInfo oAvgWeight = new AvgWeightInfo();
    List<AvgWeightInfo> listAvgWeight = new List<AvgWeightInfo>();
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {        
        txtSN.Attributes.Clear();
        txtSN.Enabled = true;
        txtSN.Attributes.Add("class", "FontSetting");
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "Select Top 20 * From Weight_Sent Where QCI_PN_1 LIKE @SN Order by BoxQty ASC";
        cmd.Parameters.Add(new SqlParameter("@SN", "%"+txtSN.Text.Trim()+"%"));        
        cmd.CommandTimeout = 180;
        dt = oCon.Query(cmd);

        if (dt.Rows.Count > 0)
        {
            //--Get data in repeter--
            listAvgWeight = new List<AvgWeightInfo>();
            foreach (DataRow row in dt.Rows)
            {
                oAvgWeight = new AvgWeightInfo();
                oAvgWeight.StrBoxQty = row["BoxQty"].ToString().Trim();
                oAvgWeight.StrFlage = row["Flag"].ToString().Trim();
                oAvgWeight.StrPN = row["QCI_PN_1"].ToString().Trim();
                oAvgWeight.StrTime = row["LastUpdTime"].ToString().Trim();
                oAvgWeight.StrUnit = row["Unit"].ToString().Trim();
                oAvgWeight.StrWeight = row["Weight"].ToString().Trim();
                listAvgWeight.Add(oAvgWeight);
            }            

            rptResult.DataSource = listAvgWeight;
            rptResult.DataBind();
        }
    }

    protected void rptResult_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //--Set find control--

            RadioButton rdoCheck = e.Item.FindControl("rdoCheck") as RadioButton;
            Label lbPN = e.Item.FindControl("lbPN") as Label;
            Label lbWeight = e.Item.FindControl("lbWeight") as Label;
            Label lbUnit = e.Item.FindControl("lbUnit") as Label;
            Label lbLastUpdate = e.Item.FindControl("lbLastUpdate") as Label;
            Label lbFlage = e.Item.FindControl("lbFlage") as Label;
            Label lbBoxQty = e.Item.FindControl("lbBoxQty") as Label;

            //--Set Model--
            AvgWeightInfo oAvgWeight = e.Item.DataItem as AvgWeightInfo;
            lbPN.Text = oAvgWeight.StrPN.ToString().Trim();
            lbWeight.Text = oAvgWeight.StrWeight.ToString().Trim();
            lbUnit.Text = oAvgWeight.StrUnit.ToString().Trim();
            lbLastUpdate.Text = oAvgWeight.StrTime.ToString().Trim();
            lbFlage.Text = oAvgWeight.StrFlage.ToString().Trim();
            lbBoxQty.Text = oAvgWeight.StrBoxQty.ToString().Trim();

            rdoCheck.Attributes.Add("onclick", "setExclusiveRadioButton('GroupName',this)");
            //rdoCheck.Attributes.Add("onclick", "SetUniqueRadioButton('MyGroupName')");
        }
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        //txtSN.Enabled = false;
        txtSN.Attributes.Clear();        
        txtSN.Attributes.Add("disabled", "true");
        txtSN.Attributes.Add("class", "FontSetting");
        int _Chk = 0;
        foreach (RepeaterItem item in rptResult.Items)
        {
            //var radioBtn = (HtmlInputRadioButton)item.FindControl("rdoCheck");
            RadioButton rdoCheck = item.FindControl("rdoCheck") as RadioButton;
            Label lbPN = item.FindControl("lbPN") as Label;
            Label lbWeight = item.FindControl("lbWeight") as Label;
            Label lbUnit = item.FindControl("lbUnit") as Label;
            Label lbLastUpdate = item.FindControl("lbLastUpdate") as Label;
            Label lbFlage = item.FindControl("lbFlage") as Label;
            Label lbBoxQty = item.FindControl("lbBoxQty") as Label;

            if (rdoCheck.Checked)
            {
                //--Set in Model--
                oAvgWeight = new AvgWeightInfo();
                oAvgWeight.StrBoxQty = lbBoxQty.Text.Trim();
                oAvgWeight.StrFlage = lbFlage.Text.Trim();
                oAvgWeight.StrPN = lbPN.Text.Trim();
                oAvgWeight.StrTime = lbLastUpdate.Text.Trim();
                oAvgWeight.StrUnit = lbUnit.Text.Trim();
                oAvgWeight.StrWeight = lbWeight.Text.Trim();
                listAvgWeight = new List<AvgWeightInfo>();
                listAvgWeight.Add(oAvgWeight);
                ViewState["listAvgWeight"] = listAvgWeight;

                txtSN.Text = lbPN.Text.Trim();
                txtAvgWeight.Text = lbWeight.Text.Trim();
                _Chk = 1;
                break;
            }
        }

        if (_Chk == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "ERROR", "alert('Please select row for adjust average weight.');", true);
            btnQuery_Click(sender, e);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //--Update data--
        if (ViewState["listAvgWeight"] != null)
        {
            listAvgWeight = ViewState["listAvgWeight"] as List<AvgWeightInfo>;
        }

        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"Update Weight_Sent set Weight = @WeightNew, Flag = @FlagNew, TransDateTime = Format(GETDATE(), 'yyyyMMddHHmmss'), LastUpdTime = Format(GETDATE(), 'yyyyMMddHHmmss') Where QCI_PN_1 = @PN 
                              AND BoxQty = @BoxQty";
            cmd.Parameters.Add(new SqlParameter("@WeightNew", Math.Round((float)Convert.ToDouble(txtAvgWeight.Text.Trim()), 2)));
            cmd.Parameters.Add(new SqlParameter("@FlagNew", "N"));
            cmd.Parameters.Add(new SqlParameter("@PN", listAvgWeight[0].StrPN.ToString().Trim()));
            cmd.Parameters.Add(new SqlParameter("@BoxQty", Convert.ToInt32(listAvgWeight[0].StrBoxQty.ToString().Trim())));
            cmd.CommandTimeout = 180;
            oCon.ExecuteCommand(cmd);

            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = "EXEC usp_Weight_ForShip_BoxWeight";
            cmd1.CommandTimeout = 180;
            oCon.ExecuteCommand(cmd1);
            
            txtSN.Attributes.Clear();
            txtSN.Enabled = true;
            txtSN.Attributes.Add("class", "FontSetting");           
            ClientScript.RegisterStartupScript(this.GetType(), "Information", "alert('Update successful.');", true);
            btnQuery_Click(sender, e);
        }
        catch (Exception ex)
        {
            txtSN.Attributes.Clear();
            txtSN.Enabled = true;
            txtSN.Attributes.Add("class", "FontSetting");
            ClientScript.RegisterStartupScript(this.GetType(), "ERROR", "alert('"+ex.Message+"');", true);
            btnQuery_Click(sender, e);
        }


    }
}