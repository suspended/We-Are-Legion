using System;

using Microsoft.Xna.Framework.Input;

using FragSharpHelper;
using FragSharpFramework;

namespace Game
{
    public partial class World : SimShader
    {
        public void EditorUpdate()
        {
            if (MapEditor && Keys.OemTilde.Pressed()) Editor_ToggleMapEditor();
            if (MapEditor && Keys.P.Pressed()) Editor_ToggleMapEditor();

            if (!MapEditorActive) return;

            //if (Keys.P.Pressed()) Editor_TogglePause();
            if (Keys.D0.Pressed()) Editor_SwitchPlayer(0);
            if (Keys.D1.Pressed()) Editor_SwitchPlayer(1);
            if (Keys.D2.Pressed()) Editor_SwitchPlayer(2);
            if (Keys.D3.Pressed()) Editor_SwitchPlayer(3);
            if (Keys.D4.Pressed()) Editor_SwitchPlayer(4);
        }

        void Editor_SwitchPlayer(int player)
        {
            MyPlayerValue = Fint(player);
            MyTeamValue = Fint(player);
        }

        void Editor_TogglePause()
        {
            SimulationPaused = !SimulationPaused;
        }

        void Editor_ToggleGridLines()
        {
            DrawGridLines = !DrawGridLines;
        }

        void Editor_ToggleMapEditor()
        {
            MapEditorActive = !MapEditorActive;

            if (MapEditorActive && MyPlayerNumber == 0)
            {
                MyPlayerNumber = 1;
            }
        }

