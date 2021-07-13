using System.IO;

namespace Codachin.Services.dto
{
    public class Commit
    {
        public string SHA { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }
        public string Message { get; set; }


        public static Commit Parse(string commitInfo)
        {
            Commit currentCommit = new Commit();
            using (var strReader = new StringReader(commitInfo))
            {
                do
                {
                    var line = strReader.ReadLine();

                    if (line.StartsWith("commit"))
                    {
                        currentCommit.SHA = line.Split(' ')[1];
                    }
                    else if (line.StartsWith("Author:"))
                    {
                        currentCommit.Author = line.Split(": ")[1];
                    }
                    else if (line.StartsWith("Date:"))
                    {
                        currentCommit.Date = line.Split(new[] { ':' }, 2)[1].TrimStart();
                    }
                    else
                    {
                        currentCommit.Message += line;
                    }
                }
                while (strReader.Peek() != -1);
            }

            return currentCommit;

        }

        public override string ToString()
        {
            return @$"
Commit: {SHA}
Author: {Author}
Date: {Date}
Message: {Message}";
        }
    }




}
