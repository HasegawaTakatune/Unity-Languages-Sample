using System;
using System.IO;
using UnityEngine;

public class JsonStream
{
    /// <summary>
    /// ファイルに設定データを保存する
    /// </summary>
    /// <param name="fileName">ファイル名</param>
    /// <param name="data">保存するテキスト</param>
    public static void SaveText(string fileName, string json)
    {
        try
        {
            string combinedPath = Path.Combine(GetInternalStoragePath(), fileName);
            using (StreamWriter streamWriter = new StreamWriter(combinedPath))
            {
                streamWriter?.WriteLine(json);
            }
        }
        catch (Exception e)
        {
            throw e;
        }

    }

    /// <summary>
    /// ファイルから設定データを取得する
    /// 指定した型定義にJsonデータを変換してから値を返す
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static T GetText<T>(string fileName)
    {
        try
        {
            string combinedPath = Path.Combine(GetInternalStoragePath(), fileName);
            using (StreamReader streamReader = new StreamReader(combinedPath))
            {
                string json = streamReader?.ReadLine();
                return JsonUtility.FromJson<T>(json);
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    ///  ファイルパスを生成する（Android対応用）
    /// </summary>
    /// <returns>ファイルパス</returns>
    private static string GetInternalStoragePath()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        using (var getFilesDir = currentActivity.Call<AndroidJavaObject>("getFilesDir"))
        {
            string secureDataPath = getFilesDir.Call<string>("getCanonicalPath");
            return secureDataPath;
        }
#else
        return Application.persistentDataPath;
#endif
    }
}
