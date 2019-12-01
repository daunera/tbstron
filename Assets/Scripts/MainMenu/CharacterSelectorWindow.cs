using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEditor;

public class CharacterSelectorWindow : MonoBehaviour
{
    public List<CharacterSelector> CharacterSelectors;

    public MapSelector MapSelector;

    private void Start()
    {
        for (int i = 0; i < CharacterSelectors.Count; i++)
        {
            CharacterSelectors[i].window = this;
        }

        MapSelector.window = this;
    }

    public void Inicialize()
    {
        CharacterSelectors[0].ActorLeftButton.gameObject.SetActive(false);
        CharacterSelectors[0].ActorRightButton.gameObject.SetActive(false);

        CharacterSelectors[0].SetCharacter(CharacterManager.Instance.UnlockedCharacters[0]);
        CharacterSelectors[0].SetPlayer();

        CharacterSelectors[1].SetCharacter(CharacterManager.Instance.UnlockedCharacters[1]);
        CharacterSelectors[1].SetEnemy(CharacterManager.Instance.UnlockedEnemies[0]);

        for (int i = 2; i < CharacterSelectors.Count; i++)
        {
            CharacterSelectors[i].SetCharacter(new Character());
            CharacterSelectors[i].SetEnemy(new Enemy("Inactive"));
            if (CharacterManager.Instance.UnlockedCharacters.Count <= i)
            {
                CharacterSelectors[i].gameObject.SetActive(false);
            }
        }

        MapSelector.SetMap(MapManager.Instance.UnlockedMaps[0]);
    }

    internal Map GetMap(Map map, bool right)
    {
        List<Map> maps = MapManager.Instance.UnlockedMaps;

        int index = (maps.IndexOf(maps.First(x => x.Id == map.Id)) + (right ? 1 : -1));
        index = index % maps.Count;
        if (index < 0)
        {
            index += maps.Count;
        }
        return maps[index];
    }

    internal Enemy GetEnemy(Enemy actor, bool right, out bool isPlayer)
    {
        List<Enemy> enemies = new List<Enemy>();
        enemies.Add(new Enemy("Guest"));
        enemies.Add(new Enemy("Inactive"));
        for (int i = 0; i < CharacterManager.Instance.UnlockedEnemies.Count; i++)
        {
            enemies.Add(CharacterManager.Instance.UnlockedEnemies[i]);
        }

        int index = (right ? 1 : -1);
        if (actor != null)
        {
            index += enemies.IndexOf(enemies.First(x => x.Name.Equals(actor.Name)));
        }
        index = index % enemies.Count;
        if (index < 0)
        {
            index += enemies.Count;
        }
        isPlayer = index == 0;
        return enemies[index];
    }

    internal Character GetCharacter(Character character, bool right)
    {
        List<Character> characters = CharacterManager.Instance.UnlockedCharacters;
        int i = (characters.IndexOf(character) + (right ? 1 : -1));
        i = i % characters.Count;
        if (i < 0)
        {
            i += characters.Count;
        }

        while (CharacterSelectors.Any(x => x.character.Id == characters[i].Id) && characters[i].Id != character.Id)
        {
            i += right ? 1 : -1;
            i = i % characters.Count;
            if (i < 0)
            {
                i += characters.Count;
            }
        }

        return characters[i];
    }

    public void OnClick_StartGameButton()
    {
        int charactersCount = CharacterSelectors.Count(x => x.character.Id != 0);
        if (charactersCount >= 2)
        {
            CharacterManager.Instance.SelectedPlayerCharacters.Clear();
            CharacterManager.Instance.SelectedPlayerCharacters.AddRange(CharacterSelectors.Where(c => c.actor == null).Select(c => c.character));

            CharacterManager.Instance.SelectedEnemyCharacters.Clear();
            CharacterManager.Instance.SelectedEnemyCharacters.AddRange(CharacterSelectors.Where(c => c.actor != null && c.actor.Id != 0).Select(c => c.character));
            CharacterManager.Instance.SelectedEnemyLevels.Clear();
            CharacterManager.Instance.SelectedEnemyLevels.AddRange(CharacterSelectors.Where(c => c.actor != null && c.actor.Id != 0).Select(c => c.actor));

            MapManager.Instance.SelectedMap = MapSelector.Map;

            SceneManager.LoadScene(2);
        }
    }
}
