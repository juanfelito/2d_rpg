using UnityEngine;

public enum GameState { FreeRoam, Dialog }

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] private DialogManager dialogManager;

    private GameState state;

    public void OnEnable()
    {
        dialogManager.onShowDialog.AddListener(OnShowDialog);
        dialogManager.onHideDialog.AddListener(OnHideDialog);
    }

    public void OnDisable()
    {
        dialogManager.onShowDialog.RemoveListener(OnShowDialog);
        dialogManager.onHideDialog.RemoveListener(OnHideDialog);
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.FreeRoam:
                playerController.HandleUpdate();
                break;
            case GameState.Dialog:
                dialogManager.HandleUpdate();
                break;
        }
    }

    private void OnShowDialog()
    {
        state = GameState.Dialog;
    }

    private void OnHideDialog()
    {
        state = GameState.FreeRoam;
    }
}
