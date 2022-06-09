using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PanelManagerMainScene : MonoBehaviour
{
    public static PanelManagerMainScene instance;
    public GameObject[] generalPanels;
    public GameObject[] superUserPanels; 
    public GameObject[] adminPanels; 
    public GameObject[] employeePanels; 
    private void Awake() {
        instance = this;
    }
    public void LoadSuperUserPanel()
    {
        foreach(GameObject g in generalPanels) {g.SetActive(false);}
        generalPanels[0].SetActive(true);
    }
    public void LoadAdminPanel()
    {
        foreach(GameObject g in generalPanels) {g.SetActive(false);}
        generalPanels[1].SetActive(true);
    }
    public void LoadEmployeePanel()
    {
        foreach(GameObject g in generalPanels) {g.SetActive(false);}
        generalPanels[1].SetActive(true);
    }
    public void LoadMainPanelSuper()
    {
        foreach(GameObject g in superUserPanels) {g.SetActive(false);}
        superUserPanels[0].SetActive(true);
    }
    public void LoadPanel(GameObject _panel)
    {
        foreach(GameObject g in superUserPanels) {g.SetActive(false);}
        foreach(GameObject g in adminPanels) {g.SetActive(false);}
        foreach(GameObject g in employeePanels) {g.SetActive(false);}
        _panel.SetActive(true);
    }
    public void LoadPanelIndex(int _index, int userType)
    {
        if(userType == 0)
        {
            foreach(GameObject g in superUserPanels) {g.SetActive(false);}
            superUserPanels[_index].SetActive(true);
        }
        else if (userType == 1)
        {
            foreach(GameObject g in adminPanels) {g.SetActive(false);}
            adminPanels[_index].SetActive(true);
        }
        else if (userType == 2)
        {
            foreach(GameObject g in employeePanels) {g.SetActive(false);}
            employeePanels[_index].SetActive(true);
        }
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
