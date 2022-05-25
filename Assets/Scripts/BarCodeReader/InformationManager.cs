using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
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
        ReadSeries();
        ReadVehicleType();
        ReadBodyClass();
        ReadDoorsNumber();
        ReadEngineCylinders();
        ReadDisplacementCC();
        ReadEngineModel();
        ReadFuelType();
        ReadValveTrain();
        ReadEngineConfiguration();
        ReadFuelDelivery();
    }
    private void ReadFuelDelivery()
    {
        for(int i = 0; i < QRDecodeTest.instance.results.Results.Count; i++){
            if(QRDecodeTest.instance.results.Results[i].Variable == "Fuel Delivery / Fuel Injection Type"){
                content[14].text = QRDecodeTest.instance.results.Results[i].Value;
                return;
            }   
        }
    }
    private void ReadEngineConfiguration()
    {
        for(int i = 0; i < QRDecodeTest.instance.results.Results.Count; i++){
            if(QRDecodeTest.instance.results.Results[i].Variable == "Engine Configuration"){
                content[13].text = QRDecodeTest.instance.results.Results[i].Value;
                return;
            }
        }
    }
    private void ReadValveTrain()
    {
        for(int i = 0; i < QRDecodeTest.instance.results.Results.Count; i++){
            if(QRDecodeTest.instance.results.Results[i].Variable == "Valve Train Design"){
                content[12].text = QRDecodeTest.instance.results.Results[i].Value;
                return;
            }
        }
    }
    private void ReadFuelType()
    {
        for(int i = 0; i < QRDecodeTest.instance.results.Results.Count; i++){
            if(QRDecodeTest.instance.results.Results[i].Variable == "Fuel Type - Primary"){
                content[11].text = QRDecodeTest.instance.results.Results[i].Value;
                return;
            }
        }
    }
    private void ReadEngineModel()
    {
        for(int i = 0; i < QRDecodeTest.instance.results.Results.Count; i++){
            if(QRDecodeTest.instance.results.Results[i].Variable == "Engine Model"){
                content[10].text = QRDecodeTest.instance.results.Results[i].Value;
                return;
            }
        }
    }
    private void ReadDisplacementCC()
    {
        for(int i = 0; i < QRDecodeTest.instance.results.Results.Count; i++){
            if(QRDecodeTest.instance.results.Results[i].Variable == "Displacement (CC)"){
                content[9].text = QRDecodeTest.instance.results.Results[i].Value;
                return;
            }
        }
    }
    private void ReadEngineCylinders()
    {
        for(int i = 0; i < QRDecodeTest.instance.results.Results.Count; i++){
            if(QRDecodeTest.instance.results.Results[i].Variable == "Engine Number of Cylinders"){
                content[8].text = QRDecodeTest.instance.results.Results[i].Value;
                return;
            }
        }
    }
    private void ReadDoorsNumber()
    {
        for(int i = 0; i < QRDecodeTest.instance.results.Results.Count; i++){
            if(QRDecodeTest.instance.results.Results[i].Variable == "Doors"){
                content[7].text = QRDecodeTest.instance.results.Results[i].Value;
                return;
            }
        }
    }
    private void ReadBodyClass()
    {
        for(int i = 0; i < QRDecodeTest.instance.results.Results.Count; i++){
            if(QRDecodeTest.instance.results.Results[i].Variable == "Body Class"){
                content[6].text = QRDecodeTest.instance.results.Results[i].Value;
                return;
            }
        }
    }
    private void ReadVehicleType()
    {
        for(int i = 0; i < QRDecodeTest.instance.results.Results.Count; i++){
            if(QRDecodeTest.instance.results.Results[i].Variable == "Vehicle Type"){
                content[5].text = QRDecodeTest.instance.results.Results[i].Value;
                return;
            }
        }
    }
    private void ReadSeries()
    {
        for(int i = 0; i < QRDecodeTest.instance.results.Results.Count; i++){
            if(QRDecodeTest.instance.results.Results[i].Variable == "Series"){
                content[4].text = QRDecodeTest.instance.results.Results[i].Value;
                return;
            }
        }
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
    public void ReloadScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
