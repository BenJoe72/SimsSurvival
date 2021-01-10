using UnityEngine;
using System.Collections;
using DG.Tweening;

[CreateAssetMenu(fileName = "RotateTween", menuName = "Configurations/Tweens/RotateTween")]
public class RotateTweenScriptable : TweenScriptable
{
    public bool Relative = false;
    public bool Local = false;
    public float Lenght = 1f;
    public Vector3 Rotation;

    public override Tween GetTween(Transform transform)
    {
        if (Local)
        {
            if (Relative)
            {
                return transform.DOLocalRotate(transform.rotation.eulerAngles + Rotation, Lenght);
            }
            else
            {
                return transform.DOLocalRotate(Rotation, Lenght);
            }
        }
        else
        {
            if (Relative)
            {
                return transform.DORotate(transform.rotation.eulerAngles + transform.InverseTransformVector(Rotation), Lenght);
            }
            else
            {
                return transform.DORotate(Rotation, Lenght);
            }
        }
    }
}
