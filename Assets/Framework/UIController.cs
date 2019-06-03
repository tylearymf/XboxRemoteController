using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIController : IInitView
{
    public Transform transform { private set; get; }
    public GameObject gameObject { private set; get; }

    void IInitView.InitView(GameObject pGameObject)
    {
        gameObject = pGameObject;
        transform = gameObject == null ? null : gameObject.transform;
    }

    internal virtual void OnInit() { }

    internal virtual void OnDestroy() { }

    internal void SetActive(bool pActive)
    {
        gameObject.SetActive(pActive);
    }


    static public implicit operator bool(UIController pCtrl)
    {
        return pCtrl != null;
    }
}
