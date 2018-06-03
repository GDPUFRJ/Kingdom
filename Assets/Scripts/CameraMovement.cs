using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour {

    public float MovementSpeed = 0.5f;
    public float ZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.
    public float MaxDist = 5;
    public float MinDist = 1;

    private float rayLength = 200;
    public LayerMask layerMask;


    void Update()
    {
        // se tiver apenas um dedo, interpreta como movimentacao
        if (Input.touchCount == 1 && WorldMapController.Selecionado)
        {
            //Armazena o toque
            Touch touchZero = Input.GetTouch(0);

            // Descobre a posicao do toque no frame anterior
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;

            //calcula o vetor distancia entre o ponto anterior e o ponto atual
            Vector3 touchDeltaMag =  touchZeroPrevPos - touchZero.position;

            transform.position += touchDeltaMag * MovementSpeed * (Camera.main.orthographicSize / 10);
        }

        // Se tiver dois toques, interpreta como movimento de zoom
        else if (Input.touchCount == 2 && WorldMapController.Selecionado)
        {
            // Armazena os dois toques
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Descobre a posicao dos toques no frame anterior
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // cacula a distancia entre os vetores
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // calcula a diferenca entre as distancias do frame atual e do frame anterior
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // Aplica somente se a camera estiver configurada como orthografica
            if (Camera.main.orthographic)
            {
                // Varia o orthografic size de acordo com a velocidade definida no editor
                Camera.main.orthographicSize += deltaMagnitudeDiff * ZoomSpeed;

                // Garante os valores minimos e maximos
                Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, MinDist);
                Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize, MaxDist);
            }
        }
    }
}
