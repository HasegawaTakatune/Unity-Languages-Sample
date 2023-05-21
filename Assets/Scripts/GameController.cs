using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private const int LOCATION_EN = 0;
    private const int LOCATION_JA = 1;

    [SerializeField]
    private Dropdown dropdown = null;

    [SerializeField]
    private UIText[] uiTexts = null;

    [SerializeField]
    private UIMessage[] uiMessages = null;

    private void Reset()
    {
        dropdown = GameObject.Find("Language Dropdown").GetComponent<Dropdown>();
        uiTexts = GameObject.FindObjectsOfType<UIText>();
        uiMessages = GameObject.FindObjectsOfType<UIMessage>();
    }

    private void Start()
    {
        Location location = Location.JAPAN;
        Languages.Init(location);
        if (location == Location.ENGLISH)
        {
            dropdown.value = LOCATION_EN;
        }
        else
        {
            dropdown.value = LOCATION_JA;
        }
    }

    public void OnSelected()
    {
        switch (dropdown.value)
        {
            case LOCATION_EN:
                Languages.SetLocation(Location.ENGLISH);
                break;

            case LOCATION_JA:
                Languages.SetLocation(Location.JAPAN);
                break;

            default: return;
        }

        foreach (UIText element in uiTexts)
        {
            element.Setup();
        }

        foreach (UIMessage element in uiMessages)
        {
            element.Setup();
        }
    }
}
