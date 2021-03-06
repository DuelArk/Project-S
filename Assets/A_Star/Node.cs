﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int gridX;
    public int gridY;
    public bool isWall;
    public Vector3 position;

    public int gCost;
    public int hCost;
    public int fCost { get { return gCost + hCost; } }

    public Node parent;
    
    public Node(bool _isWall,Vector3 _position,int _gridX, int _gridY)
    {
        isWall = _isWall;
        position = _position;
        gridX = _gridX;
        gridY = _gridY;
    }
}
