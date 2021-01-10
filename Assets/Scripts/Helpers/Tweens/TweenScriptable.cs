using UnityEngine;
using System.Collections;
using DG.Tweening;

public abstract class TweenScriptable : ScriptableObject
{
    public abstract Tween GetTween(Transform transform);
}
