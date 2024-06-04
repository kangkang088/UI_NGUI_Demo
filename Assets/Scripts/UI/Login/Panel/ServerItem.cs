using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerItem : MonoBehaviour
{
    //按钮本身
    public UIButton btn;
    public UILabel labelInfo;
    private int beginIndex;
    private int endIndex;
    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.Add(new EventDelegate(() => 
        {
            //点击按钮后，do...
            //通知 切换服务器面板 更新右侧按钮内容
            ChooseServerPanel.Instance.UpdatePanel(beginIndex, endIndex);
        }));
    }
    //外部动态创建时，用于初始化。告诉服务区的范围
    public void InitInfo(int beginIndex, int endIndex)
    {
        //记录按钮代表的区间，用于之后动态创建单个服务器内容
        this.beginIndex = beginIndex;
        this.endIndex = endIndex;
        labelInfo.text = beginIndex + " - " + endIndex + "区";
    }
}
