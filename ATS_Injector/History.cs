using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ATS_Injector
{
    public class History
    {
        private static readonly string _filePath = Path.Combine(PopOutApp.GetATSFolder(), "log_data.json");
        private static List<LogEntry> jobDescriptions;

        internal static void InitHistory()
        {
            History ab = new History();
            jobDescriptions = ab.LoadData();
        }

        public static bool NearMatchJD(string[] srcData)
        {
            bool returningBool = false;
            return false;
        }

        internal static void SaveData(string[] srcData)
        {
            string[] cleanedData = cleanDirtyData(srcData);
            try
            {
                if(cleanedData != null)
                {
                    jobDescriptions.Add(new LogEntry(cleanedData));
                    var json = JsonSerializer.Serialize(jobDescriptions);
                    File.WriteAllText(_filePath, json);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving: {ex.Message}");
            }
        }

        private static string[] cleanDirtyData(string[] srcData)
        {
            List<string> compileList = new List<string>();

            foreach (string entry in srcData)
            {
                if (string.IsNullOrEmpty(entry))
                    continue;

                // Split on newline variations
                string[] lines = entry.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.TrimEntries);

                foreach (string line in lines)
                {
                    if (string.IsNullOrEmpty(line))
                        continue;
                    // Remove unwanted characters
                    string cleaned = Regex.Replace(line, @"[^a-zA-Z0-9,\.\$\%\-\+\=\(\)\[\] ]", "");

                    // Only keep non-empty results
                    if (!string.IsNullOrEmpty(cleaned))
                    {
                        compileList.Add(cleaned);
                    }
                }
            }

            //if you have LESS than 5 entries ignore the whole thing
            if (compileList.Count < 5)
                return null;
            return compileList.ToArray();
        }

        public static List<LogEntry> GetHistroy()
        {
            return jobDescriptions;
        }



        private List<LogEntry> LoadData()
        {
            List<LogEntry> retData;// a = new List<LogEntry>();
            if (File.Exists(_filePath))
            {
                try
                {
                    var json = File.ReadAllText(_filePath);
                    retData = JsonSerializer.Deserialize< List<LogEntry>> (json);
                }
                catch
                {
                    retData = new List<LogEntry>();
                }
            }
            else
            {
                retData = new List<LogEntry>();
            }

            return retData;
        }
    }

    public class LogEntry
    {
        public DateTime Date { get; set; }
        public string[] Data { get; set; }

        public LogEntry(string[] data)
        {
            this.Date = DateTime.Today;
            this.Data = data;
        }
    }

    #region tolerance match maker
    public class TolerantMatchResult
    {
        public int Length { get; set; }
        public int Matches { get; set; }
        public int Gaps { get; set; }
        public int InputStartIndex { get; set; }
        public int CandidateStartIndex { get; set; }
        public string[] MatchedValues { get; set; }
    }

    public class ArrayMatcher
    {
        public static List<(string[] Candidate, TolerantMatchResult Match)>
            FindMatchesWithTolerance(string[] input, List<string[]> candidates, double tolerance = 0.05)
        {
            var results = new List<(string[], TolerantMatchResult)>();

            foreach (var candidate in candidates)
            {
                var match = FindBestTolerantMatch(input, candidate, tolerance);

                double inputPercent = (double)match.Matches / input.Length;
                double candidatePercent = (double)match.Matches / candidate.Length;

                if (inputPercent >= 0.5 || candidatePercent >= 0.5)
                {
                    results.Add((candidate, match));
                }
            }

            return results;
        }

        private static TolerantMatchResult FindBestTolerantMatch(string[] a, string[] b, double tolerance)
        {
            TolerantMatchResult best = new TolerantMatchResult();

            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    int ai = i;
                    int bj = j;

                    int matches = 0;
                    int gaps = 0;
                    int length = 0;

                    var tempMatch = new List<string>();

                    while (ai < a.Length && bj < b.Length)
                    {
                        length++;

                        if (a[ai] == b[bj])
                        {
                            matches++;
                            tempMatch.Add(a[ai]);
                        }
                        else
                        {
                            gaps++;
                        }

                        // Enforce tolerance dynamically
                        int allowedGaps = (int)Math.Ceiling(length * tolerance);

                        if (gaps > allowedGaps)
                            break;

                        ai++;
                        bj++;
                    }

                    if (matches > best.Matches)
                    {
                        best = new TolerantMatchResult
                        {
                            Length = length,
                            Matches = matches,
                            Gaps = gaps,
                            InputStartIndex = i,
                            CandidateStartIndex = j,
                            MatchedValues = tempMatch.ToArray()
                        };
                    }
                }
            }

            return best;
        }
    }

    #endregion tolerance match maker
}
