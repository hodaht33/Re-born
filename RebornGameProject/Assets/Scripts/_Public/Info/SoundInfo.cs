#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 오디오 정보 관리
/// </summary>
public class SoundInfo : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] bgmClips;
    [SerializeField]
    private AudioClip[] sfxClips;

    private static Dictionary<EBgmList, AudioClip> dicBgm = new Dictionary<EBgmList, AudioClip>();
    private static Dictionary<ESfxList, AudioClip> dicSfx = new Dictionary<ESfxList, AudioClip>();

    public enum EBgmList
    {
        Main,
        Subway,
        Campus,
        Calssroom,
        FriendEnding,
        BoyFriendEnding,
        GirlFriendEnding,
        FriendEnding_oldVer,
        ShortTheme,
    }

    public enum ESfxList
    {
        UIClick,
        GetItem,
        Pencil,
        Unlock,
        UseKey,
        OpenDoor,
        DiaryAlert,
        ClatteringNoise,
    }

    public static AudioClip GetBgmClip(EBgmList bgm)
    {
        return dicBgm[bgm];
    }

    public static AudioClip GetSfxClip(ESfxList sfx)
    {
        return dicSfx[sfx];
    }

    private void Awake()
    {
        for (int i = 0; i < bgmClips.Length; ++i)
        {
            dicBgm.Add((EBgmList)i, bgmClips[i]);
        }

        for (int i = 0; i < sfxClips.Length; ++i)
        {
            dicSfx.Add((ESfxList)i, sfxClips[i]);
        }
    }

    //private static string[] bgmNameList = { "로비화면", "지하철", "캠퍼스",
    //    "강의실", "여사친 (멜로디어쿠스틱)", "남친 엔딩",
    //    "여친엔딩 (201015)", "여사친엔딩 (201015)", "짧은테마1" };
    //
    //private static string[] sfxNameList = { "E1)터치1", "E2)터치2", "E3)연필 소리",
    //    "E4)자물쇠 풀리는 소리", "E5)열쇠사용음", "E6)문 열리는 소리",
    //    "E7)알림음1", "E8)덜그럭소리" };


}