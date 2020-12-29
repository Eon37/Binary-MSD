using System.Collections.Generic;

namespace lab5
{
    public class Serial : Algorithm {
        int resInd;

        public override void perform(int[] data) {
            resInd = 0;
            unsortedData = data;
            sortedData = new int[data.Length];
            List<int>[] list = new List<int>[2];
            list[0] = new List<int>();
            list[1] = new List<int>();

            for (int i = 0; i < unsortedData.Length; i++) {
                list[(~(unsortedData[i] >> 31) & 1)].Add(unsortedData[i]);
            }
            
            performNext(list[1], 30);
            performNext(list[0], 30);
        }
    
        private void performNext(List<int> list, int rank) {    
            List<int>[] listNext = new List<int>[2];
            listNext[0] = new List<int>();
            listNext[1] = new List<int>();

            if (rank < 0) {
                foreach(var elem in list) sortedData[resInd++] = elem;
            }

            for (int i = 0; i < list.Count && rank >= 0; i++) {
                listNext[(list[i] >> rank) & 1].Add(list[i]);
            }

            if (listNext[1].Count <= 1 && listNext[0].Count <= 1) {
                if(listNext[1].Count > 0) sortedData[resInd++] = listNext[1][0];
                if(listNext[0].Count > 0) sortedData[resInd++] = listNext[0][0];
                return;
            } 
            
            performNext(listNext[1], rank - 1);
            performNext(listNext[0], rank - 1);

        }
    }
    
}