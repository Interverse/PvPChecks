﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PvPChecks {
    class DataIDs {
        //Steal this class, idc

        public static int[] ammoIDs = new int[] {
            //Bullet Item IDs
            97, 234, 278, 515, 546, 1179, 1302, 1335, 1342, 1349, 1350, 1351, 1352, 3104, 3567,
            //Arrow Item IDs
            40, 41, 47, 51, 265, 516, 545, 682, 988, 1235, 1334, 1341, 3003, 3568,
            //Rocket Item IDs
            771, 772, 773, 774,
            //Dart Item IDs
            1310, 3009, 3010, 3011,
            //Misc Ammo Item IDs
            283, 154, 1261, 1783, 1785, 1836, 931, 949, 3108
        };

        public static int[] UniveralPrefixIDs = new int[] {
            36, 37, 38, 39, 40, 41, 53, 54, 55, 56, 57, 59, 60, 61
        };

        //Tools cannot have these prefixes
        public static int[] WeaponPrefixIDs = new int[] {
            20, 44, 45, 46, 47, 48, 49, 50, 51, 76
        };

        public static int[] MeleePrefixIDs = new int[] {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 81
        };

        public static int[] RangedPrefixIDs = new int[] {
            16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 58, 82
        };

        public static int[] MagicPrefixIDs = new int[] {
            26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 52, 83
        };

        //This was never used but I don't have the heart to delete 1 hour of list copying
        public static Dictionary<string, int> buffIDs = new Dictionary<string, int> {
            { "Obsidian Skin", 1 },
            { "Regeneration", 2 },
            { "Swiftness", 3 },
            { "Gills", 4 },
            { "Ironskin", 5 },
            { "Mana Regeneration", 6 },
            { "Magic Power", 7 },
            { "Featherfall", 8 },
            { "Spelunker", 9 },
            { "Invisibility", 10 },
            { "Shine", 11 },
            { "Night Owl", 12 },
            { "Battle", 13 },
            { "Thorns", 14 },
            { "Water Walking", 15 },
            { "Archery", 16 },
            { "Hunter", 17 },
            { "Gravitation", 18 },
            { "Shadow Orb", 19 },
            { "Poisoned", 20 },
            { "Potion Sickness", 21 },
            { "Darkness", 22 },
            { "Cursed", 23 },
            { "On Fire!", 24 },
            { "Tipsy", 25 },
            { "Well Fed", 26 },
            { "Fairy", 27 },
            { "Werewolf", 28 },
            { "Clairvoyance", 29 },
            { "Bleeding", 30 },
            { "Confused", 31 },
            { "Slow", 32 },
            { "Weak", 33 },
            { "Merfolk", 34 },
            { "Silenced", 35 },
            { "Broken Armor", 36 },
            { "Horrified", 37 },
            { "The Tongue", 38 },
            { "Cursed Inferno", 39 },
            { "Pet Bunny", 40 },
            { "Baby Penguin", 41 },
            { "Pet Turtle", 42 },
            { "Paladin's Shield", 43 },
            { "Frostburn", 44 },
            { "Baby Eater", 45 },
            { "Chilled", 46 },
            { "Frozen", 47 },
            { "Honey", 48 },
            { "Pygmies", 49 },
            { "Baby Skeletron Head", 50 },
            { "Baby Hornet", 51 },
            { "Tiki Spirit", 52 },
            { "Pet Lizard", 53 },
            { "Pet Parrot", 54 },
            { "Baby Truffle", 55 },
            { "Pet Sapling", 56 },
            { "Wisp", 57 },
            { "Rapid Healing", 58 },
            { "Shadow Dodge", 59 },
            { "Crystal Leaf", 60 },
            { "Baby Dinosaur", 61 },
            { "Ice Barrier", 62 },
            { "Panic!", 63 },
            { "Baby Slime", 64 },
            { "Eyeball Spring", 65 },
            { "Baby Snowman", 66 },
            { "Burning", 67 },
            { "Suffocating" , 68 },
            { "Ichor", 69 },
            { "Venom", 70 },
            { "Weapon Imbue Venom", 71 },
            { "Midas", 72 },
            { "Weapon Imbue Cursed Flames", 73 },
            { "Weapon Imbue Fire", 74 },
            { "Weapon Imbue Gold", 75 },
            { "Weapon Imbue Ichor", 76 },
            { "Weapon Imbue Nanites", 77 },
            { "Weapon Imbue Confetti", 78 },
            { "Weapon Imbue Poison", 79 },
            { "Blackout", 80 },
            { "Pet Spider", 81 },
            { "Squashling", 82 },
            { "Ravens", 83 },
            { "Black Cat", 84 },
            { "Cursed Sapling", 85 },
            { "Water Candle", 86 },
            { "Cozy Fire", 87 },
            { "Chaos State", 88 },
            { "Heart Lamp", 89 },
            { "Rudolph", 90 },
            { "Puppy", 91 },
            { "Baby Grinch", 92 },
            { "Ammo Box", 93 },
            { "Mana Sickness", 94 },
            { "Beetle Endurance 1", 95 },
            { "Beetle Endurance 2", 96 },
            { "Beetle Endurance 3", 97 },
            { "Beetle Might 1", 98 },
            { "Beetle Might 2", 99 },
            { "Beetle Might 3", 100 },
            { "Fairy 1", 101 },
            { "Fairy 2", 102 },
            { "Wet", 103 },
            { "Mining", 104 },
            { "Heartreach", 105 },
            { "Calm", 106 },
            { "Builder", 107 },
            { "Titan", 108 },
            { "Flipper", 109 },
            { "Summoning", 110 },
            { "Dangersense", 111 },
            { "Ammo Reservation", 112 },
            { "Lifeforce", 113 },
            { "Endurance", 114 },
            { "Rage", 115 },
            { "Inferno", 116 },
            { "Wrath", 117 },
            { "Minecart 1", 118 },
            { "Lovestruck", 119 },
            { "Stinky", 120 },
            { "Fishing", 121 },
            { "Sonar", 122 },
            { "Crate", 123 },
            { "Warmth", 124 },
            { "Hornet", 125 },
            { "Imp", 126 },
            { "Zephyr Fish", 127 },
            { "Bunny Mount", 128 },
            { "Pigron Mount", 129 },
            { "Slime Mount", 130 },
            { "Turtle Mount", 131 },
            { "Bee Mount", 132 },
            { "Spider", 133 },
            { "Twins", 134 },
            { "Pirate", 135 },
            { "Mini Minotaur", 136 },
            { "Slime", 137 },
            { "Minecart 2", 138 },
            { "Sharknado", 139 },
            { "UFO", 140 },
            { "UFO Mount", 141 },
            { "Drill Mount", 142 },
            { "Scutlix Mount", 143 },
            { "Electrified", 144 },
            { "Moon Bite", 145 },
            { "Happy!", 146 },
            { "Banner", 147 },
            { "Feral Bite", 148 },
            { "Webbed", 149 },
            { "Bewitched", 150 },
            { "Life Drain", 151 },
            { "Magic Lantern", 152 },
            { "Shadowflame", 153 },
            { "Baby Face Monster", 154 },
            { "Crimson Heart", 155 },
            { "Stoned", 156 },
            { "Peace Candle", 157 },
            { "Star in a Bottle", 158 },
            { "Sharpened", 159 },
            { "Dazed", 160 },
            { "Deadly Sphere", 161 },
            { "Unicorn Mount", 162 },
            { "Obstructed", 163 },
            { "Distorted", 164 },
            { "Dryad's Blessing", 165 },
            { "Minecart 3", 166 },
            { "Minecart 4", 167 },
            { "Cute Fishron", 168 },
            { "Penetrated", 169 },
            { "Solar Blaze 1", 170 },
            { "Solar Blaze 2", 171 },
            { "Solar Blaze 3", 172 },
            { "Life Nebula 1", 173 },
            { "Life Nebula 2", 174 },
            { "Life Nebula 3", 175 },
            { "Mana Nebula 1", 176 },
            { "Mana Nebula 2", 177 },
            { "Mana Nebula 3", 178 },
            { "Damage Nebula 1", 179 },
            { "Damage Nebula 2", 180 },
            { "Damage Nebula 3", 181 },
            { "Stardust Cell", 182 },
            { "Celled", 183 },
            { "Minecart 5", 184 },
            { "Minecart 6", 185 },
            { "Dryad's Bane", 186 },
            { "Stardust Guardian" , 187 },
            { "Stardust Dragon", 188 },
            { "Daybroken", 189 },
            { "Suspicious Looking Eye", 190 },
            { "Companion Cube", 191 },
            { "Sugar Rush", 192 },
            { "Basilisk Mount", 193 },
            { "Mighty Wind", 194 },
            { "Withered Armor", 195 },
            { "Withered Weapon", 196 },
            { "Oozed", 197 },
            { "Striking Moment", 198 },
            { "Creative Shock", 199 },
            { "Propeller Gato", 200 },
            { "Flickerwick", 201 },
            { "Hoardagron", 202 },
            { "Betsy's Curse", 203 },
            { "Oiled", 204 },
            { "Ballista Panic!", 205 }
        };
    }
}
