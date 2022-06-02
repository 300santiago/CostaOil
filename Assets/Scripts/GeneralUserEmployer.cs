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
    public UserEmployee thisEmployer = new UserEmployee();

    public void  AssignValueEmployer (UserEmployee _userEmployer)
    {
        thisEmployer = _userEmployer;
        nameUserEmployer.text = _userEmployer.nameEmployee;
        positionUserEmployer.text = _userEmployer.positionEmployee;
        emailUserEmployer.text = $"Email: {_userEmployer.emailEmployee}";

    }
}
