using UnityEngine;
using UnityEngine.SceneManagement;

public class LockScript : MonoBehaviour
{
    private string answer = "LOVER";
    private LockTxt[] lockTxts;
    
    private void Awake()
    {
        lockTxts = GetComponentsInChildren<LockTxt>();
        for (int i = 0; i < lockTxts.Length; ++i)
        {
            lockTxts[i].GetComponentInChildren<lockButton>().click += CheckAnswer;
        }
    }

    public void CheckAnswer()
    {
        for (int i = 0; i < lockTxts.Length; i++)
        {
            if (lockTxts[i].GetAlphabet() != answer[i])
            {
                return;
            }
        }

        SceneManager.LoadScene("End");
    }
}
