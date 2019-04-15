using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AGird : MonoBehaviour
{
    public Transform startPos;
    public LayerMask wallLayer;
    public Vector2 worldSize;
    public float nodeRadius;

    private Node[,] grid;
    public List<Node> finalPath;

    private float nodeDiameter;
    private int gridSizeX;
    private int gridSizeY;

    // Start is called before the first frame update
    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(worldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(worldSize.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * worldSize.x / 2 - Vector3.up * worldSize.y / 2;
        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPos = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool isWall = false;
                if (Physics2D.OverlapCircle(worldPos, nodeRadius * 0.9f))
                {
                    isWall = true;
                }
                grid[x, y] = new Node(isWall, worldPos, x, y);
            }
        }
    }

    public Node NodeFromPosition(Vector3 _position)
    {
        /*float posX = (_position.x + worldSize.x / 2) / worldSize.x;
        float posY = (_position.y + worldSize.y / 2) / worldSize.y;

        posX = Mathf.Clamp01(posX);
        posY = Mathf.Clamp01(posY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * posX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * posY);*/
        float posX = (_position.x + worldSize.x / 2) / nodeDiameter;
        float posY = (_position.y + worldSize.y / 2) / nodeDiameter;

        int x = (int)posX;
        int y = (int)posY;
        
        return grid[x, y];
    }

    public List<Node> NeighborNodes(Node _node)
    {
        List<Node> neighborNodes = new List<Node>();
        int x, y;

        //top
        x = _node.gridX;
        y = _node.gridY + 1;
        if(x >= 0 && x < gridSizeX)
        {
            if (y >= 0 && y < gridSizeY)
            {
                neighborNodes.Add(grid[x, y]);
            }
        }

        //top right
        x = _node.gridX + 1;
        y = _node.gridY + 1;
        if (x >= 0 && x < gridSizeX)
        {
            if (y >= 0 && y < gridSizeY)
            {
                if(!grid[x-1,y].isWall && !grid[x, y - 1].isWall)
                {
                    neighborNodes.Add(grid[x, y]);
                }
            }
        }

        //right
        x = _node.gridX + 1;
        y = _node.gridY;
        if (x >= 0 && x < gridSizeX)
        {
            if (y >= 0 && y < gridSizeY)
            {
                neighborNodes.Add(grid[x, y]);
            }
        }

        //bottom right
        x = _node.gridX + 1;
        y = _node.gridY - 1;
        if (x >= 0 && x < gridSizeX)
        {
            if (y >= 0 && y < gridSizeY)
            {
                if (!grid[x - 1, y].isWall && !grid[x, y + 1].isWall)
                {
                    neighborNodes.Add(grid[x, y]);
                }
            }
        }

        //bottom
        x = _node.gridX;
        y = _node.gridY - 1;
        if (x >= 0 && x < gridSizeX)
        {
            if (y >= 0 && y < gridSizeY)
            {
                neighborNodes.Add(grid[x, y]);
            }
        }

        //bottom left
        x = _node.gridX - 1;
        y = _node.gridY - 1;
        if (x >= 0 && x < gridSizeX)
        {
            if (y >= 0 && y < gridSizeY)
            {
                if (!grid[x + 1, y].isWall && !grid[x, y + 1].isWall)
                {
                    neighborNodes.Add(grid[x, y]);
                }
            }
        }

        //left
        x = _node.gridX - 1;
        y = _node.gridY;
        if (x >= 0 && x < gridSizeX)
        {
            if (y >= 0 && y < gridSizeY)
            {
                neighborNodes.Add(grid[x, y]);
            }
        }

        //top left
        x = _node.gridX - 1;
        y = _node.gridY + 1;
        if (x >= 0 && x < gridSizeX)
        {
            if (y >= 0 && y < gridSizeY)
            {
                if (!grid[x + 1, y].isWall && !grid[x, y - 1].isWall)
                {
                    neighborNodes.Add(grid[x, y]);
                }
            }
        }

        return neighborNodes;
    }

    private void OnDrawGizmos()
    {
        if (grid != null)
        {
            foreach(Node n in grid)
            {
                if (n.isWall)
                {
                    Gizmos.color = Color.black;
                }
                else
                {
                    Gizmos.color = Color.white;
                }
                if(finalPath != null)
                {
                    if (finalPath.Contains(n))
                    {
                        Gizmos.color = Color.red;
                    }
                }

                Gizmos.DrawCube(n.position, Vector3.one * nodeDiameter);
            }
        }
    }
}
