using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    public enum playerStates
    {
        raftControl,
        attack,
        charging
    }

    public static Vector3 mousePosition;
    public static playerStates playerState = playerStates.raftControl;
    public static bool gameOnPause = false;
    public static bool playerControlRaft = false;
    public static int healtRaft=0;
    public static int healthFrog=0;
    public static int playerScore;
    public static float playerHP;
    public static int pollen;
}
