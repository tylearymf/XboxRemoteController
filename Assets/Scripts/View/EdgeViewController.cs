using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EdgeViewController : ViewController
{
    RectTransform mAnchor;

    internal override void OnInit()
    {
        mAnchor = transform.Find("Anchor").GetComponent<RectTransform>();
        var tMaskBtn = transform.Find("Mask").GetComponent<Button>();
        tMaskBtn.onClick.AddListener(new UnityAction(OnClick_Mask));

        var tBtnGrid = transform.Find("Anchor/BtnGrid");
        var tShowXboxsBtn = tBtnGrid.Find("ShowXboxs").GetComponent<Button>();
        tShowXboxsBtn.onClick.AddListener(new UnityAction(OnClick_ShowXboxs));
        var tSettingBtn = tBtnGrid.Find("Setting").GetComponent<Button>();
        tSettingBtn.onClick.AddListener(new UnityAction(OnClick_Setting));
        var tAboutBtn = tBtnGrid.Find("About").GetComponent<Button>();
        tAboutBtn.onClick.AddListener(new UnityAction(OnClick_Abount));

        Utils.DOMove(mAnchor, 0.2F, Vector2.zero, new Vector2(600, 0));
    }

    internal override void OnDestroy()
    {
    }

    internal void Destroy()
    {
        Utils.DOMove(mAnchor, 0.2F, new Vector2(600, 0)).OnComplete(() =>
        {
            UIManager.CloseView(this);
        });
    }

    Tween DOMove(float pToPosX, float? pPosX = null)
    {
        mAnchor.DOKill();
        var tPosX = mAnchor.anchoredPosition.x;
        if (pPosX.HasValue)
        {
            tPosX = pPosX.Value;
            mAnchor.anchoredPosition = new Vector2(tPosX, 0);
        }
        return DOTween.To(() => tPosX, x => tPosX = x, pToPosX, 0.2F).OnUpdate(() =>
         {
             mAnchor.anchoredPosition = new Vector2(tPosX, 0);
         }).SetEase(Ease.OutCirc);
    }

    void OnClick_Mask()
    {
        Destroy();
    }

    void OnClick_ShowXboxs()
    {
        UIManager.OpenView<ShowXboxViewController>();
    }

    void OnClick_Setting()
    {
        UIManager.OpenView<SettingViewController>();
    }

    void OnClick_Abount()
    {
        UIManager.OpenView<AboutViewController>();
    }
}
