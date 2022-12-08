using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberManager : MonoBehaviour
{
    public List<GameObject> _objMember = new List<GameObject>();

    public Member Member
    {
        get => default;
        set
        {
        }
    }

    public Member GetMember(MemberType eMT)
    {
        return Instantiate(_objMember[(int)eMT]).GetComponent<Member>();
    }
}
public enum MemberType
{
    Pheonix,    // 피닉스
    Naiad,      // 나이아드
    Ariel,      // 에리얼
    Oread,      // 오리에드
    Ignis,      // 이그니스
    Archane     // 아르카네
}