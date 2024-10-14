using System.Collections;
using System.Runtime.CompilerServices;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Assets.Scripts
{
    public class AddForceOnCollisionController : MonoBehaviour
    {
        //El scriptable object en el que se almacene la velocidad a la que va el coche que va a chocar con el objeto. En el caso del Tow truck CarSpeedNormalized
        [SerializeField] FloatVariable carVariableSpeed;
        [SerializeField] float forceMultiplier = 1.0f;

        private float forceMagnitude = 1.0f;
        private Vector2 forceDirection = Vector2.up;
        private Vector2 forceToApply = Vector2.up;
        private Rigidbody2D rb;
      


        private void Start()
        {
            // Obtener el Rigidbody2D del objeto en el que está este script
            rb = GetComponent<Rigidbody2D>();
            
        }

        public void ReceiveCollision(Collision2D collision)
        {
            GameObject detectedGO = collision.gameObject;
            
            if (detectedGO != null)
            {
                

                // Aplicar fuerza al rb del objeto que tiene este script
                if (rb != null)
                {
                   
                    Vector2 pointOfCollision = collision.GetContact(0).point;
                    Vector2 collisionNormal = collision.GetContact(0).normal;
                    forceDirection = collisionNormal;
                    forceMagnitude = carVariableSpeed.Value * forceMultiplier;
                    forceToApply = forceDirection * forceMagnitude;
                                        
                    rb.AddForceAtPosition(forceToApply, pointOfCollision, ForceMode2D.Force);
                }





            }
        }
    }
}