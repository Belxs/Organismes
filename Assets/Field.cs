using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
[PreferBinarySerialization]
public class Field : MonoBehaviour
{
    public static bool visual = true,temprender=false;
    public static Cell[,] cells;
    public static float[,] tempersbuffer,tempercounter;
    public static Cell_Field[,] Graphic_Cells;
    public static GameObject prefab_Cell_Graphic;
    public static int Size = 25;
    public static float MaxTemp = 400, MinTemp = 0;
    public static EnvironmentTempBehaviour environment = new(); 
    public static float Atmosphere=330,TempAtmosphere,StronghtAtmosphere=0.5f, octaves=8;
    public static bool MoveAtmosphere=true,arfed=false;
    public static float speed_temp = 1;
    public static bool inited;
    public static Texture2D Square_tex;
    public static Sprite Square_spr;
    public bool genFood;
    public List<ITask> tasks = new();
    // Start is called before the first frame update
    public void Start()
    {
        Init();
        ExceptionPause.AddScript(this);
    }
    public void SetTempRender(bool value) 
    {
        temprender=value;
    }
    public void GetStringAtmosphereTemp(out string s)
    {
        s= Atmosphere+"";
    }
    public void GoArrayCell()
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            for (int x = 0; x < Size; x++)
                for (int y = 0; y < Size; y++)
                {

                    tasks[i].Action(x, y);
                }
            tasks[i].End();
        }

        tasks.Clear();
    }
    public void TempMove(Algoritm algoritm)
    {
        switch (algoritm)
        {
            case Algoritm.Perlin:
                tasks.Add(new Per());
                break;

            case Algoritm.ARF:
                tasks.Add(new Arf());
                tasks.Add(new MathTempered());
                break;
            case Algoritm.GEO:

                break;
        }
       
    }
    public void SetAtmosphereTemp(float temp)
    {
        Atmosphere = temp;
    }
    public static void Init()
    {
        if (!inited)
        {
            Debug.Log("init!!!");
            Square_tex = (Texture2D)PrefabLoader.Load("Prefab", "Square");

            Square_spr = Sprite.Create(Square_tex, new Rect(0, 0, Square_tex.width, Square_tex.height), new Vector2(0.5f, 0.5f));
            inited = true;
            tempercounter = new float[Field.Size, Field.Size];
            tempersbuffer = new float[Size, Size];
            cells = new Cell[Size, Size];
            prefab_Cell_Graphic = (GameObject)PrefabLoader.Load("Prefab", "Cell");
            Graphic_Cells = new Cell_Field[Size, Size];
            for (int x = 0; x < Size; x++)
                for (int y = 0; y < Size; y++)
                {
                    Vector2 pos = new Vector2(x, y);
                    Vector2 worldPos = new Vector2(x - Size / 2, y - Size / 2);
                    cells[x, y] = new Cell(pos, Atmosphere);
                    Graphic_Cells[x, y] = Instantiate(prefab_Cell_Graphic, worldPos, Quaternion.identity).GetComponent<Cell_Field>();
                    Graphic_Cells[x, y].cell = cells[x, y];
                }
        }
    }
    public static void SetTemperBuffer(Vector2Int pos,float temp)
    {

        
       
        tempersbuffer[pos.x, pos.y] += temp;
        tempercounter[pos.x, pos.y]++;
        
    }
    public void BehaviourAtmosphere() {
        TempAtmosphere = (Atmosphere + (1f-Mathf.PerlinNoise(Atmosphere, Atmosphere))*Atmosphere)/2f;
    
    }
    float timeSred;
    // Update is called once per frame
    void Update()
    {
        /*
        if (timeSred == 0) timeSred = Time.deltaTime;
        timeSred=(timeSred+ Time.deltaTime)/2f;
        Debug.Log(1f / timeSred+" столько нужно для произведения time и koff");
        */
        TempMove(Algoritm.Perlin);
        if(arfed)
        TempMove(Algoritm.ARF);
        
        // if (MoveAtmosphere) BehaviourAtmosphere();
        if (tasks.Count > 0)
        {
            GoArrayCell();
        }
    }
    public static Cell Get_Is_World_to_Pos(Vector2 world)
    {
        Cell result = null;
        if ((Mathf.Abs(world.x) < Size / 2) & (Mathf.Abs(world.y) < Size / 2))
        {
            if (!inited) Init();
            
            Vector2 pos = new(world.x + Size / 2, world.y + Size / 2);
            //Debug.Log(inited+" Size: " +"|pos"+ Maf.RoundToInt(pos.x) +" "+Mathf.RoundToInt(pos.y));
            result = Graphic_Cells[Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)].cell;

        }

        return result;
    }

}
public class MathTempered : ITask
{
    public void Action(int x, int y)
    {
        Field.cells[x, y].temperatura = Field.tempersbuffer[x, y]/ Field.tempercounter[x, y];
        
    }

