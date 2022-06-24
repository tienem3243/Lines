using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    float g=0, h=0, f=0;
    int x, y;
    bool isWalkable;
    Node comeFrom;
    public Node()
    {
        
    }

    public Node(int x, int y)
    {
        this.x = x;
        this.y = y;
        this.IsWalkable = true;
    }

    public Node(float g, float h, float f)
    {
        this.G = g;
        this.H = h;
        this.F = f;
    }
    
    public float G { get => g; set => g = value; }
    public float H { get => h; set => h = value; }
    public float F { get => f; set => f = value; }
    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public Node CameFromNode { get => comeFrom; set => comeFrom = value; }
    public bool IsWalkable { get => isWalkable; set => isWalkable = value; }

    public override string ToString()
    {
        return x + " " + y;
    }
    public void CalculateFCost()
    {
        f = g + h;
    }
}
