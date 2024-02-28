using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackingInterface : MonoBehaviour
{
    
    [SerializeField]
    public LifeStats l=new();
    public TargetStats t = new();
    public GenStats g = new();
    public FetusStats f = new();
    public static Organism organism;
    public  Organism free_organism;
    public static BackingInterface me;
    public static bool clear;
    [Serializable]
    public struct LifeStats
    {
        [SerializeField]
        public TextMeshProUGUI life_life, life_food, life_water, life_speed;
        public void Nulling()
        {
            life_food.text = "";
            life_life.text = "";
            life_water.text = "";
            life_speed.text = "";
        }
        public void Action()
        {
            if (organism.life != null)
            {
                Interfade(life_life, organism.life.healt);
                Interfade(life_food, organism.life.food);
                Interfade(life_water, organism.life.water);
                Interfade(life_speed, organism.life.speed);
            }
        }

    }
    [Serializable]
    public struct FetusStats
    {
        [SerializeField]
        public TextMeshProUGUI Onfetus, timeoutFet, speedFetus;
        public void Nulling()
        {
            
            Onfetus.text = "Нет";
            timeoutFet.text = "";
            speedFetus.text = "";
        }
        public void Action()
        {
            if (organism.fetus!= null)
            {
                
                Interfade(Onfetus,"Есть");
                Interfade(timeoutFet, organism.fetus.timeout);
                Interfade(speedFetus,"F: "+ organism.fetus.speedF+'\n'+"W: "+organism.fetus.speedW);
            }
        }

    }
    [Serializable]
    public struct GenStats
    {
        public bool good;
        [SerializeField]
        public TextMeshProUGUI gender,cicl, min,max, fur,cicl_gen;
        public TextMeshProUGUI life,hungry,water,speed;
        public TextMeshProUGUI s_life, s_hungry, s_water, s_speed;
        public TextMeshProUGUI timeFet;
        public Image color,s_color;
        public void Nulling()
        {
           
        }
        public void Action()
        {
            if(organism.genesize)
            if (!good)
            {
                    Interfade(gender, organism.gender.ToString());
                    Interfade(cicl, organism.Cicles.ToString());
                    Interfade(min, (((organism.MinTemp / 9f - 0.5f) * 2 * 90 + 273)-273).ToString());
                    Interfade(max, (((organism.MaxTemp / 9f - 0.5f) * 2 * 90 + 273) - 273).ToString());
                    Interfade(fur, organism.parameters.m_Fu.ToString());
                    Interfade(cicl_gen, organism.GetBetweenCurrentCicl().toString());
                    Interfade(life, organism.life.standart.m_L);
                    Interfade(hungry, organism.life.standart.m_H);
                    Interfade(water, organism.life.standart.m_Q);
                    Interfade(speed, organism.parameters.m_S);
                    Interfade(s_life, (1 + organism.preferense.m_L * organism.preferense.m_L * 10f)/8F);
                    Interfade(s_hungry, (1 + organism.preferense.m_H * organism.preferense.m_H * 10f) / 8F);
                    Interfade(s_water, (1 + organism.preferense.m_Q * organism.preferense.m_Q * 10f) / 8F);
                    Interfade(s_speed, organism.preferense.m_S);
                    Interfade(timeFet, organism.preferense.m_Fu*10+1);
                    color.color = organism.parameters.MainColor;
                    s_color.color = organism.preferense.MainColor;
                    good =true;
            }
        }

    }
    public static void NewOrganizm(Organism org)
    {
        organism = null;
        me.g.good = false;
        clear = false;
        me.free_organism = null;
        organism = org;

    }
    [Serializable]
    public struct TargetStats
    {
        [SerializeField]
        public TextMeshProUGUI
        behaviour_target_fineltar,
        behaviour_target_pos,
        behaviour_target_determine,
        behaviour_target_element;
        public void Nulling()
        {
            behaviour_target_fineltar.text = "";
            behaviour_target_pos.text = "";
            behaviour_target_determine.text = "";
            behaviour_target_element.text = "";
        }
        public void Action()
        {
            if (organism.behaviour != null)
                if (organism.behaviour.target != null)
                {
                    Interfade(behaviour_target_fineltar, organism.behaviour.target.fineltar);
                    Interfade(behaviour_target_pos, organism.behaviour.target.pos);
                    if (organism.behaviour.target.element != null)
                        Interfade(behaviour_target_element, organism.behaviour.target.element.GetType());
                    if (organism.behaviour.target.determiner != null)
                        Interfade(behaviour_target_determine, organism.behaviour.target.determiner.Use() + " " + organism.behaviour.target.determiner.type);

                }
        }

    }
    public static void Interfade(TextMeshProUGUI text, object number)
    {
        if(text!=null)    
        {
            text.text = number.ToString();
        }
    }
    public void Interfades(TextMeshProUGUI text, object[] bodys, object number)
    {

        for (int i = 0; i < bodys.Length; i++)
        {
            if (bodys[i] == null) return;
        }
        text.text = number.ToString();


    }

    void LateUpdate()
    {
        BackingInterface.me = this;
        if (free_organism!=null)
        organism = free_organism;
        l.Nulling();
        t.Nulling();
        f.Nulling();
        if (organism != null)
        {
            l.Action();
            t.Action();
            g.Action();
            f.Action();

        }
    }
}


