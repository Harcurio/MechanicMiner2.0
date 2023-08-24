using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Reflection;


public class Test : MonoBehaviour
{
    public GameObject objetoParaBuscar;
    public string nombreDelScript; //sensible a mayusculas y minusculas awaaasss!!!

    void Start()
    {
        // Verificar si se ha asignado el objeto en el inspector
        if (objetoParaBuscar != null)
        {
            // Buscar el script especificado en el objeto asignado
            Component script = objetoParaBuscar.GetComponent(nombreDelScript);
            

            // Verificar si se encontró el script
            if (script != null)
            {
                Debug.Log("Found the script!! " + nombreDelScript + " on the asigned object");
                // Puedes acceder a las variables y métodos del script aquí
                
                
                Type scriptType = Type.GetType(nombreDelScript);
                ReflectionData reflectionData = new ReflectionData();

                if (scriptType == null) 
                {
                    Debug.Log("ScriptType Null");
                }
                // Retrieve methods
                
                MethodInfo[] methods = scriptType.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    reflectionData.methodNames.Add(method.Name);
                    // Debug.Log("Method Name: " + method.Name);
                }

                // Retrieve variables
                FieldInfo[] fields = scriptType.GetFields();
                foreach (FieldInfo field in fields)
                {
                    reflectionData.variableNames.Add(field.Name);
                    reflectionData.variableTypes.Add(field.FieldType.ToString());
                    //Debug.Log("Variable Name: " + field.Name + ", Variable Type: " + field.FieldType);
                }
                
                PropertyInfo[] pis = scriptType.GetProperties();
                foreach (PropertyInfo p in pis)
                {
                    reflectionData.PropertyName.Add(p.Name);
                    //Debug.Log("Variable Name: " + p.Name + ", Variable Type: " + p.GetType().ToString());
                }


                // Convert reflectionData to JSON
                string jsonData = JsonUtility.ToJson(reflectionData, true);
                // Save the JSON data to a file
                string savePath = Application.dataPath + "/ReflectionData.json";
                File.WriteAllText(savePath, jsonData);


                Debug.Log("Termino saved in "+ Application.dataPath);

            }
            else
            {
                Debug.Log("El script " + nombreDelScript + " no está adjunto al objeto asignado.");
            }
        }
        else
        {
            Debug.Log("No se ha asignado ningún objeto para buscar el script.");
        }
    }

}

[System.Serializable]
public class ReflectionData
{
    public List<string> methodNames = new List<string>();
    public List<string> variableNames = new List<string>();
    public List<string> variableTypes = new List<string>();
    public List<string> PropertyName = new List<string>();

}