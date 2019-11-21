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

    internal void SetCharacter(Character character)
    {
        this.character = character;

        CharacterNameText.text = character.Name;
        CharacterNameText.color = character.Color;
        CharacterImage.sprite = character.Sprite;
    }

    internal void SetActor(Enemy actor)
    {
        this.actor = actor;

        if (actor == null)
        {

        }
        else
        {
            ActorNameText.text = actor.Name;
        }
    }
}
