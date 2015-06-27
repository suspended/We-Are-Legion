﻿using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Audio;

namespace Game
{
    public static class Sounds
    {
        public static void MaybePlay(this Sound sound, float volume=1, float pitch=1, float pan=1)
        {
            if (sound == null) return;

            sound.Play(volume, pitch, pan);
        }

        public static Sound
            PlaceBuilding, BuildingExplode, DyingUnit, DyingDragonLord, ExplodingDragonLord, GameOver, GiveOrder;

        public static void Initialize()
        {
            PlaceBuilding = SoundWad.Wad.FindByName("PlaceBuilding", FindStyle.NullIfNotFound);
            BuildingExplode = SoundWad.Wad.FindByName("BuildingExplode", FindStyle.NullIfNotFound);
            DyingUnit = SoundWad.Wad.FindByName("DyingUnit", FindStyle.NullIfNotFound);
            GameOver = SoundWad.Wad.FindByName("GameOver", FindStyle.NullIfNotFound);
            GiveOrder = SoundWad.Wad.FindByName("GiveOrder", FindStyle.NullIfNotFound);
            DyingDragonLord = SoundWad.Wad.FindByName("DyingDragonLord", FindStyle.NullIfNotFound);
            ExplodingDragonLord = SoundWad.Wad.FindByName("ExplodingDragonLord", FindStyle.NullIfNotFound);
        }
    }
}