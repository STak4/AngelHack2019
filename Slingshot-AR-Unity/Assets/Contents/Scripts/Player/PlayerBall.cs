using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall
{
    public class Ball
    {
        public BallContainer.BallType _type { get; private set; }
        int _count;

        public Ball(BallContainer.BallType type)
        {
            _type = type;
            _count = 0;
        }

        public bool Shoot()
        {
            if(_count > 0)
            {
                _count -= 1;
                return true;
            }
            else
            {
                return false;
            }
        } 
    }

    Ball _normals;
    Ball _fires;
    Ball _fronzens;

    List<Ball> playerBalls;

    public Ball _selected { get; private set; }

    int maxVal = 0;

    public PlayerBall()
    {
        foreach(var val in Enum.GetValues(typeof(BallContainer.BallType)))
        {
            if((int)val >= maxVal)
            {
                maxVal = (int)val;
            }
        }

        InitBalls();
    }

    public void ChoiseNext()
    {
        if (_selected == playerBalls[maxVal])
        {
            _selected = playerBalls[0];
        }
        else
        {
            _selected = playerBalls[(int)_selected._type + 1];
        }
    }

    public void ChoiseBefore()
    {
        if (_selected == playerBalls[0])
        {
            _selected = playerBalls[maxVal];
        }
        else
        {
            _selected = playerBalls[(int)_selected._type - 1];
        }
    }

    void InitBalls()
    {
        _normals = new Ball(BallContainer.BallType.Normal);
        _fires = new Ball(BallContainer.BallType.Fire);
        _fronzens = new Ball(BallContainer.BallType.Frozen);

        playerBalls.Add(_normals);
        playerBalls.Add(_fires);
        playerBalls.Add(_fronzens);

        _selected = playerBalls[0];
    }
    
}
