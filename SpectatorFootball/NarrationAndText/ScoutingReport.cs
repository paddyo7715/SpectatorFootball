using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.NarrationAndText
{
    class ScoutingReport
    {
        public static string ArmStrengthReport(int value, int lowerR, int upperR, bool bFirst, bool bLast)
        {
            string r = null;

            List<string> adj_Excellent = new List<string>() {"has rare arm strength", "has exceptional arm strength", "has excellent arm strength", "has a conon for an arm", "has a very strong arm", "has an excellent arm", "has a big arm", "has an exceptional arm" };
            List<string> adj_Good = new List<string>() {"has good arm strength","has better than average arm strength", "has a better than average arm", "has a strong arm", "has a good arm" };
            List<string> adj_Average = new List<string>() {"has average arm strength", "has an average arm", "has an unremarkable arm", "has an ordinary arm", "has ordinary arm strenth"  };
            List<string> adj_BelowAverage = new List<string>() {"has poor arm strength", "has below average arm strength", "has a below average arm","has subpar arm strength", "has a subpar arm","has substandard arm strength", "has a substandard arm" };
            List<string> adj_Weak = new List<string>() {"has weak arm strength","has very poor arm strength", "has a weak arm", "has a non professional arm" };

            int p = CommonUtils.PercentFromRange(value, lowerR, upperR);

            if (p > 90)
                r = adj_Excellent[CommonUtils.getRandomNum(1, adj_Excellent.Count()) -1];
            else if (p > 70)
                r = adj_Good[CommonUtils.getRandomNum(1, adj_Good.Count()) - 1];
            else if (p > 30)
                r = adj_Average[CommonUtils.getRandomNum(1, adj_Average.Count()) - 1];
            else if (p > 10)
                r = adj_BelowAverage[CommonUtils.getRandomNum(1, adj_BelowAverage.Count()) - 1];
            else
                r = adj_Weak[CommonUtils.getRandomNum(1, adj_Weak.Count()) - 1];

            if (bFirst)
                r = r + ", ";
            else if (bLast)
                r = " and " + r;
            else 
                r += ", ";

            return r;

        }

        public static string DecisionMakingReport(int value, int lowerR, int upperR, bool bFirst, bool bLast)
        {
            string r = null;

            List<string> adj_Excellent = new List<string>() { "makes incredible decisions with the ball", "rare decision-making talent","works extremely well thru his progressions" };
            List<string> adj_Good = new List<string>() { "pretty good decision-making", "has above average decision-making", "decision-making is a strength" };
            List<string> adj_Average = new List<string>() { "average decision making with the ball", "decision-making is average" };
            List<string> adj_BelowAverage = new List<string>() { "needs work with his decision-making", "decision-making is inconsistent", "will have to work on his decision-making" };
            List<string> adj_Weak = new List<string>() { "makes poor decisions with the ball", "a lack of patience with the ball", "forces thwows" };

            int p = CommonUtils.PercentFromRange(value, lowerR, upperR);

            if (p > 90)
                r = adj_Excellent[CommonUtils.getRandomNum(1, adj_Excellent.Count()) - 1];
            else if (p > 70)
                r = adj_Good[CommonUtils.getRandomNum(1, adj_Good.Count()) - 1];
            else if (p > 30)
                r = adj_Average[CommonUtils.getRandomNum(1, adj_Average.Count()) - 1];
            else if (p > 10)
                r = adj_BelowAverage[CommonUtils.getRandomNum(1, adj_BelowAverage.Count()) - 1];
            else
                r = adj_Weak[CommonUtils.getRandomNum(1, adj_Weak.Count()) - 1];

            if (bFirst)
                r = r + ", ";
            else if (bLast)
                r = " and " + r;
            else 
                r += ", ";


            return r;

        }

        public static string AccuracyReport(int value, int lowerR, int upperR, bool bFirst, bool bLast)
        {
            string r = null;

            List<string> adj_Excellent = new List<string>() { "has rare accuracy", "has exceptional accuracy", "has excellent accuracy", "possesses laser accuracy", "can thread a needle", "superior accuracy"};
            List<string> adj_Good = new List<string>() { "good accuracy", "better than average accuracy", "an accurate arm"};
            List<string> adj_Average = new List<string>() { "average accuracy", "has unremarkable accuracy", "has ordinary accuracy", "possesses decent accuracy" };
            List<string> adj_BelowAverage = new List<string>() { "poor accuracy", "has below average accuracy",  "subpar accuracy", "has substandard accuracy", };
            List<string> adj_Weak = new List<string>() { "has weak accuracy", "extremely poor accuracy", "has non professional accuracy" };

            int p = CommonUtils.PercentFromRange(value, lowerR, upperR);

            if (p > 90)
                r = adj_Excellent[CommonUtils.getRandomNum(1, adj_Excellent.Count()) - 1];
            else if (p > 70)
                r = adj_Good[CommonUtils.getRandomNum(1, adj_Good.Count()) - 1];
            else if (p > 30)
                r = adj_Average[CommonUtils.getRandomNum(1, adj_Average.Count()) - 1];
            else if (p > 10)
                r = adj_BelowAverage[CommonUtils.getRandomNum(1, adj_BelowAverage.Count()) - 1];
            else
                r = adj_Weak[CommonUtils.getRandomNum(1, adj_Weak.Count()) - 1];

            if (bFirst)
                r = r + ", ";
            else if (bLast)
                r = " and " + r;
            else 
                r += ", ";

            return r;

        }
        public static string PowerRunningReport(int value, int lowerR, int upperR, bool bFirst, bool bLast)
        {
            string r = null;

            List<string> adj_Excellent = new List<string>() { "a rare powerful runner", "an exceptional powerful runner", "a very powerful runner", "runs with rare power","runs with exceptional power","has ability to run over defenders" };
            List<string> adj_Good = new List<string>() { "a good power runner", "a better than average power runner", "a strong power runner", "known for power running" };
            List<string> adj_Average = new List<string>() { "not considered a power runner" };
            List<string> adj_BelowAverage = new List<string>() { "not considered a power runner" };
            List<string> adj_Weak = new List<string>() { "not considered a power runner" };

            int p = CommonUtils.PercentFromRange(value, lowerR, upperR);

            if (p > 90)
                r = adj_Excellent[CommonUtils.getRandomNum(1, adj_Excellent.Count()) - 1];
            else if (p > 70)
                r = adj_Good[CommonUtils.getRandomNum(1, adj_Good.Count()) - 1];
            else if (p > 30)
                r = adj_Average[CommonUtils.getRandomNum(1, adj_Average.Count()) - 1];
            else if (p > 10)
                r = adj_BelowAverage[CommonUtils.getRandomNum(1, adj_BelowAverage.Count()) - 1];
            else
                r = adj_Weak[CommonUtils.getRandomNum(1, adj_Weak.Count()) - 1];

            if (bFirst)
                r = r + ", ";
            else if (bLast)
                r = " and " + r;
            else 
                r += ", ";

            return r;

        }

        public static string SpeedReport(int value, int lowerR, int upperR, bool bFirst, bool bLast)
        {
            string r = null;

            List<string> adj_Excellent = new List<string>() { "a speed demon", "has exceptional speed", "has elite speed", "extrememly fast runner", "runs like the wind", "should be one of the fastest players on the field" };
            List<string> adj_Good = new List<string>() { "possesses good speed", "better than average speed", "a fast runner", "known for speed" };
            List<string> adj_Average = new List<string>() { "has average speed", "possesses average speed", "ordinary speed", "run of the mill speed" };
            List<string> adj_BelowAverage = new List<string>() { "does not possess good or even average speed", "below average speed", "not a speed demon", "slower than average for his position" };
            List<string> adj_Weak = new List<string>() { "a very slow runner", "not a fast runner at all", "does not possess speed at all" };

            int p = CommonUtils.PercentFromRange(value, lowerR, upperR);

            if (p > 90)
                r = adj_Excellent[CommonUtils.getRandomNum(1, adj_Excellent.Count()) - 1];
            else if (p > 70)
                r = adj_Good[CommonUtils.getRandomNum(1, adj_Good.Count()) - 1];
            else if (p > 30)
                r = adj_Average[CommonUtils.getRandomNum(1, adj_Average.Count()) - 1];
            else if (p > 10)
                r = adj_BelowAverage[CommonUtils.getRandomNum(1, adj_BelowAverage.Count()) - 1];
            else
                r = adj_Weak[CommonUtils.getRandomNum(1, adj_Weak.Count()) - 1];

            if (bFirst)
                r = r + ", ";
            else if (bLast)
                r = " and " + r;
            else 
                r += ", ";

            return r;

        }

        public static string AgilityReport(int value, int lowerR, int upperR, bool bFirst, bool bLast)
        {
            string r = null;

            List<string> adj_Excellent = new List<string>() { "possesses great agility", "has exceptional agility", "has elite agility", "extrememly agile runner", "possesses elite agility" };
            List<string> adj_Good = new List<string>() { "possesses good agility", "better than average agility", "an agile runner", "known for his agility" };
            List<string> adj_Average = new List<string>() { "has average agility", "possesses average agility", "ordinary agility", "run of the mill agility" };
            List<string> adj_BelowAverage = new List<string>() { "does not possess good or even average agility", "below average agility", "not an agile runner", "less than average agility for his position" };
            List<string> adj_Weak = new List<string>() { "very poor agility", "not an agile runner at all", "does not possess agility at all" };

            int p = CommonUtils.PercentFromRange(value, lowerR, upperR);

            if (p > 90)
                r = adj_Excellent[CommonUtils.getRandomNum(1, adj_Excellent.Count()) - 1];
            else if (p > 70)
                r = adj_Good[CommonUtils.getRandomNum(1, adj_Good.Count()) - 1];
            else if (p > 30)
                r = adj_Average[CommonUtils.getRandomNum(1, adj_Average.Count()) - 1];
            else if (p > 10)
                r = adj_BelowAverage[CommonUtils.getRandomNum(1, adj_BelowAverage.Count()) - 1];
            else
                r = adj_Weak[CommonUtils.getRandomNum(1, adj_Weak.Count()) - 1];

            if (bFirst)
                r = r + ", ";
            else if (bLast)
                r = " and " + r;
            else 
                r += ", ";

            return r;

        }

        public static string HandsReport(int value, int lowerR, int upperR, bool bFirst, bool bLast)
        {
            string r = null;

            List<string> adj_Excellent = new List<string>() { "possesses great hands", "exceptional hands", "has elite catching ability", "extrememly good hands", "possesses elite hands" };
            List<string> adj_Good = new List<string>() { "possesses good hands", "better than average hands", "good at catching the ball", "known for his hands" };
            List<string> adj_Average = new List<string>() { "has average hands", "possesses average hands", "has ordinary hands", "run of the mill hands" };
            List<string> adj_BelowAverage = new List<string>() { "does not possess good or even average ball catching ability", "has below average hands", "does not have even average hands", "less than average hands for his position" };
            List<string> adj_Weak = new List<string>() { "very poor hands", "does not have good hands at all", "does not possess much ability to catch the ball" };

            int p = CommonUtils.PercentFromRange(value, lowerR, upperR);

            if (p > 90)
                r = adj_Excellent[CommonUtils.getRandomNum(1, adj_Excellent.Count()) - 1];
            else if (p > 70)
                r = adj_Good[CommonUtils.getRandomNum(1, adj_Good.Count()) - 1];
            else if (p > 30)
                r = adj_Average[CommonUtils.getRandomNum(1, adj_Average.Count()) - 1];
            else if (p > 10)
                r = adj_BelowAverage[CommonUtils.getRandomNum(1, adj_BelowAverage.Count()) - 1];
            else
                r = adj_Weak[CommonUtils.getRandomNum(1, adj_Weak.Count()) - 1];

            if (bFirst)
                r = r + ", ";
            else if (bLast)
                r = " and " + r;
            else 
                r += ", ";

            return r;

        }

        public static string PassBlockingReport(int value, int lowerR, int upperR, bool bFirst, bool bLast)
        {
            string r = null;

            List<string> adj_Excellent = new List<string>() { "possesses great pass blocking skill", "an exceptional pass blocker", "has elite pass blocking ability", "extrememly good pass blocker", "possesses elite pass blocking ability" };
            List<string> adj_Good = new List<string>() { "possesses good pass blocking skill", "better than average pass blocker", "good at blocking pass rushers"};
            List<string> adj_Average = new List<string>() { "is an average pass blocker", "possesses average pass blocking ability", "ordinary pass blocker", "run of the mill pass blocker" };
            List<string> adj_BelowAverage = new List<string>() { "does not possess good or even average pass blocking ability", "below average pass blocker", "does not have even average pass blocking ability", "less than average pass blocker" };
            List<string> adj_Weak = new List<string>() { "very poor pass blocker", "does not have good pass blocking ability at all", "does not possess much ability to block pass rushers" };

            int p = CommonUtils.PercentFromRange(value, lowerR, upperR);

            if (p > 90)
                r = adj_Excellent[CommonUtils.getRandomNum(1, adj_Excellent.Count()) - 1];
            else if (p > 70)
                r = adj_Good[CommonUtils.getRandomNum(1, adj_Good.Count()) - 1];
            else if (p > 30)
                r = adj_Average[CommonUtils.getRandomNum(1, adj_Average.Count()) - 1];
            else if (p > 10)
                r = adj_BelowAverage[CommonUtils.getRandomNum(1, adj_BelowAverage.Count()) - 1];
            else
                r = adj_Weak[CommonUtils.getRandomNum(1, adj_Weak.Count()) - 1];

            if (bFirst)
                r = r + ", ";
            else if (bLast)
                r = " and " + r;
            else 
                r += ", ";

            return r;

        }

        public static string RunBlockingReport(int value, int lowerR, int upperR, bool bFirst, bool bLast)
        {
            string r = null;

            List<string> adj_Excellent = new List<string>() { "possesses great run blocking skill", "exceptional run blocker", "has elite run blocking ability", "extrememly good run blocker", "possesses elite run blocking ability" };
            List<string> adj_Good = new List<string>() { "possesses good run blocking skill", "better than average run blocker", "good at blocking run rushers" };
            List<string> adj_Average = new List<string>() { "is an average run blocker", "possesses average run blocking ability", "ordinary run blocker", "run of the mill run blocker" };
            List<string> adj_BelowAverage = new List<string>() { "does not possess good or even average ball run blocking ability", "below average run blocker", "does not have even average run blocking ability", "less than average run blocker" };
            List<string> adj_Weak = new List<string>() { "very poor run blocker", "does not have good run blocking ability at all", "does not possess much ability to block run rushers" };

            int p = CommonUtils.PercentFromRange(value, lowerR, upperR);

            if (p > 90)
                r = adj_Excellent[CommonUtils.getRandomNum(1, adj_Excellent.Count()) - 1];
            else if (p > 70)
                r = adj_Good[CommonUtils.getRandomNum(1, adj_Good.Count()) - 1];
            else if (p > 30)
                r = adj_Average[CommonUtils.getRandomNum(1, adj_Average.Count()) - 1];
            else if (p > 10)
                r = adj_BelowAverage[CommonUtils.getRandomNum(1, adj_BelowAverage.Count()) - 1];
            else
                r = adj_Weak[CommonUtils.getRandomNum(1, adj_Weak.Count()) - 1];

            if (bFirst)
                r = r + ", ";
            else if (bLast)
                r = " and " + r;
            else 
                r += ", ";

            return r;

        }

        public static string PassRushingReport(int value, int lowerR, int upperR, bool bFirst, bool bLast)
        {
            string r = null;

            List<string> adj_Excellent = new List<string>() { "possesses great pass rushing skill", "exceptional pass rusher", "has elite pass rushing ability", "extrememly good pass rusher", "possesses elite pass rushing ability", "can really get to the QB" };
            List<string> adj_Good = new List<string>() { "possesses good pass rushing skill", "better than average pass rusher", "good at rushing the passer" };
            List<string> adj_Average = new List<string>() { "is an average pass rusher", "possesses average pass rushing ability", "ordinary pass rusher", "run of the mill pass rusher" };
            List<string> adj_BelowAverage = new List<string>() { "does not possess good or even average pass rushing ability", "below average pass rusher", "does not have even average pass rushing ability", "less than average pass rusher" };
            List<string> adj_Weak = new List<string>() { "very poor pass rusher", "does not have good pass rushing ability at all", "does not possess much ability to rush the passer" };

            int p = CommonUtils.PercentFromRange(value, lowerR, upperR);

            if (p > 90)
                r = adj_Excellent[CommonUtils.getRandomNum(1, adj_Excellent.Count()) - 1];
            else if (p > 70)
                r = adj_Good[CommonUtils.getRandomNum(1, adj_Good.Count()) - 1];
            else if (p > 30)
                r = adj_Average[CommonUtils.getRandomNum(1, adj_Average.Count()) - 1];
            else if (p > 10)
                r = adj_BelowAverage[CommonUtils.getRandomNum(1, adj_BelowAverage.Count()) - 1];
            else
                r = adj_Weak[CommonUtils.getRandomNum(1, adj_Weak.Count()) - 1];

            if (bFirst)
                r = r + ", ";
            else if (bLast)
                r = " and " + r;
            else 
                r += ", ";

            return r;

        }

        public static string RunStoppingReport(int value, int lowerR, int upperR, bool bFirst, bool bLast)
        {
            string r = null;

            List<string> adj_Excellent = new List<string>() { "possesses great run stopping skill", "exceptional run stopper", "has elite run stopping ability", "extrememly good run stopper", "possesses elite run stopping ability", "can really stuff the run" };
            List<string> adj_Good = new List<string>() { "possesses good run stopping skill", "better than average run stopper", "good at stopping the run" };
            List<string> adj_Average = new List<string>() { "is an average run stopper", "possesses average run stopping ability", "ordinary run stopper", "run of the mill run stopper" };
            List<string> adj_BelowAverage = new List<string>() { "does not possess good or even average run stopping ability", "below average run stopper", "does not have even average run stopping ability", "less than average run stopper" };
            List<string> adj_Weak = new List<string>() { "very poor run stopper", "does not have good run stopping ability at all", "does not possess much ability to stop the run" };

            int p = CommonUtils.PercentFromRange(value, lowerR, upperR);

            if (p > 90)
                r = adj_Excellent[CommonUtils.getRandomNum(1, adj_Excellent.Count()) - 1];
            else if (p > 70)
                r = adj_Good[CommonUtils.getRandomNum(1, adj_Good.Count()) - 1];
            else if (p > 30)
                r = adj_Average[CommonUtils.getRandomNum(1, adj_Average.Count()) - 1];
            else if (p > 10)
                r = adj_BelowAverage[CommonUtils.getRandomNum(1, adj_BelowAverage.Count()) - 1];
            else
                r = adj_Weak[CommonUtils.getRandomNum(1, adj_Weak.Count()) - 1];

            if (bFirst)
                r = r + ", ";
            else if (bLast)
                r = " and " + r;
            else 
                r += ", ";

            return r;

        }

        public static string TackleReport(int value, int lowerR, int upperR, bool bFirst, bool bLast)
        {
            string r = null;

            List<string> adj_Excellent = new List<string>() { "possesses great tackling skill", "exceptional tackler", "has elite tackling ability", "extrememly good tackler", "possesses elite tackling ability", "can really bring ball carriers down" };
            List<string> adj_Good = new List<string>() { "possesses good tackloing skill", "better than average tackler", "good at tackling" };
            List<string> adj_Average = new List<string>() { "is an average tackler", "possesses average tackling ability", "ordinary tackler", "run of the mill tackler" };
            List<string> adj_BelowAverage = new List<string>() { "does not possess good or even average tackling ability", "below average tackler", "has below average tackling ability", "less than average tackler" };
            List<string> adj_Weak = new List<string>() { "very poor tackler", "does not have good tackling ability at all", "does not possess much ability to tackle ball carriers" };

            int p = CommonUtils.PercentFromRange(value, lowerR, upperR);

            if (p > 90)
                r = adj_Excellent[CommonUtils.getRandomNum(1, adj_Excellent.Count()) - 1];
            else if (p > 70)
                r = adj_Good[CommonUtils.getRandomNum(1, adj_Good.Count()) - 1];
            else if (p > 30)
                r = adj_Average[CommonUtils.getRandomNum(1, adj_Average.Count()) - 1];
            else if (p > 10)
                r = adj_BelowAverage[CommonUtils.getRandomNum(1, adj_BelowAverage.Count()) - 1];
            else
                r = adj_Weak[CommonUtils.getRandomNum(1, adj_Weak.Count()) - 1];

            if (bFirst)
                r = r + ", ";
            else if (bLast)
                r = " and " + r;
            else 
                r += ", ";

            return r;

        }

        public static string LegPowerReport(int value, int lowerR, int upperR, bool bFirst, bool bLast)
        {
            string r = null;

            List<string> adj_Excellent = new List<string>() { "extremely strong leg", "booming leg strength", "elite leg strength", "extrememly strong leg strength"};
            List<string> adj_Good = new List<string>() { "good leg strength", "strong leg", "above average leg strength" };
            List<string> adj_Average = new List<string>() { "average leg strength", "possesses average leg strength", "ordinary leg strength", "run of the mill leg strength" };
            List<string> adj_BelowAverage = new List<string>() { "does not possess a strong kicking leg", "below average leg strength", "has below average leg strength", "less than average leg strength" };
            List<string> adj_Weak = new List<string>() { "very poor leg strength", "does not have good leg strength at all", "does not possess much leg strength" };

            int p = CommonUtils.PercentFromRange(value, lowerR, upperR);

            if (p > 90)
                r = adj_Excellent[CommonUtils.getRandomNum(1, adj_Excellent.Count()) - 1];
            else if (p > 70)
                r = adj_Good[CommonUtils.getRandomNum(1, adj_Good.Count()) - 1];
            else if (p > 30)
                r = adj_Average[CommonUtils.getRandomNum(1, adj_Average.Count()) - 1];
            else if (p > 10)
                r = adj_BelowAverage[CommonUtils.getRandomNum(1, adj_BelowAverage.Count()) - 1];
            else
                r = adj_Weak[CommonUtils.getRandomNum(1, adj_Weak.Count()) - 1];

            if (bFirst)
                r = r + ", ";
            else if (bLast)
                r = " and " + r;
            else 
                r += ", ";

            return r;

        }

        public static string LegAccuracyKReport(int value, int lowerR, int upperR, bool bFirst, bool bLast)
        {
            string r = null;

            List<string> adj_Excellent = new List<string>() { "extremely accurate kicker", "laser accurate kicker", "elite leg accuracy", "extrememly accurate kicker" };
            List<string> adj_Good = new List<string>() { "good leg accuracy", "accurate kicker", "above average kicking accuracy" };
            List<string> adj_Average = new List<string>() { "average leg accuracy", "possesses average leg accuracy", "ordinary leg accuracy", "run of the mill leg accuracy" };
            List<string> adj_BelowAverage = new List<string>() { "does not possess an accurate kicking leg", "below average leg accuracy", "has below average leg accuracy", "less than average leg accuracy" };
            List<string> adj_Weak = new List<string>() { "very poor leg accuracy", "does not have good leg accuracy at all", "does not possess much kicking accuracy" };

            int p = CommonUtils.PercentFromRange(value, lowerR, upperR);

            if (p > 90)
                r = adj_Excellent[CommonUtils.getRandomNum(1, adj_Excellent.Count()) - 1];
            else if (p > 70)
                r = adj_Good[CommonUtils.getRandomNum(1, adj_Good.Count()) - 1];
            else if (p > 30)
                r = adj_Average[CommonUtils.getRandomNum(1, adj_Average.Count()) - 1];
            else if (p > 10)
                r = adj_BelowAverage[CommonUtils.getRandomNum(1, adj_BelowAverage.Count()) - 1];
            else
                r = adj_Weak[CommonUtils.getRandomNum(1, adj_Weak.Count()) - 1];

            if (bFirst)
                r = r + ", ";
            else if (bLast)
                r = " and " + r;
            else 
                r += ", ";

            return r;

        }

        public static string LegAccuracyPReport(int value, int lowerR, int upperR, bool bFirst, bool bLast)
        {
            string r = null;

            List<string> adj_Excellent = new List<string>() { "extremely good at coffin corner punts", "Consistantly pins teams inside the 5 yard line", "elite coffin corner kick accuracy", "extrememly good at coffin corner kicks" };
            List<string> adj_Good = new List<string>() { "good at coffin corner punts", "accurate with coffin corner punts", "above average with coffin corner punts" };
            List<string> adj_Average = new List<string>() { "average ability with coffin corner punts", "possesses average ability to pin team inside the 5 yard line with punts", "ordinary coffin corner punts", "run of the mill coffin corner accuracy" };
            List<string> adj_BelowAverage = new List<string>() { "does not possess average coffin corner punt ability", "below average coffin corner accuracy", "has below average coffin corner punts", "less than average ability to pin teams inside 5 yard line" };
            List<string> adj_Weak = new List<string>() { "very poor coffin corner punting ability", "does not have good coffin corner punt ability at all", "does not possess much coffin corner accuracy" };

            int p = CommonUtils.PercentFromRange(value, lowerR, upperR);

            if (p > 90)
                r = adj_Excellent[CommonUtils.getRandomNum(1, adj_Excellent.Count()) - 1];
            else if (p > 70)
                r = adj_Good[CommonUtils.getRandomNum(1, adj_Good.Count()) - 1];
            else if (p > 30)
                r = adj_Average[CommonUtils.getRandomNum(1, adj_Average.Count()) - 1];
            else if (p > 10)
                r = adj_BelowAverage[CommonUtils.getRandomNum(1, adj_BelowAverage.Count()) - 1];
            else
                r = adj_Weak[CommonUtils.getRandomNum(1, adj_Weak.Count()) - 1];

            if (bFirst)
                r = r + ", ";
            else if (bLast)
                r = " and " + r;
            else 
                r += ", ";

            return r;

        }



    }
}
