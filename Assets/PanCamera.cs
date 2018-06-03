using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanCamera : MonoBehaviour {
    public float mouseSensitivity = 20f;
    private GameObject goSliderSzybkoscPan;
    private Vector3 lastPosition ;
    public int x=0, y=0;

    void Start()
    {
        goSliderSzybkoscPan = GameObject.Find("SliderSzybkoscPan");
        mouseSensitivity = goSliderSzybkoscPan.GetComponent<Slider>().value;
    }
        void LateUpdate()
    {
        mouseSensitivity = goSliderSzybkoscPan.GetComponent<Slider>().value;
        if (Input.GetMouseButtonDown(1))
        {
            lastPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastPosition;
            transform.Translate(-delta.x * mouseSensitivity/1000f, -delta.y * mouseSensitivity/1000f, 0);
            lastPosition = Input.mousePosition;
        }
    }

    public void Przesun() {
        transform.position= new Vector3(x / 1000f, y / 1000f, 0);
    }
}
