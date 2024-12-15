using UnityEngine;

namespace _7DTDStuff
{
    public class Settings
    {
        public static Vector2 MiddleOfScreen = new Vector2(Screen.width / 2, Screen.height / 2);
        public static Vector2 MiddleBottomOfScreen = new Vector2(Screen.width / 2, Screen.height);
        public static KeyCode PanicKey = KeyCode.F2;

        #region Menu
        public static bool MenuOpen = true;
        public static int WindowId = 0;
        public static Rect MenuRect = new Rect(0, 0, 350, 310);
        public static GUISkin MenuSkin = null;

        public static int SelectionId = 123;
        public static string SelectionName = "";

        public static int CharacterSelectionId = 123;
        public static string CharacterSelectionName = "";
        #endregion
        #region Visuals
        public class Template
        {
            public bool Esp = false;
            public float Distance = 150f;

            public bool InfoEsp = false;
            public bool TracerEsp = false;
            public bool Box3DEsp = false;
            public bool SkeletonEsp = false;
            public bool ChamEsp = false;

            public bool InfoTextBold = true;
            public bool InfoTextShadows = true;

            public Color Color = Color.clear;
        }
        public static Template PlayerSettings = new Template()
        {
            Color = Color.cyan,
        };
        public static Template ZombieSettings = new Template()
        {
            Color = Color.red,
        };
        public static Template AnimalSettings = new Template()
        {
            Color = Color.green,
        };
        public static Template ItemSettings = new Template()
        {
            Color = Color.yellow,
        };
        public class MiscSettings
        {
            public static float Distance = 50f;

            public static bool EnableSearching = false;

            public static bool PileEsp = false;
            public static bool BackpackEsp = false;
            public static bool ShelfEsp = false;
            public static bool CrateEsp = false;
            public static bool SafeEsp = false;
            public static bool LootChestEsp = false;

            public static Color Color = Color.magenta;
        }
        #endregion
        #region Aimbot
        public class AimbotSettings
        {
            public static bool Aimbot = false;
            public static bool MagicBullet = false;
            public static bool VisCheck = false;
            public static float Smooth = 5f;

            public static bool AimAtPlayers = false;
            public static bool AimAtZombies = false;
            public static bool AimAtAnimals = false;
        }
        #endregion
        #region Misc
        public class Misc
        {
            public static bool FOVCircle = false;
            public static float FOVCirclePixels = 150f;
            public static Color FOVCircleColor = Color.red;

            public static bool NearestEnemy = false;

            public static bool InfAmmo = false;
            public static bool NoWeaponBob = false;
        }
        #endregion
    }
}