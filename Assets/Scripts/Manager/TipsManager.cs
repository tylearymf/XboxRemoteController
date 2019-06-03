using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TipsManager
{
    static public void Init()
    {
    }

    static public void ShowTips(string pMessage)
    {
        var tTipsCtrl = UIManager.OpenView<TipsViewController>(false);
        tTipsCtrl.ShowTips(pMessage);
    }
}
