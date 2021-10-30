using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 전광판에서의 레이캐스트 및 컬러 관리
/// </summary>
public class Display : MonoBehaviour
{
    private int player;                 // 플레이어 레이어 마스크
    private Vector3 start;              // 레이캐스트 시작 위치
    private SpriteRenderer spRenderer;  // 전광판 이미지
    private bool played;                // 전광판 표시 여부

    public int count;                   // 전광판 순서

    public delegate void hitPlayer(SpriteRenderer temp, ref bool played, int count);
    public hitPlayer hit;

    private void Start()
    {
        // 초기 상태 설정
        player = 1 << LayerMask.NameToLayer("Player");
        start = transform.position - new Vector3(0, 10, 0);
        spRenderer = GetComponent<SpriteRenderer>();
        played = false;
    }

    private void Update()
    {
        if (played) return;

        // 플레이어 감지
        if (Physics.Raycast(start, new Vector3(1, 0, 0), 50f, player))
            hit(spRenderer, ref played, count);
    }
}