using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterSelectorWindow : MonoBehaviour
{
    public List<CharacterSelector> CharacterSelectors;

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
}
