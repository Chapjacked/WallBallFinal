using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoadScript : MonoBehaviour {
    //This Code made with the help of the tutorial located at: https://gamedevelopment.tutsplus.com/tutorials/how-to-save-and-load-your-players-progress-in-unity--cms-20934


 
    public static List<Game> savedVariables = new List<Game>();


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void Save()
    {
        savedVariables.Add(Game.currentGame);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedVariables.gd");
        bf.Serialize(file, savedVariables);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedVariables.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedVariables.gd", FileMode.Open);
            savedVariables = (List<Game>)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.Log("File not found");
        }
    }
}
