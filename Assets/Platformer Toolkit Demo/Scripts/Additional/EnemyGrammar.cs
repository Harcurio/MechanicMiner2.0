using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Linq;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnemyGrammar : MonoBehaviour
{
    public Sprite squareSprite;  // sprite of all the parts ( we can create an array with different colors just to represent better this
    public Sprite squarewSprite;
    private List<Vector2> relativePositions = new List<Vector2>(); //positions of each part of the enemy
    private List<Vector2> squareSizes = new List<Vector2>();   //size of each part of the enemy

    //just a fun size for the moment...
    //public Vector2 squareSize = {0.8f, 0.2f};

    private List<string> groups = new List<string>(); //contains the different list of groups obtained for the grammar

    private float bodiesEnemy = 0f;
    private int weakspot { get; set; }

    private string result { get; set; }
    string projectPath; 

    void Start()
    {
        projectPath = Application.dataPath;

        if (loadGrammar())
        {
            Debug.Log("files loaded");
        }
        else 
        {
            result = GenerateString();
            result = RemoveNumbers(result);
            Weak(result);
        }
        
        //Debug.Log(result);

        //result = "hlre";

        
        //Debug.Log(result);
        // Debug.Log("hllrrbllrre");
        //generatePositions(result); // hhlrree
         //generate the weakest point in the enemy
        generatePositions(result); // grupos

        generateBody();     //posiciones y body?
        GenerateObjects(result);



    }

    /// <summary>
    /// selects a random place in the string and save that positions as the weakspot variable 
    /// that will be the weakspot of the enemy
    /// </summary>
    /// <param name="enemy">enemy is the string generated by the grammar</param>
    private void Weak(string enemy)
    {

        weakspot = Random.Range(0, enemy.Length);


    }

    private string GenerateString()
    {
        Dictionary<char, string[]> grammarRules = new Dictionary<char, string[]>
        {
            {'B', new[] {"U L R D"}},
            {'U', new[] {"h", "0", "h U"}},
            {'L', new[] {"l", "1", "l L"}},
            {'R', new[] {"r", "2", "r R"}},
            {'D', new[] {"b L R D", "E"}},
            {'E', new[] {"e", "3", "e E"}}
        };


        StringBuilder stringBuilder = new StringBuilder();
        System.Random random = new System.Random();

        char startSymbol = 'B';

        GenerateStringRecursive(startSymbol, grammarRules, random, stringBuilder);

        return stringBuilder.ToString();
    }

    public void saveGrammar() 
    {
        if (File.Exists(projectPath+"/grammarSaved.txt"))
        {
            Debug.Log("File already exists. Overwriting...");
        }

        //instance of data to be saved
        string dataToSave = result + "|" + weakspot.ToString();

        File.WriteAllText(projectPath + "/grammarSaved.txt", dataToSave);
        Debug.Log("grammar and weakspot saved to file: " + projectPath + "/grammarSaved.txt");
    }


    public bool loadGrammar() 
    {

        if (File.Exists(projectPath + "/grammarSaved.txt")) 
        {    
            string data = File.ReadAllText(projectPath + "/grammarSaved.txt");

            string[] dataArray = data.Split('|');

            if (dataArray.Length == 2) 
            {
                result = dataArray[0];
                weakspot = int.Parse(dataArray[1]);
            }

            return true;
        
        }
        Debug.Log("file not found generating grammar...");
        return false; 
    }

    private void GenerateStringRecursive(char symbol, Dictionary<char, string[]> grammarRules, System.Random random, StringBuilder stringBuilder)
    {
        if (!grammarRules.ContainsKey(symbol))
        {
            stringBuilder.Append(symbol);
        }
        else
        {
            string[] rules = grammarRules[symbol];
            string selectedRule = rules[random.Next(rules.Length)];

            string[] symbols = selectedRule.Split(' ');
            foreach (string s in symbols)
            {
                char c = s[0];
                GenerateStringRecursive(c, grammarRules, random, stringBuilder);
            }
        }
    }

    /// <summary>
    /// This method agrups the different characters depending of the type (head, legs, body) 
    /// and store them in the variable groups
    /// </summary>
    /// <param name="input"> the string generated by the grammar</param>
    private void generatePositions(string input)
    {

        string currentGroup = input[0].ToString();


        for (int i = 1; i < input.Length; i++)
        {

            if (input[i] == input[i - 1])
            {
                currentGroup += input[i];
            }
            else
            {

                groups.Add(currentGroup);
                currentGroup = input[i].ToString();
            }
        }


        groups.Add(currentGroup);


        for (int i = groups.Count - 2; i >= 0; i--)
        {

            if ((groups[i].Contains('h') || groups[i].Contains('0')) && (groups[i + 1].Contains('h') || groups[i + 1].Contains('0')))
            {

                string mergedGroup = groups[i] + groups[i + 1];
                groups[i] = mergedGroup;
                groups.RemoveAt(i + 1);

            }
            else if ((groups[i].Contains('l') || groups[i].Contains('1')) && (groups[i + 1].Contains('l') || groups[i + 1].Contains('1')))
            {

                string mergedGroup = groups[i] + groups[i + 1];
                groups[i] = mergedGroup;
                groups.RemoveAt(i + 1);
            }
            else if ((groups[i].Contains('r') || groups[i].Contains('2')) && (groups[i + 1].Contains('r') || groups[i + 1].Contains('2')))
            {

                string mergedGroup = groups[i] + groups[i + 1];
                groups[i] = mergedGroup;
                groups.RemoveAt(i + 1);
            }
            else if ((groups[i].Contains('e') || groups[i].Contains('3')) && (groups[i + 1].Contains('e') || groups[i + 1].Contains('3')))
            {

                string mergedGroup = groups[i] + groups[i + 1];
                groups[i] = mergedGroup;
                groups.RemoveAt(i + 1);
            }
        }
    }



    public string RemoveNumbers(string inputString)
    {
        string result = "";
        foreach (char character in inputString)
        {
            if (!char.IsDigit(character))
            {
                result += character;
            }
        }
        return result;
    }

    /// <summary>
    /// it generate the positions for the body I think...
    /// iterate trought the groups, delete the null characters for each part and call calculate obejct position 
    /// to calculate the position of each part.
    /// </summary>
    private void generateBody()
    {


        foreach (string group in groups)
        {

            if (group.Contains('h') || group.Contains('0'))
            {
                char caracter = '0';
                string deletex = group.Replace(caracter.ToString(), "");

                if (deletex.Length > 0)
                {
                    CalculateObjectPositions(deletex.Length, 0);
                }
            }

            if (group.Contains('l') || group.Contains('1'))
            {
                char caracter = '1';
                string deletex = group.Replace(caracter.ToString(), "");

                if (deletex.Length > 0)
                {
                    CalculateObjectPositions(deletex.Length, 2);
                }
            }

            if (group.Contains('r') || group.Contains('2'))
            {
                char caracter = '2';
                string deletex = group.Replace(caracter.ToString(), "");

                if (deletex.Length > 0)
                {
                    CalculateObjectPositions(deletex.Length, 1);
                }
            }

            if (group.Contains('e') || group.Contains('3'))
            {
                char caracter = '3';
                string deletex = group.Replace(caracter.ToString(), "");

                if (deletex.Length > 0)
                {
                    CalculateObjectPositions(deletex.Length, 3);
                }
            }

            if (group.Contains('b'))
            {
                CalculateObjectPositions(group.Length, 4); //a lo mejor no funciona bien.... el problema puede ser cuando hay 3 cuerpos y distintas extremidades...

            }

        }


    }

    /// <summary>
    /// this function calculates the position of the objects to place in the body
    /// it only uses based positions getting the (0,0) as the initial point
    /// </summary>
    /// <param name="numObjects"></param>
    /// <param name="joinDirection"></param>
    public void CalculateObjectPositions(int numObjects, int joinDirection)
    {

        float rangoTotal = 1.6f; // Longitud total del rango entre -0.8 y 0.8
        float espacioEntreObjetos = rangoTotal / (numObjects + 1); // Calcula el espacio entre cada objeto



        float posicionInicial = -0.8f + espacioEntreObjetos; // Inicia en -0.8 + espacioEntreObjetos para evitar la posici�n -0.8

        for (int i = 0; i < numObjects; i++)
        {
            float offset = posicionInicial + i * espacioEntreObjetos;

            switch (joinDirection)
            {
                case 0: //up
                    relativePositions.Add(new Vector2(offset, 0.6f));
                    squareSizes.Add(new Vector2(0.2f, 0.2f));
                    break;
                case 1: // right
                    relativePositions.Add(new Vector2(0.8f, offset));
                    squareSizes.Add(new Vector2(0.6f, 0.2f));
                    break;
                case 2: // left
                    relativePositions.Add(new Vector2(-0.8f, offset));
                    squareSizes.Add(new Vector2(0.6f, 0.2f));
                    break;
                case 3: // down legs
                    relativePositions.Add(new Vector2(offset, -0.7f));
                    squareSizes.Add(new Vector2(0.2f, 0.4f));
                    break;
                case 4: // down body
                    bodiesEnemy = bodiesEnemy - 1f;
                    relativePositions.Add(new Vector2(0f, -1f));
                    squareSizes.Add(new Vector2(1f, 1f));
                    break;
            }
        }

    }


    public void GenerateObjects(string input)
    {

        // Crea un GameObject vac�o como padre
        GameObject parentObject = new GameObject("Parent");


        //BoxCollider2D boxCollider = parentObject.AddComponent<BoxCollider2D>();

        //la posicion del objeto vacio es igual a la del objeto parent
        parentObject.transform.position = transform.localPosition;
        // Genera el cuadro principal
        Vector2 localPosition = transform.localPosition;
        GameObject mainSquare = GenerateSquare(localPosition, Vector2.one, parentObject.transform, -1);
        //parentObject = mainSquare;


        JoinObjects(parentObject, mainSquare);

        GameObject previousObject = mainSquare;
        //Vector2 targetVector = new Vector2(0f, -1f); // vector hacia abajo 



        for (int i = 0; i < relativePositions.Count; i++)
        {
            Vector2 position = relativePositions[i];
            Vector2 size = squareSizes[i];

            //obtener la distancia al objeto despues restarlo y mandarlo para generar la imagen
            Transform transformParent = previousObject.transform;
            //Debug.Log("Posici�n del objeto: " + transformParent.position);

            Vector2 posicionObjeto = new Vector2(transformParent.position.x, transformParent.position.y);

            Vector2 desplazamientoRelativo = position + posicionObjeto;
            //Debug.Log("desplazamientoRelativo: " + desplazamientoRelativo);

            GameObject newObject;// = GenerateSquare(desplazamientoRelativo, size, previousObject.transform, i);

            if (i == weakspot)
            {
                newObject = GenerateWeakSquare(desplazamientoRelativo, size, previousObject.transform);
            }
            else
            {
                newObject = GenerateSquare(desplazamientoRelativo, size, previousObject.transform, i);
            }


            //Transform transformParent2 = newObject.transform;

            if (input[i] == 'b')
            {

                JoinObjects(previousObject, newObject);
                previousObject = newObject;

            }
            else
            {
                JoinObjects(previousObject, newObject);
            }



        }


    }

    GameObject GenerateSquare(Vector2 position, Vector2 scale, Transform parent, int i)
    {
        // Crea un nuevo GameObject
        GameObject square = new GameObject("Square" + i.ToString());

        //add tag as enemy
        square.tag = "Enemy";
        square.layer = LayerMask.NameToLayer("Hurt");


        // A�ade un componente SpriteRenderer al GameObject
        SpriteRenderer spriteRenderer = square.AddComponent<SpriteRenderer>();

        // Asigna el sprite al componente SpriteRenderer
        spriteRenderer.sprite = squareSprite;

        // A�ade un componente BoxCollider2D al GameObject
        BoxCollider2D boxCollider = square.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;

        // Ajusta la posici�n del cuadro
        square.transform.position = position;

        // Ajusta la escala del cuadro
        square.transform.localScale = scale;

        // A�ade f�sica 2D al cuadro
        Rigidbody2D rigidbody = square.AddComponent<Rigidbody2D>();

        FixedJoint2D fixedJoint = square.AddComponent<FixedJoint2D>();

        // Configura el objeto padre para que los cuadros sean hijos suyos
        square.transform.parent = parent;

        if (i == -1)
        {
            EnemyMovement myScriptComponent = square.AddComponent<EnemyMovement>();
            //Debug.Log("removimos movimiento");
        }

        return square;
    }

    GameObject GenerateWeakSquare(Vector2 position, Vector2 scale, Transform parent)
    {
        // Crea un nuevo GameObject
        GameObject square = new GameObject("Square");


        //add tag Goal 
        square.tag = "Goal";
        square.layer = LayerMask.NameToLayer("Kill");

        // A�ade un componente SpriteRenderer al GameObject
        SpriteRenderer spriteRenderer = square.AddComponent<SpriteRenderer>();

        // Asigna el sprite al componente SpriteRenderer
        spriteRenderer.sprite = squarewSprite;

        // A�ade un componente BoxCollider2D al GameObject
        BoxCollider2D boxCollider = square.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;


        FixedJoint2D fixedJoint = square.AddComponent<FixedJoint2D>();
        // Ajusta la posici�n del cuadro
        square.transform.position = position;

        // Ajusta la escala del cuadro
        square.transform.localScale = scale;

        // A�ade f�sica 2D al cuadro
        Rigidbody2D rigidbody = square.AddComponent<Rigidbody2D>();

        // Configura el objeto padre para que los cuadros sean hijos suyos
        square.transform.parent = parent;

        return square;
    }
    void JoinObjects(GameObject object1, GameObject object2)
    {
        // Add FixedJoint2D component to object1
        FixedJoint2D fixedJoint = object1.AddComponent<FixedJoint2D>();

        // Set the connected object for the joint
        fixedJoint.connectedBody = object2.GetComponent<Rigidbody2D>();
    }




}

#if UNITY_EDITOR
[CustomEditor(typeof(EnemyGrammar))]
class EnemyGrammarEditor : Editor{

    public override void OnInspectorGUI()
    {
        var enemyGrammar = (EnemyGrammar)target;
        if (enemyGrammar == null) return;

        Undo.RecordObject(enemyGrammar, "savingGrammar");

        if (GUILayout.Button("SaveGrammar"))
        {
            if (Application.isPlaying)
            {
                enemyGrammar.saveGrammar();
            }
            else 
            {
                Debug.Log("Game is not running");
            }
            
        }

        DrawDefaultInspector();
    }


}
#endif