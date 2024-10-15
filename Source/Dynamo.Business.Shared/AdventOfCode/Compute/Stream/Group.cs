using System.Collections.Generic;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Stream
{
    public class Group
    {
        public List<Group> Groups { get; set; }

        public int Level { get; set; }
        public bool Closed { get; set; }
        public int NonCanceledCharacterCount { get; set; } = 0;

        public Group(int level)
        {
            Groups = new List<Group>();
            Level = level;
            Closed = false;
        }

        public void Add()
        {
            Groups.Add(new Group(Level + 1));
        }

        public int GetNumberOfGroups()
        {
            var numberOfGroups = 1; // It's set to 1 for the current group
            foreach (var group in Groups)
            {
                numberOfGroups += group.GetNumberOfGroups();
            }
            return numberOfGroups;
        }

        public int GetGroupScore()
        {
            var groupScore = Level;
            foreach (var group in Groups)
            {
                groupScore += group.GetGroupScore();
            }
            return groupScore;
        }

        public int GetNoncanceledCharacters()
        {
            var nonCanceledCharacters = NonCanceledCharacterCount;
            foreach (var group in Groups)
            {
                nonCanceledCharacters += group.GetNoncanceledCharacters();
            }
            return nonCanceledCharacters;
        }

        public void ProcessStream(Stream stream)
        {
            var isGarbage = false;
            var isEscaped = false;
            while (stream.HasNextValue())
            {
                var nextChar = stream.GetNextChar();
                if (isEscaped)
                {
                    isEscaped = false;
                }
                else
                {
                    if (nextChar == '!')
                    {
                        // Skip the next char since we see the escape char
                        isEscaped = true;
                    }
                    else
                    {
                        if (isGarbage == false)
                        {
                            if (nextChar == '{')
                            {
                                var newGroup = new Group(Level + 1);
                                Groups.Add(newGroup);
                                newGroup.ProcessStream(stream);
                            }
                            if (nextChar == '}')
                            {
                                Closed = true;
                                return;
                            }
                            if (nextChar == '<')
                            {
                                isGarbage = true;
                            }
                        }
                        else
                        {
                            if (isGarbage && nextChar == '>')
                            {
                                isGarbage = false;
                            }
                            else
                            {
                                NonCanceledCharacterCount++;
                            }
                        }
                    }
                }
            }
            return;
        }
    }
}
