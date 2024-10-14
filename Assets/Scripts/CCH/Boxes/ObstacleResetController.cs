using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Si ha entrado al collider de físicas (parecido a "se produce el evento de colisión")
//Al salir de la zona de reseteo, el objeto respawnea
//Cuando el objeto respawnea, se devanece cada uno de los elementos y se destruyen 

public class ObstacleResetController : MonoBehaviour
{

    [SerializeField] GameObject[] objectsToReset;
    //[SerializeField] bool wantsToDissolve;
    //[SerializeField] float dissolveDuration = 0.2f;
    private bool hasEnteredPhysicsBox = false;
    private Vector2[] initialPositions;



    // Start is called before the first frame update
    void Start()
    {
        initialPositions = new Vector2[objectsToReset.Length];

        for (int i = 0; i < objectsToReset.Length; i++)
        {
            initialPositions[i] = objectsToReset[i].transform.position;
        }
    }

    public void EntersPhysicsBox()
    {
        hasEnteredPhysicsBox = true;
        //Debug.Log("entra a la physics box"); 
    }

    public void StartReset()
    {
        //Debug.Log("entra a startReset");
        if (hasEnteredPhysicsBox)
        {
            /*
            //fade boxes
            if (wantsToDissolve)
            {
                foreach (GameObject obj in objectsToReset)
                {
                    StartCoroutine(FadeOutCoroutine(obj, dissolveDuration));
                }


                IEnumerator FadeOutCoroutine(GameObject obj, float dissolveDuration)
                {
                    SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        Color originalColor = spriteRenderer.color;
                        float elapsedTime = 0f;

                        while (elapsedTime < dissolveDuration)
                        {
                            elapsedTime += Time.deltaTime;
                            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / dissolveDuration);
                            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                            yield return null;
                        }

                        // Ensure the final alpha is set to 0
                        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
                    }
                }
                
            }  
            */


            //quitar fisica de las cajas, teleportarlas al punto de incio y activar física 
            for (int i = 0; i < objectsToReset.Length; i++)
            {
                Rigidbody2D rb2D = objectsToReset[i].GetComponent<Rigidbody2D>();
                if (rb2D != null)
                {
                    // Deactivate Rigidbody2D
                    rb2D.isKinematic = true;
                    // Stop any movement
                    rb2D.velocity = Vector2.zero; 

                    // Reset transform to initial values
                    objectsToReset[i].transform.position = initialPositions[i];

                    // Reactivate Rigidbody2D
                    rb2D.isKinematic = false;
                }
            }

            //Debug.Log("hizo reset");
            hasEnteredPhysicsBox = false;

        }
    }


}
