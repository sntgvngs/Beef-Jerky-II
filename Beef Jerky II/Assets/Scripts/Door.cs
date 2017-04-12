using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    private Color closed = new Color(0.2f,0.3f,0.8f,1);
    private Color opened = new Color(0.2f, 0.3f, 0.8f, 0);
    private Material myMaterial;
    private bool open;

    void Awake()
    {
        myMaterial = GetComponent<Renderer>().material;
        myMaterial.color = closed;
        open = false;
    }
    public void Open()
    {
        gameObject.layer = LayerMask.NameToLayer("Not Solid");
        myMaterial.color = opened;
        open = true;
    }

    public void Close()
    {
        gameObject.layer = LayerMask.NameToLayer("Door");
        myMaterial.color = closed;
        open = false;
    }

    public void Toggle()
    {
        if (open)
            Close();
        else
            Open();
    }
}
