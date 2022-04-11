using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infobase.Assessment
{
    internal class Utilities
    {
        /// <summary>
        /// returns all possible combination of sequential groups with N number of characters/letters in a given word
        /// </summary>
        /// <param name="word">input word</param>
        /// <param name="char_num">number of characters in each sequential group</param>
        /// <returns>A list of all possible combinations of N letter sequences</returns>
        private List<string>? GetNLetterSequentialWords(string word, int char_num)
        {
            if (word.Length < char_num) return null;
            var seqWords = new List<string>();
            for (int i = 0; i <= word.Length - char_num; i++)
                seqWords.Add((word.Substring(i, char_num)).ToLower());
            return seqWords;
        }

        /// <summary>
        /// Write content (multiple lines) to a file corresponsing to given path
        /// </summary>
        /// <param name="lines">a string array, each item representing a line in the output file</param>
        /// <param name="filePath">complete file path : the location, file name and the extension</param>
        /// <returns>void</returns>
        /// <exception cref="Exception"></exception>
        public void WriteMultiLinesToFile(List<string> lines, string filePath)
        {
            try
            {
                File.WriteAllLines(filePath, lines);
            }
            catch (Exception err)
            {
                throw new Exception("An Error occured while writing to file. Details : \n" + err.Message);
            }
        }

        /// <summary>
        /// Takes in a group of words and returns all possible unique combinations of N letter sequential words
        /// </summary>
        /// <param name="words"></param>
        /// <param name="char_num"></param>
        /// <returns></returns>
        public IDictionary<string, string> GetUniqueNLetterSequences(List<string> words, int char_num)
        {
            IDictionary<string, List<string>> seqCombinations = new Dictionary<string, List<string>>();
            foreach (string word in words)
            {
                if (word.Length >= char_num)
                {
                    var seqWords = GetNLetterSequentialWords(word, char_num);
                    if (seqWords != null && seqWords.Count > 0)
                    {
                        foreach (string seq in seqWords)
                        {
                            if (seqCombinations.ContainsKey(seq))
                                seqCombinations[seq].Add(word);
                            else
                                seqCombinations.Add(seq, new List<string> { word });
                        }
                    }
                }
            }

            var uniqueSeqCombinations = seqCombinations.AsParallel()
                                        .Where(seqComb => seqComb.Value.Count == 1)
                                        .ToDictionary(seqComb => seqComb.Key, seqComb => seqComb.Value.First());

            return uniqueSeqCombinations;
        }
    }
}
