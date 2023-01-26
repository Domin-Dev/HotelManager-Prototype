using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BuildingObject", menuName = "BuildingObject")]
public class BuildingObject : ScriptableObject
{
    public int Id;
    public string name;
    public List<Sprite> images = new List<Sprite>();
    public int price;
    public List<UIManager.ObjectTag> Tags = new List<UIManager.ObjectTag>();
    BuildingObject()
    {
        Tags.Add(UIManager.ObjectTag.none);
    }
}
