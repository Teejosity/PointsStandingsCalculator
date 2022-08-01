namespace PointsStandingsCalculator
{
    class Calculator
    {
        static void Main(string[] args)
        {
            Standings? standings = GetLeaderboardsInput();
            if (standings == null)
            {
                return;
            }
            Tournament? tournament = GetTournamentInformation(standings);

        }

        static Standings? GetLeaderboardsInput()
        {
            Console.WriteLine("Enter number of teams:");
            int total_teams = 0;
            string? teams = Console.ReadLine();
            if (teams == null)
            {
                Console.WriteLine("Invalid number of teams.");
                return null;
            }

            try
            {
                total_teams = int.Parse(teams);
            }
            catch (System.FormatException ex)
            {
                Console.WriteLine("Invalid number of teams");
                return null;
            }

            Standings standings = new Standings();

            int curr_teams = 0;

            while (curr_teams < total_teams)
            {
                Console.WriteLine("Enter team name:");
                string? team_name = Console.ReadLine();
                if (team_name == null)
                {
                    Console.WriteLine("Invalid name.");
                    continue;
                }

            askPoints:

                Console.WriteLine("Enter team points:");
                string? temp = Console.ReadLine();
                if (temp == null || temp == "")
                {
                    Console.WriteLine("Invalid point value.");
                    goto askPoints;
                }
                int team_points = 0;

                try
                {
                    team_points = int.Parse(temp);
                }
                catch (System.FormatException ex)
                {
                    Console.WriteLine("Invalid point value.");
                    goto askPoints;
                }

                standings.AddTeam(team_name, team_points);

                curr_teams++;
            }

            standings.DisplayStandings();
            return standings;
        }

        static Tournament? GetTournamentInformation(Standings standings)
        {
            Console.WriteLine("Enter number of events reminaing:");
            int total_events = 0;
            string? events = Console.ReadLine();
            if (events == null)
            {
                Console.WriteLine("Invalid number of teams.");
                return null;
            }

            try
            {
                total_events = int.Parse(events);
            }
            catch (System.FormatException ex)
            {
                Console.WriteLine("Invalid number of teams");
                return null;
            }

            //TODO: implement
            return null;
        }

    }
    class Standings
    {
        private List<KeyValuePair<string, int>> _leaderboard;
        private int longest_name = 0;

        public Standings()
        {
            _leaderboard = new List<KeyValuePair<string, int>>();
        }

        public void AddTeam(string name, int points)
        {
            _leaderboard.Add(new KeyValuePair<string, int>(name, points));
            _leaderboard.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            if (name.Length > longest_name)
            {
                longest_name = name.Length;
            }
        }

        public void DisplayStandings()
        {
            Console.WriteLine("\nOverall Standings\n" + new string ('-', 5 + longest_name + 2 + _leaderboard[0].Value.ToString().Length));
            for (int place = 1; place <= _leaderboard.Count; place++)
            {
                KeyValuePair<string, int> pair = _leaderboard[place - 1];
                string buff = place + ". " + pair.Key;
                while (buff.Length < (5 + longest_name))
                {
                    buff += " ";
                }
                Console.WriteLine(buff + "| " + pair.Value);
            }
        }
    }

    class Tournament //WIP
    {
        private int num_teams;
        private int remaining_events;
        private List<int> point_distribution;

        public Tournament(int num_teams, int remaining_events)
        {
            this.num_teams = num_teams;
            this.remaining_events = remaining_events;
            this.point_distribution = new List<int>(num_teams);
        }

        public void InsertPointDistribution(List<int> distribution)
        {
            point_distribution = distribution;
        }
    }
}