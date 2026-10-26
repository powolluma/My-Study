using System;
using System.Collections.Generic;

class Team
{
    public int Id;
    public string Name;

    public Team(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

class Player
{
    public int Id;
    public string Name;
    public int TeamId;

    public Player(int id, string name, int teamId)
    {
        Id = id;
        Name = name;
        TeamId = teamId;
    }
}

class Match
{
    public int Id;
    public int HomeTeamId;
    public int AwayTeamId;
    public int HomeGoals;
    public int AwayGoals;
    public DateTime Date;
}

class Goal
{
    public int Id;
    public int MatchId;
    public int PlayerId;

    public Goal(int id, int matchId, int playerId)
    {
        Id = id;
        MatchId = matchId;
        PlayerId = playerId;
    }
}

class ScorerInfo
{
    public string Name;
    public string TeamName;
    public int Goals;

    public ScorerInfo(string name, string teamName, int goals)
    {
        Name = name;
        TeamName = teamName;
        Goals = goals;
    }
}

class TeamInfo
{
    public string Name;
    public int GoalsFor;
    public int GoalsAgainst;
    public int Points;
}

class Program
{
    static List<Team> teams = new List<Team>();
    static List<Player> players = new List<Player>();
    static List<Match> matches = new List<Match>();
    static List<Goal> goals = new List<Goal>();
    static Random random = new Random();
    static int nextMatchId = 1;
    static int nextGoalId = 1;

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        FillStartData();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1. Добавить случайные матчи");
            Console.WriteLine("2. Топ-3 бомбардиров команды");
            Console.WriteLine("3. Лучший бомбардир команды");
            Console.WriteLine("4. Топ-3 бомбардиров чемпионата");
            Console.WriteLine("5. Лучший бомбардир чемпионата");
            Console.WriteLine("6. Топ-3 команд по забитым голам");
            Console.WriteLine("7. Команда с большим количеством забитых голов");
            Console.WriteLine("8. Топ-3 команд, пропустивших меньше всего голов");
            Console.WriteLine("9. Команда, пропустившая меньше всего голов");
            Console.WriteLine("10. Топ-3 команд по очкам");
            Console.WriteLine("11. Команда с большим количеством очков");
            Console.WriteLine("12. Топ-3 команд с меньшим количеством очков");
            Console.WriteLine("13. Команда с меньшим количеством очков");
            Console.WriteLine("14. Разница голов всех команд");
            Console.WriteLine("15. Показать все матчи");
            Console.WriteLine("16. Показать матчи за дату");
            Console.WriteLine("17. Показать матчи команды");
            Console.WriteLine("18. Показать авторов голов за дату");
            Console.WriteLine("19. Добавить матч");
            Console.WriteLine("20. Изменить матч");
            Console.WriteLine("21. Удалить матч");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите пункт: ");

            string answer = Console.ReadLine();
            if (answer == "0")
                break;

            if (answer == "1") AddRandomMatches();
            else if (answer == "2") ShowTeamScorers(3);
            else if (answer == "3") ShowTeamScorers(1);
            else if (answer == "4") ShowScorers(0, 3);
            else if (answer == "5") ShowScorers(0, 1);
            else if (answer == "6") ShowTeamStatistics(1, 3);
            else if (answer == "7") ShowTeamStatistics(1, 1);
            else if (answer == "8") ShowTeamStatistics(2, 3);
            else if (answer == "9") ShowTeamStatistics(2, 1);
            else if (answer == "10") ShowTeamStatistics(3, 3);
            else if (answer == "11") ShowTeamStatistics(3, 1);
            else if (answer == "12") ShowTeamStatistics(4, 3);
            else if (answer == "13") ShowTeamStatistics(4, 1);
            else if (answer == "14") ShowTeamStatistics(5, 0);
            else if (answer == "15") ShowMatches(0, "", DateTime.MinValue);
            else if (answer == "16") ShowMatches(1, "", ReadDate("Введите дату (например, 2026-10-27): "));
            else if (answer == "17") ShowMatches(2, ReadText("Введите название команды: "), DateTime.MinValue);
            else if (answer == "18") ShowGoalsByDate(ReadDate("Введите дату (например, 2026-10-27): "));
            else if (answer == "19") AddOneMatch();
            else if (answer == "20") EditOneMatch();
            else if (answer == "21") DeleteOneMatch();
            else Console.WriteLine("Такого пункта нет");
        }
    }

