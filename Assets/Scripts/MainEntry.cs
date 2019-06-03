using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEntry : MonoBehaviour
{
    void Awake()
    {
        var tUIRoot = GameObject.Find("UIRoot").transform;
        DOTween.Init();
        UIManager.Init(tUIRoot);
        TipsManager.Init();

        UIManager.OpenView<RemoteViewController>();
    }

    void OnApplicationQuit()
    {
        SmartGlassManager.instance.Disconnect();
    }
}
