using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class HomeEmployer : MonoBehaviour
{
    
    [Header("GameObjects panels")]
    [Header("--------------------------------------------------------------------------------------------------------")]
    [SerializeField] GameObject [] panelsEmployer;

    [Header("Titles in scene")]
    [SerializeField] TMP_Text principalTitle;
    [Header("Rect Transforms")]
    [SerializeField] RectTransform contentCars;
    [Header("InputFields in scene")]
    [SerializeField] TMP_InputField nameUser;
    [SerializeField] TMP_InputField ageUser;
    [SerializeField] TMP_InputField cellPhoneUser;
    [SerializeField] TMP_InputField numberLicenseDrive;
    [SerializeField] TMP_InputField chasisNumber;
    [SerializeField] TMP_InputField brandVehicle;
    [SerializeField] TMP_InputField modelVehicle;
    [SerializeField] TMP_InputField numberChasis;
    [SerializeField] TMP_InputField descriptionJob;
    [Header("Classes to use in scene")]
    CarJobsClass thiscarJobsClass = new CarJobsClass();


    private void Awake()
    {
        for (int i=0; i<panelsEmployer.Length; i++)
        {
            panelsEmployer[i].SetActive(false);
        }    
    }
    private void Start()
    {
        principalTitle.text = $"Welcome Employer {DataHolder.userEmployer.nameEmployer}";
        panelsEmployer[0].SetActive(true);
    }

    public void DeleteDates()
    {
        nameUser.text = string.Empty;
        ageUser.text = string.Empty;
        cellPhoneUser.text = string.Empty;
        numberLicenseDrive.text = string.Empty;
        chasisNumber.text = string.Empty;
    }

    public void AsignDatesUser()
    {
        thiscarJobsClass.nameUser = nameUser.text;
        thiscarJobsClass.ageUser = ageUser.text;
        thiscarJobsClass.cellPhoneUser = cellPhoneUser.text;
        thiscarJobsClass.numberLicense = numberLicenseDrive.text;

    }

    public void AsignDatesCar()
    {
        thiscarJobsClass.brandVehicle = brandVehicle.text;
        thiscarJobsClass.modelVehicle = modelVehicle.text;
        thiscarJobsClass.numberChasis = numberChasis.text;
        thiscarJobsClass.descriptionJob = descriptionJob.text;
    }
    
    public void SendDatesCar()
    {
        DataHolder.userEmployer.listCars.Add(thiscarJobsClass);
        DataHolder.instance.WriteNakamaEmployerUser(AuthenticationHandler.instance.email);
    }
}
