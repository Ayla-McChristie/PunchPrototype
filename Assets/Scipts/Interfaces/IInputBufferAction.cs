using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputBufferAction
{
    public bool ActionIsCancellable{ get; }
    public IInputBufferAction NextTapAction { get; }
    public IInputBufferAction NextHoldAction { get; }
    public void ResolveAction();

}
