using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventTranslatorFSM : MonoBehaviour
{
    public GameObject parentObject;
    public FiniteStateMachine fsm;

    public void InvokeEventOnFSM(EventObject eventToInvoke)
    {
        fsm.UpdateState(eventToInvoke, parentObject);
    }
}
