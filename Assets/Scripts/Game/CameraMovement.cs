using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CameraMovement : MonoBehaviour {

    public float MovementSpeed = 0.5f;
    public float ZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.
    public float zoomAnimationTax = 0.6f;
    public float MaxDist = 5;
    public float MinDist = 1;

    private bool canControl = true;
    private float timeToMove = 0.5f;

    private Camera cam;
    private Vector2 camExtents = new Vector2(), boundsExtents = new Vector2();
    [SerializeField] private BoxCollider worldCollider;

    private void Start()
    {
        cam = GetComponent<Camera>();
        
        boundsExtents.y = worldCollider.bounds.extents.y;
        boundsExtents.x = worldCollider.bounds.extents.x;
    }

    void Update()
    {
        HandleTouches();
        ClampPosition();
    }

    private void ClampPosition()
    {
        camExtents.y = cam.orthographicSize;
        camExtents.x = cam.aspect * camExtents.y;

        if (transform.position.x + camExtents.x > worldCollider.transform.position.x + boundsExtents.x)
        {
            transform.position = new Vector3(worldCollider.transform.position.x + boundsExtents.x - camExtents.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x - camExtents.x < worldCollider.transform.position.x - boundsExtents.x)
        {
            transform.position = new Vector3(worldCollider.transform.position.x - boundsExtents.x + camExtents.x, transform.position.y, transform.position.z);
        }
        if (transform.position.y + camExtents.y > worldCollider.transform.position.y + boundsExtents.y)
        {
            transform.position = new Vector3(transform.position.x, worldCollider.transform.position.y + boundsExtents.y - camExtents.y, transform.position.z);
        }
        if (transform.position.y - camExtents.y < worldCollider.transform.position.y - boundsExtents.y)
        {
            transform.position = new Vector3(transform.position.x, worldCollider.transform.position.y - boundsExtents.y + camExtents.y, transform.position.z);
        }
    }

    private void HandleTouches()
    {
        if (!canControl)
            return;
        // se tiver apenas um dedo, interpreta como movimentacao
        if (Input.touchCount == 1 && WorldMapController.Instance.Selecionado)
        {
            //Armazena o toque
            Touch touchZero = Input.GetTouch(0);

            // Descobre a posicao do toque no frame anterior
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;

            //calcula o vetor distancia entre o ponto anterior e o ponto atual
            Vector3 touchDeltaMag = touchZeroPrevPos - touchZero.position;

            transform.position += touchDeltaMag * MovementSpeed * (Camera.main.orthographicSize / 10);
        }

        // Se tiver dois toques, interpreta como movimento de zoom
        else if (Input.touchCount == 2 && WorldMapController.Instance.Selecionado)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canControl = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        canControl = true;
    }

    public IEnumerator FollowPosition(Vector2 position)
    {
        canControl = false;
        transform.DOMove(new Vector3(position.x, position.y, transform.position.z), timeToMove);
        yield return new WaitForSeconds(timeToMove);
        canControl = true;
    }
    public IEnumerator Zoom(bool toggle)
    {
        FMODPlayer.Instance.Play("whoosh");
        canControl = false;
        float tax = toggle ? zoomAnimationTax : 1f / zoomAnimationTax;
        Camera.main.DOOrthoSize(Camera.main.orthographicSize*tax, timeToMove);
        yield return new WaitForSeconds(timeToMove);
        canControl = true;
    }
}
