using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using UnityEditor;

public class obroty : MonoBehaviour {

	public int fi=0;
	public int theta=0;
	public int psi=0;
	public int playstate=0;
    public float myRotationSpeed = 100f;
    public float rotatetime = 1f;
    bool aboutpanel=false;
    public float i = 0;


    // Use this for initialization
    void Start () {

	}

	// Update is called once per frame
	void Update () {


//obort wokol Z
		if (Input.GetKey (KeyCode.Q)) {
			transform.RotateAround(transform.position, transform.up, Time.deltaTime * myRotationSpeed);
			//		transform.position += Vector3.left * Time.deltaTime;
			Debug.Log ("Q");
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.RotateAround(transform.position, transform.up, Time.deltaTime * -myRotationSpeed);
				//		transform.position += Vector3.left * Time.deltaTime;
			Debug.Log ("A");
		}

//obort wokol Y
		if (Input.GetKey (KeyCode.W)) {
			transform.RotateAround(transform.position, transform.right, Time.deltaTime * myRotationSpeed);
			//		transform.position += Vector3.left * Time.deltaTime;
			Debug.Log ("w");
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.RotateAround(transform.position, transform.right, Time.deltaTime * -myRotationSpeed);
			//		transform.position += Vector3.left * Time.deltaTime;
			Debug.Log ("s");
		}

//obort wokol Z
		if (Input.GetKey (KeyCode.E)) {
			transform.RotateAround(transform.position, transform.up, Time.deltaTime * myRotationSpeed);
			//		transform.position += Vector3.left * Time.deltaTime;
			Debug.Log ("e");
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.RotateAround(transform.position, transform.up, Time.deltaTime * -myRotationSpeed);
			//		transform.position += Vector3.left * Time.deltaTime;
			Debug.Log ("r");
		}



		if (playstate > 0) {
			StartCoroutine(obracaj());
			//StartCoroutine(RotateZXZ2());
		}

		//WireArcExample myObj = (WireArcExample) target;
		//DrawSolidArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius);
		//Handles.DrawSolidArc(transform.position, transform.up, transform.right, 90, 50);


		//transform.Rotate(0, myRotationSpeed * Time.deltaTime, 0);
		//Debug.Log (transform.eulerAngles.y.ToString ());
				
		
		//Debug.Log ("updatee");
		if (aboutpanel == true) {
			GameObject.Find ("Aboutpanel").SetActive(true);
		}
	}
	//##############################################	########################	########################	

	public void play(){
		playstate = 1;
	}

	IEnumerator obracaj() {
		if (playstate == 1) { //obrot Z1
			if (i < fi) {
				transform.RotateAround (transform.position, transform.up, fi * Time.deltaTime);
				i += fi * Time.deltaTime;
				yield return null;
			} else {
				transform.RotateAround (transform.position, transform.up, -(i - fi));
				i = 0;
				playstate += 1;
				Debug.Log ("L=" + transform.localEulerAngles.ToString ());
				Debug.Log ("G=" + transform.eulerAngles.ToString ());
			}
		}
		if (playstate == 2) { //obrot X
			if (i < theta) {
				transform.RotateAround (transform.position, transform.right, theta * Time.deltaTime);
				i += theta * Time.deltaTime;
				yield return null;
			} else {
				transform.RotateAround (transform.position, transform.right, -(i - theta));
				i = 0;
				playstate += 1;
				Debug.Log ("L=" + transform.localEulerAngles.ToString ());
				Debug.Log ("G=" + transform.eulerAngles.ToString ());
			}
		}
		if (playstate == 3) { //obrot Z2
			if (i < fi) {
				transform.RotateAround (transform.position, transform.up, psi * Time.deltaTime);
				i += psi * Time.deltaTime;
				yield return null;
			} else {
				transform.RotateAround (transform.position, transform.up, -(i - psi));
				i = 0;
				playstate = 0;
				Debug.Log ("L=" + transform.localEulerAngles.ToString ());
				Debug.Log ("G=" + transform.eulerAngles.ToString ());
			}
		}
	//	Debug.Log ("G=" + transform.localEulerAngles.ToString ());
	//	Debug.Log ("L=" + transform.eulerAngles.ToString ());
		
	}





