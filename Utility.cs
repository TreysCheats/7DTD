using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using static _7DTDStuff.Main;
using static _7DTDStuff.Settings;

namespace _7DTDStuff
{
    public class Utility : MonoBehaviour
    {
        public static Material ChamsMat, BoxMat;
        public static int ColorID;

        public static void SetStuff()
        {
            ColorID = Shader.PropertyToID("_Color");
            #region Chams Mat
            ChamsMat = new Material(Shader.Find("Hidden/Internal-Colored"))
            {
                hideFlags = (HideFlags)61
            };
            ChamsMat.SetInt("_SrcBlend", 5);
            ChamsMat.SetInt("_DstBlend", 10);
            ChamsMat.SetInt("_Cull", 0);
            ChamsMat.SetInt("_ZWrite", 0);
            ChamsMat.SetInt("_ZTest", 8); // 8 = See Through Walls
            #endregion
            #region Box Mat
            BoxMat = new Material(Shader.Find("Hidden/Internal-Colored"))
            {
                hideFlags = (HideFlags)61
            };
            BoxMat.SetInt("_SrcBlend", 5);
            BoxMat.SetInt("_DstBlend", 10);
            BoxMat.SetInt("_Cull", 0);
            BoxMat.SetInt("_ZWrite", 0);
            #endregion
            #region Menu Assets
            AssetBundle Bundle = AssetBundle.LoadFromMemory(File.ReadAllBytes("C:\\Users\\Trey\\Downloads\\EgguWare\\Assets\\EgguWareV1.assets"));
            MenuSkin = Bundle.LoadAllAssets<GUISkin>().First();
            #endregion
        }

