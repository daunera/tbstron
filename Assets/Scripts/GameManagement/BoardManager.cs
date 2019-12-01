using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public int columns;
    public int rows;

    public GameObject tile;
    public GameObject player;

    private Transform boardHolder;
    public List<PlayerInGame> players = new List<PlayerInGame>();

    private TileBehaviour[,] tiles;

    public void SetupBoard(Map map, List<Character> player, List<Character> enemies, List<Enemy> enemyLvl)
    {
        BoardSetup(map);
        WallGenerator(map.Id - 1);
        PlayersAtRandom(player);
        EnemyAtRandom(enemies, enemyLvl);
    }

    void BoardSetup(Map map)
    {
        rows = map.Height + 2;
        columns = map.Width + 2;
        tiles = new TileBehaviour[columns, rows];

        boardHolder = GameObject.Find("Board").transform;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject instance = Instantiate(tile, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
                if (x == 0 || x == columns - 1 || y == 0 || y == rows - 1)
                    instance.GetComponent<TileBehaviour>().SetType(enTileType.Wall, new Color(0, 0, 0));

                tiles[x, y] = instance.GetComponent<TileBehaviour>();
            }
        }
    }


    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(1, columns - 1), Random.Range(1, rows - 1), 0);
    }

    void PlayersAtRandom(List<Character> ch)
    {
        for (int i = 0; i < ch.Count; i++)
        {
            GameObject instance = Instantiate(player, RandomPosition(), Quaternion.identity);

            SpriteRenderer renderer = instance.transform.GetComponent<SpriteRenderer>();
            renderer.color = ch[i].Color;

            PlayerInGame pig = instance.transform.GetComponent<PlayerInGame>();
            pig.IsAlive = true;
            pig.IsEnemy = false;

            Move move = instance.GetComponent<Move>();
            setInputAxis(move, i - 1);
            instance.transform.SetParent(boardHolder);
        }
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
            setInputAxis(move, 0);

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
            Vector3 wallVector = RandomPosition();
            int direction = Random.Range(0, 3); // 0 up, 1 right, 2 down, 3 left

            tiles[(int)wallVector.x, (int)wallVector.y].SetType(enTileType.Wall, new Color(0, 0, 0));

            int doTurnCount = averageWallTurn;
            for (int j = 0; j < averageWallLength; j++)
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

                int x = (int)wallVector.x;
                int y = (int)wallVector.y;

                if (x >= 0 && x < columns && y >= 0 && y < rows && tiles[x, y].type == enTileType.Empty)
                {
                    tiles[x, y].SetType(enTileType.Wall, new Color(0, 0, 0));
                }
                else
                    break;
            }
        }
    }

    public void UpdateTile(GameObject obj, Vector3 targetVector)
    {
        int x = (int)targetVector.x;
        int y = (int)targetVector.y;

        if (x >= 0 && x < columns && y >= 0 && y < rows && tiles[x, y].type == enTileType.Empty)
        {
            tiles[x, y].SetType(enTileType.Wall, obj.transform.GetComponent<SpriteRenderer>().color);

            obj.transform.position = targetVector;
        }
        else
        {
            obj.GetComponent<PlayerInGame>().IsAlive = false;
            obj.SetActive(false);
        }
    }

    private void setInputAxis(Move move, int axis)
    {
        if (Move.playerAxis.ContainsKey(axis))
        {
            move.axis = Move.playerAxis[axis];
        }

        move.transform.forward = new Vector3(0, 1, 0);
    }
}
