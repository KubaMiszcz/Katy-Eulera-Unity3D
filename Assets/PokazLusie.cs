using UnityEngine;
using System.Collections;

public class PokazLusie : MonoBehaviour {
    public float czas=2f;
	// Use this for initialization
	void Start () {
        transform.GetComponent<Canvas>().enabled = false;
    }
	
	// Update is called once per frame
	public void Pokaz() {
        StartCoroutine(MignijLusie());
    }

    IEnumerator MignijLusie()
    {
        transform.GetComponent<Canvas>().enabled = true;

        yield return new WaitForSeconds(czas);
        transform.GetComponent<Canvas>().enabled = false;
    }

}
