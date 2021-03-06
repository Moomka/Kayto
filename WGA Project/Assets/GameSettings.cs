using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    public enum playerStates
    {
        raftControl,
        attack,
        charging,
        tongueReturn
    }

    public static Vector3 frogPosition;
    public static Vector3 mousePosition;
    public static playerStates playerState = playerStates.raftControl;
    public static bool gameOnPause = false;
    public static bool playerControlRaft = false;
    public static int healtRaft=0;
    public static int healthFrog=0;
    public static int playerScore;
    public static float playerHP=100f;
    public static int pollen;
}
