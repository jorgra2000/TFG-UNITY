using UnityEngine;

public class Node : MonoBehaviour
{
    private Nodes controllerScript;

    [SerializeField] private int id;
    [SerializeField] private bool changeVariables;

    private void Start()
    {
        controllerScript = GameObject.Find("Controller").GetComponent<Nodes>();
    }
    
    public int GetId()
    {
        try
        {
            return id;
        }
        catch
        {
            return 0;
        }

    }

    public void OnTriggerEnter(){
        
        if(controllerScript.CheckNode(id))
        {
            if(changeVariables)
            {
                try
                {
                    controllerScript.CalculateVariables(1,1);
                }
                catch
                {
                    print("Solo una variable");
                }

                controllerScript.CalculateVariables(0,controllerScript.GetInputValue());
                controllerScript.UpdateVariablesText();
            }
        }

    }
}
