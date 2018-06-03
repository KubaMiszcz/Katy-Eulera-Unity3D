using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class playclick : MonoBehaviour {

    // Use this for initialization
    public GameObject ukladostatni;
    public Button play; 

    void Start () {
        ukladostatni = GameObject.Find("UklObracany");
        play = this.GetComponent<Button>();
        Debug.Log(ukladostatni.GetComponent<ukladruchomy>().playstate.ToString());
    }
	
	// Update is called once per frame
    void Update()
    {
    }
}
