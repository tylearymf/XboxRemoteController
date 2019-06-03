using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

class SettingViewController : ViewController
{
    InputField mLiveIDInput;
    InputField mIPAdressInput;

    internal override void OnInit()
    {
        var tBackBtn = transform.Find("Back").GetComponent<Button>();
        tBackBtn.onClick.AddListener(new UnityAction(OnClick_Back));

        var tConnectBtn = transform.Find("Connect").GetComponent<Button>();
        tConnectBtn.onClick.AddListener(new UnityAction(OnClick_Connect));

        var tPowerOnBtn = transform.Find("PowerOn").GetComponent<Button>();
        tPowerOnBtn.onClick.AddListener(new UnityAction(OnClick_PowerOn));

        mLiveIDInput = transform.Find("LiveGroup/LiveID").GetComponent<InputField>();
        mIPAdressInput = transform.Find("IPGroup/IPAdress").GetComponent<InputField>();

        mLiveIDInput.text = DataManager.GetLiveId();
        mIPAdressInput.text = DataManager.GetIPAdress();
    }

    private void OnClick_PowerOn()
    {
        DataManager.SetLiveId(mLiveIDInput.text);
        DataManager.SetIPAdress(mIPAdressInput.text);
        SmartGlassManager.instance.PowerOn(DataManager.GetLiveId());
    }

    void OnClick_Connect()
    {
        DataManager.SetLiveId(mLiveIDInput.text);
        DataManager.SetIPAdress(mIPAdressInput.text);

        if (SmartGlassManager.instance.IsConnectDevice(DataManager.GetLiveId()))
        {
            TipsManager.ShowTips("正在断开连接...");
            SmartGlassManager.instance.Disconnect();
        }
        else
        {
            TipsManager.ShowTips("正在尝试连接...");
            SmartGlassManager.instance.Connect();
        }
    }

    void OnClick_Back()
    {
        UIManager.CloseView(this);
    }
}
