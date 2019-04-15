using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    private AGird grid;
    public Transform startPos;
    public Transform targetPos;

    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<AGird>();
    }

    // Update is called once per frame
    void Update()
    {
        FindPath(startPos.position, targetPos.position);
    }

    private void FindPath(Vector3 _startPos, Vector3 _targetPos)
    {
        Node startNode = grid.NodeFromPosition(_startPos);
        Node targetNode = grid.NodeFromPosition(_targetPos);

        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            Node currentNode = openList[0];
            for(int i = 1; i < openList.Count; i++)
            {
                if(openList[i].fCost <= currentNode.fCost && openList[i].hCost < currentNode.hCost)
                {
                    currentNode = openList[i];
                }
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if(currentNode == targetNode)
            {
                GetFinalPath(startNode, targetNode);
            }

            foreach(Node neighborNode in grid.NeighborNodes(currentNode))
            {
                if(neighborNode.isWall || closedList.Contains(neighborNode))
                {
                    continue;
                }

                int moveCost = currentNode.gCost + GetManhattanDistance(currentNode, neighborNode);

                if(moveCost < neighborNode.gCost || !openList.Contains(neighborNode))
                {
                    neighborNode.gCost = moveCost;
                    neighborNode.hCost = GetManhattanDistance(neighborNode, targetNode);
                    neighborNode.parent = currentNode;

                    if (!openList.Contains(neighborNode))
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }
    }

    private void GetFinalPath(Node _startNode, Node _endNode)
    {
        List<Node> _finalPath = new List<Node>();
        Node currentNode = _endNode;

        while(currentNode != _startNode)
        {
            _finalPath.Add(currentNode);
            currentNode = currentNode.parent;
        }

        _finalPath.Reverse();
        grid.finalPath = _finalPath;
    }

    private int GetManhattanDistance(Node _nodeA,Node _nodeB)
    {
        int x = Mathf.Abs(_nodeA.gridX - _nodeB.gridX);
        int y = Mathf.Abs(_nodeA.gridY - _nodeB.gridY);

        return x + y;
    }
}
