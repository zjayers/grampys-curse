using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

namespace Game_Assets.Scripts.Character
{
    public class Patrol : MonoBehaviour
    {
        [SerializeField] private GameObject splineGameObject;
        [SerializeField] private float walkDuration = 3f;
        [SerializeField] private float pauseDuration = 2f;
        private NavMeshAgent agentCmp;
        private bool isWalking = true;
        private float lengthWalked;
        private float pauseTime;

        private SplineContainer splineCmp;
        private float splineLength;

        private float splinePosition;
        private float walkTime;

        private void Awake()
        {
            if (splineGameObject == null) Debug.LogWarning($"{name} does not have a spline.");

            splineCmp = splineGameObject.GetComponent<SplineContainer>();
            splineLength = splineCmp.CalculateLength();
            agentCmp = GetComponent<NavMeshAgent>();
        }

        public Vector3 GetNextPosition()
        {
            return splineCmp.EvaluatePosition(splinePosition);
        }

        public void CalculateNextPosition()
        {
            walkTime += Time.deltaTime;

            if (walkTime > walkDuration) isWalking = false;

            if (!isWalking)
            {
                pauseTime += Time.deltaTime;

                if (pauseTime < pauseDuration) return;

                ResetTimers();
            }

            lengthWalked += Time.deltaTime * agentCmp.speed;

            if (lengthWalked > splineLength) lengthWalked = 0f;

            splinePosition = Mathf.Clamp01(lengthWalked / splineLength);
        }

        public void ResetTimers()
        {
            pauseTime = 0f;
            walkTime = 0f;
            isWalking = true;
        }

        public Vector3 GetFartherOutPosition()
        {
            var tempSplinePosition = splinePosition + 0.02f;

            if (tempSplinePosition >= 1)
                // tempSplinePosition = tempSplinePosition - 1;
                tempSplinePosition -= 1;

            return splineCmp.EvaluatePosition(tempSplinePosition);
        }
    }
}