using System;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class Words
{
    public string start;
    public string config;
    public string ok;
    public string cancel;
    public string rule;
    public string explanation;
}

[System.Serializable]
public class Messages
{
    public string helloUser;
    public string required;
}

[System.Serializable]
public class LanguagesJson
{
    public Words words;
    public Messages messages;
}

public enum Location
{
    ENGLISH,
    JAPAN,
    LENGTH
}

public class Languages
{
    private static Disc disc = Disc.STREAMING_ASSETS;
    private static readonly string LANGUAGE_EN_JSON_FILE_NAME = "location-en.json";
    private static readonly string LANGUAGE_JA_JSON_FILE_NAME = "location-ja.json";

    public static Location location = Location.ENGLISH;
    private static LanguagesJson languageJson = null;

    public static void Init(Location local = Location.ENGLISH, Disc dsc = Disc.STREAMING_ASSETS)
    {
        disc = dsc;
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
            languageJson = JsonStream.GetText<LanguagesJson>(disc, fileName);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public static string GetTextByKey(string key)
    {
        object value = null;
        try
        {
            value = GetValueByKey(string.Format("words.{0}", key), languageJson);
        }
        catch (Exception e)
        {
            throw e;
        }

        if (value == null) return null;
        return value.ToString();
    }

    public static string GetMessageByKey(string key, string attribute)
    {
        object value = null;
        try
        {
            value = GetValueByKey(string.Format("messages.{0}", key), languageJson);
        }
        catch (Exception e)
        {
            throw e;
        }

        if (value == null) return null;

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
            Debug.Log(string.Format("current key >> {0}", currentKey));
            FieldInfo property = null;
            try
            {
                property = type.GetField(currentKey);
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
                    type = property.FieldType;
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
