using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//选服面板
public class ChooseServerPanel : BasePanel<ChooseServerPanel>
{
    //左滚动视图
    public Transform svLeft;
    //右滚动视图
    public Transform svRight;
    public UILabel labelName;
    public UISprite spriteState;
    //当前选择的服务器区间名字
    public UILabel labelNowServer;
    //记录之前显示的 单个服务器按钮们
    private List<GameObject> itemList = new List<GameObject>();
    public override void Init()
    {
        //左侧按钮服务器数据 进行游戏时 是不会变化的，所以初始化一次即可
        //动态创建左侧按钮
        ServerInfo info = LoginMgr.Instance.ServerInfo;
        //按五区一个间隔,得到要创建的按钮数
        int num = info.serverDic.Count / 5 + 1;
        for (int i = 0; i < num; i++)
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("UI/btnServer"));
            obj.transform.SetParent(svLeft, false);
            obj.transform.localPosition = new Vector3(-72, 47, 0) + new Vector3(0, -63 * i, 0);
            ServerItem serverItem = obj.GetComponent<ServerItem>();
            int beginIndex = 5 * i + 1;
            int endIndex = 5 * (i + 1);
            //判断最大是不是超过了服务器总数
            if (endIndex > info.serverDic.Count)
                endIndex = info.serverDic.Count;
            serverItem.InitInfo(beginIndex, endIndex);
        }
        HideMe();   
    }
    //提供方法给外部，用于更新面板右侧的按钮显示内容
    public void UpdatePanel(int beginIndex, int endIndex)
    {
        //更新当前选择的服务器区间显示
        labelNowServer.text = "服务器" + beginIndex + "—" + endIndex + "区";
        //创建新按钮之前，删除老的。存新的
        for (int i = 0; i < itemList.Count; i++)
        {
            Destroy(itemList[i].gameObject);
        }
        itemList.Clear();
        //创建新的
        Server nowInfo;
        for (int i = beginIndex; i <= endIndex; i++)
        {
            //获取单个服务器数据
            nowInfo = LoginMgr.Instance.ServerInfo.serverDic[i];
            //动态创建按钮，安排位置
            GameObject serverItem = Instantiate(Resources.Load<GameObject>("UI/btnChooseServer"));
            serverItem.transform.SetParent(svRight,false);
            serverItem.transform.localPosition = new Vector3(-12, 74, 0) + new Vector3((i - 1) % 5 % 2 * 300, (i - 1) % 5 / 2 * -80, 0);
            //得到脚本，更新信息
            ServerChooseItem serverChooseItem = serverItem.GetComponent<ServerChooseItem>();
            serverChooseItem.InitInfo(nowInfo);
            //添加到记录列表中。方便删除
            itemList.Add(serverItem);
        }
    }
    public override void ShowMe()
    {
        base.ShowMe();
        //初始化面板显示
        //1.初始化上一次服务器相关内容显示
        if (LoginMgr.Instance.LoginData.frontServerID == 0)
        {
            //玩家从来没有选择过服务器
            labelName.text = "无";
            spriteState.gameObject.SetActive(false);
        }
        else
        {
            //玩家选了
            Server info = LoginMgr.Instance.ServerInfo.serverDic[LoginMgr.Instance.LoginData.frontServerID];
            labelName.text = info.id + "区   " + info.name;
            spriteState.gameObject.SetActive(true);
            //根据状态，改变图片
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
        //2.更新右侧按钮默认的显示
        //最大索引要判断
        UpdatePanel(1, 5 > LoginMgr.Instance.ServerInfo.serverDic.Count ? LoginMgr.Instance.ServerInfo.serverDic.Count : 5);
    }
}
