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
    Pheonix,    // �Ǵн�
    Naiad,      // ���̾Ƶ�
    Ariel,      // ������
    Oread,      // ��������
    Ignis,      // �̱״Ͻ�
    Archane     // �Ƹ�ī��
}