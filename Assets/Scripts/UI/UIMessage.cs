using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIMessage : MonoBehaviour
{
    [SerializeField]
    private Text text = null;

    [SerializeField]
    private InputField inputField = null;

    [SerializeField]
    private string key = "helloUser";

    [SerializeField]
    private string userName = "User Name";

    private void Reset()
    {
        text = GetComponent<Text>();
        inputField = GameObject.Find("Name InputField").GetComponent<InputField>();
    }

    private void Start()
    {
        inputField.text = userName;
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
            text.text = Languages.GetMessageByKey(key, userName) ?? "-- none --";
        }
    }

    public void OnInput()
    {
        userName = inputField.text;
        Setup();
    }
}
