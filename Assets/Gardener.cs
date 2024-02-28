using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gardener : MonoBehaviour
{
    public static GameObject prefab_water, prefab_food;
    public static List<GameObject> Sups = new(),waters=new(),foods=new();
    void Start()
    {
        ExceptionPause.AddScript(this);
        Organism.LoadPrefab((GameObject)PrefabLoader.Load("Prefab", "Water"),ref prefab_water);
        Organism.LoadPrefab((GameObject)PrefabLoader.Load("Prefab", "Food"), ref prefab_food);
    }
    public static void Remove(GameObject me,string Name)
    {
        Sups.Remove(me);
        switch (Name)
        {
            case "Water":
                waters.Remove(me);
                break;
            case "Food":
                foods.Remove(me);
                break;
        }
    }
    public static void Add(GameObject me,string Name)
    {
        Sups.Add(me);
        switch (Name)
        {
            case "Water":
                waters.Add(me);
                break;
            case "Food":
                foods.Add(me);
                break;
        }
    }
    // Update is called once per frame
    public void SpawnSup(GameObject item, float stronght)
    {
        float size = (Field.Size-1) / 2f;
        float x =Random.Range(-size, size),y = Random.Range(-size, size);
        Vector2 pos = new(x, y);
        if(new System.Random().NextDouble() < stronght )
        {
            Instantiate(item, pos, Quaternion.identity);
        }
    }
    void FixedUpdate()
    {
        SpawnSup(prefab_food, 0.1f);
        SpawnSup(prefab_water, 0.1f);
    }
}
