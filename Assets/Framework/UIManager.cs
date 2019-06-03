using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class UIManager
{
    static Transform sUIRoot;
    static Dictionary<Type, string> sPrefabNameDic = new Dictionary<Type, string>()
    {
        { typeof(RemoteViewController),"RemoteView" },
        { typeof(ShowXboxViewController),"ShowXboxsView" },
        { typeof(EdgeViewController),"EdgeView" },
        { typeof(SettingViewController),"SettingView" },
        { typeof(XboxItemController),"XboxItem" },
        { typeof(TipsViewController),"TipsView" },
        { typeof(AboutViewController),"AboutView" },
    };

    static HashSet<ViewController> sOpenViewCtrls = new HashSet<ViewController>();

    static public void Init(Transform pRoot)
    {
        sUIRoot = pRoot;
    }

    static public T OpenView<T>(bool pCheckSame = true) where T : ViewController, new()
    {
        return OpenView(typeof(T), pCheckSame) as T;
    }
    static public ViewController OpenView(Type pType, bool pCheckSame = true)
    {
        if (!pType.IsSubclassOf(typeof(ViewController)))
        {
            return null;
        }

        var tCtrl = GetView(pType) as ViewController;
        if (pCheckSame && tCtrl)
        {
            return tCtrl;
        }

        var tPrefabName = sPrefabNameDic[pType];
        var tPrefab = Resources.Load(string.Format("Prefabs/{0}", tPrefabName));
        var tGo = Object.Instantiate(tPrefab) as GameObject;
        tGo.ResetTransform(sUIRoot);
        tCtrl = Activator.CreateInstance(pType) as ViewController;
        (tCtrl as IInitView).InitView(tGo);

        tCtrl.OnInit();
        if (pCheckSame) sOpenViewCtrls.Add(tCtrl);
        return tCtrl;
    }

    static public bool CloseView(ViewController pCtrl)
    {
        if (pCtrl)
        {
            return CloseView(pCtrl.GetType());
        }
        return false;
    }
    static public bool CloseView<T>() where T : ViewController
    {
        return CloseView(typeof(T));
    }
    static public bool CloseView(Type pType)
    {
        var tCtrl = GetView(pType) as ViewController;
        if (tCtrl)
        {
            tCtrl.OnDestroy();
            Object.Destroy(tCtrl.gameObject);
            sOpenViewCtrls.Remove(tCtrl);
            return true;
        }

        return false;
    }
    static public bool DestroyView(ViewController pCtrl)
    {
        if (pCtrl && pCtrl.gameObject)
        {
            Object.Destroy(pCtrl.gameObject);
            return true;
        }

        return false;
    }

    static public T GetView<T>() where T : ViewController
    {
        return GetView(typeof(T)) as T;
    }
    static public ViewController GetView(Type pType)
    {
        foreach (var item in sOpenViewCtrls)
        {
            if (item.GetType() == pType)
            {
                return item;
            }
        }
        return null;
    }

    static public T AddItem<T>(Transform pParent) where T : ItemController, new()
    {
        var tPrefabName = sPrefabNameDic[typeof(T)];
        var tPrefab = Resources.Load(string.Format("Prefabs/{0}", tPrefabName));
        var tGo = Object.Instantiate(tPrefab) as GameObject;
        tGo.ResetTransform(pParent);

        var tCtrl = new T();
        (tCtrl as IInitView).InitView(tGo);
        tCtrl.OnInit();

        return tCtrl;
    }
}
