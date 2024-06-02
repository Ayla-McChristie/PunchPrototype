using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBuffer : MonoBehaviour
{
    Queue<InputBufferItem> buffer;

    public void UpdateInputBuffer()
    {

        foreach (InputBufferItem c in buffer)
        {
            c.ResolveAction();
        }
    }
}
