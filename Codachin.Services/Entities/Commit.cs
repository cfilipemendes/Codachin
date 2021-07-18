using System;
using System.IO;

namespace Codachin.Services.Dto
{
    public class Commit
    {
        public string Sha { get; set; }
        public string Author { get; set; }

        //  For this specific exercise working with DateTime instead of string would not improve anything.
        //  Since this is only used to show a specific date to the user I decided not to run into Datetime conversions.
        //  If we needed to compare Dates in the future its easy o do the conversion here. Creating a new DateTime prop that we will set when we set this one.
        public string Date { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return @$"
Commit: {Sha}
Author: {Author}
Date: {Date}
Message: {Message}";
        }
    }




}
