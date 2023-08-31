 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

public class Rules : MonoBehaviour
{
    //class to check reflection variables
    public GameObject objectToSearch;
    public string scriptName;

    //
    private string jsonData;
    ReflectionData reflectionData;

    //variables that can be modified and the list for neighbors that will be generated >:D
    List<IVariable> variablesList = new List<IVariable>();
    List<IVariable> neighbors = new List<IVariable>();
    IVariable randomVariable;
    public float rangeNeighbors;
    public int quantityNeighbors;

    private void Start()
    {
        if (objectToSearch != null)
        {
            Component script = objectToSearch.GetComponent(scriptName);

            if (script != null)
            {
                Debug.Log($"Found the script '{scriptName}' on the assigned object");

                Type scriptType = script.GetType();
                reflectionData = GenerateReflectionData(scriptType, script);

                jsonData = JsonUtility.ToJson(reflectionData, true);
                string savePath = Path.Combine(Application.dataPath, "ReflectionData.json");
                File.WriteAllText(savePath, jsonData);

                Debug.Log($"Reflection data saved at {savePath}");
                Debug.Log("saving information in variables class");

                //CODE CONTINUES HERE
                PopulateVariablesList(reflectionData);
                neighbors = GenerateNeighbors(randomVariable,rangeNeighbors,quantityNeighbors);
                //printVariables();// is working...
            }
            else
            {
                Debug.Log($"The script '{scriptName}' is not attached to the assigned object.");
            }



        }
        else
        {
            Debug.Log("No object assigned to search for the script.");
        }
    }


