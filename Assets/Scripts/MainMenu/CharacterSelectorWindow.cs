using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class CharacterSelectorWindow : MonoBehaviour
{
    public List<CharacterSelector> CharacterSelectors;

    private void Start()
    {
        for (int i = 0; i < CharacterSelectors.Count; i++)
        {
            CharacterSelectors[i].window = this;
        }
    }

    public void Inicialize()
    {
        CharacterSelectors[0].ActorLeftButton.gameObject.SetActive(false);
        CharacterSelectors[0].ActorRightButton.gameObject.SetActive(false);

        CharacterSelectors[0].SetCharacter(CharacterManager.Instance.UnlockedCharacters[0]);
        CharacterSelectors[0].SetActor(null);

        CharacterSelectors[1].SetCharacter(CharacterManager.Instance.UnlockedCharacters[1]);
        CharacterSelectors[1].SetActor(CharacterManager.Instance.UnlockedEnemies[0]);

        for (int i = 2; i < CharacterSelectors.Count; i++)
        {
            CharacterSelectors[i].SetCharacter(new Character());
            CharacterSelectors[i].SetActor(new Enemy());
            if (CharacterManager.Instance.UnlockedCharacters.Count <= i)
            {
                CharacterSelectors[i].gameObject.SetActive(false);
            }
        }
    }

    internal Enemy GetEnemy(Enemy actor, bool right)
    {
        List<Enemy> enemies = new List<Enemy>();
        enemies.Add(new Enemy());
        for (int i = 0; i < CharacterManager.Instance.UnlockedEnemies.Count; i++)
        {
            enemies.Add(CharacterManager.Instance.UnlockedEnemies[i]);
        }

        int index = (enemies.IndexOf(enemies.First(x => x.Id == actor.Id)) + (right ? 1 : -1));
        index = index % enemies.Count;
        if (index < 0)
        {
            index += enemies.Count;
        }
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
}
