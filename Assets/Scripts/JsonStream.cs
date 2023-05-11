using System;
using System.IO;

public class JsonStream
{
    /// <summary>
    /// ファイルに設定データを保存する
    /// </summary>
    /// <param name="fileName">ファイル名</param>
    /// <param name="data">保存するテキスト</param>
    private static void SaveText(string fileName, string json)
    {
        string combinedPath = Path.Combine(GetInternalStoragePath(), fileName);
        using (var streamWriter = new StreamWriter(combinedPath))
        {
            streamWriter?.WriteLine(json);
        }
    }

    /// <summary>
    /// ファイルから設定データを取得する
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    private static string GetText(string fileName)
    {
        string combinedPath = Path.Combine(GetInternalStoragePath(), fileName);
        using (var streamReader = new StreamReader(combinedPath))
        {
            return streamReader?.ReadLine();
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
