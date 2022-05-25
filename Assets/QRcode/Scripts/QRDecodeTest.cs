using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TBEasyWebCam;
using Proyecto26;
using System.Collections.Generic;

public class QRDecodeTest : MonoBehaviour
{
    public static QRDecodeTest instance;
    public GeneralBody results;
    public QRCodeDecodeController e_qrController;
    public String VINCode;
    public Button[] buttons;
    public GameObject loading;

    public Text UiText;

    public GameObject resetBtn;

    public GameObject scanLineObj;

    public Image torchImage;
    /// <summary>
    /// when you set the var is true,if the result of the decode is web url,it will open with browser.
    /// </summary>
    public bool isOpenBrowserIfUrl;
    private void Awake() 
    {
        instance = this;
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void qrScanFinished(string dataText)
    {
        Debug.Log(dataText);
        VINCode = dataText;
        this.UiText.text = $"VIN code is {dataText}, click on next to continue";
        buttons[0].interactable = true;
        // if (isOpenBrowserIfUrl) {
        // 	if (Utility.CheckIsUrlFormat(dataText))
        // 	{
        // 		if (!dataText.Contains("http://") && !dataText.Contains("https://"))
        // 		{
        // 			dataText = "http://" + dataText;
        // 		}
        // 		Application.OpenURL(dataText);
        // 	}
        // }
        // if (this.resetBtn != null)
        // {
        // 	this.resetBtn.SetActive(true);
        // }
        // if (this.scanLineObj != null)
        // {
        // 	this.scanLineObj.SetActive(false);
        // }

    }
    public void OnNextBtn()
    {
        loading.SetActive(true);
        foreach(Button b in buttons) {b.interactable = false;}
        //VINCode = "1G1AD5F5XAZ184731";
        string url = $"https://vpic.nhtsa.dot.gov/api/vehicles/decodevin/{VINCode}?format=json";
        RestClient.Get<GeneralBody>(url).Then(response =>
        {
            print(response);
            results = response;
            InformationManager.instance.LoadVinInfoScreen();
            loading.SetActive(false);
        }).Catch(error =>
        {
            print("error");
        });
    }
    public void Reset()
    {
        if (this.e_qrController != null)
        {
            this.e_qrController.Reset();
        }

        if (this.UiText != null)
        {
            this.UiText.text = string.Empty;
        }
        if (this.resetBtn != null)
        {
            this.resetBtn.SetActive(false);
        }
        if (this.scanLineObj != null)
        {
            this.scanLineObj.SetActive(true);
        }
    }

    public void Play()
    {
        Reset();
        if (this.e_qrController != null)
        {
            this.e_qrController.StartWork();
        }
    }

    public void Stop()
    {
        if (this.e_qrController != null)
        {
            this.e_qrController.StopWork();
        }

        if (this.resetBtn != null)
        {
            this.resetBtn.SetActive(false);
        }
        if (this.scanLineObj != null)
        {
            this.scanLineObj.SetActive(false);
        }
    }

    public void GotoNextScene(string scenename)
    {
        if (this.e_qrController != null)
        {
            this.e_qrController.StopWork();
        }
        //Application.LoadLevel(scenename);
        SceneManager.LoadScene(scenename);
    }


}
