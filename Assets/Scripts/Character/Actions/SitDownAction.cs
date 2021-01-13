using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using DG.Tweening;
using System.Linq;

[CreateAssetMenu(fileName = "SitDownAction", menuName = "ButtonActions/SitDownAction")]
public class SitDownAction : BaseAction
{
    public float SitDownSpeed = .5f;

    public override void Activate(Interaction interaction)
    {
        interaction.interactionMethod += MessWithAnimation;
        base.Activate(interaction);
    }

    private void MessWithAnimation(CharacterScript character, Interactable interactable)
    {
        if (interactable.InteractionPoint != null)
        {
            character.transform.DOMove(interactable.InteractionPoint.position, .25f);
            character.transform.DORotateQuaternion(interactable.InteractionPoint.rotation, SitDownSpeed);
        }

        character.mover.DisableNavmesh();
        character.animator.SitDown();
    }
}
