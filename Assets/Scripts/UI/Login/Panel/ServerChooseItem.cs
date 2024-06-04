using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerChooseItem : MonoBehaviour
{
    //按钮本身
    public UIButton btnChooseServer;
    //服务器名字
    public UILabel labelName;
    //是否是新服
    public UISprite spriteNew;
    //服务器状态
    public UISprite spriteState;
    //当前这个按钮代表的单个服务器信息，之后，用于传给我们的面板
    private Server nowInfo;
    // Start is called before the first frame update
    void Start()
    {
        btnChooseServer.onClick.Add(new EventDelegate(() =>
        {
            //点击，do...
            //记录当前玩家选择的服务器id
            LoginMgr.Instance.LoginData.frontServerID = nowInfo.id;
            //隐藏选服面板
            ChooseServerPanel.Instance.HideMe();
            //显示服务器面板
            //更新服务器面板
            ServerPanel.Instance.ShowMe();
        }));
    }
    //根据传入的单个服务器数据进行更新
    public void InitInfo(Server info)
    {
        nowInfo = info;
        labelName.text = info.id + "区   " + info.name;
        spriteNew.gameObject.SetActive(info.isNew);
        spriteState.gameObject.SetActive(true);
        switch (info.state)
        {
            case 0:
                spriteState.gameObject.SetActive(false);
                break;
            case 1:
                spriteState.spriteName = "ui_DL_liuchang_01";
                break;
            case 2:
                spriteState.spriteName = "ui_DL_fanhua_01";
                break;
            case 3:
                spriteState.spriteName = "ui_DL_huobao_01";
                break;
            case 4:
                spriteState.spriteName = "ui_DL_weihu_01";
                break;
        }
    }
}