	public void obracaj2(){
		//if (Vector3.Distance(transform.eulerAngles, to) > 1f)
		if (playstate == 1) {
			if (transform.eulerAngles.y < fi) {
				transform.eulerAngles += new Vector3 (0, Time.deltaTime * fi * myRotationSpeed, 0);
			} else {
				transform.eulerAngles = new Vector3 (0, fi, 0);
				playstate += 1;
			}
		}

		if (playstate == 2) {
			if (transform.eulerAngles.x < theta) {
				transform.eulerAngles += new Vector3 (Time.deltaTime * theta * myRotationSpeed, 0,0);
			} else {
				transform.eulerAngles = new Vector3 (0, theta, 0);
				playstate += 1;
			}
		}

		if (playstate == 3) {
			if (transform.eulerAngles.z < psi) {
				transform.eulerAngles += new Vector3 (0, 0,Time.deltaTime * psi * myRotationSpeed);
			} else {
				transform.eulerAngles = new Vector3 (0, 0,psi);
				playstate = 0;
			}
		}

}


	IEnumerator RotateZXZ2() {
		for (var t = 0f; t <= 1; t += Time.deltaTime/rotatetime) {
				transform.RotateAround (transform.position, transform.up, fi*Time.deltaTime);
				yield return null;
		}

		Debug.Log ("G=" + transform.localEulerAngles.ToString ());
		Debug.Log ("L=" + transform.eulerAngles.ToString ());

}

																			IEnumerator RotateZXZ1() {
																				//int n = byAngles % 360;
																				var byAngles = new Vector3[3];
																				//Vector3 byAngles = new Vector3[2];
																				byAngles[0]= new Vector3(0,fi,0);	//bo nasze pionowe Z to w unity Y
																				byAngles[1] = new Vector3(theta,0,0);	
																				byAngles[2]= new Vector3(0,psi,0);	
																				
																				for (int i=0; i<=2; i++) {
																					var fromAngle = transform.localRotation;
																					var toAngle = Quaternion.Euler (transform.eulerAngles + byAngles [i]);
																					for (var t = 0f; t < 1; t += Time.deltaTime/rotatetime) {
																						transform.localRotation = Quaternion.Lerp (fromAngle, toAngle, t);
																						yield return null;
																					}
																					transform.localRotation = toAngle;
																					Debug.Log ("R["+i.ToString()+"]=" + transform.localEulerAngles.ToString ());
																				}
																			}



																				IEnumerator RotateMe(Vector3 byAngles) {
																					//int n = byAngles % 360;
																					//var fromEuler = transform.eulerAngles;
																					var fromAngle = transform.rotation;
																					var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
																					for(var t = 0f; t < 1; t += Time.deltaTime/rotatetime) {
																						transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
																						yield return null;
																					}
																					//transform.eulerAngles = fromEuler + byAngles;
																					transform.rotation = toAngle;
																					Debug.Log ("x="+transform.eulerAngles.y.ToString ());
																				}

	IEnumerator Example() {
		print(Time.time);
		yield return new WaitForSeconds(5);
		print(Time.time);
	}

	public void reset(){
		Debug.Log("reset");	
		transform.eulerAngles = new Vector3 (0, 0, 0); 
		GameObject.Find ("InputFieldFi").GetComponent<InputField> ().text="30";
		GameObject.Find ("InputFieldTheta").GetComponent<InputField> ().text="30";
		GameObject.Find ("InputFieldPsi").GetComponent<InputField> ().text="30";
		
	}
	public void CloseApp(){
		Debug.Log ("exittt");
		Application.Quit();
	}
	public void Aboutpanel(){
		Debug.Log ("about");
		aboutpanel = true;
	}

	public IEnumerator czekaj(int dt) {
		//print(Time.time);
		yield return new WaitForSeconds(dt);
		//print(Time.time);
	}
}




