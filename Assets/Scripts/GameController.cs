using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        GameWindowManagement();
    }

    private static void GameWindowManagement()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}