    public void End(int x, int y)
    {

    }
    public void End()
    {
        Field.tempersbuffer = new float[Field.Size, Field.Size];
        Field.tempercounter = new float[Field.Size, Field.Size];
    }
}
public class Per : ITask
{

    public void Action(int x, int y)
    {
        Field.cells[x, y].temperatura = (Field.cells[x, y].temperatura*(Field.octaves-1) + (1f-Mathf.PerlinNoise(Field.cells[x, y].temperatura*Field.StronghtAtmosphere, Field.cells[x, y].temperatura*Field.StronghtAtmosphere)) * Field.cells[x, y].temperatura)/ Field.octaves;
    }

    public void End(int x, int y)
    {
       // throw new System.NotImplementedException();
    }

    public void End()
    {
      //  throw new System.NotImplementedException();
    }
}
public class Arf : ITask
{


    public bool good;
    Cell cell;
    public Cell[] Counter(Cell[] buffer)
    {
        List<Cell> newbuffer = new();
        for (int i = 0; i < buffer.Length; i++)
        {

            Cell getcell = buffer[i];
            Vector2 pos = getcell.pos;
            if ((pos.x >= 0) & (pos.x < Field.Size))
                if ((pos.y >= 0) & (pos.y < Field.Size))
                {
                    newbuffer.Add(getcell);
                }
        }
        return newbuffer.ToArray();
    }
    public void Action(int x, int y)
    {
        float GenArh=0, AT=0;
        float sum = 0;
        Cell[] buffer =
          {
            new Cell(new Vector2(x,y),1),
            new Cell(new Vector2(x+1,y),1),
            new Cell(new Vector2(x,y+1),1),
            new Cell(new Vector2(x-1,y),1),
            new Cell(new Vector2(x,y-1),1),
            new Cell(new Vector2(x+1,y+1),1),
            new Cell(new Vector2(x-1,y-1),1),
            new Cell(new Vector2(x-1,y+1),1),
            new Cell(new Vector2(x+1,y-1),1)
        };
        buffer = Counter(buffer);
        float count = buffer.Length;
        //Debug.Log("c: "+count);
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = buffer[i].pos;
            sum += Field.cells[(int)pos.x, (int)pos.y].temperatura;

        }
        GenArh = sum / count;
       // Debug.Log("S: "+sum);
      //  Debug.Log("GEN: " + GenArh);
        AT = GenArh - GenArh * Field.speed_temp;
       // Debug.Log("AT: " + AT);
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = buffer[i].pos;
            float t = Field.cells[(int)pos.x, (int)pos.y].temperatura;
         //   Debug.Log("t: " + AT);
            Field.SetTemperBuffer(new Vector2Int((int)pos.x, (int)pos.y), GenArh);

        }
    }

    public void ArhTemp(Cell cell1, float arh)
    {
        float t = cell1.temperatura;
        float speed = t - arh;
        cell1.temperatura = t - speed * Field.speed_temp;
    }

    public void Start(int x, int y)
    {
    }

    public void End(int x, int y)
    {
        
    }
    public void End()
    {

    }
}
public interface ITask
{
    
    public void Action(int x, int y);
    public void End(int x, int y);
    public void End();
}
[Serializable]
public class Cell
{
    public Vector2 pos;
    public float temperatura = 1;
    public Cell(Vector2 pos, float temperatura)
    {
        this.pos = pos;
        this.temperatura = temperatura;
    }
    public string toString()
    {

        return "Позиция: " + pos + "| температура: " + (temperatura - 273);
    }
}
public enum Algoritm
{
    ARF,
    GEO,
    Perlin
}