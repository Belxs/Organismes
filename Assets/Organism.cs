using System.Collections.Generic;
using UnityEngine;

public class Organism : MonoBehaviour, IElement
{
    public bool callStop;
    public string Gen;
    public Gender gender;
    public float MaxTemp, MinTemp;
    public int Cicles;
    public List<CurrentCicl> Cicls = new();
    public CurrentCicl currentCicl;
    public int stepCicl;
    public Preferense parameters = new(), preferense = new();
    public Cell currentCell;
    public Life life;
    public Memory memory;
    public Vision vision;
    public Detector detector;
    public PhysicOrganel physic;
    public OrgaBehaviour behaviour;
    public float timeoutStep, timeStep = 1,
        timeoutSpar, timeSpar = 60f;
    public static GameObject organism_prefab, Love_prefab, Atack_prefab, Damage_prefab;
    public SpriteRenderer render;
    public LateOutToDestroy ld;
    public static int MaxInPutGens = 21;
    public static float Mutanting = 0.003f;
    public const float BetweenDelta = 33.70119f;
    public Fetus fetus = null;
    public bool random;
    public bool good = false, genesize = false;
    public static bool SpeedSeed = false;
    public int FactLenghtCicle;
    public static bool LoadedPrefabs;
    public float FactSpeed;
    public int GetFLC()
    {
        int l = 0;
        for (int i = 0; i < Gen.Length; i++)
        {
            if (Gen[i] == 's') l++;
        }

        return l;
    }
    public string RandomName()
    {
        char[] naming = new char[Random.Range(3, 6)];
        string result="";
        for(int i = 0; i < naming.Length; i++)
        {
            naming[i] = (char)Random.Range(97, 123);
            result += naming[i];
        }
        return result;
    }
    public string GetLastChainGen()
    {
        string last = "";
        for (int i = 0; i < Gen.Length; i++)
        {
            if (Gen[^(i + 1)] != 's')
            {
                last = Gen[^(i + 1)] + last;
            }
            else return last;

        }
        return "null";
    }
    public void Dead()
    {
        //life = null;
        if (ld == null)
        {
            ld = GetComponent<LateOutToDestroy>();
        }
        ld.LateDestroy(3f);

    }
    public void OnDestroy()
    {
        SpawnerOrganism.RemoveStatistic(this);
      
            ExceptionPause.RemoveScript(this);
        
    }
    public float GetTemperature()
    {
        if (currentCell != null)
        {
            return currentCell.temperatura;
        }
        else return float.NaN;
    }
    public float frizetemp;
    public void NewRealizide(string gen)
    {
        Gen = gen;
        random = false;
    }
    public CurrentCicl GetBetweenCurrentCicl()
    {
        float XFP = 0, XQP = 0, XWP = 0, XEP = 0,
        XFA = 0, XQA = 0, XWA = 0, XEA = 0,
        CFA = 0, CQA = 0, CWA = 0, CEA = 0,
         HG = 0, QG = 0, LG = 0,
        VFA = 0, VQA = 0, VWA = 0, VEA = 0;
        CurrentCicl result = new();
        for (int i = 0; i < Cicles; i++)
        {
            CurrentCicl c = Cicls[i];
            XFP += c.XFP;
            XQP += c.XQP;
            XWP += c.XWP;
            XEP += c.XEP;
            XFA += c.XFA;
            XQA += c.XQA;
            XWA += c.XWA;
            XEA += c.XEA;
            CFA += c.CFA;
            CQA += c.CQA;
            CWA += c.CWA;
            CEA += c.CEA;
            HG += c.HG;
            QG += c.QG;
            LG += c.LG;
            VFA += c.VFA;
            VQA += c.VQA;
            VWA += c.VWA;
            VEA += c.VEA;
        }

        XFP /= Cicles;
        XQP /= Cicles;
        XWP /= Cicles;
        XEP /= Cicles;
        XFA /= Cicles;
        XQA /= Cicles;
        XWA /= Cicles;
        XEA /= Cicles;
        CFA /= Cicles;
        CQA /= Cicles;
        CWA /= Cicles;
        CEA /= Cicles;
        HG /= Cicles;
        QG /= Cicles;
        LG /= Cicles;
        VFA /= Cicles;
        VQA /= Cicles;
        VWA /= Cicles;
        VEA /= Cicles;

        result.XFP = Mathf.RoundToInt(XFP);
        result.XQP = Mathf.RoundToInt(XQP);
        result.XWP = Mathf.RoundToInt(XWP);
        result.XEP = Mathf.RoundToInt(XEP);
        result.XFA = Mathf.RoundToInt(XFA);
        result.XQA = Mathf.RoundToInt(XQA);
        result.XWA = Mathf.RoundToInt(XWA);
        result.XEA = Mathf.RoundToInt(XEA);
        result.CFA = Mathf.RoundToInt(CFA);
        result.CQA = Mathf.RoundToInt(CQA);
        result.CWA = Mathf.RoundToInt(CWA);
        result.CEA = Mathf.RoundToInt(CEA);
        result.HG = Mathf.RoundToInt(HG);
        result.QG = Mathf.RoundToInt(QG);
        result.LG = Mathf.RoundToInt(LG);
        result.VFA = Mathf.RoundToInt(VFA);
        result.VQA = Mathf.RoundToInt(VQA);
        result.VWA = Mathf.RoundToInt(VWA);
        result.VEA = Mathf.RoundToInt(VEA);
        return result;
    }
    public void Awake()
    {
        ExceptionPause.AddScript(this);
        timeoutSpar = timeSpar;
        if (good)
        {
            Init();
            ReadGen();
            life = new Life(parameters, this);
            behaviour = new(memory, currentCicl, life);
            render.color = parameters.MainColor;
            Action();
            genesize = true;
            SpawnerOrganism.AddStatistic(this);
        }



    }
    public static void LoadPrefab(GameObject prefab, ref GameObject staticprefab)
    {
        if (staticprefab == null)
        {
            if (prefab != null)
            {
                staticprefab = prefab;
            }
        }
    }
    public static void LoadAllPrefabs()
    {
        if (!LoadedPrefabs)
        {
            LoadedPrefabs = true;
            LoadPrefab((GameObject)PrefabLoader.Load("Prefab", "Organism"), ref organism_prefab);
            LoadPrefab((GameObject)PrefabLoader.Load("Prefab", "Love"), ref Love_prefab);
            LoadPrefab((GameObject)PrefabLoader.Load("Prefab", "Damage"), ref Damage_prefab);
            LoadPrefab((GameObject)PrefabLoader.Load("Prefab", "Atack"), ref Atack_prefab);
        }
    }
    public void Start()
    {
        LoadAllPrefabs();


    }
    void FixedUpdate()
    {
        if (good)
        {
            if (life != null)
            {
                Action();
                timeoutStep -= Time.deltaTime;
                if (timeoutStep <= 0)
                {
                    timeoutStep = timeStep;
                    GoStep();
                }
            }
            else Dead();
        }
    }
    public void GoStep()
    {
        if (stepCicl < Cicles - 1)
        {
            stepCicl++;
        }
        else stepCicl = 0;
    }

