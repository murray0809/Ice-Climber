using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NCMB;

public class NameCtr : MonoBehaviour
{
    [SerializeField] InputField m_inputField;
    private string m_name;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        m_inputField = m_inputField.GetComponent<InputField>();
        //SceneManager.sceneUnloaded += SceneUnloaded;
    }

    private void Update()
    {
        //Debug.Log(m_name);
    }
    public void InputText()
    {
        Debug.Log(m_inputField.text);
        //m_text.text = m_inputField.text;
        //string name = m_inputField.text;
    }

    public void EndText()
    {
        
        m_name = NCMBUser.CurrentUser.UserName;
        Debug.Log(m_name);
        Debug.Log("入力終わり");
        m_inputField.text = "";
    }
}
