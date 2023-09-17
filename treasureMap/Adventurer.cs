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
        private string name { get; set; }

        /// <summary>
        /// Accesseur public du nom de l'aventurier
        /// </summary>
        public string Name { get { return name; } }

        /// <summary>
        /// Indique l'orientation de l'aventurier
        /// </summary>
        private string orientation { get; set; }

        /// <summary>
        /// Accesseur public de l'orientation de l'aventurier
        /// </summary>
        public string Orientation { get { return orientation; } }

        /// <summary>
        /// Indique la séquence des mouvements pour l'aventurier
        /// </summary>
        private string movementsList { get; set; }

        /// <summary>
        /// Accesseur public de la liste des mouvements de l'aventurier
        /// </summary>
        public string MovementsList { get {  return movementsList; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principale de la classe
        /// </summary>
        /// <param name="width">Position horizontale</param>
        /// <param name="height">Position verticale</param>
        public Adventurer(int width, int height, string name, string movements, string orientation) : base(width, height)
        {
            this.name = name;
            this.orientation = orientation;
            movementsList = movements;
        }
        #endregion

        /// <summary>
        /// Modifie l'orientation de l'aventurier
        /// </summary>
        /// <param name="direction"></param>
        public void RotateAdventurer(char direction)
        {
            switch (this.orientation) 
            {
                case "N":
                    if (direction == 'D')
                        this.orientation = "E";
                    else
                        this.orientation = "O";
                    break;
                case "S":
                    if (direction == 'D')
                        this.orientation = "O";
                    else
                        this.orientation = "E";
                    break;
                case "E":
                    if (direction == 'D')
                        this.orientation = "S";
                    else
                        this.orientation = "N";
                    break;
                case "O":
                    if (direction == 'D')
                        this.orientation = "N";
                    else
                        this.orientation = "S";
                    break;
            }
        }

        /// <summary>
        /// Effectue l'action "Avancer" de l'aventurier
        /// </summary>
        /// <param name="nextTile"></param>
        public void ComputeAction(Tile nextTile, Tile currentTile)
        {
            if (nextTile.IsAvailable())
            {
                nextTile.SetAdventurer(true);
                currentTile.SetAdventurer(false);
                this.SetNewPosition(nextTile.WidthPos, nextTile.HeightPos);
                if(nextTile.TreasuresNb > 0)
                {
                    treasureNb++;
                    nextTile.treasureNb--;
                }
            }
            else
            {
                Console.WriteLine($"L'aventurier {this.Name} ne peut pas avancer sur la case [{nextTile.WidthPos} - {nextTile.HeightPos}]. Son tour a été passé.");
            }
        }

        /// <summary>
        /// Met à jour la position de l'aventurier
        /// </summary>
        /// <param name="widthPos"></param>
        /// <param name="heightPos"></param>
        private void SetNewPosition(int widthPos, int heightPos)
        {
            this.widthPos = widthPos;
            this.heightPos = heightPos;
        }
    }
}