    public static string ReplicityGen(string mother, string father)
    {
        string resultGen = "";
        System.Random random;
        int max = Mathf.Max(mother.Length, father.Length);
        //Debug.Log("max: " + max);
        int min = Mathf.Min(mother.Length, father.Length);
        // Debug.Log("min: " + min);
        for (int i = 0; i < max; i++)
        {
            random = new System.Random();
            float value;
            if (father != mother)
            {

                if (i < 21) { value = 1; } else value = 1;
                if (i < min)
                {

                    if (random.NextDouble() > Organism.Mutanting / value)
                    {
                        if (random.NextDouble() > 0.5f)
                        {
                            resultGen += mother[i];
                        }
                        else
                        {
                            resultGen += father[i];
                        }
                    }
                    else
                    {
                        if ((mother[i] != 's') & (father[i] != 's'))
                        {
                            resultGen += random.Next(0, 10);
                        }
                        else resultGen += 's';
                    }
                }
                else
                {
                    if (max == father.Length)
                    {
                        resultGen += father[i];
                    }
                    else
                    {
                        if (max == mother.Length)
                        {
                            resultGen += mother[i];
                        }
                    }
                }
            }
            else
            {
                if (i < 21) { value = 1; } else value = 1;
                if (random.NextDouble() > Organism.Mutanting / value)
                {
                    resultGen += mother[i];


                }
                else
                {
                    if (mother[i] != 's')
                    {
                        resultGen += random.Next(0, 10);
                    }
                    else resultGen += 's';
                }
            }
        }
        if (resultGen.Length != max) { Debug.LogError(resultGen.Length + "!=" + max + "||Сбитый формат гена: " + resultGen); }
        return resultGen;


    }
    public void Fertilization(string sendgen, Organism male)

