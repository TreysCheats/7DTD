using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static _7DTDStuff.Settings;
using static _7DTDStuff.Utility;

namespace _7DTDStuff
{
    public class Main : MonoBehaviour
    {
        #region Vars
        public static EntityPlayerLocal LP;
        public static GameManager GM;
        public static Camera Cam;

        public static List<EntityPlayer> Players = new List<EntityPlayer>();
        public static List<EntityZombie> Zombies = new List<EntityZombie>();
        public static List<EntityAnimal> Animals = new List<EntityAnimal>();
        public static List<EntityItem> Items;
        public static List<EntityBackpack> Backpacks;
        public static List<Transform> MiscStuff = new List<Transform>();

        IEnumerator MainUpdate, ChamUpdate;
        #endregion

        void Start()
        {
            SetStuff();

            MainUpdate = MainUpdateFunc(0f);
            StartCoroutine(MainUpdate);

            ChamUpdate = ChamUpdateFunc(0f);
            StartCoroutine(ChamUpdate);
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Delete)) Loader.Unload();
            if (Input.GetKeyDown(KeyCode.Insert)) MenuOpen = !MenuOpen;

            if (Input.GetKey(KeyCode.LeftAlt) && Zombies.Count > 0)
            {
                if (AimbotSettings.Aimbot)
                {
                    if (AimbotSettings.MagicBullet)
                    {
                        Aimbot.MagicBulletHax();
                    }
                    else
                    {
                        Aimbot.AimbotHax();
                    }
                }
            }

            if (Input.GetKeyDown(PanicKey))
            {
                #region Menu
                MenuOpen = false;
                #endregion
                #region Visuals
                PlayerSettings.Esp = false;
                ZombieSettings.Esp = false;
                AnimalSettings.Esp = false;
                ItemSettings.Esp = false;

                MiscSettings.EnableSearching = false;
                MiscSettings.PileEsp = false;
                MiscSettings.BackpackEsp = false;
                MiscSettings.ShelfEsp = false;
                MiscSettings.CrateEsp = false;
                MiscSettings.SafeEsp = false;
                MiscSettings.LootChestEsp = false;
                #endregion
                #region Aimbot
                AimbotSettings.Aimbot = false;

                Misc.FOVCircle = false;
                Misc.NearestEnemy = false;
                #endregion
            }
        }

        IEnumerator MainUpdateFunc(float time)
        {
            yield return new WaitForSeconds(time);

            try
            {
                if (!Cam) Cam = Camera.main;
                if (!GM) GM = FindObjectOfType<GameManager>();

                LP = FindObjectOfType<EntityPlayerLocal>();

                Players = GameManager.Instance.World.GetPlayers().ToList();
                Players.Remove(LP);
                Zombies = FindObjectsOfType<EntityZombie>().ToList();
                Animals = FindObjectsOfType<EntityAnimal>().ToList();
                Items = FindObjectsOfType<EntityItem>().ToList();
                Backpacks = FindObjectsOfType<EntityBackpack>().ToList();

                MiscStuff.Clear();
                if (MiscSettings.EnableSearching)
                {
                    foreach (Transform t in FindObjectsOfType<Transform>())
                    {
                        if (t.name.ToLower().Contains("pile") || t.name.Contains("backpack") || t.name.Contains("Goods") || t.name.Contains("gun_safe") || t.name.Contains("wall_safe") || t.name.Contains("desk_safe") || t.name.Contains("lab_safe") || t.name.Contains("LootChest") || t.name.ToLower().Contains("crate"))
                        {
                            MiscStuff.Add(t);
                        }
                    }
                }
            }
            catch { }

            MainUpdate = MainUpdateFunc(3f);
            StartCoroutine(MainUpdate);
        }
        IEnumerator ChamUpdateFunc(float time)
        {
            yield return new WaitForSeconds(time);

            try
            {
                if (PlayerSettings.ChamEsp)
                {
                    foreach (EntityPlayer p in Players)
                    {
                        ApplyChams(p, PlayerSettings.Color);
                    }
                }
                if (ZombieSettings.ChamEsp)
                {
                    foreach (EntityZombie e in Zombies)
                    {
                        ApplyChams(e, ZombieSettings.Color);
                    }
                }
                if (AnimalSettings.ChamEsp)
                {
                    foreach (EntityAnimal a in Animals)
                    {
                        ApplyChams(a, AnimalSettings.Color);
                    }
                }
                if (ItemSettings.ChamEsp)
                {
                    foreach (EntityItem i in Items)
                    {
                        ApplyChams(i, ItemSettings.Color);
                    }
                }
            }
            catch { }

            ChamUpdate = ChamUpdateFunc(6f);
            StartCoroutine(ChamUpdate);
        }
    }
}