using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 작성자 : 박서현
/// 기능 : 자물쇠 정답 확인
/// </summary>
public class LockScript : MonoBehaviour
{
    private string mAnswer = "LOVER";
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
            if (lockTxts[i].GetAlphabet() != mAnswer[i])
            {
                return;
            }
        }

        SceneManager.LoadScene(SceneInfoManager.dicSceneInfo[SceneInfoManager.EScene.End].SceneName);
    }
}
