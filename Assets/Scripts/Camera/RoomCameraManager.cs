using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class RoomCameraManager : MonoBehaviour
{
    [System.Serializable]
    public class RoomCamera
    {
        public string roomName;
        public CinemachineVirtualCamera cmCamera;
    }

    public List<RoomCamera> rooms;
    private CinemachineVirtualCamera currentCamera;

    private void Start()
    {
        foreach (var room in rooms)
        {
            if (room.cmCamera != null)
            {
                room.cmCamera.Priority = 0;
            }
        }

        if (rooms.Count > 0 && rooms[0].cmCamera != null)
        {
            currentCamera = rooms[0].cmCamera;
            currentCamera.Priority = 10;
        }
    }

    public void SwitchToRoom(string roomName)
    {
        var room = rooms.Find(r => r.roomName == roomName);
        if (room != null && room.cmCamera != currentCamera)
        {
            if (currentCamera != null)
            {
                currentCamera.Priority = 0;
            }

            currentCamera = room.cmCamera;
            currentCamera.Priority = 10;
        }
    }

    public string GetCurrentRoomName()
    {
        var currentRoom = rooms.Find(r => r.cmCamera == currentCamera);
        return currentRoom != null ? currentRoom.roomName : null;
    }
}