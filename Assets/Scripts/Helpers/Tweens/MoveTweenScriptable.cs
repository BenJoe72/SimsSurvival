using UnityEngine;
using System.Collections;
using DG.Tweening;

[CreateAssetMenu(fileName = "MoveTween", menuName = "Configurations/Tweens/MoveTween")]
public class MoveTweenScriptable : TweenScriptable
{
    public bool Relative = false;
    public bool Local = false;
    public float Lenght = 1f;
    public Vector3 Position;

    public override Tween GetTween(Transform transform)
    {
        if (Local)
        {
            if (Relative)
            {
                return transform.DOLocalMove(transform.position + Position, Lenght);
            }
            else
            {
                return transform.DOLocalMove(Position, Lenght);
            }
        }
        else
        {
            if (Relative)
            {
                return transform.DOMove(transform.position + Position, Lenght);
            }
            else
            {
                return transform.DOMove(Position, Lenght);
            }
        }
    }
}
