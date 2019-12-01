using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BoardManager boardScript;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        boardScript = GetComponent<BoardManager>();
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            List<Character> players = CharacterManager.Instance.SelectedPlayerCharacters;
            List<Character> enemies = CharacterManager.Instance.SelectedEnemyCharacters;
            List<Enemy> enemyLevels = CharacterManager.Instance.SelectedEnemyLevels;
            Map map = MapManager.Instance.SelectedMap;

            boardScript.SetupBoard(map, players, enemies, enemyLevels);
        }
    }
}