    {
        if (!SpawnerOrganism.MaxOrg)
            if (fetus == null)
            {
                CallStop();
                if (male != this)
                {

                    float similar = 1f - Preferense.Similarity(preferense, male.preferense);
                    //Debug.Log(15 + "similar: " + similar);
                    if (similar > 0.5f)
                    {
                        // Debug.Log(16 + "Fetus!");
                        CreateParticle(Love_prefab);
                        fetus = new Fetus(Gen, sendgen, this, male);
                    }
                }
                else
                {
                    CreateParticle(Love_prefab);
                    fetus = new Fetus(Gen, sendgen, this, male);
                }
            }
    }
    public void CreateParticle(GameObject prefab)
    {
        Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<AutoTimeDestroy>().SetPos(transform);
    }
    public void SubOrgaSpeed()
    {
        Target target = behaviour.target;
        if (target.determiner.type[0] == 'C')
        {
            Vector2 dir;
            switch (target.determiner.Use())
            {
                case "Run":
                    //Debug.Log(6.5 + target.fineltar);
                    target.DistanceComplete(transform.position, 1, '<');
                    dir = (target.pos - (Vector2)transform.position).normalized * life.speed;
                    physic.Move(-dir);
                    break;
                case "UnRun":
                    target.DistanceComplete(transform.position, vision.GetVisionZone() * 2, '>');
                    dir = (target.pos - (Vector2)transform.position).normalized * -life.speed;
                    physic.Move(dir);
                    break;
                case "Atack":
                    if (target.element.GetType() == typeof(Organism))
                    {
                        Organism enemy = (Organism)target.element;
                        if (target.fineltar != "Selection")
                        {
                            if (enemy != null)
                                if (enemy.life != null)
                                {

                                    enemy.CreateParticle(Damage_prefab);
                                    enemy.life.Damaged(1);
                                    CreateParticle(Atack_prefab);
                                    life.food += 0.35f;
                                    life.water += 0.65f;
                                }
                        }
                        else
                        {
                            if (enemy != null)
                                if (enemy.life != null)
                                {

                                    enemy.CreateParticle(Damage_prefab);
                                    enemy.life.Damaged(5);
                                    CreateParticle(Atack_prefab);
                                    if (enemy.life.healt > 0)
                                    {
                                        IElement ie = enemy;
                                        ActorTarget at = behaviour.SetActor(ie, "V", 0);
                                        behaviour.CreateTarget(enemy.transform.position, ie, at, "Atack");
                                    }

                                }
                        }

                    }
                    target.complete = true;
                    break;
                case "Spar":
                    //Debug.Log(10+"Spar "+gender);
                    if (target.element.GetType() == typeof(Organism))
                    {
                        switch (gender)
                        {
                            case Gender.M:
                                //Debug.Log(((Organism)target.element) == this);
                                InSemination((Organism)target.element);
                                //Debug.Log(11 + "m");
                                break;
                            case Gender.B:
                                Fertilization(Gen, this);
                                //((Organism)target.element).Fertilization(((Organism)target.element).Gen, this);
                                break;
                            case Gender.G:
                                InSeminationGermahrodite((Organism)target.element);
                                break;


                        }

                    }
                    target.complete = true;
                    break;


                case "Eat":
                    if (target.element.GetType() == typeof(Food))
                    {
                        Food food = (Food)target.element;
                        if (!food.ld.destroy)
                        {
                            life.food += food.nutrition;
                            food.ld.LateDestroy(1f);
                        }

                    }
                    target.complete = true;
                    break;
                case "Drink":
                    if (target.element.GetType() == typeof(Water))
                    {
                        Water water = (Water)target.element;
                        if (!water.ld.destroy)
                        {
                            life.water += water.nutrition;
                            water.ld.LateDestroy(1f);
                        }

                    }
                    target.complete = true;
                    break;
                default:

                    target.complete = true;
                    behaviour.DefaultBehaviour();
                    break;

            }
        }
    }
    public void OrganizationTarget()
    {
        Target target = behaviour.target;
        if (target != null)
        {
            if (!target.complete)
            {
                if (target.determiner != null)
                {
                    if (target.determiner.type[0] == 'V')
                    {
                        Vector2 dir;
                        switch (target.determiner.Use())
                        {
                            case "Run":
                                //  Debug.Log(target.GetDist(transform.position) + " lim: " +1);
                                //  Debug.Log(6.1 + target.fineltar);
                                target.DistanceComplete(transform.position, 1, '<');
                                // Debug.Log(6.1F +" - "+ target.GetDist(transform.position));
                                // Debug.Log(6.2f + " - " + target.complete);
                                dir = (target.pos - (Vector2)transform.position).normalized * life.speed;
                                physic.Move(dir);
                                if (target.complete)
                                {
                                    if (target.fineltar != "none")
                                    {
                                        if (behaviour.SummonTarget(target.fineltar))
                                            SubOrgaSpeed();
                                    }
                                }
                            
                                break;
                            case "UnRun":
                                //Debug.Log(6.3 + target.fineltar);
                                target.DistanceComplete(transform.position, vision.GetVisionZone(), '>');
                                dir = (target.pos - (Vector2)transform.position).normalized * -life.speed;
                                physic.Move(dir);
                                break;
                            default:
                                target.complete = true;
                                break;
                        }
                    }
                    else
                    {
                        if (target.determiner.type[0] == 'C')
                        {
                            Vector2 dir;
                            switch (target.determiner.Use())
                            {
                                case "Run":
                                    //Debug.Log(6.5 + target.fineltar);
                                    target.DistanceComplete(transform.position, 1, '<');
                                    dir = (target.pos - (Vector2)transform.position).normalized * life.speed;
                                    physic.Move(-dir);
                                    break;
                                case "UnRun":
                                    target.DistanceComplete(transform.position, vision.GetVisionZone() * 2, '>');
                                    dir = (target.pos - (Vector2)transform.position).normalized * -life.speed;
                                    physic.Move(dir);
                                    break;
                                case "Atack":
                                    if (target.element.GetType() == typeof(Organism))
                                    {
                                        Organism enemy = (Organism)target.element;
                                        if (target.fineltar != "Selection")
                                        {
                                            if (enemy != null)
                                                if (enemy.life != null)
                                                {

                                                    enemy.CreateParticle(Damage_prefab);
                                                    enemy.life.Damaged(1);
                                                    CreateParticle(Atack_prefab);
                                                    life.food += 0.35f;
                                                    life.water += 0.65f;
                                                }
                                        }
                                        else
                                        {
                                            if (enemy != null)
                                                if (enemy.life != null)
                                                {

                                                    enemy.CreateParticle(Damage_prefab);
                                                    enemy.life.Damaged(5);
                                                    CreateParticle(Atack_prefab);
                                                    if (enemy.life.healt > 0)
                                                    {
                                                        IElement ie = enemy;
                                                        ActorTarget at = behaviour.SetActor(ie, "V", 0);
                                                        behaviour.CreateTarget(enemy.transform.position, ie, at, "Atack");
                                                    }
                                                    
                                                }
                                        }

                                    }
                                    target.complete = true;
                                    break;
                                case "Spar":
                                    //Debug.Log(10+"Spar "+gender);
                                    if (target.element.GetType() == typeof(Organism))
                                    {
                                        switch (gender)
                                        {
                                            case Gender.M:
                                                //Debug.Log(((Organism)target.element) == this);
                                                InSemination((Organism)target.element);
                                                //Debug.Log(11 + "m");
                                                break;
                                            case Gender.B:
                                                Fertilization(Gen, this);
                                                //((Organism)target.element).Fertilization(((Organism)target.element).Gen, this);
                                                break;
                                            case Gender.G:
                                                InSeminationGermahrodite((Organism)target.element);
                                                break;


                                        }

                                    }
                                    target.complete = true;
                                    break;


                                case "Eat":
                                    if (target.element.GetType() == typeof(Food))
                                    {
                                        Food food = (Food)target.element;
                                        if (!food.ld.destroy)
                                        {
                                            life.food += food.nutrition;
                                            food.ld.LateDestroy(1f);
                                        }

                                    }
                                    target.complete = true;
                                    break;
                                case "Drink":
                                    if (target.element.GetType() == typeof(Water))
                                    {
                                        Water water = (Water)target.element;
                                        if (!water.ld.destroy)
                                        {
                                            life.water += water.nutrition;
                                            water.ld.LateDestroy(1f);
                                        }

                                    }
                                    target.complete = true;
                                    break;
                                default:

                                    target.complete = true;
                                    behaviour.DefaultBehaviour();
                                    break;

                            }
                        }
                    }
                }
                else
                {
                    //Debug.Log(target.GetDist(transform.position) + " lim: " + 0.5f);
                    if (life != null)
                    {
                        target.DistanceComplete(transform.position, 0.9f, '<');
                        Vector2 dir = (target.pos - (Vector2)transform.position).normalized * life.speed;
                        physic.Move(dir);
                    }
                }
            }
            else
            {
                // Debug.Log(target.fineltar);
                behaviour.target = null;
            }
        }
    }
    public void InSemination(Organism female)
    {
        float similar = 1f - Preferense.Similarity(preferense, female.preferense);
        //Debug.Log("fem:" + female.gender);
        switch (female.gender)
        {
            case Gender.W:
                //Debug.Log(12 + "similar: "+ similar);
                if (similar > 0.5f)
                {
                    //Debug.Log(13 + "Fert");
                    female.Fertilization(Gen, this);
                }




                break;
        }
    }
    public void InSeminationGermahrodite(Organism female)
    {
        float similar = 1f - Preferense.Similarity(preferense, female.preferense);
        switch (female.gender)
        {
            case Gender.G:
                if (similar > 0.5f)
                {
                    female.Fertilization(Gen, this);
                }
                break;
           
        }
    }
    public void Action()
    {
        if (life != null)
        {
            if (SpeedSeed)
            {
                timeoutSpar -= Time.deltaTime;
                if (timeoutSpar <= 0) { Dead(); }
            }
            //Debug.Log(Cicls.Count+" :Cicls");
            if (Cicls.Count != 0)
                currentCicl = Cicls[stepCicl];
            currentCell = Field.Get_Is_World_to_Pos(transform.position);
            // currentCell.temperatura = frizetemp;
            physic.Action();
            FactSpeed = physic.GetFactSpeed();
            if (life != null)
                life.Action();
            if (fetus != null) fetus.Action();
            if (life != null)
                physic.maxspeed = life.speed;
            if (Cicls.Count != 0)
                behaviour.SetCicl(currentCicl);
            if (Cicls.Count != 0)
                behaviour.Action();
            if (Cicls.Count != 0)
                OrganizationTarget();

            physic.ControlledSpeed();
            if (FactSpeed < 0.5f)
            {
                if (new System.Random().NextDouble() > 0.99f)
                {
                    if (behaviour.target != null)
                        behaviour.target.complete = true;
                }
            }
            if (life != null)
                if (Cicls.Count != 0)
                    if (life.dead)
                        L(currentCicl.toString());
            L(currentCell.toString());
            if (life.dead) Dead();
        }

    }
    public int GetT(int i)
    {
        return int.Parse(Gen[i].ToString());
    }
    public static int GetLocalT(int i, string gen)
    {
        return int.Parse(gen[i].ToString());
    }

