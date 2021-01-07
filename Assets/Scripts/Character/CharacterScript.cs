using UnityEngine;
using System.Collections;
using Cinemachine;

public class CharacterScript : MonoBehaviour
{
    public Character_Mover mover;
    public Character_Interacter interacter;
    public Character_Animator animator;
    public GameObject SelectionIndicator;
    public CinemachineVirtualCamera ProfileCamera;

    public Manager_Resource Reesources;

    public void Select()
    {
        ProfileCamera.Priority = 100;
        SelectionIndicator.SetActive(true);
    }

    public void Deselect()
    {
        ProfileCamera.Priority = 1;
        SelectionIndicator.SetActive(false);
    }
}