    static void FillStartData()
    {
        teams.Add(new Team(1, "Реал Мадрид"));
        teams.Add(new Team(2, "Барселона"));
        teams.Add(new Team(3, "Атлетико Мадрид"));
        teams.Add(new Team(4, "Севилья"));

        players.Add(new Player(1, "Игрок Реала 1", 1));
        players.Add(new Player(2, "Игрок Реала 2", 1));
        players.Add(new Player(3, "Игрок Реала 3", 1));
        players.Add(new Player(4, "Игрок Барселоны 1", 2));
        players.Add(new Player(5, "Игрок Барселоны 2", 2));
        players.Add(new Player(6, "Игрок Барселоны 3", 2));
        players.Add(new Player(7, "Игрок Атлетико 1", 3));
        players.Add(new Player(8, "Игрок Атлетико 2", 3));
        players.Add(new Player(9, "Игрок Атлетико 3", 3));
        players.Add(new Player(10, "Игрок Севильи 1", 4));
        players.Add(new Player(11, "Игрок Севильи 2", 4));
        players.Add(new Player(12, "Игрок Севильи 3", 4));

        AddMatchData(1, 2, 2, 1, new DateTime(2026, 10, 1));
        AddMatchData(3, 4, 1, 1, new DateTime(2026, 10, 2));
        AddMatchData(2, 3, 3, 0, new DateTime(2026, 10, 5));
        AddMatchData(4, 1, 0, 2, new DateTime(2026, 10, 8));
    }

    // Задание 1
    static void AddRandomMatches()
    {
        int count = ReadInt("Сколько матчей добавить? ");
        int added = 0;

        for (int i = 0; i < count; i++)
        {
            int home = random.Next(1, teams.Count + 1);
            int away = random.Next(1, teams.Count + 1);
            while (away == home)
                away = random.Next(1, teams.Count + 1);

            int homeGoals = random.Next(0, 6);
            int awayGoals = random.Next(0, 6);
            DateTime date = new DateTime(2026, 1, 1).AddDays(random.Next(0, 365));

            AddMatchData(home, away, homeGoals, awayGoals, date);
            added++;
        }

        Console.WriteLine("Добавлено матчей: " + added);
    }

    // Показ команды
    static void ShowTeamScorers(int count)
    {
        string teamName = ReadText("Введите название команды: ");
        Team team = FindTeam(teamName);

        if (team == null)
        {
            Console.WriteLine("Команда не найдена");
            return;
        }

        ShowScorers(team.Id, count);
    }

    // Показ голов
    static void ShowScorers(int teamId, int count)
    {
        List<ScorerInfo> list = new List<ScorerInfo>();

        foreach (Player player in players)
        {
            if (teamId != 0 && player.TeamId != teamId)
                continue;

            int goalsCount = 0;
            foreach (Goal goal in goals)
            {
                if (goal.PlayerId == player.Id)
                    goalsCount++;
            }

            if (goalsCount > 0)
            {
                Team team = FindTeam(player.TeamId);
                list.Add(new ScorerInfo(player.Name, team.Name, goalsCount));
            }
        }

        for (int i = 0; i < list.Count; i++)
        {
            for (int j = i + 1; j < list.Count; j++)
            {
                if (list[j].Goals > list[i].Goals)
                {
                    ScorerInfo temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;
                }
            }
        }

        if (list.Count == 0)
        {
            Console.WriteLine("Забивших игроков пока нет");
            return;
        }

        int max = count;
        if (max == 0 || max > list.Count)
            max = list.Count;

        for (int i = 0; i < max; i++)
            Console.WriteLine((i + 1) + ". " + list[i].Name + " (" + list[i].TeamName + ") — голов: " + list[i].Goals);
    }

