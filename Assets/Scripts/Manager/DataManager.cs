using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class DataManager
{
    const string cLiveIdName = "LiveID";
    const string cIPAdressName = "IPAdress";

    static public void SetLiveId(string pLiveId)
    {
        PlayerPrefs.SetString(cLiveIdName, pLiveId);
    }
    static public string GetLiveId()
    {
        return PlayerPrefs.GetString(cLiveIdName);
    }

    static public void SetIPAdress(string pIPAdress)
    {
        PlayerPrefs.SetString(cIPAdressName, pIPAdress);
    }
    static public string GetIPAdress()
    {
        return PlayerPrefs.GetString(cIPAdressName);
    }
}