    public static void L(string s)
    {
        //  if (s != "")
        //Debug.Log(s);
    }
    //0 01 2222 100 7943 817 1915 s 3611 0000 4513 537 0010
    //X-XX-XXXX XXX-XXXX XXX-XXXX-s-XXXX-XXXX-XXXX-XXX-XXXX
    public void RepeatChain(string chain, int count)
    {
        for (int i = 0; i < count; i++)
            Gen += "s" + chain;
    }
    public void CallStop()
    {
        if (callStop)
        {

        }
    }
    public void ReadGen()
    {
        int cilse = 0;
        float r = 0, g = 0, b = 0;
        int indexc = 0;
        string gencode = "";
        FactLenghtCicle = GetFLC();
        for (int i = 0; i < Gen.Length; i++)
            if (Gen[i] != 's')
            {
                string def = "";

                Color color;
                if (indexc == 0)
                {
                    switch (i)
                    {
                        case 0:
                            def += "Пол" + ": ";
                            int d = GetT(i);
                            if (d > 3) d -= 4;
                            if (d > 3) d -= 4;
                            switch (d)
                            {
                                case 0:
                                    def += "М";
                                    gender = Gender.M;
                                    break;
                                case 1:
                                    def += "Ж";
                                    gender = Gender.W;
                                    break;
                                case 2:
                                    def += "Б";
                                    gender = Gender.B;
                                    break;
                                case 3:
                                    def += "Г";
                                    gender = Gender.G;
                                    break;
                            }

                            break;
                        case 1:
                            //def += "Пол" + ": ";
                            //Debug.Log("-="+cilse);
                            // Debug.Log(GetT(i));
                            cilse += GetT(i) * 10;
                            // Debug.Log(cilse+"=-");

                            break;
                        case 2:
                            def += "Циклов" + ": ";
                            // Debug.Log("-=" + cilse);
                            // Debug.Log(GetT(i));
                            cilse += GetT(i);
                            // Debug.Log(cilse + "=-");
                            if (cilse > FactLenghtCicle)
                            {
                                //Debug.Log(cilse + ">"+FactLenghtCicle);
                                RepeatChain(GetLastChainGen(), cilse - FactLenghtCicle);
                                //cilse = FactLenghtCicle;
                            }
                            def += cilse;

                            break;
                        case 3:
                            def += "Макс. еды" + ": ";

                            parameters.m_H = GetT(i);
                            def += GetT(i);

                            break;
                        case 4:
                            def += "Макс. воды" + ": ";
                            parameters.m_Q = GetT(i);
                            def += GetT(i);

                            break;
                        case 5:
                            def += "Макс. здоровья" + ": ";
                            parameters.m_L = GetT(i);
                            def += GetT(i);

                            break;

                        case 6:
                            def += "Макс. скорости" + ": ";
                            parameters.m_S = GetT(i);
                            def += GetT(i);

                            break;
                        case 7:


                            r = GetT(i);

                            break;
                        case 8:


                            g = GetT(i);

                            break;
                        case 9:


                            b = GetT(i);
                            color = new Color(1f - r / 9f, 1f - g / 9f, 1f - b / 9f);
                            parameters.MainColor = color;

                            def += color.ToString();
                            break;
                        case 10:
                            def += "Парт.Макс. еды" + ": ";
                            preferense.m_H = GetT(i);
                            def += GetT(i);

                            break;
                        case 11:
                            def += "Парт.Макс. воды" + ": ";
                            preferense.m_Q = GetT(i);
                            def += GetT(i);

                            break;
                        case 12:
                            def += "Парт.Макс. здоровья" + ": ";
                            preferense.m_L = GetT(i);
                            def += GetT(i);

                            break;

                        case 13:
                            def += "Парт.Макс. скорости" + ": ";
                            preferense.m_S = GetT(i);
                            def += GetT(i);

                            break;
                        case 14:


                            r = GetT(i);

                            break;
                        case 15:


                            g = GetT(i);

                            break;
                        case 16:


                            b = GetT(i);
                            color = new Color(1f - r / 9f, 1f - g / 9f, 1f - b / 9f);
                            preferense.MainColor = color;
                            def += color.ToString();
                            break;
                        case 17:
                            def += "Макс. темп" + ": ";
                            MaxTemp = GetT(i);
                            def += MaxTemp;
                            break;
                        case 18:
                            def += "Мин. темп" + ": ";
                            MinTemp = GetT(i);
                            def += MinTemp;
                            break;
                        case 19:
                            def += "Уровень меха" + ": ";
                            parameters.m_Fu = GetT(i);
                            def += parameters.m_Fu;
                            break;
                        case 20:
                            def += "Парт. Уровень меха" + ": ";
                            preferense.m_Fu = GetT(i);
                            def += preferense.m_Fu;
                            break;
                    }
                }
                else
                {
                    gencode += GetT(i);
                }
                L(def);
            }
            else
            {

                indexc++;
                if (indexc > 1)
                {
                    Cicls.Add(new CurrentCicl(gencode));
                }
                gencode = "";
            }
        if (gencode != "")
            Cicls.Add(new CurrentCicl(gencode));
        Cicles = cilse;
    }
    public static float InTime(float value)
    {
        return value * Time.deltaTime * BetweenDelta;
    }
    public void Init()
    {
        name = RandomName();
        render = GetComponent<SpriteRenderer>();
        ElementInfo ei = GetElementInfo();
        physic = gameObject.AddComponent<PhysicOrganel>();
        string Buf = "";
        memory = new();
        if (random)
        {

            /*0*/
            Buf += Random.Range(0, 3);

            /*1*/
            Buf += Random.Range(0, 10);
            /*2*/
            Buf += Random.Range(0, 10);

            Buf += Random.Range(0, 10);
            Buf += Random.Range(0, 10);
            Buf += Random.Range(0, 10);
            Buf += Random.Range(0, 10);

            Buf += Random.Range(0, 10);
            Buf += Random.Range(0, 10);
            Buf += Random.Range(0, 10);
            //---
            Buf += Random.Range(0, 10);
            Buf += Random.Range(0, 10);
            Buf += Random.Range(0, 10);
            Buf += Random.Range(0, 10);

            Buf += Random.Range(0, 10);
            Buf += Random.Range(0, 10);
            Buf += Random.Range(0, 10);
            //---
            Buf += Random.Range(0, 10);
            Buf += Random.Range(0, 10);
            Buf += Random.Range(0, 10);
            Buf += Random.Range(0, 10);
            int cikles = int.Parse(Buf[1].ToString()) * 10 + int.Parse(Buf[2].ToString());
            for (int i = 0; i < cikles; i++)
            {
                Buf += "s";
                //XFP
                Buf += Random.Range(0, 10);
                //XQP
                Buf += Random.Range(0, 10);
                //XWP
                Buf += Random.Range(0, 10);
                //XEP
                Buf += Random.Range(0, 10);

                //---
                //XFA
                Buf += Random.Range(0, 3);
                //XQA
                Buf += Random.Range(0, 3);
                //XWA
                Buf += Random.Range(0, 3);
                //XEA
                Buf += Random.Range(0, 3);
                //---
                //CFA
                int[] s = { 0, 1, 4 };
                Buf += s[Random.Range(0, 3)];
                //CQA
                int[] s1 = { 0, 1, 5 };
                Buf += s1[Random.Range(0, 3)];
                //CWA
                int[] s2 = { 0, 1, 6 };
                Buf += s2[Random.Range(0, 3)];
                //CEA
                Buf += Random.Range(0, 6);
                //---
                //HG
                Buf += Random.Range(0, 10);
                //QG
                Buf += Random.Range(0, 10);
                //LG
                Buf += Random.Range(0, 10);
                //---
                //VFA
                Buf += Random.Range(0, 3);
                //VQA
                Buf += Random.Range(0, 3);
                //VWA
                Buf += Random.Range(0, 3);
                //VEA
                Buf += Random.Range(0, 3);
            }
            Gen = Buf;
        }


    }