    /// <summary>
    /// Extracts the information of the reflection and return a generationData with all the information
    /// </summary>
    /// <param name="scriptType"></param>
    /// <returns></returns>
    private static ReflectionData GenerateReflectionData(Type scriptType, Component script)
    {
        ReflectionData reflectionData = new ReflectionData();

        MethodInfo[] methods = scriptType.GetMethods();
        foreach (MethodInfo method in methods)
        {
            reflectionData.methodNames.Add(method.Name);
        }

        FieldInfo[] fields = scriptType.GetFields();
        foreach (FieldInfo field in fields)
        {
            object variableValue = field.GetValue(script);
            reflectionData.variableNames.Add(field.Name);
            reflectionData.variableTypes.Add(field.FieldType.ToString());
            reflectionData.variableValues.Add(variableValue.ToString());

        }

        PropertyInfo[] properties = scriptType.GetProperties();
        foreach (PropertyInfo property in properties)
        {
            reflectionData.PropertyName.Add(property.Name);
        }

        return reflectionData;
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="reflectionData"></param>
    private void PopulateVariablesList(ReflectionData reflectionData)
    {

        for (int i = 0; i < reflectionData.variableNames.Count; i++)
        {
            string name = reflectionData.variableNames[i];
            string typeName = reflectionData.variableTypes[i];
            string value = reflectionData.variableValues[i];
            // Type type = Type.GetType(typeName);
            // object defaultValue = Activator.CreateInstance(type);

            // Variable<object> variable = new Variable<object>(name, defaultValue);
            //variablesList.Add(variable);

            if (typeName == "System.Single")
            {

                // Variable<float> floatVariable = new Variable<float>(name, float.Parse(value));
                variablesList.Add(new Variable<float>(name, float.Parse(value)));

            }
            else if (typeName == "System.Boolean")
            {
                variablesList.Add(new Variable<bool>(name, bool.Parse(value)));
            }
            else if (typeName == "System.Double")
            {
                variablesList.Add(new Variable<double>(name, double.Parse(value)));
            }
            else if (typeName == "System.Int32")
            {
                variablesList.Add(new Variable<int>(name, int.Parse(value)));
            }
            else if (typeName == "System.String")
            {
                variablesList.Add(new Variable<string>(name, value));
            }
            else if (typeName == "UnityEngine.Vector2")
            {
                value = value.Trim('(', ')');
                Vector2 parsedVector;
                string[] components = value.Split(',');
                if (components.Length == 2 && float.TryParse(components[0], out float x) && float.TryParse(components[1], out float y))
                {
                    parsedVector = new Vector2(x, y);
                    variablesList.Add(new Variable<Vector2>(name, parsedVector)); //here is the add
                    //Debug.Log($"Parsed Vector2: {parsedVector}");
                }
                else
                {
                    Debug.LogError("Invalid vector string format");
                }



            }
            else if (typeName == "UnityEngine.Vector3")
            {
                value = value.Trim('(', ')');
                string[] components = value.Split(',');

                // Parsing the components as floats
                if (components.Length == 3 &&
                    float.TryParse(components[0], out float x) &&
                    float.TryParse(components[1], out float y) &&
                    float.TryParse(components[2], out float z))
                {
                    Vector3 parsedVector = new Vector3(x, y, z);
                    variablesList.Add(new Variable<Vector3>(name, parsedVector)); //here is the add
                    //Debug.Log($"Parsed Vector3: {parsedVector}");
                }
                else
                {
                    Debug.LogError("Invalid vector string format");
                }
            }
            else if (typeName == "UnityEngine.Transform")
            {
                Debug.LogError("Transform not implemented yet...");
            }
            else
            {
                Debug.Log("Variable type not compatible with class Variable prease check if is an important variable");
            }
        }

    }


    public void printVariables(List<IVariable> variablesList)
    {
        foreach (var variable in variablesList)
        {
            Debug.Log($"Name: {variable.Name}, Type: {variable.GetValue().GetType()}, Value: {variable.GetValue()}");
        }

    }

    //we can select a random variable to start the generation... 
    public IVariable getRandomVariable()
    {
        IVariable toModify;
        int location = UnityEngine.Random.Range(0, this.variablesList.Count);
        toModify = this.variablesList[location];
        return toModify;
    }

    // this function needs to retur to the original value of the variable 
    public void resetVariables()
    {
        //this.varList = this.oldVarList; // old code
    }


    //function to update with new changes made...
    public void updatevariables()
    {
        /*
         * OLD CODE
         * aqui creo que solo nececitamos actualizar la variable que fue modificada...
         * 
        if (newChanges)
        {
            
            Debug.Log(" changes were done......");
            movement.knockbackStrength = varList[0].valueFloat;
            movement.knockBackLength = varList[1].valueFloat;
            movement.walkSpeed = varList[2].valueFloat;
            movement.knockbackTimeCount = varList[3].valueFloat;
            movement.knockFromRight = varList[4].valueBool;
            movement.desiredWalkDirection = varList[5].valueFloat;
            movement.knockbackFinished = varList[6].valueBool;
            //jumpMovement.canDoubleJump = varList[7].valueBool;
            //floorD.isTouchingFloor = varList[8].valueBool;
            newChanges = false;
        }*/

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="variable"></param>
    /// <param name="range"></param>
    /// <param name="numNeighbors"></param>
    /// <returns></returns>
    private List<IVariable> GenerateNeighbors(IVariable variable, float range, int numNeighbors)
    {

        List<IVariable> neighbors = new List<IVariable>();


        if (variable.GetValue() is double doubleValue)
        {
            // Ensure distinct neighbors with even distribution
            HashSet<double> neighborValues = new HashSet<double>();
            while (neighborValues.Count < numNeighbors)
            {
                double neighborValue = UnityEngine.Random.Range((float)(doubleValue - range / 2), (float)(doubleValue + range / 2));
                neighborValues.Add(neighborValue);
            }

            int index = 1;
            foreach (var value in neighborValues)
            {
                string neighborName = $"{variable.Name}_neighbor{index}";
                Variable<double> neighborVariable = new Variable<double>(neighborName, value);
                neighbors.Add(neighborVariable);
                index++;
            }
        }
        else if (variable.GetValue() is float floatValue)
        {
            // Ensure distinct neighbors with even distribution
            HashSet<float> neighborValues = new HashSet<float>();
            while (neighborValues.Count < numNeighbors)
            {
                float neighborValue = UnityEngine.Random.Range(floatValue - range / 2, floatValue + range / 2);
                neighborValues.Add(neighborValue);
            }

            int index = 1;
            foreach (var value in neighborValues)
            {
                string neighborName = $"{variable.Name}_neighbor{index}";
                Variable<float> neighborVariable = new Variable<float>(neighborName, value);
                neighbors.Add(neighborVariable);
                index++;
            }
        }
        else if (variable.GetValue() is int intValue)
        {
            // Ensure distinct neighbors with even distribution
            HashSet<int> neighborValues = new HashSet<int>();
            while (neighborValues.Count < numNeighbors)
            {
                int neighborValue = UnityEngine.Random.Range(intValue - (int)(range / 2), intValue + (int)(range / 2) + 1);
                neighborValues.Add(neighborValue);
            }

            int index = 1;
            foreach (var value in neighborValues)
            {
                string neighborName = $"{variable.Name}_neighbor{index}";
                Variable<int> neighborVariable = new Variable<int>(neighborName, value);
                neighbors.Add(neighborVariable);
                index++;
            }
        }
        else if (variable.GetValue() is Vector2 vector2Value)
        {
            // Ensure distinct neighbors with even distribution
            HashSet<Vector2> neighborValues = new HashSet<Vector2>();
            while (neighborValues.Count < numNeighbors)
            {
                Vector2 neighborValue = vector2Value + UnityEngine.Random.insideUnitCircle * range;
                neighborValues.Add(neighborValue);
            }

            int index = 1;
            foreach (var value in neighborValues)
            {
                string neighborName = $"{variable.Name}_neighbor{index}";
                Variable<Vector2> neighborVariable = new Variable<Vector2>(neighborName, value);
                neighbors.Add(neighborVariable);
                index++;
            }
        }
        else if (variable.GetValue() is Vector3 vector3Value)
        {
            // Ensure distinct neighbors with even distribution
            HashSet<Vector3> neighborValues = new HashSet<Vector3>();
            while (neighborValues.Count < numNeighbors)
            {
                Vector3 neighborValue = vector3Value + UnityEngine.Random.insideUnitSphere * range;
                neighborValues.Add(neighborValue);
            }

            int index = 1;
            foreach (var value in neighborValues)
            {
                string neighborName = $"{variable.Name}_neighbor{index}";
                Variable<Vector3> neighborVariable = new Variable<Vector3>(neighborName, value);
                neighbors.Add(neighborVariable);
                index++;
            }
        }
        else if (variable.GetValue() is string stringValue)
        {
            // Ensure distinct neighbors with even distribution
            HashSet<string> neighborValues = new HashSet<string>();
            while (neighborValues.Count < numNeighbors)
            {
                int randomIndex = UnityEngine.Random.Range(0, stringValue.Length);
                char randomChar = (char)('a' + UnityEngine.Random.Range(0, 26));
                string neighborValue = stringValue.Substring(0, randomIndex) + randomChar + stringValue.Substring(randomIndex + 1);
                neighborValues.Add(neighborValue);
            }

            int index = 1;
            foreach (var value in neighborValues)
            {
                string neighborName = $"{variable.Name}_neighbor{index}";
                Variable<string> neighborVariable = new Variable<string>(neighborName, value);
                neighbors.Add(neighborVariable);
                index++;
            }
        }
        else if (variable.GetValue() is bool boolValue) //should I get just a pair of bool? 
        {
            // Generate distinct neighbors for bool variables
            HashSet<bool> neighborValues = new HashSet<bool>();
            while (neighborValues.Count < numNeighbors)
            {
                bool neighborValue = !boolValue; // Flip the value
                neighborValues.Add(neighborValue);
            }

            int index = 1;
            foreach (var value in neighborValues)
            {
                string neighborName = $"{variable.Name}_neighbor{index}";
                Variable<bool> neighborVariable = new Variable<bool>(neighborName, value);
                neighbors.Add(neighborVariable);
                index++;
            }
        }

        return neighbors;

    }

}


[Serializable]
public class ReflectionData
{
    public List<string> methodNames = new List<string>();
    public List<string> variableNames = new List<string>();
    public List<string> variableTypes = new List<string>();
    public List<string> variableValues = new List<string>();
    public List<string> PropertyName = new List<string>();
    
}

/*
    public bool newChanges = false;
    //public NewRules theRules = new NewRules();
    public List<Variable> varList = new List<Variable>();
    public List<Variable> oldVarList = new List<Variable>();


    //this part of the code gets all the different variables that can be stored and 
    //are stored in a List in a Variable format
    public Rules(string variables)
    {

        //Debug.Log(variables);

        Variable bar0 = new Variable("speed", 20f);
        Variable bar1 = new Variable("jumpForce", 8f);
        Variable bar2 = new Variable("position.x", 1f); // with this we are gonna change the transform 
        Variable bar3 = new Variable("position.y", 1f);
        //new variables here
        //gravity maybe? 


         
        //Variable bar4 = new Variable("knockFromRight", false);
        //Variable bar5 = new Variable("desiredWalkDirection", 0.9f);
        //Variable bar6 = new Variable("knockbackFinished", false);
        //Variable bar7 = new Variable("jumpSpeed", 0.8f);

        this.varList.Add(bar0);
        this.varList.Add(bar1);
        this.varList.Add(bar2);
        this.varList.Add(bar3);
        //this.varList.Add(bar4);
        //this.varList.Add(bar5);
        //this.varList.Add(bar6);

        //to store the original values if needed 
        this.oldVarList = this.varList;
    }

    //function to restore old values
    public void resetVariables()
    {
        this.varList = this.oldVarList;
    }

    
    //need to check type of variable also olverload this function.
    //complexity (O) (Nx4)^2 aprox
    public void fillDictWalkSpeed(Variable var, float x1, float x2, float y1, float y2)
    {

        //Variable newVar;
        //String key = "";



        //place to save the dictionary
        saveDict();

    }

    public void saveDict()
    {
        /*

        try
        {
            FileStream writerFileStream = new FileStream("dictionary.dat", FileMode.Create, FileAccess.Write);
            this.formatter.Serialize(writerFileStream, this.VariablesDict);
            writerFileStream.Close();
        }
        catch (Exception)
        {
            Debug.Log("Unable to save dictionary");
        } // 8/ HERE
    }


    //function to update all the new values
    public void updatevariables()
    {
        if (newChanges)
        {
            /*
            Debug.Log(" changes were done......");
            movement.knockbackStrength = varList[0].valueFloat;
            movement.knockBackLength = varList[1].valueFloat;
            movement.walkSpeed = varList[2].valueFloat;
            movement.knockbackTimeCount = varList[3].valueFloat;
            movement.knockFromRight = varList[4].valueBool;
            movement.desiredWalkDirection = varList[5].valueFloat;
            movement.knockbackFinished = varList[6].valueBool;
            //jumpMovement.canDoubleJump = varList[7].valueBool;
            //floorD.isTouchingFloor = varList[8].valueBool;// 8/ HERE
            newChanges = false;
        }

    }




    ///////////////////////////////// New Rules???
    ///


    //Conditions condition;
    Variable var;

    // LISTA DE VARIABLES
    //public List<Variable> varList = new List<Variable>();
    public Conditions con = new Conditions();
    public int variablePlace;
    public int than;
    public int value;
    public bool randomGenerated = false;


    //public Condition con = new Condition();
    public Effect eff = new Effect();



    public List<Structure> getNeighbors(Structure generated)
    {
        //Debug.Log(this.variablePlace);
        //Debug.Log(varlist.Count);
        List<Structure> newListRules = new List<Structure>();

        int step = 1;
        if (this.randomGenerated)
        {
            for (int j = 0; j < 3; j++)  //setValues(string name, string comparator, string valueComparator, string effect, string valueEffect)
            {
                Structure newNeighbor = new Structure();
                newNeighbor.setValues(generated.name, xCondition(j).ToString(), generated.valueComparator, generated.effect, generated.valueEffect);
                //generated.setComparator(xCondition(j).ToString());
                for (int i = 0; i < 5; i++)
                {
                    float valueComparator = float.Parse(generated.valueComparator);
                    float valueEffect = float.Parse(generated.valueEffect);
                    float vcm = valueComparator - (float)step;  // value comparator minus
                    float vcp = valueComparator + (float)step; // value comparator plus
                    float vem = valueEffect - (float)step;
                    float vep = valueEffect + (float)step;

                    Structure newNeighbor1 = new Structure();
                    Structure newNeighbor2 = new Structure();
                    Structure newNeighbor3 = new Structure();
                    Structure newNeighbor4 = new Structure();
                    Structure newNeighbor5 = new Structure();
                    Structure newNeighbor6 = new Structure();
                    Structure newNeighbor7 = new Structure();
                    Structure newNeighbor8 = new Structure();
                    Structure newNeighbor9 = new Structure();


                    newNeighbor1.setValues(generated.name, xCondition(j).ToString(), generated.valueComparator, xEffect(i).ToString(), generated.valueEffect);
                    newNeighbor2.setValues(generated.name, xCondition(j).ToString(), generated.valueComparator, xEffect(i).ToString(), vem.ToString());
                    newNeighbor3.setValues(generated.name, xCondition(j).ToString(), generated.valueComparator, xEffect(i).ToString(), vep.ToString());
                    newNeighbor4.setValues(generated.name, xCondition(j).ToString(), vcm.ToString(), xEffect(i).ToString(), generated.valueEffect);
                    newNeighbor5.setValues(generated.name, xCondition(j).ToString(), vcm.ToString(), xEffect(i).ToString(), vem.ToString());
                    newNeighbor6.setValues(generated.name, xCondition(j).ToString(), vcm.ToString(), xEffect(i).ToString(), vep.ToString());
                    newNeighbor7.setValues(generated.name, xCondition(j).ToString(), vcp.ToString(), xEffect(i).ToString(), generated.valueEffect);
                    newNeighbor8.setValues(generated.name, xCondition(j).ToString(), vcp.ToString(), xEffect(i).ToString(), vem.ToString());
                    newNeighbor9.setValues(generated.name, xCondition(j).ToString(), vcp.ToString(), xEffect(i).ToString(), vep.ToString());



                    newListRules.Add(newNeighbor2);
                    newListRules.Add(newNeighbor3);
                    newListRules.Add(newNeighbor4);
                    newListRules.Add(newNeighbor5);
                    newListRules.Add(newNeighbor6);
                    newListRules.Add(newNeighbor7);
                    newListRules.Add(newNeighbor8);
                    newListRules.Add(newNeighbor9);

                }
            }

            for (int i = 0; i < newListRules.Count; i++)
            {
                if (newListRules[i].valueComparator == generated.valueComparator & newListRules[i].valueEffect == generated.valueEffect)
                {
                    if (newListRules[i].comparator == generated.comparator & newListRules[i].effect == generated.effect)
                    {
                        newListRules.RemoveAt(i);
                    }
                }
            }

        }


        Debug.Log("neighborsDone");
        //printList(newListRules);
        return newListRules;
    }

    public void printList(List<Structure> varList)
    {
        for (int i = 0; i < varList.Count; i++)
        {
            Debug.Log(varList[i].name + " " + varList[i].comparator + " " + varList[i].valueComparator + " " + varList[i].effect + " " + varList[i].valueEffect);
        }
        Debug.Log("endGetNeigbors List");
    }


    public Structure getRandomRule(List<Variable> varList, int x1, int x2, int y1, int y2)
    {
        List<Variable> newVar = varList;
        Variable toModify;
        Structure generatedRule = new Structure();



        Conditions.conditions c = randomCondtion();
        Effect.effects e = randomEffect();

        //Random rd = new Random();
        int location = UnityEngine.Random.Range(0, varList.Count);
        this.variablePlace = location;


        toModify = newVar[location];


        if (toModify.isINT())
        {
            this.than = UnityEngine.Random.Range(x1, x2);
            this.value = UnityEngine.Random.Range(y1, y2);
            generatedRule.setValues(toModify.nameVariable, c.ToString(), this.than.ToString(), e.ToString(), this.value.ToString());
            this.randomGenerated = true;
        }

        if (toModify.isFLOAT())
        {
            this.than = UnityEngine.Random.Range(x1, x2);
            this.value = UnityEngine.Random.Range(y1, y2);
            generatedRule.setValues(toModify.nameVariable, c.ToString(), this.than.ToString(), e.ToString(), this.value.ToString());
            this.randomGenerated = true;
        }

        if (toModify.isBOOL())
        {
            //for bool if is true is gonna be false and if is false is gonna be true.
           // Debug.Log("Variable modified was bool");
            //toModify.valueBool = !toModify.valueBool;

        }

        // FILE

        //Metrics agentMetrics = JsonUtility.FromJson<Metrics>(json);


        //con.applyCondition();
        //newVar[this.variablePlace] = toModify;
        //Debug.Log(this.key);
        return generatedRule;
    }






    [System.Serializable]
    private class ruleGeneratedtoFile
    {
        public string generatedRule;


    }


    public Conditions.conditions randomCondtion()
    {
        System.Array A = System.Enum.GetValues(typeof(Conditions.conditions));
        Conditions.conditions x = (Conditions.conditions)A.GetValue(UnityEngine.Random.Range(0, 3));
        return x;
    }

    public Conditions.conditions randomCondtionBool()
    {
        System.Array A = System.Enum.GetValues(typeof(Conditions.conditions));
        Conditions.conditions x = (Conditions.conditions)A.GetValue(UnityEngine.Random.Range(3, 5));
        return x;
    }

    public Effect.effects randomEffect()
    {
        System.Array A = System.Enum.GetValues(typeof(Effect.effects));
        Effect.effects x = (Effect.effects)A.GetValue(UnityEngine.Random.Range(0, 5));
        return x;
    }

    public Effect.effects xEffect(int e)
    {
        System.Array A = System.Enum.GetValues(typeof(Effect.effects));
        Effect.effects x = (Effect.effects)A.GetValue(e);
        return x;
    }

    public Conditions.conditions xCondition(int e)
    {
        System.Array A = System.Enum.GetValues(typeof(Effect.effects));
        Conditions.conditions x = (Conditions.conditions)A.GetValue(e);
        return x;
    }
    public Conditions.conditions getConditionUsed(string cond)
    {
        switch (cond)
        {
            case "biggerThan":
                return Conditions.conditions.lessThan;
            case "lessThan":
                return Conditions.conditions.biggerThan;
            case "equalThan":
                return Conditions.conditions.equalThan;
            default:
                return Conditions.conditions.COUNT;
        }

    }

    public Effect.effects getEffectUsed(string cond)
    {
        switch (cond)
        {
            case "add":
                return Effect.effects.add;
            case "subtract":
                return Effect.effects.subtract;
            case "multiply":
                return Effect.effects.multiply;
            case "divide":
                return Effect.effects.divide;
            case "residue":
                return Effect.effects.residue;
            default:
                return Effect.effects.change;
        }
    }

    
    public int getEffectUsedtoInt(string cond)
    {
        switch (cond)
        {
            case "add":
                return 0;
            case "subtract":
                return 1;
            case "multiply":
                return 2;
            case "divide":
                return 3;
            case "residue":
                return 4;
            default:
                return -1;
        }
    }


}
*/