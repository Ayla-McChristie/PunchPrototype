using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable
{
    public bool IsGrabbed { get; }
    public virtual void OnGrab() { }
    public virtual void OnRelease() { }

}
