using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using DG.Tweening;

[CreateAssetMenu(fileName = "RepeatingInteractAction", menuName = "ButtonActions/RepeatingInteractAction")]
public class RepeatingInteractAction : BaseAction
{
    public float TurnSpeed = .3f;
    public float Rate = 1f;

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
