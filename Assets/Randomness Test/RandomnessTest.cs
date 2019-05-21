using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Randomness_Test
{
    public class RandomnessTest : MonoBehaviour
    {
        [SerializeField] private GameObject pointPrefab;
        [SerializeField] private int numberOfPoints = 100;
        [SerializeField] private float simulationTime = 10f;

        private Vector2 _viewportSize; // in world coordinates

        private void Start()
        {
            var cam = Camera.main;
            if (cam == null) return;
            
            _viewportSize = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            StartCoroutine(Simulate());
        }

        private IEnumerator Simulate()
        {
            var timeBetweenPoints = simulationTime / numberOfPoints;

            for (var i = 0; i < numberOfPoints; i++)
            {
                var pos = new Vector2(
                    (Random.value * 2 * _viewportSize.x) - _viewportSize.x,
                    (Random.value * 2 * _viewportSize.y) - _viewportSize.y);

                Instantiate(pointPrefab, pos, Quaternion.identity, transform);

                if (simulationTime > 0) yield return new WaitForSeconds(timeBetweenPoints);
            }
        }
    }
}
