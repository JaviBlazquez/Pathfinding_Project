using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class FileManager 
{
    public static bool WriteToFile(string filename, string data)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, filename);

        try
        {
            File.WriteAllText(fullPath, data);
            Debug.Log("Fichero guardado correctaente en: " + fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log("Error al guardar el fichero en: " + fullPath + " con el error: " + e);
            return false;
        }
    }
}
