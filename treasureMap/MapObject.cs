namespace treasureMap
{
    /// <summary>
    /// Classe mère définissant les différents objets présents sur la carte
    /// </summary>
    internal class MapObject
    {
        #region Properties
        /// <summary>
        /// Position horizontale de la case
        /// </summary>
        internal int widthPos { get; set; }

        /// <summary>
        /// Accesseur de la position horizontale de la case
        /// </summary>
        public int WidthPos { get { return this.widthPos; } }

        /// <summary>
        /// Position verticale de la case
        /// </summary>
        internal int heightPos { get; set; }

        /// <summary>
        /// Accesseur de la position verticale de la case
        /// </summary>
        public int HeightPos { get { return this.heightPos; } }

        /// <summary>
        /// Nombre de trésors
        /// </summary>
        internal int treasureNb { get; set; }

        /// <summary>
        /// Nombre de trésors
        /// </summary>
        public int TreasuresNb { get { return this.treasureNb; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principale de la classe
        /// </summary>
        /// <param name="width">Position horizontale</param>
        /// <param name="height">Position verticale</param>
        public MapObject(int width, int height)
        {
            widthPos = width;
            heightPos = height;
            treasureNb = 0;
        }
        #endregion
    }
}
