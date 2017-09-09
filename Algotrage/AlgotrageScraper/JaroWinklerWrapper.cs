using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F23.StringSimilarity;

namespace AlgotrageScraper
{
    static class JaroWinklerWrapper
    {
        private static JaroWinkler _jw;
        private static double _similarityThreshold;

        static JaroWinklerWrapper()
        {
            _jw = new JaroWinkler();
            _similarityThreshold = 2.0 / 3.0;
        }

        public static bool AreSimilar(string str1, string str2)
        {
            return _jw.Similarity(str1, str2) > _similarityThreshold;
        }
    }
}
