using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    [SerializeField]
    private Text text = null;

    [SerializeField]
    private string key = "start";

    private void Reset()
    {
        text = GetComponent<Text>();
    }

    private void Start()
    {
        Setup();
    }

    public void Setup()
    {
        StartCoroutine(_Setup());
    }

    private IEnumerator _Setup()
    {
        yield return new WaitForSeconds(0.5f);
        if (text != null)
        {
            text.text = Languages.GetTextByKey(key) ?? "-- none --";
        }
    }
}
