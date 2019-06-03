using SmartGlass;
using SmartGlass.Common;
using SmartGlass.Messaging.Session;
using SmartGlass.Messaging.Session.Messages;
using SmartGlass.Nano;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class SmartGlassManager
{
    static readonly SmartGlassManager sInstance = new SmartGlassManager();
    static public SmartGlassManager instance
    {
        get
        {
            return sInstance;
        }
    }

    public SmartGlassClient client { private set; get; }

    public string GetCurrentClientIP()
    {
        return client == null ? string.Empty : client.Device.Address.ToString();
    }
    public string GetCurrentClientLiveId()
    {
        return client == null ? string.Empty : client.Device.LiveId;
    }

    public bool IsConnectDevice(Device pDevice)
    {
        if (pDevice == null) return false;
        return IsConnectDevice(pDevice.LiveId);
    }
    public bool IsConnectDevice(string pLiveId)
    {
        if (string.IsNullOrEmpty(pLiveId)) return false;
        return GetCurrentClientLiveId() == pLiveId;
    }

    public void Connect(bool pCloseLast = false)
    {
        Connect(DataManager.GetIPAdress(), DataManager.GetLiveId());
    }
    public void Connect(Device pDevice, bool pCloseLast = false)
    {
        if (pDevice == null) return;
        Connect(pDevice.Address.ToString(), pDevice.LiveId);
    }
    public void Connect(string pIPAdress, string pLiveId, bool pCloseLast = false)
    {
        if (pCloseLast)
        {
            Disconnect();
        }

        if (client)
        {
            TipsManager.ShowTips("已存在连接的Xbox，请断开之前的连接再试");
            return;
        }

        Task.Run(() => ConnectAsync(pIPAdress, pLiveId));
    }
    async Task ConnectAsync(string pIPAdress, string pLiveId)
    {
        Debug.LogFormat($"ConnectAsync. ip:{pIPAdress} liveId:{pLiveId}");
        try
        {
            client = await SmartGlassClient.ConnectAsync(pIPAdress);
            TipsManager.ShowTips("连接成功");
        }
        catch (SmartGlassException e)
        {
            TipsManager.ShowTips($"连接失败：{e.Message}");
        }
        catch (TimeoutException)
        {
            TipsManager.ShowTips("连接超时");
        }
    }

    public void Disconnect()
    {
        DisconnectAsync();
    }
    async void DisconnectAsync()
    {
        if (client)
        {
            await client.SendMessage(new DisconnectMessage()
            {
                Reason = DisconnectReason.Unspecified,
                ErrorCode = 0,
            });
            client = null;
        }

        TipsManager.ShowTips("断开连接成功");
    }

    public void SendGamepadState(GamepadState pState)
    {
        SendGamepadStateAsync(pState);
    }
    async void SendGamepadStateAsync(GamepadState pState)
    {
        if (!client)
        {
            TipsManager.ShowTips("请连接后再试");
            return;
        }

        pState.Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        await client.InputChannel.SendGamepadStateAsync(pState);

        pState.Buttons = GamepadButtons.Clear;
        pState.Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        await client.InputChannel.SendGamepadStateAsync(pState);
    }

    public void SendPowerOff()
    {
        PowerOffAsync();
    }
    async void PowerOffAsync()
    {
        await client.PowerOffAsync();
        client = null;
    }

    public void PowerOn(string pLiveId)
    {
        if (string.IsNullOrEmpty(pLiveId))
        {
            return;
        }

        SendPowerOnAsync(pLiveId);
    }
    public void SendPowerOn(Device pDevice, Action pCallBack)
    {
        if (pDevice == null)
        {
            TipsManager.ShowTips("设备数据为空");
            return;
        }
        SendPowerOnAsync(pDevice.LiveId, pCallBack);
    }
    async void SendPowerOnAsync(string pLiveId, Action pCallBack = null)
    {
        Debug.LogFormat($"SendPowerOnAsync. liveId:{pLiveId}");
        TipsManager.ShowTips("正在开机...");

        var tDevice = await Device.PowerOnAsync(pLiveId);
        LogTool.Log($"{tDevice.Name} ({tDevice.HardwareId}) {tDevice.Address}");

        TipsManager.ShowTips("开机成功");

        if (pCallBack != null)
        {
            pCallBack();
        }
    }

    public void GetXboxConsoles(Action<IEnumerable<Device>> pCallBack)
    {
        GetXboxConsolesAsync(pCallBack);
    }
    async void GetXboxConsolesAsync(Action<IEnumerable<Device>> pCallBack)
    {
        var tDevices = await Device.DiscoverAsync();
        if (pCallBack != null)
        {
            pCallBack(tDevices);
        }
    }

    public void SendMediaState(MediaCommandMessage pMsg)
    {
        if (!client)
        {
            TipsManager.ShowTips("请连接后再试");
            return;
        }

        UpdateMediaStateAsync(pMsg);
    }
    async void UpdateMediaStateAsync(MediaCommandMessage pMsg)
    {
        await client.UpdateMediaState(pMsg);
    }

    public void SendTextInput(SystemTextInputMessage pMsg)
    {
        TextInputAsync(pMsg);
    }
    async void TextInputAsync(SystemTextInputMessage pMsg)
    {
        await client.TextInput(pMsg);
    }
}
