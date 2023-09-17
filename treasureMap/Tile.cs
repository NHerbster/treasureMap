namespace treasureMap
{
    /// <summary>
    /// Classe décrivant une case de la carte
    /// </summary>
    internal class Tile : MapObject
    {
        #region Properties
        /// <summary>
        /// Type d'environnement de la case
        /// </summary>
        private string type { get; set; }

        /// <summary>
        /// Type d'environnement de la case
        /// </summary>
        public string Type { get { return this.type; } }

        /// <summary>
        /// Indique si un aventurier est sur la case
        /// </summary>
        private bool hasAdventurer { get; set; }

        /// <summary>
        /// Indique si un aventurier est sur la case
        /// </summary>
        public bool HasAdventurer { get { return this.hasAdventurer; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principale de la classe
        /// </summary>
        /// <param name="width">Position horizontale</param>
        /// <param name="height">Position verticale</param>
        public Tile(int width, int height) : base(width, height)
        {
            // Par défaut on considère P le type plaine
            type = "P";
            hasAdventurer = false;
        }
        #endregion

        /// <summary>
        /// Permet de modifier le type de la case.
        /// </summary>
        /// <param name="type"></param>
        public void SetType(string type)
        {
            this.type = type;
        }

        /// <summary>
        /// Permet de modifier le nombre de trésors sur la case
        /// </summary>
        /// <param name="treasuresNb"></param>
        public void SetTreasures(int treasuresNb)
        {
            this.treasureNb = treasuresNb;
        }

        /// <summary>
        /// Permet de modifier e booléen indiquant si un aventurier est présent sur la case
        /// </summary>
        /// <param name="hasAdventurer"></param>
        public void SetAdventurer(bool hasAdventurer)
        {
            this.hasAdventurer = hasAdventurer;
        }

        /// <summary>
        /// Indique si la case est accessible pour un aventurier. (Ne doit pas avoir d'aventurier et le type doit être différent de M)
        /// </summary>
        /// <returns></returns>
        public bool IsAvailable()
        {
            return !this.hasAdventurer && this.type != "M";
        }
    }
}
