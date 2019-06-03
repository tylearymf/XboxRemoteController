using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

class ShowXboxViewController : ViewController
{
    RectTransform mContent;

    internal override void OnInit()
    {
        var tBackBtn = transform.Find("Back").GetComponent<Button>();
        tBackBtn.onClick.AddListener(new UnityAction(OnClick_Back));

        mContent = transform.Find("Scroll View/Viewport/Content").GetComponent<RectTransform>();

        InitItem();
    }

    void InitItem()
    {
        SmartGlassManager.instance.GetXboxConsoles(pDevices =>
        {
            var tList = pDevices.ToList();
            TipsManager.ShowTips($"Show Devices Count：{tList.Count}");
            foreach (var item in tList)
            {
                var tCtrl = UIManager.AddItem<XboxItemController>(mContent);
                tCtrl.SetData(item);
            }
        });
    }

    void OnClick_Back()
    {
        UIManager.CloseView(this);
    }
}
