using UnityEngine;

public class PaintMapParticles : MonoBehaviour
{
    private ParticleSystem painterParticles;
    private bool canPaint = false;

    void Start()
    {
        painterParticles = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        PaintMechanic();
    }

    void PaintMechanic()
    {
        if (Input.GetMouseButton(0))
        {
            if (!canPaint)
            {
                painterParticles.Play();
                canPaint = true;
            }
        }
        else
        {
            painterParticles.Stop();
            canPaint = false;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            transform.position = hit.point;
        }
    }
}