using System;
using System.Configuration;
using System.Collections.Generic;

// Каждый документ — это список токенов. То есть List<string>.
// Вместо этого будем использовать псевдоним DocumentTokens.
// Это поможет избежать сложных конструкций:
// вместо List<List<string>> будет List<DocumentTokens>
using DocumentTokens = System.Collections.Generic.List<string>;

namespace Antiplagiarism
{
    public class LevenshteinCalculator
    {
        public List<ComparisonResult> CompareDocumentsPairwise(List<DocumentTokens> documents)
        {
            var result = new List<ComparisonResult>();
            for (int i = 0; i < documents.Count; i++)
                for (int j = i + 1; j < documents.Count; j++)
                    result.Add(CompareDocuments(documents[i], documents[j]));
            return result;
        }

        ComparisonResult CompareDocuments(DocumentTokens first, DocumentTokens second)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");
            double diff;
            double[,] m = new double[first.Count + 1, second.Count + 1];

            for (int i = 0; i <= first.Count; i++) { m[i, 0] = i; }
            for (int j = 0; j <= second.Count; j++) { m[0, j] = j; }

            for (int i = 1; i <= first.Count; i++)
                for (int j = 1; j <= second.Count; j++)
                {
                    diff = (first[i - 1] == second[j - 1]) ? 0 : TokenDistanceCalculator.GetTokenDistance(first[i - 1], second[j - 1]);

                    m[i, j] = Math.Min(Math.Min(m[i - 1, j] + 1,
                                             m[i, j - 1] + 1),
                                             m[i - 1, j - 1] + diff);
                }
            return new ComparisonResult(first, second, m[first.Count, second.Count]);
        }
    }
}
