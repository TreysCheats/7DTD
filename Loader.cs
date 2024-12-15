using UnityEngine;

namespace _7DTDStuff
{
    public class Loader : MonoBehaviour
    {
        private static GameObject LoadObj = new GameObject();
        public static void Load()
        {
            LoadObj.AddComponent<Main>();
            LoadObj.AddComponent<Menu>();
            LoadObj.AddComponent<Esp>();
            DontDestroyOnLoad(LoadObj);
        }
        public static void Unload()
        {
            Destroy(LoadObj);
        }
    }
}