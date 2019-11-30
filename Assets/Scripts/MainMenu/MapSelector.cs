using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MapSelector : MonoBehaviour
{
    public Button MapRightButton;
    public Button MapLeftButton;

    public Text MapNameText;

    private Map Map;

    internal CharacterSelectorWindow window;

    internal void SetMap(Map map)
    {
        this.Map = map;

        MapNameText.text = Map.Name;
    }

    public void OnClick_MapRightButton()
    {
        SetMap(window.GetMap(Map, true));
    }

    public void OnClick_MapLeftButton()
    {
        SetMap(window.GetMap(Map, true));
    }
}
