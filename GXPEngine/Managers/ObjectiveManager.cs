using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public enum ObjectiveType
    {
        ScoreBelow,
        ScoreEqual,
        ScoreAbove,

        CollectTreats,
        DoNotColelctTreats,

        DoNotStop,
        StopInTime,

        DoNotExit,
        ExitInTime,

        TouchBumpers
    }

    public enum ObjectiveState
    {
        failed,
        pending,
        completed
    }
    public enum ScoreType
    {
        Score,
        TreatsCount,
        PlayerSpeed,
        BumperCount
    }

    public static class ObjectiveManager
    {

        public static ObjectiveType objectiveType;
        public static float parameter;

        public static ScoreType scoreType;
        private static float score;
        private static float timer;
        private static bool completed = false;

        private static float stopThreshold = 50;

        public static void setObjective(ObjectiveType PobjectiveType, float Pparameter)
        {
            objectiveType = PobjectiveType;
            parameter = Pparameter;

            switch (objectiveType)
            {
                case ObjectiveType.ScoreBelow:
                    scoreType = ScoreType.Score;
                    break;
                case ObjectiveType.ScoreEqual:
                    scoreType = ScoreType.Score;
                    break;
                case ObjectiveType.ScoreAbove:
                    scoreType = ScoreType.Score;
                    break;

                case ObjectiveType.CollectTreats:
                    scoreType = ScoreType.TreatsCount;
                    break;
                case ObjectiveType.DoNotColelctTreats:
                    scoreType = ScoreType.TreatsCount;
                    break;

                case ObjectiveType.DoNotStop:
                    scoreType = ScoreType.PlayerSpeed;
                    break;
                case ObjectiveType.StopInTime:
                    scoreType = ScoreType.PlayerSpeed;
                    break;

                case ObjectiveType.TouchBumpers:
                    scoreType = ScoreType.BumperCount;
                    break;
            }
        }
        public static void Update()
        {
            timer += Time.DeltaSeconds;
        }

        public static string GetObjectiveText()
        {
            var a = GetObjectiveState();
            if (a != ObjectiveState.pending)
                return "Level " + a;
            switch (objectiveType)
            {
                case ObjectiveType.ScoreBelow:
                    return $"Keep your score below {(int)parameter}, score: {(int)score} / {(int)parameter}";
                case ObjectiveType.ScoreEqual:
                    return $"Exit with a score of {(int)parameter}, score: {(int)score} / {(int)parameter}";
                case ObjectiveType.ScoreAbove:
                    return $"Reach a score above {(int)parameter}, score: {(int)score} / {(int)parameter}";

                case ObjectiveType.CollectTreats:
                    return $"Collect {(int)parameter} treats, {(int)score} / {(int)parameter}";
                case ObjectiveType.DoNotColelctTreats:
                    return $"Exit without colleting any treats";


                case ObjectiveType.DoNotExit:
                    return $"Survive {(int)parameter} seconds without exiting, {(int)(parameter - timer)}";
                case ObjectiveType.ExitInTime:
                    return $"Exit the level within {(int)parameter} seconds, {(int)(parameter - timer)}";

                case ObjectiveType.DoNotStop:
                    return $"Survive {(int)parameter} without stopping, {(int)(parameter - timer)}";
                case ObjectiveType.StopInTime:
                    return $"Stop the ball from moving within {(int)parameter} seconds, {(int)(parameter - timer)}";

                case ObjectiveType.TouchBumpers:
                    return $"Bounce off of {(int)parameter} bumpers, {(int)score} / {(int)parameter}";

                default:
                    return "";
            }
        }

        public static void UpdateScore(ScoreType inputType, float amount = 1)
        {
            if (inputType != scoreType)
                return;
            switch (inputType)
            {
                case ScoreType.Score:
                    score += amount;
                    break;
                case ScoreType.TreatsCount:
                    score += amount;
                    break;
                case ScoreType.PlayerSpeed:
                    if (amount < stopThreshold)
                        score += Time.DeltaSeconds;
                    else
                        score = 0;
                    break;
                case ScoreType.BumperCount:
                    score += amount;
                    break;
            }
        }

        public static ObjectiveState GetObjectiveState()
        {
            switch (objectiveType)
            {
                case ObjectiveType.ScoreBelow:
                    if (score > parameter)
                        return ObjectiveState.failed;
                    if (completed)
                        return ObjectiveState.completed;
                    return ObjectiveState.pending;
                case ObjectiveType.ScoreEqual:
                    if (completed)
                        if (score != parameter)
                            return ObjectiveState.failed;
                        else
                            return ObjectiveState.completed;
                    return ObjectiveState.pending;
                case ObjectiveType.ScoreAbove:
                    if (completed)
                        return ObjectiveState.failed;
                    if (score < parameter)
                        return ObjectiveState.pending;
                    else
                        return ObjectiveState.completed;
                case ObjectiveType.CollectTreats:
                    if (completed)
                        if (score < parameter)
                            return ObjectiveState.failed;
                        else
                            return ObjectiveState.completed;
                    return ObjectiveState.pending;
                case ObjectiveType.DoNotColelctTreats:
                    if (completed)
                        if (score > 0)
                            return ObjectiveState.failed;
                        else
                            return ObjectiveState.completed;
                    return ObjectiveState.pending;
                case ObjectiveType.DoNotExit:
                    if (completed)
                        return ObjectiveState.failed;
                    if (timer < parameter)
                        return ObjectiveState.pending;
                    else
                        return ObjectiveState.completed;
                case ObjectiveType.ExitInTime:
                    if (completed)
                        return ObjectiveState.completed;
                    if (timer < parameter)
                        return ObjectiveState.pending;
                    else
                        return ObjectiveState.failed;
                case ObjectiveType.DoNotStop:
                    if(timer > parameter)
                        return ObjectiveState.completed;
                    if(score > 1)
                        return ObjectiveState.failed;
                    return ObjectiveState.pending;
                case ObjectiveType.StopInTime:
                    if (timer > parameter)
                        return ObjectiveState.failed;
                    if (score > 1)
                        return ObjectiveState.completed;
                    return ObjectiveState.pending;
                case ObjectiveType.TouchBumpers:
                    if (completed)
                        if (score < parameter)
                            return ObjectiveState.failed;
                        else
                            return ObjectiveState.completed;
                    return ObjectiveState.pending;
                default:
                    throw new Exception("no objective set");
            }
        }

        public static void Complete()
        {
            completed = true;
        }
    }
}
