using DocumentVerification.Enums;
using DocumentVerification.Interfaces;
using DocumentVerification.Models;
using DocumentVerification.Utils;
using Google.Cloud.Vision.V1;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace DocumentVerification.Services
{
    public class DocumentService : IDocumentService
    {
        public string BaseDir = "D:\\Files";

        public Document FetchDataFromPhoto(IFormFile file)
        {
            var document = new Document();
            var fileName = file.FileName;
            var stream = file.OpenReadStream();
            var path = Path.Combine(BaseDir, fileName);

            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }

            var image = Image.FromFile(path);
            var result = ImageAnnotatorClient.Create().DetectDocumentText(image);

            if (string.IsNullOrWhiteSpace(result.Text))
            {
                return null;
            }

            var text = result.Text.Replace("\n", " ").Trim();

            if (text.ContainsIgnoreCase("Driver") && text.ContainsIgnoreCase("Licence"))
            {
                document.DocumentType = DocumentType.DriverLicence;
                if (text.ContainsIgnoreCase("New") && text.ContainsIgnoreCase("South") && text.ContainsIgnoreCase("Wales") && text.ContainsIgnoreCase("Australia"))
                {
                    document.State = State.NSW;
                    document.CardNumber = Regex.Match(text, @"\d(\s\d{3}){3}").Value?.Replace(" ", null);
                    document.LicenceNumber = Regex.Match(text, @"(?<= Licence No.).+(?=Licence Class)").Value?.Trim();
                    var dates = Regex.Matches(text, @"\d{2}\s[A-Z]{3}\s\d{4}");
                    if (dates.Count == 2)
                    {
                        DateTime.TryParse(dates[0].Value, out DateTime dateOfBirth);
                        DateTime.TryParse(dates[1].Value, out DateTime expiryDate);
                        document.DateOfBirth = dateOfBirth;
                        document.ExpiryDate = expiryDate;
                    }
                    document.Name = Regex.Match(text, @"(?<= Australia).+?(?=(Card|\d))").Value?.Trim();
                    document.Address = Regex.Match(text, @"(?<=(\s\d{3}){3}).+(?=Licence No)").Value?.Trim();

                }
                else if (text.ContainsIgnoreCase("Victoria") && text.ContainsIgnoreCase("Australia"))
                {

                }
                //else if (text.ContainsIgnoreCase("Victoria") && text.ContainsIgnoreCase("Australia"))
                //{

                //}
                //else if (text.ContainsIgnoreCase("Victoria") && text.ContainsIgnoreCase("Australia"))
                //{

                //}
                //else if (text.ContainsIgnoreCase("Victoria") && text.ContainsIgnoreCase("Australia"))
                //{

                //}
                //else if (text.ContainsIgnoreCase("Victoria") && text.ContainsIgnoreCase("Australia"))
                //{

                //}
                //else if (text.ContainsIgnoreCase("Victoria") && text.ContainsIgnoreCase("Australia"))
                //{

                //}
                //else if (text.ContainsIgnoreCase("Victoria") && text.ContainsIgnoreCase("Australia"))
                //{

                //}

            }
            else if (1 == 1)
            {
                document.DocumentType = DocumentType.Passport;
            }


            //string OCRText = result.Text;
            //string[] lines = OCRText.Split('\n');
            //int count = 0;
            //int currentInterestStart = 0;
            //int historicalInterestStart = 0;
            //int planStart = 0;
            //NZTitleSearchRelated OCRResult = new NZTitleSearchRelated();
            //foreach (string line in lines)
            //{
            //    if (line.ToLower().Contains("plan") && planStart == 0)
            //    {
            //        planStart = count;
            //    }
            //    // find current interest start
            //    if (line.ToLower().Contains("ent interests") || line.ToLower().Contains("en interests"))
            //    {
            //        currentInterestStart = count;
            //        OCRStructuredText += "Current instrument:\n";

            //    }
            //    // find historical interest end
            //    if (line.ToLower().Contains("ric interests"))
            //    {
            //        historicalInterestStart = count;
            //        OCRStructuredText += "Historical instrument:\n";

            //    }
            //    int digitsCount = 0;
            //    foreach (char item in line)
            //    {
            //        if (Char.IsDigit(item))
            //            digitsCount++;
            //    }
            //    // if have more than 3 digits, we assume is a line with valuable information in it
            //    if (digitsCount > 3)
            //    {
            //        // should be the plan number
            //        if (currentInterestStart == 0 && historicalInterestStart == 0)
            //        {
            //            NZTitleSearchRelatedItem item = new NZTitleSearchRelatedItem(line, (count - planStart)) { };
            //            item.tidyUpTheResult();
            //            OCRResult.planNumbers.Add(item);
            //            OCRStructuredText += item.value + "\n";
            //        }
            //        else if (currentInterestStart > 0 && historicalInterestStart == 0)
            //        {
            //            instrumentSearch item = new instrumentSearch(line, (count - currentInterestStart)) { };
            //            OCRResult.currentInterests.Add(item);
            //            OCRStructuredText += item.instrumentType + " " + item.value + "\n";
            //        }
            //        else
            //        {
            //            instrumentSearch item = new instrumentSearch(line, (count - historicalInterestStart)) { };
            //            OCRResult.historicalInterests.Add(item);
            //            if (item.instrumentType != null)
            //            {
            //                OCRStructuredText += item.instrumentType + " " + item.value + "\n";
            //            }

            //        }
            //    }
            //    count++;

            //}
            return document;

        }

        public class NZTitleSearchRelated
        {

            public List<NZTitleSearchRelatedItem> planNumbers { get; set; }

            public List<instrumentSearch> currentInterests { get; set; }

            public List<instrumentSearch> historicalInterests { get; set; }

            public NZTitleSearchRelated()
            {
                planNumbers = new List<NZTitleSearchRelatedItem>();
                currentInterests = new List<instrumentSearch>();
                historicalInterests = new List<instrumentSearch>();
            }

        }
        public class NZTitleSearchRelatedItem
        {
            public NZTitleSearchRelatedItem(string value, int position)
            {
                this.value = value;
                this.position = position;
            }
            public string value { get; set; }
            public int position { get; set; }
            //instrument type start with alphabet
            //there is a space after instrument search type
            //instrument number can have only one number at the start
            //instrument number should have no space
            public void tidyUpTheResult()
            {
                string[] segements = value.Split(' ');
                if (segements.Length == 3)
                {
                    char leftChar = segements[1][segements[1].Length - 1];
                    char rightChar = segements[2][0];
                    if (char.IsDigit(leftChar) && char.IsDigit(rightChar))
                    {
                        value = segements[1] + segements[2];
                    }
                    else
                    {
                        //log
                    }
                }
                //find last occurence of letter and space over there 
                //TODO: there is a case when the instrument type is something like C243
                else if (segements.Length == 1)
                {
                    int count = 0;
                    char[] charArray = value.ToCharArray();
                    Array.Reverse(charArray);
                    foreach (char ch in charArray)
                    {
                        if (char.IsLetter(ch))
                        {
                            value.Insert(value.Length - count, " ");
                        }
                        count++;
                    }
                }
                // have not found with one space but the spacing is incorrect
                else if (segements.Length == 2)
                {
                    value = segements[1];
                }
                else
                {
                    //log
                }

            }
        }
        public class instrumentSearch : NZTitleSearchRelatedItem
        {
            public instrumentSearch(string value, int position) : base(value, position)
            {
                tidyUpTheResult();
            }
            public string instrumentType { get; set; }
            public new void tidyUpTheResult()
            {
                value = value.Replace(". ", ".");
                string[] segements = value.Split(' ');
                if (segements.Length == 3)
                {
                    char leftChar = segements[1][segements[1].Length - 1];
                    char rightChar = segements[2][0];
                    if (char.IsDigit(leftChar) && char.IsDigit(rightChar))
                    {
                        value = segements[1] + segements[2];
                        instrumentType = segements[0];
                    }
                    else
                    {
                        //Log.Error("unexpected OCR value {@value}", value);
                    }
                }
                //find last occurence of letter and space over there 
                //TODO: there is a case when the instrument type is something like C243
                else if (segements.Length == 1)
                {
                    int count = 0;
                    char[] charArray = value.ToCharArray();
                    Array.Reverse(charArray);
                    string originalValue = value;
                    foreach (char ch in charArray)
                    {
                        if (char.IsLetter(ch))
                        {
                            value = value.Substring(value.Length - count, count);
                            instrumentType = originalValue.Substring(0, originalValue.Length - count);
                            break;
                        }
                        count++;
                    }
                }
                // have not found with one space but the spacing is incorrect
                else if (segements.Length == 2)
                {
                    value = segements[1];
                    instrumentType = segements[0];
                }
                else
                {
                    //Log.Error("unexpected OCR value {@value}", value);
                }
            }
        }
    }
}
