using UnityEngine;

public interface IPlayerController
{
    void HandleUpdate();
}

public interface IDialogManager
{
    void ShowDialog(Dialog dialog);
    void HandleUpdate();
    void HideDialog();
}
