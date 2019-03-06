﻿

namespace Comparison
{
    class DiffClass
    {
        TextsWithCheck text;
        private readonly string text1;
        private readonly string text2;
       
        // The Constructor gets arguments of command line and give those to TextsWithCheck constructor
        public DiffClass(string[] argumentsOfCmd)
        {
            //
            text = new TextsWithCheck(argumentsOfCmd);
            text1 = text.Text1;
            text2 = text.Text2;
        }

        // Longest common subsequence
        private int[,] LCS (string text1, string text2)
        {
            int[,] matrixOfLCS = new int[text1.Length + 1, text2.Length + 1];
            for (int i = 1; i <= text1.Length; i++)
                for (int j = 1; j <= text2.Length; j++)
                {
                    if (text1[i - 1] == text2[j - 1])
                        matrixOfLCS[i, j] = matrixOfLCS[i - 1, j - 1] + 1;
                    else if (matrixOfLCS[i - 1, j] > matrixOfLCS[i, j - 1])
                        matrixOfLCS[i, j] = matrixOfLCS[i - 1, j];
                    else
                        matrixOfLCS[i, j] = matrixOfLCS[i, j - 1];
                }
            return matrixOfLCS;
            
        }
        
        // i - lenght of text1; j - length of text2
        // This method returns a string where lost symbols write inside "()" and exited symbols inside "[]"
        private string Differences (int[,] matrixOfLCS, string text1, string text2, int i, int j)
        {
            var saverDiff = "";
            // EQUAL
            if (i > 0 && j > 0 && text1[i - 1] == text2[j - 1])
            {
                saverDiff = Differences(matrixOfLCS, text1, text2, i - 1, j - 1);
                return saverDiff + text1[i - 1];
            }
            // DELETE
            else if (i > 0 && (j == 0 || (matrixOfLCS[i, j - 1] <= matrixOfLCS[i - 1, j])))
            {
                saverDiff = Differences(matrixOfLCS, text1, text2, i - 1, j);
                if (saverDiff[saverDiff.Length - 1] == ')')
                    return saverDiff.Trim(')') + text1[i - 1] + ")";
                else
                    return saverDiff + "(" + text1[i - 1] + ")";

            }
            // INSERT
            else if (j > 0 && (i == 0 || (matrixOfLCS[i, j - 1] > matrixOfLCS[i - 1, j])))
            {

                saverDiff = Differences(matrixOfLCS, text1, text2, i, j - 1);
                if (saverDiff[saverDiff.Length - 1] == ']')
                    return saverDiff.Trim(']') + text2[j - 1] + "]";
                else
                    return saverDiff + "[" + text2[j - 1] + "]";
            }
            return saverDiff;
        }

        // This method returns the string of Differences between two strings
        public string GetStringOfDiff()
        {
            if (text1 != null && text2 != null)
                return Differences(LCS(text1, text2), text1, text2, text1.Length, text2.Length);
            else return ""; 
        }

        

    }
}