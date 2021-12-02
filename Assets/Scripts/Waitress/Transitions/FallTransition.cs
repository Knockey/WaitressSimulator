using UnityEngine;

public class FallTransition : Transition
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SlipperySurface surface))
        {
            NeedTransit = true;
        }
    }
}
