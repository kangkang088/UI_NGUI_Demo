using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterPanel : BasePanel<RegisterPanel>
{
    public UIButton btnCloseLogin;
    public UIButton btnSureLogin;
    public UIInput inputUserName;
    public UIInput inputPassword;
    public override void Init()
    {
        HideMe();
        //注册
        btnSureLogin.onClick.Add(new EventDelegate(() => 
        {
            //判断合理
            if (inputUserName.value.Length < 6 || inputPassword.value.Length < 6)
            {
                TipPanel.Instance.ShowMe();
                TipPanel.Instance.ChangeInfo("账号或密码的长度必须大于等于六位");
                return;
            }
            //注册成功do..
            if (LoginMgr.Instance.RegisterUser(inputUserName.value, inputPassword.value))
            {
                //显示登录面板，隐藏注册面板
                LoginPanel.Instance.ShowMe();
                //希望登录面板的用户名和密码和注册时一样，方便操作
                LoginPanel.Instance.SetInfo(inputUserName.value, inputPassword.value);
                //注册完后，清空之前记录的别人的服务器选择id
                LoginMgr.Instance.ClearLoginData();
                HideMe();
            }
            else//注册失败do..
            {
                //提示
                TipPanel.Instance.ChangeInfo("用户名已存在");
                TipPanel.Instance.ShowMe();
            }
            
        }));
        //取消注册
        btnCloseLogin.onClick.Add(new EventDelegate(() =>
        {
            HideMe();
            LoginPanel.Instance.ShowMe();
        }));
    }
    public override void ShowMe()
    {
        base.ShowMe();
        inputUserName.value = "";
        inputPassword.value = "";
    }
}