        public static float StaticMaxZoomOut = 1;
        float x_edge;
        public void Update()
        {
            EditorUpdate();

            float FpsRateModifier = 1;

            //const float MaxZoomOut = 5.33333f, MaxZoomIn = 200f;
            //const float MaxZoomOut = 3.2f, MaxZoomIn = 200f;
            //const float MaxZoomOut = 1f, MaxZoomIn = 200f; // Full zoom-in/out

            float MaxZoomOut, MaxZoomIn;
            if (MapEditorActive)
            {
                // Full zoom-in/out
                MaxZoomOut = 1f;
                MaxZoomIn = 200f;
            }
            else
            {
                MaxZoomOut = World.StaticMaxZoomOut;
                MaxZoomIn = 200f; // Full zoom-in, Partial zoom-out
            }
            MaxZoomOut = 1f;
            // Zoom all the way out
            if (Keys.Space.Down())
                CameraZoom = MaxZoomOut;

            // Zoom in/out, into the location of the cursor
            var world_mouse_pos = ScreenToWorldCoord(Input.CurMousePos);
            var hold_camvec = camvec;

            if (GameClass.MouseEnabled)
            {
                float MouseWheelZoomRate = 1.3333f * FpsRateModifier;
                if (Input.DeltaMouseScroll < 0) CameraZoom /= MouseWheelZoomRate;
                else if (Input.DeltaMouseScroll > 0) CameraZoom *= MouseWheelZoomRate;
            }

            float KeyZoomRate = 1.125f * FpsRateModifier;
            if (Buttons.X.Down() || Keys.X.Down() || Keys.E.Down())      CameraZoom /= KeyZoomRate;
            else if (Buttons.A.Down() || Keys.Z.Down() || Keys.Q.Down()) CameraZoom *= KeyZoomRate;

            if (CameraZoom < MaxZoomOut) CameraZoom = MaxZoomOut;
            if (CameraZoom > MaxZoomIn) CameraZoom = MaxZoomIn;

            if (GameClass.MouseEnabled && !(Buttons.A.Pressed() || Buttons.X.Pressed()))
            {
                var shifted = GetShiftedCamera(Input.CurMousePos, camvec, world_mouse_pos);
                CameraPos = shifted;
            }

            // Move the camera via: Click And Drag
            //float MoveRate_ClickAndDrag = .00165f * FpsRateModifier;
            //if (Input.LeftMouseDown)
            //    CameraPos += Input.DeltaMousPos / CameraZoom * MoveRate_ClickAndDrag * new vec2(-1, 1);

            // Move the camera via: Push Edge
            if (GameClass.MouseEnabled && (!Program.DisableScreenEdge || BoxSelecting))
            {
                float MoveRate_PushEdge = .075f * FpsRateModifier;
                var push_dir = vec2.Zero;
                float EdgeRatio = .005f;
                push_dir.x += -CoreMath.Restrict(0, 1, (EdgeRatio * GameClass.Screen.x - Input.CurMousePos.x) / (EdgeRatio * GameClass.Screen.x));
                push_dir.x += CoreMath.Restrict(0, 1, (Input.CurMousePos.x - (1 - EdgeRatio) * GameClass.Screen.x) / (EdgeRatio * GameClass.Screen.x));
                push_dir.y -= -CoreMath.Restrict(0, 1, (EdgeRatio * GameClass.Screen.y - Input.CurMousePos.y) / (EdgeRatio * GameClass.Screen.y));
                push_dir.y -= CoreMath.Restrict(0, 1, (Input.CurMousePos.y - (1 - EdgeRatio) * GameClass.Screen.y) / (EdgeRatio * GameClass.Screen.y));

                CameraPos += push_dir / CameraZoom * MoveRate_PushEdge;
            }

            // Move the camera via: Keyboard or Gamepad
            var dir = Input.Direction();

            float MoveRate_Keyboard = .07f * FpsRateModifier;
            CameraPos += dir / CameraZoom * MoveRate_Keyboard;

            // Move the camera via: Minimap
            if (Input.LeftMouseDown && !BoxSelecting && MouseOverMinimap)
            {
                CameraPos = (UiMousePos - MinimapQuad.pos) / MinimapQuad.size;
            }


            // Make sure the camera doesn't go too far offscreen
            x_edge = Math.Max(.5f * (CameraAspect / CameraZoom) + .5f * (CameraAspect / MaxZoomOut), 1);
            var TR = ScreenToWorldCoord(new vec2(GameClass.Screen.x, 0));
            if (TR.x > x_edge) CameraPos = new vec2(CameraPos.x - (TR.x - x_edge), CameraPos.y);
            if (TR.y > 1) CameraPos = new vec2(CameraPos.x, CameraPos.y - (TR.y - 1));
            var BL = ScreenToWorldCoord(new vec2(0, GameClass.Screen.y));
            if (BL.x < -x_edge) CameraPos = new vec2(CameraPos.x - (BL.x + x_edge), CameraPos.y);
            if (BL.y < -1) CameraPos = new vec2(CameraPos.x, CameraPos.y - (BL.y + 1));


            // Switch input modes

            // Switch to spells (must be playing, not in editor)
            if (!SimulationPaused)
            {
                if (Keys.D1.Pressed() || Keys.D2.Pressed() || Keys.D3.Pressed() || Keys.D4.Pressed())
                {
                    if (Keys.D1.Pressed()) CurSpell = Spells.Fireball;
                    if (Keys.D2.Pressed()) CurSpell = Spells.SkeletonArmy;
                    if (Keys.D3.Pressed()) CurSpell = Spells.Necromancer;
                    if (Keys.D4.Pressed()) CurSpell = Spells.TerracottaArmy;

                    CurUserMode = UserMode.CastSpell;
                    UnselectAll = false;
                }
            }

            // Switch to building placement
            if (Keys.B.Down())
            {
                CurUserMode = UserMode.PlaceBuilding;
                UnselectAll = true; 
                BuildingUserIsPlacing = UnitType.Barracks;
            }

            if (Keys.G.Down())
            {
                CurUserMode = UserMode.PlaceBuilding;
                UnselectAll = true;
                BuildingUserIsPlacing = UnitType.GoldMine;
            }

            if (Keys.J.Down())
            {
                CurUserMode = UserMode.PlaceBuilding;
                UnselectAll = true;
                BuildingUserIsPlacing = UnitType.JadeMine;
            }

            // Switch to standard select
            if (Keys.Escape.Down() || Keys.Back.Down() || Input.RightMousePressed)
            {
                if (CurUserMode == UserMode.PlaceBuilding)
                    SkipNextSelectionUpdate = true;

                CurUserMode = UserMode.Select;
                SkipDeselect = true;
            }

            // Switch to unit placement (editor only)
            if (MapEditorActive)
            {
                if (Keys.R.Down()) { TileUserIsPlacing = TileType.None; UnitUserIsPlacing = UnitType.Footman; CurUserMode = UserMode.Painting; UnselectAll = true; }
                if (Keys.T.Down()) { TileUserIsPlacing = TileType.None; UnitUserIsPlacing = UnitType.DragonLord; CurUserMode = UserMode.Painting; UnselectAll = true; }
                if (Keys.Y.Down()) { TileUserIsPlacing = TileType.None; UnitUserIsPlacing = UnitType.Necromancer; CurUserMode = UserMode.Painting; UnselectAll = true; }
                if (Keys.U.Down()) { TileUserIsPlacing = TileType.None; UnitUserIsPlacing = UnitType.Skeleton; CurUserMode = UserMode.Painting; UnselectAll = true; }
                if (Keys.I.Down()) { TileUserIsPlacing = TileType.None; UnitUserIsPlacing = UnitType.ClaySoldier; CurUserMode = UserMode.Painting; UnselectAll = true; }

                if (Keys.C.Down()) { UnitUserIsPlacing = UnitType.None; TileUserIsPlacing = TileType.Dirt; CurUserMode = UserMode.Painting; UnselectAll = true; }
                if (Keys.V.Down()) { UnitUserIsPlacing = UnitType.None; TileUserIsPlacing = TileType.Grass; CurUserMode = UserMode.Painting; UnselectAll = true; }
                if (Keys.N.Down()) { UnitUserIsPlacing = UnitType.None; TileUserIsPlacing = TileType.Trees; CurUserMode = UserMode.Painting; UnselectAll = true; }

                if (Keys.Tab.Pressed())
                {
                    UnitPlaceStyle++;
                    if (UnitPlaceStyle >= UnitDistribution.Last) UnitPlaceStyle = UnitDistribution.First;
                }
            }
        }

