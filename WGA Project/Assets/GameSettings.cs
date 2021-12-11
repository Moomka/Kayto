using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    public enum playerStates
    {
        raftControl,
        attack
    }

    public static Vector3 mousePosition;
    public static playerStates playerState = playerStates.raftControl;
    public static bool gameOnPause = false;
    public static bool playerControlRaft = false;
    public static int playerScore;
}