        #region Main Functions
        public static bool IsAlive(Entity e)
        {
            return e.IsAlive();
        }
        public static string Distance(Entity e)
        {
            return Vector3.Distance(Cam.transform.position, e.transform.position).ToString("F1");
        }
        public static string Distance(Transform t)
        {
            return Vector3.Distance(Cam.transform.position, t.transform.position).ToString("F1");
        }
        public static bool IsOnScreen(Vector3 w)
        {
            return w.y > 0.01f && w.y < Screen.height - 5f && w.z > 0.01f;
        }
        public static bool IsInFov(Vector3 w)
        {
            return Vector2.Distance(MiddleOfScreen, new Vector2(w.x, w.y)) < Misc.FOVCirclePixels;
        }
        public static bool IsInDistance(Entity e, float MaxDist)
        {
            return Vector3.Distance(Cam.transform.position, e.transform.position) <= MaxDist;
        }
        public static bool IsInDistance(Transform t, float MaxDist)
        {
            return Vector3.Distance(Cam.transform.position, t.transform.position) <= MaxDist;
        }
        #endregion
        #region Esp Functions
        public static void DoEsp(Template t, EntityAlive e)
        {
            Vector3 w = Cam.WorldToScreenPoint(e.transform.position);
            if (IsOnScreen(w) && IsAlive(e) && IsInDistance(e, t.Distance))
            {
                GUI.color = t.Color;

                if (t.InfoEsp) DrawString(new Vector2(w.x, Screen.height - w.y), $"[{Distance(e)}m] {e.EntityName.Replace("zombie", "Zombie ")}", t.Color, t.InfoTextBold, t.InfoTextShadows);
                if (t.TracerEsp) DrawLine(MiddleBottomOfScreen, new Vector2(w.x, Screen.height - w.y), t.Color, 2f);
                if (t.Box3DEsp) Draw3DBox(e.GetComponent<CapsuleCollider>().bounds, t.Color);
                if (t.SkeletonEsp) DrawAllBones(GetBones(e.emodel.avatarController.GetAnimator()), t.Color);
            }
        }
        public static List<Transform> GetBones(Animator anim)
        {
            List<Transform> Bones = new List<Transform>();

            Bones.Add(anim.GetBoneTransform(HumanBodyBones.Head)); // 0
            Bones.Add(anim.GetBoneTransform(HumanBodyBones.Chest)); // 1
            Bones.Add(anim.GetBoneTransform(HumanBodyBones.Hips)); // 2

            Bones.Add(anim.GetBoneTransform(HumanBodyBones.RightUpperLeg)); // 3
            Bones.Add(anim.GetBoneTransform(HumanBodyBones.RightLowerLeg)); // 4
            Bones.Add(anim.GetBoneTransform(HumanBodyBones.RightFoot)); // 5

            Bones.Add(anim.GetBoneTransform(HumanBodyBones.LeftUpperLeg)); // 6
            Bones.Add(anim.GetBoneTransform(HumanBodyBones.LeftLowerLeg)); // 7
            Bones.Add(anim.GetBoneTransform(HumanBodyBones.LeftFoot)); // 8

            Bones.Add(anim.GetBoneTransform(HumanBodyBones.RightShoulder)); // 9
            Bones.Add(anim.GetBoneTransform(HumanBodyBones.RightUpperArm)); // 10
            Bones.Add(anim.GetBoneTransform(HumanBodyBones.RightLowerArm)); // 11
            Bones.Add(anim.GetBoneTransform(HumanBodyBones.RightHand)); // 12

            Bones.Add(anim.GetBoneTransform(HumanBodyBones.LeftShoulder)); // 13
            Bones.Add(anim.GetBoneTransform(HumanBodyBones.LeftUpperArm)); // 14
            Bones.Add(anim.GetBoneTransform(HumanBodyBones.LeftLowerArm)); // 15
            Bones.Add(anim.GetBoneTransform(HumanBodyBones.LeftHand)); // 16

            return Bones;
        }
        public static void DrawBones(Transform b1, Transform b2, Color Col)
        {
            Vector3 w1 = Main.Cam.WorldToScreenPoint(b1.position);
            Vector3 w2 = Main.Cam.WorldToScreenPoint(b2.position);
            DrawLine(new Vector2(w1.x, Screen.height - w1.y), new Vector2(w2.x, Screen.height - w2.y), Col, 2f);
        }
        public static void DrawAllBones(List<Transform> Bones, Color Col)
        {
            DrawBones(Bones[0], Bones[1], Col); // Head to Chest
            DrawBones(Bones[1], Bones[2], Col); // Chest to Hips

            DrawBones(Bones[2], Bones[3], Col); // Hips to RUL
            DrawBones(Bones[3], Bones[4], Col); // RUL to RLL
            DrawBones(Bones[4], Bones[5], Col); // RLL TO RF

            DrawBones(Bones[2], Bones[6], Col); // Hips to LUL
            DrawBones(Bones[6], Bones[7], Col); // LUL to LLL
            DrawBones(Bones[7], Bones[8], Col); // LLL to LF

            DrawBones(Bones[9], Bones[13], Col); // RS to LS

            DrawBones(Bones[9], Bones[10], Col); // RS to RUA
            DrawBones(Bones[10], Bones[11], Col); // RUA to RLA
            DrawBones(Bones[11], Bones[12], Col); // RLA to RH

            DrawBones(Bones[13], Bones[14], Col); // LS to LUA
            DrawBones(Bones[14], Bones[15], Col); // LUA to LLA
            DrawBones(Bones[15], Bones[16], Col); // LLA to LH
        }
        public static void ApplyChams(Entity e, Color c)
        {
            foreach (Renderer r in e.GetComponentsInChildren<Renderer>())
            {
                if (!r.name.Contains("DaddyTrey"))
                {
                    Renderer copy = Instantiate(r);
                    copy.material = ChamsMat;
                    #region Set Color
                    if (Items.Contains(e)) copy.material.SetColor(ColorID, ItemSettings.Color);
                    switch (e.entityType)
                    {
                        case EntityType.Player:
                            copy.material.SetColor(ColorID, PlayerSettings.Color);
                            break;
                        case EntityType.Zombie:
                            copy.material.SetColor(ColorID, ZombieSettings.Color);
                            break;
                        case EntityType.Animal:
                            copy.material.SetColor(ColorID, AnimalSettings.Color);
                            break;
                    }
                    #endregion

                    r.name = $"DaddyTrey|{r.name}";
                    r.enabled = false;
                }
            }
        }
        #endregion
        #region Drawing Functions
        public static void DrawLine(Vector2 start, Vector2 end, Color color, float width)
        {
            Vector2 d = end - start;
            float a = Mathf.Rad2Deg * Mathf.Atan(d.y / d.x);
            if (d.x < 0)
                a += 180f;

            int w = (int)Mathf.Ceil(width / 2);

            GUI.color = color;
            GUIUtility.RotateAroundPivot(a, start);
            GUI.DrawTexture(new Rect(start.x, start.y - w, d.magnitude, width), Texture2D.whiteTexture, ScaleMode.StretchToFill);
            GUIUtility.RotateAroundPivot(-a, start);
        }
        public static void Draw3DBox(Bounds b, Color color)
        {
            Vector3[] pts = new Vector3[8];
            pts[0] = Cam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z));
            pts[1] = Cam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z));
            pts[2] = Cam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z));
            pts[3] = Cam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z));
            pts[4] = Cam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z));
            pts[5] = Cam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z));
            pts[6] = Cam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z));
            pts[7] = Cam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z));

            for (int i = 0; i < pts.Length; i++) pts[i].y = Screen.height - pts[i].y;

            GL.PushMatrix();
            GL.Begin(1);
            BoxMat.SetPass(0);
            GL.End();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Begin(1);
            BoxMat.SetPass(0);
            GL.Color(color);
            // Top
            GL.Vertex3(pts[0].x, pts[0].y, 0f);
            GL.Vertex3(pts[1].x, pts[1].y, 0f);
            GL.Vertex3(pts[1].x, pts[1].y, 0f);
            GL.Vertex3(pts[5].x, pts[5].y, 0f);
            GL.Vertex3(pts[5].x, pts[5].y, 0f);
            GL.Vertex3(pts[4].x, pts[4].y, 0f);
            GL.Vertex3(pts[4].x, pts[4].y, 0f);
            GL.Vertex3(pts[0].x, pts[0].y, 0f);

            // Bottom
            GL.Vertex3(pts[2].x, pts[2].y, 0f);
            GL.Vertex3(pts[3].x, pts[3].y, 0f);
            GL.Vertex3(pts[3].x, pts[3].y, 0f);
            GL.Vertex3(pts[7].x, pts[7].y, 0f);
            GL.Vertex3(pts[7].x, pts[7].y, 0f);
            GL.Vertex3(pts[6].x, pts[6].y, 0f);
            GL.Vertex3(pts[6].x, pts[6].y, 0f);
            GL.Vertex3(pts[2].x, pts[2].y, 0f);

            // Sides
            GL.Vertex3(pts[2].x, pts[2].y, 0f);
            GL.Vertex3(pts[0].x, pts[0].y, 0f);
            GL.Vertex3(pts[3].x, pts[3].y, 0f);
            GL.Vertex3(pts[1].x, pts[1].y, 0f);
            GL.Vertex3(pts[7].x, pts[7].y, 0f);
            GL.Vertex3(pts[5].x, pts[5].y, 0f);
            GL.Vertex3(pts[6].x, pts[6].y, 0f);
            GL.Vertex3(pts[4].x, pts[4].y, 0f);

            GL.End();
            GL.PopMatrix();

        }
        public static void DrawCircle(Color Col, Vector2 Center, float Radius)
        {
            GL.PushMatrix();

            if (!BoxMat.SetPass(0))
            {
                GL.PopMatrix();
                return;
            }

            GL.Begin(1);
            GL.Color(Col);

            for (float num = 0f; num < 6.28318548f; num += 0.05f)
            {
                GL.Vertex(new Vector3(Mathf.Cos(num) * Radius + Center.x, Mathf.Sin(num) * Radius + Center.y));
                GL.Vertex(new Vector3(Mathf.Cos(num + 0.05f) * Radius + Center.x, Mathf.Sin(num + 0.05f) * Radius + Center.y));
            }

            GL.End();
            GL.PopMatrix();
        }
        public static void DrawString(Vector2 pos, string str, Color c, bool bold, bool shadows)
        {
            GUIContent content = new GUIContent(str);

            GUIStyle style = new GUIStyle();
            style.fontSize = 14;
            style.richText = true;
            style.normal.textColor = c;
            if (bold) style.fontStyle = FontStyle.Bold;
            else style.fontStyle = FontStyle.Normal;

            GUIStyle styleOutline = new GUIStyle();
            styleOutline.fontSize = 14;
            styleOutline.richText = true;
            styleOutline.normal.textColor = new Color(0f, 0f, 0f, 1f);
            if (bold) styleOutline.fontStyle = FontStyle.Bold;
            else style.fontStyle = FontStyle.Normal;

            pos.x -= style.CalcSize(content).x / 2;

            if (!shadows)
            {
                GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), content, style);
            }
            if (shadows)
            {
                GUI.Label(new Rect(pos.x + 1f, pos.y + 1f, 300f, 25f), content, styleOutline);
                GUI.Label(new Rect(pos.x - 1f, pos.y - 1f, 300f, 25f), content, styleOutline);
                GUI.Label(new Rect(pos.x, pos.y - 1f, 300f, 25f), content, styleOutline);
                GUI.Label(new Rect(pos.x, pos.y + 1f, 300f, 25f), content, styleOutline);
                GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), content, style);
            }
        }
        #endregion
    }
}