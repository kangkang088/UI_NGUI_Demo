using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Xml.Serialization;

public class XmlDataMgr  {
    private static XmlDataMgr instance = new XmlDataMgr();
    public static XmlDataMgr Instance => instance;
    private XmlDataMgr() {
    
    }
    /// <summary>
    /// 保存数据到文件中
    /// </summary>
    /// <param name="data">数据对象</param>
    /// <param name="fileName">文件名</param>
    public void SaveData(object data, string fileName) {
        //1.save path
        string path = Application.persistentDataPath + "/" + fileName + ".xml";
        //2.save file
        using (StreamWriter reader = new StreamWriter(path)) {
            //3.serialize
            XmlSerializer s = new XmlSerializer(data.GetType());
            s.Serialize(reader, data);
        }

    }
    /// <summary>
    /// 从文件中读取数据到对象
    /// </summary>
    /// <param name="type">对象类型</param>
    /// <param name="fileName">文件名</param>
    /// <returns></returns>
    public object LoadData(Type type, string fileName) {
        string path = Application.persistentDataPath + "/" + fileName + ".xml";
        if (!File.Exists(path)) {
            path = Application.streamingAssetsPath + "/" + fileName + ".xml";
            if (!File.Exists(path)) {
                return Activator.CreateInstance(type); 
            }
        }
        using (StreamReader reader = new StreamReader(path)) {
            XmlSerializer s = new XmlSerializer(type);
            return s.Deserialize(reader);
        }
    }
}
