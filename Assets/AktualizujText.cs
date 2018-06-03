using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class AktualizujText : MonoBehaviour {
    private GameObject goSliderPauza;
    private GameObject goSliderSzybkosc;
    private GameObject goSliderSzybkoscZoom;
    private GameObject goSliderSzybkoscPan;
    private GameObject goifPauza;
    private GameObject goifSzybkosc;
    private GameObject goifSzybkoscZoom;
    private GameObject goifSzybkoscPan;
    public float czaspauzy;
    public float rotationspeed;
    public float zoomSpeed;
    public float szybkoscPan;

    // Use this for initialization
    void Start () {
        goSliderPauza = GameObject.Find("SliderPauza");
        goSliderSzybkosc = GameObject.Find("SliderSzybkosc");
        goSliderSzybkoscZoom = GameObject.Find("SliderSzybkoscZoom");
        goSliderSzybkoscPan = GameObject.Find("SliderSzybkoscPan");
        goifPauza = GameObject.Find("InputFieldPauza");
        goifSzybkosc = GameObject.Find("InputFieldSzybkosc");
        goifSzybkoscZoom = GameObject.Find("InputFieldSzybkoscZoom");
        goifSzybkoscPan = GameObject.Find("InputFieldSzybkoscPan");
        Aktualizuj();
    }
	
	public void Aktualizuj () {
        czaspauzy = goSliderPauza.GetComponent<Slider>().value;
        rotationspeed = goSliderSzybkosc.GetComponent<Slider>().value;
        zoomSpeed = goSliderSzybkoscZoom.GetComponent<Slider>().value;
        szybkoscPan = goSliderSzybkoscPan.GetComponent<Slider>().value;
        goifPauza.GetComponent<InputField>().text = Math.Round(czaspauzy, 1).ToString();
        goifSzybkosc.GetComponent<InputField>().text = Math.Round(rotationspeed, 1).ToString();
        goifSzybkoscZoom.GetComponent<InputField>().text = Math.Round(zoomSpeed, 1).ToString();
        goifSzybkoscPan.GetComponent<InputField>().text = Math.Round(szybkoscPan, 1).ToString();
    }
}
