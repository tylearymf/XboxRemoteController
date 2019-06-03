using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

class TipsViewController : ViewController
{
    RectTransform mAnchor;
    Image mImage;
    Text mText;

    internal override void OnInit()
    {
        mAnchor = transform.Find("Anchor").GetComponent<RectTransform>();
        mImage = transform.Find("Anchor").GetComponent<Image>();
        mText = transform.Find("Anchor/Text").GetComponent<Text>();
    }

    public void ShowTips(string pMessage)
    {
        mText.text = pMessage;
        DOTween.To(() => mImage.color, x => mImage.color = x, new Color(1, 1, 1, 0), 2F);
        Utils.DOMove(mAnchor, 2F, new Vector2(0, 600), new Vector2(0, 200)).OnComplete(() =>
        {
            UIManager.DestroyView(this);
        });
    }
}
