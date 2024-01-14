
using UnityEngine;

namespace MonsterRun
{
    public class BoundaryData
    {
        private Vector2 boundaryMarkerPosition;
        private float minimumDistanceToBoundary;

        public Vector2 BoundaryMarkerPosition => boundaryMarkerPosition;
        public float MinimumDistanceToBoundary => minimumDistanceToBoundary;

        public BoundaryData(Vector2 boundaryMarkerPosition, float minimumDistanceToBoundary)
        {
            this.boundaryMarkerPosition = boundaryMarkerPosition;
            this.minimumDistanceToBoundary = minimumDistanceToBoundary; 
        }
    }
}