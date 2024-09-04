using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem 
{

   public static void checkIfExists(string dataPath)
    {
        if (!File.Exists(Application.dataPath + dataPath))
        {
            File.Create(Application.dataPath + dataPath);
        }

    }
  public static void SaveSkills(SkillToSave savedSkills)
    {
        string skilltoSave = JsonUtility.ToJson(savedSkills);
        File.WriteAllText(Application.dataPath + "/saveSkills.txt", skilltoSave);
    }

    public static void LoadSkills(SkillToSave loadedSkills)
    {
        string SkillsToLoad = File.ReadAllText(Application.dataPath + "/saveSkills.txt");
        JsonUtility.FromJsonOverwrite(SkillsToLoad, loadedSkills);
    }
      
    public static void SaveScore(SaveScore savedScore)
    {
        string ScoreToSave = JsonUtility.ToJson(savedScore);

        File.WriteAllText(Application.dataPath + "/Score.txt", ScoreToSave);
    }

    public static void LoadScore(SaveScore SaveScoreObject)
    {
        string ScoreToLoad = File.ReadAllText(Application.dataPath + "/Score.txt");

        JsonUtility.FromJsonOverwrite(ScoreToLoad, SaveScoreObject);


    }
}
