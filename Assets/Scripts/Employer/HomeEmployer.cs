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
    [SerializeField] GameObject panelHomeEmployer;

    [Header("Titles in scene")]
    [SerializeField] TMP_Text principalTitle;
    [Header("Rect Transforms")]
    [SerializeField] RectTransform contentCars;

    private void Start()
    {
        principalTitle.text = $"Welcome Employer {DataHolder.userEmployer.nameEmployer}";    
    }
}
