using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 힌트 관련 기능
/// </summary>
public class HintManager : SingletonBase<HintManager>
{   
    [SerializeField]
    private Image hintGage;                 // 힌트 게이지

    [SerializeField]
    private GameObject hintIcon;                 // 힌트 아이콘

    private RectTransform hintTransform;    // 힌트 트랜스폼

    private float targetGage;               // 증감 목표 게이지
    
    private void Start()
    {
        hintTransform = hintGage.rectTransform;
        targetGage = hintGage.fillAmount;
    }

    // 게이지를 소모하고 힌트 표시
    public void OpenDescription(string description)
    {
        if (targetGage < 0.2f)
            return;

        ChangeTargetGage(-0.2f);
        Chat.Instance.ActivateChat(description, null, true);
    }

    // 힌트 구체를 얻을 수 있는지 확인
    public bool CheckHintGage()
    {
        return targetGage <= 0.8f;
    }

    // 목표 게이지 조정
    public void ChangeTargetGage(float amount)
    {
        targetGage += amount;
        if (targetGage > 1f)
            targetGage = 1f;
        if (targetGage < 0f)
            targetGage = 0f;

        StopCoroutine(ManageSize());
        StopCoroutine(ManageGage());
        StartCoroutine(ManageSize());
        StartCoroutine(ManageGage());
    }

    // 힌트 아이콘 움직이는 애니메이션
    public void StartMoveIcon(Vector3 position)
    {
        StartCoroutine(MoveIcon(position));
    }

    private IEnumerator MoveIcon(Vector3 start)
    {
        GameObject newIcon = Instantiate(hintIcon);
        newIcon.transform.parent = transform;

        RectTransform newRect = newIcon.GetComponent<RectTransform>();
        newRect.position = start;

        float disX = (hintTransform.position.x - start.x) / 100;
        float disY = (hintTransform.position.y - start.y) / 100;

        float count = 100;
        while(count > 0)
        {
            newRect.position = new Vector3(newRect.position.x + disX, newRect.position.y + disY);
            count--;

            yield return new WaitForSeconds(0.01f);
        }

        // 아이콘 이동 후 게이지 조절
        ChangeTargetGage(0.2f);

        Destroy(newIcon);
        yield return null;
    }

    // 브레인 아이콘 애니메이션
    private IEnumerator ManageSize()
    {
        hintTransform.localScale = new Vector3(1f, 1f, 1f);

        while (hintGage.rectTransform.localScale.x > 0.9f)
        {
            float target = hintTransform.localScale.x - 0.005f;
            hintTransform.localScale = new Vector3(target, target, target);

            yield return new WaitForSeconds(0.01f);
        }

        hintTransform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        while (hintTransform.localScale.x < 1f)
        {
            float target = hintTransform.localScale.x + 0.005f;
            hintTransform.localScale = new Vector3(target, target, target);

            yield return new WaitForSeconds(0.01f);
        }

        hintTransform.localScale = new Vector3(1f, 1f, 1f);
        yield return null;
    }

    // 목표 게이지로 이동
    private IEnumerator ManageGage()
    {
        if (targetGage > hintGage.fillAmount)
        {
            while (targetGage > hintGage.fillAmount)
            {
                hintGage.fillAmount += 0.002f;
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            while (targetGage < hintGage.fillAmount)
            {
                hintGage.fillAmount -= 0.002f;
                yield return new WaitForSeconds(0.01f);
            }
        }

        hintGage.fillAmount = targetGage;
        yield return null;
    }
}