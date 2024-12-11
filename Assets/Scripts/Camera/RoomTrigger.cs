using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public string room1Name; 
    public string room2Name; 
    private RoomCameraManager cameraManager;

    private void Start()
    {
        cameraManager = FindObjectOfType<RoomCameraManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (cameraManager != null)
            {
                
                string currentRoom = cameraManager.GetCurrentRoomName();

                
                string targetRoomName = (currentRoom == room1Name) ? room2Name : room1Name;

                cameraManager.SwitchToRoom(targetRoomName);
            }
        }
    }
}
