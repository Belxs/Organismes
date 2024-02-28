using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreateOrganism : MonoBehaviour
{
    public string gen;
    public bool random;
    public bool good;
    public Toggle onTog;
    public void Create()
    {
        good = true;
        onTog.isOn = false;

    }
    public void SetGen(string g)
    {
        gen = g;
       
    }
    public void SetRandom(bool trfl)
    {
        random = trfl;
    }
    void Awake()
    {
        good = false;
    }

    
    void Update()
    {
        if (good)
        {
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                GameObject prefab = Organism.organism_prefab;
                Organism org = Instantiate(prefab, ((Vector2)RayCastMouse.metka_s.transform.position), Quaternion.identity).GetComponent<Organism>();
                org.random = random;
                if(!random)
                org.NewRealizide(gen);
                org.good = true;
                org.Awake();
                good = false;
                RayCastMouse.org_inv_s.SetActive(good);
                onTog.isOn = true;
            }
        }
        RayCastMouse.org_inv_s.SetActive(good);
    }
}
