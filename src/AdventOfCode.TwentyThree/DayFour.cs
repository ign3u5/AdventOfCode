using AdventOfCode.Domain;

public class DayFour : IChallenge<int>
{
  public string ChallengeInputPath { get; } = DataProvider<DayFour>.GetChallengePath();

    public int RunTaskOne(string[] lines)
    {
        int totalScore = 0;
        foreach (string line in lines) {
            var scoreOfCurrentCard = 0;
            string[] cardData = line.Split(':')[1].Split('|');

            var winningNumbers = cardData[0].Split(' ')
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(int.Parse)
            .ToHashSet();

            var ourNumbers = cardData[1].Split(' ')
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(int.Parse);

            foreach(var number in ourNumbers) {
                if (winningNumbers.Contains(number)) {
                    if (scoreOfCurrentCard == 0) {
                        scoreOfCurrentCard = 1;
                    }
                    else {
                        scoreOfCurrentCard *= 2;
                    }
                }
            }

            totalScore += scoreOfCurrentCard;
        }

        return totalScore;
    }

    public int RunTaskTwo(string[] lines)
    {
        Dictionary<int, int> scratchCards = new();

        foreach (string line in lines) {

            string[] card = line.Split(':');

            int cardNo = int.Parse(card[0].Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Skip(1).First());

            UpdateScratchCards(cardNo);

            string[] cardData = card[1].Split('|');

            var winningNumbers = cardData[0].Split(' ')
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(int.Parse)
            .ToHashSet();

            var ourNumbers = cardData[1].Split(' ')
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(int.Parse);

            int count = 0;
            foreach(var number in ourNumbers) {
                if (winningNumbers.Contains(number)) {
                    count++;
                    scratchCards.TryGetValue(cardNo, out int scratchCardCount);
                    UpdateScratchCards(cardNo + count, scratchCardCount);
                }
            }
        }

        void UpdateScratchCards(int cardNo, int multiplier = 1) {
            if (scratchCards.TryGetValue(cardNo, out _)) {
                scratchCards[cardNo] += 1 * multiplier;
            }
            else {
                scratchCards[cardNo] = 1 * multiplier;
            }
        }

        return scratchCards.Sum(x => x.Value);
    }
}
