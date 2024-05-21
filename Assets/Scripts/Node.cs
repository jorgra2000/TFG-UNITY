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
        return id;
    }

    public void OnTriggerEnter(){
        
        if(controllerScript.CheckNode(id))
        {
            if(changeVariables)
            {
                controllerScript.CalculateVariables(1,1);
                controllerScript.CalculateVariables(0,controllerScript.GetInputValue());
                controllerScript.UpdateVariablesText();
            }
        }

    }
}
