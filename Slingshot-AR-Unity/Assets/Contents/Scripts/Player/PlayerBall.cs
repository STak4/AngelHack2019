using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall
{
    public enum BallType
    {
        None,
        Normal,
        Fire
    }

    public class Ball
    {
        BallType _type;
        int _count;

        public Ball()
        {
            _type = BallType.None;
            _count = 0;
        }
    }

    List<Ball> _normals;
    List<Ball> _fires;

    Ball _selected;

    public PlayerBall()
    {
        InitBalls();
    }

    public void ChangeBall()
    {

    }

    void InitBalls()
    {
        _normals = new List<Ball>();
        _fires = new List<Ball>();
    }
    
}
