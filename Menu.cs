using UnityEngine;

using static _7DTDStuff.Main;
using static _7DTDStuff.Utility;
using static _7DTDStuff.Settings;

namespace _7DTDStuff
{
    public class Menu : MonoBehaviour
    {
        void OnGUI()
        {
            GUI.skin = MenuSkin;

            if (MenuOpen)
            {
                MenuRect = GUI.Window(WindowId, MenuRect, SwitchWindow, "");
            }
        }
        void SwitchWindow(int id)
        {
            if (GUI.Button(new Rect(10, 10, 79, 25), "Visuals")) WindowId = 0;
            if (GUI.Button(new Rect(94, 10, 79, 25), "Aimbot")) WindowId = 1;
            if (GUI.Button(new Rect(178, 10, 79, 25), "Misc")) WindowId = 2;
            if (GUI.Button(new Rect(261, 10, 78, 25), "Players")) WindowId = 3;

            switch (id)
            {
                case 0: // Visuals
                    #region Buttons
                    GUILayout.Space(0);
                    GUILayout.BeginArea(new Rect(10, 45, 162, 245), style: "box", text: "Selection");

                    if (GUILayout.Button("Player Esp"))
                    {
                        SelectionId = 0;
                        SelectionName = "Player Esp";
                    }
                    if (GUILayout.Button("Zombie Esp"))
                    {
                        SelectionId = 1;
                        SelectionName = "Zombie Esp";
                    }
                    if (GUILayout.Button("Animal Esp"))
                    {
                        SelectionId = 2;
                        SelectionName = "Animal Esp";
                    }
                    if (GUILayout.Button("Item Esp"))
                    {
                        SelectionId = 3;
                        SelectionName = "Item Esp";
                    }
                    if (GUILayout.Button("Misc Esp"))
                    {
                        SelectionId = 4;
                        SelectionName = "Misc Esp";
                    }

                    GUILayout.EndArea();
                    #endregion
                    #region Options
                    GUILayout.Space(0);
                    GUILayout.BeginArea(new Rect(177, 45, 163, 245), style: "box", text: SelectionName);

                    Template ChosenTemplate = null;

                    if (SelectionId == 123)
                    {
                        GUILayout.EndArea();
                        break;
                    }
                    else if (SelectionId == 0) ChosenTemplate = PlayerSettings;
                    else if (SelectionId == 1) ChosenTemplate = ZombieSettings;
                    else if (SelectionId == 2) ChosenTemplate = AnimalSettings;
                    else if (SelectionId == 3) ChosenTemplate = ItemSettings;
                    else if (SelectionId == 4)
                    {
                        MiscSettings.EnableSearching = GUILayout.Toggle(MiscSettings.EnableSearching, "Enable Searching");
                        MiscSettings.PileEsp = GUILayout.Toggle(MiscSettings.PileEsp, "Pile Esp");
                        MiscSettings.BackpackEsp = GUILayout.Toggle(MiscSettings.BackpackEsp, "Backpack Esp");
                        MiscSettings.SafeEsp = GUILayout.Toggle(MiscSettings.SafeEsp, "Safe Esp");
                        MiscSettings.ShelfEsp = GUILayout.Toggle(MiscSettings.ShelfEsp, "Goods Esp");
                        MiscSettings.CrateEsp = GUILayout.Toggle(MiscSettings.CrateEsp, "Crate Esp");
                        MiscSettings.LootChestEsp = GUILayout.Toggle(MiscSettings.LootChestEsp, "LootChest Esp");

                        GUILayout.Label($"Show Distance: {MiscSettings.Distance}");
                        MiscSettings.Distance = GUILayout.HorizontalSlider(float.Parse(MiscSettings.Distance.ToString("F1")), 1, 500);

                        GUILayout.EndArea();
                        break;
                    }

                    ChosenTemplate.Esp = GUILayout.Toggle(ChosenTemplate.Esp, SelectionName);
                    ChosenTemplate.InfoEsp = GUILayout.Toggle(ChosenTemplate.InfoEsp, "Info Esp");
                    #region Info Esp Settings
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(15);
                    ChosenTemplate.InfoTextBold = GUILayout.Toggle(ChosenTemplate.InfoTextBold, "Text Bold");
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(15);
                    ChosenTemplate.InfoTextShadows = GUILayout.Toggle(ChosenTemplate.InfoTextShadows, "Text Shadows");
                    GUILayout.EndHorizontal();
                    #endregion
                    ChosenTemplate.TracerEsp = GUILayout.Toggle(ChosenTemplate.TracerEsp, "Tracer Esp");
                    ChosenTemplate.ChamEsp = GUILayout.Toggle(ChosenTemplate.ChamEsp, "Cham Esp");
                    if (ChosenTemplate != ItemSettings)
                    {
                        ChosenTemplate.SkeletonEsp = GUILayout.Toggle(ChosenTemplate.SkeletonEsp, "Skeleton Esp");
                        ChosenTemplate.Box3DEsp = GUILayout.Toggle(ChosenTemplate.Box3DEsp, "3D Box Esp");
                    }

                    GUILayout.Label($"Show Distance: {ChosenTemplate.Distance}");
                    ChosenTemplate.Distance = GUILayout.HorizontalSlider(float.Parse(ChosenTemplate.Distance.ToString("F1")), 1, 500);

                    GUILayout.EndArea();
                    #endregion
                    break;
                case 1: // Aimbot
                    #region Aimbot
                    GUILayout.Space(0);
                    GUILayout.BeginArea(new Rect(10, 45, 162, 245), style: "box", text: "Aimbot");

                    AimbotSettings.Aimbot = GUILayout.Toggle(AimbotSettings.Aimbot, "Aimbot");
                    AimbotSettings.MagicBullet = GUILayout.Toggle(AimbotSettings.MagicBullet, "Magic Bullet");
                    AimbotSettings.VisCheck = GUILayout.Toggle(AimbotSettings.VisCheck, "Visual Check");

                    GUILayout.EndArea();
                    #endregion
                    #region Aimbot Settings
                    GUILayout.Space(0);
                    GUILayout.BeginArea(new Rect(177, 45, 163, 245), style: "box", text: "Settings");

                    GUILayout.Label($"FOV Circle Pixels: {Misc.FOVCirclePixels}");
                    Misc.FOVCirclePixels = GUILayout.HorizontalSlider(Misc.FOVCirclePixels, 1f, 300f);

                    GUILayout.Label($"Smooth: {AimbotSettings.Smooth}");
                    AimbotSettings.Smooth = GUILayout.HorizontalSlider(AimbotSettings.Smooth, 1f, 20f);

                    Misc.FOVCircle = GUILayout.Toggle(Misc.FOVCircle, "FOV Circle");
                    Misc.NearestEnemy = GUILayout.Toggle(Misc.NearestEnemy, "Show Nearest Enemy");
                    AimbotSettings.AimAtPlayers = GUILayout.Toggle(AimbotSettings.AimAtPlayers, "Aim At Players");
                    AimbotSettings.AimAtZombies = GUILayout.Toggle(AimbotSettings.AimAtZombies, "Aim At Zombies");
                    AimbotSettings.AimAtAnimals = GUILayout.Toggle(AimbotSettings.AimAtAnimals, "Aim At Animals");

                    GUILayout.EndArea();
                    #endregion
                    break;
                    case 2: // Misc
                        if (LP == null || LP.inventory == null) break;
                        #region Current Weapon
                        GUILayout.Space(0);
                        GUILayout.BeginArea(new Rect(10, 45, 162, 245), style: "box", text: "Weapon");

                        Misc.InfAmmo = GUILayout.Toggle(Misc.InfAmmo, "Inf Ammo");
                        if (LP.inventory.GetHoldingGun() != null) LP.inventory.GetHoldingGun().InfiniteAmmo = Misc.InfAmmo;

                        Misc.NoWeaponBob = GUILayout.Toggle(Misc.NoWeaponBob, "No Weapon Bob");
                        if (Misc.NoWeaponBob)
                        {
                            LP.vp_FPWeapon.BobRate = Vector4.zero;
                            LP.vp_FPWeapon.ShakeAmplitude = Vector3.zero;
                            LP.vp_FPWeapon.StepForceScale = 0f;
                        }
                        GUILayout.EndArea();
                        #endregion
                        #region Nothing
                        // No weapon bob
                        // Infinite Ammo
                        // Double stack
                        // Refill durability
                        #endregion
                        break;
                    case 3: // Players
                        if (Players.Count == 0 || LP == null || GM == null || GM.World == null) break;
                        #region Character Selection
                        GUILayout.Space(0);
                        GUILayout.BeginArea(new Rect(10, 45, 162, 245), style: "box", text: "Characters");

                        if (GUILayout.Button(LP.EntityName))
                        {
                            CharacterSelectionId = 0;
                            CharacterSelectionName = LP.EntityName;
                        }
                        for (int i = 0; i < Players.Count; i++)
                        {
                            if (GUILayout.Button(Players[i].EntityName))
                            {
                                CharacterSelectionId = i + 1;
                                CharacterSelectionName = Players[i].EntityName;
                            }
                        }

                        GUILayout.EndArea();
                        #endregion
                        #region Character Stats
                        GUILayout.Space(0);
                        GUILayout.BeginArea(new Rect(177, 45, 163, 245), style: "box", text: CharacterSelectionName);

                        EntityPlayer ChosenPlayer = null;

                        if (CharacterSelectionId == 123)
                        {
                            GUILayout.EndArea();
                            break;
                        }
                        else if (CharacterSelectionId == 0) ChosenPlayer = LP;
                        else ChosenPlayer = Players[CharacterSelectionId];

                        GUILayout.Label($"Name: {ChosenPlayer.EntityName}");
                        GUILayout.Label($"Health: {ChosenPlayer.Stats.Health.Value}/{ChosenPlayer.Stats.Health.Max}");
                        GUILayout.Label($"Stamina: {ChosenPlayer.Stats.Stamina.Value}/{ChosenPlayer.Stats.Stamina.Max}");
                        GUILayout.Label($"Food: {ChosenPlayer.Stats.Food.Value}/{ChosenPlayer.Stats.Food.Max}");
                        GUILayout.Label($"Water: {ChosenPlayer.Stats.Water.Value}/{ChosenPlayer.Stats.Water.Max}");
                        GUILayout.Label($"Godmode? {ChosenPlayer.IsGodMode}");
                        GUILayout.Label($"Flying? {ChosenPlayer.IsFlyMode}");

                        GUILayout.Space(5);
                        if (GUILayout.Button("Godmode")) ChosenPlayer.IsGodMode.Value = !ChosenPlayer.IsGodMode.Value;
                        if (GUILayout.Button("Fly")) ChosenPlayer.IsFlyMode.Value = !ChosenPlayer.IsFlyMode.Value;
                        GUILayout.EndArea();
                        #endregion
                        break;
            }

            GUI.DragWindow();
        }
    }
}