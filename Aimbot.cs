using System.Runtime.InteropServices;
using UnityEngine;

using static _7DTDStuff.Main;
using static _7DTDStuff.Settings;
using static _7DTDStuff.Utility;

namespace _7DTDStuff
{
    public class Aimbot : MonoBehaviour
    {
        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public static void AimbotHax()
        {
            float dist = 9999f;
            Vector2 target = Vector2.zero;

            if (AimbotSettings.AimAtPlayers)
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
            if (AimbotSettings.AimAtZombies)
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
            if (AimbotSettings.AimAtAnimals)
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

            if (target != Vector2.zero)
            {
                double distX = target.x - Screen.width / 2f;
                double distY = target.y - Screen.height / 2f;

                distX /= AimbotSettings.Smooth; // Smooth
                distY /= AimbotSettings.Smooth; // Smooth

                mouse_event(0x0001, (int)distX, (int)distY, 0, 0);
            }
        }
        public static void MagicBulletHax()
        {
            float dist = 9999f;
            EntityPlayer PlayerTarget = null;
            EntityZombie ZombieTarget = null;
            EntityAnimal AnimalTarget = null;

            if (AimbotSettings.AimAtPlayers)
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
                                    PlayerTarget = p;
                                }
                            }
                        }
                    }
                }
            }
            dist = 9999f;
            if (AimbotSettings.AimAtZombies)
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
                                    ZombieTarget = e;
                                }
                            }
                        }
                    }
                }
            }
            dist = 9999f;
            if (AimbotSettings.AimAtAnimals)
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
                                    AnimalTarget = a;
                                }
                            }
                        }
                    }
                }
            }

            DamageSource ds = new DamageSource(EnumDamageSource.External, EnumDamageTypes.Concuss);
            if (PlayerTarget)
            {
                PlayerTarget.DamageEntity(ds, 100, false, 1f);
                PlayerTarget.AwardKill(LP);
            }
            if (ZombieTarget)
            {
                ds.CreatorEntityId = LP.entityId;

                ZombieTarget.DamageEntity(ds, 100, false, 1f);
                ZombieTarget.AwardKill(LP);

                LP.AddKillXP(ZombieTarget);
            }
            if (AnimalTarget)
            {
                ds.CreatorEntityId = LP.entityId;

                AnimalTarget.DamageEntity(ds, 100, false, 1f);
                AnimalTarget.AwardKill(LP);

                LP.AddKillXP(AnimalTarget);
            }
        }
    }
}