using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        Languages.Init(Location.JAPAN);
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        Debug.Log(Languages.GetTextByKey("start"));
        Debug.Log(Languages.GetMessageByKey("helloUser", "RerykA"));
    }
}
