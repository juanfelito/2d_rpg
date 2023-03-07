using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private Text dialogText;
    [SerializeField] private int lettersPerSecond;

    public UnityEvent onShowDialog;
    public UnityEvent onHideDialog;

    public void ShowDialog(Dialog dialog)
    {
        onShowDialog?.Invoke();
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public void HandleUpdate()
    {
        Debug.Log("Hola");
    }

    private IEnumerator TypeDialog(string line)
    {
        dialogText.text = "";

        foreach (char letter in line)
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }

    public void HideDialog()
    {
        onHideDialog.Invoke();
        dialogBox.SetActive(false);
    }
}