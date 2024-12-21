﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Interfaces;
using Microsoft.Xna.Framework;

namespace Gameproject.Enemies.behavior
{
    public class MonsterBehavior : IEnemybehavior
    {
        private const float Snelheid = 1f;
        public void Execute(Monster enemy, Vector2 heroPositie)
        {

            var monster = (Monster)enemy;
            Vector2 richting = heroPositie - monster.Positie;

            float afstand = richting.Length();

            if (afstand > 1f)
            {
                richting.Normalize();
                monster.Positie += richting * Snelheid;
            }
        }
        
    }
}