    // Задания 3
    static void ShowTeamStatistics(int type, int count)
    {
        List<TeamInfo> list = new List<TeamInfo>();

        foreach (Team team in teams)
        {
            TeamInfo info = new TeamInfo();
            info.Name = team.Name;
            info.GoalsFor = 0;
            info.GoalsAgainst = 0;
            info.Points = 0;

            foreach (Match match in matches)
            {
                if (match.HomeTeamId == team.Id)
                {
                    info.GoalsFor += match.HomeGoals;
                    info.GoalsAgainst += match.AwayGoals;
                    if (match.HomeGoals > match.AwayGoals) info.Points += 3;
                    else if (match.HomeGoals == match.AwayGoals) info.Points += 1;
                }

                if (match.AwayTeamId == team.Id)
                {
                    info.GoalsFor += match.AwayGoals;
                    info.GoalsAgainst += match.HomeGoals;
                    if (match.AwayGoals > match.HomeGoals) info.Points += 3;
                    else if (match.AwayGoals == match.HomeGoals) info.Points += 1;
                }
            }

            list.Add(info);
        }

        for (int i = 0; i < list.Count; i++)
        {
            for (int j = i + 1; j < list.Count; j++)
            {
                bool swap = false;
                if (type == 1 && list[j].GoalsFor > list[i].GoalsFor) swap = true;
                if (type == 2 && list[j].GoalsAgainst < list[i].GoalsAgainst) swap = true;
                if (type == 3 && list[j].Points > list[i].Points) swap = true;
                if (type == 4 && list[j].Points < list[i].Points) swap = true;

                if (swap)
                {
                    TeamInfo temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;
                }
            }
        }

        int max = count;
        if (max == 0 || max > list.Count)
            max = list.Count;

        for (int i = 0; i < max; i++)
        {
            if (type == 1) Console.WriteLine((i + 1) + ". " + list[i].Name + " — забито: " + list[i].GoalsFor);
            else if (type == 2) Console.WriteLine((i + 1) + ". " + list[i].Name + " — пропущено: " + list[i].GoalsAgainst);
            else if (type == 3) Console.WriteLine((i + 1) + ". " + list[i].Name + " — очки: " + list[i].Points);
            else if (type == 4) Console.WriteLine((i + 1) + ". " + list[i].Name + " — очки: " + list[i].Points);
            else Console.WriteLine(list[i].Name + " — разница голов: " + (list[i].GoalsFor - list[i].GoalsAgainst));
        }
    }

    // Показ матчей
    static void ShowMatches(int type, string teamName, DateTime date)
    {
        bool found = false;

        foreach (Match match in matches)
        {
            Team home = FindTeam(match.HomeTeamId);
            Team away = FindTeam(match.AwayTeamId);

            if (type == 1 && match.Date.Date != date.Date)
                continue;
            if (type == 2 && home.Name != teamName && away.Name != teamName)
                continue;

            Console.WriteLine(match.Id + ". " + match.Date.ToShortDateString() + " — " + home.Name + " " +
                match.HomeGoals + ":" + match.AwayGoals + " " + away.Name);
            found = true;
        }

        if (!found)
            Console.WriteLine("Матчи не найдены.");
    }

    // Показ голов по дате
    static void ShowGoalsByDate(DateTime date)
    {
        bool found = false;

        foreach (Goal goal in goals)
        {
            Match match = FindMatch(goal.MatchId);
            if (match != null && match.Date.Date == date.Date)
            {
                Player player = FindPlayer(goal.PlayerId);
                Console.WriteLine(player.Name);
                found = true;
            }
        }

        if (!found)
            Console.WriteLine("Голы за эту дату не найдены");
    }

    // Добавить матч
    static void AddOneMatch()
    {
        string homeName = ReadText("Команда-хозяин: ");
        string awayName = ReadText("Команда-гость: ");
        int homeGoals = ReadInt("Голы хозяев: ");
        int awayGoals = ReadInt("Голы гостей: ");
        DateTime date = ReadDate("Дата (например, 2026-10-27): ");

        Team home = FindTeam(homeName);
        Team away = FindTeam(awayName);

        if (home == null || away == null)
        {
            Console.WriteLine("Одна из команд не найдена");
            return;
        }

        if (home.Id == away.Id)
        {
            Console.WriteLine("Команда не может играть сама с собой");
            return;
        }

        foreach (Match oldMatch in matches)
        {
            if (oldMatch.HomeTeamId == home.Id && oldMatch.AwayTeamId == away.Id && oldMatch.Date.Date == date.Date)
            {
                Console.WriteLine("Такой матч уже есть");
                return;
            }
        }

        AddMatchData(home.Id, away.Id, homeGoals, awayGoals, date);
        Console.WriteLine("Матч добавлен");
    }

