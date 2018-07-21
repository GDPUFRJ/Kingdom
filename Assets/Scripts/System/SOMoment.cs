using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Custom/Moment")]
public class SOMoment : ScriptableObject {

    public int day;
    public MomentType type;

}
public enum MomentType
{
    EVENT, HAPPENING, BATTLE
}
