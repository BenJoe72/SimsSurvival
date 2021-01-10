using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class DestroySelf : MonoBehaviour
{
    public float Delay = 1f;
    public TweenScriptable[] _destoryAnimation;

    private List<Collider> colliders;

    private void Start()
    {
        colliders = new List<Collider>();
        colliders.AddRange(GetComponents<Collider>());
        colliders.AddRange(GetComponentsInChildren<Collider>(true));
    }

    public void Destroy()
    {
        // Disable colliders on current frame instead of just destroying them on the next one
        foreach (var coll in colliders)
            coll.enabled = false;

        Sequence destroySq = DOTween.Sequence();
        foreach (var anim in _destoryAnimation)
            destroySq.Append(anim.GetTween(transform));
        
        destroySq.AppendInterval(Delay);
        destroySq.AppendCallback(() => { Destroy(gameObject); });
    }
}
