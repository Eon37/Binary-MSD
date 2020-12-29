using System.Threading;
using System.Collections.Generic;
using System;

namespace lab5
{
    public class Parallel: Algorithm {
        int thrdNum;        

        Queue<int>[] thrdLists;

        public Parallel(int numOfThreads = 4) {
            thrdNum = numOfThreads;
            thrdLists = new Queue<int>[thrdNum];
            for (int i = 0; i < thrdNum; i++) {
                thrdLists[i] = new Queue<int>();
            }
        }
        public override void perform(int[] data) {
            unsortedData = data;
            sortedData = new int[data.Length];
            Thread[] thrds = new Thread[thrdNum];
            
            int block = (unsortedData.Length + 1)/thrdNum;
            int start = 0;
            int end = block;
            for (int i = 0; i < thrdNum; i++) {
                if(i == thrdNum - 1) {
                    thrds[i] = new Thread(performParallel);
                    thrds[i].Start(new int[]{start, unsortedData.Length, i});
                }
                else {
                    thrds[i] = new Thread(performParallel);
                    thrds[i].Start(new int[]{start, end, i});
                }
                start += block;
                end += block;
            }

            foreach (var thrd in thrds) {
                thrd.Join();
            }

            int resInd = 0;
            for (int i = 0; i < unsortedData.Length; i++) {
                sortedData[resInd++] = thrdLists[findMaxArrInd(thrdLists)].Dequeue();
            }
        }

        public void performParallel(object o) {
            int start = ((int[])o)[0];
            int end = ((int[])o)[1];
            int thrdId = ((int[])o)[2];

            List<int>[] list = new List<int>[2];
            list[0] = new List<int>();
            list[1] = new List<int>();

            for (int i = start; i < end; i++) {
                list[(~(unsortedData[i] >> 31) & 1)].Add(unsortedData[i]);
            }
            
            performNext(list[1], 30, ref thrdId);
            performNext(list[0], 30, ref thrdId);
        }

        private void performNext(List<int> list, int rank, ref int thrdId) {
            List<int>[] listNext = new List<int>[2];
            listNext[0] = new List<int>();
            listNext[1] = new List<int>();

            if (rank < 0) {
                foreach(var elem in list) thrdLists[thrdId].Enqueue(elem);
            }

            for (int i = 0; i < list.Count && rank >= 0; i++) {
                listNext[(list[i] >> rank) & 1].Add(list[i]);
            }            

            if (listNext[1].Count <= 1 && listNext[0].Count <= 1) {
                if(listNext[1].Count > 0) thrdLists[thrdId].Enqueue(listNext[1][0]);
                if(listNext[0].Count > 0) thrdLists[thrdId].Enqueue(listNext[0][0]);
                return;
            }           
            
            performNext(listNext[1], rank - 1, ref thrdId);
            performNext(listNext[0], rank - 1, ref thrdId);
        }

        private int findMaxArrInd(Queue<int>[] lists) {
            int max = Int32.MinValue;
            int maxInd = -1;
            for (int i = 0; i < lists.Length; i++) {
                if(lists[i].Count > 0) {
                    if (lists[i].Peek() >= max) {
                        max = lists[i].Peek();
                        maxInd = i;
                    }
                }
            }

            return maxInd;
        }

    }
}