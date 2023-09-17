using System.Text;

namespace treasureMap
{
    /// <summary>
    /// Classe principale décrivant la carte au trésor
    /// </summary>
    internal class Map
    {
        #region Properties
        /// <summary>
        /// Liste des cases de la carte
        /// </summary>
        public List<Tile> Tiles { set; get; }

        /// <summary>
        /// Liste des cases de la carte
        /// </summary>
        public List<Adventurer> Adventurers { set; get; }

        /// <summary>
        /// Longueur de la carte
        /// </summary>
        public int mapHeight { get; }

        /// <summary>
        /// Largeur de la carte
        /// </summary>
        public int mapWidth { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public Map(string[] mapData)
        {
            if (mapData[0] != "C")
            {
                throw new ArgumentException($"Erreur d'initialisation. Type de ligne attendu : 'C'. Type de ligne reçu : '{mapData[0]}'");
            }

            mapWidth = int.Parse(mapData[1]);
            mapHeight = int.Parse(mapData[2]);

            Tiles = new List<Tile>();
            Adventurers = new List<Adventurer>();

            InitialiseTiles();
        }
        #endregion

        /// <summary>
        /// Initialise la liste des case de la carte au trésor
        /// </summary>
        private void InitialiseTiles()
        {
            for (int i = 0; i < this.mapHeight; i++)
            {
                for (int j = 0; j < this.mapWidth; j++)
                {
                    this.Tiles.Add(new Tile(j, i));
                }
            }
        }

        /// <summary>
        /// Récupère une case selon les coordonnées
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public Tile GetTileByPos(int width, int height)
        {
            Tile tile = Tiles.FirstOrDefault(tile => tile.WidthPos == width && tile.HeightPos == height);
            if (tile == null)
            {
                throw new NullReferenceException($"La case n'existe pas : L = {width} - H = {height}");
            }
            return tile;
        }

        /// <summary>
        /// Ajoute un aventurier sur la carte
        /// </summary>
        /// <param name="adventurer"></param>
        public void AddAdventurer(Adventurer adventurer)
        {
            this.Adventurers.Add(adventurer);
        }

        /// <summary>
        /// Redéfinition de la méthode ToString pour afficher les détails de la carte
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"La carte contient {this.Tiles.Count} cases. Elle fait {this.mapWidth} cases de largeur et {this.mapHeight} de hauteur.");
            sb.Append(Environment.NewLine);
            sb.Append($"Il y'a {this.Tiles.Where(tile => tile.Type == "M").Count()} case(s) de montagnes.");
            sb.Append(Environment.NewLine);
            sb.Append($"Il y'a {this.Adventurers.Count} aventurier(s) présent(s).");
            sb.Append(Environment.NewLine);
            sb.Append($"Il y'a {this.Tiles.Where(tile => tile.TreasuresNb != 0).Sum(tile => tile.TreasuresNb)} trésor(s) à trouver.");

            return sb.ToString();
        }

        /// <summary>
        /// Ecrit le fichier de sortie
        /// </summary>
        /// <param name="path"></param>
        public void WriteOutputFile(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                OutputFileLine output = new OutputFileLine(this);
                writer.WriteLine(output);

                foreach(Tile tile in this.Tiles)
                {
                    if (tile.Type == "M" || tile.TreasuresNb != 0)
                    {
                        output = new OutputFileLine(tile);
                        writer.WriteLine(output);
                    }
                }

                foreach(Adventurer adventurer in this.Adventurers)
                {
                    output = new OutputFileLine(adventurer);
                    writer.WriteLine(output);
                }
            }

            Console.WriteLine("Fin de l'écriture du fichier.");
        }
    }
}
