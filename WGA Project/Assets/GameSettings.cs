using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    public enum playerStates
    {
        raftControl,
        Attack
    }

    public static playerStates playerState = playerStates.raftControl;
    public static bool gameOnPause = false;
    public static bool playerControlRaft = false;
}
