using UnityEngine;

public class PopTransition : Transition
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Shelf shells))
        {
            NeedTransit = true;
        }
    }
}
