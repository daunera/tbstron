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

    public List<PlayerInGame> players = new List<PlayerInGame>();

    void Start()
    {

    }

    void Update()
    {

    }


    public void SetupBoard(Map mp, List<Character> player, List<Character> enemies, List<Enemy> enemyLvl)
    {
        BoardSetup();
        InitList();
        WallGenerator(mp.Id - 1);
        PlayersAtRandom(player);
        EnemyAtRandom(enemies, enemyLvl);
    }

    void BoardSetup()
    {
        boardHolder = GameObject.Find("Board").transform;

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

    private Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    void PlayersAtRandom(List<Character> ch)
    {
        var playerNum = 1;
        ch.ForEach(character =>
        {
            GameObject instance = Instantiate(player, RandomPosition(), Quaternion.identity);

            SpriteRenderer renderer = instance.transform.GetComponent<SpriteRenderer>();
            renderer.color = character.Color;

            PlayerInGame pig = instance.transform.GetComponent<PlayerInGame>();
            pig.IsAlive = true;
            pig.IsEnemy = false;

            Move move = instance.GetComponent<Move>();
            setKeyCodes(move, playerNum);
            instance.transform.SetParent(boardHolder);
            playerNum++;
        });
    }

    void EnemyAtRandom(List<Character> ch, List<Enemy> enemy)
    {
        ch.ForEach(character =>
        {
            GameObject instance = Instantiate(player, RandomPosition(), Quaternion.identity);

            SpriteRenderer renderer = instance.transform.GetComponent<SpriteRenderer>();
            renderer.color = character.Color;

            PlayerInGame pig = instance.transform.GetComponent<PlayerInGame>();
            pig.IsAlive = true;
            pig.IsEnemy = true;
            pig.enemyLevel = enemy[ch.IndexOf(character)].Id;

            Move move = instance.GetComponent<Move>();
            setKeyCodes(move, 0);

            instance.transform.SetParent(boardHolder);
        });
    }

    void WallGenerator(int wallDensity)
    {
        int averageWallLength = Convert.ToInt32((columns + rows) / 2);
        int averageWallTurn = Convert.ToInt32(averageWallLength / 5);

        // wall density between 0-2 
        for (int i = 0; i < wallDensity * 5; i++)
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

    public void UpdateTile(GameObject obj, Vector3 directionVector)
    {
        var isClearTile = true;
        Vector3 expectedVector = obj.transform.position + directionVector;

        List<GameObject> nextTileObjects = new List<GameObject>();

        for (int i = 0; i < boardHolder.transform.childCount; i++)
        {
            if (boardHolder.transform.GetChild(i).position.Equals(expectedVector))
            {
                nextTileObjects.Add(boardHolder.transform.GetChild(i).gameObject);
            }
        }
        nextTileObjects.ForEach(t =>
        {
            string name = t.name.Split('(')[0];
            if (name == wall.name || name == border.name || name == player.name)
                isClearTile = false;

        });

        if (isClearTile)
        {
            GameObject instance = Instantiate(wall, obj.transform.position, Quaternion.identity);
            instance.transform.SetParent(boardHolder);
            SpriteRenderer renderer = instance.transform.GetComponent<SpriteRenderer>();
            renderer.color = obj.transform.GetComponent<SpriteRenderer>().color;

            obj.transform.position = expectedVector;
        }
        else
        {
            obj.GetComponent<PlayerInGame>().IsAlive = false;
        }
    }

    private void setKeyCodes(Move move, int axis)
    {
        if (Move.playerAxis.ContainsKey(axis))
        {
            move.axis = Move.playerAxis[axis];
        }

        move.transform.forward = new Vector3(0, 1, 0);

    }

}
