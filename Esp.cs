using UnityEngine;

using static _7DTDStuff.Main;
using static _7DTDStuff.Utility;
using static _7DTDStuff.Settings;

namespace _7DTDStuff
{
    public class Esp : MonoBehaviour
    {
        public void OnGUI()
        {
            if (Event.current.type != EventType.Repaint) return;

            if (Misc.FOVCircle) DrawCircle(Color.red, MiddleOfScreen, Misc.FOVCirclePixels);
            if (Misc.NearestEnemy)
            {
                float dist = 9999f;
                Vector2 target = Vector2.zero;

                if (AimbotSettings.AimAtPlayers && Players.Count > 0)
                {
                    foreach (EntityPlayer p in Players)
                    {
                        if (p && IsAlive(p))
                        {
                            Vector3 w = Cam.WorldToScreenPoint(p.emodel.GetHeadTransform().position);

                            if (IsInFov(w))
                            {
                                if (IsOnScreen(w))
                                {
                                    float d = Mathf.Abs(Vector2.Distance(new Vector2(w.x, Screen.height - w.y), MiddleOfScreen));

                                    if (d < dist)
                                    {
                                        dist = d;
                                        target = new Vector2(w.x, Screen.height - w.y);
                                    }
                                }
                            }
                        }
                    }
                }
                if (AimbotSettings.AimAtZombies && Zombies.Count > 0)
                {
                    foreach (EntityZombie e in Zombies)
                    {
                        if (e && IsAlive(e))
                        {
                            Vector3 w = Cam.WorldToScreenPoint(e.emodel.GetHeadTransform().position);

                            if (IsInFov(w))
                            {
                                if (IsOnScreen(w))
                                {
                                    float d = Mathf.Abs(Vector2.Distance(new Vector2(w.x, Screen.height - w.y), MiddleOfScreen));

                                    if (d < dist)
                                    {
                                        dist = d;
                                        target = new Vector2(w.x, Screen.height - w.y);
                                    }
                                }
                            }
                        }
                    }
                }
                if (AimbotSettings.AimAtAnimals && Animals.Count > 0)
                {
                    foreach (EntityAnimal a in Animals)
                    {
                        if (a && IsAlive(a))
                        {
                            Vector3 w = Cam.WorldToScreenPoint(a.emodel.GetHeadTransform().position);

                            if (IsInFov(w))
                            {
                                if (IsOnScreen(w))
                                {
                                    float d = Mathf.Abs(Vector2.Distance(new Vector2(w.x, Screen.height - w.y), MiddleOfScreen));

                                    if (d < dist)
                                    {
                                        dist = d;
                                        target = new Vector2(w.x, Screen.height - w.y);
                                    }
                                }
                            }
                        }
                    }
                }

                if (target != Vector2.zero) DrawLine(MiddleOfScreen, new Vector2(target.x, target.y), Misc.FOVCircleColor, 2f);
            }

            if (PlayerSettings.Esp)
            {
                try
                {
                    foreach (EntityPlayer p in Players)
                    {
                        DoEsp(PlayerSettings, p);
                    }
                }
                catch { }
            }
            if (ZombieSettings.Esp)
            {
                try
                {
                    foreach (EntityZombie e in Zombies)
                    {
                        DoEsp(ZombieSettings, e);
                    }
                }
                catch { }
            }
            if (AnimalSettings.Esp)
            {
                try
                {
                    foreach (EntityAnimal a in Animals)
                    {
                        DoEsp(AnimalSettings, a);
                    }
                }
                catch { }
            }
            if (ItemSettings.Esp)
            {
                try
                {
                    foreach (EntityItem i in Items)
                    {
                        Vector3 w = Cam.WorldToScreenPoint(i.transform.position);
                        if (IsOnScreen(w) && IsInDistance(i, ItemSettings.Distance))
                        {
                            GUI.color = ItemSettings.Color;
                            if (ItemSettings.InfoEsp) DrawString(new Vector2(w.x, Screen.height - w.y), $"[{Distance(i)}m] {i.name}", ItemSettings.Color, ItemSettings.InfoTextBold, ItemSettings.InfoTextShadows);
                            if (ItemSettings.TracerEsp) DrawLine(MiddleBottomOfScreen, new Vector2(w.x, Screen.height - w.y), ItemSettings.Color, 2f);
                        }
                    }
                }
                catch { }
            }
            
            if (MiscSettings.PileEsp)
            {
                try
                {
                    foreach (Transform t in MiscStuff)
                    {
                        if (t.name.ToLower().Contains("pile"))
                        {
                            Vector3 w = Cam.WorldToScreenPoint(t.transform.position);
                            if (IsOnScreen(w) && IsInDistance(t, MiscSettings.Distance))
                            {
                                GUI.color = MiscSettings.Color;
                                string name = "??? Pile";
                                #region Get name
                                if (t.name.ToLower().Contains("food")) name = "Food Pile";
                                else if (t.name.ToLower().Contains("medic")) name = "Medical Pile";
                                else if (t.name.ToLower().Contains("ammo")) name = "Ammo Pile";
                                else if (t.name.ToLower().Contains("trash")) name = "Trash Pile";
                                else if (t.name.ToLower().Contains("cloth")) name = "Clothes Pile";
                                #endregion
                                DrawString(new Vector2(w.x, Screen.height - w.y), $"[{Distance(t)}m] {name}", MiscSettings.Color, true, true);
                            }
                        }
                    }
                }
                catch { }
            }
            if (MiscSettings.BackpackEsp)
            {
                try
                {
                    foreach (Transform t in MiscStuff)
                    {
                        if (t.name.ToLower().Contains("backpack"))
                        {
                            Vector3 w = Cam.WorldToScreenPoint(t.transform.position);
                            if (IsOnScreen(w) && IsInDistance(t, MiscSettings.Distance))
                            {
                                GUI.color = MiscSettings.Color;
                                DrawString(new Vector2(w.x, Screen.height - w.y), $"[{Distance(t)}m] Backpack", MiscSettings.Color, true, true);
                            }
                        }
                    }
                }
                catch { }
            }
            if (MiscSettings.ShelfEsp)
            {
                try
                {
                    foreach (Transform t in MiscStuff)
                    {
                        if (t.name.ToLower().Contains("goods"))
                        {
                            Vector3 w = Cam.WorldToScreenPoint(t.transform.position);
                            if (IsOnScreen(w) && IsInDistance(t, MiscSettings.Distance))
                            {
                                GUI.color = MiscSettings.Color;
                                DrawString(new Vector2(w.x, Screen.height - w.y), $"[{Distance(t)}m] Shelf", MiscSettings.Color, true, true);
                            }
                        }
                    }
                }
                catch { }
            }
            if (MiscSettings.CrateEsp)
            {
                try
                {
                    foreach (Transform t in MiscStuff)
                    {
                        if (t.name.ToLower().Contains("crate"))
                        {
                            Vector3 w = Cam.WorldToScreenPoint(t.transform.position);
                            if (IsOnScreen(w) && IsInDistance(t, MiscSettings.Distance))
                            {
                                GUI.color = MiscSettings.Color;
                                DrawString(new Vector2(w.x, Screen.height - w.y), $"[{Distance(t)}m] Crate", MiscSettings.Color, true, true);
                            }
                        }
                    }
                }
                catch { }
            }
            if (MiscSettings.SafeEsp)
            {
                try
                {
                    foreach (Transform t in MiscStuff)
                    {
                        if (t.name.Contains("gun_safe") || t.name.Contains("wall_safe") || t.name.Contains("desk_safe") || t.name.Contains("lab_safe"))
                        {
                            Vector3 w = Cam.WorldToScreenPoint(t.transform.position);
                            if (IsOnScreen(w) && IsInDistance(t, MiscSettings.Distance))
                            {
                                GUI.color = MiscSettings.Color;
                                string name = "";
                                #region Get name
                                if (t.name.Contains("gun_safe")) name = "Gun Safe";
                                else if (t.name.ToLower().Contains("wall_safe")) name = "Wall Safe";
                                else if (t.name.ToLower().Contains("desk_safe")) name = "Desk Safe";
                                else if (t.name.ToLower().Contains("lab_safe")) name = "Lab Safe";
                                #endregion
                                DrawString(new Vector2(w.x, Screen.height - w.y), $"[{Distance(t)}m] {name}", MiscSettings.Color, true, true);
                            }
                        }
                    }
                }
                catch { }
            }
            if (MiscSettings.LootChestEsp)
            {
                try
                {
                    foreach (Transform t in MiscStuff)
                    {
                        if (t.name.Contains("LootChest"))
                        {
                            Vector3 w = Cam.WorldToScreenPoint(t.transform.position);
                            if (IsOnScreen(w) && IsInDistance(t, MiscSettings.Distance))
                            {
                                GUI.color = MiscSettings.Color;
                                DrawString(new Vector2(w.x, Screen.height - w.y), $"[{Distance(t)}m] LootChest", MiscSettings.Color, true, true);
                            }
                        }
                    }
                }
                catch { }
            }
        }
    }
}