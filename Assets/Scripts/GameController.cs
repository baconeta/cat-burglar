using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        GameWindowManagement();
    }

    private static void GameWindowManagement()
    {
        Cursor.lockState = CursorLockMode.Locked; // has an issue with mouse delta spikes in frame 1
        Cursor.visible = false;
    }
}