    public ElementInfo GetElementInfo()
    {
        return new ElementInfo(this);
    }
}
public enum Gender
{
    M,
    W,
    B,
    G
}
[System.Serializable]
public class CurrentCicl
{
    public float XFP, XQP, XWP, XEP,
         XFA, XQA, XWA, XEA,
         CFA, CQA, CWA, CEA,
          HG, QG, LG,
         VFA, VQA, VWA, VEA;
    public CurrentCicl (
        float XFP, float XQP, float XWP,
        float XEP, float XFA, float XQA,
        float XWA, float XEA, float CFA,
        float CQA, float CWA, float CEA,
        float HG, float QG, float LG,
        float VFA, float VQA, float VWA, float VEA)
    {
        this.XFP = XFP;
        this.XQP = XQP;
        this.XWP = XWP;
        this.XEP = XEP;
        this.XFA = XFA;
        this.XQA = XQA;
        this.XWA = XWA;
        this.XEA = XEA;
        this.CFA = CFA;
        this.CQA = CQA;
        this.CWA = CWA;
        this.CEA = CEA;
        this.HG = HG;
        this.QG = QG;
        this.LG = LG;
        this.VFA = VFA;
        this.VQA = VQA;
        this.VWA = VWA;
        this.VEA = VEA;
    }
    public static CurrentCicl operator +(CurrentCicl c1,CurrentCicl c2)
    {
        return new CurrentCicl(
         c1.XFP + c2.XFP, c1.XQP + c2.XQP, c1.XWP + c2.XWP, c1.XEP + c2.XEP,
         c1.XFA + c2.XFA, c1.XQA + c2.XQA, c1.XWA + c2.XWA, c1.XEA + c2.XEA,
         c1.CFA + c2.CFA, c1.CQA + c2.CQA, c1.CWA + c2.CWA, c1.CEA + c2.CEA,
          c1.HG + c2.HG, c1.QG +   c2.QG,  c1.LG +  c2.LG,
         c1.VFA + c2.VFA, c1.VQA + c2.VQA, c1.VWA + c2.VWA, c1.VEA + c2.VEA);
    }
    public static CurrentCicl operator /(CurrentCicl c1, float c2)
    {
        return new CurrentCicl(
         c1.XFP /c2, c1.XQP / c2, c1.XWP / c2, c1.XEP / c2,
         c1.XFA / c2, c1.XQA / c2, c1.XWA / c2, c1.XEA / c2,
         c1.CFA / c2, c1.CQA / c2, c1.CWA / c2, c1.CEA / c2,
          c1.HG / c2, c1.QG / c2, c1.LG / c2,
         c1.VFA / c2, c1.VQA / c2, c1.VWA / c2, c1.VEA / c2);
    }
    public CurrentCicl(string s)
    {
        XFP = Get(s, 0);
        XQP = Get(s, 1);
        XWP = Get(s, 2);
        XEP = Get(s, 3);
        XFA = Get(s, 4);
        XQA = Get(s, 5);
        XWA = Get(s, 6);
        XEA = Get(s, 7);
        CFA = Get(s, 8);
        CQA = Get(s, 9);
        CWA = Get(s, 10);
        CEA = Get(s, 11);
        HG = Get(s, 12);
        QG = Get(s, 13);
        LG = Get(s, 14);
        VFA = Get(s, 15);
        VQA = Get(s, 16);
        VWA = Get(s, 17);
        VEA = Get(s, 18);

    }
    public CurrentCicl() { }
    public int Get(string s, int i)
    {
        return int.Parse(s[i].ToString());
    }
    public string toString()
    {


        return
        " XFP: " + XFP +
        " XQP: " + XQP + '\n' +
        " XWP: " + XWP +
        " XEP: " + XEP + '\n' +
        " XFA: " + XFA +
        " XQA: " + XQA + '\n' +
        " XWA: " + XWA +
        " XEA: " + XEA + '\n' +
        " CFA: " + new ActorTarget(CFA, "C").Use() +
        " CQA: " + new ActorTarget(CQA, "C").Use() + '\n' +
        " CWA: " + new ActorTarget(CWA, "C").Use() +
        " CEA: " + new ActorTarget(CEA, "C").Use() + '\n' +
        " HG: " + HG +
        " QG: " + QG + '\n' +
        " LG: " + LG +
        " VFA: " + new ActorTarget(VFA, "V").Use() + '\n' +
        " VQA: " + new ActorTarget(VQA, "V").Use() +
        " VWA: " + new ActorTarget(VWA, "V").Use() + '\n' +
        " VEA: " + new ActorTarget(VEA, "V").Use();
    }
}
[System.Serializable]
public class Memory
{
    public List<MemoryCell> cells = new();
    public void Save(GameObject obj)
    {
        cells.Add(new MemoryCell(obj));
    }
    public void Save(GameObject obj, Target target)
    {
        cells.Add(new MemoryCell(obj, target));

    }
    public void Save(GameObject obj, Target target, Vector2 pos)
    {
        cells.Add(new MemoryCell(obj, target, pos));
    }
    public void Save(GameObject obj, Vector2 pos)
    {
        cells.Add(new MemoryCell(obj, pos));
    }
    public void Save(Target target, Vector2 pos)
    {
        cells.Add(new MemoryCell(target, pos));
    }
    public Vector2 Middle()
    {
        Vector2 sum = Vector2.zero, middle = Vector2.zero;
        float count = 0;

        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].pos == Vector2.zero) continue;
            sum += cells[i].pos;
            count++;
        }
        if (count > 0)
            middle = new Vector2(sum.x / count, sum.y / count);
        return middle;
    }
    public MemoryCell[] Load(TypeElement type)
    {
        List<MemoryCell> buffer = new();
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].EI.typeElement == type)
            {
                buffer.Add(cells[i]);
            }
        }
        return buffer.ToArray();
    }

    /*  public Vector2 SearchCloseElement(string name, Vector2 current)
      {
          List<Vector2> buffer = new();
          for (int i = 0; i < cells.Count; i++)
          {
              if (cells[i].EI != null)
                  if (cells[i].EI.GetType().Name == name)
                  {
                      buffer.Add(cells[i].pos);

                  }

          }

          Vector2 v = new(float.MaxValue, float.MaxValue);

          for (int i = 0; i < buffer.Count; i++)
          {
              if (Vector2.Distance(buffer[i], current) < Vector2.Distance(v, current))
              {
                  v = buffer[i];
                  Debug.Log("Memoris");
              }
          }
          return v;
      } */
}
[System.Serializable]
public class MemoryCell
{
    public Target target;
    public ElementInfo EI;
    public Vector2 pos;
    public MemoryCell(GameObject obj)
    {
        /*
        IElement b = obj.GetComponent<IElement>();
        if (b != null)
        {
            EI = b.GetElementInfo();
        }
        */
    }
    public MemoryCell(GameObject obj, Target target)
    {
        this.target = target;
        /*
        IElement b = obj.GetComponent<IElement>();
        if (b != null)
        {
            EI = b.GetElementInfo();
        }
        */
    }
    public MemoryCell(GameObject obj, Target target, Vector2 pos)
    {
        this.target = target;
        this.pos = pos;
        /*
        IElement b = obj.GetComponent<IElement>();
        if (b != null)
        {
            EI = b.GetElementInfo();
        }
        */
    }
    public MemoryCell(GameObject obj, Vector2 pos)
    {

        this.pos = pos;
        /*
        IElement b = obj.GetComponent<IElement>();
        if (b != null)
        {
            EI = b.GetElementInfo();
        }
        */
    }
    public MemoryCell(Target target, Vector2 pos)
    {
        this.target = target;
        this.pos = pos;

    }
}
[System.Serializable]
public class Preferense
{
    public float m_H, m_Q, m_L, m_S, m_Fu;
    public Color MainColor;
    public Preferense(float m_H, float m_Q, float m_L, float m_S, float m_Fu, Color MainColor)
    {
        this.m_H = m_H;
        this.m_Q = m_Q;
        this.m_L = m_L;
        this.m_S = m_S;
        this.m_Fu = m_Fu;
        this.MainColor = MainColor;
    }
    public static float Similarity(Preferense p1, Preferense p2)
    {
        float sum = 0;
        sum += Mathf.Abs(p1.m_H - p2.m_H) / 9f;
        sum += Mathf.Abs(p1.m_Q - p2.m_Q) / 9f;
        sum += Mathf.Abs(p1.m_L - p2.m_L) / 9f;
        sum += Mathf.Abs(p1.m_S - p2.m_S) / 9f;
        sum += Mathf.Abs(p1.m_Fu - p2.m_Fu) / 9f;
        sum += Mathf.Abs(p1.MainColor.r - p2.MainColor.r);
        sum += Mathf.Abs(p1.MainColor.g - p2.MainColor.g);
        sum += Mathf.Abs(p1.MainColor.b - p2.MainColor.b);
        float result = sum / 8f;
        return result;
    }
    public Preferense()
    {

    }
}
[System.Serializable]
public class Life
{
    public float healt, food, water, speed, slow = 1, min, max;
    public float speedfetus = 1;
    public bool dead;
    public Organism organism;
    public float defaultedHungry = 0, tempWaterDring = 1, tempHungry = 1;
    public Preferense parameters, standart;
    public void Damaged(float damage)
    {
        healt -= damage;
    }
    public float NoMinus(float n) { if (n < 0) n = 0; return n; }
    public Preferense GetDollis()
    {
        if (standart != null)
        {
            Preferense dolli = new();
            dolli.m_L = NoMinus(healt / standart.m_L);

            dolli.m_H = NoMinus(food / standart.m_H);
            dolli.m_Q = NoMinus(water / standart.m_Q);
            dolli.m_S = NoMinus(speed / standart.m_S);
            return dolli;
        }
        return null;
    }
    public void GetTemp()
    {
        float current_temp = organism.GetTemperature();

        if (current_temp != float.NaN)
        {
            if ((current_temp >= min) & (current_temp < max))
            {
                tempWaterDring = 1;
                slow = 1;
                tempHungry = 1;
                speedfetus = 1;
            }
            else
            {
                float sub, subAbsolut = max - min;
                if (current_temp < min)
                {
                    if (current_temp < min / 2)
                    {
                        dead = true;
                    }

                    sub = min - current_temp;
                    tempWaterDring = 1f / sub;
                    speedfetus = sub;
                    tempHungry = sub;
                    slow = 1 + (sub / 15f) / (parameters.m_Fu+1);

                }
                else
                {
                    tempHungry = 1;
                }
                if (current_temp >= max)
                {
                    if (current_temp >= max * 2)
                    {
                        dead = true;
                    }
                    sub = current_temp - max;
                    tempWaterDring = 1 + sub *4 * parameters.m_Fu;

                }
            }
        }
    }
    public Life(Preferense parameters, Organism organism)
    {
        //---
        this.parameters = parameters;
        this.organism = organism;
        healt = 1 + parameters.m_L * parameters.m_L * 10f;
        healt /= 8;
        food = 1 + parameters.m_H * parameters.m_H * 10f;
        food /= 8;
        water = 1 + parameters.m_Q * parameters.m_Q * 10f;
        water /= 8;
        speed = parameters.m_S * 1;
        standart = new();
        standart.m_H = food;
        standart.m_L = healt;
        standart.m_Q = water;
        standart.m_S = speed;
        defaultedHungry = (1) / 3000f;
        min = (organism.MinTemp / 9f - 0.5f) * 2 * 90 + 273;
        max = (organism.MaxTemp / 9f - 0.5f) * 2 * 90 + 273;

    }
    float fetus = 0;
    public void Action()
    {
        // Debug.Log(" Healt: "+healt);
        if (!dead)
        {
            GetTemp();

            // if (organism.fetus != null) { fetus = organism.fetus.speedsup; } else fetus = 0;
            food -= Organism.InTime((defaultedHungry * 4) / tempHungry);
            water -= Organism.InTime(((defaultedHungry * 2) / tempHungry) * 1 * tempWaterDring);
            speed = parameters.m_S / slow;
            float f = food, q = water, pf = 0, pq = 0;
            if (food <= 0)
            {
                f = (1 + Mathf.Abs(food));
                pf = 1;
            }
            if (water <= 0)
            {
                q = (1 + Mathf.Abs(water));
                pq = 1;
            }
            healt -= Organism.InTime((q / 64f) * pq + (f / 64f) * pf + 0.01f);
            if (healt <= 0)
            {
                dead = true;
            }
        }
    }
}
[System.Serializable]
public class OrgaBehaviour
{
    public Memory memory;
    public Life life;
    public CurrentCicl cicl;
    public Target target;

