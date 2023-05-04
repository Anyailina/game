using UnityEngine;

namespace PixelCrew.ProjectTile
{
    public class ProjectTileRandomPlace : ProjectTile
    {
        [SerializeField] private Transform[] _locationPoints;

        protected override void Start()
        {

       
            base.Start();
            CalculateLocationProjectTile();

        }
        

        private void CalculateLocationProjectTile()
        {
            var randomPosition = Random.Range(0, _locationPoints.Length - 1);
            _position = _locationPoints[randomPosition].position;
        }
    }
}