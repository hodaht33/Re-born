using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 전광판들을 관리
/// </summary>
public class DisplayManager : MonoBehaviour
{
    // 전광판 리스트
    [SerializeField] Display[] displayList;

    // 전광판 온오프 컬러
    [SerializeField] Color off;
    [SerializeField] Color on;

    // 전광판 켜지는 순서
    private int[] sequence;
    private int index;

    private void Start()
    {
        // 순서 초기 설정
        sequence = new int[]{ 0, 3, 2, 4 };
        index = 0;

        // 전광판 이벤트 추가
        displayList[0].hit += MakeBlink;
        displayList[1].hit += MakeNone;
        displayList[2].hit += MakeBlink;
        displayList[3].hit += MakeON;
        displayList[4].hit += MakeBlink;
    }

    // 빈 함수
    private void MakeNone(SpriteRenderer temp, ref bool played, int count)
    {

    }

    // 한번에 켜지는 기능
    private void MakeON(SpriteRenderer temp, ref bool played, int count)
    {
        if (played) return;

        if (sequence[index] == count)
        {
            index++;
            played = true;
            temp.color = on;
        }
    }

    // 반짝반짝 기능 사용
    private void MakeBlink(SpriteRenderer temp, ref bool played, int count)
    {
        if (played) return;

        if(sequence[index] == count)
        {
            index++;
            played = true;
            StartCoroutine(Blink(temp));
        }
    }

    // 전광판 반짝반짝하는 기능
    private IEnumerator Blink(SpriteRenderer temp)
    {
        temp.color = on;
        yield return new WaitForSeconds(0.5f);
        temp.color = off;
        yield return new WaitForSeconds(0.5f);

        temp.color = on;
        yield return new WaitForSeconds(0.5f);
        temp.color = off;
        yield return new WaitForSeconds(0.5f);

        temp.color = on;
    }
}