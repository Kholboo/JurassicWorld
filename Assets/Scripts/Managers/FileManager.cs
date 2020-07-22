using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    string dataPath = "/file.data";

    void Start()
    {
        if (!File.Exists(Application.persistentDataPath + dataPath))
        {
            Save();
            return;
        }

        Load();
    }

    public void Save()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + dataPath);

            // bf.Serialize(file, data...);
        }
        catch (System.Exception e)
        {
            if (e != null)
            {
                Debug.LogError(e);
            }
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    public void Load()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + dataPath, FileMode.Open);

            // data... = (DataType) bf.Deserialize(file);
        }
        catch (System.Exception e)
        {
            if (e != null)
            {
                Debug.LogError(e);
            }
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    public void Delete()
    {
        File.Delete(Application.persistentDataPath + dataPath);
    }
}
