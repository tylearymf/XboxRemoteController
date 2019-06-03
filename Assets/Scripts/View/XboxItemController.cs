using SmartGlass;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class XboxItemController : ItemController
{
    Device mDevice;
    Text mName;
    Text mIPAdress;
    Text mLiveId;
    Text mPowerText;
    Text mConnectText;


    internal override void OnInit()
    {
        mName = transform.Find("Name").GetComponent<Text>();
        mIPAdress = transform.Find("IPAdress").GetComponent<Text>();
        mLiveId = transform.Find("LiveID").GetComponent<Text>();

        var tPowerBtn = transform.Find("Grid/Power").GetComponent<Button>();
        tPowerBtn.onClick.AddListener(new UnityAction(OnClick_Power));
        mPowerText = transform.Find("Grid/Power/Text").GetComponent<Text>();

        var tConnectBtn = transform.Find("Grid/Connect").GetComponent<Button>();
        tConnectBtn.onClick.AddListener(new UnityAction(OnClick_Connect));
        mConnectText = transform.Find("Grid/Connect/Text").GetComponent<Text>();
    }

    public void SetData(Device pDevice)
    {
        mDevice = pDevice;

        mName.text = string.Format($"Xbox名字：{(mDevice == null ? string.Empty : mDevice.Name)}");
        mIPAdress.text = string.Format($"IP地址：{(mDevice == null ? string.Empty : mDevice.Address.ToString())}");
        mLiveId.text = string.Format($"LiveID：{(mDevice == null ? string.Empty : mDevice.LiveId)}");

        UpdateItem();
    }

    void OnClick_Power()
    {
        if (!SmartGlassManager.instance.IsConnectDevice(mDevice))
        {
            TipsManager.ShowTips("请先连接");
            return;
        }

        SmartGlassManager.instance.SendPowerOff();
    }

    void OnClick_Connect()
    {
        try
        {
            if (SmartGlassManager.instance.IsConnectDevice(mDevice))
            {
                TipsManager.ShowTips("已连接");
            }
            else
            {
                SmartGlassManager.instance.Connect(mDevice, true);
            }
        }
        catch (Exception ex)
        {
            TipsManager.ShowTips(ex.ToString());
        }
    }

    void UpdateItem()
    {
        if (mPowerText == null) return;

        mPowerText.text = mDevice.State == DeviceState.Available ? "关机" : "开机";
        mConnectText.text = SmartGlassManager.instance.IsConnectDevice(mDevice) ? "断开连接" : "连接";
    }
}
