using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InformationManager : MonoBehaviour
{
    public static InformationManager instance;
    public TMP_Text[] content;
    [Header("Screen")]
    public GameObject[] screens;
    private void Awake() {
        instance = this;
    }
    public void FillInformation()
    {
        ReadBrand();
        ReadManufacturer();
        ReadModel();
        ReadModelYear();
    }
    private void ReadModelYear()
    {
        for(int i = 0; i < QRDecodeTest.instance.results.Results.Count; i++){
            if(QRDecodeTest.instance.results.Results[i].Variable == "Model Year"){
                content[3].text = QRDecodeTest.instance.results.Results[i].Value;
                return;
            }
        }
    }
    private void ReadModel()
    {
        for(int i = 0; i < QRDecodeTest.instance.results.Results.Count; i++){
            if(QRDecodeTest.instance.results.Results[i].Variable == "Model"){
                content[2].text = QRDecodeTest.instance.results.Results[i].Value;
                return;
            }
        }
    }
    private void ReadManufacturer(){
        for(int i = 0; i < QRDecodeTest.instance.results.Results.Count; i++){
            if(QRDecodeTest.instance.results.Results[i].Variable == "Manufacturer Name"){
                content[1].text = QRDecodeTest.instance.results.Results[i].Value;
                return;
            }
        }
    }
    private void ReadBrand(){
        for(int i = 0; i < QRDecodeTest.instance.results.Results.Count; i++){
            if(QRDecodeTest.instance.results.Results[i].Variable == "Make"){
                content[0].text = QRDecodeTest.instance.results.Results[i].Value;
                return;
            }
        }
    }
    public void LoadVinInfoScreen(){
        foreach(GameObject g in screens) {g.SetActive(false);}
        screens[1].SetActive(true);
        FillInformation();
    }
}
