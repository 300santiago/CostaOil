using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GeneralUserEmployer : MonoBehaviour
{

    [SerializeField] TMP_Text nameUserEmployer;
    [SerializeField] TMP_Text positionUserEmployer;
    [SerializeField] TMP_Text emailUserEmployer;
    public UserEmployer thisEmployer = new UserEmployer();

    public void  AssignValueEmployer (UserEmployer _userEmployer)
    {
        thisEmployer = _userEmployer;
        nameUserEmployer.text = _userEmployer.nameEmployer;
        positionUserEmployer.text = _userEmployer.positionEmployer;
        emailUserEmployer.text = $"Email: {_userEmployer.emailEmployer}";

    }
}
