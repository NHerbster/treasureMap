using treasureMap;

internal class Program
{
    /// <summary>
    /// Classe principale du programme
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
        string outputPath = filePath.Insert(filePath.Length - 4, "_output");

        Console.WriteLine($"Chemin saisi : {filePath}");
        Console.WriteLine($"Le fichier de sortie sera : {outputPath}");

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

        int nbTurns = treasureMap.Adventurers.Select(a => a.MovementsList).Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur).Length;
        for (int i = 0; i < nbTurns; i++)
        {
            foreach (Adventurer adventurer in treasureMap.Adventurers)
            {
                if (adventurer.MovementsList.Length >= i + 1)
                {
                    switch (adventurer.MovementsList[i])
                    {
                        case 'A':
                            Tile nextTile = null;
                            switch (adventurer.Orientation)
                            {
                                case "N":
                                    nextTile = treasureMap.GetTileByPos(adventurer.WidthPos, adventurer.HeightPos - 1);
                                    break;
                                case "S":
                                    nextTile = treasureMap.GetTileByPos(adventurer.WidthPos, adventurer.HeightPos + 1);
                                    break;
                                case "E":
                                    nextTile = treasureMap.GetTileByPos(adventurer.WidthPos + 1, adventurer.HeightPos);
                                    break;
                                case "O":
                                    nextTile = treasureMap.GetTileByPos(adventurer.WidthPos - 1, adventurer.HeightPos);
                                    break;
                            }
                            adventurer.ComputeAction(nextTile, treasureMap.GetTileByPos(adventurer.WidthPos, adventurer.HeightPos));
                            break;
                        case 'G':
                        case 'D':
                            adventurer.RotateAdventurer(adventurer.MovementsList[i]);
                            break;
                        default:
                            Console.WriteLine($"Action {adventurer.MovementsList[i]} non reconnue pour l'aventurier {adventurer.Name}. Son tour a été passé.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"Toutes les actions de {adventurer.Name} ont été effectuées.");
                }
            }
        }

        treasureMap.WriteOutputFile(outputPath);
    }
}