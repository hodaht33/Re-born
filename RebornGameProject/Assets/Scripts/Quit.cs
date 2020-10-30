using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    private float timer;
    int waitingTime;

    void Start()
    {
        timer = 0.0f;
        waitingTime = 1;
        //inside = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        //Debug.Log(timer);
    }

    public void onClick()
    {
        //Debug.Log("click");
        //if (timer > waitingTime)
        //{

        // 에디터인 경우
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
        //}
    }
}
