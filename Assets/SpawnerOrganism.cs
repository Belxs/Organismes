using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOrganism : MonoBehaviour
{
    public static List<Organism> orgs = new(),male=new(),women=new(),germa=new(),beam=new();
    public static float BoundsOrgs=50,SpawnCount=50;
    public static bool MaxOrg;
    public bool spawn;
    public static CurrentCicl MatchDeepAnalise()
    {
        CurrentCicl sum=new();
        if (orgs.Count != 0)
        {
            float localcount = orgs.Count;
            for (int i = 0; i < orgs.Count; i++)
            {
                if ((orgs[i].life.dead)&(orgs[i].Cicles>0))
                {
                    localcount--;
                    continue;
                }
                sum += orgs[i].GetBetweenCurrentCicl();
            }
            sum /= localcount;
        }
        return sum;
    }
    void Start()
    {
        ExceptionPause.AddScript(this);
        if (spawn)
        Spawn((int)SpawnCount);
    }
    public static void AddStatistic(Organism obj)
    {
        orgs.Add(obj);
        switch (obj.gender)
        {
            case Gender.M:
                male.Add(obj);
                break;
            case Gender.W:
                women.Add(obj);
                break;
            case Gender.G:
                germa.Add(obj);
                break;
            case Gender.B:
                beam.Add(obj);
                break;
        }
        
    }
    public static void RemoveStatistic(Organism obj)
    {
        orgs.Remove(obj);
        switch (obj.gender)
        {
            case Gender.M:
                male.Remove(obj);
                break;
            case Gender.W:
                women.Remove(obj);
                break;
            case Gender.G:
                germa.Remove(obj);
                break;
            case Gender.B:
                beam.Remove(obj);
                break;
        }

    }
    public void Spawn(int count)
    {
        Organism.LoadAllPrefabs();
        for (int i = 0; i < count; i++)
        {
            float size = (Field.Size - 1) / 2f;
            float x = Random.Range(-size, size), y = Random.Range(-size, size);
            Vector2 pos = new(x, y);

           Organism org= Instantiate(Organism.organism_prefab, pos, Quaternion.identity).GetComponent<Organism>();
            //orgs.Add(org.gameObject);
           org.random = true;
           org.good = true;
           org.Awake();


        }
        
    }
    // Update is called once per frame
    public void Remoute(List<Organism> list,int index)
    {
        if (index < list.Count)
        {
            if (list[index] == null) list.RemoveAt(index);
        }
    }
    public void ClearNull()
    {
        for(int i = 0; i < orgs.Count; i++)
        {
            if (orgs[i] == null) orgs.RemoveAt(i);
            Remoute(male, i);
            Remoute(women, i);
            Remoute(germa, i);
            Remoute(beam, i);
        }
    }
    void Update()
    {
        ClearNull();
        if (orgs.Count == 0) Spawn((int)SpawnCount);
        MaxOrg = (orgs.Count >= BoundsOrgs) ;
    }
}
