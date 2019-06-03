using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

class AboutViewController : ViewController
{
    const string cUrl = "https://github.com/tylearymf/XboxRemoteController";

    internal override void OnInit()
    {
        var tText = transform.Find("Text").GetComponent<Text>();
        transform.Find("Back").GetComponent<Button>().onClick.AddListener(new UnityAction(OnClick_Back));
        transform.Find("Jump").GetComponent<Button>().onClick.AddListener(new UnityAction(OnClick_Jump));

        tText.text = string.Format($"<b>source:</b>\n<i>{cUrl}</i>");
    }

    void OnClick_Back()
    {
        UIManager.CloseView(this);
    }

    void OnClick_Jump()
    {
        Application.OpenURL(cUrl);
    }
}
