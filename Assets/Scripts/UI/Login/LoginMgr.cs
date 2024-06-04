using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginMgr
{
    private static LoginMgr instance = new LoginMgr();
    public static LoginMgr Instance => instance;
    private LoginMgr()
    {
        loginData = XmlDataMgr.Instance.LoadData(typeof(LoginData), "LoginData") as LoginData;
        registerData = XmlDataMgr.Instance.LoadData(typeof(RegisterData), "RegisterData") as RegisterData;
        serverInfo = XmlDataMgr.Instance.LoadData(typeof(ServerInfo), "ServerInfo") as ServerInfo;
    }
    //登录数据
    private LoginData loginData;
    public LoginData LoginData => loginData;
    public void SaveLoginData()
    {
        XmlDataMgr.Instance.SaveData(loginData, "LoginData");
    }
    //注册数据
    private RegisterData registerData;
    public RegisterData RegisterData => registerData;
    public void SaveRegisterData()
    {
        XmlDataMgr.Instance.SaveData(registerData, "RegisterData");
    }
    //注册成功后，把上一次别人登录的id清空
    public void ClearLoginData()
    {
        loginData.frontServerID = 0;
    }
    //返回值代表是否注册成功
    public bool RegisterUser(string userName, string password)
    {
        if (registerData.registerInfo.ContainsKey(userName))
        {
            return false;
        }
        else
        {
            registerData.registerInfo.Add(userName, password);
            SaveRegisterData();
            return true;
        }
    }
    //检测用户名和密码是否合法
    public bool CheckInfo(string userName, string password)
    {
        if (registerData.registerInfo.ContainsKey(userName))
        {
            if (registerData.registerInfo[userName] == password)
            {
                return true;
            }
        }
        return false;
    }
    //服务器数据，方便外部获取
    private ServerInfo serverInfo;
    public ServerInfo ServerInfo => serverInfo;
}
