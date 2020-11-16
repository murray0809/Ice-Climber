using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NameCtr : MonoBehaviour
{
    [SerializeField] Text m_text;
    [SerializeField] InputField m_inputField;
    private string m_name;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        m_inputField = m_inputField.GetComponent<InputField>();
        m_text = m_text.GetComponent<Text>();
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
        m_text.text = m_inputField.text;
        m_name = m_text.text;
        Debug.Log(m_name);
        Debug.Log("入力終わり");
        m_inputField.text = "";
    }
}
