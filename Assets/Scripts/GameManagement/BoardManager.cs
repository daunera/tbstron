using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public int columns = 20;
    public int rows = 20;

    public GameObject tile;
    public GameObject wall;
    public GameObject border;
    public GameObject player;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    //private GameObject[] characters;
    //private Map map = null;

    void InitList()
    {
        gridPositions.Clear();
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x <= columns; x++)
        {
            for (int y = -1; y <= rows; y++)
            {
                GameObject toInstantiate = tile;
                if (x == -1 || x == columns || y == -1 || y == rows)
                    toInstantiate = border;

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);

            }

        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    void CharactersAtRandom(List<Character> ch)
    {
        ch.ForEach(character =>
        {
            GameObject toInstantiate = player;
            SpriteRenderer renderer = toInstantiate.transform.GetComponent<SpriteRenderer>();
            renderer.color = character.Color;
            GameObject instance = Instantiate(player, RandomPosition(), Quaternion.identity);
            instance.transform.SetParent(boardHolder);
        });
    }

    void WallGenerator(int wallDensity)
    {
        int averageWallLength = Convert.ToInt32((columns + rows) / 2);
        int averageWallTurn = Convert.ToInt32(averageWallLength / 5);

        // wall density between 0-2 
        for (int i = 0; i < wallDensity*5; i++)
        {
            var containFlag = false;

            Vector3 wallVector = RandomPosition();
            int direction = Random.Range(0, 3); // 0 up, 1 right, 2 down, 3 left
            GameObject instance = Instantiate(wall, wallVector, Quaternion.identity);
            instance.transform.SetParent(boardHolder);

            int doTurnCount = averageWallTurn;
            for (int y = 0; y < averageWallLength; y++)
            {
                if (doTurnCount < 1)
                {
                    // do turn
                    int newDirection = Random.Range(0, 3);
                    // if trying to turn back
                    if (((newDirection - 2) % 4) == (direction % 4))
                        direction += 1;
                    else
                        direction = newDirection;

                    doTurnCount = averageWallTurn;
                }
                else
                    doTurnCount--;

                switch (direction)
                {
                    case 0:
                        wallVector.y += 1;
                        break;
                    case 1:
                        wallVector.x += 1;
                        break;
                    case 2:
                        wallVector.y -= 1;
                        break;
                    case 3:
                        wallVector.x -= 1;
                        break;
                    default:
                        break;
                }

                containFlag = gridPositions.Exists(v => v == wallVector);
                if (containFlag)
                {
                    instance = Instantiate(wall, wallVector, Quaternion.identity);
                    instance.transform.SetParent(boardHolder);
                    gridPositions.Remove(wallVector);
                }
                else
                    break;
            }

        }

    }

    public void SetupBoard(Map mp, List<Character> ch)
    {
        BoardSetup();
        InitList();
        WallGenerator(mp.Id-1);
        CharactersAtRandom(ch);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
