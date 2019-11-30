using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CharacterSelector : MonoBehaviour
{
    public Button ImageRightButton;
    public Button ImageLeftButton;

    public Button ActorRightButton;
    public Button ActorLeftButton;

    public Text CharacterNameText;
    public Text ActorNameText;

    public Image CharacterImage;

    public Character character;
    public Enemy actor;

    internal CharacterSelectorWindow window;

    internal void SetCharacter(Character character)
    {
        this.character = character;

        CharacterNameText.text = character.Name;
        CharacterNameText.color = character.Color;
        CharacterImage.sprite = character.Sprite;
    }

    internal void SetActor(Enemy actor)
    {
        if (actor == null)
        {
            ActorNameText.text = PlayerManager.Instance.Player.Name;
        }
        else
        {
            if (actor.Id == 0)
            {
                SetCharacter(new Character());
            }
            ActorNameText.text = actor.Name;

            if (this.actor != null && this.actor.Id == 0 && actor.Id != 0)
            {
                SetCharacter(window.GetCharacter(character, true));
            }
        }

        this.actor = actor;
    }

    public void OnClick_ActorRightButton()
    {
        SetActor(window.GetEnemy(actor, true));
    }

    public void OnClick_ActorLeftButton()
    {
        SetActor(window.GetEnemy(actor, false));
    }

    public void OnClick_CharacterRightButton()
    {
        if (actor == null || actor.Id != 0)
        {
            SetCharacter(window.GetCharacter(character, true));
        }
    }

    public void OnClick_CharacterLeftButton()
    {
        if (actor == null || actor.Id != 0)
        {
            SetCharacter(window.GetCharacter(character, false));
        }
    }
}
