using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace treasureMap
{
    /// <summary>
    /// Classe décrivant un aventurier
    /// </summary>
    internal class Adventurer : MapObject
    {
        #region Properties
        /// <summary>
        /// Nom de l'aventurier
        /// </summary>
        private string Name { get; set; }

        /// <summary>
        /// Indique l'orientation de l'aventurier
        /// </summary>
        private string Orientation { get; set; }

        /// <summary>
        /// Indique la séquence des mouvements pour l'aventurier
        /// </summary>
        private string movementsList { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principale de la classe
        /// </summary>
        /// <param name="width">Position horizontale</param>
        /// <param name="height">Position verticale</param>
        public Adventurer(int width, int height, string name, string movements, string orientation) : base(width, height)
        {
            Name = name;
            Orientation = orientation;
            movementsList = movements;
        }
        #endregion
    }
}
