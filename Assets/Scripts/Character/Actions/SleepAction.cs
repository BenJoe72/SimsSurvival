using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using DG.Tweening;
using System.Linq;

[CreateAssetMenu(fileName = "SleepAction", menuName = "ButtonActions/SleepAction")]
public class SleepAction : BaseAction
{
    public float SitDownSpeed = .5f;

    public override void Activate(Interaction interaction)
    {
        interaction.interactionMethod += SleepAnimation;
        base.Activate(interaction);
    }

    private void SleepAnimation(CharacterScript character, Interactable interactable)
    {
        if (interactable.InteractionPoint != null)
        {
            character.transform.DOMove(interactable.InteractionPoint.position, .25f);
            character.transform.DORotateQuaternion(interactable.InteractionPoint.rotation, SitDownSpeed);
        }

        character.mover.DisableNavmesh();
        character.animator.LayDown();
    }
}
