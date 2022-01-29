using System;
using UnityEngine;

[Serializable]
public struct EntitySpawn {

    [Range(0f, 1f)]
    public float rate;
    public Entity entity;
}