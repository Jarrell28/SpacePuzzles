using System;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour, ISelectable
{
    [SerializeField] private Material defaultMat;
    [SerializeField] private Material hoverColor;
    [SerializeField] private MeshRenderer renderer;

    public UnityEvent onPush;
    private bool buttonPushed;


    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        if(defaultMat) renderer.material = defaultMat;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnHoverEnter()
    {
        if (!buttonPushed)
        {
            if(hoverColor) renderer.material = hoverColor;
        }
    }

    public void OnSelect()
    {
        if(!buttonPushed)
        {
            onPush?.Invoke();
            buttonPushed = true;
        }
        
    }

    public void OnHoverExit()
    {
        if (!buttonPushed)
        {
            if(defaultMat) renderer.material = defaultMat;  
        }
    }
}
