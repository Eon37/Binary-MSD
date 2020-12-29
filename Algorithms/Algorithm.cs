using System;

namespace lab5
{
    public abstract class Algorithm {
        protected static int[] unsortedData;
        protected static int[] sortedData;

        public static int[] fillData(int numOfElements) {
            int[] arr = new int[numOfElements];
            arr = new int[numOfElements];
            Random r = new Random();

            for (int i = 0; i < numOfElements; i++) {
                arr[i] = r.Next(Int32.MinValue, Int32.MaxValue);
            }

            return arr;
        }

        public static int[] fillDataAsc(int numOfElements) {
            int[] arr = new int[numOfElements];
            arr = new int[numOfElements];
            Random r = new Random();

            for (int i = 0; i < numOfElements; i++) {
                arr[i] = i - numOfElements/2 + 11011;
            }

            return arr;
        }

        public static int[] fillDataDesc(int numOfElements) {
            int[] arr = new int[numOfElements];
            arr = new int[numOfElements];
            Random r = new Random();

            for (int i = 0; i < numOfElements; i++) {
                arr[i] = (numOfElements/2 - i) + 11010;
            }

            return arr;
        }

        public abstract void perform(int[] data);
    }
}