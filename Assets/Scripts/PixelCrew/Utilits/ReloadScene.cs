using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.Utilits
{
    public class ReloadScene: MonoBehaviour
    {
        public void Reload()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}