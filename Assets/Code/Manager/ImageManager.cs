using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageManager : MonoBehaviour
{
    [SerializeField] List<Sprite> _vecBackImages;
    [SerializeField] List<Sprite> _vecHumenImages;
    public Sprite GetBackImage(BackImageType eBIT)
    {
        return _vecBackImages[(int)eBIT];
    }
    public Sprite GetHumenImage(HumenImageType eHIT)
    {
        return _vecHumenImages[(int)eHIT];
    }
}
public enum BackImageType
{
    None, Majoc, Humen, Sunmon,HeroParty,BurningVillage,
    CloseDoor, OpenDoor, Window, RedMoon, Will, Forest,
    red
}
public enum HumenImageType
{
    None, Little_Stella, Vela, Cliris, Stella, Ophelia, GaGoil
}