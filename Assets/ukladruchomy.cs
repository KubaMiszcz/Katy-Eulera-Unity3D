using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ukladruchomy : MonoBehaviour
{
    //wiem ze mi wyszla superklasa ale nie ogarniam tego jescze a wazne ze to dziala:]

    public float fi = 0;
    public float theta = 0;
    public float psi = 0;
    public int playstate;
    public float myRotationSpeed = 100f;
    private float i = 0;
    public float czasPauzy;
    private float dangle;
    private bool koniec = false;
    private GameObject goSliderPauza;
    private GameObject gofi,gotheta, gopsi;
    private GameObject goSliderSzybkosc;
    private GameObject goUklObracanycienR1;
    private GameObject goUklObracanycienR2;
    private GameObject imglogoKIA;
    private GameObject btnPause;
    private GameObject btnPlay;
    private GameObject lblObrotuR1, lblObrotuR2, lblObrotuR3;
    private GameObject cOpcje, cUstawienia, cPomoc;
    private GameObject goWyborR1, goWyborR2, goWyborR3;
    private GameObject goWyborR1gm, goWyborR2gm, goWyborR3gm;
    private Vector3 osR1, osR2, osR3;
    private GameObject goObrotR1, goObrotR2, goObrotR3;

    private GameObject R1Arc, R1plaszczObrotu,R1txtAngle;
    private GameObject R2Arc, R2plaszczObrotu, R2txtAngle;
    private GameObject R3Arc, R3plaszczObrotu, R3txtAngle;
    public bool pokazujPlaszczyznyObrotu = true;
    private GameObject tglPokazPlaszczyzny1,  tglPokazPlaszczyzny2;
    private GameObject[] plaszczyznyObrotuWszystkie;
    private GameObject[] imgArcWszystkie;
    private GameObject[] txtAngleWszystkie;
    private GameObject btnFi, btnTheta, btnPsi;
    private float fiPrev,thetaPrev,psiPrev;
    private GameObject[] goOpisyOsiWszystkie;
    private GameObject[] goOpisyOsiR1; private GameObject[] goOpisyOsiR2; private GameObject[] goOpisyOsiR3;

    public GameObject MainCamera { get; private set; }

    // Use this for initialization
    void Start()
    {
        plaszczyznyObrotuWszystkie = GameObject.FindGameObjectsWithTag("TagPlaszczyznyObrotu");
        imgArcWszystkie = GameObject.FindGameObjectsWithTag("TagimgArc");
        txtAngleWszystkie = GameObject.FindGameObjectsWithTag("TagtxtAngle");
        MainCamera = GameObject.Find("Main Camera");
        goOpisyOsiWszystkie = GameObject.FindGameObjectsWithTag("RotacjaNaKamere");
        goOpisyOsiR1 = GameObject.FindGameObjectsWithTag("opisyOsiR1");
        goOpisyOsiR2 = GameObject.FindGameObjectsWithTag("opisyOsiR2");
        goOpisyOsiR3 = GameObject.FindGameObjectsWithTag("opisyOsiR3");

        gofi = GameObject.Find("InputFi");
        gotheta = GameObject.Find("InputTheta");
        gopsi = GameObject.Find("InputPsi");
        goSliderPauza = GameObject.Find("SliderPauza");
        goSliderSzybkosc = GameObject.Find("SliderSzybkosc");
        cOpcje = GameObject.Find("cOpcje");                 cOpcje.GetComponent<Canvas>().enabled = false;
        cUstawienia = GameObject.Find("cUstawienia");       cUstawienia.GetComponent<Canvas>().enabled = false;
        cPomoc = GameObject.Find("cPomoc");                 cPomoc.GetComponent<Canvas>().enabled = false;
        goUklObracanycienR1 = GameObject.Find("UklObracanycienR1");
        goUklObracanycienR2 = GameObject.Find("UklObracanycienR2");
        btnPause = GameObject.Find("btnPause");
        btnPlay = GameObject.Find("btnPlay");
        btnFi = GameObject.Find("ButtonFi");
        btnTheta = GameObject.Find("ButtonTheta");
        btnPsi = GameObject.Find("ButtonPsi");
        lblObrotuR1 = GameObject.Find("lblObrotuR1");
        lblObrotuR2 = GameObject.Find("lblObrotuR2");
        lblObrotuR3 = GameObject.Find("lblObrotuR3");
        goWyborR1 = GameObject.Find("WyborR1");
        goWyborR2 = GameObject.Find("WyborR2");
        goWyborR3 = GameObject.Find("WyborR3");
        goWyborR1gm = GameObject.Find("WyborR1gm");
        goWyborR2gm = GameObject.Find("WyborR2gm");
        goWyborR3gm = GameObject.Find("WyborR3gm");
        tglPokazPlaszczyzny1 = GameObject.Find("tglPokazPlaszczyzny1");
        tglPokazPlaszczyzny2 = GameObject.Find("tglPokazPlaszczyzny2");


        AktualizujKombinacjeObrotow2();
        PokazPlaszczyznyObrotu();
        AktualizujUstawienia();
        Debug.Log("startt");
        reset();

    }

    public void reset()
    {
        StopAllCoroutines();
        ZerujUklad();
        gofi.GetComponent<InputField>().text = "45"; fiPrev = 45f;
        gotheta.GetComponent<InputField>().text = "30"; thetaPrev = 45f;
        gopsi.GetComponent<InputField>().text = "120"; psiPrev = 45f;
        aktualizujkaty();
        playstate = 0;
    }


    //Update is called once per frame
    void Update()
    {
       
        if (playstate > 0)
        {
           StartCoroutine(obracaj());   
        }

        //ustaw wszystkei opisy w strone kamery
        foreach (GameObject go in goOpisyOsiWszystkie)
        {
            go.transform.rotation=MainCamera.transform.rotation;
        }
        foreach (GameObject go in txtAngleWszystkie)
        {
            go.transform.rotation = MainCamera.transform.rotation;
        }
    }

    public void AktualizujWyborRwGornymMenu() {
        goWyborR1gm.GetComponent<Dropdown>().value = goWyborR1.GetComponent<Dropdown>().value;
        goWyborR2gm.GetComponent<Dropdown>().value = goWyborR2.GetComponent<Dropdown>().value;
        goWyborR3gm.GetComponent<Dropdown>().value = goWyborR3.GetComponent<Dropdown>().value;
    }

    public void AktualizujWyborRwOpcjach()
    {
        goWyborR1.GetComponent<Dropdown>().value = goWyborR1gm.GetComponent<Dropdown>().value;
        goWyborR2.GetComponent<Dropdown>().value = goWyborR2gm.GetComponent<Dropdown>().value;
        goWyborR3.GetComponent<Dropdown>().value = goWyborR3gm.GetComponent<Dropdown>().value;
        AktualizujKombinacjeObrotow2();
    }

    IEnumerator obracaj() {
     AktualizujKombinacjeObrotow2();
     if (playstate == 1)
        { //obrot R1
            koniec = false;
                    btnPause.GetComponent<Image>().enabled = true;
                    btnPlay.GetComponent<Image>().enabled = false;
                    R1Arc.GetComponent<Image>().enabled = true; 
                    R1txtAngle.SetActive(true);
                    foreach (GameObject go in goOpisyOsiR1) { go.SetActive(true); }
                    R1txtAngle.GetComponent<TextMesh>().text = "";
                    R1plaszczObrotu.GetComponent<Image>().enabled = pokazujPlaszczyznyObrotu;
                    Vector3[] osieObrotow = { -transform.right, -transform.forward, -transform.up };
                    int idx = goWyborR1.GetComponent<Dropdown>().value;
                    osR1 = osieObrotow[idx];
            
            if ( Math.Abs(i) < Math.Abs(fi))
            {

                dangle = fi * myRotationSpeed * Time.deltaTime * 0.01f;
                Debug.Log(dangle.ToString());
                transform.RotateAround(transform.position, osR1, dangle);
                goUklObracanycienR1.transform.RotateAround(transform.position, osR1, dangle);
                goUklObracanycienR2.transform.RotateAround(transform.position, osR1, dangle);
                i += dangle;
                R1Arc.GetComponent<Image>().fillAmount = Math.Abs(i)/360f;
                R1txtAngle.GetComponent<TextMesh>().text = Math.Round(i,1).ToString();
            }
            else {
                transform.RotateAround(transform.position, osR1, -(i - fi));
                goUklObracanycienR1.transform.RotateAround(transform.position, osR1, -(i - fi));
                goUklObracanycienR2.transform.RotateAround(transform.position, osR1, -(i - fi));
                R1Arc.GetComponent<Image>().fillAmount = Math.Abs(fi) / 360f;
                R1txtAngle.GetComponent<TextMesh>().text = Math.Round(fi, 1).ToString();
                // Debug.Log((i-(i - fi)).ToString());
                playstate = 2;
                i = 0;
            }
       }

        if (playstate == 2) {
            yield return new WaitForSeconds(czasPauzy);
            StopAllCoroutines();
            playstate =3;
        }



        if (playstate == 3) { //obrot R2
            R2Arc.GetComponent<Image>().enabled = true;
            R2txtAngle.SetActive(true);
            foreach (GameObject go in goOpisyOsiR2) { go.SetActive(true); }
            R2txtAngle.GetComponent<TextMesh>().text = "";
            R2plaszczObrotu.GetComponent<Image>().enabled = pokazujPlaszczyznyObrotu;
            Vector3[] osieObrotow = { -transform.right, -transform.forward, -transform.up };
            int idx = goWyborR2.GetComponent<Dropdown>().value;
            osR2 = osieObrotow[idx];
            if (Math.Abs(i) < Math.Abs(theta))
            {
                dangle = theta * myRotationSpeed * Time.deltaTime * 0.01f;
                transform.RotateAround(transform.position, osR2, dangle);
                goUklObracanycienR2.transform.RotateAround(transform.position, osR2, dangle);
                i += dangle;
                R2Arc.GetComponent<Image>().fillAmount = Math.Abs(i) / 360f;
                R2txtAngle.GetComponent<TextMesh>().text = Math.Round(i, 1).ToString();
            }
            else {
                transform.RotateAround(transform.position, osR2, -(i - theta));
                goUklObracanycienR2.transform.RotateAround(transform.position, osR2, -(i - theta));
                R2Arc.GetComponent<Image>().fillAmount = Math.Abs(theta) / 360f;
                R2txtAngle.GetComponent<TextMesh>().text = Math.Round(theta, 1).ToString();
               // Debug.Log((i-(i - theta)).ToString());
                i = 0;
                playstate = 4;
            }          
        }

        if (playstate == 4)
        {
            yield return new WaitForSeconds(czasPauzy);
            StopAllCoroutines();
            playstate = 5;
        }


        if (playstate == 5)  { //obrot Z2
                        R3Arc.GetComponent<Image>().enabled = true;
                        R3txtAngle.SetActive(true);
                        foreach (GameObject go in goOpisyOsiR3) { go.SetActive(true); }
                        R3txtAngle.GetComponent<TextMesh>().text = "";
                        R3plaszczObrotu.GetComponent<Image>().enabled = pokazujPlaszczyznyObrotu;
                        Vector3[] osieObrotow = { -transform.right, -transform.forward, -transform.up };
                        int idx = goWyborR3.GetComponent<Dropdown>().value;
                        osR3 = osieObrotow[idx];
            if (Math.Abs(i) < Math.Abs(psi))
            {
                dangle = psi * myRotationSpeed * Time.deltaTime * 0.01f;
                transform.RotateAround(transform.position, osR3, dangle);
                i += dangle;
                R3Arc.GetComponent<Image>().fillAmount = Math.Abs(i) / 360f;
                R3txtAngle.GetComponent<TextMesh>().text = Math.Round(i, 1).ToString();
                //yield return new WaitForSeconds(1);
                //yield return null;
            }
            else {
                transform.RotateAround(transform.position, osR3, -(i - psi));
                R3Arc.GetComponent<Image>().fillAmount = Math.Abs(psi) / 360f;
                R3txtAngle.GetComponent<TextMesh>().text = Math.Round(psi, 1).ToString();
                playstate = 0;
               // Debug.Log((i-(i - psi)).ToString());
                btnPlay.GetComponent<Image>().enabled = true;
                btnPause.GetComponent<Image>().enabled = false;
                i = 0;
                koniec = true;
            }
        }        
    }
 

    public void aktualizujkaty()
    {
        if (gofi.GetComponent<InputField>().text == "") gofi.GetComponent<InputField>().text = fiPrev.ToString();
        if (gotheta.GetComponent<InputField>().text == "") gotheta.GetComponent<InputField>().text = thetaPrev.ToString();
        if (gopsi.GetComponent<InputField>().text == "") gopsi.GetComponent<InputField>().text = psiPrev.ToString();
        fi = float.Parse((gofi.GetComponent<InputField>().text));
        theta = float.Parse((gotheta.GetComponent<InputField>().text));
        psi = float.Parse((gopsi.GetComponent<InputField>().text));
        fiPrev = fi;
        thetaPrev = theta;
        psiPrev = psi;
    }

    IEnumerator czekaj(float czas)
    {
        Debug.Log("start odlicznia");
        yield return new WaitForSeconds(czas);
        Debug.Log("koniec odlicznia");
    }

    public void AktualizujOpcje()
    {
        AktualizujKombinacjeObrotow2();
        PokazPlaszczyznyObrotu();
    }

    public void AktualizujUstawienia()
    {
        AktualizujCzasPauzy();
        AktualizujSzybkoscObrotow();
        
    }

    public void AktualizujCzasPauzy() {
        czasPauzy=goSliderPauza.GetComponent<Slider>().value;
    }

    public void AktualizujSzybkoscObrotow()
    {
        myRotationSpeed = goSliderSzybkosc.GetComponent<Slider>().value;
    }



    public void CloseApp()
    {
        Debug.Log("exittt");
        Application.Quit();
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Play()
    {
        //playstate = 0;
        Time.timeScale = 1;
        if (playstate == 0) playstate = 1;
        if (koniec == true) Repeat(); 

    }

    public void Repeat()
    {
       // Debug.Log("repeat");
        ZerujUklad();
        playstate = 1;
    }

    public void AktualizujKombinacjeObrotow2()
    {

        //ustalenie osi Obrotu R1
        string[] osie = { "RX", "RY", "RZ" };
        string[] label = { "Rot X", "Rot Y", "Rot Z" };
        int idx = goWyborR1.GetComponent<Dropdown>().value;  //0do2 czyli ktora os jest osia obrotu
        goObrotR1 = lblObrotuR1.transform.Find(osie[idx]).gameObject;
        btnFi.GetComponentInChildren<Text>().text = label[idx];

        //ustalenie osi Obrotu R2
        label = new string[]{ "Rot X '", "Rot Y '", "Rot Z '" };
        idx = goWyborR2.GetComponent<Dropdown>().value;  //0do2 czyli ktora os jest osia obrotu
        goObrotR2 = lblObrotuR2.transform.Find(osie[idx]).gameObject;
        btnTheta.GetComponentInChildren<Text>().text = label[idx];
        //ustalenie osi Obrotu R3
        label = new string[] { "Rot X ''", "Rot Y ''", "Rot Z ''" };
        idx = goWyborR3.GetComponent<Dropdown>().value;  //0do2 czyli ktora os jest osia obrotu
        goObrotR3 = lblObrotuR3.transform.Find(osie[idx]).gameObject;
        btnPsi.GetComponentInChildren<Text>().text = label[idx];

        if (fi >= 0) R1Arc = goObrotR1.transform.Find("imgArc").gameObject;
        else R1Arc = goObrotR1.transform.Find("imgArcMinus").gameObject;
        R1plaszczObrotu = goObrotR1.transform.Find("imgPlaszczObrotu").gameObject;
        R1txtAngle = goObrotR1.transform.Find("txtAngle").gameObject;

        if (theta >= 0) R2Arc = goObrotR2.transform.Find("imgArc").gameObject;
        else R2Arc = goObrotR2.transform.Find("imgArcMinus").gameObject;
        R2plaszczObrotu = goObrotR2.transform.Find("imgPlaszczObrotu").gameObject;
        R2txtAngle = goObrotR2.transform.Find("txtAngle").gameObject;

        if (psi >= 0) R3Arc = goObrotR3.transform.Find("imgArc").gameObject;
        else R3Arc = goObrotR3.transform.Find("imgArcMinus").gameObject;
        R3plaszczObrotu = goObrotR3.transform.Find("imgPlaszczObrotu").gameObject;
        R3txtAngle = goObrotR3.transform.Find("txtAngle").gameObject;

         PokazPlaszczyznyObrotu();

    }

    public void ZerujUklad() {
        btnPause.GetComponent<Image>().enabled=false;
        btnPlay.GetComponent<Image>().enabled=true;
        playstate = 0;
        i = 0;
        Time.timeScale = 1;
        transform.eulerAngles = new Vector3(0, 0, 0);
        goUklObracanycienR1.transform.eulerAngles = new Vector3(0, 0, 0);
        goUklObracanycienR2.transform.eulerAngles = new Vector3(0, 0, 0);
        foreach (GameObject go in imgArcWszystkie)
        {
            go.GetComponent<Image>().fillAmount = 0f;
        }
        foreach (GameObject go in txtAngleWszystkie)
        {
            go.GetComponent<TextMesh>().text = "";
            go.SetActive(false);
        }
        GameObject[][] opisyOsi = { goOpisyOsiR1, goOpisyOsiR2, goOpisyOsiR3 };

        foreach (GameObject[] opisyOsiUkladu in opisyOsi)
        {
            foreach (GameObject go in opisyOsiUkladu)
            {
                go.SetActive(false);
            }
        }

        cOpcje.GetComponent<Canvas>().enabled=false;
        cUstawienia.GetComponent<Canvas>().enabled = false;
        cPomoc.GetComponent<Canvas>().enabled = false;
        PokazPlaszczyznyObrotu();
    }

    public void PokazPlaszczyznyObrotu()
    {
                
        foreach (GameObject go in plaszczyznyObrotuWszystkie)
        {
            go.GetComponent<Image>().enabled=false;
        }

        pokazujPlaszczyznyObrotu = tglPokazPlaszczyzny1.GetComponent<Toggle>().isOn;
        Debug.Log(tglPokazPlaszczyzny1.GetComponent<Toggle>().isOn.ToString());
        R1plaszczObrotu.GetComponent<Image>().enabled = pokazujPlaszczyznyObrotu;
        R2plaszczObrotu.GetComponent<Image>().enabled = pokazujPlaszczyznyObrotu;
        R3plaszczObrotu.GetComponent<Image>().enabled = pokazujPlaszczyznyObrotu;
    }
}