using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class ConsolaScaler : MonoBehaviour
{
    public Vector2 escala = new Vector2(1, 1);
    Vector2 lastEscala;

    public GameObject body;

    public float lado;
    float lastScaleX;
    float lastScaleY;

    public GameObject parent;

    public GameObject ladoIzquierdo;
    public GameObject ladoDerecho;
    public GameObject ladoSuperior;
    public GameObject ladoInferior;
    public GameObject esquinaSuperiorDerecha;
    public GameObject esquinaSuperiorIzquierda;
    public GameObject esquinaInferiorDerecha;
    public GameObject esquinaInferiorIzquierda;

    public TextMeshPro text;

    void Update()
    {
        if(lastEscala != escala){
            body.transform.localScale = new Vector3(escala.x*100,100,escala.y*100);
            lastEscala = escala;
        }

        float extraX = (body.transform.localScale.x - 100) / 100;
        float extraY = (body.transform.localScale.z - 100) / 100;

        if (lastScaleX != extraX || lastScaleY != extraY)
        {
            if (lastScaleX != extraX)
            {
                body.transform.localPosition = Vector3.zero;
                ladoDerecho.transform.localPosition = body.transform.localPosition + new Vector3(extraX, 0, 0);
                ladoIzquierdo.transform.localPosition = body.transform.localPosition + -new Vector3(extraX, 0, 0);

                ladoSuperior.transform.localScale = new Vector3(100, 100, extraX * 100 + 100);
                ladoInferior.transform.localScale = new Vector3(100, 100, extraX * 100 + 100);

                esquinaInferiorDerecha.transform.localPosition = body.transform.localPosition + new Vector3(extraX, esquinaInferiorDerecha.transform.localPosition.y, 0);
                esquinaInferiorIzquierda.transform.localPosition = body.transform.localPosition + new Vector3(-extraX, esquinaInferiorIzquierda.transform.localPosition.y, 0);
                esquinaSuperiorDerecha.transform.localPosition = body.transform.localPosition + new Vector3(extraX, esquinaSuperiorDerecha.transform.localPosition.y, 0);
                esquinaSuperiorIzquierda.transform.localPosition = body.transform.localPosition + new Vector3(-extraX, esquinaSuperiorIzquierda.transform.localPosition.y, 0);
            }
            if (lastScaleY != extraY)
            {

                ladoSuperior.transform.localPosition = body.transform.localPosition + new Vector3(0, extraY, 0);
                ladoInferior.transform.localPosition = body.transform.localPosition + -new Vector3(0, extraY, 0);

                ladoDerecho.transform.localScale = new Vector3(100, 100, extraY * 100 + 100);
                ladoIzquierdo.transform.localScale = new Vector3(100, 100, extraY * 100 + 100);

                esquinaInferiorDerecha.transform.localPosition = body.transform.localPosition + new Vector3(esquinaInferiorDerecha.transform.localPosition.x, -extraY, 0);
                esquinaInferiorIzquierda.transform.localPosition = body.transform.localPosition + new Vector3(esquinaInferiorIzquierda.transform.localPosition.x, -extraY, 0);
                esquinaSuperiorDerecha.transform.localPosition = body.transform.localPosition + new Vector3(esquinaSuperiorDerecha.transform.localPosition.x, extraY, 0);
                esquinaSuperiorIzquierda.transform.localPosition = body.transform.localPosition + new Vector3(esquinaSuperiorIzquierda.transform.localPosition.x, extraY, 0);
            }

            body.transform.localPosition = Vector3.zero;
            text.rectTransform.sizeDelta = new Vector2(lado * (body.transform.localScale.x/100), lado * (body.transform.localScale.z/100));
            text.rectTransform.localPosition = new Vector3(parent.transform.localPosition.x,parent.transform.localPosition.y,parent.transform.localPosition.z-0.1f);
            escala = new Vector2(body.transform.localScale.x/100,body.transform.localScale.z/100);
            lastEscala = escala;

            lastScaleX = extraX;
            lastScaleY = extraY;
        }

    }
}
