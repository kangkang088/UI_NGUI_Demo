using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerPanel : BasePanel<ServerPanel>
{
    //服务器信息
    public UILabel labelInfo;
    //选区按钮
    public UIButton btnChangeArea;
    //开始游戏按钮
    public UIButton btnBegin;
    //返回按钮
    public UIButton btnBack;
    public override void Init()
    {
        //ServerInfo info = XmlDataMgr.Instance.LoadData(typeof(ServerInfo), "ServerInfo") as ServerInfo;
        HideMe();
        btnChangeArea.onClick.Add(new EventDelegate(() =>
        {
            HideMe();
            //打开服务器选择面板
            ChooseServerPanel.Instance.ShowMe();
        }));
        btnBegin.onClick.Add(new EventDelegate(() =>
        {
            //真正进入游戏，再存储一次登录数据。
            LoginMgr.Instance.SaveLoginData();
            //游戏场景
            SceneManager.LoadScene("GameScene");
        }));
        btnBack.onClick.Add(new EventDelegate(() =>
        {
            //返回登录面板
            LoginPanel.Instance.ShowMe();
            HideMe();
        }));
    }
    public override void ShowMe()
    {
        base.ShowMe();
        //更新label显示的服务器信息
        //根据玩家上一次选择的服务器id，得到数据，进行更新
        int id = LoginMgr.Instance.LoginData.frontServerID;
        Server info = LoginMgr.Instance.ServerInfo.serverDic[id];
        labelInfo.text = info.name;
    }
}
