using System.Text;

class Jeu_de_maths
{
    // Instance pour générer des nombres aléatoires
    Random random = new Random();

    // Indique si la partie est terminée
    private bool partieFinie;

    // Numéro de la question actuelle
    private int questionNumber = 1;

    // Points accumulés par le joueur
    private int pointJoueur;

    // Plage des nombres aléatoires pour les questions
    private int NOMBREMIN = 0; // Valeur minimale des nombres
    private int NOMBREMAX = 10; // Valeur maximale par défaut

    public static void Main(string[] args)
    {
        // Création de l'objet principal du jeu
        Jeu_de_maths jd = new Jeu_de_maths();

        // Affiche le message de bienvenue et demande la difficulté
        jd.Start();

        // Début du jeu
        jd.PoserQuestion();
    }

    // Méthode permettant de changer la difficulté
    void ChangerDifficulte()
    {
        var numDif = 0;

        // Boucle pour s'assurer que l'utilisateur entre une difficulté valide
        while (true)
        {
            Console.WriteLine("Choisissez une difficulté :");
            Console.WriteLine("[1] : Facile | [2] : Moyen | [3] : Difficile");
            Console.WriteLine();
            string? difficulteStr = Console.ReadLine();

            // Vérifie si l'entrée est un entier
            if (int.TryParse(difficulteStr, out numDif))
            {
                Console.Clear();
                break; // Sort de la boucle si la saisie est valide
            }
            Console.WriteLine("Veuillez rentrer un nombre en 1 et 3.");
            Console.WriteLine();
        }

        // Définit la plage des nombres en fonction de la difficulté choisie
        switch (numDif)
        {
            case 1: NOMBREMAX = 10; // Niveau Facile
                break;
            case 2: NOMBREMAX = 20; // Niveau Moyen
                break;
            case 3: NOMBREMAX = 50; // Niveau Difficile
                break;
        }
    }

    // Méthode pour générer et afficher une question mathématique
    public void GenererQuestion(string question)
    {
        // Génération de deux nombres aléatoires dans la plage définie
        int chiffreA = random.Next(NOMBREMIN, NOMBREMAX);
        int chiffreB = random.Next(NOMBREMIN, NOMBREMAX);
        int reponse;

        // Selon le type de question, génère l'opération correspondante
        switch (question)
        {
            case "Addition":
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"{chiffreA} + {chiffreB}");
                reponse = chiffreA + chiffreB; // Calcule la réponse
                break;

            case "Soustraction":
                // Évite des résultats négatifs en intervertissant les nombres si nécessaire
                if (chiffreA < chiffreB)
                {
                    (chiffreA, chiffreB) = (chiffreB, chiffreA);
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"{chiffreA} - {chiffreB}");
                reponse = chiffreA - chiffreB;
                break;

            case "Multiplication":
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{chiffreA} * {chiffreB}");
                reponse = chiffreA * chiffreB; // Calcule la réponse
                break;

            case "Division":
                // Génération pour garantir un résultat entier
                chiffreB = random.Next(1, 10); // Diviseur
                reponse = random.Next(1, 10); // Résultat attendu
                chiffreA = reponse * chiffreB; // Dividende est un multiple du diviseur
                if (chiffreA < chiffreB)
                {
                    (chiffreA, chiffreB) = (chiffreB, chiffreA);
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{chiffreA} / {chiffreB}");
                reponse = chiffreA / chiffreB; // Calcule la réponse
                break;

            default:
                // Lève une exception si l'opération est inconnue
                throw new ArgumentException("Opération inconnue");
        }

        // Vérifie la réponse de l'utilisateur
        LectureEntree(reponse);
    }

    // Méthode principale qui pose les questions jusqu'à la fin de la partie
    public void PoserQuestion()
    {
        while (!partieFinie)
        {
            // Choisit et génère une question selon l'étape actuelle
            switch (questionNumber)
            {
                case 1: GenererQuestion("Addition"); break;
                case 2: GenererQuestion("Soustraction"); break;
                case 3: GenererQuestion("Multiplication"); break;
                case 4:
                    GenererQuestion("Division");
                    partieFinie = true; // Termine la partie après la dernière question
                    AfficherScore(); // Affiche le score final
                    break;
            }
            questionNumber++; // Passe à la question suivante
        }
    }

    // Méthode pour vérifier la réponse entrée par le joueur
    void LectureEntree(int reponseCalcule)
    {
        Console.WriteLine("Votre réponse :");
        string? reponseStr = Console.ReadLine();
        bool reponseValide = false;

        // Boucle jusqu'à ce que l'utilisateur entre une réponse valide
        while (!reponseValide)
        {
            if (int.TryParse(reponseStr, out int reponse) && reponse >= 0)
            {
                reponseValide = true;
                if (reponse == reponseCalcule)
                {
                    // Augmente le score si la réponse est correcte
                    pointJoueur++;
                    Console.WriteLine("Bonne réponse !");
                }
                else
                {
                    Console.WriteLine($"Mauvaise réponse ! La bonne réponse était : {reponseCalcule}");
                }
            }
            else
            {
                // Demande une nouvelle saisie en cas de réponse invalide
                Console.WriteLine("Tu dois rentrer un nombre valide.");
                reponseStr = Console.ReadLine();
            }
        }
    }

    // Affiche le score final et propose de recommencer ou quitter
    void AfficherScore()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"Vous avez gagné : {pointJoueur} points");

        // Passe au menu de fin de partie
        PartieTerminee();
    }
    
    //Affiche le menu en couleur
    void AfficherMenu()
    {
        Console.Write("[");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("R");
        Console.ResetColor();
        Console.Write("] Recommencer | [");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("C");
        Console.ResetColor();
        Console.Write("] Changer la difficulté | [");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("N");
        Console.ResetColor();
        Console.WriteLine("] Quitter");
    }
    
    // Méthode de démarrage du jeu (message de bienvenue et choix de difficulté)
    void Start()
    {
        Console.WriteLine("Bienvenue dans le Jeu de maths !");
        Console.WriteLine("*******************************");
        Console.WriteLine();
        ChangerDifficulte(); // Demande la difficulté
    }

    // Réinitialise les variables pour recommencer une nouvelle partie
    private void Reset()
    {
        Console.Clear();
        questionNumber = 1;
        pointJoueur = 0;
        partieFinie = false;
        PoserQuestion(); // Relance les questions
    }

    // Propose une confirmation avant de quitter le jeu
    void ExitJeu()
    {
        

        while (true)
        {
            Console.WriteLine("Êtes-vous sûr de vouloir quitter ? [O/N]");
            string? confirmation = Console.ReadLine()?.ToUpper();
            
            switch (confirmation)
            {
                case "O":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Au revoir !");
                    return;
                case "N":
                    Console.Clear();
                    PartieTerminee(); // Retourne au menu de fin de partie
                    return;
                default: 
                    Console.WriteLine();
                    Console.WriteLine("Veuillez entrer [O] pour Oui ou [N] pour Non.");
                    break;
            }   
        }
    }

    // Menu pour recommencer, changer la difficulté ou quitter
    void PartieTerminee()
    {
        while (true)
        {
            AfficherMenu();
            // Gère le choix de l'utilisateur
            string? reponseStr = Console.ReadLine()?.ToUpper();
            switch (reponseStr)
            {
                case "R": Reset(); return;
                case "C":
                    ChangerDifficulte();
                    Reset(); // Relance après changement de difficulté
                    return;
                case "N": ExitJeu(); return;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Option invalide. Choisissez parmi [R], [C], ou [N].");
                    break;
            }   
        }
        
    }
}
