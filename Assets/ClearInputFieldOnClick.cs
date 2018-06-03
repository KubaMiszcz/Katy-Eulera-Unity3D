using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class ClearInputFieldOnClick : MonoBehaviour
{
    public GameObject mainInputField;
    public bool endEdit { get; set; }
    void Start()
    {
        endEdit = true;

    }
    void Update()
    {
        //If the input field is focused, change its color to green.
        if (mainInputField.GetComponent<InputField>().isFocused == true && endEdit==true)
        {
            endEdit = false;
            mainInputField.GetComponent<InputField>().text = "";
        }
    }
}



