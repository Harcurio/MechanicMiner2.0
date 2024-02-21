using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocation : MonoBehaviour
{

    private static EnemyLocation instancia;


    public static EnemyLocation Instancia
    {
        get
        {
            
            if (instancia == null)
            {
                instancia = FindObjectOfType<EnemyLocation>();


                if (instancia == null)
                {
                    Debug.LogError("No se ha encontrado un objeto EnemyLocation en la escena.");
                }
            }
            return instancia;
        }
    }


    public Vector3 ObtenerPosicionEnemigo()
    {
        return transform.position;
    }
}
