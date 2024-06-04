using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipPanel : BasePanel<TipPanel>
{
    public UIButton btnSure;
    public UILabel btnTip;
    public override void Init()
    {
        btnSure.onClick.Add(new EventDelegate(() => { HideMe(); }));
        HideMe();
    }
    public void ChangeInfo(string info)
    {
        btnTip.text = info;
    }
}