    public void CreateTarget(Vector2 Pos, ActorTarget dm, string fineltar, IElement element = null)
    {
        memory.Save(target, life.organism.transform.position);
        if (element != null) { target = new(Pos, element, dm, fineltar); }
        else
            target = new(Pos, dm, fineltar);

    }
    public void CreateTarget(Vector2 pos, IElement element, ActorTarget dm, string fineltar)
    {
        // memory.Save(target, life.organism.transform.position);

        target = new(pos, element, dm, fineltar);



    }
    public void StopTarget()
    {

        memory.Save(target, life.organism.transform.position);
        target = null;
    }
    public OrgaBehaviour(Memory memory, CurrentCicl cicl, Life life)
    {
        this.life = life;
        this.memory = memory;
        this.cicl = cicl;
    }
    public void SetCicl(CurrentCicl cicl)
    {
        this.cicl = cicl;
    }
    [System.Serializable]
    public class EnuDecter
    {
        public string det;
        public IElement ie;
        public EnuDecter(string det, IElement ie)
        {
            this.det = det;
            this.ie = ie;
        }
    }
    public ActorTarget SetActor(IElement ie, string det, int nu)
    {
        switch (det)
        {
            case "V":
                switch (ie)
                {
                    case Food:

                        //это роман из прошлого, кароче пиши определитель актов для целирования
                        return new ActorTarget(nu, "VFA");
                    case Water:
                        return new ActorTarget(nu, "VQA");
                    case Organism:
                        return new ActorTarget(nu, "VEA");
                    case Wall:
                        return new ActorTarget(nu, "VWA");
                }
                break;
            case "C":
                switch (ie)
                {
                    case Food:
                        return new ActorTarget(nu, "CFA");
                    case Water:
                        return new ActorTarget(nu, "CQA");
                    case Organism:
                        return new ActorTarget(nu, "CEA"); ;
                    case Wall:
                        return new ActorTarget(nu, "CWA"); ;
                }

                break;
        }
        return null;
    }
        public ActorTarget GetActor(EnuDecter ed)
    {
        switch (ed.det)
        {
            case "V":
                switch (ed.ie)
                {
                    case Food:

                        //это роман из прошлого, кароче пиши определитель актов для целирования
                        return new ActorTarget(cicl.VFA, "VFA");
                    case Water:
                        return new ActorTarget(cicl.VQA, "VQA");
                    case Organism:
                        return new ActorTarget(cicl.VEA, "VEA");
                    case Wall:
                        return new ActorTarget(cicl.VWA, "VWA");
                }
                break;
            case "C":
                switch (ed.ie)
                {
                    case Food:
                        return new ActorTarget(cicl.CFA, "CFA");
                    case Water:
                        return new ActorTarget(cicl.CQA, "CQA");
                    case Organism:
                        return new ActorTarget(cicl.CEA, "CEA"); ;
                    case Wall:
                        return new ActorTarget(cicl.CWA, "CWA"); ;
                }

                break;
        }
        return null;
    }
    public ActorTarget FreeGetActor(string type, string det)
    {
        switch (det)
        {
            case "V":
                switch (type)
                {
                    case "Food":

                        //это роман из прошлого, кароче пиши определитель актов для целирования
                        return new ActorTarget(cicl.VFA, "VFA");
                    case "Water":
                        return new ActorTarget(cicl.VQA, "VQA");
                    case "Organism":
                        return new ActorTarget(cicl.VEA, "VEA");
                    case "Wall":
                        return new ActorTarget(cicl.VWA, "VWA");
                }
                break;
            case "C":
                switch (type)
                {
                    case "Food":
                        return new ActorTarget(cicl.CFA, "CFA");
                    case "Water":
                        return new ActorTarget(cicl.CQA, "CQA");
                    case "Organism":
                        return new ActorTarget(cicl.CEA, "CEA"); ;
                    case "Wall":
                        return new ActorTarget(cicl.CWA, "CWA"); ;
                }

                break;
        }
        return null;
    }
    //-------------
    public string ReasonTarget()
    {
        string fineltar = "none";
        Preferense dolli = life.GetDollis();
        string tar = "";
        float p_f = cicl.XFP, p_q = cicl.XQP;



        if (dolli.m_Q < dolli.m_H)
        {
            if (dolli.m_H < (p_f / 9f))
            {


                fineltar = "Food";


            }
            if (dolli.m_Q < (p_q / 9f))
            {

                fineltar = "Water";


            }
        }
        else
        {
            if (dolli.m_Q < (p_q / 9f))
            {

                fineltar = "Water";


            }
            if (dolli.m_H < (p_f / 9f))
            {


                fineltar = "Food";


            }
        }










        if ((fineltar == "none") & (dolli.m_H > (p_f / 9f)) & (dolli.m_Q > (p_q / 9f)) & (life.organism.fetus == null))
        {
            fineltar = "Organism";
            //Debug.Log(2+fineltar);
        }
        //Debug.Log(dolli.m_H + "<" + (p_f / 9f));

        return fineltar;

    }
    public void CreateMemoryCell()
    {
        Vision vision = life.organism.vision;
        if (vision.objs.Count > 0)
        {
            for (int i = 0; i < vision.objs.Count; i++)
            {
                memory.Save(vision.objs[i], vision.objs[i].transform.position);
            }
        }
    }
    public void RememberMemory()
    {
        if (memory.cells.Count >= 25)
        {
            for (int i = 0; memory.cells.Count >= 100; i++)
                memory.cells.RemoveAt(0);
        }
    }
    public Vector2 DontMoveInBounds(Vector2 v)
    {
        Vector2 t = new(v.x, v.y);
        if (v.x > (Field.Size / 2f) - 1)
        {
            t.x = (Field.Size / 2f) - 2;

        }
        if (v.x < -((Field.Size / 2f) - 1))
        {
            t.x = -((Field.Size / 2f) - 2);

        }
        if (v.y > (Field.Size / 2f) - 1)
        {
            t.y = (Field.Size / 2f) - 2;

        }
        if (v.y < -((Field.Size / 2f) - 1))
        {
            t.y = -((Field.Size / 2f) - 2);

        }
        return t;
    }
    public void DefaultBehaviour()
    {
        if (life.organism.FactSpeed > 0.5f)
        {
            if (memory.cells.Count == 0)
            {
                Vector2 v = (Vector2)life.organism.transform.position + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                v = DontMoveInBounds(v);
                CreateTarget(v, null, "none");
            }
            else
            {
                Vector2 v = (Vector2)life.organism.transform.position + (-memory.Middle());
                v = DontMoveInBounds(v);
                CreateTarget(v, null, "none");
            }
        }
        else
        {
            Vector2 v = (Vector2)life.organism.transform.position + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            v = DontMoveInBounds(v);
            CreateTarget(v, null, "none");
        }
    }
    public bool SummonTarget(string fineltar)
    {
        Vision vision = life.organism.vision;
        Detector detector = life.organism.detector;
        if (memory.cells.Count == 0)
        {
            if (detector.objs.Count > 0)
            {
                //Debug.Log("detector.collisions.Count: " + detector.collisions.Count);
                GameObject ieo = detector.SearchName(fineltar);
                if (ieo != null)
                {
                    IElement ie = ieo.GetComponent<IElement>();
                    ActorTarget at = GetActor(new EnuDecter("C", ie));
                    CreateTarget(ieo.transform.position, ie, at, fineltar);
                    return true;

                }
            }
            if (vision.objs.Count > 0)
            {
                //Debug.Log(3 + fineltar);
                GameObject ieo;
                if (fineltar == "Organism")
                {
                    ieo = vision.SearchName(fineltar, FreeGetActor(fineltar, "V").Use());
                }
                else ieo = vision.SearchName(fineltar, "");
                //Debug.Log(4+"" + ieo);
                if (ieo != null)
                {
                    IElement ie = ieo.GetComponent<IElement>();

                    ActorTarget at = GetActor(new EnuDecter("V", ie));
                    CreateTarget(ieo.transform.position, ie, at, fineltar);
                    //Debug.Log(5 + "CReate");
                    return true;
                }
            }


            //Кароче, цель либо нулиться либо зависает, что является причиной тупняка, найди способ переключать в deafulebehaviuot

            //Доделывай, че

        }
        else

        {
            //доделай реализацию целей и создания мыслей используя память
            //Vector2 pos = memory.SearchCloseElement(fineltar, life.organism.transform.position);
            /*
            if (pos != new Vector2(float.MaxValue, float.MaxValue))
            {
                CreateTarget(pos, null, "none");
                return true;
            }
            else
            */
            {
                if (detector.objs.Count > 0)
                {
                    //Debug.Log(7 + fineltar);
                    //Debug.Log("detector.collisions.Count: " + detector.collisions.Count);
                    if (fineltar != "Atack")
                    {
                        GameObject ieo = detector.SearchName(fineltar);
                   
                    //Debug.Log(8+"" + ieo);
                    if (ieo != null)
                    {
                        IElement ie = ieo.GetComponent<IElement>();
                        ActorTarget at = GetActor(new EnuDecter("C", ie));
                        //Debug.Log(9 + "" + at.Use());
                        CreateTarget(ieo.transform.position, ie, at, fineltar);
                        return true;

                    }
                    }
                    else {
                        if (target != null)
                        {
                            GameObject ieo = detector.SearchName(fineltar,(Organism)target.element);
                        }
                        if(target!=null)
                        return true;
                    }

                }
                if (vision.objs.Count > 0)
                {
                    //Debug.Log(3 + fineltar);
                    GameObject ieo;
                    if (fineltar == "Organism")
                    {
                        ieo = vision.SearchName(fineltar, FreeGetActor(fineltar, "V").Use());
                    }
                    else ieo = vision.SearchName(fineltar, "");
                    //Debug.Log(4 + "" + ieo);
                    if (ieo != null)
                    {
                        //Debug.Log(4 + fineltar);
                        IElement ie = ieo.GetComponent<IElement>();
                        //Debug.Log(5.6+"" + ie);
                        ActorTarget at = GetActor(new EnuDecter("V", ie));
                        //Debug.Log(6.6 + "" + at.Use());
                        CreateTarget(ieo.transform.position, ie, at, fineltar);
                        return true;
                    }
                    if (target != null)
                        return true;
                }
                DefaultBehaviour();

            }
        }
        return false;
    }
    public bool AbortedTarget(string reasontar)
    {
        string fineltar = "none";
        Preferense dolli = life.GetDollis();
        string tar = "";
        float p_f = cicl.HG, p_q = cicl.QG;
        if (dolli.m_Q < dolli.m_H)
        {
            if (dolli.m_H < (p_f / 9f))
            {


                fineltar = "Food";


            }
            if (dolli.m_Q < (p_q / 9f))
            {

                fineltar = "Water";


            }
        }
        else
        {
            if (dolli.m_Q < (p_q / 9f))
            {

                fineltar = "Water";


            }
            if (dolli.m_H < (p_f / 9f))
            {


                fineltar = "Food";


            }
        }
        //Debug.Log(1+fineltar);
        if ((fineltar == "none") & (dolli.m_H > (p_f / 9f)) & (dolli.m_Q > (p_q / 9f)) & (life.organism.fetus == null))
        {
            fineltar = "Organism";
            //Debug.Log(2+fineltar);
        }
        if (fineltar != "none")
        {
            if (fineltar != reasontar)
            {
                return true;
            }
        }
        return false;
    }
    public void AnewTarget(string fineltar)
    {
        if (fineltar != "none")
        {
            if (!SummonTarget(fineltar))
            {
                DefaultBehaviour();
            }
        }
        else
        {
            DefaultBehaviour();
        }
    }
    public float timeoutmem = 0, timemem = 0.25f;
    public void Action()
    {



        string fineltar = ReasonTarget();

        if (target == null)
        {
            AnewTarget(fineltar);
        }
        else
        {
            if (fineltar != "none")
            {
                if (target.determiner != null)
                {
                    if (AbortedTarget(fineltar))
                    {

                        StopTarget();
                        AnewTarget(fineltar);
                    }

                }
            }
        }
        timeoutmem -= Time.deltaTime;
        if (timeoutmem <= 0)
        {
            memory.Save(life.organism.gameObject, life.organism.transform.position);
            // CreateMemoryCell();
            timeoutmem = timemem;

        }
        RememberMemory();
    }

}
//[System.Serializable]
public class Target
{
    public ActorTarget determiner;
    public Vector2 pos;
    public IElement element;
    public bool complete;
    public string fineltar;

