using treasureMap;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{
    /// <summary>
    /// Classen principale du programme
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args)
    {
        string filePath = string.Empty;

        while (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("Veuillez saisir le chemin vers le fichier d'entrée : ");
            filePath = Console.ReadLine();
        }

        Console.WriteLine($"Chemin saisi : {filePath}");

        StreamReader sr = new StreamReader(filePath);
        string line = sr.ReadLine();
        // On lit le fichier pour initialiser toutes les données
        Map treasureMap = new Map(line.Split('-').Select(data => data.Trim()).ToArray());
        while ((line = sr.ReadLine()) != null)
        {
            // On trim par défaut toutes les données après le split
            string[] lineData = line.Split('-').Select(data => data.Trim()).ToArray();
            if (!line.StartsWith('#'))
            {
                switch (line.First())
                {
                    case 'C':
                        Console.WriteLine("Ligne ignorée : Il ne peut y avoir qu'une définition de carte par fichier.");
                        break;

                    case 'M':
                        try
                        {
                            Tile mountain = treasureMap.GetTileByPos(int.Parse(lineData[1]), int.Parse(lineData[2]));
                            mountain.SetType("M");
                        }
                        catch (NullReferenceException nre)
                        {
                            Console.WriteLine("Ligne ignorée : " + nre.Message);
                        }
                        break;

                    case 'T':
                        try
                        {
                            Tile treasure = treasureMap.GetTileByPos(int.Parse(lineData[1]), int.Parse(lineData[2]));
                            treasure.SetTreasures(int.Parse(lineData[3]));
                        }
                        catch (NullReferenceException nre)
                        {
                            Console.WriteLine("Ligne ignorée : " + nre.Message);
                        }
                        break;

                    case 'A':
                        try
                        {
                            Tile adventurer = treasureMap.GetTileByPos(int.Parse(lineData[2]), int.Parse(lineData[3]));
                            adventurer.SetAdventurer(true);

                            treasureMap.AddAdventurer(new Adventurer(int.Parse(lineData[2]), int.Parse(lineData[3]), lineData[1], lineData[5], lineData[4]));
                        }
                        catch (NullReferenceException nre)
                        {
                            Console.WriteLine("Ligne ignorée : " + nre.Message);
                        }
                        break;

                    default:
                        Console.WriteLine($"Caractère non reconnu dans la séquence des possibilités :  {line.First()}");
                        break;
                }
            }

        }

        Console.WriteLine("Initialisation terminée. Données actuelles : ");
        Console.WriteLine(treasureMap.ToString());
        Console.WriteLine("Appuyez sur une touche pour lancer la simulation.");
        Console.ReadLine();
    }
}