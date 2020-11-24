using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextCutScene : MonoBehaviour
{
    public void Next()
    {
        CutSceneManager.Instance.NextCutScene();
    }
}