    public void DistanceComplete(Vector2 current, float lim, char oper)
    {
        switch (oper)
        {
            case '<':
                if (Vector2.Distance(current, pos) < lim)
                {
                    complete = true;
                }

                break;
            case '>':
                if (Vector2.Distance(current, pos) > lim)
                {
                    complete = true;
                }
                break;
        }
    }
    public float GetDist(Vector2 current)
    {
        return Vector2.Distance(current, pos);
    }
    public Target(Vector2 pos, ActorTarget determine, string fineltar)
    {
        this.pos = pos;
        this.determiner = determine;
        this.fineltar = fineltar;
    }
    public Target(Vector2 pos, IElement element, ActorTarget determine, string fineltar)
    {
        this.pos = pos;
        this.element = element;
        this.determiner = determine;
        this.fineltar = fineltar;
    }
    public Target(IElement element, ActorTarget determine, string fineltar)
    {
        this.element = element;
        this.determiner = determine;
        this.fineltar = fineltar;
    }


}
public interface IActorTarget
{


    public void Init(float nu, string type);
    public string Use();
}
/*
public class ActorTarget : IActorTarget
{
    public int nu;
    public string type;
    public IActorTarget actor;
    public void Init(int nu, string type,IActorTarget actor)
    {
        this.nu = nu;
        this.type = type;
        this.actor = actor;
    }


    public string Use()
    {
        return actor.Use();
    }
}
*/
[System.Serializable]
public class ActorTarget : IActorTarget
{
    public float nu;
    public string type;
    public void Init(float nu, string type)
    {
        this.nu = nu;
        this.type = type;
    }
    public ActorTarget(float nu, string type) { Init(nu, type); }
    public string Use()
    {
        if (type[0] == 'C')
            return nu switch
            {
                0 => "Run",
                1 => "UnRun",
                2 => "Atack",
                3 => "Spar",
                4 => "Eat",
                5 => "Drink",
                _ => "none",
            };
        if (type[0] == 'V')
            return nu switch
            {
                0 => "Run",
                1 => "UnRun",
                _ => "none",
            };
        return "none";
    }
}
public class ResonTargetInfo
{
    public ResonTargetInfo()
    {

    }
}

