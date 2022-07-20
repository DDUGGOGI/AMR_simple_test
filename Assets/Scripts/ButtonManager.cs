using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [Header("Static Button")]
    public Button ApplyButton;
    public Button CancelButton;

    [Header("Option Button")]
    public Button OptionButton;
    public bool isOptionPanelShow=false;
    public GameObject OptionPanelBack;
    public GameObject OptionPanel;

    [Header("Control Type Button")]
    public Button ControlType;
    public Button Touch;
    public Text controlTypeText;
    bool isTouch=true;
    public KeyboardControl KeyboardControl;
    public NewJoystickControl newJoystickControl;
    public GameObject joystick;


    [Header("Point Of View Button")]
    public Button viewTypeButton;
    public Text viewTypeText;
    int ViewTypeNum=2;
    public GameObject birdViewCamera;
    public GameObject thirdViewCamera;
    public GameObject firstViewCamera;


    public void ClickOptionButton()     //�ɼ� ��ư Ŭ��
    {
        isOptionPanelShow = !isOptionPanelShow;

        if (isOptionPanelShow==true)
        {
            isShowOptionPanel(true);
        }
        else if (isOptionPanelShow==false)
        {
            isShowOptionPanel(false);
            
        }
    }
    public void ClickOptionClose()      //�ɼ�â �ݱ�
    {
        isShowOptionPanel(false);
        isOptionPanelShow = false;
    }
    public void isShowOptionPanel(bool isShow)      //�ɼ�â Ȱ��ȭ
    {
        OptionPanel.transform.gameObject.SetActive(isShow);
        OptionPanelBack.transform.gameObject.SetActive(isShow);
    }

    public void ClickControlType(bool isClick)      //��Ʈ�� Ÿ�� Ŭ��
    {
        isTouch = !isTouch;

        if (isTouch==true)
        {
            joystick.SetActive(true);
            controlTypeText.GetComponent<Text>().text = "Touch";
            KeyboardControl.GetComponent<KeyboardControl>().enabled = false;
            newJoystickControl.GetComponent<NewJoystickControl>().enabled = true;
        }
        else if (isTouch==false)
        {
            joystick.SetActive(false);
            controlTypeText.GetComponent<Text>().text = "Keyboard";
            KeyboardControl.GetComponent<KeyboardControl>().enabled = true;
            newJoystickControl.GetComponent<NewJoystickControl>().enabled = false;
        }
    }

    public void ClickViewTypeButton()       //��Ʈ�� ���� ��ȭ Ŭ��
    {
        ViewTypeNum += 1;

        if (ViewTypeNum%3 == 0)
        {
            viewTypeText.GetComponent<Text>().text = "First Person View";
            birdViewCamera.SetActive(false);
            thirdViewCamera.SetActive(false);
            firstViewCamera.SetActive(true);
        }
        else if (ViewTypeNum%3==1)
        {
            viewTypeText.GetComponent<Text>().text = "Third Person View";
            birdViewCamera.SetActive(false);
            thirdViewCamera.SetActive(true);
            firstViewCamera.SetActive(false);
        }
        else if (ViewTypeNum%3==2)
        {
            viewTypeText.GetComponent<Text>().text = "Bird View";
            birdViewCamera.SetActive(true);
            thirdViewCamera.SetActive(false);
            firstViewCamera.SetActive(false);
        }
    }
}
