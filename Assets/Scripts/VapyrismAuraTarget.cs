using System;
using UnityEngine;

public class VampyrismAuraTarget
{
    public Transform Transform { get; }
    public Action PlayEffect { get; }
    public Action StopEffect { get; }

    public VampyrismAuraTarget(Transform transform, Action playEffect, Action stopEffect)
    {
        Transform = transform;
        PlayEffect = playEffect;
        StopEffect = stopEffect;
    }
}