using System;
using System.Reflection;

using JsonStream;

/// <summary>
/// 多言語対応の単語を格納するクラス
/// </summary>
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

/// <summary>
/// 多言語対応のメッセージを格納するクラス
/// </summary>
[System.Serializable]
public class Messages
{
    public string helloUser;
    public string required;
}

/// <summary>
/// 多言語対応の値を格納するクラス
/// </summary>
[System.Serializable]
public class LanguagesJson
{
    public Words words;
    public Messages messages;
}

/// <summary>
/// ロケーション（地域）
/// </summary>
public enum Location
{
    ENGLISH,
    JAPAN,
    LENGTH
}

/// <summary>
/// 多言語対応
/// </summary>
public class Languages
{
    private static Disc disc = Disc.STREAMING_ASSETS;
    private static readonly string LANGUAGE_EN_JSON_FILE_NAME = "location-en.json";
    private static readonly string LANGUAGE_JA_JSON_FILE_NAME = "location-ja.json";

    public static Location location = Location.ENGLISH;
    private static LanguagesJson languageJson = null;

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="local">言語地域（Location）</param>
    /// <param name="dsc">ファイル保存する種別（StreamingAssets・Storage）</param>
    public static void Init(Location local = Location.ENGLISH, Disc dsc = Disc.STREAMING_ASSETS)
    {
        disc = dsc;
        location = local;
        Setup();
    }

    /// <summary>
    /// 言語ロケーションを設定する
    /// </summary>
    /// <param name="local">言語地域（Location）</param>
    public static void SetLocation(Location local)
    {
        location = local;
        Setup();
    }

    /// <summary>
    /// 言語を再設定する
    /// </summary>
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
            languageJson = JsonStream.Stream.GetText<LanguagesJson>(disc, fileName);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// テキスト取得
    /// </summary>
    /// <param name="key">キー（テキストの種類）</param>
    /// <returns>テキスト</returns>
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

    /// <summary>
    /// メッセージ取得
    /// </summary>
    /// <param name="key">キー（メッセージの種類）</param>
    /// <param name="attribute">差し替える文字列</param>
    /// <returns>置換後のメッセージ</returns>
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

    /// <summary>
    /// インスタンスの変数をキー指定で取得する
    /// </summary>
    /// <param name="key">キー（フィールド名）</param>
    /// <param name="instance">キー取得するインスタンス</param>
    /// <returns>フィールド値</returns>
    private static object GetValueByKey(string key, object instance)
    {
        Type type = instance.GetType();
        string[] keys = key.Split('.');
        object currentInstance = instance;

        foreach (string currentKey in keys)
        {
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
