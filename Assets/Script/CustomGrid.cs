using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CustomGrid
{
    private int width, height;
    private float cellSize;
    private Node[,] gridArray;
    private TextMesh[,] debugArray;
    private Vector3 basePos;

    public int Width { get => width; set => width = value; }
    public int Height { get => height; set => height = value; }
    public float CellSize { get => cellSize; set => cellSize = value; }
    public Node[,] GridArray { get => gridArray; set => gridArray = value; }

    public Vector3 BasePos { get => basePos; set => basePos = value; }
    public TextMesh[,] DebugArray { get => debugArray; set => debugArray = value; }

    public CustomGrid(int width, int height, int cellSize, Vector3 basePos)
    {
        this.Width = width;
        this.Height = height;
        this.CellSize = cellSize;
        this.BasePos = basePos;
        GridArray = new Node[width, height];
        DebugArray = new TextMesh[Width, height];
        for (int i = 0; i < GridArray.GetLength(0); i++)
            for (int j = 0; j < GridArray.GetLength(1); j++)
            {        
                GridArray[i, j] = new Node(i, j);
                DebugArray[i, j] = CreatWorldText(null, GetWorldPosition(i, j) + new Vector3(cellSize, cellSize) * .5f, i+"."+j, Color.white, 355, 2);
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i, j + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i + 1, j), Color.white, 100f);
            }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
        
    }




    public static TextMesh CreatWorldText(Transform parent, Vector3 position, string text, Color color, int fontSize, int sortingOrder)
    {
        GameObject gameOject = new GameObject("WorldText", typeof(TextMesh));
        Transform transform = gameOject.transform;
        transform.SetParent(parent);
        transform.position = position;
        TextMesh textMesh = gameOject.GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.characterSize = 0.03f;
        textMesh.color = color;
        textMesh.fontSize = fontSize;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }

    //convert board pos to world pos
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * CellSize + BasePos;
    }
    //converworld to board
    public void GetXY(Vector3 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPos.x - BasePos.x) / CellSize);
        y = Mathf.FloorToInt((worldPos.y - BasePos.y) / CellSize);
    }
    public void SetValue(int x, int y, Node value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            GridArray[x, y] = value;
            //TODO need make visual debug better
            if (!GridArray[x, y].IsWalkable)
            DebugArray[x, y].text ="X";
        }

    }
    public void SetValue(Vector3 worldPos, Node value)
    {

        int x, y;
        GetXY(worldPos, out x, out y);
        SetValue(x, y, value);
    }
    public Node GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return GridArray[x, y];
        }
        return null;
    }
    public Node GetValue(Vector3 worldPos)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        return GetValue(x, y);
    }
}
