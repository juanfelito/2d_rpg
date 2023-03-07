using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;
    [SerializeField] private DialogManager dialogManager;

    public void Interact()
    {
        dialogManager.ShowDialog(dialog: dialog);
    }
}
