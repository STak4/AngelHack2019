using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallContainer : MonoBehaviour
{
    public enum BallType
    {
        None = -1,
        Normal = 0,
        Fire = 1,
        Frozen = 2
    }

    [SerializeField]
    GameObject normalBall;

    [SerializeField]
    GameObject fireBall;

    [SerializeField]
    GameObject frozenBall;


    public GameObject GetBallPrefab(BallType type)
    {
        Debug.Log("Call type:" + type);
        switch (type)
        {
            case BallType.Normal:
                return normalBall;
            case BallType.Fire:
                return fireBall;
            case BallType.Frozen:
                return frozenBall;

            case BallType.None:
            default:
                return null;
        }
    }
}
