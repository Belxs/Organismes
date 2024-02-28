using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Diagnostics;
public class WriteToAnalize : MonoBehaviour
{
   // public List<FrameAnalise> frames=new();
    float timeout, time=1,
        autoout=60,autotime=60,
        deepout=1,deeptime=1;
    public string analise,deepanalise;
    string path,deepath,nam;
    private void OnApplicationQuit()
    {
       
        File.WriteAllText(path, analise);
        File.WriteAllText(deepath, deepanalise);
        Process.Start(Application.streamingAssetsPath + "\\" + "FileToExcel.exe");
    }
    public void AutoSave()
    {
        if (autoout <= 0)
        {
            autoout = autotime;
            File.WriteAllText(path, analise);
            File.WriteAllText(deepath, deepanalise);
            UnityEngine.Debug.Log("AutoSave");
        }
        else autoout -= Time.deltaTime;
    }
    void Start()
    {
        ExceptionPause.AddScript(this);
        nam = "result[" + DateTime.Now.TimeOfDay.ToString() + "]";
        string newnam="";
        for (int i = 0; i < nam.Length; i++)
        {
            char s = nam[i];
            switch (nam[i])
            {
                case '.':
                    s = '_';
                    break;
                case ':':
                    s = '-';
                    break;
                case ' ':
                    s = '_';
                    break;
            }
            newnam += s;
        }
        path = Application.streamingAssetsPath +"/Analises"+ "/"+newnam+".txt";
        deepath = Application.streamingAssetsPath + "/Analises" + "/" + "deep"+newnam + ".txt";
        File.Create(path);
        File.Create(deepath);
    }

    // Update is called once per frame
    void Update()
    {
        AutoSave();
        if (timeout <= 0)
        {
            timeout = time;
            FrameAnalise frame=new FrameAnalise();
            analise+="|time:"+Time.time+ frame.ToLocalString()+'\n' ;
        }
        else timeout -= Time.deltaTime;
        
        if (deepout <= 0)
        {
            deepout = time;
            DeepFrameAnalise frame = new DeepFrameAnalise();
            deepanalise += "|time:" + Time.time + frame.ToLocalString() + '\n';
        }
        else deepout -= Time.deltaTime;
    }
}

public class FrameAnalise{
    public int org,food,water,male,women,germa,beam;
    public FrameAnalise()
    {
        org = SpawnerOrganism.orgs.Count;
        food = Gardener.foods.Count;
        water= Gardener.waters.Count;
        male = SpawnerOrganism.male.Count;
        women = SpawnerOrganism.women.Count;
        germa= SpawnerOrganism.germa.Count;
        beam= SpawnerOrganism.beam.Count;
    }
    public string ToLocalString()
    {
        return "|1:" + org + "|2:" + food + "|3:" + water + "|4:" + male + "|5:" + women + "|6:" + germa + "|7:" + beam+"|8:"+Field.Atmosphere;
    }
}
public class DeepFrameAnalise
{
    public CurrentCicl cc;
    public DeepFrameAnalise()
    {
        cc = SpawnerOrganism.MatchDeepAnalise();
    }
    public string ToLocalString()
    {
        return 
            "|1:" + cc.XFP + "|2:" + cc.XQP + 
            "|3:" + cc.XWP + "|4:" + cc.XEP + 
            "|5:" + cc.XFA + "|6:" + cc.XQA + 
            "|7:" + cc.XWA + "|8:" + cc.XEA +
            "|9:" + cc.CFA + "|10:" + cc.CQA +
            "|11:" + cc.CWA + "|12:" + cc.CEA +
            "|13:" + cc.HG + "|14:" + cc.QG +
            "|15:" + cc.LG + "|16:" + cc.VFA +
            "|17:" + cc.VQA + "|18:" + cc.VWA +
            "|19:" + cc.VEA ;
    }
}
