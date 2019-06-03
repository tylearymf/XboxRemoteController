using DG.Tweening;
using SmartGlass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

static class Utils
{
    static public bool TryEnumParse<T1, T2>(T1 pButtonState, out T2 pGamepadState) where T1 : struct where T2 : struct
    {
        var tName = pButtonState.ToString();
        return Enum.TryParse(tName, out pGamepadState);
    }

    static public void ResetTransform(this GameObject pGo)
    {
        if (!pGo) return;
        var tTrans = pGo.GetComponent<RectTransform>();
        tTrans.ResetTransform();
    }
    static public void ResetTransform(this GameObject pGo, Transform pParent)
    {
        if (!pGo) return;
        var tTrans = pGo.GetComponent<RectTransform>();
        if (!tTrans) return;
        tTrans.SetParent(pParent);
        tTrans.ResetTransform();
    }
    static public void ResetTransform(this RectTransform pTrans)
    {
        if (!pTrans) return;

        pTrans.anchorMin = Vector2.zero;
        pTrans.anchorMax = Vector2.one;
        pTrans.pivot = Vector2.one * 0.5F;
        pTrans.anchoredPosition3D = Vector3.zero;
        pTrans.sizeDelta = Vector2.zero;
        pTrans.localEulerAngles = Vector3.zero;
        pTrans.localScale = Vector3.one;
    }

    static public Tween DOMove(RectTransform pAnchor, float pDuration, Vector2 pToPos, Vector2? pPos = null)
    {
        if (!pAnchor) return null;

        DOTween.Kill(pAnchor);
        var tPos = pAnchor.anchoredPosition;
        if (pPos.HasValue)
        {
            tPos = pPos.Value;
            pAnchor.anchoredPosition = tPos;
        }
        return DOTween.To(() => tPos, x => tPos = x, pToPos, pDuration).OnUpdate(() =>
        {
            pAnchor.anchoredPosition = tPos;
        }).SetEase(Ease.OutCirc).SetId(pAnchor);
    }
}

