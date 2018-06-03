using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class orbitkuba : MonoBehaviour {
    public GameObject target;
    public GameObject goSliderSzybkoscZoom;

    private Vector3 distance;
    private float _x, _y;
    private float _xSpeed,_ySpeed;
    private float _zoomStep;
    public float offset;

    // Use this for initialization
    void Start () {
        distance = target.transform.position - this.transform.position;
        _xSpeed = 1f;
        _ySpeed=1f;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (Input.GetMouseButton(2))
        {

            _x = -Input.GetAxis("Mouse X") * _xSpeed;
            _y = -Input.GetAxis("Mouse Y") * _ySpeed;

            transform.Translate(_x, _y, 0);
            transform.LookAt(target.transform);
        }
        
        Zoom();

    }


    void Zoom()
    {
        _zoomStep = goSliderSzybkoscZoom.GetComponent<Slider>().value;

        if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
        {
            
            transform.GetComponent<Camera>().orthographicSize += _zoomStep / 100f;
            // this.ZoomOut();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
        {
            transform.GetComponent<Camera>().orthographicSize -= _zoomStep / 100f;
            //  this.ZoomIn();
        }

    }
}
