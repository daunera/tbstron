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

    public int Number;

    internal CharacterSelectorWindow window;

    internal void SetCharacter(Character character)
    {
        this.character = character;

        CharacterNameText.text = character.Name;
        CharacterNameText.color = character.Color;
        CharacterImage.sprite = character.Sprite;
    }

    private void SetActor(bool right)
    {
        bool isPlayer = false;
        Enemy enemy = window.GetEnemy(actor, right, out isPlayer);

        if (character.Id == 0)
        {
            SetCharacter(window.GetCharacter(character, true));
        }

        if (isPlayer)
        {
            SetPlayer();
        }
        else
        {
            SetEnemy(enemy);
        }
    }

    internal void SetEnemy(Enemy actor)
    {
        if (actor.Id == 0)
        {
            SetCharacter(new Character());
        }
        ActorNameText.text = actor.Name;

        this.actor = actor;
    }

    internal void SetPlayer()
    {
        actor = null;

        if (Number == 0)
        {
            ActorNameText.text = PlayerManager.Instance.Player.Name;
        }
        else
        {
            ActorNameText.text = "Guest" + Number;
        }
    }

    public void OnClick_ActorRightButton()
    {
        SetActor(true);
    }

    public void OnClick_ActorLeftButton()
    {
        SetActor(false);
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
