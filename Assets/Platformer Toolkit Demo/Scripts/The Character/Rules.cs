 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

public class Rules
{
    //class to check reflection variables
    public GameObject objectToSearch;
    public string scriptName;

    //
    private string jsonData;
    public  ReflectionData reflectionData;

    //variables that can be modified 
    public List<Variable<object>> variablesList = new List<Variable<object>>();

    private void Start()
    {
        if (objectToSearch != null)
        {
            Component script = objectToSearch.GetComponent(scriptName);

            if (script != null)
            {
                Debug.Log($"Found the script '{scriptName}' on the assigned object");

                Type scriptType = script.GetType();
                reflectionData = GenerateReflectionData(scriptType);

                jsonData = JsonUtility.ToJson(reflectionData, true);
                string savePath = Path.Combine(Application.dataPath, "ReflectionData.json");
                File.WriteAllText(savePath, jsonData);

                Debug.Log($"Reflection data saved at {savePath}");

                //CODE CONTINUES HERE
                //NEED A FUNCTION THAT WILL GET THE NEXT FUNCTIONS
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
    private static ReflectionData GenerateReflectionData(Type scriptType)
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
            reflectionData.variableNames.Add(field.Name);
            reflectionData.variableTypes.Add(field.FieldType.ToString());
        }

        PropertyInfo[] properties = scriptType.GetProperties();
        foreach (PropertyInfo property in properties)
        {
            reflectionData.PropertyName.Add(property.Name);
        }

        return reflectionData;
    }




    private void PopulateVariablesList(ReflectionData reflectionData) 
    {
        
        for (int i = 0; i < reflectionData.variableNames.Count; i++)
        {
            string name = reflectionData.variableNames[i];
            string typeName = reflectionData.variableTypes[i];

            Type type = Type.GetType(typeName);
            object defaultValue = Activator.CreateInstance(type);

            Variable<object> variable = new Variable<object>(name, defaultValue);
            variablesList.Add(variable);
        }

    }




}


[Serializable]
public class ReflectionData
{
    public List<string> methodNames = new List<string>();
    public List<string> variableNames = new List<string>();
    public List<string> variableTypes = new List<string>();
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

    public Variable getRandomVariable()
    {
        Variable toModify;
        int location = UnityEngine.Random.Range(0, this.varList.Count);
        toModify = this.varList[location];
        return toModify;
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