        public int
            /// The simulation step the GPU sim is currently on.
            SimStep = 0,
            /// The simulation step the server is on or is nearly on.
            ServerSimStep = 0,
            /// The simulatin step that any acknowledged actions should occur on.
            AckSimStep = 0,
            /// The minimum simulation step of any client/server.
            MinClientSimStep = 0;

        /// <summary>
        /// After the simulation updates there are additional updates that must occur.
        /// These are broken into phases. This variable trackes which phase we are currently in.
        /// </summary>
        int PostUpdateStep;

        /// <summary>
        /// After the simulation updates there are additional updates that must occur.
        /// This flag is true once these additional updates have finished.
        /// </summary>
        bool PostUpdateFinished;

        void SimulationUpdate()
        {
            SimStep++;
            PostUpdateFinished = false;
            PostUpdateStep = 0;

            PostSimulationUpdate();
        }

        void FullUpdate()
        {
            SimStep++;
            PostUpdateFinished = false;
            PostUpdateStep = 0;

            while (!PostUpdateFinished)
                PostSimulationUpdate();
        }

        void PostSimulationUpdate()
        {
            switch (PostUpdateStep)
            {
                case 0:
                    DataGroup.UpdateSelect();

                    if (MapEditorActive)
                        DataGroup.EditorSimulationUpdate();
                    else
                        DataGroup.SimulationUpdate();

                    break;

                case 1:
                    DataGroup.UpdateIcons();
                    DataGroup.DoDragonLordCount(PlayerInfo); // This should happen soon after CurrentUnit.anim is updated, so it can catch the death switch with low latency.
                    DragonLordDeathCheck();

                    break;

                case 2:
                    if (SimStep % 2 == 0)
                        DataGroup.DoGoldMineCount(PlayerInfo);
                    else
                        DataGroup.DoJadeMineCount(PlayerInfo);

                    DoGoldUpdate();
                    DoJadeUpdate();

                    break;

                case 3:
                    UpdateAllPlayerUnitCounts();
                    break;

                case 4:
                    DataGroup.UpdateGradients();
                    break;

                case 5:
                    DataGroup.UpdateMagicFields();
                    DataGroup.UpdateMagicAuras();
                    break;

                case 6:
                    DataGroup.UpdateRnd();
                    DataGroup.UpdateMagicAuras(); // 2nd auro update
                    break;

                case 7:
                    UpdateMinimap();
                    break;

                default:
                    //HashCheck();
                    PostUpdateFinished = true;
                    break;
            }

            PostUpdateStep++;
        }

        private void HashCheck()
        {
            if (SimStep % Program.LogPeriod == 0 && (Program.LogShortHash || Program.LogLongHash))
            {
                string curdata_hash = DataGroup.DoHash(DataGroup.CurrentData, DataHash.Apply);
                string prevdata_hash = DataGroup.DoHash(DataGroup.PreviousData, DataHash.Apply);
                string curunit_hash = DataGroup.DoHash(DataGroup.CurrentUnits);
                string prevunit_hash = DataGroup.DoHash(DataGroup.PreviousUnits);
                string target_hash = DataGroup.DoHash(DataGroup.TargetData);
                string extra_hash = DataGroup.DoHash(DataGroup.Extra);

                if (Program.LogLongHash)
                {
                    Console.WriteLine("Hash = {0} {1} {2} {3} {4} {5}", curdata_hash, prevdata_hash, curunit_hash, prevunit_hash, target_hash, extra_hash, target_hash, extra_hash);
                }
                else
                {
                    var short_hash = (curdata_hash + prevdata_hash + curunit_hash + prevunit_hash + target_hash + extra_hash);
                    for (int i = 1; i <= 4; i++) short_hash += PlayerInfo[i].ToString();
                    Console.WriteLine(short_hash);
                    Console.WriteLine("Hash = {0}", short_hash.GetHashCode());
                }
            }
        }

        void DragonLordDeathCheck()
        {
            for (int p = 1; p <= 4; p++)
            {
                if (PlayerInfo[p].DragonLordAlive)
                {
                    if (PlayerInfo[p].DragonLords == 0)
                    {
                        PlayerInfo[p].DragonLordAlive = false;

                        if (!MapEditorActive)
                        {
                            vec2 grid_coord = DataGroup.DragonLordDeathGridCoord();
                            DragonLordDeath(grid_coord, p);
                        }
                    }
                }
                else
                {
                    if (PlayerInfo[p].DragonLords > 0)
                    {
                        PlayerInfo[p].DragonLordAlive = true;
                    }                    
                }
            }
        }

        void DoGoldUpdate()
        {
            if (MapEditorActive) return;

            for (int player = 1; player <= 4; player++) PlayerInfo[player].GoldUpdate();
        }

        void DoJadeUpdate()
        {
            if (MapEditorActive) return;

            for (int player = 1; player <= 4; player++) PlayerInfo[player].JadeUpdate();
        }
    }
}
