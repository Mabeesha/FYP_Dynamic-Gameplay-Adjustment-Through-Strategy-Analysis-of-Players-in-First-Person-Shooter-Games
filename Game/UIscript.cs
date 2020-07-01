using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIscript : MonoBehaviour
  {

    public GameObject questionGroup;
    public GameObject AnswerPanel;

    public static string result = "";

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void submitAnswer()
    {
        Debug.Log("Calling submit");
        //string result = "";
        GameObject a = questionGroup.transform.Find("Answer").gameObject;

        if (a.GetComponent<ToggleGroup>() !=null )
        {
            bool isSelected = false;
            for ( int i=0; i<a.transform.childCount; i++ )
            {
                if (a.transform.GetChild(i).GetComponent<Toggle>().isOn)
                {
                    isSelected = true;
                    result = a.transform.GetChild(i).Find("Label").GetComponent<Text>().text;
                    break;
                }
            }

            if (!isSelected)
            {
                result = "Not_defined";
            }
            
        }
        Debug.Log(" Result :"+ result);

        //return result;


    }

}










