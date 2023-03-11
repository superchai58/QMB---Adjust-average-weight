using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AvgWeightInfo
/// </summary>
/// 

[Serializable]
public class AvgWeightInfo
{
    public AvgWeightInfo()
    {}

    private string strPN = "";
    private string strWeight = "";
    private string strUnit = "";
    private string strTime = "";
    private string strFlage = "";
    private string strBoxQty = "";

    public string StrPN
    {
        get
        {
            return strPN;
        }

        set
        {
            strPN = value;
        }
    }

    public string StrWeight
    {
        get
        {
            return strWeight;
        }

        set
        {
            strWeight = value;
        }
    }

    public string StrUnit
    {
        get
        {
            return strUnit;
        }

        set
        {
            strUnit = value;
        }
    }

    public string StrTime
    {
        get
        {
            return strTime;
        }

        set
        {
            strTime = value;
        }
    }

    public string StrFlage
    {
        get
        {
            return strFlage;
        }

        set
        {
            strFlage = value;
        }
    }

    public string StrBoxQty
    {
        get
        {
            return strBoxQty;
        }

        set
        {
            strBoxQty = value;
        }
    }
}