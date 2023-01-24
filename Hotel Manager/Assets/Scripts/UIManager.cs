using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
   [SerializeField] Sprite sprite;
   [SerializeField] GameObject cell;
   [SerializeField] Transform listTransform;

   [SerializeField] Color32 orange;

   [SerializeField] Button wallButton, floorButton, doorButton;
   [SerializeField] Dropdown dropdown;


   [SerializeField] List<BuildingElement> walls = new List<BuildingElement>();
   [SerializeField] List<BuildingElement> floors = new List<BuildingElement>();


    public enum FloorTag
    {
        none,
        floor,
        ground,
    }




    private void Start()
    {
        wallButton.onClick.AddListener(() => 
        {
            GameManager.instance.gridManager.isSelected = GridManager.IsSelected.none;
            LoadList(walls,GridManager.IsSelected.wall,FloorTag.none);
            dropdown.options = new List<Dropdown.OptionData>();
            dropdown.captionText.text = "everything";
            dropdown.interactable = false;
            ClearButtons(); 
            wallButton.GetComponent<Image>().color = Color.white; 
            wallButton.transform.GetChild(0).GetComponent<Text>().color = Color.black; 
        });
        floorButton.onClick.AddListener(() => 
        {
            GameManager.instance.gridManager.isSelected = GridManager.IsSelected.none;
            LoadList(floors,GridManager.IsSelected.floor,FloorTag.none);
            List<Dropdown.OptionData> options = new List<Dropdown.OptionData>() {new Dropdown.OptionData("everything") };
            int count = Enum.GetValues(typeof(FloorTag)).Length;
            for (int i = 1; i < count; i++)
            {
                options.Add(new Dropdown.OptionData(Enum.GetName(typeof(FloorTag), i)));                
            }
            options.Add(new Dropdown.OptionData("everything"));
            dropdown.options = options;
            dropdown.interactable = true;
            dropdown.value = 0;
            dropdown.onValueChanged.AddListener((int value) =>
            {
               if(value != dropdown.options.Count -1) LoadList(floors, GridManager.IsSelected.floor, (FloorTag)value);
               else LoadList(floors, GridManager.IsSelected.floor, (FloorTag)0);
            });


            ClearButtons(); 
            floorButton.GetComponent<Image>().color = Color.white; 
            floorButton.transform.GetChild(0).GetComponent<Text>().color = Color.black; 
        });

    }


    private void LoadList(List<BuildingElement> list,GridManager.IsSelected selected,FloorTag floorTag)
    {
        ClearList();

        int index = 0;
        int childCount = listTransform.childCount;

        if (selected != GridManager.IsSelected.floor)
        {
            GameObject gObject1;
            if (index >= childCount) gObject1 = Instantiate(cell, listTransform);
            else
            {
                gObject1 = listTransform.GetChild(index).gameObject;
                gObject1.SetActive(true);
                index++;
            }
            gObject1.transform.GetChild(1).GetComponent<Image>().sprite = sprite;
            gObject1.transform.GetChild(0).GetComponent<Text>().text = "demolish";
            gObject1.GetComponent<Button>().onClick.AddListener(() =>
            {
                GameManager.instance.gridManager.SetWall(-1, 0);
                for (int i = 0; i < listTransform.transform.childCount; i++)
                {
                    listTransform.transform.GetChild(i).GetComponent<Image>().color = orange;
                }
                gObject1.GetComponent<Image>().color = Color.white;
                GameManager.instance.gridManager.isSelected = selected;
            });
        }

        foreach (BuildingElement obj in list)
        {
            GameObject gObject;

            if(floorTag == FloorTag.none || floorTag == obj.floorTag)
            {
                if (index >= childCount) gObject = Instantiate(cell, listTransform);
                else
                {
                    gObject = listTransform.GetChild(index).gameObject;
                    gObject.SetActive(true);
                    index++;
                }
                gObject.transform.GetChild(1).GetComponent<Image>().sprite = obj.image;
                gObject.transform.GetChild(0).GetComponent<Text>().text = obj.name + "\n<color=#20E600> " + obj.price + "</color>";
                gObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    GameManager.instance.gridManager.SetWall(obj.Id, obj.price);
                    for (int i = 0; i < listTransform.transform.childCount; i++)
                    {
                        listTransform.transform.GetChild(i).GetComponent<Image>().color = orange;
                    }
                    GameManager.instance.gridManager.isSelected = selected;
                    gObject.GetComponent<Image>().color = Color.white;
                });


            }
        }
        
    }

    private void ClearList()
    {   
        for (int i = 0; i < listTransform.transform.childCount; i++)
        {
            listTransform.transform.GetChild(i).GetComponent<Image>().color = orange;
            listTransform.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void ClearButtons()
    {
        wallButton.GetComponent<Image>().color = orange;
        wallButton.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        floorButton.GetComponent<Image>().color = orange;
        floorButton.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        doorButton.GetComponent<Image>().color = orange;
        doorButton.transform.GetChild(0).GetComponent<Text>().color = Color.white;
    }

}
