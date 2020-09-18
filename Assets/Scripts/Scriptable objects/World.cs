using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="World", menuName = "World")]
public class World : ScriptableObject {
    //NO START AND UPDATE IN SCRIPTABLE OBJECTS
    public Level[] levels;
}
