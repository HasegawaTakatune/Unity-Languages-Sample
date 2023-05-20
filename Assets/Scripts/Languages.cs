using System;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class MessagesJson
{
    string helloUser;
    string required;
}

[System.Serializable]
public class LanguagesJson
{
    string start;
    string config;
    string ok;
    string cancel;
    string rule;
    string explanation;

    MessagesJson messages;
}

public enum Location
{
    ENGLISH,
    JAPAN,
    LENGTH
}

public class Languages
{
    private static readonly string LANGUAGE_EN_JSON_FILE_NAME = "location-en.json";
    private static readonly string LANGUAGE_JA_JSON_FILE_NAME = "location-ja.json";

    public static Location location = Location.ENGLISH;
    private static LanguagesJson languageJson = null;

    public static void Init(Location local = Location.ENGLISH)
    {
        location = local;
        Setup();
    }

    public static void SetLocation(Location local)
    {
        location = local;
        Setup();
    }

    private static void Setup()
    {
        string fileName = null;
        switch (location)
        {
            case Location.ENGLISH: fileName = Languages.LANGUAGE_EN_JSON_FILE_NAME; break;
            case Location.JAPAN: fileName = Languages.LANGUAGE_JA_JSON_FILE_NAME; break;
            default: break;
        }
        if (fileName == null) return;

        try
        {
            languageJson = JsonStream.GetText<LanguagesJson>(fileName);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public static string GetTextByKey(string key)
    {
        object value = GetValueByKey(key, languageJson);

        if (value == null) return null;
        return value.ToString();
    }

    public static string GetMessageByKey(string key, string attribute)
    {
        object value = GetValueByKey(string.Format("messages.{0}", key), languageJson);

        if (value == null)
            return null;

        string str = value.ToString();
        return str.Replace(":attribute", attribute);
    }

    private static object GetValueByKey(string key, object instance)
    {
        Type type = instance.GetType();
        string[] keys = key.Split('.');
        object currentInstance = instance;

        foreach (string currentKey in keys)
        {
            PropertyInfo property = null;
            try
            {
                property = type.GetProperty(currentKey);
            }
            catch (Exception e)
            {
                throw e;
            }

            if (property != null)
            {
                try
                {
                    currentInstance = property.GetValue(currentInstance);
                    type = property.PropertyType;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {
                return null;
            }
        }

        return currentInstance;
    }
}
