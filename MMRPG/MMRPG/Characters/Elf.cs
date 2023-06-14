﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMRPG
{
    public class Elf
    {
        public string Name { get; }
        public int Health { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }

        public object Armor { get; private set; }

        public Elf(string name)
        {
            Name = name;
        }

        public void Wear(object armor)
        {
            Armor = armor;
        }
    }
}