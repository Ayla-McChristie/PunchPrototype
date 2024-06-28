using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBuffer : MonoBehaviour
{
    Queue<IInputBufferAction> buffer;

    public void UpdateInputBuffer()
    {

        foreach (IInputBufferAction c in buffer)
        {
            c.ResolveAction();
        }
    }
}
