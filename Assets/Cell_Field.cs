using UnityEngine;

public class Cell_Field : MonoBehaviour
{
    public Cell cell;
    public SpriteRenderer sr;
    public float GetTemperature;
    // Update is called once per frame
    public Sprite mainS;
    public Color mainC;
    public void Awake()
    {
        ExceptionPause.AddScript(this);
        sr = GetComponent<SpriteRenderer>();
        mainS = sr.sprite;
        mainC = sr.color;
    }
    
    

    public void Noize()
    {
        if (cell != null)
        {
            float a = (Field.environment.StronghtNoize * ((Mathf.Tan((cell.pos.x * cell.pos.y + Time.time * Field.environment.TanInSide) / Field.environment.TanDelinSide) / Field.environment.TanDeloutSide) + Mathf.Sin((cell.pos.x * cell.pos.y + Time.time * Field.environment.SinInSide) / Field.environment.SinDelinSide)) / Field.environment.DelNoize);
            if (a > Field.environment.StronghtNoize) a = Field.environment.StronghtNoize;
            if (a < -Field.environment.StronghtNoize) a = -Field.environment.StronghtNoize;
            cell.temperatura = ((Field.Atmosphere + Random.Range(-1f, 1f)) + cell.temperatura) / 2f - a;
        }
    }
    void Update()
    {
        Noize();
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = Field.visual;
        if (cell != null)
        {
            if (Field.MoveAtmosphere)
                GetTemperature = cell.temperatura;
            if (Field.temprender)
            {
                sr.sprite = Field.Square_spr;
                float t = (cell.temperatura - Field.MinTemp) / (Field.MaxTemp - Field.MinTemp);
                float R = 0;
                float B = 0;
                float bound = 273f / Field.MaxTemp;
                if (t <= bound)
                {
                    B = 1 - (t * (1f / bound));
                }
                else R = (t - bound) * (1f / bound);

                transform.localScale = Vector3.one * 0.5f;
                sr.color = new Color(R, 0, B);
            }
            else
            {
                sr.sprite = mainS;
                sr.color = mainC;
                transform.localScale = Vector3.one ;
            }
        }
        else
        {
            Debug.Log("Iam null");
        }
    }
}
public class EnvironmentTempBehaviour
{
    public float TanInSide = 50, TanDelinSide = 200f, TanDeloutSide = 58F;
    public float SinInSide = 50, SinDelinSide = 100f, StronghtNoize = 15f, DelNoize = 2F;
    public EnvironmentTempBehaviour()
    {

    }
    public EnvironmentTempBehaviour(float TanInSide, float TanDelinSide, float TanDeloutSide, float SinInSide, float SinDelinSide, float StronghtNoize, float DelNoize)
    {
        this.TanInSide = TanInSide;
        this.TanDelinSide = TanDelinSide;
        this.TanDeloutSide = TanDeloutSide;
        this.SinInSide = SinInSide;
        this.SinDelinSide = SinDelinSide;
        this.StronghtNoize = StronghtNoize;
        this.DelNoize = DelNoize;
    }
    
}
