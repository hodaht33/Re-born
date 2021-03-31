#pragma warning disable CS0649

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

    // 목록에 있는 배경음 반환
    public static AudioClip GetBgmClip(EBgmList bgm)
    {
        return dicBgm[bgm];
    }

    // 목록에 있는 효과음 반환
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
}