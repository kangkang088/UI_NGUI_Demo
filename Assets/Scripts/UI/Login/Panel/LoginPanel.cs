using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginPanel : BasePanel<LoginPanel>
{
    public UIInput inputUserName;
    public UIInput inputPassword;
    public UIButton btnRegister;
    public UIButton btnSure;
    public UIToggle toggleRemember;
    public UIToggle toggleAutoLogin;
    public override void Init()
    {
        //点击注册，做啥
        btnRegister.onClick.Add(new EventDelegate(() =>
        {
            //隐藏自己
            HideMe();
            //显示注册面板
            RegisterPanel.Instance.ShowMe();
        }));
        //点击确认，做啥
        btnSure.onClick.Add(new EventDelegate(() =>
        {
            //判断账号密码
            //合法，do...
            if (LoginMgr.Instance.CheckInfo(inputUserName.value, inputPassword.value))
            {
                LoginMgr.Instance.LoginData.userName = inputUserName.value;
                LoginMgr.Instance.LoginData.password = inputPassword.value;
                LoginMgr.Instance.LoginData.autoLogin = toggleAutoLogin.value;
                LoginMgr.Instance.LoginData.rememberPW = toggleRemember.value;
                LoginMgr.Instance.SaveLoginData();

                //显示服务器相关面板
                //没有选择过，那就打开服务器选择面板
                if (LoginMgr.Instance.LoginData.frontServerID == 0)
                    ChooseServerPanel.Instance.ShowMe();
                else //如果之前选择过服务器，直接打开服务器面板
                    ServerPanel.Instance.ShowMe();
                HideMe();
            }
            else//不合法，tipPanel...
            {
                TipPanel.Instance.ShowMe();
                TipPanel.Instance.ChangeInfo("账号或密码错误");
            }


        }));
        //记住密码单选框 状态变化时逻辑变化
        toggleRemember.onChange.Add(new EventDelegate(() =>
        {
            //记录数据
            //记住密码不选，自动登录不能选
            if (!toggleRemember.value)
            {
                toggleAutoLogin.value = false;
            }
        }));
        //自动登录单选框 状态变化时逻辑变化
        toggleAutoLogin.onChange.Add(new EventDelegate(() =>
        {
            //记录数据
            //自动登录的选择，强制使记住密码选中
            if (toggleAutoLogin.value)
            {
                toggleRemember.value = true;
            }
        }));

        #region 处理面板显示的相关信息(根据记录的数据初始化面板)
        LoginData data = LoginMgr.Instance.LoginData;
        //更新面板
        toggleRemember.value = data.rememberPW;
        toggleAutoLogin.value = data.autoLogin;
        if (data.userName != "")
        {
            inputUserName.value = data.userName;
        }
        if (data.rememberPW)
        {
            inputPassword.value = data.password;
        }
        if (data.autoLogin)
        {
            //判断账号密码
            //合法，do...
            if (LoginMgr.Instance.CheckInfo(inputUserName.value, inputPassword.value))
            {
                //显示服务器相关面板
                //没有选择过，那就打开服务器选择面板
                if (LoginMgr.Instance.LoginData.frontServerID == 0)
                    ChooseServerPanel.Instance.ShowMe();
                else //如果之前选择过服务器，直接打开服务器面板
                    ServerPanel.Instance.ShowMe();
                HideMe();
            }
            else//不合法，tipPanel...
            {
                TipPanel.Instance.ShowMe();
                TipPanel.Instance.ChangeInfo("账号或密码错误");
            }
        }
        #endregion
    }
    public void SetInfo(string userName, string password)
    {
        inputUserName.value = userName;
        inputPassword.value = password;
    }
}
