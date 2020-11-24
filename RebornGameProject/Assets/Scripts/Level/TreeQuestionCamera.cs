using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 삭제 예정
/// </summary>
public class TreeQuestionCamera : MonoBehaviour
{
    private Transform target;
    [SerializeField] private Transform target1;
    [SerializeField] private Transform target2;
    [SerializeField] private TreeQuestion question;
    private bool end;
    private Transform player;

    private void Start()
    {
        target = target1;
        StartCoroutine(StartTreeQuestion());
        GetComponent<Camera>().cullingMask += -1 << LayerMask.NameToLayer("Level1");
        GetComponent<Camera>().cullingMask += 1 << LayerMask.NameToLayer("Level2");
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 5.0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.RotateTowards(transform.rotation, target.rotation, 10.0f), Time.deltaTime * 10.0f);

        if (end == true)
        {
            transform.LookAt(player);
        }
    }

    private IEnumerator StartTreeQuestion()
    {
        yield return new WaitForSeconds(2.0f);
        question.StartQuestion();
        //TODO: 나무 문제 시작
    }

    public void EndCampus()
    {
        target = target2;
        end = true;
    }
}
