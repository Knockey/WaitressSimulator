using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SlipperySurface : MonoBehaviour
{
    [SerializeField] private ParticleSystem _poofEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WaitressMovement waitress))
            DestroySurface();
    }

    private void DestroySurface()
    {
        Instantiate(_poofEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