    // Изменение матча
    static void EditOneMatch()
    {
        int id = ReadInt("Введите ID матча: ");
        Match match = FindMatch(id);

        if (match == null)
        {
            Console.WriteLine("Матч не найден");
            return;
        }

        string homeName = ReadText("Новая команда-хозяин: ");
        string awayName = ReadText("Новая команда-гость: ");
        int homeGoals = ReadInt("Новый счёт хозяев: ");
        int awayGoals = ReadInt("Новый счёт гостей: ");
        DateTime date = ReadDate("Новая дата: ");

        Team home = FindTeam(homeName);
        Team away = FindTeam(awayName);

        if (home == null || away == null || home.Id == away.Id)
        {
            Console.WriteLine("Проверьте названия команд");
            return;
        }

        match.HomeTeamId = home.Id;
        match.AwayTeamId = away.Id;
        match.HomeGoals = homeGoals;
        match.AwayGoals = awayGoals;
        match.Date = date;

        goals.RemoveAll(delegate (Goal g) { return g.MatchId == match.Id; });
        AddRandomGoalScorers(match);
        Console.WriteLine("Матч изменён");
    }

    // Удалание матча
    static void DeleteOneMatch()
    {
        string homeName = ReadText("Команда-хозяин: ");
        string awayName = ReadText("Команда-гость: ");
        DateTime date = ReadDate("Дата матча: ");

        Team home = FindTeam(homeName);
        Team away = FindTeam(awayName);

        if (home == null || away == null)
        {
            Console.WriteLine("Команда не найдена");
            return;
        }

        Match match = null;
        foreach (Match item in matches)
        {
            if (item.HomeTeamId == home.Id && item.AwayTeamId == away.Id && item.Date.Date == date.Date)
            {
                match = item;
                break;
            }
        }

        if (match == null)
        {
            Console.WriteLine("Матч не найден");
            return;
        }

        Console.Write("Удалить матч? (y/n): ");
        if (Console.ReadLine().ToLower() == "y")
        {
            goals.RemoveAll(delegate (Goal g) { return g.MatchId == match.Id; });
            matches.Remove(match);
            Console.WriteLine("Матч удалён");
        }
        else
        {
            Console.WriteLine("Удаление отменено");
        }
    }

    // Добавить матч по дате
    static void AddMatchData(int homeId, int awayId, int homeGoals, int awayGoals, DateTime date)
    {
        Match match = new Match();
        match.Id = nextMatchId;
        nextMatchId++;
        match.HomeTeamId = homeId;
        match.AwayTeamId = awayId;
        match.HomeGoals = homeGoals;
        match.AwayGoals = awayGoals;
        match.Date = date.Date;
        matches.Add(match);
        AddRandomGoalScorers(match);
    }

    static void AddRandomGoalScorers(Match match)
    {
        for (int i = 0; i < match.HomeGoals; i++)
        {
            int playerId = GetRandomPlayerId(match.HomeTeamId);
            goals.Add(new Goal(nextGoalId, match.Id, playerId));
            nextGoalId++;
        }

        for (int i = 0; i < match.AwayGoals; i++)
        {
            int playerId = GetRandomPlayerId(match.AwayTeamId);
            goals.Add(new Goal(nextGoalId, match.Id, playerId));
            nextGoalId++;
        }
    }

    static int GetRandomPlayerId(int teamId)
    {
        List<Player> teamPlayers = new List<Player>();
        foreach (Player player in players)
        {
            if (player.TeamId == teamId)
                teamPlayers.Add(player);
        }
        return teamPlayers[random.Next(teamPlayers.Count)].Id;
    }

    static Team FindTeam(string name)
    {
        foreach (Team team in teams)
        {
            if (team.Name.ToLower() == name.ToLower())
                return team;
        }
        return null;
    }

    static Team FindTeam(int id)
    {
        foreach (Team team in teams)
            if (team.Id == id) return team;
        return null;
    }

    static Player FindPlayer(int id)
    {
        foreach (Player player in players)
            if (player.Id == id) return player;
        return null;
    }

    static Match FindMatch(int id)
    {
        foreach (Match match in matches)
            if (match.Id == id) return match;
        return null;
    }

    static int ReadInt(string message)
    {
        int value;
        Console.Write(message);
        while (!int.TryParse(Console.ReadLine(), out value) || value < 0)
            Console.Write("Введите целое число не меньше 0: ");
        return value;
    }

    static DateTime ReadDate(string message)
    {
        DateTime date;
        Console.Write(message);
        while (!DateTime.TryParse(Console.ReadLine(), out date))
            Console.Write("Неверная дата\nВведите ещё раз: ");
        return date.Date;
    }

    static string ReadText(string message)
    {
        Console.Write(message);
        return Console.ReadLine().Trim();
    }
}


