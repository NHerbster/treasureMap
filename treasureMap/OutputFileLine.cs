using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace treasureMap
{
    /// <summary>
    /// Classe décrivant le format d'une ligne du fichier de sortie
    /// </summary>
    internal class OutputFileLine
    {
        #region Properties
        private string Type { get; set; }

        private int Width { get; set; }

        private int Height { get; set; }

        private int NbTreasures { get; set; }

        private string Orientation { get; set; }

        private string Name { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur pour un objet Aventurier
        /// </summary>
        /// <param name="type"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="nbTreasures"></param>
        /// <param name="orientation"></param>
        /// <param name="name"></param>
        public OutputFileLine(Adventurer adventurer)
        {
            Type = "A";
            Height = adventurer.HeightPos;
            Width = adventurer.WidthPos;
            NbTreasures = adventurer.TreasuresNb;
            Orientation = adventurer.Orientation;
            Name = adventurer.Name;
        }

        /// <summary>
        /// Constructeur pour une case avec un trésor
        /// </summary>
        /// <param name="type"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="nbTreasures"></param>
        public OutputFileLine(Tile tile)
        {
            Type = tile.Type == "P" && tile.TreasuresNb != 0 ? "T" : tile.Type;
            Height = tile.HeightPos;
            Width = tile.WidthPos;
            NbTreasures = tile.TreasuresNb;
            Orientation = string.Empty;
            Name = string.Empty;
        }

        /// <summary>
        /// Constructeur basique
        /// </summary>
        /// <param name="type"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public OutputFileLine(Map treasureMap)
        {
            Type = "C";
            Height = treasureMap.mapHeight;
            Width = treasureMap.mapWidth;
            NbTreasures = 0;
            Orientation = string.Empty;
            Name = string.Empty;
        }
        #endregion

        public override string ToString()
        {
            string line = string.Empty;
            switch (Type)
            {
                case "C":
                case "M":
                    line = $"{Type} - {Width} - {Height}";
                    break;

                case "T":
                    line = $"{Type} - {Width} - {Height} - {NbTreasures}";
                    break;

                case "A":
                    line = $"{Type} - {Name} - {Width} - {Height} - {Orientation} - {NbTreasures}";
                    break;
                default:
                    Console.WriteLine("Type inconnu.");
                    break;
            }
            return line;
        }
    }
}
