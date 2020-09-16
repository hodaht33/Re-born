using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lockButton : MonoBehaviour
{
    public void ButtonUp()
    {
        if (gameObject.transform.parent.gameObject.GetComponent<LockTxt>().i >= 6)
            gameObject.transform.parent.gameObject.GetComponent<LockTxt>().i = 0;
        else
            gameObject.transform.parent.gameObject.GetComponent<LockTxt>().i++;

        gameObject.transform.parent.gameObject.GetComponent<LockTxt>().ChangeAlphabet();
    }

    public void ButtonDown()
    {
        Debug.Log("dmdmdm");
        if (gameObject.transform.parent.gameObject.GetComponent<LockTxt>().i <= 0)
            gameObject.transform.parent.gameObject.GetComponent<LockTxt>().i = 6;
        else
            gameObject.transform.parent.gameObject.GetComponent<LockTxt>().i--;

        gameObject.transform.parent.gameObject.GetComponent<LockTxt>().ChangeAlphabet();
    }
}