public class Fetus
{
    public float timefetus;
    public Organism pregnant, seminior;
    public string mother, father;
    public bool good;
    public float timeout, secondout;
    public string gen = "";
    public float l, w, f;
    public float sup;
    public float speedF, speedW;
    public Fetus(string mother, string father, Organism pregnant, Organism seminior)
    {

        this.mother = mother;
        this.father = father;
        this.pregnant = pregnant;
        timefetus =1+ pregnant.preferense.m_Fu * 10;
        pregnant.timeoutSpar = pregnant.timeSpar;
        seminior.timeoutSpar = seminior.timeSpar;
        gen = Organism.ReplicityGen(mother, father);
        f = Organism.GetLocalT(3, gen);
        w = Organism.GetLocalT(4, gen);
        l = Organism.GetLocalT(5, gen);
        f = (1 + f * f * 10) / 8f;
        w = (w * w * 10 + 1) / 8f;
        l = (l * l * 10 + 1) / 8f;
        // sup = f + w + l/2F ;
        speedF = (f + l / 2f) / timefetus;
        speedW = (w + l / 2f) / timefetus;
    }
    public void Action()
    {

        if (!good)
        {

            timeout += Time.deltaTime / pregnant.life.speedfetus;
            secondout -= Time.deltaTime / pregnant.life.speedfetus;
            //Debug.Log("Fetus:time: "+timeout);
            if (secondout <= 0)
            {
                secondout = 1;
                pregnant.life.food -= speedF;
                pregnant.life.water -= speedW;
            }
            if (timeout > timefetus)
            {
                Birth();
            }

        }
    }
    public void Birth()
    {
        good = true;

        GameObject prefab = Organism.organism_prefab;
        Organism org = GameObject.Instantiate(prefab, pregnant.transform.position, Quaternion.identity).GetComponent<Organism>();

        org.NewRealizide(gen);
        org.good = true;
        org.Awake();
        pregnant.fetus = null;
        // BackingInterface.organism = org;
    }
}


