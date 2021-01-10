using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using DG.Tweening;

[CreateAssetMenu(fileName = "InteractAction", menuName = "ButtonActions/InteractAction")]
public class InteractAction : BaseAction
{
    public float TurnSpeed = .3f;

    public override void Activate(Interaction interaction)
    {
        interaction.interactionMethod += InteractActionAnimation;
        base.Activate(interaction);
    }

    private void InteractActionAnimation(CharacterScript character, Interactable interactable)
    {
        character.transform.DOLookAt(new Vector3(interactable.transform.position.x, character.transform.position.y, interactable.transform.position.z), TurnSpeed);

        character.animator.Interact();
        interactable.Interact();
    }
}
