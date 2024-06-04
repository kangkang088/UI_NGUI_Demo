using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

//服务器数据
public class ServerInfo
{
    //key-服务器id,value-服务器详细信息
    public SerializerDictionary<int, Server> serverDic = new SerializerDictionary<int, Server>();
}
//单个服务器信息
public class Server
{
    [XmlAttribute]
    public int id;//服务器id
    [XmlAttribute]
    public string name;//服务器名字
    [XmlAttribute]
    public int state;//服务器状态
    [XmlAttribute]
    public bool isNew;//是否是新服
}

